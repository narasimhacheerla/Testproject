using System;
using System.Collections.Generic;
using System.Linq;
using Huntable.Data;
using MoreLinq;
using Snovaspace.Util.Logging;

namespace Huntable.Business.BatchJobs
{
    public class FeedsUserConnectionsUpdate : BatchJobBase
    {
        public FeedsUserConnectionsUpdate()
            : base("FeedsUserConnectionsUpdate", false)
        {
        }

        public override void Run(huntableEntities context)
        {
            var userDerivedConnections = context.PreferredFeedUserUserDeriveds.ToList();
            LoggingManager.Debug(" Entering into Feed user Connection update");
            foreach (User user in context.Users)
            {
                try
                {

             
                int userID = user.Id;

                var connections = new List<int>();

                // Get all the relavent users

                // 1. If he has opted for connectioons, find all his connections and get their feeds

                if (user.ShowFeedsFromMyConnections)
                {
                    connections.AddRange(new InvitationManager().GetLevelOneConnects(userID).Select(x => x.Id));
                }

                // 2. Union them with all their directly followed users
                connections.AddRange(context.PreferredFeedUserUsers.Where(x => x.UserId == userID).Select(x => x.FollowingUserId).ToList());

                // 3. Find all the industries he is following and the users belonging to those industries
                var industriesFollowing = context.PreferredFeedUserIndustries.Where(x => x.UserId == userID).ToList();

                foreach (var preferredFeedUserIndustry in industriesFollowing)
                {
                    connections.AddRange(preferredFeedUserIndustry.MasterIndustry.EmploymentHistories.Select(x => x.User.Id));
                }

                // 4. Find all the skills he is following and the users belonging to those skills
                var skillsFollowing = context.PreferredFeedUserSkills.Where(x => x.UserId == userID).ToList();

                foreach (var preferredSkill in skillsFollowing)
                {
                    connections.AddRange(preferredSkill.MasterSkill.UserEmploymentSkills.Select(x => x.EmploymentHistory.User.Id));
                }

                // 5. Find all the countries that he is following and the users belonging to those countries
                var countriesFollowing = context.PreferredFeedUserCountries.Where(x => x.UserId == userID).ToList();

                foreach (var preferredCountry in countriesFollowing)
                {
                    connections.AddRange(preferredCountry.MasterCountry.EducationHistories.Select(x => x.User.Id));
                }

                // 6. Find all the interests that he is following and the users belonging to those interests
                List<PreferredFeedUserInterest> interestsFollowing = context.PreferredFeedUserInterests.Where(x => x.UserId == userID).ToList();

                foreach (PreferredFeedUserInterest preferredInterest in interestsFollowing)
                {
                    connections.AddRange(preferredInterest.MasterInterest.UserInterests.Select(x => x.User.Id));
                }

                List<int> allUserDerivedConnections = userDerivedConnections.Where(x => x.UserId == userID).Select(x => x.FollowingUserId).ToList();

                // All the users added
                connections.Distinct().Where(connection => !allUserDerivedConnections.Contains(connection)).ForEach(x =>
                                                                                                             {
                                                                                                                 var feedUserDerived = new PreferredFeedUserUserDerived { CreatedDateTime = DateTime.Now, FollowingUserId = x, UserId = userID };
                                                                                                                 LoggingManager.Debug("New user followed");
                                                                                                                 LoggingManager.PrintObject(feedUserDerived);
                                                                                                                 context.AddToPreferredFeedUserUserDeriveds(feedUserDerived);
                                                                                                             });

                // All the users removed
                var allUsersDeleted = allUserDerivedConnections.Where(userDerivedConnection => !connections.Contains(userDerivedConnection)).ToList();

                foreach (int userDeleted in allUsersDeleted)
                {
                    LoggingManager.Debug("User unfollowed " + userDeleted);

                    int deleted = userDeleted;

                    if (userDeleted == user.Id) continue;

                    var deletedConnection = userDerivedConnections.FirstOrDefault(x => x.UserId == userID && x.FollowingUserId == deleted);
                    if (deletedConnection != null) context.DeleteObject(deletedConnection);
                }
                }
                catch (Exception exception)
                {
                    LoggingManager.Debug("Caught Exception in the feedUser connection upadate");
                    throw;
                }
            }

        }
    }
}