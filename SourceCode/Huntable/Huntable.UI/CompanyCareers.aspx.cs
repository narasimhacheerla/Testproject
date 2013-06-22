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


namespace Huntable.UI
{
    public partial class CompanyCareers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CompanyCareers");
            var complist = new CompanyManager();
            if (compId.HasValue)
            {
                var result = complist.GetCmpny(compId.Value).FirstOrDefault();
                lbl_compName.Text = result.CompanyName;
                string link = result.CompanyCarrersLink;
                ccr.HRef = "companycareers.aspx?Id=" + compId.Value;
                if (link != null)
                {
                    if ("http://" == link.Substring(0, 7))
                    {
                        ccl.Attributes.Add("src",link);
                    }
                    else
                    {
                       ccl.Attributes.Add("src", "http://"+link);

                    }

                }
                else
                {
                    divreg.Visible = true;
                    ccl.Visible = false;
                }
               // dl2.DataSource = result;
              //  dl2.DataBind();
                //dl.DataSource = result;
                //dl.DataBind();

            }

            overview.HRef = "companyoverview.aspx?Id=" + compId;
            activity.HRef = "businessactivity.aspx?Id=" + compId;
            productsandservices.HRef = "companyproducts.aspx?Id=" + compId;
            busunessblog.HRef = "company-blogs-popular.aspx?Id=" + compId;
            careers.HRef = "companyjobs.aspx?Id=" + compId;
            article.HRef = "article.aspx?Id=" + compId;
            a_comP_view.HRef = new UrlGenerator().CompanyUrlGenerator(Convert.ToInt32(compId));
            a_Comp_jobs.HRef = "companyjobs.aspx?Id=" + compId;
            LoggingManager.Debug("Exiting Page_Load - CompanyCareers");

        }
        private int? compId
        {
            get
            {
                LoggingManager.Debug("Entering compId - CompanyCareers");
                int otherUserId;
                if (int.TryParse(Request.QueryString["Id"], out otherUserId))
                {
                    return otherUserId;
                }
                LoggingManager.Debug("Exiting compId - CompanyCareers");
                return null;
            }
        }
    }
}