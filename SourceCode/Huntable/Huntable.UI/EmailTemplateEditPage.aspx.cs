using System;
using System.Linq;
using System.Web.UI;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI.Admin
{
    public partial class EmailTemplateEditPage : Page
    {

        private int? TemplateIdToEdit
        {
            get
            {
                if (ViewState["TemplateId"] == null)
                {
                    return null;
                }
                return Convert.ToInt32(ViewState["TemplateId"]);
            }
            set
            {
                ViewState["TemplateId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - EmailTemplateEditPage.aspx");

            if (!IsPostBack)
            {
                LoadTemplates();
            }

            LoggingManager.Debug("Exiting Page_Load - EmailTemplateEditPage.aspx");
        }


        private void LoadTemplates()
        {
            LoggingManager.Debug("Entering LoadTemplates - EmailTemplateEditPage.aspx");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var templates = context.EmailTemplates.Where(t => t.IsActive).Select(t => new { t.Name, t.Id }).ToList();
                templates.Insert(0, new { Name = "--Select Template--", Id = -1 });

                ddlEmailTemplate.DataSource = templates;
                ddlEmailTemplate.DataTextField = "Name";
                ddlEmailTemplate.DataValueField = "Id";
                ddlEmailTemplate.DataBind();
            }

            LoggingManager.Debug("Exiting LoadTemplates - EmailTemplateEditPage.aspx"); 
        }

        protected void DDLEmailTemplateSelectedIndexChanged(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering DDLEmailTemplateSelectedIndexChanged - EmailTemplateEditPage.aspx");

            if (ddlEmailTemplate.SelectedIndex == 0)
            {
                ckTemplate.Text = "";
                TemplateIdToEdit = null;
            }
            else
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    TemplateIdToEdit = Convert.ToInt32(ddlEmailTemplate.SelectedItem.Value);
                    ckTemplate.Text = context.EmailTemplates.First(t => t.Id == TemplateIdToEdit.Value).TemplateText;
                }
            }
            LoggingManager.Debug("Exiting DDLEmailTemplateSelectedIndexChanged - EmailTemplateEditPage.aspx");
        }

        protected void BtnOkClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnOkClick - EmailTemplateEditPage.aspx");

            try
            {
                if (TemplateIdToEdit.HasValue)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        context.EmailTemplates.First(t => t.Id == TemplateIdToEdit.Value).TemplateText = ckTemplate.Text;
                        context.SaveChanges();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "alert('Current Template updated successfully.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
            LoggingManager.Debug("Exiting BtnOkClick - EmailTemplateEditPage.aspx");

        }
    }
}