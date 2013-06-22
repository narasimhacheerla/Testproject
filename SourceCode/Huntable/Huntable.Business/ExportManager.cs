using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.Business
{
    public class ExportManager
    {
        public string ExportUsers(DateTime fromDate, DateTime toDate)
        {
            LoggingManager.Debug("Entering ExportUsers  - ExportManager.cs");

            List<EmploymentHistory> employmentHistoryList;
            List<UserInterest> userIntetrestList;
            List<UserLanguage> userLanguageList;
            List<User> users;

            string headerRowTemp = "Id" + ",Name" + ",ProfilePicturePath" + ",DOB"
                                         + ",IsMarried"
                                         + ",HomeAddress"
                                         + ",PhoneNumber"
                                         + ",Nationality"
                                         + ",IsVerified"
                                         + ",IsCustomizingYourFeedsAccepted"
                                         + ",IsCustomizingYourJobsAccepted"
                                         + ",IsSearchingAndConnectingWithOtherUsersAccepted"
                                         + ",ProfileImportSourceId"
                                         + ",IsPremiumAccount"
                                         + ",ReferralId"
                                         + ",AffliateAmount"
                                         + ",LevelOneInvitedCount"
                                         + ",LevelOneJoinedCount"
                                         + ",LevelTwoInvitedCount"
                                         + ",LevelTwoJoinedCount"
                                         + ",LevelThreeInvitedCount"
                                         + ",LevelThreeJoinedCount"
                                         + ",TotalJobCreditsLeft"
                                         + ",LastActivityDate"
                                         + ",LastLoginTime"
                                         + ",LastProfileUpdatedOn"
                                         + ",EmployerLogoPath"
                                         + ",IsFeatured"
                                         + ",Note"
                                         + ",CreatedDateTime"
                                         + ",Interests"
                                         + ",Languages";

            getEmploerHistoryHeader(ref headerRowTemp);
            getEmploerHistoryHeader(ref headerRowTemp);
            getEmploerHistoryHeader(ref headerRowTemp);
            getEmploerHistoryHeader(ref headerRowTemp);

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                users = context.Users.Include("MasterNationality").Include("UserInterests").Include("UserLanguages").Include("EmploymentHistories").
                    Where(x => x.CreatedDateTime >= fromDate && x.CreatedDateTime <= toDate).ToList();

                userIntetrestList = context.UserInterests.Include("MasterInterest").ToList();
                userLanguageList = context.UserLanguages.Include("MasterLanguage").ToList();

                var sbHeaderRow = new StringBuilder();
                sbHeaderRow.Append(headerRowTemp);

                string headerRow = sbHeaderRow.ToString();
                var exportedContent = new StringBuilder();
                exportedContent.Append(headerRow + Environment.NewLine);

                foreach (var user in users)
                {
                    string newRecord = "\"" + Convert.ToString(user.Id) + "\"";
                    newRecord = ConvertToCsvString(newRecord, user.FirstName + " " + user.LastName);
                    newRecord = ConvertToCsvString(newRecord, user.UserProfilePictureDisplayUrl);
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.DOB));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.IsMarried));
                    newRecord = ConvertToCsvString(newRecord, user.HomeAddress);
                    newRecord = ConvertToCsvString(newRecord, user.PhoneNumber);
                    newRecord = ConvertToCsvString(newRecord, user.MasterNationality != null ? user.MasterNationality.Description : string.Empty);


                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.IsVerified));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.IsCustomizingYourFeedsAccepted));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.IsCustomizingYourJobsAccepted));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.IsSearchingAndConnectingWithOtherUsersAccepted));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.ProfileImportSourceId));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.IsPremiumAccount));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.ReferralId));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.AffliateAmountAsText));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.LevelOneInvitedCount));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.LevelOneJoinedCount));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.LevelTwoInvitedCount));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.LevelTwoJoinedCount));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.LevelThreeInvitedCount));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.LevelThreeJoinedCount));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.TotalJobCreditsLeft));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.LastActivityDate));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.LastLoginTime));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.LastProfileUpdatedOn));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.CompanyLogoPictureDisplayUrl));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.IsFeatured));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.Note));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(user.CreatedDateTime));


                    var sbUserInterest = new StringBuilder();
                    foreach (UserInterest ui in userIntetrestList)
                    {

                        if (ui.UserId == user.Id)
                        {
                            sbUserInterest.Append(ui.MasterInterest.Description);
                            sbUserInterest.Append("  ");
                        }
                    }
                    newRecord = ConvertToCsvString(newRecord, sbUserInterest.ToString());


                    var sbUserLanguage = new StringBuilder();
                    foreach (UserLanguage ui in userLanguageList)
                    {
                        if (ui.UserId == user.Id)
                        {
                            sbUserLanguage.Append(ui.MasterLanguage.Description);
                            sbUserLanguage.Append("  ");
                        }
                    }
                    newRecord = ConvertToCsvString(newRecord, sbUserLanguage.ToString());


                    var sbUserCVs = new StringBuilder();
                    var empCount = 1;
                    employmentHistoryList = user.EmploymentHistories.ToList();
                    foreach (var item in employmentHistoryList)
                    {
                        if (empCount > 4) break;

                        //sbUserCVs.Append(item.UserId);
                        sbUserCVs.Append(ConvertToCsvString(item.JobTitle));
                        sbUserCVs.Append(ConvertToCsvString(item.FromMonthID.ToString() + "/" + item.FromYearID.ToString()));
                        sbUserCVs.Append(ConvertToCsvString(item.ToMonthID.ToString() + "/" + item.ToYearID.ToString()));
                        sbUserCVs.Append(ConvertToCsvString(item.EmploymentPresent.ToString()));
                        sbUserCVs.Append(ConvertToCsvString(item.Town));
                        sbUserCVs.Append(ConvertToCsvString(item.CountryId.ToString()));
                        sbUserCVs.Append(ConvertToCsvString(item.ContractId.ToString()));
                        sbUserCVs.Append(ConvertToCsvString(item.HoursId.ToString()));
                        sbUserCVs.Append(ConvertToCsvString(item.PaymentModeId.ToString()));
                        sbUserCVs.Append(ConvertToCsvString(item.AmountCurrencyId.ToString()));
                        sbUserCVs.Append(ConvertToCsvString(item.Amount.ToString()));
                        sbUserCVs.Append(ConvertToCsvString(item.Description));
                        sbUserCVs.Append(ConvertToCsvString(item.IsCurrent.ToString()));
                        sbUserCVs.Append(ConvertToCsvString(item.IndustryId.ToString()));
                        empCount++;
                    }
                    newRecord += sbUserCVs.ToString();

                    newRecord = newRecord + Environment.NewLine;
                    exportedContent.Append(newRecord);
                }

                LoggingManager.Debug("Exiting ExportUsers  - ExportManager.cs");


                return exportedContent.ToString();
            }
        }



        private void getEmploerHistoryHeader(ref string headerRowTemp)
        {
            LoggingManager.Debug("Entering getEmploerHistoryHeader  - ExportManager.cs");

            headerRowTemp += ",Job title"
                + ",From"
                + ",To"
                + ",Employment"
                + ",Town"
                + ",Country"
                + ",Contract"
                + ",Hours"
                + ",Payment method"
                + ",Currency"
                + ",Amount"
                + ",Description"
                + ",IsCurrent"
                + ",Industry";
            LoggingManager.Debug("Exiting getEmploerHistoryHeader  - ExportManager.cs");

        }

        private string ConvertToCsvString(string newRecord, int? value)
        {
            return newRecord + "," + "\"" + value + "\"";
        }

        public string ExportJobs(DateTime? fromDate, DateTime? toDate)
        {
            LoggingManager.Debug("Exporting jobs From Date = " + fromDate.Value.ToString("dd - MMM - yyyy") + toDate.Value.ToString("dd - MMM - yyyy"));
            
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
               
                const string headerRow = "Id"
                                   + ",Title"
                                   + ",CompanyName"
                                   + ",Salary"
                                   + ",Country"
                                   + ",Location"
                                   + ",JobDescription"
                                   + ",MinExperience"
                                   + ",MaxExperience"
                                   + ",JobType"
                                   + ",Industry"

                                   + ",DesiredCandidateProfile"
                                   + ",AboutYourCompany"
                                   + ",IsCompanyLogo"
                                   + ",UserId"
                                   + ",PhotoPath1"
                                   + ",PhotoPath2"
                                   + ",PhotoPath3"
                                   + ",PhotoPath4"
                                   + ",PhotoPath5"
                                   + ",PhotoThumbPath1"
                                   + ",PhotoThumbPath2"
                                   + ",PhotoThumbPath3"
                                   + ",PhotoThumbPath4"
                                   + ",PhotoThumbPath5"
                                   + ",ReceiveApplicants"
                                   + ",ExternalSiteApplicant"
                                   + ",TotalViews"
                                   + ",CreatedDateTime"
                                   + ",Skill"
                                   + ",IsActive";

                File.WriteAllText(Common.GetApplicationBasePath() + "/exportedjobs.csv", headerRow + Environment.NewLine); 

                var listOfJobs = context.ListofJobs(fromDate, toDate).ToList();
                LoggingManager.Debug("Job count = " + listOfJobs.Count);

                foreach (var job in listOfJobs)
                {
                    string newRecord = "\"" + Convert.ToString(job.Id) + "\"";
                    newRecord = ConvertToCsvString(newRecord, job.Title);
                    newRecord = ConvertToCsvString(newRecord, job.CompanyName);
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(job.Salary));

                    newRecord = job.MasterCountry != null
                                    ? ConvertToCsvString(newRecord, job.MasterCountry.Description)
                                    : ConvertToCsvString(newRecord, string.Empty);

                    newRecord = ConvertToCsvString(newRecord, job.LocationName);

                    newRecord = ConvertToCsvString(newRecord, job.JobDescription);
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(job.MinExperience));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(job.MaxExperience));

                    newRecord = job.MasterJobType != null
                                    ? ConvertToCsvString(newRecord, job.MasterJobType.Description)
                                    : ConvertToCsvString(newRecord, string.Empty);

                    newRecord = job.MasterIndustry != null
                                    ? ConvertToCsvString(newRecord, job.MasterIndustry.Description)
                                    : ConvertToCsvString(newRecord, string.Empty);

                    newRecord = job.MasterSector != null
                                    ? ConvertToCsvString(newRecord, job.MasterSector.Description)
                                    : ConvertToCsvString(newRecord, string.Empty);

                    newRecord = ConvertToCsvString(newRecord, job.DesiredCandidateProfile);
                    newRecord = ConvertToCsvString(newRecord, job.AboutYourCompany);
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(job.IsCompanyLogo));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(job.UserId));
                    newRecord = ConvertToCsvString(newRecord, job.PhotoPath1);
                    newRecord = ConvertToCsvString(newRecord, job.PhotoPath2);
                    newRecord = ConvertToCsvString(newRecord, job.PhotoPath3);
                    newRecord = ConvertToCsvString(newRecord, job.PhotoPath4);
                    newRecord = ConvertToCsvString(newRecord, job.PhotoPath5);
                    newRecord = ConvertToCsvString(newRecord, job.PhotoThumbPath1);
                    newRecord = ConvertToCsvString(newRecord, job.PhotoThumbPath2);
                    newRecord = ConvertToCsvString(newRecord, job.PhotoThumbPath3);
                    newRecord = ConvertToCsvString(newRecord, job.PhotoThumbPath4);
                    newRecord = ConvertToCsvString(newRecord, job.PhotoThumbPath5);
                    newRecord = ConvertToCsvString(newRecord, job.ReceiveApplicants);
                    newRecord = ConvertToCsvString(newRecord, job.ExternalSiteApplicant);
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(job.TotalViews));
                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(job.CreatedDateTime));

                    newRecord = job.MasterSkill != null
                                    ? ConvertToCsvString(newRecord, job.MasterSkill.Description)
                                    : ConvertToCsvString(newRecord, string.Empty);

                    newRecord = ConvertToCsvString(newRecord, Convert.ToString(job.IsActive));

                    newRecord = newRecord + Environment.NewLine;

                    File.AppendAllText(Common.GetApplicationBasePath() + "/exportedjobs.csv", newRecord); 
                }
            }

            LoggingManager.Debug("Exiting ExportJobs  - ExportManager.cs");

            return File.ReadAllText(Common.GetApplicationBasePath() + "/exportedjobs.csv");
        }
        private string ConvertToCsvString(string newRecord, string str)
        {
            return newRecord + "," + "\"" + Convert.ToString(str) + "\"";
        }
        private string ConvertToCsvString(string str)
        {
            return "," + "\"" + Convert.ToString(str) + "\"";
        }
    }
}