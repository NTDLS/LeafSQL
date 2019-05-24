using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LeafSQL.Library.Client.Management
{
    public class Indexes
    {
        private LeafSQLClient client;

        public Indexes(LeafSQLClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Creates an index on the given schema.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public void Create(string schema, Payloads.Index document)
        {
            string url = string.Format("api/Indexes/{0}/{1}/Create", client.Token.SessionId, schema);

            var postContent = new StringContent(JsonConvert.SerializeObject(document), Encoding.UTF8);

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

        /// <summary>
        /// Checks for the existence of an index.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public bool Exists(string schema, string indexName)
        {
            string url = string.Format("api/Indexes/{0}/{1}/{2}/Exists", client.Token.SessionId, schema, indexName);

            using (var response = client.Client.GetAsync(url))
            {
                string resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ActionResponseBoolean>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                return result.Value;
            }
        }

        /// <summary>
        /// Gets a list of indexes for a specific schema.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public List<Payloads.Index> List(string schema)
        {
            string url = string.Format("api/Indexes/{0}/{1}/List", client.Token.SessionId, schema);

            using (var response = client.Client.GetAsync(url))
            {
                string resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ActionResponseIndexes>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                return result.List;
            }
        }

    }
}
