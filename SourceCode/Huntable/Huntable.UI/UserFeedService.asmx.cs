using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Huntable.Business;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    /// <summary>
    /// Summary description for UserFeedService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserFeedService : System.Web.Services.WebService
    {
        [WebMethod(true)]
        public FeedList GetUserFeedList(string type, string profileUserId, string latestFeedId, string pageIndex)
        {
            LoggingManager.Debug("Entering GetUserFeedList - UserFeedService");
            int LatestFeedId = 0;
            if (latestFeedId != "")
                LatestFeedId = Convert.ToInt32(latestFeedId);

            int PageIndex = 1;
            if (pageIndex != "" && pageIndex != "0")
                PageIndex = Convert.ToInt32(pageIndex);

            FeedManager.FeedRetrievePage objType = (FeedManager.FeedRetrievePage)Enum.Parse(typeof(FeedManager.FeedRetrievePage), type);
            int pagesize = 10;

            if (Session[SessionNames.UserFeedPagesize] != null)
            {
                pagesize = Convert.ToInt32(Session[SessionNames.UserFeedPagesize]);
            }
            int CurrentUserId = Common.GetLoggedInUserId() ?? 0;

            int ProfileUserId = Common.GetLoggedInUserId() ?? 0;
            if (profileUserId != "")
                ProfileUserId = Convert.ToInt32(profileUserId);

            LoggingManager.Debug("Exiting GetUserFeedList - UserFeedService");
            return FeedManager.GetFeedsPagewise(objType, CurrentUserId, ProfileUserId, LatestFeedId, PageIndex, pagesize);
        }

        [WebMethod(true)]
        public FeedLikeDetail MarkLike(string feedId, string type, string refRecordId)
        {
            LoggingManager.Debug("Entering MarkLike - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            Huntable.Business.FeedManager.FeedType Type = (Huntable.Business.FeedManager.FeedType)Enum.Parse(typeof(Huntable.Business.FeedManager.FeedType), type);
            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            int RefRecordId = 0;
            if (refRecordId != "")
                RefRecordId = Convert.ToInt32(refRecordId);

            FeedManager.addFeedNotification(Type, currentUserId, RefRecordId, null);
            LoggingManager.Debug("Exiting MarkLike - UserFeedService");
            return FeedManager.GetFeedLikes(FeedId, currentUserId, Type, RefRecordId);
        }

        [WebMethod(true)]
        public FeedLikeDetail MarkUnlike(string feedId, string type, string refRecordId)
        {
            LoggingManager.Debug("Entering MarkUnlike - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            Huntable.Business.FeedManager.FeedType Type = (Huntable.Business.FeedManager.FeedType)Enum.Parse(typeof(Huntable.Business.FeedManager.FeedType), type);
            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            int RefRecordId = 0;
            if (refRecordId != "")
                RefRecordId = Convert.ToInt32(refRecordId);

            FeedManager.deleteFeedNotitifation(Type, currentUserId, RefRecordId);
            LoggingManager.Debug("Exiting MarkUnlike - UserFeedService");
            return FeedManager.GetFeedLikes(FeedId, currentUserId, Type, RefRecordId);
        }

        [WebMethod(true)]
        public FeedCommentDetail AddFeedComment(string feedId, string comments, string latestFeedId)
        {
            LoggingManager.Debug("Entering MarkUnlike - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            //int FeedUserId = 0;
            //if (feedUserId != "")
            //    FeedUserId = Convert.ToInt32(feedUserId);

            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            int LatestFeedId = 0;
            if (latestFeedId != "")
                LatestFeedId = Convert.ToInt32(latestFeedId);

            FeedManager.addCommentFeedNotification(FeedId, currentUserId, comments);
            LoggingManager.Debug("Exiting MarkUnlike - UserFeedService");
            return FeedManager.GetLatestFeedComments(FeedId, currentUserId, LatestFeedId);
        }

        [WebMethod(true)]
        public FeedCommentDetail AddCommentNotification(string feedId, string feedUserId, string type, string refRecordId, string comments, string latestFeedId)
        {
            LoggingManager.Debug("Entering AddCommentNotification - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            Huntable.Business.FeedManager.FeedType Type = (Huntable.Business.FeedManager.FeedType)Enum.Parse(typeof(Huntable.Business.FeedManager.FeedType), type);
            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            int FeedUserId = 0;
            if (feedUserId != "")
                FeedUserId = Convert.ToInt32(feedUserId);

            int RefRecordId = 0;
            if (refRecordId != "")
                RefRecordId = Convert.ToInt32(refRecordId);

            int LatestFeedId = 0;
            if (latestFeedId != "")
                LatestFeedId = Convert.ToInt32(latestFeedId);

            FeedManager.addFeedNotification(Type, currentUserId, RefRecordId, comments);
            LoggingManager.Debug("Exiting AddCommentNotification - UserFeedService");
            return FeedManager.GetLatestFeedComments(FeedId, FeedUserId, Type, RefRecordId, currentUserId, LatestFeedId);
        }

        [WebMethod(true)]
        public FeedCommentDetail GetPreviousComments(string feedId, string feedUserId, string type, string refRecordId, string pageSize, string oldestFeedId)
        {
            LoggingManager.Debug("Entering GetPreviousComments - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            int FeedUserId = 0;
            if (feedUserId != "")
                FeedUserId = Convert.ToInt32(feedUserId);

            Huntable.Business.FeedManager.FeedType Type = (Huntable.Business.FeedManager.FeedType)Enum.Parse(typeof(Huntable.Business.FeedManager.FeedType), type);
            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            int RefRecordId = 0;
            if (refRecordId != "")
                RefRecordId = Convert.ToInt32(refRecordId);

            int OldestFeedId = 0;
            if (oldestFeedId != "")
                OldestFeedId = Convert.ToInt32(oldestFeedId);

            int PageSize = 0;
            if (pageSize != "")
                PageSize = Convert.ToInt32(pageSize);
            LoggingManager.Debug("Exiting GetPreviousComments - UserFeedService");
            return FeedManager.GetFeedComments(FeedId, FeedUserId, currentUserId, OldestFeedId, PageSize, Type, RefRecordId);
        }

        [WebMethod(true)]
        public FeedCommentDetail GetComments(string feedId)
        {
            LoggingManager.Debug("Entering GetComments - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            LoggingManager.Debug("Exiting GetComments - UserFeedService");
            return FeedManager.GetFeedComments(FeedId, currentUserId);
        }
        [WebMethod(true)]
        public bool DeleteFeed(string feedId)
        {
            LoggingManager.Debug("Entering DeleteFeed - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            FeedManager.deleteFeed(FeedId);
            LoggingManager.Debug("Exiting DeleteFeed - UserFeedService");
            return true;
        }
        [WebMethod(true)]
        public bool HideFeed(string feedId)
        {
            LoggingManager.Debug("Entering HideFeed - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            int currentUserId = Common.GetLoggedInUserId() ?? 0;
            FeedManager.HideFeed(FeedId, currentUserId);
            LoggingManager.Debug("Exiting HideFeed - UserFeedService");
            return true;
        }
        [WebMethod(true)]
        public bool MarkFollow(string userId)
        {
            LoggingManager.Debug("Entering MarkFollow - UserFeedService");
            int UserId = 0;
            if (userId != "")
                UserId = Convert.ToInt32(userId);

            int currentUserId = Common.GetLoggedInUserId() ?? 0;
            UserManager.FollowUser(currentUserId, UserId);
            LoggingManager.Debug("Exiting MarkFollow - UserFeedService");
            return true;
        }

        [WebMethod(true)]
        public string MarkCommentLike(string feedId)
        {
            LoggingManager.Debug("Entering MarkCommentLike - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            Huntable.Business.FeedManager.FeedType Type = Huntable.Business.FeedManager.FeedType.Like_Comment;
            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            int RefRecordId = FeedId;

            FeedManager.addFeedNotification(Type, currentUserId, RefRecordId, null);
            var socialManager = new SocialShareManager();
            var msg = "[UserName]" + " " + "liked a comment in Huntable";
            socialManager.ShareOnFacebook(currentUserId, msg, "");
            LoggingManager.Debug("Exiting MarkCommentLike - UserFeedService");
            return FeedContentManager.getCommentLikeLink(FeedId, currentUserId);
        }

        [WebMethod(true)]
        public string MarkCommentUnlike(string feedId)
        {
            LoggingManager.Debug("Entering MarkCommentUnlike - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            Huntable.Business.FeedManager.FeedType Type = Huntable.Business.FeedManager.FeedType.Like_Comment;
            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            int RefRecordId = FeedId;

            FeedManager.deleteFeedNotitifation(Type, currentUserId, RefRecordId);
            LoggingManager.Debug("Exiting MarkCommentUnlike - UserFeedService");
            return FeedContentManager.getCommentLikeLink(FeedId, currentUserId);
        }

        [WebMethod(true)]
        public FeedLikeDetail MarkPhotoLike(string feedId, string type, string refRecordId)
        {
            LoggingManager.Debug("Entering MarkPhotoLike - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            Huntable.Business.FeedManager.FeedType Type = (Huntable.Business.FeedManager.FeedType)Enum.Parse(typeof(Huntable.Business.FeedManager.FeedType), type);
            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            int RefRecordId = 0;
            if (refRecordId != "")
                RefRecordId = Convert.ToInt32(refRecordId);

            FeedManager.addFeedNotification(Type, currentUserId, RefRecordId, null);
            LoggingManager.Debug("Exiting MarkPhotoLike - UserFeedService");
            return FeedManager.GetFeedLikes(FeedId, currentUserId, Type, RefRecordId);
        }

        [WebMethod(true)]
        public FeedLikeDetail MarkPhotoUnlike(string feedId, string type, string refRecordId)
        {
            LoggingManager.Debug("Entering MarkPhotoUnlike - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            Huntable.Business.FeedManager.FeedType Type = (Huntable.Business.FeedManager.FeedType)Enum.Parse(typeof(Huntable.Business.FeedManager.FeedType), type);
            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            int RefRecordId = 0;
            if (refRecordId != "")
                RefRecordId = Convert.ToInt32(refRecordId);

            FeedManager.deleteFeedNotitifation(Type, currentUserId, RefRecordId);
            LoggingManager.Debug("Exiting MarkPhotoUnlike - UserFeedService");
            return FeedManager.GetFeedLikes(FeedId, currentUserId, Type, RefRecordId);
        }

        [WebMethod(true)]
        public UserLikeList GetUserPhotoLikes(string pagesize, string profileUserId, string oldestLikeId, string width)
        {
            LoggingManager.Debug("Entering GetUserPhotoLikes - UserFeedService");
            int Pagesize = 0;
            if (pagesize != "")
                Pagesize = Convert.ToInt32(pagesize);

            int ProfileUserId = 0;
            if (profileUserId != "")
                ProfileUserId = Convert.ToInt32(profileUserId);

            int OldestLikeId = 0;
            if (oldestLikeId != "")
                OldestLikeId = Convert.ToInt32(oldestLikeId);

            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            if (width == "")
                width = "168px";
            LoggingManager.Debug("Exiting GetUserPhotoLikes - UserFeedService");
            return FeedManager.GetUserPhotoLikes(currentUserId, ProfileUserId, OldestLikeId, Pagesize, width);
        }

        [WebMethod(true)]
        public PopupDetail GetPopupDetail(string type, string refRecordId)
        {
            LoggingManager.Debug("Entering GetPopupDetail - UserFeedService");
            int RefRecordId = 0;
            if (refRecordId != "")
                RefRecordId = Convert.ToInt32(refRecordId);

            //Huntable.Business.FeedManager.FeedType Type = Huntable.Business.FeedManager.FeedType.Like_Comment;
            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            LoggingManager.Debug("Exiting GetPopupDetail - UserFeedService");
            return FeedManager.getPopupDetail(type, currentUserId, RefRecordId);
        }

        [WebMethod(true)]
        public FeedLikeDetail GetFeedLikeDetail(string feedId, string type, string refRecordId)
        {
            LoggingManager.Debug("Entering GetFeedLikeDetail - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            Huntable.Business.FeedManager.FeedType Type = (Huntable.Business.FeedManager.FeedType)Enum.Parse(typeof(Huntable.Business.FeedManager.FeedType), type);
            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            int RefRecordId = 0;
            if (refRecordId != "")
                RefRecordId = Convert.ToInt32(refRecordId);

            LoggingManager.Debug("Exiting GetFeedLikeDetail - UserFeedService");
            return FeedManager.GetFeedLikes(FeedId, currentUserId, Type, RefRecordId);
        }

        [WebMethod(true)]
        public Popup_LikedDetail GetFeedLikedUser(string feedId, string type, string refRecordId, string pagesize, string oldestLikeId)
        {
            LoggingManager.Debug("Entering GetFeedLikedUser - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            int Pagesize = 0;
            if (pagesize != "")
                Pagesize = Convert.ToInt32(pagesize);
            //Huntable.Business.FeedManager.FeedType Type = (Huntable.Business.FeedManager.FeedType)Enum.Parse(typeof(Huntable.Business.FeedManager.FeedType), type);
            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            int RefRecordId = 0;
            if (refRecordId != "")
                RefRecordId = Convert.ToInt32(refRecordId);

            int OldestLikeId = 0;
            if (oldestLikeId != "")
                OldestLikeId = Convert.ToInt32(oldestLikeId);

            LoggingManager.Debug("Exiting GetFeedLikedUser - UserFeedService");
            return FeedManager.GetLikedUser(FeedId, type, currentUserId, RefRecordId, OldestLikeId, Pagesize);
        }

        [WebMethod(true)]
        public string MarkDirectLike(string feedId, string type, string refRecordId)
        {
            LoggingManager.Debug("Entering MarkDirectLike - UserFeedService");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            Huntable.Business.FeedManager.FeedType Type = (Huntable.Business.FeedManager.FeedType)Enum.Parse(typeof(Huntable.Business.FeedManager.FeedType), type);
            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            int RefRecordId = 0;
            if (refRecordId != "")
                RefRecordId = Convert.ToInt32(refRecordId);

            FeedManager.addFeedNotification(Type, currentUserId, RefRecordId, null);
            LoggingManager.Debug("Exiting UserFeedService - MarkDirectLike");
            return "MarkDirectUnlike(0, '" + type + "', " + refRecordId + ")";
        }

        [WebMethod(true)]
        public string MarkDirectUnlike(string feedId, string type, string refRecordId)
        {
            LoggingManager.Debug("Entering UserFeedService - MarkDirectUnlike");
            int FeedId = 0;
            if (feedId != "")
                FeedId = Convert.ToInt32(feedId);

            Huntable.Business.FeedManager.FeedType Type = (Huntable.Business.FeedManager.FeedType)Enum.Parse(typeof(Huntable.Business.FeedManager.FeedType), type);
            int currentUserId = Common.GetLoggedInUserId() ?? 0;

            int RefRecordId = 0;
            if (refRecordId != "")
                RefRecordId = Convert.ToInt32(refRecordId);

            FeedManager.deleteFeedNotitifation(Type, currentUserId, RefRecordId);
            LoggingManager.Debug("Exiting MarkDirectUnlike - UserFeedService");
            return "MarkDirectLike(0, '" + type + "', " + refRecordId + ")";
        }

        [WebMethod(true)]
        public bool CheckIfUserLoggedIn()
        {
            LoggingManager.Debug("Entering CheckIfUserLoggedIn - UserFeedService");

            int CurrentUserId = Common.GetLoggedInUserId() ?? 0;

            LoggingManager.Debug("Exiting CheckIfUserLoggedIn - UserFeedService");
            if (CurrentUserId > 0)
                return true;
            else
                return false;
        }
        [WebMethod(true)]
        public int getAlertCount()
        {
            LoggingManager.Debug("Entering getAlertCount - UserFeedService");

            int CurrentUserId = Common.GetLoggedInUserId() ?? 0;

            LoggingManager.Debug("Exiting getAlertCount - UserFeedService");

            return FeedManager.GetFeedsAlertCount(CurrentUserId);
        }
        [WebMethod(true)]
        public List<FeedAlert> getAlertList()
        {
            LoggingManager.Debug("Entering getAlertCount - UserFeedService");

            int CurrentUserId = Common.GetLoggedInUserId() ?? 0;

            LoggingManager.Debug("Exiting getAlertCount - UserFeedService");
            return FeedManager.GetFeedsAlerts(CurrentUserId);
        }
    }
}
