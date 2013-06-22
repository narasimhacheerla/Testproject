using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Huntable.Data;
using LinkedIn.ServiceEntities;
using OAuthUtility;
using LinkedIn;
using System.Configuration;

namespace Huntable.Business
{
    public class SocialShareManager
    {
        public void ShareOnFacebook(int userId,string message,string pictureUrl)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                 var user = context.Users.First(u => u.Id == userId);
                 var oAuthToken = user.OAuthTokens.FirstOrDefault(o => o.Provider == "facebook");
                if(oAuthToken!=null)
                {
                    message = message.Replace("[UserName]", oAuthToken.ProviderUserName);
                    OAuthWebSecurity.ShareOnFacebook(oAuthToken.Token, message, pictureUrl);
                }
            }
            
        }


        public void ShareVideoOnFacebook(int userId, string message, string videoUrl)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(u => u.Id == userId);
                var oAuthToken = user.OAuthTokens.FirstOrDefault(o => o.Provider == "facebook");
                if (oAuthToken != null)
                {
                    message = message.Replace("[UserName]", oAuthToken.ProviderUserName);
                    OAuthWebSecurity.ShareVideoOnFacebook(oAuthToken.Token, message, videoUrl);
                }
            }

        }

        public void ShareLinkOnFacebook(int userId,string name, string message, string description, string caption, string url, string imageUrl)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(u => u.Id == userId);
                var oAuthToken = user.OAuthTokens.FirstOrDefault(o => o.Provider == "facebook");
                if (oAuthToken != null)
                {
                    message = message.Replace("[UserName]", oAuthToken.ProviderUserName);
                    OAuthWebSecurity.ShareLinkOnFacebook(oAuthToken.Token, name, message, description, caption, url, imageUrl);
                }
            }

        }


        public void ShareOnLinkedIn(int userId, string message, string pictureUrl)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(u => u.Id == userId);
                var oAuthToken = user.OAuthTokens.FirstOrDefault(o => o.Provider == "linkedin");
                if (oAuthToken != null)
                {
                    var consumerKey = ConfigurationManager.AppSettings["LinkedInConsumerKey"];
                    var consumerSecret = ConfigurationManager.AppSettings["LinkedInConsumerSecret"];
                    var service = new LinkedInService(consumerKey, consumerSecret, oAuthToken.Token, oAuthToken.Secret);
                    Uri ur = null;
                    if (pictureUrl.Length>0)
                     ur = new Uri(pictureUrl);
                    var uri = new Uri("https://huntable.co.uk");
                    service.CreateShare(message, "HI", "", uri, ur, VisibilityCode.Anyone, false);
                }
            }
        }

        public void ShareOnTwitter(int userId, string message)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(u => u.Id == userId);
                var oAuthToken = user.OAuthTokens.FirstOrDefault(o => o.Provider == "twitter");
                if (oAuthToken != null)
                {
                    OAuthWebSecurity.PostTweet(oAuthToken.Token,oAuthToken.Secret,message);
                }
            }
        }

    }
}
