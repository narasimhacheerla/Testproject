using System;
using System.Collections.Generic;
using Huntable.UI.LinkedIn;
using LinkedIn;
using LinkedIn.ServiceEntities;
using Huntable.Business;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class LinkedInProfile : LinkedInBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - LinkedInProfile.aspx");
            try
            {
                var service = new LinkedInService(Authorization);

                var fields = new List<ProfileField>
                             {
                                 ProfileField.PersonId,
                                 ProfileField.FirstName,
                                 ProfileField.LastName,
                                 ProfileField.Headline,
                                 ProfileField.CurrentStatus,
                                 ProfileField.PositionId,
                                 ProfileField.PositionTitle,
                                 ProfileField.PositionSummary,
                                 ProfileField.PositionStartDate,
                                 ProfileField.PositionEndDate,
                                 ProfileField.PositionIsCurrent,
                                 ProfileField.PositionCompanyName,
                                 ProfileField.PictureUrl,
                                 ProfileField.ThreePastPositions,
                                 ProfileField.DateOfBirth,
                                 ProfileField.ThreeCurrentPositions,
                                 ProfileField.LocationName,
                                 ProfileField.LocationCountryCode,
                                 ProfileField.PhoneNumbers   ,
                                 ProfileField.EducationActivities,
                                 ProfileField.EducationDegree,
                                 ProfileField.EducationEndDate,
                                 ProfileField.EducationFieldOfStudy,
                                 ProfileField.EducationSchoolName,
                                 ProfileField.EducationStartDate,
                                 ProfileField.Specialties,
                                 ProfileField.MainAddress,
                                 ProfileField.Summary
                               
                                 
                             };

                DisplayProfile(service.GetCurrentUser(ProfileType.Standard, fields));
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load -LinkedInProfile.aspx");
        }

        private void DisplayProfile(Person person)
        {
            LoggingManager.Debug("Entering DisplayProfile -LinkedInProfile.aspx");
            try
            {
                int? userId = Common.GetLoggedInUserId(Session);
                if (userId.HasValue)
                {
                    UserManager.ImportProfileFromLinkedIn(userId.Value, person);
                    Response.Redirect("EditProfilePage.aspx");
                }
            }
            catch (Exception ex)
            {
              LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DisplayProfile -LinkedInProfile.aspx");
        }
    }
}
