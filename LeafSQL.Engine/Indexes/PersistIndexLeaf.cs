using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeafSQL.Engine.Indexes
{
    [ProtoContract]
    public class PersistIndexLeaf
    {
        [ProtoMember(1)]
        public string Key { get; set; }
        [ProtoMember(2)]
        public HashSet<Guid> DocumentIDs = null;
        [ProtoMember(3)]
        public PersistIndexExtent Extent = new PersistIndexExtent();

        /// <summary>
        /// Returns all document IDs from the bottom level of the extent.
        /// </summary>
        /// <returns></returns>
        public List<Guid> Coalesce()
        {
            List<Guid> documentIds = new List<Guid>();

            if (DocumentIDs != null)
            {
                documentIds.AddRange(DocumentIDs); //If this is the bottom, just return all the doucment IDs.
            }

            //...Otherwise we need to traverse to the end of each entent and grab the document IDs.

            foreach (var leaf in Extent)
            {
                var childDocumentIDs = leaf.Coalesce();
                if (documentIds != null)
                {
                    documentIds.AddRange(childDocumentIDs);
                }
            }
            return documentIds;
        }

        /// <summary>
        /// Returns true if this is the level of the index that contains the document IDs.
        /// </summary>
        [ProtoIgnore]
        public bool IsBottom
        {
            get
            {
                return (DocumentIDs != null && DocumentIDs.FirstOrDefault() != null);
            }
        }

        public PersistIndexLeaf()
        {

        }

        public PersistIndexLeaf(string key)
        {
            Key = key;
        }
    }
}
