using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util;
using Snovaspace.Util.Logging;

namespace Huntable.UI.UserControls
{
    public partial class HeaderBeforeLoggingIn : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - HeaderBeforeLoggingIn.ascx");
            if (!IsPostBack)
            {
                lblSignInError.Visible = false;
                txtPassword.Attributes.Add("value", "Password");
                if (Request.Cookies["myCookie"] != null)
                {
                    HttpCookie cookie = Request.Cookies.Get("myCookie");
                    txtEmail.Value = cookie.Values["username"];
                    txtPassword.Text = cookie.Values["password"];
                    chkRemember.Checked = true;
                }

            }

            LoggingManager.Debug("Exiting Page_Load - HeaderBeforeLoggingIn.ascx");
        }



        protected void BtnSignInClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSignInClick - HeaderBeforeLoggingIn.ascx");

            if (!string.IsNullOrEmpty(txtEmail.Value) || !string.IsNullOrEmpty(txtPassword.Text))
            {
                var myCookie = new HttpCookie("myCookie");
                bool isRemember = chkRemember.Checked;

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var login =
                        context.Logins.FirstOrDefault(
                            u => u.EmailAddress.ToLower() == txtEmail.Value.ToLower() && u.Password == txtPassword.Text);
                    //var user = context.Users.FirstOrDefault(u => u.EmailAddress.ToLower() == txtEmail.Value.ToLower() && u.Password == txtPassword.Text);
                    string strRedirect = string.Empty;
                    if(login !=null)
                    {
                        if (login.IsVerified == true&&(login.IsDeleted==null||login.IsDeleted==false))
                        {
                            if (isRemember)
                            {
                                myCookie.Values.Add("username", txtEmail.Value);
                                myCookie.Values.Add("password", txtPassword.Text);
                                myCookie.Expires = DateTime.Now.AddDays(15);
                            }
                            else
                            {

                                myCookie.Values.Add("username", string.Empty);
                                myCookie.Values.Add("password", string.Empty);
                                myCookie.Expires = DateTime.Now.AddMinutes(-1);

                            }
                                           Response.Cookies.Add(myCookie);
                                           LoggingManager.Debug(string.Format("User login success for {0}.", login.EmailAddress));        
                                           
                            if (login.IsUser == true)
                            {
                                Session["Datetm"] = hdndt.Value;
                                Session[SessionNames.LoggedInUserId] = login.UserId;
                                Session[SessionNames.UserEmailAddress] = login.EmailAddress;
                                var user1 = context.Users.FirstOrDefault(x => x.Id == login.UserId);
                                user1.LastLoginTime = DateTime.Now;
                                context.SaveChanges();
                                Session[SessionNames.LoggedInUserId] = user1.Id;
                                var tkt = new FormsAuthenticationTicket(1, user1.EmailAddress, DateTime.Now,
                                                                                              DateTime.Now.AddMinutes(30), false, "");
                                string cookiestr = FormsAuthentication.Encrypt(tkt);
                                var ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr) { Path = FormsAuthentication.FormsCookiePath };

                                Response.Cookies.Add(ck);
                                if ((!user1.IsCustomizingYourFeedsAccepted.HasValue || !user1.IsCustomizingYourFeedsAccepted.Value))
                                {
                                    strRedirect = "~/HuntableTourFeatures.aspx";
                                }
                                else if ((!user1.IsCustomizingYourJobsAccepted.HasValue || !user1.IsCustomizingYourJobsAccepted.Value))
                                {
                                    strRedirect = "~/HuntableTourCustomizeFeeds.aspx";
                                }
                                else if ((!user1.IsSearchingAndConnectingWithOtherUsersAccepted.HasValue || !user1.IsSearchingAndConnectingWithOtherUsersAccepted.Value))
                                {
                                    strRedirect = "~/HuntableTourCustomizeJobs.aspx";
                                }
                                else if (!user1.IsProfileUpdated)
                                {
                                    strRedirect = "~/ProfileUploadPage.aspx";
                                }
                                else
                                {
                                    strRedirect = "~/HomePageAfterLoggingIn.aspx";
                                }
                            }
                            else if(login.IsCompany == true)
                            {
                                Session["Datetm"] = hdndt.Value;
                                var tkt = new FormsAuthenticationTicket(1, login.EmailAddress, DateTime.Now,
                                                                                              DateTime.Now.AddMinutes(30), false, "");
                                string cookiestr = FormsAuthentication.Encrypt(tkt);
                                var ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr) { Path = FormsAuthentication.FormsCookiePath };
                                var user1 = context.Users.FirstOrDefault(x => x.Id == login.CompanyId);
                                if (user1 != null) user1.LastLoginTime = DateTime.Now;
                                context.SaveChanges();
                                Response.Cookies.Add(ck);
                                Session[SessionNames.LoggedInUserId] = login.CompanyId;
                                strRedirect = "~/companyregistration2.aspx";
                            }
                            else if(login.IsAdmin == true)
                            {

                                var tkt = new FormsAuthenticationTicket(1, login.EmailAddress, DateTime.Now,
                                                                                              DateTime.Now.AddMinutes(30), false, "");
                                string cookiestr = FormsAuthentication.Encrypt(tkt);
                                var ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr) { Path = FormsAuthentication.FormsCookiePath };
                                Response.Cookies.Add(ck);
                                Session[SessionNames.LoggedInUserId] = login.UserId;
                                strRedirect = "~/HomePageAfterLoggingIn.aspx";
                            }

                        }
                        else
                        {
                              strRedirect = "~/EmailConfirmationSentSuccess.aspx";
                        }

                        if (Request["ReturnUrl"] != null && Request["ReturnUrl"].Contains("contact-invitepage.aspx"))
                        {
                            strRedirect = Request["ReturnUrl"];
                        }
                         Response.Redirect(strRedirect, false);
                    }
                    //if (user != null)
                    //{

                    //    Session[SessionNames.UserEmailAddress] = user.EmailAddress;
                    //    try
                    //    {
                    //        if (user.IsVerified == true)
                    //        {
                    //            if (isRemember)
                    //            {
                    //                myCookie.Values.Add("username", txtEmail.Value);
                    //                myCookie.Values.Add("password", txtPassword.Text);
                    //                myCookie.Expires = DateTime.Now.AddDays(15);
                    //            }
                    //            else
                    //            {
                                    
                    //                myCookie.Values.Add("username", string.Empty);
                    //                myCookie.Values.Add("password", string.Empty);
                    //                myCookie.Expires = DateTime.Now.AddMinutes(-1);

                    //            }
                    //            Response.Cookies.Add(myCookie);




                    //            LoggingManager.Debug(string.Format("User login success for {0}.", user.EmailAddress));
                    //            user.LastLoginTime = DateTime.Now;
                    //            context.SaveChanges();
                    //            Session[SessionNames.LoggedInUserId] = user.Id;
                    //            var tkt = new FormsAuthenticationTicket(1, user.EmailAddress, DateTime.Now,
                    //                                                                          DateTime.Now.AddMinutes(30), false, "");
                    //            string cookiestr = FormsAuthentication.Encrypt(tkt);
                    //            var ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr) { Path = FormsAuthentication.FormsCookiePath };

                    //            Response.Cookies.Add(ck);

                    //            if (!user.IsCustomizingYourFeedsAccepted.HasValue || !user.IsCustomizingYourFeedsAccepted.Value)
                    //            {
                    //                strRedirect = "~/HuntableTourFeatures.aspx";
                    //            }
                    //            else if (!user.IsCustomizingYourJobsAccepted.HasValue || !user.IsCustomizingYourJobsAccepted.Value)
                    //            {
                    //                strRedirect = "~/HuntableTourCustomizeFeeds.aspx";
                    //            }
                    //            else if (!user.IsSearchingAndConnectingWithOtherUsersAccepted.HasValue || !user.IsSearchingAndConnectingWithOtherUsersAccepted.Value)
                    //            {
                    //                strRedirect = "~/HuntableTourCustomizeJobs.aspx";
                    //            }
                    //            else if (!user.IsProfileUpdated)
                    //            {
                    //                strRedirect = "~/ProfileUploadPage.aspx";
                    //            }
                    //            else
                    //            {
                    //                strRedirect = "~/HomePageAfterLoggingIn.aspx";
                    //            }

                    //        }
                    //        else
                    //        {
                    //            strRedirect = "~/EmailConfirmationSentSuccess.aspx";
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        LoggingManager.Error(ex);
                    //    }

                       
                  
                    else
                    {
                        lblSignInError.Text = "Invalid credentials.";
                        lblSignInError.Visible = true;
                    }
                }
            }
            else
            {
                lblSignInError.Text = "Enter Credentials";
                lblSignInError.Visible = true;
            }

            LoggingManager.Debug("Exiting BtnSignInClick - HeaderBeforeLoggingIn.ascx");
        }

        //protected void BtnSearchForUsersClick(object sender, EventArgs e)
        //{
        //    LoggingManager.Debug("Entering BtnSearchForUsersClick - Default.aspx");
        //    try
        //    {
        //        string url = string.Format("~/UserSearch.aspx?keyword={0}", txtUserSearchKeyword.Text);
        //        Response.Redirect(url, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggingManager.Error(ex);

        //    }
        //    LoggingManager.Debug("Exiting BtnSearchForUsersClick - Default.aspx");
        //}

        //protected void BtnSearchForJobsClick(object sender, EventArgs e)
        //{
        //    LoggingManager.Debug("Entering BtnSearchForJobsClick - Default.aspx");
        //    try
        //    {
        //        string url = string.Format("~/JobSearch.aspx?keyword={0}", txtUserSearchKeyword.Text);
        //        Response.Redirect(url, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggingManager.Error(ex);

        //    }
        //    LoggingManager.Debug("Exiting BtnSearchForJobsClick - Default.aspx");
        //}

        protected void LogOutClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering LogOutClick - HeaderBeforeLoggingIn.ascx");
            Session.Abandon();
            Session.Clear();
            Response.Redirect(PageNames.Home);
            LoggingManager.Debug("Exiting LogOutClick - HeaderBeforeLoggingIn.ascx");
        }


        protected void PictureUploadClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering PictureUploadClick - HeaderBeforeLoggingIn.ascx");
            Response.Redirect("PictureUpload.aspx", false);

            LoggingManager.Debug("Exiting PictureUploadClick - HeaderBeforeLoggingIn.ascx");
        }

        protected void lnkHunt_click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkHunt_click - HeaderBeforeLoggingIn.ascx");

            Response.Redirect("~/WhatIsHuntable.aspx");

            LoggingManager.Debug("Exiting lnkHunt_click - HeaderBeforeLoggingIn.ascx");
        }

        protected void lnkFriends_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkFriends_Click - HeaderBeforeLoggingIn.ascx");

            Response.Redirect("~/InviteFriends.aspx");

            LoggingManager.Debug("Exiting lnkFriends_Click - HeaderBeforeLoggingIn.ascx");

        }
        protected void lnkRecruiter_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering lnkRecruiter_Click - HeaderBeforeLoggingIn.ascx");
            Response.Redirect("~/Recruiter.aspx");
            LoggingManager.Debug("Exiting lnkRecruiter_Click - HeaderBeforeLoggingIn.ascx");
        }
    }


}