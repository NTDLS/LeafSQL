using System;
using System.Collections.Generic;

namespace LeafSQL.Engine.Sessions
{
    public class SessionManager
    {
        private Core core;

        private UInt64 nextProcessId = 1;

        public Dictionary<Guid, UInt64> Collection { get; set; }

        public SessionManager(Core core)
        {
            this.core = core;
            Collection = new Dictionary<Guid, ulong>();
        }

        public UInt64 LoginSession(Guid sessionId)
        {
            lock (Collection)
            {
                if (Collection.ContainsKey(sessionId))
                {
                    return Collection[sessionId];
                }
                else
                {
                    UInt64 processId = nextProcessId++;
                    Collection.Add(sessionId, processId);
                    return processId;
                }
            }
        }

        public void LogoutSession(Guid sessionId)
        {
            lock (Collection)
            {
                if (Collection.ContainsKey(sessionId))
                {
                    Collection.Remove(sessionId);
                }
            }
        }

        public UInt64 SessionIdToProcessId(Guid sessionId)
        {
            lock (Collection)
            {
                if (Collection.ContainsKey(sessionId))
                {
                    return Collection[sessionId];
                }
                else
                {
                    throw new Exception("The sesson was not found because either the user has not logged in or the session has expired.");
                }
            }
        }
    }
}
