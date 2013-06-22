using System;
using System.Collections.Generic;
using System.Linq;
using Huntable.Data;
using Huntable.Entities;
using Snovaspace.Util;
using Snovaspace.Util.Logging;

namespace Huntable.Business
{
    public class UserFeedManager
    {
        public void SaveUserFeed(int userID, string feedType, string feed)
        {
            LoggingManager.Debug("Entering SaveUserFeed - UserFeedManager");
            var context = huntableEntities.GetEntitiesWithNoLock();
            feed = FeedContentManager.getLinkContent(feed);
            var userFeed = new UserFeed
                               {
                                   UserID = userID,
                                   FeedDesription = feed,
                                   CreatedDateTime = DateTime.Now
                               };
            context.UserFeeds.AddObject(userFeed);
            context.SaveChanges();
            FeedManager.addFeedNotification(FeedManager.FeedType.Feed_Update, userID,userFeed.ID,null);
            LoggingManager.Debug("Exiting SaveUserFeed - UserFeedManager");
        }

        private string FormatFeed(int userID, string feedType, string feed)
        {
            LoggingManager.Debug("Entering FormatFeed - UserFeedManager");
            string feedDescription = "";
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                User user = context.Users.First(u => u.Id == userID);
                
                switch (feedType)
                {
                    case "WhatsonMind":
                        feedDescription = "<a href=ViewUserProfile.aspx?UserId=" + user.Id + ">" + user.Name + "</a>" + " " +
                                          feed;
                        break;
                    case "Job":
                        Job id = context.Jobs.FirstOrDefault(u => u.Title == feed);
                        feedDescription = "<a href=ViewUserProfile.aspx?UserId=" + user.Id + ">" + user.Name + "</a>" +
                                          " has posted new job opportunity " + "<a href=" +new UrlGenerator().JobsUrlGenerator(id.Id) + ">" + feed;
                        break;
                    case "Employment":
                      

                        feedDescription = "<a href=ViewUserProfile.aspx?UserId=" + user.Id + ">" + user.Name + " has changed his current employments status to " + feed;
                        break;
                    case "Education":
                        feedDescription = "<a href=ViewUserProfile.aspx?UserId=" + user.Id + ">" + user.Name + " has new education " + feed;
                        break;
                    case "Interest":
                        feedDescription = "<a href=ViewUserProfile.aspx?UserId=" + user.Id + ">" + user.Name + " has a new Interest " + feed;
                        break;
                    case "ProfilePicture":
                        feedDescription = "<a href=ViewUserProfile.aspx?UserId=" + user.Id + ">" + user.Name + " has a new Profile Picture";
                        break;
                }
            }
            LoggingManager.Debug("Exiting FormatFeed - UserFeedManager");
            return feedDescription;
        }

        public IList<CustomUserFeed> GetUserFeeds(int userID, int count)
        {
            LoggingManager.Debug("Entering GetUserFeeds - UserFeedManager");
            LoggingManager.Info("GetUserFeeds entry.");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var feedsQuery = (from followingUser in context.PreferredFeedUserUserDeriveds
                                  join userfeed in context.UserFeeds on followingUser.FollowingUserId equals
                                      userfeed.UserID
                                  join user in context.Users on followingUser.FollowingUserId equals user.Id
                                  where followingUser.UserId == userID
                                  select
                                      new
                                          {
                                              userfeed.ID,
                                              userfeed.UserID,
                                              userfeed.FeedDesription,
                                              user.PersonalLogoFileStoreId,
                                              userfeed.CreatedDateTime
                                          }).OrderByDescending(x => x.CreatedDateTime).Take(count);

                LoggingManager.Debug(feedsQuery.ToTraceString());
                LoggingManager.Debug("Exiting GetUserFeeds - UserFeedManager");
                return
                    feedsQuery.ToList().Select(
                        x =>
                        new CustomUserFeed(x.ID, x.UserID, x.FeedDesription, x.PersonalLogoFileStoreId,
                                           x.CreatedDateTime)).ToList();
            }
            
        }
    }
}
