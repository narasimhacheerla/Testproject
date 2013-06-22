using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Huntable.Data;
using Huntable.Data.Enums;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;

namespace Huntable.Business.BatchJobs
{
    public class JobAlert
    {

        public void Run(int jobid)
        {
            LoggingManager.Debug("Entering into JobAlert");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                foreach (
                    User user in
                        context.Users.Where(
                            x =>
                            x.IsVerified.HasValue && x.IsVerified.Value && !x.IsAdmin.HasValue &&
                            x.LastActivityDate.HasValue).OrderByDescending(x => x.LastActivityDate).ToList())
                {
                    try
                    {

                 
                    var job = context.Jobs.FirstOrDefault(x => x.Id == jobid);
                    var posteduser = context.Users.FirstOrDefault(x => x.Id == job.UserId);
                    var preferredJobUserIndustries =
                        context.PreferredJobUserIndustries.Where(x => x.UserId == user.Id).ToList();
                    User user1 = user;
                    var list = context.PreferredJobUserSalaries.ToList();
                    var salary = list.FirstOrDefault(x => x.UserId==user.Id);
                 
                        MasterMaximumSalary maxsalary;
                        MasterMinimumSalary minsalary;
                        var minsalarye = 0;
                        var maxsalarye = 0;
                        if (salary != null)
                        {
                            var minsalryid = salary.MinSalary;

                        minsalary = context.MasterMinimumSalaries.ToList().FirstOrDefault(
                            x => (salary.MinSalary != null && x.Id == minsalryid));
                        maxsalary = context.MasterMaximumSalaries.ToList().FirstOrDefault(
                            x => x.Id == salary.MaxSalary);
                            if (minsalary != null) minsalarye = (int) minsalary.MinimumSalary;
                            if (maxsalary != null) maxsalarye = (int) maxsalary.MaximumSalary;
                        }

                    var preferredJobUserkills = context.PreferredJobUserSkills.Where(x => x.UserId == user.Id).ToList();
                            var preferredJobUserCountries =
                                context.PreferredJobUserCountries.Where(x => x.UserId == user.Id).ToList();
                            var preferredJobUserJobTypes =
                                context.PreferredJobUserJobTypes.Where(x => x.UserId == user.Id).ToList();
                            var industriesFollowing =
                                preferredJobUserIndustries.Where(x => x.UserId == user.Id&&x.MasterIndustryId==job.MasterIndustry.Id)
                                                          .Select(x => x.MasterIndustry.Description)
                                                          .ToList();
                    
                            var jobSkills =
                                preferredJobUserkills.Where((x => x.UserId == user.Id&&x.MasterSkillId==job.MasterSkill.Id))
                                                     .Select(x => x.MasterSkill.Description)
                                                     .ToList();
                            var countriesFollowing = preferredJobUserCountries.Where(x => x.UserId == user.Id&&x.MasterCountryId==job.MasterCountry.Id).Select(x => x.MasterCountry.Description).ToList();
                            var jobTypesFollowing = preferredJobUserJobTypes.Where(x => x.UserId == user.Id&&x.MasterJobTypeId==job.MasterJobType.Id).Select(x => x.MasterJobType.Description).ToList();
                            var industries = string.Join<string>(",", industriesFollowing);
                            var skills = string.Join<string>(",", jobSkills);
                            var countries = string.Join<string>(",", countriesFollowing);
                            var jobtypes = string.Join<string>(",", jobTypesFollowing);

                            if (industriesFollowing.Count > 0 && jobSkills.Count > 0 && countriesFollowing.Count > 0 &&
                                jobTypesFollowing.Count > 0&&minsalarye>0&&maxsalarye>0)
                            {
                                var template = EmailTemplateManager.GetTemplate(EmailTemplates.JobAlert);


                       
                                var valuesList = new Hashtable
                                    {
                                        {"Name", user.Name},
                                        {"JobTitle", job.Title},
                                        {"Jobtype", job.MasterJobType.Description},
                                        {"Experience", job.MinExperience},
                                        {"Industry", job.MasterIndustry.Description},
                                        {"Salary", job.Salary},
                                        {"Skill", job.MasterSkill.Description},
                                        {"Created", job.CreatedDateTime},
                                        {"JobDescription", job.JobDescription},
                                        {"UserMinSalary",minsalarye},
                                        {"UserMaxSalary",maxsalarye},
                                        {"UserIndustry",industries},
                                        {"UserSkill", skills},
                                        {"UserJobType",  jobtypes},
                                        {"UserCountry", countries},
                                        {"Currency",salary.MasterCurrencyType.Symbol}
                                                 
                                    };
                                string baseUrl = "https://huntable.co.uk/";
                                string userProfilePath = Path.Combine(baseUrl, "ViewUserProfile.aspx?UserId=" + posteduser.Id);
                                valuesList.Add("Actual User Profile Link", userProfilePath);
                                string customizedurl = Path.Combine(baseUrl, "CustomizeJobsUsers.aspx");
                                valuesList.Add("Customized Url",customizedurl);
                                valuesList.Add("Base Url",baseUrl);
                                string applyjob = Path.Combine(baseUrl, new UrlGenerator().JobsUrlGenerator(job.Id));
                                valuesList.Add("Job Url",applyjob);
                                string userProfilePicturePath = Path.Combine(baseUrl, posteduser.UserProfilePictureDisplayUrl.Replace("~/", string.Empty));
                                valuesList.Add("User Profile Picture", userProfilePicturePath);
                                string body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
                                SnovaUtil.SendEmail(template.Subject, body, user.EmailAddress);
                            }
                    }
                    catch (Exception exception)
                    {
                        LoggingManager.Debug("Excetion Caught in JobAlert"+exception);
                        throw;
                    }
                        }
                    }
                  
                    }

                }
            }
        
    

