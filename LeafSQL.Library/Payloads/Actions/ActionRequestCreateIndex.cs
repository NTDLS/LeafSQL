using LeafSQL.Library.Payloads.Actions;
using System;

namespace LeafSQL.Library.Payloads
{
    public class ActionRequestCreateIndex : ActionGenericBase
    {
        public ActionRequestCreateIndex(Guid sessionId) : base(sessionId) { }

        public Payloads.Models.Index @Object { get; set; }
    }
}
