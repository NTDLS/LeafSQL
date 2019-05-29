using LeafSQL.Engine.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeafSQL.Engine.Documents
{
    [Serializable]
    public class PersistDocumentCatalog : ICatalog<PersistDocumentMeta>
    {
        public List<PersistDocumentMeta> Collection = new List<PersistDocumentMeta>();

        [JsonIgnore]
        public Object LockObject { get; set; } = new object();

        public PersistDocumentMeta Add(PersistDocument document)
        {
                var catalogItem = new PersistDocumentMeta()
                {
                    Id = document.Id
                };

                this.Collection.Add(catalogItem);

                return catalogItem;
        }

        public void Remove(PersistDocumentMeta item)
        {
                Collection.Remove(item);
        }

        public void Add(PersistDocumentMeta item)
        {
                this.Collection.Add(item);
        }

        public PersistDocumentMeta GetById(Guid id)
        {
                return (from o in Collection where o.Id == id select o).FirstOrDefault();
        }

        public List<PersistDocumentMeta> Clone()
        {
            var catalog = new PersistDocumentCatalog();

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

