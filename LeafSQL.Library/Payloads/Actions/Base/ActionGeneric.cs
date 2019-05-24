using System;

namespace LeafSQL.Library.Payloads.Actions.Base
{
    public class ActionGeneric
    {
        public ActionGeneric(Guid sessionId)
        {
            this.SessionId = sessionId;
        }

        /// <summary>
        /// The Id of the logged in session.
        /// </summary>
        public Guid SessionId { get; set; }
    }
}
