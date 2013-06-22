<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CustomizeJobsUsers.aspx.cs" Inherits="Huntable.UI.CustomizeJobsUsers" %>

<%@ Register Src="~/Controls/DataPager.ascx" TagPrefix="uc" TagName="DataPager" %>
<%@ Register Src="~/UserControls/FeaturedRecruiters.ascx" TagName="uclfeatured" TagPrefix="uc6" %>
<%@ Register Src="~/UserControls/SeeYourFriendsInvitations.ascx" TagName="uslseemore"
    TagPrefix="uc7" %>
<%@ Register Src="~/UserControls/ImportContacts.ascx" TagName="uclimport" TagPrefix="uc8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        div.st_horizontal div.st_sliding_active div.st_slide_container
        {
            margin: 0px;
        }
        div.st_horizontal ul.st_tabs li a
        {
            padding: 10px 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="header-section">
        <p class="top-strip">
            &nbsp;</p>
        <!-- this script used for both clickable slide and tab slide function -->
        <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
        <!-- this script used for both clickable slide and tab slide function -->
        <script type="text/javascript">
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
        </script>
    </div>
    <!-- Header section ends -->
    <div id="content-section">
        <div id="content-inner">
            <div class="green-desc">
                Customize Your Feeds and jobs here. Select either individual or multiple selections
                from various selection.
            </div>
            <div class="content-inner-left">
                <ul class="customize-feed">
                    <li><a href="Customize-User.aspx" style="border-right: 1px solid #fff;">Customize Feeds</a></li>
                    <li><a href="CustomizeJobsSalary.aspx" class="select">Customize Jobs</a></li>
                </ul>
                <div id="st_horizontal" class="st_horizontal">
                    <div class="st_tabs_container st_sliding_active">
                        <div style="overflow: visible;" class="st_slide_container">
                            <ul style="margin-left: 0px;" class="st_tabs">
                                <li><a href="CustomizeJobsSalary.aspx" class="st_tab st_first_tab st_tab_active">Salary</a></li>
                                <li><a href="CustomizeJobsIndustry.aspx" class="st_tab">Industry</a></li>
                                <li><a href="CustomizeJobsSkill.aspx" class="st_tab ">Skill</a></li>
                                <li><a href="CustomizeJobsJobType.aspx" class="st_tab">Job type</a></li>
                                <%--<li><a href="CustomizeJobsCompany.aspx" class="st_tab">Company</a></li>
                                <li><a href="CustomizeJobsInterest.aspx" class="st_tab">Interest</a></li>--%>
                                <li><a href="CustomizeJobsCountry.aspx" class="st_tab">Country</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="job-type job-type-user">
                    <asp:CheckBox ID="cbShowConnections" runat="server" CssClass="checkbox-tab" OnCheckedChanged="CbShowConnectionsCheckedChanged" />
                    <label>
                        <strong>Show Feeds From my Connections</strong></label>
                </div>
                <div class="profile-search">
                    <b>Search for User:</b><asp:TextBox runat="server" ID="txtTopSearchKeyword" CssClass="textbox-search textbox-search-inner textbox-search-customize"
                        name="email" /><a runat="server" id="btnSearchTopKeyword" class="button-orange button-orange-search">Search<img
                            alt="arrow" src="HuntableImages/search-arrow.png" width="22" height="23"></a></div>
                <div class="notification notification-user">
                    <div class="notification-left">
                        <asp:TextBox ID="txtSearchSkills" runat="server" onblur="if (this.value == '') {this.value ='Skills';}"
                            CssClass="textbox textbox-search1" onfocus="if (this.value =='Skills') {this.value ='';}"
                            value="Skills" />
                        <asp:ImageButton ID="btnSkillsSearch" OnClick="BtnSkillsSearchClick" ImageUrl="HuntableImages/search-img.png"
                            Width="28" Height="27" runat="server" />
                        <br>
                        <br>
                        <asp:TextBox ID="txtSearchKeywords" runat="server" onblur="if (this.value == '') {this.value ='Keywords';}"
                            CssClass="textbox textbox-search1" onfocus="if (this.value =='Keywords') {this.value ='';}"
                            value="Keywords" />
                        <asp:ImageButton ID="btnKeywordSearch" OnClick="BtnKeywordSearchClick" ImageUrl="HuntableImages/search-img.png"
                            Width="28" Height="27" runat="server" />
                        <asp:TextBox ID="txtSearchFirstName" runat="server" onblur="if (this.value == '') {this.value ='First name';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='First name') {this.value ='';}"
                            name="email" value="First name" />
                        <asp:TextBox ID="txtSearchLastName" runat="server" onblur="if (this.value == '') {this.value ='Last name';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Last name') {this.value ='';}"
                            name="email" value="Last name" />
                        <asp:TextBox ID="txtTitle" runat="server" onblur="if (this.value == '') {this.value ='Title';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Title') {this.value ='';}"
                            name="email" value="Title" />
                        <select class="textbox selectbox-inner">
                            <option>Current or Past</option>
                            <option>Job Type 1</option>
                            <option>Job Type 2</option>
                        </select>
                        <asp:TextBox ID="txtSearchCompany" runat="server" onblur="if (this.value == '') {this.value ='Company';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Company') {this.value ='';}"
                            name="email" value="Company" />
                        <asp:TextBox ID="txtSearchSchool" runat="server" onblur="if (this.value == '') {this.value ='School';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='School') {this.value ='';}"
                            name="email" value="School" />
                        <asp:TextBox ID="txtSearchExp" runat="server" onblur="if (this.value == '') {this.value ='Experience';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Experience') {this.value ='';}"
                            name="email" value="Experience" />
                        <asp:TextBox ID="txtSearchYear" runat="server" onblur="if (this.value == '') {this.value ='Year';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Year') {this.value ='';}"
                            name="email" value="Year" />
                        <br>
                        <br>
                        <b>Available Now</b>
                        <asp:DropDownList ID="ddlAvailableNow" runat="server" CssClass="textbox selectbox-inner">
                            <asp:ListItem Text="Yes" Selected="True" />
                            <asp:ListItem Text="No" />
                        </asp:DropDownList>
                        <br>
                        <br>
                        <b>Country</b>
                        <asp:DropDownList ID="ddlSearchCountry" runat="server" CssClass="textbox selectbox-inner" />
                        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="button-green button-green-jobpost"
                            OnClick="BtnSearchClick" />
                    </div>
                    <div class="notification-right">
                        <ul class="user-list">
                            <asp:DataList ID="lstUsers" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                    <li><a href="ViewUserProfile.aspx">
                                        <img id="Img1" class="profile-pic profile-pic-user" alt="User" runat="server" src='<%#Eval("UserProfilePictureDisplayUrl")%>'
                                            width="46" height="45"></a><a class="accounts-link" href="ViewUserProfile.aspx"><%#Eval("Name")%></a><br>
                                        <%#Eval("CurrentPosition")%>,
                                        <%#Eval("CurrentCompany")%><br>
                                        <strong>Location:</strong>
                                        <%#Eval("CountryName")%>
                                        <div id="Div1" class="floating" visible='<%#Eval("IsUserFollowing")%>' runat="server">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("Id")%>'
                                                OnClick="FollowupClick" class="invite-friend-btn">
                                                Follow +</asp:LinkButton></div>
                                        <div id="Div2" class="floating" visible='<%#Eval("IsUserNotFollowing")%>' runat="server">
                                            <asp:Image runat="server" Width="20" Height="20" ID="Following" ImageAlign="Right"
                                                ImageUrl="images/tick.png" />
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:DataList>
                        </ul>
                        <div class="pagination pagination-user">
                            <uc:DataPager ID="pagerUsers" runat="server" PageIndex="1" RecordsPerPage="10" TotalRecords="0" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- content inner left ends -->
            <div class="content-inner-right">
                <uc6:uclfeatured ID="uc6" runat="server" />
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc8:uclimport ID="uc8" runat="server" />
                    <p class="margin-top-visible">
                        &nbsp;</p>
                    <div class="box-right">
                        <uc7:uslseemore ID="uc7" runat="server" />
                    </div>
                    <!-- content inner right ends -->
                </div>
                <!-- content inner ends -->
            </div>
            <!-- content section ends -->
            <!-- Range Slider Script Begins -->
            <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
            <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
                type="text/javascript"></script>
            <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
            <!-- Range Slider Script Ends -->
            <!-- Footer section ends -->
        </div>
    </div>
</asp:Content>
