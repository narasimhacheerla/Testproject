using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;

namespace Huntable.UI.UserControls
{
    public partial class PeopleYouMayKnowHorizontalDisplay : System.Web.UI.UserControl
    {
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - CompanyView");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - CompanyView");

                return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }
        public void LoadUsers()
        {
            
            GetMutualFollowersList();
        }

        public void GetMutualFollowersList()
        {
            if (LoginUserId != 0)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var mutualFollowersCount =
                        context.MasterPeoples.Where(s => s.UserId == LoginUserId && s.IsMutualFollowersFollower == false)
                               .ToList();

                    List<MasterPeople> mutualFollowersList = new List<MasterPeople>();
                    if (mutualFollowersCount.Count() > 0)
                    {
                        mutualFollowersList =
                            context.MasterPeoples.Where(
                                s => s.UserId == LoginUserId && s.IsMutualFollowersFollower == false).ToList();
                    }
                    else
                    {
                        mutualFollowersList =
                            context.MasterPeoples.Where(
                                s => s.UserId == LoginUserId && s.IsMutualFollowersFollower == true).ToList();
                    }
                    IEnumerable<User> mutualUsersList = from mp in mutualFollowersList
                                                        join u in context.Users on mp.MutualFollowerId equals u.Id
                                                        where mp.MutualFollowerId != LoginUserId
                                                        select u;
                    var mutualUsersDataToBeDisplayed = from u in mutualUsersList
                                                       join eh in context.EmploymentHistories.Include("MasterCompany")
                                                           on
                                                           u.Id equals eh.UserId
                                                       orderby eh.User.CreatedDateTime descending
                                                       select new
                                                           {
                                                               u.Id,
                                                               PersonalLogoFileStoreId = u.PersonalLogoFileStoreId ?? 1,
                                                               FirstName = u.FirstName ?? string.Empty,
                                                               City = u.City ?? string.Empty,
                                                               JobTitle = eh.JobTitle ?? string.Empty,
                                                               Description =
                                                           eh.MasterCompany != null &&
                                                           (eh.MasterCompany.Description == null)
                                                               ? string.Empty
                                                               : eh.Description
                                                           };
                    var distinctMutualUsersList = from a in mutualUsersDataToBeDisplayed
                                                  group a by a.Id
                                                  into b
                                                  select b.FirstOrDefault();
                    if (!distinctMutualUsersList.Any())
                    {
                        //GetRandomUsersForNewUsers();
                    }
                    else
                    {
                        var followersCount = distinctMutualUsersList.Count();
                        var rand = new System.Random();
                        var nextRandomMutualFollowers = distinctMutualUsersList.Skip(rand.Next(followersCount)).Take(3);
                        dlPeopleYouMayKnow.DataSource = nextRandomMutualFollowers;
                        dlPeopleYouMayKnow.DataBind();
                    }

                }
            }
        }
        public void GetRandomUsersForNewUsers()
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var topHundredUsers =
                    context.Users.Where(s => s.Id != LoginUserId).OrderByDescending(s => s.CreatedDateTime).Take(100);
                var topHundredUsersAsMutualFollowers = from eachtopHundredUsers in topHundredUsers
                                                       join eh in context.EmploymentHistories.Include("MasterCompany")
                                                           on eachtopHundredUsers.Id equals eh.UserId
                                                       select new
                                                       {
                                                           eachtopHundredUsers.CreatedDateTime,
                                                           eachtopHundredUsers.Id,
                                                           PersonalLogoFileStoreId = eachtopHundredUsers.PersonalLogoFileStoreId ?? 1,
                                                           FirstName = eachtopHundredUsers.FirstName ?? string.Empty,
                                                           City = eachtopHundredUsers.City ?? string.Empty,
                                                           JobTitle = eh.JobTitle ?? string.Empty,

                                                       };
                var distincttopHundredUsersAsMutualFollowers = from a in topHundredUsersAsMutualFollowers
                                                               group a by a.Id
                                                                   into b
                                                                   select b.FirstOrDefault();

                var distincttopHundredUsersAsMutualFollowersList =
                    distincttopHundredUsersAsMutualFollowers.OrderBy(s => s.Id);
                var followersCount = distincttopHundredUsersAsMutualFollowersList.Count();
                var rand = new System.Random();
                var nextRandomMutualFollowers = distincttopHundredUsersAsMutualFollowersList.Skip(rand.Next(followersCount)).Take(3);
                dlPeopleYouMayKnow.DataSource = nextRandomMutualFollowers;
                dlPeopleYouMayKnow.DataBind();


            }
        }
        public string Picture(object id)
        {

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Users.FirstOrDefault(x => x.Id == p);
                var photo = result.PersonalLogoFileStoreId;
                return new FileStoreService().GetDownloadUrl(photo);
            }

        }

        

        public bool IsThisUserFollowing(object useridFollowedByCompany)
        {
            LoggingManager.Debug("Entering IsThisUserFollowingCompany - CompanyView");



            //return UserManager.FollowingUser(LoginUserId, Convert.ToInt32(useridFollowedByCompany)).Any();
            var userTo = Convert.ToInt32(useridFollowedByCompany);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //var res=context.PreferredFeedUserUsers.Where(x => x.UserId == loginUserId && x.FollowingUserId == user_to).FirstOrDefault();
                var result = context.PreferredFeedUserUsers.Where(y => y.UserId == userTo && y.FollowingUserId == LoginUserId).ToList();



                if (result.Count > 0)
                    return true;
                else
                    return false;

            }


        }
        protected void FollowupClick(object sender, EventArgs e)
        {

            var button = sender as LinkButton;
            if (button != null)
            {
                if (button.Text.Contains("Follow +"))
                {

                    int userId = Convert.ToInt32(button.CommandArgument);
                    UserManager.FollowUser(LoginUserId, userId);

                    var objMessageManager = new UserMessageManager();
                    objMessageManager.FollowUser(userId, LoginUserId);
                    button.Text = "Following";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Call my function", "overlay('You are now following')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Call my function",
                                                        "overlay('You are now following')", true);
                }
            }

            GetMutualFollowersList();
        }

        protected void RefreshClick(object sender, EventArgs e)
        {
            if (LoginUserId != 0)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var mutualFollowersCount =
                        context.MasterPeoples.Where(s => s.UserId == LoginUserId && s.IsMutualFollowersFollower == false)
                               .ToList();

                    List<MasterPeople> mutualFollowersList = new List<MasterPeople>();
                    if (mutualFollowersCount.Count() > 0)
                    {
                        mutualFollowersList =
                            context.MasterPeoples.Where(
                                s => s.UserId == LoginUserId && s.IsMutualFollowersFollower == false).ToList();
                    }
                    else
                    {
                        mutualFollowersList =
                            context.MasterPeoples.Where(
                                s => s.UserId == LoginUserId && s.IsMutualFollowersFollower == true).ToList();
                    }

                    IEnumerable<User> mutualUsersList = from mp in mutualFollowersList
                                                        join u in context.Users on mp.MutualFollowerId equals u.Id
                                                        where mp.MutualFollowerId != LoginUserId
                                                        select u;
                    var mutualUsersDataToBeDisplayed = from u in mutualUsersList
                                                       join eh in context.EmploymentHistories.Include("MasterCompany")
                                                           on
                                                           u.Id equals eh.UserId
                                                       orderby eh.User.CreatedDateTime descending
                                                       select new
                                                       {
                                                           u.Id,
                                                           PersonalLogoFileStoreId = u.PersonalLogoFileStoreId ?? 1,
                                                           FirstName = u.FirstName ?? string.Empty,
                                                           City = u.City ?? string.Empty,
                                                           JobTitle = eh.JobTitle ?? string.Empty,
                                                           Description =
                                                       eh.MasterCompany != null &&
                                                       (eh.MasterCompany.Description == null)
                                                           ? string.Empty
                                                           : eh.Description
                                                       };
                    var distinctMutualUsersList = from a in mutualUsersDataToBeDisplayed
                                                  group a by a.Id
                                                      into b
                                                      select b.FirstOrDefault();
                    if (!distinctMutualUsersList.Any())
                    {
                        GetRandomUsersForNewUsers();
                    }
                    else
                    {
                        var followersCount = distinctMutualUsersList.Count();
                        var rand = new System.Random();
                        var nextRandomMutualFollowers = distinctMutualUsersList.Skip(rand.Next(followersCount)).Take(3);
                        dlPeopleYouMayKnow.DataSource = nextRandomMutualFollowers;
                        dlPeopleYouMayKnow.DataBind();
                    }
                }
            }
        }
        public string UrlGenerator(object id)
        {
            if ((id != null))
            {
                int userid = Convert.ToInt32(id.ToString());
                return "~/" + new UrlGenerator().UserUrlGenerator(userid);
            }
            else
            {
                return null;
            }

        }
    }
}