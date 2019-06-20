using System.Collections.Generic;
using System.Linq;

namespace LeafSQL.Engine.Indexes
{
    public class PotentialIndex
    {
        public List<IndexHandledCondition> IndexHandledConditions { get; set; }
        public PersistIndex Index { get; set; }

        public string FirstAttributeName
        {
            get
            {
                return Index.Attributes.FirstOrDefault()?.Name;
            }
        }

        public PotentialIndex(PersistIndex index, List<IndexHandledCondition> indexHandledConditions)
        {
            this.Index = index;
            this.IndexHandledConditions = indexHandledConditions;
        }
    }
}

