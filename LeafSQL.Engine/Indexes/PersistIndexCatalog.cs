﻿using LeafSQL.Engine.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeafSQL.Engine.Indexes
{
    [Serializable]
    public class PersistIndexCatalog : ICatalog<PersistIndex>
    {
        public List<PersistIndex> Collection = new List<PersistIndex>();

        [JsonIgnore]
        public Object LockObject { get; set; } = new object();

        [JsonIgnore]
        public string DiskPath { get; set; }

        public void Remove(PersistIndex item)
        {
            Collection.Remove(item);
        }

        public void Add(PersistIndex item)
        {
            this.Collection.Add(item);
        }

        public PersistIndex GetById(Guid id)
        {
            return (from o in Collection where o.Id == id select o).FirstOrDefault();
        }

        public PersistIndex GetByName(string name)
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

        public List<PersistIndex> Clone()
        {
            lock (LockObject)
            {
                var catalog = new PersistIndexCatalog();

                foreach (var obj in Collection)
                {
                    catalog.Collection.Add(obj.Clone());
                }

                return catalog.Collection;
            }
        }
    }
}