using LeafSQL.Engine.Sessions;
using System;
using System.IO;
using System.Linq;

namespace LeafSQL.Engine.Security
{
    public class SecurityManager
    {
        private Core core;

        public PersistLoginCatalog Catalog { get; set; }

        private string loginCatalogFile;

        public SecurityManager(Core core)
        {
            this.core = core;

            loginCatalogFile = Path.Combine(core.Settings.DataRootPath, Constants.LoginCatalogFile);

            //If the catalog doesnt exist, create a new empty one.
            if (File.Exists(loginCatalogFile) == false)
            {
                Catalog = new PersistLoginCatalog();

                Catalog.Add(new PersistLogin()
                {
                    Name = "admin",
                    PasswordHash = Library.Utility.HashPassword("")
                });

                Directory.CreateDirectory(core.Settings.DataRootPath);
                core.IO.PutJsonNonTracked(loginCatalogFile, Catalog);
            }

            Catalog = core.IO.GetJsonNonTracked<PersistLoginCatalog>(loginCatalogFile);
        }

        /// <summary>
        /// Logs a user in and establishes a new session id.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public Session Login(string username, string passwordHash)
        {
            Guid sessionId = Guid.NewGuid();

            PersistLogin foundLogin = null;

            lock (Catalog.Collection)
            {
                foundLogin = Catalog.Collection.Where(o =>
                            o.Name.ToLower() == username.ToLower()
                            && o.PasswordHash.ToLower() == passwordHash.ToLower()
                            ).FirstOrDefault();
            }

            if (foundLogin == null)
            {
                throw new Exceptions.LeafSQLInvalidUsernameOrPassword();
            }

            return core.Sessions.LoginSession(foundLogin.Id, sessionId);
        }

        /// <summary>
        /// Logs a user out.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public void Logout(Guid sessionId)
        {
            core.Sessions.LogoutSession(sessionId);
        }
    }
}
