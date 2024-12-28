using System;

public class QueryResult
{
    public string ServerName { get; set; }
    public string Version { get; set; }
    public string Message { get; set; }
    public TimeSpan ExecutionTime { get; set; }
}