<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportContacts.ascx.cs"
    Inherits="Huntable.UI.UserControls.ImportContacts" %>
<div class="box-right">
    <div class="head-ash">
        <h3>
            Import Contacts</h3>
    </div>
    <p class="import-contact-desc">
        Its easy to search your social, email contacts and grow your network.
    </p>
    <div class="social-icon">
        <a href="#" title="Facebook">
            <asp:ImageButton ID="ibtnFacebook" runat="server" Width="34" Height="34" alt="Facebook"
                ImageUrl="../images/facebook.jpg" OnClick="IbtnFacebookClick" CausesValidation="false" /></a>
        <a href="#" title="Linked in">
            <asp:ImageButton ID="ibtnLinkedIn" runat="server" ImageUrl="../images/linkedin.jpg"
                Width="34" Height="34" alt="Linkedin" OnClick="IbtnLinkedInClick" CausesValidation="false" />
        </a><a href="#" title="Twitter">
            <asp:ImageButton ID="ibtnTwitter" runat="server" ImageUrl="../images/twitter.jpg"
                Width="34" Height="34" alt="Gmail" OnClick="IbtnTwitterClick" CausesValidation="false" />
        </a><a href="#" title="Gmail">
            <asp:ImageButton ID="ibtnGoogle" runat="server" ImageUrl="../images/gmail.jpg" Width="34"
                Height="34" alt="Gmail" OnClick="IbtnGoogleClick" CausesValidation="false" /></a>
        <a href="#" title="yahoo">
            <asp:ImageButton ID="ibtnYahoo" runat="server" Width="34" Height="34" alt="yahoo"
                ImageUrl="../images/yahoo.png" OnClick="IbtnYahooClick" CausesValidation="false" /></a>
        <a href="#" title="Msn">
            <asp:ImageButton ID="ibtnLive" runat="server" Width="34" Height="34" alt="Msn" ImageUrl="../images/msn.jpg"
                OnClick="IbtnLiveClick" CausesValidation="false" /></a>
    </div>
    <p class="margin-top-visible">
        &nbsp;</p>
    <div>
        <img src="../images/excel.png" width="35" height="35" alt="Excel format" />&nbsp;&nbsp;Import
        your contacts from a CSV file<br />
        <p style="float: right;">
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"  BackColor="Yellow"/>
        </p>
    </div>
</div>
