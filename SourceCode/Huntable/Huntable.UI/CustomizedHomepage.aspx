<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomizedHomepage.aspx.cs" Inherits="Huntable.UI.CustomizedHomepage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The  Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="Styles/style.css" type="text/css" rel="stylesheet" />
    
    <!-- Slider Script Begins -->
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="js/script.js"></script>
    <script type="text/javascript">
//	<![CDATA[
        $(document).ready(function () {
            $('div.test').click(function () {
                $('ul.list').slideToggle('medium');
            });
        });
        $(document).ready(function () {
            $('div.test1').click(function () {
                $('ul.list1').slideToggle('medium');
            });
        });
//]]>
</script>
    <script type="text/javascript">
        var emailPat = /^(\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)$/;
        var ck_username = /^[a-zA-Z][a-zA-Z ]+$/;
        function Validate() {
            var fName = document.getElementById("<%=txtFirstName.ClientID%>").value;
            var lName = document.getElementById("<%=txtLastName.ClientID%>").value;
            var email = document.getElementById("<%=txtEmail.ClientID%>").value;
            var pwd = document.getElementById("<%=txtPassword.ClientID%>").value;


            if (fName == "First name") {
                alert("Enter first name please");
                document.getElementById("<%=txtFirstName.ClientID%>").focus();
                return false;
            }
            if (lName == "Last name") {
                alert("Enter last name please");
                document.getElementById("<%=txtLastName.ClientID%>").focus();
                return false;
            }
            if (email == "Email") {
                alert("Enter email please");
                document.getElementById("<%=txtEmail.ClientID%>").focus();
                return false;
            }
            if (pwd == "Password") {
                alert("Enter password");
                document.getElementById("<%=txtPassword.ClientID%>").focus();
                return false;
            }
            var matchArray = email.match(emailPat);

            if (matchArray == null) {
                alert("Your email address seems incorrect. Please try again.");
                document.getElementById("<%=txtEmail.ClientID %>").focus();
                return false;
            }
            if (!ck_username.test(fName)) {
                alert("Enter valid text in First Name field");
                return false;
            }
            if (!ck_username.test(lName)) {
                alert("Enter valid text in Last Name field");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header-section">
       <%-- <p class="top-strip">
            &nbsp;</p>
        <p class="bottom-strip">
            &nbsp;</p>--%>
        <div id="banner-section" style="background-color:White">
            <div id="banner-inner">
                <div class="banner-left">
                    <div>
                        <div>
                                <h2>Hi <strong class="red-color"><asp:Label ID="lblInvitee" runat="server"></asp:Label></strong></h2>
                                <h3><asp:Label ID="lblInviter" runat="server"></asp:Label> has invited you to join Huntable</h3>
                                <div align="center" style="margin:10px 0px;box-shadow:0px 0px 2px 2px #ccc; border:1px solid #ccc;">
                                <img id="Img1" src="images/you-invited.jpg" width="650"  height="405" alt="You-Invited" runat="server" />
                                </div>
                        </div>
                       
                    </div>
                </div>
                <div class="banner-right1">
                    <h2><asp:Label ID="lblInviter2" runat="server"></asp:Label> Message to you</h2>
                	<div class="user-msg">
                   
                    <p>
                    	<asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </p>
                    </div>
                    <div class="huntable-join">
                    <div class="slidingDiv slidingdiv-new1">
                    <div class="banner-right">
                    <h1>
                        Join Huntable Today</h1>
                    <div class="banner-right-inner">
                    
                        <asp:TextBox ID="txtFirstName" runat="server" onblur="if (this.value == '') {this.value ='First name';}"
                            onfocus="if (this.value =='First name') {this.value ='';}" value="First name"
                            CssClass="textbox textbox-join"></asp:TextBox>
                        <asp:TextBox ID="txtLastName" type="text" runat="server" onblur="if (this.value == '') {this.value ='Last name';}"
                            onfocus="if (this.value =='Last name') {this.value ='';}" value="Last name" class="textbox textbox-join" />
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox textbox-join" onblur="if (this.value == '') {this.value ='Email';}"
                            onfocus="if (this.value =='Email') {this.value ='';}" value="Email"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            Font-Size="Small" Display="Dynamic" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>--%>
                      <%-- <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            Font-Size="Small" Display="Dynamic" ForeColor="Red" Text="Please enter email in correct format"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                        
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox textbox-join"
                                Text="Password" onblur="if (this.value == '') {this.value ='Password';}" onfocus="if (this.value =='Password') {this.value ='';}"></asp:TextBox><br />
                            <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword"
                                ValidationExpression="(?=.{8,})[a-zA-Z]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+" Display="Dynamic"
                                Font-Size="Small" ForeColor="Red" ErrorMessage="Password must be 8 characters and have both letters and numbers." />
                       
                        <br />
                        
                            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" Font-Size="Small"></asp:Label><br />
                            <asp:Button ID="btnJoin" runat="server" OnClick="BtnJoinClick" Text="Join Now" CssClass="button-green button-green-join" /> 
                        
                        <b class="star">*</b>
                        <span class="join">Join Huntable today for free.</span></div>
                        <p class="privacy">
                            <img src="images/icon-privacy.png" width="14" height="18" alt="Privacy" /><b>Huntable
                                Protects Your Privacy</b></p>
                                
                                </div></div>
                                 <a class="button-green button-green-join show_hide">
            Join now
            </a>
                  
                    <b class="star">*</b>
                    <span class="join" style="margin-top:-36px;margin-left:115px;">Join Huntable today for free.</span>
                    <%--<p class="privacy" style="width:66%;"> <img width="14" height="18" alt="Privacy" src="images/icon-privacy.png" /><b>Huntable Protects Your Privacy</b></p>--%>
                   
                    </div>
                    
                  <div class="box-right box-right-new">
               
                    	<h3 style="text-shadow: 0px 1px 1px rgb(255, 255, 255);color: rgb(102, 102, 102);" ><asp:Label runat="server" ID="lblInviteename"></asp:Label> Activites</h3>
                    
                    <table class="a-table">
  <tr style="font-size:12px">
    <td width="156" valign="top">Following</td>
    <td width="19" valign="top">-</td>
    <td width="159" valign="top"><strong><asp:Label runat="server" ID="lblfollowing"></asp:Label></strong></td>
  </tr>
  <tr style="font-size:12px">
    <td valign="top">Followers</td>
    <td valign="top">-</td>
      <td valign="top"><strong><Asp:label runat="server" ID="lblfollowers"/></strong></td>
  </tr>
  <tr style="font-size:12px">
    <td valign="top">Companies Following</td>
    <td valign="top">-</td>
      <td valign="top"><strong><Asp:label runat="server" ID="lblcompaniesfollowing"/></strong></td>
  </tr>
  <tr style="font-size:12px" >
    <td valign="top">Profile Views</td>
    <td valign="top">-</td>
    <td valign="top"><strong><Asp:Label runat="server" ID="lblProfileViews"></Asp:Label></strong></td>
  </tr>
  <tr style="font-size:12px">
    <td valign="top">Affiliate Earnings</td>
    <td valign="top">-</td>
      <td valign="top"><strong>$ <Asp:label runat="server" ID="lblAffilateEarnings"/></strong></td>
  </tr>
  <tr style="font-size:12px">
    <td valign="top">Jobs on site</td>
    <td valign="top">-</td>
      <td valign="top"><strong><Asp:label runat="server" ID="lbljobs"/></strong></td>
  </tr>
</table>

                
              </div>
              <%--<a href="aboutus.aspx" style="margin-top:15px; font-size:14px; float:left;" class="accounts-link">Find out more &rsaquo;&rsaquo;</a>--%>
              <br></br>
              <br></br>
              <br></br>
                </div>

            </div>
        </div>
        <!-- Banner section ends -->
    </div>
    <!-- Header section ends -->
    <%--<div style="width:100%">
    <div style="width:980px">--%>

  <%-- </div>
   </div>--%>
    <div id="content-section">
        <div id="content-inner">
         <div id="search-inner1" style="margin-left:7px;width:1013px;">
            <label style="margin-left:0%;">
                Search People:</label>
            <input id="txtSearchPeople" runat="server" type="text" onblur="if (this.value == '') {this.value ='e.g: Name, Company, Skill, Job title';}"
                onfocus="if (this.value =='e.g: Name, Company, Skill, Job title') {this.value ='';}"
                value="e.g: Name, Company, Skill, Job title" class="textbox-search" />
            <label style="margin-left:1%;">
                Search Jobs:</label>
            <input id="txtSearchJobs" runat="server" type="text" onblur="if (this.value == '') {this.value ='e.g: Job title, Skill, Keyword, Location';}"
                onfocus="if (this.value =='e.g: Job title, Skill, Keyword, Location') {this.value ='';}"
                value="e.g: Job title, Skill, Keyword, Location" class="textbox-search" />
                 <label style="margin-left:1%;">
                Search Company:</label>
            <input id="txtSearchCompany" runat="server" type="text" onblur="if (this.value == '') {this.value ='e.g: Companyname, Location, Industry';}"
                onfocus="if (this.value =='e.g: Companyname, Location, Industry') {this.value ='';}"
                value="e.g: Companyname, Location, Industry" class="textbox-search" />
            <br />
            <br />
            <asp:LinkButton ID="btnSearchPeopleOrJobs" OnClick="BtnSearchClick" runat="server"
                CausesValidation="false" CssClass="button-orange button-orange-search" style="margin-right:45%;">Search<img src="images/search-arrow.png" width="22" height="23" alt="arrow"/></asp:LinkButton>
        </div>
           <h2 class="join-fmly"> Join <asp:Label runat="server" ID="lblname"></asp:Label>'s network and create your own Network in 2 easy steps.</h2>
    <ul class="customized-img-list">
    	<li>
        	<p><a href="Super-power.aspx">Super Power Your Profile</a></p>
       <a href="Super-power.aspx"> <img src="images/customized-img1.jpg" width="139" height="94" alt="Super Power Your Profile" /> </a>
        </li>
        <li>
        	<p><a href="Super-power.aspx">Connect &amp; Network</a></p>
        <a href="Super-power.aspx"><img src="images/customized-img4.jpg" width="139" height="94" alt="Connect &amp; Network" /> </a>
        </li>
        <li>
        	<p><a href="Super-power.aspx">Customize feeds &amp; jobs you receive</a></p>
        <a href="Super-power.aspx"><img src="images/customized-img2.jpg" width="139" height="94" alt="Customize feeds &amp; jobs you receive" /> </a>
        </li>
        <li>
        	<p><a href="Super-power.aspx">Follow your favourite person, company skill or industry.</a></p>
        <a href="Super-power.aspx"><img src="images/customized-img3.jpg" width="139" height="94" alt="Follow your favourite person, company skill or industry." /> </a>
        </li>
        <li>
        	<p><a href="Super-power.aspx">Get Headhunted</a></p>
        <a href="Super-power.aspx"><img src="images/customized-img5.jpg" width="139" height="94" alt="Get Headhunted" /></a> 
        </li>
        <li style="margin-right:0px;">
        	<p><a href="Super-power.aspx">Find your Dream Job</a></p>
       <a href="Super-power.aspx"> <img src="images/customized-img6.jpg" width="139" height="94" alt="Find your Dream Job" /></a> 
        </li>
    </ul>
    <div class="upgrade register-font" > <strong ><a href="Default.aspx" class="accounts-link register-link">Register Now</a> and its totally <strong class="red-color">Free !!!</strong></strong> 
     </div>
     
        </div>
    </div>
    <!-- Content section ends -->
    <!-- Range Slider Script Begins -->
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <!-- Range Slider Script Ends -->
    <%-- <script type="text/javascript">
        $(function () {
            $("#Slider").dragval();
        });	
    </script>--%>
    <!-- Footer section ends -->
    <script type="text/javascript">

        $(document).ready(function () {

            $(".slidingDiv").hide();
            $(".show_hide").show();

            $('.show_hide').click(function () {
                $(".slidingDiv").slideToggle();
            });

        });
 
</script>
    </div>
</asp:Content>
