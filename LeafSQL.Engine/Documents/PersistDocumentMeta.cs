using System;

namespace LeafSQL.Engine.Documents
{
    [Serializable]
    public class PersistDocumentMeta
    {
        public Guid Id { get; set; }

        public Library.Payloads.Models.DocumentMeta ToPayload()
        {
            return new Library.Payloads.Models.DocumentMeta()
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
