using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI.UserControls
{
    public partial class Viewers_also_viewed : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CompId.HasValue)
            {

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var result = context.CompanyProfileVisitedHistories.FirstOrDefault(x => x.CompanyId == CompId);
                    if (result != null)
                    {
                        int vCompId = Int32.Parse(result.VisitorCompanyId.ToString());
                        var visitedcomp = context.CompanyProfileVisitedHistories.Where(x => x.VisitorCompanyId == vCompId).ToList().Distinct();
                        var compResult = from cv in context.CompanyProfileVisitedHistories
                                          join c in context.Companies on cv.CompanyId equals c.Id
                                          where cv.VisitorCompanyId == vCompId
                                          select new
                                                     {
                                                         c.CompanyLogoId
                                                     };
                        var comp_result_ = compResult.Distinct();
                        var _comp_result = comp_result_.Take(8);
                        int count = _comp_result.Count();
                        int n = 0;
                        foreach (var image in _comp_result)
                        {
                            if (n == 0)
                            {
                                img0.Attributes.Add("src", image.CompanyLogoId.ToString());
                                img0.Visible = true;
                           
                                n++;
                                if (count == n)
                                {
                                    break;
                                }
                            }
                            if (n == 1)
                            {
                                img1.Src = image.CompanyLogoId.ToString(); n++;
                                img1.Visible = true;
                                if (count == n)
                                {
                                    break;
                                }
                            }
                            if (n == 2)
                            {
                                img2.Src = image.CompanyLogoId.ToString(); n++;
                                img2.Visible = true;

                                if (count == n)
                                {
                                    break;
                                }
                            }
                            if (n == 3)
                            {
                                img3.Src = image.CompanyLogoId.ToString(); n++;
                                img3.Visible = true;
                                if (count == n)
                                {
                                    break;
                                }
                            }
                            if (n == 4)
                            {
                                img4.Src = image.CompanyLogoId.ToString(); n++;
                                img4.Visible = true;
                                if (count == n)
                                {
                                    break;
                                }
                            }
                            if (n == 5)
                            {
                                img5.Src = image.CompanyLogoId.ToString(); n++;
                                img5.Visible = true;
                                if (count == n)
                                {
                                    break;
                                }
                            }
                            if (n == 6)
                            {
                                img6.Src = image.CompanyLogoId.ToString(); n++;
                                img6.Visible = true;
                                if (count == n)
                                {
                                    break;
                                }
                            }
                            if (n == 7)
                            {
                                img7.Src = image.CompanyLogoId.ToString(); n++;
                                img7.Visible = true;
                                if (count == n)
                                {
                                    break;
                                }
                            }
                        }

                        if (count > 4)
                        {
                            btn_li02.Visible = true;
                            btn_li01.Visible = true;
                        }
                    }
                }
            }
        }

        protected void btn_li01_Click(object sender, EventArgs e)
        {
            li01.Visible = true;
            li02.Visible = false;
        }
        protected void btn_li02_Click(object sender, EventArgs e)
        {
            li02.Visible = true;
            li01.Visible = false;
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