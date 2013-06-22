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
	public partial class Pictures : System.Web.UI.Page
    {
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
            LoggingManager.Debug("Entering Page_Load - Pictures.aspx");

            DisplayPictures();
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                if (loggedInUserId != null)
                {


                    var usri = context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
                    if (usri != null && usri.IsPremiumAccount == null)
                    {
                        bimage.Visible = true;
                        pimage.Visible = false;
                    }
                    else
                    {
                        bimage.Visible = false;
                        pimage.Visible = true;
                    }
                }
                else
                {
                    bimage.Visible = true;
                    pimage.Visible = false;
                }
            }
            LoggingManager.Debug("Exiting Page_Load - Pictures.aspx");

        }
        protected void RepeaterItemDataBound(object sender, DataListItemEventArgs e)
        {
            LoggingManager.Debug("Entering RepeaterItemDataBound - Pictures.aspx");
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            if((OtherUserId == null && loggedInUserId.HasValue) || OtherUserId.Value==loggedInUserId)
            {
                var button = (ImageButton)e.Item.FindControl("delete");
               
                if (button != null)
                {
                    button.Visible = true;
                }
            }
            LoggingManager.Debug("Exiting RepeaterItemDataBound - Pictures.aspx");
        }

	    protected void DisplayPictures()
        {
            LoggingManager.Debug("Entering DisplayPictures - Pictures.aspx");
            var objInvManager = new UserManager();
            if (OtherUserId != null)
            {
                var pictureList = objInvManager.GetPictures(OtherUserId.Value);
                uname.Text = objInvManager.GetUserName(OtherUserId.Value);
                vw.HRef = new UrlGenerator().UserUrlGenerator(OtherUserId.Value);
                if (pictureList.Count != 0)
                {
                    pictureList.Reverse();
                    dlpicture.DataSource = pictureList;
                    dlpicture.DataBind();
                }
                else
                {
                    lblpictures.Visible = true;
                }
            }
            if (OtherUserId == null)
            {
                var pictureListo = objInvManager.GetPictures(LoginUserId);
                uname.Text = objInvManager.GetUserName(LoginUserId);
                vw.HRef = "viewuserprofile.aspx?Id";
                if (pictureListo.Count != 0)
                {
                    pictureListo.Reverse();
                    dlpicture.DataSource = pictureListo;
                    dlpicture.DataBind();
                }
                else
                {
                    lblpictures.Visible = true;
                }
            }
            LoggingManager.Debug("Exiting DisplayPictures - Pictures.aspx");
        }

        protected void DeleteClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering DeleteClick - Pictures.aspx");
             var button = sender as ImageButton;
             if (button != null)
             {
                 int pId = Convert.ToInt32(button.CommandArgument);
                 using (var context = huntableEntities.GetEntitiesWithNoLock())
                 {
                     var videoDel = context.UserPortfolios.FirstOrDefault(s => s.Id == pId);
                     context.DeleteObject(videoDel);
                     context.SaveChanges();
                 }

             }
             DisplayPictures();
             LoggingManager.Debug("Exiting DeleteClick - Pictures.aspx");
        }
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - Pictures.aspx");
            if (id != null)
            {
                int p = Int32.Parse(id.ToString());


                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {

                    return new FileStoreService().GetDownloadUrl(p);
                }

            }
            else
            {
                LoggingManager.Debug("Exiting Picture - Pictures.aspx");
                return null;
            }

        }
        private int? OtherUserId
        {
            get
            {
                int otherUserId;
                if (int.TryParse(Request.QueryString["UserId"], out otherUserId))
                {
                    return otherUserId;
                }
                return null;
            }
        }


            //}
            //public int i = 0;
            //int?[] j  = new int?[10];
            //protected void addpic(object sender, EventArgs e)
            //{
            //    var filestore = new FileStoreService();
            //    int? imId = filestore.LoadFileFromFileUpload("picture", fp);
            //    im.ImageUrl = "~/LoadFile.ashx?id=" + imId;
            //   // IList<int?> pic = new List<int?>();
            //  // pic.Add(imId);
            //    pictureupload(imId);


            //}
            //public void pictureupload(int? pic)
            //{

            //    j[i] = pic;
            //     i++;

            //}
            ////protected void uplot(object sender, EventArgs e)
            ////{
            ////    int k;
            ////    for (k = 0; k < i; k++)
            ////    {
            ////       using(var context = huntableEntities.GetEntitiesWithNoLock())
            ////       {
            ////           var userport = new UserPortfolio { UserId = LoginUserId, PictureId = j.ToString() };
            ////            context.AddToUserPortfolios(userport);
            ////            context.SaveChanges();
            ////            FeedManager.addFeedNotification(FeedManager.FeedType.User_Potfolio_Photo,LoginUserId,userport.Id,null);

            ////       }

            ////    }
            //    i = 0;
            //}
        
	}
}