using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.Engine.Indexes
{
    [ProtoContract]
    public class PersistIndexLeaves
    {
        [ProtoMember(1)]
        public List<PersistIndexLeaf> Entries = new List<PersistIndexLeaf>();

        [JsonIgnore]
        public int Count
        {
            get
            {
                return Entries.Count;
            }
        }

        public PersistIndexLeaf AddNewleaf(string key)
        {
            key = key.ToLower(); //TODO: Make this optional for selective case sensitivity?
            var leaf = new PersistIndexLeaf(key);
            Entries.Add(leaf);
            return leaf;
        }

        public IEnumerator<PersistIndexLeaf> GetEnumerator()
        {
            int position = 0;
            while (position < Entries.Count - 1)
            {
                yield return this[++position];
            }
        }

        public PersistIndexLeaf this[int index]    
        {
            get
            {
                return Entries[index];
            }
        }
    }
}
