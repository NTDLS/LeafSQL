using LeafSQL.Engine.Interfaces;
using System;

namespace LeafSQL.Engine.Locking
{
    public class LockManager : CoreManagementBase
    {
        public ObjectLocks Locks { get; set; }

        public LockManager(Core core) : base(core)
        {
            Locks = new ObjectLocks(core);
        }

        public void Remove(ObjectLock objectLock)
        {
            lock (CriticalSections.AcquireLock)
            {
                try
                {
                    Locks.Remove(objectLock);
                }
                catch (Exception ex)
                {
                    core.Log.Write("Failed to remove lock.", ex);
                    throw;
                }
            }
        }
    }
}
