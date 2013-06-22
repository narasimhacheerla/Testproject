using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Huntable.Data;
using Huntable.Entities;
using MoreLinq;
using PayPal.PayPalAPIInterfaceService.Model;
using PayPal.PayPalAPIInterfaceService;
using Huntable.Business;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class SecureCheckout : System.Web.UI.Page
    {
        private readonly string _featuredCategoryPrice = ConfigurationManager.AppSettings["FeaturedCategoryPrice"];
        private readonly string _vatamount = ConfigurationManager.AppSettings["VatAmount"];
        public string SuccessUrl
        {

            get
            {
                LoggingManager.Debug("Entering Jobs - SecureCheckout.aspx");
                LoggingManager.Debug("Exiting Jobs - SecureCheckout.aspx");
                return Convert.ToString(Request.QueryString["SuccessUrl"]);
            }
        }

        public string FailureUrl
        {
            get
            {
                LoggingManager.Debug("Entering FailureUrl - SecureCheckout.aspx");
                LoggingManager.Debug("Exiting FailureUrl - SecureCheckout.aspx");

                return Convert.ToString(Request.QueryString["FailureUrl"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - SecureCheckout.aspx");
            try
            {
                lblResult.Visible = false;
                if (Page.IsPostBack) return;
                BindCountries();
                BindMonths();
                BindYears();
                //BindStates();
                BindUserData();
                if (Request.Url.Query.Contains("amt") || Request.QueryString["amt"] != string.Empty)
                {
                    div_pack.Visible = true;
                    decimal amount = Convert.ToDecimal(Request.QueryString["amt"]);

                    string totalicludingvat = (amount + (amount*20/100)).ToString();

                    txtAmount.Text = totalicludingvat;
                      
                    lblPackageval.Text = "$" + Convert.ToString(Request.QueryString["amt"]);
                }

            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }

            LoggingManager.Debug("Exiting Page_Load - SecureCheckout.aspx");
        }
        protected void BtnClientReviewAndContinue(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering BtnClientReviewAndContinue - SecureCheckout.aspx");

           

            if (DoDirectPaymentCode())
            {
                Response.Redirect(SuccessUrl + "?CheckOutSuccess=true");
            }
           
            LoggingManager.Debug("Exiting BtnClientReviewAndContinue - SecureCheckout.aspx");
        }

        //private bool ProcessTransaction()
        //{
        //    LoggingManager.Debug("Entering ProcessTransaction - SecureCheckout.aspx");
        //    try
        //    {
        //        LoggingManager.Info("Preparing Paypal Parameters");
        //        var payPalRequest = new StringBuilder();
        //        payPalRequest.Append("TRXTYPE=S");//S - sale transaction , C = Credit, A - Authorization,D = Delayed Capture, V = Viod ,F = Voice Authorization , I = Inquiry. N = Duplicate Transaction;

        //        if (ddlBankCardTypes.SelectedValue == "Visa" || ddlBankCardTypes.SelectedValue == "MasterCard") // C -  Credit Card, A - Automated Cleaning House , D - Pinless Debit , K - TeleCheck , P - Paypal
        //            payPalRequest.Append("&TENDER=C");
        //        else if (ddlBankCardTypes.SelectedValue == "American Express") payPalRequest.Append("&TENDER=D");

        //        payPalRequest.Append("&ACCT=" + txtcard.Text);
        //        payPalRequest.Append("&EXPDATE=" + ddlMonth.SelectedValue.PadLeft(2, '0') + ddlYear.SelectedItem.Text.Substring(2, 2));
        //        payPalRequest.Append("&CVV2=" + txtcsc.Text);  //card validation value (card security code)
        //        payPalRequest.Append("&AMT=" + txtAmount.Text);
        //        //payPalRequest.Append("&COMMENT1=huntable transaction");
        //        //payPalRequest.Append("&COMMENT1=direct debit");
        //        //payPalRequest.Append("&USER=" + ConfigurationManager.AppSettings["USER"]);
        //        //payPalRequest.Append("&VENDOR=" + ConfigurationManager.AppSettings["VENDOR"]);
        //        //payPalRequest.Append("&PARTNER=" + ConfigurationManager.AppSettings["PARTNER"]);
        //        //payPalRequest.Append("&PWD=" + ConfigurationManager.AppSettings["PWD"]);
        //        payPalRequest.Append("&FIRSTNAME=" + txtfirst.Text);
        //        payPalRequest.Append("&LASTNAME=" + txtmiddle.Text + txtlast.Text);
        //        payPalRequest.Append("&STREET=" + txtadress.Text + txtaddress2.Text + "," + txtcity.Text + "," + txtState.Text + "," + txtemail.Text + "," + txttele.Text);
        //        payPalRequest.Append("&ZIP=" + txtpin.Text);
        //        LoggingManager.Info("Submiting Paypal Request");
        //        //// Create an instantce of PayflowNETAPI.
        //        var payflowNETAPI = new PayflowNETAPI();
        //        // RequestId is a unique string that is required for each & every transaction. 
        //        // The merchant can use her/his own algorithm to generate this unique request id or 
        //        // use the SDK provided API to generate this as shown below (PayflowUtility.RequestId).
        //        //string PayPalResponse = PayflowNETAPI.SubmitTransaction(PayPalRequest, PayflowUtility.RequestId);
        //        var payPalResponse = payflowNETAPI.SubmitTransaction(payPalRequest.ToString(), PayflowUtility.RequestId);
        //        //place data from PayPal into a namevaluecollection
        //        var requestCollection = GetPayPalCollection(payflowNETAPI.TransactionRequest);
        //        var responseCollection = GetPayPalCollection(payPalResponse);
        //        LoggingManager.Info("Got response from Paypal");
        //        //show request
        //        lblResult.Text = "<span class=\"heading\">PayPal Payflow Pro transaction request</span><br />";
        //        lblResult.Text += ShowPayPalInfo(requestCollection);
        //        //show response
        //        lblResult.Text += "<br /><br /><span class=\"heading\">PayPal Payflow Pro transaction response</span><br />";
        //        lblResult.Text += ShowPayPalInfo(responseCollection);
        //        //show transaction errors if any
        //        var transErrors = payflowNETAPI.TransactionContext.ToString();
        //        if (transErrors.Length > 0)
        //        {
        //            lblResult.Text += string.Format("<br /><br /><span class=\"bold-text\">Transaction Errors:</span> {0}", transErrors);
        //        }
        //        //show transaction status
        //        lblResult.Text += string.Format("<br /><br /><span class=\"bold-text\">Status:</span> {0}", PayflowUtility.GetStatus(payPalResponse));
        //        lblResult.Visible = true;
        //        tbResult.Visible = true;
        //        pnlMain.Visible = false;

        //        bool success = lblResult.Text.Contains("Transaction Successful");

        //        if (success)
        //        {
        //            AuttomaticInvoice();
        //            if (SuccessUrl.Contains("WhatIsHuntableUpgrade"))
        //            {
        //                SavePremiumUser();
        //            }
        //            if (SuccessUrl.Contains("PostJob"))
        //            {
        //                UpdateTheJobCredits();
        //            }
        //        }

        //        LoggingManager.Info("Inserting Record into User Payment Info table");
        //        string[] sResponse = payPalResponse.Split('&');
        //        var paymentInfo = new UserPaymentInfo
        //                              {
        //                                  UserID = Convert.ToInt32(Business.Common.GetLoggedInUserId(this.Session)),
        //                                  FirstName = txtfirst.Text,
        //                                  LastName = txtfirst.Text + txtmiddle.Text,
        //                                  PaymentAddress =
        //                                      txtadress.Text + txtaddress2.Text + "," + txtcity.Text + "," +
        //                                      txtState.Text + ", " + ddlcountrynames.SelectedItem.Text,
        //                                  ZipCode = txtpin.Text,
        //                                  Telephone = txttele.Text,
        //                                  Email = txtemail.Text,
        //                                  TransactionType = "S",
        //                                  Tender = "C",
        //                                  ACCNO = "656565656565",
        //                                  ExpDate =
        //                                      Convert.ToInt32(ddlMonth.SelectedValue +
        //                                                      ddlYear.SelectedItem.Text.Substring(2, 2)),
        //                                  CVV2 = txtcsc.Text,
        //                                  Amount = Convert.ToDecimal(txtAmount.Text),
        //                                  Result =
        //                                      Convert.ToInt32(sResponse[0].Substring(sResponse[0].IndexOf("=") + 1,
        //                                                                             sResponse[0].Length -
        //                                                                             sResponse[0].IndexOf("=") - 1))
        //                              };
        //        btnOk.ToolTip = paymentInfo.Result.ToString();
        //        if (success)
        //        {
        //            paymentInfo.PNREF = sResponse[1].Substring(sResponse[1].IndexOf("=") + 1, sResponse[1].Length - sResponse[1].IndexOf("=") - 1);
        //            paymentInfo.RESPMSG = sResponse[2].Substring(sResponse[2].IndexOf("=") + 1, sResponse[2].Length - sResponse[2].IndexOf("=") - 1);
        //            paymentInfo.AUTHCODE = sResponse[3].Substring(sResponse[3].IndexOf("=") + 1, sResponse[3].Length - sResponse[3].IndexOf("=") - 1);
        //            paymentInfo.CVV2MATCH = sResponse[sResponse.Length - 1].Substring(sResponse[sResponse.Length - 1].IndexOf("=") + 1, sResponse[sResponse.Length - 1].Length - sResponse[sResponse.Length - 1].IndexOf("=") - 1);
        //            paymentInfo.PaymentStatus = "Transaction Successful";
        //            btnOk.Text = "OK";
        //        }
        //        else
        //        {
        //            paymentInfo.PNREF = sResponse[1].Substring(sResponse[1].IndexOf("=") + 1, sResponse[1].Length - sResponse[1].IndexOf("=") - 1);
        //            paymentInfo.RESPMSG = sResponse[2].Substring(sResponse[2].IndexOf("=") + 1, sResponse[2].Length - sResponse[2].IndexOf("=") - 1);
        //            paymentInfo.PaymentStatus = "Transaction Failed";
        //            btnOk.Text = "Back";
        //        }

        //        var userManager = new UserManager();
        //        userManager.SaveUserPaymentInfo(paymentInfo);
        //        LoggingManager.Info("Record Inserted into User Payment Info table");
        //        var featiredSele = new FeaturedSelections { Jobpackage = null };
        //        Session["FeaturedSelections"] = null;
        //        return lblResult.Text.Contains("Transaction Successful");
        //    }
        //    catch (Exception ex)
        //    {
        //        lblResult.Text = ex.Message.ToString(CultureInfo.InvariantCulture);
        //        lblResult.Visible = true;

        //        LoggingManager.Debug("Exiting ProcessTransaction - SecureCheckout.aspx");
        //        return false;
        //    }
        //}

        private void SavePremiumUser()
        {
            LoggingManager.Debug("Entering SavePremiumUser - SecureCheckout.aspx");
            try
            {
                var usrMngr = new UserManager();
                var userId = Convert.ToInt32(Business.Common.GetLoggedInUserId(this.Session));
                usrMngr.SavePremiumUser(userId);
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting SavePremiumUser -  SecureCheckout.aspx");

        }
        private void UpdateTheJobCredits()
        {
            LoggingManager.Debug("Entering UpdateTheJobCredits -  SecureCheckout.aspx");
            LoggingManager.Info("Paypal Transaction Successful");
            LoggingManager.Info("Inserting record into Job Credits Purchased table");
            var jobManager = new JobsManager();
            var creditsPurchased = new JobCreditsPurchased
                {
                    AmountPaid = Convert.ToDecimal(txtAmount.Text),
                    NoOfCredits = Convert.ToInt32(Request.QueryString["Credits"]),
                    PurchaseDateTime = DateTime.Now,
                    Purpose = "Credits Purchased",
                    UserId =
                        Convert.ToInt32(Common.GetLoggedInUserId(Session))
                };
            jobManager.SaveJobCreditsPurchased(creditsPurchased);
            LoggingManager.Info("Record Inserted into Job Credits Purchased table");
            LoggingManager.Debug("Exiting UpdateTheJobCredits -  SecureCheckout.aspx");
        }

        private string ShowPayPalInfo(NameValueCollection collection)
        {
            LoggingManager.Debug("Entering ShowPayPalInfo -  SecureCheckout.aspx");
            LoggingManager.Debug("Exiting ShowPayPalInfo -  SecureCheckout.aspx");
            return collection.AllKeys.Aggregate("", (current, key) => current + ("<br /><span class=\"bold-text\">" + key + ":</span> " + collection[key]));
        }

        private void BindUserData()
        {
            LoggingManager.Debug("Entering BindUserData -  SecureCheckout.aspx");

            huntableEntities entities = huntableEntities.GetEntitiesWithNoLock();
            try
            {
                int UserID = Convert.ToInt32(Business.Common.GetLoggedInUserId(this.Session));
                User _user = entities.Users.First(u => u.Id == UserID);
                if (_user != null)
                {
                    txtfirst.Text = _user.Name;
                    txtadress.Text = _user.HomeAddress;
                    txttele.Text = _user.PhoneNumber;
                    txtemail.Text = _user.EmailAddress;
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BindUserData -  SecureCheckout.aspx");
        }

        private void BindMonths()
        {
            LoggingManager.Debug("Entering BindMonths -  SecureCheckout.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    ddlMonth.DataSource = context.MasterMonths.ToList().DistinctBy(c => c.Description).OrderBy(c => c.ID);
                    ddlMonth.DataTextField = "Description";
                    ddlMonth.DataValueField = "Id";
                    ddlMonth.DataBind();
                    ddlMonth.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BindMonths -  SecureCheckout.aspx");
        }
        private void BindYears()
        {
            LoggingManager.Debug("Entering BindYears - SecureCheckout.aspx");
            try
            {
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var yearsList = context.MasterYears.DistinctBy(c => c.Description).Where(c => c.ID >= 63).ToList();

                    ddlYear.DataSource = yearsList;
                    ddlYear.DataTextField = "Description";
                    ddlYear.DataValueField = "Id";
                    ddlYear.DataBind();
                    ddlYear.Items.Insert(0, new ListItem { Text = "Select", Value = "0" });
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BindYears - SecureCheckout.aspx");
        }
        private void BindCountries()
        {
            LoggingManager.Debug("Entering BindCountries - SecureCheckout.aspx");
            try
            {
                ddlcountrynames.DataSource = MasterDataManager.AllCountries.ToList().DistinctBy(c => c.Description);
                ddlcountrynames.DataTextField = "Description";
                ddlcountrynames.DataValueField = "Id";
                ddlcountrynames.DataBind();
                ddlcountrynames.Items.Insert(0, new ListItem { Text = "Please Select...", Value = "0" });
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting BindCountries - SecureCheckout.aspx");
        }
        private NameValueCollection GetPayPalCollection(string payPalInfo)
        {
            LoggingManager.Debug("Entering GetPayPalCollection - SecureCheckout.aspx");

            //place the responses into collection
            var payPalCollection = new NameValueCollection();
            var arrayReponses = payPalInfo.Split('&');
            foreach (var temp in arrayReponses.Select(t => t.Split('=')))
            {
                payPalCollection.Add(temp[0], temp[1]);
            }
            LoggingManager.Debug("Exiting  GetPayPalCollection - SecureCheckout.aspx");
            return payPalCollection;
        }

        protected void BtnOkClick(object sender, EventArgs e)
        {

            LoggingManager.Debug("Entering  btnOkClick - SecureCheckout.aspx");
            try
            {
                if (btnOk.Text == "OK")
                    Response.Redirect(SuccessUrl, false);
                else
                {
                    if (btnOk.Text == "OK")
                        Response.Redirect(SuccessUrl);
                    else
                    {
                        tbResult.Visible = false;
                        pnlMain.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting  btnOkClick - SecureCheckout.aspx");

        }
        private void AuttomaticInvoice()
        {
            LoggingManager.Debug("Entering  AuttomaticInvoice - SecureCheckout.aspx");
            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                var totalamount=0;
                var vat=0;
                var totalamountaftervat=0;
               double jobpackagenew = 0;
                 var purpose=string.Empty;
                int NoofCredits=0;
               var usrmngr = new UserMessageManager();
                int userid = Convert.ToInt32(Common.GetLoggedInUserId(Session));
                var userinfo = context.Users.FirstOrDefault(x => x.Id == userid);
                var featiredSele = (FeaturedSelections) Session["FeaturedSelections"];
                var jobpackage = (featiredSele != null) ?featiredSele.Jobpackage:"0";
                var industry = (featiredSele != null) ? featiredSele.Industries.Count() *
                               Convert.ToDouble(_featuredCategoryPrice) : 0;
                var country = (featiredSele != null) ? featiredSele.Countries.Count() *
                               Convert.ToDouble(_featuredCategoryPrice) : 0;
                var interests = (featiredSele != null) ? featiredSele.Interests.Count() *
                               Convert.ToDouble(_featuredCategoryPrice) : 0;
                var skill = (featiredSele != null) ? featiredSele.Skills.Count() *
                               Convert.ToDouble(_featuredCategoryPrice) : 0;
                var premium = (featiredSele != null) ? featiredSele.PremiumPackage 
                                : 0;
                jobpackagenew = Convert.ToDouble(jobpackage);
                if(premium>0)
                {
                    purpose = "Premimum";
                    NoofCredits = 10;
                    if (userinfo != null) usrmngr.SendEmailToPremiumusers(userinfo.EmailAddress,userinfo.Name);
                }
                if (featiredSele != null)
                {
                    var amt = ((industry+ interests +
                                country + skill)
                              );
                    if(amt>00&&jobpackagenew>0)
                    {
                        const string str = "JobPackage,Featuredrecuirters";
                        purpose = string.Format("{0} {1}", str, purpose);
                        NoofCredits = ((int)jobpackagenew / 5); 
                    }
                    else if(jobpackagenew>0)
                    {
                        const string str = "JobPackage";
                        purpose = str;
                        NoofCredits = ((int)jobpackagenew/5);
                    }
                    else if(amt>0)
                    {
                        const string str = "Featuredrecuirters";
                        NoofCredits = 0;
                        purpose = str;
                    }
                    totalamount = (int) (amt + jobpackagenew+premium);
                    vat =(int) (totalamount * 0.20);
                    totalamountaftervat =Convert.ToInt32(totalamount) + vat;
                }
                var date = DateTime.Now;
                  var newdate=String.Format("{0:MMM d,yyyy}", date);
                var rand = new Random((int)DateTime.Now.Ticks);
                int randomNumber = rand.Next(100000, 999999);
               
                if (userinfo != null)
                {
                    var valuesList = new Hashtable
                                         {
                                             {"Name",userinfo.Name},
                                             {"Date", newdate},
                                             {"InvoiceNo",randomNumber },
                                             {"Address1", userinfo.HomeAddress},
                                             {"Address2", userinfo.City},
                                             {"Address3", userinfo.LocationToDisplay},
                                             {"PremiumPice", premium},
                                             {"JobsPackage", jobpackagenew},
                                             {"FeaturedIndustry", industry},
                                             {"FeaturedSkill", skill},
                                             {"FeaturedCountry", country},
                                             {"FeaturedInterests",interests},
                                             {"TotalAmountBeforeVat", totalamount},
                                             {"VatAmount", vat},
                                             {"TotalAmountAfterVat", totalamountaftervat},
                                             {"JobsPackageCount",jobpackagenew/5},
                                             {"IndustryCount",(featiredSele != null) ? featiredSele.Industries.Count():0},
                                             {"SkillCount",(featiredSele != null) ? featiredSele.Skills.Count():0},
                                             {"CountryCount",(featiredSele != null) ? featiredSele.Countries.Count():0},
                                             {"InterestsCount",(featiredSele != null) ? featiredSele.Interests.Count() :0}
                                         };

                   
                    usrmngr.SendAutomaticEmailtouser(userid,
                                                     userinfo.EmailAddress, valuesList);
                    var transcation = new JobCreditsPurchased
                                          {
                                              UserId = userid,
                                              AmountPaid = totalamountaftervat,
                                              PurchaseDateTime = DateTime.Now,
                                              FeaturedCountry = (int?) country,
                                              FeaturedIndustry = (int?) industry,
                                              FeaturedInterests = (int?) interests,
                                              FeaturedSkill = (int?) skill,
                                              JobPackage = (int?) jobpackagenew,
                                              VatAmount = vat,
                                              TotalAmount = totalamount,
                                              TotalAmountAftervat = totalamountaftervat,
                                              Premiumpackage = premium,
                                              InvoiceNo = randomNumber,
                                              Purpose = purpose,
                                              NoOfCredits = NoofCredits
                                          };
                    context.AddToJobCreditsPurchaseds(transcation);
                    context.SaveChanges();
                }
            }
            LoggingManager.Debug("Exiting  AuttomaticInvoice - SecureCheckout.aspx");
        }

        public bool DoDirectPaymentCode()
        {
            LoggingManager.Debug("Entering  DoDirectPaymentCode - SecureCheckout.aspx");
            #region New API Code
            decimal amount = Convert.ToDecimal(Request.QueryString["amt"]);
            // Create request object
            DoDirectPaymentRequestType request = new DoDirectPaymentRequestType();


            DoDirectPaymentRequestDetailsType requestDetails = new DoDirectPaymentRequestDetailsType();
            request.DoDirectPaymentRequestDetails = requestDetails;

            requestDetails.PaymentAction = PaymentActionCodeType.SALE;

            // Populate card requestDetails
            CreditCardDetailsType creditCard = new CreditCardDetailsType();
            requestDetails.CreditCard = creditCard;
            PayerInfoType payer = new PayerInfoType();
            PersonNameType name = new PersonNameType();
            name.FirstName = txtfirst.Text;
            name.LastName = txtmiddle.Text + txtlast.Text;
            payer.PayerName = name;
            creditCard.CardOwner = payer;

            creditCard.CreditCardNumber = txtcard.Text;
            switch (ddlBankCardTypes.SelectedValue)
            {
                case "Visa":
                    creditCard.CreditCardType = CreditCardTypeType.VISA;
                    break;
                case "MasterCard":
                    creditCard.CreditCardType = CreditCardTypeType.MASTERCARD;
                    break;
                case "Discover":
                    creditCard.CreditCardType = CreditCardTypeType.DISCOVER;
                    break;
                case "American Express":
                    creditCard.CreditCardType = CreditCardTypeType.AMEX;
                    break;
            }

            creditCard.CVV2 = txtcsc.Text;
            int month = 0;
            Int32.TryParse(ddlMonth.SelectedValue, out month);
            if(month >0)
                creditCard.ExpMonth = month;
            int year = 0;
            Int32.TryParse(ddlYear.SelectedItem.Text, out year);
            if (year > 0)
                creditCard.ExpYear = year;
            

            requestDetails.PaymentDetails = new PaymentDetailsType();
            AddressType billingAddr = new AddressType();

            billingAddr.Name = payer.PayerName.FirstName + " " + payer.PayerName.LastName;
            billingAddr.Street1 = txtadress.Text;
            billingAddr.Street2 = txtaddress2.Text;
            billingAddr.CityName = txtcity.Text;
            billingAddr.StateOrProvince = txtState.Text;
            billingAddr.CountryName = ddlcountrynames.SelectedItem.Text;
            billingAddr.PostalCode = txtpin.Text;
            billingAddr.Phone = txttele.Text;
            //billingAddr.email = txtemail.Text;
            payer.Address = billingAddr;

            // Populate payment requestDetails
            CurrencyCodeType currency = CurrencyCodeType.USD;
            BasicAmountType paymentAmount = new BasicAmountType(currency, txtAmount.Text);
            requestDetails.PaymentDetails.OrderTotal = paymentAmount;


            // Invoke the API
            DoDirectPaymentReq wrapper = new DoDirectPaymentReq();
            wrapper.DoDirectPaymentRequest = request;

            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();
            DoDirectPaymentResponseType response = service.DoDirectPayment(wrapper);
            LoggingManager.Info("Got response from Paypal");

            string Errors = "";
            foreach (var err in response.Errors)
            {
                Errors = "<span class=\"bold-text\">" + err.ErrorCode + "</span> - " + err.ShortMessage + " - " + err.LongMessage + "<br/>";
            }
            if (Errors.Length > 0)
            {
                lblResult.Text = "<span class=\"bold-text\">Transaction failed.</span>";
            }

            lblResult.Visible = true;
            tbResult.Visible = true;
            pnlMain.Visible = false;

            if (response.Ack == AckCodeType.SUCCESS)
            {
                AuttomaticInvoice();
                if (SuccessUrl.Contains("WhatIsHuntableUpgrade"))
                {
                    SavePremiumUser();
                }
                if (SuccessUrl.Contains("PostJob"))
                {
                    UpdateTheJobCredits();
                }
                Errors = AckCodeType.SUCCESS.ToString();
            }

            LoggingManager.Info("Inserting Record into User Payment Info table");
            var paymentInfo = new UserPaymentInfo
            {
                UserID = Convert.ToInt32(Business.Common.GetLoggedInUserId(this.Session)),
                FirstName = txtfirst.Text,
                LastName = txtfirst.Text + txtmiddle.Text,
                PaymentAddress =
                    txtadress.Text + txtaddress2.Text + "," + txtcity.Text + "," +
                    txtState.Text + ", " + ddlcountrynames.SelectedItem.Text,
                ZipCode = txtpin.Text,
                Telephone = txttele.Text,
                Email = txtemail.Text,
                TransactionType = "S",
                Tender = "C",
                ACCNO = "656565656565",
                ExpDate =
                    Convert.ToInt32(ddlMonth.SelectedValue +
                                    ddlYear.SelectedItem.Text.Substring(2, 2)),
                CVV2 = txtcsc.Text,
                TranscationDateTime = DateTime.Now,
                Amount = Convert.ToDecimal(txtAmount.Text),
                Vat = Convert.ToDecimal(amount* 20 / 100),
                Result = (int)response.Ack
            };
            btnOk.ToolTip = paymentInfo.Result.ToString();
            if (response.Ack == AckCodeType.SUCCESS)
            {
                paymentInfo.PNREF = response.TransactionID;
                paymentInfo.RESPMSG = response.Build;
                paymentInfo.AUTHCODE = response.CorrelationID;
                paymentInfo.CVV2MATCH = response.CVV2Code;
                paymentInfo.PaymentStatus = response.Ack.ToString();
                btnOk.Text = "OK";
            }
            else
            {
                paymentInfo.PNREF = response.TransactionID;
                paymentInfo.RESPMSG = response.Build;
                paymentInfo.PaymentStatus = response.Ack.ToString();
                btnOk.Text = "Back";
            }

            var userManager = new UserManager();
            userManager.SaveUserPaymentInfo(paymentInfo);
            LoggingManager.Info("Record Inserted into User Payment Info table");
            var featiredSele = new FeaturedSelections { Jobpackage = null };
            Session["FeaturedSelections"] = null;

            // Check for API return status
            //setKeyResponseObjects(service, response);
            #endregion
            //return pp_response.Ack.ToString();
            LoggingManager.Debug("Exiting  DoDirectPaymentCode - SecureCheckout.aspx");
            return (response.Ack == AckCodeType.SUCCESS);
        }
    }
}