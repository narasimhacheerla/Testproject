using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util;
using Snovaspace.Util.Logging;
using Snovaspace.Util.FileDataStore;


namespace Huntable.UI
{
    public partial class AddVideos : System.Web.UI.Page
    {

        public const string PicturesStateViewStateKey = "Videos"; 
        public static IList<string> zi = new List<string>();
        public static IList<string> ji = new List<string>();
        public static IList<string> de = new List<string>();
        public static IList<DateTime> dt = new List<DateTime>();
        public IDictionary<string,string> Videos
        {
            get
            {
                return (IDictionary<string, string>)(ViewState[PicturesStateViewStateKey] ?? new Dictionary<string,string>());
            }
            set
            {
                ViewState[PicturesStateViewStateKey] = value;
            }
        }
        public int LoginUserId
        {
            get
            {
                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;
                return 0;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Videos =new Dictionary<string, string>(Videos);
            }
        }

        protected void Addpic(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Addpic - AddVideos.aspx");
            if(tx.Text != "")
            {
            string vidoeurl = tx.Text;
            string ttl = txd.Text;
            DateTime dat = DateTime.Now;
            if ((vidoeurl.Contains("?v=") == true))
            {
               //string res = "?v=";
              string[]  result = vidoeurl.Split(new string[]{"?v="}, StringSplitOptions.RemoveEmptyEntries);
                string newurl = result[1];
                vidoeurl = "http://www.youtube.com/embed/" +newurl;
            }
                if (Videos.Any(x => x.Key == ttl))
                {
                    ttl = ttl + randomstring(6);


                }
                de.Add(ttl);
                dt.Add(dat);
                zi.Add(vidoeurl);
                Videos.Add(ttl,vidoeurl);
                
                rpic.DataSource =Videos.Values;
                rpic.DataBind();


                txd.Text = "";

            tx.Text = "";
            LoggingManager.Debug("Exiting Addpic - AddVideos.aspx");
            }
        }
      
        protected void Uplot(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Uplot - AddVideos.aspx");

              for (int index = 0; index <Videos.Count; index++ )            
            {
                var video = Videos.ElementAt(index);
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {

                    var videos = new UserVideo { UserId = LoginUserId, VideoUrl =video.Value , VideoTitle = video.Key ,AddedDateTime = DateTime.Now };
                    context.AddToUserVideos(videos);
                    context.SaveChanges();
                    FeedManager.addFeedNotification(FeedManager.FeedType.User_Video, LoginUserId, videos.Id, null);
                    var socialManager = new SocialShareManager();
                    var msg = "[UserName]" + " " + "added video";
                    socialManager.ShareVideoOnFacebook(LoginUserId, msg,videos.VideoUrl);
                }

            }
            if (zi.Count == 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Video added succesfully')", true);
            }
            if (zi.Count > 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Videos added succesfully')", true);
            }
            
            zi = new List<string>();
            dt = new List<DateTime>();
            de = new List<string>();
            Videos.Clear();
            rpic.DataSource = null;
            rpic.DataBind();
            LoggingManager.Debug("Exiting Uplot- AddVideos.aspx");
        }
        protected void cancel(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering cancel - AddVideos.aspx");
            zi = new List<string>();
            Videos.Clear();
            rpic.DataSource = null;
            rpic.DataBind();
            LoggingManager.Debug("Exiting cancel - AddVideos.aspx");
        }
        public static string randomstring(int Length)
        {
            string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            Random randNum = new Random();
            char[] chars = new char[Length];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < Length; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }

            return new string(chars);
        }
    }
}