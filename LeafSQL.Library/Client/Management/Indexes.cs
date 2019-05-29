using LeafSQL.Library.Client.Management.Base;
using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Actions;
using LeafSQL.Library.Payloads.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeafSQL.Library.Client.Management
{
    public class Indexes : ManagementBase
    {
        private LeafSQLClient client;

        public Indexes(LeafSQLClient client)
            : base(client)
        {
            this.client = client;
        }

        /// <summary>
        /// Creates an index on the given schema.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public async Task CreateAsync(string schema, Payloads.Models.Index index)
        {
            var action = new ActionRequestCreateIndex(client.Token.SessionId)
            {
                SchemaName = schema,
                Object = index
            };

            await SubmitAsync<ActionRequestCreateIndex, ActionResponseBase>("api/Indexes/Create", action);
        }

        public void Create(string schema, Payloads.Models.Index index)
        {
            var action = new ActionRequestCreateIndex(client.Token.SessionId)
            {
                SchemaName = schema,
                Object = index
            };

            Submit<ActionRequestCreateIndex, ActionResponseBase>("api/Indexes/Create", action);
        }

        /// <summary>
        /// Checks for the existence of an index.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public async Task RebuildAsync(string schema, string indexName)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema,
                ObjectName = indexName
            };

            await SubmitAsync<ActionGenericBase, ActionResponseBase>("api/Indexes/RebuildByName", action);
        }

        public void Rebuild(string schema, string indexName)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema,
                ObjectName = indexName
            };

            Submit<ActionGenericBase, ActionResponseBase>("api/Indexes/RebuildByName", action);
        }

        /// <summary>
        /// Checks for the existence of an index.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public async Task<bool> ExistsAsync(string schema, string indexName)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema,
                ObjectName = indexName
            };

            return (await SubmitAsync<ActionGenericBase, ActionResponseBoolean>("api/Indexes/ExistsByName", action)).Value;
        }

        public bool Exists(string schema, string indexName)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema,
                ObjectName = indexName
            };

            return Submit<ActionGenericBase, ActionResponseBoolean>("api/Indexes/ExistsByName", action).Value;
        }

        /// <summary>
        /// Gets a list of indexes for a specific schema.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public async Task<List<Payloads.Models.Index>> ListAsync(string schema)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema
            };

            return (await SubmitAsync<ActionGenericBase, ActionResponseIndexes>("api/Indexes/List", action)).List;
        }

        public List<Payloads.Models.Index> List(string schema)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema
            };

            return Submit<ActionGenericBase, ActionResponseIndexes>("api/Indexes/List", action).List;
        }

        /// <summary>
        /// Creates an index on the given schema.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public async Task DeleteByNameAsync(string schema, string indexName)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema,
                ObjectName = indexName
            };

            await SubmitAsync<ActionGenericBase, ActionResponseBase>("api/Indexes/DeleteByName", action);
        }

        public void DeleteByName(string schema, string indexName)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema,
                ObjectName = indexName
            };

            Submit<ActionGenericBase, ActionResponseBase>("api/Indexes/DeleteByName", action);
        }
    }
}
