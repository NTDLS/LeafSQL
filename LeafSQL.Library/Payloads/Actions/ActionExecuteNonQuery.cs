using LeafSQL.Library.Payloads.Actions.Base;
using System;

namespace LeafSQL.Library.Payloads
{
    public class ActionExecuteNonQuery : ActionGeneric
    {
        public ActionExecuteNonQuery(Guid sessionId) : base(sessionId) { }

        public string Statement { get; set; }
    }
}
