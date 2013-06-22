using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Snovaspace.Util.Logging;
using Huntable.Business;
using Huntable.Data;

namespace Huntable.UI
{
    public partial class Likes : System.Web.UI.Page
    {

        private int? UserId
        {
            get
            {
                int userId;
                if (Request.QueryString["UserId"] != null)
                {
                    if (int.TryParse(Request.QueryString["UserId"], out userId))
                        return userId;
                    else return null;
                }
                else if (Session[SessionNames.LoggedInUserId] != null)
                {
                    if (int.TryParse(Session[SessionNames.LoggedInUserId].ToString(), out userId))
                        return userId;
                    else return null;
                }
                else
                    return null;
            }
        }
        private int? OtherUserId
        {
            get
            {
                int otherUserId;
                if (int.TryParse(Request.QueryString["UserId"], out otherUserId))
                {
                    return otherUserId;
                }
                if (Page.RouteData.Values["ID"] != null && (Page.RouteData.Values["ID"]).ToString() != "ShareMail.aspx" && (Page.RouteData.Values["ID"]).ToString() != "ChartImg.axd")
                {
                    string id = (Page.RouteData.Values["ID"]).ToString();
                    string[] words = id.Split('-');
                    int k = words.Length;
                    string userid = words[k - 1];
                    return Convert.ToInt32(userid);
                }



                return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - Likes.aspx");
            hdnprofileUserId.Value = Convert.ToString(UserId);
            var loggedInUserId = Common.GetLoggedInUserId(Session);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                if (OtherUserId.HasValue)
                {
                    var usri = context.Users.FirstOrDefault(x => x.Id == OtherUserId.Value);
                    lblName.Text = usri.Name;
                }
               
                if (loggedInUserId != null)
                {


                    var usri = context.Users.FirstOrDefault(x => x.Id == loggedInUserId);
                    if (usri != null)
                    {
                        
                        if (usri.IsPremiumAccount == null)
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
                }
                else
                {
                    bimage.Visible = true;
                    pimage.Visible = false;
                }
            }
            LoggingManager.Debug("Exiting Page_Load - Likes.aspx");
        }
    }
}