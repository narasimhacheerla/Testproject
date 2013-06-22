using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Snovaspace.Util.Logging;

namespace Snovaspace.Util
{
   public class ExecutionTimer : IHttpModule
    {
        public void Dispose()
        {
            
        }
        private int _startTime;
        public void Init(HttpApplication context)
        {
            //context.BeginRequest += ApplicationBeginRequest;
            context.EndRequest += new EventHandler(context_EndRequest);
            context.PostRequestHandlerExecute += new EventHandler(context_PostRequestHandlerExecute);
            _startTime = Environment.TickCount;
        }

        void context_PostRequestHandlerExecute(object sender, EventArgs e)
        {
            //LoggingManager.Debug("Entering into ExecutionTimer section");
            //int endTime = Environment.TickCount;
            //double executionTime = (double)(endTime - _startTime) / 1000.0;
            //LoggingManager.Debug("Exiting into ExecutionTimer section");
            ////return executionTime.ToString();
            //HttpContext.Current.Session["ExecutionTime"] = executionTime;
        }

        void context_EndRequest(object sender, EventArgs e)
        {
          
            //HttpContext.Current.s
        }

      //  public void ApplicationBeginRequest (object source,EventArgs e)
      //  {
            
      //  }
      //void OnEndRequest(object sender, System.EventArgs e)
      //{
          
      //}
      
       
    }
}
