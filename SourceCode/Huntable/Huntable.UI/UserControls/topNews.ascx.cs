using System;
using System.Collections.Generic;
using System.Linq;
using Huntable.Business;
using Huntable.Data;
using Snovaspace.Util.FileDataStore;

namespace Huntable.UI.UserControls
{
    public partial class TopNews : System.Web.UI.UserControl
    {
        public List<string> Links;

        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateNews();
           
         }
        
        public void PopulateNews()
        {
            var jm = new JobsManager();
            List<News> newsList = jm.GetNews();
            Links = newsList.Select(n => n.link).Take(8).ToList();

            lnkApple.Text = newsList[0].Title;
            imgNews1.ImageUrl = newsList[0].NewsImageBasePath;
           
            lnkMac.Text = newsList[1].Title;
            imgNews2.ImageUrl = newsList[1].NewsImageBasePath;

            lnkErrorMessages.Text = newsList[2].Title;
            imgNews3.ImageUrl = newsList[2].NewsImageBasePath;

            lnkMac1.Text = newsList[3].Title;
            imgNews6.ImageUrl = newsList[3].NewsImageBasePath;

            lnkEM.Text = newsList[4].Title;
            imgNews10.ImageUrl = newsList[4].NewsImageBasePath;

            LinkButton1.Text = newsList[5].Title;
            Image1.ImageUrl = newsList[5].NewsImageBasePath;

            LinkButton2.Text = newsList[6].Title;
            Image2.ImageUrl = newsList[6].NewsImageBasePath;

            LinkButton3.Text = newsList[7].Title;
            Image3.ImageUrl = newsList[7].NewsImageBasePath;
        }

        public int? PicturePath { get; set; }
    }
   
}