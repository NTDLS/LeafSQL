using LeafSQL.Library.Client.Management.Base;
using LeafSQL.Library.Payloads.Actions;
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
            Submit<ActionRequestBase, ActionResponseBase>
                ("api/Transaction/Begin", new ActionRequestBase(client.Token.SessionId));
        }

        public void Commit()
        {
            Submit<ActionRequestBase, ActionResponseBase>
                ("api/Transaction/Commit", new ActionRequestBase(client.Token.SessionId));
        }

        public void Rollback()
        {
            Submit<ActionRequestBase, ActionResponseBase>
                ("api/Transaction/Rollback", new ActionRequestBase(client.Token.SessionId));
        }
    }
}
