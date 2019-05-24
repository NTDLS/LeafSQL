using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace LeafSQL.Library.Client.Management
{
    public class Security
    {
        private LeafSQLClient client;

        public Security(LeafSQLClient client)
        {
            this.client = client;
        }

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
    }
}
