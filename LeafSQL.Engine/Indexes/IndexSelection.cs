using System;
using System.Collections.Generic;

namespace LeafSQL.Engine.Indexes
{
    public class IndexSelection
    {
        public PersistIndex Index;
        public List<string> HandledKeyNames { get; set; }
        public Guid HandledConditionID { get; set; }

        public IndexSelection(PersistIndex index, List<string> handledKeyNames, Guid handledConditionID)
        {
            this.HandledKeyNames = handledKeyNames;
            this.HandledConditionID = handledConditionID;
            this.Index = index;
        }
    }
}
