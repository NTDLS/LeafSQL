using System;
using System.Collections.Generic;
using System.Linq;

namespace LeafSQL.Engine.Indexes
{
    public class PotentialIndex
    {
        public List<string> HandledKeyNames { get; set; }
        public PersistIndex Index { get; set; }
        public bool Eliminate { get; set; }

        public string Key
        {
            get
            {
                return "[" + string.Join("]-[", HandledKeyNames) + "]";
            }
        }

        public bool AreAllAttributesUsed
        {
            get
            {
                return Index.Attributes.Count == HandledKeyNames.Count;
            }
        }

        /// <summary>
        /// Returns true if this index would fully cover all of the attriutes of the passed index.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsFullyCoveredBy(PotentialIndex other)
        {
            if (this.Index.Attributes.Count < other.Index.Attributes.Count)
            {
                return true;
            }

            int attribsToCheck = Math.Max(this.Index.Attributes.Count, other.Index.Attributes.Count);

            for (int i = 0; i < attribsToCheck; i++)
            {
                if (other.Index.Attributes[i].Name != this.Index.Attributes[i].Name)
                {
                    return false;
                }
            }

            return true;
        }

        //Returns a list of attributes that are covered by the passed index but not by this index.
        public List<string> GetDifferenceOfAttributes(PotentialIndex other)
        {
            var thisAttributes = this.Index.Attributes.Select(o => o.Name);
            var otherAttributes = other.Index.Attributes.Select(o => o.Name);

            return otherAttributes.Where(p => !thisAttributes.Any(p2 => p2 == p)).ToList();
        }

        public PotentialIndex(PersistIndex index, List<string> handledKeyNames)
        {
            this.Index = index;
            this.HandledKeyNames = handledKeyNames;
        }
    }
}
