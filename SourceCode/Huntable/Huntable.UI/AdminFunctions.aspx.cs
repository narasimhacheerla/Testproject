using System;
using System.Linq;
using Huntable.Business.BatchJobs;
using Huntable.Data;
using Snovaspace.Util.Mail;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class AdminFunctions : System.Web.UI.Page
    {
        protected void BtnCustomizeFeedsBatchRunClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCustomizeFeedsBatchRunClick - AdminFunctions");

            new Snovaspace.Util.Utility().RunAsTask(() => new FeedsUserConnectionsUpdate().Run());

            LoggingManager.Debug("Exiting BtnCustomizeFeedsBatchRunClick - AdminFunctions");

        }

        protected void BtnCustomizeJobsBatchRunClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCustomizeJobsBatchRunClick - AdminFunctions");

            new Snovaspace.Util.Utility().RunAsTask(() => new JobsUserConnectionsUpdate().Run());

            LoggingManager.Debug("Exiting BtnCustomizeJobsBatchRunClick - AdminFunctions");
        }

        protected void BtnPeopleYouMayKnowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnPeopleYouMayKnowClick - AdminFunctions");

            new Snovaspace.Util.Utility().RunAsTask(() => new PeopleYouMayKnowUpdate().Run());

            LoggingManager.Debug("Exiting BtnPeopleYouMayKnowClick- AdminFunctions");
        }

        protected void BtnClearCacheClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnClearCacheClick - AdminFunctions");

            MasterDataManager.RefreshCache();

            LoggingManager.Debug("Exiting BtnClearCacheClick - AdminFunctions");


        }

        protected void BtnResendInvitations(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnResendInvitations - AdminFunctions");

            new Snovaspace.Util.Utility().RunAsTask(() => new ReSendInvitations().Run());

            LoggingManager.Debug("Exiting BtnResendInvitations - AdminFunctions");
        }

        protected void BtnSendClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSendClick - AdminFunctions");
            try
            {
                string subject = txtSubject.Text;
                string body = txtBody.Text;
                MailList(subject, body);
                Page.ClientScript.RegisterStartupScript(GetType(), "click", "alert('Message sent successfully to all subscribed users');", true);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnSendClick - AdminFunctions");
        }

        private void MailList(string sub, string msgBody)
        {
            LoggingManager.Debug("Entering MailList - AdminFunctions");

            using (huntableEntities context = huntableEntities.GetEntitiesWithNoLock())
            {
                var mails = context.Users.Select(u => u.EmailAddress).ToList();
                foreach (string email in mails) SnovaUtil.SendEmail(sub, msgBody, email);
            }
            LoggingManager.Debug("Exiting MailList - AdminFunctions");

        }

        protected void BtnFeaturedRecruiters(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnFeaturedRecruiters - AdminFunctions");

            new Snovaspace.Util.Utility().RunAsTask(() => new FeaturedRecruiters().Run());

            LoggingManager.Debug("Exiting BtnFeaturedRecruiters - AdminFunctions");

        }
        protected void BtnJobFeeds(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnJobFeeds - AdminFunctions");

            new Snovaspace.Util.Utility().RunAsTask(() => new JobFeeds().Run());
            jbfeeds.Text = "jobfeeds completed";
            LoggingManager.Debug("Exiting BtnJobFeeds - AdminFunctions");
            

        }
        protected void BtnRememberEmail(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnRememberEmail - AdminFunctions");

            new Snovaspace.Util.Utility().RunAsTask(() => new RememberEmail().Run());
            RmbrEmail.Text = "Remember emails sent";
            LoggingManager.Debug("Exiting BtnRememberEmails - AdminFunctions");


        }
        protected void BtnJobrememberEmail(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnRememberEmail - AdminFunctions");

            new Snovaspace.Util.Utility().RunAsTask(() => new JobRemember().Run());
            lbljobRemember.Text = "Job Remember emails sent";
            LoggingManager.Debug("Exiting BtnRememberEmails - AdminFunctions");


        }


        protected void btnEmailInvitesClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnEmailInvitesClick - AdminFunctions");

            new Snovaspace.Util.Utility().RunAsTask(() => new EmailInvites().Run());
            lblEmailInvites.Text = "Job Email Invite alerts sent";
            LoggingManager.Debug("Exiting btnEmailInvitesClick - AdminFunctions");


        }
        protected void btnJobsStatusClick(object sender, EventArgs e)
        {
            Response.Redirect("~/JobsStatus.aspx");
        }
        protected void btnsitemapClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnsitemapClick - AdminFunctions");

            new Snovaspace.Util.Utility().RunAsTask(() => new Sitemap().Run());
            jbfeeds.Text = "Sitemap created";
            LoggingManager.Debug("Exiting btnsitemapClick - AdminFunctions");
        }
    }
}
