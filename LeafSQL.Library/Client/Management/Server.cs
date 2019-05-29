using LeafSQL.Library.Client.Management.Base;
using LeafSQL.Library.Payloads.Actions;
using LeafSQL.Library.Payloads.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeafSQL.Library.Client.Management
{
    public class Server : ManagementBase
    {
        private LeafSQLClient client;
        public Settings Settings { get; set; }
        public State State { get; set; }

        public Server(LeafSQLClient client)
            : base(client)
        {
            this.client = client;
            this.Settings = new Settings(client);
            this.State = new State(client);
        }
    }
}