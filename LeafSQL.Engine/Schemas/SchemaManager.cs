﻿using LeafSQL.Engine.Documents;
using LeafSQL.Engine.Exceptions;
using LeafSQL.Engine.Indexes;
using LeafSQL.Engine.Interfaces;
using LeafSQL.Engine.Sessions;
using LeafSQL.Engine.Transactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.Schemas
{
    public class SchemaManager : CoreManagementBase
    {
        private string rootCatalogFile;
        private PersistSchema rootSchemaMeta = null;

        public PersistSchema RootSchemaMeta
        {
            get
            {
                if (rootSchemaMeta == null)
                {
                    rootSchemaMeta = new PersistSchema()
                    {
                        Id = Constants.RootSchemaGUID,
                        DiskPath = core.Settings.DataRootPath,
                        VirtualPath = string.Empty,
                        Exists = true,
                        Name = string.Empty,
                    };
                }
                return rootSchemaMeta;
            }
        }

        public SchemaManager(Core core) : base(core)
        {
            rootCatalogFile = Path.Combine(core.Settings.DataRootPath, Constants.SchemaCatalogFile);

            //If the catalog doesnt exist, create a new empty one.
            if (File.Exists(rootCatalogFile) == false)
            {
                Directory.CreateDirectory(core.Settings.DataRootPath);

                core.IO.PutJsonNonTracked(Path.Combine(core.Settings.DataRootPath, Constants.SchemaCatalogFile), new PersistSchemaCatalog());
                core.IO.PutJsonNonTracked(Path.Combine(core.Settings.DataRootPath, Constants.DocumentCatalogFile), new PersistDocumentCatalog());
                core.IO.PutJsonNonTracked(Path.Combine(core.Settings.DataRootPath, Constants.IndexCatalogFile), new PersistIndexCatalog());
            }
        }

        public List<PersistSchema> GetChildrenMeta(Transaction transaction, PersistSchema node, LockOperation intendedOperation)
        {
            List<PersistSchema> metaList = new List<PersistSchema>();

            string namespaceCatalogDiskPath = Path.Combine(node.DiskPath, Constants.SchemaCatalogFile);

            if (core.IO.FileExists(transaction, namespaceCatalogDiskPath, intendedOperation))
            {
                var namespaceCatalog = core.IO.GetJson<PersistSchemaCatalog>(transaction, namespaceCatalogDiskPath, intendedOperation);

                foreach (var catalogItem in namespaceCatalog.Collection)
                {
                    metaList.Add(new PersistSchema()
                    {
                        DiskPath = node.DiskPath + "\\" + catalogItem.Name,
                        Exists = true,
                        Id = catalogItem.Id,
                        Name = catalogItem.Name,
                        VirtualPath = node.VirtualPath + ":" + catalogItem.Name
                    });
                }
            }

            return metaList;
        }

        public PersistSchema GetParentMeta(Transaction transaction, PersistSchema child, LockOperation intendedOperation)
        {
            try
            {
                if (child == RootSchemaMeta)
                {
                    return null;
                }
                var segments = child.VirtualPath.Split(':').ToList();
                segments.RemoveAt(segments.Count - 1);
                string parentNs = string.Join(":", segments);
                return VirtualPathToMeta(transaction, parentNs, intendedOperation);
            }
            catch (Exception ex)
            {
                core.Log.Write("Failed to get parent namespace meta.", ex);
                throw;
            }
        }

        public PersistSchema VirtualPathToMeta(Transaction transaction, string schemaPath, LockOperation intendedOperation)
        {
            try
            {
                schemaPath = schemaPath.Trim(new char[] { ':' }).Trim();

                if (schemaPath == string.Empty)
                {
                    return RootSchemaMeta;
                }
                else
                {
                    var segments = schemaPath.Split(':');
                    string schemaName = segments[segments.Count() - 1];

                    string namespaceDiskPath = Path.Combine(core.Settings.DataRootPath, String.Join("\\", segments));
                    string parentSchemaDiskPath = Directory.GetParent(namespaceDiskPath).FullName;

                    string parentCatalogDiskPath = Path.Combine(parentSchemaDiskPath, Constants.SchemaCatalogFile);

                    if (core.IO.FileExists(transaction, parentCatalogDiskPath, intendedOperation) == false)
                    {
                        throw new LeafSQLInvalidSchemaException(string.Format("The schema [{0}] does not exist.", schemaPath));
                    }

                    var parentCatalog = core.IO.GetJson<PersistSchemaCatalog>(transaction,
                        Path.Combine(parentSchemaDiskPath, Constants.SchemaCatalogFile), intendedOperation);

                    var namespaceMeta = parentCatalog.GetByName(schemaName);
                    if (namespaceMeta != null)
                    {
                        namespaceMeta.Name = schemaName;
                        namespaceMeta.DiskPath = namespaceDiskPath;
                        namespaceMeta.VirtualPath = schemaPath;
                        namespaceMeta.Exists = true;
                    }
                    else
                    {
                        namespaceMeta = new PersistSchema()
                        {
                            Name = schemaName,
                            DiskPath = core.Settings.DataRootPath + "\\" + schemaPath.Replace(':', '\\'),
                            VirtualPath = schemaPath,
                            Exists = false
                        };
                    }

                    transaction.LockDirectory(intendedOperation, namespaceMeta.DiskPath);

                    return namespaceMeta;
                }
            }
            catch (Exception ex)
            {
                core.Log.Write("Failed to translate virtual path to namespace meta.", ex);
                throw;
            }
        }

        public List<PersistSchema> GetList(Session session, string schema)
        {
            try
            {
                using (var txRef = core.Transactions.Begin(session))
                {
                    PersistSchema schemaMeta = VirtualPathToMeta(txRef.Transaction, schema, LockOperation.Read);
                    if (schemaMeta == null || schemaMeta.Exists == false)
                    {
                        throw new LeafSQLSchemaDoesNotExistException(schema);
                    }

                    var list = new List<PersistSchema>();

                    var filePath = Path.Combine(schemaMeta.DiskPath, Constants.SchemaCatalogFile);
                    var namespaceCatalog = core.IO.GetJson<PersistSchemaCatalog>(txRef.Transaction, filePath, LockOperation.Read);

                    foreach (var item in namespaceCatalog.Collection)
                    {
                        list.Add(item);
                    }

                    txRef.Commit();

                    return list;
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to get namespace list for session {0}.", session.ProcessId), ex);
                throw;
            }
        }

        private void CreateSingle(Session session, string schema)
        {
            try
            {
                using (var txRef = core.Transactions.Begin(session))
                {
                    Guid newSchemaId = Guid.NewGuid();

                    var schemaMeta = VirtualPathToMeta(txRef.Transaction, schema, LockOperation.Write);
                    if (schemaMeta.Exists)
                    {
                        txRef.Commit();
                        //The namespace already exists.
                        return;
                    }

                    var parentSchemaMeta = GetParentMeta(txRef.Transaction, schemaMeta, LockOperation.Write);
                    if (parentSchemaMeta.Exists == false)
                    {
                        throw new Exception("The parent namespace does not exists. use CreateLineage() instead of CreateSingle().");
                    }

                    string parentSchemaCatalogFile = Path.Combine(parentSchemaMeta.DiskPath, Constants.SchemaCatalogFile);
                    PersistSchemaCatalog parentCatalog = null;

                    parentCatalog = core.IO.GetJson<PersistSchemaCatalog>(txRef.Transaction, parentSchemaCatalogFile, LockOperation.Write);

                    string filePath = string.Empty;

                    core.IO.CreateDirectory(txRef.Transaction, schemaMeta.DiskPath);

                    //Create default namespace catalog file.
                    filePath = Path.Combine(schemaMeta.DiskPath, Constants.SchemaCatalogFile);
                    if (core.IO.FileExists(txRef.Transaction, filePath, LockOperation.Write) == false)
                    {
                        core.IO.PutJson(txRef.Transaction, filePath, new PersistSchemaCatalog());
                    }

                    //Create default document catalog file.
                    filePath = Path.Combine(schemaMeta.DiskPath, Constants.DocumentCatalogFile);
                    if (core.IO.FileExists(txRef.Transaction, filePath, LockOperation.Write) == false)
                    {
                        core.IO.PutJson(txRef.Transaction, filePath, new PersistDocumentCatalog());
                    }

                    //Create default index catalog file.
                    filePath = Path.Combine(schemaMeta.DiskPath, Constants.IndexCatalogFile);
                    if (core.IO.FileExists(txRef.Transaction, filePath, LockOperation.Write) == false)
                    {
                        core.IO.PutJson(txRef.Transaction, filePath, new PersistIndexCatalog());
                    }

                    if (parentCatalog.ContainsName(schema) == false)
                    {
                        parentCatalog.Add(new PersistSchema
                        {
                            Id = newSchemaId,
                            Name = schemaMeta.Name
                        });

                        core.IO.PutJson(txRef.Transaction, parentSchemaCatalogFile, parentCatalog);
                    }

                    txRef.Commit();
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to create single namespace for session {0}.", session.ProcessId), ex);
                throw;
            }
        }

        /// <summary>
        /// Creates a structure of namespaces, denotaed by colons.
        /// </summary>
        /// <param name="namespacePath"></param>
        public void Create(Session session, string schemaPath)
        {
            try
            {
                var segments = schemaPath.Split(':');

                StringBuilder pathBuilder = new StringBuilder();

                foreach (string name in segments)
                {
                    pathBuilder.Append(name);
                    CreateSingle(session, pathBuilder.ToString());
                    pathBuilder.Append(":");
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to create namespace lineage for session {0}.", session.ProcessId), ex);
                throw;
            }
        }

        /// <summary>
        /// Returns true if the schema exists.
        /// </summary>
        /// <param name="namespacePath"></param>
        public bool Exists(Session session, string schemaPath)
        {
            try
            {
                bool result = false;

                using (var txRef = core.Transactions.Begin(session))
                {
                    var segments = schemaPath.Split(':');

                    StringBuilder pathBuilder = new StringBuilder();

                    foreach (string name in segments)
                    {
                        pathBuilder.Append(name);
                        var schema = VirtualPathToMeta(txRef.Transaction, pathBuilder.ToString(), LockOperation.Read);

                        result = (schema != null && schema.Exists);

                        if (result == false)
                        {
                            break;
                        }

                        pathBuilder.Append(":");
                    }

                    txRef.Commit();
                    return result;
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to confirm namespace for session {0}.", session.ProcessId), ex);
                throw;
            }
        }

        /// <summary>
        /// Drops a single namespace or an entire namespace path.
        /// </summary>
        /// <param name="schema"></param>
        public void Drop(Session session, string schema)
        {
            try
            {
                using (var txRef = core.Transactions.Begin(session))
                {
                    var segments = schema.Split(':');
                    string schemaName = segments[segments.Count() - 1];

                    var schemaMeta = VirtualPathToMeta(txRef.Transaction, schema, LockOperation.Write);
                    if (schemaMeta == null || schemaMeta.Exists == false)
                    {
                        throw new LeafSQLSchemaDoesNotExistException(schema);
                    }

                    var parentSchemaMeta = GetParentMeta(txRef.Transaction, schemaMeta, LockOperation.Write);

                    string parentSchemaCatalogFile = Path.Combine(parentSchemaMeta.DiskPath, Constants.SchemaCatalogFile);
                    var parentCatalog = core.IO.GetJson<PersistSchemaCatalog>(txRef.Transaction, parentSchemaCatalogFile, LockOperation.Write);

                    var nsItem = parentCatalog.Collection.FirstOrDefault(o => o.Name == schemaName);
                    if (nsItem != null)
                    {
                        parentCatalog.Collection.Remove(nsItem);

                        core.IO.DeletePath(txRef.Transaction, schemaMeta.DiskPath);

                        core.IO.PutJson(txRef.Transaction, parentSchemaCatalogFile, parentCatalog);
                    }

                    txRef.Commit();
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to drop namespace for session {0}.", session.ProcessId), ex);
                throw;
            }
        }
    }
}
