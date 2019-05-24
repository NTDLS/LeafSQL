using System.Collections.Generic;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponseStrings : IActionResponse
    {
        public List<string> Values { get; set; }

        public void Add(string value)
        {
            Values.Add(value);
        }
    }
}
