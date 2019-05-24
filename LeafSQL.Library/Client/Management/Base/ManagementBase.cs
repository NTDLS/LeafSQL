using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LeafSQL.Library.Client.Management.Base
{
    public class ManagementBase
    {
        private LeafSQLClient client;

        public ManagementBase(LeafSQLClient client)
        {
            this.client = client;
        }

        public r Submit<s, r>(string url, s action) where r : IActionResponse
        {
            var postContent = new StringContent(JsonConvert.SerializeObject(action), Encoding.UTF8, "application/json");

            using (var response = client.Client.PostAsync(url, postContent))
            {
                var resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<r>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                return result;
            }
        }

        public async Task<r> SubmitAsync<s, r>(string url, s action) where r : IActionResponse
        {
            var postContent = new StringContent(JsonConvert.SerializeObject(action), Encoding.UTF8, "application/json");

            using (var response = await client.Client.PostAsync(url, postContent))
            {
                var resultText = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<r>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                return result;
            }
        }
    }
}
