using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;
using System.Collections;
using Snovaspace.Util.Logging;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using HtmlAgilityPack;
using System.Web;

namespace Huntable.Business
{
    public static class FeedContentManager
    {
        public static string rightContentPlaceHolder = "$RIGHTCONTENT$";
        #region Main Feed HTML
        public static string getFeed_User_Interest(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();
            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");
            sb.Append(" a new interest - ");
            var interest = obj.FeedUserInterests.OrderByDescending(a => a.Id).FirstOrDefault();
            if (interest != null)
                sb.Append("<a href='#'>" + interest.MasterInterest.Description + "</a>");

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_User_Skill(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();
            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");
            sb.Append(" a new skill - ");
            var interest = obj.FeedUserSkills.OrderByDescending(a => a.Id).FirstOrDefault();
            if (interest != null)
                sb.Append("<a href='#'>" + interest.MasterSkill.Description + "</a>");

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_User_EducationHistory(FeedUserMain obj, int currentUserId)
        {

            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();
            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");
            if (obj.Type == FeedManager.FeedType.User_School.ToString())
                sb.Append(" updated school to ");
            else
                sb.Append(" updated education to ");

            var interest = obj.FeedUserEducationSchools.OrderByDescending(a => a.Id).FirstOrDefault();
            if (interest != null)
            {
                if (obj.Type == FeedManager.FeedType.User_School.ToString())
                    sb.Append("<a href='#'>" + interest.EducationHistory.Institution + "</a>");
                else
                    sb.Append("<a href='#'>" + interest.EducationHistory.Degree + "</a>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_Link_share(FeedUserMain obj, int currentUserId)
        {

            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();
            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User, false, true));

            var rec = obj.FeedUserUserFeeds.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                if (rec.UserFeed.LinkDescription != null)
                    sb.Append("Shared a link");
                if (rec.UserFeed.FeedDesription != null)
                {
                    sb.Append("<div class='user-posted-link'>");
                    sb.Append(rec.UserFeed.FeedDesription);
                    sb.Append("</div>");
                }
                if (rec.UserFeed.LinkDescription != null)
                {
                    sb.Append("<div class='video-comment'>");
                    sb.Append(rec.UserFeed.LinkDescription);
                    sb.Append("</div>");
                }
                //sb.Append("<div class='video-desc video-desc-feed '>");
                //sb.Append("<a href='#' class='accounts-link'> Hey Jude Olympic (Spontaneous 27.07.12) @ Liverpool st Station LONDON</a><br>");
                //sb.Append("<a href='#' class='live-link'>www.youtube.com</a>");
                //sb.Append("<p>");
                //sb.Append("After olympic opening ceremony");
                //sb.Append("After olympic opening ceremony");
                //sb.Append("After olympic opening ceremony");
                //sb.Append("After olympic opening ceremony");
                //sb.Append("After olympic opening ceremony");
                //sb.Append("After olympic opening ceremony");
                //sb.Append("After olympic opening ceremony");
                //sb.Append("</p>");
                //sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_User_EmployementHistory(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));
            HttpContext context = HttpContext.Current;
            string baseUrl = "http://" + context.Request.Url.Authority + context.Request.ApplicationPath;
            StringBuilder sb = new StringBuilder();
            var rec = obj.FeedUserEmployementHistories.OrderByDescending(a => a.Id).FirstOrDefault();
            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");
            if (obj.Type == FeedManager.FeedType.Current_Job.ToString())
                sb.Append(" updated current employment status to ");
            else if (obj.Type == FeedManager.FeedType.Current_Company.ToString())
                sb.Append(" updated current company to ");
            else
            {
                sb.Append(" endorsed ");
                if (rec != null)
                {
                    sb.Append(GetUserLink(rec.EmploymentHistory.UserId, currentUserId, rec.EmploymentHistory.User));
                }
                sb.Append(" for the employment status ");
            }
            if (rec != null)
            {
                string userurl = baseUrl + new UrlGenerator().UserUrlGenerator(rec.EmploymentHistory.UserId);
                if (obj.Type == FeedManager.FeedType.Current_Job.ToString())
                {
                   
                    sb.Append("<a href='"+userurl.ToString()+ "'>" + rec.EmploymentHistory.JobTitle + "</a> at ");
                    var cmp = FeedManager.getCompany(rec.EmploymentHistory.MasterCompany.Description);
                    
                    if (cmp != null)
                    {
                        string companyurl = baseUrl+ new UrlGenerator().CompanyUrlGenerator(cmp.Id);
                        sb.Append("<a href='" + companyurl + "'>" + cmp.CompanyName + "</a>");
                    }
                    else
                        sb.Append(rec.EmploymentHistory.MasterCompany.Description);
                }
                else if (obj.Type == FeedManager.FeedType.Endorsed.ToString())
                    sb.Append("<a href='"+userurl.ToString()+ "'>" + rec.EmploymentHistory.JobTitle + "</a>");
                else
                {
                    var cmp = FeedManager.getCompany(rec.EmploymentHistory.MasterCompany.Description);
                    string companyurl = baseUrl +new UrlGenerator().CompanyUrlGenerator(cmp.Id);
                    if (cmp != null)
                        sb.Append("<a href='\"" + companyurl.ToString() + "\"'>" + cmp.CompanyName + "</a>");
                    else
                        sb.Append(rec.EmploymentHistory.MasterCompany.Description);
                }
            }
            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_Follow(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();
            HttpContext context = HttpContext.Current;
            string baseUrl = "http://" + context.Request.Url.Authority + context.Request.ApplicationPath;
            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" are");
            else
                sb.Append(" is");
            sb.Append(" following ");

            var rec = obj.FeedUserFollows.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                var recCompany = FeedManager.getCompany(rec.FollowingUserId.GetValueOrDefault());
                if (rec.User.IsCompany == true && recCompany != null)
                {
                    string companyurl = baseUrl+ new UrlGenerator().CompanyUrlGenerator(recCompany.Id);
                    if (rec.FollowingUserId == currentUserId)
                        sb.Append(" <a href='\"" + companyurl.ToString() + "\"'>Your Company</a> ");
                    else
                        sb.Append("Company <a href='"+ companyurl+"'>" + rec.User.FirstName + "</a> ");

                    sb.Append("<table class='user-follow'>");
                    sb.Append("<tbody>");
                    sb.Append("<tr>");
                    sb.Append("<td width='42%' valign='top'>");
                    sb.Append("<a href='" + companyurl.ToString() + "'>");
                    sb.Append("<img width='430px' src='" + new FileStoreService().GetDownloadUrlDirect(rec.User.PersonalLogoFileStoreId) + "' alt='Feaured-logo' class='profile-pic profile-pic2'>");
                    sb.Append("</a>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");

                    if (recCompany != null)
                    {
                        sb.Append("<td width='58%' valign='top' align='left'>");
                        sb.Append("<p style='line-height:18px; text-align:justify;'>");
                        sb.Append(" <a href='http://" + recCompany.CompanyWebsite + "' target='_blank' class='accounts-link'>" + recCompany.CompanyWebsite + "</a>");
                        sb.Append("<br>");
                        sb.Append(recCompany.CompanyDescription + "<br>");
                        if (rec.FollowingUserId != currentUserId && !FeedManager.CheckUserFollow(currentUserId, rec.FollowingUserId.GetValueOrDefault(0)))
                            sb.Append("<a id='follow_user_" + rec.FollowingUserId
                                + "' href='javascript:markFollow(" + rec.FollowingUserId + ",\"follow_user_" + rec.FollowingUserId + "\")' class='invite-friend-btn invite-friend-btn-ov'>Follow</a>");
                        sb.Append(" </p>");
                        sb.Append("</td>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                }
                else
                {
                    int recf = Convert.ToInt32(rec.FollowingUserId);
                    string userurl =  baseUrl+ new UrlGenerator().UserUrlGenerator(recf);
                    sb.Append(GetUserLink(rec.FollowingUserId ?? 0, currentUserId, rec.User));

                    sb.Append(" ");

                    sb.Append("<table class='user-follow'>");
                    sb.Append("<tbody>");
                    sb.Append("<tr>");
                    sb.Append("<td width='42%' valign='top'>");
                    sb.Append("<a href='"+userurl.ToString()+ "'>");
                    sb.Append("<img width='204px' src='" + new FileStoreService().GetDownloadUrlDirect(rec.User.PersonalLogoFileStoreId) + "' alt='Feaured-logo' class='profile-pic profile-pic2'>");
                    sb.Append("</a>");
                    sb.Append("</td>");
                    sb.Append("<td width='58%' valign='top' align='left'>");
                    sb.Append("<p style='line-height:18px; text-align:justify;'>");
                    sb.Append(" <a href='"+userurl.ToString() + "' class='accounts-link'>" + rec.User.FirstName + " " + rec.User.LastName + "</a>");
                    sb.Append("<br>");
                    sb.Append(rec.User.CurrentPosition + "<br>");
                    sb.Append(rec.User.City + " (" + rec.User.State + ") " + "<br>");
                    if (rec.FollowingUserId != currentUserId && !FeedManager.CheckUserFollow(currentUserId, rec.FollowingUserId.GetValueOrDefault(0)))
                        sb.Append("<a id='follow_user_" + rec.FollowingUserId
                                + "' href='javascript:markFollow(" + rec.FollowingUserId + ",\"follow_user_" + rec.FollowingUserId + "\")' class='invite-friend-btn invite-friend-btn-ov'>Follow</a>");
                    sb.Append(" </p>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                }
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_Profile_Picture(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            //FeedType.Profile_Picture;
            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");

            sb.Append(" updated Profile Picture");
            sb.Append("<div class='video-comment'>");
            sb.Append("<div class='auto-adjust-imgblock'>");
            sb.Append("<div><a href='javascript:OpenMainPopup(\"/ajax-picture.aspx?"
                + Huntable.Business.FeedManager.ajaxPopup.ProfilePhotoId.ToString() + "=" + obj.UserId + "\")' class='nyroModal'>");
            sb.Append("<img alt='large' width='430px' class='profile-pic' src='" + new FileStoreService().GetDownloadUrlDirect(obj.User.PersonalLogoFileStoreId) + "'>");
            sb.Append("</a>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_Got_Connected(FeedUserMain obj, int currentUserId, bool conn_1st, bool conn_2nd, bool conn_3rd)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));
            HttpContext context1 = HttpContext.Current;
            string baseUrl = "http://" + context1.Request.Url.Authority + context1.Request.ApplicationPath;
            StringBuilder sb = new StringBuilder();

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                if (conn_1st)
                {
                    var user = obj.User.User2;
                    if (user != null)
                    {
                        string userurl = baseUrl+ new UrlGenerator().UserUrlGenerator(obj.UserId);
                        sb.Append(GetUserLink(user.Id, currentUserId, user, true));
                        sb.Append(" friend ");
                        sb.Append("<a href='"+userurl.ToString() + "'>" + obj.User.FirstName + " " + obj.User.LastName + "</a>");
                        sb.Append(" has joined Huntable");
                    }
                }
                else if (conn_2nd)
                {
                    var user = context.Users.FirstOrDefault(a => a.Id == obj.User.User2.ReferralId);
                    if (user != null)
                    {
                        string userurl = baseUrl+ new UrlGenerator().UserUrlGenerator(obj.UserId);
                        sb.Append(GetUserLink(user.Id, currentUserId, user, true));
                        sb.Append(" 2nd connection ");
                        sb.Append("<a href='"+userurl.ToString() + "'>" + obj.User.FirstName + " " + obj.User.LastName + "</a>");
                        sb.Append(" has joined Huntable");
                    }
                }
                else if (conn_3rd)
                {
                    var user = context.Users.FirstOrDefault(a => a.User1.Any(b => b.Id == obj.User.User2.ReferralId));
                    if (user != null)
                    {
                        string userurl = baseUrl+ new UrlGenerator().UserUrlGenerator(obj.UserId);
                        sb.Append(GetUserLink(user.Id, currentUserId, user, true));
                        sb.Append(" 3rd connection ");
                        sb.Append("<a href='" + userurl.ToString() + "'>" + obj.User.FirstName + " " + obj.User.LastName + "</a>");
                        sb.Append(" has joined Huntable");
                    }
                }
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_Job_Post(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");

            sb.Append(" posted a new job opportunity- ");

            var rec = obj.FeedUserJobs.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                int jbid = Convert.ToInt32(rec.JobId);
                sb.Append("<a href='" + new UrlGenerator().JobsUrlGenerator(jbid) + "'>" + rec.Job.Title + "</a>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_Profile_Viewed(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();
            HttpContext context = HttpContext.Current;
            string baseUrl = "http://" + context.Request.Url.Authority + context.Request.ApplicationPath;
            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has ");

            sb.Append(" viewed ");

            var rec = obj.FeedUserVisitors.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                int recf = Convert.ToInt32(rec.VisitorUserId);
                string userurl = baseUrl+ new UrlGenerator().UserUrlGenerator(recf);
                if (rec.VisitorUserId == currentUserId)
                    sb.Append("your");
                else
                    sb.Append("<a href='"+userurl.ToString()+ "'>" + rec.User.FirstName + " " + rec.User.LastName + "</a>'s");
            }
            sb.Append(" profile");

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_User_Potfolio_Photo(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            //FeedType.Company_Video;
            //FeedType.User_Portfolio_video;
            //FeedType.User_Video;
            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");
            var rec = obj.FeedUserPorfolioPhotoes.OrderByDescending(a => a.Id);
            if (rec != null)
            {
                if (rec.Count() > 1)
                    sb.Append(" added pictures to Portfolio");
                else
                    sb.Append(" added picture to Portfolio");
                sb.Append("<div class='video-comment'>");
                sb.Append("<div class='auto-adjust-imgblock'>");
                string width1 = "151px";
                string width2 = "";
                switch (rec.Count())
                {
                    case 1:
                        width1 = "430px";
                        break;
                    case 2:
                    case 3:
                    case 6:
                    case 8:
                    case 9:
                        break;
                    case 4:
                        width1 = "184px";
                        break;
                    case 5:
                        width2 = "222px";
                        width1 = "143px";
                        break;
                    case 7:
                        width1 = "225px";
                        break;

                }
                int cnt = 1;
                foreach (var data in rec)
                {
                    if (width2 == "")
                    {
                        sb.Append("<div><a href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPortfolioPhotoId.ToString()
                            + "=" + data.PortfolioPhotoId + "\")' class='nyroModal'>");
                        sb.Append("<img alt='large' class='profile-pic' width='" + width1 + "' src='" + new FileStoreService().GetDownloadUrlDirect(data.EmploymentHistoryPortfolio.FileId) + "'>");
                        sb.Append("</a>");
                        sb.Append("</div>");
                    }
                    else
                    {
                        string finalwidth = "";
                        if (cnt <= rec.Count() / 2)
                        {
                            finalwidth = width2;
                        }
                        else
                        {
                            finalwidth = width1;
                            width2 = "";
                        }
                        sb.Append("<div><a href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPortfolioPhotoId.ToString()
                            + "=" + data.PortfolioPhotoId + "\")' class='nyroModal'>");
                        sb.Append("<img alt='large' class='profile-pic' width='" + finalwidth + "' src='" + new FileStoreService().GetDownloadUrlDirect(data.EmploymentHistoryPortfolio.FileId) + "'>");
                        sb.Append("</a>");
                        sb.Append("</div>");
                        cnt++;
                    }
                }
                sb.Append("</div>");
                sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_User_Portfolio_video(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            //FeedType.Company_Video;
            //FeedType.User_Portfolio_video;
            //FeedType.User_Video;
            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");
            var rec = obj.FeedUserPortfolioVideos.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                sb.Append(" added video to Portfolio");
                sb.Append("<div class='video-comment'>");
                sb.Append("<div class='auto-adjust-imgblock'>");

                sb.Append("<div><a href='ajax-picture.aspx' class='nyroModal'>");
                sb.Append("<iframe src='" + rec.EmploymentHistoryVideo.VideoURL + "' width='430px' height='350px' ></iframe>");
                //sb.Append("<img alt='large' class='profile-pic' src='" + rec.EmploymentHistoryVideo.VideoURL + "'>");
                sb.Append("</a>");
                sb.Append("</div>");

                sb.Append("</div>");
                sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_User_Photo(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            //FeedType.Company_Video;
            //FeedType.User_Portfolio_video;
            //FeedType.User_Video;
            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");
            var rec = obj.FeedUserPhotoes.OrderByDescending(a => a.Id);
            if (rec != null)
            {
                if (rec.Count() > 1)
                    sb.Append(" added pictures");
                else
                    sb.Append(" added picture");
                sb.Append("<div class='video-comment'>");
                sb.Append("<div class='auto-adjust-imgblock'>");
                string width1 = "151px";
                string width2 = "";
                switch (rec.Count())
                {
                    case 1:
                        width1 = "430px";
                        break;
                    case 2:
                    case 3:
                    case 6:
                    case 8:
                    case 9:
                        break;
                    case 4:
                        width1 = "184px";
                        break;
                    case 5:
                        width2 = "222px";
                        width1 = "143px";
                        break;
                    case 7:
                        width1 = "225px";
                        break;

                }
                int cnt = 1;
                foreach (var data in rec)
                {
                    if (width2 == "")
                    {
                        sb.Append("<div><a href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPhotoId.ToString()
                            + "=" + data.UserPhotoId + "\")' class='nyroModal'>");
                        sb.Append("<img alt='large' class='profile-pic' width='" + width1 + "' src='" + new FileStoreService().GetDownloadUrlDirect(data.UserPortfolio.PictureId) + "'>");
                        sb.Append("</a>");
                        sb.Append("</div>");
                    }
                    else
                    {
                        string finalwidth = "";
                        if (cnt <= Math.Floor((double)rec.Count() / 2))
                        {
                            finalwidth = width2;
                        }
                        else
                        {
                            finalwidth = width1;
                            width2 = "";
                        }
                        sb.Append("<div><a href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPhotoId.ToString()
                            + "=" + data.UserPhotoId + "\")' class='nyroModal'>");
                        sb.Append("<img alt='large' class='profile-pic' width='" + finalwidth + "' src='" + new FileStoreService().GetDownloadUrlDirect(data.UserPortfolio.PictureId) + "'>");
                        sb.Append("</a>");
                        sb.Append("</div>");
                        cnt++;
                    }
                }
                //foreach (var data in rec)
                //{
                //    sb.Append("<div><a href='ajax-picture.aspx' class='nyroModal'>");
                //    sb.Append("<img alt='large' class='profile-pic' width='146px' src='" + new FileStoreService().GetDownloadUrlDirect(data.UserPortfolio.PictureId) + "'>");
                //    sb.Append("</a>");
                //    sb.Append("</div>");
                //}
                sb.Append("</div>");
                sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }

        public static string getFeed_Wall_Picture(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            //FeedType.Company_Video;
            //FeedType.User_Portfolio_video;
            //FeedType.User_Video;
            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");
            var rec = obj.FeedUserPhotoes.FirstOrDefault();
            if (rec != null)
            {
                sb.Append(" added picture");
                sb.Append("<div class='video-comment'>");
                sb.Append("<div class='auto-adjust-imgblock'>");
                string width1 = "430px";
                sb.Append("<p>" + rec.UserPortfolio.PictureDescription + "</p>");
                sb.Append("<div><a href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPhotoId.ToString()
                    + "=" + rec.UserPhotoId + "\")' class='nyroModal'>");
                sb.Append("<img alt='large' class='profile-pic' width='" + width1 + "' src='" + new FileStoreService().GetDownloadUrlDirect(rec.UserPortfolio.PictureId) + "'>");
                sb.Append("</a>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_User_video(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            //FeedType.Company_Video;
            //FeedType.User_Portfolio_video;
            //FeedType.User_Video;
            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");
            var rec = obj.FeedUserVideos.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                sb.Append(" added video");
                sb.Append("<div class='video-comment'>");
                sb.Append("<div class='auto-adjust-imgblock'>");

                sb.Append("<div><a href='ajax-picture.aspx' class='nyroModal'>");
                sb.Append("<iframe src='" + rec.UserVideo.VideoUrl + "' width='430px' height='350px' ></iframe>");
                //sb.Append("<img alt='large' class='profile-pic' src='" + rec.UserVideo.VideoUrl + "'>");
                sb.Append("</a>");
                sb.Append("</div>");

                sb.Append("</div>");
                sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_Company_Portfolio_Photo(FeedUserMain obj, int currentUserId)
        {

            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            //FeedType.Company_Video;
            //FeedType.User_Portfolio_video;
            //FeedType.User_Video;
            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");
            var rec = obj.FeedUserCompanyPhotoes.OrderByDescending(a => a.Id);
            if (rec != null)
            {
                if (rec.Count() > 1)
                    sb.Append(" added pictures to Portfolio");
                else
                    sb.Append(" added picture to Portfolio");
                sb.Append("<div class='video-comment'>");
                sb.Append("<div class='auto-adjust-imgblock'>");
                string width1 = "151px";
                string width2 = "";
                switch (rec.Count())
                {
                    case 1:
                        width1 = "430px";
                        break;
                    case 2:
                    case 3:
                    case 6:
                    case 8:
                    case 9:
                        break;
                    case 4:
                        width1 = "184px";
                        break;
                    case 5:
                        width2 = "222px";
                        width1 = "143px";
                        break;
                    case 7:
                        width1 = "225px";
                        break;

                }
                int cnt = 1;
                foreach (var data in rec)
                {
                    if (width2 == "")
                    {
                        sb.Append("<div><a href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.CompanyPortfolioPhotoId.ToString()
                            + "=" + data.CompanyPhotoId + "\")' class='nyroModal'>");
                        sb.Append("<img alt='large' class='profile-pic' width='" + width1 + "' src='" + new FileStoreService().GetDownloadUrlDirect(data.CompanyPortfolio.PortfolioImageid) + "'>");
                        sb.Append("</a>");
                        sb.Append("</div>");
                    }
                    else
                    {
                        string finalwidth = "";
                        if (cnt <= rec.Count() / 2)
                        {
                            finalwidth = width2;
                        }
                        else
                        {
                            finalwidth = width1;
                            width2 = "";
                        }
                        sb.Append("<div><a href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.CompanyPortfolioPhotoId.ToString()
                            + "=" + data.CompanyPhotoId + "\")' class='nyroModal'>");
                        sb.Append("<img alt='large' class='profile-pic' width='" + finalwidth + "' src='" + new FileStoreService().GetDownloadUrlDirect(data.CompanyPortfolio.PortfolioImageid) + "'>");
                        sb.Append("</a>");
                        sb.Append("</div>");
                        cnt++;
                    }
                }
                //foreach (var data in rec)
                //{
                //    sb.Append("<div><a href='ajax-picture.aspx' class='nyroModal'>");
                //    //sb.Append("<img alt='large' class='profile-pic' src='" + new FileStoreService().GetDownloadUrlDirect(data.UserPortfolio.PictureId) + "'>");
                //    sb.Append("</a>");
                //    sb.Append("</div>");
                //}
                sb.Append("</div>");
                sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_Company_Video(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            //FeedType.Company_Video;
            //FeedType.User_Portfolio_video;
            //FeedType.User_Video;
            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");
            var rec = obj.FeedUserCompanyVideos.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                sb.Append(" added video to Portfolio");
                sb.Append("<div class='video-comment'>");
                sb.Append("<div class='auto-adjust-imgblock'>");
                sb.Append("<div><a href='ajax-picture.aspx' class='nyroModal'>");
                sb.Append("<iframe src='" + rec.CompanyVideo.VideoUrl + "' width='430px' height='350px' ></iframe>");
                //sb.Append("<img alt='large' class='profile-pic' src='" + rec.CompanyVideo.VideoUrl + "'>");
                sb.Append("</a>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getFeed_Company_Product(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            //FeedType.Company_Video;
            //FeedType.User_Portfolio_video;
            //FeedType.User_Video;
            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have");
            else
                sb.Append(" has");
            var rec = obj.FeedUserCompanyProducts.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                sb.Append(" added new product");
                sb.Append("<table class='user-follow'>");
                sb.Append("<tbody>");
                sb.Append("<tr>");
                sb.Append("<td width='42%' valign='top'>");
                sb.Append("<a href='companyproducts.aspx?Id=" + rec.CompanyProduct.ComapnyId + "'>");
                sb.Append("<img width='204px' src='" + new FileStoreService().GetDownloadUrlDirect(rec.CompanyProduct.ProductImageId) + "' alt='Feaured-logo' class='profile-pic profile-pic2'>");
                sb.Append("</a>");
                sb.Append("</td>");
                sb.Append("<td width='58%' valign='top' align='left'>");
                sb.Append("<p style='line-height:18px; text-align:justify;'>");
                sb.Append(rec.CompanyProduct.ProductDescription + "<br>");
                sb.Append(" </p>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        #endregion

        #region Like And Comment HTML
        public static string getLikeComment_Link_share(FeedUserMain obj, FeedUserMain objMain, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getLikeCommentFeedContainer(obj, objMain, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            sb.Append(feedForLike ? " liked " : " commented on ");
            var rec = obj.FeedUserUserFeeds.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                sb.Append(GetUserLink(rec.UserFeed.UserID, currentUserId, rec.UserFeed.User, true));
                sb.Append(" update");

                if (rec.UserFeed.LinkDescription != null)
                    sb.Append("Shared a link");
                if (rec.UserFeed.FeedDesription != null)
                {
                    sb.Append("<div class='user-posted-link'>");
                    sb.Append(rec.UserFeed.FeedDesription);
                    sb.Append("</div>");
                }
                if (rec.UserFeed.LinkDescription != null)
                {
                    sb.Append("<div class='video-comment'>");
                    sb.Append(rec.UserFeed.LinkDescription);
                    sb.Append("</div>");
                }
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getLikeComment_User_Potfolio_Photo(FeedUserMain obj, FeedUserMain objMain, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getLikeCommentFeedContainer(obj, objMain, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            var rec = obj.FeedUserPorfolioPhotoes.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                sb.Append(GetUserLink(rec.EmploymentHistoryPortfolio.EmploymentHistory.UserId, currentUserId, rec.EmploymentHistoryPortfolio.EmploymentHistory.User, true));

                sb.Append(" Portfolio picture");
                sb.Append("<div class='video-comment'>");
                sb.Append("<div class='auto-adjust-imgblock'>");
                string width1 = "151px";
                width1 = "430px";
                var data = rec;
                sb.Append("<div><a href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPortfolioPhotoId.ToString()
                            + "=" + data.PortfolioPhotoId + "\")' class='nyroModal'>");
                sb.Append("<img alt='large' class='profile-pic' width='" + width1 + "' src='" + new FileStoreService().GetDownloadUrlDirect(data.EmploymentHistoryPortfolio.FileId) + "'>");
                sb.Append("</a>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getLikeComment_User_Portfolio_video(FeedUserMain obj, FeedUserMain objMain, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getLikeCommentFeedContainer(obj, objMain, currentUserId));

            StringBuilder sb = new StringBuilder();
            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));


            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");

            var rec = obj.FeedUserPortfolioVideos.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {

                sb.Append(GetUserLink(rec.EmploymentHistoryVideo.EmploymentHistory.UserId, currentUserId, rec.EmploymentHistoryVideo.EmploymentHistory.User, true));
                sb.Append(" video");

                sb.Append("<div class='video-comment'>");
                sb.Append("<div class='auto-adjust-imgblock'>");
                sb.Append("<div><a href='ajax-picture.aspx' class='nyroModal'>");
                sb.Append("<iframe src='" + rec.EmploymentHistoryVideo.VideoURL + "' width='430px' height='350px' ></iframe>");
                //sb.Append("<img alt='large' class='profile-pic' src='" + rec.EmploymentHistoryVideo.VideoURL + "'>");
                sb.Append("</a>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getLikeComment_User_Photo(FeedUserMain obj, FeedUserMain objMain, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getLikeCommentFeedContainer(obj, objMain, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");

            var rec = obj.FeedUserPhotoes.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                sb.Append(GetUserLink(rec.UserPortfolio.UserId, currentUserId, null, true));

                sb.Append(" picture");
                sb.Append("<div class='video-comment'>");
                sb.Append("<div class='auto-adjust-imgblock'>");
                string width1 = "151px";
                width1 = "430px";
                var data = rec;
                sb.Append("<div><a href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPhotoId.ToString()
                            + "=" + data.UserPhotoId + "\")' class='nyroModal'>");
                sb.Append("<img alt='large' class='profile-pic' width='" + width1 + "' src='" + new FileStoreService().GetDownloadUrlDirect(data.UserPortfolio.PictureId) + "'>");
                sb.Append("</a>");
                sb.Append("</div>");

                sb.Append("</div>");
                sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getLikeComment_User_video(FeedUserMain obj, FeedUserMain objMain, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getLikeCommentFeedContainer(obj, objMain, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            var rec = obj.FeedUserVideos.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {

                sb.Append(GetUserLink(rec.UserVideo.UserId ?? 0, currentUserId, null, true));

                sb.Append(" video");
                sb.Append("<div class='video-comment'>");
                sb.Append("<div class='auto-adjust-imgblock'>");
                sb.Append("<div><a href='ajax-picture.aspx' class='nyroModal'>");
                sb.Append("<iframe src='" + rec.UserVideo.VideoUrl + "' width='430px' height='350px' ></iframe>");
                //sb.Append("<img alt='large' class='profile-pic' src='" + rec.UserVideo.VideoUrl + "'>");
                sb.Append("</a>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getLikeComment_Company_Video(FeedUserMain obj, FeedUserMain objMain, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getLikeCommentFeedContainer(obj, objMain, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            var rec = obj.FeedUserCompanyVideos.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                sb.Append(GetUserLink(rec.CompanyVideo.Company.Userid ?? 0, currentUserId, rec.CompanyVideo.Company.User, true));

                sb.Append(" video");
                sb.Append("<div class='video-comment'>");
                sb.Append("<div class='auto-adjust-imgblock'>");
                sb.Append("<div><a href='ajax-picture.aspx' class='nyroModal'>");
                sb.Append("<iframe src='" + rec.CompanyVideo.VideoUrl + "' width='430px' height='350px' ></iframe>");
                //sb.Append("<img alt='large' class='profile-pic' src='" + rec.CompanyVideo.VideoUrl + "'>");
                sb.Append("</a>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getLikeComment_Company_Portfolio_Photo(FeedUserMain obj, FeedUserMain objMain, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getLikeCommentFeedContainer(obj, objMain, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            var rec = obj.FeedUserCompanyPhotoes.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {

                sb.Append(GetUserLink(rec.CompanyPortfolio.Company.Userid ?? 0, currentUserId, rec.CompanyPortfolio.Company.User, true));

                sb.Append(" added picture to Portfolio");
                sb.Append("<div class='video-comment'>");
                sb.Append("<div class='auto-adjust-imgblock'>");
                string width1 = "151px";
                width1 = "430px";
                var data = rec;
                sb.Append("<div><a href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.CompanyPortfolioPhotoId.ToString()
                            + "=" + data.CompanyPhotoId + "\")' class='nyroModal'>");
                sb.Append("<img alt='large' class='profile-pic' width='" + width1 + "' src='" + new FileStoreService().GetDownloadUrlDirect(data.CompanyPortfolio.PortfolioImageid) + "'>");
                sb.Append("</a>");
                sb.Append("</div>");
                sb.Append("</div>");
                sb.Append("</div>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getLikeComment_Main_Feed(FeedUserMain obj, FeedUserMain objMain, int currentUserId, bool feedForLike,
            List<int> retrieveUserList_1st, List<int> retrieveUserList_2nd, List<int> retrieveUserList_3rd)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getLikeCommentFeedContainer(obj, objMain, currentUserId, false));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            var rec = obj.FeedUserMainFeeds.Where(a => a.FeedUserMain.IsDelete != true).OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                sb.Append(GetUserLink(rec.FeedUserMain1.UserId, currentUserId, rec.FeedUserMain1.User, true));
                sb.Append(" update");

                var feed = rec.FeedUserMain1;
                #region Feed Description
                switch ((Huntable.Business.FeedManager.FeedType)Enum.Parse(
                    typeof(Huntable.Business.FeedManager.FeedType), feed.Type))
                {
                    case Huntable.Business.FeedManager.FeedType.User_Interest:
                        sb.Append(FeedContentManager.getFeed_User_Interest(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.User_Skill:
                        sb.Append(FeedContentManager.getFeed_User_Skill(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.User_Education:
                    case Huntable.Business.FeedManager.FeedType.User_School:
                        sb.Append(FeedContentManager.getFeed_User_EducationHistory(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.Feed_Update:
                    case Huntable.Business.FeedManager.FeedType.Link_share:
                        sb.Append(FeedContentManager.getFeed_Link_share(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.Current_Job:
                    case Huntable.Business.FeedManager.FeedType.Current_Company:
                    case Huntable.Business.FeedManager.FeedType.Endorsed:
                        sb.Append(FeedContentManager.getFeed_User_EmployementHistory(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.Follow:
                        sb.Append(FeedContentManager.getFeed_Follow(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.Profile_Picture:
                        sb.Append(FeedContentManager.getFeed_Profile_Picture(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.Got_Connected:
                        sb.Append(FeedContentManager.getFeed_Got_Connected(feed, currentUserId
                            , retrieveUserList_1st.Any(a => a == feed.UserId)
                            , retrieveUserList_2nd.Any(a => a == feed.UserId)
                            , retrieveUserList_3rd.Any(a => a == feed.UserId)));
                        break;
                    case Huntable.Business.FeedManager.FeedType.Job_Post:
                        sb.Append(FeedContentManager.getFeed_Job_Post(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.Profile_Viewed:
                        sb.Append(FeedContentManager.getFeed_Profile_Viewed(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.User_Potfolio_Photo:
                    case Huntable.Business.FeedManager.FeedType.Multiple_User_Potfolio_Photo:
                        sb.Append(FeedContentManager.getFeed_User_Potfolio_Photo(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.User_Portfolio_video:
                        sb.Append(FeedContentManager.getFeed_User_Portfolio_video(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.User_Photo:
                    case Huntable.Business.FeedManager.FeedType.Multiple_User_Photo:
                        sb.Append(FeedContentManager.getFeed_User_Photo(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.Wall_Picture:
                        sb.Append(FeedContentManager.getFeed_Wall_Picture(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.User_Video:
                        sb.Append(FeedContentManager.getFeed_User_video(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.Company_Video:
                        sb.Append(FeedContentManager.getFeed_Company_Video(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.Company_Portfolio_Photo:
                    case Huntable.Business.FeedManager.FeedType.Multiple_Company_Portfolio_Photo:
                        sb.Append(FeedContentManager.getFeed_Company_Portfolio_Photo(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.Company_Product:
                        sb.Append(FeedContentManager.getFeed_Company_Product(feed, currentUserId));
                        break;
                    case Huntable.Business.FeedManager.FeedType.Like_Company_Profile:
                    case Huntable.Business.FeedManager.FeedType.Like_User_Profile:
                        sb.Append(FeedContentManager.getLikeComment_Profile(feed, feed, currentUserId, true));
                        break;
                    case Huntable.Business.FeedManager.FeedType.Like_User_Feed:
                    //sb.Append(FeedContentManager.getLikeComment_Link_share(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_User_Feed:
                    //sb.Append(FeedContentManager.getLikeComment_Link_share(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_User_Portfolio_Photo:
                    //sb.Append(FeedContentManager.getLikeComment_User_Potfolio_Photo(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_User_Portfolio_Photo:
                    //sb.Append(FeedContentManager.getLikeComment_User_Potfolio_Photo(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_User_Portfolio_Video:
                    //sb.Append(FeedContentManager.getLikeComment_User_Portfolio_video(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_User_Portfolio_Video:
                    //sb.Append(FeedContentManager.getLikeComment_User_Portfolio_video(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_User_Photo:
                    //sb.Append(FeedContentManager.getLikeComment_User_Photo(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_User_Photo:
                    //sb.Append(FeedContentManager.getLikeComment_User_Photo(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_User_Video:
                    //sb.Append(FeedContentManager.getLikeComment_User_video(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_User_Video:
                    //sb.Append(FeedContentManager.getLikeComment_User_video(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_Company_Video:
                    //sb.Append(FeedContentManager.getLikeComment_Company_Video(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_Company_Video:
                    //sb.Append(FeedContentManager.getLikeComment_Company_Video(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_Company_Portfolio_Photo:
                    //sb.Append(FeedContentManager.getLikeComment_Company_Portfolio_Photo(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_Company_Portfolio_Photo:
                    //sb.Append(FeedContentManager.getLikeComment_Company_Portfolio_Photo(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_Comment:
                    case Huntable.Business.FeedManager.FeedType.Like_Main_Feed:
                    case Huntable.Business.FeedManager.FeedType.Comment_Main_Feed:
                    //sb.Append(" " + feed.Type);
                    //break;
                    //sb.Append(FeedContentManager.getLikeComment_Profile(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_Company_Product:
                    //sb.Append(FeedContentManager.getLikeComment_Company_Product(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_Company_Product:
                    //sb.Append(FeedContentManager.getLikeComment_Company_Product(feed, currentUserId, false));
                    //break;
                    default:
                        sb.Append(FeedContentManager.getFeed_Common_Content(feed, currentUserId));
                        break;
                }
                #endregion
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }

        public static string getLikeComment_Profile(FeedUserMain obj, FeedUserMain objMain, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getLikeCommentFeedContainer(obj, objMain, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));


            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");

            var rec = obj.FeedUserProfiles.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                sb.Append(GetUserLink(rec.ProfileUserId ?? 0, currentUserId, rec.User, true));
            }
            sb.Append(" profile");

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getLikeComment_Company_Product(FeedUserMain obj, FeedUserMain objMain, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getLikeCommentFeedContainer(obj, objMain, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));


            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");

            var rec = obj.FeedUserCompanyProducts.OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                sb.Append(GetUserLink(rec.CompanyProduct.Company.Userid ?? 0, currentUserId, rec.CompanyProduct.Company.User, true));

                if (obj.UserId == currentUserId)
                    sb.Append(" have");
                else
                    sb.Append(" has");
                sb.Append(" added new product");
                sb.Append("<table class='user-follow'>");
                sb.Append("<tbody>");
                sb.Append("<tr>");
                sb.Append("<td width='42%' valign='top'>");
                sb.Append("<a href='companyproducts.aspx?Id=" + rec.CompanyProduct.ComapnyId + "'>");
                sb.Append("<img width='204px' src='" + new FileStoreService().GetDownloadUrlDirect(rec.CompanyProduct.ProductImageId) + "' alt='Feaured-logo' class='profile-pic profile-pic2'>");
                sb.Append("</a>");
                sb.Append("</td>");
                sb.Append("<td width='58%' valign='top' align='left'>");
                sb.Append("<p style='line-height:18px; text-align:justify;'>");
                sb.Append(rec.CompanyProduct.ProductDescription + "<br>");
                sb.Append(" </p>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        #endregion

        #region Alert HTML
        public static string getAlert_Link_share(FeedUserMain obj, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getAlertFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " update.");

            //var rec = obj.FeedUserUserFeeds.OrderByDescending(a => a.Id).FirstOrDefault();
            //if (rec != null)
            //{
            //    sb.Append(GetUserLink(rec.UserFeed.UserID, currentUserId, rec.UserFeed.User, true));
            //    sb.Append(" update");

            //    if (rec.UserFeed.LinkDescription != null)
            //        sb.Append("Shared a link");
            //    if (rec.UserFeed.FeedDesription != null)
            //    {
            //        sb.Append("<div class='user-posted-link'>");
            //        sb.Append(rec.UserFeed.FeedDesription);
            //        sb.Append("</div>");
            //    }
            //    if (rec.UserFeed.LinkDescription != null)
            //    {
            //        sb.Append("<div class='video-comment'>");
            //        sb.Append(rec.UserFeed.LinkDescription);
            //        sb.Append("</div>");
            //    }
            //}

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getAlert_User_Potfolio_Photo(FeedUserMain obj, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getAlertFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " portfolio picture.");
            //var rec = obj.FeedUserPorfolioPhotoes.OrderByDescending(a => a.Id).FirstOrDefault();
            //if (rec != null)
            //{
            //    sb.Append(GetUserLink(rec.EmploymentHistoryPortfolio.EmploymentHistory.UserId, currentUserId, rec.EmploymentHistoryPortfolio.EmploymentHistory.User, true));

            //    sb.Append(" Portfolio picture");
            //    sb.Append("<div class='video-comment'>");
            //    sb.Append("<div class='auto-adjust-imgblock'>");
            //    string width1 = "151px";
            //    width1 = "430px";
            //    var data = rec;
            //    sb.Append("<div><a href='javascript:OpenMainPopup(\"ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPortfolioPhotoId.ToString()
            //                + "=" + data.PortfolioPhotoId + "\")' class='nyroModal'>");
            //    sb.Append("<img alt='large' class='profile-pic' width='" + width1 + "' src='" + new FileStoreService().GetDownloadUrlDirect(data.EmploymentHistoryPortfolio.FileId) + "'>");
            //    sb.Append("</a>");
            //    sb.Append("</div>");
            //    sb.Append("</div>");
            //    sb.Append("</div>");
            //}

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getAlert_User_Portfolio_video(FeedUserMain obj, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getAlertFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();
            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));


            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " portfolio video.");

            //var rec = obj.FeedUserPortfolioVideos.OrderByDescending(a => a.Id).FirstOrDefault();
            //if (rec != null)
            //{

            //    sb.Append(GetUserLink(rec.EmploymentHistoryVideo.EmploymentHistory.UserId, currentUserId, rec.EmploymentHistoryVideo.EmploymentHistory.User, true));
            //    sb.Append(" video");

            //    sb.Append("<div class='video-comment'>");
            //    sb.Append("<div class='auto-adjust-imgblock'>");
            //    sb.Append("<div><a href='ajax-picture.aspx' class='nyroModal'>");
            //    sb.Append("<iframe src='" + rec.EmploymentHistoryVideo.VideoURL + "' width='430px' height='350px' ></iframe>");
            //    //sb.Append("<img alt='large' class='profile-pic' src='" + rec.EmploymentHistoryVideo.VideoURL + "'>");
            //    sb.Append("</a>");
            //    sb.Append("</div>");
            //    sb.Append("</div>");
            //    sb.Append("</div>");
            //}

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getAlert_User_Photo(FeedUserMain obj, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getAlertFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " picture.");

            //var rec = obj.FeedUserPhotoes.OrderByDescending(a => a.Id).FirstOrDefault();
            //if (rec != null)
            //{
            //    sb.Append(GetUserLink(rec.UserPortfolio.UserId, currentUserId, null, true));

            //    sb.Append(" picture");
            //    sb.Append("<div class='video-comment'>");
            //    sb.Append("<div class='auto-adjust-imgblock'>");
            //    string width1 = "151px";
            //    width1 = "430px";
            //    var data = rec;
            //    sb.Append("<div><a href='javascript:OpenMainPopup(\"ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPhotoId.ToString()
            //                + "=" + data.UserPhotoId + "\")' class='nyroModal'>");
            //    sb.Append("<img alt='large' class='profile-pic' width='" + width1 + "' src='" + new FileStoreService().GetDownloadUrlDirect(data.UserPortfolio.PictureId) + "'>");
            //    sb.Append("</a>");
            //    sb.Append("</div>");

            //    sb.Append("</div>");
            //    sb.Append("</div>");
            //}

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getAlert_User_video(FeedUserMain obj, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getAlertFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " video.");
            //var rec = obj.FeedUserVideos.OrderByDescending(a => a.Id).FirstOrDefault();
            //if (rec != null)
            //{

            //    sb.Append(GetUserLink(rec.UserVideo.UserId ?? 0, currentUserId, null, true));

            //    sb.Append(" video");
            //    sb.Append("<div class='video-comment'>");
            //    sb.Append("<div class='auto-adjust-imgblock'>");
            //    sb.Append("<div><a href='ajax-picture.aspx' class='nyroModal'>");
            //    sb.Append("<iframe src='" + rec.UserVideo.VideoUrl + "' width='430px' height='350px' ></iframe>");
            //    //sb.Append("<img alt='large' class='profile-pic' src='" + rec.UserVideo.VideoUrl + "'>");
            //    sb.Append("</a>");
            //    sb.Append("</div>");
            //    sb.Append("</div>");
            //    sb.Append("</div>");
            //}

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getAlert_Company_Video(FeedUserMain obj, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getAlertFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " video.");
            //var rec = obj.FeedUserCompanyVideos.OrderByDescending(a => a.Id).FirstOrDefault();
            //if (rec != null)
            //{
            //    sb.Append(GetUserLink(rec.CompanyVideo.Company.Userid ?? 0, currentUserId, rec.CompanyVideo.Company.User, true));

            //    sb.Append(" video");
            //    sb.Append("<div class='video-comment'>");
            //    sb.Append("<div class='auto-adjust-imgblock'>");
            //    sb.Append("<div><a href='ajax-picture.aspx' class='nyroModal'>");
            //    sb.Append("<iframe src='" + rec.CompanyVideo.VideoUrl + "' width='430px' height='350px' ></iframe>");
            //    //sb.Append("<img alt='large' class='profile-pic' src='" + rec.CompanyVideo.VideoUrl + "'>");
            //    sb.Append("</a>");
            //    sb.Append("</div>");
            //    sb.Append("</div>");
            //    sb.Append("</div>");
            //}

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getAlert_Company_Portfolio_Photo(FeedUserMain obj, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getAlertFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " portfolio picture.");
            //var rec = obj.FeedUserCompanyPhotoes.OrderByDescending(a => a.Id).FirstOrDefault();
            //if (rec != null)
            //{

            //    sb.Append(GetUserLink(rec.CompanyPortfolio.Company.Userid ?? 0, currentUserId, rec.CompanyPortfolio.Company.User, true));

            //    sb.Append(" added picture to Portfolio");
            //    sb.Append("<div class='video-comment'>");
            //    sb.Append("<div class='auto-adjust-imgblock'>");
            //    string width1 = "151px";
            //    width1 = "430px";
            //    var data = rec;
            //    sb.Append("<div><a href='javascript:OpenMainPopup(\"ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.CompanyPortfolioPhotoId.ToString()
            //                + "=" + data.CompanyPhotoId + "\")' class='nyroModal'>");
            //    sb.Append("<img alt='large' class='profile-pic' width='" + width1 + "' src='" + new FileStoreService().GetDownloadUrlDirect(data.CompanyPortfolio.PortfolioImageid) + "'>");
            //    sb.Append("</a>");
            //    sb.Append("</div>");
            //    sb.Append("</div>");
            //    sb.Append("</div>");
            //}

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getAlert_Main_Feed(FeedUserMain obj, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getAlertFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));

            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            var rec = obj.FeedUserMainFeeds.Where(a => a.FeedUserMain.IsDelete != true).OrderByDescending(a => a.Id).FirstOrDefault();
            if (rec != null)
            {
                var feed = rec.FeedUserMain1;
                #region Feed Description
                switch ((Huntable.Business.FeedManager.FeedType)Enum.Parse(
                    typeof(Huntable.Business.FeedManager.FeedType), feed.Type))
                {
                    case Huntable.Business.FeedManager.FeedType.User_Interest:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " interest update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.User_Skill:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " skill update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.User_Education:
                    case Huntable.Business.FeedManager.FeedType.User_School:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " education update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Feed_Update:
                    case Huntable.Business.FeedManager.FeedType.Link_share:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Current_Job:
                    case Huntable.Business.FeedManager.FeedType.Current_Company:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " employment update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Endorsed:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " endorsement update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Follow:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " follow update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Profile_Picture:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " profile picture.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Got_Connected:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " new connection update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Job_Post:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " job post.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Profile_Viewed:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " profile visit.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.User_Potfolio_Photo:
                    case Huntable.Business.FeedManager.FeedType.Multiple_User_Potfolio_Photo:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " portfolio picture update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.User_Portfolio_video:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " portfolio video update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.User_Photo:
                    case Huntable.Business.FeedManager.FeedType.Multiple_User_Photo:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " picture update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Wall_Picture:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " picture update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.User_Video:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " video update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Company_Video:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " video update.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Company_Portfolio_Photo:
                    case Huntable.Business.FeedManager.FeedType.Multiple_Company_Portfolio_Photo:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " portfolio photo.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Company_Product:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " product.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Like_Company_Profile:
                    case Huntable.Business.FeedManager.FeedType.Like_User_Profile:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " profile.");
                        break;
                    case Huntable.Business.FeedManager.FeedType.Like_User_Feed:
                    //sb.Append(FeedContentManager.getAlert_Link_share(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_User_Feed:
                    //sb.Append(FeedContentManager.getAlert_Link_share(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_User_Portfolio_Photo:
                    //sb.Append(FeedContentManager.getAlert_User_Potfolio_Photo(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_User_Portfolio_Photo:
                    //sb.Append(FeedContentManager.getAlert_User_Potfolio_Photo(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_User_Portfolio_Video:
                    //sb.Append(FeedContentManager.getAlert_User_Portfolio_video(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_User_Portfolio_Video:
                    //sb.Append(FeedContentManager.getAlert_User_Portfolio_video(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_User_Photo:
                    //sb.Append(FeedContentManager.getAlert_User_Photo(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_User_Photo:
                    //sb.Append(FeedContentManager.getAlert_User_Photo(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_User_Video:
                    //sb.Append(FeedContentManager.getAlert_User_video(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_User_Video:
                    //sb.Append(FeedContentManager.getAlert_User_video(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_Company_Video:
                    //sb.Append(FeedContentManager.getAlert_Company_Video(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_Company_Video:
                    //sb.Append(FeedContentManager.getAlert_Company_Video(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_Company_Portfolio_Photo:
                    //sb.Append(FeedContentManager.getAlert_Company_Portfolio_Photo(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_Company_Portfolio_Photo:
                    //sb.Append(FeedContentManager.getAlert_Company_Portfolio_Photo(feed, currentUserId, false));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_Comment:
                    case Huntable.Business.FeedManager.FeedType.Like_Main_Feed:
                    case Huntable.Business.FeedManager.FeedType.Comment_Main_Feed:
                    //sb.Append(" " + feed.Type);
                    //break;
                    //sb.Append(FeedContentManager.getAlert_Profile(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Like_Company_Product:
                    //sb.Append(FeedContentManager.getAlert_Company_Product(feed, currentUserId, true));
                    //break;
                    case Huntable.Business.FeedManager.FeedType.Comment_Company_Product:
                    //sb.Append(FeedContentManager.getAlert_Company_Product(feed, currentUserId, false));
                    //break;
                    default:
                        sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " " + feed.Type);
                        break;
                }
                #endregion
            }

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }

        public static string getAlert_Profile(FeedUserMain obj, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getAlertFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));


            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");

            //var rec = obj.FeedUserProfiles.OrderByDescending(a => a.Id).FirstOrDefault();
            //if (rec != null)
            //{
            //    sb.Append(GetUserLink(rec.ProfileUserId ?? 0, currentUserId, rec.User, true));
            //}
            sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " profile.");

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getAlert_Company_Product(FeedUserMain obj, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getAlertFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));


            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " product.");

            //var rec = obj.FeedUserCompanyProducts.OrderByDescending(a => a.Id).FirstOrDefault();
            //if (rec != null)
            //{
            //    sb.Append(GetUserLink(rec.CompanyProduct.Company.Userid ?? 0, currentUserId, rec.CompanyProduct.Company.User, true));

            //    if (obj.UserId == currentUserId)
            //        sb.Append(" have");
            //    else
            //        sb.Append(" has");
            //    sb.Append(" added new product");
            //    sb.Append("<table class='user-follow'>");
            //    sb.Append("<tbody>");
            //    sb.Append("<tr>");
            //    sb.Append("<td width='42%' valign='top'>");
            //    sb.Append("<a href='companyproducts.aspx?Id=" + rec.CompanyProduct.ComapnyId + "'>");
            //    sb.Append("<img width='204px' src='" + new FileStoreService().GetDownloadUrlDirect(rec.CompanyProduct.ProductImageId) + "' alt='Feaured-logo' class='profile-pic profile-pic2'>");
            //    sb.Append("</a>");
            //    sb.Append("</td>");
            //    sb.Append("<td width='58%' valign='top' align='left'>");
            //    sb.Append("<p style='line-height:18px; text-align:justify;'>");
            //    sb.Append(rec.CompanyProduct.ProductDescription + "<br>");
            //    sb.Append(" </p>");
            //    sb.Append("</td>");
            //    sb.Append("</tr>");
            //    sb.Append("</tbody>");
            //    sb.Append("</table>");
            //}

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getAlert_Like_Comment(FeedUserMain obj, FeedUserMain objComment, int currentUserId, bool feedForLike)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getAlertFeedContainer(obj, currentUserId));

            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));


            if (feedForLike)
                sb.Append(" liked ");
            else
                sb.Append(" commented on ");
            if (obj.Type == Huntable.Business.FeedManager.FeedType.Like_Comment.ToString())
            {
                if (objComment != null)
                {
                    sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " comment on ");
                    var user = FeedManager.getUser(objComment.OwnerUserId??0);
                    sb.Append(GetAlertUserLink(objComment.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, user) + " update.");
                }
                else
                {
                    sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " comment on ");
                    sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " update.");
                }
            }
            else
                sb.Append(GetAlertUserLink(obj.OwnerUserId ?? 0, obj.UserId, currentUserId, obj.User, obj.User1) + " update.");

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }
        public static string getAlertFeedContainer(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<li id='feed_" + obj.Id + "' onclick='displayFeed(" + obj.Id + ")'>");
            sb.Append(getLeftUserPhoto(currentUserId, obj.User, 42));

            sb.Append("<div  class='name-date'>");
            sb.Append("<div  class='name'>");
            sb.Append(rightContentPlaceHolder);
            sb.Append("</div>");
            sb.Append("<span  class='date'>" + GetTimeLine(obj.Date) + "</span>");
            sb.Append("</div>");

            sb.Append("</li>");
            return sb.ToString();
        }

        public static string GetAlertUserLink(int ownerUserId, int feedUserId, int currentUserId, User objFeedUser, User objOwnerUser)//, bool useRelation = false, bool useOnlyName = false)//,int ownerId = 0)
        {
            StringBuilder sb = new StringBuilder();
            HttpContext context = HttpContext.Current;
            string baseUrl = "http://" + context.Request.Url.Authority + context.Request.ApplicationPath;
           
            if (ownerUserId > 0 && objOwnerUser!=null)
            {
                if (feedUserId == ownerUserId)
                {
                    #region OwnerUser

                    if (objFeedUser != null)
                    {
                        if (objFeedUser.IsCompany ?? false)
                        {
                            sb.Append("<a href='javascript:openLink(\"CompanyView.aspx\")'>Own</a>");
                        }
                        else
                        {
                            sb.Append("<a href='javascript:openLink(\"VisualCV.aspx\")'>Own</a>");
                        }
                    }
                    #endregion
                }
                else if (ownerUserId == currentUserId)
                {
                    #region OwnerUser

                    if (objOwnerUser != null)
                    {
                        if (objOwnerUser.IsCompany ?? false)
                        {
                            sb.Append("<a href='javascript:openLink(\"CompanyView.aspx\")'>Your</a>");
                        }
                        else
                        {
                            sb.Append("<a href='javascript:openLink(\"VisualCV.aspx\")'>Your</a>");
                        }
                    }
                    #endregion
                }
                else
                {
                    if (objOwnerUser.IsCompany ?? false)
                    {
                        var recCompany = FeedManager.getCompany(ownerUserId);
                        string companyurl = baseUrl + new UrlGenerator().CompanyUrlGenerator(recCompany.Id);
                        sb.Append("<a href='javascript:openLink(\"" + companyurl.ToString() + "\")'>" + objOwnerUser.FirstName + "</a>'s");
                    }
                    else
                    {
                        string userurl = baseUrl+ new UrlGenerator().UserUrlGenerator(objOwnerUser.Id);
                        sb.Append("<a href='javascript:openLink(\""+userurl.ToString() + "\")'>" + objOwnerUser.FirstName + " " + objOwnerUser.LastName + "</a>'s");
                    }
                }
            }
            else if (feedUserId == currentUserId)
            {
                #region FeedUser

                if (objFeedUser != null)
                {
                    if (objFeedUser.IsCompany ?? false)
                    {
                        sb.Append("<a href='javascript:openLink(\"CompanyView.aspx\")'>Your</a>");
                    }
                    else
                    {
                        sb.Append("<a href='javascript:openLink(\"VisualCV.aspx\")'>Your</a>");
                    }
                }
                #endregion
            }
            else
            {
                if (objFeedUser.IsCompany ?? false)
                {
                    var recCompany = FeedManager.getCompany(feedUserId);
                    string companyurl = baseUrl + new UrlGenerator().CompanyUrlGenerator(recCompany.Id);
                    sb.Append("<a href='javascript:openLink(\"" + companyurl.ToString() + "\")'>" + objFeedUser.FirstName + "</a>'s");
                }
                else
                {
                    string userurl = baseUrl + new UrlGenerator().UserUrlGenerator(objFeedUser.Id);
                    sb.Append("<a href='javascript:openLink(\"" + userurl.ToString() + "\")'>" + objFeedUser.FirstName + " " + objFeedUser.LastName + "</a>'s");
                }
            }
            return sb.ToString();
        }
        #endregion

        public static string getFeed_Common_Content(FeedUserMain obj, int currentUserId)
        {
            StringBuilder sbMain = new StringBuilder();
            sbMain.Append(getFeedContainer(obj, currentUserId));

            //FeedType.Company_Video;
            //FeedType.User_Portfolio_video;
            //FeedType.User_Video;
            StringBuilder sb = new StringBuilder();

            sb.Append(GetUserLink(obj.UserId, currentUserId, obj.User));
            if (obj.UserId == currentUserId)
                sb.Append(" have ");
            else
                sb.Append(" has ");

            sb.Append(obj.Type);

            sbMain = sbMain.Replace(rightContentPlaceHolder, sb.ToString());
            return sbMain.ToString();
        }

        public static string getFeed_Link_share_Profile(FeedUserMain obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='all-feeds-list'>");

            //sb.Append(getLeftUserPhoto(obj.User.PersonalLogoFileStoreId));

            sb.Append("<div class='feed-right'>");
            sb.Append("<a href='#'>Kelly Retica</a>Shared a<a href='#'> link</a>");
            sb.Append("<table class='user-follow'>");
            sb.Append("<tbody><tr>");
            sb.Append("<td width='18%' valign='top'><a href='#'>");
            sb.Append("<img src='images/profile-thumb-large.jpg' alt='Feaured-logo' class='profile-pic profile-pic2'></a></td>");
            sb.Append("<td width='82%' valign='top' align='left'><p style='line-height:18px;'> <a href='#' class='accounts-link'>Ruben Daniel</a><br>");
            sb.Append("Loyola College of Engineering<br>");
            sb.Append("<strong>Chennai (Madras)</strong><br>");
            sb.Append("<a href='#' class='invite-friend-btn invite-friend-btn-ov'>Follow</a> </p></td>");
            sb.Append("</tr>");
            sb.Append("</tbody></table>");
            //sb.Append(getLikeComment(obj));
            sb.Append("</div>");

            sb.Append("</div>");
            return sb.ToString();
        }

        public static string getFeedContainer(FeedUserMain obj, int currentUserId, bool includeLikeComment = true)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='all-feeds-list' id='feed_" + obj.Id + "'>");

            sb.Append("<div class='feed-left'>");
            sb.Append(getLeftUserPhoto(currentUserId, obj.User));
            sb.Append("</div>");

            sb.Append("<div class='feed-right'> ");

            if (obj.UserId == currentUserId)
                sb.Append("<a href='javascript:deleteFeed(" + obj.Id + ",\"feed_" + obj.Id + "\")' class='hide'> ");
            else
                sb.Append("<a href='javascript:hideFeed(" + obj.Id + ",\"feed_" + obj.Id + "\")' class='hide'> ");
            //sb.Append("&nbsp;&nbsp;&nbsp;");
            sb.Append("<img alt='' src='images/close_icon.png'/> ");
            sb.Append("</a>");

            sb.Append(rightContentPlaceHolder);
            if (includeLikeComment)
                sb.Append(getLikeComment(obj, currentUserId));
            sb.Append("</div>");

            sb.Append("</div>");
            return sb.ToString();
        }
        public static string getLikeCommentFeedContainer(FeedUserMain obj, FeedUserMain objMain, int currentUserId, bool includeLikeComment = true)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='all-feeds-list' id='feed_" + objMain.Id + "'>");

            sb.Append("<div class='feed-left'>");
            sb.Append(getLeftUserPhoto(currentUserId, obj.User));
            sb.Append("</div>");

            sb.Append("<div class='feed-right'> ");

            if (obj.UserId == currentUserId)
                sb.Append("<a href='javascript:deleteFeed(" + obj.Id + ",\"feed_" + objMain.Id + "\")' class='hide'> ");
            else
                sb.Append("<a href='javascript:hideFeed(" + obj.Id + ",\"feed_" + objMain.Id + "\")' class='hide'> ");
            //sb.Append("&nbsp;&nbsp;&nbsp;");
            sb.Append("<img alt='' src='images/close_icon.png'/> ");
            sb.Append("</a>");

            sb.Append(rightContentPlaceHolder);
            if (includeLikeComment)
                sb.Append(getLikeComment(objMain, currentUserId));
            sb.Append("</div>");

            sb.Append("</div>");
            return sb.ToString();
        }
        public static string getLikeComment(FeedUserMain obj, int currentUserId)
        {
            FeedLikeDetail objLike = new FeedLikeDetail();
            FeedCommentDetail objComment = new FeedCommentDetail();

            int oldestFeedId = 0;
            int Pagesize = 2;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                #region Feed type
                switch ((FeedManager.FeedType)Enum.Parse(typeof(FeedManager.FeedType), obj.Type))
                {
                    case FeedManager.FeedType.User_Interest:
                    case FeedManager.FeedType.User_Skill:
                    case FeedManager.FeedType.User_Education:
                    case FeedManager.FeedType.User_School:
                    case FeedManager.FeedType.Current_Job:
                    case FeedManager.FeedType.Current_Company:
                    case FeedManager.FeedType.Follow:
                    case FeedManager.FeedType.Profile_Picture:
                    case FeedManager.FeedType.Got_Connected:
                    case FeedManager.FeedType.Endorsed:
                    case FeedManager.FeedType.Job_Post:
                    case FeedManager.FeedType.Profile_Viewed:
                    case FeedManager.FeedType.Multiple_User_Potfolio_Photo:
                    case FeedManager.FeedType.Multiple_User_Photo:
                    case FeedManager.FeedType.Multiple_Company_Portfolio_Photo:
                    case FeedManager.FeedType.Like_Company_Profile:
                    case FeedManager.FeedType.Like_User_Profile:
                        objLike = FeedManager.GetFeedLikes(obj.Id, currentUserId, FeedManager.FeedType.Like_Main_Feed, obj.Id);
                        objComment = FeedManager.GetFeedComments(obj.Id, obj.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_Main_Feed, obj.Id);
                        break;
                    case FeedManager.FeedType.Feed_Update:
                    case FeedManager.FeedType.Link_share:
                    case FeedManager.FeedType.Like_User_Feed:
                    case FeedManager.FeedType.Comment_User_Feed:
                        {
                            var rec = context.FeedUserUserFeeds.FirstOrDefault(a => a.FeedId == obj.Id);
                            if (rec != null)
                            {
                                int refId = rec.UserFeedId ?? 0;
                                objLike = FeedManager.GetFeedLikes(obj.Id, currentUserId, FeedManager.FeedType.Like_User_Feed, refId);
                                objComment = FeedManager.GetFeedComments(obj.Id, obj.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_User_Feed, refId);
                            }
                        }
                        break;
                    case FeedManager.FeedType.User_Potfolio_Photo:
                    case FeedManager.FeedType.Like_User_Portfolio_Photo:
                    case FeedManager.FeedType.Comment_User_Portfolio_Photo:
                        {
                            var rec = context.FeedUserPorfolioPhotoes.FirstOrDefault(a => a.FeedId == obj.Id);
                            if (rec != null)
                            {
                                int refId = rec.PortfolioPhotoId ?? 0;
                                objLike = FeedManager.GetFeedLikes(obj.Id, currentUserId, FeedManager.FeedType.Like_User_Portfolio_Photo, refId);
                                objComment = FeedManager.GetFeedComments(obj.Id, obj.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_User_Portfolio_Photo, refId);
                            }
                        }
                        break;
                    case FeedManager.FeedType.User_Portfolio_video:
                    case FeedManager.FeedType.Like_User_Portfolio_Video:
                    case FeedManager.FeedType.Comment_User_Portfolio_Video:
                        {
                            var rec = context.FeedUserPortfolioVideos.FirstOrDefault(a => a.FeedId == obj.Id);
                            if (rec != null)
                            {
                                int refId = rec.PortfolioVideoId ?? 0;
                                objLike = FeedManager.GetFeedLikes(obj.Id, currentUserId, FeedManager.FeedType.Like_User_Portfolio_Video, refId);
                                objComment = FeedManager.GetFeedComments(obj.Id, obj.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_User_Portfolio_Video, refId);
                            }
                        }
                        break;
                    case FeedManager.FeedType.Wall_Picture:
                    case FeedManager.FeedType.User_Photo:
                    case FeedManager.FeedType.Like_User_Photo:
                    case FeedManager.FeedType.Comment_User_Photo:
                        {
                            var rec = context.FeedUserPhotoes.FirstOrDefault(a => a.FeedId == obj.Id);
                            if (rec != null)
                            {
                                int refId = rec.UserPhotoId ?? 0;
                                objLike = FeedManager.GetFeedLikes(obj.Id, currentUserId, FeedManager.FeedType.Like_User_Photo, refId);
                                objComment = FeedManager.GetFeedComments(obj.Id, obj.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_User_Photo, refId);
                            }
                        }
                        break;
                    case FeedManager.FeedType.User_Video:
                    case FeedManager.FeedType.Like_User_Video:
                    case FeedManager.FeedType.Comment_User_Video:
                        {
                            var rec = context.FeedUserVideos.FirstOrDefault(a => a.FeedId == obj.Id);
                            if (rec != null)
                            {
                                int refId = rec.UserVideoId ?? 0;
                                objLike = FeedManager.GetFeedLikes(obj.Id, currentUserId, FeedManager.FeedType.Like_User_Video, refId);
                                objComment = FeedManager.GetFeedComments(obj.Id, obj.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_User_Video, refId);
                            }
                        }
                        break;
                    case FeedManager.FeedType.Company_Video:
                    case FeedManager.FeedType.Like_Company_Video:
                    case FeedManager.FeedType.Comment_Company_Video:
                        {
                            var rec = context.FeedUserCompanyVideos.FirstOrDefault(a => a.FeedId == obj.Id);
                            if (rec != null)
                            {
                                int refId = rec.CompanyVideoId ?? 0;
                                objLike = FeedManager.GetFeedLikes(obj.Id, currentUserId, FeedManager.FeedType.Like_Company_Video, refId);
                                objComment = FeedManager.GetFeedComments(obj.Id, obj.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_Company_Video, refId);
                            }
                        }
                        break;
                    case FeedManager.FeedType.Company_Portfolio_Photo:
                    case FeedManager.FeedType.Like_Company_Portfolio_Photo:
                    case FeedManager.FeedType.Comment_Company_Portfolio_Photo:
                        {
                            var rec = context.FeedUserCompanyPhotoes.FirstOrDefault(a => a.FeedId == obj.Id);
                            if (rec != null)
                            {
                                int refId = rec.CompanyPhotoId ?? 0;
                                objLike = FeedManager.GetFeedLikes(obj.Id, currentUserId, FeedManager.FeedType.Like_Company_Portfolio_Photo, refId);
                                objComment = FeedManager.GetFeedComments(obj.Id, obj.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_Company_Portfolio_Photo, refId);
                            }
                        }
                        break;
                    case FeedManager.FeedType.Like_Comment:
                    case FeedManager.FeedType.Like_Main_Feed:
                    case FeedManager.FeedType.Comment_Main_Feed:
                        {
                            var rec = context.FeedUserMainFeeds.FirstOrDefault(a => a.FeedId == obj.Id && a.FeedUserMain.IsDelete != true);
                            if (rec != null)
                            {
                                int refId = rec.ReferencedFeedId ?? 0;
                                objLike = FeedManager.GetFeedLikes(obj.Id, currentUserId, FeedManager.FeedType.Like_Main_Feed, refId);
                                objComment = FeedManager.GetFeedComments(obj.Id, obj.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_Main_Feed, refId);
                            }
                        }
                        break;
                    case FeedManager.FeedType.Company_Product:
                    case FeedManager.FeedType.Like_Company_Product:
                    case FeedManager.FeedType.Comment_Company_Product:
                        {
                            var rec = context.FeedUserCompanyProducts.FirstOrDefault(a => a.FeedId == obj.Id);
                            if (rec != null)
                            {
                                int refId = rec.CompanyProductId ?? 0;
                                objLike = FeedManager.GetFeedLikes(obj.Id, currentUserId, FeedManager.FeedType.Like_Company_Product, refId);
                                objComment = FeedManager.GetFeedComments(obj.Id, obj.UserId, currentUserId, oldestFeedId, Pagesize, FeedManager.FeedType.Comment_Company_Product, refId);
                            }
                        }
                        break;
                }
                #endregion
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='like-portion'>");
            sb.Append(objLike.LikeLinkHTML);

            sb.Append(getCommentLink(obj.Id));

            sb.Append("<span>" + GetTimeLine(obj.Date) + "</span>");
            sb.Append("<input type='hidden' id='hdnfeed_comment_oldest' value='0'/>");
            sb.Append("<input type='hidden' id='hdnfeed_comment_latest' value='0'/>");
            sb.Append("<input type='hidden' id='hdnfeed_Id'/>");

            #region Like section
            if (objLike.LikeHeader != "")
            {
                sb.Append("<div class='comments-head comments-head-like' id='feed_like_container_" + obj.Id + "'>");
                sb.Append(objLike.LikeHeader);
                sb.Append("</div>");
            }
            else
            {
                sb.Append("<div class='comments-head comments-head-like' id='feed_like_container_" + obj.Id + "' style='display:none;'>");
                sb.Append("</div>");
            }
            #endregion

            sb.Append("<div class='comments' id='feed_comment_container_" + obj.Id + "' style='display:none;'>");

            //if (objComment.CommentHeader != "")
            //{
            sb.Append("<div class='comments-head' style='display:none;'>");
            sb.Append(objComment.CommentHeader);
            sb.Append("</div>");
            //}
            sb.Append("</div>");
            sb.Append("<div class='comments' id='feed_comment_new_" + obj.Id + "'>");
            sb.Append("</div>");

            #region Comment section
            //if(objComment.TotalCommentCount>0)
            //{
            //    //sb.Append("<div class='comments'>");

            //#region comment View link
            //    //if (objComment.DisplayedCommentCount < objComment.TotalCommentCount)
            //    //{
            //    //    sb.Append("<div class='comments-head'>");
            //    //    sb.Append("<img width='15' height='15' src='images/comments-icon.png' alt='comments'><a href='#' onclick='"+objComment.CommentLinkClick+"'>View previous comments(showing " +
            //    //            objComment.DisplayedCommentCount + " out of " + objComment.TotalCommentCount + ")</a>");
            //    //    sb.Append("</div>");
            //    //}
            //#endregion

            ////sb.Append(getComment());
            ////sb.Append(getComment());

            //#region comment textarea            
            ////sb.Append("<div class='comments-desc'>");
            ////sb.Append("<div class='comments-desc-left comments-desc-left-new'>");
            ////sb.Append("<a href='#'>");
            ////sb.Append("<img width='46' height='45' src='images/profile-thumb-small.jpg' alt='img'>");
            ////sb.Append("</a>");
            ////sb.Append("</div>");
            ////sb.Append("<div class='comments-desc-right'>");
            ////sb.Append("<textarea onblur='if(this.value=='')this.value=this.defaultValue;' onfocus='if(this.value==this.defaultValue)this.value='';' class='textarea-profile textarea-comment textarea-comment-pr'>Write a comment...</textarea> <br><br>");
            ////sb.Append("</div>");
            ////sb.Append("</div>");
            //#endregion


            ////sb.Append("</div>");
            //}
            #endregion

            sb.Append("</div>");
            return sb.ToString();
        }

        public static string getLikeLink(int feedId, int cnt, bool isLiked, string likeType, int refId)
        {
            StringBuilder sb = new StringBuilder();
            string onclick = "MarkLike(" + feedId + ",\"" + likeType + "\"," + refId + ")";
            if (isLiked)
                onclick = "MarkUnlike(" + feedId + ",\"" + likeType + "\"," + refId + ")";

            sb.Append("<a style='margin-left:0px;' href='javascript:" + onclick + "' id='feed_link_like_" + feedId + "'>");
            sb.Append("<img width='13' height='12' src='../../images/icon-like1.png' alt='Like'>");
            if (cnt > 0)
                sb.Append("<label id='feed_total_like_" + feedId + "' style='display:none;'>" + cnt.ToString() + "</label>" + " ");
            if (isLiked)
                sb.Append("Unlike");
            else
                sb.Append("Like");
            sb.Append("</a> ");

            return sb.ToString();
        }
        public static string getLikeHeader(int cnt, bool isLiked, int FeedId, string FeedType, int RefRecordId)
        {
            StringBuilder sb = new StringBuilder();
            if (cnt > 0)
            {
                sb.Append("<img width='13' height='12' src='../../images/icon-like1.png' alt='Like'>");
                if (isLiked)
                {
                    cnt = cnt - 1;
                    sb.Append("<a class='accounts-link poplight' href='#'>You</a> ");
                    if (cnt > 0)
                    {
                        sb.Append("and ");
                        sb.Append("<a class='accounts-link poplight nyroModal' href='javascript:OpenLikePopup(\"/ajax-like.aspx?FeedId="
                            + FeedId + "&FeedType=" + FeedType + "&RefRecordId=" + RefRecordId + "\")'>" + cnt.ToString() + " others</a> ");
                    }
                }
                else
                {
                    sb.Append("<a class='accounts-link poplight nyroModal' href='javascript:OpenLikePopup(\"/ajax-like.aspx?FeedId="
                            + FeedId + "&FeedType=" + FeedType + "&RefRecordId=" + RefRecordId + "\")'>" + cnt.ToString() + " people</a> ");
                }
                sb.Append("like this");
            }
            return sb.ToString();
        }

        public static string getCommentLink(int feedId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<a href='#' id='feed_link_comment_" + feedId + "'>");
            sb.Append("<img width='13' height='12' src='../../images/icon-comment.png' alt='Like'>");
            sb.Append("<label id='feed_total_comment_" + feedId + "'></label>");
            sb.Append("Comment");
            sb.Append("</a>");
            return sb.ToString();
        }
        public static string getCommentHeaderLink(int feedId, int feedUserId, int OldestFeedId, Huntable.Business.FeedManager.FeedType type, int refRecordId, int DisplayedCommentCount, int TotalCommentCount)
        {
            StringBuilder sb = new StringBuilder();
            if (DisplayedCommentCount < TotalCommentCount)
            {
                int pagesize = 50;
                sb.Append("<img width='15' height='15' src='../../images/comments-icon.png' alt='comments'><a href='javascript:"
                        + "GetPreviousComments(" + feedId + "," + feedUserId + "," + OldestFeedId + ",\"" + type.ToString() + "\"," + refRecordId + "," + pagesize + ")"
                        + "'>View previous comments(showing " +
                      DisplayedCommentCount + " out of " + TotalCommentCount + ")</a>");
            }
            return sb.ToString();
        }
        public static List<FeedDetail> getComments(List<FeedComment> list, int currentUserId)
        {
            List<FeedDetail> objresult = new List<FeedDetail>();
            foreach (var data in list)
            {
                objresult.Add(getComment(data, currentUserId));
            }
            return objresult;
        }
        public static FeedDetail getComment(FeedComment obj, int currentUserId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='comments-desc' id='comment_" + obj.feedId + "'>");

            sb.Append("<div class='comments-desc-left comments-desc-left-new'>");

            var user = FeedManager.getUser(obj.userId);
            sb.Append(getLeftUserPhoto(currentUserId, user, 46));

            sb.Append("</div>");

            sb.Append("<div class='comments-desc-right'>");

            if (obj.userId == currentUserId || obj.mainFeedUserId == currentUserId)
            {
                sb.Append("<a href='javascript:deleteFeed(" + obj.feedId + ",\"comment_" + obj.feedId + "\")' class='hide'> ");
                //sb.Append("&nbsp;&nbsp;&nbsp;");
                sb.Append("<img alt='' src='images/close_icon.png'/> ");
                sb.Append("</a>");
            }
            sb.Append(GetUserLink(obj.userId, currentUserId, user, false, true));

            sb.Append("<p>" + obj.comments + "</p>");
            sb.Append("<span><strong>" + GetTimeLine(obj.date) + "</strong>");


            //sb.Append(" <a href='#'>Like</a></span>");
            sb.Append(getCommentLikeLink(obj.feedId, currentUserId));
            sb.Append("</div>");
            sb.Append("</div>");

            FeedDetail objresult = new FeedDetail();
            objresult.feedDescription = sb.ToString();
            objresult.feedId = obj.feedId;
            objresult.MainfeedId = 0;
            return objresult;
        }
        public static string getCommentLikeLink(int feedId, int currentUserId)
        {
            int cnt = FeedManager.GetCommentLikes(feedId);
            bool isLiked = FeedManager.GetIfCommentLiked(currentUserId, feedId);
            StringBuilder sb = new StringBuilder();
            string onclick = "MarkCommentLike(\"" + feedId + "\",\"feed_comment_like_\")";
            if (isLiked)
                onclick = "MarkCommentUnlike(\"" + feedId + "\",\"feed_comment_like_\")";

            sb.Append("<span id='feed_comment_like_" + feedId + "'> ");
            sb.Append("<a style='margin-left:0px;' href='javascript:" + onclick + "' id='feed_comment_like_" + feedId + "'>");
            if (isLiked)
                sb.Append("Unlike");
            else
                sb.Append("Like");
            sb.Append("</a> ");
            if (cnt > 0)
            {
                sb.Append("<img width='13' height='12' src='../../images/icon-like1.png' alt='Like'>");
                sb.Append("<label id='feed_total_like_" + feedId + "'>" + cnt.ToString() + "</label>" + " ");
            }
            sb.Append("</span> ");
            return sb.ToString();
        }
        public static string GetTimeLine(DateTime dt)
        {
            TimeSpan time = DateTime.Now.Subtract(dt);

            if (time.Seconds <= 0)
            {
                return "few second(s) ago";
            }
            else if (time.Days > 30)
            {
                return time.Days / 30 + " month" + (time.Days / 30 >= 2 ? "s " : " ") + "ago";
            }
            else if (time.Days > 0)
            {
                return time.Days + " days(s) ago";
            }
            else if (time.Hours > 0)
            {
                return time.Hours + " hour(s) ago";
            }
            else if (time.Minutes > 0)
            {
                return time.Minutes + " minute(s) ago";
            }
            else if (time.Seconds > 5)
            {
                return time.Seconds + " second(s) ago";
            }
            else
            {
                return "few fecond(s) ago";
            }
        }

        public static string GetUserLink(int userId, int currentUserId, User obj, bool useRelation = false, bool useOnlyName = false)//,int ownerId = 0)
        {
            StringBuilder sb = new StringBuilder();
            HttpContext context = HttpContext.Current;
            string baseUrl = "http://" + context.Request.Url.Authority + context.Request.ApplicationPath;
           
           
            if (obj != null)
            {
                if (obj.IsCompany ?? false)
                {
                    var recCompany = FeedManager.getCompany(userId);
                    if (recCompany != null)
                    {
                        string companyurl = baseUrl + new UrlGenerator().CompanyUrlGenerator(recCompany.Id);


                        if (!useRelation)
                        {
                            if (userId == currentUserId && !useOnlyName)
                                sb.Append("<a href='javascript:openLink(\"CompanyView.aspx\")'>You</a>");
                            else
                                sb.Append("<a href='javascript:openLink(\"" + companyurl.ToString() + "\")'>" + obj.FirstName + "</a>");
                        }
                        else
                        {
                            if (userId == currentUserId && !useOnlyName)
                            {
                                sb.Append("<a href='javascript:openLink(\"CompanyView.aspx\")'>Your</a>");
                            }
                            //else if (ownerId!=0 && userId == ownerId && !useOnlyName)
                            //{
                            //    sb.Append("<a href='javascript:openLink(\"CompanyView.aspx\")'>Own</a>");
                            //}
                            else
                                sb.Append("<a href='javascript:openLink(\"" + companyurl.ToString() + "\")'>" + obj.FirstName + "</a>'s");
                        }
                    }
                }
                else
                {
                    string userurl = baseUrl + new UrlGenerator().UserUrlGenerator(obj.Id);
                    if (!useRelation)
                    {
                        if (userId == currentUserId && !useOnlyName)
                            sb.Append("<a href='javascript:openLink(\"VisualCV.aspx\")'>You</a>");
                        else
                            sb.Append("<a href='javascript:openLink(\""+ userurl.ToString() + "\")'>" + obj.FirstName + " " + obj.LastName + "</a>");
                    }
                    else
                    {
                        if (userId == currentUserId && !useOnlyName)
                        {
                            sb.Append("<a href='javascript:openLink(\"VisualCV.aspx\")'>Your</a>");
                        }
                        //else if (ownerId!=0 && userId == ownerId && !useOnlyName)
                        //{
                        //    sb.Append("<a href='javascript:openLink(\"CompanyView.aspx\")'>Own</a>");
                        //}
                        else
                            sb.Append("<a href='javascript:openLink(\"" +userurl.ToString()+"\")'>" + obj.FirstName + " " + obj.LastName + "</a>'s");
                    }
                }
            }
            return sb.ToString();
        }
        public static string getLeftUserPhoto(int currentUserId, User obj, int PhotoWidth = 76)
        {
            StringBuilder sb = new StringBuilder();
            HttpContext context = HttpContext.Current;
            string baseUrl = "http://" + context.Request.Url.Authority + context.Request.ApplicationPath;
            if (obj != null)
            {
                if (obj.IsCompany ?? false)
                {
                    var recCompany = FeedManager.getCompany(obj.Id);
                    string companyurl = baseUrl + new UrlGenerator().CompanyUrlGenerator(recCompany.Id);
                    if (obj.Id == currentUserId)
                        sb.Append("<a href='javascript:openLink(\"CompanyView.aspx\")'>");
                    else
                        sb.Append("<a href='javascript:openLink(\"" + companyurl.ToString() + "\")'>");

                }
                else
                {
                    string userurl = baseUrl+ new UrlGenerator().UserUrlGenerator(obj.Id);
                    if (obj.Id == currentUserId)
                        sb.Append("<a href='javascript:openLink(\"VisualCV.aspx\")'>");
                    else
                        sb.Append("<a href='javascript:openLink(\""+userurl.ToString() + "\")'>");
                }
            }
            sb.Append("<img width='" + PhotoWidth + "' class='profile-pic' src='"
                + new FileStoreService().GetDownloadUrlDirect(obj.PersonalLogoFileStoreId) + "' alt='feeds-img'>");
            sb.Append("</a>");
            return sb.ToString();
        }

        #region Popup Detail
        public static string getOwnerDetail(int currentUserId, User obj)
        {
            LoggingManager.Debug("Entering getOwnerDetail - FeedContentManager");
            StringBuilder sb = new StringBuilder();
            HttpContext context = HttpContext.Current;
            string baseUrl = "http://" + context.Request.Url.Authority + context.Request.ApplicationPath;
            string strLink = "";
            string strName = "";
            if (obj != null)
            {
                if (obj.IsCompany ?? false)
                {
                    var recCompany = FeedManager.getCompany(obj.Id);
                    string companyurl = baseUrl+ new UrlGenerator().CompanyUrlGenerator(recCompany.Id);
                    if (obj.Id == currentUserId)
                        strLink = "<a href='javascript:openLink(\"CompanyView.aspx\")'>";
                    else
                        strLink = "<a href='javascript:openLink(\"" + companyurl.ToString() + "\")'>";

                    strName = obj.FirstName;
                }
                else
                {
                    string userurl = baseUrl + new UrlGenerator().UserUrlGenerator(obj.Id);
                    if (obj.Id == currentUserId)
                        strLink = "<a href='javascript:openLink(\"VisualCV.aspx\")'>";
                    else
                        strLink = "<a href='javascript:openLink(\""+userurl.ToString()+ "\")'>";
                    strName = obj.FirstName + " " + obj.LastName;
                }
            }
            sb.Append("<div class='want-to-follow-list-left'>");
            sb.Append(strLink);
            sb.Append("<img alt='Invite-friends-img' class='profile-pic profile-pic2' width='70px' src='" +
                 new FileStoreService().GetDownloadUrlDirect(obj.PersonalLogoFileStoreId) + "'></a>");
            sb.Append("</div>");
            sb.Append("<div class='want-to-follow-list-middle' style='width: 155px;'>");
            sb.Append("<strong>");
            sb.Append(strLink);
            sb.Append(strName);
            sb.Append("</a></strong>");
            var rec = obj.EmploymentHistories.FirstOrDefault(a => a.IsCurrent);
            if (rec != null)
            {
                sb.Append("<p>");
                sb.Append(rec.JobTitle);
                sb.Append("</p>");
            }
            sb.Append("</div>");
            sb.Append("<div style='float: left;' class='want-to-follow-list-right'>");
            if (obj.Id != currentUserId && !FeedManager.CheckUserFollow(currentUserId, obj.Id))
                sb.Append("<a style='float: left;' class='button-yellow' id='follow_user_" + obj.Id
                    + "' href='javascript:markFollow(" + obj.Id + ",\"follow_user_" + obj.Id + "\")'>Follow +</a>");
            sb.Append("</div>");
            LoggingManager.Debug("Exiting getOwnerDetail - FeedContentManager");
            return sb.ToString();
        }
        public static List<string> getLikedUser(int feedId, int currentUserId, List<FeedUserMain> objFeeds)
        {
            LoggingManager.Debug("Entering getLikedUser - FeedContentManager");
            List<string> objResult = new List<string>();
            HttpContext context = HttpContext.Current;
            string baseUrl = "http://" + context.Request.Url.Authority + context.Request.ApplicationPath;
            int lastId = 0;
            foreach (var feed in objFeeds)
            {
                #region List
                StringBuilder sb = new StringBuilder();
                string strLink = "";
                string strName = "";
                if (feed.User.IsCompany ?? false)
                {
                    var recCompany = FeedManager.getCompany(feed.User.Id);
                    string companyurl = baseUrl+ new UrlGenerator().CompanyUrlGenerator(recCompany.Id);
                    if (feed.User.Id == currentUserId)
                        strLink = "<a href='javascript:openLink(\"CompanyView.aspx\")'>";
                    else
                        strLink = "<a href='javascript:openLink(\"" + companyurl.ToString() + "\")'>";

                    strName = feed.User.FirstName;
                }
                else
                {
                    string userurl = baseUrl + new UrlGenerator().UserUrlGenerator(feed.User.Id);
                    if (feed.User.Id == currentUserId)
                        strLink = "<a href='javascript:openLink(\"VisualCV.aspx\")'>";
                    else
                        strLink = "<a href='javascript:openLink(\""+userurl.ToString()+"\")'>";
                    strName = feed.User.FirstName + " " + feed.User.LastName;
                }

                sb.Append("<div class='want-to-follow-list'>");
                sb.Append("<div class='want-to-follow-list-left'>");
                sb.Append(strLink);
                sb.Append("<img alt='Invite-friends-img' class='profile-pic profile-pic2' width='46px' src='" +
                     new FileStoreService().GetDownloadUrlDirect(feed.User.PersonalLogoFileStoreId) + "'></a>");
                sb.Append("</div>");
                sb.Append("<div class='want-to-follow-list-middle'>");
                sb.Append("<strong>");
                sb.Append(strLink);
                sb.Append(strName);
                sb.Append("</a></strong>");
                sb.Append("<p>");
                var rec = feed.User.EmploymentHistories.FirstOrDefault(a => a.IsCurrent == true);
                if (rec != null)
                {
                    string userurl = baseUrl+ new UrlGenerator().UserUrlGenerator(rec.UserId);
                    //sb.Append("<a href='JobView.aspx?JobId=" + rec.Id + "' class='accounts-link'>" + rec.JobTitle + "</a> at ");
                    sb.Append("<a href='javascript:openLink(\""+userurl.ToString() + "\")' class='accounts-link'>" + rec.JobTitle + "</a> at ");

                    var cmp = FeedManager.getCompany(rec.MasterCompany.Description);
                    if (cmp != null)
                    {
                    string companyurl =baseUrl+ new UrlGenerator().CompanyUrlGenerator(cmp.Id);
                        sb.Append("<a href='javascript:openLink(\"" + companyurl.ToString() + "\")'>" + cmp.CompanyName + "</a>");
                    }
                    else
                        sb.Append(rec.MasterCompany.Description);

                    //sb.Append("<a href='companyview.aspx?Id=" + rec.CompanyId + "' class='accounts-link'>" + rec.MasterCompany.Description + "</a>");

                }
                sb.Append("</p>");
                sb.Append("</div>");
                sb.Append("<div class='want-to-follow-list-right'>");
                //sb.Append("<a class='invite-friend-btn' href='#'>Follow +</a>");

                if (feed.User.Id != currentUserId && !FeedManager.CheckUserFollow(currentUserId, feed.User.Id))
                    sb.Append("<a style='float: left;' class='invite-friend-btn' id='follow_user_" + feed.User.Id
                        + "' href='javascript:markFollow(" + feed.User.Id + ",\"follow_user_" + feed.User.Id + "\")'>Follow +</a>");
                sb.Append("</div>");
                sb.Append("</div>");
                objResult.Add(sb.ToString());
                #endregion
                lastId = feed.Id;
            }
            if (lastId > 0)
            {
                objResult.Add("<input type='button' value='Show more' class='show-more' onclick='GetLikedUserList(" + lastId + ")'");
            }
            return objResult;
            LoggingManager.Debug("Exiting getLikedUser - FeedContentManager");
        }
        public static string GetImage(int? imageId)
        {
            return "<img width='500px' alt='cake' src='" + new FileStoreService().GetDownloadUrlDirect(imageId) + "'>";
        }
        public static string GetVideo(string VideoUrl)
        {
            return "<iframe src='" + VideoUrl + "' width='500px' height='300px' ></iframe>";
        }
        #endregion
        #region User Like pictures and videos
        public static UserLikeDetail GetUserPorfolioPhotoesDetail(int feedId, EmploymentHistoryPortfolio objPhoto, LikeComment objDetail, string width)
        {
            UserLikeDetail objResult = new UserLikeDetail();
            StringBuilder sbDetail = new StringBuilder();
            string onclick = "MarkPhotoLike(" + feedId + ",\"" + FeedManager.FeedType.User_Potfolio_Photo + "\"," + objPhoto.Id + ")";
            if (objDetail.IsLikedByCurrentUser)
                onclick = "MarkPhotoUnlike(" + feedId + ",\"" + FeedManager.FeedType.User_Potfolio_Photo + "\"," + objPhoto.Id + ")";

            sbDetail.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPortfolioPhotoId.ToString()
                            + "=" + objPhoto.Id + "\")'>");
            sbDetail.Append("<img width='" + width + "' alt='large' class='profile-pic' src='" + new FileStoreService().GetDownloadUrlDirect(objPhoto.FileId) + "' />");
            sbDetail.Append("</a>");
            objResult.feedId = feedId;
            objResult.detailHTML = sbDetail.ToString();

            StringBuilder sbLike = new StringBuilder();
            sbLike.Append("<span id='photo_detail_" + feedId + "'> ");
            sbLike.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPortfolioPhotoId.ToString()
                            + "=" + objPhoto.Id + "\")'>");
            sbLike.Append("<img width='13' height='12' alt='Like' src='../../images/icon-like1.png'>Like");
            sbLike.Append("</a>");
            sbLike.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPortfolioPhotoId.ToString()
                            + "=" + objPhoto.Id + "\")'>");
            sbLike.Append("<img width='13' height='12' alt='Like' src='../../images/icon-comment.png'><label id='feed_total_comment_2167'></label>Comment");
            sbLike.Append("</a>");

            //sbLike.Append(getLikeLink(feedId, objDetail.TotalLikes, objDetail.IsLikedByCurrentUser,
            //    FeedManager.FeedType.Like_User_Portfolio_Photo.ToString(), objPhoto.Id));
            //sbLike.Append(getCommentLink(feedId));
            sbLike.Append("</span> ");
            //if (objDetail.TotalLikes > 0)
            //{
            //    sbLike.Append("<img width='13' height='12' src='images/icon-like1.png' alt='Like'>");
            //    sbLike.Append("<label id='photo_total_like_" + feedId + "'>" + objDetail.TotalLikes.ToString() + "</label>" + " ");
            //}
            //sbLike.Append("<a style='margin-left:0px;' href='javascript:" + onclick + "' id='feed_link_like_" + feedId + "'>");
            //if (objDetail.IsLikedByCurrentUser)
            //    sbLike.Append("Unlike");
            //else
            //    sbLike.Append("Like");
            //sbLike.Append("</a> ");
            //if (objDetail.TotalComments > 0)
            //{
            //    sbLike.Append("<img width='13' height='12' src='images/comments-icon.png' alt='Like'>");
            //    sbLike.Append("<label id='photo_total_comment_" + feedId + "'>" + objDetail.TotalComments.ToString() + "</label>" + " ");
            //}
            //sbDetail.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPortfolioPhotoId.ToString()
            //                + "=" + objPhoto.Id + "\")'>");
            //sbLike.Append("Comment");
            //sbLike.Append("</a> ");
            objResult.actionHTML = sbLike.ToString();
            return objResult;
        }
        public static UserLikeDetail GetCompanyPortfolioPhotoDetail(int feedId, CompanyPortfolio objPhoto, LikeComment objDetail, string width)
        {
            UserLikeDetail objResult = new UserLikeDetail();
            StringBuilder sbDetail = new StringBuilder();
            string onclick = "MarkPhotoLike(" + feedId + ",\"" + FeedManager.FeedType.Company_Portfolio_Photo + "\"," + objPhoto.Id + ")";
            if (objDetail.IsLikedByCurrentUser)
                onclick = "MarkPhotoUnlike(" + feedId + ",\"" + FeedManager.FeedType.Company_Portfolio_Photo + "\"," + objPhoto.Id + ")";

            sbDetail.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.CompanyPortfolioPhotoId.ToString()
                            + "=" + objPhoto.Id + "\")'>");
            sbDetail.Append("<img width='" + width + "' alt='large' class='profile-pic' src='" + new FileStoreService().GetDownloadUrlDirect(objPhoto.PortfolioImageid) + "' />");
            sbDetail.Append("</a>");
            objResult.feedId = feedId;
            objResult.detailHTML = sbDetail.ToString();

            StringBuilder sbLike = new StringBuilder();
            sbLike.Append("<span id='photo_detail_" + feedId + "'> ");
            sbLike.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.CompanyPortfolioPhotoId.ToString()
                            + "=" + objPhoto.Id + "\")'>");
            sbLike.Append("<img width='13' height='12' alt='Like' src='../../images/icon-like1.png'>Like");
            sbLike.Append("</a>");
            sbLike.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.CompanyPortfolioPhotoId.ToString()
                            + "=" + objPhoto.Id + "\")'>");
            sbLike.Append("<img width='13' height='12' alt='Like' src='../../images/icon-comment.png'><label id='feed_total_comment_2167'></label>Comment");
            sbLike.Append("</a>");
            //sbLike.Append(getLikeLink(feedId, objDetail.TotalLikes, objDetail.IsLikedByCurrentUser,
            //    FeedManager.FeedType.Like_Company_Portfolio_Photo.ToString(), objPhoto.Id));
            //sbLike.Append(getCommentLink(feedId));
            //if (objDetail.TotalLikes > 0)
            //{
            //    sbLike.Append("<img width='13' height='12' src='images/icon-like1.png' alt='Like'>");
            //    sbLike.Append("<label id='photo_total_like_" + feedId + "'>" + objDetail.TotalLikes.ToString() + "</label>" + " ");
            //}
            //sbLike.Append("<a style='margin-left:0px;' href='javascript:" + onclick + "' id='feed_link_like_" + feedId + "'>");
            //if (objDetail.IsLikedByCurrentUser)
            //    sbLike.Append("Unlike");
            //else
            //    sbLike.Append("Like");
            //sbLike.Append("</a> ");

            //if (objDetail.TotalComments > 0)
            //{
            //    sbLike.Append("<img width='13' height='12' src='images/comments-icon.png' alt='Like'>");
            //    sbLike.Append("<label id='photo_total_comment_" + feedId + "'>" + objDetail.TotalComments.ToString() + "</label>" + " ");
            //}
            //sbDetail.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.CompanyPortfolioPhotoId.ToString()
            //                + "=" + objPhoto.Id + "\")'>");
            //sbLike.Append("Comment");
            //sbLike.Append("</a> ");
            sbLike.Append("</span> ");
            objResult.actionHTML = sbLike.ToString();
            return objResult;
        }
        public static UserLikeDetail GetUserPhotoDetail(int feedId, UserPortfolio objPhoto, LikeComment objDetail, string width)
        {
            UserLikeDetail objResult = new UserLikeDetail();
            StringBuilder sbDetail = new StringBuilder();
            string onclick = "MarkPhotoLike(" + feedId + ",\"" + FeedManager.FeedType.User_Photo + "\"," + objPhoto.Id + ")";
            if (objDetail.IsLikedByCurrentUser)
                onclick = "MarkPhotoUnlike(" + feedId + ",\"" + FeedManager.FeedType.User_Photo + "\"," + objPhoto.Id + ")";

            sbDetail.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPhotoId.ToString()
                            + "=" + objPhoto.Id + "\")'>");
            sbDetail.Append("<img width='" + width + "' alt='large' class='profile-pic' src='" + new FileStoreService().GetDownloadUrlDirect(objPhoto.PictureId) + "' />");
            sbDetail.Append("</a>");
            objResult.feedId = feedId;
            objResult.detailHTML = sbDetail.ToString();

            StringBuilder sbLike = new StringBuilder();
            sbLike.Append("<span id='photo_detail_" + feedId + "'> ");
            sbLike.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPhotoId.ToString()
                            + "=" + objPhoto.Id + "\")'>");
            sbLike.Append("<img width='13' height='12' alt='Like' src='../../images/icon-like1.png'>Like");
            sbLike.Append("</a>");
            sbLike.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPhotoId.ToString()
                            + "=" + objPhoto.Id + "\")'>");
            sbLike.Append("<img width='13' height='12' alt='Like' src='../../images/icon-comment.png'><label id='feed_total_comment_2167'></label>Comment");
            sbLike.Append("</a>");
            //sbLike.Append(getLikeLink(feedId, objDetail.TotalLikes, objDetail.IsLikedByCurrentUser,
            //    FeedManager.FeedType.Like_User_Photo.ToString(), objPhoto.Id));
            //sbLike.Append(getCommentLink(feedId));
            //if (objDetail.TotalLikes > 0)
            //{
            //    sbLike.Append("<img width='13' height='12' src='images/icon-like1.png' alt='Like'>");
            //    sbLike.Append("<label id='photo_total_like_" + feedId + "'>" + objDetail.TotalLikes.ToString() + "</label>" + " ");
            //}
            //sbLike.Append("<a style='margin-left:0px;' href='javascript:" + onclick + "' id='feed_link_like_" + feedId + "'>");
            //if (objDetail.IsLikedByCurrentUser)
            //    sbLike.Append("Unlike");
            //else
            //    sbLike.Append("Like");
            //sbLike.Append("</a> ");

            //if (objDetail.TotalComments > 0)
            //{
            //    sbLike.Append("<img width='13' height='12' src='images/comments-icon.png' alt='Like'>");
            //    sbLike.Append("<label id='photo_total_comment_" + feedId + "'>" + objDetail.TotalComments.ToString() + "</label>" + " ");
            //}
            //sbDetail.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPhotoId.ToString()
            //                + "=" + objPhoto.Id + "\")'>");
            //sbLike.Append("Comment");
            //sbLike.Append("</a> ");
            sbLike.Append("</span> ");
            objResult.actionHTML = sbLike.ToString();
            return objResult;
        }
        public static UserLikeDetail GetUserProfilePhotoDetail(int feedId, User objPhoto, LikeComment objDetail, string width)
        {
            UserLikeDetail objResult = new UserLikeDetail();
            StringBuilder sbDetail = new StringBuilder();
            string onclick = "MarkPhotoLike(" + feedId + ",\"" + FeedManager.FeedType.Like_Main_Feed + "\"," + feedId + ")";
            if (objDetail.IsLikedByCurrentUser)
                onclick = "MarkPhotoUnlike(" + feedId + ",\"" + FeedManager.FeedType.Like_Main_Feed + "\"," + feedId + ")";

            sbDetail.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPortfolioPhotoId.ToString()
                            + "=" + objPhoto.Id + "\")'>");
            sbDetail.Append("<img width='" + width + "' alt='large' class='profile-pic' src='" + new FileStoreService().GetDownloadUrlDirect(objPhoto.PersonalLogoFileStoreId) + "' />");
            sbDetail.Append("</a>");
            objResult.feedId = feedId;
            objResult.detailHTML = sbDetail.ToString();

            StringBuilder sbLike = new StringBuilder();
            sbLike.Append("<span id='photo_detail_" + feedId + "'> ");
            sbLike.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.ProfilePhotoId.ToString()
                            + "=" + objPhoto.Id + "\")'>");
            sbLike.Append("<img width='13' height='12' alt='Like' src='../../images/icon-like1.png'>Like");
            sbLike.Append("</a>");
            sbLike.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"/ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.ProfilePhotoId.ToString()
                            + "=" + objPhoto.Id + "\")'>");
            sbLike.Append("<img width='13' height='12' alt='Like' src='../../images/icon-comment.png'><label id='feed_total_comment_2167'></label>Comment");
            sbLike.Append("</a>");
            //sbLike.Append(getLikeLink(feedId, objDetail.TotalLikes, objDetail.IsLikedByCurrentUser,
            //    FeedManager.FeedType.Like_Main_Feed.ToString(), feedId));
            //sbLike.Append(getCommentLink(feedId));
            //if (objDetail.TotalLikes > 0)
            //{
            //    sbLike.Append("<img width='13' height='12' src='images/icon-like1.png' alt='Like'>");
            //    sbLike.Append("<label id='photo_total_like_" + feedId + "'>" + objDetail.TotalLikes.ToString() + "</label>" + " ");
            //}
            //sbLike.Append("<a style='margin-left:0px;' href='javascript:" + onclick + "' id='feed_link_like_" + feedId + "'>");
            //if (objDetail.IsLikedByCurrentUser)
            //    sbLike.Append("Unlike");
            //else
            //    sbLike.Append("Like");
            //sbLike.Append("</a> ");

            //if (objDetail.TotalComments > 0)
            //{
            //    sbLike.Append("<img width='13' height='12' src='images/comments-icon.png' alt='Like'>");
            //    sbLike.Append("<label id='photo_total_comment_" + feedId + "'>" + objDetail.TotalComments.ToString() + "</label>" + " ");
            //}
            //sbDetail.Append("<a class='nyroModal' href='javascript:OpenMainPopup(\"ajax-picture.aspx?" + Huntable.Business.FeedManager.ajaxPopup.UserPortfolioPhotoId.ToString()
            //                + "=" + objPhoto.Id + "\")'>");
            //sbLike.Append("Comment");
            //sbLike.Append("</a> ");
            sbLike.Append("</span> ");
            objResult.actionHTML = sbLike.ToString();
            return objResult;
        }
        #endregion

        #region Screen scrapting
        public static string getLinkContent(string feed)
        {
            //string adress = "hello www.google.ca";
            // Size the control to fill the form with a margin
            if (!feed.Contains("<img"))
            {
                MatchCollection ms = Regex.Matches(feed, @"(http.+)([\s]|$)");
                if (ms.Count > 0)
                {
                    #region link sharing
                    string url = ms[0].Value.ToString();
                    feed = feed.Replace(url, "<a href='" + url + "' target='_blank'>" + url + "</a>");
                    var webGet = new HtmlWeb();
                    var document = webGet.Load(url);

                    var titleOfPage = (from lnks in document.DocumentNode.Descendants()
                                       where lnks.Name == "title" &&
                                       lnks.InnerText.Trim().Length > 0
                                       select lnks.InnerText).FirstOrDefault();

                    var metaOfPage = (from lnks in document.DocumentNode.Descendants()
                                      where lnks.Name.ToLower() == "meta"
                                       && lnks.Attributes["name"] != null
                                       && lnks.Attributes["name"].Value.ToLower() == "description"
                                      select lnks.Attributes["content"].Value).FirstOrDefault();

                    var imgsOnPage = from lnks in document.DocumentNode.Descendants()
                                     where lnks.Name == "img" &&
                                           lnks.Attributes["src"] != null
                                     select new
                                     {
                                         Url = lnks.Attributes["src"].Value,
                                         Text = lnks.Attributes["alt"]
                                     };
                    string imageUrl = "";
                    //List<HTMLImageDetail> Images = new List<HTMLImageDetail>();
                    Uri baseUri = new Uri(url, UriKind.Absolute);
                    foreach (var link in imgsOnPage)
                    {
                        Uri page = new Uri(baseUri, link.Url.ToString());
                        //HTMLImageDetail img = new HTMLImageDetail();
                        //img.Url = page.AbsoluteUri;
                        //img.Text = page.AbsolutePath;
                        try
                        {
                            WebRequest req = WebRequest.Create(page.AbsoluteUri);
                            WebResponse response = req.GetResponse();
                            Stream stream = response.GetResponseStream();
                            System.Drawing.Image imgFile = System.Drawing.Image.FromStream(stream);
                            stream.Close();

                            if (imgFile.Width > 100 || imgFile.Height > 100)
                            {
                                imageUrl = page.AbsoluteUri;
                                break;
                            }

                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        //img.Height = imgFile.Height;
                        //img.width = imgFile.Width;
                        //Images.Add(img);
                    }
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='user-follow'>");
                    sb.Append("<tbody>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2'>");
                    sb.Append("<p>" + feed + "</p>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    if (imageUrl != "")
                    {
                        sb.Append("<td width='42%' valign='top'>");
                        sb.Append("<a href='" + url + "' target='_blank'>");
                        sb.Append("<img width='204px' class='profile-pic profile-pic2' alt='Site Picture' src='" + imageUrl + "'>");
                        sb.Append("</a>");
                        sb.Append("</td>");
                    }
                    sb.Append("<td width='58%' valign='top' align='left'>");
                    sb.Append("<p style='line-height:18px; text-align:justify;'>");
                    sb.Append(" <a class='accounts-link' href='" + url + "' target='_blank'>");
                    sb.Append(titleOfPage);
                    sb.Append("</a>");
                    sb.Append("<br>");
                    sb.Append(metaOfPage);
                    sb.Append("<br> ");
                    sb.Append("</p>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                    //string html = GetHtmlPage(url);
                    return sb.ToString();
                    #endregion
                }
                else
                    return feed;
            }
            else
                return feed;
        }
        //public static string GetHtmlPage(string strURL)
        //{
        //    try
        //    {
        //        string result;
        //        WebResponse response;
        //        WebRequest request = HttpWebRequest.Create(strURL);
        //        response = request.GetResponse();
        //        using (var sr = new StreamReader(response.GetResponseStream()))
        //        {
        //            result = sr.ReadToEnd();
        //            sr.Close();
        //        }
        //        return result;
        //    }
        //    catch (Exception Ex)
        //    {
        //        return "";
        //    }
        //}
        //public static string GetTitle(string content)
        //{
        //    string pattern = @"(?&lt;=&lt;title.*&gt;)([\s\S]*)(?=&lt;/title&gt;)";
        //    System.Text.RegularExpressions.Match match = Regex.Match(content, pattern);
        //    return match.Value;
        //}
        //private static string Getpictures(string content, string url)
        //{

        //    string pattern = @"(?&lt;=src=(\x22|\x27))[^&gt;]*[^/].(?:jpg|bmp|gif|JPG|BMP|GIF|PNG|png)[^\x22|\x27]*(?=\x22|\x27)";
        //    System.Text.RegularExpressions.MatchCollection matches = Regex.Matches(content, pattern);
        //    //' Return match.Value
        //    ArrayList arr = new ArrayList();
        //    DataTable dt = new DataTable();

        //    dt.Columns.Add("link", typeof(string));

        //    foreach (Match match in matches)
        //    {
        //        arr.Add(match.Value);
        //        DataRow row = dt.NewRow();

        //        if (match.Value.Contains("http://"))
        //        {
        //            row.ItemArray[0] = match.Value;
        //        }
        //        else
        //        {
        //            if (url[url.Length - 1] == '/')
        //            {
        //                if (match.Value[0] == '/')
        //                {
        //                    //'    Response.Write("&lt;img src='" &amp; Me.TextBox1.Text &amp; CType(arr.Item(count), String).Remove(0, 1) &amp; "'&gt;&lt;br&gt;")
        //                    Uri uri = new Uri(url);
        //                    string domain = uri.Host;
        //                    row.ItemArray[0] = domain + match.Value.Substring(1);
        //                }
        //                else
        //                {
        //                    //' Response.Write("&lt;img src='" &amp; Me.TextBox1.Text &amp; CType(arr.Item(count), String) &amp; "'&gt;&lt;br&gt;")
        //                    Uri uri = new Uri(url);
        //                    string domain = uri.Host;
        //                    row.ItemArray[0] = domain + "/" + match.Value;

        //                }

        //            }
        //            else
        //            {
        //                if (match.Value[0] == '/')
        //                {

        //                    //'Response.Write("&lt;img src='" &amp; Me.TextBox1.Text &amp; CType(arr.Item(count), String) &amp; "'&gt;&lt;br&gt;")
        //                    Uri uri = new Uri(url);
        //                    string domain = uri.Host;
        //                    row.ItemArray[0] = domain + match.Value;
        //                }
        //                else
        //                {
        //                    //'   Response.Write("&lt;img src='" &amp; Me.TextBox1.Text &amp; "/" &amp; CType(arr.Item(count), String) &amp; "'&gt;&lt;br&gt;")
        //                    Uri uri = new Uri(url);
        //                    string domain = uri.Host;
        //                    row.ItemArray[0] = domain + "/" + match.Value;
        //                }
        //            }
        //        }
        //        dt.Rows.Add(row);
        //    }
        //    if (dt.Rows.Count > 0)
        //        return dt.Rows[0][0].ToString();
        //    else
        //        return "";
        //}


        #endregion
    }

    public class Feed_DerivedUser : IEnumerable
    {
        public Feed_DerivedUser() { }
        public Feed_DerivedUser(int uId, DateTime? dt)
        {
            this.userId = uId;
            this.date = dt;
        }
        public int userId { get; set; }
        public DateTime? date { get; set; }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

    }

    public class FeedList
    {
        public int totalRecords { get; set; }
        public int LatestFeedId { get; set; }
        public int PageIndex { get; set; }
        public int nextPageIndex { get; set; }
        public int pageSize { get; set; }
        public List<FeedDetail> feeds { get; set; }
    }
    public class FeedDetail
    {
        public int MainfeedId { get; set; }
        public int feedId { get; set; }
        public string feedDescription { get; set; }
    }
    public class FeedLikeDetail
    {
        public int feedId { get; set; }
        public int LikeCount { get; set; }
        public string LikeLinkHTML { get; set; }
        public string LikeHeader { get; set; }
    }

    public class FeedCommentDetail
    {
        public int feedId { get; set; }
        public int OldestCommentId { get; set; }
        public int LatestCommentId { get; set; }
        public int TotalCommentCount { get; set; }
        public int DisplayedCommentCount { get; set; }
        public string CommentLinkHTML { get; set; }//used in popup only
        public string CommentHeader { get; set; }
        public List<FeedDetail> comments { get; set; }
    }

    public class FeedComment
    {
        public int feedId { get; set; }
        public int mainFeedUserId { get; set; }
        public DateTime date { get; set; }
        public int? profilePictureId { get; set; }
        public int userId { get; set; }
        public string name { get; set; }
        public string comments { get; set; }
    }

    public class UserLikeList
    {
        public int totalLikes { get; set; }
        public int pendingLikes { get; set; }
        public int OldestLikeId { get; set; }
        public List<UserLikeDetail> detail { get; set; }
    }
    public class UserLikeDetail
    {
        public int feedId { get; set; }
        public string detailHTML { get; set; }
        public string actionHTML { get; set; }
    }
    public class LikeComment
    {
        public int TotalLikes { get; set; }
        public int TotalComments { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
    }

    public class PopupDetail
    {
        public string OwnerDetail { get; set; }//
        public string LikeFeedType { get; set; }//
        public string CommentFeedType { get; set; }//
        public int feedId { get; set; }//
        public int feedUserId { get; set; }//
        public string FeedType { get; set; }//
        public int RefRecordId { get; set; }//
        public int NextRecordId { get; set; }
        public int PrevRecordId { get; set; }
        public string Detail { get; set; }
        public string Description { get; set; }
        public string TimeStamp { get; set; }

        //public Popup_LikedDetail LikedUsers { get; set; }
        //public FeedLikeDetail LikeDetail { get; set; }
        //public FeedCommentDetail CommentDetail { get; set; }
    }
    public class Popup_LikedDetail
    {
        public int feedId { get; set; }
        public List<string> LikedUsers { get; set; }
    }
    public class HTMLImageDetail
    {
        public string Url { get; set; }
        public string Text { get; set; }
        public int width { get; set; }
        public int Height { get; set; }
    }
    public class FeedAlert
    {
        public int feedId { get; set; }
        public string feedDescription { get; set; }
    }

}
