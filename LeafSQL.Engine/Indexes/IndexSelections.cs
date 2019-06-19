using LeafSQL.Engine.Query;
using System.Collections.Generic;

namespace LeafSQL.Engine.Indexes
{
    public class IndexSelections: List<IndexSelection>
    {
        public List<string> UnhandledKeys { get; set; }
        public Conditions Conditions { get; set; }

        public IndexSelections()
        {
            UnhandledKeys = new List<string>();
        }

        public IndexSelections(Conditions conditions)
        {
            UnhandledKeys = new List<string>();
            Conditions = conditions;
        }
    }
}
