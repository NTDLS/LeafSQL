using LeafSQL.Library.Payloads.Actions;
using LeafSQL.Library.Payloads.Models;
using System;

namespace LeafSQL.Library.Payloads.Actions
{
    public class ActionRequestLogin : ActionRequestBase
    {
        public ActionRequestLogin(Guid sessionId) : base(sessionId) { }
        public Login Login { get; set; }
    }
}

