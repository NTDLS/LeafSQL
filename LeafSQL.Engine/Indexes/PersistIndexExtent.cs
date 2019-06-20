using Newtonsoft.Json;
using ProtoBuf;
using System.Collections.Generic;

namespace LeafSQL.Engine.Indexes
{
    [ProtoContract]
    public class PersistIndexExtent
    {
        [ProtoMember(1)]
        public List<PersistIndexLeaf> Leaves = new List<PersistIndexLeaf>();

        [JsonIgnore]
        public int Count
        {
            get
            {
                return Leaves.Count;
            }
        }

        public PersistIndexLeaf AddNewleaf(string key)
        {
            key = key.ToLower();
            var leaf = new PersistIndexLeaf(key);
            Leaves.Add(leaf);
            return leaf;
        }

        public IEnumerator<PersistIndexLeaf> GetEnumerator()
        {
            int position = 0;
            while (position < Leaves.Count)
            {
                yield return this[position++];
            }
        }

        public PersistIndexLeaf this[int index]
        {
            get
            {
                return Leaves[index];
            }
        }
    }
}
