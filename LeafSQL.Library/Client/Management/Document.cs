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
    public class Document
    {
        private LeafSQLClient client;

        public Document(LeafSQLClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Stores a document in the given schema.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public async Task StoreAsync(string schema, Payloads.Document document)
        {
            string url = string.Format("api/Document/{0}/{1}/Store", client.Token.SessionId, schema);

            var postContent = new StringContent(JsonConvert.SerializeObject(document), Encoding.UTF8);

            using (var response = await client.Client.PostAsync(url, postContent))
            {
                string resultText = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IActionResponse>(resultText);
                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }
            }
        }

        public void Store(string schema, Payloads.Document document)
        {
            string url = string.Format("api/Document/{0}/{1}/Store", client.Token.SessionId, schema);

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
        /// Deletes a document in the given schema by its Id.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public async Task DeleteByIdAsync(string schema, Guid id)
        {
            string url = string.Format("api/Document/{0}/{1}/{2}/DeleteById", client.Token.SessionId, schema, id);

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

        public void DeleteById(string schema, Guid id)
        {
            string url = string.Format("api/Document/{0}/{1}/{2}/DeleteById", client.Token.SessionId, schema, id);

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

        /// <summary>
        /// Lists the doucments within a given schema.
        /// </summary>
        /// <param name="schema"></param>
        public async Task<List<DocumentMeta>> GetCatalogAsync(string schema)
        {
            string url = string.Format("api/Document/{0}/{1}/Catalog", client.Token.SessionId, schema);

            using (var response = await client.Client.GetAsync(url))
            {
                string resultText = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ActionResponseDocuments>(resultText);

                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                return result.List;
            }
        }

        public List<DocumentMeta> GetCatalog(string schema)
        {
            string url = string.Format("api/Document/{0}/{1}/Catalog", client.Token.SessionId, schema);

            using (var response = client.Client.GetAsync(url))
            {
                string resultText = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ActionResponseDocuments>(resultText);

                if (result.Success == false)
                {
                    throw new Exception(result.Message);
                }

                return result.List;
            }
        }

    }
}