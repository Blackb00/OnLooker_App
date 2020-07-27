using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using SearchingNews.WebService.Config;

[assembly: OwinStartup(typeof(SearchingNews.WebApiHost.Startup))]

namespace SearchingNews.WebApiHost
{
    public class Startup
    {
        public static HttpConfiguration HttpConfiguration { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration = new HttpConfiguration();

            WebApiConfig.Register(HttpConfiguration);

            app.UseWebApi(HttpConfiguration);
        }
    }
}