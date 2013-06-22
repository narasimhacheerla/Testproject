using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using DotNetOpenAuth.AspNet.Clients;

namespace OAuthUtility
{
    public class LinkedInOAuthClient : OAuthClient
    {
        public static string ConsumerKey ;
        public static string ConsumerSecret;

        private static readonly MessageReceivingEndpoint ProfileEndpoint =
           new MessageReceivingEndpoint("http://api.linkedin.com/v1/people/~:(id,first-name,last-name,picture-url)",
                                        HttpDeliveryMethods.AuthorizationHeaderRequest | HttpDeliveryMethods.GetRequest);

        private static readonly MessageReceivingEndpoint ContactsEndpoint =
           new MessageReceivingEndpoint("http://api.linkedin.com/v1/people/~/connections:(id,first-name,last-name,picture-url,public-profile-url)",
                                        HttpDeliveryMethods.AuthorizationHeaderRequest | HttpDeliveryMethods.GetRequest);

        public static readonly ServiceProviderDescription LinkedInServiceDescription = new ServiceProviderDescription
        {
            RequestTokenEndpoint =
                new MessageReceivingEndpoint(
                "https://api.linkedin.com/uas/oauth/requestToken",
                HttpDeliveryMethods.
                    AuthorizationHeaderRequest |
                HttpDeliveryMethods.
                    GetRequest),
            UserAuthorizationEndpoint =
                new MessageReceivingEndpoint(
                "https://www.linkedin.com/uas/oauth/authenticate",
                HttpDeliveryMethods.
                    AuthorizationHeaderRequest |
                HttpDeliveryMethods.
                    GetRequest),
            AccessTokenEndpoint =
                new MessageReceivingEndpoint(
                "https://api.linkedin.com/uas/oauth/accessToken",
                HttpDeliveryMethods.
                    AuthorizationHeaderRequest |
                HttpDeliveryMethods.
                    GetRequest),
            TamperProtectionElements =
                new ITamperProtectionChannelBindingElement
                []
                {
                    new HmacSha1SigningBindingElement
                        ()
                },
        };

        public LinkedInOAuthClient(string consumerKey, string consumerSecret)
            : this(consumerKey, consumerSecret, new CookieOAuthTokenManager()) { }

        public LinkedInOAuthClient(string consumerKey, string consumerSecret, IOAuthTokenManager tokenManager)
            : base("linkedin", LinkedInServiceDescription, new SimpleConsumerTokenManager(consumerKey, consumerSecret, tokenManager))
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
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
            var accessToken = response.AccessToken;
            var accessSecret = (response as ITokenSecretContainingMessage).TokenSecret;
            var extraData = response.ExtraData;
            extraData.Add("accesstoken", accessToken);
            extraData.Add("tokensecret", accessSecret);
            const string ProfileRequestUrl = "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,headline,industry,summary)";
            var profileEndpoint = new MessageReceivingEndpoint(ProfileRequestUrl, HttpDeliveryMethods.GetRequest);
            HttpWebRequest request = this.WebWorker.PrepareAuthorizedRequest(profileEndpoint, accessToken);

            try
            {
                using (WebResponse profileResponse = request.GetResponse())
                {
                    using (Stream responseStream = profileResponse.GetResponseStream())
                    {
                        XDocument document = LoadXDocumentFromStream(responseStream);
                        string userId = document.Root.Element("id").Value;

                        string firstName = document.Root.Element("first-name").Value;
                        string lastName = document.Root.Element("last-name").Value;
                        string userName = firstName + " " + lastName;
                        extraData.Add("name", userName);

                        return new AuthenticationResult(
                            isSuccessful: true, provider: this.ProviderName, providerUserId: userId, userName: userName, extraData: extraData);
                    }
                }
            }
            catch (Exception exception)
            {
                return new AuthenticationResult(
                           isSuccessful: true, provider: this.ProviderName, providerUserId: "", userName: "userName", extraData: extraData);
            }


        }

        public List<Contact> GetContacts(string accessToken,int tokenid)
        {
            var contacts = new List<Contact>();
            HttpWebRequest webRequest = this.WebWorker.PrepareAuthorizedRequest(ContactsEndpoint, accessToken);

            using (var webResponse = webRequest.GetResponse())
            using (var stream = webResponse.GetResponseStream())
            {
                if (stream == null)
                    return null;

                using (var textReader = new StreamReader(stream))
                {
                    var body = textReader.ReadToEnd();
                    XDocument contactsXml = XDocument.Parse(body);
                    IEnumerable<XElement> persons = contactsXml.Root.Elements("person");
                    var i = 1;
                    foreach (var person in persons)
                    {
                        contacts.Add(new Contact()
                        {
                            Id=i++,
                            Provider = "linkedin",
                            TokenId = tokenid,
                            UniqueId = person.Element("id") != null ? person.Element("id").Value : "",
                            ProfileUrl = person.Element("public-profile-url") != null ? person.Element("public-profile-url").Value : "",
                            ProfilePictureUrl = person.Element("picture-url") != null ? person.Element("picture-url").Value : "",
                            Name = person.Element("first-name") != null ? person.Element("first-name").Value : "" + " " + person.Element("last-name") != null ? person.Element("last-name").Value : ""

                        });
                    }
                }
            }

            return contacts;
        }

    }
}
