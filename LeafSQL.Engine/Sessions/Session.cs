using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.Engine.Sessions
{
    public class Session
    {
        public UInt64 ProcessId { get; set; }
        public Guid SessionId { get; set; }
        public Guid LoginId { get; set; }

        public string InstanceKey
        {
            get
            {
                return $"{LoginId}.{SessionId}.{ProcessId}";
            }
        }
    }
}
