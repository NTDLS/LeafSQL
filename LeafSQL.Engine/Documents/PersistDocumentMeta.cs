using System;
using LeafSQL.Library.Payloads;

namespace LeafSQL.Engine.Documents
{
    [Serializable]
    public class PersistDocumentMeta
    {
        public Guid Id { get; set; }

        public DocumentMeta ToPayload()
        {
            return new DocumentMeta()
            {
                Id = this.Id
            };
        }

        public string FileName
        {
            get
            {
                return Helpers.GetDocumentModFilePath(Id);
            }
        }

        public PersistDocumentMeta Clone()
        {
            return new PersistDocumentMeta
            {
                Id = this.Id
            };
        }
    }
}
