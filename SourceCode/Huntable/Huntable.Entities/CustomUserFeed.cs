using System;
using Snovaspace.Util;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;

namespace Huntable.Entities
{
    public class CustomUserFeed
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FeedDesription { get; set; }
        public int? ProfilePicturePath { get; set; }
        public string ProfilePictureDisplayPath
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(ProfilePicturePath);
            }
        }
        public DateTime CreatedDateTime { get; set; }

        public CustomUserFeed(int id, int userId, string feedDesription, int? profilePicturePath, DateTime createdDateTime)
        {
            LoggingManager.Debug("Entering CustomUserFeed  -  CustomUserFeed.cs");
            Id = id;
            UserId = userId;
            FeedDesription = feedDesription;
            ProfilePicturePath = profilePicturePath;
            CreatedDateTime = createdDateTime;
            LoggingManager.Debug("Exiting CustomUserFeed  -  CustomUserFeed.cs");
        }
    }
}