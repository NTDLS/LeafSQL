using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
using System;

namespace LeafSQL.Library.Client.Management
{
    public class Settings
    {
        private LeafSQLClient client;

        public Settings(LeafSQLClient client)
        {
            this.client = client;
        }
    
        public ServerSettings Get()
        {
            string url = string.Format("api/Server/{0}/Settings", client.Token.SessionId);

            using (var response = client.Client.GetAsync(url))
            {
                string resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ActionResponseServerSettings>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                return result.Settings;
            }
        }
    }
}
