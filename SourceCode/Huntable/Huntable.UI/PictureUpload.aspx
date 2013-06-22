<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PictureUpload.aspx.cs" Inherits="Huntable.UI.PictureUpload" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Src="UserControls/ProfileCompletionSteps.ascx" TagName="ProfileCompletionSteps"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="profile-box">
                    <div class="accounts-profile accounts-profile1 accounts-profile-picture">
                        <div class="accounts-profile-left">
                            <a href="profileuploadpage.aspx">
                                <asp:Image runat="server" ID="ImgPicture" class="profile-pic" Width="76" Height="81"
                                    alt="Profile-pic" /></a>
                        </div>
                        <div class="accounts-profile-right accounts-profile-right-picture ">
                            <div class="accounts-top">
                                <table>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblPercentCompleted" runat="server" Height="16px" />
                                        </td>
                                        <td align="left">
                                            Complete
                                        </td>
                                        <td align="left">
                                            <eo:ProgressBar ID="ProgressBar2" runat="server" Width="90px" BackgroundImage="00060301"
                                                BackgroundImageLeft="00060302" BackgroundImageRight="00060303" ControlSkinID="None"
                                                IndicatorImage="00060304" ShowPercentage="True" Value="30">
                                            </eo:ProgressBar>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="accounts-middle">
                                <strong>Primary E-mail:</strong><asp:Label ID="lblemail" runat="server"></asp:Label><br />
                                <a href="ChangeEmail.aspx" class="accounts-link">Change</a>&nbsp;/&nbsp;<a href="ChangeEmail.aspx"
                                    class="accounts-link">Add</a>
                            </div>
                            <strong>Password:</strong> <a href="ChangeEmail.aspx" class="accounts-link">Change</a>
                        </div>
                        <br />
                        <div style="float: left; margin-top: 10px; margin-right: 120px;">
                            <a href="ViewUserProfile.aspx" class="profile-name">
                                <asp:Label ID="lblname" Width="120px" runat="server"></asp:Label></a>
                        </div>
                    </div>
                    <div class="ashabcd-box" id="divYes" runat="server">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblposition1" runat="server"></asp:Label> &nbsp;<asp:Label runat="server" ID="lblat" Text="at"></asp:Label>&nbsp; <asp:Label ID="lblcompany1"
                            runat="server"></asp:Label><br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:label ID="lblpositiontext"  Text=" Is this your current Position?" runat="server">
                           </asp:label>
                        <div style="margin-top: 10px;" runat="server" id="y_n">
                            <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="button-green button-green-stats"
                              OnClick="BtnYesClick"  />
                            <a href="EditProfilePage.aspx" class="button-ash button-ash-stats">
                                <img src="images/icon-close-stats.png" width="17" height="16" alt="Yes" />No</a>
                        </div>
                    </div>
                   
                </div>
                <div class="picture-upload">
                    <div class="picture-upload-left">
                        <h2>
                            Personal</h2>
                        <a href="#">
                            <asp:Image runat="server" ID="ImgPreview" class="profile-pic profile-picture" Width="76"
                                Height="81" alt="Profile-pic" /></a>
                        <asp:FileUpload ID="uploadPhoto" runat="server" /><br />
                        <br />
                        <asp:Button ID="btnChangePic" class="button-green" runat="server" BorderStyle="None"
                            Text="Change Photo" Width="100px" OnClick="BtnChangePicClick" /></a>
                    </div>
                    <div class="picture-upload-right">
                        <h2>
                            Company</h2>
                        <a href="#">
                            <asp:Image runat="server" ID="ImgCompanyLogo" class="profile-pic profile-picture"
                                Width="76" Height="81" alt="Company-Pic" /></a>
                        <asp:FileUpload ID="UploadCompanyLogo" runat="server" /><br />
                        <br />
                        <asp:Button ID="btnChangeCompanyPic" class="button-green" BorderStyle="None" runat="server"
                            Text="Change Company Logo" OnClick="BtnChangeCompanyPicClick" /></a>
                    </div>
                </div>
                <div class="pic-agree">
                    <h2>
                    </h2>
                    <a id="a_profile" runat="server" class="button-orange floatright">View My Profile</a>
                </div>
            </div>
            <!-- content inner left ends -->
            <div class="content-inner-right">
                <uc1:ProfileCompletionSteps ID="ProfileCompletionSteps" runat="server" />
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <div class="head-ash">
                        <h3>
                            Import Your Profile</h3>
                    </div>
                    <p class="import-contact-desc">
                        Instantly update your profile by<br />
                        Importing from Linkedin or Facebook
                    </p>
                    <a href="InviteFriends.aspx" title="Facebook" style="margin-left: 10px;">
                        <img src="images/import-facebook.png" width="133" height="41" alt="Facebook" /></a>&nbsp;&nbsp;
                    <a href="InviteFriends.aspx" title="Linked in">
                        <img src="images/import-linkedin.png" width="133" height="41" alt="Linked in" /></a><br />
                    <br />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <div class="privacy">
                        <img src="images/icon-privacy1.png" width="48" height="48" alt="Privacy" />
                        <strong>Your privacy is important to us. Your contact details is never shown to others.</strong>
                    </div>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <div class="head-ash">
                        <h3>
                            Import Contacts</h3>
                    </div>
                    <p class="import-contact-desc">
                        Its easy to search your social, email contacts and grow your network.
                    </p>
                    <div class="social-icon">
                        <a href="InviteFriends.aspx" title="Facebook">
                            <img src="images/facebook.jpg" width="34" height="34" alt="Facebook" title="Facebook" /></a>
                        <a href="InviteFriends.aspx" title="Linked in">
                            <img src="images/linkedin.jpg" width="34" height="34" alt="Linkedin" title="Linked in" /></a>
                        <a href="InviteFriends.aspx" title="Twitter">
                            <img src="images/twitter.jpg" width="34" height="34" alt="Twitter" title="Twitter" /></a>
                        <a href="InviteFriends.aspx" title="Gmail">
                            <img src="images/gmail.jpg" width="34" height="34" alt="Gmail" title="Gmail" /></a><a
                                href="InviteFriends.aspx" title="yahoo"><img src="images/yahoo.png" width="34" height="34"
                                    alt="yahoo" /></a> <a href="InviteFriends.aspx" title="Msn">
                                        <img src="images/msn.jpg" width="34" height="34" alt="Msn" title="Msn" /></a>
                    </div>
                    <a href="invitefriends.aspx" class="invite-friend-btn" style="margin: 0px 0px 5px 15px; float: left;">
                        Upload File</a> <a href="invitefriends.aspx" class="learn-more">Import</a>
                </div>
            </div>
            <!-- content inner right ends -->
        </div>
        <!-- content inner ends -->
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
