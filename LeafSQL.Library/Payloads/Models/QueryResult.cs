using System.Collections.Generic;

namespace LeafSQL.Library.Payloads.Models
{
    public class QueryResult
    {
        public List<QueryColumn> Columns { get; set; }
        public List<QueryRow> Rows { get; set; }

        public QueryResult()
        {
            Columns = new List<QueryColumn>();
            Rows = new List<QueryRow>();
        }
    }
}