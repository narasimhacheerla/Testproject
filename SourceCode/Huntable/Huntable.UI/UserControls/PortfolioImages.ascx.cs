using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;
using System.Data;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI.UserControls
{
    public partial class PortfolioImages : System.Web.UI.UserControl
    {
        public List<string> Links;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (CompId.HasValue)
            //{

            //    using (var context = huntableEntities.GetEntitiesWithNoLock())
            //    {
            //        int y = Convert.ToInt32(CompId);
            //        List<CompanyPortfolio> images = context.CompanyPortfolios.Where(x => x.CompanyId == y).Take(4).ToList();
            //        int n=images.Count;
            //        if (n == 1)
            //        {
            //            img3.Src = images[0].NewsImageBasePath;
            //        }
            //        if (n == 2)
            //        {
            //            img3.Src = images[0].NewsImageBasePath;
            //            img4.Src = images[1].NewsImageBasePath;
                        
            //        }
            //        if (n == 3)
            //        {
            //            img3.Src = images[0].NewsImageBasePath;
            //            img4.Src = images[1].NewsImageBasePath;
            //            img5.Src = images[2].NewsImageBasePath;
            //        }
            //        if (n == 4)
            //        {
            //            img3.Src = images[0].NewsImageBasePath;
            //            img4.Src = images[1].NewsImageBasePath;
            //            img5.Src = images[2].NewsImageBasePath;
            //            img6.Src = images[3].NewsImageBasePath;
            //        }
            //    }
            //}
            var objInvManager = new UserManager();
            if (CompId != null)
            {
                var piclist = objInvManager.GetPics(CompId.Value);
                if (!piclist.Any())
                {
                    div_portfolio.Visible = false;
                }
            }
        }


        protected string GetMyPictures()
        {
            var myPictureList = "";
            const string myPicture = "<li><img src='{0}' width='280' runat='server' id='imgPicture' height='174' alt='portfolio' /></li>";
            var objInvManager = new UserManager();
            if (CompId != null)
            {
                var  piclist = objInvManager.GetPics(CompId.Value);
                var pictureList = piclist.Take(6);
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    int index = 1;
                    foreach (var p in pictureList)
                    {
                        myPictureList += string.Format(myPicture, new FileStoreService().GetDownloadUrl(p.PortfolioImageid), index);
                        index++;
                    }
                }
            }
            return myPictureList;
        }

        private int? CompId
        {
            get
            {
                int otherUserId;
                if (int.TryParse(Request.QueryString["Id"], out otherUserId))
                {
                    return otherUserId;
                }
                return null;
            }
        }

    }
}