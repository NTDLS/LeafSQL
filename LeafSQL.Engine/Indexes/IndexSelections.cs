using LeafSQL.Engine.Query;
using System.Collections.Generic;
using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Indexes
{
    public class IndexSelections : List<IndexSelection>
    {
        /// <summary>
        /// The condition keys that this index cannot satisify.
        /// </summary>
        public List<string> UnhandledKeys { get; set; }

        /// <summary>
        /// The condition that this index can satisfiy.
        /// </summary>
        public List<Condition> Conditions { get; set; }

        public ConditionType ConditionGroupType { get; set; }

        public IndexSelections()
        {
            UnhandledKeys = new List<string>();
        }

        public IndexSelections(Conditions conditions)
        {
            UnhandledKeys = new List<string>();
            Conditions = conditions.Root;
            ConditionGroupType = conditions.ConditionGroupType;
        }
    }
}
