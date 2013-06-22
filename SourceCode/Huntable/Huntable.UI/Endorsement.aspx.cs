using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EO.Web.Internal;
using Huntable.Business;
using Huntable.Data;
using Huntable.Data.Enums;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;

namespace Huntable.UI
{
    public partial class Endorsement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - Endorsement.aspx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var loggedInUserId = Common.GetLoggedInUserId(Session);
                var companynames = from a in context.EmploymentHistories
                                   join b in context.MasterCompanies on a.CompanyId equals b.Id
                                   where a.UserId == loggedInUserId.Value && a.IsDeleted == null
                                   select new
                                       {
                                           a.Id,
                                           b.Description
                                       };
                var user = context.Users.FirstOrDefault(x => x.Id == loggedInUserId.Value);

                ddl.DataSource = companynames;
                ddl.DataTextField = "Description";
                ddl.DataValueField = "Id";
                ddl.DataBind();
                if (!IsPostBack)
                {
                    if (user != null)
                        EndTextbox.Text = @"Hi,

I am asking for a brief recommendation about my work, which I can add it in my Huntable profile. Your recommendation will help me to build my profile and also, this will help you grow your Network. 

Thanks for helping me.
"+user.FirstName;
                }
                
            }
            LoggingManager.Debug("Exiting Page_Load - Endorsement.aspx");
            
        }

        protected void Sendendorsementmessage(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - endendorsementmessage.aspx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            { var loggedInUserId = Common.GetLoggedInUserId(Session);
                var emailaddress = emailtextbox.Text;
                var emailtemplate = EmailTemplateManager.GetTemplate(EmailTemplates.EndorsementRequest);
                string[] emailaddresses = emailaddress.Split(',');
                var companyname = ddl.SelectedItem;
                var enodrsementBody = EndTextbox.Text.Replace("\n", "<br/>");
                var user = context.Users.FirstOrDefault(x => x.Id == loggedInUserId.Value);
                foreach (string emailadd in emailaddresses)
                {
                    if (user != null && emailadd != "Enter the email addresses (sepreated by comma)")
                    {
                        var valuesList = new Hashtable
                            {
                                {"Name", user.Name},
                                {"Requested User Name", user.Name},
                                {"Requested User Role", user.CurrentPosition},
                                {"Requested User Location", user.LocationToDisplay},
                                {"Requested User Company", companyname},
                                {"Requested User Description", user.Summary},
                                {"Endorsement Message",enodrsementBody}

                            };

                        string baseUrl = Common.GetApplicationBaseUrl();
                        valuesList.Add("Server Url", baseUrl);
                        string userProfilePicturePath = Path.Combine(baseUrl, user.UserProfilePictureDisplayUrl.Replace("~/",
                                                                                                                        string
                                                                                                                            .Empty));
                        valuesList.Add("Requested User Picture", userProfilePicturePath);
                        //string userProfilePath = Path.Combine(baseUrl, "ViewUserProfile.aspx?UserId=" + user.Id);
                        var url = new UrlGenerator().UserUrlGenerator(user.Id);
                        string userProfilePath = Path.Combine(baseUrl, url);
                        valuesList.Add("Requested User Profile", userProfilePath);
                        valuesList.Add("Dont Want To Receive Email", Path.Combine(baseUrl, "UserEmailNotification.aspx"));
                        string body = SnovaUtil.LoadTemplate(emailtemplate.TemplateText, valuesList);
                        SnovaUtil.SendEmail("Can you endorse me", body, emailadd);
                        Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Endorsement Request Sent')", true);

                    }
                }

           
            }
            LoggingManager.Debug("Exiting Sendendorsementmessage - endendorsementmessage.aspx");
        }
         
    }
}