<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Myinvitations.aspx.cs" Inherits="Huntable.UI.Myinvitations" %>

<%@ Register Src="~/UserControls/FriendsToInvite.ascx" TagPrefix="uc3" TagName="Invite" %>
<%@ Register Src="~/UserControls/SeeYourFriendsInvitations.ascx" TagPrefix="uc4"
    TagName="YourFrInvitations" %>
<%@ Register Src="UserControls/YourAccountAtGlance.ascx" TagName="YourAccountAtGlance"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/cvStatistics.ascx" TagName="cvStatistics" TagPrefix="uc2" %>
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
            <div class="content-inner-left">
                <ul class="tab-list">
                    <li><a href="MyAccount.aspx" class="st_tab">My Accounts </a></li>
                    <li><a href="Myinvitations.aspx" class="st_tab st_first_tab st_tab_active">Invitations</a></li>
                    <li><a href="#" class="st_tab">My CV/Resume</a></li>
                </ul>
                <ul class="invitation-list" style="height: 400px;">
                    <li class="bg-ash-invite"><span class="invite-width-one"><strong>My accounts/Review
                    </strong></span><span class="invite-width-two">
                        <select class="textbox listbox" style="width: 120px;">
                            <option>Status</option>
                            <option>Status1</option>
                            <option>Status2</option>
                        </select>
                    </span></li>
                    <li><span class="invite-width-zero">1.</span> <span class="invite-width-one"><a href="#"
                        class="accounts-link">Arun Manicakam</a></span> <span class="invite-width-two"><strong>
                            Joined</strong></span> <span class="invite-width-three"><a href="#">&nbsp;</a></span>
                        <span class="invite-width-four">
                            <img width="15" height="15" alt="close" src="images/icon-close.png" />
                            <a href="#">Delete</a></span> </li>
                    <li><span class="invite-width-zero">2.</span> <span class="invite-width-one"><a href="#"
                        class="accounts-link">Arun Manicakam</a></span> <span class="invite-width-two"><strong>
                            Joined</strong></span> <span class="invite-width-three"><a href="#">&nbsp;</a></span>
                        <span class="invite-width-four">
                            <img width="15" height="15" alt="close" src="images/icon-close.png" />
                            <a href="#">Delete</a></span> </li>
                    <li><span class="invite-width-zero">3.</span> <span class="invite-width-one"><a href="#"
                        class="accounts-link">Arun Manicakam</a></span> <span class="invite-width-two"><strong>
                            Joined</strong></span> <span class="invite-width-three"><a href="#">&nbsp;</a></span>
                        <span class="invite-width-four">
                            <img width="15" height="15" alt="close" src="images/icon-close.png" />
                            <a href="#">Delete</a></span> </li>
                    <li><span class="invite-width-zero">4.</span> <span class="invite-width-one"><a href="#"
                        class="accounts-link">Arun Manicakam</a></span> <span class="invite-width-two"><strong>
                            Joined</strong></span> <span class="invite-width-three"><a href="#">&nbsp;</a></span>
                        <span class="invite-width-four">
                            <img width="15" height="15" alt="close" src="images/icon-close.png" />
                            <a href="#">Delete</a></span> </li>
                    <li><span class="invite-width-zero">5.</span> <span class="invite-width-one"><a href="#"
                        class="accounts-link">Arun Manicakam</a></span> <span class="invite-width-two"><strong>
                            Joined</strong></span> <span class="invite-width-three"><a href="#">&nbsp;</a></span>
                        <span class="invite-width-four">
                            <img width="15" height="15" alt="close" src="images/icon-close.png" />
                            <a href="#">Delete</a></span> </li>
                    <li><span class="invite-width-zero">6.</span> <span class="invite-width-one"><a href="#"
                        class="accounts-link">Arun Manicakam</a></span> <span class="invite-width-two"><strong>
                            Not Yet Joined</strong></span> <span class="invite-width-three"><a href="#">Resend Invitation</a></span>
                        <span class="invite-width-four">
                            <img width="15" height="15" alt="close" src="images/icon-close.png" />
                            <a href="#">Delete</a></span> </li>
                    <li><span class="invite-width-zero">7.</span> <span class="invite-width-one"><a href="#"
                        class="accounts-link">Arun Manicakam</a></span> <span class="invite-width-two"><strong>
                            Not Yet Joined</strong></span> <span class="invite-width-three"><a href="#">Resend Invitation</a></span>
                        <span class="invite-width-four">
                            <img width="15" height="15" alt="close" src="images/icon-close.png" />
                            <a href="#">Delete</a></span> </li>
                    <li><span class="invite-width-zero">8.</span> <span class="invite-width-one"><a href="#"
                        class="accounts-link">Arun Manicakam</a></span> <span class="invite-width-two"><strong>
                            Not Yet Joined</strong></span> <span class="invite-width-three"><a href="#">Resend Invitation</a></span>
                        <span class="invite-width-four">
                            <img width="15" height="15" alt="close" src="images/icon-close.png" />
                            <a href="#">Delete</a></span> </li>
                    <li><span class="invite-width-zero">9.</span> <span class="invite-width-one"><a href="#"
                        class="accounts-link">Arun Manicakam</a></span> <span class="invite-width-two"><strong>
                            Not Yet Joined</strong></span> <span class="invite-width-three"><a href="#">Resend Invitation</a></span>
                        <span class="invite-width-four">
                            <img width="15" height="15" alt="close" src="images/icon-close.png" />
                            <a href="#">Delete</a></span> </li>
                    <li><span class="invite-width-zero">10.</span> <span class="invite-width-one"><a
                        href="#" class="accounts-link">Arun Manicakam</a></span> <span class="invite-width-two">
                            <strong>Not Yet Joined</strong></span> <span class="invite-width-three"><a href="#">
                                Resend Invitation</a></span> <span class="invite-width-four">
                                    <img width="15" height="15" alt="close" src="images/icon-close.png" />
                                    <a href="#">Delete</a></span> </li>
                    <li><span class="invite-width-zero">11.</span> <span class="invite-width-one"><a
                        href="#" class="accounts-link">Arun Manicakam</a></span> <span class="invite-width-two">
                            <strong>Not Yet Joined</strong></span> <span class="invite-width-three"><a href="#">
                                Resend Invitation</a></span> <span class="invite-width-four">
                                    <img width="15" height="15" alt="close" src="images/icon-close.png" />
                                    <a href="#">Delete</a></span> </li>
                    <li><span class="invite-width-zero">12.</span> <span class="invite-width-one"><a
                        href="#" class="accounts-link">Arun Manicakam</a></span> <span class="invite-width-two">
                            <strong>Not Yet Joined</strong></span> <span class="invite-width-three"><a href="#">
                                Resend Invitation</a></span> <span class="invite-width-four">
                                    <img width="15" height="15" alt="close" src="images/icon-close.png" />
                                    <a href="#">Delete</a></span> </li>
                    <li><span class="invite-width-zero">13.</span> <span class="invite-width-one"><a
                        href="#" class="accounts-link">Arun Manicakam</a></span> <span class="invite-width-two">
                            <strong>Not Yet Joined</strong></span> <span class="invite-width-three"><a href="#">
                                Resend Invitation</a></span> <span class="invite-width-four">
                                    <img width="15" height="15" alt="close" src="images/icon-close.png" />
                                    <a href="#">Delete</a></span> </li>
                </ul>
                <asp:Label ID="Label8" runat="server" Text="Have you added new contacts to your Email/Social contacts recently"
                    ForeColor="#3399FF"></asp:Label>
                <asp:Button ID="Button7" runat="server" BackColor="#FF9900" BorderStyle="None" Text="Sync NOw" />
                <div class="floatleft">
                    <p class="margin-top-visible">
                        &nbsp;</p>
                    <div class="box-right">
                        <uc4:YourFrInvitations ID="FriendsInvitations" runat="server" />
                    </div>
                </div>
            </div>
            <div class="content-inner-right">
                <div class="blue-box-company">
                    How you added more new contacts to your email/social accounts recently?
                    <div class="button-green-company" align="center">
                        <a href="#" class="button-green button-green-company1">Sync Now</a><br />
                        <br />
                        To send invitation to your new contacts
                    </div>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc3:Invite ID="inviteFriends" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc1:YourAccountAtGlance ID="acctsGlance" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc2:cvStatistics ID="cvStats" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
