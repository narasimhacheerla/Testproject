using System;
using System.Linq;
using System.Web.UI;
using Snovaspace.Util.Logging;
using Huntable.Business;
using Huntable.Data;

namespace Huntable.UI
{
    public partial class ChangeEmailSettings : Page
    {
        public int LoginUserId
        {
            
            get
            {
                LoggingManager.Debug("Entering LoginUserId - ChangeEmailSettings");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - ChangeEmailSettings");
                return 0;
               
            }
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - ChangeEmailSettings");
            try
            {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var logins =
                        context.Logins.FirstOrDefault(u => u.UserId == LoginUserId || u.CompanyId == LoginUserId);
                if (logins != null)
                {
                    lblemail.Text = logins.EmailAddress;
                }
            }
            
                //bool userLoggedIn = Common.IsLoggedIn();
                //var user = Common.GetLoggedInUser();


                //if (userLoggedIn)
                //{
                //    lblemail.Text = user.EmailAddress;
                //    _email = user.EmailAddress;
                //}
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting Page_Load - ChangeEmailSettings");
        }

        protected void BtnEmailClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnEmailClick - ChangeEmailSettings");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var usrmngr = new UserMessageManager();
                if (txtNewEmail.Text != string.Empty)
                {

                    var logins =
                        context.Logins.FirstOrDefault(u => u.UserId == LoginUserId || u.CompanyId == LoginUserId);

                    if (logins != null)
                    {
                        logins.EmailAddress = txtNewEmail.Text;
                        if(logins.IsUser==true)
                        {
                            var user = context.Users.FirstOrDefault(x => x.Id == LoginUserId);
                            if (user != null) user.EmailAddress = txtNewEmail.Text;
                            context.SaveChanges();
                        }
                        else if(logins.IsCompany==true)
                        {
                            var company = context.Companies.FirstOrDefault(x => x.Userid == LoginUserId);
                            if (company != null) company.CompanyEmail = txtNewEmail.Text;
                            context.SaveChanges();
                        }
                    }
                    context.SaveChanges();
                    usrmngr.NewEmailAddress(LoginUserId);
                    Page.ClientScript.RegisterStartupScript(GetType(), "Call my function", "overlay('E-mail changed succesfully')", true);
                    if (logins != null) lblemail.Text = logins.EmailAddress;
                    txtNewEmail.Text = "";
                }

            }
            LoggingManager.Debug("Exiting BtnEmailClick - ChangeEmailSettings");

        }

        protected void BtnPwdChangeClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnPwdChangeClick - ChangeEmailSettings");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var usrmngr = new UserMessageManager();
                 var code = Guid.NewGuid();
                var logins =
                      context.Logins.FirstOrDefault(u => u.UserId == LoginUserId || u.CompanyId == LoginUserId);
                if (logins != null && txtOld.Text == logins.Password)
                {
                    //logins.Password = txtNewCnfrm.Text;
                    if (logins.IsUser == true)
                    {
                        var user = context.Users.FirstOrDefault(x => x.Id == LoginUserId);
                        if (user != null)
                        {
                            user.TempPwd = txtNew.Text;
                            user.Code = code;
                             
                        }
                        context.SaveChanges();
                        //lblpwd.Text = "Password Changed Successfully";
                        li_pwd.Visible = true;
                    }
                    else if (logins.IsCompany == true)
                    {
                        var company = context.Companies.FirstOrDefault(x => x.Userid == LoginUserId);
                        var user = context.Users.FirstOrDefault(x => x.Id == LoginUserId);
                        if (user != null)
                        {
                            user.TempPwd = txtNew.Text;
                            user.Code = code;
                           
                        }
                        if (company != null) company.Password = txtNew.Text;
                        context.SaveChanges();
                        
                        //lblpwd.Text = "Password Changed Successfully";
                        li_pwd.Visible = true;
                      
                    }
                    usrmngr.SendPassworToUser(LoginUserId, txtNew.Text, code);
                    context.SaveChanges();
                    ScriptManager.RegisterStartupScript(this ,GetType(), "Call my function", "overlay('Password Changed Successfully')", true);
                }
                else
                {
                    lblmesg.Text = "Old Password you entered is Wrong";
                    li_msg.Visible = true;
                   
                }

                LoggingManager.Debug("Exiting BtnPwdChangeClick - ChangeEmailSettings");
            }
        }
    }
}