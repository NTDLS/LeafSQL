using System.Collections.Generic;

namespace LeafSQL.Engine.Indexes
{
    public class PotentialIndex
    {
        public List<string> HandledKeyNames { get; set; }
        public PersistIndex Index { get; set; }
        public bool Eliminate { get; set; }

        public string FirstAttributeName
        {
            get
            {
                return Index.Attributes[0].Name;
            }
        }

        public PotentialIndex(PersistIndex index, List<string> handledKeyNames)
        {
            this.Index = index;
            this.HandledKeyNames = handledKeyNames;
        }
    }
}
