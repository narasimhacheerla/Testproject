<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ForgotPassword.aspx.cs"
    Inherits="Huntable.UI.ForgotPassword" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- main menu ends -->
    <div id="content-section">
        <div id="content-inner">
            <div class="login">
                <h2>
                    Forgot Your Password</h2>
                Enter Your email below and we will send you an email with a link to reset your password.
                <table class="login-table">
                    <tr>
                        <td width="35%" align="right">
                            <label>
                                Email:</label>
                        </td>
                        <td width="65%">
                            <%-- <input type="text" class="textbox textbox-rec" />%--%>
                            <asp:TextBox ID="txtemail" runat="server" class="textbox textbox-rec"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtemail" ForeColor="Red"
                             Text="*" Display="Dynamic" ></asp:RequiredFieldValidator>
                            <br />
                             <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtemail" ForeColor="Red"
                              Text="Enter valid email please" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ></asp:RegularExpressionValidator> 
                          
                        </td>
                    </tr>
                    <tr>
                        <td width="35%">
                            &nbsp;
                        </td>
                        <td width="65%">
                            <%-- <input type="button" class="button-green" value="Reset Password" /> --%>
                            <asp:Button ID="Button1" runat="server" class="button-green" OnClick="Button1Click"
                                Text="Send" />
                        </td>
                    </tr>
                    <tr>
                        <td width="35%" colspan="2">
                            <asp:Label ID="lblemail" runat="server" Visible="False"></asp:Label>
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
