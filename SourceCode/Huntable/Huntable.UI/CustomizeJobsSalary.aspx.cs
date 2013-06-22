using System;
using System.Collections.Generic;
using System.Linq;
using Huntable.Data;
using Snovaspace.Util.Logging;
using Huntable.Business;

namespace Huntable.UI
{
    public partial class CustomizeFeedsSalary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CustomizeFeedsSalary");

            if (!IsPostBack)
            {
                LoadData();
                LoadDDls();
            }
            LoggingManager.Debug("Exiting Page_Load - CustomizeFeedsSalary");

        }
        public void LoadData()
        {
            LoggingManager.Debug("Entering LoadData - CustomizeFeedsSalary");
            var userId = Common.GetLoggedInUserId(Session);
            if (userId == null) { ucPplUMayKnow1.Visible = false; }

            if (userId.HasValue)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var prefJobSalary = context.PreferredJobUserSalaries.Any(i => i.UserId == userId);
                    if (prefJobSalary != false)
                    {
                        var user = context.PreferredJobUserSalaries.First(i => i.UserId == userId);
                        if (user.MinSalary != null)
                            ddlMin.SelectedValue = (user.MinSalary.HasValue ? user.MinSalary : -1).ToString();
                        if (user.MaxSalary != null)
                            ddlMax.SelectedValue = (user.MaxSalary.HasValue ? user.MaxSalary : -1).ToString();
                        ddlCurrency.SelectedValue = (user.CurrencyTypeId.HasValue ? user.CurrencyTypeId : -1).ToString();
                    }
                    //else
                    //{
                    //    var pref = new PreferredJobUserSalary
                    //        {
                    //            MinSalary = 2,
                    //            MaxSalary = 2,
                    //            CurrencyTypeId = 1,
                    //            UserId = Convert.ToInt32(userId)
                    //        };
                    //    context.PreferredJobUserSalaries.AddObject(pref);
                    //    context.SaveChanges();
                    //}
                }

            }
            LoggingManager.Debug("Exiting LoadData - CustomizeFeedsSalary");
        }
        private void LoadDDls()
        {
            LoggingManager.Debug("Entering LoadDDls - CustomizeFeedsSalary");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {

                    var masterCurrencyType = context.MasterCurrencyTypes.ToList();
                    ddlCurrency.DataTextField = "Description";
                    ddlCurrency.DataValueField = "Id";
                    ddlCurrency.DataSource = masterCurrencyType;
                    ddlCurrency.DataBind();

                    var masterMinSalary = context.MasterMinimumSalaries.ToList();
                    ddlMin.DataTextField = "MinimumSalary";
                    ddlMin.DataValueField = "Id";
                    ddlMin.DataSource = masterMinSalary;
                    ddlMin.DataBind();

                    var masterMaxSalary = context.MasterMaximumSalaries.ToList();
                    ddlMax.DataTextField = "MaximumSalary";
                    ddlMax.DataValueField = "Id";
                    ddlMax.DataSource = masterMaxSalary;
                    ddlMax.DataBind();

                }
            }

            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting LoadDDls - CustomizeFeedsSalary");
        }

        protected void BtnSaveClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSaveClick - CustomizeFeedsSalary");

            var loggedInUserId = Common.GetLoggedInUserId(Session);
            try
            {
                if (loggedInUserId.HasValue)
                {
                   
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        if (context.PreferredJobUserSalaries.Any(i => i.UserId == loggedInUserId))
                        {
                            DeleteFeeds();
                        }
                        var prefSal = new PreferredJobUserSalary();
                        
                           if(ddlMin.SelectedValue != "-1")
                          {prefSal.MinSalary = Convert.ToInt32(ddlMin.SelectedItem.Value);}
                        
                       if(ddlMax.SelectedValue != "-1"){prefSal.MaxSalary = Convert.ToInt32(ddlMax.SelectedItem.Value);}

                          if(ddlCurrency.SelectedValue != "-1"){prefSal.CurrencyTypeId = Convert.ToInt32(ddlCurrency.SelectedItem.Value);}
                            prefSal.UserId = Convert.ToInt32(loggedInUserId);
                       
                        context.PreferredJobUserSalaries.AddObject(prefSal);
                        context.SaveChanges();                        
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "click", "overlay('preferences saved successfully');", true);
                        hdnsave.Value = "save";
                    
                }
            }}
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnSaveClick - CustomizeFeedsSalary");
        }

        protected void BtnNextClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnNextClick - CustomizeFeedsSalary");
            var userId = Common.GetLoggedInUserId(Session);
            var context = new huntableEntities();
            var user = context.PreferredJobUserSalaries.FirstOrDefault(i => i.UserId == userId);
            if (user != null)
            {
                if ((user.MaxSalary != Convert.ToInt16(ddlMax.SelectedValue)&& ddlMax.SelectedValue != "-1") ||
                    (user.MinSalary != Convert.ToInt16(ddlMin.SelectedValue) && ddlMin.SelectedValue != "-1") ||
                    (user.CurrencyTypeId != Convert.ToInt16(ddlCurrency.SelectedValue)&& ddlCurrency.SelectedValue != "-1"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "click", "rowAction2('test');", true);
                }
                else
                {
                    Nextop();
                }
            }
            else
            {
                if (ddlMax.SelectedValue != "-1" || ddlMin.SelectedValue != "-1" || ddlCurrency.SelectedValue != "-1")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "click", "rowAction2('test');", true);
                }
                else
                {
                    Nextop();
                }
            }
            LoggingManager.Debug("Exiting BtnNextClick - CustomizeFeedsSalary");
        }

        public void Nextop()
        {
            LoggingManager.Debug("Entering Nextop - CustomizeFeedsSalary");
            if (HiddenField1.Value != string.Empty)
            {
                Response.Redirect(HiddenField1.Value);
            }
            else
            {


                Response.Redirect("CustomizeJobsIndustry.aspx");
            }
            LoggingManager.Debug("Exiting Nextop - CustomizeFeedsSalary");
        }

        protected void BtnActualValuesClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnActualValuesClick - CustomizeFeedsSalary");
            LoadData();
            LoggingManager.Debug("Exiting BtnActualValuesClick - CustomizeFeedsSalary");
        }
        private void DeleteFeeds()
        {
            LoggingManager.Debug("Entering DeleteFeeds - CustomizeFeedsSalary");

            var userId = Common.GetLoggedInUserId(Session);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                if (context.PreferredJobUserSalaries.Any(x => x.UserId == userId))
                {
                    List<PreferredJobUserSalary> salDetails = context.PreferredJobUserSalaries.Where(x => x.UserId == userId).ToList();
                    foreach (var salDet in salDetails)
                    {
                        context.PreferredJobUserSalaries.DeleteObject(salDet);
                        context.SaveChanges();
                    }
                }
            }

            LoggingManager.Debug("Exiting DeleteFeeds - CustomizeFeedsSalary");
        }
    }
}