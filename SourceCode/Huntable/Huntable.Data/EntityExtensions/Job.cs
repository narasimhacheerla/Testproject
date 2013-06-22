using System.Text.RegularExpressions;
using Snovaspace.Util.FileDataStore;
using System.Linq;
using System;

namespace Huntable.Data
{
    public partial class Job
    {
        public string CountryName
        {
            get
            {
                var country = MasterDataManager.AllCountries.FirstOrDefault(x => x.Id == CountryId);
                if (country != null) return country.Description;
                return null;
            }
        }

        public string ProfileImagePath { get; set; }

        public bool IsUserAlreadyToThisJob { get; set; }
        public bool IsUserNotAppliedToThisJob { get{ return !IsUserAlreadyToThisJob; } }
        public string PhotoThumbDisplayPath1
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(PhotoThumbPath1);
            }
        }

        public string PhotoThumbDisplayPath2
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(PhotoThumbPath2);
            }
        }

        public string PhotoThumbDisplayPath3
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(PhotoThumbPath3);
            }
        }

        public string PhotoThumbDisplayPath4
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(PhotoThumbPath4);
            }
        }

        public string PhotoThumbDisplayPath5
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(PhotoThumbPath5);
            }
        }

        public string PhotoDisplayPath1
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(PhotoPath1);
            }
        }

        public string PhotoDisplayPath2
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(PhotoPath2);
            }
        }

        public string PhotoDisplayPath3
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(PhotoPath3);
            }
        }

        public string PhotoDisplayPath4
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(PhotoPath4);
            }
        }

        public string PhotoDisplayPath5
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(PhotoPath5);
            }
        }

        public bool IsAlreadyApplied { get; set; }
        public bool IsAlreadyNotApplied { get { return !IsAlreadyApplied ; }}
        public DateTime AppliedDate { get; set; }

        public int TotalApplicants { get; set; }

        public string CurrencyDescription
        {
            get
            {
                if (SalaryCurrencyId != null)
                {
                    return MasterDataManager.AllCurrencyTypes.Where(x => x.ID == SalaryCurrencyId).Select(x => x.Description).FirstOrDefault();
                }
                else
                {
                    return MasterDataManager.AllCurrencyTypes.Where(x => x.ID == 22).Select(x => x.Description).FirstOrDefault();
                }
            }
        }

        public string Symbol { get; set; }
        public string UrlJob
        {
            get
            {
              
                var companyname = (IsRssJob != true) ? CompanyName : string.Empty;
                var location = LocationName ?? string.Empty;
                int salary = (Salary != 0) ? Salary : 0;
                string jobtitle = Title.Replace(" ", "-");
                string companynm = companyname.Replace(" ", "-");


                if (companyname == string.Empty && location != string.Empty)
                {
                    string joburl = "jobs/" + Regex.Replace(jobtitle, "[^a-zA-Z0-9% ._]", "-") + "-" + Regex.Replace(location, "[^a-zA-Z0-9% ._]", string.Empty) + "-" +
                                    salary + "-" + Id;
                    return joburl;
                }
                if (location == string.Empty && companyname != string.Empty)
                {
                    string joburl = "jobs/" + Regex.Replace(jobtitle, "[^a-zA-Z0-9% ._]", "-") + "-" + Regex.Replace(companynm, "[^a-zA-Z0-9% ._]", string.Empty) + "-" +
                                    salary + "-" + Id;
                    return joburl;
                }
                if (location == string.Empty && companyname == string.Empty)
                {
                    string joburl = "jobs/" + Regex.Replace(jobtitle, "[^a-zA-Z0-9% ._]", "-") + "-" +
                                    salary + "-" + Id;
                    return joburl;
                }
                string joburl1 = "jobs/" + Regex.Replace(jobtitle, "[^a-zA-Z0-9% ._]", "-") + "-" + Regex.Replace(companynm, "[^a-zA-Z0-9% ._]", string.Empty) + "-" + Regex.Replace(location, "[^a-zA-Z0-9% ._]", string.Empty) + "-" +
                                    salary + "-" + Id;
                return joburl1;
            }
        }
    }
}
