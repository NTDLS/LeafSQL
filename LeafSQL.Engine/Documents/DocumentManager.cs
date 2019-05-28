using LeafSQL.Library;
using System;
using System.Collections.Generic;
using System.IO;
using static LeafSQL.Engine.Constants;
using LeafSQL.Library.Payloads;
using LeafSQL.Engine.Schemas;
using LeafSQL.Engine.Exceptions;
using LeafSQL.Engine.Query;
using System.Linq;
using Newtonsoft.Json.Linq;
using LeafSQL.Engine.Transactions;
using LeafSQL.Engine.Sessions;
using LeafSQL.Library.Payloads.Models;
using LeafSQL.Engine.Indexes;

namespace LeafSQL.Engine.Documents
{
    public class DocumentManager
    {
        private Core core;

        public DocumentManager(Core core)
        {
            this.core = core;
        }

        public QueryResult ExecuteSelect(Session session, PreparedQuery preparedQuery)
        {
            try
            {
                using (var txRef = core.Transactions.Begin(session))
                {
                    var schemaMeta = core.Schemas.VirtualPathToMeta(txRef.Transaction, preparedQuery.Schema, LockOperation.Read);
                    if (schemaMeta == null || schemaMeta.Exists == false)
                    {
                        throw new LeafSQLSchemaDoesNotExistException(preparedQuery.Schema);
                    }

                    string documentCatalogDiskPath = Path.Combine(schemaMeta.DiskPath, Constants.DocumentCatalogFile);

                    return FindDocuments(txRef.Transaction, schemaMeta, preparedQuery.Conditions, preparedQuery.RowLimit, preparedQuery.SelectFields);

                    /*
                    var documentCatalog = core.IO.GetJson<PersistDocumentCatalog>(txRef.Transaction, documentCatalogDiskPath, LockOperation.Write);

                    var persistDocument = documentCatalog.GetById(newId);
                    if (persistDocument != null)
                    {
                        string documentDiskPath = Path.Combine(schemaMeta.DiskPath, Helpers.GetDocumentModFilePath(persistDocument.Id));

                        core.IO.DeleteFile(txRef.Transaction, documentDiskPath);

                        documentCatalog.Remove(persistDocument);

                        core.Indexes.DeleteDocumentFromIndexes(txRef.Transaction, schemaMeta, persistDocument.Id);

                        core.IO.PutJson(txRef.Transaction, documentCatalogDiskPath, documentCatalog);
                    }
                    */

                    txRef.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new LeafSQLExecutionException($"Failed to execute statement: {ex.Message}");
            }
        }

        private QueryResult FindDocuments(Transaction transaction, PersistSchema schemaMeta, Conditions conditions, int rowLimit, List<string> fieldList)
        {
            QueryResult results = new QueryResult();

            try
            {
                conditions.MakeLowerCase();

                if (fieldList.Count == 1 && fieldList[0] == "*")
                {
                    fieldList = null;
                }

                if (fieldList?.Count() > 0)
                {
                    foreach (var field in fieldList)
                    {
                        results.Columns.Add(new QueryColumn(field));
                    }
                }
                else
                {
                    results.Columns.Add(new QueryColumn("Id"));
                    results.Columns.Add(new QueryColumn("Created"));
                    results.Columns.Add(new QueryColumn("Modfied"));
                    results.Columns.Add(new QueryColumn("Content"));
                }

                bool hasFieldList = fieldList != null && fieldList.Count > 0;
                var indexSelections = core.Indexes.SelectIndexes(transaction, schemaMeta, conditions);

                string documentCatalogDiskPath = Path.Combine(schemaMeta.DiskPath, Constants.DocumentCatalogFile);

                if (indexSelections.Count == 0)
                {
                    var documentCatalog = core.IO.GetJson<PersistDocumentCatalog>(transaction, documentCatalogDiskPath, LockOperation.Read);

                    foreach (var documentMeta in documentCatalog.Collection)
                    {
                        string documentDiskPath = Path.Combine(schemaMeta.DiskPath, Helpers.GetDocumentModFilePath(documentMeta.Id));
                        PersistDocument persistDocument = core.IO.GetJson<PersistDocument>(transaction, documentDiskPath, LockOperation.Read);

                        JObject jsonContent = JObject.Parse(persistDocument.Content);
                        bool fullAttributeMatch = true;

                        foreach (Condition condition in conditions.Collection)
                        {
                            JToken jToken = null;

                            if (jsonContent.TryGetValue(condition.Key, StringComparison.CurrentCultureIgnoreCase, out jToken))
                            {
                                if (condition.IsMatch(jToken.ToString().ToLower()) == false)
                                {
                                    fullAttributeMatch = false;
                                    break;
                                }
                            }
                        }

                        if (fullAttributeMatch)
                        {
                            QueryRow rowValues = new QueryRow();

                            if (rowLimit > 0 && results.Rows.Count > rowLimit)
                            {
                                break;
                            }

                            if (hasFieldList)
                            {
                                if (jsonContent == null)
                                {
                                    jsonContent = JObject.Parse(persistDocument.Content);
                                }

                                foreach (string fieldName in fieldList)
                                {
                                    if (fieldName == "#RID")
                                    {
                                        rowValues.Add(persistDocument.Id.ToString());
                                    }
                                    else
                                    {
                                        JToken fieldToken = null;
                                        if (jsonContent.TryGetValue(fieldName, StringComparison.CurrentCultureIgnoreCase, out fieldToken))
                                        {
                                            rowValues.Add(fieldToken.ToString());
                                        }
                                        else
                                        {
                                            rowValues.Add(string.Empty);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //If no fields "*" was specified as the select list, just return the content of each document and some metadata.
                                rowValues.Add(persistDocument.Id.ToString());
                                rowValues.Add(persistDocument.Created.ToString());
                                rowValues.Add(persistDocument.Modfied.ToString());
                                rowValues.Add(persistDocument.Content);
                            }

                            results.Rows.Add(rowValues);
                        }
                    }
                }
                else
                {
                    foreach (var selectedIndex in indexSelections)
                    {
                        var indexPageCatalog = core.IO.GetPBuf<PersistIndexPageCatalog>(transaction, selectedIndex.Index.DiskPath, LockOperation.Read);

                        Console.WriteLine(selectedIndex.Index.DiskPath);
                    }
                }

                return results;

                /*
                var indexCatalog = GetIndexCatalog(txRef.Transaction, schemaMeta, LockOperation.Write);
                indexCatalog.Add(persistIndex);
                core.IO.PutJson(txRef.Transaction, indexCatalog.DiskPath, indexCatalog);

                persistIndex.DiskPath = Path.Combine(schemaMeta.DiskPath, MakeIndexFileName(index.Name));
                core.IO.PutPBuf(txRef.Transaction, persistIndex.DiskPath, new PersistIndexPageCatalog());



                List<int> intersectedDocuments = new List<int>();
                    bool firstLookup = true;

                    foreach (var indexSelection in indexSelections)
                    {
                        string indexPageCatalogFileName = Utility.MakePath(serverCore.Configuration.NamespacesPath, namespacePath, indexSelection.Index.Filename);
                        PersistIndexPageCatalog IndexPageCatalog = serverCore.IO.DeserializeFromProtoBufFile<PersistIndexPageCatalog>(indexPageCatalogFileName);

                        List<Condition> keyValues = new List<Condition>();

                        foreach (string attributeName in indexSelection.HandledKeyNames)
                        {
                            keyValues.Add((from o in conditions.Collection where o.Key == attributeName select o).First());
                        }

                        List<int> foundIndexPages = null;

                        //Get all index pages that match the key values.
                        if (indexSelection.Index.Attributes.Count == keyValues.Count)
                        {
                            if (indexSelection.Index.IndexType == IndexType.Unique)
                            {
                                planExplanationNode.Operation = PlanOperation.FullUniqueIndexMatchScan;
                            }
                            else
                            {
                                planExplanationNode.Operation = PlanOperation.FullIndexMatchScan;
                            }

                            foundIndexPages = IndexPageCatalog.FindDocuments(keyValues, planExplanationNode);
                        }
                        else
                        {
                            planExplanationNode.Operation = PlanOperation.PartialIndexMatchScan;
                            foundIndexPages = IndexPageCatalog.FindDocuments(keyValues, planExplanationNode);
                        }

                        //By default, FindPagesByPartialKey and FindPageByExactKey report ResultingNodes in "pages". Convert to documents.
                        planExplanationNode.ResultingNodes = foundIndexPages.Count;

                        if (firstLookup)
                        {
                            firstLookup = false;
                            //If we do not currently have any items in the result, then add the ones we just found.
                            intersectedDocuments.AddRange(foundIndexPages);
                        }
                        else
                        {
                            //Each time we do a subsequent lookup, find the intersection of the IDs from
                            //  this lookup and the previous looksup and make it our result.
                            //In this way, we continue to limit down the resulting rows by each subsequent index lookup.
                            intersectedDocuments = foundIndexPages.Intersect(intersectedDocuments).ToList();
                        }

                        planExplanationNode.IntersectedNodes = intersectedDocuments.Count;
                        planExplanationNode.Duration = explainStepDuration.Elapsed;
                        explanation.Steps.Add(planExplanationNode);

                        if (intersectedDocuments.Count == 0)
                        {
                            break; //Early termination, all rows eliminated.
                        }
                    }

                    List<Document> resultDocuments = new List<Document>();

                    var unindexedConditions = conditions.Collection.Where(p => indexSelections.UnhandledKeys.Any(p2 => p2 == p.Key)).ToList();

                    bool foundKey = false;
                    Stopwatch documentScanExplanationDuration = new Stopwatch();

                    PlanExplanationNode documentScanExplanationNode = null;

                    if (unindexedConditions.Count == 0 || intersectedDocuments.Count == 0)
                    {
                        foundKey = true;
                    }
                    else
                    {
                        documentScanExplanationDuration.Start();
                        documentScanExplanationNode = new PlanExplanationNode()
                        {
                            CoveredAttributes = (from o in unindexedConditions select o.Key).ToList(),
                            Operation = PlanOperation.DocumentScan
                        };
                    }

                    foreach (int documentId in intersectedDocuments)
                    {
                        if (documentScanExplanationNode != null)
                        {
                            documentScanExplanationNode.ScannedNodes++;
                        }

                        string persistDocumentFile = Utility.MakePath(serverCore.Configuration.NamespacesPath,
                            namespacePath,
                            PersistIndexPageCatalog.DocumentFileName(documentId));

                        timer.Restart();
                        PersistDocument persistDocument = serverCore.IO.DeserializeFromJsonFile<PersistDocument>(persistDocumentFile);
                        timer.Stop();
                        serverCore.PerformaceMetrics.AddDeserializeDocumentMs(timer.ElapsedMilliseconds);

                        bool fullAttributeMatch = true;

                        JObject jsonContent = null;

                        //If we have unindexed attributes, then open each of the documents from the previous index scans and compare the remining values.
                        if (unindexedConditions.Count > 0)
                        {
                            jsonContent = JObject.Parse(persistDocument.Text);

                            foreach (Condition condition in unindexedConditions)
                            {
                                JToken jToken = null;

                                if (jsonContent.TryGetValue(condition.Key, StringComparison.CurrentCultureIgnoreCase, out jToken))
                                {
                                    foundKey = true; //TODO: Implement this on the index scan!

                                    if (condition.IsMatch(jToken.ToString().ToLower()) == false)
                                    {
                                        fullAttributeMatch = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    fullAttributeMatch = false;
                                    break;
                                }
                            }
                        }

                        if (fullAttributeMatch)
                        {
                            rowCount++;
                            if (rowLimit > 0 && rowCount > rowLimit)
                            {
                                break;
                            }

                            if (documentScanExplanationNode != null)
                            {
                                documentScanExplanationNode.ResultingNodes++;
                            }

                            if (hasFieldList)
                            {
                                if (jsonContent == null)
                                {
                                    jsonContent = JObject.Parse(persistDocument.Text);
                                }

                                List<string> fieldValues = new List<string>();

                                foreach (string fieldName in fieldList)
                                {
                                    if (fieldName == "#RID")
                                    {
                                        fieldValues.Add(persistDocument.Id.ToString());
                                    }
                                    else
                                    {
                                        JToken fieldToken = null;
                                        if (jsonContent.TryGetValue(fieldName, StringComparison.CurrentCultureIgnoreCase, out fieldToken))
                                        {
                                            fieldValues.Add(fieldToken.ToString());
                                        }
                                        else
                                        {
                                            fieldValues.Add(string.Empty);
                                        }
                                    }
                                }

                                rowValues.Add(fieldValues);
                            }
                            else
                            {
                                resultDocuments.Add(new Document
                                {
                                    Id = persistDocument.Id,
                                    OriginalType = persistDocument.OriginalType,
                                    Bytes = persistDocument.Bytes
                                });
                            }
                        }
                    }

                    if (documentScanExplanationNode != null)
                    {
                        documentScanExplanationNode.Duration = documentScanExplanationDuration.Elapsed;
                        documentScanExplanationNode.IntersectedNodes = resultDocuments.Count;
                        explanation.Steps.Add(documentScanExplanationNode);
                    }
                    */
            }
            catch (Exception ex)
            {
                throw new LeafSQLExecutionException(ex.Message);
            }
        }

        public void Store(Session session, string schema, Library.Payloads.Models.Document document, out Guid newId)
        {
            try
            {
                var persistDocument = PersistDocument.FromPayload(document);

                if (persistDocument.Id == Guid.Empty)
                {
                    persistDocument.Id = Guid.NewGuid();
                }
                if (persistDocument.Created == DateTime.MinValue)
                {
                    persistDocument.Created = DateTime.UtcNow;
                }
                if (persistDocument.Modfied == DateTime.MinValue)
                {
                    persistDocument.Modfied = DateTime.UtcNow;
                }

                using (var txRef = core.Transactions.Begin(session))
                {
                    var schemaMeta = core.Schemas.VirtualPathToMeta(txRef.Transaction, schema, LockOperation.Write);
                    if (schemaMeta == null || schemaMeta.Exists == false)
                    {
                        throw new LeafSQLSchemaDoesNotExistException(schema);
                    }

                    string documentCatalogDiskPath = Path.Combine(schemaMeta.DiskPath, Constants.DocumentCatalogFile);

                    var documentCatalog = core.IO.GetJson<PersistDocumentCatalog>(txRef.Transaction, documentCatalogDiskPath, LockOperation.Write);
                    documentCatalog.Add(persistDocument);
                    core.IO.PutJson(txRef.Transaction, documentCatalogDiskPath, documentCatalog);

                    string documentDiskPath = Path.Combine(schemaMeta.DiskPath, Helpers.GetDocumentModFilePath(persistDocument.Id));
                    core.IO.CreateDirectory(txRef.Transaction, Path.GetDirectoryName(documentDiskPath));
                    core.IO.PutJson(txRef.Transaction, documentDiskPath, persistDocument);

                    core.Indexes.InsertDocumentIntoIndexes(txRef.Transaction, schemaMeta, persistDocument);

                    newId = document.Id;

                    txRef.Commit();
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to store document for process {0}.", session.ProcessId), ex);
                throw;
            }
        }

        public void DeleteById(Session session, string schema, Guid newId)
        {
            try
            {
                using (var txRef = core.Transactions.Begin(session))
                {
                    var schemaMeta = core.Schemas.VirtualPathToMeta(txRef.Transaction, schema, LockOperation.Write);
                    if (schemaMeta == null || schemaMeta.Exists == false)
                    {
                        throw new LeafSQLSchemaDoesNotExistException(schema);
                    }

                    string documentCatalogDiskPath = Path.Combine(schemaMeta.DiskPath, Constants.DocumentCatalogFile);

                    var documentCatalog = core.IO.GetJson<PersistDocumentCatalog>(txRef.Transaction, documentCatalogDiskPath, LockOperation.Write);

                    var persistDocument = documentCatalog.GetById(newId);
                    if (persistDocument != null)
                    {
                        string documentDiskPath = Path.Combine(schemaMeta.DiskPath, Helpers.GetDocumentModFilePath(persistDocument.Id));

                        core.IO.DeleteFile(txRef.Transaction, documentDiskPath);

                        documentCatalog.Remove(persistDocument);

                        core.Indexes.DeleteDocumentFromIndexes(txRef.Transaction, schemaMeta, persistDocument.Id);

                        core.IO.PutJson(txRef.Transaction, documentCatalogDiskPath, documentCatalog);
                    }

                    txRef.Commit();
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to delete document by ID for process {0}.", session.ProcessId), ex);
                throw;
            }
        }

        public List<PersistDocumentMeta> EnumerateCatalog(Session session, string schema)
        {
            try
            {
                using (var txRef = core.Transactions.Begin(session))
                {
                    PersistSchema schemaMeta = core.Schemas.VirtualPathToMeta(txRef.Transaction, schema, LockOperation.Read);
                    if (schemaMeta == null || schemaMeta.Exists == false)
                    {
                        throw new LeafSQLSchemaDoesNotExistException(schema);
                    }

                    var list = new List<PersistDocumentMeta>();

                    var filePath = Path.Combine(schemaMeta.DiskPath, Constants.DocumentCatalogFile);
                    var documentCatalog = core.IO.GetJson<PersistDocumentCatalog>(txRef.Transaction, filePath, LockOperation.Read);

                    foreach (var item in documentCatalog.Collection)
                    {
                        list.Add(item);
                    }

                    txRef.Commit();

                    return list;
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to get catalog for process {0}.", session.ProcessId), ex);
                throw;
            }
        }
    }
}
