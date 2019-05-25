using System;

namespace LeafSQL.Library.Payloads.Models
{
    public class Login
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modfied { get; set; }
        public string PasswordHash { get; set; }
    }
}
