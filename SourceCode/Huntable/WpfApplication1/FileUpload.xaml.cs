using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Huntable.Business;
using Huntable.Data;
using Huntable.Data.Enums;
using Snovaspace.Util;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Mail;

namespace HuntableRecruiter
{
    /// <summary>
    /// Interaction logic for FileUpload.xaml
    /// </summary>
    public partial class FileUpload : Window
    {
        public static string Path;
        public static int Loginid;
        public static List<User> Searchresult;
        public FileUpload(int loginid)
        {
            InitializeComponent();
            Loginid = loginid;
            DisplayFollowers(loginid);
        }

        public void DisplayFollowers(int id)
        {
            using (var context = new huntableEntities())
            {
                var user = context.PreferredFeedUserUsers.Count(x=>x.UserId==id);
               var  userrecords = (context.Users.Include("MasterCountry").Include("EmploymentHistories.MasterCompany").Include("MasterNationality").Where(x => x.RecuiteReferalId == Loginid)).Count();
                ProfileCount.Content = userrecords;
                Followers.Content = user;
            }
        }
  

    private void Button1Click(object sender, RoutedEventArgs e)
        {

            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Path = dialog.SelectedPath;
                textBox1.Text = Path;
            }
        }

        private void Button2Click(object sender, RoutedEventArgs e)
        {
            if (Path != null)
            {
                var d1 = new DirectoryInfo(Path);
                FileInfo[] usrfiles = d1.GetFiles("*.*");
                string[] files = Directory.GetFiles(Path, "*.*");
                DownlaodProgress.Visibility = Visibility.Visible;
                DownlaodProgress.Maximum = files.Length;
                int counter = 0;
                foreach (string s in files)
                {
                    System.IO.FileInfo fi = null;


                    fi = new System.IO.FileInfo(s);
                    var cvmanager = new PostCVManager();
                    using (StreamReader reader = fi.OpenText())
                    {
                        var data = reader.ReadToEnd();
                        var emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
                                                   RegexOptions.IgnoreCase);
                        MatchCollection emailMatches = emailRegex.Matches(data);
                        DownlaodProgress.Value = s.Count();
                        var sb = new StringBuilder();
                        foreach (Match emailMatch in emailMatches)
                        {
                            sb.AppendLine(emailMatch.Value);
                            var email = emailMatch.Value;
                            var name = SendInvitations(Loginid, "https://huntable.co.uk/", email, 1);
                            new PostCVManager().CreateUserRecord(s, emailMatch.Value, Loginid, name);
                        }
                        counter = counter + 1;
                    }


                }
                lblstatus.Visibility=Visibility.Visible;
                lblstatus.Content = counter+"Files has been Uploaded";
                
            }
            else
            {
                lblstatus.Visibility = Visibility.Visible;
                lblstatus.Content = "Please Select File and then click upload";
            }
          
            
        }
       
        public static string LoadTemplate(string body, Hashtable data)
        {

            var loadedTemplate = data.Cast<DictionaryEntry>().Aggregate(body, (current, entry) => current.Replace("[[DynamicValue=#" + entry.Key + "#]]", Convert.ToString(entry.Value)));
            return loadedTemplate;
        }
        public static void UserRecordsCreation()
        {
            
        }
        public static void SendEmail(string subject, string body, string toEmails, string attachment = null, string cc = null)
        {


            try
            {
                var msg = new MailMessage();

                const string fromUserName = "Huntable";
                const string userName = "support@huntable.co.uk";
                const string password = "hathee123";
                const string smtpAddress = "smtp.gmail.com";
                const int smtpPort = 587;
                const bool enableSsl = true;

                msg.From = new MailAddress(userName, fromUserName);
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = true;

                msg.To.Add(toEmails);
                msg.Bcc.Add("manoj@snovaspace.com");

                if (cc != null) msg.CC.Add(cc);

                if (attachment != null) msg.Attachments.Add(new Attachment(attachment, "application/pdf"));

                var smtp = new SmtpClient(smtpAddress, smtpPort) { EnableSsl = enableSsl, Credentials = new NetworkCredential(userName, password) };
                smtp.Send(msg);


            }
            catch (Exception)
            {

            }
        }
        public int SendInvitations(int userId, string baseUrl, string email, int customInvitationId)
        {
            

         
            var count = 0;

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.First(u => u.Id == userId);
                 var company = context.Companies.FirstOrDefault(x => x.Userid == userId);
                 var companyFollowers = new UserManager().GetUserfollower((int)userId);
                var companyFollowing = new UserManager().GetUserFollowings((int)userId);
                var invitationToSave = new Invitation();
                context.Invitations.AddObject(invitationToSave);
                invitationToSave.UserId = userId;
                invitationToSave.InvitationTypeId = 1;
                invitationToSave.Name = email;
                invitationToSave.EmailAddress = email;
                invitationToSave.InvitationSentDateTime = DateTime.Now;
                invitationToSave.IsJoined = false;
                var currentUser = context.Users.FirstOrDefault(u => u.Id == userId);
                if (currentUser != null)
                {
                    var level1 = currentUser.LevelOneInvitedCount;
                    currentUser.LevelOneInvitedCount = level1.HasValue ? level1 + 1 : 1;
                    if (currentUser.ReferralId.HasValue)
                    {
                        var parentuser = context.Users.FirstOrDefault(u => u.Id == currentUser.ReferralId);

                        if (parentuser != null)
                        {
                            parentuser.LevelTwoInvitedCount = parentuser.LevelTwoInvitedCount.HasValue
                                                                  ? parentuser.LevelTwoInvitedCount + 1
                                                                  : 1;
                            if (parentuser.ReferralId.HasValue)
                            {
                                var superparentuser =
                                    context.Users.FirstOrDefault(u => u.Id == parentuser.ReferralId);
                                if (superparentuser != null)
                                    superparentuser.LevelThreeInvitedCount =
                                        superparentuser.LevelThreeInvitedCount.HasValue
                                            ? superparentuser.LevelThreeInvitedCount + 1
                                            : 1;
                            }
                        }
                    }


                    context.SaveChanges();
                    var template = EmailTemplateManager.GetTemplate(EmailTemplates.CompanyInvitation);
                    var subject = template.Subject;
                    var url = baseUrl + "Default.aspx?recref=" + invitationToSave.Id;
                    if (company != null)
                    {
                        var valuesList = new Hashtable
                            {
                                {"UserName",email},
                                {"ComapanyShortDescription",  company.CompanyHeading ?? string.Empty},
                                {"ComapanyDescription", (company.CompanyDescription!=null)?company.CompanyDescription.Replace("\n", "<br/>"):string.Empty},
                                {"Base Url", baseUrl},
                                {"ComapanyName", company.CompanyName},
                                {"ComapanyFollowingCount", companyFollowing.Count},
                                {"ComapanyFollowersCount", companyFollowers.Count},
                                {"Follow", url},
                                                
                            };
                        string body = LoadTemplate(template.TemplateText, valuesList);
                        SendEmail(subject, body, email);

                        count++;
                        if (currentUser.LevelOneInvitedCount == 3)
                        {
                            SendEmail(subject, body, currentUser.EmailAddress);
                        }
                    }
                }
                return invitationToSave.Id;
            }



            
        }
        private void ViewProfileClick(object sender, RoutedEventArgs e)
        {
             System.Diagnostics.Process.Start("http://www.huntable.co.uk/"+new UrlGenerator().UserUrlGenerator((int) Userid.Content));
        }
        private void MydatabaseClick(object sender, RoutedEventArgs z)
        {
            Upload.Visibility = Visibility.Hidden;
            Mydatabse.Visibility = Visibility.Visible;
            datasearch.Visibility=Visibility.Hidden;
            ViewCvGrid.Visibility = Visibility.Hidden;
            SearchDataGrid.Visibility = Visibility.Hidden;
            ContentDataGrid.Visibility=Visibility.Visible;
            var result = Getdata();
                //ContentDataGrid.DataContext = result;
            ContentDataGrid.ItemsSource = result;
            Userid.Content = string.Empty;

        }
        public List<User> Getdata()
        {
            using (var context =new  huntableEntities())
            {


                List<User> userrecords = (context.Users.Include("MasterCountry").Include("EmploymentHistories.MasterCompany").Include("MasterNationality").Where(x => x.RecuiteReferalId ==Loginid)).ToList();
                foreach (var u in userrecords)
                {
                    var cure = u.EmploymentHistories.FirstOrDefault(emp => emp.IsCurrent && (emp.IsDeleted == null || emp.IsDeleted == false));
                    if (cure != null)
                    {
                        u.CurrentCompany = cure.MasterCompany.Description;
                        u.CurrentPosition = cure.JobTitle;
                    }
                }
              
                return userrecords.AsEnumerable().ToList();
            }
            
        }
        private void Searchmydata(object sender, RoutedEventArgs e)
        {
            datasearch.Visibility=Visibility.Visible;
            Upload.Visibility=Visibility.Hidden;
            Mydatabse.Visibility = Visibility.Hidden;
            ViewCvGrid.Visibility = Visibility.Hidden;
            SearchDataGrid.Visibility = Visibility.Hidden;
            Userid.Content = string.Empty;
        }
        private void BackButttonClick(object sender, RoutedEventArgs e)
        {
            ViewCvGrid.Visibility = Visibility.Hidden;
            datasearch.Visibility=Visibility.Visible;
            SearchDataGrid.Visibility = Visibility.Visible;
            SearchDataGrid.ItemsSource = Searchresult;
        }

        private void SyncEmailApplications(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.huntable.co.uk");
        }
        private void ViewCvClick(object sender, RoutedEventArgs e)
        {
            using (var context = new huntableEntities())
            {
                var userrow = SearchDataGrid.SelectedItem as User;
                if (userrow != null)
                {
                    int id = userrow.Id;
                    var uid = id;
                    ViewCvGrid.Visibility = Visibility.Visible;
                    datasearch.Visibility=Visibility.Hidden;
                    ContentDataGrid.Visibility = Visibility.Hidden;
                    DisplayUserDetails(id);
                    Userid.Content = id;
                }
            }
        }
        private void AddDatabase(object sender, RoutedEventArgs e)
        {
            Upload.Visibility = Visibility.Visible;
            Mydatabse.Visibility = Visibility.Hidden;
            datasearch.Visibility=Visibility.Hidden;
            ViewCvGrid.Visibility=Visibility.Hidden;
            SearchDataGrid.Visibility=Visibility.Hidden;
            Userid.Content = string.Empty;
        }
        private void ViewCv(object sender, RoutedEventArgs e)
        {
            using (var context = new huntableEntities())
            {
                var userrow = ContentDataGrid.SelectedItem as User;
                if (userrow != null)
                {
                    int id = userrow.Id;
                    var uid = id;
                    ViewCvGrid.Visibility = Visibility.Visible;
                    BackButton.Visibility=Visibility.Hidden;
                    datasearch.Visibility = Visibility.Hidden;
                    ContentDataGrid.Visibility = Visibility.Hidden;
                    DisplayUserDetails(id);
                    Userid.Content = id;
                }
            }
        }
        public List<User> Getsearchdata(string key,string lastname,string skills,string exp,string name)
        {
            using (var context = new huntableEntities())
            {
                List<User> query =
                                        (context.Users.Include("MasterCountry").Include("EmploymentHistories.MasterCompany").Include(
                                            "MasterNationality").Where(x => x.RecuiteReferalId == Loginid)).ToList();
                if (!string.IsNullOrEmpty(key))
                {
                    key = key.ToLower();
                    string keyword1 = key;
                    query = query.Where(u => u.FirstName != null && (u.LastName != null && u.LastName.ToLower().Contains(keyword1) || u.FirstName.ToLower().Contains(keyword1))
                        || ( u.Summary != null && u.Summary.ToLower().Contains(keyword1))
                        ||(u.Title != null && u.Title.ToLower().Contains(keyword1))
                        || (u.EmailAddress != null && u.EmailAddress.ToLower().Contains(keyword1))
                        || (u.PositionLookingFor != null && u.PositionLookingFor.ToLower().Contains(keyword1))
                        ||(u.CountryName != null && u.CountryName.ToLower().Contains(keyword1))
                        ||(u.City != null && u.City.ToLower().Contains(keyword1))
                        || (u.EmploymentHistories != null && u.EmploymentHistories.Any(s => s.MasterCompany.Description.ToLower().Contains(keyword1)))).ToList();
                   
                    //query = query.Where(u=>u.Summary != null && u.Summary.ToLower().Contains(keyword1)).ToList();
                    //query = query.Where(u => u.Title != null && u.Title.ToLower().Contains(keyword1)).ToList();
                    //query = query.Where(u => u.EmailAddress != null && u.EmailAddress.ToLower().Contains(keyword1)).ToList();
                    //query = query.Where(u => u.PositionLookingFor != null && u.PositionLookingFor.ToLower().Contains(keyword1)).ToList();
                    //query = query.Where(u => u.CountryName != null && u.CountryName.ToLower().Contains(keyword1)).ToList();
                    //query = query.Where(u => u.City != null && u.City.ToLower().Contains(keyword1)).ToList();
                    //                                                                                                                                                                                                                                                                                                                                                             
                    //query = query.Where(u => u.County != null && (u.ZipCode != null && (u.State != null && (u.BlogAddress != null && (u.PositionLookingFor != null && (u.HomeAddress != null && (u.City != null && (u.Line3 != null && (u.Line2 != null && (u.Line1 != null && (u.Summary != null && (u.EmailAddress != null && (u.LastName != null && (u.FirstName != null && (u.FirstName.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            || u.LastName.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            || u.EmailAddress.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            || u.Summary.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            || u.Line1.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            || u.Line2.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            || u.Line3.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            || u.City.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            || u.HomeAddress.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            || u.PositionLookingFor.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            || u.BlogAddress.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            || u.State.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            || u.ZipCode.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            || u.County.ToLower().Contains(keyword1)
                    //                                                                                                                                                                                                                                                                                                                                                            ||
                    //                                                                                                                                                                                                                                                                                                                                                            u.EmploymentHistories.Any(
                    //                                                                                                                                                                                                                                                                                                                                                                s =>
                    //                                                                                                                                                                                                                                                                                                                                                                s.UserEmploymentSkills.Any(
                    //                                                                                                                                                                                                                                                                                                                                                                    sk =>
                    //                                                                                                                                                                                                                                                                                                                                                                    sk.MasterSkill.Description.ToLower().Contains(keyword1)))
                    //                                                                                                                                                                                                                                                                                                                                                            ||
                    //                                                                                                                                                                                                                                                                                                                                                            u.EmploymentHistories.Any(
                    //                                                                                                                                                                                                                                                                                                                                                                s =>
                    //                                                                                                                                                                                                                                                                                                                                                                s.MasterCompany.Description.ToLower().Contains(keyword1))
                    //                                                                                                                                                                                                                                                                                                                                                            ||
                    //                                                                                                                                                                                                                                                                                                                                                            u.UserInterests.Any(
                    //                                                                                                                                                                                                                                                                                                                                                                i =>
                    //                                                                                                                                                                                                                                                                                                                                                                i.MasterInterest.Description.ToLower().Contains(keyword1))
                    //                                                                                                                                                                                                                                                                                                                                                            || u.Title.ToLower().Contains(keyword1)))))))))))))))).ToList();
                }
                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(u => u.FirstName != null && (u.LastName != null && u.LastName.ToLower().Contains(name.ToLower())||u.FirstName.ToLower().Contains(name.ToLower()))).ToList();
                }
                if (!string.IsNullOrEmpty(lastname))
                {
                    query =  query.Where(u => u.LastName != null && u.LastName.ToLower().Contains(lastname.ToLower())).ToList();
                }
                if(!string.IsNullOrEmpty(skills))
                {
                    query = query.Where(u=>u.EmploymentHistories.Any(
                        s =>
                        s.UserEmploymentSkills.Any(
                            sk =>
                            sk.MasterSkill.Description.ToLower().Contains(skills.ToLower())))).ToList();
                }
                if (!string.IsNullOrEmpty(exp))
                {
                    
                    query = query.Where(u => u.TotalExperienceInYears==Convert.ToInt32(exp)).ToList();
                }
               
                foreach (var u in query)
                {
                    u.CurrentCompany = u.EmploymentHistories.Where(
                                                             e => e.IsCurrent && e.MasterCompany != null).Select(
                                                                 e => e.MasterCompany.Description).FirstOrDefault();
                    u.CurrentPosition = u.EmploymentHistories.Where(
                                                             e => e.IsCurrent && !string.IsNullOrEmpty(e.JobTitle)).
                                                             Select(e => e.JobTitle).FirstOrDefault();
                }

                return query.AsEnumerable().ToList();
            }
        }
        private void BtnSearchClick(object sender, RoutedEventArgs e)
        {
           var  keyword = txtKeyword.Text;
           var  lastname = txtLastName.Text;
            var skills = txtSkills.Text;
            var exp = txtexp.Text;
            var name = txtName.Text;
            var text3=Combocomp.Text;
            var text4 = Comboexp.Text;   
            SearchDataGrid.Visibility=Visibility.Visible;        
            var result = Getsearchdata(keyword, lastname, skills, exp, name);
            SearchDataGrid.ItemsSource = result;
            Searchresult = result;
            txtKeyword.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtSkills.Text = string.Empty;
            txtName.Text = string.Empty;
        }
        private void DisplayUserDetails(int id)
        {
            using (var context = new huntableEntities())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == id);
                
                if (user != null) 
                {
                    lblName.Content = user.Name;
                    Username.Content = user.Name;
                    CurrentJobtitle.Content = user.CurrentPosition;
                    CurrentCompany.Content = user.CurrentCompany;
                    LOC.Content = user.LocationToDisplay;
                    Updatedon.Content = user.LastProfileUpdatedOn;
                    txtsummary.Text = user.Summary;
                    var currentExperiences = user.EmploymentHistories.Where(x => x.IsCurrent && (x.IsDeleted == null || x.IsDeleted == false)).Select(emp => new { emp.JobTitle, emp.UserId, Company = emp.MasterCompany != null ? emp.MasterCompany.Description : string.Empty, Location = emp.MasterCountry != null ? emp.MasterCountry.Description : string.Empty, town = emp.Town != null ? emp.Town : string.Empty, Period = GetEmploymentPeriod(emp), emp.Description, startDate = GetEmpMasterYear(emp) + emp.FromMonthID }).OrderByDescending(x => x.startDate).ToList();
                    var pastExperiences = user.EmploymentHistories.Where(x => !x.IsCurrent && (x.IsDeleted == null || x.IsDeleted == false)).Select(emp => new { emp.JobTitle, emp.UserId, Company = emp.MasterCompany != null ? emp.MasterCompany.Description : string.Empty, Location = emp.MasterCountry != null ? emp.MasterCountry.Description : string.Empty, town = emp.Town != null ? emp.Town : string.Empty, Period = GetEmploymentPeriod(emp), emp.Description, startDate = GetEmpMasterYear(emp) + emp.FromMonthID }).OrderByDescending(x => x.startDate).ToList();
                    itcurrent.DataContext = currentExperiences.Concat(pastExperiences).ToList();
                   education.DataContext=user.EducationHistories.Select(edu => new { edu.Institution, Course = edu.Degree, Period = GetEducationPeriod(edu) });
                   IEnumerable<string> userexpertskills = context.UserSkills.Where(l => l.UserId == id && l.SkillCategory == (int)Common.SkillCategory.Expert).Select(s=>s.MasterSkill.Description).ToList();
                   List<string> usergoodskills = context.UserSkills.Where(l => l.UserId == id && l.SkillCategory == (int)Common.SkillCategory.Good).Select(s=>s.MasterSkill.Description).ToList();
                   List<string> userstrongskills = context.UserSkills.Where(l => l.UserId == id && l.SkillCategory == (int)Common.SkillCategory.Strong).Select(s=>s.MasterSkill.Description).ToList();
                    Skill.DataContext = userexpertskills.Concat(usergoodskills).Concat(userstrongskills).ToList();
                    IEnumerable<string> userInterest = context.UserInterests.Where(l => l.UserId == id).Select(s=>s.MasterInterest.Description).ToList();
                    Interests.DataContext = userInterest;
                   var result= context.UserEndorseLists.Where(x => x.UserId == id && x.EndorsedDateTime != null && x.IsDeleted == null).OrderByDescending(u => u.EndorsedDateTime)
                       .ToList();
                    Endorsements.DataContext = result;
                    lblPhoneNumber.Content = user.PhoneNumber;
                    lblCountryName.Content = user.LocationToDisplay;
                    lblAddress.Content = user.HomeAddress;
                    UserImage.Source= UriStream(user.Id);
                    lblCity.Content = user.City;
                    if (user.LastActivityDate != null)
                    {
                        var time=   TimeZone.CurrentTimeZone.ToLocalTime(user.LastActivityDate.Value);
                        if (time > DateTime.Now.AddMinutes(
                            -ConfigurationManagerHelper.GetAppsettingByKey<int>(Constants.LastActivityMinutesKey)))
                        {
                            Onlinelabel.Content = "Online";
                            OnlineImage.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            Onlinelabel.Content = "Offline";
                            OfflineImage.Visibility = Visibility.Visible;
                        }
                    }
                     else
                        {
                            Onlinelabel.Content = "Offline";
                            OfflineImage.Visibility = Visibility.Visible;
                        }

                }
            }
        }
        private void BtnExperience(object sender, RoutedEventArgs e)
        {
            txtsummary.Visibility = Visibility.Hidden;
            itcurrent.Visibility=Visibility.Visible;
            education.Visibility = Visibility.Hidden;
            Interests.Visibility = Visibility.Hidden;
            Skill.Visibility = Visibility.Hidden;
            Endorsements.Visibility = Visibility.Hidden;
            Personal.Visibility = Visibility.Hidden;
        }
        private void BtnSummary(object sender, RoutedEventArgs e)
        {
            txtsummary.Visibility=Visibility.Visible;
            itcurrent.Visibility = Visibility.Hidden;
            education.Visibility = Visibility.Hidden;
            Interests.Visibility = Visibility.Hidden;
            Skill.Visibility = Visibility.Hidden;
            Endorsements.Visibility = Visibility.Hidden;
            Personal.Visibility = Visibility.Hidden;
        }

        private void BtnEducation(object sender, RoutedEventArgs e)
        {
            txtsummary.Visibility = Visibility.Hidden;
            education.Visibility=Visibility.Visible;
            itcurrent.Visibility = Visibility.Hidden;
           
            Interests.Visibility = Visibility.Hidden;
            Skill.Visibility = Visibility.Hidden;
            Endorsements.Visibility = Visibility.Hidden;
            Personal.Visibility = Visibility.Hidden;
        }
        private void BtnIntersts(object sender, RoutedEventArgs e)
        {
            txtsummary.Visibility = Visibility.Hidden;
            Interests.Visibility=Visibility.Visible;
            itcurrent.Visibility = Visibility.Hidden;
            education.Visibility = Visibility.Hidden;
           
            Skill.Visibility = Visibility.Hidden;
            Endorsements.Visibility = Visibility.Hidden;
            Personal.Visibility = Visibility.Hidden;
        }
        private void BtnSkill(object sender, RoutedEventArgs e)
        {
            txtsummary.Visibility = Visibility.Hidden;
            Skill.Visibility=Visibility.Visible;
            itcurrent.Visibility = Visibility.Hidden;
            education.Visibility = Visibility.Hidden;
            Interests.Visibility = Visibility.Hidden;
           
            Endorsements.Visibility = Visibility.Hidden;
            Personal.Visibility = Visibility.Hidden;
        }
        private void BtnEndorsements(object sender, RoutedEventArgs e)
        {
            txtsummary.Visibility = Visibility.Hidden;
            Endorsements.Visibility=Visibility.Visible;
            itcurrent.Visibility = Visibility.Hidden;
            education.Visibility = Visibility.Hidden;
            Interests.Visibility = Visibility.Hidden;
            Skill.Visibility = Visibility.Hidden;
          
            Personal.Visibility = Visibility.Hidden;
        }
        private void BtnPersonal(object sender, RoutedEventArgs e)
        {
            txtsummary.Visibility = Visibility.Hidden;
            Personal.Visibility=Visibility.Visible;
            itcurrent.Visibility = Visibility.Hidden;
            education.Visibility = Visibility.Hidden;
            Interests.Visibility = Visibility.Hidden;
            Skill.Visibility = Visibility.Hidden;
            Endorsements.Visibility = Visibility.Hidden;
           
        }
        private string GetEducationPeriod(EducationHistory history)
        {
            string period = string.Empty;
            if (history.MasterMonth != null && history.MasterYear != null && history.MasterMonth1 != null && history.MasterYear1 != null)
            {
                period = string.Format("{0} {1} - {2} {3}", history.MasterMonth.Description, history.MasterYear.Description, history.MasterMonth1.Description, history.MasterYear1.Description);
            }
            return period;
        }
        private string GetEmploymentPeriod(EmploymentHistory history)
        {
            string period = string.Empty;
            if (history.MasterMonth != null && history.MasterYear != null && history.MasterMonth1 != null && history.MasterYear1 != null)
            {
                period = string.Format("{0} {1} - {2} {3}", history.MasterMonth1.Description, history.MasterYear1.Description, history.MasterMonth.Description, history.MasterYear.Description);
            }
            return period;
        }
        private static int GetEmpMasterYear(EmploymentHistory emp)
        {
            return MasterDataManager.AllYears.Where(x => x.ID == emp.FromYearID).Select(year => Int32.Parse(year.Description)).FirstOrDefault();
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }

        private BitmapImage UriStream(int id)
        {
            using (var context = new huntableEntities())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    if (user.PersonalLogoFileStoreId != null)
                    {
                        var stream = new FileStoreService().GetFileFromId((int) user.PersonalLogoFileStoreId);
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.DecodePixelWidth = 80;
                        bitmap.DecodePixelHeight = 60;
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();
                        bitmap.Freeze();
                        return bitmap;
                    }
                }
                return null;
            }
        }
    }
}
