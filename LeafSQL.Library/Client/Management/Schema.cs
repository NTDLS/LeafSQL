using LeafSQL.Library.Client.Management.Base;
using LeafSQL.Library.Payloads.Actions;
using LeafSQL.Library.Payloads.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeafSQL.Library.Client.Management
{
    public class Schema : ManagementBase
    {
        private LeafSQLClient client;
        public Indexes Indexes { get; set; }

        public Schema(LeafSQLClient client)
            : base(client)
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
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema
            };

            await SubmitAsync<ActionGenericBase, ActionResponseBase>("api/Schema/Create", action);
        }

        public void Create(string schema)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema
            };

            Submit<ActionGenericBase, ActionResponseBase>("api/Schema/Create", action);
        }

        /// <summary>
        /// Checks for the existence of a schema.
        /// </summary>
        /// <param name="schema"></param>
        public async Task<bool> ExistsAsync(string schema)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema
            };

            return (await SubmitAsync<ActionGenericBase, ActionResponseBoolean>("api/Schema/Exists", action)).Value;
        }

        public bool Exists(string schema)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema
            };

            return Submit<ActionGenericBase, ActionResponseBoolean>("api/Schema/Exists", action).Value;
        }


        /// <summary>
        /// Drops a single schema or an entire schema path.
        /// </summary>
        /// <param name="schema"></param>
        public async Task DropAsync(string schema)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema
            };

            await SubmitAsync<ActionGenericBase, ActionResponseBase>("api/Schema/Drop", action);
        }

        public void Drop(string schema)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema
            };

            Submit<ActionGenericBase, ActionResponseBase>("api/Schema/Drop", action);
        }

        /// <summary>
        /// Lists the existing schemas within a given schema.
        /// </summary>
        /// <param name="schema"></param>
        public async Task<List<Payloads.Models.Schema>> ListAsync(string schema)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema
            };

            return (await SubmitAsync<ActionGenericBase, ActionResponseSchemas>("api/Schema/List", action)).List;
        }

        public List<Payloads.Models.Schema> List(string schema)
        {
            var action = new ActionGenericBase(client.Token.SessionId)
            {
                SchemaName = schema
            };

            return Submit<ActionGenericBase, ActionResponseSchemas>("api/Schema/List", action).List;
        }
    }
}