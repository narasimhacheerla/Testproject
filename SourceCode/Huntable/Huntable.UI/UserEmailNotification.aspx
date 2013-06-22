<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableSessionState="true"
    AutoEventWireup="true" CodeBehind="UserEmailNotification.aspx.cs" Inherits="Huntable.UI.UserEmailNotification" %>

<%@ Register Src="UserControls/JobControl.ascx" TagName="JobControl" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <div id="content-section">
        <div id="content-inner">
            <div class="general-setting-tab">
                <a href="ChangeEmail.aspx">General Settings</a> 
                <h3 class="setting-active" style=" float: left;width: 200px; font-size: 20px; font-family: 'GalantBold'; text-shadow: 0px 1px 1px white; color: #747474; margin-right: 10px;
                                                     padding: 7px 10px; text-align: center; border: 1px solid #CCC; border-radius: 4px 4px; background: -webkit-gradient(linear,left top,left bottom,color-stop(0%,#FBFBFB), color-stop(100%,#CECECE));">E-mail Notifications</h3>
              
            </div>
            <div class="general-setting">
                <h2 class="general-setting-head">
                    Notify When Someone</h2>
                <ul class="general-setting-list">
                    <li><strong>General</strong></li>
                    <li>
                        <p>
                            When Friends join my network
                        </p>
                        <label>
                            <asp:CheckBox runat="server" ID="chkBxWhenFriendsFriedJoin" Checked="True" /></label>
                    </li>
                    <li>
                        <p>
                            When 3rd connection joins</p>
                        <label>
                            <asp:CheckBox runat="server" ID="chkBxWhenThirdConnJoins" Checked="True" /></label></li>
                </ul>
                <ul class="general-setting-list">
                    <li><strong>Job Post</strong></li>
                    <li>
                        <p>
                            When a user applies for a job
                        </p>
                        <label>
                            <asp:CheckBox runat="server" ID="chkBxWhnUserAppliesForJob" Checked="True" /></label></li>
                </ul>
                <ul class="general-setting-list">
                    <li><strong>Remainders</strong></li>
                    <li>
                        <p>
                            Endorsement request</p>
                        <label>
                            <asp:CheckBox runat="server" ID="chkBxEndorsementrequest" Checked="True" /></label></li>
                </ul>
                <ul class="general-setting-list">
                    <li><strong>Others</strong></li>
                   <%-- <li>
                        <p>
                            Forget your password</p>
                        <label>
                            <asp:CheckBox runat="server" ID="chkBxForgotPassword"  Checked="True"/></label>
                    </li>--%>
                    <li>
                        <p>
                            <asp:Label runat="server" ID="lblAdminEmailTemp" Text="Admin Email template"></asp:Label>
                        </p>
                        <label>
                            <asp:CheckBox runat="server" ID="chkBxAdminEmailTemp" Checked="True" />
                        </label>
                    </li>
                    <li>
                        <p>
                            <asp:Label runat="server" ID="lblWhenUserSndMsg" Text="When user send a message"></asp:Label>
                        </p>
                        <label>
                            <asp:CheckBox runat="server" ID="chkBxWhenUserSndMsg" Checked="True"/>
                        </label>
                    </li>
                    <li>
                        <p>
                            <asp:Label runat="server" ID="lblWhenFriendsJOin" Text="When Friends join"></asp:Label>
                        </p>
                        <label>
                            <asp:CheckBox runat="server" ID="chkBxWhenFriendsJOin" Checked="True"/>
                        </label>
                    </li>
                    <li>
                        <p>
                            <asp:Label runat="server" ID="lblForgotPwdEmailLnk" Text="Forgot password Email link"></asp:Label>
                        </p>
                        <label>
                              <asp:Image ID="Image1" runat="server" Height="20" Width="20" ImageUrl="images/tick.png"/>
                        </label>
                    </li>
                    <li>
                        <p>
                            <asp:Label runat="server" ID="lblInviteFrndsWhnJoined" Text="Invite friends when joined"></asp:Label>
                        </p>
                        <label>
                            <asp:CheckBox runat="server" ID="chkBxInviteFrndsWhnJoined" Checked="True" />
                        </label>
                    </li>
                    <li>
                        <p>
                            <asp:Label runat="server" ID="lblResetPwd" Text="Reset password"></asp:Label>
                        </p>
                        <label>
                            <asp:Image runat="server"  Height="20" Width="20" ImageUrl="images/tick.png"/>
                           <%-- <asp:CheckBox runat="server" ID="chkBxResetPwd" Checked="True" />--%>
                        </label>
                    </li>
                    <li>
                        <p>
                            <asp:Label runat="server" ID="lblVerifyYOurEmail" Text="Verify your Email"></asp:Label>
                        </p>
                        <label>
                            <asp:CheckBox runat="server" ID="chkBxVerifyYOurEmail" Checked="True"/>
                        </label>
                    </li>
                    <li>
                        <p>
                            <asp:Label runat="server" ID="lblWelcome" Text="Welcome Email"></asp:Label>
                        </p>
                        <label>
                            <asp:CheckBox runat="server" ID="chkBxWelcomeEmail" Checked="True"/>
                        </label>
                    </li>
                    <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
                    <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="BtnSaveClick" CssClass="button-green floatleft"
                        Style="clear: both; margin: 20px 0px 0px 600px;" />
                </ul>
                <%--<table style="width: 60%">
                    <tr>
                        <td class="cellStyles10" colspan="2" align="center">
                            <asp:Panel runat="server" BackColor="gray" Width="100%" ID="Panel1">
                                <h3>
                                    <asp:Label runat="server" ID="Label1" Text="Email Notifications"></asp:Label></h3>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="style9" align="left" style="width: 300px">
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblEndorsementrequest" Text="Endorsement request"></asp:Label>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblWhnUserAppliesForJob" Text="When a user applies for a job"></asp:Label>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblForgotPassword" Text="Forget your password"></asp:Label>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                        </td>
                    </tr>
                </table>--%>
                 <div ><asp:Image ID="bimage" runat="server" CssClass="advert"  ImageUrl="images/basic-user-advert.gif"/>
                <asp:Image ID="pimage" runat="server" CssClass="advert" ImageUrl="images/premium-user-advert.gif" /></div>
            </div>
        </div>
    </div>
     <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
