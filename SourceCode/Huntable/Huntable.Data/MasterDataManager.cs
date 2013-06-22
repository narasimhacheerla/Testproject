using System.Collections.Generic;
using System.Linq;
using Snovaspace.Util.Logging;

namespace Huntable.Data
{
    public class MasterDataManager
    {
        public static void RefreshCache()
        {
            LoggingManager.Debug("Entering RefreshCache  -  MasterDataManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                context.MasterMonths.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterYears.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterCurrencyTypes.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterCountries.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterSkills.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterInterests.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterIndustries.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterNationalities.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterLanguages.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterJobTypes.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterCompanies.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterSalaries.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterMinimumSalaries.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterMaximumSalaries.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterMaximumSalaries.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterEmployees.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.Companies.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                context.MasterExpYears.MergeOption = System.Data.Objects.MergeOption.NoTracking;

                AllJobTypes = context.MasterJobTypes.OrderBy(x => x.Description).ToList();
                AllMonths = context.MasterMonths.OrderBy(x => x.ID).ToList();
                AllYears = context.MasterYears.ToList().OrderByDescending(x => x.Description).ToList();
                AllCurrencyTypes = context.MasterCurrencyTypes.OrderBy(x => x.Description).ToList();
                AllCountries = context.MasterCountries.OrderBy(x => x.Description).ToList();
                AllSkills = context.MasterSkills.OrderBy(x => x.Description).ToList();
                AllInterests = context.MasterInterests.OrderBy(x => x.Description).ToList();
                AllIndustries = context.MasterIndustries.OrderBy(x => x.Description).ToList();
                AllNationalities = context.MasterNationalities.OrderBy(x => x.Description).ToList();
                AllMasterLanguages = context.MasterLanguages.OrderBy(x => x.Description).ToList();
                AllCities = context.MasterCities.OrderBy(x => x.Description).ToList();
                AllMasterCompanies = context.MasterCompanies.OrderBy(x => x.Description).ToList();
                AllMasterSalaries = context.MasterSalaries.OrderBy(x => x.Salary).ToList();
                AllMasterMinimumSalary = context.MasterMinimumSalaries.OrderBy(x => x.MinimumSalary).ToList();
                AllMasterMaximumSalary = context.MasterMaximumSalaries.OrderBy(x => x.MaximumSalary).ToList();
                AllLevels = context.MasterLevels.OrderBy(x => x.Rank).ToList();
                AllEmployees = context.MasterEmployees.OrderBy(x => x.Id).ToList();
                SeoInfos = context.SeoInfoes.ToList();
                AllCompanies = context.Companies.ToList();
                AllExpYears = context.MasterExpYears.ToList();
            }

            LoggingManager.Debug("Exiting RefreshCache  -  MasterDataManager.cs");
        }

        static MasterDataManager()
        {
            RefreshCache();
        }

        public static List<SeoInfo> SeoInfos { get; private set; }

        public static List<Company> AllCompanies { get; private set; }

        public static List<MasterJobType> AllJobTypes { get; private set; }

        public static List<MasterMonth> AllMonths { get; private set; }

        public static List<MasterYear> AllYears { get; private set; }

        public static List<MasterCurrencyType> AllCurrencyTypes { get; private set; }

        public static List<Huntable.Data.MasterCountry> AllCountries { get; private set; }

        public static List<MasterLevel> AllLevels { get; private set; }

        public static List<MasterSkill> AllSkills { get; set; }

        public static List<MasterInterest> AllInterests { get; set; }

        public static List<MasterIndustry> AllIndustries { get; set; }
        public static List<MasterCity> AllCities { get; set; }

        public static List<MasterNationality> AllNationalities { get; set; }

        public static List<MasterLanguage> AllMasterLanguages { get; set; }

        public static List<MasterCompany> AllMasterCompanies { get; set; }

        public static List<MasterSalary> AllMasterSalaries { get; set; }
        public static List<MasterMinimumSalary> AllMasterMinimumSalary { get; set; }
        public static List<MasterMaximumSalary> AllMasterMaximumSalary { get; set; }

        public static List<MasterEmployee> AllEmployees { get; set; }

        public static List<MasterExpYear> AllExpYears { get; set; } 

        public static void AddNewSkills(params string[] skills)
        {
            LoggingManager.Debug("Entering AddNewSkills  -  MasterDataManager.cs");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var existingSkills = context.MasterSkills.Select(s => s.Description).ToList();
                var skillsToCreate = skills.Where(s => existingSkills.Any(es => es.ToLower() == s.ToLower())).ToList();
                foreach (var skill in skillsToCreate)
                {
                    context.MasterSkills.AddObject(new MasterSkill { Description = skill });
                }
                context.SaveChanges();
                
                context.MasterSkills.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                AllSkills = context.MasterSkills.ToList();
            }
            LoggingManager.Debug("Exiting AddNewSkills  -  MasterDataManager.cs");
            
        }

        public static int? GetSkillId(string skill)
        {
            LoggingManager.Debug("Entering GetSkillId  -  MasterDataManager.cs");
            int? skillId;
            if (AllSkills.Any(l => l.Description.ToLower() == skill.ToLower()))
            {
                skillId = AllSkills.First(l => l.Description.ToLower() == skill.ToLower()).Id;
            }
            else
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var newSkillToCreate = new MasterSkill { Description = skill };
                    context.MasterSkills.AddObject(newSkillToCreate);
                    context.SaveChanges();
                    skillId = newSkillToCreate.Id;
                    AllSkills = context.MasterSkills.ToList();
                }
            }
            LoggingManager.Debug("Exiting GetSkillId -  MasterDataManager.cs");

            return skillId;
          
           
        }

        public static void AddNewInterests(params string[] interests)
        {
            LoggingManager.Debug("Entering AddNewInterests  -  MasterDataManager.cs");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var interestsToCreate = interests.Where(i => interests.Any(ei => ei.ToLower() == i.ToLower())).ToList();
                foreach (var interest in interestsToCreate)
                {
                    context.MasterInterests.AddObject(new MasterInterest { Description = interest });
                }
                context.SaveChanges();

                context.MasterInterests.MergeOption = System.Data.Objects.MergeOption.NoTracking;
                AllInterests = context.MasterInterests.ToList();
            }
            LoggingManager.Debug("Exiting AddNewInterests  -  MasterDataManager.cs");
        }

        public static int? GetInterestId(string interest)
        {
            return AllInterests.Where(i => i.Description.ToLower() == interest.ToLower()).Select(i => i.Id).FirstOrDefault();
        }

        public static int? GetYearId(string year)
        {
            LoggingManager.Debug("Entering GetYearId  -  MasterDataManager.cs");
            if (AllYears.Any(y => y.Description == year))
            {
                return AllYears.First(y => y.Description == year).ID;
            }
            else
            {
                LoggingManager.Debug("Exiting GetYearId  -  MasterDataManager.cs");
                return null;
            }
        }

        public static int? GetMonthId(int month)
        {
            LoggingManager.Debug("Entering GetMonthId  -  MasterDataManager.cs");
            if (AllMonths.Any(m => m.ID == month))
            {
                return AllMonths.First(m => m.ID == month).ID;
            }
            else
            {
                LoggingManager.Debug("Exiting GetMonthId  -  MasterDataManager.cs");
                
                return null;
            }
        }

        /// <summary>
        /// Gets the language id for a given lang text if found in db. else creates the new lang and gets new id.
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static int GetLanguageId(string language)
        {
            LoggingManager.Debug("Entering GetLanguageId  -  MasterDataManager.cs");
            int? langId;
            if (AllMasterLanguages.Any(l => l.Description.ToLower() == language.ToLower()))
            {
                langId = AllMasterLanguages.First(l => l.Description.ToLower() == language.ToLower()).Id;
            }
            else
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var newLanguageToCreate = new MasterLanguage { Description = language };
                    context.MasterLanguages.AddObject(newLanguageToCreate);
                    context.SaveChanges();
                    langId = newLanguageToCreate.Id;
                    AllMasterLanguages = context.MasterLanguages.ToList();
                }
            }
            LoggingManager.Debug("Exiting GetLanguageId  -  MasterDataManager.cs");
            return langId.Value;
        }

        public static int GetMasterCompanyId(string companyName)
        {
            LoggingManager.Debug("Entering GetMasterCompanyId  -  MasterDataManager.cs");
            int? companyId;
            if (AllMasterCompanies.Any(l => l.Description.ToLower() == companyName.ToLower()))
            {
                companyId = AllMasterCompanies.First(l => l.Description.ToLower() == companyName.ToLower()).Id;
            }
            else
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    char[] letters = companyName.ToCharArray();
                    // upper case the first char
                    letters[0] = char.ToUpper(letters[0]);
                    var cmpnme = new string(letters);
                    var newCompanyToCreate = new MasterCompany { Description = cmpnme };
                    context.MasterCompanies.AddObject(newCompanyToCreate);
                    context.SaveChanges();
                    companyId = newCompanyToCreate.Id;
                    AllMasterCompanies = context.MasterCompanies.ToList();
                }
            }
            LoggingManager.Debug("Exiting GetMasterCompanyId  -  MasterDataManager.cs");

            return companyId.Value;
        }
      
    }
}
