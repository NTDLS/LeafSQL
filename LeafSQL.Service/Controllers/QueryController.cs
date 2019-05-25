using LeafSQL.Library;
using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Web.Http;

namespace LeafSQL.Service.Controllers
{
    public class QueryController : ApiController
    {
        [HttpPost]
        public IActionResponse Execute([FromBody]ActionRequestExecuteNonQuery action)
        {
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", session.ProcessId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            var result = new ActionResponseId();

            try
            {
                var statement = JsonConvert.DeserializeObject<string>(action.Statement);

                Program.Core.Query.Execute(session, statement);

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
