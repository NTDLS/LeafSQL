using System.Collections.Generic;

namespace LeafSQL.Engine.Indexes
{
    public class IndexSelections: List<IndexSelection>
    {
        public List<string> UnhandledKeys { get; set; }

        public IndexSelections()
        {
            UnhandledKeys = new List<string>();
        }
    }
}
