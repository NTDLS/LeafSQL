using LeafSQL.Library.Payloads.Models;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponseServerSettings : IActionResponse
    {
        public ServerSettings Settings { get; set; }
    }
}
