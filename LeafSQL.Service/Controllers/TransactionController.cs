using LeafSQL.Library;
using LeafSQL.Library.Payloads.Actions.Base;
using LeafSQL.Library.Payloads.Responses;
using System;
using System.Threading;
using System.Web.Http;

namespace LeafSQL.Service.Controllers
{
    public class TransactionController : ApiController
    {
        [HttpPost]
        public IActionResponse Begin([FromBody]ActionGeneric action)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(action.SessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            IActionResponse result = new IActionResponse();

            try
            {
                Program.Core.Transactions.Begin(processId, true);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        [HttpPost]
        public IActionResponse Commit([FromBody]ActionGeneric action)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(action.SessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            IActionResponse result = new IActionResponse();

            try
            {
                Program.Core.Transactions.Commit(processId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        [HttpPost]
        public IActionResponse Rollback([FromBody]ActionGeneric action)
        {
            UInt64 processId = Program.Core.Sessions.SessionIdToProcessId(action.SessionId);
            Thread.CurrentThread.Name = string.Format("API:{0}:{1}", processId, Utility.GetCurrentMethod());
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            IActionResponse result = new IActionResponse();

            try
            {
                Program.Core.Transactions.Rollback(processId);
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
