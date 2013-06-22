using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Huntable.Data.Enums;
using OAuthUtility;
using Snovaspace.Util;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using Snovaspace.Util.SocialMedia;

namespace Huntable.UI
{
    public partial class Facebookfriends : System.Web.UI.Page
    {
        List<Contact> _lstFriends;
        List<Data.Invitation> _lstInvitations;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Enter Page_Load - Facebookfriends.aspx");

            if (!IsPostBack)
            {
                LoggingManager.Debug("Enter !IsPostBack - Facebookfriends.aspx");              
                  
                var user = Common.GetLoginUser(Session);
                Img1.Src = user.UserProfilePictureDisplayUrl;
                hfImageId.Value = user.PersonalLogoFileStoreId.ToString();

                GetFacebookFriendList();
            }

            LoggingManager.Debug("Exit Page_Load - Facebookfriends.aspx");
        }

        private void GetFacebookFriendList()
        {
            LoggingManager.Debug("Entering GetFacebookFriendList - Facebookfriends.aspx");
            var tokenid = 0;
            if (Request.QueryString["tokenid"] != null)
             {
                 int.TryParse(Request.QueryString["tokenid"], out tokenid);
             }
            using(var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var oauthtoken = context.OAuthTokens.FirstOrDefault(o => o.Provider=="facebook" && (o.Id==tokenid || tokenid==0));
                if(oauthtoken!=null)
                {
                    var accesstoken = oauthtoken.Token;
                    Session["FacebookAccessToken"] = accesstoken;
                    _lstFriends = FacebookOAuth2Client.GetFacebookContacts(accesstoken,oauthtoken.Id);
                    foreach (var frnd in _lstFriends)
                    {
                        frnd.TokenId = oauthtoken.Id;
                        frnd.Provider = oauthtoken.Provider;
                    }
                    Session["FacebookFriends"] = _lstFriends;
                    BindFriendList();
                }
            }

            LoggingManager.Debug("Exiting GetFacebookFriendList - Facebookfriends.aspx");
        }

        private void BindFriendList()
        {
            LoggingManager.Debug("Enter BindFriendList - Facebookfriends.aspx");

            var userId = Common.GetLoggedInUserId(Session);
            _lstFriends = (List<Contact>)Session["FacebookFriends"];

            var invmanager = new InvitationManager();
            if (userId != null) _lstInvitations = invmanager.GetInvitationList(userId.Value, InvitationType.Facebook);

            rptrFriends.DataSource = _lstFriends;
            rptrFriends.DataBind();

            LoggingManager.Debug("Exit BindFriendList - Facebookfriends.aspx");
        }

        protected void lbtnInvite_OnClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Enter lbtnInvite_OnClick - Facebookfriends.aspx");
            var user = Common.GetLoginUser(Session);
            var lnkBtn = (LinkButton)sender;
            ViewState["mode"] = lnkBtn.ToolTip;
            var friends = (List<Contact>)Session["FacebookFriends"];
            var uniqueid = lnkBtn.ToolTip;
            var friend = friends.FirstOrDefault(u => u.UniqueId == uniqueid);
            LoggingManager.Debug("Test for facebook url entering"+user.IsCompany);
           var message=  ((user.IsCompany!=null||user.IsCompany!=false))?Constants.CustomCompanyInvitationMessage:Constants.CustomInvitationMessage;
            if (friend != null) message = message.Replace("[first name]", friend.Name);
            txtMessage.Text = message;
            hfMessage.Value = message;

            mpeCustomize.Show();
            LoggingManager.Debug("Exit lbtnInvite_OnClick - Facebookfriends.aspx");
        }

        protected void rptrFriends_OnItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            LoggingManager.Debug("Enter rptrFriends_OnItemDataBound - Facebookfriends.aspx");

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

            LoggingManager.Debug("Exit rptrFriends_OnItemDataBound - Facebookfriends.aspx");
        }

        private void SendInvite(int invId, string uniqueId, string name, int custId)
        {
            LoggingManager.Debug("Enter SendInvite - Facebookfriends.aspx");

            try
            {
                var baseUrl = new Utility().GetApplicationBaseUrl();
                var user = Common.GetLoginUser(Session);
                var url = (user.IsCompany!=null) ? baseUrl + "Default.aspx?ref=" + invId + "&isComp=" + 1 : "Default.aspx?ref=" + invId;
                if (custId > 0)
                    url = ((user.IsCompany != null || user.IsCompany != false)) ? baseUrl + "CustomizedHomepage.aspx?ref=" + invId + "&isComp=" + 1 : "CustomizedHomepage.aspx?ref=" + invId;
                var fbuserid = long.Parse(uniqueId);
                var token = (string) Session["FacebookAccessToken"];
                FacebookOAuth2Client.SendInvitation(token, fbuserid, url, name);

                //var fbc = new FacebookClient();
                //fbc.SendInviteTest(token, url, fbuserid, name);

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting SendInvite - Facebookfriends.aspx");
        }


        //protected void lbtnInviteAll_Click(object sender, EventArgs e)
        //{

        //    LoggingManager.Debug("Entering lbtnInviteAll_Click - Facebookfriends.aspx");

        //    ViewState["mode"] = "all";
        //    var message = Constants.CustomInvitationMessage;
        //    message = message.Replace("[first name]", "friend");
        //    txtMessage.Text = message;
        //    hfMessage.Value = message;
        //    mpeCustomize.Show();
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Invitaion sent succesfully')", true);

        //    LoggingManager.Debug("Exiting lbtnInviteAll_Click - Facebookfriends.aspx");
        //}

        //protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    LoggingManager.Debug("Entering chkSelectAll_CheckedChanged - Facebookfriends.aspx");

        //    foreach (RepeaterItem item in rptrFriends.Items)
        //    {
        //        var chkInv = item.FindControl("chkSelect") as CheckBox;
        //        if (chkInv != null) chkInv.Checked = chkSelectAll.Checked;
        //    }

        //    LoggingManager.Debug("Exiting chkSelectAll_CheckedChanged - Facebookfriends.aspx");

        //}

        protected void lbNoCustom_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbNoCustom_Click - Facebookfriends.aspx");
            mpeCustomize.Hide();
            SendInvites(0);
            LoggingManager.Debug("Exiting lbNoCustom_Click - Facebookfriends.aspx");
        }

        protected void btnCustomInvite_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnCustomInvite_Click - Facebookfriends.aspx");
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
            LoggingManager.Debug("Exiting btnCustomInvite_Click - Facebookfriends.aspx");
        }

        private void SendInvites(int customId)
        {
            LoggingManager.Debug("Entering SendInvites - Facebookfriends.aspx");
            var mode = (string)ViewState["mode"];
            var userId = Common.GetLoggedInUserId(Session);
            var friends = (List<Contact>)Session["FacebookFriends"];
            var user = Common.GetLoginUser(Session);
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
                            var id = lnkBtn.ToolTip;
                            var contact = friends.FirstOrDefault(u => u.UniqueId == id);
                            if (contact != null)
                            {
                                var invId = invmanager.SaveInvitation(userId.Value, InvitationType.Facebook, contact.UniqueId, contact.Name, contact.ProfilePictureUrl, customId,contact.TokenId);
                                SendInvite(invId, contact.UniqueId, contact.Name,customId);
                            }
                        }
                    }
                }

                new Snovaspace.Util.Utility().DisplayMessageWithPostback(this, "Invitations sent successfully");

              
            }
            else
            {
                var friend = friends.FirstOrDefault(u => u.UniqueId == mode);
                var invmanager = new InvitationManager();
                if (friend != null)
                {
                    var invId = invmanager.SaveInvitation(userId.Value, InvitationType.Facebook, friend.UniqueId, friend.Name, friend.ProfilePictureUrl, customId,friend.TokenId);

                    //SendInvite(invId, mode, friend.Name, customId);
                    var baseUrl = new Utility().GetApplicationBaseUrl();
                 
                    var url = (user.IsCompany != null || user.IsCompany != false) ? baseUrl + "Default.aspx?ref=" + invId + "&isComp=" + 1 : "Default.aspx?ref=" + invId;
                    if (customId > 0)
                        url = ((user.IsCompany != null || user.IsCompany != false)) ? baseUrl + "CustomizedHomepage.aspx?ref=" + invId + "&isComp=" + 1 : "CustomizedHomepage.aspx?ref=" + invId;
                    if (user.IsCompany == true)
                    {
                        var message = "Hi " + friend.Name +
                                      ", I am inviting to follow my company in Huntable, the fastest growing Professional Resourcing Network.  Huntable lets you: super power your profile, receive customized feeds and jobs, follow your favorite person or company, connect and network, get headhunted, find your dream job and many more… It is FREE to join Huntable, and it only takes few clicks. There is nothing to loose. See you there! ";
                        var funcall = "postToFeed('" + url + "','" + message + "','" + mode + "');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "function", funcall, true);
                    }
                    else
                    {
                        var message = "Hi " + friend.Name +
                                      ", I am inviting you to join Huntable, the fastest growing Professional Resourcing Network.  Huntable lets you: super power your profile, receive customized feeds and jobs, follow your favorite person or company, connect and network, get headhunted, find your dream job and many more… It is FREE to join Huntable, and it only takes few clicks. There is nothing to loose. See you there! ";
                        var funcall = "postToFeed('" + url + "','" + message + "','" + mode + "');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "function", funcall, true);
                    }
                    mpeCustomize.Hide();
                }

                //new Utility().DisplayMessageWithPostback(this, "Invitation sent successfully");
            }
            LoggingManager.Debug("Exiting SendInvites - Facebookfriends.aspx");


            //BindFriendList();
        }

        protected void lbtnChangeImage_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbtnChangeImage_Click - Facebookfriends.aspx");

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
            LoggingManager.Debug("Exiting lbtnChangeImage_Click - Facebookfriends.aspx");
        }
    }
}