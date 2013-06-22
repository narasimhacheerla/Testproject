<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="~/CustomizeJobsCompany.aspx.cs" Inherits="Huntable.UI.CustomizeJobsCompany" %>

<%@ Register Src="~/UserControls/FeaturedRecruiters.ascx" TagName="uclfeatured" TagPrefix="uc6" %>
<%@ Register Src="~/UserControls/SeeYourFriendsInvitations.ascx" TagName="uslseemore"
    TagPrefix="uc7" %>
<%@ Register Src="~/UserControls/ImportContacts.ascx" TagName="uclimport" TagPrefix="uc8" %>
<%@ Register Src="~/UserControls/atozsearch.ascx" TagName="atoz" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
    <div id="content-section">
        <div id="content-inner">
            <div class="green-desc">
                Customize Your Feeds and jobs here. Select either individual or multiple selections
                from various selection.
            </div>
            <div class="content-inner-left">
                <ul class="customize-feed">
                    <li><a href="Customize-User.aspx" style="border-right: 1px solid #fff;">Customize Feeds</a></li>
                    <li><a href="CustomizeJobsUsers.aspx" class="select">Customize Jobs</a></li>
                </ul>
                <div id="st_horizontal" class="st_horizontal">
                    <div class="st_tabs_container st_sliding_active">
                        <div style="overflow: visible;" class="st_slide_container">
                            <ul style="margin-left: 0px;" class="st_tabs">
                                <li><a href="CustomizeJobsUsers.aspx" class="st_tab">User</a></li>
                                <li><a href="CustomizeJobsIndustry.aspx" class="st_tab">Industry</a></li>
                                <li><a href="CustomizeJobsSkill.aspx" class="st_tab ">Skill</a></li>
                                <li><a href="CustomizeJobsJobType.aspx" class="st_tab">Job type</a></li>
                                <li><a href="CustomizeJobsCompany.aspx" class="st_tab st_first_tab st_tab_active">Company</a></li>
                                <li><a href="CustomizeJobsInterest.aspx" class="st_tab">Interest</a></li>
                                <li><a href="CustomizeJobsCountry.aspx" class="st_tab">Country</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
              <uc:atoz runat="server" ID="ucAtoZFeedsCompany" SearchTitle = "Search by Company:"/>
                <a href="#" class="button-green floatright">Next</a>
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
</asp:Content>
