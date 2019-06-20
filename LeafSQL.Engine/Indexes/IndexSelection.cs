using System.Collections.Generic;

namespace LeafSQL.Engine.Indexes
{
    public class IndexSelection
    {
        public PersistIndex Index;
        public List<IndexHandledCondition> IndexHandledConditions { get; set; }

        public IndexSelection(PersistIndex index, List<IndexHandledCondition> indexHandledConditions)
        {
            this.IndexHandledConditions = indexHandledConditions;
            this.Index = index;
        }
    }
}