<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="JobsearchTips.aspx.cs" Inherits="Huntable.UI.JobSearchTips" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Src="UserControls/JobControl.ascx" TagPrefix="uc1" TagName="JobControl" %>
<%@ Register Src="UserControls/FeaturedRecruiters.ascx" TagName="FeaturedRecruters"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(function () {

            var msie6 = $.browser == 'msie' && $.browser.version < 7;
            if (!msie6) {
                var top = $('#bx24').offset().top;
                $(window).scroll(function (event) {
                    var y = $(this).scrollTop();
                    if (y >= 46) { $('#bx24').addClass('fixed'); }
                    else { $('#bx24').removeClass('fixed'); }
                });
            }
        });</script>
    <script type="text/javascript">
        $(function () {

            var msie6 = $.browser == 'msie' && $.browser.version < 7;
            if (!msie6) {
                var top = $('#bx25').offset().top;
                $(window).scroll(function (event) {
                    var y = $(this).scrollTop();
                    if (y >= 147) { $('#bx25').addClass('fixed'); }
                    else { $('#bx25').removeClass('fixed'); }
                });
            }
        });</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-inner">
        <div class="content-inner-left">
            <div class="profile-main">
                <div class="accounts-profile accounts-profile1">
                    <div class="accounts-profile-left">
                        <asp:ImageButton ID="imgProfile" runat="server" CssClass="profile-pic" Width="76"
                            Height="81" AlternateText="Profile-pic" />
                        <asp:Label ID="lblUName" runat="server" CssClass="profile-name"></asp:Label>
                    </div>
                    <div class="accounts-profile-right">
                        <div class="accounts-top" style="width: 300px; float: left; margin-left: 90px; margin-top: -100px;">
                            <table>
                                <tr>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblPComplete" runat="server" CssClass="profile-complete"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        Complete
                                    </td>
                                    <td style="text-align: left;">
                                        <eo:ProgressBar ID="ProgressBar2" runat="server" Width="90px" BackgroundImage="00060301"
                                            BackgroundImageLeft="00060302" BackgroundImageRight="00060303" ControlSkinID="None"
                                            IndicatorImage="00060304" ShowPercentage="True" Value="30">
                                        </eo:ProgressBar>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="blue-box" style="float: left; margin-left: 90px; margin-top: -70px;">
                            <strong>TIPS:</strong> Make sure your profile is completed before you apply for
                            a job. This will increase your chance of getting a job.
                        </div>
                    </div>
                </div>
            </div>
            <div class="notification notification-job" style="width: 754px; margin-top: 10px;
                }">
                <div id="bx25">
                    <uc1:JobControl ID="JobControl1" runat="server"></uc1:JobControl>
                </div>
                <div class="notification-right notification-right1" style="margin-left: 222px;">
                    <div class="profile-search profile-search-job">
                        <div class="profile-head">
                            <strong class="floatleft">Search For Jobs:</strong> &nbsp;<img src="images/pagination-arrow-right.png"
                                    width="4" height="7" alt="arrow" /></div>
                        <asp:TextBox ID="txtJobsSearchKeyword" runat="server" onblur="if (this.value == '') {this.value ='e.g: Job Title,Keywords, or Company name';}"
                            onfocus="if (this.value =='e.g: Job Title,Keywords, or Company name') {this.value ='';}"
                            value="e.g: Job Title,Keywords, or Company name" class="textbox-search textbox-search-inner"></asp:TextBox>
                        <asp:Button class="button-orange button-orange-search" ID="btnJobsSearch" runat="server"
                            Text="Search" OnClick="btnJobsSearch_Click" />
                        <%--<a href="#" onclick="SearchJob" class="button-orange button-orange-search">Search<img src="images/search-arrow.png"
                            width="22" height="23" alt="arrow" /></a>--%>
                    </div>
                    <div class="searching-tips">
                        <h3 class="login-heading login-heading-search">
                            My Job Searching Tips</h3>
                        <ul class="list-green list-green-searching">
                            <li style="margin-top: 0px;">Make sure your profile is up-to-date</li>
                            <li>Customize your job feeds</li>
                            <li>Follow interested companies so you can keep on track</li>
                            <li>Increase you network, so people can find you</li>
                            <li>Become a premium member your chance of getting found increases</li>
                            <li>Do more relevent searches</li>
                            <li>Set your email notifications</li>
                        </ul>
                        <img src="images/searching-tips-image.jpg" width="148" height="222" alt="Searching-tips-image" /></div>
                    <div class="searching-tips searching-tips-margin" style="width: 452px">
                        <h3 class="login-heading login-heading-search">
                            Jobs you may be Interested in</h3>
                        <asp:UpdatePanel ID="update2" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hJobfield" Value="" runat="server" />
                                <div style="margin-top:40px;">
                                <asp:DataList ID="dtLstJobs" runat="server">
                                    <ItemTemplate>
                                        <div class="notification-head">
                                            <a href="<%#UrlGenerator(Eval("Id")) %>">
                                                <img id="imgBtn" class="profile-pic profile-pic2" width="124" height="70" src='<%#Eval("ProfileImagePath")%>'
                                                    runat="server" />
                                            </a><a href="<%#UrlGenerator(Eval("Id")) %>" class="accounts-link">
                                                <asp:Label ID="jobTitle" Text='<%#Eval("Title") %>' runat="server" Font-Size="Medium"
                                                    Font-Names="Georgia, Arial"></asp:Label></a><br />
                                            <p>
                                                <div style="height: 40px; overflow: hidden;">
                                                    <asp:Label ID="Label1" Text='<%#Eval("JobDescription") %>' runat="server"></asp:Label>
                                                </div>
                                                <div style="margin-left: 139px;">
                                                    <a href="<%#UrlGenerator(Eval("Id")) %>" >more</a>
                                                </div>
                                            </p>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                                </div>
                                <asp:Button ID="btnMore" runat="server" CssClass="show-more" OnClick="BtnJobShowCLick" Text="Show More" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                         <asp:UpdateProgress ID="updateProgress1" AssociatedUpdatePanelID="update2" runat="server">
                                    <ProgressTemplate>
                                        <img src="images/loadingBar.gif" style="height: 50px" alt="Update in progress" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                        <%--<a href="#" class="show-more" style="margin-bottom: 20px;">Show More </a>--%>
                        <p class="margin-top-visible">
                            &nbsp;</p>
                        <div class="blue-box blue-box-tips">
                            Have you customized your job feeds yet? You will receive only jobs you are interested.
                            eg. Administrator, London, £30k.
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="content-inner-right" style="margin-top: 24px;">
            <div class="post-opportunity">
                <asp:Button class="button-green post-opportunity-btn" runat="server" Text="Post an Opportunity" OnClick ="BtnPostOpportunityClick"/>
            </div>
            <p class="margin-top-visible">
                &nbsp;</p>
            <div id="bx24">
                <uc2:FeaturedRecruters ID="ucFeaturedRecruters" runat="server" />
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
