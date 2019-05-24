using LeafSQL.Library.Payloads.Actions.Base;
using System;

namespace LeafSQL.Library.Payloads
{
    public class ActionRequestExecuteNonQuery : ActionGeneric
    {
        public ActionRequestExecuteNonQuery(Guid sessionId) : base(sessionId) { }

        public string Statement { get; set; }
    }
}
