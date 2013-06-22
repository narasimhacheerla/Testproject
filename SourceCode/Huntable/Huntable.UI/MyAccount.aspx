<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableSessionState="true"
    AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="Huntable.UI.MyAccount" %>

<%@ Register Src="UserControls/JobControl.ascx" TagName="JobControl" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/SeeYourFriendsInvitations.ascx" TagName="seefriends"
    TagPrefix="uc2" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script type="text/javascript">
        $(document).ready(function () {
            //When you click on a link with class of poplight and the href starts with a # 
            $('a.poplight[href^=#]').click(function () {
                var popID = $(this).attr('rel'); //Get Popup Name
                var popURL = $(this).attr('href'); //Get Popup href to define size
                url.src = "ShareMail.aspx";
                //Pull Query & Variables from href URL
                var query = popURL.split('?');
                var dim = query[1].split('&');
                var popWidth = dim[0].split('=')[1]; //Gets the first query string value

                //Fade in the Popup and add close button
                $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="images/close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>');

                //Define margin for center alignment (vertical + horizontal) - we add 80 to the height/width to accomodate for the padding + border width defined in the css
                var popMargTop = ($('#' + popID).height() + 80) / 2;
                var popMargLeft = ($('#' + popID).width() + 80) / 2;

                //Apply Margin to Popup
                $('#' + popID).css({
                    'margin-top': -popMargTop,
                    'margin-left': -popMargLeft
                });

                //Fade in Background
                $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
                $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); //Fade in the fade layer 

                return false;
            });


            //Close Popups and Fade Layer
            $('a.close, #fade').live('click', function () { //When clicking on the close or fade layer...
                $('#fade , .popup_block').fadeOut(function () {
                    $('#fade, a.close').remove();
                }); //fade them both out

                return false;
            });


        });
    </script>--%>
    <script language="javascript" type="text/javascript">
        function ShowSharingPopUp(actin) {
            document.forms[0].action = actin;
            window.open(actin, 'mywindow', 'width=500,height=300,toolbar=no, location=no,directories=no,statusbar=no,menubar=no,scrollbars=no,copyhistory=no, resizable=yes');
            document.forms[0].target = 'mywindow';
            return true;
        }
    </script>
    <div id="content-section">
        <div id="content-inner">
            <div class="accounts-profile accounts-profile1">
                <div class="accounts-profile-left">
                    <asp:Image runat="server" ID="imgProfilePicture" class="profile-pic" Width="76" Height="81"
                        alt="Profile-pic" />
                    
                        <%-- <asp:Label ID="lblProfileName" Text="User Profile Name" Width="100%" runat="server"></asp:Label></a>--%>
                </div>
                <div class="accountsab-profile-right">
                    <div class="accountsab-top">
                        <table width="100%">
                            <tr style="height: 20px;">
                                <td valign="top" style="width: 15%;">
                                    <asp:Label ID="lblPercentCompleted" CssClass="labelProfile" runat="server"></asp:Label>&nbsp;Complete
                                </td>
                                <td valign="top" align="left">
                                    <eo:ProgressBar ID="ProgressBar2" runat="server" Width="90px" BackgroundImage="00060301"
                                        BackgroundImageLeft="00060302" BackgroundImageRight="00060303" ControlSkinID="None"
                                        IndicatorImage="00060304" ShowPercentage="True" Value="30">
                                    </eo:ProgressBar>
                                </td>
                                <td valign="top">
                                  <asp:Label runat="server" Text="Member since:"  Width="90px" ></asp:Label>
                                        <asp:Label ID="lblmember"  runat="server" Width="130px"  ></asp:Label>
                                    
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="accounts-middle">
                        <strong style="float: left; height: 15px;">Primary E-mail:</strong>&nbsp;&nbsp;
                        <div style="float: left; margin-left: 20px;">
                            <asp:Label ID="lblEmail" class="profile-complete" Width="140px" runat="server"></asp:Label><br />
                        </div>
                       <div id="upgrade" runat="server"> <a href="WhatIsHuntableUpgrade.aspx" class="button-green" style="float: right;">Upgrade</a><br /></div>
                        <a href="ChangeEmail.aspx" class="accounts-link">Change</a>&nbsp;/&nbsp;<a href="ChangeEmail.aspx"
                            class="accounts-link">Add</a><br />
                        <strong>Password:</strong> <a href="ChangeEmail.aspx" class="accounts-link">Change</a>
                    </div>
                </div>
                <asp:Label ID="lblProfileName" Text="User Profile Name" Font-Bold="true" Font-Size="12px"
                    ForeColor="#0396ac" runat="server"></asp:Label></a>
            </div>
            <div class=" floatleft" style="width: 280px; margin-left: 20px;">
                <div class="post-opportunity" id="ProfileHuntablediv" runat="server">
                    <a id="editprofileupgrade" runat="server" class="button-orange floatleft " style="font-size: 12px;
                        padding: 7px 10px;">Is Your Profile Huntable? Update-Edit now</a>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="post-opportunity">
                   <asp:Button ID="Button1" class="button-green post-opportunity-btn" runat="server" Text="Post an Opportunity" OnClick ="BtnPostOpportunityClick"/>
                </div>
            </div>
            <div class="accounts-box-main">
                <div class="accounts-box">
                    <b>Profile</b>
                    <div class="accounts-box-img">
                        <img src="images/icon-profile.png" width="54" height="74" alt="Profile" />
                    </div>
                    <ul class="accounts-list">
                        <li><a id="a_viewprofile" runat="server">View Profile</a></li>
                        <li><a id="a_editprofile" runat="server">Edit Profile</a></li>
                        <li><a href="Followers.aspx">Followers</a></li>
                       <%-- <li><a href="#">
                            <li class="last"><a id="ssss" href="#?w=500" class="poplight" rel="popupShare">Share</a></li>--%>
                            <%--<asp:LinkButton runat="server" ID="hyplnkshare" OnClientClick="javascript:window.open('ShareMail.aspx','MyWindow','width=900,height=400,top=100,left=120,resizable=yes'); return false;">Share</asp:LinkButton></a></li>--%>
                            <li><a href="Following.aspx">Following</a></li>
                            <li><a href="Pictures.aspx">My Pictures</a></li>
                            <li><a href="Videos.aspx">My Videos</a></li>
                    </ul>
                </div>
                <div class="accounts-box">
                    <b>Affiliate</b>
                    <div class="accounts-box-img">
                        <img src="images/icon-affiliate.png" width="54" height="74" alt="Profile" />
                    </div>
                    <ul class="accounts-list">
                       <%-- <li><a  href="#">Send Invitations</a></li>--%>
                        <li><a href="InvitationsReport.aspx">Sent Invitations</a></li>
                        <li><a href="InvitationsOverview.aspx">Account Overview</a></li>
                        <li><a href="WithDrawFunds.aspx">Withdraw funds</a></li>
                    </ul>
                </div>
                <div class="accounts-box">
                    <b>Messages</b>
                    <div class="accounts-box-img">
                        <img src="images/icon-message.png" width="54" height="74" alt="Profile" />
                    </div>
                    <ul class="accounts-list">
                        <li><a href="MessageInbox.aspx">Reveived messages</a></li>
                        <li><a href="MessageInbox.aspx">Sent messages</a></li>
                    </ul>
                </div>
                <div class="accounts-box">
                    <b>Jobs</b>
                    <div class="accounts-box-img">
                        <img src="images/icon-jobs.png" width="54" height="74" alt="Profile" />
                    </div>
                    <ul class="accounts-list">
                        <li><a href="MyPostedJobs.aspx">My Job postings</a></li>
                        <li><a href="Applicants.aspx">My applicants</a></li>
                        <li><a runat="server" id="a_jobapplied" href="JobsApplied.aspx">Jobs I Applied</a></li>
                        <li><a id="Buycredits" runat="server">Buy Credits</a></li>
                        <li><a id="profilesta" runat="server">My Profile stats</a></li>
                    </ul>
                </div>
                <div class="accounts-box" style="margin-right: 0px;">
                    <b>Settings</b>
                    <div class="accounts-box-img">
                        <img src="images/icon-setting.png" width="54" height="74" alt="Profile" />
                    </div>
                    <ul class="accounts-list">
                        <li><a href="UserEmailNotification.aspx">E-mail Settings</a></li>
                        <li><a href="CustomizeFeedsInterest.aspx">Customize Feeds</a></li>            
                        <li><a href="ViewPurchases.aspx">View Purchases</a></li>
                        <li><a href="PictureUpload.aspx">Add My profile picture </a></li>
                       
                    </ul>
                </div>
            </div>
            <div class="floatleft">
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc2:seefriends ID="uc3" runat="server"></uc2:seefriends>
                </div>
            </div>
        </div>
        <!-- content inner ends -->
    </div>
    <div id="popupShare" class="popup_block">
        <iframe id="url" src="ShareMail.aspx" style="border: none;" width="100%" height="280px"
            frameborder="0" scrolling="no"></iframe>
    </div>
    <%--<table style="width: 100%">
                 
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" ID="lblSettings" Text="Settings :"></asp:Label>
                        </td>
                      
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkEmailSettings" NavigateUrl ="~/UserEmailNotification.aspx" >Email settings</asp:HyperLink>
                        </td>
                    </tr>
                       <tr>
                        <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkCustomizeFeeds" NavigateUrl ="~/CustomizeFeedsjobsCountry.aspx">Customize feeds</asp:HyperLink>
                         </td>
                    </tr>
                    <tr>
                         <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkCustomizeJobs" NavigateUrl ="~/CustomizeFeedsjobsCountry.aspx">Customize jobs</asp:HyperLink>
                         </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" ID="lblAffiliate" Text="Affiliate :"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                         <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkSentInvitations" NavigateUrl="~/InvitationsOverview.aspx" >Sent Invitations</asp:HyperLink>
                         </td>
                    </tr>
                    <tr>
                        <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkWithdrawFunds" NavigateUrl="~/WithDrawFunds.aspx" >With draw funds</asp:HyperLink>
                         </td>
                    </tr>
                    <tr>
                        <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkAccountBalance" NavigateUrl ="~/MyAccount.aspx"  >account balance</asp:HyperLink>
                         </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" ID="lblProfile" Text="Profile:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                          <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkViewProfile" NavigateUrl ="~/ViewUserProfile.aspx" >View profile</asp:HyperLink>
                         </td>
                    </tr>
                    <tr>
                       <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkEditProfile" NavigateUrl ="~/EditProfilePage.aspx" >edit profile</asp:HyperLink>
                         </td>
                    </tr>
                    <tr>
                       <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkExport"  NavigateUrl ="~/ViewUserProfile.aspx" >export</asp:HyperLink>
                         </td>
                    </tr>
                    <tr>
                       <td align="left">
                                 &nbsp;&nbsp;<asp:LinkButton runat ="server" ID ="hyplnkshare`1" OnClientClick="javascript:window.open('ShareMail.aspx','MyWindow','width=900,height=400,top=100,left=120,resizable=yes'); return false;">share</asp:LinkButton>
                         </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" ID="lblJobs" Text="Jobs:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                       <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkMyJobpostings" NavigateUrl ="#"  >My Job postings</asp:HyperLink>
                         </td>
                    </tr>
                    <tr>
                       <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkMyJobapplications" NavigateUrl ="#"  >My Job applications</asp:HyperLink>
                         </td>
                    </tr>
                    <tr>
                       <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkCandidatesapplied" NavigateUrl ="#"  >Candidates applied</asp:HyperLink>
                         </td>
                    </tr>
                    <tr>
                       <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkBuyJobcredit" NavigateUrl ="~/BuyCredit.aspx"  >Buy Job credit</asp:HyperLink>
                         </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label runat="server" ID="lblMessages" Text="Messages:"></asp:Label>
                        </td>
                    </tr>
                      <tr>
                       <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkReceived" NavigateUrl ="~/MessageInbox.aspx">Received </asp:HyperLink>
                         </td>
                    </tr>
                    <tr>
                       <td align="left">
                                 &nbsp;&nbsp;<asp:HyperLink runat ="server" ID ="hyplnkSent" NavigateUrl ="~/MessageInbox.aspx" >Sent </asp:HyperLink>
                         </td>
                    </tr>
                </table>--%>
    <div id="popup2" class="popup_block">
        <iframe id="url" src="ShareMail.aspx" style="border: none;" width="100%" height="280px"
            frameborder="0" scrolling="no"></iframe>
    </div>
  <%--  <script type="text/javascript">
        $.noConflict();
        $(document).ready(function () {

            //When you click on a link with class of poplight and the href starts with a # 
            $('a.poplight[href^=#]').click(function () {
                var popID = $(this).attr('rel'); //Get Popup Name
                var popURL = $(this).attr('href'); //Get Popup href to define size

                //Pull Query & Variables from href URL
                var query = popURL.split('?');
                var dim = query[1].split('&');
                var popWidth = dim[0].split('=')[1]; //Gets the first query string value

                //Fade in the Popup and add close button
                $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="images/close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>');

                //Define margin for center alignment (vertical + horizontal) - we add 80 to the height/width to accomodate for the padding + border width defined in the css
                var popMargTop = ($('#' + popID).height() + 80) / 2;
                var popMargLeft = ($('#' + popID).width() + 80) / 2;

                //Apply Margin to Popup
                $('#' + popID).css({
                    'margin-top': -popMargTop,
                    'margin-left': -popMargLeft
                });

                //Fade in Background
                $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
                $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); //Fade in the fade layer 

                return false;
            });


            //Close Popups and Fade Layer
            $('a.close, #fade').live('click', function () { //When clicking on the close or fade layer...
                $('#fade , .popup_block').fadeOut(function () {
                    $('#fade, a.close').remove();
                }); //fade them both out

                return false;
            });


        });

    </script>--%>
</asp:Content>
