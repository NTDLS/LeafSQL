using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.UI
{
    public class Types
    {
        public enum TreeNodeType
        {
            Server,
            Schemas,
            Schema,
            Logins,
            Login,
            Indexes,
            Index,
            IndexAttribute
        }
    }
}
