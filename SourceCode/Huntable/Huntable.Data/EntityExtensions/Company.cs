using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Snovaspace.Util.FileDataStore;

namespace Huntable.Data
{
    public partial class Company
    {
        public string CompanyImagePath
        {
            get { return new FileStoreService().GetDownloadUrl(CompanyLogoId); }
        }
        public int TotalJobs { get; set; }
        public bool IsUserFollowingCompany { get; set; }
        public bool IsUserNotFollowingCompany  { get { return !IsUserFollowingCompany; } }
        public string Url
        {
            get
            {

               
                var companyname = CompanyName ?? string.Empty;
                var location = TownCity ?? string.Empty;
                string industry = (CompanyIndustry != null) ? MasterIndustry.Description : string.Empty;

                if (location == string.Empty && industry != string.Empty)
                {
                    string companyurl = "companies/" + companyname.Replace("/", "") + "-" +
                                industry.Replace("/", "-") + "-" + Id;
                    return companyurl;
                }
                if (location != string.Empty && industry == string.Empty)
                {
                    string companyurl = "companies/" + companyname.Replace("/", "") + "-" +
                                location.Replace("/", "-") + "-" + Id;
                    return companyurl;
                }
                if (location != string.Empty && industry == string.Empty)
                {
                    string companyurl = "companies/" + companyname.Replace("/", "") +
                                                    "-" + Id;
                    return companyurl;
                }
                string companyurl1 = "companies/" + companyname.Replace("/", "") + "-" + location.Replace("/", "") + "-" +
                                 industry + "-" + Id;
                return companyurl1;
            }
        }
    }
}
