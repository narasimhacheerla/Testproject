using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentConversion;
using Huntable.Data;
using Snovaspace.Util.Logging;
using Sovren;

namespace Huntable.Business
{
    public class PostCVManager
    {
        public int? GetSkillId(string skillText)
        {
            LoggingManager.Debug("Entering GetSkillId - PostCVManager");
            var skill = MasterDataManager.AllSkills.FirstOrDefault(s => s.Description.ToLower() == skillText.ToLower());
            if (skill == null)
            {
                return CreateSkill(skillText).Id;
            }
            LoggingManager.Debug("Exiting GetSkillId - PostCVManager");
            return skill.Id;
        }

        private MasterSkill CreateSkill(string skillText)
        {
            LoggingManager.Debug("Entering CreateSkill - PostCVManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var masterSkill = new MasterSkill { Description = skillText };
                context.AddToMasterSkills(masterSkill);

                MasterDataManager.AllSkills.Add(masterSkill);
                LoggingManager.Debug("Exiting CreateSkill - PostCVManager");
                return masterSkill;
            }
            
        }
        public  void CreateUserRecord(string fileName,string email,int referalid,int inv)
        {
            LoggingManager.Debug("Entering ImportResume - PostCVManager.aspx");
            using (var docConvert = new DocumentConverter())
            {
                string[] convertedDocument = docConvert.DoConversion(File.ReadAllBytes(fileName), DocumentConverter.OutputTypes.PlainText);
                hrxmlResume hrxml = ParseToHrXml(convertedDocument[1]);
                var name = hrxml.StructuredXMLResume.ContactInfo.PersonName;
                var firstname = name.FormattedName;
                if (hrxml.UserArea.PersonalInformation != null && hrxml.UserArea.PersonalInformation.DateOfBirth.Year > 1900 && hrxml.UserArea.PersonalInformation.DateOfBirth.Month > 0 && hrxml.UserArea.PersonalInformation.DateOfBirth.Day > 0)
                {
                    var dob = hrxml.UserArea.PersonalInformation.DateOfBirth;
                }
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var user = new User
                                   {

                                       EmailAddress = email,
                                       FirstName = firstname,
                                       LastName = name.FamilyName,
                                       Password = firstname,
                                       LastLoginTime = DateTime.Now,
                                       LastProfileUpdatedOn = DateTime.Now,
                                       CreatedDateTime = DateTime.Now,
                                       IsDeleted = true,
                                       RecuiteReferalId = referalid,
                                       InvitationId = inv,
                                       ReferralId = referalid
                                   };
                    context.Users.AddObject(user);
                    context.SaveChanges();
                   new PostCVManager().ImportResume(context,user.Id,fileName);
                }
               
            }
            LoggingManager.Debug("Exiting ImportResume - PostCVManager.aspx");

        }
        public  void ImportResume(huntableEntities context, int userId, string fileName)
        {
            LoggingManager.Debug("Entering ImportResume - PostCVManager.aspx");
            try
            {
                using (var docConvert = new DocumentConverter())
                {
                    User user = context.Users.First(u => u.Id == userId);

                    string[] convertedDocument = docConvert.DoConversion(File.ReadAllBytes(fileName), DocumentConverter.OutputTypes.PlainText);
                    hrxmlResume hrxml = ParseToHrXml(convertedDocument[1]);

                    var name= hrxml.StructuredXMLResume.ContactInfo.PersonName;
                    var personame = name.FormattedName;
                    if (hrxml.UserArea.PersonalInformation != null && hrxml.UserArea.PersonalInformation.DateOfBirth.Year > 1900 && hrxml.UserArea.PersonalInformation.DateOfBirth.Month > 0 && hrxml.UserArea.PersonalInformation.DateOfBirth.Day > 0)
                    {
                        user.DOB = hrxml.UserArea.PersonalInformation.DateOfBirth;
                    }
                    if (hrxml.StructuredXMLResume.ContactInfo.GetPhonesFieldedList() != null && hrxml.StructuredXMLResume.ContactInfo.GetPhonesFieldedList().Length > 0)
                    {
                        //user.PhoneNumber = hrxml.StructuredXMLResume.ContactInfo.GetPhonesFieldedList()[0];
                    }
                    //Update personal and contact details
                    UpdateContactMethodInformation(context, user, hrxml.StructuredXMLResume.ContactInfo);
                    user.Summary = string.IsNullOrEmpty(hrxml.StructuredXMLResume.ExecutiveSummary) ? hrxml.StructuredXMLResume.Objective : hrxml.StructuredXMLResume.ExecutiveSummary;

                    //Update education history
                    UpdateEducationHistory(context, user, hrxml.StructuredXMLResume.EducationHistory);

                    //Update employment history
                    UpdateEmploymentHistory(context, user, hrxml.StructuredXMLResume.EmploymentHistory);

                    if (hrxml.UserArea != null && hrxml.UserArea.Hobbies != null)
                    {
                        var hobbies = hrxml.UserArea.Hobbies.Text.Split(',').Where(h => !string.IsNullOrWhiteSpace(h) && !string.IsNullOrWhiteSpace(h.Trim())).ToArray();
                        if (hobbies.Length > 0)
                        {
                            // Adding the new hobbies to master tables.
                            MasterDataManager.AddNewInterests(hobbies);
                            foreach (var hobby in hobbies)
                            {
                                var masterInterestId = MasterDataManager.GetInterestId(hobby);
                                if (masterInterestId.HasValue)
                                {
                                    user.UserInterests.Add(new UserInterest { MasterInterestId = masterInterestId.Value });
                                }
                            }
                        }
                    }
                    if (hrxml.UserArea != null && hrxml.UserArea.ExperienceSummary != null && hrxml.UserArea.ExperienceSummary.YearsOfWorkExperience > 0)
                    {
                        user.TotalExperienceInYears = hrxml.UserArea.ExperienceSummary.YearsOfWorkExperience;
                    }
                    user.IsProfileUpdated = true;
                    context.SaveChanges();
                    LoggingManager.Debug("Exiting ImportResume - PostCVManager.aspx");
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
        }

        private static void UpdateEmploymentHistory(huntableEntities context, User user, hrxmlEmploymentHistoryType employmentHistory)
        {
            LoggingManager.Debug("Entering UpdateEmploymentHistory - PostCVManager.aspx");

            List<hrxmlEmployerOrgType> emps = employmentHistory.ToList();
            foreach (var employment in emps)
            {
                var employmentHistoryToCreate = new EmploymentHistory
                                                    {
                                                        CompanyId = MasterDataManager.GetMasterCompanyId(employment.EmployerOrgName)
                                                    };
                var isCurrentEmployer = employment.EmployerOrgName == employmentHistory.MostRecentEmployer;
                employmentHistoryToCreate.IsCurrent = isCurrentEmployer;
                if (user != null) employmentHistoryToCreate.UserId = user.Id;
                if (employment.PositionHistory != null && employment.PositionHistory.Count > 0)
                {
                    employmentHistoryToCreate.JobTitle = employment.PositionHistory[0].Title;
                    employmentHistoryToCreate.Description = employment.PositionHistory[0].Description;
                    if (employment.PositionHistory[0].StartDate.HasValue && employment.PositionHistory[0].StartDate.AsDateTime.Month > 0 && employment.PositionHistory[0].StartDate.AsDateTime.Year > 0)
                    {
                        employmentHistoryToCreate.FromMonthID = Common.GetMonthId(context, employment.PositionHistory[0].StartDate.AsDateTime.Month);
                        employmentHistoryToCreate.FromYearID = Common.GetYearId(context, employment.PositionHistory[0].StartDate.AsDateTime.Year);
                    }
                    if (employment.PositionHistory[0].EndDate.HasValue && employment.PositionHistory[0].EndDate.AsDateTime.Month > 0 && employment.PositionHistory[0].EndDate.AsDateTime.Year > 0)
                    {
                        employmentHistoryToCreate.ToMonthID = Common.GetMonthId(context, employment.PositionHistory[0].EndDate.AsDateTime.Month);
                        employmentHistoryToCreate.ToYearID = Common.GetYearId(context, employment.PositionHistory[0].EndDate.AsDateTime.Year);
                    }
                }
                context.EmploymentHistories.AddObject(employmentHistoryToCreate);
            }

            context.SaveChanges();
            LoggingManager.Debug("Exiting UpdateEmploymentHistory - PostCVManager.aspx");
        }

        private static void UpdateEducationHistory(huntableEntities context, User user, IEnumerable<hrxmlSchoolOrInstitutionType> educationHistory)
        {
            LoggingManager.Debug("Entering UpdateEducationHistory - PostCVManager.aspx");

            foreach (var edu in educationHistory)
            {
                var edu1History = new EducationHistory { UserId = user.Id };
                if (edu.Schools.Count > 0)
                    edu1History.Institution = edu.Schools[0].SchoolName;
                if (edu.schoolType == hrxmlGlobals.SchoolTypes.highschool || edu.schoolType == hrxmlGlobals.SchoolTypes.secondary)
                {
                    edu1History.IsSchool = true;
                }
                var degree = !string.IsNullOrEmpty(edu.MastersDegreeName) ? edu.MastersDegreeName : (!string.IsNullOrEmpty(edu.DoctoralDegreeName) ? edu.DoctoralDegreeName : edu.UndergraduateDegreeName);
                edu1History.Degree = degree;
                if (edu.DoctoralGradDate.Year > 1753 && edu.DoctoralGradDate.Month > 0)
                {
                    edu1History.ToMonthID = Common.GetMonthId(context, edu.DoctoralGradDate.Month);
                    edu1History.ToYearID = Common.GetYearId(context, edu.DoctoralGradDate.Year);

                    edu1History.FromMonthID = edu1History.ToMonthID;
                    edu1History.FromYearID = edu1History.ToYearID;
                }
                else if (edu.MastersGradDate.Year > 1753 && edu.MastersGradDate.Month > 0)
                {
                    edu1History.ToMonthID = Common.GetMonthId(context, edu.MastersGradDate.Month);
                    edu1History.ToYearID = Common.GetYearId(context, edu.MastersGradDate.Year);

                    edu1History.FromMonthID = edu1History.ToMonthID;
                    edu1History.FromYearID = edu1History.ToYearID;
                }
                else if (edu.UndergraduateGradDate.Year > 1753 && edu.UndergraduateGradDate.Month > 0)
                {
                    edu1History.ToMonthID = Common.GetMonthId(context, edu.UndergraduateGradDate.Month);
                    edu1History.ToYearID = Common.GetYearId(context, edu.UndergraduateGradDate.Year);

                    edu1History.FromMonthID = edu1History.ToMonthID;
                    edu1History.FromYearID = edu1History.ToYearID;
                }
                context.EducationHistories.AddObject(edu1History);
            }

            context.SaveChanges();
            LoggingManager.Debug("Exiting UpdateEducationHistory - PostCVManager.aspx");
        }

        private static void UpdateContactMethodInformation(huntableEntities context, User user, hrxmlContactInfo contactInfo)
        {
            LoggingManager.Debug("Entering UpdateContactMethodInformation - PostCVManager.aspx");

            var postalAddressContactMethod = contactInfo.ContactMethod.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.PostalAddress.PostalCode));

            if (postalAddressContactMethod != null)
            {
                user.ZipCode = postalAddressContactMethod.PostalAddress.PostalCode;
                user.State = postalAddressContactMethod.PostalAddress.Municipality;
                user.County = postalAddressContactMethod.PostalAddress.Municipality;
                user.City = postalAddressContactMethod.PostalAddress.Municipality;

                var countryCode = postalAddressContactMethod.PostalAddress.CountryCode;
                if (countryCode == "UK") countryCode = "United Kingdom";
                int countryId = (context.MasterCountries.Where(x => x.Description.ToUpper() == countryCode.ToUpper() || (!string.IsNullOrEmpty(x.Code) && x.Code.ToUpper() == countryCode.ToUpper())).Select(x => x.Id)).FirstOrDefault();
                if (countryId > 0)
                {
                    user.CountryID = countryId;
                }
            }

            var emailContactMethod = contactInfo.ContactMethod.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.InternetEmailAddress));
            if (emailContactMethod != null)
                user.SecondaryEmail = emailContactMethod.InternetEmailAddress;
            context.SaveChanges();
            LoggingManager.Debug("Exiting UpdateContactMethodInformation - PostCVManager.aspx");
        }

        private static hrxmlResume ParseToHrXml(string cvtext)
        {
            LoggingManager.Debug("Entering ParseToHrXml - PostCVManager.aspx");
            //actually does the parsing
            var settings = new ParserSettings
            {
                Coverage =
                {
                    DateOfBirth = true,
                    Location = true,
                    AddCertificationsAndLicensesToSkills = true,
                    AddLanguagesToSkills = true,
                    DrivingLicense = true,
                    Passport = true,
                    Salary = true,
                    Visa = true,
                    ContactInfo = true,
                    EducationHistory = true,
                    EmploymentHistory = true,
                    ExecutiveSummary = true,
                    Gender = true,
                    MaritalStatus = true,
                    PersonalInformation = true,
                    Skills = true

                },
                Culture = { UseUK = true }
            };

            //coverage

            //culture
            var p = new ParserMapper(settings);
            p.Parse(cvtext);
            LoggingManager.Debug("Exiting ParseToHrXml - PostCVManager.aspx");
            return p.Resume;
        }
    }
}
