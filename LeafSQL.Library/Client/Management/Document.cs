using LeafSQL.Library.Client.Management.Base;
using LeafSQL.Library.Payloads.Actions;
using LeafSQL.Library.Payloads.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeafSQL.Library.Client.Management
{
    public class Document: ManagementBase
    {
        private LeafSQLClient client;

        public Document(LeafSQLClient client)
            : base(client)
        {
            this.client = client;
        }

        /// <summary>
        /// Stores a document in the given schema.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public async Task StoreAsync(string schema, Payloads.Models.Document document)
        {
            var action = new ActionRequestStoreDocument(client.Token.SessionId)
            {
                SchemaName = schema,
                Object = document
            };

            await SubmitAsync<ActionRequestStoreDocument, ActionResponseBase>("api/Document/Store", action);
        }

        public void Store(string schema, Payloads.Models.Document document)
        {
            var action = new ActionRequestStoreDocument(client.Token.SessionId)
            {
                SchemaName = schema,
                Object = document
            };

            Submit<ActionRequestStoreDocument, ActionResponseBase>("api/Document/Store", action);
        }

        /// <summary>
        /// Deletes a document in the given schema by its Id.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public async Task DeleteByIdAsync(string schema, Guid id)
        {
            var action = new ActionGenericObject(client.Token.SessionId)
            {
                SchemaName = schema,
                ObjectId = id
            };

            await SubmitAsync<ActionGenericObject, ActionResponseBase>("api/Document/DeleteById", action);
        }

        public void DeleteById(string schema, Guid id)
        {
            var action = new ActionGenericObject(client.Token.SessionId)
            {
                SchemaName = schema,
                ObjectId = id
            };

            Submit<ActionGenericObject, ActionResponseBase>("api/Document/DeleteById", action);
        }

        /// <summary>
        /// Lists the doucments within a given schema.
        /// </summary>
        /// <param name="schema"></param>
        public async Task<List<Payloads.Models.DocumentMeta>> GetCatalogAsync(string schema)
        {
            var action = new ActionGenericObject(client.Token.SessionId)
            {
                SchemaName = schema,
            };

            return (await SubmitAsync<ActionGenericObject, ActionResponseDocuments>("api/Document/List", action)).List;
        }

        public List<Payloads.Models.DocumentMeta> GetCatalog(string schema)
        {
            var action = new ActionGenericObject(client.Token.SessionId)
            {
                SchemaName = schema,
            };

            return Submit<ActionGenericObject, ActionResponseDocuments>("api/Document/List", action).List;
        }
    }
}
