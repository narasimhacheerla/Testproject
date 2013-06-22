using System;
using System.Collections.Generic;
using System.Web.UI;
using Huntable.Business;
using ImpactWorks.FBGraph.Core;
using Snovaspace.Facebook.Entities;
using Snovaspace.Facebook;
using System.IO;
using Huntable.Data;
using System.Drawing;
using Snovaspace.Util.Logging;
using System.Web.UI.HtmlControls;
using System.Linq;

namespace Huntable.UI
{
    public partial class HProfileUploadPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - ProfileUploadPage.aspx");

            var userid = Common.GetLoggedInUserId();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var usr = context.Users.FirstOrDefault(x => x.Id == userid);
                if (usr.IsProfileUpdated == false)
                {
                    usr.IsProfileUpdated = true;
                    context.SaveChanges();
                }
            }
            if (Page.Master != null)
            {
                var uch = Page.Master.FindControl("headerAfterLoggingIn") as UserControl;
                if (uch != null)
                {
                    var divMenu = (HtmlGenericControl)uch.FindControl("menu");
                    var divMsg = (HtmlGenericControl)uch.FindControl("Div1");
                    divMenu.Visible = false;

                }
            }


            try
            {

                if (!IsPostBack)
                {
                    LoadProfilePercentCompleted();
                }

                Facebook fb = new FacebookService().GetById(99); // we haven't saved anything in any database, so we are getting a fake'd object;
                string facebookClientId = System.Configuration.ConfigurationManager.AppSettings["facebookAppID"];
                string host = Request.ServerVariables["HTTP_HOST"];
                var protocol = Request.Url.Scheme;
                imgBtnFB.PostBackUrl = string.Concat(@"https://graph.facebook.com/oauth/authorize?client_id=" + facebookClientId + "&",
                                                            "redirect_uri=" + protocol + "://" + host + @"/ImportDetailsFromFacebook.aspx&scope=user_photos,user_videos,publish_stream,offline_access,user_photo_video_tags");
                List<FBPermissions> permissions = new List<FBPermissions>() {
            
                FBPermissions.email, //To get user's email address
                FBPermissions.user_about_me, // to read about me
                FBPermissions.user_birthday, // Get DOB
                FBPermissions.user_education_history, //get education
                FBPermissions.user_location, //Location of user
                FBPermissions.user_relationships,//relationship status of user
                FBPermissions.user_work_history,//Workhistory of user
                FBPermissions.user_website,//website entered in fb Profilr
                FBPermissions.user_status
            };

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - ProfileUploadPage.aspx");
        }

        private void LoadProfilePercentCompleted()
        {
            LoggingManager.Debug("Entering LoadProfilePercentCompleted - ProfileUploadPage.aspx");
            try
            {
                var userId = Common.GetLoggedInUserId(Session);
                if (userId.HasValue)
                {
                    var percentCompleted = UserManager.GetProfilePercentCompleted(userId.Value);
                    lblPercentCompleted.Text = (percentCompleted / 100).ToString("00%");
                    int value = Convert.ToInt32(percentCompleted);
                    //ProgressBar1.Value = value;
                    ProgressBar2.Value = value;
                    if (percentCompleted > 60)
                    {
                        uploadResume.Visible = false;
                        btnUploadProfile.Visible = false;

                    }
                    LoggingManager.Info("Percent Completed:" + lblPercentCompleted.Text);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadProfilePercentCompleted - ProfileUploadPage.aspx");

        }

        protected void BtnprofileClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnprofileClick - ProfileUploadPage.aspx");
            try
            {
                string dirName = Path.GetTempPath();
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
                string fileName = dirName + "\\" + uploadResume.FileName;
                uploadResume.SaveAs(fileName);

                var context = huntableEntities.GetEntitiesWithNoLock();

                var userId = Common.GetLoggedInUserId(Session);

                if (userId != null)
                {
                    string extension = Path.GetExtension(uploadResume.PostedFile.FileName);
                    if (extension == ".doc" || extension == ".docx" || extension == ".txt" ||
                        extension == ".rtf" || extension == ".pdf")
                    {
                        new PostCVManager().ImportResume(context, userId.Value, fileName);

                        lblUploadResumeMessage.Text = "Resume uploaded successfully.";
                        lblUploadResumeMessage.ForeColor = Color.Green;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Call my function", "showDiv()", true);
                        //Response.Redirect("EditProfilePage.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                lblUploadResumeMessage.Text = "Error occurred while uploading the message.";
                lblUploadResumeMessage.ForeColor = Color.Red;
                LoggingManager.Error(ex);
                lblUploadResumeMessage.Text = ex.Message;
            }
            LoggingManager.Debug("Exiting BtnprofileClick - ProfileUploadPage.aspx");
        }

        protected void ImageButton1Click(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering BtnprofileClick - ProfileUploadPage.aspx");
            Response.Redirect("PictureUpload.aspx");
            LoggingManager.Debug("Exiting BtnprofileClick - ProfileUploadPage.aspx");
        }

        protected void BtnImportFromLinkedinClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering BtnprofileClick - ProfileUploadPage.aspx");
            Response.Redirect("LinkedInProfile.aspx");
            LoggingManager.Debug("Exiting BtnprofileClick - ProfileUploadPage.aspx");
        }

        protected void BtndetailsClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnprofileClick - ProfileUploadPage.aspx");
            Response.Redirect("EditProfilePage.aspx");
            LoggingManager.Debug("Exiting BtnprofileClick - ProfileUploadPage.aspx");
        }
    }
}