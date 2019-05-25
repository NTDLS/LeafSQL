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
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            IActionResponse result = new IActionResponse();

            try
            {
                Program.Core.Transactions.Begin(session, true);
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
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            IActionResponse result = new IActionResponse();

            try
            {
                Program.Core.Transactions.Commit(session);
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
            var session = Program.Core.Sessions.GetSession(action.SessionId);
            Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
            Program.Core.Log.Trace(Thread.CurrentThread.Name);

            IActionResponse result = new IActionResponse();

            try
            {
                Program.Core.Transactions.Rollback(session);
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
