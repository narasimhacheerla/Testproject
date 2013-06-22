<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableSessionState="true"
    AutoEventWireup="true" CodeBehind="Applicants.aspx.cs" Inherits="Huntable.UI.Applicants" %>

<%@ Register Src="UserControls/JobControl.ascx" TagPrefix="snovaspace" TagName="SearchJobs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

            var msie6 = $.browser == 'msie' && $.browser.version < 7;
            if (!msie6) {
                var top = $('#bx23').offset().top;
                $(window).scroll(function (event) {
                    var y = $(this).scrollTop();
                    if (y >= 57) { $('#bx23').addClass('fixed'); }
                    else { $('#bx23').removeClass('fixed'); }
                });
            }
        });</script>
            <script type="text/javascript">
                function overlay(id) {
                    el = document.getElementById('ovrly');
                    $('#ovrly').show();

                    $('#<%= pbl.ClientID %>').text(id);

                }



                $(document).ready(function () {

                    $('#ximg').click(function () {
                        $('#ovrly').hide();
                        return false;
                    });
                });
    </script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
     <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>

    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="accounts-profile accounts-profile1">
                    <a href="#" class="accounts-link">My Accounts</a>&nbsp;&rsaquo;&rsaquo;&nbsp;<a>My job
                        applicants</a>
                </div>
                <div class="notification" style="margin-top: 12px; width: 755px; }">
                    <div id="bx23">
                        <snovaspace:SearchJobs ID="SearchJobs1" runat="server"></snovaspace:SearchJobs>
                    </div>
                    <div class="notification-right notification-right1" style="margin-left: 221px;">
                        <h3 class="login-heading">
                            Your Job Applicants(<Asp:Label runat="server" ID="lblCount"></Asp:Label>)</h3>
                        <asp:Label runat="server" Font-Bold="true" ForeColor="Red" Font-Size="Large" ID="lblMessage"></asp:Label>
                        <asp:ListView ID="rspdata" runat="server" OnItemCommand="LvJobsApplicantsItemCommand">
                            <LayoutTemplate>
                                <table cellpadding="2" runat="server" id="tblEmployees" style="width: 460px">
                                    <tr runat="server" id="itemPlaceholder">
                                    </tr>
                                </table>
                                <div class="pagination1">
                                    <br />
                                    <asp:DataPager ID="dpgSearchResultsPager" runat="server" PagedControlID="rspdata"
                                        PageSize="10">
                                        <Fields>
                                            <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
                                            <asp:NumericPagerField />
                                            <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="notification-head">
                                    <div class="user-left">
                                        <asp:ImageButton runat="server" ID="ibtnSenderProfilePicture" ImageUrl='<%#Eval("UserProfilePictureDisplayUrl")%>'
                                            CssClass="profile-pic profile-pic2" Width="76" Height="81" PostBackUrl='<%#UrlGenerator(Eval("Id")) %>' />
                                        <asp:ImageButton ID="imgAvailability" runat="server" ImageUrl='<%#Eval("UserAvailabilityImagePath") %>'
                                            Width="16px" Height="16px" OnClientClick='<%# GetUrl(Eval("Id")) %>' />
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("MemberAvailabilityMessage") %>'></asp:Label>
                                    </div>
                                    <div class="user-right">
                                        <a href='<%#UrlGenerator(Eval("Id")) %>' class="accounts-link">
                                            <%#Eval("Name") %></a><br />
                                        <a href='<%#UrlGenerator(Eval("Id")) %>' class="accounts-link">
                                            <%#Eval("CurrentPosition")%></a> at <a href='<%#UrlGenerator(Eval("Id")) %>'
                                                class="accounts-link">
                                                <%# Eval("CurrentCompany")%></a><br />
                                        <strong>Past : </strong><a href="#" class="accounts-link">
                                            <%#Eval("PastPosition")%></a> at <a href="#" class="accounts-link">
                                                <%#Eval("PastCompany")%></a><a href='<%#UrlGenerator(Eval("Id")) %>'
                                                    class="details-link details-link-user">More...</a><br />
                                        <strong>
                                            <%--  <%#Eval("Address.City")%>--%>
                                            : </strong>
                                        <%-- <%#Eval("Address.Country")%><br />--%>
                                        <strong>Experience : </strong>
                                        <%#Eval("TotalExperienceInYears") %>
                                        Years<br />
                                    </div>
                                    <%--    <div class="notification-links notification-links2">
                                        <b>Available Now :</b>
                                        <%# Eval("UserAvailabilityInformation")%><a href='<%# "ViewUserProfile.aspx?UserId=" + Eval("Id") %>'
                                            class="details-link"><%# Eval("ProfessionalRecommendationsCount") %>
                                            Professional Recommendations&nbsp;&nbsp;<img src="images/pagination-arrow-right.png"
                                                width="4" height="7" alt="arrow" /></a>
                                    </div>
                                    <div class="total-views">
                                        <div class="floatleft">
                                            <a href="#" class="accounts-link">Times Profile viewed :<%#Eval("ProfileVisitedCount") %></a>&nbsp;&nbsp;<a
                                                href="#" class="accounts-link">Affiliate Earnings :<%#Eval("AffliateAmountAsText") %></a></div>--%>
                                    <%--    <asp:Button ID="btnMessage" Text="Message" runat="server" UseSubmitBehavior="false"
                                            CssClass="button-orange button-orange-msg" OnClick="BtnMessageClick" CommandArgument='<%#Eval("Id")%>'
                                            OnClientClick='<%# DataBinder.Eval(Container.DataItem, "Name", "javascript:return rowAction(this.name,\"{0}\");")%>' />--%>
                                    <div class="notification-links notification-links1" style="margin-top: -35px;">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="invite-friend-btn invite-friend-btn1"
                                            Text="Delete" CommandName="jobDelete" CommandArgument='<%# Eval("Id")%>' />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
            <div class="content-inner-right">
                <div class="post-opportunity" id="ProfileHuntablediv" runat="server" >
                    <a id="upgradelink" runat="server" class="button-orange floatleft " style="font-size: 12px;
                        padding: 7px 10px;">Is Your Profile Huntable? Update-Edit now</a>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="post-opportunity">
                    <asp:Button ID="Button1" class="button-green post-opportunity-btn" runat="server"
                        Text="Post an Opportunity" OnClick="BtnPostOpportunityClick" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="google-add">
                         <asp:Image ID="bimage" runat="server" CssClass="advert1"  ImageUrl="images/basic-user-advert.gif"/>
                <asp:Image ID="pimage" runat="server" CssClass="advert1"   ImageUrl="images/premium-user-advert.gif" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
