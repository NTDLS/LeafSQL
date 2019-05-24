using LeafSQL.Library;
using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
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
        [HttpGet]
        //api/Namespace/List
        public ActionResponseDocuments List(Guid sessionId, string schema)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(sessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            var persistCatalog = Program.Core.Documents.EnumerateCatalog(processId, schema);

            var result = new ActionResponseDocuments();

            foreach (var catalogItem in persistCatalog)
            {
                result.Add(catalogItem.ToPayload());
            }

            return result;
        }

        public ActionResponseId Store(Guid sessionId, string schema, [FromBody]string value)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(sessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            var result = new ActionResponseId();

            try
            {
                var content = JsonConvert.DeserializeObject<Document>(value);

                Guid newId = Guid.Empty;

                Program.Core.Documents.Store(processId, schema, content, out newId);

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
        [HttpGet]
        //api/Document/{Namespace}/DeleteById/{Id}
        public IActionResponse DeleteById(Guid sessionId, string schema, Guid doc)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(sessionId);

            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            IActionResponse result = new IActionResponse();

            try
            {
                Program.Core.Documents.DeleteById(processId, schema, doc);
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
