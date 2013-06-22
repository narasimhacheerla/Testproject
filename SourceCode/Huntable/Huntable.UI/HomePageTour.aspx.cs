using System;
using System.Linq;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class HomePageTour : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - HomePageTour.aspx");
            bool userLoggedIn = Common.IsLoggedIn();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                if (userLoggedIn)
                {
                    var user = Common.GetLoggedInUser(context);

                    if (user.Homepagetour == null || user.Homepagetour == false)
                    {
                        var userupdate = context.Users.FirstOrDefault(x => x.Id == user.Id);
                        if (userupdate != null) userupdate.Homepagetour = true;
                        context.SaveChanges();
                    }
                }
            }
            LoggingManager.Debug("Exiting Page_Load - HomePageTour.aspx");
        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Entering UrlGenerator - HomePageTour.aspx");
            if ((id != null))
            {
                int jobid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().JobsUrlGenerator(jobid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - HomePageTour.aspx");
                return null;
            }


        }
    }
}