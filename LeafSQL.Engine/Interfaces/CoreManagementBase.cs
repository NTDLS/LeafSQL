using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.Engine.Interfaces
{
    public class CoreManagementBase
    {
        public Core core { get; set; }

        public CoreManagementBase(Core core)
        {
            this.core = core;
        }
    }
}
