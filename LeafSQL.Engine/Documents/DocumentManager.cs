using LeafSQL.Engine.Exceptions;
using LeafSQL.Engine.Indexes;
using LeafSQL.Engine.Interfaces;
using LeafSQL.Engine.Query;
using LeafSQL.Engine.Schemas;
using LeafSQL.Engine.Sessions;
using LeafSQL.Engine.Transactions;
using LeafSQL.Library.Payloads.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Documents
{
    public class DocumentManager : CoreManagementBase
    {
        public DocumentManager(Core core) : base(core)
        {
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

                    var result = FindDocuments(txRef.Transaction, schemaMeta, preparedQuery.Conditions, preparedQuery.RowLimit, preparedQuery.SelectFields);

                    txRef.Commit();

                    return result;
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
                    List<string> intersectingDocumentIds = new List<string>();
                    HashSet<Guid> intersectedDocumentIds = new HashSet<Guid>();

                    foreach (var selectedIndex in indexSelections)
                    {
                        var indexPageCatalog = core.IO.GetPBuf<PersistIndexPageCatalog>(transaction, selectedIndex.Index.DiskPath, LockOperation.Read);
                        var targetedIndexConditions = (from o in conditions.Collection.Where(o => selectedIndex.HandledKeyNames.Contains(o.Key)) select o).ToList();
                        intersectedDocumentIds = core.Indexes.MatchDocuments(indexPageCatalog, targetedIndexConditions, intersectedDocumentIds);
                    }

                    //Now that we have elimiated all but the document IDs that we care about, all we
                    //  have to do is open each of them up and pull the content requeted in the field list.
                    if (intersectedDocumentIds.Count > 0)
                    {
                        var documentCatalog = core.IO.GetJson<PersistDocumentCatalog>(transaction, documentCatalogDiskPath, LockOperation.Read);

                        foreach (var intersectedDocumentId in intersectedDocumentIds)
                        {
                            var documentMeta = documentCatalog.GetById(intersectedDocumentId);

                            string documentDiskPath = Path.Combine(schemaMeta.DiskPath, Helpers.GetDocumentModFilePath(documentMeta.Id));
                            PersistDocument persistDocument = core.IO.GetJson<PersistDocument>(transaction, documentDiskPath, LockOperation.Read);
                            JObject jsonContent = JObject.Parse(persistDocument.Content);
                            QueryRow rowValues = new QueryRow();

                            if (rowLimit > 0 && results.Rows.Count > rowLimit)
                            {
                                break;
                            }

                            bool fullAttributeMatch = true;

                            //If we have any conditions that were not indexes, open the remainder
                            //  of the documents and do additonal document-level filtering.
                            if (indexSelections.UnhandledKeys?.Count > 0)
                            {
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
                            }

                            if (fullAttributeMatch)
                            {
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
                }

                return results;
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
