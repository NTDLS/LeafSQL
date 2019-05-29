using LeafSQL.Library.Client.Management.Base;
using LeafSQL.Library.Payloads.Actions;
using LeafSQL.Library.Payloads.Actions.Base;
using LeafSQL.Library.Payloads.Models;
using LeafSQL.Library.Payloads.Responses;
using System;
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
            var action = new ActionRequestLogin(client.Token.SessionId)
            {
                Login = new Login(username, Utility.HashPassword(password))
            };

            client.Token = Submit<ActionRequestLogin, ActionResponceLogin>("api/Security/Login", action).ToToken();

            return client.Token;
        }

        /// <summary>
        /// Logs a user out of the server.
        /// </summary>
        public void Logout()
        {
            Submit<ActionGeneric, IActionResponse>
                ("api/Security/Logout", new ActionGeneric(client.Token.SessionId));

            client.Token = new Payloads.Models.LoginToken();
        }

        /// <summary>
        /// Gets a list of all logins from the server.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Payloads.Models.Login>> GetLoginsAsync()
        {

            return (await SubmitAsync<ActionGeneric, ActionResponceLogins>
                ("api/Security/ListLogins", new ActionGeneric(client.Token.SessionId))).List;
        }

        public List<Payloads.Models.Login> GetLogins()
        {
            return Submit<ActionGeneric, ActionResponceLogins>
                ("api/Security/ListLogins", new ActionGeneric(client.Token.SessionId)).List;
        }

        public async Task<Guid> CreateLoginAsync(string username, string passwordHash)
        {
            var action = new ActionRequestLogin(client.Token.SessionId)
            {
                Login = new Login(username, passwordHash)
            };

            return (await SubmitAsync<ActionRequestLogin, ActionResponseId>
                ("api/Security/CreateLogin", action)).Id;
        }

        public Guid CreateLogin(string username, string passwordHash)
        {
            var action = new ActionRequestLogin(client.Token.SessionId)
            {
                Login = new Login(username, passwordHash)
            };

            return Submit<ActionRequestLogin, ActionResponseId>
                ("api/Security/CreateLogin", action).Id;
        }

        public async Task<Guid> SetLoginPasswordAsync(string username, string passwordHash)
        {
            var action = new ActionRequestLogin(client.Token.SessionId)
            {
                Login = new Login(username, passwordHash)
            };

            return (await SubmitAsync<ActionRequestLogin, ActionResponseId>
                ("api/Security/SetLoginPasswordByName", action)).Id;
        }

        public Guid SetLoginPassword(string username, string passwordHash)
        {
            var action = new ActionRequestLogin(client.Token.SessionId)
            {
                Login = new Login(username, passwordHash)
            };

            return Submit<ActionRequestLogin, ActionResponseId>
                ("api/Security/SetLoginPasswordByName", action).Id;
        }

        public async Task DeleteLoginByNameAsync(string username)
        {
            var action = new ActionGenericObject(client.Token.SessionId)
            {
                ObjectName = username
            };

            await SubmitAsync<ActionGenericObject, ActionResponseId>
                ("api/Security/DeleteLoginByName", action);
        }

        public void DeleteLoginByName(string username)
        {
            var action = new ActionGenericObject(client.Token.SessionId)
            {
                ObjectName = username
            };

            Submit<ActionGenericObject, ActionResponseId>
                ("api/Security/DeleteLoginByName", action);
        }
    }
}
