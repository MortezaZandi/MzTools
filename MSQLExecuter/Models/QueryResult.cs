using System;
using System.Collections.Generic;
using System.Data;

namespace MSQLExecuter.Models
{
    public class QueryResult
    {
        public List<QueryColumn> Columns { get; set; } = new List<QueryColumn>();
        public List<object[]> Rows { get; set; } = new List<object[]>();
        public int RowsAffected { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public List<DataTable> ResultSets { get; } = new List<DataTable>();
        public Exception Error { get; set; }
        public bool HasError => Error != null;
    }

    public class QueryColumn
    {
        public string Name { get; set; }
        public Type Type { get; set; }
    }
} 