<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="Huntable.UI.ResetPassword" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="login">
                <h2>
                    Reset Your Password</h2>
                <strong>To verify your new password, please enter it once in each field below.</strong><br />
                Passwords are case-sensitive and must be at least 6 characters long. A good password
                should contain a mix of capital and lower-case letters, numbers and symbols.
                <table class="login-table" style="margin-left: 0px;">
                    <tr>
                        <td width="50%" align="right">
                            <label>
                                Enter new password:</label>
                        </td>
                        <td width="50%">
                            <%-- <input type="text" class="textbox textbox-rec" />--%>
                            <asp:TextBox ID="txtPwd" runat="server" class="textbox textbox-rec" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" align="right">
                            <label>
                                Re-enter new password:</label>
                        </td>
                        <td width="50%">
                            <asp:TextBox ID="txtCnfrmpwd" runat="server" class="textbox textbox-rec" TextMode="Password"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPwd"
                                ControlToValidate="txtCnfrmpwd" EnableViewState="False" Visible="False">Passwords didnot amtch</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            &nbsp;
                        </td>
                        <td width="60%">
                            <%--<input type="button" class="button-green" value="Reset Password" /> --%>
                            <asp:Button ID="btnResetPassword" runat="server" class="button-green" Text="Reset Password"
                                OnClick="BtnResetPasswordClick" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblpasswd" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
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
</asp:Content>
