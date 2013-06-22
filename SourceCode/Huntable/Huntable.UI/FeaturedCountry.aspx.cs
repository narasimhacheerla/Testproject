using System;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Data;
using System.Configuration;
using Snovaspace.Util.Logging;
using Huntable.Entities;
using Huntable.Business;

namespace Huntable.UI
{
    public partial class FeaturedCountry : System.Web.UI.Page
    {
        private readonly string _featuredCategoryPrice = ConfigurationManager.AppSettings["FeaturedCategoryPrice"];
        
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - FeaturedCountry.aspx");
            try
            {
                if (!IsPostBack)
                {
                    if (!string.IsNullOrWhiteSpace(_featuredCategoryPrice))
                    {
                        lblFeaturedCategoryPrice.Text = _featuredCategoryPrice;
                    }

                    BindCountriesForFeatured();
                    CheckSelectedData();
                    UpdateFeaturedCount();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - FeaturedCountry.aspx");
        }
        private void CheckSelectedData()
        {
            LoggingManager.Debug("Entering CheckSelectedData - FeaturedCountry.aspx");

            if (Session["FeaturedSelections"] != null)
            {
                var featiredSele = (FeaturedSelections)Session["FeaturedSelections"];
                foreach (WebControl item in dlCountries.Items)
                {
                    int id = Int32.Parse(((Label)(item.FindControl("lblFeatured"))).Text);
                    if (featiredSele.Countries.Contains(id))
                    {
                        ((CheckBox)(item.FindControl("chkBtnFeatured"))).Checked = true;
                    }
                }
            }

            LoggingManager.Debug("Exiting CheckSelectedData - FeaturedCountry.aspx");
        }
        private void UpdateFeaturedCount()
        {
            LoggingManager.Debug("Entering UpdateFeaturedCount - FeaturedCountry.aspx");

            var featiredSele = (FeaturedSelections)Session["FeaturedSelections"];
            if (featiredSele.Jobpackage != null)
                lblJobPackage.Text = (featiredSele != null) ? featiredSele.Jobpackage : "0";
            lblIndustries.Text = (featiredSele != null) ? featiredSele.Industries.Count().ToString() : "0";
            lblCountries.Text = (featiredSele != null) ? featiredSele.Countries.Count().ToString() : "0";
            lblInterests.Text = (featiredSele != null) ? featiredSele.Interests.Count().ToString() : "0";
            lblSkills.Text = (featiredSele != null) ? featiredSele.Skills.Count().ToString() : "0";
            double jobpackage = 0;
            if(lblJobPackage.Text!=string.Empty)
            jobpackage = Convert.ToDouble(lblJobPackage.Text);
            double amt = 0;
            if ((featiredSele != null) && (!string.IsNullOrWhiteSpace(_featuredCategoryPrice)))
            {
                amt = ((featiredSele.Industries.Count() + featiredSele.Interests.Count() + featiredSele.Countries.Count() + featiredSele.Skills.Count()) * Convert.ToDouble(_featuredCategoryPrice));
            }
            var totalamount = amt + jobpackage;
            lblTotalCost.Text = totalamount.ToString();
            if (featiredSele != null)
            {
                int totalCount = featiredSele.Industries.Count() + featiredSele.Interests.Count() + featiredSele.Countries.Count() + featiredSele.Skills.Count();
                lblTotalReach.InnerText = (totalCount*650*DateTime.Now.DayOfYear).ToString();
            }
            LoggingManager.Debug("Exiting UpdateFeaturedCount - FeaturedCountry.aspx");

        }

        protected void BtnCheckOutClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCheckOutClick - FeaturedCountry.aspx");
            UpdateSession();
            UpdateFeaturedCount();

            LoggingManager.Debug("Exiting BtnCheckOutClick - FeaturedCountry.aspx");
        }
        private void UpdateSession()
        {
            LoggingManager.Debug("Entering UpdateSession - FeaturedCountry.aspx");

            var featiredSele = new FeaturedSelections();
            if (Session["FeaturedSelections"] != null)
            {
                featiredSele = (FeaturedSelections)Session["FeaturedSelections"];
            }
            featiredSele.Countries.Clear();
            foreach (WebControl item in dlCountries.Items)
            {
                if (((CheckBox)(item.FindControl("chkBtnFeatured"))).Checked)
                {
                    int id = Int32.Parse(((Label)(item.FindControl("lblFeatured"))).Text);
                    featiredSele.Countries.Add(id);
                }
            }
            Session["FeaturedSelections"] = featiredSele;

            LoggingManager.Debug("Exiting UpdateSession - FeaturedCountry.aspx");
        }
        protected void BtnCountryClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCountryClick - FeaturedCountry.aspx");
            try
            {
                Server.Transfer("securecheckout.aspx?amt=" + lblTotalCost.Text + "&SuccessUrl=FeaturedIndustry.aspx" + "&FailureUrl=CheckoutError.aspx");
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnCountryClick - FeaturedCountry.aspx");
        }

        private void BindCountriesForFeatured()
        {
            LoggingManager.Debug("Entering BindCountriesForFeatured - FeaturedCountry.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var masterCountriesList = context.MasterCountries.ToList();
                    dlCountries.DataSource = masterCountriesList;
                    dlCountries.DataBind();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BindCountriesForFeatured - FeaturedCountry.aspx");

        }
        protected void BtnPostOpportunityClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnPostOpportunityClick - FeaturedCountry.aspx");

            var loggedInUserId = Common.GetLoggedInUserId(Session);
            var jobManager = new InvitationManager();
            var result = jobManager.GetUserDetails(loggedInUserId.Value);
            string credit = (result.CreditsLeft).ToString();

            if (result.IsPremiumAccount == false)
            {
                Server.Transfer("WhatIsHuntableUpgrade.aspx");
            }
            else if (result.CreditsLeft == null)
            {
                Server.Transfer("BuyCredit.aspx");
            }
            else
            {
                Server.Transfer("PostJob.aspx");
            }

            LoggingManager.Debug("Exiting BtnPostOpportunityClick - FeaturedCountry.aspx");
        }
    }
}