using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WebFormsApp
{
    public class Global : HttpApplication
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
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            this.BeginRequest += this.Context_BeginRequest;
            this.EndRequest += this.Context_EndRequest;
        }        
    }
}