using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Huntable.Entities;
using Snovaspace.Util;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class BuyCredit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - BuyCredit");
            try
            {
                if (!IsPostBack)
                {
                    credits.Visible = false;
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                         var loginUserId = Common.GetLoggedInUserId(Session);
                        var user1 = context.Users.FirstOrDefault(u => u.Id == loginUserId.Value);
                        if (user1.CreditsLeft <= 0||user1.CreditsLeft==null)
                        {
                            credits.Visible = true;
                        }
                        if (user1.FreeCredits==true)
                        {
                            credits.Visible = false;
                        }
                    }
                    GetJobPackages();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting Page_Load - BuyCredit");
        }
        protected void BtnGetItFreeClick(object sender, EventArgs e)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                 var loginUserId = Common.GetLoggedInUserId(Session);
                var user = context.Users.FirstOrDefault(u => u.Id == loginUserId.Value);
                user.CreditsLeft = 10;
                user.FreeCredits = true;
                context.SaveChanges();
                credits.Visible = false;
                hdntemp.Value = "click";
                Response.Redirect("PostJob.aspx");
            }
        }
        private void GetJobPackages()
        {
            LoggingManager.Debug("Entering GetJobPackages - BuyCredit");

            var dataManager = new JobsManager();
            IList<CustomMasterJobPackage> list = dataManager.AllJobPackages();
            rblCredits.DataSource = list;
            rblCredits.DataBind();

            for (int i = 0; i < rblCredits.Items.Count; i++)
            {
                if (i != 0)
                    rblCredits.Items[i].Attributes.Add("class", "bg-ash");
            }
            LoggingManager.Debug("Exiting GetJobPackages - BuyCredit");
        }
        
        protected void BtnAddClick(object sender, EventArgs e)
        {
            LoggingManager.Info("Clicked on add button and Redirecting to featured page");
            if (hdntemp.Value != "click"&&credits.Visible==true)
            {
                Response.Redirect("WhatIsHuntableUpgrade.aspx");
            }
            else
            {
                var featiredSele = new FeaturedSelections();
                string[] result = null;
                LoggingManager.Info("Checking for selected Job Package");
                foreach (ListItem lst in rblCredits.Items)
                {
                    if (lst.Selected)
                    {
                        string val = lst.Value;
                        result = val.Split(',');
                    }
                }
                if (result != null)
                {
                    string val1 = result[1];
                    featiredSele.Jobpackage = val1;
                    Session["FeaturedSelections"] = featiredSele;
                }
                Response.Redirect("featuredindustry.aspx", false);
           }
        }

        protected void BtnContinueClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnContinueClick - BuyCredit");
            try
            {
                if (hdntemp.Value != "click"&&credits.Visible==true)
                {
                    Response.Redirect("WhatIsHuntableUpgrade.aspx");
                }
                else
                {
                   var featiredSele = new FeaturedSelections();
                    string[] result = null;
                    LoggingManager.Info("Checking for selected Job Package");
                    foreach (ListItem lst in rblCredits.Items)
                    {
                        if (lst.Selected)
                        {
                            string val = lst.Value;
                            result = val.Split(',');
                        }
                    }
                    if (result != null)
                    {
                        string val1 = result[1];
                        featiredSele.Jobpackage = val1;
                        Session["FeaturedSelections"] = featiredSele;
                        LoggingManager.Info("selected Job Package " + result[0] +
                                            " and Redirecting to secure checkout page");
                        Server.Transfer(
                            "securecheckout.aspx?SuccessUrl=PostJob.aspx&amt=" + result[1] + "&Credits=" + result[0],
                            false);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting btnContinueClick - BuyCredit");
        }
    }
}