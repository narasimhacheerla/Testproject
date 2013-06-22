using Huntable.Business.BatchJobs;
using Snovaspace.Util.Logging;

namespace Huntable.BatchJobs
{
    public class Program
    {
        static void Main(string[] args)
        {
            //new FeaturedRecruiters().Run();
            //new PeopleYouMayKnowUpdate().Run();
            //new JobsUserConnectionsUpdate().Run();
            //new FeedsUserConnectionsUpdate().Run();

            if (args.Length == 0) new JobFeeds().Run();
           
                if (args[0] == "JobsUserConnectionsUpdate")
                {
                   
                    new JobsUserConnectionsUpdate().Run();
                }
           
            if (args[0] == "EmailInvites")
            {
               
                new EmailInvites().Run();
               
            }
            if (args[0] == "JobsRemember")
            {
                LoggingManager.Debug("JobsRemember feeds entering");
                new JobRemember().Run();
                LoggingManager.Debug("JobsRemember feeds exiting");
            }
            if (args[0] == "RememberEmail")
            {
                LoggingManager.Debug("RememberEmail feeds entering");
                new RememberEmail().Run();
                LoggingManager.Debug("RememberEmail feeds exiting");
            }
            if (args[0] == "JobFeeds")
            {
                LoggingManager.Debug("JobFeeds feeds entering");
                new JobFeeds().Run();
                LoggingManager.Debug("JobFeeds feeds exiting");
            }
            if (args[0] == "FeaturedRecuirters")
            {
                LoggingManager.Debug("FeaturedRecuirters feeds entering");
                new FeaturedRecruiters().Run();
                LoggingManager.Debug("FeaturedRecuirters feeds exiting");
            }
            if (args[0] == "ResendInvitations")
            {
                LoggingManager.Debug("ResendInvitations feeds entering");
                new ReSendInvitations().Run();
                LoggingManager.Debug("ResendInvitations feeds exiting");
            }
            if (args[0] == "PeopleYouMayKnowUpdate")
            {
                LoggingManager.Debug("PeopleYouMayKnowUpdate feeds entering");
                new PeopleYouMayKnowUpdate().Run();
                LoggingManager.Debug("PeopleYouMayKnowUpdate feeds exiting");
            }
            if (args[0] == "JobsUserConnectionsUpdate")
            {
                LoggingManager.Debug("JobsUserConnectionsUpdate feeds entering");
                new JobsUserConnectionsUpdate().Run();
                LoggingManager.Debug("JobsUserConnectionsUpdate feeds exiting");
            }
            if (args[0] == "FeedsUserConnectionsUpdate")
            {
                LoggingManager.Debug("FeedsUserConnectionsUpdate feeds entering");
                new FeedsUserConnectionsUpdate().Run();
                LoggingManager.Debug("FeedsUserConnectionsUpdate feeds exiting");
            }
            if (args[0] == "SiteMap")
            {
                LoggingManager.Debug("sitemap feeds entering");
                new Sitemap().Run();
                LoggingManager.Debug("sitemap feeds exiting");
            }
            if (args[0] == "MutualFollower")
            {
                LoggingManager.Debug("MutualFollower");
                new MutualFollowers().LoadMutualUsers();
                LoggingManager.Debug("MutualFOllowers exiting");
            }
        }
    }
}
