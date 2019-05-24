using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
using System;

namespace LeafSQL.Library.Client.Management
{
    public class Transaction
    {
        private LeafSQLClient client;

        public Transaction(LeafSQLClient client)
        {
            this.client = client;
        }

        public void Begin()
        {
            string url = string.Format("api/Transaction/{0}/Begin", client.Token.SessionId);

            using (var response = client.Client.GetAsync(url))
            {
                string resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<IActionResponse>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }
            }
        }

        public void Commit()
        {
            string url = string.Format("api/Transaction/{0}/Commit", client.Token.SessionId);

            using (var response = client.Client.GetAsync(url))
            {
                string resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<IActionResponse>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }
            }
        }

        public void Rollback()
        {
            string url = string.Format("api/Transaction/{0}/Rollback", client.Token.SessionId);

            using (var response = client.Client.GetAsync(url))
            {
                string resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<IActionResponse>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }
            }
        }

    }
}
