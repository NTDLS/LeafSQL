using LeafSQL.Library.Payloads.Models;
using System;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponceLogin : ActionResponseBase
    {
        public Guid SessionId { get; set; }
        public Guid LoginId { get; set; }
        public UInt64 ProcessId { get; set; }

        public LoginToken ToToken()
        {
            return new LoginToken(this);
        }

    }
}
