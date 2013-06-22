using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Huntable.Data.Enums;
using OAuthUtility;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;



namespace Huntable.UI
{
    public partial class TwitterFriends : System.Web.UI.Page
    {
        private List<Contact> _contacts;
        List<Invitation> _lstInvitations;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - TwitterFriends.aspx");
            if (!IsPostBack)
            {
                LoggingManager.Debug("Not postback Page_Load of TwitterFriends");               
                  
                var user = Common.GetLoginUser(Session);
                Img1.Src = user.UserProfilePictureDisplayUrl;
                hfImageId.Value = user.PersonalLogoFileStoreId.ToString();
             
                GetFriendList();
                BindContacts();
            }
            LoggingManager.Debug("Exiting Page_Load - TwitterFriends.aspx");
        }

        private void GetFriendList()
        {
            LoggingManager.Debug("Entering GetFriendList - TwitterFriends.aspx");

            var tokenid = 0;
            if (Request.QueryString["tokenid"] != null)
            {
                int.TryParse(Request.QueryString["tokenid"], out tokenid);
            }
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var oauthtoken = context.OAuthTokens.FirstOrDefault(o => o.Provider=="twitter" &&( o.Id == tokenid || tokenid == 0));
                if (oauthtoken != null)
                {
                    var accesstoken = oauthtoken.Token;
                    Session["TwitterAccessToken"] = accesstoken;
                    Session["TwitterTokenSecret"] = oauthtoken.Secret;
                    _contacts = TwitterOAuthClient.GetTwitterContacts(accesstoken, oauthtoken.Secret, oauthtoken.Id);

                    Session["TwitterFriends"] = _contacts;
                    BindContacts();
                }
            }

            LoggingManager.Debug("Exiting GetFriendList - TwitterFriends.aspx");
        }

        private void BindContacts()
        {
            LoggingManager.Debug("Entering BindContacts - TwitterFriends.aspx");

            var userId = Common.GetLoggedInUserId(Session);
            var invmanager = new InvitationManager();
            if (userId != null) _lstInvitations = invmanager.GetInvitationList(userId.Value, InvitationType.Twitter);
            _contacts = (List<Contact>)Session["TwitterFriends"];
            rptrFriends.DataSource = _contacts;
            rptrFriends.DataBind();

            LoggingManager.Debug("Exiting BindContacts - TwitterFriends.aspx");
        }

        protected void lbtnInvite_OnClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbtnInvite_OnClick - TwitterFriends.aspx");
            var lnkBtn = (LinkButton)sender;
            ViewState["mode"] = lnkBtn.ToolTip;
            _contacts = (List<Contact>)Session["TwitterFriends"];
            var id = lnkBtn.ToolTip;
            var connection = _contacts.FirstOrDefault(u => u.UniqueId == id);
            var user = Common.GetLoginUser(Session);
            var message = ((user.IsCompany != null || user.IsCompany != false)) ? Constants.CustomCompanyInvitationMessage : Constants.CustomInvitationMessage;
            message = message.Replace("[first name]", connection.Name);
            txtMessage.Text = message;
            hfMessage.Value = message;

            mpeCustomize.Show();
            LoggingManager.Debug("Exiting lbtnInvite_OnClick - TwitterFriends.aspx");
        }

        protected void rptrFriends_OnItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            LoggingManager.Info("Entering rptrFriends_OnItemDataBound  - TwitterFriends.aspx");

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

            LoggingManager.Debug("Exiting rptrFriends_OnItemDataBound  - TwitterFriends.aspx");
        }

        private void SendInvite(int invId, string id, string name,int custId)
        {
            LoggingManager.Debug("Entering SendInvite  - TwitterFriends.aspx");
            var user = Common.GetLoginUser(Session);
            try
            {
                var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
                var url = (user.IsCompany != null) ? baseUrl + "Default.aspx?ref=" + invId + "&isComp=" + 1 : "Default.aspx?ref=" + invId;
                if (custId > 0)
                    url = ((user.IsCompany != null || user.IsCompany != false)) ? baseUrl + "CustomizedHomepage.aspx?ref=" + invId + "&isComp=" + 1 : "CustomizedHomepage.aspx?ref=" + invId;
               var uid = decimal.Parse(id);
               var token=(string) Session["TwitterAccessToken"];
               var secret = (string)Session["TwitterTokenSecret"];
               TwitterOAuthClient.SendInvitation(token,secret,uid,name,url);
            }
            catch (Exception exception)
            {
                LoggingManager.Error(exception);
            }

            LoggingManager.Debug("Exiting SendInvite  - TwitterFriends.aspx");
        }

        protected void lbtnInviteAll_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbtnInviteAll_Click  - TwitterFriends.aspx");
            var user = Common.GetLoginUser(Session);
            ViewState["mode"] = "all";
            var message = Constants.CustomInvitationMessage;
            message = message.Replace("[first name]", "friend");
            txtMessage.Text = message;
            hfMessage.Value = message;
            mpeCustomize.Show();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Invitaion sent succesfully')", true);
            LoggingManager.Debug("Exiting lbtnInviteAll_Click  - TwitterFriends.aspx");
            
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkSelectAll_CheckedChanged  - TwitterFriends.aspx");

            foreach (RepeaterItem item in rptrFriends.Items)
            {
                var chkInv = item.FindControl("chkSelect") as CheckBox;
                if (chkInv != null) chkInv.Checked = chkSelectAll.Checked;
            }
            LoggingManager.Debug("Exiting chkSelectAll_CheckedChanged  - TwitterFriends.aspx");
        }


        protected void lbNoCustom_Click(object sender, EventArgs e)

        {
            LoggingManager.Debug("Entering lbNoCustom_Click  - TwitterFriends.aspx");
            mpeCustomize.Hide();
            SendInvites(0);
            LoggingManager.Debug("Exiting lbNoCustom_Click  - TwitterFriends.aspx");
        }

        protected void btnCustomInvite_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnCustomInvite_Click  - TwitterFriends.aspx");
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
            LoggingManager.Debug("Exiting btnCustomInvite_Click  - TwitterFriends.aspx");
        }

        private void SendInvites(int customId)
        {
            LoggingManager.Debug("Entering SendInvites  - TwitterFriends.aspx");
            var mode = (string)ViewState["mode"];
            var userId = Common.GetLoggedInUserId(Session);
            _contacts = (List<Contact>)Session["TwitterFriends"];
            if (mode == "all")
            {
               
                var invmanager = new InvitationManager();
                foreach (RepeaterItem item in rptrFriends.Items)
                {
                    var chkInv = item.FindControl("chkSelect") as CheckBox;
                    if (chkInv != null && chkInv.Checked)
                    {
                        var lnkBtn = item.FindControl("lbtnInvite") as LinkButton;
                        if (lnkBtn != null)
                        {
                            var id =(lnkBtn.ToolTip);
                            var contact = _contacts.FirstOrDefault(u => u.UniqueId == id);

                            var invId = invmanager.SaveInvitation(userId.Value, InvitationType.Twitter, contact.UniqueId, contact.Name, contact.ProfilePictureUrl, customId,contact.TokenId);
                            SendInvite(invId, contact.UniqueId, contact.Name,customId);
                        }
                    }
                }

                new Snovaspace.Util.Utility().DisplayMessageWithPostback(this, "Invitations sent successfully");
               
            }
            else
            {
                var connection = _contacts.FirstOrDefault(u => u.UniqueId == mode);
                var invmanager = new InvitationManager();
                if (connection != null)
                {
                    var invId = invmanager.SaveInvitation(userId.Value, InvitationType.Twitter, connection.UniqueId, connection.Name, connection.ProfilePictureUrl, customId,connection.TokenId);
                    SendInvite(invId,mode, connection.Name,customId);
                }
                new Snovaspace.Util.Utility().DisplayMessageWithPostback(this, "Invitation sent successfully");
             
               
            }

            BindContacts();
            LoggingManager.Debug("Exiting SendInvites  - TwitterFriends.aspx");
        }

        protected void lbtnChangeImage_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbtnChangeImage_Click  - TwitterFriends.aspx");
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
            LoggingManager.Debug("Exiting lbtnChangeImage_Click  - TwitterFriends.aspx");
        }
    }
}