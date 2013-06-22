using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using DotNetOpenAuth.AspNet.Clients;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace OAuthUtility
{
    public class GoogleOAuth2Client : OAuth2Client
    {


        #region Constants and Fields

        /// <summary>
        /// The authorization endpoint.
        /// </summary>
        private const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";

        /// <summary>
        /// The token endpoint.
        /// </summary>
        private const string TokenEndpoint = "https://accounts.google.com/o/oauth2/token";

        /// <summary>
        /// The user info endpoint.
        /// </summary>
        private const string UserInfoEndpoint = "https://www.googleapis.com/oauth2/v1/userinfo";

        /// <summary>
        /// The contacts endpoint.
        /// </summary>
        private const string ContactsEndpoint = "https://www.google.com/m8/feeds/contacts/default/full";

        /// <summary>
        /// The base uri for scopes.
        /// </summary>
        private const string ScopeBaseUri = "https://www.googleapis.com/auth/";

        /// <summary>
        /// The _app id.
        /// </summary>
        private readonly string _clientId;

        /// <summary>
        /// The _app secret.
        /// </summary>
        private readonly string _clientSecret;

        /// <summary>
        /// The requested scopes.
        /// </summary>
        private readonly string[] _requestedScopes;

        #endregion


        #region Applications enum

        /// <summary>
        /// The many specific authorization scopes Google offers.
        /// </summary>
        [Flags]
        public enum Applications : long
        {
            /// <summary>
            /// The Gmail address book.
            /// </summary>
            Contacts = 0x1,

            /// <summary>
            /// Appointments in Google Calendar.
            /// </summary>
            Calendar = 0x2,

            /// <summary>
            /// Blog post authoring.
            /// </summary>
            Blogger = 0x4,

            /// <summary>
            /// Google Finance
            /// </summary>
            UserContent = 0x8,

            /// <summary>
            /// Google Gmail
            /// </summary>
            Gmail = 0x10,

            /// <summary>
            /// Google Health
            /// </summary>
            Health = 0x20,

            /// <summary>
            /// Google OpenSocial
            /// </summary>
            OpenSocial = 0x40,

            /// <summary>
            /// Picasa Web
            /// </summary>
            PicasaWeb = 0x80,

            /// <summary>
            /// Google Spreadsheets
            /// </summary>
            Spreadsheets = 0x100,

            /// <summary>
            /// Webmaster Tools
            /// </summary>
            WebmasterTools = 0x200,

            /// <summary>
            /// YouTube service
            /// </summary>
            YouTube = 0x400,

            /// <summary>
            /// Google Docs
            /// </summary>
            DocumentsList = 0x800,

            /// <summary>
            /// Google Book Search
            /// </summary>
            BookSearch = 0x1000,

            /// <summary>
            /// Google Base
            /// </summary>
            GoogleBase = 0x2000,

            /// <summary>
            /// Google Analytics
            /// </summary>
            Analytics = 0x4000,

            /// <summary>
            /// Google Maps
            /// </summary>
            Maps = 0x8000,
        }

        /// <summary>
        /// A mapping between Google's applications and their URI scope values.
        /// </summary>
        private static readonly Dictionary<Applications, string> DataScopeUris = new Dictionary<Applications, string>
                                                                                     {
                                                                                         {
                                                                                             Applications.Analytics,
                                                                                             "https://www.google.com/analytics/feeds/"
                                                                                             },
                                                                                         {
                                                                                             Applications.GoogleBase,
                                                                                             "http://www.google.com/base/feeds/"
                                                                                             },
                                                                                         {
                                                                                             Applications.Blogger,
                                                                                             "http://www.blogger.com/feeds"
                                                                                             },
                                                                                         {
                                                                                             Applications.BookSearch,
                                                                                             "http://www.google.com/books/feeds/"
                                                                                             },
                                                                                         {
                                                                                             Applications.Calendar,
                                                                                             "http://www.google.com/calendar/feeds/"
                                                                                             },
                                                                                         {
                                                                                             Applications.Contacts,
                                                                                             "https://www.google.com/m8/feeds/"
                                                                                             },
                                                                                         {
                                                                                             Applications.DocumentsList,
                                                                                             "https://docs.google.com/feeds/"
                                                                                             },
                                                                                         {
                                                                                             Applications.UserContent,
                                                                                             "http://docs.googleusercontent.com/"
                                                                                             },
                                                                                         {
                                                                                             Applications.Gmail,
                                                                                             "https://mail.google.com/mail/feed/atom"
                                                                                             },
                                                                                         {
                                                                                             Applications.Health,
                                                                                             "https://www.google.com/h9/feeds/"
                                                                                             },
                                                                                         {
                                                                                             Applications.Maps,
                                                                                             "http://maps.google.com/maps/feeds/"
                                                                                             },
                                                                                         {
                                                                                             Applications.OpenSocial,
                                                                                             "http://sandbox.gmodules.com/api/"
                                                                                             },
                                                                                         {
                                                                                             Applications.PicasaWeb,
                                                                                             "http://picasaweb.google.com/data/"
                                                                                             },
                                                                                         {
                                                                                             Applications.Spreadsheets,
                                                                                             "https://spreadsheets.google.com/feeds/"
                                                                                             },
                                                                                         {
                                                                                             Applications.WebmasterTools,
                                                                                             "http://www.google.com/webmasters/tools/feeds/"
                                                                                             },
                                                                                         {
                                                                                             Applications.YouTube,
                                                                                             "http://gdata.youtube.com"
                                                                                             },
                                                                                     };

        #endregion
        /// <summary>
        /// Creates a new Google OAuth2 Client, requesting the default "userinfo.profile" and "userinfo.email" scopes.
        /// </summary>
        /// <param name="clientId">The Google Client Id</param>
        /// <param name="clientSecret">The Google Client Secret</param>
        public GoogleOAuth2Client(string clientId, string clientSecret)
            : this(clientId, clientSecret, new[] { "userinfo.profile", "userinfo.email","https://www.google.com/m8/feeds/" }) { }

        /// <summary>
        /// Creates a new Google OAuth2 client.
        /// </summary>
        /// <param name="clientId">The Google Client Id</param>
        /// <param name="clientSecret">The Google Client Secret</param>
        /// <param name="requestedScopes">One or more requested scopes, passed without the base URI.</param>
        public GoogleOAuth2Client(string clientId, string clientSecret, params string[] requestedScopes)
            : base("google")
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw new ArgumentNullException("clientId");

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw new ArgumentNullException("clientSecret");

            if (requestedScopes == null)
                throw new ArgumentNullException("requestedScopes");

            if (requestedScopes.Length == 0)
                throw new ArgumentException("One or more scopes must be requested.", "requestedScopes");

            _clientId = clientId;
            _clientSecret = clientSecret;
            _requestedScopes = requestedScopes;
        }

        protected override Uri GetServiceLoginUrl(Uri returnUrl)
        {
            var scopes = _requestedScopes.Select(x => !x.StartsWith("http", StringComparison.OrdinalIgnoreCase) ? ScopeBaseUri + x : x);

            return BuildUri(AuthorizationEndpoint, new NameValueCollection
                {
                    { "response_type", "code" },
                    { "client_id", _clientId },
                    { "scope", string.Join(" ", scopes) },
                    { "redirect_uri", returnUrl.GetLeftPart(UriPartial.Path) },
                    { "state", returnUrl.Query.Substring(1) },
                });
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
                    var extraData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    return extraData;
                }
            }
        }

        public static List<Contact> GetContacts(string accessToken, int maxResults=500,int startIndex=1)
        {
            var uri = BuildUri(ContactsEndpoint, new NameValueCollection { { "access_token", accessToken } , {"start-index", startIndex.ToString(CultureInfo.InvariantCulture)},
                                    {"max-results", maxResults.ToString(CultureInfo.InvariantCulture)} });
            IEnumerable<Contact> contacts = new BindingList<Contact>();
            var webRequest = (HttpWebRequest)WebRequest.Create(uri);

            try
            {
                using (var webResponse = webRequest.GetResponse())
                using (var stream = webResponse.GetResponseStream())
                {
                    if (stream == null)
                        return null;
                    
                    using (var textReader = new StreamReader(stream))
                    {
                        var body = textReader.ReadToEnd();
                        XDocument contactsXML = XDocument.Parse(body);
                        XNamespace xn = "http://schemas.google.com/g/2005";
                        var i = 1;
                        contacts = from c in contactsXML.Descendants(contactsXML.Root.GetDefaultNamespace() + "entry")
                                   select new Contact()
                                   {
                                       Id = i++,
                                       Provider = "email",
                                       UniqueId = c.Element(contactsXML.Root.GetDefaultNamespace() + "id").Value,
                                       Name = c.Element(contactsXML.Root.GetDefaultNamespace() + "title").Value,
                                       Email = (c.Element(xn + "email") == null) ? "" : c.Element(xn + "email").Attribute("address").Value,
                                       ProfilePictureUrl = ""
                                   };
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return contacts.Where(u=>u.Email!="").OrderBy(u => u.Email).ToList();
        }

        protected override string QueryAccessToken(Uri returnUrl, string authorizationCode)
        {
            var postData = HttpUtility.ParseQueryString(string.Empty);
            postData.Add(new NameValueCollection
                {
                    { "grant_type", "authorization_code" },
                    { "code", authorizationCode },
                    { "client_id", _clientId },
                    { "client_secret", _clientSecret },
                    { "redirect_uri", returnUrl.GetLeftPart(UriPartial.Path) },
                });

            var webRequest = (HttpWebRequest)WebRequest.Create(TokenEndpoint);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            using (var s = webRequest.GetRequestStream())
            using (var sw = new StreamWriter(s))
                sw.Write(postData.ToString());

            using (var webResponse = webRequest.GetResponse())
            {
                var responseStream = webResponse.GetResponseStream();
                if (responseStream == null)
                    return null;

                using (var reader = new StreamReader(responseStream))
                {
                    var response = reader.ReadToEnd();
                    var json = JObject.Parse(response);
                    var accessToken = json.Value<string>("access_token");
                    return accessToken;
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
        /// Google requires that all return data be packed into a "state" parameter.
        /// This should be called before verifying the request, so that the url is rewritten to support this.
        /// </summary>
        public static void RewriteRequest()
        {
            var ctx = HttpContext.Current;

            var stateString = HttpUtility.UrlDecode(ctx.Request.QueryString["state"]);
            if (stateString == null || !stateString.Contains("__provider__=google"))
                return;

            var q = HttpUtility.ParseQueryString(stateString);
            q.Add(ctx.Request.QueryString);
            q.Remove("state");

            ctx.RewritePath(ctx.Request.Path + "?" + q);
        }

    }
}
