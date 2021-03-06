﻿using LeafSQL.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeafSQL.Engine.Sessions
{
    public class SessionManager : CoreManagementBase
    {

        private UInt64 nextProcessId = 1;
        public List<Session> Collection { get; set; }

        public SessionManager(Core core) : base(core)
        {
            Collection = new List<Session>();
        }

        public Session LoginSession(Guid loginId, Guid sessionId)
        {
            lock (Collection)
            {
                var session = Collection.Where(o => o.SessionId == sessionId).FirstOrDefault();
                if (session != null)
                {
                    return session;
                }
                else
                {
                    session = new Session()
                    {
                        LoginId = loginId,
                        SessionId = sessionId,
                        ProcessId = nextProcessId++
                    };

                    Collection.Add(session);

                    return session;
                }
            }
        }

        public void LogoutSession(Guid sessionId)
        {
            lock (Collection)
            {
                var session = Collection.Where(o => o.SessionId == sessionId).FirstOrDefault();
                if(session != null)
                {
                    Collection.Remove(session);
                }
            }
        }

        public Session GetSession(UInt64 processId)
        {
            lock (Collection)
            {
                var session = Collection.Where(o => o.ProcessId == processId).FirstOrDefault();
                if (session != null)
                {
                    return session;
                }
                else
                {
                    throw new Exception("The sesson was not found because either the user has not logged in or the session has expired.");
                }
            }
        }

        public Session GetSession(Guid sessionId)
        {
            lock (Collection)
            {
                var session = Collection.Where(o => o.SessionId == sessionId).FirstOrDefault();
                if (session != null)
                {
                    return session;
                }
                else
                {
                    throw new Exception("The sesson was not found because either the user has not logged in or the session has expired.");
                }
            }
        }
    }
}
