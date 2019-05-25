using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeafSQL.Engine.Security
{
    [Serializable]
    public class PersistLoginCatalog
    {
        public List<PersistLogin> Collection = new List<PersistLogin>();

        [JsonIgnore]
        public string DiskPath { get; set; }

        public void Remove(PersistLogin item)
        {
            Collection.Remove(item);
        }

        public void Add(PersistLogin item)
        {
            this.Collection.Add(item);
        }

        public PersistLogin GetById(Guid id)
        {
            return (from o in Collection where o.Id == id select o).FirstOrDefault();
        }

        public PersistLogin GetByName(string name)
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

        public PersistLoginCatalog Clone()
        {
            var catalog = new PersistLoginCatalog();

            lock (this)
            {
                foreach (var obj in Collection)
                {
                    catalog.Collection.Add(obj.Clone());
                }
            }

            return catalog;
        }
    }
}
