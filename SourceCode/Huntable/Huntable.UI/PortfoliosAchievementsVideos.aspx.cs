using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Data;
using Huntable.Business;
using System.Web.UI.HtmlControls;
using Snovaspace.Util.Logging;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI
{
    public partial class PortfoliosAchievementsVideos : System.Web.UI.Page
    {
        private List<EmploymentHistory> EmploymentHistories
        {
            get
            {
                LoggingManager.Debug("Entering EmploymentHistories - PortfoliosAchievementsVideos.aspx");
                LoggingManager.Debug("Exiting EmploymentHistories - PortfoliosAchievementsVideos.aspx");
                return Session["EmpHistories"] as List<EmploymentHistory>;
            }
        }

        private int LoginUserId
        {
            
            get
            {
                LoggingManager.Debug("Entering LoginUserId - PortfoliosAchievementsVideos.aspx");
                LoggingManager.Debug("Exiting LoginUserId - PortfoliosAchievementsVideos.aspx");
                return Common.GetLoggedInUserId(Session).Value;
            }
           
        }

        private string EmploymentHistoryId
        {
            get
            {
                return (String.IsNullOrEmpty(Request.QueryString["id"]) ? null : Request.QueryString["id"].ToString());
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Init - PortfoliosAchievementsVideos.aspx");

            DisplayVideos();
            DisplayPortfolios();
            DisplayAchievements();

            LoggingManager.Debug("Exiting Page_Init - PortfoliosAchievementsVideos.aspx");
        }

        private void DisplayPortfolios()
        {
            LoggingManager.Debug("Entering DisplayPortfolios - PortfoliosAchievementsVideos.aspx");
            //pnlPortfolio.Controls.Clear();
            //using (var context = huntableEntities.GetEntitiesWithNoLock())
            //{
            //    int eid = Convert.ToInt32(EmploymentHistoryId);
            //    EmploymentHistory history = context.EmploymentHistories.First(h => h.Id == eid);
            //    int portfolioCount = 0;
            //    foreach (var p in history.EmploymentHistoryPortfolios)
            //    {
            //        portfolioCount++;
            //        var url = UserProfileManager.GetPortfolioPictureDisplayUrl(p.FileId);
            //        Image i = new Image();
            //        i.ImageUrl = url;
            //        i.Width = 79;
            //        i.Height = 91;
            //        i.CssClass = "profile-pic floatleft";
            //        pnlPortfolio.Controls.Add(i);
            //    }
            //}
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int eid = Convert.ToInt32(EmploymentHistoryId);
                var result = context.EmploymentHistories.First(x => x.Id == eid);
                var history = result.EmploymentHistoryPortfolios.ToList();
               //  history = context.EmploymentHistories.First(s => s.Id == eid);
                dlportfolio.DataSource = history ;
                dlportfolio.DataBind();
            }

            LoggingManager.Debug("Exiting DisplayPortfolios - PortfoliosAchievementsVideos.aspx");
        }

        protected void DeleteClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering DeleteClick");
            var button = sender as ImageButton;
            int eid = Convert.ToInt32(EmploymentHistoryId);

            if (button != null)
            {
                int Id = Convert.ToInt32(button.CommandArgument);

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var selected_row = context.EmploymentHistoryPortfolios.First(x => x.FileId == Id && x.EmplementHistoryId == eid);
                    context.DeleteObject(selected_row);
                    context.SaveChanges();

                    DisplayPortfolios();
                   
                }



            }
            LoggingManager.Debug("Exiting FollowupClick - CompaniesCountry");
        }
        public static string GetPortfolioPictureDisplayUrl(object id)
        {
            LoggingManager.Debug("Entering GetPortfolioPictureDisplayUrl - PortfoliosAchievementsVideos.aspx");
            int _id = Convert.ToInt32(id);
            LoggingManager.Debug("Exiting GetPortfolioPictureDisplayUrl - PortfoliosAchievementsVideos.aspx");
            return new FileStoreService().GetDownloadUrl(_id);
            

        }
        private void DisplayVideos()
        {

            LoggingManager.Debug("Entering DisplayVideos - PortfoliosAchievementsVideos.aspx");

            //pnlVideo.Controls.Clear();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int eid = Convert.ToInt32(EmploymentHistoryId);
                var result = context.EmploymentHistories.First(h => h.Id == eid);
                var res_videos = result.EmploymentHistoryVideos.ToList();
                dl_videos.DataSource = res_videos;
                dl_videos.DataBind();

                //int videoCount = 0;
                //foreach (var p in history.EmploymentHistoryVideos)
                //{
                //    videoCount++;
                //    var url = p.VideoURL;
                //    pnlVideo.Controls.Add(new LiteralControl("<iframe id='iFrameVideoView" + p.Id + "' width='279' height='230' frameborder='0' src='" + url + "'></iframe>"));
                //}
            }
            LoggingManager.Debug("Exiting DisplayVideos - PortfoliosAchievementsVideos.aspx");

        }
        private void DisplayAchievements()
        {

            LoggingManager.Debug("Entering DisplayAchievements - PortfoliosAchievementsVideos.aspx");

            pnlAchievement.Controls.Clear();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int eid = Convert.ToInt32(EmploymentHistoryId);
                EmploymentHistory history = context.EmploymentHistories.First(h => h.Id == eid);
                foreach (var p in history.EmploymentHistoryAchievements)
                {
                    pnlAchievement.Controls.Add(new LiteralControl("<p class='textbox' style='margine:5px;'>" + p.Summary + "</p><br/>"));
                }
            }
            LoggingManager.Debug("Exiting DisplayAchievements - PortfoliosAchievementsVideos.aspx");

        }
        protected void del_Video(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering del_Video- PortfoliosAchievementsVideos.aspx");
             var button = sender as ImageButton;
             if (button != null)
             {
                 int V_Id = Convert.ToInt32(button.CommandArgument);
                 using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var v_to_del = context.EmploymentHistoryVideos.FirstOrDefault(s => s.Id == V_Id);
                    context.DeleteObject(v_to_del);
                    context.SaveChanges();
                 }

                 DisplayVideos();
             }
             LoggingManager.Debug("Exiting del_Video- PortfoliosAchievementsVideos.aspx");

        }
        protected void AddPortfolioClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering AddPortfolioClick - PortfoliosAchievementsVideos.aspx");

            AddPortfolio();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Call my function", "parent.overlay('picture saved succesfully')", true);

            LoggingManager.Debug("Exiting AddPortfolioClick - PortfoliosAchievementsVideos.aspx");
        }
        private void AddPortfolio()
        {
            LoggingManager.Debug("Entering AddPortfolio - PortfoliosAchievementsVideos.aspx");

            if (filePhotoUpload.HasFile)
            {
                UserProfileManager.UploadPortfolioPicture(filePhotoUpload, Convert.ToInt32(EmploymentHistoryId), LoginUserId);
                filePhotoUpload.PostedFile.InputStream.Dispose();
            }

            DisplayPortfolios();

            LoggingManager.Debug("Exiting AddPortfolio - PortfoliosAchievementsVideos.aspx");
        }
        protected void AddVideoClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering AddVideoClick - PortfoliosAchievementsVideos.aspx");

            AddVideo();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Call my function", "parent.overlay('videos saved succesfully')", true);

            LoggingManager.Debug("Exiting AddVideoClick - PortfoliosAchievementsVideos.aspx");
        }
        private void AddVideo()
        {
            LoggingManager.Debug("Entering AddVideo - PortfoliosAchievementsVideos.aspx");
            //if (Convert.ToString(txtVideoUrl.Value) = "") { return; }
            string _videoUrl= Convert.ToString(txtVideoUrl.Value);
            if (_videoUrl == "") { return; }
            if (txtVideoUrl.Value.ToString().Trim().IndexOf("e.g: Video URL") >= 0) return;

            UserProfileManager.UploadJobVideo(Convert.ToInt32(EmploymentHistoryId), hdnVedioUrl.Value, LoginUserId);
            DisplayVideos();
            txtVideoUrl.Value = "";
            hdnVedioUrl.Value = "";

            LoggingManager.Debug("Exiting AddVideo - PortfoliosAchievementsVideos.aspx");

        }
        protected void AddAchievementClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering AddAchievementClick - PortfoliosAchievementsVideos.aspx");

            AddAchievement();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Call my function", "parent.overlay('Achievements saved succesfully')", true);

            LoggingManager.Debug("Exiting AddAchievementClick - PortfoliosAchievementsVideos.aspx");
        }
        private void AddAchievement()
        {
            LoggingManager.Debug("Entering AddAchievement - PortfoliosAchievementsVideos.aspx");

            if (txtAchievement.Value == "Say something about your achievement in no more than 140 characters") return;
            UserProfileManager.AddJobAchievement(Convert.ToInt32(EmploymentHistoryId), txtAchievement.Value, LoginUserId);
            DisplayAchievements();
            txtAchievement.Value = "Say something about your achievement in no more than 140 characters";
            LoggingManager.Debug("Exiting AddAchievement - PortfoliosAchievementsVideos.aspx");

        }
        protected void SaveChangesClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering SaveChangesClick - PortfoliosAchievementsVideos.aspx");

            AddPortfolio();
            AddVideo();
            AddAchievement();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Call my function", "parent.overlay('Details added succesfully')", true);

            LoggingManager.Debug("Exiting SaveChangesClick - PortfoliosAchievementsVideos.aspx");
        }
    }
}