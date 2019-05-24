using LeafSQL.Library.Client.Management.Base;
using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Actions.Base;
using LeafSQL.Library.Payloads.Models;
using LeafSQL.Library.Payloads.Responses;
using System.Threading.Tasks;

namespace LeafSQL.Library.Client.Management
{
    public class Settings : ManagementBase
    {
        private LeafSQLClient client;

        public Settings(LeafSQLClient client)
            : base(client)
        {
            this.client = client;
        }

        public async Task<ServerSettings> GetAsync()
        {
            var action = new ActionGeneric(client.Token.SessionId)
            {
            };

            return (await SubmitAsync<ActionGeneric, ActionResponseServerSettings>("api/Server/Settings", action)).Settings;
        }

        public ServerSettings Get()
        {
            var action = new ActionGeneric(client.Token.SessionId)
            {
            };

            return Submit<ActionGeneric, ActionResponseServerSettings>("api/Server/Settings", action).Settings;
        }
    }
}

