using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data.Enums;
using Snovaspace.Util.Logging;
using OAuthUtility;

namespace Huntable.UI
{
    public partial class email_contacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - email_contacts.aspx");
            if (!IsPostBack)
            {
                LoadContacts();
            }
            LoggingManager.Debug("Exiting Page_Load - email_contacts.aspx");
        }

        private void LoadContacts()
        {
            LoggingManager.Debug("Entering LoadContacts - email_contacts.aspx");
            try
            {
                if (Session["contacts"] != null)
                {
                   var contacts = (List<Contact>)Session["contacts"];
                   gvInvitations.DataSource = contacts.Where(c=>c.Email !="");
                   gvInvitations.DataBind();
                }
               
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadContacts - email_contacts.aspx");
        }

        protected void lbtnInvite_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lbtnInvite_Click - email_contacts.aspx");
            try
            {
                SendInvitations();
            }
            catch(Exception ex)
            {
                LoggingManager.Error(ex);
                new Snovaspace.Util.Utility().DisplayMessage(this, "Exception:"+ex.Message);
            }
            LoggingManager.Debug("Exiting lbtnInvite_Click - email_contacts.aspx");
          
        }

        private void SendInvitations()
        {
            LoggingManager.Debug("Entering SendInvitations - email_contacts.aspx");
            var contacts = new List<Contact>();
            if (Session["contacts"] != null)
            {
                var _contacts = (List<Contact>)Session["contacts"];
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
                                var contact = _contacts.FirstOrDefault(c => c.Email == email);
                                contacts.Add(contact);
                            }
                        }
                    }
                }
               
            }
            if (contacts.Any())
            {
                if(Session["senderid"] !=null)
                {
                    var userId = (int) Session["senderid"];
                    var objInvManager = new InvitationManager();
                    var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
                    var count = objInvManager.SendInvitations(userId, baseUrl, contacts, 0,false);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sent " + count + " Invitations');window.location ='HomePageAfterLoggingIn.aspx';", true);
                }
            }
            else
            {
                new Snovaspace.Util.Utility().DisplayMessage(this, "No contacts available for invite");
            }
            LoggingManager.Debug("Exiting SendInvitations - email_contacts.aspx");
        }
    }
}