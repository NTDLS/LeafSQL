using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.Engine.Sessions
{
    public class SecurityManager
    {
        private Core core;

        public Dictionary<Guid, UInt64> Collection { get; set; }

        public SecurityManager(Core core)
        {
            this.core = core;
            Collection = new Dictionary<Guid, ulong>();
        }

        /// <summary>
        /// Logs a user in and establishes a new session id.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public Guid Login(string username, string passwordHash)
        {
            Guid sessionId = Guid.NewGuid();
            core.Sessions.LoginSession(sessionId);

            return sessionId;
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
