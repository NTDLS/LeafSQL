using LeafSQL.Engine.Interfaces;
using LeafSQL.Library.Payloads.Models;
using System;

namespace LeafSQL.Engine.Security
{
    [Serializable]
    public class PersistLogin : IPayloadCompatible<PersistLogin, Login>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modfied { get; set; }
        public string PasswordHash { get; set; }

        public PersistLogin()
        {
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
            Modfied = DateTime.UtcNow;
        }

        public PersistLogin Clone()
        {
            return new PersistLogin
            {
                Id = Id,
                Name = Name,
                Created = Created,
                Modfied = Modfied,
                PasswordHash = PasswordHash
            };
        }

        public Login ToPayload()
        {
            var result = new Login()
            {
                Name = this.Name,
                Created = this.Created,
                Id = this.Id,
                PasswordHash = this.PasswordHash,
                Modfied = this.Modfied                 
            };

            return result;
        }

        static public PersistLogin FromPayload(Login login)
        {
            var persistLogin = new PersistLogin()
            {
                Id = login.Id,
                Name = login.Name,
                Created = login.Created,
                Modfied = login.Modfied,
                PasswordHash = login.PasswordHash
            };

            return persistLogin;
        }
    }
}
