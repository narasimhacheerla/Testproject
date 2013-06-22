<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="WhatIsHuntableUpgrade.aspx.cs" Inherits="Huntable.UI.page24" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="profile-main">
                <div class="accounts-profile accounts-profile-upgrade">
                    <h3 style="float: left;">
                        Your current membership:<asp:Label runat="server" ID="accountstat"></asp:Label></h3>
                    <asp:Button ID="btnupgrade1" runat="server" OnClick="BtnbasicClick" Text="Upgrade"
                        CssClass="button-orange floatleft marginleft" />
                </div>
                <div id="up1" runat="server" style="float: right; margin-right: 70px;">
                    <p>
                        <h5>
                            <asp:CheckBox ID="chck"  runat="server" />
                            * by joining, you agree to Huntable's
                            <asp:LinkButton ID="LinkButton1" runat="server" Text="Terms & conditions" OnClick="lnkTerms_Click"
                                CausesValidation="false"></asp:LinkButton>,
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" OnClick="lnkPrivacy_Click"
                                Text="Privacy policy"></asp:LinkButton>, and &nbsp<a href="AboutUs.aspx">Cookie policy</a></h5>
                    </p>
                    <asp:CustomValidator ForeColor="red"   runat="server" ID="CheckBoxRequired"
                                EnableClientScript="true" ErrorMessage="You must Agree Huntable Terms to proceed."
                                ClientValidationFunction = "ValidateCheckBox"></asp:CustomValidator>
                </div>
                <%--<asp:Panel ID="pnl2" runat="server" Height="362px">
                    <table class="style10" style="height: 348px">
                        <tr>
                            <td class="style19">
                                Features</td>
                            <td class="style18">
                                <asp:Panel ID="pnl3" runat="server" Height="320px" Width="218px" BorderColor=Black>
                                    <table class="style10" style="height: 317px; width: 98%;">
                                        <tr>
                                            <td class="style20">
                                            <asp:image ID="imgjob" runat="server" img src="images/jobseekers.png" 
             />
                                                <br />
                                                <asp:Button ID="btnjob" runat="server" BorderStyle="None" 
                                                    Text="For Job seekers" Width="136px" />
                                            </td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnl4" runat="server" Height="186px">
                                                <p>
                                                <font color="blue">
                                                Search and contact anyone you need Send private messages to any one Chat with anyone who's online Search & apply for jobs Customize your feeds Customize jobs you receive invite friends and earn money Get a complete stats about who's voewd your profile 
                                                </font>
                                                </p>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td class="style22">
                                <asp:Panel ID="pnl5" runat="server" Height="310px" Width="181px">
                                    <table class="style10" style="height: 303px; width: 98%;">
                                        <tr>
                                            <td class="style20">
                                                <asp:image ID="imgemp" runat="server" img src="images/emp.png" 
             />
                                                <br />
                                                <asp:Button ID="btnemp" runat="server" BorderStyle="None" 
                                                    Text="Employers/Companies" Width="137px" Height="22px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnl6" runat="server" Height="212px">
                                                <p style="height: 207px"><font color="blue">

                                                    Rregister your company get followers to your compnay Post Jobs for only $1 a job 
                                                    Search and Hire anyone for FREE There is no need to connect.Send message to any 
                                                    user Customize your feeds to follow like minded users Promote your company</font>
                                                </p>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                                <table class="style10" style="height: 317px">
                                    <tr>
                                        <td class="style30">
                                            <asp:image ID="imgrecuit" runat="server" img src="images/recuit.png" 
             />
                                            <br />
                                            <asp:Button ID="btnrecuit" runat="server" BorderStyle="None" Text="Recruiters" 
                                                Width="131px" />
                                        </td>
                                        <td class="style23">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="style31">
                                            <asp:Panel ID="pnl7" runat="server" Height="183px">
                                            <p>
                                            <font color="blue">
                                            head hunt anyone around the world search and contact any one Chat & conduct interview online Post jobs for only $1 Get a complete report who's applied Filter your favourite staff 1 click Job upload-NO need to type all details
                                            </font>
                                            </p>
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>--%>
                <div class="accounts-box-main">
                    <div class="upgrade-box-main" runat="server">
                        <div class="premium">
                            <span style="float: left;">Upgrade to Huntable Premium Membership<br />
                                One membership Great features for everyone. <strong class="red-color">Just $10 - One
                                    off fees</strong></span>
                            <img src="/images/premium.png" align="right" width="83" height="83" alt="premium" />
                        </div>
                        <div class="upgrade-box">
                            <img src="/images/job-seeker-img.jpg" class="profile-pic" width="300" height="121"
                                alt="Job-Seeker" /><br />
                            <div class="upgrade-btn">
                                <a href="#">For Job Seeker</a>
                            </div>
                            <ul class="upgrade-list">
                                <li>Search and contact any other user</li>
                                <li>Send private messages to other users</li>
                                <li>Chat with anyone who's online</li>
                                <li>Search &amp; apply for jobs</li>
                                <li>Customize your feeds</li>
                                <li>Customise job details received</li>
                                <li>Invite friends and earn money</li>
                                <li>View complete stats regarding profile views</li>
                            </ul>
                            <div class="upgrade-link">
                                <a href="#">Show More
                                    <img src="/images/people-arrow.png" width="7" height="4" alt="arrow" />
                                </a>
                            </div>
                        </div>
                        <div class="upgrade-box">
                            <img src="/images/companies-img.jpg" class="profile-pic" width="300" height="121"
                                alt="Top Companies" /><br />
                            <div class="upgrade-btn">
                                <a href="#">Employers/Companies</a>
                            </div>
                            <ul class="upgrade-list">
                                <li>Register your company</li>
                                <li>Attract followers</li>
                                <li>Post jobs - only $5 per job!</li>
                                <li>Search and Hire anyone for FREE</li>
                                <li>No need to connect - send message to any user</li>
                                <li>Customise your feeds to follow relevant users</li>
                                <li>Promote your company</li>
                            </ul>
                            <div class="upgrade-link">
                                <a href="#">Show More
                                    <img src="/images/people-arrow.png" width="7" height="4" alt="arrow" />
                                </a>
                            </div>
                        </div>
                        <div class="upgrade-box" style="margin-right: 0px;">
                            <img src="/images/recruiters-img.jpg" class="profile-pic" width="300" height="121"
                                alt="Recruiters" /><br />
                            <div class="upgrade-btn">
                                <a href="#">Recruiters</a>
                            </div>
                            <ul class="upgrade-list">
                                <li>Convert out-dated Cv’s to Live database</li>
                                <li>FREE software for all your recruitment needs</li>
                                <li>Headhunt & contact anyone around the world</li>
                                <li>Ability to discuss & conduct interview online with user</li>
                                <li>Post jobs for as little as $5</li>
                                <li>Receive complete applicant report</li>
                                <li>Filter your favourite candidates</li>
                            </ul>
                            <div class="upgrade-link">
                                <a href="#">Show More
                                    <img src="/images/people-arrow.png" width="7" height="4" alt="arrow" />
                                </a>
                            </div>
                        </div>
                        <div class="current-member" id="UpgradeBox" runat="server">
                            <div class="current-member1">
                                Your Current Membership</div>
                            <ul class="current-member-list">
                                <li style="background-color: #f0f0f0; height: 30px;"><span class="mem-list1">&nbsp;</span>
                                    <asp:Button ID="Button1" runat="server" BorderStyle="None" Text="Basic Free version"
                                        CssClass="mem-list2" OnClick="BtnbasicClick" />
                                    <input type="button" value="Premium&#13;&#10;Paid version-&#13;&#10;Only $10" style="text-align: center;
                                        border-style: none; vertical-align: top; float: right;" />
                                    <%--<asp:Button ID="Button2" runat="server" BorderStyle="None" CssClass="memab-list3" Text="Premium Paid version- Only $10" />--%>
                                    <%-- <span class="mem-list2"><strong>BASIC</strong><br />Free version</span> --%>
                                    <%--<span class="mem-list3"><strong>PREMIUM</strong><br />Paid version - Only $10</span>--%>
                                </li>
                                <li><span class="mem-list1">Chat with online users</span> <span class="mem-list2">
                                    <img src="/images/icon-no.png" width="16" height="16" alt="no" /></span> <span class="mem-list3">
                                        <img src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span>
                                </li>
                                <li><span class="mem-list1">1 Click CV uploader</span><span class="mem-list3"><img
                                    src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span><span class="mem-list3"><img
                                        src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span> </li>
                                <li><span class="mem-list1">Huntable CV optimiser</span><span class="mem-list3"><img
                                    src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span><span class="mem-list3"><img
                                        src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span> </li>
                                <li><span class="mem-list1">Customised Feeds</span><span class="mem-list3"><img src="/images/icon-tick.png"
                                    width="18" height="16" alt="tick" /></span><span class="mem-list3"><img src="/images/icon-tick.png"
                                        width="18" height="16" alt="tick" /></span> </li>
                                <li><span class="mem-list1">Customised Jobs</span><span class="mem-list3"><img src="/images/icon-tick.png"
                                    width="18" height="16" alt="tick" /></span><span class="mem-list3"><img src="/images/icon-tick.png"
                                        width="18" height="16" alt="tick" /></span> </li>
                                <li><span class="mem-list1">Get your profile Huntable globally</span><span class="mem-list3"><img
                                    src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span><span class="mem-list3"><img
                                        src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span> </li>
                                <li><span class="mem-list1">Search &amp; apply for jobs</span><span class="mem-list3"><img
                                    src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span><span class="mem-list3"><img
                                        src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span> </li>
                                <li><span class="mem-list1">Earn money through affiliate programme</span> <span class="mem-list2">
                                    <img src="/images/icon-no.png" width="16" height="16" alt="no" /></span> <span class="mem-list3">
                                        <img src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span>
                                </li>
                                <li><span class="mem-list1">Post jobs from only $5</span> <span class="mem-list2">
                                    <img src="/images/icon-no.png" width="16" height="16" alt="no" /></span> <span class="mem-list3">
                                        <img src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span>
                                </li>
                                <li><span class="mem-list1">Company profile</span> <span class="mem-list2">
                                    <img src="/images/icon-no.png" width="16" height="16" alt="no" /></span> <span class="mem-list3">
                                        <img src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span>
                                </li>
                                <li><span class="mem-list1">View complete statistics of who's viewed your profile</span>
                                    <span class="mem-list2">
                                        <img src="/images/icon-no.png" width="16" height="16" alt="no" /></span> <span class="mem-list3">
                                            <img src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span>
                                </li>
                                <li><span class="mem-list1">Search &amp; Headhunt anyone</span><span class="mem-list3"><img
                                    src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span><span class="mem-list3"><img
                                        src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span> </li>
                                <li><span class="mem-list1">Get endorsement from collegues</span><span class="mem-list3"><img
                                    src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span><span class="mem-list3"><img
                                        src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span> </li>
                                <li style="border-bottom: none;"><span class="mem-list1">Promote your company by targeted
                                    Ad's</span> <span class="mem-list2">
                                        <img src="/images/icon-no.png" width="16" height="16" alt="no" /></span> <span class="mem-list3">
                                            <img src="/images/icon-tick.png" width="18" height="16" alt="tick" /></span>
                                </li>
                            </ul>
                        </div>
                        <div class="upgrade">
                            <strong class="floatleft">One membership - Great features for everyone. Just $10 - <strong
                                class="red-color">One off fees</strong></strong>&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnupgrade2" runat="server" Visible="False"  CssClass="button-orange" Text="Upgrade"
                                OnClick="BtnbasicClick" />
                            <%-- <a href="#" class="button-orange floatleft" style="margin-left: 20px;">Upgrade</a>--%>
                        </div>
                      <%--  <div id="up2" runat="server" style="float: right; margin-top: 5px;">
                            <p>
                                <h5>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                    * by joining, you agree to Huntable's
                                    <asp:LinkButton ID="LinkButton3" runat="server" Text="Terms & conditions" OnClick="lnkTerms_Click"
                                        CausesValidation="false"></asp:LinkButton>,
                                    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" OnClick="lnkPrivacy_Click"
                                        Text="Privacy policy"></asp:LinkButton>, and &nbsp<a href="AboutUs.aspx">Cookie policy</a></h5>
                            </p>
                        </div>--%>
                    </div>
                </div>
                <br />
                <br />
                <%-- <asp:Button ID="btnbasic" runat="server" BackColor="Silver" BorderStyle="None" Text="Basic"
                                Width="184px" OnClick="btnbasic_Click" />
                       
                            <asp:Button ID="btnpremium" runat="server" BackColor="#CCCCCC" BorderStyle="None"
                                Text="Premium" Width="205px" />--%>
                <br />
            </div>
        </div>
        <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
            type="text/javascript"></script>
        <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript">
                    
        </script>
        <script type="text/javascript">
            function ValidateCheckBox(sender, args) {
                if (document.getElementById("<%=chck.ClientID %>").checked == true) {
                    args.IsValid = true;
                } else {
                    args.IsValid = false;
                }
            }
        </script>
    </div>
</asp:Content>
