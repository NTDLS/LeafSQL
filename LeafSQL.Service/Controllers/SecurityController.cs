using LeafSQL.Library;
using LeafSQL.Library.Payloads;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Web.Http;

namespace LeafSQL.Service.Controllers
{
    public class SecurityController : ApiController
    {
        //api/Security/Login
        public LoginResponse Login([FromBody]string value)
        {
            try
            {
                Thread.CurrentThread.Name = string.Format("API:{0}", Utility.GetCurrentMethod());
                Program.Core.Log.Trace(Thread.CurrentThread.Name);

                var loginRequest = JsonConvert.DeserializeObject<LoginRequest>(value);

                Guid sessionId = Program.Core.Security.Login(loginRequest.Username, loginRequest.PasswordHash);

                var result = new LoginResponse
                {
                    Success = true,
                    SessionId = sessionId,
                    ProcessId = Program.Core.Sessions.SessionIdToProcessId(sessionId)
                };

                return result;
            }
            catch (Exception ex)
            {
                var result = new LoginResponse
                {
                    Success = false,
                    Message = "Login failed with an exception: " + ex.Message
                };
                return result;
            }
        }

        [HttpGet]
        //api/Security/{sessionId}/Logout
        public ActionResponse Logout(Guid sessionId)
        {
            ActionResponse result = new ActionResponse();

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
