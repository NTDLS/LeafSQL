using LeafSQL.Engine.Interfaces;
using System;

namespace LeafSQL.Engine.Locking
{
    public class LockManager : ICoreManagement
    {
        public ObjectLocks Locks { get; set; }
        public Core core { get; set; }

        public LockManager(Core core)
        {
            this.core = core;
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
