using LeafSQL.Library.Payloads.Actions.Base;
using System;

namespace LeafSQL.Library.Payloads
{
    public class ActionRequestCreateIndex : ActionGenericObject
    {
        public ActionRequestCreateIndex(Guid sessionId) : base(sessionId) { }

        public Payloads.Models.Index @Object { get; set; }
    }
}
