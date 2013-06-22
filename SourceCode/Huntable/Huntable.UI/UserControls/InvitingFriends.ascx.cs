using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Snovaspace.Util.Logging;
namespace Huntable.UI.UserControls
{
    public partial class InvitingFriends : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             //int? userId = Business.Common.GetLoggedInUserId(Session);
             //if (!userId.HasValue)
             //{
             //    invitefriends.Visible = false;

             //}
          
             //int? userId = Business.Common.GetLoggedInUserId(Session);
             //if (!userId.HasValue)
             //{
             //    invitefriends.Visible = false;

             //}
            try
            {

                dlFriends.DataSource = new InvitationManager().GetYourFriendsInvitationsCount();
                dlFriends.DataBind();
            }
            catch (Exception ex) {
                LoggingManager.Error(ex);
            }
        
       
        }
        public string UrlGenerator(object id)
        {
            if ((id != null))
            {
                int jobid = Convert.ToInt32(id.ToString());
                return "~/"+ new UrlGenerator().UserUrlGenerator(jobid);
            }
            else
            {
                return null;
            }

        }
    }
}