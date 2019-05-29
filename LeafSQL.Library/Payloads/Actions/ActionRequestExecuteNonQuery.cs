using LeafSQL.Library.Payloads.Actions;
using System;

namespace LeafSQL.Library.Payloads
{
    public class ActionRequestExecuteNonQuery : ActionRequestBase
    {
        public ActionRequestExecuteNonQuery(Guid sessionId) : base(sessionId) { }

        public string Statement { get; set; }
    }
}
