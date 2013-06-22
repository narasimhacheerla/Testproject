using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;

namespace Huntable.UI.LinkedIn
{
    public class XmlTokenManager : IConsumerTokenManager
    {
        private string _xmlFileName = "~/App_Data/TokensAndSecrets.xml";
        private List<TokenxxxSecret> _tokensAndSecrets;

        public XmlTokenManager(string consumerKey, string consumerSecret)
        {
            if (String.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }

            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;

            Initialize();
        }

        public string ConsumerKey { get; private set; }

        public string ConsumerSecret { get; private set; }

        #region ITokenManager Members
        public string GetTokenSecret(string token)
        {
            TokenxxxSecret tokenAndSecret = _tokensAndSecrets.Find(
                ts => ts.Token.Equals(token, StringComparison.InvariantCultureIgnoreCase));

            return tokenAndSecret != null ? tokenAndSecret.TokenSecret : null;
        }

        public void StoreNewRequestToken(UnauthorizedTokenRequest request, ITokenSecretContainingMessage response)
        {
            CreateToken(response.Token, response.TokenSecret, string.Empty);
        }

        public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
        {
            DeleteToken(requestToken);
            CreateToken(accessToken, accessTokenSecret, HttpContext.Current.Session.SessionID);
        }

        /// <summary>
        /// Classifies a token as a request token or an access token.
        /// </summary>
        /// <param name="token">The token to classify.</param>
        /// <returns>Request or Access token, or invalid if the token is not recognized.</returns>
        public TokenType GetTokenType(string token)
        {
            throw new NotImplementedException();
        }
        #endregion

        public string GetTokenByUserName(string userName)
        {
            TokenxxxSecret tokenAndSecret = _tokensAndSecrets.Find(
                ts => ts.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));

            return tokenAndSecret != null ? tokenAndSecret.Token : null;
        }

        private void Initialize()
        {
            string fullyQualifiedPath = VirtualPathUtility.Combine
                (VirtualPathUtility.AppendTrailingSlash
                     (HttpRuntime.AppDomainAppVirtualPath), _xmlFileName);

            _xmlFileName = HostingEnvironment.MapPath(fullyQualifiedPath);

            // Make sure we have permission to read the XML data source and throw an exception if we don't
            var permission = new FileIOPermission(FileIOPermissionAccess.Write, _xmlFileName);
            permission.Demand();

            ReadDataStore();
        }

        private void ReadDataStore()
        {
            lock (this)
            {
                if (_tokensAndSecrets == null)
                {
                    _tokensAndSecrets = new List<TokenxxxSecret>();
                    var doc = new XmlDocument();
                    doc.Load(_xmlFileName);
                    XmlNodeList nodes = doc.GetElementsByTagName("TokenAndSecret");

                    foreach (XmlNode node in nodes)
                    {
                        var xmlElement = node["Token"];
                        if (xmlElement != null)
                        {
                            var element = node["TokenSecret"];
                            if (element != null)
                            {
                                var xmlElement1 = node["UserName"];
                                if (xmlElement1 != null)
                                    _tokensAndSecrets.Add(new TokenxxxSecret
                                                              {
                                                                  Token = xmlElement.InnerText,
                                                                  TokenSecret = element.InnerText,
                                                                  UserName = xmlElement1.InnerText
                                                              });
                            }
                        }
                    }
                }
            }
        }

        private void CreateToken(string token, string tokenSecret, string userName)
        {
            var doc = new XmlDocument();
            doc.Load(_xmlFileName);

            XmlNode xmlTokenAndSecretRoot = doc.CreateElement("TokenAndSecret");
            XmlNode xmlToken = doc.CreateElement("Token");
            XmlNode xmlTokenSecret = doc.CreateElement("TokenSecret");
            XmlNode xmlUserName = doc.CreateElement("UserName");
            XmlNode xmlTimestamp = doc.CreateElement("Timestamp");

            xmlToken.InnerText = token;
            xmlTokenSecret.InnerText = tokenSecret;
            xmlUserName.InnerText = userName;
            xmlTimestamp.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            xmlTokenAndSecretRoot.AppendChild(xmlToken);
            xmlTokenAndSecretRoot.AppendChild(xmlTokenSecret);
            xmlTokenAndSecretRoot.AppendChild(xmlUserName);
            xmlTokenAndSecretRoot.AppendChild(xmlTimestamp);

            var selectSingleNode = doc.SelectSingleNode("TokensAndSecrets");
            if (selectSingleNode != null)
                selectSingleNode.AppendChild(xmlTokenAndSecretRoot);
            doc.Save(_xmlFileName);

            _tokensAndSecrets.Add(new TokenxxxSecret
                                          {
                                              Token = token,
                                              TokenSecret = tokenSecret,
                                              UserName = userName
                                          });
        }

        private void DeleteToken(string token)
        {
            var doc = new XmlDocument();
            doc.Load(_xmlFileName);

            foreach (XmlNode node in doc.GetElementsByTagName("TokenAndSecret"))
            {
                if (node.ChildNodes[0].InnerText.Equals(token, StringComparison.OrdinalIgnoreCase))
                {
                    doc.SelectSingleNode("TokensAndSecrets").RemoveChild(node);
                    doc.Save(this._xmlFileName);

                    this._tokensAndSecrets.Remove(this._tokensAndSecrets.Find(delegate(TokenxxxSecret t)
                                                                                {
                                                                                    return t.Token.Equals(token, StringComparison.InvariantCultureIgnoreCase);
                                                                                }));

                    return;
                }
            }

            return;
        }
    }
}
