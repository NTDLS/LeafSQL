using LeafSQL.Library.Client.Management.Base;
using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Models;
using LeafSQL.Library.Payloads.Responses;
using System.Threading.Tasks;

namespace LeafSQL.Library.Client.Management
{
    public class Query : ManagementBase
    {
        private LeafSQLClient client;

        public Query(LeafSQLClient client)
            : base(client)
        {
            this.client = client;
        }

        public async Task ExecuteNonQueryAsync(string statement)
        {
            var action = new ActionRequestExecuteNonQuery(client.Token.SessionId)
            {
                Statement = statement
            };

            await SubmitAsync<ActionRequestExecuteNonQuery, ActionResponseBase>("api/Query/ExecuteNonQuery", action);
        }

        public void ExecuteNonQuery(string statement)
        {
            var action = new ActionRequestExecuteNonQuery(client.Token.SessionId)
            {
                Statement = statement
            };

            Submit<ActionRequestExecuteNonQuery, ActionResponseBase>("api/Query/ExecuteNonQuery", action);
        }

        public async Task<QueryResult> ExecuteQueryAsync(string statement)
        {
            var action = new ActionRequestExecuteQuery(client.Token.SessionId)
            {
                Statement = statement
            };

            return (await SubmitAsync<ActionRequestExecuteQuery, ActionResponseQuery>("api/Query/ExecuteQuery", action)).Result;
        }

        public QueryResult ExecuteQuery(string statement)
        {
            var action = new ActionRequestExecuteQuery(client.Token.SessionId)
            {
                Statement = statement
            };

            return Submit<ActionRequestExecuteQuery, ActionResponseQuery>("api/Query/ExecuteQuery", action).Result;
        }
    }
}
