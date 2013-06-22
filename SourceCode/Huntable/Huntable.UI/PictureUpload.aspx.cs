using System;
using Huntable.Business;
using Snovaspace.Util.Logging;
using Huntable.Data;
using System.Linq;

namespace Huntable.UI
{
    public partial class PictureUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load PictureUpload.aspx");
            var user = Common.GetLoggedInUser();
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            try
            {
                if (!IsPostBack)
                {
                    LoadCurrentProfile();
                }

                var objInvManager = new InvitationManager();
                bool userLoggedIn = Common.IsLoggedIn();
                var userId = Common.GetLoggedInUserId(Session);
               
                int id = Convert.ToInt32(userId);
                var userList = objInvManager.GetUsercurrentrole(id);
                if (userLoggedIn)
                {
                    ImgPicture.ImageUrl = user.UserProfilePictureDisplayUrl;
                   
                    if (userId != null)
                    {
                        
                        lblposition1.Text = userList.JobTitle;
                        lblcompany1.Text = userList.MasterCompany.Description;
                        var percentCompleted = UserManager.GetProfilePercentCompleted(userId.Value);
                        lblPercentCompleted.Text = (percentCompleted / 100).ToString("00%");
                        int value = Convert.ToInt32(percentCompleted);
                        ProgressBar2.Value = value;
                    }
                }
                
                    
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }


            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                Company Comp_id;
                if (user.IsCompany == true)
                {
                    ProfileCompletionSteps.Visible = false;
                    lblpositiontext.Visible = false;
                    lblposition1.Visible = false;
                    lblat.Visible = false;
                Comp_id = context.Companies.FirstOrDefault(x => x.Userid == loggedInUserId);
                    if (Comp_id == null) throw new ArgumentNullException("sender");
                    string compId = Comp_id.Id.ToString();
                    a_profile.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(compId));
                    y_n.Visible = false;
                }
                else
                {
                    a_profile.HRef = "ViewUserProfile.aspx";
                }
            }
            
            LoggingManager.Debug("Exiting Page_Load PictureUpload.aspx");
        }

        private void LoadCurrentProfile()
        {
            LoggingManager.Debug("Entering LoadCurrentProfile PictureUpload.aspx");
            try
            {
                var user = Common.GetLoginUser(Session);
                var userId = Common.GetLoggedInUserId(Session);
                if (user != null)
                {
                    ImgPicture.ImageUrl = user.UserProfilePictureDisplayUrl;

                    lblname.Text = user.Name;
                    lblemail.Text = user.EmailAddress;
                    ImgCompanyLogo.ImageUrl = user.CompanyLogoPictureDisplayUrl;
                    if (userId != null)
                    {
                        var percentCompleted = UserManager.GetProfilePercentCompleted(userId.Value);
                        lblPercentCompleted.Text = (percentCompleted / 100).ToString("00%");
                    }
                    LoggingManager.Info("Percent Completed:" + lblPercentCompleted.Text);
                    ImgPreview.ImageUrl = user.UserProfilePictureDisplayUrl;
                }
                //var currentEmployment = user.EmploymentHistories
                //if (currentEmployment != null)
                //{
                //    LoggingManager.Info("Inside Current Employment");
                //    lblCurrentRole.Text = string.Format("{0} at {1}", currentEmployment.JobTitle, currentEmployment.MasterCompany != null ? currentEmployment.MasterCompany.Description : string.Empty);
                //}
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadCurrentProfile PictureUpload.aspx");
        }

        protected void BtnChangePicClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnChangePicClick PictureUpload.aspx");
            try
            {
                var userId = Common.GetLoggedInUserId(Session);
                if (userId.HasValue)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var usid = context.Users.FirstOrDefault(x => x.Id == userId.Value);
                        if (usid.IsCompany == true)
                        {
                            if (uploadPhoto.HasFile)
                            {
                                UserManager.UploadCompanyPicture1(uploadPhoto, userId.Value);
                                LoadCurrentProfile();
                            }
                        }
                        else
                        {
                            if (uploadPhoto.HasFile)
                            {
                                UserManager.UploadUserPicture(uploadPhoto, userId.Value);
                                LoadCurrentProfile();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnChangePicClick PictureUpload.aspx");
        }

        protected void BtnViewProfileClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnViewProfileClick PictureUpload.aspx");
            try
            {
                LoggingManager.Info("Redirecting to ViewUserProfile.aspx");
                Response.Redirect("ViewUserProfile.aspx", false);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnViewProfileClick PictureUpload.aspx");
        }

        protected void BtnChangeCompanyPicClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnChangeCompanyPicClick PictureUpload.aspx");
            try
            {
                var userId = Common.GetLoggedInUserId(Session);
                if (userId.HasValue)
                {
                    if (UploadCompanyLogo.HasFile)
                    {
                        UserManager.UploadCompanyPicture(UploadCompanyLogo, userId.Value);
                        LoadCurrentProfile();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnChangeCompanyPicClick PictureUpload.aspx");
        }
        protected void BtnYesClick(object sender, EventArgs e)
        {
            divYes.Visible = false;
        }
    }
}