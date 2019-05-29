using LeafSQL.Library.Payloads.Models;
using System.Collections.Generic;

namespace LeafSQL.Library.Payloads.Responses
{
    public class ActionResponseDocuments : ActionResponseBase
    {
        public List<DocumentMeta> List { get; set; }

        public void Add(DocumentMeta value)
        {
            List.Add(value);
        }
    }
}
