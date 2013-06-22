<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MyPostedJobs.aspx.cs" Inherits="Huntable.UI.MyPostedJobs" %>

<%@ Register Src="UserControls/JobControl.ascx" TagPrefix="snovaspace" TagName="SearchJobs" %>
<%@ Register Src="UserControls/FeaturedRecruiters.ascx" TagName="FeaturedRecruters"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/NewStylesheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {

            var msie6 = $.browser == 'msie' && $.browser.version < 7;
            if (!msie6) {
                var top = $('#bx26').offset().top;
                $(window).scroll(function (event) {
                    var y = $(this).scrollTop();
                    if (y >= 78) { $('#bx26').addClass('fixed'); }
                    else { $('#bx26').removeClass('fixed'); }
                });
            }
        });</script>
    <script type="text/javascript">
        $(function () {

            var msie6 = $.browser == 'msie' && $.browser.version < 7;
            if (!msie6) {
                var top = $('#bx27').offset().top;
                $(window).scroll(function (event) {
                    var y = $(this).scrollTop();
                    if (y >= 100) { $('#bx27').addClass('fixed'); }
                    else { $('#bx27').removeClass('fixed'); }
                });
            }
        });</script>
    <style type="text/css">
        div#pager
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="accounts-profile accounts-profile1">
                    <a href="myaccount.aspx" class="accounts-link">My Accounts</a>&nbsp;&rsaquo;&rsaquo;&nbsp;<a>My job
                        postings</a>
                </div>
                <div class="notification" style="width: 754px; margin-top: 27px">
                    <div id="bx26">
                        <snovaspace:SearchJobs ID="SearchJobs1" runat="server"></snovaspace:SearchJobs>
                    </div>
                    <div class="notification-right notification-right1" style="margin-left: 210px;">
                        <h3 class="login-heading">
                            My Job Postings(<asp:Label ID="jobCount" runat="server"></asp:Label>
                            jobs)</h3>
                        <div class="notification-head" style="width: 500px">
                            <asp:ListView ID="lvPostedJobs" runat="server" DataKeyNames="Id" OnItemCommand="LvPostedJobsItemCommand">
                                <LayoutTemplate>
                                    <table cellpadding="2" runat="server" id="tblEmployees" style="width: 460px">
                                        <tr runat="server" id="itemPlaceholder">
                                        </tr>
                                    </table>
                                    <div class="pagination">
                                        <div align="center">
                                            <img src="images/google-banner-medium.jpg" width="300" height="100" alt="google-add" />
                                        </div>
                                        <br />
                                        <div id ="pgr" runat="server"  style="margin-left: 100px;
width: 0px;" >
                                        <asp:DataPager ID="dpgSearchResultsPager" runat="server" PagedControlID="lvPostedJobs"
                                            PageSize="10">
                                            <Fields>
                                                <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
                                                <asp:NumericPagerField />
                                                <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" />
                                            </Fields>
                                        </asp:DataPager>
                                        </div>
                                    </div>
                                </LayoutTemplate>
                                <ItemSeparatorTemplate>
                                    <hr />
                                </ItemSeparatorTemplate>
                                <ItemTemplate>
                                    <div class="notification-head">
                                        <a href="#">
                                            <img src="<%#Eval("ProfileImagePath")%>" class="profile-pic profile-pic2" width="76"
                                                height="81" alt="" /></a><a href="<%#UrlGenerator(Eval("Id")) %>" class="accounts-link">
                                                    <%#Eval("Title") %></a><br />
                                        <b class="green-color">Salary
                                            <%# Eval("Symbol") %>
                                            <%# Eval("Salary") %>
                                            k</b><br />
                                        <strong>
                                            <%# Convert.ToString(Eval("LocationName")) + ", " + Convert.ToString(Eval("CountryName"))%></strong><br />
                                        <%#Eval("JobDescription").ToString().Substring(0, Math.Min(500, Eval("JobDescription").ToString().Length))%>
                                        <div class="notification-links notification-links1">
                                            <strong>Date Posted: </strong>
                                            <%#Eval("CreatedDateTime","{0:d/M/yy}")%>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="invite-friend-btn invite-friend-btn1"
                                                Text="Delete" CommandName="jobDelete" CommandArgument='<%# Eval("Id")%>' />
                                        </div>
                                        <div class="total-views">
                                      <%--      <a href="#" class="accounts-link">
                                                <%# "Total Views : " + Convert.ToString(Eval("TotalViews"))%></a>--%>&nbsp;&nbsp;<a href="JobApplicants.aspx?JobId=<%#Eval("Id") %>"
                                                    class="accounts-link">
                                                    <%# "Total Applications : " + Convert.ToString(Eval("TotalApplicants"))%></a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="content-inner-right" style="margin-right: -85px;height:535px;">
                <div class="post-opportunity" id="ProfileHuntablediv" runat="server" >
                    <a href="#" id="isyour" runat="server" class="button-orange floatleft " style="font-size: 12px;
                        padding: 7px 10px;">Is Your Profile Huntable? Update-Edit now</a>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="post-opportunity">
                    <asp:Button ID="Button1" class="button-green post-opportunity-btn" runat="server" Text="Post an Opportunity" OnClick ="BtnPostOpportunityClick"/>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div id="bx27">
                    <uc2:FeaturedRecruters ID="ucFeaturedRecruters" runat="server" />
                </div>
            </div>
            <!-- content inner ends -->
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
