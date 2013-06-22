using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;
using Snovaspace.Util.FileDataStore;
using System.Web.UI.HtmlControls;

namespace Huntable.UI.UserControls
{
    public partial class Followers : System.Web.UI.UserControl
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
            var cmpnyflwr = new CompanyManager();
            if (CompId.HasValue)
            {
                var result = cmpnyflwr.GetCmpnyFlwr(CompId.Value);
                //var preferredFeedUserCompaniesFollwers = result as PreferredFeedUserCompaniesFollwer[] ?? result.ToArray();
                int count = result.Count();
                string cnt = count.ToString(CultureInfo.InvariantCulture);
                lbl.Text = cnt;
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    int? cmpnyid = context.Companies.FirstOrDefault(x => x.Id == CompId.Value).Userid;
                    seemore.HRef = "~/Followers.aspx?UserId=" + cmpnyid;
                }
                
                dl.DataSource = result.Take(4);
                dl.DataBind();

            }
            else
            {
                seemore.HRef = "~/Followers.aspx?UserId=" + LoginUserId;
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
        public string Picture(object id)
        {
            
                int p = Int32.Parse(id.ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var res = context.Users.FirstOrDefault(x => x.Id == p);
                    var ph = res.PersonalLogoFileStoreId;
                    return new FileStoreService().GetDownloadUrl(ph);
                }
            
           

        }
        public int useid(object id)
        {
            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var res1 = context.Users.FirstOrDefault(x => x.Id == p);
                return res1.Id;
            }
        }
        protected void itembound(object sender, DataListItemEventArgs e)
        {
            HtmlAnchor A1 = (HtmlAnchor)e.Item.FindControl("a1");
            if (e.Item.DataItem != null)
            {
                int strUsername = Int32.Parse((e.Item.DataItem).ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var usr = context.Users.FirstOrDefault(x => x.Id == strUsername && x.IsCompany == null);
                    if (usr != null)
                    {
                        A1.HRef = "~/" + new UrlGenerator().UserUrlGenerator(strUsername);
                    }
                    else
                    {
                        int cmpid = context.Companies.FirstOrDefault( x => x.Userid == strUsername).Id;
                        A1.HRef = "~/" + new UrlGenerator().CompanyUrlGenerator(cmpid);
                    }
                }
            }
        }
    }
}