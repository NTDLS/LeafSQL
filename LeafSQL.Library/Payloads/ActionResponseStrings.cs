using System;
using System.Collections.Generic;

namespace LeafSQL.Library.Payloads
{
    public class ActionResponseStrings : ActionResponse
    {
        public List<string> Values { get; set; }

        public void Add(string value)
        {
            Values.Add(value);
        }
    }
}
