using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Huntable.Business.BatchJobs;
using Huntable.Data;
using Huntable.Data.Enums;
using Huntable.Entities;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;

namespace Huntable.Business
{
    public class JobsManager
    {
        public List<User> GetAllUsers(DateTime fromDate, DateTime toDate)
        {
            LoggingManager.Debug("Entering GetAllUsers - JobsManager");
            var dataContext = huntableEntities.GetEntitiesWithNoLock();

            List<User> allUsers = dataContext.Users.Include("MasterNationality").Include("UserInterests").Include("UserLanguages").Where(x => x.CreatedDateTime >= fromDate && x.CreatedDateTime <= toDate).ToList();
            LoggingManager.Debug("Exiting GetAllUsers - JobsManager");
            return allUsers;
        }

        public IList<User> JobApplicants(int jobId)
        {
            LoggingManager.Debug("Entering JobApplicants - JobsManager");
            List<User> jobApplicts;
            List<User> jobapplicants;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                jobApplicts = (from u in context.Users
                               join j in context.Jobs on u.Id equals j.UserId
                               join ja in context.JobApplications on j.Id equals ja.JobId
                               where u.Id == jobId
                               select u).ToList();
                jobapplicants = (
                                 from j in context.Jobs
                                 join ja in context.JobApplications on j.Id equals ja.JobId
                                 join u in context.Users on ja.UserId equals u.Id
                                 where ja.JobId == jobId
                                 select u).ToList();

                jobapplicants.ToList().ForEach(u =>
                {
                    u.CurrentPosition = u.EmploymentHistories.Where(e => e.IsCurrent && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.JobTitle).FirstOrDefault();
                    u.CurrentCompany = u.EmploymentHistories.Where(e => e.IsCurrent && e.MasterCompany != null).Select(e => e.MasterCompany.Description).FirstOrDefault();
                    u.PastPositions = string.Join("<br/>", u.EmploymentHistories.Where(e => !e.IsCurrent && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.JobTitle));
                    u.PastPosition = u.EmploymentHistories.Where(e => (e.IsCurrent == false) && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.JobTitle).FirstOrDefault();
                    u.PastCompany = u.EmploymentHistories.Where(e => (e.IsCurrent == false) && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.MasterCompany.Description).FirstOrDefault();
                    u.ProfileVisitedCount = u.UserProfileVisitedHistories.Count();
                });

            }
            LoggingManager.Debug("Exiting JobApplicants - JobsManager");
            return jobapplicants;
        }
        public List<User> MyApplicants(int userId)
        {
            LoggingManager.Debug("Entering MyApplicants - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                List<User> myapplicants = (from j in context.Jobs
                                           join ja in context.JobApplications on j.Id equals ja.JobId
                                           join u in context.Users on ja.UserId equals u.Id
                                           where j.UserId == userId && ja.IsDeleted == null
                                           select new { u, ja }).ToList().OrderByDescending(x => x.ja.AppliedDateTime).Select(x => x.u).Distinct().ToList();
                myapplicants.ToList().ForEach(u =>
               {
                   u.CurrentPosition = u.EmploymentHistories.Where(e => e.IsCurrent && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.JobTitle).FirstOrDefault();
                   u.CurrentCompany = u.EmploymentHistories.Where(e => e.IsCurrent && e.MasterCompany != null).Select(e => e.MasterCompany.Description).FirstOrDefault();
                   u.PastPositions = string.Join("<br/>", u.EmploymentHistories.Where(e => !e.IsCurrent && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.JobTitle));
                   u.PastPosition = u.EmploymentHistories.Where(e => (e.IsCurrent == false) && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.JobTitle).FirstOrDefault();
                   u.PastCompany = u.EmploymentHistories.Where(e => (e.IsCurrent == false) && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.MasterCompany.Description).FirstOrDefault();
                   u.ProfileVisitedCount = u.UserProfileVisitedHistories.Count();
               });
                LoggingManager.Debug("Exiting MyApplicants - JobsManager");
                return myapplicants;
            }
        }
        public List<Job> GetAllJobs(DateTime fromDate, DateTime toDate)
        {
            LoggingManager.Debug("Entering GetAllJobs - JobsManager");
            List<Job> allJobs;
            toDate = toDate.AddDays(1).AddSeconds(-1);

            using (var dataContext = huntableEntities.GetEntitiesWithNoLock())
            {
                allJobs = dataContext.Jobs.Include("MasterJobType").Include("MasterCountry").
                   Include("MasterIndustry").Include("MasterSector").Include("MasterSkill").
                   Where(x => x.CreatedDateTime >= fromDate && x.CreatedDateTime <= toDate).ToList();

            }
            LoggingManager.Debug("Exiting GetAllJobs - JobsManager");
            return allJobs.ToList();
        }

        public List<News> GetNews()
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.News.OrderByDescending(news => news.Id).Take(8).ToList();
            }
        }

        public List<MasterSalary> GetSalaryByJobtype(int jobType)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.MasterSalaries.Where(i => i.JobTypeId == jobType).ToList();
            }
        }

        public List<Job> GetJobByUser(int userId)
        {

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.Jobs.Include("MasterJobType")
                    .Include("User")
                    .Include("JobApplications")
                    .Include("MasterIndustry")
                    .Where(j => j.UserId == userId).ToList();
            }

        }

        public SeoInfo GetMeta(string pageName)
        {
            return MasterDataManager.SeoInfos.FirstOrDefault(s => s.PageName == pageName);
        }

        public int GetUserJobs(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.Jobs.Count(j => j.UserId == userId && j.IsDeleted == null);
            }
        }

        public int GetUserJobApplications(int userId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.JobApplications.Count(j => j.UserId == userId && j.IsDeleted == null);
            }
        }

        public void SaveJobApplication(JobApplication details,int userId, int jId)
        {
            LoggingManager.Debug("Entering SaveJobApplication - JobsManager");
         

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                context.JobApplications.AddObject(details);
                context.SaveChanges();
                var job = context.Jobs.First(j => j.Id == jId);
              
                    var userEmailNotificationData = Common.GetuserEmailPref(job.User.Id);
                    if (userEmailNotificationData == null || userEmailNotificationData.WhenUserAppliesForAJob)
                    {
                        var appliedUser = context.Users.First(u => u.Id == userId);
                        var emailTemplate = EmailTemplateManager.GetTemplate(EmailTemplates.JobApplicationNotification);
                        var valuesList = new Hashtable
                                             {
                                                 {"Applied User Name", appliedUser.Name},
                                                 {"Applied User Role", appliedUser.CurrentPosition},
                                                 {"Applied User Company", appliedUser.CurrentCompany},
                                                 {"Applied User Location", appliedUser.LocationToDisplay},
                                                 {"Applied User Summary", appliedUser.Summary.Replace("\n" ,"<br/>")},
                                                  {"Applied User Comment", details.UserComments.Replace("\n" ,"<br/>")}
                                             };
                        string baseUrl = Common.GetApplicationBaseUrl();
                        string profilePicturePath = Path.Combine(baseUrl, appliedUser.UserProfilePictureDisplayUrl.Replace("~/", string.Empty));
                        //string profilePicturePath = string.Format("{0}{1}", baseUrl, appliedUser.);
                        valuesList.Add("ProfilePicturePath", profilePicturePath);
                        valuesList.Add("Server Url", baseUrl);
                        //valuesList.Add("Applied User Comment", "Need to update");
                        valuesList.Add("Job Name", job.Title);
                        var url = new UrlGenerator().UserUrlGenerator(appliedUser.Id);
                        valuesList.Add("Job Reference Number", (object) job.ReferenceNumber ?? job.Id);
                        string appliedUserProfilePath = Path.Combine(baseUrl, url);
                        valuesList.Add("Applied User Profile Link", appliedUserProfilePath);
                        valuesList.Add("Dont Want To Receive Email", Path.Combine(baseUrl, "UserEmailNotification.aspx"));
                        var body = SnovaUtil.LoadTemplate(emailTemplate.TemplateText, valuesList);
                        var emails = new[] { job.User.EmailAddress, job.ReceiveApplicants };

                        SnovaUtil.SendEmail(emailTemplate.Subject, body, emails);                       
                    }
                }
            
            LoggingManager.Debug("Exiting SaveJobApplication - JobsManager");
        }
    
        public void SaveJobCreditsPurchased(JobCreditsPurchased creditsPurchased)
        {
            LoggingManager.Debug("Entering SaveJobCreditsPurchased - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
               
                User user = context.Users.First(u => u.Id == creditsPurchased.UserId);
                if (user != null)
                {
                    user.CreditsLeft = user.CreditsLeft == null ? 0 + creditsPurchased.NoOfCredits : user.CreditsLeft + creditsPurchased.NoOfCredits;
                    context.SaveChanges();
                }
            }
            LoggingManager.Debug("Exiting SaveJobCreditsPurchased - JobsManager");
        }

        public IList<CustomMasterJobPackage> AllJobPackages()
        {
            LoggingManager.Debug("Entering AllJobPackages - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var res =
                    context.MasterJobPackages.Select(
                        j => new { ID = j.Id, Jobs = j.NoOfJobs, Price = Math.Round(j.Price, 2) });
                IList<CustomMasterJobPackage> list = (from x in res.AsEnumerable()
                                                      select new CustomMasterJobPackage
                                                                 {
                                                                     ID = x.ID,
                                                                     Jobs = x.Jobs,
                                                                     Price = x.Price,
                                                                     DisplayText =
                                                                        x.Jobs.ToString() + " - job Pack " + "\t" + "-$" +
                                                                         Math.Round(x.Price, 2).ToString(),
                                                                     DisplayValue =
                                                                         x.Jobs.ToString() + "," +
                                                                         Math.Round(x.Price, 2).ToString()
                                                                 }).ToList();
                LoggingManager.Debug("Exiting AllJobPackages - JobsManager");
                return list;
            }
        }

        public IEnumerable<Job> GetPasiveJobs()
        {
            LoggingManager.Debug("Entering GetPasiveJobs - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //var allPassiveJobs = (from job in context.Jobs.AsEnumerable()
                //                      join app in context.JobApplications.AsEnumerable() on job.Id equals app.JobId into
                //                          temp
                //                      join usr in context.Users.AsEnumerable() on job.UserId equals usr.Id
                //                      where (job.IsActive == null || job.CreatedDateTime == null || job.CreatedDateTime < System.DateTime.Now.AddDays(-30))
                //                      from i in temp.DefaultIfEmpty()
                //                      select new
                //                                 {
                //                                     job,
                //                                     country = usr.CountryName,
                //                                     JobDisplayImageFileStoreId =
                //                          job.IsCompanyLogo ? usr.CompanyLogoFileStoreId : usr.PersonalLogoFileStoreId
                //                                 }).ToList().Distinct();
                var date = System.DateTime.Now.AddDays(-30);
                    var alljobs = context.Jobs.Where(x => x.CreatedDateTime <date).OrderByDescending(x=>x.CreatedDateTime).ToList();

            
                return alljobs;
               
            }
        }

        public IList<Job> AppliedJobs(int userId)
        {
            LoggingManager.Debug("Entering AppliedJobs - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var appliedJobs = (from j in context.Jobs
                                   join ja in context.JobApplications on j.Id equals ja.JobId
                                   where ja.UserId == userId && ja.IsDeleted == null
                                   orderby ja.AppliedDateTime descending
                                   select new { j, ja }).ToList();

                foreach (var appliedJob in appliedJobs)
                {
                    appliedJob.j.ProfileImagePath = new FileStoreService().GetDownloadUrl(appliedJob.j.IsCompanyLogo ? appliedJob.j.User.CompanyLogoFileStoreId : appliedJob.j.User.PersonalLogoFileStoreId);
                    appliedJob.j.AppliedDate = appliedJob.ja.AppliedDateTime;
                }
                LoggingManager.Debug("Exiting AppliedJobs - JobsManager");
                return appliedJobs.Select(j1 => j1.j).ToList();
            }
        }

        public IList<Job> GetUserEmployment(int userId, int id)
        {
            LoggingManager.Debug("Entering GetUserEmployment - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result =
                    context.EmploymentHistories.FirstOrDefault(x => x.UserId == userId);
                var industry = ((result != null) ? result.IndustryId : 3);
                var d1 = DateTime.Now.AddDays(-30);
                var country = ((result != null) ? result.CountryId : 5);
                var jobs = context.Jobs.Where(x => x.IndustryId == industry && x.CountryId == country && x.CreatedDateTime > d1).Take(3).OrderByDescending(x => x.CreatedDateTime).Take(id).Distinct().ToList();
                foreach (Job job in jobs)
                {
                    job.ProfileImagePath = new FileStoreService().GetDownloadUrl(job.IsCompanyLogo ? job.User.CompanyLogoFileStoreId : job.User.PersonalLogoFileStoreId);

                }
                LoggingManager.Debug("Exiting GetUserEmployment - JobsManager");
                return jobs;

            }
        }
        public IList<Job> PostedJobs(int userId)
        {
            LoggingManager.Debug("Entering PostedJobs - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                List<Job> jobs = (from j in context.Jobs
                                  where j.UserId == userId && (j.IsDeleted == false || j.IsDeleted == null)
                                  orderby j.CreatedDateTime descending
                                  select j).ToList();

                foreach (Job job in jobs)
                {
                    job.ProfileImagePath = new FileStoreService().GetDownloadUrl(job.IsCompanyLogo ? job.User.CompanyLogoFileStoreId : job.User.PersonalLogoFileStoreId);
                    job.TotalApplicants = context.JobApplications.Distinct().Count(x => x.JobId == job.Id);

                }
                LoggingManager.Debug("Exiting PostedJobs - JobsManager");
                return jobs.ToList();
            }
        }

        public int SaveJob(Job jobOuter)
        {
            LoggingManager.Debug("Entering SaveJob - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                Job job;
                if (jobOuter.Id > 0)
                {
                    job = context.Jobs.First(j => j.Id == jobOuter.Id);
                }
                else
                {
                    job = new Job { CreatedDateTime = DateTime.Now };
                    context.AddToJobs(job);
                }
                job.Title = jobOuter.Title;
                job.CompanyName = jobOuter.CompanyName;
                job.Salary = jobOuter.Salary;
                job.CountryId = jobOuter.CountryId;
                job.LocationName = jobOuter.LocationName;
                job.JobDescription = jobOuter.JobDescription;
                job.MinExperience = jobOuter.MinExperience;
                job.MaxExperience = jobOuter.MaxExperience;
                job.JobTypeId = jobOuter.JobTypeId;
                job.IndustryId = jobOuter.IndustryId;
                job.SectorId = 1;
                job.SkillId = jobOuter.SkillId;
                job.DesiredCandidateProfile = jobOuter.DesiredCandidateProfile;
                job.AboutYourCompany = jobOuter.AboutYourCompany;
                job.UserId = jobOuter.UserId;
                job.IsCompanyLogo = false;
                job.ReceiveApplicants = jobOuter.ReceiveApplicants;
                job.ExternalSiteApplicant = jobOuter.ExternalSiteApplicant;
                job.IsActive = true;
                job.SalaryCurrencyId = jobOuter.SalaryCurrencyId;
                job.ReferenceNumber = jobOuter.ReferenceNumber;
                context.SaveChanges();
                int jobId = job.Id;
                if (!(jobOuter.Id > 0))
                {
                    User user = context.Users.First(u => u.Id == job.UserId);
                    if (user != null)
                    {
                        user.CreditsLeft = user.CreditsLeft - 1;
                        context.SaveChanges();
                    }
                    var userFeedManager = new UserFeedManager();
                    //userFeedManager.SaveUserFeed(job.UserId, "Job", job.Title);
                }
                new Snovaspace.Util.Utility().RunAsTask(() => new JobAlert().Run(job.Id));
                FeedManager.addFeedNotification(FeedManager.FeedType.Job_Post, jobOuter.UserId, job.Id, null);
                 var socialManager = new SocialShareManager();
                var joburl = new UrlGenerator().JobsUrlGenerator(job.Id);
                 var msg = "[UserName]" + " " + "posted new job - " + job.Title + " -" + " " + "https://huntable.co.uk/"+joburl;
                        socialManager.ShareOnFacebook(job.UserId, msg, "");
                LoggingManager.Debug("Exiting SaveJob - JobsManager");
                return jobId;
            }

        }

        public int SaveUserEmailNotification(UserEmailPreference detials)
        {
            LoggingManager.Debug("Entering SaveUserEmailNotification - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                context.UserEmailPreferences.AddObject(detials);
                LoggingManager.Debug("Exiting SaveUserEmailNotification - JobsManager");
                return context.SaveChanges();
            }
        }


        public void DeleteUserEmailNotification(int userId)
        {
            LoggingManager.Debug("Entering DeleteUserEmailNotification - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                UserEmailPreference usrPrev = context.UserEmailPreferences.First(j => j.UserId == userId);
                context.UserEmailPreferences.DeleteObject(usrPrev);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting DeleteUserEmailNotification - JobsManager");
        }
        public List<Invoice> GetUserTranscationDetails(int userId)
        {
            using (var context = new huntableEntities())
            {
                return context.Invoices.Where(u => u.UserId == userId).OrderBy(x => x.WithdrawnDateTime).ToList();
            }
        }
        public List<UserTransaction> GetUserTransactions(int usrId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.UserTransactions.Where(c => c.UserId == usrId).ToList();
            }
        }

        public void SaveUserInvoice(UserInvoice saveUserInvoice)
        {
            LoggingManager.Debug("Entering SaveUserInvoice - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                context.UserInvoices.AddObject(saveUserInvoice);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting SaveUserInvoice - JobsManager");
        }
        public void SaveTranscation(Invoice saveUserInvoice)
        {
            LoggingManager.Debug("Entering SaveTranscation - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                context.Invoices.AddObject(saveUserInvoice);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting SaveTranscation - JobsManager");
        }

        public void DeletePostedJob(int jobId, int userId)
        {
            LoggingManager.Debug("Entering DeletePostedJob - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                Job job = context.Jobs.FirstOrDefault(j => j.Id == jobId && j.UserId == userId);
                if (job != null) job.IsDeleted = true;
                context.SaveChanges();
                FeedManager.deleteFeedNotitifation(Huntable.Business.FeedManager.FeedType.Job_Post, userId, jobId);
            }
            LoggingManager.Debug("Exiting DeletePostedJob - JobsManager");
        }

        public void DeleteAppliedJob(int jobId, int userId)
        {
            LoggingManager.Debug("Entering DeleteAppliedJob - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                List<Job> jobs = (from j in context.Jobs
                                  where j.UserId == userId && (j.IsDeleted == false || j.IsDeleted == null)
                                  orderby j.CreatedDateTime descending
                                  select j).ToList();
                JobApplication job = context.JobApplications.FirstOrDefault(j => j.JobId == jobId && j.UserId == userId);
                if (job != null)
                    job.IsDeleted = true;

                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting DeleteAppliedJob - JobsManager");
        }

        public void DeleteJobApplicants(int otheruserid, int userId)
        {
            LoggingManager.Debug("Entering DeleteAppliedJob - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                List<Job> jobs = (from j in context.Jobs
                                  where j.UserId == userId && (j.IsDeleted == false || j.IsDeleted == null)
                                  orderby j.CreatedDateTime descending
                                  select j).ToList();
                foreach (var job1 in jobs)
                {
                    JobApplication job = context.JobApplications.FirstOrDefault(j => j.JobId == job1.Id && j.UserId == otheruserid);
                    if (job != null)
                        job.IsDeleted = true;
                    context.SaveChanges();
                }


            }
        }
        public List<Job> GetJobBySearchCriteria(string keyword, out int totalRecordsCount, int? recordsCount = null, int? pageIndex = null)
        {
            LoggingManager.Debug("Entering GetJobBySearchCriteria - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                try
                {
                    var leftJoin = (from job in context.Jobs
                                    join usr in context.Users on job.UserId equals usr.Id
                                    join skl in context.MasterSkills on job.SkillId equals skl.Id
                                    join jtype in context.MasterJobTypes on job.JobTypeId equals jtype.Id
                                    join jind in context.MasterIndustries on job.IndustryId equals jind.Id
                                    join jcntry in context.MasterCountries on job.CountryId equals jcntry.Id
                                    orderby job.CreatedDateTime
                                    where (job.IsActive != null && job.CreatedDateTime != null && job.CreatedDateTime > DateTime.Now.AddDays(-90))
                                    select new
                                    {
                                        OriginalJob = job,
                                        job.Id,
                                        JobTitle = job.Title,
                                        job.Salary,
                                        JobDesc = job.JobDescription,
                                        job.DesiredCandidateProfile,
                                        job.CountryId,
                                        Country = jcntry.Description,
                                        job.SkillId,
                                        Skill = skl.Description,
                                        job.IndustryId,
                                        Industry = jind.Description,
                                        job.MinExperience,
                                        job.MaxExperience,
                                        job.JobTypeId,
                                        JobType = jtype.Description,
                                        job.CompanyName,
                                        Location = job.LocationName,
                                        JobDisplayImageFileStoreId = job.IsCompanyLogo ? usr.CompanyLogoFileStoreId : usr.PersonalLogoFileStoreId
                                    });

                    //if (!string.IsNullOrEmpty(jobtitle))
                    //{
                    //    leftJoin = leftJoin.Where(x => x.JobTitle.ToLower().Contains(jobtitle.ToUpper()));
                    //}
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        leftJoin = leftJoin.Where(x => ((x.JobDesc.ToUpper().Contains(keyword.ToUpper())))
                            || (x.DesiredCandidateProfile.ToUpper().Contains(keyword.ToUpper()))
                            || (x.JobTitle.ToUpper().Contains(keyword.ToUpper()))
                            || (x.Location.ToUpper().Contains(keyword.ToUpper()))
                            || (x.Skill.ToUpper().Contains(keyword.ToUpper()))
                            || (x.Country.ToUpper().Contains(keyword.ToUpper()))
                            );
                    }
                    totalRecordsCount = leftJoin.Count();
                    //if (!string.IsNullOrEmpty(location))
                    //{
                    //    leftJoin = leftJoin.Where(x => x.location.ToUpper().Contains(location.ToUpper()));
                    //}

                    //if (!string.IsNullOrEmpty(company))
                    //{
                    //    leftJoin = leftJoin.Where(x => x.CompanyName.ToUpper().Contains(company.ToUpper()));
                    //}

                    //if (!string.IsNullOrEmpty(country))
                    //{
                    //    leftJoin = leftJoin.Where(x => x.Country.ToLower().Contains(country.ToLower()));
                    //}

                    //if (!string.IsNullOrEmpty(skill))
                    //{
                    //    leftJoin = leftJoin.Where(x => x.Skill.ToLower().Contains(skill.ToLower()));
                    //}

                    //if (!string.IsNullOrEmpty(industry))
                    //{
                    //    leftJoin = leftJoin.Where(x => x.Industry.ToLower().Contains(industry.ToLower()));
                    //}

                    //if (!string.IsNullOrEmpty(jobtype))
                    //{
                    //    leftJoin = leftJoin.Where(x => x.JobType.ToLower().Contains(jobtype.ToLower()));
                    //}

                    // Taking the page count records only.
                    if (recordsCount.HasValue && pageIndex.HasValue)
                    {
                        leftJoin = leftJoin.OrderBy(u => u.Id).Skip(recordsCount.Value * (pageIndex.Value - 1)).Take(recordsCount.Value);
                    }

                    var jobSearchResults = leftJoin.ToList().Select(jobSearchResult =>
                                                               new
                                                               {
                                                                   jobSearchResult.OriginalJob,
                                                                   jobSearchResult,
                                                                   jobSearchResult.JobDisplayImageFileStoreId,
                                                                   //AlreadyApplied = IsUserAlreadyToThisJob(context, jobSearchResult.Id) TODO
                                                               }).ToList();

                    foreach (var jobSearchResult in jobSearchResults)
                    {
                        jobSearchResult.OriginalJob.ProfileImagePath = new FileStoreService().GetDownloadUrl(jobSearchResult.JobDisplayImageFileStoreId);
                    }
                    totalRecordsCount = jobSearchResults.Count();
                    LoggingManager.Debug("Exiting GetJobBySearchCriteria - JobsManager");
                    return jobSearchResults.Select(x => x.OriginalJob).OrderByDescending(j => j.CreatedDateTime).ToList();
                }
                catch (Exception exception)
                {
                    LoggingManager.Error(exception);
                    totalRecordsCount = 0;

                    return null;
                }
            }

        }

        public List<Job> GetJobBySearchCriteria(string jobtitle, string keyword, Int16 country, Int16 experience, Int16 jobtype, Int16 industry, string location, Int16 salary, Int16 skill, string company, string skillText, out int totalRecordsCount, int? recordsCount = null, int? pageIndex = null)
        {
            LoggingManager.Debug("Entering GetJobBySearchCriteria - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                try
                {
                    var date = DateTime.Now.AddDays(-30);
                    var leftJoin = (from job in context.Jobs
                                    join usr in context.Users on job.UserId equals usr.Id
                                    orderby job.CreatedDateTime
                                    orderby job.CreatedDateTime descending
                                    where job.CreatedDateTime >= date && job.IsDeleted == null
                                    select new
                                               {
                                                   OriginalJob = job,
                                                   job.Id,
                                                   JobTitle = job.Title,
                                                   job.Salary,
                                                   JobDesc = job.JobDescription,
                                                   job.DesiredCandidateProfile,
                                                   job.CountryId,
                                                   job.SkillId,
                                                   SkillText = job.MasterSkill.Description,
                                                   job.IndustryId,
                                                   job.MinExperience,
                                                   job.MaxExperience,
                                                   job.JobTypeId,
                                                   job.CompanyName,
                                                   location = job.LocationName,
                                                   job.CreatedDateTime,
                                                   JobDisplayImageFileStoreId = job.IsCompanyLogo ? usr.CompanyLogoFileStoreId : usr.PersonalLogoFileStoreId
                                               }
                                      );


                    if (!string.IsNullOrEmpty(jobtitle))
                    {
                        leftJoin = leftJoin.Where(x => x.JobTitle.ToUpper().Contains(jobtitle.ToUpper()));
                    }
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        leftJoin = leftJoin.Where(x => ((x.JobDesc.ToUpper().Contains(keyword.ToUpper()))) || (x.DesiredCandidateProfile.ToUpper().Contains(keyword.ToUpper()) || (x.JobTitle.ToUpper().Contains(keyword.ToUpper()))));
                    }

                    if (!string.IsNullOrEmpty(location))
                    {
                        leftJoin = leftJoin.Where(x => x.location.ToUpper() == location.ToUpper());
                    }

                    if (!string.IsNullOrEmpty(company))
                    {
                        leftJoin = leftJoin.Where(x => x.CompanyName.ToUpper() == company.ToUpper());
                    }

                    if (country != 0)
                    {
                        leftJoin = leftJoin.Where(x => (x.CountryId == country));
                    }

                    if (skill != 0)
                    {
                        leftJoin = leftJoin.Where(x => x.SkillId == skill);
                    }

                    if (!string.IsNullOrEmpty(skillText))
                    {
                        leftJoin = leftJoin.Where(s => s.SkillText == skillText);
                    }

                    if (industry != 0)
                    {
                        leftJoin = leftJoin.Where(x => x.IndustryId == industry);
                    }

                    if (salary != 0)
                    {
                        leftJoin = leftJoin.Where(x => x.Salary >= salary - 5000 && x.Salary >= salary + 5000);
                    }

                    if (jobtype != 0)
                    {
                        leftJoin = leftJoin.Where(x => x.JobTypeId == jobtype);
                    }
                    if (experience != 0)
                    {
                        leftJoin = leftJoin.Where(x => ((x.MaxExperience < experience) && (x.MinExperience > experience) || ((x.MaxExperience == experience) || (x.MinExperience == experience))));
                    }
                    totalRecordsCount = leftJoin.Count();
                    // Taking the page count records only.
                    if (recordsCount.HasValue && pageIndex.HasValue)
                    {
                        leftJoin = leftJoin.OrderByDescending(u => u.CreatedDateTime).Skip(recordsCount.Value * (pageIndex.Value - 1)).Take(recordsCount.Value);
                    }

                    var jobSearchResults = leftJoin.ToList().Select(jobSearchResult =>
                                                               new
                                                               {
                                                                   jobSearchResult.OriginalJob,
                                                                   jobSearchResult,
                                                                   jobSearchResult.JobDisplayImageFileStoreId,
                                                                   //AlreadyApplied = IsUserAlreadyToThisJob(context, jobSearchResult.Id) TODO
                                                               }).ToList().Distinct();

                    foreach (var jobSearchResult in jobSearchResults)
                    {
                        jobSearchResult.OriginalJob.ProfileImagePath = new FileStoreService().GetDownloadUrl(jobSearchResult.JobDisplayImageFileStoreId);
                    }
                    LoggingManager.Debug("Exiting GetJobBySearchCriteria - JobsManager");
                    return jobSearchResults.Select(x => x.OriginalJob).ToList();
                }
                catch (Exception exception)
                {
                    LoggingManager.Error(exception);
                    totalRecordsCount = 0;
                    return null;
                }
            }
        }

        public bool IsUserAlreadyToThisJob(huntableEntities context, int jobId)
        {
            LoggingManager.Debug("Entering IsUserAlreadyToThisJob - JobsManager");
            int? userId = Common.GetLoggedInUserId();
            LoggingManager.Debug("Exiting IsUserAlreadyToThisJob - JobsManager");
            return !context.JobApplications.Any(x => x.JobId == jobId && userId.HasValue && x.UserId == userId.Value);
        }

        public Job GetJobDetailsById(int jobId)
        {

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.Jobs.Include("MasterCountry")
                                   .Include("MasterJobType")
                                   .Include("MasterIndustry")
                                   .Include("MasterCurrencyType")
                                   .Include("User")
                                   .Include("MasterSkill")
                                   .First(j => j.Id == jobId);
            }
        }
        public Job GetJobDetailsByTitle(string title)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.Jobs.Include("MasterCountry")
                                   .Include("MasterJobType")
                                   .Include("MasterIndustry")
                                   .Include("MasterCurrencyType")
                                   .Include("User")
                                   .First(j => j.Title == title);
            }
        }
        public List<PreferredFeedUserUser> Getfollowerslist(int jobid)
        {
            LoggingManager.Debug("Entering Getfollowerslist - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var list = context.Jobs.FirstOrDefault(x => x.Id == jobid);
                if (list != null)
                {
                    int userid = list.UserId;
                    LoggingManager.Debug("Exiting Getfollowerslist - JobsManager");
                    return context.PreferredFeedUserUsers.Where(x => x.UserId == userid).ToList();
                }
            }
            return null;
        }

        public object GetJobsToWhichPeopleAppliedToThisJob(int numberOfJobsToShow, int jobId)
        {
            LoggingManager.Debug("Entering GetJobsToWhichPeopleAppliedToThisJob - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var innerQueryJa = context.JobApplications.Where(x => x.JobId == jobId);

                var innerQueryUserListJau = (from jau in context.JobApplications
                                             join ja in innerQueryJa on jau.UserId equals ja.UserId
                                             select jau).ToList().Distinct();

                var jobUserList = (from j in context.Jobs.AsEnumerable()
                                   join iqulj in innerQueryUserListJau.AsEnumerable() on j.Id equals iqulj.JobId
                                   where j.UserId == iqulj.UserId
                                   select j).ToList();

                var jobCountryFinalList = (from mc in context.MasterCountries.AsEnumerable()
                                           join jul in jobUserList.AsEnumerable() on mc.Id equals jul.CountryId
                                           select new { jul.Id, jul.Title, mc.Description, jul.Salary }).ToList();
                LoggingManager.Debug("Exiting GetJobsToWhichPeopleAppliedToThisJob - JobsManager");
                return jobCountryFinalList.Distinct().Take(numberOfJobsToShow);
            }
        }

        public int GetNumberOfApplicationsOfThisJOb(int jobId)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                return context.JobApplications.Count(x => x.JobId == jobId);
            }
        }

        public void UpdateJobTotalViews(int jobId)
        {
            LoggingManager.Debug("Entering UpdateJobTotalViews - JobsManager");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                Job jo = context.Jobs.First(j => j.Id == jobId);
                jo.TotalViews = jo.TotalViews + 1;
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting UpdateJobTotalViews - JobsManager");
        }
    }
}
