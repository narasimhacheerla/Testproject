using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util;
using Snovaspace.Util.Logging;
using log4net;
using System.Data;
using System.Net;
using System.Web;

namespace Huntable.UI
{
    public partial class WithDrawFunds : System.Web.UI.Page
    {
        int count;
        protected void Page_Load(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering page_load  - WithDrawFunds.aspx");
            try
            {
                if (!IsPostBack)
                {
                    var jbMgr = new JobsManager();
                    var userId = Business.Common.GetLoggedInUserId(Session);
                    LoadTranscation();
                    LoadSummary();
                    if (userId != null) gvTranscation.DataSource = jbMgr.GetUserTranscationDetails(userId.Value);
                    gvTranscation.DataBind();

                    int amount = count;
                    if (amount <= 100)
                    {
                        btnwthdraw.Attributes.Add("onclick", "return Test()");
                    }


                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting page_load - WithDrawFunds.aspx");

        }

        private void LoadTranscation()
        {
            var objInvManager = new InvitationManager();

            LoggingManager.Debug("Entering  LoadTranscation-WithDrawFunds ");
            try
            {
                var userId = Business.Common.GetLoggedInUserId(Session);
                var dtSummary = new DataTable();
                dtSummary.Columns.Add("Id");
                dtSummary.Columns.Add("Total invited");
                dtSummary.Columns.Add("1st Connection");
                dtSummary.Columns.Add("2nd Connection");
                dtSummary.Columns.Add("Total Affilate Earnings");
                dtSummary.Columns.Add("Requested Date");
                dtSummary.Columns.Add("Paid on");
                if (userId != null)
                {
                    var currentUser = objInvManager.GetUserDetails(userId.Value);
                }
                var drlevel1 = dtSummary.NewRow();


            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting  LoadTranscation- WithDrawFunds.aspx");
        }
        protected void GvRowDataBound(object sender, GridViewRowEventArgs e)
        {
            LoggingManager.Debug("Entering GvRowDataBound  - WithDrawFunds.aspx");

            if (e.Row.Cells[8].Text == "Under Process")
            {
                e.Row.Cells[8].CssClass = "target";

            }
            if (e.Row.Cells[8].Text == "Invoice")
            {
                e.Row.Cells[8].CssClass = "button-green";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[6].Text = "$" + e.Row.Cells[6].Text;
                e.Row.Cells[6].ForeColor = System.Drawing.Color.Blue;
            }
            LoggingManager.Debug("Exiting GvRowDataBound  - WithDrawFunds.aspx");

        }
        protected void GvTranscationRowCommand(object s, GridViewCommandEventArgs e)
        {
            LoggingManager.Debug("Entering GvTranscationRowCommand  - WithDrawFunds.aspx");

            int id = 0;
            if (e.CommandName == "linkButton")
            {
                id = Convert.ToInt32(e.CommandArgument);
            }
            HttpRequest request = HttpContext.Current.Request;

            string websiteurl = request.IsSecureConnection ? "https://" : "http://";
            
            websiteurl += request["HTTP_HOST"] + "/";
            //string websiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebsiteURL"];
            string url = websiteurl + "UserInvoice.aspx?id=" + id +  "";
            string filePath = UserProfileManager.CreatePDFForUrl(url, id + ".pdf");
            DownLoadPdf(filePath, id + ".pdf");

            LoggingManager.Debug("Exiting GvTranscationRowCommand  - WithDrawFunds.aspx");

        }
        private void DownLoadPdf(string pdfFilePathAndName, string fileName)
        {
            LoggingManager.Debug("Entering DownLoadPdf  - WithDrawFunds.aspx");

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

            LoggingManager.Debug("Exiting DownLoadPdf  - WithDrawFunds.aspx");
        }
        protected void Lb1Click(object sender, GridViewCommandEventArgs e)
        {
            LoggingManager.Debug("Entering Lb1Click  - WithDrawFunds.aspx");

            var clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            if (clickedRow != null)
            {
                var link = (LinkButton)clickedRow.FindControl("lnkname");
                string id = link.ID;
            }
            Response.Redirect(string.Format("~/UserInvoice.aspx?id={0}", e.CommandArgument));

            LoggingManager.Debug("Exiting Lb1Click  - WithDrawFunds.aspx");
        }
        private void LoadSummary()
        {
            var objInvManager = new InvitationManager();
            LoggingManager.Debug("Entering LoadSummary - Withdrawfunds.aspx");
            try
            {

                var userId = Business.Common.GetLoggedInUserId(Session);
                var dtSummary = new DataTable();
                dtSummary.Columns.Add("Level", typeof(string));
                dtSummary.Columns.Add("INVITED", typeof(int));
                dtSummary.Columns.Add("JOINED", typeof(int));
                dtSummary.Columns.Add("EARNINGS", typeof(string));


                if (userId != null)
                {
                    var currentUser = objInvManager.GetUserDetails(userId.Value);
                    var trancation = objInvManager.GetUserInvoice(userId.Value);
                    var level1Count = (trancation != null) ? trancation.Level1Count : 0;
                    var level2Count = (trancation != null) ? trancation.Level2Count : 0;
                    var level3Count = (trancation != null) ? trancation.Level3Count : 0;
                    var level1 = currentUser.LevelOnePremiumCount.HasValue ? currentUser.LevelOnePremiumCount : 0;
                    var level2 = currentUser.LevelTwoPremiumCount.HasValue ? currentUser.LevelTwoPremiumCount : 0;
                    var level3 = currentUser.LevelThreePremiumCount.HasValue ? currentUser.LevelThreePremiumCount : 0;

                    var drLevel1 = dtSummary.NewRow();
                    drLevel1["Level"] = "1st Connections";
                    drLevel1["INVITED"] = currentUser.LevelOneInvitedCount.HasValue ? currentUser.LevelOneInvitedCount : 0;
                    drLevel1["JOINED"] = level1 - level1Count;
                    drLevel1["EARNINGS"] = "$" + ((level1 - level1Count) * 4);
                    dtSummary.Rows.Add(drLevel1);

                    var drLevel2 = dtSummary.NewRow();
                    drLevel2["Level"] = "2nd Connections";
                    drLevel2["INVITED"] = currentUser.LevelTwoInvitedCount.HasValue ? currentUser.LevelTwoInvitedCount : 0;
                    drLevel2["JOINED"] = level2 - level2Count;
                    drLevel2["EARNINGS"] = "$" + ((level2 - level2Count) * 1);
                    dtSummary.Rows.Add(drLevel2);


                    var drLevel3 = dtSummary.NewRow();
                    drLevel3["Level"] = "3rd Connections";
                    drLevel3["INVITED"] = currentUser.LevelThreeInvitedCount.HasValue ? currentUser.LevelThreeInvitedCount : 0;
                    drLevel3["JOINED"] = level3 - level3Count;
                    drLevel3["EARNINGS"] = "$" + ((level3 - level3Count) * .5);
                    dtSummary.Rows.Add(drLevel3);


                    gvInvSummary.DataSource = dtSummary;
                    gvInvSummary.DataBind();
                    count = Convert.ToInt32((level1 - level1Count) * 4 + (level2 - level2Count) * 1 + (level3 - level3Count) * .5);
                    lblTotalEarnings.Text = "Total : $" + ((level1 - level1Count) * 4 + (level2 - level2Count) * 1 + (level3 - level3Count) * .5).ToString();
                }
                LoggingManager.Info("Total earnings:" + lblTotalEarnings.Text);
                //if(count==0)
                // {
                //    btnwthdraw.Visible=false;
                // }

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadSummary - Withdrawfunds.aspx");

        }
       


        protected void BtnInvoiceClick(object sender, EventArgs e)
        {

            var objInvManager = new InvitationManager();
            LoggingManager.Debug("Entering BtnInvoiceClick - WithDrawFunds.aspx");
            int amount = count;
            if(count>100)
            {
                //if (count == 0)
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "Message", "callAlert('You Dont have Enough money to withdraw');", true);
                //}
                //else
                //{
                try
                {
                    var userId = Business.Common.GetLoggedInUserId(Session);
                    if (userId != null)
                    {
                        var currentUser = objInvManager.GetUserDetails(userId.Value);
                        var getdetails = objInvManager.GetUserInvoice(userId.Value);
                        var level1Previous = (getdetails != null) ? getdetails.Level1Count : 0;
                        var level2Previous = (getdetails != null) ? getdetails.Level2Count : 0;
                        var level3Previous = (getdetails != null) ? getdetails.Level3Count : 0;
                        var level1Count = Convert.ToInt32(currentUser.LevelOnePremiumCount);
                        var level2Count = Convert.ToInt32(currentUser.LevelTwoPremiumCount);
                        var level3Count = Convert.ToInt32(currentUser.LevelThreePremiumCount);
                        var totalinvited = (Convert.ToInt32(currentUser.LevelOneInvitedCount) +
                                            Convert.ToInt32(currentUser.LevelTwoInvitedCount) +
                                            Convert.ToInt32(currentUser.LevelThreeInvitedCount));
                        var totalamount =
                            Convert.ToDecimal((level1Count - level1Previous)*5 + (level2Count - level2Previous)*1 +
                                              (level3Count - level3Previous)*.5);

                        var usrInv = new Invoice
                                         {
                                             TotalInvited = totalinvited,
                                             Level1Count = level1Count,
                                             Level2Count = level2Count,
                                             Level3Count = level3Count,
                                             DrawnLevel1Count = level1Count - level1Previous,
                                             DrawnLevel2Count = level2Count - level2Previous,
                                             DrawnLevel3Count = level3Count - level3Previous,
                                             WithdrawnDateTime = DateTime.Today.Date,
                                             UserId = Convert.ToInt32(userId),
                                             Amount = Convert.ToDecimal(totalamount),

                                         };
                        var jbMgr = new JobsManager();
                        jbMgr.SaveTranscation(usrInv);
                    }
                }

           
            catch (Exception ex)
                {
                    LoggingManager.Error(ex);
                }
                LoggingManager.Debug("Exiting BtnInvoiceClick - WithDrawFunds.aspx");
            }
        }

        
    }
}