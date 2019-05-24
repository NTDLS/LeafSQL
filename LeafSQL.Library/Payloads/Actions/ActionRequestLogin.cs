using LeafSQL.Library.Payloads.Actions.Base;
using System;

namespace LeafSQL.Library.Payloads.Actions
{
    public class ActionRequestLogin : ActionGeneric
    {
        public ActionRequestLogin(Guid sessionId) : base(sessionId) { }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
