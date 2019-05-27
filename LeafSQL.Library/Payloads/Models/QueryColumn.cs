using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.Library.Payloads.Models
{
    public class QueryColumn
    {
        public string Name { get; set; }

        public QueryColumn(string name)
        {
            Name = name;
        }
    }
}
