using LeafSQL.Library;
using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Responses;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Web.Http;

namespace LeafSQL.Service.Controllers
{
    public class SecurityController : ApiController
    {
        //api/Security/Login
        public ActionResponceLogin Login([FromBody]string value)
        {
            var result = new ActionResponceLogin();

            try
            {
                Thread.CurrentThread.Name = string.Format("API:{0}", Utility.GetCurrentMethod());
                Program.Core.Log.Trace(Thread.CurrentThread.Name);

                var loginRequest = JsonConvert.DeserializeObject<LoginRequest>(value);
                result.SessionId = Program.Core.Security.Login(loginRequest.Username, loginRequest.PasswordHash);
                result.ProcessId = Program.Core.Sessions.SessionIdToProcessId(result.SessionId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Login failed with an exception: " + ex.Message;
            }
            return result;
        }

        [HttpGet]
        //api/Security/{sessionId}/Logout
        public IActionResponse Logout(Guid sessionId)
        {
            IActionResponse result = new IActionResponse();

            try
            {
                Thread.CurrentThread.Name = string.Format("API:{0}", Utility.GetCurrentMethod());
                Program.Core.Log.Trace(Thread.CurrentThread.Name);

                Program.Core.Sessions.LogoutSession(sessionId);
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
