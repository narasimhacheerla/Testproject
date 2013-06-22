using System;
using Huntable.Data;
using Huntable.Business;
using System.Linq;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class MyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - MyAccount.aspx");
            var user = Common.GetLoggedInUser();
            if (!IsPostBack)
            {
                var loggedInUserId = Common.GetLoggedInUserId(Session);
                if (loggedInUserId.HasValue)
                {
                    Company Comp_id;
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var objMessageManager = new UserMessageManager();
                        User userDetails = objMessageManager.GetUserbyUserId(context, loggedInUserId.Value);
                        if(user.IsPremiumAccount==true)
                        {
                            upgrade.Visible = false;
                        }
                
                        lblProfileName.Text = userDetails.Name;
                        imgProfilePicture.ImageUrl = userDetails.UserProfilePictureDisplayUrl;
                        lblmember.Text = userDetails.CreatedDateTime.ToString();
                        lblEmail.Text = userDetails.EmailAddress;
                        var percentCompleted = UserManager.GetProfilePercentCompleted(loggedInUserId.Value);
                        lblPercentCompleted.Text = (percentCompleted / 100).ToString("00%");
                        LoggingManager.Info("Percent Completed:" + lblPercentCompleted.Text);
                        int value = Convert.ToInt32(percentCompleted);
                        ProgressBar2.Value = value;
                        Comp_id = context.Companies.FirstOrDefault(x => x.Userid == loggedInUserId);                       
                        if (user.IsCompany == true)
                        {
                            a_editprofile.HRef = "companyregistration2.aspx";
                            a_viewprofile.HRef = new UrlGenerator().CompanyUrlGenerator(Comp_id.Id);
                            editprofileupgrade.HRef = "companyregistration2.aspx";
                            ProfileHuntablediv.Visible = false;
                        }
                        else
                        {
                            a_editprofile.HRef = "EditProfilePage.aspx";
                            a_viewprofile.HRef = "ViewUserProfile.aspx";
                            editprofileupgrade.HRef = "EditProfilePage.aspx";
                        }
                      
                       profilesta.HRef = user.IsPremiumAccount == true ? "ProfileStatusAtGlance.aspx" : "Whatishuntableupgrade.aspx";
                        Buycredits.HRef = user.IsPremiumAccount == true
                                              ? "BuyCredit.aspx"
                                              : "Whatishuntableupgrade.aspx";
                    }
                }
            }


            if (user.IsCompany == true)
            {
                a_jobapplied.Visible = false;
            }

            
            LoggingManager.Debug("Exiting Page_Load - MyAccount.aspx");
        }
        protected void BtnPostOpportunityClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnPostOpportunityClick - MyAccount.aspx");

            var loggedInUserId = Common.GetLoggedInUserId(Session);
            var jobManager = new InvitationManager();
            var result = jobManager.GetUserDetails(loggedInUserId.Value);
            string credit = (result.CreditsLeft).ToString();

            if (result.IsPremiumAccount == false || result.IsPremiumAccount == null)
            {
                Server.Transfer("WhatIsHuntableUpgrade.aspx");
            }
            else if (result.CreditsLeft == null|| result.CreditsLeft == 0)
            {
                Server.Transfer("BuyCredit.aspx");
            }
            else
            {
                Server.Transfer("PostJob.aspx");
            }

            LoggingManager.Debug("Exiting BtnPostOpportunityClick - MyAccount.aspx");
        }
       
    }
}