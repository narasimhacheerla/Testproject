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
    
    public partial class Videos : System.Web.UI.Page
    {
        private int? UserId
        {
            get
            {
                LoggingManager.Debug("Entering UserId - Videos.aspx");
                int userId;
                if (int.TryParse(Request.QueryString["UserId"], out userId))
                {
                    LoggingManager.Debug("Exiting UserId - Videos.aspx");
                    return userId;
                }
              
                return null;
            }
        }
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - Videos.aspx");
                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;
                LoggingManager.Debug("Exiting LoginUserId - Videos.aspx");
                return 0;
            }
        }
        protected void RepeaterItemDataBound(object sender, DataListItemEventArgs e)
        {
            LoggingManager.Debug("Entering RepeaterItemDataBound - Videos.aspx");
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            if (UserId != null && UserId.Value == loggedInUserId)
            {
                var button = (ImageButton)e.Item.FindControl("delete");

                if (button != null)
                {
                    button.Visible = true;
                }
            }
            LoggingManager.Debug("Exiting RepeaterItemDataBound - Videos.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - Videos.aspx");

            DisplayVideos();
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


            LoggingManager.Debug("Exiting Page_Load - Videos.aspx");


        }

        private void DisplayVideos()
        {
            LoggingManager.Debug("Entering DisplayVideos - Videos.aspx");
            var objInvManager = new UserManager();
            if (UserId != null)
            {
                var videoList = objInvManager.GetVideos((int)UserId);
                lblname.Text = objInvManager.GetUserName((int)UserId);
                if (videoList.Count != 0)
                {

                    dlvideo.DataSource = videoList;
                    dlvideo.DataBind();
                }
                else
                {
                    lblvideos.Visible = true;
                }
            }
            else
            {
                var loggedInUserId = Common.GetLoggedInUserId(Session);
                if (loggedInUserId != null)
                {
                    var videoList = objInvManager.GetVideos((int) loggedInUserId);
                    lblname.Text = objInvManager.GetUserName((int) loggedInUserId);
                    if (videoList.Count != 0)
                    {

                        dlvideo.DataSource = videoList;
                        dlvideo.DataBind();
                    }
                    else
                    {
                        lblvideos.Visible = true;
                    }
                }
            }
            LoggingManager.Debug("Exiting DisplayVideos - Videos.aspx");
        }


        protected void DeleteClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering DeleteClick - Videos.aspx");
            var button = sender as ImageButton;
            if (button != null)
            {
                int vId = Convert.ToInt32(button.CommandArgument);

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var videoDel = context.UserVideos.FirstOrDefault(s => s.Id == vId);
                    context.DeleteObject(videoDel);
                    context.SaveChanges();
                }
            }
            DisplayVideos();
            LoggingManager.Debug("Exiting DeleteClick - Videos.aspx");
        }
           

    }
}