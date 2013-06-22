using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Huntable.Data.UI;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;
using System.Web.UI.HtmlControls;
using System.Data;

namespace Huntable.UI
{
    public partial class Following : System.Web.UI.Page
    {
        public int LoginUserId
        {
            get
            {
                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;
                return 0;
            }
        }
        private int? userId
        {
            get
            {
                LoggingManager.Debug("Entering compId - Following");
                int otherUserId;
                if (int.TryParse(Request.QueryString["UserId"], out otherUserId))
                {
                    return otherUserId;
                }
                LoggingManager.Debug("Exiting compId - Following");
                return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - Following.aspx");
            //var userId = Common.GetLoggedInUserId(Session);
            if (LoginUserId == 0) { pplYouMayKnowDiv.Visible = false; }
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var getfollowerid = 0;
                string username ="";
                if (userId != null)
                {
                    getfollowerid = (int)userId;
                }
                else
                {
                    getfollowerid = (int)LoginUserId;
                }
                follower.HRef = "~/Followers.aspx?UserId=" + getfollowerid;
                following.HRef = "~/Following.aspx?UserId=" + getfollowerid;
                var usr = context.Users.FirstOrDefault(x => x.Id == getfollowerid);
               
                username = usr.FirstName;
                
                lblname.Text = username;
                var userresult = new UserManager();
                {
                    var resulkt = userresult.GetUserfollower((int)getfollowerid);
                    var resultfollowing = userresult.GetUserFollowings((int)getfollowerid);

                    string s = resulkt != null ? lblFollowers.Text = resulkt.Count.ToString(CultureInfo.InvariantCulture) : lblFollowers.Text = "0";
                    string k = resultfollowing != null
                                   ? lblfollowing.Text = resultfollowing.Count.ToString(CultureInfo.InvariantCulture)
                                   : lblfollowing.Text = "0";
                    dlFollowing.DataSource = resultfollowing;
                    dlFollowing.DataBind();
                }
                var usri = context.Users.FirstOrDefault(x => x.Id == LoginUserId);
                if (usri != null && usri.IsPremiumAccount == null)
                {
                    bimage.Visible = true;
                    pimage.Visible = false;
                }
                else
                {
                    bimage.Visible = false;
                    pimage.Visible = true;
                }
            }
            if (Common.GetLoggedInUserId(Session) == null)
            {
                bimage.Visible = true;
                pimage.Visible = false;
            }
            LoggingManager.Debug("Exiting Page_Load - Following.aspx");
        }

        public string Picture(object id)
        {

            LoggingManager.Debug("Entering Picture - Following.aspx");
            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Users.FirstOrDefault(x => x.Id == p);
               
                    var photo = result.PersonalLogoFileStoreId;

                    LoggingManager.Debug("Exiting Picture - Following.aspx");
                    return new FileStoreService().GetDownloadUrl(photo);
                
            }
            
        }
        protected void UnFollowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UnFollowClick - Following.aspx");
            var button = sender as LinkButton;
            if (button != null)
            {
                int userid = Convert.ToInt32(button.CommandArgument);
                var usrmgr = new UserManager();
                usrmgr.Unfollow(userid, LoginUserId);
                var getfollowerid = 0;

                if (userId != null)
                {
                    getfollowerid = (int)userId;
                }
                else
                {
                    getfollowerid = (int)LoginUserId;
                }
                var userresult = new UserManager();
                {
                    var resultfollowing = userresult.GetUserFollowings((int)getfollowerid);
                    if (resultfollowing != null)
                    {
                        dlFollowing.DataSource = resultfollowing;
                        dlFollowing.DataBind();
                    }
                }
                //update.Update();
                //Response.Redirect("following.aspx");
                ScriptManager.RegisterStartupScript(this ,this.GetType(), "Call my function", "overlay('Unfollowed succesfully')", true);
            }
            LoggingManager.Debug("Exiting UnFollowClick - Following.aspx");
        }
        protected void FollowupClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering FollowupClick - Following.aspx");
            var button = sender as LinkButton;
            if (button != null)
            {
                int userId = Convert.ToInt32(button.CommandArgument);
                UserManager.FollowUser(LoginUserId, userId);

                var objMessageManager = new UserMessageManager();
                objMessageManager.FollowUser(userId, LoginUserId);
            }
            LoggingManager.Debug("Exiting FollowupClick - Following.aspx");
        }
        protected void itembound(object sender, DataListItemEventArgs e)
        {
            LoggingManager.Debug("Entering itembound - Following.aspx");
            HtmlAnchor a1 = (HtmlAnchor)e.Item.FindControl("A1");
            HtmlAnchor a2 = (HtmlAnchor)e.Item.FindControl("A2");
            Control div1 = (Control)e.Item.FindControl("dv");
            Control div2 = (Control)e.Item.FindControl("undv");
            if (e.Item.DataItem != null)
            {
                int strUsername = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "Id").ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var usr = context.Users.FirstOrDefault(x => x.Id == strUsername);
                    if (usr.IsDeleted == null)
                    {
                        if (usr.IsCompany == null)
                        {
                            a1.HRef = new UrlGenerator().UserUrlGenerator(usr.Id);
                            a2.HRef = new UrlGenerator().UserUrlGenerator(usr.Id);
                        }
                        else
                        {
                            var cmpny = context.Companies.FirstOrDefault(x => x.Userid == usr.Id);
                            if (cmpny != null)
                            {
                                a1.HRef = new UrlGenerator().CompanyUrlGenerator(cmpny.Id);
                                a2.HRef = new UrlGenerator().CompanyUrlGenerator(cmpny.Id);
                            }
                        }

                    }
                }
                if (userId.HasValue && LoginUserId != userId.Value)
                {

                    div1.Visible = false;
                    div2.Visible = true;
                    
                }


            }
            LoggingManager.Debug("Exiting itembound - Following.aspx");
        }
        public bool IsThisUserFollowing(object useridFollowedByCompany)
        {
            LoggingManager.Debug("Entering IsThisUserFollowingCompany - Following");



            //return UserManager.FollowingUser(LoginUserId, Convert.ToInt32(useridFollowedByCompany)).Any();
            var userTo = Convert.ToInt32(useridFollowedByCompany);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //var res=context.PreferredFeedUserUsers.Where(x => x.UserId == loginUserId && x.FollowingUserId == user_to).FirstOrDefault();
                var result = context.PreferredFeedUserUsers.Where(y => y.UserId == userTo && y.FollowingUserId == LoginUserId).ToList();



                if (result.Count > 0)
                    return true;
                else
                    LoggingManager.Debug("Exiting IsThisUserFollowingCompany - Following");
                    return false;

            }


        }

    }
}