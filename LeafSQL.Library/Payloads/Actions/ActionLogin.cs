using LeafSQL.Library.Payloads.Actions.Base;
using System;

namespace LeafSQL.Library.Payloads.Actions
{
    public class ActionLogin : ActionGeneric
    {
        public ActionLogin(Guid sessionId) : base(sessionId) { }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
