using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Estream.AjaxChat.Classes;
using Huntable.Business;
using Huntable.Data;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using OAuthUtility;
using Snovaspace.Util;
using Snovaspace.Util.Mail;
using log4net;
using System.Text;
using Snovaspace.Util.Logging;
using User = Huntable.Data.User;

namespace Huntable.UI
{
    public class Global : HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly object CompositionLock = new object();
        public static IUnityContainer Container;

        //public string GetCurrentlyLoggedInUsername()
        //{
        //    LoggingManager.Debug("Entering GetCurrentlyLoggedInUsername - Global.asax");

        //    LoggingManager.Debug("Exiting GetCurrentlyLoggedInUsername - Global.asax");

        //    return HttpContext.Current.Session[SessionNames.LoggedInUserId] == null ? null : ((Int32)(HttpContext.Current.Session[SessionNames.LoggedInUserId])).ToString();
        //}

        //public bool IsRoomAdmin(string username, int chatRoomId)
        //{
        //    LoggingManager.Debug("Entering IsRoomAdmin -  Global.asax");
        //    User user = Common.GetUserById(username);
        //    if (user != null) return user.IsAdmin.HasValue && user.IsAdmin.Value;
        //    LoggingManager.Debug("Exiting IsRoomAdmin - Global.asax");
        //    return false;
        //}

        //public bool HasChatAccess(string username, int chatRoomId)
        //{
        //    LoggingManager.Debug("Entering HasChatAccess - Global.asax");
        //    LoggingManager.Debug("Enxiting HasChatAccess - Global.asax");

        //    return true;
        //}

        //public string GetUserDisplayName(string username)
        //{

        //    LoggingManager.Debug("Entering GetUserDisplayName - Global.asax");
            
        //    User user = Common.GetUserById(username);
        //    if (user != null) return user.Name;
        //    LoggingManager.Debug("Exiting GetUserDisplayName - Global.asax");
        //    return null;
        //}

        //public bool UserExists(string username)
        //{
        //    LoggingManager.Debug("Entering UserExists - Global.asax");
        //    using (var context = huntableEntities.GetEntitiesWithNoLock())
        //    {
        //        LoggingManager.Debug("Exiting UserExists - Global.asax");
        //        return context.Users.Any(ur => ur.Id.ToString() == username);
        //    }
            
        //}

        //public string GetLoginUrl()
        //{
        //    LoggingManager.Debug("Entering GetLoginUrl -Global.asax");
        //    LoggingManager.Debug("Exiting GetLoginUrl - Global.asax");
        //    return "~/Login.aspx";
        //}

        //public string GetUserPhoto(string username, int width, int height)
        //{
        //    LoggingManager.Debug("Entering GetUserPhoto - Global.asax");
        //    User user = Common.GetUserById(username);
        //    if (user != null) return user.UserProfilePictureDisplayUrl;
        //    LoggingManager.Debug("Exiting GetUserPhoto - Global.asax");
        //    return null;
        //}

        void Application_Start(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Application_Start - Global.asax");
            // Code that runs on application startup

            Container = new UnityContainer();

            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");

            section.Configure(Container);

            OAuthWebSecurity.ClearProviders();
            OAuthWebSecurity.RegisterGoogleClient("550207822052.apps.googleusercontent.com", "UtZ_aBwuBHGWcXN-umhGALnm");
            OAuthWebSecurity.RegisterFacebookClient("307487099357078", "d24675d8248f10d3f8a52e4d8fe29062");
            OAuthWebSecurity.RegisterTwitterClient("xjrx5FD3sXx7CcmyB7wnOQ", "sD9Ym2AswDWdd7Hs1ZtoPpVEBYTaqpvBYOdNmO7dA");
            OAuthWebSecurity.RegisterLinkedInClient("dcwbtwvuewmw", "S3uIVhNxXd58f3QK");
            OAuthWebSecurity.RegisterMicrosoftClient("000000004408E558", "od8NVdanEIWqmlKu9hOepBE3AfUu4jCw");
            OAuthWebSecurity.RegisterYahooClient("dj0yJmk9ajJ2T0ZzcFhQV09PJmQ9WVdrOVEwWTFTVFoyTkdFbWNHbzlNVGN6T0RVM01qQTJNZy0tJnM9Y29uc3VtZXJzZWNyZXQmeD1lMQ--", "5046ab49ed7735a2b4c4efe338afafe670c3a304");

            log4net.Config.XmlConfigurator.Configure();
            Logger.InfoFormat("Application started at: {0}", DateTime.Now);
            RegisterRoutes(RouteTable.Routes);
            LoggingManager.Debug("Exiting Application_Start - Global.asax");
        }

        void Application_End(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Application_End - Global.asax");
            Logger.InfoFormat("Application ended at: {0}", DateTime.Now);
            Container.Dispose();
            LoggingManager.Debug("Exiting Application_End - Global.asax");
        }

        void Application_Error(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Application_Error - Global.asax");
            new Utility().RunAsTask(
                () =>
                {
                    var lastError = Server.GetLastError();
                    if (lastError != null)
                    {
                        // Code that runs when an unhandled error occurs
                        Exception ex = lastError.GetBaseException();
                        if (HttpContext.Current != null)
                        {
                            StringBuilder body = new StringBuilder()
                                .Append(string.Format("An unhandled exception was thrown: {0}\n{1}\n{2}\n{3}",
                                                      HttpContext.Current.Request.Url,
                                                      ex.Message,
                                                      ex.Source, ex.StackTrace));
                            Logger.Error(body);
                            if (ex.Message != "File does not exist.")
                            {
                                //SnovaUtil.SendEmail("Claims: Error", body.ToString(), new[] {"huntable@snovaspace.com"});
                            }
                        }
                    }
                }
                );
            LoggingManager.Debug("Exiting Application_Error -Global.asax");
        }

        void Session_End(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Session_End - Global.asax");
            Session.Clear();
            Session.Abandon();
            LoggingManager.Debug("Exiting Session_End - Global.asax");
        }

        public static void RegisterRoutes(RouteCollection routes)
        {

            RouteTable.Routes.Ignore("{*alljs}", new { alljs = @".*\.js(/.*)?" });
            routes.MapPageRoute("",
               "Jobs/{ID}",
               "~/jobview.aspx");
            routes.MapPageRoute("",
              "companies/{ID}",
              "~/companyview.aspx");
            routes.MapPageRoute("",
             "users/{ID}",
             "~/VisualCv.aspx");
            routes.MapPageRoute("",
          "users/text/{ID}",
          "~/ViewUserProfile.aspx");
            routes.MapPageRoute("",
          "users/activity/{ID}",
          "~/VisualCvActivity.aspx");
           
        }
    }
}
