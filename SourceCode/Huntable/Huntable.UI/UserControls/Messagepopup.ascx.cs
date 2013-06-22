using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI.UserControls
{
    public partial class Messagepopup : System.Web.UI.UserControl
    {
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - CompanyView");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - CompanyView");

                return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                LoggingManager.Debug("Entering LoadProfile - Messagepopup.ascx");
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var loginUserId = Common.GetLoggedInUserId(Session);
                    //User user = null;
                    //user = OtherUserId.HasValue ? context.Users.First(u => u.Id == OtherUserId.Value) : context.Users.First(u => u.Id == loginUserId.Value);
                    if (OtherUserId.HasValue)
                    {
                        var comp = context.Companies.FirstOrDefault(s => s.Id == OtherUserId);
                        if (comp != null) txtToAddress.Text = comp.CompanyName;
                        if (comp.Userid == LoginUserId)
                        {
                            btnMessage.Visible = false;
                        }
                    }
                   
                   

                    hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

         
            LoggingManager.Debug("Exiting LoadProfile - Messagepopup.ascx");
        }

        private int? OtherUserId
        {
            get
            {
                int otherUserId;
                if (int.TryParse(Request.QueryString["Id"], out otherUserId))
                {
                    return otherUserId;
                }
                if ((Page.RouteData.Values["ID"]) != null)
                {
                    string id = (Page.RouteData.Values["ID"]).ToString();
                    string[] words = id.Split('-');
                    int k = words.Length;
                    string companyid = words[k - 1];
                    return Convert.ToInt32(companyid);

                }
                return null;
            }
        }
        protected void BtnMessageClick(object sender, EventArgs e)
        {
            try
            {
                LoggingManager.Debug("Entering btnMessage_Click - MessagePopup.ascx");
                if (Common.GetLoggedInUserId(Session) != null)
                {
                    if (rbMessageList.SelectedValue == "0")
                    {
                        hfSubject.Value = "Job Enquiry";
                    }
                    else if (rbMessageList.SelectedValue == "1")
                    {
                        hfSubject.Value = "Request endorsement";
                    }
                    else if (rbMessageList.SelectedValue == "2")
                    {
                        hfSubject.Value = "Introduce Yourself";
                    }
                    else if (rbMessageList.SelectedValue == "3")
                    {
                        hfSubject.Value = "New Business opportunity";
                    }
                    else if (rbMessageList.SelectedValue == "4")
                    {
                        hfSubject.Value = "Your Recruitment requirement";
                    }
                    else
                    {
                        hfSubject.Value = "Introduce Yourself";
                    }
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var msgto = context.Companies.FirstOrDefault(s => s.Id == OtherUserId);
                        int? msgtoo = msgto.Userid;
                        if (OtherUserId != null)
                        {
                            var userMessage = new UserMessage
                            {
                                SentBy = Convert.ToInt32(Common.GetLoggedInUserId(Session)),
                                SentTo = msgtoo.Value,
                                Subject = hfSubject.Value,
                                Body = txtMessage.Text,
                                IsActive = true,
                                SentIsActive = true,
                                IsRead = false,
                                SentDate = DateTime.Now
                            };
                            var objMessageManager = new UserMessageManager();
                            objMessageManager.SaveMessage(context, userMessage);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Message sent succesfully ')", true);
                            var control = (HeaderAfterLoggingIn)FindControl("HeaderAfterLoggingIn");
                            control.Flashmessage("Message");
                        }
                    }
                }
                else
                {

                    Response.Write("<script language='javascript'>alert('You are not loggedin.Please login first');</script>");
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting btnMessage_Click - Messagepopup.ascx");
        }
        public void FlasMessage(string message,string popupmessage2ClientID,Page yourpage,HtmlGenericControl popupmessage2)
        {
            
        }
    }
}