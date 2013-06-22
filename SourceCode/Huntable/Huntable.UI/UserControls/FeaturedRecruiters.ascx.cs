using System;
using System.Collections.Generic;
using System.Linq;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;
using Huntable.Business;

namespace Huntable.UI.UserControls
{
    public partial class FeaturedRecruiters : System.Web.UI.UserControl
    {
        readonly InvitationManager _objInvManager = new InvitationManager();
        int? _userId = Common.GetLoggedInUserId();
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                companies.InnerText = context.Users.Count().ToString();
            }

            if (_userId.HasValue)
            {
                List<FeaturedUserCompany> result = _objInvManager.GetFeaturedCompanies(_userId.Value,null);
                if (result.Count != 0)
                {
                    dtlistFeatured.DataSource = result;
                    dtlistFeatured.DataBind();
                }
                else
                {
                    using( var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var y = from a in context.FeaturedUserCompanies
                                join l in context.Companies on a.CompanyId equals l.Id
                                where l.IsVerified != null && l.CompanyLogoId != null
                                select new
                                {
                                    a.CompanyId,
                                    l.CompanyLogoId
                                };
                        IEnumerable<dynamic> dt = y.Distinct().Take(8);
                    dtlistFeatured.DataSource = dt;
                    dtlistFeatured.DataBind();
                }
                }
            }
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
        public int Jobs(object id)
        {
            int p = Int32.Parse(id.ToString());
            var cmpMgr = new CompanyManager();
            return cmpMgr.GetJobsPostedByCompany(p);
        }
        public string UrlGenerator(object id)
        {
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().CompanyUrlGenerator(companyid);
            }
            else
            {
                return null;
            }

        }
    }
}