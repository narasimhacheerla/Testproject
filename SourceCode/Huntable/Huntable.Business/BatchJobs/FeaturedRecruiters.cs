using System;
using System.Collections.Generic;
using System.Linq;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.Business.BatchJobs
{
    public class FeaturedRecruiters : BatchJobBase
    {
        public FeaturedRecruiters()
            : base("FeaturedRecruiters")
        {

        }

        public override void Run(huntableEntities huntableEntities)
        {
            LoggingManager.Debug("Entering into Featured Recuirters");
            List<User> allUsers = huntableEntities.Users.Where(x => x.IsActive.HasValue && x.IsActive.Value).ToList();
            List<FeaturedUserCompany> currentFeaturedUserCompanies = huntableEntities.FeaturedUserCompanies.ToList();

            List<FeaturedCountry> allFeaturedCountries = huntableEntities.FeaturedCountries.ToList();
            List<FeaturedIndustry> allFeaturedIndustries = huntableEntities.FeaturedIndustries.ToList();
            List<FeaturedInterest> allFeaturedInterests = huntableEntities.FeaturedInterests.ToList();
            List<FeaturedSkill> allFeaturedSkills = huntableEntities.FeaturedSkills.ToList();

            foreach (User user in allUsers)
            {
                try
                {


                    // Find all the companies that purchased featured services for the user's country

                    User user1 = user;

                    IEnumerable<MasterCountry> masterCountries =
                        allFeaturedCountries.Where(x => x.UserId == user1.Id)
                                            .Select(x => x.MasterCountry)
                                            .Distinct()
                                            .ToList();
                    var newFeaturedCompanies = masterCountries.SelectMany(masterCountry => masterCountry.Users).ToList();

                    // Find all the companies that purchased featured services for the user's industry
                    List<MasterIndustry> masterIndustries =
                        allFeaturedIndustries.Where(x => x.UserId == user1.Id)
                                             .Select(x => x.MasterIndustry)
                                             .Distinct()
                                             .ToList();
                    newFeaturedCompanies.AddRange(
                        masterIndustries.SelectMany(
                            masterIndustry => masterIndustry.EmploymentHistories.Select(x => x.User).ToList()));

                    // Find all the companies that purchased featured services for the user's interest
                    var masterInterests =
                        allFeaturedInterests.Where(x => x.UserId == user1.Id)
                                            .Select(x => x.MasterInterest)
                                            .Distinct()
                                            .ToList();
                    newFeaturedCompanies.AddRange(
                        masterInterests.SelectMany(
                            masterInterest => masterInterest.UserInterests.Select(x => x.User).ToList()));

                    // Find all the companies that purchased featured services for the user's skill
                    var masterSkills =
                        allFeaturedSkills.Where(x => x.UserId == user1.Id)
                                         .Select(x => x.MasterSkill)
                                         .Distinct()
                                         .ToList();
                    newFeaturedCompanies.AddRange(
                        masterSkills.SelectMany( 
                            masterSkill =>
                            masterSkill.UserEmploymentSkills.Select(x => x.EmploymentHistory.User).ToList()));

                    // Find all the companies that are not currently listed in the users featured countries list.
                    List<int?> newFeaturedCompaniesIds =
                        newFeaturedCompanies.Select(x => x.GetCompanyForUser()).ToList();
                    List<int> currentFeaturedUserCompaniesIds =
                        currentFeaturedUserCompanies.Select(x => x.CompanyId).ToList();

                    List<int?> featuredCompaniesIdThatAreNew =
                        newFeaturedCompaniesIds.Where(
                            x => x != null && !currentFeaturedUserCompaniesIds.Contains(x.Value)).ToList();

                    featuredCompaniesIdThatAreNew.ForEach(x =>
                        {
                            if (x != null)
                                AddToFeaturedUserCompanies(huntableEntities,
                                                           currentFeaturedUserCompanies,
                                                           user, x.Value);
                        });
                }
                catch (Exception exception)
                {
                    LoggingManager.Debug("Caught exception in Featured recuirters "+exception);
                    throw;
                }
                }
            
            
        }

        private static void AddToFeaturedUserCompanies(huntableEntities huntableEntities, IEnumerable<FeaturedUserCompany> featuredUserCompanies, User user, int companyId)
        {
            User user2 = user;

            if (!featuredUserCompanies.Any(x => (x.UserId == user2.Id && x.MasterCompany.Id == companyId)))
            {
                huntableEntities.AddToFeaturedUserCompanies(new FeaturedUserCompany
                                                                {
                                                                    UserId = user.Id,
                                                                    CompanyId = companyId,
                                                                    CreatedDateTime = DateTime.Now
                                                                });
            }
        }
    }
}