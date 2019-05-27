using LeafSQL.Library;
using LeafSQL.Library.Payloads.Actions.Base;
using LeafSQL.Library.Payloads.Responses;
using System;
using System.Diagnostics;
using System.Reflection;
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
                Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
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

        // GET api/Server/Version
        [HttpPost]
        public ActionResponseServerVersion Version([FromBody]ActionGeneric action)
        {
            var result = new ActionResponseServerVersion();

            try
            {
                var session = Program.Core.Sessions.GetSession(action.SessionId);
                Thread.CurrentThread.Name = $"API:{session.InstanceKey}:{Utility.GetCurrentMethod()}";
                Program.Core.Log.Trace(Thread.CurrentThread.Name);

                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

                result.Version = new Library.Payloads.Models.ServerVersion()
                {
                    Name = fileVersionInfo.ProductName,
                    Description = fileVersionInfo.FileDescription,
                    Version = fileVersionInfo.ProductVersion
                };

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
