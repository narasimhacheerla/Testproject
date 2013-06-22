using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Huntable.Data;
using Snovaspace.Util.Logging;

//using Huntable.Data;


namespace HuntableJobs
{

    class Program
    {
        private static string _urlMatchingRegularExpression;
        private static string _allhRefsEregularExpression;
        private static int _shineJobsCount = 0;
        static void Main(string[] args)
        {
            LoggingManager.Debug("Entering into shine Jobs loading");
            LoadAllJobsForNaukri();
        }

        private static void LoadAllJobsForShine()
        {
            string paging = "";
            int numOfPages = 0;
            string[] websiteUrls = { "http://www.shine.com/", "http://www.shine.com/job-search/#functionalarea/" };
            foreach (string weburl in websiteUrls)
            {
                string eachurl = weburl;


                string feed = eachurl;
                MatchCollection ms = Regex.Matches(feed, @"(http.+)([\s]|$)");
                if (ms.Count > 0)
                {
                    #region link sharing

                    string url = ms[0].Value.ToString();
                    var webGet = new HtmlWeb();
                    var document = webGet.Load(url);


                    //testing
                    var mainnodes = document.DocumentNode.SelectNodes("//a[@href]");
                    if (mainnodes != null)
                    {
                        List<string> mainnodes2 =
                            mainnodes.ToList()
                                     .ConvertAll(r => r.Attributes.ToList().ConvertAll(i => i.Value))
                                     .SelectMany(j => j)
                                     .ToList();

                        foreach (string mn in mainnodes2)
                        {
                            // string mn = "http://www.shine.com/job-search/simple/?rect=72381";
                            string innerUrl = "";
                            if (mn.ToLower().Contains("job-search/simple"))
                            {

                                if (mn.ToLower().Contains("www.shine.com")) //Type-1
                                {
                                    innerUrl = mn;
                                }
                                else //Type-2
                                {
                                    innerUrl = "http://www.shine.com" + mn;
                                }
                                if (innerUrl != "")
                                {
                                    //var subdocument = webGet.Load(innerUrl);

                                    //var paging =
                                    //    subdocument.DocumentNode.SelectNodes("//div[@class='pagination searchpagination']");
                                    //if (paging != null)
                                    //{
                                    //    _paging = paging.FirstOrDefault().InnerText;
                                    //    if (_paging != "")
                                    //    {
                                    //        _paging = _paging.Substring(_paging.Length - 5, 5);
                                    //        _paging = new String(_paging.Where(x => char.IsNumber(x)).ToArray());
                                    //        if (_paging != "")
                                    //        {
                                    //            NumOfPages = Convert.ToInt32(_paging);
                                    //        }
                                    //        else
                                    //        {
                                    //            NumOfPages = 0;
                                    //        }

                                    //    }
                                    //    else
                                    //    {
                                    //        NumOfPages = 1;
                                    //    }

                                    //}
                                    //Initially loading page 1 jobs
                                    var PageOneJobs = webGet.Load(innerUrl);
                                    var pageOneNodes = PageOneJobs.DocumentNode.SelectNodes("//a[@class='cls_searchresult_a']");
                                    if (pageOneNodes != null)
                                    {

                                        List<string> pageOneNodesList =
                                                pageOneNodes.ToList()
                                                     .ConvertAll(r => r.Attributes.ToList().ConvertAll(i => i.Value))
                                                     .SelectMany(j => j)
                                                     .ToList();

                                        foreach (string n in pageOneNodesList)
                                        {
                                            LoadJobDetails(n);

                                        }
                                    }
                                    //Initially loading page 1 jobs End

                                    int pagingEnd = 0;
                                    for (int eachpage = 1; pagingEnd == 0; eachpage++)
                                    {
                                        string pagingUrl = innerUrl + "?&page=" + eachpage.ToString();
                                        var outerDocument = webGet.Load(pagingUrl);

                                        var nodes = outerDocument.DocumentNode.SelectNodes("//a[@class='cls_searchresult_a']");
                                        //checking PageingEnd
                                        string pagedivFirst = "", pagedivPrev = "";
                                        //string pagingurlPresent = innerUrl + "?&page=" + eachpage.ToString();
                                        //var pagingDocumentFirst = webGet.Load(pagingurlPresent);
                                        var pagingnodescount = outerDocument.DocumentNode.SelectNodes("//div[@class='chkboxlen txmyjob']");

                                        if (pagingnodescount != null)
                                        {
                                            pagedivFirst = pagingnodescount.FirstOrDefault().InnerText;
                                        }
                                        string pagingurlPrev = innerUrl + "?&page=" + (eachpage - 1).ToString();
                                        var pagingDocumentPrev = webGet.Load(pagingurlPrev);
                                        var pagingnodesPrev = pagingDocumentPrev.DocumentNode.SelectNodes("//div[@class='chkboxlen txmyjob']");

                                        if (pagingnodesPrev != null)
                                        {
                                            pagedivPrev = pagingnodesPrev.FirstOrDefault().InnerText;
                                        }
                                        //finding error message
                                        //var errmsg = pagingDocumentPrev.DocumentNode.SelectNodes("//strong[@style='font-size:18px;color:#ff6600;padding:0px 0px 10px 0px;']");
                                        //if (errmsg != null)
                                        //{
                                        //    string errtxt = errmsg.FirstOrDefault().InnerText;
                                        //    if (errtxt != "")
                                        //    {
                                        //        pagedivPrev = errtxt;
                                        //    }
                                        //}
                                        if (pagedivFirst == pagedivPrev)
                                        {

                                            pagingEnd++;
                                            continue;

                                        }
                                        //checking PageingEnd
                                        if (nodes != null)
                                        {
                                            List<string> nodes2 =
                                                nodes.ToList()
                                                     .ConvertAll(r => r.Attributes.ToList().ConvertAll(i => i.Value))
                                                     .SelectMany(j => j)
                                                     .ToList();


                                            foreach (string n in nodes2)
                                            {
                                                LoadJobDetails(n);

                                            }
                                        }
                                    }
                                }
                            }
                            if (mn.ToLower().Contains("/jobs/")) //type-3
                            {
                                innerUrl = "http://www.shine.com" + mn;
                                LoadJobDetails(innerUrl);

                            }

                            //Top companies tab

                            string urltest = "";
                            if (mn.Length > 4)
                            {
                                urltest = mn.Substring(mn.Length - 4, 3);
                                urltest = new String(urltest.Where(x => char.IsNumber(x)).ToArray());

                            }

                            if (mn.ToLower().Contains("www.shine.com") && urltest != "")
                            {
                                LoadTopCompanies(mn);

                            }

                            //Top companies tab End


                        }
                    }
                    #endregion
                }
            }
            LoadNumberOFjobsUploaded("Shine", _shineJobsCount);
        }

        private static void LoadAllJobsForNaukri()
        {
            LoggingManager.Debug("Entering into shine Jobs loading");
            int count = 0, jobsCount = 0;
            string jobdescription = "", jobTitle = "";
            string education01 = "",
                   industry = "",
                   functionalArea = "",
                   location = "",
                   keywords = "",
                   contact = "",
                   jobposted = "",
                   reference = "",
                   telephone01 = "",
                   email01 = "",
                   _salary = "",
                   companyProfile = "",
                   companyName = "",
                   minExpericence = "",
                   maxExpericence = "";
            string feed = "http://www.naukri.com/";

            _urlMatchingRegularExpression = @"(http.+)([\s]|$)";

            MatchCollection ms = Regex.Matches(feed, _urlMatchingRegularExpression);

            if (ms.Count > 0)
            {
                #region link sharing

                string url = ms[0].Value;
                var webGet = new HtmlWeb();
                var document = webGet.Load(url);

                LoggingManager.Debug(url + " loaded");

                _allhRefsEregularExpression = "//a[@href]";
                var nodes = document.DocumentNode.SelectNodes(_allhRefsEregularExpression);
                if (nodes != null)
                {
                    try
                    {

                        List<string> nodes2 =
                            nodes.ToList().ConvertAll(r => r.Attributes.ToList().ConvertAll(i => i.Value)).SelectMany(j => j).ToList();


                        foreach (string mainPageUrlNode in nodes2)
                        {
                            //if (mainPageUrlNode == "http://www.naukri.com/tieups/tieups.php?othersrcp=14705")
                            //{
                            //    continue;
                            //}
                            if (mainPageUrlNode.Contains("http://www.naukri.com/tieups/tieups.php?othersrcp="))
                            {
                                //handling Exception
                                //if (n.Contains("http://www.naukri.com/tieups/tieups.php?othersrcp=15688"))
                                //{
                                //    continue;
                                //}
                                //if (n.Contains("http://www.naukri.com/tieups/tieups.php?othersrcp=14602"))
                                //{
                                //    continue; 
                                //}
                                //if (n.Contains("http://www.naukri.com/tieups/tieups.php?othersrcp=14605"))
                                //{
                                //    continue;
                                //}
                                if (mainPageUrlNode == "http://www.naukri.com/tieups/tieups.php?othersrcp=14705")
                                {
                                    continue;
                                }


                                var newfeed = mainPageUrlNode.Trim();
                                MatchCollection ms1 = Regex.Matches(newfeed, _urlMatchingRegularExpression);
                                string newurl = ms1[0].Value.ToString();
                                var newwebGet = new HtmlWeb();
                                // var s11=  newwebGet.ResponseUri;

                                var doc1 = newwebGet.Load(mainPageUrlNode);
                                LoggingManager.Debug(mainPageUrlNode + " loaded");

                                var xpath = "//meta[@http-equiv='refresh' and contains(@content, 'URL')]";
                                var refresh = doc1.DocumentNode.SelectSingleNode(xpath);
                                if (refresh != null)
                                {
                                    var content = refresh.Attributes["content"].Value;
                                    var finalUrl = Regex.Match(content, @"\s*URL\s*=\s*([^ ;]+)").Groups[1].Value.Trim();
                                    //inner part
                                    var doc2 = newwebGet.Load(finalUrl);

                                    LoggingManager.Debug(finalUrl + " loaded");

                                    var innernodes = doc2.DocumentNode.SelectNodes(_allhRefsEregularExpression);
                                    if (innernodes != null)
                                    {
                                        if (innernodes != null)
                                        {
                                            List<string> innernodes2 =
                                                innernodes.ToList()
                                                          .ConvertAll(r => r.Attributes.ToList().ConvertAll(i => i.Value))
                                                          .SelectMany(j => j)
                                                          .ToList();


                                            foreach (string Inode in innernodes2)
                                            {
                                                try
                                                {

                                                    if (Inode.Contains("http://jobsearch.naukri.com/job-listings"))
                                                    {
                                                        //handling Exception
                                                        if (Inode ==
                                                            "http://jobsearch.naukri.com/job-listings----0-to-0-?xp=1&xz=7_0_5&xo=14580&id=&xid=136482588463701200&qc=20951&qs=r&qt=all&enableRoleMapping=y&mode=7")
                                                        {
                                                            continue;
                                                        }
                                                        if (Inode ==
                                                            "http://jobsearch.naukri.com/job-listings-Sr-collections-Manager-Delhi-Frankfinn-Institute-of-Air-Hostess-Training-Delhi-10-to-20-160413000666?xp=1&xz=7_0_5&xo=&id=0d6f7c5b774bbc8f8a5cf22fe48755f622e2bb0598c50fb4ff003c62a2e1a36431b890266d0ecd01|X|Y|X|E&xid=136611930294256500&qc=499125&qs=p&qt=all&enableRoleMapping=y&mode=7")
                                                        {
                                                            continue;
                                                        }


                                                        var innernewfeed = Inode.Trim();
                                                        MatchCollection Innerms = Regex.Matches(innernewfeed, _urlMatchingRegularExpression);
                                                        string Innernewurl = Innerms[0].Value.ToString();


                                                        var InnerDoc = newwebGet.Load(Inode);
                                                        // Differentiating between the two kinds


                                                        var diffTypes = InnerDoc.DocumentNode.SelectNodes("//*[@id='rtBanner']");
                                                        if (diffTypes != null) // FIRST KIND OF JOBS
                                                        {
                                                            var jobPostName = InnerDoc.DocumentNode.SelectNodes("//*[@class='jobTitle']");
                                                            if (jobPostName != null)
                                                            {
                                                                jobTitle = jobPostName.FirstOrDefault().InnerText; //JOB POST NAME  / JOBTitle
                                                            }


                                                            var place = InnerDoc.DocumentNode.SelectNodes("//*[@class='jobDet']");
                                                            if (place != null)
                                                            {
                                                                string _place = place.FirstOrDefault().InnerText;
                                                                if (_place.ToLower().Contains("in"))
                                                                {
                                                                    string[] lines = Regex.Split(_place, "in");
                                                                    if (lines.Count() >= 2)
                                                                    {
                                                                        companyName = lines[0];
                                                                        location = lines[1]; //PLACE    / Location
                                                                        companyName = companyName.Remove(0, 3); //COMPANY NAME
                                                                    }
                                                                }
                                                            }


                                                            // for finding  EXPERIENCE and OPENINGS 
                                                            var exp = InnerDoc.DocumentNode.SelectNodes("//*[@class='fl']");
                                                            if (exp != null)
                                                            {
                                                                foreach (HtmlNode exp1 in exp)
                                                                {
                                                                    string Expce = exp1.InnerText;
                                                                    if (Expce.ToLower().Contains("experience"))
                                                                    {
                                                                        string _Expce = Expce;
                                                                        string Expericence = _Expce.Remove(0, 26);

                                                                        if (Expericence.ToLower().Contains("to"))
                                                                        {
                                                                            Expericence = Expericence.Substring(0, 10); //EXPERIENCE

                                                                            string[] minAndMaxExpericence = Regex.Split(Expericence, "to");
                                                                            minExpericence = minAndMaxExpericence[0];
                                                                            maxExpericence = minAndMaxExpericence[1];
                                                                        }
                                                                        else
                                                                        {
                                                                            minExpericence = "0";
                                                                            maxExpericence = "0";
                                                                        }


                                                                        //int pos1 = Expericence.IndexOf("Experience") + 5;
                                                                        // Expericence = Expericence.Remove(Expericence.Length-3,3 );
                                                                    }
                                                                    if (Expce.ToLower().Contains("opening"))
                                                                    {
                                                                        string _openings = Expce;
                                                                        _openings = _openings.Substring(_openings.Length - 3, 3);
                                                                        //OPENINGS
                                                                    }
                                                                }
                                                            }
                                                            //or finding JOB DESCRIPTION
                                                            var jobDescription = InnerDoc.DocumentNode.SelectNodes("//*[@class='jdDesc']");
                                                            if (jobDescription != null)
                                                            {
                                                                jobdescription = jobDescription.FirstOrDefault().InnerText; //JOB DESCRIPTION
                                                            }


                                                            var jd = InnerDoc.DocumentNode.SelectNodes("//*[@class='jdRow']");
                                                            if (jd != null)
                                                            {
                                                                foreach (HtmlNode _jd in jd)
                                                                {
                                                                    string jobdetails = _jd.InnerText;
                                                                    if (jobdetails.ToLower().Contains("salary"))
                                                                    {
                                                                        string[] salarysplit = Regex.Split(jobdetails, "\n");
                                                                        _salary = salarysplit[1]; //SALARY
                                                                    }
                                                                    if (jobdetails.ToLower().Contains("industry"))
                                                                    {
                                                                        string[] industrySplit = Regex.Split(jobdetails, "\n");
                                                                        industry = industrySplit[1]; //INDUSTRY
                                                                    }
                                                                    if (jobdetails.ToLower().Contains("functional"))
                                                                    {
                                                                        string[] functionalAreaSplit = Regex.Split(jobdetails, "\n");
                                                                        string functonalArea = functionalAreaSplit[1]; //FUNCTIONAL AREA
                                                                    }
                                                                    if (jobdetails.ToLower().Contains("role category"))
                                                                    {
                                                                        string[] roleCategeorySplit = Regex.Split(jobdetails, "\n");
                                                                        string roleCategeory = roleCategeorySplit[1]; //ROLE CATEGEORY
                                                                    }
                                                                    if (jobdetails.ToLower().Contains("role"))
                                                                    {
                                                                        string[] roleSplit = Regex.Split(jobdetails, "\n");
                                                                        string _role = roleSplit[1]; //ROLE
                                                                    }
                                                                    if (jobdetails.ToLower().Contains("keyskills"))
                                                                    {
                                                                        string[] keySkillsSplit = Regex.Split(jobdetails, "\n");
                                                                        string keySkills = keySkillsSplit[1]; // KEY SKILLS
                                                                    }
                                                                    if (jobdetails.ToLower().Contains("education"))
                                                                    {
                                                                        string[] educationSplit = Regex.Split(jobdetails, "\n");
                                                                        if (educationSplit.Count() >= 8)
                                                                        {
                                                                            string education = educationSplit[1] + educationSplit[2] +
                                                                                               educationSplit[3] + educationSplit[4] +
                                                                                               educationSplit[6] + educationSplit[7];
                                                                            education01 = education.Replace("   ", ""); //EDUCATION
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            var _compProfile =
                                                                InnerDoc.DocumentNode.SelectNodes("//*[@class='companyProf noPortFolio']");
                                                            if (_compProfile != null)
                                                            {
                                                                var compProfile = _compProfile.FirstOrDefault();

                                                                if (compProfile != null)
                                                                {
                                                                    string comp = compProfile.InnerText;
                                                                    comp = comp.Replace("\n", "");
                                                                    comp = comp.Replace("\r", "");
                                                                    if (comp.Length > 305)
                                                                    {
                                                                        companyProfile = comp.Remove(comp.Length - 305, 305); //COMPANY PROFILE
                                                                    }
                                                                    else
                                                                    {
                                                                        companyProfile = comp.Substring(0, 20);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                var _compProfile01 =
                                                                    InnerDoc.DocumentNode.SelectNodes("//*[@class='companyProf w590']");
                                                                if (_compProfile01 != null)
                                                                {
                                                                    var compProfile01 = _compProfile01.FirstOrDefault();
                                                                    if (compProfile01 != null)
                                                                    {
                                                                        string comp = compProfile01.InnerText;
                                                                        comp = comp.Replace("\n", "");
                                                                        comp = comp.Replace("\r", "");
                                                                        companyProfile = comp.Remove(comp.Length - 305, 305); //COMPANY PROFILE
                                                                    }
                                                                }
                                                            }
                                                            // Recruiters Logo
                                                            var recruitersLogo = InnerDoc.DocumentNode.SelectNodes("//*[@class='recruiterLogo']");
                                                            if (recruitersLogo != null)
                                                            {
                                                                foreach (HtmlNode logoNode in recruitersLogo)
                                                                {
                                                                    HtmlNode imageNode = logoNode.SelectSingleNode("//img");
                                                                    HtmlAttribute src = imageNode.Attributes["src"];
                                                                    string compayLogo = src.Value; // COMPANY LOGO
                                                                }
                                                            }
                                                            // Query String for Job identification

                                                            int QueryCount = 0, QueryCount1 = 0;
                                                            string NewQueryString = "";
                                                            if (Inode.ToLower().Contains("xp="))
                                                            {
                                                                string[] SplitUrl = Regex.Split(Inode, "xp=");
                                                                string s1 = SplitUrl[0];
                                                                string s2 = SplitUrl[1];
                                                                string QueryString = s1.Substring(s1.Length - 13, 12);
                                                                foreach (char c in QueryString)
                                                                {
                                                                    if (!Char.IsDigit(c))
                                                                        QueryCount1++;
                                                                    if (QueryCount1 == 0)
                                                                    {
                                                                        NewQueryString = QueryString;
                                                                    }
                                                                }
                                                            }
                                                            //
                                                            string QueryString1 = Inode.Substring(Inode.Length - 12, 12);

                                                            //
                                                            //string[] SplitUrl1 = Regex.Split(Inode, "xz=");
                                                            //string s11 = SplitUrl1[0];
                                                            //string s22 = SplitUrl1[1];
                                                            //string QueryString2 = s11.Substring(s11.Length - 13, 12);


                                                            foreach (char c in QueryString1)
                                                            {
                                                                if (!Char.IsDigit(c))
                                                                    QueryCount++;
                                                                if (QueryCount == 0)
                                                                {
                                                                    NewQueryString = QueryString1;
                                                                }
                                                            }


                                                            //foreach (char c in QueryString2)
                                                            //{
                                                            //    if (!Char.IsDigit(c))
                                                            //        QueryCount2++;
                                                            //    if (QueryCount2 == 0)
                                                            //    {
                                                            //        NewQueryString = QueryString2;
                                                            //    }

                                                            //}

                                                            string _NewQueryString = NewQueryString;
                                                            string conatctdetailsUrl =
                                                                "http://jobsearch.naukri.com/mynaukri/getContactDetails.php?encId=" +
                                                                _NewQueryString + "&rnum=976";
                                                            var contactDetails = webGet.Load(conatctdetailsUrl);
                                                            var _conatctDetails = contactDetails.DocumentNode.SelectNodes("//div[@class='cls']");
                                                            if (_conatctDetails != null)
                                                            {
                                                                foreach (
                                                                    HtmlNode node in
                                                                        contactDetails.DocumentNode.SelectNodes("//div[@class='cls']"))
                                                                {
                                                                    foreach (HtmlNode node2 in node.SelectNodes(".//span"))
                                                                    {
                                                                        string details = node2.InnerText;
                                                                        if (details.ToLower() == "recruiter name:")
                                                                        {
                                                                            HtmlNode Rname = node.SelectNodes(".//p").FirstOrDefault();
                                                                            string RecruiterName = Rname.InnerText; // Recruiter Name
                                                                        }
                                                                        if (details.ToLower() == "contact company:")
                                                                        {
                                                                            HtmlNode _ContactComapy =
                                                                                node.SelectNodes(".//p").FirstOrDefault();
                                                                            string contactCompany = _ContactComapy.InnerText;
                                                                            //Contact Company:
                                                                        }
                                                                        if (details.ToLower() == "address:")
                                                                        {
                                                                            HtmlNode _address =
                                                                                node.SelectNodes(".//p").FirstOrDefault();
                                                                            string address = _address.InnerText; //Address
                                                                        }
                                                                        if (details.ToLower() == "telephone:")
                                                                        {
                                                                            HtmlNode _telephone =
                                                                                node.SelectNodes(".//p").FirstOrDefault();
                                                                            string telephone = _telephone.InnerText; //Telephone  
                                                                        }
                                                                        if (details.ToLower() == "email id:" ||
                                                                            details.ToLower() == "email address:")
                                                                        {
                                                                            HtmlNode _email = node.SelectNodes(".//p").FirstOrDefault();
                                                                            string email = _email.InnerText; //Email
                                                                        }
                                                                        if (details.ToLower() == " reference id:")
                                                                        {
                                                                            HtmlNode _referenceID =
                                                                                node.SelectNodes(".//p").FirstOrDefault();
                                                                            string referenceID = _referenceID.InnerText; //Reference Id
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            int CountOfNodes;
                                                            var companyDetails = InnerDoc.DocumentNode.SelectNodes("//*[@class='detailJob']");
                                                            if (companyDetails != null)
                                                            {
                                                                companyProfile = companyDetails.FirstOrDefault().InnerText;
                                                                //COMPANY DETAILS / Company Profile
                                                            }


                                                            var CountOfNodesNodes = InnerDoc.DocumentNode.SelectNodes("//td");
                                                            if (CountOfNodesNodes != null)
                                                            {
                                                                CountOfNodes = CountOfNodesNodes.Count();
                                                            }
                                                            else { CountOfNodes = 0; }


                                                            if (CountOfNodes > 0)
                                                            {
                                                                foreach (HtmlNode link in InnerDoc.DocumentNode.SelectNodes("//td"))
                                                                {
                                                                    if (link.InnerText == "Designation" && count == 0)
                                                                    {
                                                                        count = 1;
                                                                    }
                                                                    if (link.InnerText != "Designation" && count == 1)
                                                                    {
                                                                        jobTitle = link.InnerText; //Designation  / JOBTITLE
                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText == "Job Description" && count == 0)
                                                                    {
                                                                        count = 2;
                                                                    }
                                                                    if (link.InnerText != "Job Description" && count == 2)
                                                                    {
                                                                        jobdescription = link.InnerText; //Job description
                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText == "Desired Profile " && count == 0)
                                                                    {
                                                                        count = 3;
                                                                    }
                                                                    if (link.InnerText != "Desired Profile " && count == 3)
                                                                    {
                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText.ToLower().Contains("experience") && count == 0)
                                                                    {
                                                                        count = 4;
                                                                    }
                                                                    if ((!link.InnerText.ToLower().Contains("experience")) && count == 4)
                                                                    {
                                                                        string experience = link.InnerText;
                                                                        if (experience.Contains("-") && experience.ToLower().Contains("years"))
                                                                        {
                                                                            string[] minAndMax = experience.Split('-');
                                                                            if (minAndMax.Count() == 2)
                                                                            {
                                                                                minExpericence = minAndMax[0];
                                                                                maxExpericence = minAndMax[1];
                                                                                maxExpericence =
                                                                                    maxExpericence.Remove(maxExpericence.Length - 8, 8);
                                                                            }
                                                                        }

                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText.ToLower().Contains("industry type") && count == 0)
                                                                    {
                                                                        count = 5;
                                                                    }
                                                                    if ((!link.InnerText.ToLower().Contains("industry type")) && count == 5)
                                                                    {
                                                                        industry = link.InnerText; //Industry
                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText == "Role" && count == 0)
                                                                    {
                                                                        count = 6;
                                                                    }
                                                                    if (link.InnerText != "Role" && count == 6)
                                                                    {
                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText.ToLower().Contains("functional area") && count == 0)
                                                                    {
                                                                        count = 7;
                                                                    }
                                                                    if ((!link.InnerText.ToLower().Contains("functional area")) && count == 7)
                                                                    {
                                                                        functionalArea = link.InnerText; //Functional area
                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText.ToLower().Contains("education") && count == 0)
                                                                    {
                                                                        count = 8;
                                                                    }
                                                                    if ((!link.InnerText.ToLower().Contains("education")) && count == 8)
                                                                    {
                                                                        education01 = link.InnerText; //education
                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText.ToLower().Contains("location") && count == 0)
                                                                    {
                                                                        count = 9;
                                                                    }
                                                                    if ((!link.InnerText.ToLower().Contains("location")) && count == 9)
                                                                    {
                                                                        location = link.InnerText; // location
                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText.ToLower().Contains("keywords") && count == 0)
                                                                    {
                                                                        count = 10;
                                                                    }
                                                                    if ((!link.InnerText.ToLower().Contains("keywords")) && count == 10)
                                                                    {
                                                                        keywords = link.InnerText; // keyworde
                                                                        count = 0;
                                                                    }

                                                                    if ((link.InnerText.ToLower().Contains("contact")) && count == 0)
                                                                    {
                                                                        count = 11;
                                                                    }
                                                                    if ((!link.InnerText.ToLower().Contains("contact")) && count == 11)
                                                                    {
                                                                        contact = link.InnerText;
                                                                        contact = contact.Replace("\r", "");
                                                                        contact = contact.Replace("\n", "");
                                                                        contact = contact.Replace("\t", ""); //contact
                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText == "Website" && count == 0)
                                                                    {
                                                                        count = 12;
                                                                    }
                                                                    if (link.InnerText != "Website" && count == 12)
                                                                    {
                                                                        string website = link.InnerText;
                                                                        website = website.Replace("\r", "");
                                                                        website = website.Replace("\n", ""); //website
                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText.ToLower().Contains("job posted") && count == 0)
                                                                    {
                                                                        count = 13;
                                                                    }
                                                                    if ((!link.InnerText.ToLower().Contains("job posted")) && count == 13)
                                                                    {
                                                                        jobposted = link.InnerText; // Job Posted
                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText.ToLower().Contains("reference") && count == 0)
                                                                    {
                                                                        count = 14;
                                                                    }
                                                                    if ((!link.InnerText.ToLower().Contains("reference")) && count == 14)
                                                                    {
                                                                        reference = link.InnerText; // reference
                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText.ToLower().Contains("telephone") && count == 0)
                                                                    {
                                                                        count = 15;
                                                                    }
                                                                    if ((!link.InnerText.ToLower().Contains("telephone")) && count == 15)
                                                                    {
                                                                        telephone01 = link.InnerText; // telephone
                                                                        count = 0;
                                                                    }

                                                                    if (link.InnerText.ToLower().Contains("email") && count == 0)
                                                                    {
                                                                        count = 16;
                                                                    }
                                                                    if ((!link.InnerText.ToLower().Contains("email")) && count == 16)
                                                                    {
                                                                        email01 = link.InnerText; // email
                                                                        count = 0;
                                                                    }

                                                                    if ((link.InnerText.ToLower().Contains("compensation")) && count == 0)
                                                                    {
                                                                        count = 17;
                                                                    }
                                                                    if ((!link.InnerText.ToLower().Contains("compensation")) && count == 17)
                                                                    {
                                                                        _salary = link.InnerText; // Compensation / SALARY
                                                                        count = 0;
                                                                    }
                                                                }
                                                            }
                                                        }


                                                        //Checking does Expericence is having only neumaric or not
                                                        int mincount = 0, maxcount = 0;
                                                        foreach (char c in minExpericence)
                                                        {
                                                            if (!Char.IsDigit(c))
                                                            {
                                                                if (!(c == ' '))
                                                                {
                                                                    mincount++;
                                                                }
                                                            }
                                                            if (mincount > 0)
                                                            {
                                                                minExpericence = "0";
                                                            }
                                                        }
                                                        foreach (char c in maxExpericence)
                                                        {
                                                            if (!Char.IsDigit(c))
                                                            {
                                                                if (!(c == ' '))
                                                                {
                                                                    maxcount++;
                                                                }
                                                            }
                                                            if (maxcount > 0)
                                                            {
                                                                maxExpericence = "0";
                                                            }
                                                        }
                                                        if (jobdescription.Length > 4990)
                                                        {
                                                            jobdescription = jobdescription.Substring(0, 4990);
                                                        }
                                                        if (companyProfile.Length > 4990)
                                                        {
                                                            companyProfile.Substring(0, 4990);
                                                        }
                                                        if (education01.Length > 4990)
                                                        {
                                                            education01 = education01.Substring(0, 4990);
                                                        }

                                                        using (var context = huntableEntities.GetEntitiesWithNoLock())
                                                        {
                                                            var duplicatejobs = context.Jobs.FirstOrDefault(c => c.Title.ToLower() == jobTitle.ToLower() && c.CompanyName.ToLower() == companyName.ToLower());
                                                            if (duplicatejobs == null)
                                                            {
                                                                var jobs = new Huntable.Data.Job()
                                                                    {
                                                                        Title = jobTitle,
                                                                        CompanyName = companyName,
                                                                        JobDescription = jobdescription + " - Via Naukri",
                                                                        CreatedDateTime = DateTime.Now.Date,
                                                                        MinExperience =
                                                                            (minExpericence == "")
                                                                                ? 0
                                                                                : Convert.ToInt32(minExpericence),
                                                                        MaxExperience =
                                                                            (maxExpericence == "")
                                                                                ? 0
                                                                                : Convert.ToInt32(maxExpericence),
                                                                        AboutYourCompany = companyProfile,
                                                                        LocationName = location,
                                                                        DesiredCandidateProfile = education01,
                                                                        CountryId = 108, //INDIA
                                                                        //IndustryId = Convert.ToInt32(IndustryId(industry)),
                                                                        Salary = SalaryAmount(_salary),
                                                                        // JobTypeId = 1,
                                                                        //SectorId = 3,
                                                                        UserId = 130,
                                                                        IsRssJob = true,
                                                                        Url = Inode,
                                                                        IsNaukri = true
                                                                    };
                                                                context.Jobs.AddObject(jobs);
                                                                context.SaveChanges();
                                                                jobsCount++; // For counting number of jobs uploadded
                                                            }
                                                        }
                                                    }
                                                }
                                                catch (Exception exception)
                                                {
                                                    LoggingManager.Error(exception);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }

                    }
                    catch (Exception exception)
                    {
                        LoggingManager.Error(exception);
                    }
                }
                #endregion

                LoadNumberOFjobsUploaded("Naukri", jobsCount);
            }
        }


        public static int SalaryAmount(string salary)
        {
            string test = salary;
            string number = new String(test.Where(x => char.IsNumber(x)).ToArray());
            if (number == "")
            {
                return 0;
            }
            else
            {
                if (salary.Contains("-"))
                {
                    string[] sal = Regex.Split(salary, "-");
                    string newsalary = sal[0];
                    newsalary = new String(newsalary.Where(x => char.IsNumber(x)).ToArray());
                    if (newsalary == "")
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(newsalary);
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public static int IndustryId(string industry)
        {
            int mIndustryId = 0;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {

                List<MasterIndustry> indus = context.MasterIndustries.ToList();
                foreach (MasterIndustry masterIndustry in indus)
                {
                    if (masterIndustry.Description.ToLower().Contains(industry.ToLower()))
                    {
                        mIndustryId =
                        masterIndustry.Id;
                    }
                }
            }
            if (mIndustryId == 0)
            {
                return 5;
            }
            else
            {
                return mIndustryId;
            }
        }

        public static void LoadJobDetails(string n)
        {
            int _salary = 0;
            string title = "";
            string companyName = "", location = "", maxExperience = "0", minExperience = "0", _jobdescription = "";
            var webGet = new HtmlWeb();
            if (n.ToLower().Contains("/jobs"))
            {
                string innerurl = "http://www.shine.com" + n;
                var innerDocument = webGet.Load(innerurl);

                //reading job details from page
                var titleAndCompanyName =
                    innerDocument.DocumentNode.SelectNodes("//div[@class='jdtittle white']");

                if (titleAndCompanyName != null)
                {
                    string _titleAndCompanyName = titleAndCompanyName.FirstOrDefault().InnerText;
                    if (_titleAndCompanyName.Contains(","))
                    {
                        string[] TCArray = Regex.Split(_titleAndCompanyName, ",");
                        title = TCArray[0]; // Title
                        if (title.ToLower().Contains("&#39;"))
                        {
                            title = title.Replace("&#39;", "'");
                        }
                        string companyNameAndDate = TCArray[1];
                        if (companyNameAndDate.ToLower().Contains("posted"))
                        {
                            string[] _companyNameAndDate = Regex.Split(companyNameAndDate, "Posted");
                            companyName = _companyNameAndDate[0]; //CompanyName
                        }
                    }

                }

                var details =
                    innerDocument.DocumentNode.SelectNodes("//li[@class='jobrequirment_bottom_mid1']");
                if (details != null)
                {
                    foreach (var detail in details)
                    {
                        string _detail = detail.InnerText;
                        if (_detail.ToLower().Contains("location"))
                        {
                            if (_detail.Length >= 9)
                            {
                                location = _detail.Remove(0, 8); //Location
                            }

                        }
                        if (_detail.ToLower().Contains("experience"))
                        {
                            if (_detail.Length >= 11)
                            {
                                string experience = _detail.Remove(0, 10);
                                if (experience.Contains("-"))
                                {
                                    string[] minAndMaxExperience = Regex.Split(experience, "-");
                                    minExperience = minAndMaxExperience[0]; // Min Experience
                                    minExperience =
                                        new String(minExperience.Where(x => char.IsNumber(x)).ToArray());
                                    maxExperience = minAndMaxExperience[1];
                                    maxExperience = maxExperience.Remove(maxExperience.Length - 4, 4);
                                    maxExperience =
                                        new String(maxExperience.Where(x => char.IsNumber(x)).ToArray());
                                    // Max Expericence
                                }
                                else if (experience.Contains("to"))
                                {
                                    string[] minAndMaxExperience = Regex.Split(experience, "to");
                                    minExperience = minAndMaxExperience[0]; // Min Experience
                                    minExperience =
                                        new String(minExperience.Where(x => char.IsNumber(x)).ToArray());
                                    maxExperience = minAndMaxExperience[1];
                                    // maxExperience = maxExperience.Remove(maxExperience.Length - 4, 4);
                                    maxExperience =
                                        new String(maxExperience.Where(x => char.IsNumber(x)).ToArray());
                                }
                                else if (experience.Contains("Yrs"))
                                {
                                    maxExperience = new String(experience.Where(x => char.IsNumber(x)).ToArray());
                                    minExperience = "0";
                                }
                                else
                                {
                                    minExperience = "0";
                                    maxExperience = "0";
                                }
                            }
                        }
                        if (_detail.ToLower().Contains("salary"))
                        {
                            if (_detail.Length >= 7)
                            {
                                string salary = _detail.Remove(0, 6);

                                string[] typesSalary = Regex.Split(salary, "/");

                                if (typesSalary[0].Contains("-"))
                                {
                                    string[] salSplit = Regex.Split(typesSalary[0], "-");
                                    string minSalary = salSplit[0];
                                    minSalary = minSalary.Remove(0, 2);
                                    minSalary = minSalary.Replace(" ", "");
                                    if (minSalary.Contains("."))
                                    {
                                        decimal _sal = Convert.ToDecimal(minSalary);
                                        _salary = Convert.ToInt32(_sal * 100000); //Salary
                                    }
                                    else
                                    {
                                        string salWithoutComma =
                                            new String(minSalary.Where(x => char.IsNumber(x)).ToArray());
                                        if (salWithoutComma != "")
                                        {
                                            _salary = Convert.ToInt32(salWithoutComma);
                                        }
                                    }

                                }
                                else
                                {
                                    string saltype2 =
                                        new String(typesSalary[0].Where(x => char.IsNumber(x)).ToArray());
                                    if (saltype2 != "")
                                    {
                                        _salary = Convert.ToInt32(saltype2);
                                    }
                                }
                            }
                        }
                    }
                }

                var jobDescription = innerDocument.DocumentNode.SelectNodes("//div[@class='jd_inside']");
                if (jobDescription != null)
                {
                    _jobdescription = jobDescription.FirstOrDefault().InnerText;
                    // here we are getting contineous text (because newline is reading as &nbsp;) , if we want the same format as given in the site , we can replace the  &nbsp with the newline charecter so that they are divided into lines
                    // for now i am reading it as a  contineous series of text by removing &nbsp
                    _jobdescription = _jobdescription.Replace("&nbsp;", " ");
                    _jobdescription = _jobdescription.Replace("&amp;", " ");
                    _jobdescription = _jobdescription.Replace("&ldquo;", " "); //JobDescription

                }

                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    if (minExperience == "") { minExperience = "0"; }
                    if (maxExperience == "") { maxExperience = "0"; }
                    int minExp = Convert.ToInt32(minExperience);
                    int maxExp = Convert.ToInt32(maxExperience);
                    var DuplicateJob = context.Jobs.FirstOrDefault(c => c.Title == title && c.CompanyName.ToLower() == companyName.ToLower()
                        && c.MinExperience == minExp && c.MaxExperience == maxExp);
                    if (DuplicateJob == null)
                    {

                        var jobs = new Huntable.Data.Job()
                            {
                                Title = title,
                                CompanyName = companyName,
                                JobDescription = _jobdescription + " - Via Shine",
                                CreatedDateTime = DateTime.Now.Date,
                                MinExperience =
                                    (minExperience == "")
                                        ? 0
                                        : Convert.ToInt32(minExperience),
                                MaxExperience =
                                    (maxExperience == "")
                                        ? 0
                                        : Convert.ToInt32(maxExperience),
                                AboutYourCompany = "",
                                LocationName = location,
                                DesiredCandidateProfile = "",
                                CountryId = 108, //INDIA
                                //IndustryId = Convert.ToInt32(IndustryId(industry)),
                                Salary = _salary,
                                // JobTypeId = 1,
                                //SectorId = 3,
                                UserId = 130,
                                IsRssJob = true,
                                Url = innerurl,
                                IsShine = true
                            };
                        context.Jobs.AddObject(jobs);
                        context.SaveChanges();
                        _shineJobsCount++; // For counting number of jobs uploadded
                    }
                }
                //pnlExpertSkill.Controls.Add(
                //    new LiteralControl("<tr><td style='border:1px solid red;'>" +
                //                       title +
                //                       "</td><td style='border:1px solid red;'>" +
                //                       companyName +
                //                       "</td><td style='border:1px solid red;'>" +
                //                       _salary +
                //                       "</td><td style='border:1px solid red;'>" +
                //                       minExperience + "-" + maxExperience +
                //                       "</td><td style='border:1px solid red;'>" +
                //                       location +
                //                       "</td><td style='border:1px solid red;'>" +
                //                       _jobdescription +
                //                       "</td></tr>"));
            }
        }

        public static void LoadTopCompanies(string companiesUrl)
        {
            string mn = companiesUrl;
            var webGet1 = new HtmlWeb();
            string type2Url = "";
            if (mn.ToLower().Contains("http://www.shine.com/microsite"))
            {
                type2Url = mn + "?tab=2";

            }
            else
            {
                type2Url = mn;
            }
            // var document001 = webGet1.Load(mn);
            var document001 = webGet1.Load(type2Url);
            var compnodes = document001.DocumentNode.SelectNodes("//a[@class='cls_searchresult_a']");
            if (compnodes != null)
            {
                List<string> compnodes2 =
                    compnodes.ToList()
                             .ConvertAll(r => r.Attributes.ToList().ConvertAll(i => i.Value))
                             .SelectMany(j => j)
                             .ToList();

                foreach (string n in compnodes2)
                {
                    LoadJobDetails(n);

                }
            }


        }

        public static void LoadNumberOFjobsUploaded(string jobType, int numberOfJobs)
        {
            string _jobtype = jobType;
            int _numberOfJobs = numberOfJobs;
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var jobsStatus = new Huntable.Data.JobsStatusDaily()
                {
                    NoOfJobsUploaded = _numberOfJobs,
                    DateTime = DateTime.Now,
                    Type = _jobtype
                };
                context.JobsStatusDailies.AddObject(jobsStatus);
                context.SaveChanges();
            }
        }
    }
}
