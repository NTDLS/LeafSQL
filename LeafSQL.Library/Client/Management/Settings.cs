using LeafSQL.Library.Client.Management.Base;
using LeafSQL.Library.Payloads.Actions;
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
            return (await SubmitAsync<ActionRequestBase, ActionResponseServerSettings>
                ("api/Server/Settings", new ActionRequestBase(client.Token.SessionId))).Settings;
        }

        public ServerSettings Get()
        {
            return Submit<ActionRequestBase, ActionResponseServerSettings>
                ("api/Server/Settings", new ActionRequestBase(client.Token.SessionId)).Settings;
        }

        public async Task<ServerVersion> GetVersionAsync()
        {
            return (await SubmitAsync<ActionRequestBase, ActionResponseServerVersion>
                ("api/Server/Version", new ActionRequestBase(client.Token.SessionId))).Version;
        }

        public ServerVersion GetVersion()
        {
            return Submit<ActionRequestBase, ActionResponseServerVersion>
                ("api/Server/Version", new ActionRequestBase(client.Token.SessionId)).Version;
        }
    }
}

