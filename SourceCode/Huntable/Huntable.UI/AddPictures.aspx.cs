using System;
using System.Collections.Generic;
using System.Linq;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class AddPictures : System.Web.UI.Page
    {
        public const string PicturesStateViewStateKey = "Pictures"; 
        public static IList<int?> Zi = new List<int?>();
        public static IList<int?> Ji = new List<int?>();
        public static IList<string> De = new List<string>();
        public static IList<DateTime> Dt = new List<DateTime>();
        public IDictionary<int,string> Pictures
        {
            get
            {
                return (IDictionary<int, string>)(ViewState[PicturesStateViewStateKey] ?? new Dictionary<int,string>());
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
            LoggingManager.Debug("Entering Page_Load - AddPictures.aspx");
           
            if(!Page.IsPostBack)
            {
              Pictures =new Dictionary<int, string>(Pictures);
            }
            LoggingManager.Debug("Exiting Page_Load - Addpictures.aspx");
        }
     
        protected void Addpic(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Addpic - AddPictures.aspx");
            if (fp.HasFile)
            {
              
                var filestore = new FileStoreService();
                int? imId = filestore.LoadImageAndResize("picture", fp);
                Pictures.Add((int) imId,txd.Text);
                string des = txd.Text;
                DateTime dat = DateTime.Now;
                Dt.Add(dat);
                De.Add(des);
                Zi.Add(imId);
              
                rpic.DataSource = Pictures.Keys;
                rpic.DataBind();

                txd.Text = "";
                LoggingManager.Debug("Exiting Addpic - AddPictures.aspx");

               
            }

        }
     
        protected void Uplot(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Uplot - AddPictures.aspx");

            for (int index = 0; index <Pictures.Count; index++ )
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var pictureid = Pictures.ElementAt(index);
                    var userport = new UserPortfolio { UserId = LoginUserId, PictureId =pictureid.Key ,PictureDescription = pictureid.Value , AddedDateTime = DateTime.Now};
                    context.AddToUserPortfolios(userport);
                    context.SaveChanges();
                    FeedManager.addFeedNotification(FeedManager.FeedType.User_Photo, LoginUserId, userport.Id, null);
                    var socialManager = new SocialShareManager();
                    var msg = "https://huntable.co.uk/LoadFile.ashx?id="+pictureid.Value;                   
                    socialManager.ShareOnFacebook(LoginUserId, "[UserName] has added a picture", msg);
                   

                }

            }
            if (Zi.Count == 1)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Picture added succesfully')", true);
            }
            if (Zi.Count > 1)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('Pictures added succesfully')", true);
            }

            Zi = new List<int?>();
            De = new List<string>();
            Pictures.Clear();
            rpic.DataSource = null;
            rpic.DataBind();
            LoggingManager.Debug("Exiting Uplot - AddPictures.aspx");
        }
        protected void Cancel(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Cancel - AddPictures.aspx");


            Zi = new List<int?>();
            De = new List<string>();
            Pictures.Clear();
            rpic.DataSource = null;
            rpic.DataBind();
            LoggingManager.Debug("Exiting Cancel - AddPictures.aspx");
        }
    }
}