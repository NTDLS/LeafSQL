using LeafSQL.Library.Payloads.Models;
using System.Collections.Generic;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponseSchemas : IActionResponse
    {
        public List<Schema> List { get; set; }

        public ActionResponseSchemas()
        {
            List = new List<Schema>();
        }

        public void Add(Schema value)
        {
            List.Add(value);
        }
    }
}
