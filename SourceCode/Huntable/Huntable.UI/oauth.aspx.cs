using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using Huntable.Business;
using Huntable.Data;
using System.Xml.Linq;
using System.ComponentModel;
using OAuthUtility;
using Snovaspace.Util.Logging;




public partial class oauth : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        LoggingManager.Debug("Entering Page_Load - oauth.aspx");

        if (!IsPostBack)
        {

            if (Request.QueryString["currpage"] != null && Request.QueryString["currpage"] == "live")
                Session["currpage"] = Request.QueryString["currpage"];

            if (Session["currpage"] != null && Session["currpage"].ToString() == "live")
            {
               
                    var wll = new WindowsLiveLogin(true);
                    var consentUrl = wll.GetConsentUrl("Contacts.View");
                    var req = HttpContext.Current.Request;
                    var action = req["action"];

                    if (LiveUserToken == null && action == "login")
                    {
                        var user = wll.ProcessLogin(req.Form);
                        if (user != null)
                        {
                            LiveUserToken = user.Token;
                        }
                        Response.Redirect(consentUrl, false);
                        return;
                    }

                    if (LiveConcentToken == null && action == "delauth")
                    {
                        var user = wll.ProcessToken(LiveUserToken);

                        if (user != null)
                        {
                            var ct = wll.ProcessConsent(req.Form);
                            if ((ct != null) && ct.IsValid())
                            {

                                var contacts = LiveContacts(ct);
                                Session["contacts"] = contacts;
                                Session["currpage"] = null;

                                var returnurl = "SendInvitations.aspx";
                                if (Session["oauthmode"] != null && Session["oauthmode"].ToString() == "email")
                                {
                                    returnurl = "email-contacts.aspx";
                                    Session["oauthmode"] = null;
                                }
                                Response.Redirect(returnurl, false);


                                return;
                            }
                        }

                    }
                    Response.Redirect("http://login.live.com/wlogin.srf?appid=" + wll.AppId);
               
                //http://login.live.com/wlogin.srf?appid=00000000440CD409&alg=wsignin1.0
             

            }
            else
            {
                var result = OAuthWebSecurity.VerifyAuthentication();
                var returnurl = "";
                if (result.IsSuccessful)
                {
                    var extradata = result.ExtraData;
                   
                    var token = extradata["accesstoken"];
                    var secret = "";
                    if(extradata.ContainsKey("tokensecret"))
                      secret = extradata["tokensecret"];

                    List<Contact> contacts = null;
                    int? tokenid = null;
                    var user = Common.GetLoginUser(Session);
                    if (user != null)
                    {
                        using (var context = huntableEntities.GetEntitiesWithNoLock())
                        {
                            var isNew = false;
                            var oauthtoken =
                                context.OAuthTokens.FirstOrDefault(
                                    o => o.ProviderUserId == result.ProviderUserId && o.Provider == result.Provider && o.UserId==user.Id);
                            if (oauthtoken == null)
                            {
                                var username = result.UserName;
                                if (extradata.ContainsKey("name"))
                                    username = extradata["name"];
                                oauthtoken = new OAuthToken();
                                oauthtoken.Provider = result.Provider;
                                oauthtoken.ProviderUserId = result.ProviderUserId;
                                oauthtoken.ProviderUserName = username;
                                oauthtoken.CreatedOn = DateTime.Now;
                                oauthtoken.UserId = user.Id;
                                isNew = true;
                            }
                            oauthtoken.Token = token;
                            oauthtoken.Secret = secret;
                            oauthtoken.UpdatedOn = DateTime.Now;
                            if (isNew)
                            {
                                context.OAuthTokens.AddObject(oauthtoken);
                            }
                            context.SaveChanges();

                            tokenid = oauthtoken.Id;
                        }
                    }

                    switch (result.Provider)
                    {
                        case "google":
                            contacts = GoogleOAuth2Client.GetContacts(token);
                            returnurl = "SendInvitations.aspx";
                            break;
                        case "facebook":
                            returnurl = "Facebookfriends.aspx?tokenid=" + tokenid;
                            break;
                        case "yahoo":
                            {
                                var client = (YahooOAuthClient)OAuthWebSecurity.GetOAuthClient("yahoo");
                                contacts = client.GetContacts(token, result.ProviderUserId);
                                returnurl = "SendInvitations.aspx";
                            }
                            break;
                        case "twitter":
                            returnurl = "TwitterFriends.aspx?tokenid=" + tokenid;
                            break;
                        case "linkedin":
                            returnurl = "LinkedInFriends.aspx?tokenid=" + tokenid;
                            break;
                    }

                    if (contacts != null)
                    {
                        foreach (var contact in contacts)
                        {
                            contact.Provider = result.Provider;
                            if (tokenid != null) contact.TokenId = (int)tokenid;
                        }

                        Session["contacts"] = contacts;
                    }

                    if (Session["oauthmode"] != null)
                    {
                        switch (Session["oauthmode"].ToString())
                        {
                            case "email":
                                returnurl = "email-contacts.aspx";
                                break;
                            case "facebookconnect":
                                returnurl = "HomePageAfterLoggingIn.aspx";
                                break;
                            case "socialshare":
                                returnurl = "HomePageAfterLoggingIn.aspx";
                                break;
                            case "visualactivity":
                                returnurl = "VisualCvActivity.aspx";
                                break;
                                
                        }

                    }

                }
                else
                {
                    returnurl = "InviteFriends.aspx?oauth=fail";

                    if (Session["oauthmode"] != null)
                    {
                        switch (Session["oauthmode"].ToString())
                        {
                            case "email":
                                returnurl = "contact-invitepage.aspx?oauth=fail";
                                break;
                            case "facebookconnect":
                                returnurl = "FacebookAuthenticate.aspx?oauth=fail";
                                break;
                            case "socialshare":
                                returnurl = "HomePageAfterLoggingIn.aspx?oauth=fail";
                                break;
                            case "visualactivity":
                                returnurl = "VisualCvActivity.aspx?oauth=fail";
                                break;
                        }

                    }
                   
                }

                Session["oauthmode"] = null;

                Response.Redirect(returnurl, false);

            }

          
         
        }

        LoggingManager.Debug("Exiting Page_Load - oauth.aspx");

    }


    private List<Contact> LiveContacts(WindowsLiveLogin.ConsentToken cToken)
    {
        LoggingManager.Debug("Entering LiveContacts - oauth.aspx");
        var uri = "https://livecontacts.services.live.com/@L@" + cToken.LocationID + "/rest/LiveContacts/Contacts/";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
        request.UserAgent = "Windows Live Data Interactive SDK";
        request.ContentType = "application/xml; charset=utf-8";
        request.Method = "GET";
        request.Headers.Add("Authorization", "DelegatedToken dt=\"" + cToken.DelegationToken + "\"");

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        var body = new StreamReader(response.GetResponseStream()).ReadToEnd();

        IEnumerable<Contact> contacts = new BindingList<Contact>();

        try
        {
            var contactsXml = XDocument.Parse(body);
            var id = 1;
            contacts = from contact in contactsXml.Descendants("Contact")
                       where contact.Element("Emails") != null
                       select new Contact
                       {
                           Id = id++,
                           Provider = "live",
                           UniqueId = contact.Descendants("ID").First().Value,
                           Name = contact.Descendants("Profiles").FirstOrDefault() != null ? contact.Descendants("Profiles").First().Descendants("Personal").FirstOrDefault() != null ?
                                     contact.Descendants("Profiles").First().Descendants("Personal").First().Descendants("FirstName").FirstOrDefault() != null ? contact.Descendants("Profiles").First().Descendants("Personal").First().Descendants("FirstName").First().Value : null : null : null,
                           Email = contact.Descendants("Emails").FirstOrDefault() != null ? contact.Descendants("Emails").First().Descendants("Email").FirstOrDefault() != null ?
                                     contact.Descendants("Emails").First().Descendants("Email").First().Descendants("Address").FirstOrDefault() != null ? contact.Descendants("Emails").First().Descendants("Email").First().Descendants("Address").First().Value : null : null : null,
                           ProfilePictureUrl = ""
                       };

        }

        catch (Exception ex)
        {
            LoggingManager.Error(ex);
        }

        LoggingManager.Debug("Exiting LiveContacts - oauth.aspx");
        return contacts.Where(u => u.Email != "").ToList();
    }

    //private void SendInvitaion(string invId)
    //{
    //    LoggingManager.Debug("Entering SendInvitaion - oauth.aspx");

    //    using (var context = huntableEntities.GetEntitiesWithNoLock())
    //    {
    //        var id = int.Parse(invId);
    //        var inv = context.Invitations.FirstOrDefault(i => i.Id == id);
    //        var baseUrl = new Snovaspace.Util.Utility().GetApplicationBaseUrl();
    //        var objEmailTemplate = new EmailTemplateManager();
    //        var template = objEmailTemplate.GetTemplate("LinkedInInvitation");
    //        if (inv != null)
    //        {
    //            var url = baseUrl + "Default.aspx?ref=" + inv.Id;

    //            if(inv.CustomInvitationId.HasValue)
    //                url = baseUrl + "CustomizedHomepage.aspx?ref=" + inv.Id;
    //            switch (inv.InvitationTypeId)
    //            {
    //                case (int)InvitationType.Facebook:
    //                    {
    //                        var fbClient = new FacebookClient();
    //                        long uid;
    //                        long.TryParse(inv.UniqueId, out uid);
    //                        fbClient.SendInvite(FacebookClient, url, uid, inv.Name);
    //                    }
    //                    break;
    //                case (int)InvitationType.Linkedin:
    //                    {
    //                        var authorization = (WebOAuthAuthorization)Session["LinkedInAuthorization"];
    //                        var service = new LinkedInService(authorization);
    //                        var body = template.TemplateText;
    //                        body = body.Replace("[NAME]", string.Format("{0}", inv.Name));
    //                        body = body.Replace("[LINK]", url);

    //                        IEnumerable<string> ids = new[] { inv.UniqueId };
    //                        service.SendMessage(template.Subject, body, ids, true);
    //                    }
    //                    break;
    //                case (int)InvitationType.Twitter:
    //                    {
    //                        var tokens = new OAuthTokens
    //                                         {
    //                                             AccessToken = (string)Session["TwitterAccessToken"],
    //                                             AccessTokenSecret = (string)Session["TwitterTokenSecret"],
    //                                             ConsumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"],
    //                                             ConsumerSecret =
    //                                                 ConfigurationManager.AppSettings["twitterConsumerSecret"]
    //                                         };
    //                        var uid = decimal.Parse(inv.UniqueId);
    //                        var fName = inv.Name.Length > 15 ? inv.Name.Split(' ')[0] : inv.Name;

    //                        string body = "Hi [NAME] I am inviting you to join my network in Huntable.Click here to connect [LINK]";
    //                        body = body.Replace("[NAME]", string.Format("{0}", fName));
    //                        body = body.Replace("[LINK]", url);
    //                        TwitterDirectMessage.Send(tokens, uid, body);
    //                    }
    //                    break;
    //            }
    //        }
    //    }
    //    const string popupScript = "<script language='javascript'>closepopup();</script>";
    //    Page.ClientScript.RegisterStartupScript(GetType(), "PopupScript", popupScript);

    //    LoggingManager.Debug("Exiting SendInvitaion - oauth.aspx");
    //}

 
    private string LiveUserToken
    {
        get { return (string)Session["LiveUserToken"]; }
        set { Session["LiveUserToken"] = value; }
    }


    private string LiveConcentToken
    {
        get { return (string)Session["LiveConcentToken"]; }
        set { Session["LiveConcentToken"] = value; }
    }

    private string YahooAccessToken
    {
        get { return (string)Session["YahooAccessToken"]; }
        set { Session["YahooAccessToken"] = value; }
    }


    private string YahooGuid
    {
        get { return (string)Session["YahooGuid"]; }
        set { Session["YahooGuid"] = value; }
    }

    private string GoogleAccessToken
    {
        get { return (string)Session["GoogleAccessToken"]; }
        set { Session["GoogleAccessToken"] = value; }
    }

    private string FacebookAccessToken
    {
        get { return (string)Session["FacebookAccessToken"]; }
        set { Session["FacebookAccessToken"] = value; }
    }

    private string LinkedInAccessToken
    {
        get { return (string)Session["LinkedInAccessToken"]; }
        set { Session["LinkedInAccessToken"] = value; }
    }

    private string TwitterAccessToken
    {
        get { return (string)Session["TwitterAccessToken"]; }
        set { Session["TwitterAccessToken"] = value; }
    }

    private string TwitterTokenSecret
    {
        get { return (string)Session["TwitterTokenSecret"]; }
        set { Session["TwitterTokenSecret"] = value; }
    }


   

    //private MyTokenManager GoogleTokenManager
    //{
    //    get
    //    {
    //        var tokenManager = (MyTokenManager)Application["GoogleTokenManager"];
    //        if (tokenManager == null)
    //        {
    //            var consumerKey = ConfigurationManager.AppSettings["googleConsumerKey"];
    //            var consumerSecret = ConfigurationManager.AppSettings["googleConsumerSecret"];
    //            if (!string.IsNullOrEmpty(consumerKey))
    //            {
    //                tokenManager = new MyTokenManager(consumerKey, consumerSecret);
    //                Application["GoogleTokenManager"] = tokenManager;
    //            }
    //        }


    //        return tokenManager;
    //    }
    //}


    //private MyTokenManager YahooTokenManager
    //{
    //    get
    //    {
    //        var tokenManager = (MyTokenManager)Application["YahooTokenManager"];
    //        if (tokenManager == null)
    //        {
    //            var consumerKey = ConfigurationManager.AppSettings["YahooConsumerKey"];
    //            var consumerSecret = ConfigurationManager.AppSettings["YahooConsumerSecret"];
    //            if (!string.IsNullOrEmpty(consumerKey))
    //            {
    //                tokenManager = new MyTokenManager(consumerKey, consumerSecret);
    //                Application["YahooTokenManager"] = tokenManager;
    //            }
    //        }

    //        return tokenManager;
    //    }
    //}

    //private MyTokenManager TwitterTokenManager
    //{
    //    get
    //    {
    //        var tokenManager = (MyTokenManager)Application["TwitterTokenManager"];
    //        if (tokenManager == null)
    //        {
    //            var consumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"];
    //            var consumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"];
    //            if (!string.IsNullOrEmpty(consumerKey))
    //            {
    //                tokenManager = new MyTokenManager(consumerKey, consumerSecret);
    //                Application["TwitterTokenManager"] = tokenManager;
    //            }
    //        }

    //        return tokenManager;
    //    }
    //}

    //private MyTokenManager LinkedInTokenManager
    //{
    //    get
    //    {
    //        var tokenManager = (MyTokenManager)Application["LinkedInTokenManager"];
    //        if (tokenManager == null)
    //        {
    //            var consumerKey = ConfigurationManager.AppSettings["LinkedInConsumerKey"];
    //            var consumerSecret = ConfigurationManager.AppSettings["LinkedInConsumerSecret"];
    //            if (!string.IsNullOrEmpty(consumerKey))
    //            {
    //                tokenManager = new MyTokenManager(consumerKey, consumerSecret);
    //                Application["LinkedInTokenManager"] = tokenManager;
    //            }
    //        }

    //        return tokenManager;
    //    }
    //}
}
