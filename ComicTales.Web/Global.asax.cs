using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ComicTales;
using ComicTales.SignalR;
using Microsoft.AspNet.SignalR;

namespace ComicTales
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Register the default hubs route: ~/signalr
            var hubConfiguration = new HubConfiguration { EnableDetailedErrors = true }; //for debug
            RouteTable.Routes.MapHubs(hubConfiguration);
            GlobalHost.HubPipeline.AddModule(new SignalRLoggingPipelineModule());

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Bootstrapper.Initialise();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }
    }
}