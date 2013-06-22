using System;
using System.Linq;
using System.Xml.Linq;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.Business.BatchJobs
{
    public class Sitemap
    {
        public void Run()
        {
            LoggingManager.Debug("Entering into Sitemap");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                try
                {

             
                XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
                XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
                new XElement(ns + "urlset",
                             new XAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9"),
                             new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                             new XAttribute(xsi + "schemaLocation",
                                            "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd"),
                         from node in context.Users.ToList()
                         where node.IsCompany == null
                         select new XElement(ns + "url",
                                             new XElement(ns + "loc", "http://huntable.co.uk/"+ node.Url),
                                             new XElement(ns + "Name", node.Name),
                                             new XElement(ns + "Jobtitle", node.Title),
                                             new XElement(ns + "Location", node.City)
                             ),
                                 from node in context.ListofJobsSiteMap(System.DateTime.Now.AddYears(-5), System.DateTime.Now)
                                 where node.IsRssJob == null || node.IsNaukri == null || node.IsShine  == null ||node.IsRssJob== false || node.IsShine == false || node.IsNaukri == false
                                 select new XElement(ns + "url", 
                                                     new XElement(ns + "loc", "http://huntable.co.uk/" + new UrlGenerator().JobsUrlGenerator(node.Id)),
                                                     new XElement(ns + "Title", node.Title),
                                                     new XElement(ns + "Companyname", node.CompanyName),
                                                     new XElement(
                                                         ns + "Location", node.LocationName)


                                     ),
                                       from node in context.ListofJobsSiteMap(System.DateTime.Now.AddYears(-5), System.DateTime.Now)
                                       where node.IsRssJob != null
                                       select new XElement(ns + "url", 
                                                           new XElement(ns + "loc", "http://huntable.co.uk/" + new UrlGenerator().JobsUrlGenerator(node.Id)),
                                                           new XElement(ns + "Title", node.Title),

                                                           new XElement(
                                                               ns + "Location", node.LocationName)


                                           ),
                                      from node in context.Companies.ToList()
                                      where node.CompanyIndustry != null
                                      select new XElement(ns + "url",
                                                              new XElement(ns + "loc", "http://huntable.co.uk/" + node.Url),
                                                              new XElement(ns + "companyname", node.CompanyName),
                                                              new XElement(ns + "Location", node.TownCity),
                                                              new XElement(ns + "Industry", node.MasterIndustry.Description)

                                              )

                    ).Save("C://inetpub//vhosts//snovasys.com//huntable//sitemap.xml");
                }
                catch (Exception exception)
                {
                    LoggingManager.Debug("Exiting From SiteMap With exception"+exception);
                    throw;
                }
                
            }

            LoggingManager.Debug("Exiting From Sitemap");
        }
    }
}
