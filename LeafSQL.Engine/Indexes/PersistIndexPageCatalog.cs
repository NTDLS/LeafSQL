using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.Engine.Indexes
{
    [ProtoContract]
    public class PersistIndexPageCatalog
    {
        [ProtoMember(1)]
        public PersistIndexExtent Leaves = new PersistIndexExtent();
    }

}
