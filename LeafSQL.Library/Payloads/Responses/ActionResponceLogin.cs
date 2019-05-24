using System;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponceLogin : IActionResponse
    {
        public Guid SessionId { get; set; }
        public UInt64 ProcessId { get; set; }

        public LoginToken ToToken()
        {
            return new LoginToken(this);
        }

    }
}
