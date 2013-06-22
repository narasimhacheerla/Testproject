using System;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Data;
using Huntable.Business;
using System.Configuration;
using Snovaspace.Util.Logging;
using Huntable.Entities;

namespace Huntable.UI
{
    public partial class FeaturedIndustry : System.Web.UI.Page
    {
        private readonly string _featuredCategoryPrice = ConfigurationManager.AppSettings["FeaturedCategoryPrice"];

        public string CheckOutSuccess
        {
            get
            {
                return Convert.ToString(Request.QueryString["CheckOutSuccess"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - FeaturedIndustry.aspx");
            try
            {
                if (!IsPostBack)
                {
                    if (!string.IsNullOrWhiteSpace(CheckOutSuccess))
                    {
                        if (Convert.ToBoolean(CheckOutSuccess))
                        {
                            SaveFeaturedData();
                            Session["FeaturedSelections"] = null;
                            Response.Redirect("CheckoutSuccess.aspx");
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(_featuredCategoryPrice))
                    {
                        lblFeaturedCategoryPrice.Text = _featuredCategoryPrice;
                    }

                    BindIndustriesForFeatured();
                    CheckSelectedData();
                    UpdateFeaturedCount();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - FeaturedIndustry.aspx");
        }

        private void SaveFeaturedData()
        {

            LoggingManager.Debug("Entering SaveFeaturedData - FeaturedIndustry.aspx");
            int? loggedinuserid = Common.GetLoggedInUserId();

            if (Session["FeaturedSelections"] != null && loggedinuserid != null)
            {
                var featiredSele = (FeaturedSelections)Session["FeaturedSelections"];
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    //Save Countries
                    DeleteCountriesForFeatured(loggedinuserid.Value);
                    foreach (int countryid in featiredSele.Countries)
                    {
                        context.AddToFeaturedCountries(new Data.FeaturedCountry { MasterCountryId = countryid, UserId = loggedinuserid.Value });
                    }

                    //Save Industries
                    DeleteIndustriesForFeatured(loggedinuserid.Value);
                    foreach (int industryid in featiredSele.Industries)
                    {
                        context.AddToFeaturedIndustries(new Data.FeaturedIndustry { MasterIndustryId = industryid, UserId = loggedinuserid.Value });
                    }

                    //Save Interests
                    DeleteInterestForFeatured(loggedinuserid.Value);
                    foreach (int interestid in featiredSele.Interests)
                    {
                        context.AddToFeaturedInterests(new Data.FeaturedInterest { MasterInterestId = interestid, UserId = loggedinuserid.Value });
                    }

                    //Save Skills
                    DeleteSkillForFeatured(loggedinuserid.Value);
                    foreach (int skillid in featiredSele.Skills)
                    {
                        context.AddToFeaturedSkills(new Data.FeaturedSkill { MasterSkillId = skillid, UserId = loggedinuserid.Value });
                    }

                    context.SaveChanges();
                }
            }
            LoggingManager.Debug("Exiting SaveFeaturedData - FeaturedIndustry.aspx");

        }
        private void DeleteSkillForFeatured(int loggedinuserid)
        {
            LoggingManager.Debug("Entering DeleteSkillForFeatured - FeaturedIndustry.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var skills = context.FeaturedSkills.Where(x => x.UserId == loggedinuserid).ToList();
                    foreach (Data.FeaturedSkill s in skills)
                    {
                        context.FeaturedSkills.DeleteObject(s);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DeleteSkillForFeatured - FeaturedIndustry.aspx");
        }
        private void DeleteInterestForFeatured(int loggedinuserid)
        {
            LoggingManager.Debug("Entering DeleteInterestForFeatured - FeaturedIndustry.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var interests = context.FeaturedInterests.Where(x => x.UserId == loggedinuserid).ToList();
                    foreach (Data.FeaturedInterest i in interests)
                    {
                        context.FeaturedInterests.DeleteObject(i);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DeleteInterestForFeatured - FeaturedIndustry.aspx");
        }
        private void DeleteIndustriesForFeatured(int loggedinuserid)
        {
            LoggingManager.Debug("Entering DeleteIndustriesForFeatured - FeaturedIndustry.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var industry = context.FeaturedIndustries.Where(x => x.UserId == loggedinuserid).ToList();
                    foreach (Data.FeaturedIndustry i in industry)
                    {
                        context.FeaturedIndustries.DeleteObject(i);

                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DeleteIndustriesForFeatured - FeaturedIndustry.aspx");
        }
        private void DeleteCountriesForFeatured(int loggedinuserid)
        {
            LoggingManager.Debug("Entering DeleteCountriesForFeatured - FeaturedIndustry.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var country = context.FeaturedCountries.Where(x => x.UserId == loggedinuserid).ToList();
                    foreach (Data.FeaturedCountry c in country)
                    {
                        context.FeaturedCountries.DeleteObject(c);
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting DeleteCountriesForFeatured - FeaturedIndustry.aspx");
        }

        private void CheckSelectedData()
        {
            LoggingManager.Debug("Entering CheckSelectedData - FeaturedIndustry.aspx");
            if (Session["FeaturedSelections"] != null)
            {
                var featiredSele = (FeaturedSelections)Session["FeaturedSelections"];
                foreach (WebControl item in dlIndustries.Items)
                {
                    int id = Int32.Parse(((Label)(item.FindControl("lblFeatured"))).Text);
                    if (featiredSele.Industries.Contains(id))
                    {
                        ((CheckBox)(item.FindControl("chkBtnFeatured"))).Checked = true;
                    }
                }
            }

            LoggingManager.Debug("Exiting CheckSelectedData - FeaturedIndustry.aspx");
        }
        private void UpdateFeaturedCount()
        {
            LoggingManager.Debug("Entering UpdateFeaturedCount - FeaturedIndustry.aspx");
            var featiredSele = (FeaturedSelections)Session["FeaturedSelections"];
            if (featiredSele.Jobpackage != null)
                lblJobPackage.Text = (featiredSele != null) ? featiredSele.Jobpackage : "0";
            lblIndustries.Text = (featiredSele != null) ? featiredSele.Industries.Count().ToString() : "0";
            lblCountries.Text = (featiredSele != null) ? featiredSele.Countries.Count().ToString() : "0";
            lblInterests.Text = (featiredSele != null) ? featiredSele.Interests.Count().ToString() : "0";
            lblSkills.Text = (featiredSele != null) ? featiredSele.Skills.Count().ToString() : "0";
            double jobpackage = 0;
            if (lblJobPackage.Text != string.Empty)
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
                lblTotalReach.InnerText = (totalCount * 650 * DateTime.Now.DayOfYear).ToString();
            }
            LoggingManager.Debug("Exiting UpdateFeaturedCount - FeaturedIndustry.aspx");

        }

        protected void BtnCheckOutClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCheckOutClick - FeaturedIndustry.aspx");

            UpdateSession();
            UpdateFeaturedCount();

            LoggingManager.Debug("Exiting BtnCheckOutClick - FeaturedIndustry.aspx");
        }
        private void UpdateSession()
        {
            LoggingManager.Debug("Entering UpdateSession - FeaturedIndustry.aspx");

            var featiredSele = new FeaturedSelections();
            if (Session["FeaturedSelections"] != null)
            {
                featiredSele = (FeaturedSelections)Session["FeaturedSelections"];
            }
            featiredSele.Industries.Clear();
            foreach (WebControl item in dlIndustries.Items)
            {
                if (((CheckBox)(item.FindControl("chkBtnFeatured"))).Checked)
                {
                    int id = Int32.Parse(((Label)(item.FindControl("lblFeatured"))).Text);
                    featiredSele.Industries.Add(id);
                }
            }
            Session["FeaturedSelections"] = featiredSele;

            LoggingManager.Debug("Exiting UpdateSession - FeaturedIndustry.aspx");
        }
        protected void BtnIndustryClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnIndustryClick - FeaturedIndustry.aspx");
            try
            {
                Server.Transfer("securecheckout.aspx?amt=" + lblTotalCost.Text + "&SuccessUrl=FeaturedIndustry.aspx" + "&FailureUrl=CheckoutError.aspx");
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnIndustryClick - FeaturedIndustry.aspx");
        }

        private void BindIndustriesForFeatured()
        {
            LoggingManager.Debug("Entering BindIndustriesForFeatured - FeaturedIndustry.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var masterIndustriesList = context.MasterIndustries.ToList();
                    dlIndustries.DataSource = masterIndustriesList;
                    dlIndustries.DataBind();
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BindIndustriesForFeatured - FeaturedIndustry.aspx");
        }
        protected void BtnPostOpportunityClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering BtnPostOpportunityClick - FeaturedIndustry.aspx");

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

            LoggingManager.Debug("Exiting BtnPostOpportunityClick - FeaturedIndustry.aspx");
        }
    }
}