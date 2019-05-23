using System;

namespace LeafSQL.Library.Payloads
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid SessionId { get; set; }
        public UInt64 ProcessId { get; set; }

        public LoginToken ToToken()
        {
            return new LoginToken(this);
        }

    }
}
