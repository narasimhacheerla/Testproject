using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;
using Snovaspace.Util.FileDataStore;
namespace Huntable.UI.UserControls
{
    public partial class ViewersOfCompanyAlso : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Company> cmpusr = new List<Company>();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int? usrid = context.Companies.FirstOrDefault(x => x.Id == compId).Userid;
                var result =
                            context.UserProfileVisitedHistories.Where(x => x.UserId == usrid).ToList();
                foreach (var res in result)
                {
                    var viewlist =
                                 from a in context.UserProfileVisitedHistories
                                 where a.VisitorUserId == res.VisitorUserId
                                 select new
                                 {
                                     a.UserId,
                                     a.Date
                                 };
                    viewlist.OrderByDescending(x => x.Date);
                    foreach (var vwlis in viewlist)
                    {
                        var cmpur = context.Companies.FirstOrDefault(x => x.Userid == vwlis.UserId && x.Id != compId);
                        if (cmpur != null)
                        {
                            cmpusr.Add(cmpur);
                        }
                    }
                }
                if (cmpusr.Count != 0)
                {
                    cmpusr.Reverse();
                    dlview.DataSource = cmpusr.Distinct().Take(4);
                    dlview.DataBind();
                }
            }
        }
        private int? compId
        {

            get
            {
                LoggingManager.Debug("Entering compId - Article");

                int otherUserId;
                if (int.TryParse(Request.QueryString["Id"], out otherUserId))
                {
                    return otherUserId;
                }
                LoggingManager.Debug("Exiting compId - Article");
                return null;
            }
        }
        public string Picture(object id)
        {
            if (id != null)
            {
                int p = Int32.Parse(id.ToString());


                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {

                    return new FileStoreService().GetDownloadUrl(p);
                }

            }
            else
            {
                return null;
            }




        }
        protected void click_seemore(object sender, EventArgs e)
        {
            if (Hidden_Field.Value == string.Empty)
                Hidden_Field.Value = "4";
            Hidden_Field.Value = (Convert.ToInt32(Hidden_Field.Value) + 4).ToString();
            List<Company> cmpusr = new List<Company>();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int? usrid = context.Companies.FirstOrDefault(x => x.Id == compId).Userid;
                var result =
                            context.UserProfileVisitedHistories.Where(x => x.UserId == usrid).ToList();
                foreach (var res in result)
                {
                    var viewlist =
                                  from a in context.UserProfileVisitedHistories
                                  where a.VisitorUserId == res.VisitorUserId
                                  select new
                                  {
                                      a.UserId,
                                      a.Date
                                  };
                    viewlist.OrderByDescending(x => x.Date);
                    foreach (var vwlis in viewlist)
                    {
                        var cmpur = context.Companies.FirstOrDefault(x => x.Userid == vwlis.UserId && x.Id != compId);
                        if (cmpur != null)
                        {
                            cmpusr.Add(cmpur);
                        }
                    }
                }
                if (cmpusr.Count != 0)
                {
                    cmpusr.Reverse();
                    dlview.DataSource = cmpusr.Distinct().Take(8);
                    dlview.DataBind();
                }


            }

        }
        public string UrlGenerator(object id)
        {
            if ((id != null))
            {
                int companyid = Convert.ToInt32(id.ToString());
                return "~/"+ new UrlGenerator().CompanyUrlGenerator(companyid);
            }
            else
            {
                return null;
            }

        }
    }
}