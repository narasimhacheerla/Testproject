<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ChangeEmailSettings.aspx.cs" Inherits="Huntable.UI.ChangeEmailSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Range Slider Script Ends -->
    <style type="text/css">
        .style1
        {
            height: 70px;
        }
    </style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
           <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>

    <div id="content-section">
        <div id="content-inner">
            <div class="general-setting-tab">
                <a href="#" class="setting-active">General Settings</a> <a href="#">E-mail Notifications</a>
            </div>
            <div class="general-setting">
                <ul class="menu collapsible ">
                    <li class="expand"><a href="#" class="menu-ash menu-ash-inner"><strong>Email</strong><span>&nbsp;</span></a>
                        <ul class="acitem">
                            <li>
                                <div class="email-section">
                                    <p>
                                        <strong>This email is used to login</strong></p>
                                    <table class="login-table">
                                        <tr>
                                            <td width="40%" align="right">
                                                <label>
                                                    <strong>Current E-mail</strong></label>
                                            </td>
                                            <td width="60%">
                                                <div style="float: left;">
                                                    <asp:Label ID="lblemail" runat="server" CssClass="emailtext"></asp:Label></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%" align="right" class="style1" style="margin-top: 0px;">
                                                <label>
                                                    <strong>Change E-mail</strong></label>
                                            </td>
                                            <td width="60%" class="style1">
                                                <%--<input type="text" class="textbox textbox-rec" />--%>
                                                <asp:TextBox ID="txtNewEmail" runat="server" class="textbox textbox-rec"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtNewEmail"
                                                    ValidationGroup="mail" ForeColor="Red" Text="*" Display="Dynamic" ErrorMessage="Please enter email"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtNewEmail"
                                                    ValidationGroup="mail" ForeColor="Red" Display="Dynamic" ErrorMessage="*Please enter valid email"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                &nbsp;
                                            </td>
                                            <td width="60%">
                                                <br />
                                                <asp:Button ID="btnEmail" runat="server" BorderStyle="None" class="button-green"
                                                    ValidationGroup="mail" OnClick="BtnEmailClick" Text="Change" />
                                                <asp:ValidationSummary ID="vsMail" runat="server" ValidationGroup="mail" ShowMessageBox="true"
                                                    ShowSummary="false" ForeColor="Red" DisplayMode="BulletList" HeaderText="Following errors occurred" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </li>
                        </ul>
                    </li>
                    <li><a href="#" class="menu-ash menu-ash-inner"><strong>Password</strong><span>&nbsp;</span></a>
                        <asp:UpdatePanel ID="upPassword" runat="server">
                            <ContentTemplate>
                                <ul class="acitem">
                                    <li>
                                        <div class="email-section">
                                            <p>
                                                <strong>New Passwords are required to be a minimum of 6 characters in length.</strong></p>
                                            <table class="login-table">
                                                <tr>
                                                    <td width="40%" align="right">
                                                        <label>
                                                            <strong>Old Password</strong></label>
                                                    </td>
                                                    <td width="60%">
                                                        <asp:TextBox ID="txtOld" runat="server" class="textbox textbox-rec" TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvOld" runat="server" ControlToValidate="txtOld"
                                                            ValidationGroup="password" ForeColor="Red" Text="*" Display="Dynamic" ErrorMessage="Please enter old password"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%" align="right">
                                                        <label>
                                                            <strong>New Password</strong></label>
                                                    </td>
                                                    <td width="60%">
                                                        <asp:TextBox ID="txtNew" runat="server" class="textbox textbox-rec" TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvNew" runat="server" ControlToValidate="txtNew"
                                                            ValidationGroup="password" ForeColor="Red" Text="*" Display="Dynamic" ErrorMessage="Please enter new password"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revPwd" runat="server" ControlToValidate="txtNew"
                                                            ForeColor="Red" ValidationGroup="password" Display="Dynamic" ErrorMessage="Password must be 8 characters and have both letters and numbers."
                                                            ValidationExpression="(?=.{8,})[a-zA-Z]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%" align="right">
                                                        <label>
                                                            <strong>Confirm New Password</strong></label>
                                                    </td>
                                                    <td width="60%">
                                                        <asp:TextBox ID="txtNewCnfrm" runat="server" class="textbox textbox-rec" TextMode="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvconfirm" runat="server" ControlToValidate="txtNewCnfrm"
                                                            ValidationGroup="password" ForeColor="Red" Text="*" Display="Dynamic" ErrorMessage="Please re-enter new password"></asp:RequiredFieldValidator><br />
                                                        <asp:CompareValidator ID="cmpPwd" runat="server" ControlToValidate="txtNewCnfrm"
                                                            ValidationGroup="password" ControlToCompare="txtNew" ForeColor="Red" ErrorMessage="Re entered password doesn't match"></asp:CompareValidator>
                                                        <asp:ValidationSummary ID="vsPwd" runat="server" ShowMessageBox="true" ShowSummary="false"
                                                            ForeColor="Red" ValidationGroup="password" DisplayMode="BulletList" HeaderText="Following errors occurred" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        &nbsp;
                                                    </td>
                                                    <td width="60%">
                                                        <%--<input type="button" class="button-green" value="Change" /> --%>
                                                        <asp:Button ID="btnPwdChange" runat="server" BorderStyle="None" class="button-green"
                                                            ValidationGroup="password" Text="Change" OnClick="BtnPwdChangeClick" />
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                           
                                            <ul runat="server" style="margin-top:-30px;">
                                            <li id="li_pwd" runat="server" style="margin-left:-471px;">
                                            <asp:Label runat="server" ID="lblpwd" width="267px"></asp:Label></li>
                                            <li id="li_msg" runat="server" style="margin-left:-471px;">
                                            <asp:Label runat="server" ID="lblmesg" width="267px" ForeColor="Red"></asp:Label></li>
                                            </ul>
                                            
                                        </div>
                                    </li>
                                </ul>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </li>
                </ul>
            </div>
        </div>
        <!-- content inner ends -->
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
