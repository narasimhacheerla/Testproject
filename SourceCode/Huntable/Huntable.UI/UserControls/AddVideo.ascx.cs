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
    public partial class AddVideo : System.Web.UI.UserControl
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
            

        }
        protected void BtnVideoSave(object sender, EventArgs e)
        {
            var usrMgr = new UserManager();
            usrMgr.AddUserVideo(LoginUserId,txtvideoname.Text,txtvideourl.Text);
            var videoList = usrMgr.GetRecentVideos(LoginUserId);
           
        }
        protected void BtnPictureSave(object sender, EventArgs e)
        {
            var usrMgr = new UserManager();
            int portfolioid = Convert.ToInt32(new FileStoreService().LoadFileFromFileUpload(Constants.PortFolioImages, fpPictureUpload));
            string picturedescription = txtpicturedescription.Text;
            usrMgr.AddUserPortfolio(LoginUserId,portfolioid,picturedescription);
            //usrMgr.AddUserVideo();
        }
    }
}