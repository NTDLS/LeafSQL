using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.Library.Payloads.Models
{
    public class QueryRow
    {
        public List<string> Values { get; set; }

        public QueryRow(List<string> values)
        {
            Values = values;
        }

        public QueryRow()
        {
            Values = new List<string>();
        }

        public void Add(string value)
        {
            Values.Add(value);
        }
    }
}
