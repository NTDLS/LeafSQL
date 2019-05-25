using LeafSQL.Library;
using LeafSQL.Library.Payloads.Actions.Base;
using LeafSQL.Library.Payloads.Responses;
using System;
using System.Threading;
using System.Web.Http;

namespace LeafSQL.Service.Controllers
{
    public class SchemaController : ApiController
    {
        /// <summary>
        /// Lists the existing namespaces within a given namespace.
        /// </summary>
        /// <param name="schema"></param>
        //api/Namespace/List
        [HttpPost]
        public ActionResponseSchemas List([FromBody]ActionGenericObject action)
        {
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            ActionResponseSchemas result = new ActionResponseSchemas();

            try
            {
                var persistSchemas = Program.Core.Schemas.GetList(session, action.SchemaName);

                foreach (var persistSchema in persistSchemas)
                {
                    result.Add(persistSchema.ToPayload());
                }

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Creates a single namespace or an entire namespace path.
        /// </summary>
        /// <param name="schema"></param>
        //api/Namespace/{Namespace}/Create
        [HttpPost]
        public IActionResponse Create([FromBody]ActionGenericObject action)
        {
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            IActionResponse result = new IActionResponse();

            try
            {
                Program.Core.Schemas.Create(session, action.SchemaName);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Checks for the existence of a schema.
        /// </summary>
        /// <param name="schema"></param>
        //api/Namespace/{Namespace}/Exists
        [HttpPost]
        public ActionResponseBoolean Exists([FromBody]ActionGenericObject action)
        {
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            ActionResponseBoolean result = new ActionResponseBoolean();

            try
            {
                result.Value = Program.Core.Schemas.Exists(session, action.SchemaName);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Drops a single namespace or an entire namespace path.
        /// </summary>
        /// <param name="schema"></param>
        //api/Namespace/{Namespace}/Drop
        [HttpPost]
        public IActionResponse Drop([FromBody]ActionGenericObject action)
        {
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            IActionResponse result = new IActionResponse();

            try
            {
                Program.Core.Schemas.Drop(session, action.SchemaName);
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
