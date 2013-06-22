<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Company-registration.aspx.cs" Inherits="Huntable.UI.Company_registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Huntable - The Professional Network</title>
        <meta name="description" content="Huntable - The Professional Network" />
        <meta name="keywords" content="The Professional Network" />
        <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
    </head>
    <body>
        <div id="content-section">
            <div id="content-inner">
                <div class="login login-reg">
                    <div class="ribbon">
                        Start Your company page
                    </div>
                    <table class="company-reg">
                        <tr>
                            <th class="head-reg" valign="top" colspan="2">
                                Tell us more about your company<br />
                                <span class="blue-color">*mandatory</span>
                            </th>
                        </tr>
                        <tr style="line-height:1px;">
                            <td valign="top" width="32%">
                                Your Company name*
                            </td>
                            <td valign="top" width="68%">
                                <asp:TextBox runat="server" CssClass="textbox textbox-reg" ID="txtCompanyName"></asp:TextBox><asp:RequiredFieldValidator
                                    runat="server" ForeColor="Red" Text="Please enter Company Name" ControlToValidate="txtCompanyName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="line-height:74px;">
                            <td valign="top">
                                Your company website
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtCompanywebsite" CssClass="textbox textbox-reg" runat="server"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Password
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="textbox textbox-reg"
                                    runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                    Text="Please enter Password" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Confirm Password
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtConfrmPassword" TextMode="Password" CssClass="textbox textbox-reg"
                                    runat="server"></asp:TextBox>
                                <asp:CompareValidator runat="server" Text="Passwords not matching" ForeColor="Red"
                                    ControlToCompare="txtPassword" ControlToValidate="txtConfrmPassword" EnableClientScript="True"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                Country*
                            </td>
                            <td valign="top">
                                <asp:DropDownList  runat="server" ID="ddlcountry" CssClass="textbox textbox-reg" />
                            </td>
                        </tr>
                        <tr>
                            <th class="head-reg" valign="top" colspan="2">
                                Please enter your e-mail address so that we can authentify your company
                            </th>
                        </tr>
                        <tr>
                            <td valign="top">
                                Your Company e-mail address*
                            </td>
                            <td valign="top">
                                <asp:TextBox runat="server" CssClass="textbox textbox-company" ID="txtComapnyEmail"></asp:TextBox><a
                                    href="tooltip.htm?width=375" name="Important" class="jTip" id="one">?</a>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                    Text="Please enter Company Email" ControlToValidate="txtComapnyEmail"></asp:RequiredFieldValidator>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                              ValidationExpression="(.*)^(?!(.*)@gmail.com\b)(?!(.*)@yahoo.com\b)(?!(.*)@hotmail.com\b)(?!(.*)@hotmail.co.uk\b)(?!(.*)@outlook.com\b)(.*)" ForeColor="Red" ControlToValidate="txtComapnyEmail" Text="Valid email, but not for starting company.  No free service emails!"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="top">
                                <asp:CheckBox runat="server" ID="chbAgree" />&nbsp;&nbsp;I represent the company
                                and agree with the <a href="#" class="accounts-link">Terms of use</a>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="top" align="center">
                                <asp:Button runat="server" class="button-orange" Text="Confirm and start my company page"
                                    ID="Companystart" OnClick="CompanystartClick" OnClientClick="return isGoodEmail()" />
                         
                                <br />
                                <!--    <a href="#" class="accounts-link">&lsaquo;&lsaquo; Back to previous page</a>-->
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" width="100%">
                                <asp:Label runat="server" ID="lblMessage"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!-- content inner ends -->
        </div>
        <!-- content section ends -->
        <script src="js/jtip.js" type="text/javascript"></script>
        <script type="text/javascript">
            function isGoodEmail() {
                var email = document.getElementById("<%=txtComapnyEmail.ClientID%>").value;
                    if (window.isValidEmail(email)) {
                        if (/(aol|gmail|yahoo|hotmail)\.com$/.test(email)) {
                            alert(' valid email, but not for this site.  No free service emails!');
                            return false;
                        }
                        return true;
                    }
                    return false;
                
            }
            $(document).ready(function () {
                $("#Companystart").click(function () {
                    var EmailText = $("#txtComapnyEmail").val();
                    if ($.trim(EmailText).length == 0) {
                        alert("Please enter email address");
                        return false;
                    }
                    if (validateEmail(EmailText)) {
                        alert('Valid Email Address');
                        return true;
                    }
                    else {
                        alert('Invalid Email Address');
                        return false;
                    }
                });
            });
            function validateEmail(sEmail) {
                var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
                if (filter.test(sEmail)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        </script>
    </body>
    </html>
</asp:Content>
