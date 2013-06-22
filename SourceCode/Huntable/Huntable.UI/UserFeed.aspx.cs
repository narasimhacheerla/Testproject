using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class UserFeed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - UserFeed");

            if (Request["feedId"] != null)
            {
                int feedId = Convert.ToInt32(Request["feedId"]);
                int? userid = Common.GetLoggedInUserId();

                List<int> retrieveUserList_1st = new List<int>();
                List<int> retrieveUserList_2nd = new List<int>();
                List<int> retrieveUserList_3rd = new List<int>();
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    #region retrieveFeedUserList
                    retrieveUserList_1st = (from a in context.Users
                                            where a.ReferralId == userid
                                            select a.Id).ToList();

                    retrieveUserList_2nd = (from b in context.Users
                                            where b.User1.Any(a => a.ReferralId == userid)
                                            select b.Id).ToList();

                    retrieveUserList_3rd = (from c in context.Users
                                            where c.User1.Any(a => a.User1.Any(b => b.ReferralId == userid))
                                            select c.Id).ToList();
                    #endregion
                    var user = Common.GetLoginUser(Session);
                    if (user != null)
                    {
                        var photo = user.PersonalLogoFileStoreId;
                        hdnUserImage.Value = new FileStoreService().GetDownloadUrlDirect(photo);
                    }
                    else
                        hdnUserImage.Value = "";
                }
                var detail = FeedManager.getMainFeed(feedId, userid ?? 0, retrieveUserList_1st, retrieveUserList_2nd, retrieveUserList_3rd);                
                litFeed.Text = detail.feedDescription;
                hdnFeedId.Value = detail.feedId.ToString();
            }
            LoggingManager.Debug("Exiting Page_Load - UserFeed");
        }
    }
}