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
    public partial class Followers : System.Web.UI.Page
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
                LoggingManager.Debug("Entering compId -  Followers");
                int otherUserId;
                if (int.TryParse(Request.QueryString["UserId"], out otherUserId))
                {
                    return otherUserId;
                }
                LoggingManager.Debug("Exiting compId -  Followers");
                return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - Followers.aspx");
            //var userId = Common.GetLoggedInUserId(Session);
            if (LoginUserId == 0) { pplYouMayKnowDiv.Visible = false; }
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var getfollowerid=0;
                if(userId!=null)
                {
                    getfollowerid= (int) userId;
                }
                else
                {
                    getfollowerid = (int) LoginUserId;
                }
                followers.HRef = "~/Followers.aspx?UserId=" + getfollowerid;
                following.HRef = "~/Following.aspx?UserId=" + getfollowerid;
                var usr = context.Users.FirstOrDefault(x => x.Id == getfollowerid);

               string username = usr.FirstName;

                lblname.Text = username;
                var userresult = new UserManager();
                {
                    var resulkt = userresult.GetUserfollower((int)getfollowerid);
                    var resultfollowing = userresult.GetUserFollowings((int)getfollowerid);
                    
                   

                    string s = resulkt != null ? lblFollowers.Text = resulkt.Count.ToString(CultureInfo.InvariantCulture) : lblFollowers.Text = "0";
                    string k = resultfollowing != null
                                   ? lblfollowing.Text = resultfollowing.Count.ToString(CultureInfo.InvariantCulture)
                                   : lblfollowing.Text = "0";
                    dlFollowers.DataSource = resulkt;
                    dlFollowers.DataBind();
                    //dlFollowing.DataSource = resultfollowing;
                    //dlFollowing.DataBind();
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
            LoggingManager.Debug("Exiting Page_Load - Followers.aspx");
        }
        public string Picture(object id)
        {
            LoggingManager.Debug("Entering Picture - Followers.aspx");

            int p = Int32.Parse(id.ToString());
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var result = context.Users.FirstOrDefault(x => x.Id == p);

                if (result != null)
                {
                    var photo = result.PersonalLogoFileStoreId;

                    LoggingManager.Debug("Exiting Picture - Followers.aspx");
                    return new FileStoreService().GetDownloadUrl(photo);
                }
            }

            return null;
        }
        protected void FollowupClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering FollowupClick - Followers.aspx");
            var button = sender as LinkButton;
            if (button != null)
            {
                int id = Convert.ToInt32(button.CommandArgument);
                UserManager.FollowUser(LoginUserId, id);

                var objMessageManager = new UserMessageManager();
                objMessageManager.FollowUser(id, LoginUserId);
            }

            LoggingManager.Debug("Exiting FollowupClick - Followers.aspx");
        }
        protected void itembound(object sender, DataListItemEventArgs e)
        {
            LoggingManager.Debug("Entering itembound - Followers.aspx");
            HtmlAnchor a1 = (HtmlAnchor)e.Item.FindControl("A1");
            HtmlAnchor a2 = (HtmlAnchor)e.Item.FindControl("A2");
            Button b1 = (Button) e.Item.FindControl("btnblock");
            Button b2 = (Button)e.Item.FindControl("btnunblock");

            if (e.Item.DataItem != null)
            {
                int strUsername = Int32.Parse(DataBinder.Eval(e.Item.DataItem, "Id").ToString());
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var usr = context.Users.FirstOrDefault(x => x.Id == strUsername);
                    var blckuser =
                        context.UserBlockLists.FirstOrDefault(x => x.UserId == LoginUserId && x.BlockedUserId == usr.Id);
                    if (userId == null || userId == LoginUserId)
                    {
                        if (blckuser != null)
                        {
                            b1.Visible = false;
                            b2.Visible = true;
                        }
                        if (blckuser == null)
                        {
                            b1.Visible = true;
                            b2.Visible = false;
                        }
                    }
                    if (userId != null && userId != LoginUserId)
                    {
                        b1.Visible = false;
                        b2.Visible = false;
                    }
                    if (usr != null && usr.IsCompany == null)
                    {
                        a1.HRef =  new UrlGenerator().UserUrlGenerator(usr.Id);
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
            LoggingManager.Debug("Exiting itembound - Followers.aspx");

        }
        protected void Blockuser(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Blockuser - Followers.aspx");
            var button = sender as Button;
            if (button!= null)
            {
                int id = Convert.ToInt32(button.CommandArgument);
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    //var blockuser = new UserBlockList {UserId = LoginUserId, BlockedUserId = id};
                    //context.AddToUserBlockLists(blockuser);
                    //context.SaveChanges();
                    var objMessageManager = new UserMessageManager();
                    objMessageManager.BlockUser(context, Convert.ToInt32(Common.GetLoggedInUserId(Session)), id);
                }
                Response.Redirect("Followers.aspx");

            }
            LoggingManager.Debug("Exiting Blockuser - Followers.aspx");
        }

        protected void UnBlockuser(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UnBlockuser - Followers.aspx");
            var button = sender as Button;
            if (button != null)
            {
                int id = Convert.ToInt32(button.CommandArgument);
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var blockuser =
                        context.UserBlockLists.FirstOrDefault(x => x.UserId == LoginUserId && x.BlockedUserId == id);
                    context.DeleteObject(blockuser);
                    context.SaveChanges();
                }
                Response.Redirect("Followers.aspx");

            }
            LoggingManager.Debug("Exiting UnBlockuser - Followers.aspx");
        }






    }
}