using LeafSQL.Library.Payloads.Models;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponseServerSettings : ActionResponseBase
    {
        public ServerSettings Settings { get; set; }
    }
}
