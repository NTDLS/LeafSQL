using LeafSQL.Engine.Documents;
using LeafSQL.Engine.Exceptions;
using LeafSQL.Engine.Interfaces;
using LeafSQL.Engine.Query;
using LeafSQL.Engine.Schemas;
using LeafSQL.Engine.Sessions;
using LeafSQL.Engine.Transactions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Indexes
{
    public class IndexManager : CoreManagementBase
    {
        public IndexManager(Core core) : base(core)
        {
        }

        /// <summary>
        /// Returns a list of conditon groups and an associated list of supporting indexes to satisify each group (nested conditions).
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="schemaMeta"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public List<IndexSelections> SelectIndexes(Transaction transaction, PersistSchema schemaMeta, Conditions conditions)
        {
            List<IndexSelections> indexSelections = new List<IndexSelections>();
            IndexSelections indexSelection = new IndexSelections(conditions);

            SelectIndexes(transaction, schemaMeta, conditions.Root, ref indexSelection);
            indexSelections.Add(indexSelection);

            if (conditions.Children != null && conditions.Children.Count > 0)
            {
                foreach (var childConditions in conditions.Children)
                {
                    var childIndexSelections = SelectIndexes(transaction, schemaMeta, childConditions);
                    if (childIndexSelections != null && childIndexSelections.Count > 0)
                    {
                        indexSelections.AddRange(childIndexSelections);
                    }
                }
            }

            return indexSelections;
        }

        private void SelectIndexes(Transaction transaction, PersistSchema schemaMeta, List<Condition> conditions, ref IndexSelections indexSelection)
        {
            try
            {
                var indexCatalog = GetIndexCatalog(transaction, schemaMeta, LockOperation.Read);
                List<PotentialIndex> potentialIndexs = new List<PotentialIndex>();

                var indexConditions = new IndexConditions(conditions);

                //Loop though each index in the schema and create a list of all indexes which could potentially be used to match the conditions.
                foreach (var indexMeta in indexCatalog.Collection)
                {
                    var indexHandledCondition = new List<IndexHandledCondition>();

                    for (int i = 0; i < indexMeta.Attributes.Count; i++)
                    {
                        var indexConditonMatches = indexConditions.FindAll(o => o.Key == indexMeta.Attributes[i].Name.ToLower() && o.Handled == false);

                        if (indexConditonMatches.Count > 0)
                        {
                            foreach (var indexConditonMatche in indexConditonMatches)
                            {
                                indexHandledCondition.Add(new IndexHandledCondition(indexConditonMatche, i));
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (indexHandledCondition.Count > 0)
                    {
                        potentialIndexs.Add(new PotentialIndex(indexMeta, indexHandledCondition));
                    }
                }

                //Group the indexes by their first attribute.
                var distinctFirstAttributes = potentialIndexs.Select(o => o.Index.Attributes[0].Name).Distinct();
                foreach (var distinctFirstAttribute in distinctFirstAttributes)
                {
                    //Find all idexes with the same first attribute:
                    var indexGroup = potentialIndexs.Where(o => o.FirstAttributeName == distinctFirstAttribute);

                    //For the group of indexes, find the one index that handles the most keys but also has the fewest atributes.
                    var firstIndexInGroup = (from o in indexGroup select o)
                        .OrderByDescending(s => s.IndexHandledConditions.Count)
                        .ThenBy(t => t.Index.Attributes.Count).FirstOrDefault();

                    foreach (var indexHandledCondition in firstIndexInGroup.IndexHandledConditions)
                    {
                        //Mark the keys which are handled by this index as "handled".
                        var handledKeys = (from o in indexConditions where o.Id == indexHandledCondition.Id select o).ToList();
                        foreach (var handledKey in handledKeys)
                        {
                            handledKey.Handled = true;
                        }
                    }

                    indexSelection.Add(new IndexSelection(firstIndexInGroup.Index, firstIndexInGroup.IndexHandledConditions));
                }

                indexSelection.UnhandledKeys.AddRange((from o in indexConditions where o.Handled == false select o.Key).ToList());
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to select indexes for process {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

        public List<Library.Payloads.Models.Index> List(Session session, string schema)
        {
            var result = new List<Library.Payloads.Models.Index>();
            try
            {
                using (var txRef = core.Transactions.Begin(session))
                {
                    var schemaMeta = core.Schemas.VirtualPathToMeta(txRef.Transaction, schema, LockOperation.Read);
                    if (schemaMeta != null && schemaMeta.Exists)
                    {
                        var indexCatalog = GetIndexCatalog(txRef.Transaction, schemaMeta, LockOperation.Read);
                        if (indexCatalog != null)
                        {
                            foreach (var index in indexCatalog.Collection)
                            {
                                result.Add(index.ToPayload());
                            }
                        }
                    }

                    txRef.Commit();
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to list indexes for process {0}.", session.ProcessId), ex);
                throw;
            }

            return result;
        }


        public bool Exists(Session session, string schema, string indexName)
        {
            bool result = false;
            try
            {
                using (var txRef = core.Transactions.Begin(session))
                {
                    var schemaMeta = core.Schemas.VirtualPathToMeta(txRef.Transaction, schema, LockOperation.Read);
                    if (schemaMeta != null && schemaMeta.Exists)
                    {
                        var indexCatalog = GetIndexCatalog(txRef.Transaction, schemaMeta, LockOperation.Write);
                        if (indexCatalog != null)
                        {
                            result = indexCatalog.GetByName(indexName) != null;
                        }
                    }

                    txRef.Commit();
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to create index for process {0}.", session.ProcessId), ex);
                throw;
            }

            return result;
        }

        public void Create(Session session, string schema, Library.Payloads.Models.Index index, out Guid newId)
        {
            try
            {
                var persistIndex = PersistIndex.FromPayload(index);

                if (persistIndex.Id == Guid.Empty)
                {
                    persistIndex.Id = Guid.NewGuid();
                }
                if (persistIndex.Created == DateTime.MinValue)
                {
                    persistIndex.Created = DateTime.UtcNow;
                }
                if (persistIndex.Modfied == DateTime.MinValue)
                {
                    persistIndex.Modfied = DateTime.UtcNow;
                }

                using (var txRef = core.Transactions.Begin(session))
                {
                    var schemaMeta = core.Schemas.VirtualPathToMeta(txRef.Transaction, schema, LockOperation.Read);
                    if (schemaMeta == null || schemaMeta.Exists == false)
                    {
                        throw new LeafSQLSchemaDoesNotExistException(schema);
                    }

                    var indexCatalog = GetIndexCatalog(txRef.Transaction, schemaMeta, LockOperation.Write);
                    indexCatalog.Add(persistIndex);
                    core.IO.PutJson(txRef.Transaction, indexCatalog.DiskPath, indexCatalog);

                    persistIndex.DiskPath = Path.Combine(schemaMeta.DiskPath, MakeIndexFileName(index.Name));
                    core.IO.PutPBuf(txRef.Transaction, persistIndex.DiskPath, new PersistIndexPageCatalog());

                    RebuildIndex(txRef.Transaction, schemaMeta, persistIndex);

                    newId = persistIndex.Id;

                    txRef.Commit();
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to create index for process {0}.", session.ProcessId), ex);
                throw;
            }
        }

        public void DeleteById(Session session, string schema, Guid indexId)
        {
            try
            {
                using (var txRef = core.Transactions.Begin(session))
                {
                    var schemaMeta = core.Schemas.VirtualPathToMeta(txRef.Transaction, schema, LockOperation.Read);
                    if (schemaMeta == null || schemaMeta.Exists == false)
                    {
                        throw new LeafSQLSchemaDoesNotExistException(schema);
                    }

                    var indexCatalog = GetIndexCatalog(txRef.Transaction, schemaMeta, LockOperation.Write);

                    var existingIndex = indexCatalog.Collection.Find(o => o.Id == indexId);
                    if (existingIndex == null)
                    {
                        throw new Exceptions.LeafSQLIndexDoesNotExistException($"The index id [{indexId}] does not exist in ThreadExceptionEventArgs schema[{schema}].");
                    }

                    core.IO.DeleteFile(txRef.Transaction, existingIndex.DiskPath);

                    indexCatalog.Collection.Remove(existingIndex);

                    core.IO.PutJson(txRef.Transaction, indexCatalog.DiskPath, indexCatalog);

                    txRef.Commit();
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to create index for process {0}.", session.ProcessId), ex);
                throw;
            }
        }

        public void DeleteByName(Session session, string schema, string indexName)
        {
            try
            {
                using (var txRef = core.Transactions.Begin(session))
                {
                    var schemaMeta = core.Schemas.VirtualPathToMeta(txRef.Transaction, schema, LockOperation.Read);
                    if (schemaMeta == null || schemaMeta.Exists == false)
                    {
                        throw new LeafSQLSchemaDoesNotExistException(schema);
                    }

                    var indexCatalog = GetIndexCatalog(txRef.Transaction, schemaMeta, LockOperation.Write);

                    var existingIndex = indexCatalog.Collection.Find(o => o.Name.ToLower() == indexName.ToLower());
                    if (existingIndex == null)
                    {
                        throw new Exceptions.LeafSQLIndexDoesNotExistException($"The index [{indexName}] does not exist in ThreadExceptionEventArgs schema[{schema}].");
                    }

                    core.IO.DeleteFile(txRef.Transaction, existingIndex.DiskPath);

                    indexCatalog.Collection.Remove(existingIndex);

                    core.IO.PutJson(txRef.Transaction, indexCatalog.DiskPath, indexCatalog);

                    txRef.Commit();
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to create index for process {0}.", session.ProcessId), ex);
                throw;
            }
        }

        public void Rebuild(Session session, string schema, string indexName)
        {
            try
            {           
                using (var txRef = core.Transactions.Begin(session))
                {
                    var schemaMeta = core.Schemas.VirtualPathToMeta(txRef.Transaction, schema, LockOperation.Read);
                    if (schemaMeta == null || schemaMeta.Exists == false)
                    {
                        throw new LeafSQLSchemaDoesNotExistException(schema);
                    }

                    var indexCatalog = GetIndexCatalog(txRef.Transaction, schemaMeta, LockOperation.Write);

                    var indexMeta = indexCatalog.GetByName(indexName);
                    if (indexMeta == null)
                    {
                        throw new LeafSQLIndexDoesNotExistException(schema);
                    }

                    indexMeta.DiskPath = Path.Combine(schemaMeta.DiskPath, MakeIndexFileName(indexMeta.Name));

                    RebuildIndex(txRef.Transaction, schemaMeta, indexMeta);
   
                    txRef.Commit();
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to rebuild index for process {0}.", session.ProcessId), ex);
                throw;
            }
        }

        private PersistIndexCatalog GetIndexCatalog(Transaction transaction, string schema, LockOperation intendedOperation)
        {
            var schemaMeta = core.Schemas.VirtualPathToMeta(transaction, schema, intendedOperation);
            return GetIndexCatalog(transaction, schemaMeta, intendedOperation);
        }

        public string MakeIndexFileName(string indexName)
        {
            return string.Format("@Idx_{0}_Pages.PBuf", Helpers.MakeSafeFileName(indexName));
        }

        private PersistIndexCatalog GetIndexCatalog(Transaction transaction, PersistSchema schemaMeta, LockOperation intendedOperation)
        {
            string indexCatalogDiskPath = Path.Combine(schemaMeta.DiskPath, Constants.IndexCatalogFile);
            var indexCatalog = core.IO.GetJson<PersistIndexCatalog>(transaction, indexCatalogDiskPath, intendedOperation);
            indexCatalog.DiskPath = indexCatalogDiskPath;

            foreach (var index in indexCatalog.Collection)
            {
                index.DiskPath = Path.Combine(schemaMeta.DiskPath, MakeIndexFileName(index.Name));
            }

            return indexCatalog;
        }

        private List<string> GetIndexSearchTokens(Transaction transaction, PersistIndex indexMeta, PersistDocument document)
        {
            try
            {
                List<string> result = new List<string>();

                foreach (var indexAttribute in indexMeta.Attributes)
                {
                    var jsonContent = JObject.Parse(document.Content);
                    JToken jToken = null;
                    if (jsonContent.TryGetValue(indexAttribute.Name, StringComparison.CurrentCultureIgnoreCase, out jToken))
                    {
                        result.Add(jToken.ToString());
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to build index search tokens for process {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

        /// <summary>
        /// Finds document IDs given a set of conditions.
        /// </summary>
        /// <param name="indexPageCatalog"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public HashSet<Guid> MatchDocuments(PersistIndexPageCatalog indexPageCatalog, List<Condition> conditions)
        {
            HashSet<Guid> globallyFoundDocumentIds = new HashSet<Guid>();
            MatchDocuments(indexPageCatalog.Leaves, conditions, 0, globallyFoundDocumentIds);
            return globallyFoundDocumentIds;
        }

        /// <summary>
        /// Finds document IDs given a set of conditions.
        /// </summary>
        /// <param name="persistIndexLeaves"></param>
        /// <param name="conditions"></param>
        /// <param name="foundDocumentIds"></param>
        private void MatchDocuments(PersistIndexExtent persistIndexLeaves, List<Condition> conditions, int conditionOrdinal, HashSet<Guid> globallyFoundDocumentIds)
        {
            HashSet<Guid> sessionFoundDocumentIds = new HashSet<Guid>();

            //TODO: This is broken, very broken.
            // We have scenarios where we have ANDs/ORs and nested ANDs/ORs.
            // We also have to support the same key being used in an OR (e.g. Color = 'BLACK' OR Color = 'Silver')

            foreach (var leaf in persistIndexLeaves.Leaves)
            {
                Condition condition = conditions[conditionOrdinal];

                if (condition.IsMatch(leaf.Key) == true)
                {
                    if (conditions.Count == conditionOrdinal + 1)
                    {
                        //We have exausted all of our conditons, go ahead and skip to the document IDs.
                        foreach (var documentId in leaf.Coalesce())
                        {
                            if (condition.ConditionType == ConditionType.None) //None means this is the first condition in a group.
                            {
                                sessionFoundDocumentIds.Add(documentId);
                            }
                            else if (condition.ConditionType == ConditionType.And)
                            {
                                if (globallyFoundDocumentIds.Contains(documentId))
                                {
                                    sessionFoundDocumentIds.Add(documentId);
                                }
                            }
                            else if (condition.ConditionType == ConditionType.Or)
                            {
                                sessionFoundDocumentIds.Add(documentId);
                            }
                            else
                            {
                                throw new LeafSQLExceptionBase("Unsupported expression type.");
                            }

                        }
                    }
                    else if (leaf.IsBottom) //This is the bottom of the index, where the doucment IDs are stored.
                    {
                        //We have matched all of the index attributes.
                        foreach (var documentId in leaf.DocumentIDs)
                        {
                            if (condition.ConditionType == ConditionType.None) //None means this is the first condition in a group.
                            {
                                sessionFoundDocumentIds.Add(documentId);
                            }
                            else if (condition.ConditionType == ConditionType.And)
                            {
                                if (globallyFoundDocumentIds.Contains(documentId))
                                {
                                    sessionFoundDocumentIds.Add(documentId);
                                }
                            }
                            else if (condition.ConditionType == ConditionType.Or)
                            {
                                sessionFoundDocumentIds.Add(documentId);
                            }
                            else
                            {
                                throw new LeafSQLExceptionBase("Unsupported expression type.");
                            }
                        }
                    }
                    else
                    {
                        //Match the next condition to the next lowest leaf level.
                        MatchDocuments(leaf.Extent, conditions, conditionOrdinal + 1, globallyFoundDocumentIds);
                        return;
                    }
                }
            }

            //globallyFoundDocumentIds.Add(
        }

        /// <summary>
        // Finds the appropriate index page for a set of key values.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="indexMeta"></param>
        /// <param name="searchTokens"></param>
        /// <returns></returns>
        private FindKeyPageResult FindKeyPage(Transaction transaction, PersistIndex indexMeta, List<string> searchTokens)
        {
            return FindKeyPage(transaction, indexMeta, searchTokens, null);
        }

        /// <summary>
        /// Finds the appropriate index page for a set of key values using a long lived index page catalog.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="indexMeta"></param>
        /// <param name="searchTokens"></param>
        /// <param name="indexPageCatalog"></param>
        /// <returns></returns>
        private FindKeyPageResult FindKeyPage(Transaction transaction, PersistIndex indexMeta, List<string> searchTokens, PersistIndexPageCatalog suppliedIndexPageCatalog)
        {
            try
            {
                var indexPageCatalog = suppliedIndexPageCatalog;
                if (indexPageCatalog == null)
                {
                    indexPageCatalog = core.IO.GetPBuf<PersistIndexPageCatalog>(transaction, indexMeta.DiskPath, LockOperation.Write);
                }

                lock (indexPageCatalog)
                {
                    FindKeyPageResult result = new FindKeyPageResult()
                    {
                        Catalog = indexPageCatalog
                    };

                    result.Extent = result.Catalog.Leaves;
                    if (result.Extent == null || result.Extent.Count == 0)
                    {
                        //The index is empty.
                        return result;
                    }

                    int foundExtentCount = 0;

                    foreach (var token in searchTokens)
                    {
                        bool locatedExtent = false;

                        foreach (var leaf in result.Extent.Leaves)
                        {
                            if (leaf.Key == token)
                            {
                                locatedExtent = true;
                                foundExtentCount++;
                                result.Leaf = leaf;
                                result.Extent = leaf.Extent; //Move one level lower in the extent tree.

                                result.IsPartialMatch = true;
                                result.ExtentLevel = foundExtentCount;

                                if (foundExtentCount == searchTokens.Count)
                                {
                                    result.IsPartialMatch = false;
                                    result.IsFullMatch = true;
                                    return result;
                                }
                            }
                        }

                        if (locatedExtent == false)
                        {
                            return result;
                        }
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to locate key page for process {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

        /// <summary>
        /// Updates an index entry for a single document into each index in the schema.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public void UpdateDocumentIntoIndexes(Transaction transaction, PersistSchema schemaMeta, PersistDocument document)
        {
            try
            {
                var indexCatalog = GetIndexCatalog(transaction, schemaMeta, LockOperation.Read);

                //Loop though each index in the schema.
                foreach (var indexMeta in indexCatalog.Collection)
                {
                    DeleteDocumentFromIndex(transaction, schemaMeta, indexMeta, document.Id);
                    InsertDocumentIntoIndex(transaction, schemaMeta, indexMeta, document);
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Multi-index insert failed for process {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

        /// <summary>
        /// Inserts an index entry for a single document into each index in the schema.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="schema"></param>
        /// <param name="document"></param>
        public void InsertDocumentIntoIndexes(Transaction transaction, PersistSchema schemaMeta, PersistDocument document)
        {
            try
            {
                var indexCatalog = GetIndexCatalog(transaction, schemaMeta, LockOperation.Read);

                //Loop though each index in the schema.
                foreach (var indexMeta in indexCatalog.Collection)
                {
                    InsertDocumentIntoIndex(transaction, schemaMeta, indexMeta, document);
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Multi-index insert failed for process {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

        /// <summary>
        /// Inserts an index entry for a single document into a single index.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="schemaMeta"></param>
        /// <param name="indexMeta"></param>
        /// <param name="document"></param>
        private void InsertDocumentIntoIndex(Transaction transaction, PersistSchema schemaMeta, PersistIndex indexMeta, PersistDocument document)
        {
            InsertDocumentIntoIndex(transaction, schemaMeta, indexMeta, document, null, true);
        }

        /// <summary>
        /// Inserts an index entry for a single document into a single index using a long lived index page catalog.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="schemaMeta"></param>
        /// <param name="indexMeta"></param>
        /// <param name="document"></param>
        private void InsertDocumentIntoIndex(Transaction transaction, PersistSchema schemaMeta, PersistIndex indexMeta, PersistDocument document, PersistIndexPageCatalog indexPageCatalog, bool flushPageCatalog)
        {
            try
            {
                var searchTokens = GetIndexSearchTokens(transaction, indexMeta, document);
                var findResult = FindKeyPage(transaction, indexMeta, searchTokens, indexPageCatalog);

                if (searchTokens.Count == 0)
                {
                    //None of the supplied index attributes could be found on this document.
                    return;
                }

                //If we found a full match for all supplied key values - add the document to the leaf collection.
                if (findResult.IsFullMatch)
                {
                    if (findResult.Leaf.DocumentIDs == null)
                    {
                        findResult.Leaf.DocumentIDs = new HashSet<Guid>();
                    }

                    if (indexMeta.IsUnique && findResult.Leaf.DocumentIDs.Count > 1)
                    {
                        string exceptionText = string.Format("Duplicate key violation occurred for index [{0}]/[{1}]. Values: {{{2}}}",
                            schemaMeta.VirtualPath, indexMeta.Name, string.Join(",", searchTokens));

                        throw new LeafSQLDuplicateKeyViolation(exceptionText);
                    }

                    findResult.Leaf.DocumentIDs.Add(document.Id);
                    if (flushPageCatalog)
                    {
                        core.IO.PutPBuf(transaction, indexMeta.DiskPath, findResult.Catalog);
                    }
                }
                else
                {
                    //If we didn't find a full match for all supplied key values,
                    //  then create the tree and add the document to the lowest leaf.
                    //Note that we are going to start creating the leaf level at the findResult.ExtentLevel.
                    //  This is because we may have a partial match and don't need to create the full tree.
                    lock (indexPageCatalog)
                    {
                        for (int i = findResult.ExtentLevel; i < searchTokens.Count; i++)
                        {
                            findResult.Leaf = findResult.Extent.AddNewleaf(searchTokens[i]);
                            findResult.Extent = findResult.Leaf.Extent;
                        }

                        if (findResult.Leaf == null || findResult.Leaf.DocumentIDs == null)
                        {
                            findResult.Leaf.DocumentIDs = new HashSet<Guid>();
                        }

                        findResult.Leaf.DocumentIDs.Add(document.Id);
                    }
                    if (flushPageCatalog)
                    {
                        core.IO.PutPBuf(transaction, indexMeta.DiskPath, findResult.Catalog);
                    }
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Index document insert failed for process {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

        class RebuildIndexItemThreadProc_ParallelState
        {
            public int ThreadsCompleted { get; set; }
            public int ThreadsStarted { get; set; }
            public int TargetThreadCount { get; set; }
            public bool Success { get; set; } = true;
            public StringBuilder Exception { get; set; } = new StringBuilder();
            public bool Cancel { get; set; }

            public bool IsComplete
            {
                get
                {
                    lock (this)
                    {
                        return (ThreadsStarted - ThreadsCompleted) == 0;
                    }
                }
            }
        }

        class RebuildIndexItemThreadProc_Params
        {
            public RebuildIndexItemThreadProc_ParallelState State { get; set; }
            public Transaction Transaction { get; set; }
            public PersistSchema SchemaMeta { get; set; }
            public PersistIndex IndexMeta { get; set; }
            public PersistDocumentCatalog DocumentCatalog { get; set; }
            public PersistIndexPageCatalog IndexPageCatalog { get; set; }
            public AutoResetEvent Initialized { get; set; }
            public RebuildIndexItemThreadProc_Params()
            {
                Initialized = new AutoResetEvent(false);
            }
        }

        void RebuildIndexItemThreadProc(object oParam)
        {
            RebuildIndexItemThreadProc_Params param = (RebuildIndexItemThreadProc_Params)oParam;

            try
            {
                int threadMod = 0;

                lock (param.State)
                {
                    threadMod = param.State.ThreadsStarted;
                    param.State.ThreadsStarted++;
                    Thread.CurrentThread.Name = $"IDXTHD_{param.Transaction.Session.InstanceKey}_{param.State.ThreadsStarted}";
                    param.Initialized.Set();
                }

                for (int i = 0; i < param.DocumentCatalog.Collection.Count; i++)
                {
                    if ((i % param.State.TargetThreadCount) == threadMod)
                    {
                        var documentCatalogItem = param.DocumentCatalog.Collection[i];
                        string documentDiskPath = Path.Combine(param.SchemaMeta.DiskPath, documentCatalogItem.FileName);
                        var persistDocument = core.IO.GetJson<PersistDocument>(param.Transaction, documentDiskPath, LockOperation.Read);
                        InsertDocumentIntoIndex(param.Transaction, param.SchemaMeta, param.IndexMeta, persistDocument, param.IndexPageCatalog, false);
                    }

                    if (param.State.Cancel)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                param.State.Success = false;
                param.State.Cancel = true;

                lock (param.State.Exception)
                {
                    param.State.Exception.Append(ex.Message);
                }
            }

            lock (param.State)
            {
                param.State.ThreadsCompleted++;
            }
        }

        /// <summary>
        /// Inserts all documents in a schema into a single index in the schema.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="schemaMeta"></param>
        /// <param name="indexMeta"></param>
        private void RebuildIndex(Transaction transaction, PersistSchema schemaMeta, PersistIndex indexMeta)
        {
            try
            {
                var filePath = Path.Combine(schemaMeta.DiskPath, Constants.DocumentCatalogFile);
                var documentCatalog = core.IO.GetJson<PersistDocumentCatalog>(transaction, filePath, LockOperation.Read);

                //Clear out the existing index pages.
                core.IO.PutPBuf(transaction, indexMeta.DiskPath, new PersistIndexPageCatalog());

                var indexPageCatalog = core.IO.GetPBuf<PersistIndexPageCatalog>(transaction, indexMeta.DiskPath, LockOperation.Write);

                var state = new RebuildIndexItemThreadProc_ParallelState()
                {
                    TargetThreadCount = Environment.ProcessorCount * 2,

                };

                state.TargetThreadCount = 8;

                var param = new RebuildIndexItemThreadProc_Params()
                {
                    DocumentCatalog = documentCatalog,
                    State = state,
                    IndexMeta = indexMeta,
                    IndexPageCatalog = indexPageCatalog,
                    SchemaMeta = schemaMeta,
                    Transaction = transaction
                };

                for (int i = 0; i < state.TargetThreadCount; i++)
                {
                    new Thread(RebuildIndexItemThreadProc).Start(param);
                    param.Initialized.WaitOne(Timeout.Infinite);
                }

                while (state.IsComplete == false)
                {
                    Thread.Sleep(1);
                }

                if (state.Success == false)
                {
                    throw new LeafSQLExceptionBase($"Failed to build index: {state.Exception.ToString()}");
                }

                core.IO.PutPBuf(transaction, indexMeta.DiskPath, indexPageCatalog);
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to rebuild single index for process {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

        public void DeleteDocumentFromIndexes(Transaction transaction, PersistSchema schemaMeta, Guid documentId)
        {
            try
            {
                var indexCatalog = GetIndexCatalog(transaction, schemaMeta, LockOperation.Read);

                //Loop though each index in the schema.
                foreach (var indexMeta in indexCatalog.Collection)
                {
                    DeleteDocumentFromIndex(transaction, schemaMeta, indexMeta, documentId);
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Multi-index upsert failed for process {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

        private bool RemoveDocumentFromLeaves(ref PersistIndexExtent leaves, Guid documentId)
        {
            foreach (var leaf in leaves)
            {
                if (leaf.DocumentIDs != null && leaf.DocumentIDs.Count > 0)
                {
                    if (leaf.DocumentIDs.Remove(documentId))
                    {
                        return true; //We found the document and removed it.
                    }
                }

                if (leaf.Extent != null && leaf.Extent.Count > 0)
                {
                    if (RemoveDocumentFromLeaves(ref leaf.Extent, documentId))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Removes a document from an index.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="schemaMeta"></param>
        /// <param name="indexMeta"></param>
        /// <param name="document"></param>
        private void DeleteDocumentFromIndex(Transaction transaction, PersistSchema schemaMeta, PersistIndex indexMeta, Guid documentId)
        {
            try
            {
                var persistIndexPageCatalog = core.IO.GetPBuf<PersistIndexPageCatalog>(transaction, indexMeta.DiskPath, LockOperation.Write);

                if (RemoveDocumentFromLeaves(ref persistIndexPageCatalog.Leaves, documentId))
                {
                    core.IO.PutPBuf(transaction, indexMeta.DiskPath, persistIndexPageCatalog);
                }                
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Index document upsert failed for process {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

    }
}
