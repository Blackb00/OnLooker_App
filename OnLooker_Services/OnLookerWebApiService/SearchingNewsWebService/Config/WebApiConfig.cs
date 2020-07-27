using System.Web.Http;
using System.Xml;

namespace SearchingNews.WebService.Config
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.XmlFormatter.UseXmlSerializer= true;
           // config.Formatters.XmlFormatter.SerializerSettings.ContractResolver = ;
           // config.Formatters.XmlFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
            );

        }
    }
}
