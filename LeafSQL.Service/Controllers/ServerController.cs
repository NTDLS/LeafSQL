using LeafSQL.Library;
using LeafSQL.Library.Payloads.Actions.Base;
using LeafSQL.Library.Payloads.Responses;
using System;
using System.Threading;
using System.Web.Http;

namespace LeafSQL.Service.Controllers
{
    public class ServerController : ApiController
    {
        // GET api/Server/Settings
        [HttpPost]
        public ActionResponseServerSettings Settings([FromBody]ActionGeneric action)
        {
            var result = new ActionResponseServerSettings();

            try
            {
                var session = Program.Core.Sessions.GetSession(action.SessionId);
                Thread.CurrentThread.Name = string.Format("API:{0}:{1}", session.ProcessId, Utility.GetCurrentMethod());
                Program.Core.Log.Trace(Thread.CurrentThread.Name);

                result.Settings = Program.Core.Settings;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
