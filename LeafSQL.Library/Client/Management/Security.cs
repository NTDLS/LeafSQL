using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.Library.Client.Management
{
    public class Security
    {
        private LeafSQLClient client;

        public Security(LeafSQLClient client)
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
            string url = string.Format("api/Security/Login");

            var loginRequest = new LoginRequest()
            {
                Username = username,
                PasswordHash = Utility.HashPassword(password)
            };

            var postContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8);

            using (var response = client.Client.PostAsync(url, postContent))
            {
                string resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ActionResponceLogin>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                client.Token = result.ToToken();

                return client.Token;
            }
        }

        /// <summary>
        /// Logs a user out of the server.
        /// </summary>
        public void Logout()
        {
            string url = string.Format("api/Security/{0}/Logout", client.Token.SessionId);

            client.Token = new LoginToken();

            using (var response = client.Client.GetAsync(url))
            {
                string resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ActionResponceLogin>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }
            }
        }

        /// <summary>
        /// Gets a list of all logins from the server.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Login>> GetLoginsAsync()
        {
            string url = string.Format("api/Security/{0}/ListLogins", client.Token.SessionId);

            using (var response = await client.Client.GetAsync(url))
            {
                string resultText = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ActionResponceLogins>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                return result.List;
            }
        }

        public List<Login> GetLogins()
        {
            string url = string.Format("api/Security/{0}/ListLogins", client.Token.SessionId);

            using (var response = client.Client.GetAsync(url))
            {
                string resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ActionResponceLogins>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                return result.List;
            }
        }

    }
}
