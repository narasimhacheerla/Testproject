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
using System.Web.UI.HtmlControls;
namespace Huntable.UI.UserControls
{
    public partial class Followers_6_ : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var count01=0;
             
              
                if (CompId.HasValue)
                {
                    var follcount01 =
                     context.PreferredFeedUserCompaniesFollwers.Where(x => x.CompanyID == CompId.Value).Select(
                         x => x.FollowingUserId).Distinct().ToList();
                   int? cmid = context.Companies.FirstOrDefault(x => x.Id == CompId.Value).Userid;
                   var follIds01 = follcount01.Take(6);
                    count01 = follcount01.Count();
                    lblCount.Text = count01.ToString();
                    if (count01 == 0)
                    {
                        lbl_foll.Visible = true;
                        dlFollowers.Visible = false;
                    }
                    else
                    {
                        dlFollowers.DataSource = follIds01;
                        dlFollowers.DataBind();
                    }
                    A2.HRef = "~/followers.aspx?userId=" + cmid;
                }
                else
                {

                    var comp = context.Users.FirstOrDefault(x => x.Id == LoginUserId&& x.IsCompany == true);
                    if (comp != null && comp.IsCompany == true)
                    {
                        var copmanyid = context.Companies.FirstOrDefault(x => x.Userid == comp.Id);
                        var follcount01 =
                            context.PreferredFeedUserCompaniesFollwers.Where(x => x.CompanyID == copmanyid.Id).Select(
                                x => x.FollowingUserId).Distinct().ToList();
                        var follIds01 = follcount01.Take(6);
                        count01 = follcount01.Count();
                        lblCount.Text = count01.ToString();
                        if (count01 == 0)
                        {
                            lbl_foll.Visible = true;
                            dlFollowers.Visible = false;
                        }
                        else
                        {
                            dlFollowers.DataSource = follIds01;
                            dlFollowers.DataBind();
                        }
                      
                    }

                    if (comp != null) A2.HRef = "~/Followers.aspx?Userid=" + comp.Id;

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
               
                return null;
               
            }
        }
      
        public string FollowersPhoto(object id)
        {

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Users.FirstOrDefault(x => x.Id == p);
                
                    var photo = result.PersonalLogoFileStoreId;
                    return new FileStoreService().GetDownloadUrl(photo);
              
            }

        }
        public int LoginUserId
        {
            get
            {
                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;
                return 0;
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
                        int cmpid = context.Companies.FirstOrDefault(x => x.Userid == strUsername).Id;
                        A1.HRef = "~/" +new UrlGenerator().CompanyUrlGenerator(cmpid);
                    }
                }
            }
        }
    }
}