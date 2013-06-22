using System;
using System.Linq;
using System.Web.UI;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI.UserControls
{
    public partial class UserMessagePopup : UserControl
    {
        public int LoginUserId
        {
            get
            {
                

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

               
                return 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    if (OtherUserId.HasValue)
                    {
                        var userMesg = context.Users.FirstOrDefault(s => s.Id == OtherUserId);
                        if (userMesg != null) txtToAddress.Text = userMesg.FirstName;
                        if (userMesg != null && userMesg.Id == LoginUserId)
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


            
        }

       


        private int? OtherUserId
        {
            get
            {
                int otherUserId;
                if (int.TryParse(Request.QueryString["UserId"], out otherUserId))
                {
                    return otherUserId;
                }
                if (Page.RouteData.Values["ID"] != null)
                {
                    string id = (Page.RouteData.Values["ID"]).ToString();
                    string[] words = id.Split('-');
                    int k = words.Length;
                    string userid = words[k - 1];
                    return Convert.ToInt32(userid);
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
                        if (OtherUserId != null)
                        {
                            var userMessage = new UserMessage
                            {
                                SentBy = Convert.ToInt32(Common.GetLoggedInUserId(Session)),
                                SentTo = OtherUserId.Value,
                                Subject = hfSubject.Value,
                                Body = txtMessage.Text,
                                IsActive = true,
                                SentIsActive = true,
                                IsRead = false,
                                SentDate = DateTime.Now
                            };
                            var objMessageManager = new UserMessageManager();
                            objMessageManager.SaveMessage(context, userMessage);
                            Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Message sent succesfully ')", true);
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
    }
}