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

                var session = Program.Core.Security.Login(action.Username, action.PasswordHash);

                result.SessionId = session.SessionId;
                result.LoginId = session.LoginId;
                result.ProcessId = session.ProcessId;
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

                result.List = new List<Login>();

                lock (Program.Core.Security.Catalog.Collection)
                {
                    foreach (var login in Program.Core.Security.Catalog.Collection)
                    {
                        result.List.Add(login.ToPayload());
                    }
                }

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
