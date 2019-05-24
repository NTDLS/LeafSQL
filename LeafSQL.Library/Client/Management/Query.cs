using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace LeafSQL.Library.Client.Management
{
    public class Query
    {
        private LeafSQLClient client;

        public Query(LeafSQLClient client)
        {
            this.client = client;
        }

        public void Execute(string statement)
        {
            string url = string.Format("api/Query/{0}/Execute", client.Token.SessionId);

            var postContent = new StringContent(JsonConvert.SerializeObject(statement), Encoding.UTF8);

            using (var response = client.Client.PostAsync(url, postContent))
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
