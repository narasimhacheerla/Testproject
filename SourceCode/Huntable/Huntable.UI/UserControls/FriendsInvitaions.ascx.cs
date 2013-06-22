using System;
using Huntable.Business;
using System.Web.UI.WebControls;

namespace Huntable.UI.UserControls
{
    public partial class FriendsInvitaions : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var userId = Business.Common.GetLoggedInUserId(Session);

            if (!userId.HasValue) return;

            var objInvManager = new InvitationManager();
            var userList = objInvManager.GetFriendsInvitaions(userId.Value);
            rptrFriends.DataSource = userList;
            rptrFriends.DataBind();

        }

        protected void lbtnDetails_OnClick(object sender, EventArgs e)
        {            
               
                var lnkBtn = (LinkButton)sender;
                var id = long.Parse(lnkBtn.ToolTip);              

        }
    }
}