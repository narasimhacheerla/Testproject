using System;
using System.Web;
using Snovaspace.Util.Logging;

namespace Snovaspace.Util
{
   public class IpAddressBlocking: IHttpModule
    {
       public void Dispose()
        {
           
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += ApplicationBeginRequest;
        }

       public void ApplicationBeginRequest(object source,EventArgs e)
        {
            //LoggingManager.Debug("Entering into Blocking section");
            //HttpContext context = ((HttpApplication) source).Context;

           //string strIpAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
           //                     context.Request.ServerVariables["REMOTE_ADDR"];
           //if (strIpAddress != null)
           //{
           //    context.Response.StatusCode = 403;
           //}
           //LoggingManager.Debug("Exiting  Blocking section");


        }
    }
}
