using System;
using System.Linq;
using System.Web;
using Huntable.Data;
using Snovaspace.Util.Logging;
using Huntable.Business;
using System.Net;

namespace Huntable.UI
{
    public partial class UserInvoice : System.Web.UI.Page
    {
        int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - UserInvoice.aspx");

            _id = Convert.ToInt16(Request.QueryString["Id"]);

            try
            {
                int uid;
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    Invoice invoice = context.Invoices.FirstOrDefault(u => u.Id == _id);
                    if (invoice != null)
                    {
                        decimal amount = invoice.Amount;
                        lblwithdrawn.Text = (invoice.WithdrawnDateTime.ToShortDateString()).ToString();
                        lbltrancation.Text = true ? (invoice.TransactionCompletedDateTime).ToString() : "Underprocess";
                        lblAmount.Text = Convert.ToString(amount);
                        lblTotalAmount.Text = Convert.ToString(amount);
                        lblTotAmount.Text = Convert.ToString(amount);
                        lblAmountToWithdraw.Text = Convert.ToString("$" + (amount - 5));
                        lbltranscationid.Text = invoice.InvoicePathId.ToString();

                    }
                    if (invoice != null)
                        uid = invoice.UserId;

                    User user = context.Users.FirstOrDefault(u => u.Id == invoice.UserId);
                    if (user != null)
                    {
                        lblHuntable.Text = user.Name;
                        lblemail.Text = user.EmailAddress;
                        lblcountry.Text = user.CountryName;
                        lblstate.Text = user.State;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting Page_Load - UserInvoice.aspx");

        }

        protected void BtnprintClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnprintClick - UserInvoice.aspx");

            HttpRequest request = HttpContext.Current.Request;

            string websiteurl = request.IsSecureConnection ? "https://" : "http://";

            websiteurl += request["HTTP_HOST"] + "/";
            //string websiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebsiteURL"];
            string url = websiteurl + "UserInvoice.aspx?id=" + _id + "";
            string filePath = UserProfileManager.CreatePDFForUrl(url, _id + ".pdf");
            DownLoadPdf(filePath, _id + ".pdf");

            LoggingManager.Debug("Exiting BtnprintClick - UserInvoice.aspx");

        }
        private void DownLoadPdf(string pdfFilePathAndName, string fileName)
        {

            LoggingManager.Debug("Entering DownLoadPdf - UserInvoice.aspx");
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
            LoggingManager.Debug("Exiting DownLoadPdf - UserInvoice.aspx");
        }
    }
}