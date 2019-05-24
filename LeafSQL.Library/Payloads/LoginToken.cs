using LeafSQL.Library.Payloads.Responses;
using System;

namespace LeafSQL.Library.Payloads
{
    public class LoginToken
    {
        public Guid SessionId { get; set; }
        public UInt64 ProcessId { get; set; }

        private bool isValid = false;
        public bool IsValid
        {
            get
            {
                return isValid;
            }
        }

        public LoginToken()
        {
            SessionId = Guid.Empty;
        }

        public LoginToken(ActionResponceLogin response)
        {
            if (response.Success == false)
            {
                throw new Exception("The login response does not contain a valid login token.");
            }

            this.SessionId = response.SessionId;
            this.ProcessId = response.ProcessId;

            this.isValid = true;
        }
    }
}
