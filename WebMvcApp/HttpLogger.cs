using System;
using System.Web;

namespace WebMvcApp
{
    public class HttpLogger : IHttpModule
    {
        private HttpApplication _application;        
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            _application = context;            
            _application.BeginRequest += new EventHandler(Context_BeginRequest);
            _application.EndRequest += new EventHandler(Context_EndRequest);
        }
        void Context_BeginRequest(object sender, EventArgs e)
        {
            var Request = _application.Request;
            LoggerSingleton.Info("{method} {url} {headers}", Request.RequestType, Request.RawUrl, Request.Headers);
        }
        void Context_EndRequest(object sender, EventArgs e)
        {
            var Request = _application.Request;
            LoggerSingleton.Info("{method} {url} {headers}", Request.RequestType, Request.RawUrl, Request.Headers);
        }
    }
}