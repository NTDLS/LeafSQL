using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace LeafSQL.Library.Client.Management
{
    public class Settings
    {
        private LeafSQLClient client;

        public Settings(LeafSQLClient client)
        {
            this.client = client;
        }
    
        public async Task<ServerSettings> GetAsync()
        {
            string url = string.Format("api/Server/{0}/Settings", client.Token.SessionId);

            using (var response = await client.Client.GetAsync(url))
            {
                string resultText = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ActionResponseServerSettings>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                return result.Settings;
            }
        }

        public ServerSettings Get()
        {
            return GetAsync().Result;
        }

    }
}
