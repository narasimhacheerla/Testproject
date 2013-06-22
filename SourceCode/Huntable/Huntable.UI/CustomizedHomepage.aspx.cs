using System;
using System.Globalization;
using System.Linq;
using Huntable.Business.BatchJobs;
using Huntable.Data;
using Huntable.Business;
using Snovaspace.Util;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using System.Web.UI;
using System.Web.UI.HtmlControls;


namespace Huntable.UI
{
    public partial class CustomizedHomepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CustomizedHomepage");
            txtPassword.Attributes.Add("value", "Password");
            btnJoin.Attributes.Add("onclick", "return Validate()");
            HtmlGenericControl ftr;
            ftr = (HtmlGenericControl)Master.FindControl("footersection");
            ftr.Visible = false;
            if (Common.IsLoggedIn())
            {
                Response.Redirect(PageNames.HomePageAfterLoggingIn);
            }
            if (Request.QueryString["isComp"] != null)
            {
                Session["isComp"] = Request.QueryString["isComp"];
            }
            if (Request.QueryString["ref"] != null)
            {
                Session["ref"] = Request.QueryString["ref"];
                int invId;
              
                int.TryParse(Session["ref"].ToString(), out invId);
                var objInvManager = new InvitationManager();
                 Invitation inv;
                 using (var context = huntableEntities.GetEntitiesWithNoLock())
                 {
                     inv = context.Invitations.FirstOrDefault(i => i.Id == invId);
                     var usr = context.Users.FirstOrDefault(i => i.Id == inv.UserId);
                     lblname.Text = usr.FirstName+" "+usr.LastName;
                     if(inv != null && inv.CustomInvitationId.HasValue)
                     {
                         if (usr != null)
                         {
                             var userresult = new UserManager();
                             var resulkt = userresult.GetUserfollower((int)usr.Id);
                             var resultfollowing = userresult.GetUserFollowings((int)usr.Id);



                             string s = resulkt != null ? lblfollowers.Text = resulkt.Count.ToString(CultureInfo.InvariantCulture) : lblfollowers.Text = "0";
                             string k = resultfollowing != null
                                            ? lblfollowing.Text = resultfollowing.Count.ToString(CultureInfo.InvariantCulture)
                                            : lblfollowing.Text = "0";
                             var currentUser = objInvManager.GetUserDetails(usr.Id);
                             var level1 = currentUser.LevelOnePremiumCount.HasValue ? currentUser.LevelOnePremiumCount : 0;
                             var level2 = currentUser.LevelTwoPremiumCount.HasValue ? currentUser.LevelTwoPremiumCount : 0;
                             var level3 = currentUser.LevelThreePremiumCount.HasValue ? currentUser.LevelThreePremiumCount : 0;
                             lblMessage.Text = inv.CustomInvitationDetail.Message.Replace("\n", "<br/>");
                             Img1.Src = new FileStoreService().GetDownloadUrl(inv.CustomInvitationDetail.PhotoFileStoreId);
                             lblInvitee.Text = inv.Name;
                             lblInviter.Text = usr.FirstName;
                             lblInviter2.Text = usr.FirstName;
                             lblInviteename.Text = usr.FirstName;

                             lblProfileViews.Text = context.UserProfileVisitedHistories.Count(p => p.UserId == usr.Id).ToString();
                             lblAffilateEarnings.Text = (level1 * 4 + level2 * 1 + level3 * .5).ToString();
                             lbljobs.Text = context.Jobs.Count().ToString();
                             var result = userresult.Companyfollower(usr.Id);
                             lblcompaniesfollowing.Text = result.ToString();
                         }
                     }
                 }
            }
            LoggingManager.Debug("Exiting Page_Load - CustomizedHomepage");
        }

        protected void BtnJoinClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnJoinClick - Default.aspx");

            try
            {
                lblError.Text = string.Empty;
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var iscomp = Session["isComp"];
                    // Email already registered.
                    if (context.Users.Any(u => u.EmailAddress.ToLower() == txtEmail.Text.ToLower()))
                    {
                        lblError.Visible = true;
                        lblError.Text = "Email already registered with us";
                    }
                    else
                    {


                        var chatUser = new ChatUser
                        {
                            cu_username = txtFirstName.Text,
                            cu_displayname = txtFirstName.Text + " " + txtLastName.Text,
                            cu_begintime = DateTime.Now,
                            cu_lastactive = DateTime.Now
                        };


                        var user = new User
                        {

                            EmailAddress = txtEmail.Text,
                            FirstName = txtFirstName.Text,
                            LastName = txtLastName.Text,
                            Password = txtPassword.Text,
                            LastLoginTime = DateTime.Now,
                            LastProfileUpdatedOn = DateTime.Now,
                            CreatedDateTime = DateTime.Now,
                            ChatUser = chatUser
                        };

                        var invMngr = new InvitationManager();
                        Invitation invitaion = null;
                        if (!string.IsNullOrEmpty(Session["ref"] as string))
                        {
                            var id = Session["ref"] as string;
                            int invId;
                            int.TryParse(id, out invId);
                            if (invId > 0)
                            {

                                invitaion = invMngr.UpdateInvitation(invId);
                                if (invitaion != null)
                                {
                                    user.ReferralId = invitaion.UserId;
                                    if (iscomp != null) new Utility().RunAsTask(() => new FollowCompany().Run(user.Id, invitaion.UserId));
                                }
                            }
                        }

                        context.ChatUsers.AddObject(chatUser);
                        context.Users.AddObject(user);
                        context.SaveChanges();

                        var preferredFeedUserUserDerived = new PreferredFeedUserUserDerived
                        {
                            UserId = user.Id,
                            FollowingUserId = user.Id,
                            CreatedDateTime = DateTime.Now
                        };
                        var logins = new Login
                        {
                            UserId = user.Id,
                            EmailAddress = txtEmail.Text,
                            Password = txtPassword.Text,
                            IsUser = true
                        };
                        context.Logins.AddObject(logins);
                        context.SaveChanges();
                        context.PreferredFeedUserUserDeriveds.AddObject(preferredFeedUserUserDerived);

                        context.SaveChanges();
                        FeedManager.addFeedNotification(Huntable.Business.FeedManager.FeedType.Got_Connected, user.Id, user.ReferralId, null);
                        if (invitaion != null)
                        {
                            invMngr.UpdateUserIdInInvitaion(invitaion.Id, user.Id);
                        }
                        var applicationBaseUrl = Common.GetApplicationBaseUrl();
                        new UserManager().SendVerificationEMail(user, applicationBaseUrl);
                        Session[SessionNames.UserEmailAddress] = user.EmailAddress;
                        Response.Redirect("~/EmailConfirmationSentSuccess.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
                lblError.Text = "Error occurred while processing the request.";
            }
            LoggingManager.Debug("Exiting BtnJoinClick -  CustomizedHomepage");
        }

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSearchClick -  CustomizedHomepage");

            if (!string.IsNullOrWhiteSpace(txtSearchPeople.Value.Trim()) && (txtSearchPeople.Value.Trim() != "e.g: Name, Company, Skill, Job title"))
            {
                Response.Redirect(string.Format("~/UserSearch.aspx?keyword={0}", txtSearchPeople.Value), false);
            }
            else if (!string.IsNullOrWhiteSpace(txtSearchCompany.Value.Trim()) &&
                (txtSearchCompany.Value.Trim() != "e.g: Companyname, Location, Industry"))
            {
                string url = string.Format("~/companiessearch.aspx?keyword={0}", txtSearchCompany.Value);
                new Utility().RedirectUrl(Response, url);
            }
            else if (!string.IsNullOrWhiteSpace(txtSearchJobs.Value.Trim()) && (txtSearchJobs.Value.Trim() != "e.g: Job title, Skill, Keyword, Location"))
            {
                Response.Redirect(string.Format("~/JobSearch.aspx?keyword={0}", txtSearchJobs.Value), false);
            }
            else
            {
                var keyword = string.Empty;
                if (!string.IsNullOrWhiteSpace(txtSearchJobs.Value.Trim()) &&
                    (txtSearchJobs.Value.Trim() != "e.g: Job title, Skill, Keyword, Location"))
                {
                    keyword = txtSearchJobs.Value;
                }
                Response.Redirect(string.Format("~/JobSearch.aspx?keyword={0}", keyword), false);
                txtSearchJobs.Value = string.Empty;
            }
            LoggingManager.Debug("Exiting BtnSearchClick -  CustomizedHomepage");
        }
    }
}