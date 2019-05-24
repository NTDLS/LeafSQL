using LeafSQL.Library.Client.Management.Base;
using LeafSQL.Library.Payloads.Actions.Base;
using LeafSQL.Library.Payloads.Responses;

namespace LeafSQL.Library.Client.Management
{
    public class Transaction : ManagementBase
    {
        private LeafSQLClient client;

        public Transaction(LeafSQLClient client)
            : base(client)
        {
            this.client = client;
        }

        public void Begin()
        {
            var action = new ActionGeneric(client.Token.SessionId)
            {
            };

            Submit<ActionGeneric, IActionResponse>("api/Transaction/Begin", action);
        }

        public void Commit()
        {
            var action = new ActionGeneric(client.Token.SessionId)
            {
            };

            Submit<ActionGeneric, IActionResponse>("api/Transaction/Commit", action);
        }

        public void Rollback()
        {
            var action = new ActionGeneric(client.Token.SessionId)
            {
            };

            Submit<ActionGeneric, IActionResponse>("api/Transaction/Rollback", action);
        }
    }
}
