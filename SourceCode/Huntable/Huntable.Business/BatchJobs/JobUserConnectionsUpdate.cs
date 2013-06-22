using System;
using System.Collections.Generic;
using System.Linq;
using Huntable.Data;
using MoreLinq;
using Snovaspace.Util.Logging;

namespace Huntable.Business.BatchJobs
{
    public class JobsUserConnectionsUpdate : BatchJobBase
    {
        public JobsUserConnectionsUpdate()
            : base("JobsUserConnectionsUpdate", false)
        {
        }

        public override void Run(huntableEntities context)
        {
            LoggingManager.Debug("Entering into JobUserConnectionUpdate");
            try
            {
                foreach (
                    User user in
                        context.Users.Where(
                            x =>
                            x.IsVerified.HasValue && x.IsVerified.Value && !x.IsAdmin.HasValue &&
                            x.LastActivityDate.HasValue).ToList())
                {
                    try
                    {


                        int userId = user.Id;
                        LoggingManager.Debug("Updating the feeds of userid"+userId);
                        List<JobFeed> currentJobFeeds = context.JobFeeds.Where(x => x.UserId == userId).ToList();
                        var preferredJobUserIndustries =
                            context.PreferredJobUserIndustries.Where(x => x.UserId == userId).ToList();
                        var preferredJobUserkills =
                            context.PreferredJobUserSkills.Where(x => x.UserId == userId).ToList();
                        var preferredJobUserCountries =
                            context.PreferredJobUserCountries.Where(x => x.UserId == userId).ToList();
                        var preferredJobUserJobTypes =
                            context.PreferredJobUserJobTypes.Where(x => x.UserId == userId).ToList();

                        foreach (var currentJobFeed in currentJobFeeds.Where(x => x.UserId == userId).ToList())
                        {
                            context.DeleteObject(currentJobFeed);
                        }

                        context.SaveChanges();

                        var allExpectedUserJobsTypeOne = new List<Job>();
                        var allExpectedUserJobsTypeTwo = new List<Job>();
                        var allExpectedUserJobsTypeThree = new List<Job>();
                        var allExpectedUserJobsTypeFour = new List<Job>();

                        // All jobs by industries
                        var industriesFollowing =
                            preferredJobUserIndustries.Where(x => x.UserId == userId)
                                                      .Select(x => x.MasterIndustryId)
                                                      .ToList();
                        var date = System.DateTime.Now;
                        var olddate = System.DateTime.Now.AddDays(-30);
                        var jobs = context.ListofJobs(olddate, date);
                        allExpectedUserJobsTypeOne.AddRange(
                           jobs.Where(
                                x => !industriesFollowing.Any() || industriesFollowing.Contains((int) x.IndustryId))
                                   .ToList());

                        // All jobs by skills
                        var skillsFollowing =
                            preferredJobUserkills.Where(x => x.UserId == userId).Select(x => x.MasterSkillId).ToList();
                        allExpectedUserJobsTypeTwo.AddRange(
                            jobs.Where(
                                x =>
                                !skillsFollowing.Any() ||
                                (x.SkillId != null && skillsFollowing.Contains(x.SkillId.Value))).ToList());

                        // All jobs by countries
                        var countriesFollowing =
                            preferredJobUserCountries.Where(x => x.UserId == userId)
                                                     .Select(x => x.MasterCountryId)
                                                     .ToList();
                        allExpectedUserJobsTypeThree.AddRange(
                            jobs.Where(
                                x => !countriesFollowing.Any() || countriesFollowing.Contains(x.CountryId)).ToList());

                        // All jobs by interests
                        var jobTypesFollowing =
                            preferredJobUserJobTypes.Where(x => x.UserId == userId)
                                                    .Select(x => x.MasterJobTypeId)
                                                    .ToList();
                        allExpectedUserJobsTypeFour.AddRange(
                            jobs.Where(
                                x => !jobTypesFollowing.Any() || jobTypesFollowing.Contains((int) x.JobTypeId)).ToList());

                        List<Job> allExpectedUserJobs =
                            allExpectedUserJobsTypeOne.Intersect(allExpectedUserJobsTypeTwo)
                                                      .Intersect(allExpectedUserJobsTypeThree)
                                                      .Intersect(allExpectedUserJobsTypeFour)
                                                      .OrderByDescending(x => x.CreatedDateTime)
                                                      .Take(1000)
                                                      .ToList();

                        // All the users added
                        allExpectedUserJobs.Distinct().ForEach(x =>
                            {
                                var jobFeed = new JobFeed {CreatedDateTime = DateTime.Now, UserId = userId, Job = x};
                                context.AddToJobFeeds(jobFeed);
                            });

                        context.SaveChanges();
                        LoggingManager.Debug("Updated the feeds of the user id "+userId);
                    }
                    catch (Exception exception)
                    {
                        LoggingManager.Debug("Caught Exception in JobuserConnectionsUodate" + exception);
                        throw;
                    }
                }
            }
            catch(Exception exception)
                    {
                        LoggingManager.Debug("Caught Exception in JobuserConnectionsUodate" + exception);
                        throw;
                    }
                }
           
        }
    }

