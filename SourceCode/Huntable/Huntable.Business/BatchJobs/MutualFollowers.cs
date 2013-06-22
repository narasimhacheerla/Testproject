using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.Business.BatchJobs
{
    public class MutualFollowers
    {
       
        public
            void LoadMutualUsers 
            ()
            {
                LoggingManager.Debug("Entering into Mutual Followers");
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    List<User> usersList = context.Users.Where(x => x.IsVerified != null).ToList();
                    foreach (User eachUser in usersList)
                    {
                        try
                        {


                            int userId = eachUser.Id;
                            int commomFollowers = 0;
                            List<PreferredFeedUserUser> mutualFollowers = new List<PreferredFeedUserUser>();
                            List<PreferredFeedUserUser> mutualFollowersList = new List<PreferredFeedUserUser>();
                            List<PreferredFeedUserUser> mutualFollowersListWithAtleastTwo =
                                new List<PreferredFeedUserUser>();
                            List<PreferredFeedUserUser> mutualFollowersFollowersList = new List<PreferredFeedUserUser>();
                            List<PreferredFeedUserUser> mutualFollowersListWithAtleastTwoForFollowersFollowers =
                                new List<PreferredFeedUserUser>();

                            List<PreferredFeedUserUser> followingUsers =
                                context.PreferredFeedUserUsers.Where(s => s.FollowingUserId == userId).ToList();

                            foreach (PreferredFeedUserUser eachFollowingUser in followingUsers)
                            {
                                mutualFollowers =
                                    context.PreferredFeedUserUsers.Where(
                                        s => s.FollowingUserId == eachFollowingUser.UserId).ToList();
                                foreach (PreferredFeedUserUser eachMutualFollowers in mutualFollowers)
                                {
                                    mutualFollowersList.Add(eachMutualFollowers);
                                }
                            }

                            //Test function

                            var DistinctMutualFollowersList = from a in mutualFollowersList
                                                              group a by a.UserId
                                                              into b
                                                              select b.FirstOrDefault();


                            //Load MutualFollowers with Atleast 2 followers
                            foreach (var eachDistinctMutualFollowersList in DistinctMutualFollowersList)
                            {
                                foreach (PreferredFeedUserUser eachFollowingUsers in followingUsers)
                                {
                                    List<PreferredFeedUserUser> numberOfUsersFollowingEachMutualFollowers =
                                        context.PreferredFeedUserUsers.Where(
                                            s =>
                                            s.UserId == eachDistinctMutualFollowersList.UserId &&
                                            s.FollowingUserId == eachFollowingUsers.UserId &&
                                            s.FollowingUserId != userId).ToList();
                                    if (numberOfUsersFollowingEachMutualFollowers.Count > 0)
                                    {
                                        commomFollowers++;
                                    }
                                }
                                if (commomFollowers >= 2)
                                {
                                    mutualFollowersListWithAtleastTwo.Add(eachDistinctMutualFollowersList);
                                    commomFollowers = 0;
                                }
                                else
                                {
                                    commomFollowers = 0;
                                }
                            }
                            //Insert into database
                            foreach (
                                PreferredFeedUserUser eachmutualFollowersListWithAtleastTwo in
                                    mutualFollowersListWithAtleastTwo)
                            {
                                var duplicateUser =
                                    context.MasterPeoples.Where(
                                        s =>
                                        s.UserId == userId &&
                                        s.MutualFollowerId == eachmutualFollowersListWithAtleastTwo.UserId).ToList();
                                if (duplicateUser.Count == 0)
                                {
                                    var eachFollower = new MasterPeople()
                                        {
                                            MutualFollowerId = eachmutualFollowersListWithAtleastTwo.UserId,
                                            UserId = userId,
                                            IsMutualFollowersFollower = false
                                        };
                                    context.MasterPeoples.AddObject(eachFollower);
                                    context.SaveChanges();
                                    context.AcceptAllChanges();
                                }
                            }


                            //Followers Of Mutual Followers
                            foreach (var eachDistinctMutualFollowersList in DistinctMutualFollowersList)
                            {
                                foreach (PreferredFeedUserUser eachFollowingUsers in followingUsers)
                                {
                                    List<PreferredFeedUserUser> numberOfUsersFollowingEachMutualFollowers =
                                        context.PreferredFeedUserUsers.Where(
                                            s =>
                                            s.UserId == eachDistinctMutualFollowersList.UserId &&
                                            s.FollowingUserId == eachFollowingUsers.UserId).ToList();
                                    if (numberOfUsersFollowingEachMutualFollowers.Count > 0)
                                    {
                                        commomFollowers++;
                                    }
                                }
                                if (commomFollowers >= 2)
                                {
                                    mutualFollowersListWithAtleastTwoForFollowersFollowers.Add(
                                        eachDistinctMutualFollowersList);
                                    commomFollowers = 0;
                                }
                                else
                                {
                                    commomFollowers = 0;
                                }
                            }

                            foreach (
                                PreferredFeedUserUser eachMutualFollower in
                                    mutualFollowersListWithAtleastTwoForFollowersFollowers)
                            {
                                List<PreferredFeedUserUser> mutualFollowersFollowers =
                                    context.PreferredFeedUserUsers.Where(
                                        s => s.FollowingUserId == eachMutualFollower.UserId).ToList();
                                foreach (PreferredFeedUserUser eachMutualFollowersFollowers in mutualFollowersFollowers)
                                {
                                    List<PreferredFeedUserUser> userCount =
                                        context.PreferredFeedUserUsers.Where(
                                            s =>
                                            s.UserId == eachMutualFollowersFollowers.UserId &&
                                            s.FollowingUserId == userId).ToList();
                                    if (userCount.Count == 0)
                                    {
                                        mutualFollowersFollowersList.Add(eachMutualFollowersFollowers);
                                    }

                                }
                            }

                            var DistinctMutualFollowersFollowersList = from a in mutualFollowersFollowersList
                                                                       group a by a.UserId
                                                                       into b
                                                                       select b.FirstOrDefault();
                            foreach (
                                PreferredFeedUserUser eachDistinctMutualFollowersFollowers in
                                    DistinctMutualFollowersFollowersList)
                            {
                                var dupliacateUser =
                                    context.MasterPeoples.Where(
                                        s =>
                                        s.UserId == userId &&
                                        s.MutualFollowerId == eachDistinctMutualFollowersFollowers.UserId).ToList();
                                if (dupliacateUser.Count == 0)
                                {
                                    var eachFollower = new MasterPeople()
                                        {
                                            MutualFollowerId = eachDistinctMutualFollowersFollowers.UserId,
                                            UserId = userId,
                                            IsMutualFollowersFollower = true
                                        };
                                    context.MasterPeoples.AddObject(eachFollower);
                                    context.SaveChanges();
                                    context.AcceptAllChanges();
                                }
                            }
DeleteIfAnyFollowedUserPresent();
                            //Followers Of Mutual Followers End

                        }
                        catch (Exception exception)
                        {
                            LoggingManager.Debug("Exiting from Mutual followers with exception" + exception);
                            throw;
                        }
                    }
                }

            }
        public void DeleteIfAnyFollowedUserPresent()
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                List<User> userList = context.Users.ToList();
                foreach (User eachUser in userList)
                {

                    List<PreferredFeedUserUser> dupliacteUsersList =
                        context.PreferredFeedUserUsers.Where(s => s.FollowingUserId == eachUser.Id).ToList();
                    foreach (PreferredFeedUserUser eachDupliacteUsersList in dupliacteUsersList)
                    {
                        var duplicateFollower =
                            context.MasterPeoples.Where(
                                s =>
                                s.UserId == eachDupliacteUsersList.FollowingUserId &&
                                s.MutualFollowerId == eachDupliacteUsersList.UserId).ToList();
                        foreach (var eachDuplicateFollower in duplicateFollower)
                        {
                            context.MasterPeoples.DeleteObject(eachDuplicateFollower);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
        }

    }

