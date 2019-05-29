using LeafSQL.Engine.Exceptions;
using LeafSQL.Engine.Interfaces;
using LeafSQL.Library.Payloads.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeafSQL.Engine.Security
{
    [Serializable]
    public class PersistLoginCatalog: ICatalog<PersistLogin>
    {
        [JsonProperty]
        private List<PersistLogin> Collection = new List<PersistLogin>();

        [JsonIgnore]
        public Object LockObject { get; set; } = new object();

        [JsonIgnore]
        public string DiskPath { get; set; }

        public List<PersistLogin> Clone()
        {
            var catalog = new PersistLoginCatalog();

            lock (LockObject)
            {
                foreach (var obj in Collection)
                {
                    catalog.Collection.Add(obj.Clone());
                }
            }

            return catalog.Collection;
        }

        /// <summary>
        /// Adds a new login, returns its new ID.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Guid Add(PersistLogin item)
        {
            lock (LockObject)
            {
                item.Id = Guid.NewGuid();
                this.Collection.Add(item);
                return item.Id;
            }
        }

        public void Remove(PersistLogin item)
        {
            lock (LockObject)
            {
                Collection.Remove(item);
            }
        }
        
        public PersistLogin GetById(Guid id)
        {
            lock (LockObject)
            {
                return (from o in Collection where o.Id == id select o).FirstOrDefault();
            }
        }

        public void SetLoginPasswordByName(Login login)
        {
            lock (LockObject)
            {
                var persistLogin = GetByName(login.Name);

                if (persistLogin == null)
                {
                    throw new LeafSQLExceptionBase("Login with the specified name could not be located.");
                }

                persistLogin.PasswordHash = login.PasswordHash;
            }
        }

        public void DeleteLoginByName(string name)
        {
            lock (LockObject)
            {
                var persistLogin = GetByName(name);

                if (persistLogin == null)
                {
                    throw new LeafSQLExceptionBase("Login with the specified name could not be located.");
                }

                Collection.Remove(persistLogin);
            }
        }

        public PersistLogin GetByName(string name)
        {
            lock (LockObject)
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
        }

        public PersistLogin GetByNameandPasswordHash(string name, string passwordHash)
        {
            lock (LockObject)
            {
                return Collection.Where(o =>
                                    o.Name.ToLower() == name.ToLower()
                                    && o.PasswordHash.ToLower() == passwordHash.ToLower()
                                    ).FirstOrDefault();
            }
        }
    }
}