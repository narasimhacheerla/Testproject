using System;
using Huntable.Business;
using Snovaspace.Util.Logging;

namespace Huntable.UI.UserControls
{
    public partial class FriendsToInvite : System.Web.UI.UserControl
    {
        private const int StartRecordCount = 5;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoggingManager.Debug("Entering Page_Load - FriendsToInvite.ascx");
                if (!IsPostBack)
                {
                    int? userId = Common.GetLoggedInUserId(Session);
                    if (userId.HasValue)
                    {
                        BindFriendsToInvite(GetLoggedInUserId());
                    }
                    else
                    {
                        friendsInvite.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting Page_Load - FriendsToInvite.ascx");
        }

        private void BindFriendsToInvite(int userId)
        {
            int recordCount = StartRecordCount;
            if (!String.IsNullOrWhiteSpace(hfCurrentRecordCount.Value))
            {
                recordCount = Convert.ToInt32(hfCurrentRecordCount.Value) + StartRecordCount;
            }
            var friendsToInvite = new UserManager().FriendsToInvite(userId, recordCount);
            rpFriendsToInvite.DataSource = friendsToInvite;
            rpFriendsToInvite.DataBind();
            hfCurrentRecordCount.Value = recordCount.ToString();
        }

        protected void BtnSeeMoreClick(object sender, EventArgs e)
        {
            if (GetLoggedInUserId() > 0)
            {
                BindFriendsToInvite(GetLoggedInUserId());
            }
        }

        protected void RpFriendsToInviteItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            var friendsToInviteId = (System.Web.UI.WebControls.HiddenField)e.Item.FindControl("hfFriendsToInviteId");

            switch (e.CommandName)
            {
                case "Invite":
                    new UserManager().UpdateInvitedStatus(Convert.ToInt32(friendsToInviteId.Value), GetLoggedInUserId());
                    BindFriendsToInvite(GetLoggedInUserId());
                    Response.Write(
                            "<script language='javascript'>alert('Invitation Sent Successfully');</script>");
                    break;

                case "Close":
                    new UserManager().UpdateCancelledStatus(Convert.ToInt32(friendsToInviteId.Value), GetLoggedInUserId());
                    BindFriendsToInvite(GetLoggedInUserId());
                    break;
            }
        }

        private int GetLoggedInUserId()
        {
            return Convert.ToInt32(Common.GetLoggedInUserId(Session));
        }
    }
}