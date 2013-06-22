using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using OAuthUtility;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using Huntable.Data.Enums;

namespace Huntable.UI
{
    public partial class SendInvitations : Page
    {
        private List<Contact> _contacts;
        List<Data.Invitation> _lstInvitations;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - SendInvitations.aspx");
            try
            {
                if (Session["contacts"] != null)
                {
                    _contacts = (List<Contact>)Session["contacts"];
                }
                else
                {
                   _contacts= Contact.SampleContacts.ToList();
                }

                if (!IsPostBack)
                {
                    LoadContacts();
                    var user = Common.GetLoginUser(Session);
                    if (user != null)
                    {
                        Img1.Src = user.UserProfilePictureDisplayUrl;
                        hfImageId.Value = user.PersonalLogoFileStoreId.ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - SendInvitations.aspx");   
        }

       


        private void LoadContacts()
        {
            LoggingManager.Debug("Entering LoadContacts - SendInvitations.aspx");
            try
            {
                var invmanager = new InvitationManager();
                var userId = Common.GetLoggedInUserId(Session);
                if (userId != null) _lstInvitations = invmanager.GetInvitationList(userId.Value, InvitationType.Email);

                gvInvitations.DataSource = _contacts;
                gvInvitations.DataBind();
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadContacts - SendInvitations.aspx");
        }

        protected void GVInvitationsRowCommand(object sender, GridViewCommandEventArgs e)
        {
            LoggingManager.Debug("Entering GVInvitationsRowCommand - SendInvitations.aspx");
            try
            {
                var message = Constants.CustomInvitationMessage;
                switch (e.CommandName)
                {
                    case "invite":
                        {
                            var dataKey = gvInvitations.DataKeys[Convert.ToInt32(e.CommandArgument)];
                            if (dataKey != null)
                            {
                                if (dataKey.Values != null)
                                {
                                    var email = dataKey.Values["Email"].ToString();
                                    ViewState["mode"] = email;
                                }
                            }
                            message = message.Replace("[first name]", dataKey.Values["Name"].ToString());
                            txtMessage.Text = message;
                            mpeCustomize.Show();
                        }

                        break;
                    case "inviteall":
                        {
                            ViewState["mode"] = "all";
                            message = message.Replace("[first name]", "friend");
                            txtMessage.Text = message;
                            mpeCustomize.Show();
                        }
                        break;
                       
                }
                hfMessage.Value = message;

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting GVInvitationsRowCommand - SendInvitations.aspx");
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering chkSelectAll_CheckedChanged - SendInvitations.aspx");
           
            foreach (var chkInv in from GridViewRow gv in gvInvitations.Rows select gv.FindControl("chkSelect") as CheckBox)
            {
                chkInv.Checked = !chkInv.Checked;
            }

            LoggingManager.Debug("Exiting chkSelectAll_CheckedChanged - SendInvitations.aspx"); 
        }

        protected void lbNoCustom_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbNoCustom_Click - SendInvitations.aspx"); 
            try
            {
                mpeCustomize.Hide();
                SendInvites(0);
            }
            catch(Exception){}
            
            LoggingManager.Debug("Exiting lbNoCustom_Click - SendInvitations.aspx"); 

        }

        protected void btnCustomInvite_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnCustomInvite_Click - SendInvitations.aspx"); 
            var customId = 0;
            var imageId = 0;
            int.TryParse(hfImageId.Value, out imageId);

            if (imageId>0)
            {
                mpeCustomize.Hide();
                try
                {


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
                catch(Exception ex)
                {
                    
                }
            }
            else
            {
                new Snovaspace.Util.Utility().DisplayMessage(this, "please select an image");
                mpeCustomize.Show();
            }
            LoggingManager.Debug("Exiting btnCustomInvite_Click - SendInvitations.aspx");  
        }

        private void SendInvites(int customId)
        {
            LoggingManager.Debug("Entering SendInvites - SendInvitations.aspx");  
            var mode = (string)ViewState["mode"];
            var user = Common.GetLoginUser(Session);
            var userId = Common.GetLoggedInUserId(Session);
            var count = 0;
            if (mode == "all")
            {
                var contacts = new List<Contact>();
                foreach (GridViewRow gvr in gvInvitations.Rows)
                {
                    var chkSelect = (CheckBox)gvr.FindControl("chkSelect");
                    if (chkSelect.Checked)
                    {
                        var dataKey = gvInvitations.DataKeys[gvr.RowIndex];
                        if (dataKey != null)
                        {
                            if (dataKey.Values != null)
                            {
                                var email = dataKey.Values["Email"].ToString();
                                if (!string.IsNullOrEmpty(email))
                                {
                                    var contact = _contacts.FirstOrDefault(c => c.Email == email);
                                    contacts.Add(contact);
                                }
                            }
                        }
                    }
                }

                var objInvManager = new InvitationManager();
                var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();

                if (userId != null)
                {
                    count = objInvManager.SendInvitations(userId.Value, baseUrl, contacts, customId,user.IsCompany);
                    new Snovaspace.Util.Utility().DisplayMessageWithPostback(this, "Sent " + count + " Invitations");
                }
            }
            else
            {
                var contacts = _contacts.Where(c => c.Email == mode).ToList();
                var objInvManager = new InvitationManager();
                var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
                if (userId != null)
                {
                    count = objInvManager.SendInvitations(userId.Value, baseUrl, contacts, customId,user.IsCompany);
                }
                new Snovaspace.Util.Utility().DisplayMessageWithPostback(this,
                                                                        count == 0
                                                                            ? "Invitation sent already to this contact"
                                                                            : "Invitation sent successfully");
            }

            LoadContacts();
            LoggingManager.Debug("Entering SendInvites - SendInvitations.aspx");  
        }

        protected void lbtnChangeImage_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbtnChangeImage_Click - SendInvitations.aspx");
            if (fuPhoto.HasFile)
            {
                var id = new FileStoreService().LoadFileFromFileUpload(Constants.CustomInvitationImages, fuPhoto);
                Img1.Src = new FileStoreService().GetDownloadUrl(id);
                hfImageId.Value = id.ToString();

            }
            else
            {
                new Snovaspace.Util.Utility().DisplayMessage(this,"please select an image");
               
            }
            mpeCustomize.Show();

            LoggingManager.Debug("Exiting lbtnChangeImage_Click - SendInvitations.aspx");
        }

        protected void gvInvitations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LoggingManager.Debug("Entering gvInvitations_RowDataBound - SendInvitations.aspx");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                var lbtnInvite = ((Button)e.Row.FindControl("btnInvite"));
                var dataKey = gvInvitations.DataKeys[Convert.ToInt32(e.Row.RowIndex)];
                if (dataKey != null)
                {
                    if (dataKey.Values != null)
                    {
                        var email = dataKey.Values["Email"].ToString();
                        if (_lstInvitations.Any(i => i.EmailAddress == email))
                        {
                            lbtnInvite.Text = "Re-Invite";

                            if (_lstInvitations.Any(i => i.EmailAddress == email && i.IsJoined))
                            {
                                lbtnInvite.Text = "Joined";
                                lbtnInvite.Enabled = false;
                            }
                        }
                    }
                }
              

               
            }

            LoggingManager.Debug("Exit gvInvitations_RowDataBound - SendInvitations.aspx");
        }
    
    }
}