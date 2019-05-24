using Newtonsoft.Json;
using System;
using LeafSQL.Engine.Interfaces;
using LeafSQL.Library.Payloads.Models;

namespace LeafSQL.Engine.Schemas
{
    public class PersistSchema : IPayloadCompatible<PersistSchema, Schema>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        [JsonIgnore]
        public string DiskPath { get; set; }
        [JsonIgnore]
        public string VirtualPath { get; set; }
        [JsonIgnore]
        public bool Exists { get; set; }

        public Library.Payloads.Models.Schema ToPayload()
        {
            return new Library.Payloads.Models.Schema()
            {
                Id = Id,
                Name = Name
            };
        }

        public PersistSchema Clone()
        {
            return new PersistSchema
            {
                DiskPath = DiskPath,
                Exists = Exists,
                Id = Id,
                Name = Name,
                VirtualPath = VirtualPath
            };
        }
    }
}
