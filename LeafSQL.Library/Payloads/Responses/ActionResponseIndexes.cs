using System.Collections.Generic;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponseIndexes : IActionResponse
    {
        public List<Index> List { get; set; }

        public ActionResponseIndexes()
        {
            List = new List<Index>();
        }

        public void Add(Index value)
        {
            List.Add(value);
        }
    }
}
