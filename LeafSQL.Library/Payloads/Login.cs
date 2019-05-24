using System;

namespace LeafSQL.Library.Payloads
{
    public class Login
    {
        public string Username { get; set; }
        public string Passwordhash { get; set; }
        public Guid Id { get; set; }
    }
}
