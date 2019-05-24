using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeafSQL.Library.Client.Management
{
    public class Schema
    {
        private LeafSQLClient client;

        public Indexes Indexes { get; set; }

        public Schema(LeafSQLClient client)
        {
            this.client = client;
            this.Indexes = new Indexes(client);
        }

        /// <summary>
        /// Creates a single schema or an entire schema path.
        /// </summary>
        /// <param name="schema"></param>
        public async Task CreateAsync(string schema)
        {
            string url = string.Format("api/Schema/{0}/{1}/Create", client.Token.SessionId, schema);

            using (var response = await client.Client.GetAsync(url))
            {
                string resultText = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IActionResponse>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }
            }
        }
        public void Create(string schema)
        {
            CreateAsync(schema).Wait();
        }

        /// <summary>
        /// Checks for the existence of a schema.
        /// </summary>
        /// <param name="schema"></param>
        public async Task<bool> ExistsAsync(string schema)
        {
            string url = string.Format("api/Schema/{0}/{1}/Exists", client.Token.SessionId, schema);

            using (var response = await client.Client.GetAsync(url))
            {
                string resultText = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ActionResponseBoolean>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                return result.Value;
            }
        }

        public bool Exists(string schema)
        {
            return ExistsAsync(schema).Result;
        }


        /// <summary>
        /// Drops a single schema or an entire schema path.
        /// </summary>
        /// <param name="schema"></param>
        public async Task DropAsync(string schema)
        {
            string url = string.Format("api/Schema/{0}/{1}/Drop", client.Token.SessionId, schema);

            using (var response = await client.Client.GetAsync(url))
            {
                string resultText = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IActionResponse>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }
            }
        }

        public void Drop(string schema)
        {
            DropAsync(schema).Wait();
        }

        /// <summary>
        /// Lists the existing schemas within a given schema.
        /// </summary>
        /// <param name="schema"></param>
        public async Task<List<Payloads.Schema>> ListAsync(string schema)
        {
            string url = string.Format("api/Schema/{0}/{1}/List", client.Token.SessionId, schema);

            using (var response = await client.Client.GetAsync(url))
            {
                string resultText = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ActionResponseSchemas>(resultText);

                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                return result.List;
            }
        }

        public List<Payloads.Schema> List(string schema)
        {
            return ListAsync(schema).Result;
        }
    }
}
