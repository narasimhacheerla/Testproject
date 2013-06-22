using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Huntable.Business;
using OAuthUtility;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using Huntable.Data;

namespace Huntable.UI
{
    public partial class InviteFriends : Page
    {

        private string callbackuri;
        private bool _isUserLoggedIn;
        protected void BtnInviteByEmailAddressesClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnInviteByEmailAddressesClick - InviteFriends.aspx");
            try
            {
                var message = Constants.CustomInvitationMessage;
                message = message.Replace("[first name]", "friend");
                txtMessage.Text = message;
                mpeCustomize.Show();
                hfMessage.Value = message;
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting BtnInviteByEmailAddressesClick - InviteFriends.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - InviteFriends.aspx");
            callbackuri = FullyQualifiedApplicationPath + "oauth.aspx";
            var user = Common.GetLoginUser(Session);
            if (!IsPostBack)
            {
                
                if (user != null)
                {
                    Img1.Src = user.UserProfilePictureDisplayUrl;
                    hfImageId.Value = user.PersonalLogoFileStoreId.ToString();
                    hdnUserId.Value = user.Id.ToString();
                }
                
            }
            if (user == null)
            {
                _isUserLoggedIn = false;
                pplYoumayKnow.Visible = false;
                getStartedHere.HRef = "~/default.aspx";
            }
            else
            {
                _isUserLoggedIn = true;
                getStartedHere.HRef = "~/HomePageAfterLoggingIn.aspx";
            }
            
            LoggingManager.Debug("Exiting Page_Load - InviteFriends.aspx");
        }

        public string FullyQualifiedApplicationPath
        {
            get
            {
                //Return variable declaration
                var appPath = string.Empty;

                //Getting the current context of HTTP request
                var context = HttpContext.Current;

                //Checking the current context content
                if (context != null)
                {
                    //Formatting the fully qualified website url/name
                    appPath = string.Format("{0}://{1}{2}{3}",
                                            context.Request.Url.Scheme,
                                            context.Request.Url.Host,
                                            context.Request.Url.Port == 80
                                                ? string.Empty
                                                : ":" + context.Request.Url.Port,
                                            context.Request.ApplicationPath);
                }

                if (!appPath.EndsWith("/"))
                    appPath += "/";

                return appPath;
            }
        }

        protected void IbtnGoogleClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnGoogleClick - InviteFriends.aspx");
            try
            {
                if(_isUserLoggedIn)
                {
                    OAuthWebSecurity.RequestAuthentication("Google", callbackuri);
                }
                else
                {
                    new Snovaspace.Util.Utility().DisplayMessage(this, "Login to invite your friends.");
                }
               
               // Response.Redirect("oauth.aspx?currpage=gmail",false);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting IbtnGoogleClick - InviteFriends.aspx");
        }

        protected void IbtnYahooClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnYahooClick - InviteFriends.aspx");
            try
            {
                if (_isUserLoggedIn)
                {
                    OAuthWebSecurity.RequestAuthentication("Yahoo", callbackuri);
                }
                else
                {
                    new Snovaspace.Util.Utility().DisplayMessage(this, "Login to invite your friends.");
                }
                // Response.Redirect("oauth.aspx?currpage=yahoo");
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting IbtnYahooClick - InviteFriends.aspx");
        }

        protected void IbtnLiveClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnLiveClick - InviteFriends.aspx");
            if (_isUserLoggedIn)
            {
                Response.Redirect("oauth.aspx?currpage=live", false);
            }
            else
            {
                new Snovaspace.Util.Utility().DisplayMessage(this, "Login to invite your friends.");
            }
            LoggingManager.Debug("Exiting IbtnLiveClick - InviteFriends.aspx");
        }

        protected void IbtnFacebookClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnFacebookClick - InviteFriends.aspx");
            if (_isUserLoggedIn)
            {
                OAuthWebSecurity.RequestAuthentication("Facebook", callbackuri);
            }
            else
            {
                new Snovaspace.Util.Utility().DisplayMessage(this, "Login to invite your friends.");
            }
            LoggingManager.Debug("Exiting IbtnFacebookClick - InviteFriends.aspx");

          //  Response.Redirect("oauth.aspx?currpage=facebook", false);
        }

        protected void IbtnTwitterClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnTwitterClick - InviteFriends.aspx");
            if (_isUserLoggedIn)
            {
                OAuthWebSecurity.RequestAuthentication("Twitter", callbackuri);
            }
            else
            {
                new Snovaspace.Util.Utility().DisplayMessage(this, "Login to invite your friends.");
            }
            // Response.Redirect("oauth.aspx?currpage=twitter", false);
            LoggingManager.Debug("Exiting IbtnTwitterClick - InviteFriends.aspx");
        }

        protected void IbtnLinkedInClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnLinkedInClick - InviteFriends.aspx");
            if (_isUserLoggedIn)
            {
                OAuthWebSecurity.RequestAuthentication("LinkedIn", callbackuri);
            }
            else
            {
                new Snovaspace.Util.Utility().DisplayMessage(this, "Login to invite your friends.");
            }
            //  Response.Redirect("oauth.aspx?currpage=linkedin", false);
            LoggingManager.Debug("Exiting IbtnLinkedInClick - InviteFriends.aspx");
        }

        protected void UploadInvites(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UploadInvites - InviteFriends.aspx");
            
            new InvitationManager().UploadContactsFromFileUploadControl(Page, fuInvitationFriends);

            LoggingManager.Debug("Exiting UploadInvites - InviteFriends.aspx");
        }

        protected void imgYoutube_Click(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering imgYoutube_Click - InviteFriends.aspx");
            Response.Redirect("http://www.youtube.com/user/london2012");
            LoggingManager.Debug("Exiting imgYoutube_Click - InviteFriends.aspx");
        }

        protected void lbNoCustom_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbNoCustom_Click - InviteFriends.aspx");
            mpeCustomize.Hide();
            SendInvites(0);
            LoggingManager.Debug("Exiting lbNoCustom_Click - InviteFriends.aspx");
        }

        protected void btnCustomInvite_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Exiting btnCustomInvite_Click - InviteFriends.aspx");
            int customId;
            int imageId;

            if (string.IsNullOrWhiteSpace(txtMailIDs.Text.Trim()))
            {
                new Snovaspace.Util.Utility().DisplayMessage(this, "Please enter atleast one email id.");
                return;
            }

            int.TryParse(hfImageId.Value, out imageId);

            if (imageId > 0)
            {
                mpeCustomize.Hide();

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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Invitaion sent succesfully')", true);
            }
            else
            {
                new Snovaspace.Util.Utility().DisplayMessage(this, "please select an image");
                mpeCustomize.Show();
            }
            LoggingManager.Debug("Exiting btnCustomInvite_Click - InviteFriends.aspx");
        }

        private void SendInvites(int customId)
        {
            LoggingManager.Debug("Entering SendInvites - InviteFriends.aspx");
            var contacts = txtMailIDs.Text.Split(',');
            if (contacts.Length > 0)
            {
                var emailcontacts =
                    contacts.Select(email => new Contact {Name = string.Empty, Email = email.Trim()}).ToList();

                new InvitationManager().InviteEmailFriends(Page, emailcontacts, customId);

                txtMailIDs.Text = "";
            }
            LoggingManager.Debug("Exiting SendInvites - InviteFriends.aspx");
        }

        protected void lbtnChangeImage_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbtnChangeImage_Click - InviteFriends.aspx");
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
            LoggingManager.Debug("Exiting lbtnChangeImage_Click - InviteFriends.aspx");
        }
    }
}