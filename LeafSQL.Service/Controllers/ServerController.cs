using LeafSQL.Library;
using LeafSQL.Library.Payloads;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Http;

namespace LeafSQL.Service.Controllers
{
    public class ServerController : ApiController
    {
        // GET api/Server/Settings
        [HttpGet]
        public ServerSettingsResponse Settings(Guid sessionId)
        {
            ServerSettingsResponse result = new ServerSettingsResponse();

            try
            {
                Thread.CurrentThread.Name = string.Format("API:{0}", Utility.GetCurrentMethod());
                Program.Core.Log.Trace(Thread.CurrentThread.Name);

                result.Settings = Program.Core.Settings;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Login failed with an exception: " + ex.Message;
            }

            return result;
        }
    }
}
