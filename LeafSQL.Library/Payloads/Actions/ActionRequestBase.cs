using System;

namespace LeafSQL.Library.Payloads.Actions
{
    public class ActionRequestBase: IActionRequest
    {
        public ActionRequestBase(Guid sessionId)
        {
            this.SessionId = sessionId;
        }

        /// <summary>
        /// The Id of the logged in session.
        /// </summary>
        public Guid SessionId { get; set; }
    }
}
