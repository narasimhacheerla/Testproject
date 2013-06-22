using System;
using System.Linq;
using Huntable.Data;

namespace Huntable.UI.UserControls
{
    public partial class ProfileCompletionSteps : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userId = Business.Common.GetLoggedInUserId(Session);
                if (userId.HasValue)
                {
                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var user = context.Users.First(u => u.Id == userId);
                       
                        if (!string.IsNullOrWhiteSpace(user.Summary))
                        {
                            liSummary.Visible = false;
                        }
                        if (user.EmploymentHistories.Any(h => h.IsCurrent == true))
                        {
                            liCurrentEmp.Visible = false;
                        }
                        if (user.EmploymentHistories.Any(h => h.IsCurrent == false))
                        {
                            liPastExp.Visible = false;
                        }
                        if (user.EducationHistories.Any(h => h.IsSchool))
                        {
                            liSchool.Visible = false;
                        }
                        if (user.EducationHistories.Any(h => h.IsSchool == false))
                        {
                            liEduHistroy.Visible = false;
                        }
                        if (user.UserInterests.Any())
                        {
                            liIntrests.Visible = false;
                        }
                    }
                }
            }
        }
    }
}