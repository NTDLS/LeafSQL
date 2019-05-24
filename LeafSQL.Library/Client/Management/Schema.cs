﻿using LeafSQL.Library.Payloads;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        public void Create(string schema)
        {
            string url = string.Format("api/Schema/{0}/{1}/Create", client.Token.SessionId, schema);

            using (var response = client.Client.GetAsync(url))
            {
                string resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ActionResponse>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }
            }
        }

        /// <summary>
        /// Checks for the existence of a schema.
        /// </summary>
        /// <param name="schema"></param>
        public bool Exists(string schema)
        {
            string url = string.Format("api/Schema/{0}/{1}/Exists", client.Token.SessionId, schema);

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
        /// Drops a single schema or an entire schema path.
        /// </summary>
        /// <param name="schema"></param>
        public void Drop(string schema)
        {
            string url = string.Format("api/Schema/{0}/{1}/Drop", client.Token.SessionId, schema);

            using (var response = client.Client.GetAsync(url))
            {
                string resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ActionResponse>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }
            }
        }

        /// <summary>
        /// Lists the existing schemas within a given schema.
        /// </summary>
        /// <param name="schema"></param>
        public List<Payloads.Schema> List(string schema)
        {
            string url = string.Format("api/Schema/{0}/{1}/List", client.Token.SessionId, schema);

            using (var response = client.Client.GetAsync(url))
            {
                string resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ActionResponseSchemas>(resultText);

                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                return result.List;
            }
        }
    }
}

