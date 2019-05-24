using System;

namespace LeafSQL.Library.Payloads.Models
{
    public class Login
    {
        public string Username { get; set; }
        public string Passwordhash { get; set; }
        public Guid Id { get; set; }
    }
}
