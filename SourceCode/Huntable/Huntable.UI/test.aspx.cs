using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data.Enums;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;

namespace Huntable.UI
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - test.aspx");
            var upgradeTemplate = EmailTemplateManager.GetTemplate(EmailTemplates.Upgrade);
            var upgradeList = new Hashtable
                                                  {
                                                      {"Name","Manoj"},
                                                      {"FirstInvited",0},
                                                      {"FirstJoined",0},
                                                      {"FirstEarnings",0},
                                                      {"SecondInvited",0},
                                                      {"SecondJoined",0},
                                                      {"SecondEarnings",0},
                                                      {"ThirdInvited",0},
                                                      {"ThirdJoined",0},
                                                      {"ThirdEarnings",0}
                                                  };
           
            string upgradeBody = SnovaUtil.LoadTemplate(upgradeTemplate.TemplateText, upgradeList);
            SnovaUtil.SendEmail(upgradeTemplate.Subject, upgradeBody, "sivajayanth.46@gmail.com");
            LoggingManager.Debug("Exiting Page_Load - test.aspx");
        }
    }
}