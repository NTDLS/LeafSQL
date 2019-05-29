using LeafSQL.Library.Payloads.Actions;
using LeafSQL.Library.Payloads.Models;
using System;

namespace LeafSQL.Library.Payloads.Actions
{
    public class ActionRequestStoreDocument : ActionGenericObject
    {
        public ActionRequestStoreDocument(Guid sessionId) : base(sessionId) { }

        public Payloads.Models.Document @Object { get; set; }
    }
}
