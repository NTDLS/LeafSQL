﻿using LeafSQL.Library.Payloads.Models;
using System.Collections.Generic;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponceLogins : ActionResponseBase
    {
        public List<Login> List { get; set; }
    }
}
