using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Huntable.Data;
using Huntable.Data.Enums;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;

namespace Huntable.Business.BatchJobs
{
    public class JobRemember
    {
        public void Run()
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LoggingManager.Debug("Entering into Jobs Remember");
                var emailtemplate = EmailTemplateManager.GetTemplate(EmailTemplates.JobRememberEmail);
                var date = DateTime.Now.Date.AddDays(-14);
                var jobs = context.Jobs.Where(x => EntityFunctions.TruncateTime(x.CreatedDateTime) == date&&(x.IsDeleted==false||x.IsDeleted==null)&&(x.IsRssJob==false||x.IsRssJob==null)).ToList();
                foreach (var job in jobs)
                {
                    try
                    {
                    var userdetails = context.Users.FirstOrDefault(x => x.Id == job.UserId);
                    var jobapplicationcount = context.JobApplications.Count(x => x.JobId == job.Id);
                    var jobUrl =new UrlGenerator().JobsUrlGenerator(job.Id);
                    var newdate = DateTime.Now.AddDays(+16).ToString("g", new CultureInfo("en-US"));
                    var valuesList = new Hashtable
                                               {
                                                  { "Name" , userdetails.Name},
                                                  {"JobTitle" ,job.Title},
                                                  {"TotalVIews" , job.TotalViews},
                                                  {"TotalApplications" , jobapplicationcount},
                                                   {"JobDescription" ,job.JobDescription},
                                                    {"Salary" , job.Salary},
                                                    {"NewDate",newdate},
                                                    {"JobUrl","http://huntable.co.uk/"+jobUrl}
                                               };
                    string baseUrl = "https://huntable.co.uk/";
                    valuesList.Add("Server Url","https://huntable.co.uk/");
                  
                        string userProfilePicturePath = Path.Combine(baseUrl, userdetails.UserProfilePictureDisplayUrl.Replace("~/", string.Empty));
                        valuesList.Add("ProfilePic", userProfilePicturePath);
                
                  
                    var body = SnovaUtil.LoadTemplate(emailtemplate.TemplateText, valuesList);
                    SnovaUtil.SendEmail(emailtemplate.Subject, body, new List<string> { userdetails.EmailAddress }.ToArray(),null,null);
                    }
                    catch (Exception exception)
                    {
                        LoggingManager.Debug("Exception caught in"+ exception);
                        throw;
                    }
                }
            }
        }
    }
}
