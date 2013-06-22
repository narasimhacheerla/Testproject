using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Data;

namespace Huntable.UI.UserControls
{
    public partial class video : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CompId.HasValue)
            {

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {

                    var videoList = context.CompanyVideos.Where(x => x.CompanyId == CompId).Distinct().Take(1);
                    if (videoList.Count() == 0)
                    {
                        div_video.Visible = false;
                    }
                    else
                    {
                        dlvideos.DataSource = videoList;
                        dlvideos.DataBind();

                    }
                   
                }
            }
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
                if ((Page.RouteData.Values["ID"]) != null)
                {
                    string id = (Page.RouteData.Values["ID"]).ToString();
                    string[] words = id.Split('-');
                    int k = words.Length;
                    string companyid = words[k - 1];
                    return Convert.ToInt32(companyid);

                }
                return null;
            }
        }
        protected void btn_click(object sender, EventArgs e)
        {
            if (Hidden_Field.Value == string.Empty)
                Hidden_Field.Value = "1";
            Hidden_Field.Value = (Convert.ToInt32(Hidden_Field.Value) + 1).ToString();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                var videoList = context.CompanyVideos.Where(x => x.CompanyId == CompId).Distinct().ToList();
               
               
                    dlvideos.DataSource = videoList;
                    dlvideos.DataBind();

                

            }
        }

        //protected void btn_slide_Click(object sender, EventArgs e)
        //{
        //    li02.Visible = false;
        //}
        //protected void btn_next_Click(object sender, EventArgs e)
        //{
        //    li01.Visible = false;
        //    li02.Visible = true;
        //}
    }
}