using System;
using System.Linq;
using Huntable.Data;
using Huntable.Business;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class MessageRead : System.Web.UI.Page
    {
        int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - MessageRead.aspx");
            _id = Convert.ToInt16(Request.QueryString["MessageID"]);
            if (!IsPostBack)
            {
                var loggedInUserId = Common.GetLoggedInUserId(Session);
                if (loggedInUserId != null)
                {
                    var percentCompleted = UserManager.GetProfilePercentCompleted(loggedInUserId.Value);
                    lblPercentCompleted.Text = (percentCompleted / 100).ToString("00%");
                    int value = Convert.ToInt32(percentCompleted);
                    ProgressBar2.Value = value;
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {


                        var usri = context.Users.FirstOrDefault(x => x.Id == loggedInUserId.Value);
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
                var user = Common.GetLoggedInUser();


                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var objMessageManager = new UserMessageManager();
                    objMessageManager.MarkasRead(context, _id);
                    var userMessages = objMessageManager.GetMessageDetails(_id);
                    int totalRecordsCount;
                    var allMessages = objMessageManager.GetUserMessages(context, Convert.ToInt32(Common.GetLoggedInUserId(Session)),out totalRecordsCount);
                    if (allMessages.SkipWhile(x => x.Id != _id).Skip(1).FirstOrDefault() != null)
                    {
                        var firstOrDefault = allMessages.SkipWhile(x => x.Id != _id).Skip(1).FirstOrDefault();
                        int prviousMessageId=0;
                        if (firstOrDefault != null)
                            prviousMessageId = firstOrDefault.Id;
                        lnkPrevious.NavigateUrl = "MessageRead.aspx?MessageID=" + prviousMessageId.ToString();
                    }
                    else
                    {
                        lnkPrevious.Visible = false;
                    }
                    allMessages.Reverse();
                    if (allMessages.SkipWhile(x => x.Id != _id).Skip(1).FirstOrDefault() != null)
                    {
                        var firstOrDefault = allMessages.SkipWhile(x => x.Id != _id).Skip(1).FirstOrDefault();
                        int nextMessageId=0;
                        if (firstOrDefault != null)
                            nextMessageId = firstOrDefault.Id;
                        lnkNext.NavigateUrl = "MessageRead.aspx?MessageID=" + nextMessageId.ToString();
                    }
                    else
                    {
                        lnkNext.Visible = false;
                    }
                    //rspMessages.DataSource = userMessages;
                    //rspMessages.DataBind();
                    var userMessage = userMessages.Where(x => x.Id == _id).ToList().FirstOrDefault();
                    if (userMessage != null)
                    {
                        lblsubject.Text = userMessage.Subject;
                        imgBtnSentBy.ImageUrl = Picture(userMessage.SentBy);
                        //lblName.Text = txtToAddress.Text = userMessage.User.Name;
                        var firstOrDefault = context.Users.Where(x => x.Id == userMessage.SentBy).ToList().FirstOrDefault();
                        if (firstOrDefault != null)
                            lbluserName.Text = txtToAddress.Text = lblName.Text = firstOrDefault.Name;
                        if (userMessage.Body != null) lblBody.Text = userMessage.Body.Replace("\n", "<br/>");
                        hdnSentTo.Value = userMessage.SentBy.ToString();
                    }
                    DateTime dt = user.LastLoginTime;
                    string s = dt.ToString("hh:mm t.\\M");
                    if (loggedInUserId != null)
                    {
                        User userDetails = objMessageManager.GetUserbyUserId(context, loggedInUserId.Value);
                        lblProfileName.Text = userDetails.Name;
                        imgProfilePicture.ImageUrl = userDetails.UserProfilePictureDisplayUrl;
                        lblLogDate.Text = user.LastLoginTime.ToShortDateString();
                        lblLastLoginTime.Text = s;
                        lblLastProfileUpdatedOn.Text = userDetails.LastProfileUpdatedOn.ToShortDateString();
                    }
                }
            }
            else
            {
                if (!(String.IsNullOrEmpty(hdnReplySubject.Value) && String.IsNullOrEmpty(hdnReplyBody.Value)))
                {
                    SaveRepliedMessage();
                }
            }

            LoggingManager.Debug("Exiting Page_Load - MessageRead.aspx");
        }
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - MessageRead.aspx");
            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Users.FirstOrDefault(x => x.Id == p);
                var photo = result.PersonalLogoFileStoreId;

            LoggingManager.Debug("Exiting Picture - MessageRead.aspx");
                return new FileStoreService().GetDownloadUrl(photo);
            }
        }
        public string Position(object id)
        {
            LoggingManager.Debug("Entering Position - MessageRead.aspx");

            var objInvManager = new InvitationManager();
            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Users.FirstOrDefault(x => x.Id == p);
                var userList = objInvManager.GetUsercurrentrole(p);
                if (userList != null)
                {
                    var label = userList.JobTitle+"at"+userList.MasterCompany.Description;

                    return label;
                }
                LoggingManager.Debug("Exiting Position - MessageRead.aspx");

                return null;
            }

        }
        protected void Delete(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Delete - MessageRead.aspx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var message = context.UserMessages.FirstOrDefault(x => x.Id == _id && x.IsActive ==true);
                if (message != null) message.IsActive = false;
                context.SaveChanges();

                Response.Redirect("MessageInbox.aspx");
            }
            LoggingManager.Debug("Exiting Delete - MessageRead.aspx");
        }
        private void SaveRepliedMessage()
        {
            LoggingManager.Debug("Entering saveRepliedMessage - MessageRead.aspx");
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
                        IsRead = false,
                        SentIsActive = true,
                        SentDate = DateTime.Now
                    };
                    LoggingManager.Info("Message subject:" + hdnReplySubject.Value);
                    LoggingManager.Info("Message Body:" + hdnReplyBody.Value);
                    lblMessage.Visible = true;
                    var objMessageManager = new UserMessageManager();
                    objMessageManager.SaveMessage(context, userMessage);
                    hdnReplySubject.Value = "";
                    hdnReplyBody.Value = "";
                    
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            hdnReplyBody.Value = "";
            hdnReplySubject.Value = "";
            LoggingManager.Debug("Exiting saveRepliedMessage - MessageRead.aspx");
        }
    }
}