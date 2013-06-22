using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI.UserControls
{
    public partial class UserFeedList : System.Web.UI.UserControl
    {
        public FeedManager.FeedRetrievePage PageType { 
            get { return (FeedManager.FeedRetrievePage)Enum.Parse(typeof(FeedManager.FeedRetrievePage), hdnPageType.Value); }
            set { hdnPageType.Value = ((int)value).ToString(); }
        }
        public string profileUserId
        {
            get { return hdnprofileUserId.Value; }
            set { hdnprofileUserId.Value = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //int? userid =Common.GetLoggedInUserId();
                var user = Common.GetLoginUser(Session);
                if (user != null)
                {
                    var photo = user.PersonalLogoFileStoreId;
                    hdnUserImage.Value = new FileStoreService().GetDownloadUrlDirect(photo);
                }
                else
                    hdnUserImage.Value = "";
            }
        }
    }
}