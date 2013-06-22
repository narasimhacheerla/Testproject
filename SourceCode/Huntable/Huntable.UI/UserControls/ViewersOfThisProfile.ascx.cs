using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI.UserControls
{
    public partial class ViewersOfThisProfile : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var loginUserId = Common.GetLoggedInUserId(Session);

            if (loginUserId == null || loginUserId <= 0)
            {
                Visible = false;
                return;
            }

            var jm = new InvitationManager();

            if (OtherUserId.HasValue)
            {
                var result = jm.GetUserProfileVisitedHistory(OtherUserId.Value, 3);
                if (result.Count != 0)
                {
                    var result1 = from a in result
                                  select new
                                  {
                                      a.ID,
                                      a.Jobtitle,
                                      a.MasterCompany,
                                      a.Name
                                  };
                    rspview.DataSource = result1;
                    rspview.DataBind();
                }
                else
                {
                    lblMessage.Text = string.Format("You Are The First Person To View This Profile", "ARG0");
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;

                }
            }
            else
            {

                var result = jm.GetUserProfileVisitedHistory(loginUserId.Value, 3);
                if (result != null)
                {
                    if (result.Count != 0)
                    {
                        var result1 = from a in result
                                      select new
                                          {
                                              a.ID,
                                              a.Jobtitle,
                                              a.MasterCompany,
                                              a.Name
                                          };
                        rspview.DataSource = result1;
                        rspview.DataBind();
                    }
                    else
                    {
                        lblMessage.Text = string.Format("No Data {0}Available", "ARG0");
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                }
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