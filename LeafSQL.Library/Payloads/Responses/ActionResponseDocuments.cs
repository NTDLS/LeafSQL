using System;
using System.Collections.Generic;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponseDocuments : IActionResponse
    {
        public List<DocumentMeta> List { get; set; }

        public void Add(DocumentMeta value)
        {
            List.Add(value);
        }
    }
}
