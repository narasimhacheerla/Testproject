using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using Huntable.Business.BatchJobs;
using Huntable.Data;
using Huntable.Business;
using Snovaspace.Util;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - Default");

            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName.ToString(CultureInfo.InvariantCulture)];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                if (authTicket != null && !authTicket.Expired && authTicket.Name== "dontshowthisagain")
                {
                    hideshow1.Visible = false;
                }
            }

            txtPassword.Attributes.Add("value", "Password");
            btnJoin.Attributes.Add("onclick", "return Validate()");
            
            if (Common.IsLoggedIn())
            {
                Response.Redirect(PageNames.HomePageAfterLoggingIn);
            }
           
            if (Request.QueryString["ref"] != null)
            {
                Session["ref"] = Request.QueryString["ref"];
            }
            if (Request.QueryString["recref"] != null)
            {
                Session["refrec"] = Request.QueryString["recref"];
            }
            if (Request.QueryString["isComp"] != null)
            {
                Session["isComp"] = Request.QueryString["isComp"];
            }
            LoggingManager.Debug("Exiting Page_Load - Default");
        }      
        protected void BtnJoinClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnJoinClick - Default");

            try
            {
                lblError.Text = string.Empty;
                var recreffid = Session["refrec"];
                Session["refrec"] = string.Empty;
                var iscomp = Session["isComp"];
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    // Email already registered.
                    if (context.Logins.Any(u => u.EmailAddress.ToLower() == txtEmail.Text.ToLower()))
                    {
                        lblError.Visible = true;
                        lblError.Text = "Email already registered with us";
                    }
                    else
                    {
                        if (recreffid != null &&(string) recreffid != string.Empty)
                        {
                            int refidinv = Convert.ToInt32(recreffid);
                            var usrdetails = context.Users.FirstOrDefault(x => x.InvitationId ==refidinv);
                            var inv = new InvitationManager();
                            var chtuser = new ChatUser
                                              {
                                                  cu_username = txtFirstName.Text,
                                                  cu_displayname = txtFirstName.Text + " " + txtLastName.Text,
                                                  cu_begintime = DateTime.Now,
                                                  cu_lastactive = DateTime.Now
                                              };
                            if (usrdetails != null)
                            {
                                usrdetails.FirstName = txtFirstName.Text;
                                usrdetails.LastName = txtLastName.Text;
                                usrdetails.Password = txtPassword.Text;
                                usrdetails.LastLoginTime = DateTime.Now;
                                usrdetails.LastProfileUpdatedOn = DateTime.Now;
                                usrdetails.CreatedDateTime = DateTime.Now;
                                usrdetails.ChatUser = chtuser;
                                usrdetails.IsDeleted = false;
                                if (usrdetails.InvitationId != null)
                                {
                                    var id = (int)usrdetails.InvitationId;
                                    inv.UpdateInvitation(id);
                                 if(iscomp!=null)new Utility().RunAsTask(() => new FollowCompany().Run(usrdetails.Id,usrdetails.InvitationId));
                                }
                                context.SaveChanges();
                                var preferredFeedUserUserDerived = new PreferredFeedUserUserDerived
                                {
                                    UserId = usrdetails.Id,
                                    FollowingUserId = usrdetails.Id,
                                    CreatedDateTime = DateTime.Now
                                };
                                var logins = new Login
                                {
                                    UserId = usrdetails.Id,
                                    EmailAddress = txtEmail.Text,
                                    Password = txtPassword.Text,
                                    IsUser = true
                                };
                                context.Logins.AddObject(logins);
                                context.SaveChanges();
                                context.PreferredFeedUserUserDeriveds.AddObject(preferredFeedUserUserDerived);

                                context.SaveChanges();

                                if (usrdetails.InvitationId != null)
                                {
                                    inv.UpdateUserIdInInvitaion((int)usrdetails.InvitationId, usrdetails.Id);
                                    FeedManager.addFeedNotification(FeedManager.FeedType.Got_Connected, usrdetails.Id, usrdetails.ReferralId, null);
                                }
                                var applicationBaseUrl = Common.GetApplicationBaseUrl();
                                new UserManager().SendVerificationEMail(usrdetails, applicationBaseUrl);
                                Session[SessionNames.UserEmailAddress] = usrdetails.EmailAddress;
                                Response.Redirect("~/EmailConfirmationSentSuccess.aspx", false);
                            }

                        }
                        else
                        {
                            var invMngr = new InvitationManager();
                            Invitation invitaion = null;
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


                            if (!string.IsNullOrEmpty(Session["ref"] as string))
                            {
                                var id = Session["ref"] as string;
                                Session["ref"] = string.Empty;
                                int invId;
                                int.TryParse(id, out invId);
                                if (invId > 0)
                                {

                                    invitaion = invMngr.UpdateInvitation(invId);
                                    if (invitaion != null)
                                    {
                                        user.ReferralId = invitaion.UserId;
                                        FeedManager.addFeedNotification(FeedManager.FeedType.Got_Connected, user.Id, user.ReferralId, null);
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
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
                lblError.Text = "Error occurred while processing the request.";
            }
            LoggingManager.Debug("Exiting BtnJoinClick - Default");
        }
        protected void FollowCompany()
        {
            
        }
        protected void BtnSearchClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSearchClick - Default");

            if (!string.IsNullOrWhiteSpace(txtSearchPeople.Value.Trim()) && (txtSearchPeople.Value.Trim() != "e.g: Name, Company, Skill, Job title"))
            {
                Response.Redirect(string.Format("~/UserSearch.aspx?keyword={0}", txtSearchPeople.Value), false);
                txtSearchPeople.Value = string.Empty;
            }
         
            else if (!string.IsNullOrWhiteSpace(txtSearchCompany.Value.Trim()) &&
                     (txtSearchCompany.Value.Trim() != "e.g: Companyname, Location, Industry"))
            {
                string url = string.Format("~/companiessearch.aspx?keyword={0}", txtSearchCompany.Value);
                new Utility().RedirectUrl(Response, url);
            }
            else
            {
                var keyword=string.Empty;
                if (!string.IsNullOrWhiteSpace(txtSearchJobs.Value.Trim()) &&
                    (txtSearchJobs.Value.Trim() != "e.g: Job title, Skill, Keyword, Location"))
                {
                    keyword = txtSearchJobs.Value;
                }
                  Response.Redirect(string.Format("~/JobSearch.aspx?keyword={0}",keyword), false);
                txtSearchJobs.Value = string.Empty;
            }
            //(!string.IsNullOrWhiteSpace(txtSearchJobs.Value.Trim()) && (txtSearchJobs.Value.Trim() != "e.g: Job title, Skill, Keyword, Location"))
            //{
              
            //}
            LoggingManager.Debug("Exiting BtnSearchClick - Default");
        }
        protected void lnkTerms_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkTerms_Click - Default");

            Server.Transfer("Terms.aspx");

            LoggingManager.Debug("Exiting lnkTerms_Click - Default");
        }

        protected void lnkPrivacy_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkPrivacy_Click - Default");

            Server.Transfer("PrivacyPolicy.aspx");

            LoggingManager.Debug("Exiting lnkPrivacy_Click - Default");
        }
        protected void donot_click(object sender, EventArgs e)
        {
            const string username = "dontshowthisagain";
            const string password = "password";

            const int timeout = 120;
            var ticket = new FormsAuthenticationTicket(username,false ,timeout);
            var customTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate,
                                                             ticket.Expiration, ticket.IsPersistent, password);
            string encrypted = FormsAuthentication.Encrypt(customTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
            {
                Expires = DateTime.Now.AddMinutes(timeout)
            };


            HttpContext.Current.Response.Cookies.Add(cookie);
            hideshow1.Visible = false;
        }
    }
}