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
            Submit<ActionGeneric, IActionResponse>
                ("api/Transaction/Begin", new ActionGeneric(client.Token.SessionId));
        }

        public void Commit()
        {
            Submit<ActionGeneric, IActionResponse>
                ("api/Transaction/Commit", new ActionGeneric(client.Token.SessionId));
        }

        public void Rollback()
        {
            Submit<ActionGeneric, IActionResponse>
                ("api/Transaction/Rollback", new ActionGeneric(client.Token.SessionId));
        }
    }
}
