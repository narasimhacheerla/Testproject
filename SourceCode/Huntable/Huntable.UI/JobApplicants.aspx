<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="JobApplicants.aspx.cs" Inherits="Huntable.UI.JobApplicants" %>

<%@ Register Src="UserControls/JobControl.ascx" TagPrefix="snovaspace" TagName="SearchJobs" %>
<%@ Register Src="~/UserControls/FriendsToInvite.ascx" TagPrefix="uc1" TagName="InviteFr" %>
<%@ Register Src="~/UserControls/cvStatistics.ascx" TagPrefix="uc2" TagName="CvStats" %>
<%@ Register Src="~/UserControls/SeeYourFriendsInvitations.ascx" TagPrefix="uc3"
    TagName="FriendsInvitations" %>
<%@ Register Src="~/Controls/DataPager.ascx" TagPrefix="uc" TagName="DataPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $().ready(function () {
            $('#dialogContent').dialog({
                autoOpen: false,
                modal: true,
                bgiframe: true,
                title: "Send Message",
                width: 800,
                height: 400
            });
            $('#dialogContent').parent().appendTo($("form:first"));
        });

        function rowAction(uniqueID, userName) {
            $('#<%=txtToAddress.ClientID %>').val(userName);
            $('#<%=lblName.ClientID %>').text(userName);
            $('#<%=txtSubject.ClientID %>').val('');
            $('#<%=txtMessage.ClientID %>').val('');
            $('#dialogContent').dialog('option', 'buttons',
				{
				    "OK": function () { __doPostBack(uniqueID, ''); $(this).dialog("close"); },
				    "Cancel": function () { $(this).dialog("close"); }
				});

            $('#dialogContent').dialog('open');

            return false;
        }
    </script>
    <script type="text/javascript">
        $(function () {

            var msie6 = $.browser == 'msie' && $.browser.version < 7;
            if (!msie6) {
                var top = $('#bx23').offset().top;
                $(window).scroll(function (event) {
                    var y = $(this).scrollTop();
                    if (y >= 70) { $('#bx23').addClass('fixed'); }
                    else { $('#bx23').removeClass('fixed'); }
                });
            }
        });</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="accounts-profile accounts-profile1">
                    <a href="myaccount.aspx" class="accounts-link">My Accounts</a>&nbsp;&rsaquo;&rsaquo;&nbsp;<a>My job
                        applications</a>
                </div>
                <div class="notification" style="margin-top: 23px;">
                    <div id="bx23"><snovaspace:SearchJobs ID="SearchJobs1" runat="server"></snovaspace:SearchJobs></div>
                    <div class="notification-right notification-right1" style="margin-left:219px;">
                        <h3 class="login-heading">
                            Job Applicants</h3>
                        <asp:Label runat="server" ID="lblMessage"></asp:Label>
                        <asp:Repeater ID="rpJobApplicants" runat="server">
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
                                          <%--  <%#Eval("City")%>
                                            : </strong>
                                        <%#Eval("Country")%><br />--%>
                                        <strong>Experience : </strong>
                                        <%#Eval("TotalExperienceInYears") %>
                                        Years<br />
                                    </div>
                                    <div class="notification-links notification-links2">
                                        <b>Available Now :</b>
                                        <%# Eval("UserAvailabilityInformation")%><a href='<%#UrlGenerator(Eval("Id")) %>'
                                            class="details-link"><%# Eval("ProfessionalRecommendationsCount") %>
                                            Professional Recommendations&nbsp;&nbsp;<img src="images/pagination-arrow-right.png"
                                                width="4" height="7" alt="arrow" /></a>
                                    </div>
                                    <div class="total-views">
                                        <div class="floatleft">
                                            <a href="#" class="accounts-link">Times Profile viewed :<%#Eval("ProfileVisitedCount") %></a>&nbsp;&nbsp;<a
                                                href="#" class="accounts-link">Affiliate Earnings :<%#Eval("AffliateAmountAsText") %></a></div>
                                        <asp:Button ID="btnMessage" Text="Message" runat="server" UseSubmitBehavior="false"
                                            CssClass="button-orange button-orange-msg" OnClick="BtnMessageClick" CommandArgument='<%#Eval("Id")%>'
                                            OnClientClick='<%# DataBinder.Eval(Container.DataItem, "Name", "javascript:return rowAction(this.name,\"{0}\");")%>' />
                                    </div>
                                </div>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <hr />
                            </SeparatorTemplate>
                        </asp:Repeater>
                        <div align="center">
                            <a href="#">
                                <img src="images/google-banner-medium.jpg" width="300" height="100" alt="google-add" /></a>
                        </div>
                        <p class="strip-bottom">
                            &nbsp;</p>
                        <uc:DataPager ID="pagerUsers" runat="server" PageIndex="1" RecordsPerPage="3" TotalRecords="0" />
                        <div id="dialogContent" align="center">
                            To:
                            <asp:TextBox ID="txtToAddress" runat="server" ReadOnly="true" /><br />
                            What would you like to message
                            <asp:Label ID="lblName" runat="server" />
                            ?<br />
                            Subject:
                            <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox><br />
                            <br />
                            Message:
                            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="content-inner-right">
                <div class="post-opportunity">
                    <a id="isyourprofile" runat="server" class="button-orange floatleft " style="font-size: 12px; padding: 7px 10px;">
                        Is Your Profile Huntable? Update-Edit now</a>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="post-opportunity">
                    <a id="postanoppurtuinity" runat="server" class="button-green post-opportunity-btn">Post an Opportunity</a>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="google-add">
                    <asp:Image ID="bimage" runat="server" CssClass="advert1"  ImageUrl="images/basic-user-advert.gif"/>
                <asp:Image ID="pimage" runat="server" CssClass="advert1"   ImageUrl="images/premium-user-advert.gif" />
                </div>
                <%-- <div class="box-right">
                    <uc1:InviteFr ID="InviteFriends" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc2:CvStats ID="CvStatistics" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc3:FriendsInvitations ID="friendsInvts" runat="server" />
                </div>--%>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
