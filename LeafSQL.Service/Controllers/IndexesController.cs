using LeafSQL.Engine.Exceptions;
using LeafSQL.Library;
using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Actions;
using LeafSQL.Library.Payloads.Responses;
using System;
using System.Threading;
using System.Web.Http;

namespace LeafSQL.Service.Controllers
{
    public class IndexesController : ApiController
    {
        [HttpPost]
        public ActionResponseId Create([FromBody] ActionRequestCreateIndex action)
        {
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            var result = new ActionResponseId();

            try
            {
                Guid newId = Guid.Empty;

                if (Program.Core.Indexes.Exists(session, action.SchemaName, action.@Object.Name))
                {
                    throw new LeafSQLDuplicateObjectException($"An index already exist in the schema [{action.SchemaName}] with the name: [{action.@Object.Name}]");
                }

                Program.Core.Indexes.Create(session, action.SchemaName, action.@Object, out newId);

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
        public ActionResponseBase DeleteByName([FromBody] ActionGenericObject action)
        {
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            ActionResponseBase result = new ActionResponseBoolean();

            try
            {
                Program.Core.Indexes.DeleteByName(session, action.SchemaName, action.ObjectName);
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
        public ActionResponseBase RebuildByName([FromBody] ActionGenericObject action)
        {
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            ActionResponseBase result = new ActionResponseBoolean();

            try
            {
                Program.Core.Indexes.Rebuild(session, action.SchemaName, action.ObjectName);
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
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            ActionResponseBoolean result = new ActionResponseBoolean();

            try
            {
                result.Value = Program.Core.Indexes.Exists(session, action.SchemaName, action.ObjectName);
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
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            var result = new ActionResponseIndexes();

            try
            {
                result.List = Program.Core.Indexes.List(session, action.SchemaName);
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
