using System;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponseBoolean : IActionResponse
    {
        public bool Value { get; set; }
    }
}
