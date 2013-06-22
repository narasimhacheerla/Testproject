using System;
using System.IO;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;


namespace Huntable.UI
{
    public partial class InsertNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - InsertNews.aspx");

            LoggingManager.Debug("Exiting Page_Load - InsertNews.aspx");

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnSubmit_Click - InsertNews.aspx");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
               
                int virtualRootPath;
                virtualRootPath = Convert.ToInt32(new FileStoreService().LoadImageAndResize(Constants.NewsPathKey, fuploads));

                var news = new News { Title = txtTitle.Text, PicturePathId = virtualRootPath, link = txtLink.Text, CreatedDateTime = DateTime.Now,PicturePath = txtLink.Text};
                context.News.AddObject(news);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting btnSubmit_Click - InsertNews.aspx");
        }
    }
}