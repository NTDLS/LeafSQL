using LeafSQL.Library.Client.Management.Base;
using LeafSQL.Library.Payloads;
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

        public async Task ExecuteAsync(string statement)
        {
            var action = new ActionRequestExecuteNonQuery(client.Token.SessionId)
            {
                Statement = statement
            };

            await SubmitAsync<ActionRequestExecuteNonQuery, IActionResponse>("api/Query/Execute", action);
        }

        public void Execute(string statement)
        {
            var action = new ActionRequestExecuteNonQuery(client.Token.SessionId)
            {
                Statement = statement
            };

            Submit<ActionRequestExecuteNonQuery, IActionResponse>("api/Query/Execute", action);
        }
    }
}
