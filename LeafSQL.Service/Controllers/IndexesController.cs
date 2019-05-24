using LeafSQL.Engine.Exceptions;
using LeafSQL.Library;
using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Web.Http;

namespace LeafSQL.Service.Controllers
{
    public class IndexesController : ApiController
    {
        public ActionResponseId Create(Guid sessionId, string schema, [FromBody]string value)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(sessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            var result = new ActionResponseId();

            try
            {
                var index = JsonConvert.DeserializeObject<Index>(value);

                Guid newId = Guid.Empty;

                if (Program.Core.Indexes.Exists(processId, schema, index.Name))
                {
                    throw new LeafSQLDuplicateObjectException($"An index already exist in the schema [{schema}] with the name: [{index.Name}]");
                }

                Program.Core.Indexes.Create(processId, schema, index, out newId);

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
        /// Rebuilds a single index.
        /// </summary>
        /// <param name="schema"></param>
        [HttpGet]
        public IActionResponse Rebuild(Guid sessionId, string schema, string byName)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(sessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            IActionResponse result = new ActionResponseBoolean();

            try
            {
                Program.Core.Indexes.Rebuild(processId, schema, byName);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Checks for the existence of an index.
        /// </summary>
        /// <param name="schema"></param>
        [HttpGet]
        public ActionResponseBoolean Exists(Guid sessionId, string schema, string byName)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(sessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            ActionResponseBoolean result = new ActionResponseBoolean();

            try
            {
                result.Value = Program.Core.Indexes.Exists(processId, schema, byName);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Gets a list of indexes for a specific schema.
        /// </summary>
        /// <param name="schema"></param>
        [HttpGet]
        public ActionResponseIndexes List(Guid sessionId, string schema)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(sessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            var result = new ActionResponseIndexes();

            try
            {
                result.List = Program.Core.Indexes.List(processId, schema);
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
