using LeafSQL.Library.Payloads.Actions.Base;
using LeafSQL.Library.Payloads.Models;
using System;

namespace LeafSQL.Library.Payloads.Actions
{
    public class ActionRequestLogin : ActionGeneric
    {
        public ActionRequestLogin(Guid sessionId) : base(sessionId) { }
        public Login Login { get; set; }
    }
}

