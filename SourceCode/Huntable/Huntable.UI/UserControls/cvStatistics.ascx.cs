using System;
using Huntable.Data;
using System.Linq;
using Huntable.Entities.Enums;
using System.Data.Objects;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace Huntable.UI.UserControls
{
    public partial class CVStatistics : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                int? userId = Business.Common.GetLoggedInUserId(Session);
                if (userId.HasValue)
                {
                    LoadCVStatisticsData();
                }
                else
                {
                    cvStats.Visible = false;
                }
            //}
        }

        private void LoadCVStatisticsData()
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int? userId = Business.Common.GetLoggedInUserId(Session);
                if (userId.HasValue)
                {
                    if (context.Users.FirstOrDefault(x => x.Id == userId).ProfileSearchResultCount > 0)
                    {
                        phGraph.Controls.Add(new LiteralControl("<iframe src='https://huntable.co.uk/Graph.aspx' scrolling='no' frameborder='0'></iframe>"));
                        show.Visible = false;
                    }
                    else
                    {
                        show.Visible = true;
                    }
                    lblTotalViews.Text = context.UserProfileVisitedHistories.Count(p => p.UserId == userId).ToString();
                    const int jobInquiryTypeId = (int)EMessageType.JobInquiry;
                    var msg = context.UserMessages.Where(m => m.SentTo == userId && m.Subject == "Job Enquiry").ToList();
                    lblJobEnquiryMessages.Text = msg.Count.ToString();
                        //context.UserMessages.Count(m => m.SentBy == userId && m.Subject == "Job Enquiry").ToString();
                    //lblJobEnquiryMessages.Text = context.UserMessage.Count(m => m.UserMessage.SentBy == userId && m.MessageTypeId == jobInquiryTypeId).ToString();

                    var user = context.Users.First(u => u.Id == userId);
                    lblTotalSearchResults.Text = user.ProfileSearchResultCount.ToString();
                    string chartdata = GetProfileVisitedGraphData(context);
                    scriptLiteral.Text = string.Format("<script type=\"text/javascript\">var histData1 ={0};</script>", chartdata);
                }
            }
        }

        private string GetProfileVisitedGraphData(huntableEntities contex)
        {
            int? userId = Business.Common.GetLoggedInUserId(Session);
            var res = contex.UserProfileVisitedHistories.Where(h => h.UserId == userId).GroupBy(h => EntityFunctions.TruncateTime(h.Date)).OrderBy(h => h.Key);

            string data = string.Empty;
            int recordsCount = 0;
            foreach (var hist in res)
            {
                recordsCount++;
                data += string.Format("[\"{0}\",{1}],", string.Format("{0}/{1}/{2}", hist.Key.Value.Month, hist.Key.Value.Day, hist.Key.Value.Year), hist.Count());
            }
            //if (recordsCount == 1)
            //{
            //    var lastDate = res.First().Key;
            //    lastDate = lastDate.Value.AddDays(-1);
            //    data += string.Format("[\"{0}\",{1}],", string.Format("{0}/{1}/{2}", lastDate.Value.Month, lastDate.Value.Day, lastDate.Value.Year), 0);
            //}

            if (recordsCount > 2 && data.Length > 0)
            {
                data = data.Substring(0, data.Length - 1);
                return "[" + data + "]";
            }
            else
            {
                //data = string.Format("[\"{0}\",{1}],[\"{2}\",{3}]", string.Format("{0}/{1}/{2}", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Year), 0,
                //    string.Format("{0}/{1}/{2}", DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, DateTime.Now.AddDays(-1).Year), 0);
               
                return "null";
            }

            
        }

        protected void lnkViewMore_Click(object sender, EventArgs e)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                int? userId = Business.Common.GetLoggedInUserId(Session);
                var user = context.Users.First(u => u.Id == userId);
                if (user.IsPremiumAccount.HasValue && user.IsPremiumAccount.Value)
                {
                    Response.Redirect("~/ProfileStatusAtGlance.aspx");
                }
                else
                {
                    Response.Redirect("~/WhatIsHuntableUpgrade.aspx");
                }
            }
        }
    }
}