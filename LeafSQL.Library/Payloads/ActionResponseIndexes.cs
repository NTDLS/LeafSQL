using System.Collections.Generic;

namespace LeafSQL.Library.Payloads
{
    public class ActionResponseIndexes : ActionResponse
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
