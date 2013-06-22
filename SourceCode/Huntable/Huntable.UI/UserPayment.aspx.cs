using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class UserPayment : System.Web.UI.Page
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - UserPayment.aspx");
            _id = Convert.ToInt16(Request.QueryString["Id"]);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var paymentinfo = context.JobCreditsPurchaseds.FirstOrDefault(x => x.Id == _id);
                var userinfo = context.Users.FirstOrDefault(x => x.Id == paymentinfo.UserId);
                if (userinfo != null)
                {
                    lblCity.Text = userinfo.City;
                    lblCountry.Text = userinfo.CountryName;
                    lblHomeAddress.Text = userinfo.HomeAddress;
                    lblName.Text = userinfo.Name;
                }
                if (paymentinfo != null)
                {
                    lblDtae.Text = paymentinfo.PurchaseDateTime.ToString();
                    lblCountry.Text = paymentinfo.FeaturedCountry.ToString();
                    lblSkill.Text = paymentinfo.FeaturedSkill.ToString();
                    lblinterests.Text = paymentinfo.FeaturedInterests.ToString();
                    lblInvoiceno.Text = paymentinfo.InvoiceNo.ToString();
                    lblPremiumPrice.Text = paymentinfo.Premiumpackage.ToString();
                    lblIndustry.Text = paymentinfo.FeaturedIndustry.ToString();
                    lblTotalAmount.Text = paymentinfo.TotalAmount.ToString();
                    lblTotalAmountAfterVat.Text = paymentinfo.TotalAmountAftervat.ToString();
                    lblVat.Text = paymentinfo.VatAmount.ToString();
                    lblJobsPackage.Text = paymentinfo.JobPackage.ToString();
                    lblJobpackageCount.Text =( paymentinfo.JobPackage/5).ToString();
                    lblCountryCount.Text = (paymentinfo.FeaturedCountry/10).ToString();
                    lblIndustries.Text = (paymentinfo.FeaturedIndustry/10).ToString();
                    lblInterestCount.Text = (paymentinfo.FeaturedInterests/10).ToString();
                    lblSkillCount.Text = (paymentinfo.FeaturedSkill/10).ToString();
                }
            }
            LoggingManager.Debug("Exiting Page_Load - UserPayment.aspx");
        }

        protected void Downloadclick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Downloadclick - UserPayment.aspx");

            HttpRequest request = HttpContext.Current.Request;

            string websiteurl = request.IsSecureConnection ? "https://" : "http://";

            websiteurl += request["HTTP_HOST"] + "/";
            //string websiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebsiteURL"];
            string url = websiteurl + "UserPayment.aspx?id=" + _id + "";
            string filePath = UserProfileManager.CreatePDFForUrl(url, _id + ".pdf");
            DownLoadPdf(filePath, _id + ".pdf");

            LoggingManager.Debug("Exiting Downloadclick - UserPayment.aspx");

        }
        private void DownLoadPdf(string pdfFilePathAndName, string fileName)
        {

            LoggingManager.Debug("Entering DownLoadPdf - UserPayment.aspx");
            try
            {
                var client = new WebClient();
                Byte[] buffer = client.DownloadData(pdfFilePathAndName);

                Response.ClearContent();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());

                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

                Response.BinaryWrite(buffer);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            LoggingManager.Debug("Exiting DownLoadPdf - UserPayment.aspx");
        }
    }
}

