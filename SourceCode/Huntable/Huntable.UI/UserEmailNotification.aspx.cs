using System;
using Huntable.Data;
using Huntable.Business;
using Snovaspace.Util.Logging;
using System.Linq;

namespace Huntable.UI
{
    public partial class UserEmailNotification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - UserMailNotification.aspx");
            try
            {
                if (!IsPostBack)
                {
                    LoadValues();

                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - UserMailNotification.aspx");
        }

        private void LoadValues()
        {
            LoggingManager.Debug("Entering LoadValues - UserMailNotification.aspx");
            try
            {
                var loggedInUserId = Common.GetLoggedInUserId();
                if (loggedInUserId != null)
                {
                    UserEmailPreference userEmailNotificationData = Common.GetuserEmailPref(loggedInUserId.Value);
                    if (userEmailNotificationData != null)
                    {
                        chkBxAdminEmailTemp.Checked = userEmailNotificationData.AdminEmail;
                        chkBxEndorsementrequest.Checked = userEmailNotificationData.EndorsementRequest;
                        //chkBxForgotPassword.Checked = userEmailNotificationData.ForgotYourPassword;
                        //chkBxForgotPwdEmailLnk.Checked = userEmailNotificationData.ForgotPasswordEmailLnk;
                        chkBxInviteFrndsWhnJoined.Checked = userEmailNotificationData.InviteFriendsWhenJoined;
                        //chkBxResetPwd.Checked = userEmailNotificationData.ResetPassword;
                        chkBxVerifyYOurEmail.Checked = userEmailNotificationData.VerifyYourEmail;
                        chkBxWelcomeEmail.Checked = userEmailNotificationData.WelcoomeEmail;
                        chkBxWhenFriendsFriedJoin.Checked = userEmailNotificationData.WhenFriendsFriendsJoin;
                        chkBxWhenFriendsJOin.Checked = userEmailNotificationData.WhenFriendsJoin;
                        chkBxWhenThirdConnJoins.Checked = userEmailNotificationData.WhenThirdConnectionJoins;
                        chkBxWhenUserSndMsg.Checked = userEmailNotificationData.WhenUserSendAMessage;
                        chkBxWhnUserAppliesForJob.Checked = userEmailNotificationData.WhenUserAppliesForAJob;
                    }
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var usr = context.Users.FirstOrDefault(x => x.Id == loggedInUserId.Value);
                        if (usr != null && usr.IsPremiumAccount == null)
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
                    LoggingManager.Debug("User not logged in");
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadValues - UserMailNotification.aspx");
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSaveClick - UserMailNotification.aspx");
            try
            {
                var loggedInUserId = Common.GetLoggedInUserId();
                if (loggedInUserId != null)
                {
                    var emPref = new UserEmailPreference
                                     {
                                         AdminEmail = (chkBxAdminEmailTemp.Checked),
                                         EndorsementRequest = (chkBxEndorsementrequest.Checked),
                                         //ForgotPasswordEmailLnk = (chkBxForgotPwdEmailLnk.Checked),
                                         //ForgotYourPassword = (chkBxForgotPassword.Checked),
                                         InviteFriendsWhenJoined = (chkBxInviteFrndsWhnJoined.Checked),
                                         //ResetPassword = (chkBxResetPwd.Checked),
                                         UserId = loggedInUserId.Value,
                                         VerifyYourEmail = (chkBxVerifyYOurEmail.Checked),
                                         WelcoomeEmail = (chkBxWelcomeEmail.Checked),
                                         WhenFriendsFriendsJoin = (chkBxWhenFriendsFriedJoin.Checked),
                                         WhenFriendsJoin = (chkBxWhenFriendsJOin.Checked),
                                         WhenThirdConnectionJoins = (chkBxWhenThirdConnJoins.Checked),
                                         WhenUserAppliesForAJob = (chkBxWhnUserAppliesForJob.Checked),
                                         WhenUserSendAMessage = (chkBxWhenUserSndMsg.Checked)
                                     };

                    var js = new JobsManager();
                    js.DeleteUserEmailNotification(emPref.UserId);
                    js.SaveUserEmailNotification(emPref);
                }
                else
                {
                    LoggingManager.Debug("User not logged in");
                }

                btnSave.Visible = false;
                lblStatus.Visible = true;
                lblStatus.Text = "Successfully saved the information to profile";
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting BtnSaveClick - UserMailNotification.aspx");
        }
    }
}