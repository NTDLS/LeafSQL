using LeafSQL.Library.Payloads.Actions.Base;
using System;

namespace LeafSQL.Library.Payloads.Actions
{
    public class ActionStoreDocument : ActionGenericObject
    {
        public ActionStoreDocument(Guid sessionId) : base(sessionId) { }

        public Document @Object { get; set; }
    }
}
