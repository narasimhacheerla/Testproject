using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using System.Data;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI.UserControls
{
    public partial class SeeYourFriendsInvitations : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InvitationManager _objInvManager = new InvitationManager();
            int? userId = Business.Common.GetLoggedInUserId(Session);
            if (!userId.HasValue)
            {
                seeInvitations.Visible = false;
            }
            if (OtherUserId.HasValue)
            {
                var result = _objInvManager.GetUserFriends(OtherUserId.Value);
                rsplist.DataSource = result.Take(4);
                rsplist.DataBind();
            }
            else if(userId.HasValue)
            {
                var result = _objInvManager.GetUserFriends(userId.Value);
                rsplist.DataSource = result.Take(4);
                rsplist.DataBind();
            }
          
        }

     
        private int? OtherUserId
        {
            get
            {
                int otherUserId;
                if (int.TryParse(Request.QueryString["UserId"], out otherUserId))
                {
                    return otherUserId;
                }
                return null;
            }
        }
        public string UrlGenerator(object id)
        {
            if ((id != null))
            {
                int userid = Convert.ToInt32(id.ToString());
                return "~/"+ new UrlGenerator().UserUrlGenerator(userid);
            }
            else
            {
                return null;
            }

        }
    }
}