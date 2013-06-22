using System.Linq;
using Huntable.Data.Enums;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.Business
{
    public class EmailTemplateManager
    {
        public static EmailTemplate GetTemplate(EmailTemplates template)
        {
            LoggingManager.Debug("Entering GetTemplate  - EmailTemplateManager.cs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var templateId = (int) template;

                LoggingManager.Debug("Exiting GetTemplate  - EmailTemplateManager.cs");

                return context.EmailTemplates.First(t => t.Id == templateId);
            }
        }

        public EmailTemplate GetTemplate(string templateName)
        {
            LoggingManager.Debug("Entering GetTemplate  - EmailTemplateManager.cs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var template = context.EmailTemplates.Single(data => data.Name.ToUpper() == templateName.ToUpper());

                LoggingManager.Debug("Exiting GetTemplate  - EmailTemplateManager.cs");
                return template;
            }
        }
    }
}
