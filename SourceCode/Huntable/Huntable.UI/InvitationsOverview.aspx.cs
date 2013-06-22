using System;
using System.Collections.Generic;
using System.Data;
using Huntable.Business;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Data;
using Snovaspace.Util;
using Snovaspace.Util.Logging;
using System.Linq;

namespace Huntable.UI
{
    public partial class InvitationsOverview : System.Web.UI.Page
    {
        readonly InvitationManager _objInvManager = new InvitationManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - InvitationsOverview.aspx");
            try
            {
                if (!IsPostBack)
                {
                    var userId = Business.Common.GetLoggedInUserId(Session);
                     var loggedInUserId = Common.GetLoggedInUserId(Session);
                     if (loggedInUserId.HasValue)
                     {
                        
                         using (var context = huntableEntities.GetEntitiesWithNoLock())
                         {
                             var objMessageManager = new UserMessageManager();
                             User userDetails = objMessageManager.GetUserbyUserId(context, loggedInUserId.Value);
                             lblmember.Text = userDetails.CreatedDateTime.ToString();
                         }
                     }
                     
                    LoadSummary();
                    LoadConnections(gvLevel1, userId.Value,1);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - InvitationsOverview.aspx");
            try
            {
                bool userLoggedIn = Common.IsLoggedIn();
                if (!IsPostBack)
                {
                    LoadProfilePercentCompleted();
                }
                var user = Common.GetLoggedInUser();
                if(user.IsCompany==true)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var compid = context.Companies.FirstOrDefault(x => x.Userid == user.Id);
                        imagehref.HRef = new UrlGenerator().CompanyUrlGenerator(compid.Id);
                        namehref.HRef = new UrlGenerator().CompanyUrlGenerator(compid.Id);
                    }
                }
                else
                {
                    imagehref.HRef = "~/viewuserprofile?UserId=" + user.Id;
                    namehref.HRef = "~?viewuserprofile?UserId=" + user.Id;
                }
                if (userLoggedIn)
                {
                    DateTime dt = user.LastLoginTime;
                    string s = dt.ToString("hh:mm t.\\M");
                    lblName.Text = user.Name;
                    lblLogin.Text = user.LastLoginTime.ToShortDateString();
                    
                    lblProfile.Text = user.LastProfileUpdatedOn.ToShortDateString();
                    imgProfile.ImageUrl = user.UserProfilePictureDisplayUrl;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

        }

        private void LoadSummary()
        {
            LoggingManager.Debug("Entering LoadSummary - InvitationsOverview.aspx");
            try
            {

                var userId = Business.Common.GetLoggedInUserId(Session);
                var dtSummary = new DataTable();
                dtSummary.Columns.Add("Level", typeof(string));
                dtSummary.Columns.Add("INVITED", typeof(int));
                dtSummary.Columns.Add("JOINED", typeof(int));
                dtSummary.Columns.Add("EARNINGS", typeof(string));


                var currentUser = _objInvManager.GetUserDetails(userId.Value);

                var level1 = currentUser.LevelOnePremiumCount.HasValue ? currentUser.LevelOnePremiumCount : 0;
                var level2 = currentUser.LevelTwoPremiumCount.HasValue ? currentUser.LevelTwoPremiumCount : 0;
                var level3 = currentUser.LevelThreePremiumCount.HasValue ? currentUser.LevelThreePremiumCount : 0;

                var drLevel1 = dtSummary.NewRow();
                drLevel1["Level"] = "1st Connections";
                drLevel1["INVITED"] = currentUser.LevelOneInvitedCount.HasValue ? currentUser.LevelOneInvitedCount : 0;
                drLevel1["JOINED"] = level1;
                drLevel1["EARNINGS"] = "$" + (level1 * 4);
                dtSummary.Rows.Add(drLevel1);

                var drLevel2 = dtSummary.NewRow();
                drLevel2["Level"] = "2nd Connections";
                drLevel2["INVITED"] = currentUser.LevelTwoInvitedCount.HasValue ? currentUser.LevelTwoInvitedCount : 0;
                drLevel2["JOINED"] = level2;
                drLevel2["EARNINGS"] = "$" + (level2 * 1);
                dtSummary.Rows.Add(drLevel2);


                var drLevel3 = dtSummary.NewRow();
                drLevel3["Level"] = "3rd Connections";
                drLevel3["INVITED"] = currentUser.LevelThreeInvitedCount.HasValue ? currentUser.LevelThreeInvitedCount : 0;
                drLevel3["JOINED"] = level3;
                drLevel3["EARNINGS"] = "$" + (level3 * .5);
                dtSummary.Rows.Add(drLevel3);


                gvInvSummary.DataSource = dtSummary;
                gvInvSummary.DataBind();

                lblTotalEarnings.Text = currentUser.AffliateAmountAsText;
                LoggingManager.Info("Total earnings:" + lblTotalEarnings.Text);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadSummary - InvitationsOverview.aspx");

        }

        private void LoadConnections(GridView gvConnections,int userId,int level)
        {
            LoggingManager.Debug("Entering LoadConnections - InvitationsOverview.aspx");
            try
            {
                var connections = _objInvManager.GetLevelOneConnects(userId);
                gvConnections.DataSource = connections;
                gvConnections.DataBind();
                var uid = connections.Count > 0 ? connections[0].Id : 0;

                if(level ==1)
                {
                    LoadConnections(gvLevel2, uid, 2);
                }

                if (level == 2)
                {

                    LoadConnections(gvLevel3, uid, 3);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadConnections - InvitationsOverview.aspx");

        }

        protected void gvLevel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering gvLevel1_SelectedIndexChanged - InvitationsOverview.aspx");
            try
            {
                var row = gvLevel1.SelectedRow;
                if (row.RowType == DataControlRowType.DataRow)
                {
                    var userId = gvLevel1.DataKeys[row.RowIndex].Values["Id"].ToString();
                    LoadConnections(gvLevel2, Convert.ToInt32(userId),2);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting gvLevel1_SelectedIndexChanged - InvitationsOverview.aspx");
        }

        protected void gvLevel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering gvLevel2_SelectedIndexChanged - InvitationsOverview.aspx");
            try
            {
                var row = gvLevel2.SelectedRow;
                if (row.RowType == DataControlRowType.DataRow)
                {
                    var userId = gvLevel2.DataKeys[row.RowIndex].Values["Id"].ToString();
                    LoadConnections(gvLevel3, Convert.ToInt32(userId),3);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting gvLevel2_SelectedIndexChanged - InvitationsOverview.aspx");
        }

        protected void gvLevel1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LoggingManager.Debug("Entering gvLevel1_RowDataBound - InvitationsOverview.aspx");
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    var _singleClickButton = (LinkButton)e.Row.Cells[0].Controls[0];
                    string _jsSingle = ClientScript.GetPostBackClientHyperlink(_singleClickButton, "select$" + e.Row.RowIndex);
                    e.Row.Style["cursor"] = "hand";
                    e.Row.Attributes["onclick"] = _jsSingle;
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    var lblTotal = (Label)e.Row.FindControl("lblTotal");
                    lblTotal.Text = "Total :" + gvLevel1.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting gvLevel1_RowDataBound - InvitationsOverview.aspx");
        }

        protected override void Render(HtmlTextWriter writer)
        {
            LoggingManager.Debug("Entering Render - InvitationsOverview.aspx");
           
            foreach (GridViewRow row in gvLevel1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    ClientScript.RegisterForEventValidation(((LinkButton)row.Cells[0].Controls[0]).UniqueID, "select$" + row.RowIndex);
                }
            }
            foreach (GridViewRow row in gvLevel2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    ClientScript.RegisterForEventValidation(((LinkButton)row.Cells[0].Controls[0]).UniqueID, "select$" + row.RowIndex);
                }
            }

            base.Render(writer);
            LoggingManager.Debug("Exiting Render - InvitationsOverview.aspx");
           
        }

        protected void gvLevel2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           LoggingManager.Debug("Entering gvLevel2_RowDataBound - InvitationsOverview.aspx");
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var _singleClickButton = (LinkButton)e.Row.Cells[0].Controls[0];
                    string _jsSingle = ClientScript.GetPostBackClientHyperlink(_singleClickButton, "select$" + e.Row.RowIndex);
                    e.Row.Style["cursor"] = "hand";
                    e.Row.Attributes["onclick"] = _jsSingle;
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    var lblTotal = (Label)e.Row.FindControl("lblTotal");
                    lblTotal.Text = "Total :" + gvLevel2.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting gvLevel2_RowDataBound - InvitationsOverview.aspx");
            
        }

        protected void gvLevel3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LoggingManager.Debug("Entering gvLevel3_RowDataBound - InvitationsOverview.aspx");
            try
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    var lblTotal = (Label)e.Row.FindControl("lblTotal");
                    lblTotal.Text = "Total :" + gvLevel3.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting gvLevel3_RowDataBound - InvitationsOverview.aspx");
        }
        private void LoadProfilePercentCompleted()
        {
            LoggingManager.Debug("Entering LoadProfilePercentCompleted - InvitationsOverview.aspx");
            try
            {
                var userId = Common.GetLoggedInUserId(Session);
                if (userId.HasValue)
                {
                    var percentCompleted = UserManager.GetProfilePercentCompleted(userId.Value);
                    lblPercentCompleted.Text = (percentCompleted / 100).ToString("00%");
                    int value = Convert.ToInt32(percentCompleted);
                    ProgressBar2.Value = value;
                    LoggingManager.Info("Percent Completed:" + lblPercentCompleted.Text);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadProfilePercentCompleted - InvitationsOverview.aspx");

        }
        
    }
}