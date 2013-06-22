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
    public partial class CompaniesYouMayBeIntrested : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Common.GetLoggedInUser();
            if (user!=null&&user.IsCompany == true)
            {

                var company = new CompanyManager();
                var count = company.GetFeaturedrecuirtersCount(LoginUserId, null);
                if (count != 0)
                {
                    var result2 = company.GetFeaturedUserComapnies(LoginUserId, null);

                    dlcomp.DataSource = result2;
                    dlcomp.DataBind();

                }

                else
                {
                    var dt = CompanyManager.GetFeaturedUserComp(null);
                    dlcomp.DataSource = dt;
                    dlcomp.DataBind();
                }
            }
            else
            {
                return;
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
        public int Jobs(object id)
        {
            int p = Int32.Parse(id.ToString());
            var cmpMgr = new CompanyManager();
            return cmpMgr.GetJobsPostedByCompany(p);
        }
        public string Picture(object id)
        {
            if (id != null)
            {
                int p = Int32.Parse(id.ToString());
                return new FileStoreService().GetDownloadUrl(p);
            }
            else
            {
                return new FileStoreService().GetDownloadUrl(null);
            }
        }
        public string UrlGenerator(object id)
        {
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return "~/" + new UrlGenerator().CompanyUrlGenerator(companyid);
            }
            else
            {
                return null;
            }

        }
    }
}