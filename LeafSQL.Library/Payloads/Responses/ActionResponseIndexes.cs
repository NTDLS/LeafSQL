﻿using LeafSQL.Library.Payloads.Models;
using System.Collections.Generic;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponseIndexes : ActionResponseBase
    {
        public List<Index> List { get; set; }

        public ActionResponseIndexes()
        {
            List = new List<Index>();
        }

        public void Add(Index value)
        {
            List.Add(value);
        }
    }
}
