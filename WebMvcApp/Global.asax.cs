using Lib;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using static System.Net.Mime.MediaTypeNames;

namespace WebMvcApp
{
    public class MvcApplication : System.Web.HttpApplication
    {        
        void Context_BeginRequest(object sender, EventArgs e)
        {
            var Request = this.Request;
            LoggerSingleton.Info("{method} {url} {headers}", Request.RequestType, Request.RawUrl, Request.Headers);
        }
        void Context_EndRequest(object sender, EventArgs e)
        {
            var Request = this.Request;
            LoggerSingleton.Info("{method} {url} {headers}", Request.RequestType, Request.RawUrl, Request.Headers);
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);            
            //this.BeginRequest += this.Context_BeginRequest;
            //this.EndRequest += this.Context_EndRequest;
        }         
    }
}
