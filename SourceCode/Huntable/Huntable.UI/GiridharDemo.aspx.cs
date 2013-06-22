using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Data;
using Huntable.Entities;
using Snovaspace.Util.Logging;    

namespace Huntable.UI
{
    public partial class GiridharDemo : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - GiridharDemo.aspx");
             try
             {

                 if (!IsPostBack)
                 {
                     //followers binding 
                     rspFollowers.DataSource = BindFollowers();
                     rspFollowers.DataBind();

                 }
             }
             catch (Exception ex)
             {
                 LoggingManager.Error(ex);   
             }
           
        }

    
        /// <summary>
        /// Fetch the list of followers for the logged in user
        /// </summary>
        /// <returns>list of followers</returns>
        private List<User> BindFollowers()
        {
            List<User> followersData = new List<User>();
            User userData = new User();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                var user = Business.Common.GetLoggedInUser(context);
                lblUserName.Text = user.Name;
                List<PreferredFeedUserUser> followers = context.PreferredFeedUserUsers.Where(h => h.UserId == user.Id ).ToList();
                foreach (PreferredFeedUserUser follower in followers)
                {
                    userData = context.Users.Where(x => x.Id == follower.FollowingUserId).FirstOrDefault();
                    followersData.Add(new User
                    {
                        FirstName = userData.FirstName,
                        City = userData.City,
                        CurrentCompany = userData.CurrentCompany
                    });    
                }
            }

            lblFollowersCount.Text  = Convert.ToString(followersData.Count);
            return followersData;
        }

     
    }


}