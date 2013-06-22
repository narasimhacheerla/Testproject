using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Business;
using Huntable.Data;
using OAuthUtility;
using Snovaspace.Util.FileDataStore;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class CompanyRegistration2 : System.Web.UI.Page
    {
        public static int portfolioid = 0;
        public int LoginUserId
        {
            get
            {
                LoggingManager.Debug("Entering LoginUserId - CompanyRegistration2");

                var loginUserId = Common.GetLoggedInUserId(Session);
                if (loginUserId != null) return loginUserId.Value;

                LoggingManager.Debug("Exiting LoginUserId - CompanyRegistration2");
                return 0;

                
            }
        }
      
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - CompanyRegistration2");

            if (!Page.IsPostBack)
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                   
                    var countries = context.MasterCountries.ToList();
                    ddlcountry.DataSource = countries;
                    ddlcountry.DataTextField = "Description";
                    ddlcountry.DataValueField = "Id";
                    ddlcountry.DataBind();
                    var industries = context.MasterIndustries.ToList();
                    ddlIndustry.DataSource = industries;
                    ddlIndustry.DataTextField = "Description";
                    ddlIndustry.DataValueField = "Id";
                    ddlIndustry.DataBind();
                    var empployees = context.MasterEmployees.ToList();
                    ddlEmployess.DataSource = empployees;
                    ddlEmployess.DataTextField = "NoofEmployess";
                    ddlEmployess.DataValueField = "Id";
                    ddlEmployess.DataBind();
                    portfolioimg.ImageUrl = "images/no-image1.jpg";
                    imgproduct.ImageUrl = "images/what-like-img2.jpg";
                   
                    FillDeatils();
                }
            }
            LoggingManager.Debug("Exiting Page_Load - CompanyRegistration2");
        }

        public void FillDeatils()
        {
            LoggingManager.Debug("Entering FillDeatils - CompanyRegistration2");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                 int companylogoid = Convert.ToInt32(new FileStoreService().LoadImageAndResize(Constants.PortFolioImages, fuCompanyImage));
            
                var companydetails = context.Companies.FirstOrDefault(x => x.Userid == LoginUserId);
                if(companydetails!=null)
                {
                    lblCompName.Text = companydetails.CompanyName;
                    txtCompanyName.Text = companydetails.CompanyName;
                    txtCompanyHeading.Text = companydetails.CompanyHeading;
                    txtCompanyWebsite.Text = companydetails.CompanyWebsite;
                    txtCompanyDescription.Text = companydetails.CompanyDescription;
                    txtCompanyBlog.Text = companydetails.CompanyBlogLink;                   
                    txtTownCity.Text = companydetails.TownCity;
                    txtPostCode.Text = companydetails.Postcode;
                    txtEmailAddress.Text = companydetails.EmailAdress;                    
                    txtAddress2.Text = companydetails.Address2;                    
                    txtaddress1.Text = companydetails.Address1;
                    txtPhoneNum.Text = companydetails.PhoneNo;
                    txtComapnyCarrer.Text = companydetails.CompanyCarrersLink;
               
                    ddlIndustry.SelectedItem.Value = (companydetails.CompanyIndustry == null ? 0 : (int)companydetails.CompanyIndustry).ToString();
                    ddlEmployess.SelectedIndex = companydetails.NoofEmployees == null?0:(int)companydetails.NoofEmployees.Value-1;
                    if (companydetails.CountryId != null)
                        ddlcountry.SelectedIndex =( (int) companydetails.CountryId)-1;

                    imgCompany.ImageUrl = companydetails.CompanyLogoId!=null ? new FileStoreService().GetDownloadUrl(companydetails.CompanyLogoId) : "~/images/what-like-img1.jpg";
                    
                }
            }
            LoggingManager.Debug("Exiting FillDeatils - CompanyRegistration2");
        }
        protected void btnOverview_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnOverview_Click - CompanyRegistration2");

            pnlproductsservices.Visible = false;
            OverViewNextClick.Visible = true;
            btnOverview.CssClass = "sel_tab";
            btnservices.CssClass = "btnborder";
            btncarrer.CssClass = "btnborder";
            btnBusiness.CssClass = "btnborder";
            btnarticle.CssClass = "btnborder";

            LoggingManager.Debug("Exiting btnOverview_Click - CompanyRegistration2");
        }
        protected void btnarticle_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnarticle_Click - CompanyRegistration2");

            pnlproductsservices.Visible = true;
            OverViewNextClick.Visible = false;
            btnarticle.CssClass = "sel_tab";
            btnOverview.CssClass = "btnborder";
            btnservices.CssClass = "btnborder";
            btncarrer.CssClass = "btnborder";
            btnBusiness.CssClass = "btnborder";

            LoggingManager.Debug("Exiting btnarticle_Click - CompanyRegistration2");
        }
        protected void btnBusiness_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnBusiness_Click - CompanyRegistration2");

            pnlproductsservices.Visible = true;
            OverViewNextClick.Visible = false;
            btnBusiness.CssClass = "sel_tab";
            btnarticle.CssClass = "btnborder";
            btnOverview.CssClass = "btnborder";
            btnservices.CssClass = "btnborder";
            btncarrer.CssClass = "btnborder";

            LoggingManager.Debug("Exiting btnBusiness_Click - CompanyRegistration2");


        }
        protected void btncarrer_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btncarrer_Click - CompanyRegistration2");

            pnlproductsservices.Visible = true;
            OverViewNextClick.Visible = false;
            btncarrer.CssClass = "sel_tab";
            btnservices.CssClass = "btnborder";
            btnBusiness.CssClass = "btnborder";
            btnarticle.CssClass = "btnborder";
            btnOverview.CssClass = "btnborder";

            LoggingManager.Debug("Exiting btncarrer_Click - CompanyRegistration2");
        }

        protected void btnservices_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnservices_Click - CompanyRegistration2");

            pnlproductsservices.Visible = true;
            OverViewNextClick.Visible = false;
            btnservices.CssClass = "sel_tab";
            btncarrer.CssClass = "btnborder";
            btnBusiness.CssClass = "btnborder";
            btnarticle.CssClass = "btnborder";
            btnOverview.CssClass = "btnborder";

            LoggingManager.Debug("Exiting btnservices_Click - CompanyRegistration2");
        }

        protected void OverViewNextClickClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering OverViewNextClickClick - CompanyRegistration2");
           
            pnlproductsservices.Visible = true;
            OverViewNextClick.Visible = false;

            LoggingManager.Debug("Exiting OverViewNextClickClick - CompanyRegistration2");
        }

        protected void btn_skip_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering OverViewNextClickClick - CompanyRegistration2");
           
            pnlproductsservices.Visible = true;
            OverViewNextClick.Visible = false;

            LoggingManager.Debug("Exiting OverViewNextClickClick - CompanyRegistration2");
        }
        protected void BtnNextClick(object sender,EventArgs e)
        {
            LoggingManager.Debug("Entering BtnNextClick - CompanyRegistration2");

            pnlproductsservices.Visible = true;
            OverViewNextClick.Visible = false;

            LoggingManager.Debug("Exiting BtnNextClick - CompanyRegistration2");
        }
        protected void BtnsaveClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnsaveClick - CompanyRegistration2");

            var cmpyManager = new CompanyManager();
           
            string portfolioDescription = txtPortfolioDescription.Text;
            
            string videourl = txtVideoUrl.Text;
            if (videourl != null)
            {
                if ((videourl.Contains("?v=") == true))
                {

                    //string res = "?v=";
                    string[] result = videourl.Split(new string[] { "?v=" }, StringSplitOptions.RemoveEmptyEntries);
                    string newurl = result[1];
                    videourl = "http://www.youtube.com/embed/" + newurl;

                }
                else
                    videourl = txtVideoUrl.Text;
            }
            cmpyManager.CompanyRegistrationupdatePortfolio(LoginUserId,portfolioid,portfolioDescription,videourl);
            if (portfolioid != 0 && (videourl.Length == 0 || videourl =="Video url"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Picture saved succesfully')", true);

            }
            else if(portfolioid == 0 && videourl.Length != 0 && videourl != "Video url") {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Video Added  succesfully')", true);
            }
            else if (portfolioid != 0 && videourl.Length != 0 && videourl != "Video url")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Details saved succesfully')", true);
            }
            pnlproductsservices.Visible = true;
            portfolioid = 0;
            txtPortfolioDescription.Text = "";
            portfolioimg.ImageUrl = "images/no-image1.jpg";
            
           
            LoggingManager.Debug("Exiting BtnsaveClick - CompanyRegistration2");
        }
       
        protected void BtnsaveUpperClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnsaveUpperClick - CompanyRegistration2");

            int companylogoid = Convert.ToInt32(new FileStoreService().LoadImageAndResize(Constants.PortFolioImages, fuCompanyImage));
           
                string companyname = txtCompanyName.Text;
            string companyheader = txtCompanyHeading.Text ==
                                   "e.g: Brief heading about your company (max 200 characters)"
                                       ? ""
                                       : txtCompanyHeading.Text;


            string companydescription = txtCompanyDescription.Text ==
                                        "Now lets hear about your business, short & sweet... (max 300 characters)"
                                            ? ""
                                            : txtCompanyDescription.Text;
                int industryid = Convert.ToInt32(ddlIndustry.SelectedValue);
                string companywebsite = txtCompanyWebsite.Text;
                string phoneno = txtPhoneNum.Text;
                int employee = Convert.ToInt32(ddlEmployess.SelectedItem.Value);
                string address1 = txtaddress1.Text == "address" ? "" : txtaddress1.Text;
                string address2 = txtAddress2.Text=="address"?"":txtAddress2.Text;
                string pincode = txtPostCode.Text;
                string towncity = txtTownCity.Text == "Town/ciy"?"":txtTownCity.Text;
                string emailaddress = txtEmailAddress.Text;
                int countryid = Convert.ToInt32(ddlcountry.SelectedItem.Value);
                var cmpyManager = new CompanyManager();
                cmpyManager.CompanyRegistrationupdateCommon(LoginUserId, companylogoid, companyname, companyheader, companydescription, industryid, companywebsite, phoneno, emailaddress, employee, address1, address2, towncity, pincode, countryid);
                FillDeatils();
            
            LoggingManager.Debug("Exiting BtnsaveUpperClick - CompanyRegistration2");

        }

        //protected void BTnInviteFriendsClickClick(object sender, EventArgs e)
        //{

        //    LoggingManager.Debug("Entering BTnInviteFriendsClickClick - CompanyRegistration2");


        //    LoggingManager.Debug("Exiting BTnInviteFriendsClickClick - CompanyRegistration2");
        //}

        protected void BtnInviteByEmailAddressesClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnInviteByEmailAddressesClick - InviteFriends.aspx");
            try
            {
                if (string.IsNullOrWhiteSpace(txtMailIDs.Text.Trim()))
                {
                    new Snovaspace.Util.Utility().DisplayMessage(this, "Please enter atleast one email id.");
                    return;
                }

                var contacts = txtMailIDs.Text.Split(',');
                if (contacts.Length > 0)
                {
                    var emailcontacts = contacts.Select(email => new Contact { Name = email.Trim(), Email = email.Trim() }).ToList();

                    new InvitationManager().InviteEmailFriends(Page, emailcontacts,0);

                    txtMailIDs.Text = "";
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting BtnInviteByEmailAddressesClick - InviteFriends.aspx");
        }
        protected void BtnCompanycarrersaveClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCompanycarrersaveClick - CompanyRegistration2");

            var cmpMgr = new CompanyManager();
            cmpMgr.CompanyCarrerUpdate(LoginUserId,txtComapnyCarrer.Text,txtCompanyBlog.Text);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Details saved succesfully')", true);

            LoggingManager.Debug("Exiting BtnCompanycarrersaveClick - CompanyRegistration2");

        }
    
        protected void BtnVideoAddClick(object sender,EventArgs e)
        {
            LoggingManager.Debug("Entering BtnVideoAddClick - CompanyRegistration2");

            var cmpMgr = new CompanyManager();
            const int portfolioid = 0;
            
            string videourl = txtVideoUrl.Text;
            if (videourl != null)
            {
                if ((videourl.Contains("?v=") == true))
                {
                    
                    //string res = "?v=";
                    string[] result = videourl.Split(new string[] { "?v=" }, StringSplitOptions.RemoveEmptyEntries);
                    string newurl = result[1];
                    videourl = "http://www.youtube.com/embed/" + newurl;
                    ifrmVideo.Attributes.Add("Src", videourl);
                }
                else
                    ifrmVideo.Attributes.Add("Src", videourl);
                var socialManager = new SocialShareManager();
                var msg = videourl;
                socialManager.ShareOnFacebook(LoginUserId, msg, "");
            }
           


            LoggingManager.Debug("Exiting BtnVideoAddClick - CompanyRegistration2");
        }
        protected void BtnCompanyPorductsClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnCompanyPorductsClick - CompanyRegistration2");

            var cmpMgr = new CompanyManager();
            int productimageid=Convert.ToInt32(new FileStoreService().LoadImageAndResize(Constants.PortFolioImages, Fproduct));
            cmpMgr.CompanyProductUpdate(LoginUserId,productimageid,txtProductDescriptipon.Text);
            ProductImage.Visible = false;
            imgproduct.Visible = true;
            if (productimageid != 0)
            {
                imgproduct.ImageUrl = new FileStoreService().GetDownloadUrl(productimageid);
                var socialManager = new SocialShareManager();
                var url = "https://huntable.co.uk/LoadFile.ashx?id=" + productimageid;
                var msg = "[UserName]" + " " + "updated new products & services  to their company";
                socialManager.ShareOnFacebook(LoginUserId, msg,url);
            }
            LoggingManager.Debug("Exiting BtnCompanyPorductsClick - CompanyRegistration2");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('product added sucessfully')", true);
        }
        protected void BtnSendImport(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnSendImport - CompanyRegistration2");

            LoggingManager.Debug("Exiting BtnSendImport - CompanyRegistration2");

        }
        protected void BtnArticleSaveClick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnArticleSaveClick - CompanyRegistration2");

            var cmpMgr = new CompanyManager();
            cmpMgr.CompanyArticleUpdate(LoginUserId,txtArticleTitle.Text,txtArticleDescription.Text);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Article added sucessfully')", true);
            LoggingManager.Debug("Exiting BtnArticleSaveClick - CompanyRegistration2");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering btnAdd_Click - CompanyRegistration2");
            var cmpMgr = new CompanyManager();
            portfolioid = Convert.ToInt32(new FileStoreService().LoadImageAndResize(Constants.PortFolioImages, fpPortfolio));
            //cmpMgr.CompanyRegistrationupdatePortfolio(LoginUserId, portfolioid, txtPortfolioDescription.Text,null);
            portfolioId.Visible = false;
            portfolioimg.Visible = true;
            if (portfolioid != 0)
            {
                portfolioimg.ImageUrl = new FileStoreService().GetDownloadUrl(portfolioid);                
                LoggingManager.Debug("Exiting btnAdd_Click - CompanyRegistration2");
            }

        }
        protected void UploadInvites(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering UploadInvites - CompanyRegistration2");

            new InvitationManager().UploadContactsFromFileUploadControl(Page, fuInvitationFriends);

            LoggingManager.Debug("Exiting UploadInvites - CompanyRegistration2");
        }
        protected void IbtnFacebookClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnFacebookClick - CompanyRegistration2");

            Response.Redirect("oauth.aspx?currpage=facebook", false);

            LoggingManager.Debug("Exiting IbtnFacebookClick - CompanyRegistration2");

        }

        protected void IbtnTwitterClick(object sender, ImageClickEventArgs e)
        {

            LoggingManager.Debug("Entering IbtnTwitterClick - CompanyRegistration2");

            Response.Redirect("oauth.aspx?currpage=twitter", false);

            LoggingManager.Debug("Exiting IbtnTwitterClick - CompanyRegistration2");
        }

        protected void IbtnLinkedInClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnLinkedInClick - CompanyRegistration2");

            Response.Redirect("oauth.aspx?currpage=linkedin", false);

            LoggingManager.Debug("Exiting IbtnLinkedInClick - CompanyRegistration2");

        }
        protected void IbtnGoogleClick(object sender, ImageClickEventArgs e)
        {


            LoggingManager.Debug("Entering IbtnGoogleClick - CompanyRegistration2");
            try
            {
                Response.Redirect("oauth.aspx?currpage=gmail", false);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting IbtnGoogleClick - CompanyRegistration2");
        }

        protected void IbtnYahooClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnYahooClick - CompanyRegistration2");
            try
            {
                Response.Redirect("oauth.aspx?currpage=yahoo");
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting IbtnYahooClick - CompanyRegistration2");
        }

        protected void IbtnLiveClick(object sender, ImageClickEventArgs e)
        {
            LoggingManager.Debug("Entering IbtnLiveClick - CompanyRegistration2");

            Response.Redirect("oauth.aspx?currpage=live", false);

            LoggingManager.Debug("Exiting IbtnLiveClick - CompanyRegistration2");
        }


    }
}