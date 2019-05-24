using LeafSQL.Engine.Interfaces;
using LeafSQL.Library.Payloads.Models;

namespace LeafSQL.Engine.Indexes
{
    public class PersistIndexAttribute : IPayloadCompatible<PersistIndexAttribute, IndexAttribute>
    {
        public string Name { get; set; }

        public static PersistIndexAttribute FromPayload(IndexAttribute indexAttribute)
        {
            return new PersistIndexAttribute()
            {
                Name = indexAttribute.Name
            };
        }

        public PersistIndexAttribute Clone()
        {
            return new PersistIndexAttribute()
            {
                Name = Name
            };
        }

        public IndexAttribute ToPayload()
        {
            return new IndexAttribute()
            {
                 Name = this.Name
            };
        }

    }
}
