using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Huntable.Data;
using Huntable.Business;
using Snovaspace.Util;
using Snovaspace.Util.Logging;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;

namespace Huntable.UI
{
    public partial class EditProfilePage : Page
    {

        private List<EmploymentHistory> EmploymentHistories
        {
            get
            {
                return Session["EmpHistories"] as List<EmploymentHistory>;
            }
            set
            {
                Session["EmpHistories"] = value;
            }
        }

        private List<EducationHistory> EducationalHistories
        {
            get
            {
                return Session["EducationalHistories"] as List<EducationHistory>;
            }
            set
            {
                Session["EducationalHistories"] = value;
            }
        }

        private int LoginUserId
        {
            get
            {
                var loggedInUserId = Common.GetLoggedInUserId(Session);
                if (loggedInUserId != null) return loggedInUserId.Value;
                return 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Enter Page_Load - EditProfilePage.aspx");

            //Page.Form.Attributes.Add("enctype", "multipart/form-data");
            Page.Form.Enctype = "multipart/form-data";
            if (!IsPostBack)
            {
                try
                {
                    LoggingManager.Debug("Loading the DDLs in Edit profile page.");
                    LoadDDls();
                    LoggingManager.Debug("Loading the profile details in Edit profile page.");
                    LoadProfileDetails();
                    LoadProfilePercentCompleted();

                
                    var facebookClientId = System.Configuration.ConfigurationManager.AppSettings["facebookAppID"];
                    var host = Request.ServerVariables["HTTP_HOST"];
                    var protocol = Request.Url.Scheme;
                    imgBtnFB.PostBackUrl = string.Concat(@"https://graph.facebook.com/oauth/authorize?client_id=" + facebookClientId + "&",
                                                                "redirect_uri=" + protocol + "://" + host + @"/ImportDetailsFromFacebook.aspx&scope=email,user_about_me,user_birthday,user_location,user_website,user_work_history,user_education_history,user_likes");

                }
                catch (Exception ex)
                {
                    LoggingManager.Error(ex);
                }
            }
            if (Common.IsLoggedIn())
            {
                //GetUserLanguages();
                //GetUserInterests();
                GetUserSkills();
                Display();
                GetInterests();
            }
            // trying another way of displaying the content
            //using (var context = huntableEntities.GetEntitiesWithNoLock())
            //{

            //    var lang = context.UserLanguages.Where(x => x.UserId == LoginUserId).ToList();

            //    dllang.DataSource = lang;
            //    dllang.DataBind();
            //    //  List<UserLanguage> userlanguages = context.UserLanguages.Where(l => l.UserId == LoginUserId).ToList();

            //    //pnlLanguages.Controls.Add(new LiteralControl("<tr><td width='50%'>" + ul.MasterLanguage.Description + "</td><td width='50%'><img src='images/rating-star" + ul.Level + ".png' /></td></tr>"));


            //}


            LoggingManager.Debug("Exit Page_Load - EditProfilePage.aspx");
        }

        public void Display()
        {
            LoggingManager.Debug("Entering Display - EditProfilePage.aspx");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                var lang = context.UserLanguages.Where(x => x.UserId == LoginUserId).ToList();

                dllang.DataSource = lang;
                dllang.DataBind();

            }
            LoggingManager.Debug("Exiting Display - EditProfilePage.aspx");
        }


        public string Stars(object id)
        {
            LoggingManager.Debug("Entering Display - EditProfilePage.aspx");
            if (id != null)
            {
                string img = "images/rating-star" + id + ".png";
                return img;
            }
            LoggingManager.Debug("Exitng Display - EditProfilePage.aspx");
            return null;
        }

        public string Interest(object id)
        {
            LoggingManager.Debug("Entering Interest - EditProfilePage.aspx");
            if (id != null)
            {
                string img = "images/interests/" + id + ".png";
                return img;
            }
            LoggingManager.Debug("Entering Interest - EditProfilePage.aspx");
            return null;
        }

        protected void DeleteClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering DeleteClick - EditProfilePage.aspx");
            var button = sender as ImageButton;

            if (button != null)
            {
                int id = Convert.ToInt32(button.CommandArgument);

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var selectedRow = context.UserLanguages.First(x => x.MasterLanguageId == id && x.UserId == LoginUserId);
                    context.DeleteObject(selectedRow);
                    UserManager.ProfileUpdatedOn(LoginUserId);
                    ScriptManager.RegisterStartupScript(this, GetType(), "Call my function", "overlay('Details saved succesfully')", true);
                    context.SaveChanges();

                    Display();
                }



            }
            LoggingManager.Debug("Exiting FollowupClick-EditProfilePage.aspx");
        }
        protected void InterestClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering InterestClick-EditProfilePage.aspx");
            var button = sender as ImageButton;

            if (button != null)
            {
                int id = Convert.ToInt32(button.CommandArgument);

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var selectedRow = context.UserInterests.First(x => x.MasterInterestId == id && x.UserId == LoginUserId);
                    context.DeleteObject(selectedRow);
                    UserManager.ProfileUpdatedOn(LoginUserId);
                    context.SaveChanges();

                    GetInterests();
                }
            }
            LoggingManager.Debug("Exiting InterestClick-EditProfilePage.aspx");

        }

        protected void BtnImportFromLinkedinClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering BtnprofileClick - ProfileUploadPage.aspx");
            Response.Redirect("LinkedInProfile.aspx");
            LoggingManager.Debug("Exiting BtnprofileClick - ProfileUploadPage.aspx");
        }

        private void LoadDDls()
        {
            LoggingManager.Debug("Entering LoadDDls - EditProfilePage.aspx");
            try
            {
                new Utility().BindDropdownList(ddlNationality, MasterDataManager.AllNationalities)
                    .BindDropdownList(ddlCountry, MasterDataManager.AllCountries)
                    .BindDropdownList(ddlExpIndustry, MasterDataManager.AllIndustries)
                    .BindDropdownList(ddlMin, MasterDataManager.AllMasterMinimumSalary, "Id", "MinimumSalary")
                    .BindDropdownList(ddlMax, MasterDataManager.AllMasterMaximumSalary, "Id", "MaximumSalary")
                    .BindDropdownList(ddlCurr, MasterDataManager.AllCurrencyTypes)
                    .BindDropdownList(ddlInterest, MasterDataManager.AllInterests)
                    .BindDropdownList(ddlSkillHowLongYears, MasterDataManager.AllExpYears)
                    .BindDropdownList(ddlSkillGoodHowLong, MasterDataManager.AllExpYears)
                    .BindDropdownList(ddlExpertSkillHowLong, MasterDataManager.AllExpYears);

            }

            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting LoadDDls - EditProfilePage.aspx");
        }

        private void LoadProfileDetails()
        {
            LoggingManager.Debug("Entering LoadProfileDetails - EditProfilePage.aspx");
            try
            {
                var userId = Common.GetLoggedInUserId(Session);
                if (userId.HasValue)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var user = context.Users.First(u => u.Id == userId.Value);
                        var userSal1 = context.PreferredJobUserSalaries.Where(u => u.UserId == userId.Value).ToList();
                        userSal1.Reverse();
                        var userSal = userSal1.FirstOrDefault();
                        if (userSal != null && userSal.MinSalary.HasValue)
                        {
                            var min = context.MasterMinimumSalaries.FirstOrDefault(m => m.Id == userSal.MinSalary.Value);
                            if (min != null)
                            {
                                foreach (ListItem ddl in ddlMin.Items)
                                {
                                    if (ddl.Text == (min.MinimumSalary).ToString())
                                        ddl.Selected = true;
                                }

                            }
                            //ddlMin.SelectedItem.Text = (min != null ? min.MinimumSalary.ToString() : "");
                        }
                        if (userSal != null && userSal.MaxSalary.HasValue)
                        {
                            var max = context.MasterMaximumSalaries.FirstOrDefault(m => m.Id == userSal.MaxSalary.Value);

                            if (max != null)
                            {
                                foreach (ListItem ddl in ddlMax.Items)
                                {
                                    if (ddl.Text == (max.MaximumSalary).ToString() && ddl.Value == max.Id.ToString())
                                        ddl.Selected = true;
                                }
                                //ddlMax.SelectedItem.Text = (max != null ? max.MaximumSalary.ToString() : "");
                            }
                        }
                        if (userSal != null && userSal.CurrencyTypeId.HasValue)
                        {
                            var currency = context.MasterCurrencyTypes.FirstOrDefault(c => c.ID == userSal.CurrencyTypeId.Value);

                            if (currency != null)
                            {
                                foreach (ListItem ddl in ddlCurr.Items)
                                {
                                    if (ddl.Text == currency.Description)
                                        ddl.Selected = true;
                                }
                            }

                        }
                        cbAvailaleNow.Checked = user.IsProfileAvailable;
                        txtExp.Text = user.TotalExperienceInYears.ToString();
                        txtWebSite.Text = user.WebsiteAddress;
                        txtBlog.Text = user.BlogAddress;
                        {
                            hfUserId.Value = user.Id.ToString();
                            txtSummary.Text = user.Summary;

                            EmploymentHistories = user.EmploymentHistories.ToList();

                            ReloadEmployment(true, rpCurrentExperience);
                            ReloadEmployment(false, rpPastEmployment);

                            EducationalHistories = user.EducationHistories.ToList();
                            ReloadEducationalHistories(false, rpEducation);
                            ReloadEducationalHistories(true, rpSchool);
                        }
                        if (user.NationalityId.HasValue)
                        {
                            ddlNationality.SelectedValue = user.NationalityId.Value.ToString();
                        }
                        txtAlternameEmail.Text = user.SecondaryEmail;
                        if (user.DOB.HasValue)
                        {
                            txtDOB.Text = user.DOB.Value.ToString("dd/MM/yyyy");
                        }
                        txtPositionLookingFor.Text = user.PositionLookingFor;
                        txtPhoneNumber.Text = user.PhoneNumber;
                        txtHomeAddress.Text = user.HomeAddress;
                        txtCity.Text = user.City;
                        ddlCountry.Items.Insert(0, new ListItem("--Please Select--", "-1"));
                        ddlCountry.SelectedValue = (user.CountryID.HasValue ? user.CountryID.Value : -1).ToString();
                        ddlNationality.Items.Insert(0,new ListItem("--Please Select--","-1"));
                        ddlInterest.Items.Insert(0, new ListItem("--Please Select--", "-1"));
                        ddlCurr.Items.Insert(0, new ListItem("--Please Select--", "-1"));
                        ddlMax.Items.Insert(0, new ListItem("--Please Select--", "-1"));
                        ddlMin.Items.Insert(0, new ListItem("--Please Select--", "-1"));
                        ddlSkillHowLongYears.Items.Insert(0, new ListItem("How Long?", "-1"));
                        ddlSkillGoodHowLong.Items.Insert(0, new ListItem("How Long?", "-1"));
                        ddlExpertSkillHowLong.Items.Insert(0, new ListItem("How Long?", "-1"));
                        if (user.ExpectedIndustry.HasValue)
                            ddlExpIndustry.SelectedValue = user.ExpectedIndustry.Value.ToString();

                        rbMartialStatus.SelectedIndex = user.IsMarried ? 0 : 1;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting LoadProfileDetails - EditProfilePage.aspx");
        }

        protected void RpCurrentExperienceItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LoggingManager.Debug("Entering RpCurrentExperienceItemCommand - EditProfilePage.aspx");
            try
            {

                SaveEmploymentHistory(e, true, rpCurrentExperience);
                UserManager.ProfileUpdatedOn(LoginUserId);

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting RpCurrentExperienceItemCommand - EditProfilePage.aspx");
        }

        //private void cb_CheckedChanged(object sender, EventArgs e)
        //{

        //    CheckBox cb = (CheckBox)sender;
        //    if (cb.Checked)
        //    {


        //        DropDownList ddlM = ri.FindControl("ddlToMonth") as DropDownList;
        //        DropDownList ddlY = ri.FindControl("ddlToYear") as DropDownList;
        //        ddlM.Visible = false;
        //        ddlY.Visible = false;
        //    }
        //}
        private void SaveEmploymentHistory(RepeaterCommandEventArgs e, bool isCurrent, Repeater repeater)
        {
            LoggingManager.Debug("Entering SaveEmploymentHistory - EditProfilePage.aspx");
            try
            {
                switch (e.CommandName)
                {
                    case "Edit":
                        var idHiddenField = e.Item.FindControl("id") as HiddenField;
                        e.Item.FindControl("tblReadonly").Visible = false;
                        e.Item.FindControl("tblEditMode").Visible = true;
                        var iframe = (HtmlControl)e.Item.FindControl("ifPortfoliosAchievementsVideos");
                        iframe.Style.Add("display", "");
                        iframe.Attributes.Add("scrolling", "no");
                        if (idHiddenField != null)
                            iframe.Attributes.Add("src", "PortfoliosAchievementsVideos.aspx?id=" + idHiddenField.Value);
                        iframe.Attributes.Add("onload", "this.style.height = this.contentWindow.document.body.offsetHeight + 'px';");
                        var button = e.Item.FindControl("btnSave") as Button;
                        if (button != null)
                            button.Visible = false;
                        var button1 = e.Item.FindControl("btnCancel") as Button;
                        if (button1 != null)
                            button1.Visible = false;
                        break;

                    case "Remove":
                        var hiddenField = e.Item.FindControl("id") as HiddenField;
                        if (hiddenField != null)
                        {
                            string tempId = hiddenField.Value;
                            RemoveEmployment(tempId, isCurrent, repeater);
                            ScriptManager.RegisterStartupScript(this, GetType(), "Call my function", "overlay('Details saved succesfully')", true);                            
                        }
                        break;
                    
                    case "Move":
                        var movetoPast = e.Item.FindControl("id") as HiddenField;
                            if (movetoPast != null)
                            {
                                string tempid = movetoPast.Value;
                                MoveToPastExperience(tempid, isCurrent, repeater);
                                ReloadEmployment(isCurrent, repeater);
                                ScriptManager.RegisterStartupScript(this, GetType(), "Call my function", "overlay('Details saved succesfully')", true);
                               
                            }
                        break;
                    case "Save":
                        var field = e.Item.FindControl("id") as HiddenField;
                        if (field != null)
                        {
                            string id = field.Value;
                            int convertedId;
                            EmploymentHistory employmentHistory = int.TryParse(id, out convertedId) ? EmploymentHistories.First(h => h.Id == convertedId) : EmploymentHistories.First(h => h.TempId == id);
                            var textBox = e.Item.FindControl("txtJobTitle") as TextBox;
                            if (textBox != null)
                                employmentHistory.JobTitle = textBox.Text;

                            var box = e.Item.FindControl("txtCompany") as TextBox;

                            if (box != null)
                            {
                                box.Text = box.Text.Trim();
                                employmentHistory.CompanyId = MasterDataManager.GetMasterCompanyId(box.Text);
                            }

                            if (isCurrent)
                            {

                                var textPSkill = e.Item.FindControl("txtSkill") as TextBox;
                                if (textPSkill != null)
                                    employmentHistory.SkillId = MasterDataManager.GetSkillId(textPSkill.Text);

                                var fromMonthDDl = e.Item.FindControl("ddlFromMonth") as DropDownList;
                                if (fromMonthDDl != null && fromMonthDDl.SelectedItem != null)
                                {
                                    employmentHistory.FromMonthID = Convert.ToInt32(fromMonthDDl.SelectedItem.Value);
                                }

                                var ddlFromYear = e.Item.FindControl("ddlFromYear") as DropDownList;
                                if (ddlFromYear != null && ddlFromYear.SelectedItem != null)
                                {
                                    employmentHistory.FromYearID = Convert.ToInt32(ddlFromYear.SelectedItem.Value);
                                }

                                var ddlToMonth = e.Item.FindControl("ddlToMonth") as DropDownList;
                                if (ddlToMonth != null && ddlToMonth.SelectedItem != null)
                                {
                                    employmentHistory.ToMonthID = Convert.ToInt32(ddlToMonth.SelectedItem.Value);
                                }

                                var ddlToYear = e.Item.FindControl("ddlToYear") as DropDownList;
                                if (ddlToYear != null && ddlToYear.SelectedItem != null)
                                {
                                    employmentHistory.ToYearID = Convert.ToInt32(ddlToYear.SelectedItem.Value);
                                }

                                var ddlLevel = e.Item.FindControl("ddlLevel") as DropDownList;
                                if (ddlLevel != null && ddlLevel.SelectedItem != null)
                                {
                                    employmentHistory.LevelId = Convert.ToInt32(ddlLevel.SelectedItem.Value);
                                }
                            }
                            else
                            {
                                var textSkill = e.Item.FindControl("txtSkill") as TextBox;
                                if (textSkill != null)
                                    employmentHistory.SkillId = MasterDataManager.GetSkillId(textSkill.Text);

                                var fromMonthDDl = e.Item.FindControl("ddlFromMonth1") as DropDownList;
                                if (fromMonthDDl != null && fromMonthDDl.SelectedItem != null)
                                {
                                    employmentHistory.FromMonthID = Convert.ToInt32(fromMonthDDl.SelectedItem.Value);
                                }

                                var ddlFromYear = e.Item.FindControl("ddlFromYear1") as DropDownList;
                                if (ddlFromYear != null && ddlFromYear.SelectedItem != null)
                                {
                                    employmentHistory.FromYearID = Convert.ToInt32(ddlFromYear.SelectedItem.Value);
                                }

                                var ddlToMonth = e.Item.FindControl("ddlToMonth1") as DropDownList;
                                if (ddlToMonth != null && ddlToMonth.SelectedItem != null)
                                {
                                    employmentHistory.ToMonthID = Convert.ToInt32(ddlToMonth.SelectedItem.Value);
                                }

                                var ddlToYear = e.Item.FindControl("ddlToYear1") as DropDownList;
                                if (ddlToYear != null && ddlToYear.SelectedItem != null)
                                {
                                    employmentHistory.ToYearID = Convert.ToInt32(ddlToYear.SelectedItem.Value);
                                }

                                var ddlLevel = e.Item.FindControl("ddlLevel") as DropDownList;
                                if (ddlLevel != null && ddlLevel.SelectedItem != null)
                                {
                                    employmentHistory.LevelId = Convert.ToInt32(ddlLevel.SelectedItem.Value);
                                }
                            }

                            var ddlPstExpCountry = e.Item.FindControl("ddlPstExpCountry") as DropDownList;
                            if (ddlPstExpCountry != null && ddlPstExpCountry.SelectedItem != null)
                            {
                                employmentHistory.CountryId = Convert.ToInt32(ddlPstExpCountry.SelectedItem.Value);
                            }
                            var textBox2 = e.Item.FindControl("txtTown") as TextBox;
                            if (textBox2 != null)
                                employmentHistory.Town = textBox2.Text;

                            var textBox1 = e.Item.FindControl("txtDescription") as TextBox;
                            if (textBox1 != null)
                                employmentHistory.Description = textBox1.Text;

                            var ddlIndustry = e.Item.FindControl("ddlIndustry") as DropDownList;
                            if (isCurrent)
                            {
                                var checkBox = e.Item.FindControl("cbPresent") as CheckBox;
                                employmentHistory.EmploymentPresent = checkBox != null && checkBox.Checked;
                            }


                            if (ddlIndustry != null && ddlIndustry.SelectedItem != null)
                            {
                                employmentHistory.IndustryId = Convert.ToInt32(ddlIndustry.SelectedItem.Value);
                            }
                            int? historyId = null;
                            if (convertedId > 0)
                            {
                                historyId = convertedId;
                            }
                            employmentHistory.IsCurrent = isCurrent;
                            UserProfileManager.SaveEmploymentHistory(employmentHistory, historyId, LoginUserId);
                            ScriptManager.RegisterStartupScript(this, GetType(), "Call my function", "overlay('Details saved succesfully')", true);                          
                        }
                        using (var context = huntableEntities.GetEntitiesWithNoLock())
                        {
                            int userId = LoginUserId;
                            EmploymentHistories =
                                context.EmploymentHistories.Include("MasterCompany").Include("MasterMonth").Include("MasterYear").Include("MasterMonth1")
                                .Include("MasterYear1").Include("MasterIndustry").Include("UserEmploymentSkills.MasterSkill").Include("MasterLevel").Where(
                                    x => x.UserId == userId).ToList();
                        }
                        ReloadEmployment(isCurrent, repeater);
                        //using (var context01 = huntableEntities.GetEntitiesWithNoLock())
                        //{
                        //    int userId = LoginUserId;


                        //     var textboxskill = e.Item.FindControl("txtSkill") as TextBox;

                        //    var skillId= context01.MasterSkills.FirstOrDefault(s=>s.Description==textboxskill.Text);
                        //    var res= context01.UserEmploymentSkills.FirstOrDefault(x=>x.EmploymentHistoryId==skillId.Id);
                        //    if (res==null)
                        //    {
                        //        var hisId = context01.EmploymentHistories.FirstOrDefault(s => s.UserId == userId);
                        //        var skilltosave = new UserEmploymentSkill
                        //        {
                        //            EmploymentHistoryId = hisId.Id,
                        //            MasterSkillId = Convert.ToInt32(skillId.Id)
                        //        };
                        //        context01.UserEmploymentSkills.AddObject(skilltosave);
                        //        context01.SaveChanges();
                        //    }
                        //    else
                        //    {
                        //        var hisId = context01.EmploymentHistories.FirstOrDefault(s => s.UserId == userId);
                        //        res.EmploymentHistoryId = hisId.Id;
                        //        //context01.UserEmploymentSkills.AddObject(res);
                        //        context01.SaveChanges();
                        //    }


                        //}

                        break;

                    case "Cancel":
                        int id1;
                        var hiddenField1 = e.Item.FindControl("id") as HiddenField;
                        if (hiddenField1 != null && int.TryParse(hiddenField1.Value, out id1))
                        {
                            e.Item.FindControl("tblReadonly").Visible = true;
                            e.Item.FindControl("tblEditMode").Visible = false;
                        }
                        else
                        {
                            var field1 = e.Item.FindControl("id") as HiddenField;
                            if (field1 != null)
                                RemoveEmployment(field1.Value, isCurrent, repeater);
                        }
                        break;
                }
                UserManager.ProfileUpdatedOn(LoginUserId);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting SaveEmploymentHistory - EditProfilePage.aspx");
        }

        private void RemoveEmployment(string tempId, bool isCurrent, Repeater repeater)
        {
            LoggingManager.Debug("Entering RemoveEmployment - EditProfilePage.aspx");
            try
            {
                int idToDelete;
                if (int.TryParse(tempId, out idToDelete))
                {
                    UserProfileManager.DeleteEmploymentHistory(idToDelete);
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        int userId = Convert.ToInt32(hfUserId.Value);
                        EmploymentHistories =
                            context.EmploymentHistories.Include("MasterCompany").Include("MasterMonth").Include("MasterYear").Include("MasterMonth1")
                                .Include("MasterYear1").Include("MasterIndustry").Include("UserEmploymentSkills.MasterSkill").Include("MasterLevel").Where(
                                x => x.UserId == userId).ToList();
                    }
                }
                else
                {
                    List<EmploymentHistory> employmentHistories = EmploymentHistories;
                    employmentHistories.Remove(employmentHistories.First(h => h.TempId == tempId));

                    EmploymentHistories = employmentHistories;
                }
                ReloadEmployment(isCurrent, repeater);
               
                Response.Redirect("EditProfilePage.aspx");
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting RemoveEmployment - EditProfilePage.aspx");
        }

        private void MoveToPastExperience(string tempId, bool isCurrent, Repeater repeater)
        {
            LoggingManager.Debug("Entering MoveToPastExperience - EditProfilePage.aspx");
            int idToMove;
            if (int.TryParse(tempId, out idToMove))
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    int userId = Convert.ToInt32(hfUserId.Value);
                    var history = context.EmploymentHistories.First(e => e.Id == idToMove);
                    history.IsCurrent = !isCurrent;
                    context.SaveChanges();
                    var socialManager = new SocialShareManager();
                    var msg = "[UserName]" + " " + "has moved to past experience - " + history.JobTitle + " " + "in Huntable";
                    socialManager.ShareOnFacebook(userId, msg, "");
                }
            }
            LoggingManager.Debug("Exiting MoveToPastExperience - EditProfilePage.aspx");
            
        }

        private void ReloadEmployment(bool isCurrent, Repeater repeater)
        {
            LoggingManager.Debug("Entering ReloadEmployment - EditProfilePage.aspx");
            try
            {
                IEnumerable<EmploymentHistory> employmentHistories = EmploymentHistories.Where(e => e.IsCurrent == isCurrent && (e.IsDeleted == null || e.IsDeleted == false));

                if (isCurrent == false)
                {
                    employmentHistories = EmploymentHistories.Where(e => !e.IsCurrent && (e.IsDeleted == null || e.IsDeleted == false)).OrderByDescending(e=>e.Id);
                }
                LoggingManager.Info("Loading Employment Histories");

                repeater.DataSource = employmentHistories;
                repeater.DataBind();
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting ReloadEmployment - EditProfilePage.aspx");
        }


        protected void RpCurrentExperienceItemCreated(object sender, RepeaterItemEventArgs e)
        {
            //AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            //trigger.ControlID = e.Item.FindControl("imgAddPortfolio").UniqueID;
            //trigger.EventName = "Click";
            //pnl.Triggers.Add(trigger);
            //Page.Master.ScriptManager1.RegisterAsyncPostBackControl(e.Item.FindControl("imgAddPortfolio"));
        }

        protected void RpCurrentExperienceItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LoggingManager.Debug("Entering RpCurrentExperienceItemDataBound - EditProfilePage.aspx");
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    PopulateEmploymentDetails(e, true);
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting RpCurrentExperienceItemDataBound - EditProfilePage.aspx");
        }

        private void PopulateEmploymentDetails(RepeaterItemEventArgs e, bool currentEmployment)
        {
            LoggingManager.Debug("Entering PopulateEmploymentDetails - EditProfilePage.aspx");
            //try
            //{
            var history = e.Item.DataItem as EmploymentHistory;

            if (currentEmployment)
            {
                var ddlFromMonth = e.Item.FindControl("ddlFromMonth") as DropDownList;
                PopulateMothDDL(ddlFromMonth);
                if (ddlFromMonth != null)
                    if (history != null) ddlFromMonth.SelectedValue = history.FromMonthID.ToString();

                var ddlPstExpCountry = e.Item.FindControl("ddlPstExpCountry") as DropDownList;
                PopulateCountry(ddlPstExpCountry);
                if (ddlPstExpCountry != null)
                {
                    ddlPstExpCountry.Items.Insert(0, new ListItem("--Please Select--", "-1"));
                    ddlPstExpCountry.SelectedValue = history != null ? history.CountryId.ToString() : Convert.ToString(-1);
                }
                var ddlFromYear = e.Item.FindControl("ddlFromYear") as DropDownList;
                PopulateYearDDL(ddlFromYear);
                if (ddlFromYear != null) if (history != null) ddlFromYear.SelectedValue = history.FromYearID.ToString();

                var ddlToMonth = e.Item.FindControl("ddlToMonth") as DropDownList;
                PopulateMothDDL(ddlToMonth);
                if (ddlToMonth != null) if (history != null) ddlToMonth.SelectedValue = history.ToMonthID.ToString();

                var ddlToYear = e.Item.FindControl("ddlToYear") as DropDownList;
                PopulateYearDDL(ddlToYear);
                if (ddlToYear != null) if (history != null) ddlToYear.SelectedValue = history.ToYearID.ToString();

                var ddlLevel = e.Item.FindControl("ddlLevel") as DropDownList;
                PopulateLevel(ddlLevel);
                if (ddlLevel != null) if (history != null) ddlLevel.SelectedValue = history.LevelId.ToString();
            }
            else
            {
                var ddlFromMonth = e.Item.FindControl("ddlFromMonth1") as DropDownList;
                PopulateMothDDL(ddlFromMonth);
                if (ddlFromMonth != null)
                    if (history != null) ddlFromMonth.SelectedValue = history.FromMonthID.ToString();

                var ddlPstExpCountry = e.Item.FindControl("ddlPstExpCountry") as DropDownList;
                PopulateCountry(ddlPstExpCountry);
                if (ddlPstExpCountry != null)
                {
                    ddlPstExpCountry.Items.Insert(0, new ListItem("--Please Select--", "-1"));
                    ddlPstExpCountry.SelectedValue = history != null ? history.CountryId.ToString() : Convert.ToString(-1);
                }
                var ddlFromYear = e.Item.FindControl("ddlFromYear1") as DropDownList;
                PopulateYearDDL(ddlFromYear);
                if (ddlFromYear != null) if (history != null) ddlFromYear.SelectedValue = history.FromYearID.ToString();

                var ddlToMonth = e.Item.FindControl("ddlToMonth1") as DropDownList;
                PopulateMothDDL(ddlToMonth);
                if (ddlToMonth != null) if (history != null) ddlToMonth.SelectedValue = history.ToMonthID.ToString();

                var ddlToYear = e.Item.FindControl("ddlToYear1") as DropDownList;
                PopulateYearDDL(ddlToYear);
                if (ddlToYear != null) if (history != null) ddlToYear.SelectedValue = history.ToYearID.ToString();

                var ddlLevel = e.Item.FindControl("ddlLevel") as DropDownList;
                PopulateLevel(ddlLevel);
                if (ddlLevel != null) if (history != null) ddlLevel.SelectedValue = history.LevelId.ToString();
            }

            var ddlIndustry = e.Item.FindControl("ddlIndustry") as DropDownList;
            PopulateIndustryDDL(ddlIndustry);
            if (history != null && history.IndustryId.HasValue)
            {
                if (ddlIndustry != null) ddlIndustry.SelectedValue = history.IndustryId.ToString();
            }

            //var lbSkills = e.Item.FindControl("lbSkills") as ListBox;
            //PopulateSkills(lbSkills);
            //if (lbSkills != null)
            //    foreach (ListItem item in lbSkills.Items)
            //    {
            //        if (history != null)
            //            item.Selected = history.UserEmploymentSkills.Any(s => s.MasterSkillId == Convert.ToInt32(item.Value));
            //    }

            var textBox = e.Item.FindControl("txtJobTitle") as TextBox;
            if (textBox != null)
                if (history != null) textBox.Text = history.JobTitle;
            var textSkillBox = e.Item.FindControl("txtSkill") as TextBox;
            if (textSkillBox != null)
                if (history != null) textSkillBox.Text = string.Join(", ", history.UserEmploymentSkills.Select(s => s.MasterSkill.Description));
            var textPSkillBox = e.Item.FindControl("txtCSkill") as TextBox;
            if (textPSkillBox != null)
                if (history != null)
                {
                    textPSkillBox.Text = string.Join(", ", history.UserEmploymentSkills.Select(s => s.MasterSkill.Description));
                }
            var box = e.Item.FindControl("txtCompany") as TextBox;
            if (box != null)
                if (history != null) box.Text = history.MasterCompany != null ? history.MasterCompany.Description : string.Empty;
            var textBox1 = e.Item.FindControl("txtDescription") as TextBox;
            if (textBox1 != null)
                if (history != null) textBox1.Text = history.Description;
            var presentCb = e.Item.FindControl("cbPresent") as CheckBox;
            if (presentCb != null)
            {
                if (history != null)
                    presentCb.Checked = history.EmploymentPresent.HasValue && history.EmploymentPresent.Value;
            }
            //(e.Item.FindControl("cbPresent") as CheckBox).Checked = history.EmploymentPresent.HasValue && history.EmploymentPresent.Value;

            var textBox3 = e.Item.FindControl("txtTown") as TextBox;
            if (textBox3 != null)
                if (history != null) textBox3.Text = history.Town;

            // Read-only controls.
            (e.Item.FindControl("txtTitle") as TextBox).Text = history.JobTitle;
            (e.Item.FindControl("txtCurCompany") as TextBox).Text = history.MasterCompany != null ? history.MasterCompany.Description : string.Empty;
            if (!history.IsNew)
            {
                string period = string.Empty;
                if (history.MasterMonth != null && history.MasterYear1 != null)
                {
                    period = string.Format("{0} {1}", history.MasterMonth.Description, history.MasterYear1.Description);
                }
                if (history.MasterYear != null && history.MasterMonth1 != null)
                {
                    period
                     = string.Format("{0} - {1} {2}", period, history.MasterMonth1.Description, history.MasterYear.Description);
                }
                else if (!string.IsNullOrEmpty(period))
                {
                    period = string.Format("{0} - {1}", period, "Present");
                }
                (e.Item.FindControl("txtCPeriod") as TextBox).Text = period;
            }

            (e.Item.FindControl("txtCDesc") as TextBox).Text = history.Description;
            (e.Item.FindControl("txtCLevel") as TextBox).Text = history.MasterLevel != null ? history.MasterLevel.DisplayText : string.Empty;
            (e.Item.FindControl("txtCInd") as TextBox).Text = history.MasterIndustry != null ? history.MasterIndustry.Description : string.Empty;
            (e.Item.FindControl("txtCSkill") as TextBox).Text = string.Join(", ", history.UserEmploymentSkills.Select(s => s.MasterSkill.Description));
            var idField = e.Item.FindControl("id") as HiddenField;
            idField.Value = history.IsNew ? history.TempId : history.Id.ToString();
            //}
            //catch (Exception ex)
            //{
            //    LoggingManager.Error(ex);
            //}
            LoggingManager.Debug("Exiting PopulateEmploymentDetails - EditProfilePage.aspx");
        }

        private void PopulateMothDDL(DropDownList ddlFromMonth)
        {
            LoggingManager.Debug("Entering PopulateMothDDL - EditProfilePage.aspx");

            new Utility().BindDropdownList(ddlFromMonth, MasterDataManager.AllMonths.OrderBy(m => m.ID).ToList());

            LoggingManager.Debug("Exiting PopulateMothDDL - EditProfilePage.aspx");
        }

        private void PopulateCountry(DropDownList ddlPstExpCountry)
        {
            LoggingManager.Debug("Entering PopulateCountry - EditProfilePage.aspx");

            new Utility().BindDropdownList(ddlPstExpCountry, MasterDataManager.AllCountries);

            LoggingManager.Debug("Exiting PopulateCountry - EditProfilePage.aspx");
        }

        private void PopulateLevel(DropDownList ddlPstLevel)
        {
            LoggingManager.Debug("Entering PopulateLevel - EditProfilePage.aspx");

            new Utility().BindDropdownList(ddlPstLevel, MasterDataManager.AllLevels, "ID", "DisplayText");
            ddlPstLevel.Items.Insert(0, new ListItem("--Please Select--", "-1"));

            LoggingManager.Debug("Exiting PopulateLevel - EditProfilePage.aspx");
        }

        private void PopulateYearDDL(DropDownList yearddl)
        {
            LoggingManager.Debug("Entering PopulateYearDDL - EditProfilePage.aspx");

            new Utility().BindDropdownList(yearddl, MasterDataManager.AllYears.OrderByDescending(y => y.Description).ToList());

            LoggingManager.Debug("Exiting PopulateYearDDL - EditProfilePage.aspx");
        }

        private void PopulateIndustryDDL(DropDownList industryDdl)
        {

            LoggingManager.Debug("Entering PopulateIndustryDDL - EditProfilePage.aspx");

            new Utility().BindDropdownList(industryDdl, MasterDataManager.AllIndustries);

            LoggingManager.Debug("Exiting PopulateIndustryDDL - EditProfilePage.aspx");
        }

        protected void OnAddPhotoClick(object sender, EventArgs e)
        {

        }

        protected void BtnAddNewPresentEmploymentHistoryClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnAddNewPresentEmploymentHistoryClick - EditProfilePage.aspx");
            try
            {
                AddNewEmployment(true, rpCurrentExperience);
                UserManager.ProfileUpdatedOn(LoginUserId);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnAddNewPresentEmploymentHistoryClick - EditProfilePage.aspx");
        }

        private void AddNewEmployment(bool isCurrent, Repeater repeater)
        {
            LoggingManager.Debug("Entering AddNewEmployment - EditProfilePage.aspx");
            try
            {

                if (EmploymentHistories == null)
                {
                    EmploymentHistories = new List<EmploymentHistory>();
                }
                if (EmploymentHistories.Count() == 0)
                {
                    //for new users
                    EmploymentHistories.Add(new EmploymentHistory { IsCurrent = isCurrent });


                    repeater.DataSource = EmploymentHistories.Where(h => h.IsCurrent == isCurrent);
                    repeater.DataBind();

                    repeater.Items[repeater.Items.Count - 1].FindControl("tblEditMode").Visible = true;
                    repeater.Items[repeater.Items.Count - 1].FindControl("tblReadonly").Visible = false;

                    var tr = (HtmlControl)repeater.Items[repeater.Items.Count - 1].FindControl("trPortfoliosAchievementsVideos");
                    tr.Style.Add("display", "none");
                }
                else
                {
                    var lastUpdated = EmploymentHistories.Last();
                    if (lastUpdated.UserId == 0 && lastUpdated.IsCurrent == isCurrent)
                    {

                    }
                    else
                    {
                        EmploymentHistories.Add(new EmploymentHistory { IsCurrent = isCurrent });


                        repeater.DataSource = EmploymentHistories.Where(h => h.IsCurrent == isCurrent);
                        repeater.DataBind();

                        repeater.Items[repeater.Items.Count - 1].FindControl("tblEditMode").Visible = true;
                        repeater.Items[repeater.Items.Count - 1].FindControl("tblReadonly").Visible = false;

                        var tr = (HtmlControl)repeater.Items[repeater.Items.Count - 1].FindControl("trPortfoliosAchievementsVideos");
                        tr.Style.Add("display", "none");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting AddNewEmployment - EditProfilePage.aspx");
        }

        protected void BtnAddNewPastEmploymentClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnAddNewPastEmploymentClick - EditProfilePage.aspx");
            try
            {
                AddNewEmployment(true, rpPastEmployment);
                UserManager.ProfileUpdatedOn(LoginUserId);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnAddNewPastEmploymentClick - EditProfilePage.aspx");
        }

        protected void RpPastEmploymentItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LoggingManager.Debug("Entering RpPastEmploymentItemCommand - EditProfilePage.aspx");
            try
            {
                SaveEmploymentHistory(e, false, rpPastEmployment);
                UserManager.ProfileUpdatedOn(LoginUserId);

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting RpPastEmploymentItemCommand - EditProfilePage.aspx");

        }

        protected void RpPastEmploymentItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LoggingManager.Debug("Entering RpPastEmploymentItemDataBound - EditProfilePage.aspx");

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //try
                {
                    PopulateEmploymentDetails(e, false);
                }
                //catch (Exception ex)
                //{

                //    LoggingManager.Error(ex);
                //}

            }
            LoggingManager.Debug("Exiting RpPastEmploymentItemDataBound - EditProfilePage.aspx");

        }

        protected void BtnAddNewEducationClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnAddNewEducationClick - EditProfilePage.aspx");
            try
            {
                AddNewEducation(false, rpEducation);
                UserManager.ProfileUpdatedOn(LoginUserId);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnAddNewEducationClick - EditProfilePage.aspx");
        }

        private void AddNewEducation(bool isSchool, Repeater repeater)
        {
            LoggingManager.Debug("Entering AddNewEducation - EditProfilePage.aspx");
            try
            {
                if (EducationalHistories == null)
                {
                    EducationalHistories = new List<EducationHistory>();
                }
                if (EducationalHistories.Count()==0)
                {
                    EducationalHistories.Add(new EducationHistory { IsSchool = isSchool });
                    repeater.DataSource = EducationalHistories.Where(h => h.IsSchool == isSchool);
                    repeater.DataBind();

                    repeater.Items[repeater.Items.Count - 1].FindControl("tblEditMode").Visible = true;
                    repeater.Items[repeater.Items.Count - 1].FindControl("tblReadonly").Visible = false;
                }
                else
                {
                    var lastEducaion = EducationalHistories.Last();
                    if (lastEducaion.UserId == 0 && lastEducaion.IsSchool == isSchool)
                    {

                    }
                    else
                    {
                        EducationalHistories.Add(new EducationHistory { IsSchool = isSchool });
                        repeater.DataSource = EducationalHistories.Where(h => h.IsSchool == isSchool);
                        repeater.DataBind();

                        repeater.Items[repeater.Items.Count - 1].FindControl("tblEditMode").Visible = true;
                        repeater.Items[repeater.Items.Count - 1].FindControl("tblReadonly").Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting AddNewEducation - EditProfilePage.aspx");
        }

        protected void RpEducationItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LoggingManager.Debug("Entering RpEducationItemCommand - EditProfilePage.aspx");
            try
            {
                SaveEducation(e, false, rpEducation);
                UserManager.ProfileUpdatedOn(LoginUserId);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Call my function", "overlay('Details saved succesfully')", true);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting RpEducationItemCommand - EditProfilePage.aspx");
        }

        private void SaveEducation(RepeaterCommandEventArgs e, bool isSchool, Repeater repeater)
        {
            LoggingManager.Debug("Entering SaveEducation - EditProfilePage.aspx");
            //try
            //{
            switch (e.CommandName)
            {
                case "Edit":
                    e.Item.FindControl("tblReadonly").Visible = false;
                    e.Item.FindControl("tblEditMode").Visible = true;

                    var checkBox = e.Item.FindControl("cbPresent") as CheckBox;
                    if (checkBox != null)
                        checkBox.Visible = false;
                    break;

                case "Remove":
                    var hiddenField = e.Item.FindControl("id") as HiddenField;
                    if (hiddenField != null)
                    {
                        string tempId = hiddenField.Value;
                        RemoveEducation(tempId, isSchool, repeater);
                    }
                   
                    break;

                case "Save":

                    var field = e.Item.FindControl("id") as HiddenField;
                    if (field != null)
                    {
                        string id = field.Value;
                        int convertedId;
                        EducationHistory educationHistory = int.TryParse(id, out convertedId) ? EducationalHistories.First(h => h.ID == convertedId) : EducationalHistories.First(h => h.TempId == id);
                        var textBox = e.Item.FindControl("txtCollege") as TextBox;
                        if (textBox != null)
                            educationHistory.Institution = textBox.Text;
                        if (!isSchool)
                        {
                            var box = e.Item.FindControl("txtDegree") as TextBox;
                            if (box != null)
                                educationHistory.Degree = box.Text;

                        }
                        var textBox1 = e.Item.FindControl("txtDescription") as TextBox;
                        if (textBox1 != null)
                            educationHistory.Description = textBox1.Text;

                        if (isSchool)
                        {
                            var fromMonthDDl = e.Item.FindControl("ddlFromMonth3") as DropDownList;
                            if (fromMonthDDl != null && fromMonthDDl.SelectedItem != null)
                            {
                                educationHistory.FromMonthID = Convert.ToInt32(fromMonthDDl.SelectedItem.Value);
                            }

                            var ddlFromYear = e.Item.FindControl("ddlFromYear3") as DropDownList;
                            if (ddlFromYear != null && ddlFromYear.SelectedItem != null)
                            {
                                educationHistory.FromYearID = Convert.ToInt32(ddlFromYear.SelectedItem.Value);
                            }

                            var ddlToMonth = e.Item.FindControl("ddlToMonth3") as DropDownList;
                            if (ddlToMonth != null && ddlToMonth.SelectedItem != null)
                            {
                                educationHistory.ToMonthID = Convert.ToInt32(ddlToMonth.SelectedItem.Value);
                            }

                            var ddlToYear = e.Item.FindControl("ddlToYear3") as DropDownList;
                            if (ddlToYear != null && ddlToYear.SelectedItem != null)
                            {
                                educationHistory.ToYearID = Convert.ToInt32(ddlToYear.SelectedItem.Value);
                            }
                        }
                        else
                        {
                            var fromMonthDDl = e.Item.FindControl("ddlFromMonth2") as DropDownList;
                            if (fromMonthDDl != null && fromMonthDDl.SelectedItem != null)
                            {
                                educationHistory.FromMonthID = Convert.ToInt32(fromMonthDDl.SelectedItem.Value);
                            }

                            var ddlFromYear = e.Item.FindControl("ddlFromYear2") as DropDownList;
                            if (ddlFromYear != null && ddlFromYear.SelectedItem != null)
                            {
                                educationHistory.FromYearID = Convert.ToInt32(ddlFromYear.SelectedItem.Value);
                            }

                            var ddlToMonth = e.Item.FindControl("ddlToMonth2") as DropDownList;
                            if (ddlToMonth != null && ddlToMonth.SelectedItem != null)
                            {
                                educationHistory.ToMonthID = Convert.ToInt32(ddlToMonth.SelectedItem.Value);
                            }

                            var ddlToYear = e.Item.FindControl("ddlToYear2") as DropDownList;
                            if (ddlToYear != null && ddlToYear.SelectedItem != null)
                            {
                                educationHistory.ToYearID = Convert.ToInt32(ddlToYear.SelectedItem.Value);
                            }
                        }
                        int? historyId = null;
                        if (convertedId > 0)
                        {
                            historyId = convertedId;
                        }
                        educationHistory.IsSchool = isSchool;
                        UserProfileManager.SaveEducatinalHistories(educationHistory, historyId, Convert.ToInt32(hfUserId.Value));
                        ScriptManager.RegisterStartupScript(this, GetType(), "Call my function", "overlay('Details saved succesfully')", true);                       
                    }
                    int userId = Convert.ToInt32(hfUserId.Value);
                    var educationalHistories = huntableEntities.GetEntitiesWithNoLock().EducationHistories.Where(x => x.UserId == userId).ToList();
                    EducationalHistories = educationalHistories;
                    ReloadEducationalHistories(isSchool, repeater);

                    break;

                case "Cancel":
                    int id1;
                    var hiddenField1 = e.Item.FindControl("id") as HiddenField;
                    if (hiddenField1 != null && int.TryParse(hiddenField1.Value, out id1))
                    {
                        e.Item.FindControl("tblReadonly").Visible = true;
                        e.Item.FindControl("tblEditMode").Visible = false;
                    }
                    else
                    {
                        var field1 = e.Item.FindControl("id") as HiddenField;
                        if (field1 != null)
                            RemoveEducation(field1.Value, isSchool, repeater);
                    }
                    break;
            }
            UserManager.ProfileUpdatedOn(LoginUserId);
            //}
            //catch (Exception ex)
            //{
            //    LoggingManager.Error(ex);
            //}
            LoggingManager.Debug("Exiting SaveEducation - EditProfilePage.aspx");
        }



        private void RemoveEducation(string tempId, bool isSchool, Repeater repeater)
        {
            LoggingManager.Debug("Entering RemoveEducation - EditProfilePage.aspx");
            try
            {
                int idToDelete;
                if (int.TryParse(tempId, out idToDelete))
                {
                    UserProfileManager.DeleteEducationHistory(idToDelete, Convert.ToInt32(hfUserId.Value));
                    ScriptManager.RegisterStartupScript(this, GetType(), "Call my function", "overlay('Details saved succesfully')", true);                   
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        int userId = Convert.ToInt32(hfUserId.Value);
                        EducationalHistories = context.EducationHistories.Include("MasterMonth")
                            .Include("MasterYear").Include("MasterMonth1").Include("MasterYear1").Where(x => x.UserId == userId).ToList();
                    }
                }
                else
                {
                    List<EducationHistory> educationalHistories = EducationalHistories;
                    educationalHistories.Remove(educationalHistories.First(h => h.TempId == tempId));

                    EducationalHistories = educationalHistories;
                }
                ReloadEducationalHistories(isSchool, repeater);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting RemoveEducation - EditProfilePage.aspx");
        }

        protected void RpEducationItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LoggingManager.Debug("Entering RpEducationItemDataBound - EditProfilePage.aspx");
            try
            {
                PopulateEducations(e, false);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting RpEducationItemDataBound - EditProfilePage.aspx");
        }

        private void PopulateEducations(RepeaterItemEventArgs e, bool isSchool)
        {
            LoggingManager.Debug("Entering PopulateEducations - EditProfilePage.aspx");
            //try
            //{
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var history = e.Item.DataItem as EducationHistory;
                if (isSchool)
                {
                    var ddlFromMonth = e.Item.FindControl("ddlFromMonth3") as DropDownList;
                    PopulateMothDDL(ddlFromMonth);
                    if (ddlFromMonth != null)
                        if (history != null) ddlFromMonth.SelectedValue = history.FromMonthID.ToString();

                    var ddlFromYear = e.Item.FindControl("ddlFromYear3") as DropDownList;
                    PopulateYearDDL(ddlFromYear);
                    if (ddlFromYear != null)
                        if (history != null) ddlFromYear.SelectedValue = history.FromYearID.ToString();

                    var ddlToMonth = e.Item.FindControl("ddlToMonth3") as DropDownList;
                    PopulateMothDDL(ddlToMonth);
                    if (ddlToMonth != null)
                        if (history != null) ddlToMonth.SelectedValue = history.ToMonthID.ToString();

                    var ddlToYear = e.Item.FindControl("ddlToYear3") as DropDownList;
                    PopulateYearDDL(ddlToYear);
                    if (ddlToYear != null) if (history != null) ddlToYear.SelectedValue = history.ToYearID.ToString();
                }
                else
                {
                    var ddlFromMonth = e.Item.FindControl("ddlFromMonth2") as DropDownList;
                    PopulateMothDDL(ddlFromMonth);
                    if (ddlFromMonth != null)
                        if (history != null) ddlFromMonth.SelectedValue = history.FromMonthID.ToString();

                    var ddlFromYear = e.Item.FindControl("ddlFromYear2") as DropDownList;
                    PopulateYearDDL(ddlFromYear);
                    if (ddlFromYear != null)
                        if (history != null) ddlFromYear.SelectedValue = history.FromYearID.ToString();

                    var ddlToMonth = e.Item.FindControl("ddlToMonth2") as DropDownList;
                    PopulateMothDDL(ddlToMonth);
                    if (ddlToMonth != null)
                        if (history != null) ddlToMonth.SelectedValue = history.ToMonthID.ToString();

                    var ddlToYear = e.Item.FindControl("ddlToYear2") as DropDownList;
                    PopulateYearDDL(ddlToYear);
                    if (ddlToYear != null) if (history != null) ddlToYear.SelectedValue = history.ToYearID.ToString();
                }

                var checkBox = e.Item.FindControl("cbPresent") as CheckBox;
                if (checkBox != null) checkBox.Visible = true;

                var textBox = e.Item.FindControl("txtCollege") as TextBox;
                if (textBox != null)
                    if (history != null) textBox.Text = history.Institution;
                if (!isSchool)
                {
                    var box = e.Item.FindControl("txtDegree") as TextBox;
                    if (box != null)
                        if (history != null) box.Text = history.Degree;


                    // Read-only controls.                 
                    var label = e.Item.FindControl("txtDeg") as TextBox;
                    if (label != null)
                        if (history != null) label.Text = history.Degree;

                }
                var textBox1 = e.Item.FindControl("txtDescription") as TextBox;
                if (textBox1 != null)
                    if (history != null) textBox1.Text = history.Description;

                var label1 = e.Item.FindControl("txtCDesc") as TextBox;
                if (label1 != null)
                    if (history != null) label1.Text = history.Description;

                var label2 = e.Item.FindControl("txtCollegeDet") as TextBox;
                if (label2 != null)
                    if (history != null) label2.Text = history.Institution;

                if (history != null && !history.IsNew)
                {
                    if (history.MasterMonth != null && history.MasterYear != null && history.MasterMonth1 != null && history.MasterYear1 != null)
                    {
                        var label = e.Item.FindControl("txtCPeriod") as TextBox;
                        if (label != null)
                            label.Text = string.Format("{0} {1} - {2} {3}", history.MasterMonth.Description, history.MasterYear1.Description, history.MasterMonth1.Description, history.MasterYear.Description);
                    }
                }

                var idField = e.Item.FindControl("id") as HiddenField;
                if (idField != null)
                    if (history != null)
                        idField.Value = history.IsNew ? history.TempId : history.ID.ToString();
            }

            //}
            //catch (Exception ex)
            //{
            //    LoggingManager.Error(ex);
            //}
            LoggingManager.Debug("Exiting PopulateEducations - EditProfilePage.aspx");
        }

        private void ReloadEducationalHistories(bool isSchool, Repeater repeater)
        {
            LoggingManager.Debug("Entering ReloadEducationalHistories - EditProfilePage.aspx");
            try
            {

                repeater.DataSource = EducationalHistories.Where(e => e.IsSchool == isSchool).ToList();
                repeater.DataBind();
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting ReloadEducationalHistories - EditProfilePage.aspx");
        }

        protected void BtnAddNewSchoolClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnAddNewSchoolClick - EditProfilePage.aspx");
            try
            {

                AddNewEducation(true, rpSchool);
                UserManager.ProfileUpdatedOn(LoginUserId);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnAddNewSchoolClick - EditProfilePage.aspx");
        }

        protected void RpSchoolItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LoggingManager.Debug("Entering RpSchoolItemCommand - EditProfilePage.aspx");
            try
            {
                SaveEducation(e, true, rpSchool);
                UserManager.ProfileUpdatedOn(LoginUserId);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Call my function", "overlay('Details saved succesfully')", true);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting RpSchoolItemCommand - EditProfilePage.aspx");
        }

        protected void RpSchoolItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LoggingManager.Debug("Entering RpSchoolItemDataBound - EditProfilePage.aspx");
            try
            {
                PopulateEducations(e, true);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting RpSchoolItemDataBound - EditProfilePage.aspx");
        }

        protected void BtnSummarySaveClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSummarySaveClick - EditProfilePage.aspx");
            try
            {
                var userId = Common.GetLoggedInUserId(Session);
                if (userId.HasValue)
                {
                    UserManager.ProfileUpdatedOn(userId.Value);

                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var user = context.Users.First(u => u.Id == userId.Value);
                        user.Summary = txtSummary.Text;
                        user.IsProfileAvailable = cbAvailaleNow.Checked;
                        user.PositionLookingFor = txtPositionLookingFor.Text;
                        context.SaveChanges();
                        // if (DateTime.TryParse(txtDOB.Text, out dob))
                        // {
                        //     user.DOB = dob;
                        // }

                        // //decimal value = Convert.ToDecimal(txtExpSalary.Text);
                        // //user.ExpectedSalary = (value != null) ? value : 0;
                        // var checknotnull = "-1";
                        // if (ddlMin.SelectedItem.Value!=checknotnull && ddlMax.SelectedItem.Value != checknotnull && ddlCurr.SelectedItem.Value != checknotnull)
                        // {
                        //     var prefSal = new PreferredJobUserSalary();
                        //     prefSal.UserId = Convert.ToInt32(userId);
                        //     if (ddlMin.SelectedItem != null)
                        //     {
                        //         prefSal.MinSalary = Convert.ToInt32(ddlMin.SelectedItem.Value);
                        //         prefSal.MaxSalary = Convert.ToInt32(ddlMax.SelectedItem.Value);
                        //         if (ddlCurr.SelectedItem != null)
                        //             prefSal.CurrencyTypeId = Convert.ToInt32(ddlCurr.SelectedItem.Value);
                        //     }
                        //     context.PreferredJobUserSalaries.AddObject(prefSal);

                        // }

                        // user.BlogAddress = txtBlog.Text;
                        // user.WebsiteAddress = txtWebSite.Text;
                        // user.TotalExperienceInYears = Convert.ToDouble(txtExp.Text);
                        // user.HomeAddress = txtHomeAddress.Text;
                        // user.IsMarried = rbMartialStatus.SelectedIndex == 0;
                        // user.PhoneNumber = txtPhoneNumber.Text;
                        // user.IsProfileUpdated = true;
                        // user.SecondaryEmail = txtAlternameEmail.Text;
                        // user.City = txtCity.Text;
                        // //user.ExpectedSalary = decimal.TryParse(txtExpSalary.Text, out parsedValue) ? txtExpSalary.Text : (decimal?)null;
                        //if(ddlCountry.SelectedItem.Value!=checknotnull) user.CountryID = Convert.ToInt32(ddlCountry.SelectedItem.Value);

                        // if (ddlNationality.SelectedItem.Value != checknotnull)
                        // {
                        //     user.NationalityId = Convert.ToInt32(ddlNationality.SelectedItem.Value);
                        // }
                        // if (ddlExpIndustry.SelectedItem.Value != checknotnull)
                        // {
                        //     user.ExpectedIndustry = Convert.ToInt32(ddlExpIndustry.SelectedItem.Value);
                        // }



                        //if (uploadPhoto.HasFile)
                        //{
                        //    UserManager.UploadCompanyPicture(uploadPhoto, userId.Value);
                        //}

                        context.SaveChanges();
                        ScriptManager.RegisterStartupScript(this, GetType(), "Call my function", "overlay('Details saved succesfully')", true);
                        var socialManager = new SocialShareManager();
                        var msg = "[UserName]" + " " + "has updated profile in Huntable";
                        socialManager.ShareOnFacebook(userId.Value, msg, "");
                        //lblSummaryMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnSummarySaveClick - EditProfilePage.aspx");
        }



        protected void BtnSaveClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSaveClick - EditProfilePage.aspx");
            lbltext.Visible = true;
            try
            {
                var userId = Common.GetLoggedInUserId(Session);
                if (userId.HasValue)
                {
                    UserManager.ProfileUpdatedOn(userId.Value);
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var user = context.Users.First(u => u.Id == userId.Value);
                        //user.Summary = txtSummary.Text;
                        DateTime dob;
                        if (DateTime.TryParse(txtDOB.Text, out dob))
                        {
                            user.DOB = dob;
                        }

                        //decimal value = Convert.ToDecimal(txtExpSalary.Text);
                        //user.ExpectedSalary = (value != null) ? value : 0;
                        const string checknotnull = "-1";
                        if (ddlMin.SelectedItem.Value != checknotnull && ddlMax.SelectedItem.Value != checknotnull && ddlCurr.SelectedItem.Value != checknotnull)
                        {
                            var prefSal = new PreferredJobUserSalary
                                {
                                    UserId = Convert.ToInt32(userId),
                                    MinSalary = Convert.ToInt32(ddlMin.SelectedItem.Value),
                                    MaxSalary = Convert.ToInt32(ddlMax.SelectedItem.Value),
                                    CurrencyTypeId = Convert.ToInt32(ddlCurr.SelectedItem.Value)
                                };
                            context.PreferredJobUserSalaries.AddObject(prefSal);
                            context.SaveChanges();
                        }
                        //user.IsProfileAvailable = cbAvailaleNow.Checked;
                        user.BlogAddress = txtBlog.Text;
                        user.WebsiteAddress = txtWebSite.Text;
                        user.TotalExperienceInYears = Convert.ToDouble(txtExp.Text);
                        user.HomeAddress = txtHomeAddress.Text;
                        user.IsMarried = rbMartialStatus.SelectedIndex == 0;
                        user.PhoneNumber = txtPhoneNumber.Text;
                        user.IsProfileUpdated = true;
                        user.SecondaryEmail = txtAlternameEmail.Text;
                        user.City = txtCity.Text;


                        //user.ExpectedSalary = decimal.TryParse(txtExpSalary.Text, out parsedValue) ? txtExpSalary.Text : (decimal?)null;
                        if (ddlCountry.SelectedItem.Value != checknotnull)
                        {
                            user.CountryID = Convert.ToInt32(ddlCountry.SelectedItem.Value);
                        }
                        if (ddlNationality.SelectedItem != null)
                        {
                            user.NationalityId = Convert.ToInt32(ddlNationality.SelectedItem.Value);
                        }
                        if (ddlExpIndustry.SelectedItem != null)
                        {
                            user.ExpectedIndustry = Convert.ToInt32(ddlExpIndustry.SelectedItem.Value);
                        }


                        //user.PositionLookingFor = txtPositionLookingFor.Text;

                        //if (uploadPhoto.HasFile)
                        //{
                        //    UserManager.UploadCompanyPicture(uploadPhoto, userId.Value);
                        //}

                        context.SaveChanges();
                        //save for Interest, Lamguage, Skill section
                        AddInterest();
                        AddLanguage();
                        AddSkill(Common.SkillCategory.Expert);
                        AddSkill(Common.SkillCategory.Good);
                        AddSkill(Common.SkillCategory.Strong);
                        ScriptManager.RegisterStartupScript(this, GetType(), "Call my function", "overly('Details saved succesfully')", true);
                        var socialManager = new SocialShareManager();
                        var msg = "[UserName]" + " " + "has updated contact details in Huntable";
                        socialManager.ShareOnFacebook(LoginUserId, msg, "");
                        //
                        //Response.Redirect("~/ViewUserProfile.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BtnSaveClick - EditProfilePage.aspx");
        }

        //protected void AjaxFileUpload1UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        //{
        //    string filePath = "~/upload/" + e.FileName;
        //    AjaxFileUpload1.SaveAs(filePath);
        //}

        public ListControl DdlExpCountry { get; set; }

        [WebMethod]
        public static List<string> GetAutoCompleteData(string username)
        {
            LoggingManager.Debug("Entering GetAutoCompleteData - EditProfilePage.aspx");

            List<string> result;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                result = (from c in context.MasterSkills
                          //where sq SqlMethods.Like(c.Description, username + "%")
                          where c.Description.Contains("" + username + "")
                          select c.Description).ToList();
            }

            LoggingManager.Debug("Exiting GetAutoCompleteData - EditProfilePage.aspx");

            return result;
        }

        protected void AddInterestClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering AddInterestClick - EditProfilePage.aspx");

            AddInterest();
            UserManager.ProfileUpdatedOn(LoginUserId);
            LoggingManager.Debug("Exiting AddInterestClick - EditProfilePage.aspx");

        }
        private void AddInterest()
        {
            LoggingManager.Debug("Entering AddInterest - EditProfilePage.aspx");

            if (ddlInterest.SelectedValue != "-1")
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    int intid = Convert.ToInt32(ddlInterest.SelectedValue);
                    var interstid =
                        context.UserInterests.FirstOrDefault(
                            x =>
                            x.UserId == LoginUserId && x.MasterInterestId == intid);
                    if (interstid == null)
                    {
                        UserManager.AddInterestToUser(LoginUserId, Convert.ToInt32(ddlInterest.SelectedValue));
                        GetInterests();
                        ddlInterest.SelectedValue = "-1";
                    }
                    else
                    {
                        GetInterests();
                        ddlInterest.SelectedValue = "-1";
                       ScriptManager.RegisterStartupScript(this ,GetType(), "alert function", "alert('Interest already added')", true);
                    }
                }
            }

            LoggingManager.Debug("Exiting AddInterest - EditProfilePage.aspx");
        }
        //private void GetUserInterests()
        //{

        //    LoggingManager.Debug("Entering GetUserInterests - EditProfilePage.aspx");

        //    pnlInterests.Controls.Clear();
        //    using (var context = huntableEntities.GetEntitiesWithNoLock())
        //    {
        //        List<UserInterest> userInterest = context.UserInterests.Where(l => l.UserId == LoginUserId).ToList();
        //        if (userInterest.Count > 0)
        //        {
        //            pnlInterests.Controls.Add(new LiteralControl("<table width='100%' class='textbox'><tr style='vertical-align: top;'>"));
        //            foreach (var ul in userInterest)
        //            {
        //                pnlInterests.Controls.Add(new LiteralControl("<td class='textbox'><img height='38' width='40' alt='Interest' src='images/interests/" + ul.MasterInterest.Description + ".png' /><br />" + ul.MasterInterest.Description + "</td>"));
        //            }
        //            pnlInterests.Controls.Add(new LiteralControl("</tr></table>"));
        //        }
        //    }
        //    LoggingManager.Debug("Exiting GetUserInterests - EditProfilePage.aspx");

        //}
        private void GetInterests()
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var userInterests = context.UserInterests.Where(s => s.UserId == LoginUserId).ToList();
                dlintrest.DataSource = userInterests;
                dlintrest.DataBind();
            }
        }

        protected void AddLanguageClick(object sender, EventArgs e)
        {
            AddLanguage();
            UserManager.ProfileUpdatedOn(LoginUserId);
        }
        private void AddLanguage()
        {
            LoggingManager.Debug("Entering AddLanguage - EditProfilePage.aspx");

            if (txtLanguageKnown.Value.Trim().IndexOf("e.g: Search...", StringComparison.Ordinal) >= 0) return;
            var ratingSelected = GetLanguageRating();
            if (!String.IsNullOrEmpty(hdnLanguageSelected.Value))
            {
                UserManager.AddLanguageToUser(LoginUserId, Convert.ToInt32(hdnLanguageSelected.Value), ratingSelected);
            }
            else
            {
                UserManager.AddLanguageToUser(LoginUserId, txtLanguageKnown.Value, ratingSelected);
            }
            txtLanguageKnown.Value = "e.g: Search...";
            hdnLanguageSelected.Value = "";
            //GetUserLanguages();
            Display();

            LoggingManager.Debug("Exiting AddLanguage - EditProfilePage.aspx");

        }
        private int? GetLanguageRating()
        {
            LoggingManager.Debug("Entering GetLanguageRating - EditProfilePage.aspx");

            if (rdLanguageRatingBasic.Checked) return 1;
            if (rdLanguageRatingIntermediate.Checked) return 2;
            if (rdLanguageRatingFluent.Checked) return 3;
            if (rdLanguageRatingExcellent.Checked) return 4;
            if (rdLanguageRatingNative.Checked) return 5;

            LoggingManager.Debug("Exiting GetLanguageRating - EditProfilePage.aspx");

            return null;
        }
        //private void GetUserLanguages()
        //{
        //    LoggingManager.Debug("Entering GetUserLanguages - EditProfilePage.aspx");

        //    pnlLanguages.Controls.Clear();
        //    using (var context = huntableEntities.GetEntitiesWithNoLock())
        //    {
        //        pnlLanguages.Controls.Add(new LiteralControl("<table width='100%'>"));
        //        List<UserLanguage> userlanguages = context.UserLanguages.Where(l => l.UserId == LoginUserId).ToList();
        //        foreach (var ul in userlanguages)
        //        {
        //            pnlLanguages.Controls.Add(new LiteralControl("<tr><td width='50%'>" + ul.MasterLanguage.Description + "</td><td width='50%'><img src='images/rating-star" + ul.Level + ".png' /></td></tr>"));
        //        }
        //        pnlLanguages.Controls.Add(new LiteralControl("</table>"));
        //    }

        //    LoggingManager.Debug("Exiting GetUserLanguages - EditProfilePage.aspx");
        //}
        protected void AddExpertSkillClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering AddExpertSkillClick - EditProfilePage.aspx");

            AddSkill(Common.SkillCategory.Expert);
            UserManager.ProfileUpdatedOn(LoginUserId);

            LoggingManager.Debug("Exiting AddExpertSkillClick - EditProfilePage.aspx");

        }
        protected void AddGoodSkillClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering AddGoodSkillClick - EditProfilePage.aspx");

            AddSkill(Common.SkillCategory.Good);
            UserManager.ProfileUpdatedOn(LoginUserId);

            LoggingManager.Debug("Exiting AddGoodSkillClick - EditProfilePage.aspx");


        }
        protected void AddStrongSkillClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering AddStrongSkillClick - EditProfilePage.aspx");
            AddSkill(Common.SkillCategory.Strong);
            UserManager.ProfileUpdatedOn(LoginUserId);


            LoggingManager.Debug("Exiting AddStrongSkillClick - EditProfilePage.aspx");
        }
        private void AddSkill(Common.SkillCategory skillCategory)
        {
            LoggingManager.Debug("Entering AddSkill - EditProfilePage.aspx");

            HtmlInputHidden masterSkillIdToSave = null;
            HtmlInputText skillToSave = null;
            string skillHowLongToSave = null;

            if (skillCategory == Common.SkillCategory.Expert) { masterSkillIdToSave = null; skillToSave = txtExpertSkill; skillHowLongToSave = ddlExpertSkillHowLong.SelectedValue; }
            if (skillCategory == Common.SkillCategory.Good) { masterSkillIdToSave = null; skillToSave = txtGoodSkill; skillHowLongToSave = ddlSkillGoodHowLong.SelectedValue; }
            if (skillCategory == Common.SkillCategory.Strong) { masterSkillIdToSave = null; skillToSave = txtStrongSkill; skillHowLongToSave = ddlSkillHowLongYears.SelectedValue; }
            if (skillToSave != null && skillToSave.Value.Trim().IndexOf("e.g: Search...") >= 0) return;

            if (masterSkillIdToSave != null && !String.IsNullOrEmpty(masterSkillIdToSave.Value))
            {
                if (ddlSkillHowLongYears != null)
                    UserManager.AddSkillToUser(LoginUserId, Convert.ToInt32(masterSkillIdToSave.Value), (int)skillCategory, skillHowLongToSave);
            }
            else
            {
               
                using (var context= huntableEntities.GetEntitiesWithNoLock())
                {
                    //checking duplication
                    int skilltosave =
                        context.MasterSkills.Where(s => s.Description.ToLower() == skillToSave.Value)
                               .Select(s => s.Id)
                               .FirstOrDefault();
                    var duplicateskill =
                        context.UserSkills.FirstOrDefault(
                            s =>
                            s.UserId == LoginUserId && s.MasterSkillId == skilltosave &&
                            s.SkillCategory == (int) skillCategory && s.HowLong == skillHowLongToSave);
                    
                    
                    if (duplicateskill==null)
                    {
                        if (skillHowLongToSave != null)
                            if (skillToSave != null)
                                UserManager.AddSkillToUser(LoginUserId, skillToSave.Value, (int)skillCategory, skillHowLongToSave);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "AskQuestionsClick", "alert('Skill already exist');", true);
                        
                    }

                }
            }
            if (skillToSave != null) skillToSave.Value = "e.g: Search...";
            if (masterSkillIdToSave != null) masterSkillIdToSave.Value = "";
            GetUserSkills();

            LoggingManager.Debug("Exiting AddSkill - EditProfilePage.aspx");
        }
        private void GetUserSkills()
        {
            LoggingManager.Debug("Entering GetUserSkills - EditProfilePage.aspx");

           // pnlExpertSkill.Controls.Clear();
           // pnlStrongSkill.Controls.Clear();
           // pnlGoodSkill.Controls.Clear();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                List<UserSkill> userexpertskills = context.UserSkills.Include("MasterSkill").Where(l => l.UserId == LoginUserId && l.SkillCategory == (int)Common.SkillCategory.Expert).ToList();
                List<UserSkill> usergoodskills = context.UserSkills.Where(l => l.UserId == LoginUserId && l.SkillCategory == (int)Common.SkillCategory.Good).ToList();
                List<UserSkill> userstrongskills = context.UserSkills.Where(l => l.UserId == LoginUserId && l.SkillCategory == (int)Common.SkillCategory.Strong).ToList();
                var expertSkills = from us in context.UserSkills
                                   where
                                       us.UserId == LoginUserId && us.SkillCategory == (int) Common.SkillCategory.Expert
                                   select new
                                       {
                                           us.MasterSkill.Description,
                                           howlong = (us.HowLong == "1") ? us.HowLong + " year" : us.HowLong + " years",
                                           us.Id
                                       };
                var goodSkills = from us in context.UserSkills
                                 where
                                     us.UserId == LoginUserId && us.SkillCategory == (int)Common.SkillCategory.Good
                                 select new
                                 {
                                     us.MasterSkill.Description,
                                     howlong = (us.HowLong == "1") ? us.HowLong + " year" : us.HowLong + " years",
                                     us.Id
                                 };
                var strongSkills = from us in context.UserSkills
                                 where
                                     us.UserId == LoginUserId && us.SkillCategory == (int)Common.SkillCategory.Strong
                                 select new
                                 {
                                     us.MasterSkill.Description,
                                     howlong = (us.HowLong == "1") ? us.HowLong + " year" : us.HowLong + " years",
                                     us.Id
                                 };
                
                dlExpertSkillDisplay.DataSource = expertSkills;
                dlExpertSkillDisplay.DataBind();
                dlStrongSkill.DataSource = strongSkills;
                dlStrongSkill.DataBind();
                dlGoodSkills.DataSource = goodSkills;
                dlGoodSkills.DataBind();

                //pnlExpertSkill.Controls.Add(new LiteralControl("<table width='100%'>"));
                //foreach (var ul in userexpertskills)
                //{
                //    if (ul.HowLong == "1")
                //    {
                //        pnlExpertSkill.Controls.Add(new LiteralControl("<tr><td width='50%'>" + ul.MasterSkill.Description + "- </td><td width='50%'>" + ul.HowLong + " Year</td></tr>"));
                //    }
                //    else
                //    {
                //        pnlExpertSkill.Controls.Add(new LiteralControl("<tr><td width='50%'>" + ul.MasterSkill.Description + "- </td><td width='50%'>" + ul.HowLong + " Years</td></tr>"));
                //    }
                //}
                //pnlExpertSkill.Controls.Add(new LiteralControl("</table>"));
                //pnlGoodSkill.Controls.Add(new LiteralControl("<table width='100%'>"));
                //foreach (var ul in usergoodskills)
                //{
                //    if (ul.HowLong == "1")
                //    {
                //        pnlGoodSkill.Controls.Add(new LiteralControl("<tr><td width='50%'>" + ul.MasterSkill.Description + "- </td><td width='50%'>" + ul.HowLong + " Year</td></tr>"));
                //    }
                //    else
                //    {
                //        pnlGoodSkill.Controls.Add(new LiteralControl("<tr><td width='50%'>" + ul.MasterSkill.Description + "- </td><td width='50%'>" + ul.HowLong + " Years</td></tr>"));
                //    }
                //}
                //pnlGoodSkill.Controls.Add(new LiteralControl("</table>"));
                //pnlStrongSkill.Controls.Add(new LiteralControl("<table width='100%'>"));
                //foreach (var ul in userstrongskills)
                //{
                //    if (ul.HowLong == "1")
                //    {
                //        pnlStrongSkill.Controls.Add(new LiteralControl("<tr><td width='50%'>" + ul.MasterSkill.Description + "- </td><td width='50%'>" + ul.HowLong + " Year</td></tr>"));
                //    }
                //    else
                //    {
                //        pnlStrongSkill.Controls.Add(new LiteralControl("<tr><td width='50%'>" + ul.MasterSkill.Description + "- </td><td width='50%'>" + ul.HowLong + " Years</td></tr>"));
                //    }
                //}
                //pnlStrongSkill.Controls.Add(new LiteralControl("</table>"));
            }
            LoggingManager.Debug("Exiting GetUserSkills - EditProfilePage.aspx");

        }
        protected void BtnDeleteExpertClick(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            if (button != null)
            {
                int skillId = Convert.ToInt32(button.CommandArgument);
                using (var context= huntableEntities.GetEntitiesWithNoLock())
                {
                    var skilltobedeleted = context.UserSkills.FirstOrDefault(s => s.Id == skillId);
                    context.DeleteObject(skilltobedeleted);
                    context.SaveChanges();
                    context.AcceptAllChanges();
                }
                GetUserSkills();
            }
        }

        protected void BtnprofileClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnprofileClick - ProfileUploadPage.aspx");
            try
            {
                string dirName = Path.GetTempPath();
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
                string fileName = dirName + "\\" + uploadResume.FileName;
                uploadResume.SaveAs(fileName);

                var context = huntableEntities.GetEntitiesWithNoLock();

                var userId = Common.GetLoggedInUserId(Session);

                if (userId != null)
                {
                    string extension = Path.GetExtension(uploadResume.PostedFile.FileName);
                    if (extension == ".doc" || extension == ".docx" || extension == ".txt" ||
                        extension == ".rtf" || extension == ".pdf")
                    {
                        new PostCVManager().ImportResume(context, userId.Value, fileName);

                        //lblUploadResumeMessage.Text = "Resume uploaded successfully.";
                        //lblUploadResumeMessage.ForeColor = Color.Green;
                        ScriptManager.RegisterStartupScript(this, GetType(), "Call my function", "showDiv()", true);
                    }
                }
            }
            catch (Exception ex)
            {
                //lblUploadResumeMessage.Text = "Error occurred while uploading the message.";
                //lblUploadResumeMessage.ForeColor = Color.Red;
                //LoggingManager.Error(ex);
                //lblUploadResumeMessage.Text = ex.Message;
            }
            LoggingManager.Debug("Exiting BtnprofileClick - ProfileUploadPage.aspx");
        }
        private void LoadProfilePercentCompleted()
        {
            //LoggingManager.Debug("Entering LoadProfilePercentCompleted - ProfileUploadPage.aspx");

            var userId = Common.GetLoggedInUserId(Session);
            if (userId.HasValue)
            {
                var percentCompleted = UserManager.GetProfilePercentCompleted(userId.Value);

                if (percentCompleted > 60)
                {
                    uploadResume.Visible = false;
                    btnUploadProfile.Visible = false;

                }
                //lblPercentCompleted.Text = (percentCompleted / 100).ToString("00%");
                //int value = Convert.ToInt32(percentCompleted);
                ////ProgressBar1.Value = value;
                //ProgressBar2.Value = value;
                //LoggingManager.Info("Percent Completed:" + lblPercentCompleted.Text);
            }

        }
    }
}