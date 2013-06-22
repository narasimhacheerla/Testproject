using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Snovaspace.Util.Logging;
using System.Data;
using Snovaspace.Util.FileDataStore;
using Huntable.Data;
using Huntable.Business;

namespace Huntable.UI
{
    public partial class Company_blogs_popular : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - Company_blogs_popular");

            InvitationManager _objInvManager = new InvitationManager();
            var complist = new CompanyManager();
            if (ComId.HasValue)

            {
                overview.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(ComId));
                activity.HRef = "businessactivity.aspx?Id=" + ComId;
                productsandservices.HRef = "companyproducts.aspx?Id=" + ComId;
                busunessblog.HRef = "company-blogs-popular.aspx?Id=" + ComId;
                careers.HRef = "companyjobs.aspx?Id=" +ComId;
                article.HRef = "article.aspx?Id=" + ComId;
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var compblog = context.Companies.FirstOrDefault(x => x.Id == ComId);
                    string link=compblog.CompanyBlogLink;
                    string compname = compblog.CompanyName;
                    string compurl = compblog.CompanyWebsite;
                    lblcompname.Text = compname;
                    comp_url_link.HRef = compurl + "?Id=" + ComId;
                    string str1="http://";
                    string str2=link;
                    if(link!=null)
                    {
                        if (str1 == str2.Substring(0, 7))
                        {
                            ifblog.Attributes.Add("src", link);
                        }
                        else
                            ifblog.Attributes.Add("src",str1+link);
                    }
                    else
                    {
                        divreg.Visible=true;
                        ifblog.Visible = false;
                    }
                 
                }

            }
            LoggingManager.Debug("Exiting Page_Load - Company_blogs_popular");
           
        }
        private int? ComId
        {
            get
            {
                LoggingManager.Debug("Entering ComId - Company_blogs_popular");

                int otherUserId;
                if (int.TryParse(Request.QueryString["Id"], out otherUserId))
                {
                    return otherUserId;
                }
                LoggingManager.Debug("Exiting ComId - Company_blogs_popular");
                return null;
            }
        }
    }
}