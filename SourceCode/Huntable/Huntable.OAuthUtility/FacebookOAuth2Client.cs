using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;
using ImpactWorks.FBGraph.Connector;
using ImpactWorks.FBGraph.Core;
using ImpactWorks.FBGraph.Interfaces;
using Newtonsoft.Json;

namespace OAuthUtility
{
    /// <summary>
    /// A DotNetOpenAuth client for logging in to Facebook using OAuth2.
    /// Reference: http://developers.facebook.com/docs/howtos/login/server-side-login/
    /// </summary>
    public class FacebookOAuth2Client : OAuth2Client
    {
        #region Constants and Fields

        /// <summary>
        /// The authorization endpoint.
        /// </summary>
        private const string AuthorizationEndpoint = "https://www.facebook.com/dialog/oauth";

        /// <summary>
        /// The token endpoint.
        /// </summary>
        private const string TokenEndpoint = "https://graph.facebook.com/oauth/access_token";

        /// <summary>
        /// The user info endpoint.
        /// </summary>
        private const string UserInfoEndpoint = "https://graph.facebook.com/me";

        /// <summary>
        /// The app id.
        /// </summary>
        private readonly string _appId;

        /// <summary>
        /// The app secret.
        /// </summary>
        private readonly string _appSecret;

        /// <summary>
        /// The requested scopes.
        /// </summary>
        private readonly string[] _requestedScopes;

        #endregion

        /// <summary>
        /// Creates a new Facebook OAuth2 client, requesting the default "email" scope.
        /// </summary>
        /// <param name="appId">The Facebook App Id</param>
        /// <param name="appSecret">The Facebook App Secret</param>
        public FacebookOAuth2Client(string appId, string appSecret)
            : this(appId, appSecret, new[] { "email", "publish_stream" }) { }

        /// <summary>
        /// Creates a new Facebook OAuth2 client.
        /// </summary>
        /// <param name="appId">The Facebook App Id</param>
        /// <param name="appSecret">The Facebook App Secret</param>
        /// <param name="requestedScopes">One or more requested scopes, passed without the base URI.</param>
        public FacebookOAuth2Client(string appId, string appSecret, params string[] requestedScopes)
            : base("facebook")
        {
            if (string.IsNullOrWhiteSpace(appId))
                throw new ArgumentNullException("appId");

            if (string.IsNullOrWhiteSpace(appSecret))
                throw new ArgumentNullException("appSecret");

            if (requestedScopes == null)
                throw new ArgumentNullException("requestedScopes");

            if (requestedScopes.Length == 0)
                throw new ArgumentException("One or more scopes must be requested.", "requestedScopes");

            _appId = appId;
            _appSecret = appSecret;
            _requestedScopes = requestedScopes;
        }

        protected override Uri GetServiceLoginUrl(Uri returnUrl)
        {
            return BuildUri(AuthorizationEndpoint, new NameValueCollection
                {
                    { "client_id", _appId },
                    { "scope", string.Join(" ", _requestedScopes) },
                    { "redirect_uri", returnUrl.GetLeftPart(UriPartial.Path) },
                    { "state", returnUrl.Query.Substring(1) },
                });
        }


        public override AuthenticationResult VerifyAuthentication(HttpContextBase context, Uri returnPageUrl)
        {

            string code = context.Request.QueryString["code"];
            if (string.IsNullOrEmpty(code))
            {
                return AuthenticationResult.Failed;
            }

            var response = this.QueryAccessToken(returnPageUrl, code);

            var results = HttpUtility.ParseQueryString(response);
            var accessToken = results["access_token"];
            var expires = results["expires"];
            if (accessToken == null)
            {
                return AuthenticationResult.Failed;
            }

            IDictionary<string, string> userData = this.GetUserData(accessToken);
            if (userData == null)
            {
                return AuthenticationResult.Failed;
            }

            string id = userData["id"];
            string name;

            // Some oAuth providers do not return value for the 'username' attribute. 
            // In that case, try the 'name' attribute. If it's still unavailable, fall back to 'id'
            if (!userData.TryGetValue("username", out name) && !userData.TryGetValue("name", out name))
            {
                name = id;
            }

            // add the access token to the user data dictionary just in case page developers want to use it
            userData["accesstoken"] = accessToken;
            userData["expires"] = expires;
            userData.Add("tokensecret", "");

            return new AuthenticationResult(
                isSuccessful: true, provider: this.ProviderName, providerUserId: id, userName: name, extraData: userData);
        }


        protected override IDictionary<string, string> GetUserData(string accessToken)
        {
            var uri = BuildUri(UserInfoEndpoint, new NameValueCollection { { "access_token", accessToken } });

            var webRequest = (HttpWebRequest)WebRequest.Create(uri);

            using (var webResponse = webRequest.GetResponse())
            using (var stream = webResponse.GetResponseStream())
            {
                if (stream == null)
                    return null;

                using (var textReader = new StreamReader(stream))
                {
                    var json = textReader.ReadToEnd();
                    var extraData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    var data = extraData.ToDictionary(x => x.Key, x => x.Value.ToString());

                    data.Add("picture", string.Format("https://graph.facebook.com/{0}/picture", data["id"]));

                    return data;
                }
            }
        }

        protected override string QueryAccessToken(Uri returnUrl, string authorizationCode)
        {
            var uri = BuildUri(TokenEndpoint, new NameValueCollection
                {
                    { "code", authorizationCode },
                    { "client_id", _appId },
                    { "client_secret", _appSecret },
                    { "redirect_uri", returnUrl.GetLeftPart(UriPartial.Path) },
                });

            var webRequest = (HttpWebRequest)WebRequest.Create(uri);

            using (var webResponse = webRequest.GetResponse())
            {
                var responseStream = webResponse.GetResponseStream();
                if (responseStream == null)
                    return null;

                using (var reader = new StreamReader(responseStream))
                {
                    var response = reader.ReadToEnd();
                    return response;
                    //var results = HttpUtility.ParseQueryString(response);
                    //return results["access_token"];
                }
            }
        }

        private static Uri BuildUri(string baseUri, NameValueCollection queryParameters)
        {
            var q = HttpUtility.ParseQueryString(string.Empty);
            q.Add(queryParameters);
            var builder = new UriBuilder(baseUri) { Query = q.ToString() };
            return builder.Uri;
        }

        /// <summary>
        /// Facebook works best when return data be packed into a "state" parameter.
        /// This should be called before verifying the request, so that the url is rewritten to support this.
        /// </summary>
        public static void RewriteRequest()
        {
            var ctx = HttpContext.Current;

            var stateString = HttpUtility.UrlDecode(ctx.Request.QueryString["state"]);
            if (stateString == null || !stateString.Contains("__provider__=facebook"))
                return;

            var q = HttpUtility.ParseQueryString(stateString);
            q.Add(ctx.Request.QueryString);
            q.Remove("state");

            ctx.RewritePath(ctx.Request.Path + "?" + q);
        }


        public static bool ImageShare(string accesstoken, string message, string imageUrl)
        {
            try
            {
              
                var facebook = new Facebook(accesstoken);
                var user = facebook.GetLoggedInUserInfo();
                if(user!=null)
                {
                    var userid = (long)user.id;
                    //IFeedPost post = new FeedPost();
                    //post.Message = message;
                    //post.ImageUrl = imageUrl;
                    //var result = facebook.PostToWall(userid, post);
                    //if(result=="")
                    //    result = facebook.PostToWall(userid, post);

                    var uploadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "@TempImages");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    var fileName = Path.Combine(uploadPath, DateTime.Now.ToOADate() + ".jpg");
                    var client = new WebClient();
                    client.DownloadFile(imageUrl, fileName);

                    var fbMedia = new FacebookMedia();
                    fbMedia.ContentType = "image/jpeg";
                    fbMedia.FilePath = fileName;
                    fbMedia.FileName = "My Image";
                    fbMedia.Name = "test";
                    facebook.UploadPhoto(userid, userid, message, fbMedia);
                        
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool VideoShare(string accesstoken, string message, string videoUrl)
        {
            try
            {
                var facebook = new Facebook(accesstoken);
                var user = facebook.GetLoggedInUserInfo();
                if (user != null)
                {
                    var userid = (long) user.id;
                    IFeedPost post = new FeedPost();

                    if (!string.IsNullOrEmpty(message))
                        post.Message = message;

                    if (!string.IsNullOrEmpty(videoUrl))
                        post.Url = videoUrl;

                    var result = facebook.PostToWall(userid, post);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool LinkShare(string accesstoken, string name, string message, string description, string caption, string url, string imageUrl)
        {
            try
            {

                var facebook = new Facebook(accesstoken);
                var user = facebook.GetLoggedInUserInfo();
                if (user != null)
                {
                    var userid = (long)user.id;
                    IFeedPost post = new FeedPost();

                    ////if (!string.IsNullOrEmpty(url))
                    ////    post.Action = new FBAction { Link = url, Name = "Read" };
                    if (!string.IsNullOrEmpty(message))
                        post.Message = message;
                    if (!string.IsNullOrEmpty(caption))
                         post.Caption = caption;
                    if (!string.IsNullOrEmpty(description))
                         post.Description = description;
                    if (!string.IsNullOrEmpty(name))
                         post.Name = name;
                    if (!string.IsNullOrEmpty(url))
                         post.Url = url;
                    if (!string.IsNullOrEmpty(imageUrl))
                        post.ImageUrl = imageUrl;

                    var result = facebook.PostToWall(userid, post);
                }

                return false;
            }
            catch
            {
                return false;
            }
        }


        public static bool StatusShare(string accesstoken, string message)
        {
            try
            {

                var facebook = new Facebook(accesstoken);
                var user = facebook.GetLoggedInUserInfo();
                if (user != null)
                {
                    var userid = (long)user.id;
                    IFeedPost post = new FeedPost();
                    post.Message = message;
                    var result = facebook.PostToWall(userid, post);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }


        public static bool SendInvitation(string accesstoken,long id,string url,string name)
        {
            try
            {
                //var config = new FacebookConfig();
                //config.LiveAppID = "307487099357078";
                //config.LiveSecret = "d24675d8248f10d3f8a52e4d8fe29062";
                //config.Mode = FacebookMode.Live;
                var facebook = new Facebook(accesstoken);
               // facebook.Token = accesstoken;
                IFeedPost post = new FeedPost();
                post.Action = new FBAction { Link = url, Name = "Read" };
                const string message = "Hi [NAME], I am inviting you to join Huntable, the fastest growing Professional Resourcing Network.  Huntable lets you: super power your profile, receive customized feeds and jobs, follow your favorite person or company, connect and network, get headhunted, find your dream job and many more… It’s FREE to join Huntable, and it only takes few clicks. There is nothing to loose. See you there! ";
                post.Message = message.Replace("[NAME]", name);
                post.Caption = "Inviting you to join huntable";
                post.Description = "The fastest growing Professional Resourcing Network";
                post.Name = "Click here to find out more";
                post.Url = url;
                var result = facebook.PostToWall(id, post);
                


                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<Contact> GetFacebookContacts(string accesstoken,int tokenid)
        {
            IEnumerable<Contact> contacts = new BindingList<Contact>();

            //var accesstkn =
            //    "AAAEjTSwEtN8BALTTu6NFPbwgV8Q5izuZCiSVicZBTCbdLVIPLeVfSvEQ9fC0E5GaKpkXeSaCINlKjavXY4aZAQhca58IZCZBK5c6YAOMZBhAZDZD";

            var facebook = new Facebook(accesstoken);
            var user = facebook.GetLoggedInUserInfo();
            var friends = facebook.GetFriends(user.id);
            var i = 1;

            contacts = from fbFriend in friends
                       select new Contact
                       {
                           Id = i++,
                           Provider = "facebook",
                           Name = fbFriend.name,
                           UniqueId = fbFriend.id.ToString(),
                           ProfilePictureUrl = fbFriend.image,
                           TokenId = tokenid
                       };

            return contacts.ToList();
        }

        public static List<FBFriend> GetFacebookFriends(string accesstoken)
        {
            //IEnumerable<Contact> contacts = new BindingList<Contact>();

            //var accesstkn =
            //    "AAAEjTSwEtN8BALTTu6NFPbwgV8Q5izuZCiSVicZBTCbdLVIPLeVfSvEQ9fC0E5GaKpkXeSaCINlKjavXY4aZAQhca58IZCZBK5c6YAOMZBhAZDZD";

            var facebook = new Facebook(accesstoken);
            var user = facebook.GetLoggedInUserInfo();
            var friends = facebook.GetFriends(user.id);


            return friends.ToList();
        }
    }
}

