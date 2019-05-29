using LeafSQL.Library.Payloads.Models;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponseServerVersion : ActionResponseBase
    {
        public ServerVersion Version { get; set; }
    }
}
