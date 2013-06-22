using System;
using Huntable.Data.Enums;
using Snovaspace.Util;
using Snovaspace.Util.FileDataStore;
using System.Linq;
using Snovaspace.Util.Logging;

namespace Huntable.Data
{
    public partial class Invitation
    {
        public InvitationType InvitationType
        {
            get
            {
                if (InvitationTypeId != null) return (InvitationType)InvitationTypeId;
                return InvitationType.Email;
            }
            set { InvitationTypeId = (int)value; }
        }
    }
    public partial class News
    {
        public string NewsImageBasePath
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(PicturePathId);
            }
        }


    }

    public partial class CompanyPortfolio
    {
        public string NewsImageBasePath
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(PortfolioImageid);
            }
        }


    }
  
    public partial class User
    {
       
        public int? GetCompanyForUser()
        {
            LoggingManager.Debug("Entering GetCompanyForUser  -  User.cs");

            EmploymentHistory currentEmploymentHistory = EmploymentHistories.FirstOrDefault(x => x.IsCurrent);

            if (currentEmploymentHistory == null || !currentEmploymentHistory.CompanyId.HasValue)
            {
                return null;
            }
            LoggingManager.Debug("Exiting GetCompanyForUser  -  User.cs");

            return currentEmploymentHistory.MasterCompany.Id;
        }

        public bool IsUserOnline
        {
            get
            {
                if (LastActivityDate.HasValue && LastActivityDate.Value > DateTime.Now.AddMinutes(-ConfigurationManagerHelper.GetAppsettingByKey<int>(Constants.LastActivityMinutesKey)))
                {
                    return true;
                }
                return false;
            }
        }
       
        public bool IsUserFollowing { get; set; }
        public bool IsUserNotFollowing { get { return !IsUserFollowing; } }
        public bool IsJobFollowing { get; set; }

        public string MemberAvailabilityMessage
        {
            get
            {
                if (IsUserOnline)
                {
                    return "ONLINE NOW";
                }
                return "OFFLINE";
            }
        }

        public string UserAvailabilityImagePath
        {
            get
            {
                return IsUserOnline ? ConfigurationManagerHelper.GetAppsettingByKey<string>(Constants.UserOnlineImagePathKey) : ConfigurationManagerHelper.GetAppsettingByKey<string>(Constants.UserOfflineImagePathKey);
            }
        }

        public string UserAvailabilityInformation
        {
            get
            {
                return IsUserOnline ? "Yes" : "No";
            }
        }
        public bool UserHasCompany
        {
            get
            {
                return HasCompany != null && HasCompany.Value? true : false;
                
            }
        }
        public string UserProfileUpdated
        {
            get { return IsVerified.HasValue ? "Updated" : "Notupdated"; }
        }
        public string UserProfilePictureDisplayUrl
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(PersonalLogoFileStoreId);
            }
        }

        public string CompanyLogoPictureDisplayUrl
        {
            get
            {
                return new FileStoreService().GetDownloadUrl(CompanyLogoFileStoreId);
            }
        }


        public string AffliateAmountAsText
        {
            get
            {
                return "Total : $" + ((LevelOnePremiumCount.HasValue ? LevelOnePremiumCount : 0) * 4 + (LevelTwoPremiumCount.HasValue ? LevelTwoPremiumCount : 0) * 1 + (LevelThreePremiumCount.HasValue ? LevelThreePremiumCount : 0) * .5).ToString();
            }
        }

        private string _currentPosition;

        public string CurrentPosition
        {
            get
            {
                if (string.IsNullOrEmpty(_currentPosition))
                {
                    _currentPosition = EmploymentHistories.Where(e => e.IsCurrent && !string.IsNullOrEmpty(e.JobTitle)).Select(e => e.JobTitle).FirstOrDefault();
                }
                return _currentPosition;
            }
            set { _currentPosition = value; }
        }




        private string _currentCompany;

        public string CurrentCompany
        {
            get
            {
                if (string.IsNullOrEmpty(_currentCompany))
                {
                    _currentCompany = EmploymentHistories.Where(e => e.IsCurrent && e.MasterCompany != null).Select(e => e.MasterCompany.Description).FirstOrDefault();
                }
                return _currentCompany;
            }
            set
            {
                _currentCompany = value;
            }
        }

        public string PastPositions { get; set; }

        public string CountryName
        {
            get
            {
                return MasterDataManager.AllCountries.Where(x => CountryID != null && x.Id == CountryID.Value).Select(x => x.Description).FirstOrDefault();
            }
        }

        public string LocationToDisplay
        {
            get
            {
                return string.Format("{0}, {1}", this.City, this.CountryName);
            }
        }

        public int ProfileVisitedCount { get; set; }

        public string PastPosition { get; set; }

        public string PastCompany { get; set; }

        public string Name
        {
            get { return FirstName + " " + LastName; }
        }
        public string Url
        {
            get
            {
                string firstname =   FirstName ?? string.Empty;
                string lastname = LastName ?? string.Empty;
                string title = CurrentPosition ?? string.Empty;
                string location = City ?? string.Empty;
                if (title == string.Empty && location != string.Empty)
                {
                    string userurl = "users/" + firstname + "-" + lastname + "-" + location.Replace(" ", "-") + "-" + Id;
                    return userurl;
                }
                if (location == string.Empty && title != string.Empty)
                {
                    string userurl = "users/" + firstname + "-" + lastname + "-" + title.Replace(" ", "-") + "-" + Id;
                    return userurl;
                }
                if (location == string.Empty && title == string.Empty)
                {
                    string userurl = "users/" + firstname + "-" + lastname + "-" + Id;
                    return userurl;
                }

                string userurl1 = "users/" + firstname + "-" + lastname + "-" + title.Replace(" ", "-") + "-" + location.Replace(" ", "-") + "-" + Id;
                return userurl1;
            }
        }
    }
}
