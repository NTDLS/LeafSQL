using LeafSQL.Engine.Interfaces;
using LeafSQL.Engine.Sessions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LeafSQL.Engine.Transactions
{
    public class TransactionManager : ICoreManagement
    {
        public List<Transaction> Collection = new List<Transaction>();
        public Core core { get; set; }

        public TransactionManager(Core core)
        {
            this.core = core;
        }

        public Transaction GetByProcessId(UInt64 processId)
        {
            lock (Collection)
            {
                var transaction = (from o in Collection where o.ProcessId == processId select o).FirstOrDefault();
                return transaction;
            }
        }

        public void RemoveByProcessId(UInt64 processId)
        {
            lock (Collection)
            {
                var transaction = GetByProcessId(processId);
                this.Collection.Remove(transaction);
            }
        }

        public void Recover()
        {
            try
            {
                core.Log.Write("Starting recovery.");

                Directory.CreateDirectory(core.Settings.TransactionDataPath);

                var transactionFiles = Directory.EnumerateFiles(core.Settings.TransactionDataPath, Constants.TransactionActionsFile, SearchOption.AllDirectories);

                if (transactionFiles.Count() > 0)
                {
                    core.Log.Write(string.Format("Found {0} open transactions.", transactionFiles.Count()), Constants.LogSeverity.Warning);
                }

                foreach (string transactionFile in transactionFiles)
                {
                    UInt64 processId = UInt64.Parse(Path.GetFileNameWithoutExtension(Path.GetDirectoryName(transactionFile)));

                    Transaction transaction = new Transaction(core, this, processId, true);

                    var reversibleActions = File.ReadLines(transactionFile).ToList();
                    foreach (var reversibleAction in reversibleActions)
                    {
                        transaction.ReversibleActions.Add(JsonConvert.DeserializeObject<ReversibleAction>(reversibleAction));
                    }

                    core.Log.Write(string.Format("Rolling back session {0} with {1} actions.",
                        transaction.ProcessId, transaction.ReversibleActions.Count), Constants.LogSeverity.Warning);

                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        core.Log.Write(string.Format("Failed to rollback transaction for process ID {0}.", transaction.ProcessId), ex);
                    }

                }

                core.Log.Write("Recovery complete.");
            }
            catch (Exception ex)
            {
                core.Log.Write("Could not recover uncomitted transations.", ex);
                throw;
            }
        }

        /// <summary>
        /// Begin an atomic operation. If the session already has an open transaction then its reference count is incremented and then decremented on TransactionReference.Dispose();
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public TransactionReference Begin(Session session, bool isLongLived)
        {
            try
            {
                lock (Collection)
                {
                    Transaction transaction = GetByProcessId(session.ProcessId);
                    if (transaction == null)
                    {
                        transaction = new Transaction(core, this, session.ProcessId, false)
                        {
                            IsLongLived = isLongLived
                        };

                        Collection.Add(transaction);
                    }

                    transaction.AddReference();

                    return new TransactionReference(transaction);
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to begin transaction for process {0}.", session.ProcessId), ex);
                throw;
            }
        }

        public TransactionReference Begin(Session session)
        {
            return Begin(session, false);
        }

        public void Commit(Session session)
        {
            try
            {
                var transaction = GetByProcessId(session.ProcessId);
                if (transaction != null)
                {
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to commit transaction for process {0}.", session.ProcessId), ex);
                throw;
            }
        }

        public void Rollback(Session session)
        {
            try
            {
                var transaction = GetByProcessId(session.ProcessId);
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to rollback transaction for process {0}.", session.ProcessId), ex);
                throw;
            }

        }
    }
}