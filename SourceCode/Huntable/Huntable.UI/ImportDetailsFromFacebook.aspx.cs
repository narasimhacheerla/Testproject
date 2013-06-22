using System;
using System.Linq;
using Snovaspace.Facebook.Entities;
using Snovaspace.Facebook;
using Snovaspace.Facebook.Tools;
using Huntable.Data;
using Huntable.Business;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class ImportDetailsFromFacebook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - ImportDetailsFromFacebook.aspx");
            try
            {
                string facebookClientId = System.Configuration.ConfigurationManager.AppSettings["facebookAppID"];
                string facebookSecret = System.Configuration.ConfigurationManager.AppSettings["facebookAppSecret"];
                string host = Request.ServerVariables["HTTP_HOST"];

                string code = Request.QueryString["code"];
                var protocol = Request.Url.Scheme;
                // remember to fix the Url for your own development environment
                string response = FacebookTools.CallUrl(string.Concat("https://graph.facebook.com/oauth/access_token?client_id=" + facebookClientId,
                                                                    "&redirect_uri=" + protocol + "://" + host + "/ImportDetailsFromFacebook.aspx",
                                                                    "&client_secret=" + facebookSecret,
                                                                    "&code=" + code));


                Facebook fb = new FacebookService().GetById(99);
                fb.FacebookAccessToken = response.Replace("access_token=", ""); // save the access token you are given
                fb.FacebookId = new FacebookService().User_GetDetails(fb.FacebookAccessToken).id; // call facebook for the profile id
                new FacebookService().Save(fb); // save it (puts stuff into session for the time being)


                FacebookProfile profileInfo = new FacebookService().User_GetDetails(fb.FacebookAccessToken);
                if (profileInfo != null)
                {
                    SaveInfoFromFB(profileInfo);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
                Response.Write(ex.Message);
                if (ex.InnerException != null)
                    Response.Write(ex.InnerException.Message);
            }
            LoggingManager.Debug("Exiting Page_Load - ImportDetailsFromFacebook.aspx");
        }

        private void SaveInfoFromFB(FacebookProfile profileInfo)
        {
            LoggingManager.Debug("Entering SaveInfoFromFB - ImportDetailsFromFacebook.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var loginUserId = Common.GetLoggedInUserId(Session);
                    if (loginUserId.HasValue)
                    {
                        var user = context.Users.First(u => u.Id == loginUserId);
                        LoggingManager.Info("User :" + user);
                        DateTime dob;
                        if (DateTime.TryParse(profileInfo.birthday, out dob))
                        {
                            user.DOB = dob;
                        }

                        user.Summary = profileInfo.bio;
                        user.SecondaryEmail = profileInfo.email;

                        if (!string.IsNullOrEmpty(profileInfo.link))
                        {

                        }

                        if (!string.IsNullOrEmpty(profileInfo.gender))
                        {
                            user.Gender = profileInfo.gender;
                        }
                        user.WebsiteAddress = profileInfo.website;
                        if (profileInfo.location != null)
                        {
                            user.City = profileInfo.location.name.Split(',')[0];
                        }
                        // Adding the languages.
                        if (profileInfo.languages != null)
                        {
                            foreach (var lang in profileInfo.languages)
                            {
                                var userLanguage = new UserLanguage { MasterLanguageId = MasterDataManager.GetLanguageId(lang.name) };
                                if (
                                    !context.UserLanguages.Any(
                                        l =>
                                        l.MasterLanguageId == userLanguage.MasterLanguageId && l.UserId == loginUserId))
                                {
                                    user.UserLanguages.Add(userLanguage);
                                }
                            }
                        }
                        if (profileInfo.work != null)
                        {
                            foreach (var workInfo in profileInfo.work)
                            {
                                if (workInfo.position != null && workInfo.employer != null)
                                {
                                    var employment = new EmploymentHistory
                                                         {
                                                             CompanyId =
                                                                 MasterDataManager.GetMasterCompanyId(
                                                                     workInfo.employer.name),
                                                             JobTitle = workInfo.position.name
                                                         };
                                    if (workInfo.location != null)
                                        employment.Town = workInfo.location.name;
                                    int? startYear = ExtractYear(workInfo.start_date);
                                    if (startYear.HasValue)
                                    {
                                        employment.FromYearID = MasterDataManager.GetYearId(startYear.Value.ToString());
                                    }
                                    int? startMonth = ExtractMonth(workInfo.start_date);
                                    if (startMonth.HasValue)
                                    {
                                        employment.FromMonthID = MasterDataManager.GetMonthId(startMonth.Value);
                                    }

                                    int? endYear = ExtractYear(workInfo.end_date);
                                    if (endYear.HasValue)
                                    {
                                        employment.ToYearID = MasterDataManager.GetYearId(endYear.Value.ToString());
                                    }
                                    int? endMonth = ExtractMonth(workInfo.end_date);
                                    if (endMonth.HasValue)
                                    {
                                        employment.ToMonthID = MasterDataManager.GetMonthId(endMonth.Value);
                                    }

                                    
                                    employment.IsCurrent = (!endYear.HasValue && !endMonth.HasValue);
                                    employment.Description = workInfo.description;
                                    user.EmploymentHistories.Add(employment);
                                }
                            }
                        }
                        // Uploading the educations.
                        if (profileInfo.education != null)
                        {
                            foreach (var eduInfo in profileInfo.education)
                            {
                                var education = new EducationHistory
                                                    {
                                                        Institution = eduInfo.school.name,
                                                        IsSchool = eduInfo.type == "High School"
                                                        //ToYearID = Common.GetYearId(context, eduInfo.year.name)
                                                    };
                                if (eduInfo.year != null && !string.IsNullOrEmpty(eduInfo.year.name))
                                {
                                    education.ToYearID = Common.GetYearId(context, eduInfo.year.name);
                                }

                                
                                user.EducationHistories.Add(education);
                            }
                        }
                        context.SaveChanges();
                        Response.Redirect("EditProfilePage.aspx", false);
                    }

                    context.SaveChanges();
                    Response.Redirect("EditProfilePage.aspx", false);

                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
                Response.Write(ex.Message);
                if (ex.InnerException != null)
                    Response.Write(ex.InnerException.Message);
            }
            LoggingManager.Debug("Exiting SaveInfoFromFB - ImportDetailsFromFacebook.aspx");
        }

        private int? ExtractYear(string dateString)
        {
            LoggingManager.Debug("Entering ExtractYear - ImportDetailsFromFacebook.aspx");

            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }
            else
            {
                int convertedValue = Convert.ToInt32(dateString.Split('-')[0]);
                if (convertedValue > 0)
                {
                    return convertedValue;
                }
                else
                {
                    LoggingManager.Debug("Exiting ExtractYear - ImportDetailsFromFacebook.aspx");
                    return null;
                }
            }

        }

        private int? ExtractMonth(string dateString)
        {
            LoggingManager.Debug("Entering ExtractMonth - ImportDetailsFromFacebook.aspx");

            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }
            else
            {
                int convertedValue = Convert.ToInt32(dateString.Split('-')[1]);
                if (convertedValue > 0)
                {
                    return convertedValue;
                }
                else
                {
                    LoggingManager.Debug("Exiting ExtractMonth - ImportDetailsFromFacebook.aspx");

                    return null;
                }
            }
        }
    }
}