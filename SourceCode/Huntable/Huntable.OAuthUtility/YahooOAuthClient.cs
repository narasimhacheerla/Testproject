using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using Newtonsoft.Json;

namespace OAuthUtility
{
    public class YahooOAuthClient : OAuthClient 
    {

        public static readonly ServiceProviderDescription YahooServiceDescription = new ServiceProviderDescription
        {
            RequestTokenEndpoint =
                new MessageReceivingEndpoint(
                "https://api.login.yahoo.com/oauth/v2/get_request_token",
                HttpDeliveryMethods.
                    AuthorizationHeaderRequest |
                HttpDeliveryMethods.
                    GetRequest),
            UserAuthorizationEndpoint =
                new MessageReceivingEndpoint(
                "https://api.login.yahoo.com/oauth/v2/request_auth",
                HttpDeliveryMethods.
                    AuthorizationHeaderRequest |
                HttpDeliveryMethods.
                    GetRequest),
            AccessTokenEndpoint =
                new MessageReceivingEndpoint(
                "https://api.login.yahoo.com/oauth/v2/get_token",
                HttpDeliveryMethods.
                    AuthorizationHeaderRequest |
                HttpDeliveryMethods.
                    GetRequest),
            TamperProtectionElements =
                new ITamperProtectionChannelBindingElement[]
                    {
                        new HmacSha1SigningBindingElement
                            ()
                    },
        };

        public YahooOAuthClient(string consumerKey, string consumerSecret)
			: this(consumerKey, consumerSecret, new CookieOAuthTokenManager()) { }

        public YahooOAuthClient(string consumerKey, string consumerSecret, IOAuthTokenManager tokenManager)
            : base("yahoo", YahooServiceDescription, new SimpleConsumerTokenManager(consumerKey, consumerSecret, tokenManager))
        {
		}

        internal static XDocument LoadXDocumentFromStream(Stream stream)
        {
            const int MaxChars = 0x10000; // 64k

            var settings = CreateUntrustedXmlReaderSettings();
            settings.MaxCharactersInDocument = MaxChars;
            return XDocument.Load(XmlReader.Create(stream, settings));
        }

        internal static XmlReaderSettings CreateUntrustedXmlReaderSettings()
        {
            return new XmlReaderSettings
            {
                MaxCharactersFromEntities = 1024,
                XmlResolver = null,
            #if CLR4
				DtdProcessing = DtdProcessing.Prohibit,
            #else
                ProhibitDtd = true,
            #endif
            };
        }

        protected override AuthenticationResult VerifyAuthenticationCore(AuthorizedTokenResponse response)
        {
            // See here for Field Selectors API http://developer.linkedin.com/docs/DOC-1014
            const string ProfileRequestUrl = "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,headline,industry,summary)";

            string accessToken = response.AccessToken;
            var extraData = response.ExtraData;
            extraData.Add("accesstoken", accessToken);
            extraData.Add("tokensecret", "");
            return new AuthenticationResult(
                           isSuccessful: true, provider: this.ProviderName, providerUserId: response.ExtraData["xoauth_yahoo_guid"], userName: "userName", extraData: extraData);
            //var profileEndpoint = new MessageReceivingEndpoint(ProfileRequestUrl, HttpDeliveryMethods.GetRequest);
           // HttpWebRequest request = this.WebWorker.PrepareAuthorizedRequest(profileEndpoint, accessToken);

            //try
            //{
            //    using (WebResponse profileResponse = request.GetResponse())
            //    {
            //        using (Stream responseStream = profileResponse.GetResponseStream())
            //        {
            //            XDocument document = LoadXDocumentFromStream(responseStream);
            //            string userId = document.Root.Element("id").Value;

            //            string firstName = document.Root.Element("first-name").Value;
            //            string lastName = document.Root.Element("last-name").Value;
            //            string userName = firstName + " " + lastName;

            //            var extraData = new Dictionary<string, string>();
            //            extraData.Add("accesstoken", accessToken);
            //            extraData.Add("name", userName);
                       
            //            return new AuthenticationResult(
            //                isSuccessful: true, provider: this.ProviderName, providerUserId: userId, userName: userName, extraData: extraData);
            //        }
            //    }
            //}
            //catch (Exception exception)
            //{
            //    return new AuthenticationResult(exception);
            //}
        }

        private static Uri BuildUri(string baseUri, NameValueCollection queryParameters)
        {
            var q = HttpUtility.ParseQueryString(string.Empty);
            q.Add(queryParameters);
            var builder = new UriBuilder(baseUri) { Query = q.ToString() };
            return builder.Uri;
        }

        public  List<Contact> GetContacts(string accessToken, string yahoGuid)
        {
            var contacts = new List<Contact>();
            var ContactsUrl =
                string.Format("http://social.yahooapis.com/v1/user/{0}/contacts;out=name,phone,email?format=XML",
                              yahoGuid);
            var ContactsEndpoint = new MessageReceivingEndpoint(ContactsUrl, HttpDeliveryMethods.GetRequest);
            HttpWebRequest webRequest = this.WebWorker.PrepareAuthorizedRequest(ContactsEndpoint, accessToken);
          //  var uri = BuildUri(ContactsEndpoint, new NameValueCollection { { "access_token", accessToken }, { "count", "max" } });

           // var webRequest = (HttpWebRequest)WebRequest.Create(uri);
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
                        XDocument result = XDocument.Parse(body);


                        //Extract information from XML


                        XNamespace xn = result.Root.GetDefaultNamespace();
                        XNamespace attxn = "http://www.yahooapis.com/v1/base.rng";

                        result.Root.Descendants(result.Root.GetDefaultNamespace() + "contact").ToList().ForEach(x =>
                        {
                            IEnumerable<XElement> contactFields = x.Elements(xn + "fields").ToList();
                            var contact = new Contact();
                            var i = 1;
                            foreach (var field in contactFields)
                            {

                                contact.Id = i++;
                                contact.Provider = "email";
                                if (field.Attribute(attxn + "uri").Value.Contains("/yahooid/"))
                                {
                                    //contact.Name = field.Element(xn + "value").Value;
                                    //contact.Email = field.Element(xn + "value").Value + "@yahoo.com";
                                }
                                else if (field.Attribute(attxn + "uri").Value.Contains("/name/"))
                                {
                                    //Contact c = contacts.Last<Contact>();
                                    contact.Name = field.Element(xn + "value").Element(xn + "givenName").Value + " " + field.Element(xn + "value").Element(xn + "familyName").Value;
                                    //contacts[contacts.Count - 1] = c;
                                    //continue;
                                }
                                else if (field.Attribute(attxn + "uri").Value.Contains("/email/"))
                                {
                                    //contact.Name = field.Element(xn + "value").Value.Replace("@yahoo.com", "");
                                    contact.Email = field.Element(xn + "value").Value;
                                }
                            }
                            if (!string.IsNullOrEmpty(contact.Email) && !contacts.Exists(y => y.Email == contact.Email))
                                contacts.Add(contact);
                        });


                    }
                }
            }
            catch (Exception ex)
            {

            }


            return contacts.Where(u => u.Email != "").OrderBy(u => u.Email).ToList();
        }
    }
}
