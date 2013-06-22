using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using LinkedIn;
using OAuthUtility;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class InvitationsReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - InvitationReport.aspx");
            try
            {
                if (!IsPostBack)
                {
                    LoadInvitations();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - InvitationReport.aspx");
            
        }


        private void LoadInvitations()
        {
            LoggingManager.Debug("Entering LoadInvitations - InvitationReport.aspx");
            try
            {
                var objInvManager = new InvitationManager();
                var userId = Common.GetLoggedInUserId(Session);
                if (userId != null) gvInvitations.DataSource = objInvManager.GetInvitationList(userId.Value);
                gvInvitations.DataBind();
                if (userId == null)
                {
                    ucpplYouMayKnow.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadInvitations - InvitationReport.aspx");
           

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering ddlStatus_SelectedIndexChanged - InvitationReport.aspx");

            var objInvManager = new InvitationManager();
            var userId = Common.GetLoggedInUserId(Session);
            if (ddlStatus.SelectedValue == "1")
            {
                if (userId != null) gvInvitations.DataSource = objInvManager.GetJoinedList(userId.Value);
                gvInvitations.DataBind();
            }
            if (ddlStatus.SelectedValue == "2")
            {
                if (userId != null) gvInvitations.DataSource = objInvManager.GetNotJoinedList(userId.Value);
                gvInvitations.DataBind();
                chkSelectAll.Visible = true;
            }
            if (ddlStatus.SelectedValue == "0")
            {
                if (userId != null) gvInvitations.DataSource = objInvManager.GetInvitationList(userId.Value);
                gvInvitations.DataBind();
            }
            LoggingManager.Debug("Exiting ddlStatus_SelectedIndexChanged - InvitationReport.aspx");
        }

        protected void GVInvitationsRowCommand(object sender, GridViewCommandEventArgs e)
        {
            LoggingManager.Debug("Entering gvInvitationsRowCommand - InvitationReport.aspx");
            try
            {
                switch (e.CommandName)
                {
                    case "invite":
                        {
                            var dataKey = gvInvitations.DataKeys[Convert.ToInt32(e.CommandArgument)];
                            if (dataKey != null)
                            {
                                if (dataKey.Values != null)
                                {
                                    var id = dataKey.Values["Id"].ToString();
                                    var invitationId = int.Parse(id);
                                    try
                                    {
                                        SendInvitation(invitationId);
                                    }
                                    catch(Exception ex)
                                    {
                                        new Snovaspace.Util.Utility().DisplayMessage(this, "Unable to send invitation. Please try later.");
                                    }
                                   
                                    //var type = dataKey.Values["InvitationTypeId"].ToString();
                                    //if(type=="1")
                                    //{
                                    //    var objInvManager = new InvitationManager();
                                    //    var baseUrl = GetApplicationBaseUrl();
                                    //    objInvManager.ResendInvitation(baseUrl, Convert.ToInt32(id),0);
                                    //    new Snovaspace.Util.Utility().DisplayMessage(this, "Invitation Sent");
                                    //}
                                    //else
                                    //{
                                    //    var popupScript = "<script language='javascript'>" + "window.open('oauth.aspx?currpage=[CURRPAGE]&invId=" + id + "', 'ThisPopUp', " + "'left = 300, top=150, width=400, height=300, " +
                                    //                      "menubar=no, scrollbars=no, resizable=no')" + "</script>";
                                    //    if(type=="2")
                                    //    {
                                    //        popupScript = popupScript.Replace("[CURRPAGE]", "facebook"); 
                                    //    }
                                    //    else if(type=="3")
                                    //    {
                                    //        popupScript = popupScript.Replace("[CURRPAGE]", "linkedin"); 
                                    //    }
                                    //    else if (type == "4")
                                    //    {
                                    //        popupScript = popupScript.Replace("[CURRPAGE]", "twitter"); 
                                    //    }

                                    //    Page.ClientScript.RegisterStartupScript(GetType(), "PopupScript", popupScript);
                                    //}
                                }
                            }
                        }
                        break;
                    case "deleterow":
                        {
                            var dataKey = gvInvitations.DataKeys[Convert.ToInt32(e.CommandArgument)];
                            if (dataKey != null)
                            {
                                if (dataKey.Values != null)
                                {
                                    var id = dataKey.Values["Id"].ToString();

                                    var objInvManager = new InvitationManager();
                                    objInvManager.DeleteInvitation(Convert.ToInt32(id));
                                }
                            }
                            new Snovaspace.Util.Utility().DisplayMessage(this, "Record Deleted");
                            LoadInvitations();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting gvInvitationsRowCommand - InvitationReport.aspx");
        }

        private void SendInvitation(int invitationId)
        {

            LoggingManager.Debug("Entering SendInvitation - InvitationReport.aspx"); 
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var invitation = context.Invitations.FirstOrDefault(i => i.Id == invitationId);
                if(invitation!=null)
                {
                    var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
                    if(invitation.InvitationTypeId==1)
                    {
                        var objInvManager = new InvitationManager();
                        objInvManager.ResendInvitation(baseUrl, invitationId, 0);
                        
                    }
                    else
                    {
                        var token = context.OAuthTokens.FirstOrDefault(o => o.Id == invitation.TokenId);
                        if(token!=null)
                        {
                            var url = baseUrl +(invitation.CustomInvitationId > 0 ? "CustomizedHomepage.aspx?ref=" : "Default.aspx?ref=") + invitationId;


                            switch (token.Provider)
                            {
                                case "facebook":
                                    {

                                        var message = "Hi " + invitation.Name + ", I am inviting you to join Huntable, the fastest growing Professional Resourcing Network.  Huntable lets you: super power your profile, receive customized feeds and jobs, follow your favorite person or company, connect and network, get headhunted, find your dream job and many more… It is FREE to join Huntable, and it only takes few clicks. There is nothing to loose. See you there! ";

                                        var funcall = "postToFeed('" + url + "','" + message + "','" + invitation.UniqueId + "');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "function", funcall, true);
                              
                                    }
                                    break;
                                case "linkedin":
                                    {
                                        var objEmailTemplate = new EmailTemplateManager();
                                        var template = objEmailTemplate.GetTemplate("LinkedInInvitation");
                                        var body = template.TemplateText;
                                        body = body.Replace("[NAME]", string.Format("{0}", invitation.Name));
                                        body = body.Replace("[LINK]", url);
                                        var tokenManager = new InMemoryTokenManager(LinkedInOAuthClient.ConsumerKey, LinkedInOAuthClient.ConsumerSecret);
                                        tokenManager.SetTokenSecret(token.Token, token.Secret);
                                        var authorization = new WebOAuthAuthorization(tokenManager, token.Token);
                                        var service = new LinkedInService(authorization);
                                        IEnumerable<string> ids = new[] { invitation.UniqueId };
                                        service.SendMessage(template.Subject, body, ids, true);
                                    }
                                    break;
                                case "twitter":
                                    {
                                        var userid = decimal.Parse(invitation.UniqueId);
                                        TwitterOAuthClient.SendInvitation(token.Token, token.Secret, userid, invitation.Name, url);
                                    }
                                    break;
                            }

                            invitation.InvitationSentDateTime = DateTime.Now;
                            context.SaveChanges();
                        }

                    }
                    if (invitation.InvitationTypeId != 2)
                        new Snovaspace.Util.Utility().DisplayMessage(this, "Invitation Sent");
                }

            }
            LoggingManager.Debug("Exiting SendInvitation - InvitationReport.aspx");
        }

        protected void BtnHiddenClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnHiddenClick - InvitationReport.aspx");

            new Snovaspace.Util.Utility().DisplayMessage(this, "Invitation Sent");

            LoggingManager.Debug("Exiting BtnHiddenClick - InvitationReport.aspx");
        }
    }
}