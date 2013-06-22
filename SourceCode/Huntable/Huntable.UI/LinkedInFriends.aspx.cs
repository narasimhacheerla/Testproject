using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Huntable.Data.Enums;
using LinkedIn;
using LinkedIn.ServiceEntities;
using OAuthUtility;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using System.ComponentModel;

namespace Huntable.UI
{
    public partial class LinkedInFriends : System.Web.UI.Page
    {
        List<Data.Invitation> _lstInvitations;
        private List<Contact> _lstConnections;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - LinkedInFriends.aspx");
            if (!IsPostBack)
            {
                var user = Common.GetLoginUser(Session);
                Img1.Src = user.UserProfilePictureDisplayUrl;
                hfImageId.Value = user.PersonalLogoFileStoreId.ToString();

                GetLinkedInFriendList();
            }
            LoggingManager.Debug("Exiting Page_Load - LinkedInFriends.aspx");
        }

        private void GetLinkedInFriendList()
        {
            LoggingManager.Debug("Entering GetLinkedInFriendList - LinkedInFriends.aspx");
            var tokenid = 0;
            if (Request.QueryString["tokenid"] != null)
            {
                int.TryParse(Request.QueryString["tokenid"], out tokenid);
            }
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var oauthtoken = context.OAuthTokens.FirstOrDefault(o =>  o.Provider=="linkedin" && (o.Id == tokenid || tokenid == 0));
                if (oauthtoken != null)
                {
                    var accesstoken = oauthtoken.Token;
                    Session["LinkedInAccessToken"] = accesstoken;
                    Session["LinkedInTokenSecret"] = oauthtoken.Secret;
                    var tokenManager = new InMemoryTokenManager(LinkedInOAuthClient.ConsumerKey, LinkedInOAuthClient.ConsumerSecret);
                    tokenManager.SetTokenSecret(accesstoken,oauthtoken.Secret);
                    var authorization = new WebOAuthAuthorization(tokenManager, accesstoken);
                    var service = new LinkedInService(authorization);
                    var connections = service.GetConnectionsForCurrentUser();
                    IEnumerable<Contact> contacts = new BindingList<Contact>();
                    var i = 1;
                    contacts = from c in connections.Items
                                      select new Contact
                                                 {
                                                     Id = i++,
                                                     UniqueId = c.Id,
                                                     Name = c.Name,
                                                     ProfilePictureUrl = c.PictureUrl,
                                                     ProfileUrl = c.PublicProfileUrl
                                                 };
                    _lstConnections = contacts.ToList();
                    Session["LinkedInConnections"] = _lstConnections;
                    LoadContacts();
                }
            }
            LoggingManager.Debug("Exiting GetLinkedInFriendList - LinkedInFriends.aspx");
        }

        private void LoadContacts()
        {
            LoggingManager.Debug("Entering LoadContacts - LinkedInFriends.aspx");

            var userId = Common.GetLoggedInUserId(Session);
            var invmanager = new InvitationManager();
            if (userId != null) _lstInvitations = invmanager.GetInvitationList(userId.Value, InvitationType.Linkedin);
            _lstConnections = (List<Contact>)Session["LinkedInConnections"];

            foreach (var item in _lstConnections.Where(item => item.ProfilePictureUrl == null))
            {
                item.ProfilePictureUrl = "http://static02.linkedin.com/scds/common/u/img/icon/icon_no_photo_80x80.png";
            }
            rptrFriends.DataSource = _lstConnections.ToList();
            rptrFriends.DataBind();
            LoggingManager.Debug("Exiting LoadContacts - LinkedInFriends.aspx");
        }

        protected void lbtnInvite_OnClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbtnInvite_OnClick - LinkedInFriends.aspx");
            var lnkBtn = (LinkButton)sender;
            ViewState["mode"] = lnkBtn.ToolTip;
            var user = Common.GetLoginUser(Session);
            _lstConnections = (List<Contact>)Session["LinkedInConnections"];
            var connection = _lstConnections.FirstOrDefault(u => u.UniqueId == lnkBtn.ToolTip);
            var message = ((user.IsCompany != null || user.IsCompany != false)) ? Constants.CustomCompanyInvitationMessage : Constants.CustomInvitationMessage;
            message = message.Replace("[first name]", connection.Name);
            txtMessage.Text = message;
            hfMessage.Value = message;
            mpeCustomize.Show();
            LoggingManager.Debug("Exiting lbtnInvite_OnClick - LinkedInFriends.aspx");
        }

        protected void rptrFriends_OnItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            LoggingManager.Debug("Entering rptrFriends_OnItemDataBound - LinkedInFriends.aspx");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var lbtnInvite = ((LinkButton)e.Item.FindControl("lbtnInvite"));

                var uid = lbtnInvite.ToolTip;

                if (_lstInvitations.Any(i => i.UniqueId == uid))
                {
                    lbtnInvite.Text = "Re-Invite";
                    if (_lstInvitations.Any(i => i.UniqueId == uid && i.IsJoined))
                    {
                        lbtnInvite.Text = "Joined";
                        lbtnInvite.Enabled = false;
                    }
                }

            }
            LoggingManager.Debug("Exiting rptrFriends_OnItemDataBound - LinkedInFriends.aspx");
        }

      

        private void SendInvite(int invId, string id, string name, int custId)
        {
            LoggingManager.Debug("Entering SendInvite -  LinkedinFriends.aspx");
            var user = Common.GetLoginUser(Session);
            try
            {
                var baseUrl = Common.GetApplicationBaseUrl();
                var url = (user.IsCompany != null) ? baseUrl + "Default.aspx?ref=" + invId + "&isComp=" + 1 : baseUrl + "Default.aspx?ref=" + invId;
                if (custId > 0)
                    url = ((user.IsCompany != null || user.IsCompany != false)) ? baseUrl + "CustomizedHomepage.aspx?ref=" + invId + "&isComp=" + 1 : baseUrl + "CustomizedHomepage.aspx?ref=" + invId;
                var objEmailTemplate = new EmailTemplateManager();
                var template = objEmailTemplate.GetTemplate("LinkedInInvitation");
                var body = template.TemplateText;
                body = body.Replace("[NAME]", string.Format("{0}", name));
                body = body.Replace("[LINK]", url);
                var tokenManager = new InMemoryTokenManager(LinkedInOAuthClient.ConsumerKey, LinkedInOAuthClient.ConsumerSecret);
                
                var accesstoken = (string)Session["LinkedInAccessToken"];
                var tokensecret = (string)Session["LinkedInTokenSecret"];
                tokenManager.SetTokenSecret(accesstoken, tokensecret);
                var authorization = new WebOAuthAuthorization(tokenManager, accesstoken);
                var service = new LinkedInService(authorization);
                IEnumerable<string> ids = new[] { id };
                service.SendMessage(template.Subject, body, ids, true);
            }
            catch(Exception exception)
            {
                LoggingManager.Error(exception);
            }

            LoggingManager.Debug("Exiting SendInvite - LinkedinFriends.aspx");
        }


        protected void lbtnInviteAll_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbtnInviteAll_Click - LinkedInFriends.aspx");

            ViewState["mode"] = "all";
            var message = Constants.CustomInvitationMessage;
            txtMessage.Text = message;
            hfMessage.Value = message;
            mpeCustomize.Show();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Invitaion sent succesfully')", true);
            LoggingManager.Debug("Exiting lbtnInviteAll_Click - LinkedInFriends.aspx");
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkSelectAll_CheckedChanged - LinkedInFriends.aspx");

            foreach (RepeaterItem item in rptrFriends.Items)
            {
                var chkInv = item.FindControl("chkSelect") as CheckBox;
                if (chkInv != null) chkInv.Checked = chkSelectAll.Checked;
            }
            LoggingManager.Debug("Exiting chkSelectAll_CheckedChanged - LinkedInFriends.aspx");
        }


        protected void lbNoCustom_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbNoCustom_Click - LinkedInFriends.aspx");
            mpeCustomize.Hide();
            SendInvites(0);
            LoggingManager.Debug("Exiting lbNoCustom_Click - LinkedInFriends.aspx");
        }

        protected void btnCustomInvite_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnCustomInvite_Click - LinkedInFriends.aspx");
            var imageId = 0;
            int.TryParse(hfImageId.Value, out imageId);

            if (imageId > 0)
            {
                mpeCustomize.Hide();

                var customId = 0;
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var customInv = new CustomInvitationDetail();
                    context.CustomInvitationDetails.AddObject(customInv);
                    customInv.Message = txtMessage.Text;
                    customInv.PhotoFileStoreId = imageId;

                    context.SaveChanges();
                    customId = customInv.Id;
                }
                SendInvites(customId);
            }
            else
            {
                new Snovaspace.Util.Utility().DisplayMessage(this, "please select an image");
                mpeCustomize.Show();
            }
            LoggingManager.Debug("Exiting btnCustomInvite_Click - LinkedInFriends.aspx");

        }

        private void SendInvites(int customId)
        {
            LoggingManager.Debug("Entering SendInvites - LinkedInFriends.aspx");
            var mode = (string)ViewState["mode"];
            var userId = Common.GetLoggedInUserId(Session);
            _lstConnections = (List<Contact>)Session["LinkedInConnections"];
            if (mode == "all")
            {
                
                var invmanager = new InvitationManager();

                foreach (RepeaterItem item in rptrFriends.Items)
                {
                    var chkInv = item.FindControl("chkSelect") as CheckBox;
                    if (chkInv != null && chkInv.Checked)
                    {
                        var lnkBtn = item.FindControl("lbtnInvite") as LinkButton;
                        var id = lnkBtn.ToolTip;
                        var connection = _lstConnections.FirstOrDefault(u => u.UniqueId == id);
                        var invId = invmanager.SaveInvitation(userId.Value, InvitationType.Linkedin, connection.UniqueId, connection.Name, connection.ProfilePictureUrl, customId,connection.TokenId);
                        SendInvite(invId, connection.UniqueId, connection.Name,customId);
                    }
                }

                new Snovaspace.Util.Utility().DisplayMessageWithPostback(this, "Invitations sent successfully");
               

            }
            else
            {
                var connection = _lstConnections.FirstOrDefault(u => u.UniqueId == mode);
                var invmanager = new InvitationManager();
                if (connection != null)
                {
                    var invId = invmanager.SaveInvitation(userId.Value, InvitationType.Linkedin, connection.UniqueId, connection.Name, connection.ProfilePictureUrl, customId,connection.TokenId);

                    SendInvite(invId, mode, connection.Name,customId);
                }
                new Snovaspace.Util.Utility().DisplayMessageWithPostback(this, "Invitation sent successfully");

            }

            LoadContacts();
            LoggingManager.Debug("Exiting SendInvites - LinkedInFriends.aspx");
        }

        protected void lbtnChangeImage_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbtnChangeImage_Click - LinkedInFriends.aspx");
            if (fuPhoto.HasFile)
            {
                var id = new FileStoreService().LoadFileFromFileUpload(Constants.CustomInvitationImages, fuPhoto);
                Img1.Src = new FileStoreService().GetDownloadUrl(id);
                hfImageId.Value = id.ToString();

            }
            else
            {
                new Snovaspace.Util.Utility().DisplayMessage(this, "please select an image");

            }
            mpeCustomize.Show();
            LoggingManager.Debug("Exiting lbtnChangeImage_Click - LinkedInFriends.aspx");
        }
    }
}