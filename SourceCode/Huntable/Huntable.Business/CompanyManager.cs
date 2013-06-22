using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using DotNetOpenAuth.ApplicationBlock;
using Huntable.Data;
using Huntable.Data.EntityExtensions;
using Huntable.Data.Enums;
using MoreLinq;
using Snovaspace.Util;
using Snovaspace.Util.Mail;
using System.Web;
using Snovaspace.Util.Logging;


namespace Huntable.Business
{
    public class CompanyManager
    {
        public void CompanyFirstRegistrayion(string companyname, string companywebsite, int country, string companyemail,
                                             int userid, string password, bool rec)
        {
            LoggingManager.Debug("Entering CompanyFirstRegistrayion  - CompanyManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = new User
                {
                    FirstName = companyname,
                    Password = password,
                    EmailAddress = companyemail,
                    IsCompany = true,
                    CreatedDateTime = DateTime.Now,
                    LastLoginTime = DateTime.Now,
                    LastProfileUpdatedOn = DateTime.Now,
                    
                    
                };
                context.Users.AddObject(user);
                context.SaveChanges();
                var company = new Company
                                  {

                                      Password = password,
                                      CompanyName = companyname,
                                      CompanyWebsite = companywebsite,
                                      CountryId = country,
                                      CompanyEmail = companyemail,
                                      Userid = user.Id,
                                      AgreeTerms = true,
                                      CreatedDateTime = DateTime.Now,

                                  };

                context.Companies.AddObject(company);
                context.SaveChanges();
                var logins = new Login
                {
                    EmailAddress = companyemail,
                    CompanyId = user.Id,
                    Password = password,
                    IsCompany = true,
                    IsRecuirter = rec
                };
                context.Logins.AddObject(logins);
                context.SaveChanges();
                var first = context.Users.FirstOrDefault(x => x.Id == userid);
                var userCompany = new UserCompany
                    {
                        UserId = userid,
                        CompanyId = company.Id,
                    };
                context.UserCompanies.AddObject(userCompany);
                context.SaveChanges();
                if (first != null)
                {
                    var firstname = first.FirstName;
                    first.HasCompany = true;
                    context.SaveChanges();
                   
                    SendVerificationEMail(company, firstname);
                   
                }
            }
            LoggingManager.Debug("Exiting CompanyFirstRegistrayion  - CompanyManager.cs");
          
        }

        public void SendVerificationEMail(Company company, string firstname)
        {
            var applicationBaseUrl = Common.GetApplicationBaseUrl();
            LoggingManager.Debug("Entering SendVerificationEMail  - CompanyManager.cs");

            var url = applicationBaseUrl + @"ConfirmCompanyEmailAddress.aspx?Id=" + company.Userid;
            var emailTemplate = EmailTemplateManager.GetTemplate(EmailTemplates.VerifyCompanyEmail);
            var verifyEmailValues = new Hashtable { { "FirstName", firstname }, { "Server Url", applicationBaseUrl }, { "LINK", url } };
            var body = SnovaUtil.LoadTemplate(emailTemplate.TemplateText, verifyEmailValues);
            var subject = emailTemplate.Subject;
            SnovaUtil.SendEmail(subject, body, company.CompanyEmail);

            LoggingManager.Debug("Exiting SendVerificationEMail  - CompanyManager.cs");
        }

        public void CompanyRegistrationupdateCommon(int loginid, int logoid, string companyname, string companyheader,
                                                    string companydescription, int industry, string companywebsite,
                                                    string phonenumber, string emialaddress, int employyee,
                                                    string address1, string address2, string towncity, string postcode,
                                                    int countryid)
        {
            LoggingManager.Debug("Entering CompanyRegistrationupdateCommon  - CompanyManager.cs");


            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var socialManager = new SocialShareManager();
                var user = context.Users.FirstOrDefault(x => x.Id == loginid);
                var company = context.Companies.FirstOrDefault(x => x.Userid == user.Id && user.IsCompany == true);
                if (company != null)
                {
                    if (logoid != 0)
                    {
                        company.CompanyLogoId = logoid;
                        if (user != null) user.PersonalLogoFileStoreId = logoid;                        
                        var url1 = "https://huntable.co.uk/LoadFile.ashx?id=" + company.CompanyLogoId;
                        socialManager.ShareOnFacebook(loginid, "[UserName] updated their Company logo in Huntable ", url1);
                    }

                    company.CompanyName = companyname;
                    company.CompanyDescription = companydescription;
                    company.CompanyHeading = companyheader;
                    company.CompanyIndustry = industry;
                    company.CompanyWebsite = companywebsite;
                    company.PhoneNo = phonenumber.ToString(CultureInfo.InvariantCulture);
                    company.EmailAdress = emialaddress;
                    company.NoofEmployees = employyee;
                    company.Address1 = address1;
                    company.Address2 = address2;
                    company.TownCity = towncity;
                    company.CountryId = countryid;
                    company.Postcode = postcode.ToString(CultureInfo.InvariantCulture);
                    if (user != null) user.LastProfileUpdatedOn = DateTime.Now;
                    context.SaveChanges();                                                                      
                }

            }
            LoggingManager.Debug("Exiting CompanyRegistrationupdateCommon  - CompanyManager.cs");


        }

        public void CompanyRegistrationupdatePortfolio(int loginid, int portfolioid, string portfoliodescription,
                                                      string videourl)
        {
            LoggingManager.Debug("Entering CompanyRegistrationupdatePortfolio  - CompanyManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == loginid);
                user.LastProfileUpdatedOn = DateTime.Now;
                var company = context.Companies.FirstOrDefault(x => x.Userid == user.Id && user.IsCompany == true);
                if (company != null)
                {
                    var companyid = company.Id;
                    if (portfolioid != 0)
                    {
                        var portfolio = new CompanyPortfolio
                            {
                                CompanyId = companyid,
                                PortfolioImageid = portfolioid,
                                PortfolioDescription = portfoliodescription,
                                CreatedDateTime = DateTime.Now
                            };
                        context.CompanyPortfolios.AddObject(portfolio);
                        context.SaveChanges();
                        var socialManager = new SocialShareManager();
                        var msg = "https://huntable.co.uk/LoadFile.ashx?id=" + portfolioid;
                        socialManager.ShareOnFacebook(loginid, "[UserName] updated its portfolio picture",msg);
                        FeedManager.addFeedNotification(FeedManager.FeedType.Company_Portfolio_Photo, loginid, portfolio.Id, null);
                    }
                    if (!string.IsNullOrEmpty(videourl) && videourl != "Video url")
                    {

                        var video = new CompanyVideo { CompanyId = companyid, VideoUrl = videourl, CreatedDatetime = DateTime.Now };
                        context.CompanyVideos.AddObject(video);
                        context.SaveChanges();
                        var socialManager = new SocialShareManager();
                        var msg = "[UserName]" + " " + "shared a video via Huntable";
                        socialManager.ShareVideoOnFacebook(loginid, msg, video.VideoUrl);
                        FeedManager.addFeedNotification(FeedManager.FeedType.Company_Video, loginid, video.Id, null);
                    }
                }
            }

            LoggingManager.Debug("Exiting CompanyRegistrationupdatePortfolio  - CompanyManager.cs");
        }

        public void CompanyProductUpdate(int loginid, int productid, string productdescription)
        {
            LoggingManager.Debug("Entering CompanyProductUpdate  - CompanyManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == loginid);
                user.LastProfileUpdatedOn = DateTime.Now;
                var company = context.Companies.FirstOrDefault(x => x.Userid == user.Id && user.IsCompany == true);
                if (company != null)
                {
                    var companyid = company.Id;
                    var product = new CompanyProduct
                                      {
                                          ComapnyId = companyid,
                                          ProductImageId = productid,
                                          ProductDescription = productdescription,
                                          CreatedDateTime = DateTime.Now
                                      };
                    context.CompanyProducts.AddObject(product);

                    context.SaveChanges();
                    FeedManager.addFeedNotification(FeedManager.FeedType.Company_Product, user.Id, product.Id, null);                    
                }
            }
            LoggingManager.Debug("Exiting CompanyProductUpdate  - CompanyManager.cs");

        }

        public void CompanyArticleUpdate(int loginid, string articletitle, string articledescription)
        {
            LoggingManager.Debug("Entering CompanyArticleUpdate  - CompanyManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == loginid);
                user.LastProfileUpdatedOn = DateTime.Now;
                var company = context.Companies.FirstOrDefault(x => x.Userid == user.Id && user.IsCompany == true);
                if (company != null)
                {
                    var companyid = company.Id;

                    var article = new CompanyArticle
                                      {
                                          CompanyId = companyid,
                                          Article = articletitle,
                                          ArticleDescription = articledescription,
                                          PostedDate = DateTime.Now
                                      };
                    context.CompanyArticles.AddObject(article);
                    context.SaveChanges();
                    var socialManager = new SocialShareManager();
                    var msg = "[UserName]" + " " + "added new Article - "+article.Article+"-"+" "+"https://huntable.co.uk/article.aspx?Id="+companyid+"&"+"ATId="+article.Id;
                    socialManager.ShareOnFacebook(loginid, msg, "");
                }
            }
            LoggingManager.Debug("Exiting CompanyArticleUpdate  - CompanyManager.cs");
        }

        public void CompanyCarrerUpdate(int loginid, string companycarrerlink, string companyblog)
        {
            LoggingManager.Debug("Entering CompanyCarrerUpdate  - CompanyManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == loginid);
                user.LastProfileUpdatedOn = DateTime.Now;
                var company = context.Companies.FirstOrDefault(x => x.Userid == user.Id && user.IsCompany == true);
                if (company != null)
                {
                    company.CompanyBlogLink = companyblog;
                    company.CompanyCarrersLink = companycarrerlink;
                }
                context.SaveChanges();
                var socialManager = new SocialShareManager();
                var bloglink = "[UserName]" + " " + "updated their blog - " + "https://huntable.co.uk/company-blogs-popular.aspx?Id"+company.Id;
                var careerslink = "[UserName]" + " " + "updated their Career page - " + "https://huntable.co.uk/companycareers.aspx?Id=" + company.Id;
                socialManager.ShareOnFacebook(loginid, bloglink, "");
                socialManager.ShareOnFacebook(loginid,careerslink,"");
            }
            LoggingManager.Debug("Exiting CompanyCarrerUpdate  - CompanyManager.cs");
        }
        public string CompanyCarrerlink(int loginid)
        {
            LoggingManager.Debug("Entering CompanyCarrerlink  - CompanyManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == loginid);
                user.LastProfileUpdatedOn = DateTime.Now;
                var company = context.Companies.FirstOrDefault(x => x.Userid == user.Id && user.IsCompany == true);

                LoggingManager.Debug("Extinging CompanyCarrerlink  - CompanyManager.cs");

                return company.CompanyCarrersLink;
            }
        }
        public static List<Company> GetUserCountry(int countryid, int sizeid, int industryid)
        {
            LoggingManager.Debug("Entering GetUserCountry  - CompanyManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var companycounty = context.Companies.Where(x => x.CountryId == countryid);

                LoggingManager.Debug("Exiting GetUserCountry  - CompanyManager.cs");

                return companycounty.ToList();
            }


        }

        public List<Company> GetCompaniesCountryList(int loginid, string letterFilter = null, IList<int> countryIds = null)
        {
            LoggingManager.Debug("Entering GetCompaniesCountryList  - CompanyManager.cs");

            if (string.IsNullOrWhiteSpace(letterFilter))
            {
                letterFilter = string.Empty;
            }

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var companies = context.Companies.Include("MasterCountry").Include("MasterIndustry").Include("MasterEmployee").Where(x => x.CompanyName.StartsWith(letterFilter) && x.IsVerified == true).ToList();

                if (countryIds == null || countryIds.Count == 0)
                {
                    var query1 = companies;
                    query1.ToList().ForEach(u =>
                    {
                        u.IsUserFollowingCompany =
                            !context.PreferredFeedUserCompaniesFollwers.Any(
                                x => x.FollowingUserId == loginid && x.CompanyID == u.Id);
                    }
                   );
                    return query1;
                }

                LoggingManager.Debug("Exiting GetCompaniesCountryList  - CompanyManager.cs");

                var query = companies.Where(x => x.CountryId != null && (countryIds.Contains(x.CountryId.Value))).ToList();

                query.ToList().ForEach(u =>
                                           {
                                               u.IsUserFollowingCompany =
                                                   !context.PreferredFeedUserCompaniesFollwers.Any(
                                                       x => x.FollowingUserId == loginid && x.CompanyID == u.Id);
                                           }
                    );
                return query;
            }


        }
        public static List<Company> GetFeaturedUserPCompanies(int count=8,string letterfilter = null)
        {
            LoggingManager.Debug("Entering GetFeaturedUserPCompanies  - CompanyManager.cs");

            if (string.IsNullOrWhiteSpace(letterfilter))
            {
                letterfilter = string.Empty;
            }
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LoggingManager.Debug("Exiting GetFeaturedUserPCompanies  - CompanyManager.cs");

                return context.Companies.Where(x => x.CompanyName.StartsWith(letterfilter) && x.IsVerified == true).Take(count).OrderByDescending(x=>x.CreatedDateTime).ToList();
            }
        }

        public static List<Company> GetFeaturedUserComp(string letterfilter = null)
        {
            LoggingManager.Debug("Entering GetFeaturedUserComp  - CompanyManager.cs");

            if (string.IsNullOrWhiteSpace(letterfilter))
            {
                letterfilter = string.Empty;
            }
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LoggingManager.Debug("Exiting GetFeaturedUserComp  - CompanyManager.cs");

                return context.Companies.Where(x => x.CompanyName.StartsWith(letterfilter) && x.IsVerified == true).Take(9).OrderByDescending(x=>x.CreatedDateTime).ToList();
            }
        }
        public List<Company> GetCompaniessizeList(int loginid, string letterFilter = null, IList<int> sizeIds = null)
        {
            LoggingManager.Debug("Entering GetCompaniessizeList  - CompanyManager.cs");
            if (string.IsNullOrWhiteSpace(letterFilter))
            {
                letterFilter = string.Empty;
            }

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var companies = context.Companies.Include("MasterCountry").Include("MasterIndustry").Include("MasterEmployee").Where(x => x.CompanyName.StartsWith(letterFilter) && x.IsVerified == true).ToList();

                if (sizeIds == null || sizeIds.Count == 0)
                {
                    var query1 = companies;
                    query1.ToList().ForEach(u =>
                    {
                        u.IsUserFollowingCompany =
                            !context.PreferredFeedUserCompaniesFollwers.Any(
                                x => x.FollowingUserId == loginid && x.CompanyID == u.Id);
                    }
                   );
                    return query1;
                }

                LoggingManager.Debug("Exiting GetCompaniessizeList  - CompanyManager.cs");

                var query = companies.Where(x => x.NoofEmployees != null && (x.CountryId != null && (sizeIds.Contains(x.NoofEmployees.Value))) && x.IsVerified == true).ToList();
                query.ToList().ForEach(u =>
                {
                    u.IsUserFollowingCompany =
                        !context.PreferredFeedUserCompaniesFollwers.Any(
                            x => x.FollowingUserId == loginid && x.CompanyID == u.Id);
                }
                   );
                return query;
            }

        }
        public List<Company> GetCompaniesIndustryList(int loginid, string letterFilter = null, IList<int> industryIds = null)
        {
            LoggingManager.Debug("Entering GetCompaniesIndustryList  - CompanyManager.cs");
            if (string.IsNullOrWhiteSpace(letterFilter))
            {
                letterFilter = string.Empty;
            }

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var companies = context.Companies.Include("MasterCountry").Include("MasterIndustry").Include("MasterEmployee").Where(x => x.CompanyName.StartsWith(letterFilter) && x.IsVerified == true).OrderByDescending(x=>x.CreatedDateTime).ToList();

                if (industryIds == null || industryIds.Count == 0)
                {
                    var query1 = companies;
                    query1.ToList().ForEach(u =>
                    {
                        u.IsUserFollowingCompany =
                            !context.PreferredFeedUserCompaniesFollwers.Any(
                                x => x.FollowingUserId == loginid && x.CompanyID == u.Id);
                    }
                   );
                    return query1;
                }

                LoggingManager.Debug("Exiting GetCompaniesIndustryList  - CompanyManager.cs");

                var query = companies.Where(x => x.CompanyIndustry != null && (x.CountryId != null && (industryIds.Contains(x.CompanyIndustry.Value)))).ToList();
                query.ToList().ForEach(u =>
                {
                    u.IsUserFollowingCompany =
                        !context.PreferredFeedUserCompaniesFollwers.Any(
                            x => x.FollowingUserId == loginid && x.CompanyID == u.Id);
                }
                 );
                return query;
            }
        }


        public static List<PreferredFeedUserCompaniesFollwer> GetUserFollowingCompanies(int loginid)
        {
            LoggingManager.Debug("Entering GetUserFollowingCompanies  - CompanyManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LoggingManager.Debug("Exiting GetUserFollowingCompanies  - CompanyManager.cs");

                return
                    context.PreferredFeedUserCompaniesFollwers.Include("Company").Where(
                        x => x.FollowingUserId == loginid).ToList();
            }

        }
        public static bool FollowCompany(int loginUserId, int companyid)
        {
            LoggingManager.Debug("Entering FollowCompany  - CompanyManager.cs");

            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var cmpid = context.Companies.FirstOrDefault(x => x.Id == companyid);

                    var followup = new PreferredFeedUserCompaniesFollwer { CompanyID = companyid, FollowingUserId = loginUserId, DateTime=DateTime.Now };
                    var feeduser = new PreferredFeedUserUser { FollowingUserId = loginUserId, UserId = (int)cmpid.Userid };
                    context.PreferredFeedUserUsers.AddObject(feeduser);
                    context.PreferredFeedUserCompaniesFollwers.AddObject(followup);
                    FOllowCompanyEmail(companyid, loginUserId);
                    context.SaveChanges();
                    FeedManager.addFeedNotification(FeedManager.FeedType.Follow, loginUserId, feeduser.UserId, null);
                    var socialManager = new SocialShareManager();
                    var msg = "[UserName]" + " " + "is following" + " " + cmpid.CompanyName+" "+"in Huntable";
                    socialManager.ShareOnFacebook(loginUserId, msg, "");
                }

                return true;
            }
            catch (Exception ex)
            {
                LoggingManager.Debug("Exiting FollowCompany  - CompanyManager.cs");
                // TO DO : log the exception into note pad.
                return false;
            }


        }

        public static bool UnfollowCompany(int loginuserid, int companyid)
        {
            LoggingManager.Debug("Entering UnfollowComapny-CompanyManager.cs");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var unfollow =
                        context.PreferredFeedUserCompaniesFollwers.FirstOrDefault(
                            x => x.CompanyID == companyid && x.FollowingUserId == loginuserid);
                    context.DeleteObject(unfollow);
                    context.SaveChanges();
                    var queuser = context.Companies.FirstOrDefault(x => x.Id == companyid);
                    var result = context.PreferredFeedUserUsers.FirstOrDefault(y => y.UserId == queuser.Userid && y.FollowingUserId == loginuserid);

                    FeedManager.deleteFeedNotitifation(FeedManager.FeedType.Follow, loginuserid, result.UserId);
                    context.DeleteObject(result);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Debug("Exiting UnFollowCompany  - CompanyManager.cs");
                // TO DO : log the exception into note pad.
                return false;
            }
        }
        public static List<PreferredFeedUserCompaniesFollwer> FollowingCompany(int loginUserId, int companyid)
        {
            LoggingManager.Debug("Entering FollowingCompany  - CompanyManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LoggingManager.Debug("Exiting FollowingCompany  - CompanyManager.cs");
                return
                    context.PreferredFeedUserCompaniesFollwers.Where(
                        x => x.CompanyID == companyid && x.FollowingUserId == loginUserId).ToList();
            }

        }
        public static void FOllowCompanyEmail(int otheruserid, int loginuserid)
        {
            LoggingManager.Debug("Entering FOllowCompanyEmail  - CompanyManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var touser = context.Companies.FirstOrDefault(x => x.Id == otheruserid);
                var actuser = context.Users.First(x => x.Id == loginuserid);
                var following = context.PreferredFeedUserUsers.Where(x => x.UserId == loginuserid).ToList();
                var followed = context.PreferredFeedUserUsers.Where(x => x.FollowingUserId == loginuserid).ToList();
                var template = EmailTemplateManager.GetTemplate(Data.Enums.EmailTemplates.FollowUser);
                var valuesList = new Hashtable
                                     {
                                         {"Name", actuser.Name},
                                         {"Actual User Name", touser.CompanyName},
                                         {"Actual User Role", actuser.CurrentPosition},
                                         {"Actual User Company", actuser.CurrentCompany},
                                         {"Description",touser.CompanyDescription},
                                         {"Following", (following.Count != 0) ? following.Count : 0},
                                         {"Followed", (followed.Count != 0) ? followed.Count : 0}
                                     };
                string baseUrl = Common.GetApplicationBaseUrl();
                string userProfilePath = Path.Combine(baseUrl, "ViewUserProfile.aspx?UserId=" + actuser.Id);
                valuesList.Add("Actual User Profile Link", userProfilePath);
                string userProfilePicturePath = Path.Combine(baseUrl, actuser.UserProfilePictureDisplayUrl.Replace("~/", string.Empty));
                valuesList.Add("User Profile Picture", userProfilePicturePath);
                valuesList.Add("Server Url", baseUrl);
                valuesList.Add("Dont Want To Receive Email", Path.Combine(baseUrl, "UserEmailNotification.aspx"));
                valuesList.Add("FollowBackUser", baseUrl + "default.aspx?" + "ReturnUrl=ViewUserProfile.aspx?UserId=" + actuser.Id);
                string body = SnovaUtil.LoadTemplate(template.TemplateText, valuesList);
                SnovaUtil.SendEmail(template.Subject, body, touser.CompanyEmail);
                LoggingManager.Debug("Exiting FOllowCompanyEmail  - CompanyManager.cs");
            }
        }
        public List<Company> GetCmpny(int cmpid)
        {
            LoggingManager.Debug("Entering GetCmpny  - CompanyManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LoggingManager.Debug("Exiting GetCmpny  - CompanyManager.cs");

                var result = context.Companies.Include("MasterIndustry").Where(x => x.Id == cmpid).ToList();
                return result;
            }
        }
        public List<CompanyArticle> GetCmpny1(int cmpid)
        {
            LoggingManager.Debug("Entering GetCmpny1  - CompanyManager.cs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LoggingManager.Debug("Exiting GetCmpny1  - CompanyManager.cs");

                return context.CompanyArticles.Where(x => x.CompanyId == cmpid).ToList();
            }
        }
        public List<CompanyPortfolio> GetCmpny2(int cmpid)
        {
            LoggingManager.Debug("Entering GetCmpny2  - CompanyManager.cs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LoggingManager.Debug("Exiting GetCmpny2  - CompanyManager.cs");

                return context.CompanyPortfolios.Where(x => x.CompanyId == cmpid).ToList();
            }
        }
        public List<int?> GetCmpnyFlwr(int cmpid)
        {
            LoggingManager.Debug("Entering GetCmpnyFlwr  - CompanyManager.cs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LoggingManager.Debug("Exiting GetCmpnyFlwr  - CompanyManager.cs");
                List<PreferredFeedUserCompaniesFollwer> newResult = context.PreferredFeedUserCompaniesFollwers.Where(x => x.CompanyID == cmpid).OrderByDescending(s => s.DateTime).ToList();
                var result = newResult.Select(s => s.FollowingUserId).Distinct().ToList();
               // var oldresult = context.PreferredFeedUserCompaniesFollwers.Where(x => x.CompanyID == cmpid).OrderBy(s => s.DateTime).Select(u => u.FollowingUserId).Distinct().ToList();
              // return context.PreferredFeedUserCompaniesFollwers.Where(x => x.CompanyID == cmpid).OrderBy(s=>s.DateTime).Select(u => u.FollowingUserId).Distinct().ToList();
                return result;
            }
        }

        public int GetJobsPostedByCompany(int compid)
        {
            LoggingManager.Debug("Entering GetJobsPostedByCompany  - CompanyManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                var companydetails = context.Companies.FirstOrDefault(x => x.Id == compid);
                var compname = companydetails.CompanyName;
                LoggingManager.Debug("Exiting GetJobsPostedByCompany  - CompanyManager.cs");
               var date = DateTime.Now.AddDays(-30);
                return context.Jobs.Count(x => x.UserId == companydetails.Userid && x.CreatedDateTime>date);
            }
        }
        public int GetcompanyFollowers(int cmpid)
        {
            LoggingManager.Debug("Entering GetcompanyFollowers  - CompanyManager.cs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                LoggingManager.Debug("Exiting GetcompanyFollowers  - CompanyManager.cs");

                return context.PreferredFeedUserCompaniesFollwers.Where(x => x.CompanyID == cmpid).Select(u => u.FollowingUserId).Distinct().Count();

            }
        }

        public int GetFeaturedrecuirtersCount(int loginid, string letterFilter = null)
        {
            LoggingManager.Debug("Entering GetFeaturedrecuirtersCount  - CompanyManager.cs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                if (string.IsNullOrWhiteSpace(letterFilter))
                {
                    letterFilter = string.Empty;
                }
                var newquery = (from a in context.Users
                                join b in context.EmploymentHistories on a.Id equals b.UserId
                                join c in context.Companies on a.CountryID equals c.CountryId
                                where a.Id == loginid
                                select new { c.CompanyLogoId, c.CompanyName, c.Id }).ToList().Distinct();

                newquery.Where(x => x.CompanyName.StartsWith(letterFilter));
                LoggingManager.Debug("Exiting GetFeaturedrecuirtersCount  - CompanyManager.cs");

                return newquery.Count();
            }
        }
        public object GetFeaturedUserComapnies(int loginid, string letterFilter = null)
        {
            LoggingManager.Debug("Entering GetFeaturedUserComapnies  - CompanyManager.cs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                if (string.IsNullOrWhiteSpace(letterFilter))
                {
                    letterFilter = string.Empty;
                }
                var newquery = (from a in context.Users
                                join b in context.EmploymentHistories on a.Id equals b.UserId
                                join c in context.Companies on a.CountryID equals c.CountryId
                                where a.Id == loginid && c.IsVerified == true
                                select new { c.CompanyLogoId, c.CompanyName, c.Id , c.CreatedDateTime}).ToList().Distinct().OrderByDescending(x=>x.CreatedDateTime);
                LoggingManager.Debug("Exiting GetFeaturedUserComapnies  - CompanyManager.cs");

                return newquery.Where(x => x.CompanyName.StartsWith(letterFilter));
            }
        }


        public object GetFeaturedUserPcomp(int loginid, string letterFilter = null)
        {
            LoggingManager.Debug("Entering GetFeaturedUserPcomp  - CompanyManager.cs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                if (string.IsNullOrWhiteSpace(letterFilter))
                {
                    letterFilter = string.Empty;
                }
                var newquery = (from a in context.Users
                                join b in context.EmploymentHistories on a.Id equals b.UserId
                                join c in context.Companies on a.CountryID equals c.CountryId
                                where a.Id == loginid
                                select new { c.CompanyLogoId, c.CompanyName, c.Id }).ToList().Distinct();
                LoggingManager.Debug("Exiting GetFeaturedUserPcomp  - CompanyManager.cs");

                return newquery.Where(x => x.CompanyName.StartsWith(letterFilter)).Take(9);
            }
        }


        public List<Company> GetCompanyFollowersIList(int loginid, List<MasterEmployee> followerdids = null, string letterFilter = null)
        {
            LoggingManager.Debug("Entering GetCompanyFollowers  - CompanyManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                if (string.IsNullOrWhiteSpace(letterFilter))
                {
                    letterFilter = string.Empty;
                }
                IList<Company> company = context.Companies.Include("MasterCountry").Include("MasterIndustry").Include("MasterEmployee").Where(x => x.CompanyName.StartsWith(letterFilter) && x.IsVerified == true).ToList();
                if (followerdids == null || followerdids.Count == 0)
                {
                    var query1 = company;
                    query1.ToList().ForEach(u =>
                    {
                        u.IsUserFollowingCompany =
                            !context.PreferredFeedUserCompaniesFollwers.Any(
                                x => x.FollowingUserId == loginid && x.CompanyID == u.Id);
                    }
                   );
                    return (List<Company>)query1;
                }

                IList<int> companyIds = new List<int>();
                List<Company> searchResultCompanies = new List<Company>();
                IList<string> searchResultsCompanyNames = new List<string>();
                IList<Company> companies = new List<Company>();
                IQueryable<IGrouping<int, PreferredFeedUserCompaniesFollwer>> que =
                    from c in context.PreferredFeedUserCompaniesFollwers
                    group c by c.CompanyID;
                foreach (var gro in que)
                {
                    foreach (var masterEmployee in followerdids)
                    {
                        if (gro.Count() >= masterEmployee.Min && gro.Count() <= masterEmployee.Max)
                        {
                            companyIds.Add(gro.Key);
                        }
                    }
                }

                var companiesList = context.Companies.Include("MasterCountry").Include("MasterIndustry").Include("MasterEmployee").ToList();

                foreach (var companyId in companyIds)
                {
                    var companyLocated = companiesList.FirstOrDefault(x => x.Id == companyId);
                    if (companyLocated != null) searchResultCompanies.Add(companyLocated);
                }
                searchResultCompanies.ToList().ForEach(u =>
                {
                    u.IsUserFollowingCompany =
                        !context.PreferredFeedUserCompaniesFollwers.Any(
                            x => x.FollowingUserId == loginid && x.CompanyID == u.Id);
                }
                  );
                LoggingManager.Debug("Exiting GetCompanyFollowers  - CompanyManager.cs");
                return searchResultCompanies.ToList();
            }
        }

        public IList<Company> GetCompanyJobs(int loginid, IList<MasterEmployee> searchCriterias = null, string letterFilter = null)
        {
            LoggingManager.Debug("Entering GetCompanyJobs  - CompanyManager.cs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                if (string.IsNullOrWhiteSpace(letterFilter))
                {
                    letterFilter = string.Empty;
                }
                IList<Company> company = context.Companies.Include("MasterCountry").Include("MasterIndustry").Include("MasterEmployee").Where(x => x.CompanyName.StartsWith(letterFilter) && x.IsVerified == true).ToList();
                if (searchCriterias == null || searchCriterias.Count == 0)
                {
                    var query1 = company;
                    query1.ToList().ForEach(u =>
                    {
                        u.IsUserFollowingCompany =
                            !context.PreferredFeedUserCompaniesFollwers.Any(
                                x => x.FollowingUserId == loginid && x.CompanyID == u.Id);
                    }
                   );
                    return query1;
                }

                List<Company> searchResultCompanies = new List<Company>();
                IList<string> searchResultsCompanyNames = new List<string>();
                IQueryable<IGrouping<string, Job>> jobsByCompany = from c in context.Jobs group c by c.CompanyName;
                foreach (var jobsByCompanyGroup in jobsByCompany)
                {
                    foreach (var searchCriteria in searchCriterias)
                    {
                        if (jobsByCompanyGroup.Count() >= searchCriteria.Min && jobsByCompanyGroup.Count() <= searchCriteria.Max)
                        {
                            searchResultsCompanyNames.Add(jobsByCompanyGroup.Key);
                        }
                    }
                }

                var companiesList = context.Companies.ToList();

                foreach (var companyName in searchResultsCompanyNames)
                {
                    var companyLocated = companiesList.FirstOrDefault(x => x.CompanyName == companyName);

                    if (companyLocated != null) searchResultCompanies.Add(companyLocated);
                }

                searchResultCompanies.ForEach(u => u.IsUserFollowingCompany = !context.PreferredFeedUserCompaniesFollwers.Any(x => x.FollowingUserId == loginid && x.CompanyID == u.Id));

                LoggingManager.Debug("Exiting GetCompanyJobs  - CompanyManager.cs");

                return searchResultCompanies;
            }
        }

        public List<Company> GetCompanySearch(string keyword, string letterFilter = null, int? loginid = null)
        {
            LoggingManager.Debug("Entering GetCompanySearch  - CompanyManager.cs");
            if (string.IsNullOrWhiteSpace(letterFilter))
            {
                letterFilter = string.Empty;
            }
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var query = (context.Companies.Include("MasterIndustry").Include("MasterEmployee").Include("MasterCountry").Where(x => x.CompanyName.StartsWith(letterFilter) && x.IsVerified == true));
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower();
                    query = query.Where(u => u.CompanyName.Contains(keyword)
                                                    || u.EmailAdress.Contains(keyword)
                                                    || u.MasterCountry.Description.Contains(keyword)
                                                    || u.MasterIndustry.Description.Contains(keyword)
                                                    || u.CompanyDescription.Contains(keyword)
                                                    || u.CompanyHeading.Contains(keyword)).OrderByDescending(u=>u.CreatedDateTime);
                }

                LoggingManager.Debug("Exiting GetCompanySearch  - CompanyManager.cs");
                query.ToList().OrderByDescending(u=>u.CreatedDateTime).ForEach(u =>
                                         {
                                             u.IsUserFollowingCompany =
                                                 !context.PreferredFeedUserCompaniesFollwers.Any(
                                                     x => x.FollowingUserId == loginid && x.CompanyID == u.Id);
                                         });
                return query.OrderByDescending(u=>u.CreatedDateTime).ToList();

            }
        }
        public void InsertCompanyProfileVisitedHistory(int loginUserId, int OtherComId)
        {
            LoggingManager.Debug("Entering InsertCompanyProfileVisitedHistory  - CompanyManager.cs");

            using (var context = new huntableEntities())
            {
                var companyProfileVisitedHistory = new CompanyProfileVisitedHistory
                {
                    Date = DateTime.Now,
                    CompanyId = OtherComId,
                    VisitorCompanyId = loginUserId,
                    IPAddress = GetIpAddress()
                };
                context.CompanyProfileVisitedHistories.AddObject(companyProfileVisitedHistory);
                context.SaveChanges();
            }
            LoggingManager.Debug("Exiting InsertCompanyProfileVisitedHistory  - CompanyManager.cs");
        }

        private string GetIpAddress()
        {
            LoggingManager.Debug("Entering GetIpAddress  - CompanyManager.cs");
            string strIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                                  HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            LoggingManager.Debug("Exiting GetIpAddress  - CompanyManager.cs");
            return strIpAddress;
        }

        public List<CompanyProfileVisitedHistory> GetViewrsOfThisCompany(int companyId)
        {
            LoggingManager.Debug("Entering GetViewrsOfThisCompany  - CompanyManager.cs");

            using (var context = new huntableEntities())
            {
                LoggingManager.Debug("Exiting GetViewrsOfThisCompany  - CompanyManager.cs");

                return context.CompanyProfileVisitedHistories.Where(x => x.CompanyId == companyId).ToList();
            }
        }


        public List<CompanyProfileVisitedHistory> GetCompaniesVisitedOfThisUser(int userId)
        {
            LoggingManager.Debug("Entering GetCompaniesVisitedOfThisUser  - CompanyManager.cs");
            using (var context = new huntableEntities())
            {
                LoggingManager.Debug("Exiting GetCompaniesVisitedOfThisUser  - CompanyManager.cs");
                // TO DO - change VisitorCompanyId to VisitorUserId
                return context.CompanyProfileVisitedHistories.Where(x => x.VisitorCompanyId == userId).ToList();
            }
        }

        public Company GetLogIdForCompany(int companyId)
        {
            LoggingManager.Debug("Entering GetLogIdForCompany  - CompanyManager.cs");
            using (var context = new huntableEntities())
            {
                LoggingManager.Debug("Exiting GetLogIdForCompany  - CompanyManager.cs");
                // TO DO - change VisitorCompanyId to VisitorUserId
                return context.Companies.FirstOrDefault(x => x.Id == companyId);
            }
        }

        public static List<User> GetUsersAndFriends(int userId, int pageIndex, int pageSize, out int totalRecords)
        {
            LoggingManager.Debug("Entering GetUsersAndFriends - UserManager");
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["HuntableDb"].ConnectionString);
            var command = new SqlCommand(StoredProcedures.SearchUsersToFollow, con) { CommandType = System.Data.CommandType.StoredProcedure };
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@PageIndex", pageIndex);
            command.Parameters.AddWithValue("@PageSize", pageSize);
            command.Parameters.Add("@TotalRecordsCount", System.Data.SqlDbType.Int);
            command.Parameters["@TotalRecordsCount"].Direction = System.Data.ParameterDirection.Output;
            con.Open();
            SqlDataReader dr = command.ExecuteReader();
            var users = new List<User>();
            while (dr.Read())
            {
                var user = new User
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    PersonalLogoFileStoreId = (dr["PersonalLogoFileStoreId"] == null || dr["PersonalLogoFileStoreId"] == DBNull.Value) ? null : (int?)Convert.ToInt32(dr["PersonalLogoFileStoreId"]),
                    CountryID = new Utility().ConvertToInt(dr["CountryID"] as string),
                    CurrentCompany = Convert.ToString(dr["CurrentCompany"]),
                    CurrentPosition = Convert.ToString(dr["CurrentRole"]),
                    IsUserFollowing = Convert.ToInt32(dr["FollowupCount"]) <= 0,
                    IsJobFollowing = Convert.ToInt32(dr["FollowupJobCount"]) <= 0
                };


                users.Add(user);
            }
            dr.Close();
            totalRecords = Convert.ToInt32(command.Parameters["@TotalRecordsCount"].Value);
            LoggingManager.Debug("Exiting GetUsersAndFriends - UserManager");
            return users;
        }
       
        public int IsUserFollowingCompany(int userid,int Companyid)
        {
            using (var context = new huntableEntities())
            {
                var user = context.Companies.FirstOrDefault(x => x.Id == Companyid);
                var result =
                    context.PreferredFeedUserUsers.Where(y => y.UserId == user.Userid && y.FollowingUserId == userid)
                           .ToList();
                return result.Count;

            }
        }
      

    }

}


