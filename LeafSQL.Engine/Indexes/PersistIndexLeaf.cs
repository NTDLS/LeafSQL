using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.Engine.Indexes
{
    [ProtoContract]
    public class PersistIndexLeaf
    {
        [ProtoMember(1)]
        public string Key { get; set; }
        [ProtoMember(2)]
        public HashSet<Guid> DocumentIDs = null;
        [ProtoMember(3)]
        public PersistIndexLeaves Leaves = new PersistIndexLeaves();


        /// <summary>
        /// Returns true if this is the level of the index that contains the document IDs.
        /// </summary>
        [ProtoIgnore]
        public bool IsBottom
        {
            get
            {
                return (DocumentIDs != null && DocumentIDs.FirstOrDefault() != null);
            }
        }

        public PersistIndexLeaf()
        {

        }

        public PersistIndexLeaf(string key)
        {
            Key = key;
        }
    }
}
