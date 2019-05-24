using LeafSQL.Library.Payloads.Actions.Base;
using System;

namespace LeafSQL.Library.Payloads
{
    public class ActionCreateIndex : ActionGenericObject
    {
        public ActionCreateIndex(Guid sessionId) : base(sessionId) { }

        public Index @Object { get; set; }
    }
}
