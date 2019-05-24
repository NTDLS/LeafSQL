using LeafSQL.Library;
using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Web.Http;

namespace LeafSQL.Service.Controllers
{
    public class QueryController : ApiController
    {
        public IActionResponse Execute(Guid sessionId, [FromBody]string value)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(sessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            var result = new ActionResponseId();

            try
            {
                var statement = JsonConvert.DeserializeObject<string>(value);

                Program.Core.Query.Execute(processId, statement);

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
