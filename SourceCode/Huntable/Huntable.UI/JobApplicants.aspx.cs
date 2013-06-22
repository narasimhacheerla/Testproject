using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Entities.SearchCriteria;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class JobApplicants : System.Web.UI.Page
    {
        public delegate void DelPopulateSearchUsers(int pageIndex);

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoggingManager.Debug("Entering Page_Load - JobApplicants.aspx");
                var loggedInUserId = Common.GetLoggedInUserId(Session);
                var jobManager = new InvitationManager();
                if (loggedInUserId != null)
                {
                    var result = jobManager.GetUserDetails(loggedInUserId.Value);
                    if (!IsPostBack)
                    {
                        if (result.IsPremiumAccount == false)
                        {
                            postanoppurtuinity.HRef="WhatIsHuntableUpgrade.aspx";
                        }
                        else if (result.CreditsLeft == null)
                        {
                            postanoppurtuinity.HRef="BuyCredit.aspx";
                        }
                        else
                        {
                            postanoppurtuinity.HRef="PostJob.aspx";
                        }
                        if(result.IsCompany==true)
                        {
                            isyourprofile.HRef = "CompanyRegistration2.aspx";
                        }
                        else
                        {
                            isyourprofile.HRef = "editprofilepage.apsx";
                        }
                        int jobId;
                        if (!string.IsNullOrEmpty(Request.QueryString["JobId"]))
                        {
                            jobId =Convert.ToInt32(Request.QueryString["JobId"]);
                            var jobapplicants = new JobsManager().JobApplicants(jobId);
                            rpJobApplicants.DataSource = jobapplicants;
                            rpJobApplicants.DataBind();
                        }
                        using (var context = huntableEntities.GetEntitiesWithNoLock())
                        {


                            var usri = context.Users.FirstOrDefault(x => x.Id == loggedInUserId.Value);
                            if (usri != null && usri.IsPremiumAccount == null)
                            {
                                bimage.Visible = true;
                                pimage.Visible = false;
                            }
                            else
                            {
                                bimage.Visible = false;
                                pimage.Visible = true;
                            }
                        }
                    }

                }
                else
                {
                    bimage.Visible = true;
                    pimage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - JobApplicants.aspx");
        }

        protected void BtnMessageClick(object sender, EventArgs e)
        {
            try
            {
                LoggingManager.Debug("Entering BtnMessageClick - JobApplicants.aspx");
                if (Common.GetLoggedInUserId(Session) != null)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var button = sender as Button;
                        if (button != null)
                        {
                            var userMessage = new UserMessage
                            {
                                SentBy = Convert.ToInt32(Common.GetLoggedInUserId(Session)),
                                SentTo = Convert.ToInt32(button.CommandArgument),
                                Subject = txtSubject.Text,
                                Body = txtMessage.Text,
                                IsActive = true,
                                SentIsActive = true,
                                IsRead = false,
                                SentDate = DateTime.Now
                            };
                            var objMessageManager = new UserMessageManager();
                            lblMessage.Text = objMessageManager.SaveMessage(context, userMessage) == 1 ? "Message Sent Successfully." : "Message Sending Failed. Please try again.";
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Please Login to Send Message.";
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnMessageClick - JobApplicants.aspx");
        }

        protected string GetUrl(object userId)
        {
            LoggingManager.Debug("Entering GetUrl - JobApplicants.aspx");
            int? loggedInUserId = Common.GetLoggedInUserId();

            if (loggedInUserId.HasValue && (Int32)userId != loggedInUserId.Value)
            {
                return "window.open('AjaxChat/MessengerWindow.aspx?init=1&target=" + userId + "', '" + userId + "', 'width=650,height=400,resizable=1,menubar=0,status=0,toolbar=0'); return false";
            }
            LoggingManager.Debug("Exiting GetUrl - JobApplicants.aspx");
            return null;
        }
        public string UrlGenerator(object id)
        {
            LoggingManager.Debug("Eing UrlGenerator - JobApplicants.aspx"); if ((id != null))
            {
                int userid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().UserUrlGenerator(userid);
            }
            else
            {
                LoggingManager.Debug("Exiting UrlGenerator - JobApplicants.aspx");
                return null;
            }

        }
    }
}