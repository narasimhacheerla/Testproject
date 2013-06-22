<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="Huntable.UI.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The  Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="Styles/style.css" type="text/css" rel="stylesheet" />
    <link href="css/Home-Video-css/style-pop1.css" rel="stylesheet" type="text/css" />
    <link href="css/Home-Video-css/videopop.css" rel="stylesheet" type="text/css" />
    <link href="css/counter.css" rel="stylesheet" type="text/css" />
    <%--<link href="css/style-innerpage.css" rel="stylesheet" type="text/css" />--%>
    <!-- Slider Script Begins -->
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="js/script.js"></script>
    <script type="text/javascript">
        //        $(document).ready(function () {
        //            $("#hideshow1").show();
        //            $("#close_div").click(function () {
        //                $("#hideshow1").hide();
        //            });
        //            $(".close_div").click(function () {
        //                $("#hideshow1").hide();
        //            });
        //        });
        $(document).ready(function () {
            $("#close_div").click(function () {
                $("popup_content").empty();
                $("#MainContent_hideshow1").hide();
                $("iframe#player").attr('src', '');
            });
        });
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
        <p class="top-strip">
            &nbsp;</p>
        <p class="bottom-strip">
            &nbsp;</p>
        <div id="banner-section" style="position: relative">
            <div id="banner-inner">
                <div class="banner-left">
                    <div id="gallery1">
                        <div id="slides">
                            <div class="slide">
                                <img src="images/banner-img1.png" width="651" height="307" alt="banner-img1" />
                                <p class="banner-text1">
                                    You are worth more than you think. Let others find you.</p>
                                <b class="banner-text2"><a href="#">Register now</a> and create a professional identity
                                    for yourself.</b>
                            </div>
                            <div class="slide">
                                <img src="images/banner-img2.png" width="651" height="307" alt="banner-img1" />
                                <p class="banner-text1">
                                    Any Skill Industry, Country, Experience. This is just for you.</p>
                                <b class="banner-text2"><a href="#">Register now</a> and create a professional identity
                                    for yourself.</b>
                            </div>
                            <div class="slide">
                                <img src="images/banner-img3.png" width="651" height="307" alt="banner-img1" />
                                <p class="banner-text1">
                                    Search, Connect, Contact, Headhunt, &amp;Hire the easy &amp; Fast Way</p>
                                <b class="banner-text2">You can find the exact talent and skills you need.Contact or
                                    Chat with
                                    <br />
                                    any user. Find out more...</b>
                            </div>
                            <div class="slide">
                                <img src="images/banner-img4.png" width="651" height="307" alt="banner-img1" />
                                <p class="banner-text1">
                                    Find anyone easily. Contact, or Chat with Relevent Candidates.</p>
                                <b class="banner-text2">The best Recruiters choice. Promote your company and save money
                                    <br />
                                    on job postings. Register now</b>
                            </div>
                            <div class="slide">
                                <img src="images/banner-img5.png" width="651" height="307" alt="banner-img1" />
                                <p class="banner-text1">
                                    Customize jobs, Feeds, Connect with other users, Follow Industry,
                                    <br />
                                    Users, Skills, and many more.</p>
                                <b class="banner-text2"><a href="#">Register now</a> to Find out the opportunities waiting
                                    for you.</b>
                            </div>
                        </div>
                        <div id="menu">
                            <ul>
                                <li class="menuItem"><a href="#">Get headhunted </a></li>
                                <li class="menuItem"><a href="#">All skills - All country </a></li>
                                <li class="menuItem"><a href="#">Companies </a></li>
                                <li class="menuItem"><a href="#">Recruiters</a></li>
                                <li class="menuItem"><a href="#" style="background: none;">Customize-connect-grow</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="banner-right1" onkeypress="return WebForm_FireDefaultButton(event, '<%= btnJoin.ClientID %>')">
                    <h1>
                        Join Huntable Today</h1>
                    <div class="banner-right-inner1">
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
                        <div id="div2">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="textbox textbox-join"
                                Text="Password" onblur="if (this.value == '') {this.value ='Password';}" onfocus="if (this.value =='Password') {this.value ='';}"></asp:TextBox><br />
                            <%--    <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword"
                                ValidationExpression="(?=.{8,})[a-zA-Z]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+" Display="Dynamic"
                                Font-Size="Small" ForeColor="Red" ErrorMessage="Password must be 8 characters and have both letters and numbers." />--%>
                        </div>
                        <br />
                        <div style="border-top: -10px">
                            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" Font-Size="Small"></asp:Label><br />
                            <asp:Button ID="btnJoin" runat="server" OnClick="BtnJoinClick" Text="Join Now" CssClass="button-green button-green-join" />
                            <b class="star">*</b>
                        </div>
                        <%--<b class="star">*</b>--%>
                        <span class="join">Join Huntable today for free.</span>
                        <p class="privacy">
                            <img src="images/icon-privacy.png" width="14" height="18" alt="Privacy" /><b>Huntable
                                Protects Your Privacy</b></p>
                    </div>
                </div>
                <div id="hideshow1" runat="server" style="position: absolute; width: 100%; height: 100%;
                    top: 0; left: 0;">
                    <div id="fade1">
                    </div>
                    <div class="popup_block1">
                        <div class="popupv">
                            <p class="dont-show1" id="dontshowdiv">
                                <asp:Button BorderStyle="None" runat="server" OnClick="donot_click" Text="Do not show this again"
                                    Style="margin-left: 356px; background-color: #888; color: white; height: 29px;
                                    font-weight: bold; padding: 3px; margin-top: -12px;border-radius: 2px;"></asp:Button>
                                <a href="#" class="close_div" onclick="return false;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    <img src="css/Home-Video-css/icon_close.png" class="cntrl" id="close_div" title="Close"></a></p>
                            <iframe id="player" src="http://player.vimeo.com/video/65019649" width="500" height="281"
                                frameborder="0" webkitallowfullscreen="" mozallowfullscreen="" allowfullscreen="">
                            </iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Banner section ends -->
    </div>
    <!-- Header section ends -->
    <div id="search-section" onkeypress="return WebForm_FireDefaultButton(event, '<%= btnSearchPeopleOrJobs.ClientID %>')">
        <div id="search-inner02">
            <label style="margin-left: 60px">
                Search People:</label>
            <input id="txtSearchPeople" runat="server" type="text" onblur="if (this.value == '') {this.value ='e.g: Name, Company, Skill, Job title';}"
                onfocus="if (this.value =='e.g: Name, Company, Skill, Job title') {this.value ='';}"
                value="e.g: Name, Company, Skill, Job title" class="textbox-search" />
            <label>
                Search Jobs:</label>
            <input id="txtSearchJobs" runat="server" type="text" onblur="if (this.value == '') {this.value ='e.g: Job title, Skill, Keyword, Location';}"
                onfocus="if (this.value =='e.g: Job title, Skill, Keyword, Location') {this.value ='';}"
                value="e.g: Job title, Skill, Keyword, Location" class="textbox-search" />
            <label>
                Search Company:</label>
            <input id="txtSearchCompany" runat="server" type="text" onblur="if (this.value == '') {this.value ='e.g: Companyname, Location, Industry';}"
                onfocus="if (this.value =='e.g: Companyname, Location, Industry') {this.value ='';}"
                value="e.g: Companyname, Location, Industry" class="textbox-search" />
            <br />
            <br />
            <asp:LinkButton ID="btnSearchPeopleOrJobs" OnClick="BtnSearchClick" runat="server"
                CausesValidation="false" CssClass="button-orange button-orange-search" Style="margin-right: 530px;">Search<img src="images/search-arrow.png" width="22"
                 height="23" alt="arrow" /></asp:LinkButton>
        </div>
    </div>
    <div id="content-section">
        <div id="content-inner">
            <div class="box">
                <h2>
                    Find Your Dream Job</h2>
                <img src="images/box-arrow.jpg" width="89" height="69" alt="Find your Dream Job" />
                <p>
                    Millions of jobs available for you. Customize your job feeds and get jobs coming
                    to you, exactly what you want in your industry, skill and country.</p>
            </div>
            <div class="box">
                <h2>
                    <asp:Label runat="server" ID="lblip"></asp:Label>
                    Find Staff You Want
                </h2>
                <img src="images/box-people.jpg" width="89" height="69" alt="Find your Dream Job" />
                <p>
                    Search, connect, contact, Headhunt &amp; Hire anyone for any skills and industry
                    around the world.</p>
            </div>
            <div class="box box1">
                <h2>
                    Get Headhunted</h2>
                <img src="images/box-huntered.jpg" width="89" height="69" alt="Find your Dream Job" />
                <p>
                    someone out there needs you. You are worth more than you think. Keep your profile
                    up-to-date so employers &amp; Recruiters can find you.</p>
            </div>
            <div style="float: right">
                <p>
                    <h6>
                        * by joining, you agree to Huntable's
                        <asp:LinkButton runat="server" Text="Terms & conditions" OnClick="lnkTerms_Click"
                            CausesValidation="false"></asp:LinkButton>,
                        <asp:LinkButton runat="server" CausesValidation="false" OnClick="lnkPrivacy_Click"
                            Text="Privacy policy"></asp:LinkButton>, and &nbsp<a href="AboutUs.aspx">Cookie policy</a></h6>
                </p>
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

        var _gaq = _gaq || [];

        _gaq.push(['_setAccount', 'UA-32991521-1']);

        _gaq.push(['_trackPageview']);



        (function () {

            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;

            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';

            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);

        })();
       

    </script>
</asp:Content>
