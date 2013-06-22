using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using Snovaspace.Util.Logging;
using TweetSharp;

namespace OAuthUtility
{
    public class TwitterOAuthClient : OAuthClient
    {
        public static readonly ServiceProviderDescription TwitterServiceDescription = new ServiceProviderDescription
        {
            RequestTokenEndpoint =
                new MessageReceivingEndpoint(
                "https://api.twitter.com/oauth/request_token",
                HttpDeliveryMethods.
                    GetRequest |
                HttpDeliveryMethods.
                    AuthorizationHeaderRequest),
            UserAuthorizationEndpoint =
                new MessageReceivingEndpoint(
                "https://api.twitter.com/oauth/authenticate",
                HttpDeliveryMethods.
                    GetRequest |
                HttpDeliveryMethods.
                    AuthorizationHeaderRequest),
            AccessTokenEndpoint =
                new MessageReceivingEndpoint(
                "https://api.twitter.com/oauth/access_token",
                HttpDeliveryMethods.
                    GetRequest |
                HttpDeliveryMethods.
                    AuthorizationHeaderRequest),
            TamperProtectionElements =
                new ITamperProtectionChannelBindingElement
                []
                    {
                        new HmacSha1SigningBindingElement
                            ()
                    },
        };

        public TwitterOAuthClient(string consumerKey, string consumerSecret)
            : this(consumerKey, consumerSecret, new CookieOAuthTokenManager()) { }

        public TwitterOAuthClient(string consumerKey, string consumerSecret, IOAuthTokenManager tokenManager)
            : base("twitter", TwitterServiceDescription, new SimpleConsumerTokenManager(consumerKey, consumerSecret, tokenManager)) { }

        protected override AuthenticationResult VerifyAuthenticationCore(AuthorizedTokenResponse response)
        {
            var accessToken = response.AccessToken;
            var accessSecret = (response as ITokenSecretContainingMessage).TokenSecret;
            var extraData = response.ExtraData;
            extraData.Add("accesstoken", accessToken);
            extraData.Add("tokensecret", accessSecret);
            var userId = response.ExtraData["user_id"];
            var userName = response.ExtraData["screen_name"];

            return new AuthenticationResult(
                           isSuccessful: true, provider: this.ProviderName, providerUserId: userId, userName: userName, extraData: extraData);
        }


        public static List<Contact> GetTwitterContacts(string accesstoken, string tokensecret, int tokenId)
        {
            var service = new TwitterService(ConfigurationManager.AppSettings["twitterConsumerKey"], ConfigurationManager.AppSettings["twitterConsumerSecret"]);
            service.AuthenticateWith(accesstoken, tokensecret);
            var me = service.GetUserProfile(new GetUserProfileOptions());

            var twitterfrnds = new List<TwitterUser>();
            var frndids = service.ListFriendIdsOf(new ListFriendIdsOfOptions {UserId = me.Id});
            var count = 0;
            var ids = frndids.Take(100).Distinct().ToList();
            while(ids.Any())
            {
                var frnds = service.ListUserProfilesFor(new ListUserProfilesForOptions { UserId = ids });
                if(frnds!=null)
                {
                    twitterfrnds.AddRange(frnds);
                }
                else
                {
                    break;
                }
                count++;
                ids = frndids.Skip(count * 100).Take(100).Distinct().ToList();

            }
           
            IEnumerable<Contact> contacts = new BindingList<Contact>();
            var i = 1;
            contacts = from follower in twitterfrnds
                       select
                           new Contact()
                           {
                               Id = i++,
                               Name = follower.Name,
                               ProfilePictureUrl = follower.ProfileImageUrl,
                               UniqueId = follower.Id.ToString(),
                               Provider = "twitter",
                               TokenId = tokenId,
                               ProfileUrl = follower.ScreenName
                           };



            return contacts.ToList();
        }

        public static void SendInvitation(string token, string secret, decimal id, string name, string url)
        {
            LoggingManager.Debug("Entering SendInvitation  - TwitterOAuthClient");

            try
            {

                var fName = name.Length > 15 ? name.Split(' ')[0] : name;
                var body = "Hi [NAME] I am inviting you to join my network in Huntable.Click here to connect [LINK]";
                body = body.Replace("[NAME]", string.Format("{0}", fName));
                body = body.Replace("[LINK]", url);


                var service = new TwitterService(ConfigurationManager.AppSettings["twitterConsumerKey"], ConfigurationManager.AppSettings["twitterConsumerSecret"]);
                service.AuthenticateWith(token, secret);
                var response = service.SendDirectMessage(new SendDirectMessageOptions
                {
                    UserId = (long)id,
                    Text = body
                });

            }
            catch (Exception exception)
            {
                LoggingManager.Error(exception);
            }

            LoggingManager.Debug("Exiting SendInvitation  - TwitterOAuthClient");
        }

        public static void PostTweet(string token, string secret, string message)
        {
            LoggingManager.Debug("Entering SendInvitation  - TwitterOAuthClient");

            try
            {
                if (message.Length > 140)
                    message = message.Substring(0, 140);

                var service = new TwitterService(ConfigurationManager.AppSettings["twitterConsumerKey"], ConfigurationManager.AppSettings["twitterConsumerSecret"]);
                service.AuthenticateWith(token, secret);

                var tweet = service.SendTweet(new SendTweetOptions { Status = message });

            }
            catch (Exception exception)
            {
                LoggingManager.Error(exception);
            }

            LoggingManager.Debug("Exiting SendInvitation  - TwitterOAuthClient");
        }

    }
}
