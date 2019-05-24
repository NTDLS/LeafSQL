using System;
using System.Collections.Generic;
using LeafSQL.Library.Payloads;
using Newtonsoft.Json;

namespace LeafSQL.Engine.Indexes
{
    [Serializable]
    public class PersistIndex
    {
        public List<PersistIndexAttribute> Attributes { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modfied { get; set; }
        public bool IsUnique { get; set; }


        [JsonIgnore]
        public string DiskPath { get; set; }


        public PersistIndex()
        {
            Attributes = new List<PersistIndexAttribute>();
        }

        public PersistIndex Clone()
        {
            return new PersistIndex
            {
                Id = Id,
                Name = Name,
                Created = Created,
                Modfied = Modfied,
                IsUnique = IsUnique
            };
        }

        public Library.Payloads.Index ToPayload()
        {
            var result = new Library.Payloads.Index()
            {
                Name = this.Name,
                Created = this.Created,
                Id = this.Id,
                IsUnique = this.IsUnique,
                Modfied = this.Modfied
            };

            foreach (var att in this.Attributes)
            {
                result.Attributes.Add(att.ToPayload());
            }

            return result;
        }

        public void AddAttribute(string name)
        {
            AddAttribute(new PersistIndexAttribute()
            {
                Name = name
            });
        }
        public void AddAttribute(PersistIndexAttribute attribute)
        {
            Attributes.Add(attribute);
        }

        static public PersistIndex FromPayload(Index index)
        {
            var persistIndex = new PersistIndex()
            {
                Id = index.Id,
                Name = index.Name,
                Created = index.Created,
                Modfied = index.Modfied,
                IsUnique = index.IsUnique
            };

            foreach (var indexAttribute in index.Attributes)
            {
                persistIndex.AddAttribute(PersistIndexAttribute.FromPayload(indexAttribute));
            }

            return persistIndex;
        }
    }
}
