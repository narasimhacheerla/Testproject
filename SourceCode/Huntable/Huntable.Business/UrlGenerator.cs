using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Huntable.Data;

namespace Huntable.Business
{
    public class UrlGenerator
    {
        public string JobsUrlGenerator(int id)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var job = context.Jobs.Include("MasterCurrencyType").FirstOrDefault(x => x.Id == id);
                var companyname = (job.IsRssJob != true) ? job.CompanyName : string.Empty;
                var location = job.LocationName ?? string.Empty;
                int salary = (job.Salary != 0) ? job.Salary : 0;
                string jobtitle = job.Title.Replace(" ", "-");
                string companynm = companyname.Replace(" ", "-");
                
                
                if (companyname == string.Empty && location != string.Empty)
                {
                    string joburl = "jobs/" + Regex.Replace(jobtitle, "[^a-zA-Z0-9% ._]", "-") + "-" + Regex.Replace(location, "[^a-zA-Z0-9% ._]", string.Empty) + "-" +
                                    salary + "-" + id;
                    return joburl;
                }
                if (location == string.Empty && companyname != string.Empty)
                {
                    string joburl = "jobs/" + Regex.Replace(jobtitle, "[^a-zA-Z0-9% ._]", "-") + "-" + Regex.Replace(companynm, "[^a-zA-Z0-9% ._]", string.Empty) + "-" +
                                    salary + "-" + id;
                    return joburl;
                }
                if (location == string.Empty && companyname == string.Empty )
                {
                    string joburl = "jobs/" + Regex.Replace(jobtitle, "[^a-zA-Z0-9% ._]", "-") + "-" +
                                    salary + "-" + id;
                    return joburl;
                }
                string joburl1 = "jobs/" + Regex.Replace(jobtitle, "[^a-zA-Z0-9% ._]", "-") + "-" + Regex.Replace(companynm, "[^a-zA-Z0-9% ._]", string.Empty) + "-" + Regex.Replace(location, "[^a-zA-Z0-9% ._]", string.Empty) + "-" +
                                    salary + "-" + id;
                return joburl1;
            }
           
            
        }
        public string CompanyUrlGenerator(int id)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var company = context.Companies.Include("MasterIndustry").FirstOrDefault(x => x.Id == id);
                var companyname = (company.CompanyName != null) ? company.CompanyName : string.Empty;
                var location = company.TownCity ?? string.Empty;
                string industry = (company.CompanyIndustry != null) ? company.MasterIndustry.Description : string.Empty;
             
                if (location == string.Empty && industry != string.Empty)
                {
                    string companyurl = "companies/" + companyname.Replace("/", "") + "-" +
                                industry.Replace("/","-").Replace("&","") + "-" + id;
                    return companyurl;
                }
                if (location != string.Empty && industry == string.Empty)
                {
                    string companyurl = "companies/" + companyname.Replace("/", "") + "-" +
                                location.Replace("/", "-") + "-" + id;
                    return companyurl;
                }
                if (location != string.Empty && industry == string.Empty)
                {
                    string companyurl = "companies/" + companyname.Replace("/", "") + 
                                                    "-" + id;
                    return companyurl;
                }
                string companyurl1 = "companies/" + companyname.Replace("/", "") + "-" + location.Replace("/", "") + "-" +
                                 industry.Replace("&", "").Replace("/", "-") + "-" + id;
                return companyurl1;
            }
        }
        public string UserUrlGenerator(int id)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == id);
                string firstname = user != null && (user.FirstName != null) ? user.FirstName : string.Empty;
                string lastname = user.LastName ?? string.Empty;
                string title = user.CurrentPosition ?? string.Empty;
                string location = user.City ?? string.Empty;
                if (title == string.Empty && location != string.Empty)
                {
                    string userurl = "users/" + firstname + "-" + lastname + "-" + location.Replace(" ", "-") + "-" + id;
                    return userurl;
                }
                if (location == string.Empty && title != string.Empty)
                {
                    string userurl = "users/" + firstname + "-" + lastname + "-" + title.Replace(" ","-") + "-" + id;
                    return userurl;
                }
                if (location == string.Empty && title == string.Empty)
                {
                    string userurl = "users/" + firstname + "-" + lastname   + "-" + id;
                    return userurl;
                }

                string userurl1 = "users/" + firstname + "-" + lastname + "-" + title.Replace(" ", "-") + "-" + location.Replace(" ", "-") + "-" + id;
                return userurl1;
            }
        }
        public string UserTextUrlGenerator(int id)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == id);
                string firstname = (user.FirstName != null) ? user.FirstName : string.Empty;
                string lastname = user.LastName ?? string.Empty;
                string title = user.CurrentPosition ?? string.Empty;
                string location = user.City ?? string.Empty;
                if (title == string.Empty && location != string.Empty)
                {
                    string userurl = "users/text/" + firstname + "-" + lastname + "-" + location.Replace(" ", "-") + "-" + id;
                    return userurl;
                }
                if (location == string.Empty && title != string.Empty)
                {
                    string userurl = "users/text/" + firstname + "-" + lastname + "-" + title.Replace(" ", "-") + "-" + id;
                    return userurl;
                }
                if (location == string.Empty && title == string.Empty)
                {
                    string userurl = "users/text/" + firstname + "-" + lastname +"-" + id;
                    return userurl;
                }

                string userurl1 = "users/text/" + firstname + "-" + lastname + "-" + title.Replace(" ", "-") + "-" + location.Replace(" ", "-") + "-" + id;
                return userurl1;
            }
        }
        public string UserActivityUrlGenerator(int id)
        {
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == id);
                string firstname = (user.FirstName != null) ? user.FirstName : string.Empty;
                string lastname = user.LastName ?? string.Empty;
                string title = user.CurrentPosition ?? string.Empty;
                string location = user.City ?? string.Empty;
                if (title == string.Empty && location != string.Empty)
                {
                    string userurl = "users/activity/" + firstname + "-" + lastname + "-" + location.Replace(" ", "-") + "-" + id;
                    return userurl;
                }
                if (location == string.Empty && title != string.Empty)
                {
                    string userurl = "users/activity/" + firstname + "-" + lastname + "-" + title.Replace(" ", "-") + "-" + id;
                    return userurl;
                }
                if (location == string.Empty && title == string.Empty)
                {
                    string userurl = "users/activity/" + firstname + "-" + lastname  +"-" + id;
                    return userurl;
                }

                string userurl1 = "users/activity/" + firstname + "-" + lastname + "-" + title.Replace(" ", "-") + "-" + location.Replace(" ", "-") + "-" + id;
                return userurl1;
            }
        }
    }
}
