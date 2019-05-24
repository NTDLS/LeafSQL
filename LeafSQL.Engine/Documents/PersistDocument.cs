using System;
using LeafSQL.Library.Payloads;

namespace LeafSQL.Engine.Documents
{
    [Serializable]
    public class PersistDocument
    {
        public string Content { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modfied { get; set; }

        public PersistDocument Clone()
        {
            return new PersistDocument
            {
                Id = Id,
                Content = Content,
                Created = Created,
                Modfied = Modfied
            };
        }

        static public PersistDocument FromPayload(Document document)
        {
            return new PersistDocument()
            {
                Id = document.Id,
                Created = document.Created,
                Modfied = document.Modfied,
                Content = document.Content
            };
        }

        static public Document ToPayload(PersistDocument document)
        {
            return new Document()
            {
                Id = document.Id,
                Created = document.Created,
                Modfied = document.Modfied,
                Content = document.Content
            };
        }

    }
}
