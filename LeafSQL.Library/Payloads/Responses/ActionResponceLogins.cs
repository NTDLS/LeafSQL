using System;
using System.Collections.Generic;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponceLogins : IActionResponse
    {
        public List<Login> List { get; set; }
    }
}
