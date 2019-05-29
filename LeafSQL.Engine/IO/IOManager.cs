﻿using LeafSQL.Engine.Interfaces;
using LeafSQL.Engine.Transactions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using static LeafSQL.Engine.Constants;

namespace LeafSQL.Engine.IO
{
    public class IOManager : ICoreManagement
    {
        public Core core { get; set; }
        public IOManager(Core core)
        {
            this.core = core;
        }

        #region Getters.

        public T GetJsonNonTracked<T>(string filePath)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));
        }

        public T GetPBufNonTracked<T>(string filePath)
        {
            using (var file = File.OpenRead(filePath))
            {
                return ProtoBuf.Serializer.Deserialize<T>(file);
            }
        }

        public T GetJson<T>(Transaction transaction, string filePath, LockOperation intendedOperation)
        {
            return InternalTrackedGet<T>(transaction, filePath, intendedOperation, IOFormat.JSON);
        }

        public T GetPBuf<T>(Transaction transaction, string filePath, LockOperation intendedOperation)
        {
            return InternalTrackedGet<T>(transaction, filePath, intendedOperation, IOFormat.PBuf);
        }

        public T InternalTrackedGet<T>(Transaction transaction, string filePath, LockOperation intendedOperation, IOFormat format)
        {
            try
            {
                string cacheKey = Helpers.RemoveModFileName(filePath.ToLower());
                transaction.LockFile(intendedOperation, cacheKey);

                if (core.Settings.EnableIOCaching)
                {
                    var cachedObject = core.Cache.Get(cacheKey);

                    if (cachedObject != null)
                    {
                        core.Health.Increment(Constants.HealthCounterType.IOCacheReadHits);

                        core.Log.Trace(String.Format("IO:CacheHit:{0}->{1}", transaction.ProcessId, filePath));

                        return (T)cachedObject.Value;
                    }
                }

                core.Health.Increment(Constants.HealthCounterType.IOCacheReadMisses);

                core.Log.Trace(String.Format("IO:Read:{0}->{1}", transaction.ProcessId, filePath));

                T deserializedObject;

                if (format == IOFormat.JSON)
                {
                    string text = File.ReadAllText(filePath);
                    deserializedObject = JsonConvert.DeserializeObject<T>(text);
                }
                else if (format == IOFormat.PBuf)
                {
                    using (var file = File.OpenRead(filePath))
                    {
                        deserializedObject = ProtoBuf.Serializer.Deserialize<T>(file);
                        file.Close();
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }

                if (core.Settings.EnableIOCaching)
                {
                    core.Cache.Upsert(cacheKey, deserializedObject);
                    core.Health.Increment(Constants.HealthCounterType.IOCacheReadAdditions);
                }

                return deserializedObject;
            }
            catch (Exception ex)
            {
                core.Log.Write("Failed to get JSON object.", ex);
                throw;
            }
        }

        #endregion

        #region Putters.

        public void PutJsonNonTracked(string filePath, object deserializedObject)
        {
            File.WriteAllText(filePath, JsonConvert.SerializeObject(deserializedObject));
        }

        public void PutPBufNonTracked(string filePath, object deserializedObject)
        {
            using (var file = File.Create(filePath))
            {
                ProtoBuf.Serializer.Serialize(file, deserializedObject);
            }
        }

        public void PutJson(Transaction transaction, string filePath, object deserializedObject)
        {
            InternalTrackedPut(transaction, filePath, deserializedObject, IOFormat.JSON);
        }

        public void PutPBuf(Transaction transaction, string filePath, object deserializedObject)
        {
            InternalTrackedPut(transaction, filePath, deserializedObject, IOFormat.PBuf);
        }

        private void InternalTrackedPut(Transaction transaction, string filePath, object deserializedObject, IOFormat format)
        {
            try
            {
                string cacheKey = Helpers.RemoveModFileName(filePath.ToLower());
                transaction.LockFile(Constants.LockOperation.Write, cacheKey);

                bool deferDiskWrite = false;

                if (transaction != null)
                {
                    bool doesFileExist = File.Exists(filePath);

                    if (doesFileExist == false)
                    {
                        transaction.RecordFileCreate(filePath);
                    }
                    else
                    {
                        transaction.RecordFileAlter(filePath);
                    }

                    if (core.Settings.EnableDeferredIO && transaction.IsLongLived)
                    {
                        deferDiskWrite = transaction.DeferredIOs.RecordDeferredDiskIO(cacheKey, filePath, deserializedObject, format);
                    }
                }

                if (deferDiskWrite == false)
                {
                    core.Log.Trace(String.Format("IO:Write:{0}->{1}", transaction.ProcessId, filePath));

                    if (format == IOFormat.JSON)
                    {
                        string text = JsonConvert.SerializeObject(deserializedObject);
                        File.WriteAllText(filePath, text);
                    }
                    else if (format == IOFormat.PBuf)
                    {
                        using (var file = File.Create(filePath))
                        {
                            ProtoBuf.Serializer.Serialize(file, deserializedObject);
                            file.Close();
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else
                {
                    core.Log.Trace(String.Format("IO:Write-Deferred:{0}->{1}", transaction.ProcessId, filePath));
                }

                if (core.Settings.EnableIOCaching)
                {
                    core.Cache.Upsert(cacheKey, deserializedObject);
                    core.Health.Increment(Constants.HealthCounterType.IOCacheWriteAdditions);
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to put JSON file for session {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

        #endregion

        public bool DirectoryExists(Transaction transaction, string diskPath, LockOperation intendedOperation)
        {
            try
            {
                string cacheKey = Helpers.RemoveModFileName(diskPath.ToLower());
                transaction.LockDirectory(intendedOperation, cacheKey);

                core.Log.Trace(String.Format("IO:Exists-Directory:{0}->{1}", transaction.ProcessId, diskPath));

                return Directory.Exists(diskPath);
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to verify directory for session {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

        public void CreateDirectory(Transaction transaction, string diskPath)
        {
            try
            {
                string cacheKey = Helpers.RemoveModFileName(diskPath.ToLower());
                transaction.LockDirectory(Constants.LockOperation.Write, cacheKey);

                bool doesFileExist = Directory.Exists(diskPath);

                core.Log.Trace(String.Format("IO:Create-Directory:{0}->{1}", transaction.ProcessId, diskPath));

                if (doesFileExist == false)
                {
                    Directory.CreateDirectory(diskPath);
                    transaction.RecordDirectoryCreate(diskPath);
                }
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to create directory for session {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

        public bool FileExists(Transaction transaction, string filePath, LockOperation intendedOperation)
        {
            try
            {
                string lowerFilePath = filePath.ToLower();

                var deferredExists = transaction.DeferredIOs.Collection.Values.FirstOrDefault(o => o.LowerDiskPath == lowerFilePath);
                if (deferredExists != null)
                {
                    //The file might not yet exist, but its in the cache.
                    return true;
                }

                string cacheKey = Helpers.RemoveModFileName(lowerFilePath);
                transaction.LockFile(intendedOperation, cacheKey);

                core.Log.Trace(String.Format("IO:Exits-File:{0}->{1}", transaction.ProcessId, filePath));

                return File.Exists(filePath);
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to verify file for session {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

        public void DeleteFile(Transaction transaction, string filePath)
        {
            try
            {
                string cacheKey = Helpers.RemoveModFileName(filePath.ToLower());
                transaction.LockFile(Constants.LockOperation.Write, cacheKey);

                if (core.Settings.EnableIOCaching)
                {
                    core.Cache.Remove(cacheKey);
                }

                transaction.RecordFileDelete(filePath);

                core.Log.Trace(String.Format("IO:Delete-File:{0}->{1}", transaction.ProcessId, filePath));

                File.Delete(filePath);
                Helpers.RemoveDirectoryIfEmpty(Path.GetDirectoryName(filePath));
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to delete file for process {0}.", transaction.ProcessId), ex);
                throw;
            }
        }

        public void DeletePath(Transaction transaction, string diskPath)
        {
            try
            {
                string cacheKey = Helpers.RemoveModFileName(diskPath.ToLower());
                transaction.LockDirectory(Constants.LockOperation.Write, cacheKey);

                if (core.Settings.EnableIOCaching)
                {
                    core.Cache.RemoveStartsWith(cacheKey);
                }

                transaction.RecordPathDelete(diskPath);

                core.Log.Trace(String.Format("IO:Delete-Directory:{0}->{1}", transaction.ProcessId, diskPath));

                Directory.Delete(diskPath, true);
            }
            catch (Exception ex)
            {
                core.Log.Write(String.Format("Failed to delete path for process {0}.", transaction.ProcessId), ex);
                throw;
            }
        }
    }
}
