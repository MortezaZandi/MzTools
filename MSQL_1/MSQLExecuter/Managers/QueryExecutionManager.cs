using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

public class QueryExecutionManager
{
    private readonly Dictionary<string, CancellationTokenSource> _runningQueries = 
        new Dictionary<string, CancellationTokenSource>();
    
    public async Task<QueryResult> ExecuteQueryAsync(string queryText, ConnectionInfo connection, string queryId)
    {
        var result = new QueryResult();
        var cts = new CancellationTokenSource();
        _runningQueries[queryId] = cts;
        
        try
        {
            using (var conn = connection.CreateConnection())
            {
                await conn.OpenAsync(cts.Token);
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = queryText;
                    cmd.CommandTimeout = 300; // 5 minutes
                    
                    var startTime = DateTime.Now;
                    using (var reader = await cmd.ExecuteReaderAsync(cts.Token))
                    {
                        do
                        {
                            var table = new DataTable();
                            table.Load(reader);
                            result.ResultSets.Add(table);
                        } while (!reader.IsClosed && await reader.NextResultAsync(cts.Token));
                    }
                    result.ExecutionTime = DateTime.Now - startTime;
                }
            }
        }
        catch (Exception ex)
        {
            result.Error = ex;
        }
        finally
        {
            _runningQueries.Remove(queryId);
        }
        
        return result;
    }
    
    public void CancelQuery(string queryId)
    {
        if (_runningQueries.TryGetValue(queryId, out var cts))
        {
            cts.Cancel();
        }
    }
}

public class QueryResult
{
    public List<DataTable> ResultSets { get; } = new List<DataTable>();
    public Exception Error { get; set; }
    public TimeSpan ExecutionTime { get; set; }
    public bool HasError => Error != null;
} 