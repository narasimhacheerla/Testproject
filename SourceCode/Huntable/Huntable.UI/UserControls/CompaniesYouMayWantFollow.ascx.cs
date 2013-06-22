using System;
using System.Web.UI;
using Huntable.Business;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI.UserControls
{
    public partial class CompaniesYouMayWantFollow : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var company = new CompanyManager();
            var count = company.GetFeaturedrecuirtersCount(LoginUserId);
            if (count != 0)
            {
                var result2 = company.GetFeaturedUserComapnies(LoginUserId);

                dlcomp.DataSource = result2;
                dlcomp.DataBind();

            }

            else
            {
                var dt = CompanyManager.GetFeaturedUserComp();
                dlcomp.DataSource = dt;
                dlcomp.DataBind();
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
            return new FileStoreService().GetDownloadUrl(null);
        }

        public string UrlGenerator(object id)
        {
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return "~/" + new UrlGenerator().CompanyUrlGenerator(companyid);
            }
            return null;
        }
    }
}