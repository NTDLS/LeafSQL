using LeafSQL.Library.Payloads.Actions.Base;
using System;

namespace LeafSQL.Library.Payloads
{
    public class ActionRequestExecuteQuery : ActionGeneric
    {
        public ActionRequestExecuteQuery(Guid sessionId) : base(sessionId) { }

        public string Statement { get; set; }
    }
}
