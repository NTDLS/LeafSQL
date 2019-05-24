using LeafSQL.Library;
using LeafSQL.Library.Payloads;
using LeafSQL.Library.Payloads.Actions;
using LeafSQL.Library.Payloads.Models;
using LeafSQL.Library.Payloads.Responses;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Http;

namespace LeafSQL.Service.Controllers
{
    public class SecurityController : ApiController
    {
        //api/Security/Login
        [HttpPost]
        public ActionResponceLogin Login([FromBody]ActionRequestLogin action)
        {
            var result = new ActionResponceLogin();

            try
            {
                Thread.CurrentThread.Name = string.Format("API:{0}", Utility.GetCurrentMethod());
                Program.Core.Log.Trace(Thread.CurrentThread.Name);

                result.SessionId = Program.Core.Security.Login(action.Username, action.PasswordHash);
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

        //api/Security/{sessionId}/Logout
        [HttpPost]
        public IActionResponse Logout([FromBody]ActionRequestExecuteNonQuery action)
        {
            IActionResponse result = new IActionResponse();

            try
            {
                Thread.CurrentThread.Name = string.Format("API:{0}", Utility.GetCurrentMethod());
                Program.Core.Log.Trace(Thread.CurrentThread.Name);

                Program.Core.Sessions.LogoutSession(action.SessionId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        //api/Security/{sessionId}/ListLogins
        [HttpPost]
        public ActionResponceLogins ListLogins([FromBody]ActionRequestExecuteNonQuery action)
        {
            var result = new ActionResponceLogins();

            try
            {
                Thread.CurrentThread.Name = string.Format("API:{0}", Utility.GetCurrentMethod());
                Program.Core.Log.Trace(Thread.CurrentThread.Name);

                result.List = new List<Login>(); //TODO: Implment users/roles.

                result.List.Add(new Login() { Id = Guid.NewGuid(), Username = "admin" });
                result.List.Add(new Login() { Id = Guid.NewGuid(), Username = "Dummy1" });
                result.List.Add(new Login() { Id = Guid.NewGuid(), Username = "Dummy2" });
                result.List.Add(new Login() { Id = Guid.NewGuid(), Username = "Dummy3" });

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
