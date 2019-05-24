using LeafSQL.Library.Client.Management.Base;
using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Actions;
using LeafSQL.Library.Payloads.Actions.Base;
using LeafSQL.Library.Payloads.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeafSQL.Library.Client.Management
{
    public class Security : ManagementBase
    {
        private LeafSQLClient client;

        public Security(LeafSQLClient client)
            : base(client)
        {
            this.client = client;
        }

        /// <summary>
        /// Logs a user into the server.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public LoginToken Login(string username, string password)
        {
            var action = new ActionLogin(client.Token.SessionId)
            {
                Username = username,
                PasswordHash = Utility.HashPassword(password)
            };

            client.Token = Submit<ActionLogin, ActionResponceLogin>("api/Security/Login", action).ToToken();

            return client.Token;
        }

        /// <summary>
        /// Logs a user out of the server.
        /// </summary>
        public void Logout()
        {
            var action = new ActionGeneric(client.Token.SessionId)
            {
            };

            Submit<ActionGeneric, IActionResponse>("api/Security/Logout", action);

            client.Token = new LoginToken();
        }

        /// <summary>
        /// Gets a list of all logins from the server.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Login>> GetLoginsAsync()
        {
            var action = new ActionGeneric(client.Token.SessionId)
            {
            };

            return (await SubmitAsync<ActionGeneric, ActionResponceLogins>("api/Security/ListLogins", action)).List;
        }

        public List<Login> GetLogins()
        {
            var action = new ActionGeneric(client.Token.SessionId)
            {
            };

            return Submit<ActionGeneric, ActionResponceLogins>("api/Security/ListLogins", action).List;
        }
    }
}
