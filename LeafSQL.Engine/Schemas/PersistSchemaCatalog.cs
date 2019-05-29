using LeafSQL.Engine.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeafSQL.Engine.Schemas
{
    public class PersistSchemaCatalog : ICatalog<PersistSchema>
    {
        public List<PersistSchema> Collection = new List<PersistSchema>();

        [JsonIgnore]
        public Object LockObject { get; set; } = new object();

        public void Add(PersistSchema namespaceMeta)
        {
            this.Collection.Add(namespaceMeta);
        }

        public bool ContainsName(string name)
        {
            foreach (var item in Collection)
            {
                if (string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public PersistSchema GetByName(string name)
        {
            foreach (var item in Collection)
            {
                if (string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            return null;
        }

        public PersistSchema GetById(Guid id)
        {
            return (from o in Collection where o.Id == id select o).FirstOrDefault();
        }

        public List<PersistSchema> Clone()
        {
            var catalog = new PersistSchemaCatalog();

            lock (LockObject)
            {
                foreach (var obj in Collection)
                {
                    catalog.Collection.Add(obj.Clone());
                }
            }

            return catalog.Collection;
        }
    }
}
