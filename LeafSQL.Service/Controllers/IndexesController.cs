using LeafSQL.Engine.Exceptions;
using LeafSQL.Library;
using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Actions.Base;
using LeafSQL.Library.Payloads.Responses;
using System;
using System.Threading;
using System.Web.Http;

namespace LeafSQL.Service.Controllers
{
    public class IndexesController : ApiController
    {
        [HttpPost]
        public ActionResponseId Create([FromBody] ActionCreateIndex action)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(action.SessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            var result = new ActionResponseId();

            try
            {
                Guid newId = Guid.Empty;

                if (Program.Core.Indexes.Exists(processId, action.SchemaName, action.@Object.Name))
                {
                    throw new LeafSQLDuplicateObjectException($"An index already exist in the schema [{action.SchemaName}] with the name: [{action.@Object.Name}]");
                }

                Program.Core.Indexes.Create(processId, action.SchemaName, action.@Object, out newId);

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
        /// Deletes a single index.
        /// </summary>
        /// <param name="schema"></param>
        [HttpPost]
        public IActionResponse DeleteByName([FromBody] ActionGenericObject action)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(action.SessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            IActionResponse result = new ActionResponseBoolean();

            try
            {
                Program.Core.Indexes.DeleteByName(processId, action.SchemaName, action.ObjectName);
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
        [HttpPost]
        public IActionResponse RebuildByName([FromBody] ActionGenericObject action)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(action.SessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            IActionResponse result = new ActionResponseBoolean();

            try
            {
                Program.Core.Indexes.Rebuild(processId, action.SchemaName, action.ObjectName);
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
        [HttpPost]
        public ActionResponseBoolean ExistsByName([FromBody] ActionGenericObject action)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(action.SessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            ActionResponseBoolean result = new ActionResponseBoolean();

            try
            {
                result.Value = Program.Core.Indexes.Exists(processId, action.SchemaName, action.ObjectName);
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
        [HttpPost]
        public ActionResponseIndexes List([FromBody] ActionGenericObject action)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(action.SessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            var result = new ActionResponseIndexes();

            try
            {
                result.List = Program.Core.Indexes.List(processId, action.SchemaName);
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
