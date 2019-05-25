﻿using LeafSQL.Engine.Sessions;
using LeafSQL.Library;
using LeafSQL.Library.Payloads.Actions;
using LeafSQL.Library.Payloads.Actions.Base;
using LeafSQL.Library.Payloads.Responses;
using System;
using System.Threading;
using System.Web.Http;

namespace LeafSQL.Service.Controllers
{
    public class DocumentController : ApiController
    {
        /// <summary>
        /// Lists the documents within a given namespace.
        /// </summary>
        /// <param name="schema"></param>
        //api/Namespace/List
        [HttpPost]
        public ActionResponseDocuments List([FromBody] ActionGenericObject action)
        {
            Session session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", session.ProcessId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            var persistCatalog = Program.Core.Documents.EnumerateCatalog(session, action.SchemaName);

            var result = new ActionResponseDocuments();

            foreach (var catalogItem in persistCatalog)
            {
                result.Add(catalogItem.ToPayload());
            }

            return result;
        }

        [HttpPost]
        public ActionResponseId Store([FromBody] ActionRequestStoreDocument action)
        {
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", session.ProcessId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            var result = new ActionResponseId();

            try
            {
                Guid newId = Guid.Empty;

                Program.Core.Documents.Store(session, action.SchemaName, action.Object, out newId);

                result.Id = newId;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Deletes a single document by its Id.
        /// </summary>
        /// <param name="schema"></param>
        //api/Document/{Namespace}/DeleteById/{Id}
        [HttpPost]
        public IActionResponse DeleteById([FromBody] ActionGenericObject action)
        {
            var session = Program.Core.Sessions.GetSession(action.SessionId);

            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", session.ProcessId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            IActionResponse result = new IActionResponse();

            try
            {
                Program.Core.Documents.DeleteById(session, action.SchemaName, action.ObjectId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
