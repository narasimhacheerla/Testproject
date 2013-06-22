using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.Logging;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI.UserControls
{
    public partial class InfosysEmployees : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (compId.HasValue)
            {
               
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var cmpnyname = context.Companies.FirstOrDefault(x => x.Id == compId);
                    lbl.Text = cmpnyname.CompanyName;
                }
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    try
                    {
                        var mcmpny = (from c in context.Companies
                                      join d in context.MasterCompanies on c.CompanyName equals d.Description
                                      where c.Id == compId
                                      select d.Id).Single();

                        var mcp = Int32.Parse(mcmpny.ToString());
                        var emplyuser = from a in context.EmploymentHistories
                                        join b in context.Users on a.UserId equals b.Id
                                        where a.CompanyId == mcp && a.UserId != LoginUserId
                                        select new
                                        {
                                            b.FirstName,
                                            a.JobTitle,
                                            b.PersonalLogoFileStoreId,
                                            b.Password,
                                            b.Id

                                        };
                        // var emply = context.EmploymentHistories.Where(x => x.CompanyId == compId);
                        dl.DataSource = emplyuser.Distinct().Take(4);
                        dl.DataBind();
                    }
                    catch (Exception ex)
                    {
                        LoggingManager.Error(ex);
                    }
                }
                
            }
            hdnUserId.Value = Session[SessionNames.LoggedInUserId] == null ? string.Empty : Session[SessionNames.LoggedInUserId].ToString();

        }
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - CompaniesFollowers");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - CompaniesFollowers");

                return 0;
            }
        }
        private int? compId
        {
            get
            {
                int otherUserId;
                if (int.TryParse(Request.QueryString["Id"], out otherUserId))
                {
                    return otherUserId;
                }
                return null;
            }
        }
        public string Picture(object id)
        {
            if (id != null)
            {
                int p = Int32.Parse(id.ToString());
                return new FileStoreService().GetDownloadUrl(p);
            }
            else
            {
                return new FileStoreService().GetDownloadUrl(null);
            }

        }

        protected void Follow(object sender, EventArgs e)
        {
            var p = sender as Button;
            if (p != null)
            {
                int flwngid = Int32.Parse(p.CommandArgument);
                var loginuserid = Common.GetLoggedInUserId(Session);
                UserManager.FollowUser(loginuserid.Value, flwngid);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('You are now following')", true);
            }
        }
        public bool IsThisUserFollowingCompany(object useridFollowedByCompany)
        {
            LoggingManager.Debug("Entering IsThisUserFollowingCompany - CompanyView");



            //return UserManager.FollowingUser(LoginUserId, Convert.ToInt32(useridFollowedByCompany)).Any();
            var user_to = Convert.ToInt32(useridFollowedByCompany);
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                //var res=context.PreferredFeedUserUsers.Where(x => x.UserId == loginUserId && x.FollowingUserId == user_to).FirstOrDefault();
                var result = context.PreferredFeedUserUsers.Where(y => y.UserId == user_to && y.FollowingUserId == LoginUserId).ToList();

                if (result.Count > 0)
                    return true;
                else
                    return false;

            }


        }
        protected void CommandCompanyEmployeeUnFollowClick(object sender, EventArgs e)
        {
            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);

                var btnCompanyEmployeeFollow = sender as LinkButton;
                if (btnCompanyEmployeeFollow != null)
                {
                    int companyEmployeeUserId = Convert.ToInt32(btnCompanyEmployeeFollow.CommandArgument);
                }

              
                    var usrmgr = new UserManager();

                    if (loginUserId != null)
                    {
                        usrmgr.Unfollow(Convert.ToInt32(btnCompanyEmployeeFollow.CommandArgument), loginUserId.Value);
                        using (var context = huntableEntities.GetEntitiesWithNoLock())
                        {
                           
                                var mcmpny = (from c in context.Companies
                                              join d in context.MasterCompanies on c.CompanyName equals d.Description
                                              where c.Id == compId
                                              select d.Id).Single();

                                var mcp = Int32.Parse(mcmpny.ToString());
                                var emplyuser = from a in context.EmploymentHistories
                                                join b in context.Users on a.UserId equals b.Id
                                                where a.CompanyId == mcp && a.UserId != LoginUserId
                                                select new
                                                {
                                                    b.FirstName,
                                                    a.JobTitle,
                                                    b.PersonalLogoFileStoreId,
                                                    b.Password,
                                                    b.Id

                                                };
                                // var emply = context.EmploymentHistories.Where(x => x.CompanyId == compId);
                                dl.DataSource = emplyuser.Distinct().Take(4);
                                dl.DataBind();


                                Page.ClientScript.RegisterStartupScript( this.GetType(), "Call my function", "overlay('Succesfully Unfollowed')", true);
                           
                           
                        }
                        

                    }
                
            }
            catch (Exception)
            {
                { }
                throw;
            }
          
        }

        protected void CommandCompanyEmployeeFollowClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering CommandCompanyEmployeeFollow_Click - CompanyView");
            try
            {
                int? loginUserId = Common.GetLoggedInUserId(Session);

                var btnCompanyEmployeeFollow = sender as LinkButton;
                if (btnCompanyEmployeeFollow != null)
                {
                    int companyEmployeeUserId = Convert.ToInt32(btnCompanyEmployeeFollow.CommandArgument);
                }

               
                    if (loginUserId != null)
                    {
                        UserManager.FollowUser(loginUserId.Value, Convert.ToInt32(btnCompanyEmployeeFollow.CommandArgument));
                        using (var context = huntableEntities.GetEntitiesWithNoLock())
                        {

                            var mcmpny = (from c in context.Companies
                                          join d in context.MasterCompanies on c.CompanyName equals d.Description
                                          where c.Id == compId
                                          select d.Id).Single();

                            var mcp = Int32.Parse(mcmpny.ToString());
                            var emplyuser = from a in context.EmploymentHistories
                                            join b in context.Users on a.UserId equals b.Id
                                            where a.CompanyId == mcp && a.UserId != LoginUserId
                                            select new
                                            {
                                                b.FirstName,
                                                a.JobTitle,
                                                b.PersonalLogoFileStoreId,
                                                b.Password,
                                                b.Id

                                            };
                            // var emply = context.EmploymentHistories.Where(x => x.CompanyId == compId);
                            dl.DataSource = emplyuser.Distinct().Take(4);
                            dl.DataBind();


                           

                        }
                      
                        Page.ClientScript.RegisterStartupScript( this.GetType(), "Call my function", "overlay('You are now following')", true);
                    }
                
            }
            catch (Exception)
            {
                { }
                throw;
            }

            LoggingManager.Debug("Exiting CommandCompanyEmployeeFollow_Click - CompanyView");
        }
        public string UrlGenerator(object id)
        {
            if ((id != null))
            {
                int jobid = Convert.ToInt32(id.ToString());
                return new UrlGenerator().UserUrlGenerator(jobid);
            }
            else
            {
                return null;
            }

        }
        protected void click_seemore(object sender, EventArgs e)
        {
            if (Hidden_Field.Value == string.Empty)
                Hidden_Field.Value = "3";
            Hidden_Field.Value = (Convert.ToInt32(Hidden_Field.Value) + 3).ToString();
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                try
                {
                    var mcmpny = (from c in context.Companies
                                  join d in context.MasterCompanies on c.CompanyName equals d.Description
                                  where c.Id == compId
                                  select d.Id).Single();

                    var mcp = Int32.Parse(mcmpny.ToString());
                    var emplyuser = from a in context.EmploymentHistories
                                    join b in context.Users on a.UserId equals b.Id
                                    where a.CompanyId == mcp && a.UserId != LoginUserId
                                    select new
                                    {
                                        b.FirstName,
                                        a.JobTitle,
                                        b.PersonalLogoFileStoreId,
                                        b.Password,
                                        b.Id

                                    };
                    // var emply = context.EmploymentHistories.Where(x => x.CompanyId == compId);
                    dl.DataSource = emplyuser.Distinct().ToList();
                    dl.DataBind();
                }
                catch (Exception ex)
                {
                    LoggingManager.Error(ex);
                }
            }
            btn_more.Visible = false;

        }


        
    }
}