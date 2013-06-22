using System;
using System.IO;
using System.Linq;
using System.Xml;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;

namespace Huntable.Business.BatchJobs
{
    public class JobFeeds
    {
        public static XmlTextReader Reader;
        public static XmlDocument Document;
        public static XmlNode Rss;
        public static XmlNode Channel;
        public static XmlNode Item;
        public static bool error;
        public void Run()
        {
            LoggingManager.Debug("Entering  Run -JobFeeds.cs");

            int jobsCount = 0;
            var file = new FileInfo("c://filest//huntablefeed.txt");
            var filestore = new FileStoreService();
            int? fileId = filestore.LoadFileFromFileUploadc("rssfeed", file);
            if (fileId.HasValue)
            {
                Stream stream = filestore.GetFileFromId(fileId.Value);

                using (var s = new StreamReader(stream))
                {
                    string li;

                    while ((li = s.ReadLine()) != null)
                    {
                        try
                        {
                            LoggingManager.Debug("Line = " + li);

                            string ln1 = li;
                            string[] words = ln1.Split('@');
                            string country = words[1];
                            string location = words[2];
                            string ln = words[3];

                            if ((ln.Length < 7) || (ln.Substring(0, 7).ToLower() != "http://"))
                            {
                                ln = "http://" + ln;
                            }
                            try
                            {
                                Reader = new XmlTextReader(ln);
                                Document = new XmlDocument();
                                Document.Load(Reader);
                            }

                            catch
                            {
                                error = true;
                                Console.WriteLine("An error occured while opening " + ln);
                            }

                            for (int i = 0; i < Document.ChildNodes.Count; i++)
                            {
                                if (Document.ChildNodes[i].Name == "rss")
                                {
                                    Rss = Document.ChildNodes[i];
                                }
                            }

                            for (int i = 0; i < Rss.ChildNodes.Count; i++)
                            {
                                if (Rss.ChildNodes[i].Name == "channel")
                                {
                                    Channel = Rss.ChildNodes[i];
                                }
                            }

                            int num = 0;
                            for (int i = 0; i < Channel.ChildNodes.Count; i++)
                            {
                                if (Channel.ChildNodes[i].Name == "item")
                                {
                                    num++;
                                }
                            }

                            var datarray = new string[num, 3];
                            num = 0;

                            for (int i = 0; i < Channel.ChildNodes.Count; i++)
                            {
                                try
                                {
                                    if (Channel.ChildNodes[i].Name == "item")
                                    {
                                        Item = Channel.ChildNodes[i];
                                        datarray[num, 0] = Item["title"].InnerText;
                                        datarray[num, 1] = Item["link"].InnerText;
                                        datarray[num, 2] = Item["description"].InnerText;

                                        string title = datarray[num, 0];
                                        string desciption = datarray[num, 2];
                                        using (var con = huntableEntities.GetEntitiesWithNoLock())
                                        {
                                            var tit = con.Jobs.FirstOrDefault(x => x.Title == title);
                                            if (tit == null)
                                            {
                                                using (var context = huntableEntities.GetEntitiesWithNoLock())
                                                {
                                                    LoggingManager.Debug("Loading Job = " + title);

                                                    int countryid;
                                                    string url = datarray[num, 1];
                                                    var countryname = con.MasterCountries.FirstOrDefault(x => x.Description == country);
                                                    if (countryname != null)
                                                    {
                                                        countryid = countryname.Id;
                                                    }
                                                    else
                                                    {
                                                        var cntry = new MasterCountry { Description = country };
                                                        con.AddToMasterCountries(cntry);
                                                        con.SaveChanges();
                                                        countryid = cntry.Id;
                                                    }
                                                    var job = new Job
                                                    {
                                                        Title = title,
                                                        JobDescription = desciption,
                                                        CompanyName = string.Empty,
                                                        CreatedDateTime = DateTime.Now.Date,
                                                        Salary = 0,
                                                        MinExperience = 0,
                                                        MaxExperience = 0,
                                                        DesiredCandidateProfile = string.Empty,
                                                        UserId = 130,
                                                        IsRssJob = true,
                                                        CountryId = countryid,
                                                        LocationName = location,
                                                        Url = url
                                                    };
                                                    context.Jobs.AddObject(job);
                                                    context.SaveChanges();
                                                    jobsCount++;
                                                }
                                            }
                                            else
                                            {
                                                LoggingManager.Debug("Ignoring Job = " + title);
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
                        catch (Exception exception)
                        {
                            LoggingManager.Error(exception);
                        }
                    }

                    using (var context = huntableEntities.GetEntitiesWithNoLock())
                    {
                        var jobsStatus = new Huntable.Data.JobsStatusDaily()
                        {
                            NoOfJobsUploaded = jobsCount,
                            DateTime = DateTime.Now.Date,
                            Type = "RSSFeeds"
                        };
                        context.JobsStatusDailies.AddObject(jobsStatus);
                        context.SaveChanges();
                    }
                }
                LoggingManager.Debug("Exiting  Run - JobFeeds.cs");
            }
        }
    }
}
