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

namespace Huntable.UI
{
    public partial class PeopleYouCanFollw : System.Web.UI.Page
    {
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - PeopleYouCanFollw");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - PeopleYouCanFollw");

                return 0;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - PeopleYouCanFollw");
            if (!IsPostBack)
            {
                GetMutualFollowersList();
            }
           
            using (var context= huntableEntities.GetEntitiesWithNoLock())
            {
                var usri = context.Users.FirstOrDefault(x => x.Id == LoginUserId);
                if (usri != null && usri.IsPremiumAccount == null)
                {
                    bimage.Visible = true;
                    pimage.Visible = false;
                }
                if (usri != null && usri.IsPremiumAccount != null)
                {
                    bimage.Visible = false;
                    pimage.Visible = true;
                }
                if (usri==null)
                {
                    bimage.Visible = true;
                }
            }
            LoggingManager.Debug("Exiting Page_Load - PeopleYouCanFollw");
        }
        public void GetMutualFollowersList()
        {
            LoggingManager.Debug("Entering GetUsers - PeopleYouCanFollw");
            if (LoginUserId != 0)
            {

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var mutualFollowersCount =
                        context.MasterPeoples.Where(s => s.UserId == LoginUserId && s.IsMutualFollowersFollower == false)
                               .ToList();
                    var mutualFollowersFollowersCount =
                        context.MasterPeoples.Where(s => s.UserId == LoginUserId && s.IsMutualFollowersFollower == true)
                               .ToList();

                    List<MasterPeople> mutualFollowersList = new List<MasterPeople>();
                    if (mutualFollowersCount.Count()>0)
                    {
                        mutualFollowersList =
                            context.MasterPeoples.Where(
                                s => s.UserId == LoginUserId && s.IsMutualFollowersFollower == false).ToList();
                    }
                    else if(mutualFollowersFollowersCount.Count()>0)
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
                        //GetRandomUsersForNewUsers();// if mutual followers and mutual followers followers becomes zero
                        return;
                    }
                    else
                    {
                        dlPeopleYouMayKnow.DataSource = distinctMutualUsersList.ToList();
                        dlPeopleYouMayKnow.DataBind();
                    }
                }
            }

            LoggingManager.Debug("Exiting GetUsers - PeopleYouCanFollw");
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
                var aaa = distincttopHundredUsersAsMutualFollowers.Count();
                dlPeopleYouMayKnow.DataSource = distincttopHundredUsersAsMutualFollowers.OrderByDescending(s=>s.CreatedDateTime);
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
            LoggingManager.Debug("Entering IsThisUserFollowingCompany - PeopleYouCanFollw");



            //return UserManager.FollowingUser(LoginUserId, Convert.ToInt32(useridFollowedByCompany)).Any();
            var userTo = Convert.ToInt32(useridFollowedByCompany);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //var res=context.PreferredFeedUserUsers.Where(x => x.UserId == loginUserId && x.FollowingUserId == user_to).FirstOrDefault();
                var result = context.PreferredFeedUserUsers.Where(y => y.UserId == userTo && y.FollowingUserId == LoginUserId).ToList();



                if (result.Count > 0)
                    return true;
                else
                    LoggingManager.Debug("Exiting IsThisUserFollowingCompany - PeopleYouCanFollw");
                    return false;

            }


        }
        protected void FollowupClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering FollowupClick - PeopleYouCanFollw");
            var button = sender as LinkButton;
            if (button != null)
            {
                if (button.Text.Contains("Follow +"))
                {
                    int userId = Convert.ToInt32(button.CommandArgument);
                    UserManager.FollowUser(LoginUserId, userId);

                    var objMessageManager = new UserMessageManager();
                    objMessageManager.FollowUser(userId, LoginUserId);
                   // button.Text = "Following";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Call my function",
                                                        "overlay('You are now following')", true);
                }
            }
            GetMutualFollowersList();
           LoggingManager.Debug("Exiting FollowupClick - PeopleYouCanFollw");
        }
        protected void UnFollowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UnFollowClick - PeopleYouCanFollw.aspx");
            var button = sender as LinkButton;
            if (button != null)
            {
                int userid = Convert.ToInt32(button.CommandArgument);
                var usrmgr = new UserManager();
                usrmgr.Unfollow(userid, LoginUserId);
                            }
            LoggingManager.Debug("Exiting UnFollowClick - PeopleYouCanFollw.aspx");
        }
        protected void itembound(object sender, DataListItemEventArgs e)
        {
            LoggingManager.Debug("Entering itembound - PeopleYouCanFollw.aspx");
           // HtmlAnchor a1 = (HtmlAnchor)e.Item.FindControl("A1");
           // HtmlAnchor a2 = (HtmlAnchor)e.Item.FindControl("A2");
            //Control div1 = (Control)e.Item.FindControl("dv");
            //Control div2 = (Control)e.Item.FindControl("undv");
            if (e.Item.DataItem != null)
            {
                int strUsername = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "Id").ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var usr = context.Users.FirstOrDefault(x => x.Id == strUsername);
                    if (usr.IsCompany == null)
                    {
                       // a1.HRef = new UrlGenerator().UserUrlGenerator(strUsername);
                       // a2.HRef = new UrlGenerator().UserUrlGenerator(usr.Id);
                    }
                    else
                    {
                        var cmpny = context.Companies.FirstOrDefault(x => x.Userid == usr.Id);
                        //a1.HRef = new UrlGenerator().CompanyUrlGenerator(cmpny.Id);
                        //a2.HRef = new UrlGenerator().CompanyUrlGenerator(cmpny.Id);
                    }

                }
                //if (userId.HasValue && LoginUserId != userId.Value)
                //{

                //    div1.Visible = false;
                //    div2.Visible = true;

                //}


            }
            LoggingManager.Debug("Exiting itembound - PeopleYouCanFollw.aspx");
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