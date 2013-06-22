using System;
using System.Configuration;
using LinkedIn;

namespace Huntable.UI.LinkedIn
{
    public class LinkedInBasePage : System.Web.UI.Page
    {
        private string AccessToken
        {
            get 
            {
                var accessToken = (string)Session["AccessToken"];
                if (string.IsNullOrEmpty(accessToken))
                {
                    accessToken = TokenManager.GetTokenByUserName(Session.SessionID);
                }

                return accessToken;
            }
            set { Session["AccessToken"] = value; }
        }

        private XmlTokenManager TokenManager
        {
            get
            {
                var tokenManager = (XmlTokenManager)Application["TokenManager"];
                if (tokenManager == null)
                {
                    string consumerKey = ConfigurationManager.AppSettings["LinkedInConsumerKey"];
                    string consumerSecret = ConfigurationManager.AppSettings["LinkedInConsumerSecret"];
                    if (string.IsNullOrEmpty(consumerKey) == false)
                    {
                        tokenManager = new XmlTokenManager(consumerKey, consumerSecret);
                        Application["TokenManager"] = tokenManager;
                    }
                }

                return tokenManager;
            }
        }

        protected WebOAuthAuthorization Authorization
        {
            get;
            private set;
        }

        protected override void OnLoad(EventArgs e)
        {
            Authorization = new WebOAuthAuthorization(TokenManager, AccessToken);

            if (!IsPostBack)
            {
                string accessToken = Authorization.CompleteAuthorize();
                if (accessToken != null)
                {
                    AccessToken = accessToken;

                    Response.Redirect(Request.Path, false);
                }

                if (AccessToken == null) Authorization.BeginAuthorize();
            }

            base.OnLoad(e);
        }
    }
}
