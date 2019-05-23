using Newtonsoft.Json;
using System;

namespace LeafSQL.Library.Payloads
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
