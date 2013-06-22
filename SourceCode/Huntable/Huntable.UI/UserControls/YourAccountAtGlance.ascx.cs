using System;
using Huntable.Business;

namespace Huntable.UI.UserControls
{
    public partial class YourAccountAtGlance : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
                var userId = Business.Common.GetLoggedInUserId(Session);

                if (userId.HasValue)
                {

                    LoadData();
                }
                else
                {
                    acctsDiv.Visible = false;
                }
            
        }

        private void LoadData()
        {
            var userId = Business.Common.GetLoggedInUserId(Session);

            if(!userId.HasValue) return;

            var objInvManager = new InvitationManager();
            var currentUser = objInvManager.GetUserDetails(userId.Value);
            var level1Inv = currentUser.LevelOneInvitedCount.HasValue ? currentUser.LevelOneInvitedCount.Value : 0;
            var level2Inv = currentUser.LevelTwoInvitedCount.HasValue ? currentUser.LevelTwoInvitedCount.Value : 0;
            var level3Inv = currentUser.LevelThreeInvitedCount.HasValue ? currentUser.LevelThreeInvitedCount.Value : 0;
            var level1 = currentUser.LevelOnePremiumCount.HasValue ? currentUser.LevelOnePremiumCount.Value : 0;
            var level2 = currentUser.LevelTwoPremiumCount.HasValue ? currentUser.LevelTwoPremiumCount.Value : 0;
            var level3 = currentUser.LevelThreePremiumCount.HasValue ? currentUser.LevelThreePremiumCount.Value : 0;

            lblTotalInvitaions.Text = (level1Inv ).ToString();
            lblFriendsJoined.Text = level1.ToString();
            //lblLevelOne.Text = level1.ToString();
            lblLevelTwo.Text = level2.ToString();
            lblLevelThree.Text = level3.ToString();
            lblTotalEarnings.Text = (level1*4 + level2*1 + level3*.5).ToString() + "$";
        }
    }
}