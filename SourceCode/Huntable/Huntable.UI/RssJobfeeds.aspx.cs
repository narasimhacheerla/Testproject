using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.CSV;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    
    public partial class RssJobFeeds : System.Web.UI.Page
    {
        public static XmlTextReader Reader;
        public static XmlDocument Document;
        public static XmlNode Rss;
        public static XmlNode Channel;
        public static XmlNode Item;
        public static bool error;
        
       
        protected void UploadInvites(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering UploadInvites - RssJobFeeds.aspx");
            

          //  if (UploadContacts.HasFile)
           // {
                //string filename = System.IO.Path.GetFileName(UploadContacts.FileName);

                FileInfo file = new FileInfo("c://filest//raju.txt");
                var filestore = new FileStoreService();
                int? fileId = filestore.LoadFileFromFileUploadc("rssfeed", file);
                if (fileId.HasValue)
                {
                    Stream stream = filestore.GetFileFromId(fileId.Value);
               
               // UploadContacts.PostedFile.SaveAs("c://filest\\" + filename);

                using (StreamReader s = new StreamReader(stream))
                {
                   
                  
                    string li;
                    
                    List<string> lines = new List<string>();

                    while ((li = s.ReadLine()) != null)
                    {
                        
                       
                            string ln = li;
                      

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

                            string[,] datarray = new string[num, 3];
                            num = 0;

                            for (int i = 0; i < Channel.ChildNodes.Count; i++)
                            {
                                if (Channel.ChildNodes[i].Name == "item")
                                {
                                    string title, desciption, url;
                                    Item = Channel.ChildNodes[i];
                                    datarray[num, 0] = Item["title"].InnerText;
                                    datarray[num, 1] = Item["link"].InnerText;
                                    datarray[num, 2] = Item["description"].InnerText;
                                   
                                    title = datarray[num, 0];
                                    using (var con = huntableEntities.GetEntitiesWithNoLock())
                                    {
                                        var tit = con.Jobs.FirstOrDefault(x => x.Title == title);
                                        if (tit == null)
                                        {

                                            url = datarray[num, 1];
                                            desciption = datarray[num, 2];
                                            using (var context = huntableEntities.GetEntitiesWithNoLock())
                                            {
                                                var job = new Job
                                                {
                                                    Title = title,
                                                    JobDescription = desciption,
                                                    CompanyName = url,
                                                    CreatedDateTime = DateTime.Now,
                                                    DesiredCandidateProfile = "tttt",
                                                    UserId = 130,
                                                    IndustryId = 28,
                                                    JobTypeId = 1,
                                                    SectorId = 3,
                                                    CountryId = 6
                                                };
                                                context.Jobs.AddObject(job);
                                                context.SaveChanges();
                                            }

                                        }
                                    }
                                }
                            }
                       
                    }
                 
                     
                    }
                    
               // }

            }
            LoggingManager.Debug("Exiting UploadInvites - RssJobFeeds.aspx");
        }
    }
}

    

    
    

