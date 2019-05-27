using LeafSQL.Library.Payloads.Models;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponseServerVersion : IActionResponse
    {
        public ServerVersion Version { get; set; }
    }
}
