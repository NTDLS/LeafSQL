using System;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponseId: IActionResponse
    {
        public Guid Id { get; set; }
    }
}
