using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Huntable.Data;
using Huntable.Business;
using Snovaspace.Util.Logging;
using System.Linq;
using System.Data;
using System.Web.UI;

namespace Huntable.UI
{
    public partial class MessageInbox : System.Web.UI.Page
    {
        public delegate void DelPopulateSearchUsers(int pageIndex);

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - MessageInbox.aspx");
            try
            {
                if (!IsPostBack)
                {
                    var loggedInUserId = Common.GetLoggedInUserId(Session);
                    var user = Common.GetLoggedInUser();
                    if (loggedInUserId.HasValue)
                    {
                        using (var context = huntableEntities.GetEntitiesWithNoLock())
                        {
                            DateTime dt = user.LastLoginTime;
                            string s = dt.ToString("hh:mm t.\\M");
                            var objMessageManager = new UserMessageManager();
                            User userDetails = objMessageManager.GetUserbyUserId(context, loggedInUserId.Value);
                            lblProfileName.Text = userDetails.Name;
                            imgProfilePicture.ImageUrl = userDetails.UserProfilePictureDisplayUrl;
                            lblLogDate.Text = user.LastLoginTime.ToShortDateString();
                            lblLastLoginTime.Text = s;
                            lblLastProfileUpdatedOn.Text = userDetails.LastProfileUpdatedOn.ToShortDateString();
                            //ddlToUser.DataTextField = "Name";
                            //ddlToUser.DataValueField = "Id";
                            //ddlToUser.DataSource = objMessageManager.GetAllUserExcludingLoginUser(context, loggedInUserId.Value);
                            //ddlToUser.DataBind();
                            var percentCompleted = UserManager.GetProfilePercentCompleted(loggedInUserId.Value);
                            lblPercentCompleted.Text = (percentCompleted / 100).ToString("00%");
                            int value = Convert.ToInt32(percentCompleted);
                            ProgressBar2.Value = value;
                            LoggingManager.Info("Percent Completed:" + lblPercentCompleted.Text);
                            rptrSentMessages.Visible = false;
                            GetMessages(1);
                            var usr = context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
                            if (usr.IsCompany == true)
                            {
                                isyour.HRef = "companyregistration2.aspx";
                                ProfileHuntablediv.Visible = false;

                            }
                            else
                            {
                                isyour.HRef = "editprofilepage.aspx";
                            }
                            var usri = context.Users.FirstOrDefault(x => x.Id == LoginUserId);
                            if (usri != null && usri.IsPremiumAccount == null)
                            {
                                bimage.Visible = true;
                                pimage.Visible = false;
                            }
                            else
                            {
                                bimage.Visible = false;
                                pimage.Visible = true;
                            }
                           
                            
                        }
                    }
                    else
                    {
                        bimage.Visible = true;
                        pimage.Visible = false;
                    }
                }
                if (rptrSentMessages.Visible)
                {
                    var delPopulate = new DelPopulateSearchUsers(GetSentMessages);
                    pagerMessages.UpdatePageIndex = delPopulate;
                }
                else
                {
                    var delPopulate = new DelPopulateSearchUsers(GetMessages);
                    pagerMessages.UpdatePageIndex = delPopulate;
                }

                if (!String.IsNullOrEmpty(hdnSentTo.Value))
                {
                    SaveRepliedMessage();
                    hdnSentTo.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - MessageInbox.aspx");
        }

        protected void BtnSentClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSentClick - MessageInbox.aspx");
            try
            {
                rptrMessage.Visible = false;
                btnMarkAsUnreadSelectedMessages.Visible = false;
                //HtmlInputButton btnSelectRead = this.FindControl("btnSelectRead") as HtmlInputButton;
                //btnSelectRead.Visible = false;
                //btnSelectRead.Attributes.Add("hidden", "hidden");
                rptrSentMessages.Visible = true;
                GetSentMessages(1);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnSentClick - MessageInbox.aspx");
        }
        private int LoginUserId
        {
            get
            {
                return Common.GetLoggedInUserId(Session).Value;
            }
        }
        protected void BtnSearchCLick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering BtnSearchCLick - MessageInbox.aspx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var query = context.UserMessages.Where(x => x.SentTo == LoginUserId);
                query=query.Where(x => x.Body.Contains(txtsearch.Text.ToLower())
                                          || x.Subject.Contains(txtsearch.Text.ToLower()));
                rptrMessage.DataSource = query;
                rptrMessage.DataBind();
            }
            LoggingManager.Debug("Exiting BtnSearchCLick - MessageInbox.aspx");
        }
        protected void BtnInboxClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnInboxClick - MessageInbox.aspx");
            try
            {
                rptrSentMessages.Visible = false;
                btnMarkAsUnreadSelectedMessages.Visible = true;
                rptrMessage.Visible = true;
                GetMessages(1);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnInboxClick - MessageInbox.aspx");
            
        }

        protected void BtnComposeClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnComposeClick - MessageInbox.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var objMessageManager = new UserMessageManager();
                    var toUser = objMessageManager.GetUserByName(context, txtToUser.Text.Trim());
                    if (toUser !=null)
                    {
                        var userMessage = new UserMessage
                        {
                            SentBy = Convert.ToInt32(Common.GetLoggedInUserId(Session)),
                            SentTo = Convert.ToInt32(toUser.Id),
                            Subject = txtComposeSubject.Value,
                            Body = txtComposeBody.Value,
                            IsActive = true,
                            SentIsActive = true,
                            IsRead = false,
                            SentDate = DateTime.Now
                        };

                        lblMessage.Text = objMessageManager.SaveMessage(context, userMessage) == 1 ? "Message Sent Successfully." : "Message Sending Failed. Please try again.";
                    }
                    else
                    {
                        lblMessage.Text = string.Format("Entered User Email does not Exist.");
                    }
                    txtToUser.Text = "";
                   
                    txtComposeBody.Value = "";
                    //txtComposeBody.("placeholder", "Body...");

                    txtComposeSubject.Value = "";
                    //txtComposeSubject.Attributes("placeholder" , "Subject...")

                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnComposeClick - MessageInbox.aspx");
        }



        //protected void ReadClick(object sender, EventArgs e)
        //{
        //    LoggingManager.Debug("Entering read_Click - MessageInbox.aspx");
        //    try
        //    {
        //        using (var context = huntableEntities.GetEntitiesWithNoLock())
        //        {
        //            var objMessageManager = new UserMessageManager();
        //            var button = sender as Button;
        //            if (button != null)
        //                objMessageManager.MarkasRead(context, Convert.ToInt32(button.CommandArgument));
        //        }
        //       Label lblmsgcnt = Page.Master.FindControl("headerAfterLoggingIn").FindControl("lblMessagesCount") as Label;
        //        using (var context = huntableEntities.GetEntitiesWithNoLock())
        //        {
        //            var user = Common.GetLoggedInUser(context);
        //            lblmsgcnt.Text = Common.GetUnreadMessagesCount(context, user).ToString();
        //        }
        //        GetMessages(pagerMessages.PageIndex);
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggingManager.Error(ex);
        //    }
        //    LoggingManager.Debug("Exiting read_Click - MessageInbox.aspx");
        //}

        private void SaveRepliedMessage()
        {
            LoggingManager.Debug("Entering SaveRepliedMessage - MessageInbox.aspx");
            try
            {
              
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var userMessage = new UserMessage
                                              {
                                                  SentBy = Convert.ToInt32(Common.GetLoggedInUserId(Session)),
                                                  SentTo = Convert.ToInt32(hdnSentTo.Value),
                                                  Subject = hdnReplySubject.Value,
                                                  Body = hdnReplyBody.Value,
                                                  IsActive = true,
                                                  SentIsActive = true,
                                                  IsRead = false,
                                                  SentDate = DateTime.Now
                                              };
                        LoggingManager.Info("Message subject:" + hdnReplySubject.Value);
                        LoggingManager.Info("Message Body:" + hdnReplyBody.Value);

                        var objMessageManager = new UserMessageManager();
                        lblMessage.Text = objMessageManager.SaveMessage(context, userMessage) == 1
                                              ? "Message Sent Successfully."
                                              : "Message Sending Failed. Please try again.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            hdnReplyBody.Value = "";
            hdnReplySubject.Value = "";
            LoggingManager.Debug("Exiting SaveRepliedMessage - MessageInbox.aspx");
        }

        protected void RowDeleteClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering RowDeleteClick - MessageInbox.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var objMessageManager = new UserMessageManager();
                    var button = sender as Button;
                    if (button != null)
                        objMessageManager.DeleteMessage(context, Convert.ToInt32(button.CommandArgument));
                }
                GetMessages(pagerMessages.PageIndex);
                lblMessage.Text = "Message Deleted Successfully";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting RowDeleteClick - MessageInbox.aspx");
        }

        

        protected void BtnDeleteSelectedMessagesClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnDeleteSelectedMessagesClick - MessageInbox.aspx");
            try
            {
                DeleteSelectedMessages(rptrSentMessages.Visible ? rptrSentMessages : rptrMessage);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnDeleteSelectedMessagesClick - MessageInbox.aspx");
        }

        private void DeleteSelectedMessages(Repeater ctrlRepeater)
        {
            LoggingManager.Debug("Entering DeleteSelectedMessages - MessageInbox.aspx");
            try
            {
                bool selectionFound = false;

                for (int i = 0; i < ctrlRepeater.Items.Count; i++)
                {
                    var checkBox = ctrlRepeater.Items[i].FindControl("cbSelect") as CheckBox;
                    if (checkBox != null && checkBox.Checked)
                    {
                        selectionFound = true;
                        break;
                    }
                }

                if (selectionFound)
                {
                    var selectedMessageIds = new List<int>();
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var objMessageManager = new UserMessageManager();
                        for (int i = 0; i < ctrlRepeater.Items.Count; i++)
                        {
                            var checkBox = ctrlRepeater.Items[i].FindControl("cbSelect") as CheckBox;
                            if (checkBox != null && checkBox.Checked)
                            {
                                var label = ctrlRepeater.Items[i].FindControl("lblMessageId") as Label;
                                if (label != null)
                                    selectedMessageIds.Add(Convert.ToInt32(label.Text));
                                var label1 = rptrMessage.Items[i].FindControl("lblMessageId") as Label;
                                if (label1 != null)
                                    objMessageManager.DeleteMessage(context, Convert.ToInt32(label1.Text));
                            }
                        }
                        objMessageManager.DeleteMutipleSentMessages(context, selectedMessageIds);
                    }
                    LoggingManager.Info("PagerMessages Page Index:" + pagerMessages.PageIndex);
                    GetSentMessages(pagerMessages.PageIndex);
                   
                    GetMessages(pagerMessages.PageIndex);
                    lblMessage.Text = "Message Deleted Successfully";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                  
                }
                else
                {
                    lblMessage.Text = string.Format("Please select message(s) to delete.");
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DeleteSelectedMessages - MessageInbox.aspx");
        }

        protected void BtnMarkAsUnreadSelectedMessagesClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnMarkAsUnreadSelectedMessagesClick - MessageInbox.aspx");
            try
            {
                bool selectionFound = false;

                for (int i = 0; i < rptrMessage.Items.Count; i++)
                {
                    var checkBox = rptrMessage.Items[i].FindControl("cbSelect") as CheckBox;
                    if (checkBox != null && checkBox.Checked)
                    {
                        selectionFound = true;
                        break;
                    }
                }

                if (selectionFound)
                {
                    var selectedMessageIds = new List<int>();
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var objMessageManager = new UserMessageManager();
                        for (int i = 0; i < rptrMessage.Items.Count; i++)
                        {
                            var checkBox = rptrMessage.Items[i].FindControl("cbSelect") as CheckBox;
                            if (checkBox != null && checkBox.Checked)
                            {
                                var label = rptrMessage.Items[i].FindControl("lblMessageId") as Label;
                                if (label != null)
                                    selectedMessageIds.Add(Convert.ToInt32(label.Text));
                            }
                        }
                        objMessageManager.AlterMutipleMessages(context, selectedMessageIds, "MarkAsUnread");
                    }
                    LoggingManager.Info("Pager Messages Page Index;" + pagerMessages.PageIndex);

                    Label lblmsgcnt = Page.Master.FindControl("headerAfterLoggingIn").FindControl("lblMessagesCount") as Label;
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var user = Common.GetLoggedInUser(context);
                        lblmsgcnt.Text = Common.GetUnreadMessagesCount(context, user).ToString();
                    }

                    GetMessages(pagerMessages.PageIndex);
                }
                else
                {
                    lblMessage.Text = "Please select message(s) to Mark as Unread.";
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnMarkAsUnreadSelectedMessagesClick - MessageInbox.aspx");
        }

        protected void BtnDeleteSentMessageClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnDeleteSentMessageClick - MessageInbox.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var objMessageManager = new UserMessageManager();
                    var button = sender as Button;
                    if (button != null)
                        objMessageManager.DeleteSentMessage(context, Convert.ToInt32(button.CommandArgument));
                }
                GetSentMessages(pagerMessages.PageIndex);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnDeleteSentMessageClick - MessageInbox.aspx");
        }

        private void GetMessages(int pageIndex)
        {
            LoggingManager.Debug("Entering GetMessages - MessageInbox.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    int totalRecords;

                    var objMessageManager = new UserMessageManager();
                    var userMessages = objMessageManager.GetUserMessages(context, Convert.ToInt32(Common.GetLoggedInUserId(Session)), out totalRecords, pagerMessages.RecordsPerPage, pageIndex);
                    foreach (UserMessage usrMsg in userMessages)
                    {
                        if (objMessageManager.GetUserMessageStatus(context, usrMsg.SentTo, usrMsg.SentBy))
                        {
                            usrMsg.IsUserBlockMsg = false;
                        }
                        else
                        {
                            usrMsg.IsUserBlockMsg = true;
                        }
                    }
                    pagerMessages.TotalRecords = totalRecords;
                    LoggingManager.Info("Total records:" + totalRecords);
                    rptrMessage.DataSource = userMessages;
                    rptrMessage.DataBind();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting GetMessages - MessageInbox.aspx");
        }

        private void GetSentMessages(int pageIndex)
        {
            LoggingManager.Debug("Entering GetSentMessages - MessageInbox.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    int totalRecords;

                    var objMessageManager = new UserMessageManager();
                    var userSentMessages = objMessageManager.GetUserSentMessages(context, Convert.ToInt32(Common.GetLoggedInUserId(Session)), out totalRecords, pagerMessages.RecordsPerPage, pageIndex);

                    pagerMessages.TotalRecords = totalRecords;
                    LoggingManager.Info("Total records:" + totalRecords);
                    rptrSentMessages.DataSource = userSentMessages;
                    rptrSentMessages.DataBind();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting GetSentMessages - MessageInbox.aspx");
           
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnSelect_Click - MessageInbox.aspx");

            if (rptrSentMessages.Visible == true)
            {
                foreach (RepeaterItem ri in rptrSentMessages.Items)
                {
                    CheckBox chkMessage = ri.FindControl("cbSelect") as CheckBox;

                    if (chkMessage != null)
                    {
                        chkMessage.Checked = true;
                    }
                }
            }
            else
            {
                foreach (RepeaterItem ri in rptrMessage.Items)
                {
                    CheckBox chkMessage = ri.FindControl("cbSelect") as CheckBox;

                    if (chkMessage != null)
                    {
                        chkMessage.Checked = true;
                    }
                }
            }
            LoggingManager.Debug("Exiting btnSelect_Click - MessageInbox.aspx");
        }

        protected void btnUnCheck_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnUnCheck_Click - MessageInbox.aspx");
            if (rptrSentMessages.Visible == true)
            {
                foreach (RepeaterItem ri in rptrSentMessages.Items)
                {
                    CheckBox chkMessage = ri.FindControl("cbSelect") as CheckBox;

                    if (chkMessage != null)
                    {
                        chkMessage.Checked = false;
                    }
                }
            }
            else
            {
                foreach (RepeaterItem ri in rptrMessage.Items)
                {
                    CheckBox chkMessage = ri.FindControl("cbSelect") as CheckBox;

                    if (chkMessage != null)
                    {
                        chkMessage.Checked = false;
                    }
                }
            }
            LoggingManager.Debug("Exiting btnUnCheck_Click - MessageInbox.aspx");

        }
        //protected void userprofile(object sender, EventArgs e)
        //{
        //    var btn = sender as ImageButton;
        //    using (var context = huntableEntities.GetEntitiesWithNoLock())
        //    {
        //        var usr = context.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(btn.CommandArgument));

        //        if (usr.IsCompany == true)
        //        {

        //        }
        //    }
        //}
        protected void itembound(object sender , RepeaterItemEventArgs e)
        {
            Label l = (Label)e.Item.FindControl("lblMessageDateTime");
            HtmlAnchor a1 = (HtmlAnchor)e.Item.FindControl("imga");
            HyperLink h1 = (HyperLink)e.Item.FindControl("lnkSenderProfileName");
            if (e.Item.DataItem != null)
            {
                l.Attributes.Add("style", "margin-left:82px");
                int strUsername = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "User.Id").ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var usr = context.Users.FirstOrDefault(x => x.Id == strUsername);
                    
                  
                        if (usr.IsCompany == true)
                        {
                            var cmpny = context.Companies.FirstOrDefault(x => x.Userid == strUsername);
                            a1.HRef = new UrlGenerator().CompanyUrlGenerator(cmpny.Id);
                            h1.NavigateUrl = new UrlGenerator().CompanyUrlGenerator(cmpny.Id);
                        }
                        else
                        {
                            a1.HRef =  new UrlGenerator().UserUrlGenerator(strUsername);
                            h1.NavigateUrl = new UrlGenerator().UserUrlGenerator(strUsername);
                        }
                    
                 }

            }
        }
        protected void itemboundsent(object sender, RepeaterItemEventArgs e)
        {

            HtmlAnchor a1 = (HtmlAnchor)e.Item.FindControl("imgsent");
            HyperLink h1 = (HyperLink)e.Item.FindControl("lnkSenderProfileName");
            if (e.Item.DataItem != null)
            {
                int strUsername = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "User1.Id").ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var usr = context.Users.FirstOrDefault(x => x.Id == strUsername);


                    if (usr.IsCompany == true)
                    {
                        var cmpny = context.Companies.FirstOrDefault(x => x.Userid == strUsername);
                        a1.HRef = new UrlGenerator().CompanyUrlGenerator(cmpny.Id);
                        h1.NavigateUrl = new UrlGenerator().CompanyUrlGenerator(cmpny.Id);
                    }
                    else
                    {
                        a1.HRef = new UrlGenerator().UserUrlGenerator(strUsername);
                        h1.NavigateUrl = new UrlGenerator().UserUrlGenerator(strUsername);
                    }

                }

            }
        }
    }
}