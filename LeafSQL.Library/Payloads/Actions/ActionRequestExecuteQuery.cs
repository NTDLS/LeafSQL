using LeafSQL.Library.Payloads.Actions;
using System;

namespace LeafSQL.Library.Payloads
{
    public class ActionRequestExecuteQuery : ActionRequestBase
    {
        public ActionRequestExecuteQuery(Guid sessionId) : base(sessionId) { }

        public string Statement { get; set; }
    }
}
