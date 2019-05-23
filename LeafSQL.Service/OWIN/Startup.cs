using Owin;
using System.Web.Http;

namespace LeafSQL.Service.OWIN
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                 "GenericByNameActions",
                 "api/{controller}/{sessionId}/{schema}/{byName}/{action}"
            );

            config.Routes.MapHttpRoute(
                 "NamespaceActions",
                 "api/{controller}/{sessionId}/{schema}/{action}"
            );

            config.Routes.MapHttpRoute(
                 "RootActions",
                 "api/{controller}/{sessionId}/{action}"
            );
            config.Routes.MapHttpRoute(
                 "DirectActions",
                 "api/{controller}/{action}"
            );

            config.Formatters.Insert(0, new TextMediaTypeFormatter());

            appBuilder.UseWebApi(config);
        }
    }
}
