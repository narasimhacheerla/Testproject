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
    public partial class ajax_picture : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load -  ajax-picture .aspx");
            if (!IsPostBack)
            {
                if (Request[Huntable.Business.FeedManager.ajaxPopup.ProfilePhotoId.ToString()] != null)
                {
                    int id = Convert.ToInt32(Request[Huntable.Business.FeedManager.ajaxPopup.ProfilePhotoId.ToString()]);
                    Getdata(FeedManager.FeedType.Profile_Picture.ToString(), id);
                }
                else if (Request[Huntable.Business.FeedManager.ajaxPopup.UserPhotoId.ToString()] != null)
                {
                    int id = Convert.ToInt32(Request[Huntable.Business.FeedManager.ajaxPopup.UserPhotoId.ToString()]);
                    Getdata(FeedManager.FeedType.User_Photo.ToString(), id);
                }
                else if (Request[Huntable.Business.FeedManager.ajaxPopup.UserPortfolioPhotoId.ToString()] != null)
                {
                    int id = Convert.ToInt32(Request[Huntable.Business.FeedManager.ajaxPopup.UserPortfolioPhotoId.ToString()]);
                    Getdata(FeedManager.FeedType.User_Potfolio_Photo.ToString(), id);
                }
                else if (Request[Huntable.Business.FeedManager.ajaxPopup.CompanyPortfolioPhotoId.ToString()] != null)
                {
                    int id = Convert.ToInt32(Request[Huntable.Business.FeedManager.ajaxPopup.CompanyPortfolioPhotoId.ToString()]);
                    Getdata(FeedManager.FeedType.Company_Portfolio_Photo.ToString(), id);
                }
                else if (Request[Huntable.Business.FeedManager.ajaxPopup.UserVideoId.ToString()] != null)
                {
                    int id = Convert.ToInt32(Request[Huntable.Business.FeedManager.ajaxPopup.UserVideoId.ToString()]);
                    Getdata(FeedManager.FeedType.User_Video.ToString(), id);
                }
                else if (Request[Huntable.Business.FeedManager.ajaxPopup.CompanyVideoId.ToString()] != null)
                {
                    int id = Convert.ToInt32(Request[Huntable.Business.FeedManager.ajaxPopup.CompanyVideoId.ToString()]);
                    Getdata(FeedManager.FeedType.Company_Video.ToString(), id);
                }
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    //int? userid =Common.GetLoggedInUserId();
                    var user = Common.GetLoginUser(Session);
                    if (user != null)
                    {
                        var photo = user.PersonalLogoFileStoreId;
                        UserFeedComments1.UserImage = new FileStoreService().GetDownloadUrl(photo);
                    }
                    else
                        UserFeedComments1.UserImage = "";
                }
                LoggingManager.Debug("Exiting Page_Load -  ajax-picture .aspx");
            }

        }
        public void Getdata(string type, int refRecordId)
        {
            LoggingManager.Debug("Entering Getdata -  ajax-picture .aspx");

            PopupDetail Detail = new PopupDetail();
            int currentUserId = Common.GetLoggedInUserId() ?? 0;
            Detail = FeedManager.getPopupDetail(type, currentUserId, refRecordId);

            LitOwner.Text = Detail.OwnerDetail;
            litDescription.Text = Detail.Description;
            litDetail.Text = Detail.Detail;
            lblTimeStamp.Text = Detail.TimeStamp;
            hdnPictureFeedType.Value = Detail.FeedType;

            if (Detail.PrevRecordId > 0)
            {
                imgPrev.CommandArgument = Detail.PrevRecordId.ToString();
            }
            else
                imgPrev.Visible = false;
            if (Detail.NextRecordId > 0)
            {
                imgNext.CommandArgument = Detail.NextRecordId.ToString();
            }
            else
                imgNext.Visible = false;

            UserFeedLikedUser1.Visible = true;
            UserFeedLikedUser1.FeedId = Detail.feedId.ToString();
            UserFeedLikedUser1.FeedType = Detail.LikeFeedType;
            UserFeedLikedUser1.RefRecordId = Detail.RefRecordId.ToString();

            UserFeedComments1.Visible = true;
            UserFeedComments1.FeedId = Detail.feedId.ToString();
            UserFeedComments1.FeedType = Detail.CommentFeedType;
            UserFeedComments1.RefRecordId = Detail.RefRecordId.ToString();
            UserFeedComments1.FeedUserId = Detail.feedUserId.ToString();
            LoggingManager.Debug("Exiting Getdata -  ajax-picture .aspx");

        }
        protected void imgPrev_Click(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering imgPrev_Click -  ajax-picture .aspx");
            ImageButton btn = (ImageButton)sender;
            Getdata(hdnPictureFeedType.Value, Convert.ToInt32(btn.CommandArgument));
            LoggingManager.Debug("Entering imgPrev_Click -  ajax-picture .aspx");
        }

        protected void imgNext_Click(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering imgNext_Click -  ajax-picture .aspx");
            ImageButton btn = (ImageButton)sender;
            Getdata(hdnPictureFeedType.Value, Convert.ToInt32(btn.CommandArgument));
            LoggingManager.Debug("Entering imgNext_Click -  ajax-picture .aspx");
        }

    }
}