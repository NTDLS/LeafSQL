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

        public Login()
        {
        }

        public Login(string name)
        {
            this.Name = name;
        }

        public Login(string name, string passwordHash)
        {
            this.Name = name;
            this.PasswordHash = passwordHash;
        }
    }
}
