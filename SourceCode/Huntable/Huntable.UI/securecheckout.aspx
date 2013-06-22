<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="securecheckout.aspx.cs" Inherits="Huntable.UI.SecureCheckout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style15
        {
            height: 46px;
        }
        .style16
        {
            height: 6px;
        }
        
        .style17
        {
            height: 27px;
        }
        .style18
        {
            height: 23px;
        }
        
        .style19
        {
            height: 23px;
            width: 133px;
        }
        
        .style21
        {
            width: 74px;
        }
        
        .style22
        {
            width: 92px;
        }
        
        .style23
        {
            width: 269px;
        }
        .style24
        {
            height: 357px;
            width: 133px;
        }
        .style25
        {
            height: 357px;
        }
        
        .style26
        {
        }
        
        .style27
        {
            width: 31px;
        }
        .style28
        {
            width: 26px;
        }
        .style29
        {
            width: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner" class="checkout-border">
            <div class="content-inner-left checkout-content">
                <h2 class="login-heading-profile">
                    <span style="float: left;">
                        <img src="images/checkout-icon.jpg" width="29" height="29" alt="Checkout" title="Checkout"
                            style="vertical-align: middle;" />
                        Secure Checkout</span> <a href="Default.aspx" class="checkout-link">
                            <img src="images/left-arrow.png" width="4" height="7" alt="" style="vertical-align: middle;" />
                            &nbsp;Back to main site</a></h2>
                <br />
                <table id="tbResult" runat="server" visible="false" style="margin-top: 20px;" width="100%">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblResult" Text="" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnOk" class runat="server" Text="Ok" OnClick="BtnOkClick" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <br />
                <div id="div_pack" runat="server" visible="false" style="width: 250px;">
                    <strong>
                        <asp:Label Text="Job Package Selected : " Visible="False" runat="server" ID="lblpackage"></asp:Label></strong>
                    <asp:Label runat="server" ID="lblPackageval" Visible="False"></asp:Label>
                </div>
                <asp:Panel ID="pnlMain" runat="server">
                    <div class="upload-inner checkout-box">
                        <%--<asp:Label ID="lblcountry" runat="server" ForeColor="Black" Text="Country"></asp:Label>--%>
                        <label>
                            Country:</label>
                        <asp:DropDownList class="textbox listbox" ID="ddlcountrynames" runat="server">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <label>
                            Card Number:</label>
                        <asp:TextBox ID="txtcard" class="textbox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCard" runat="server" ControlToValidate="txtcard"
                            ForeColor="Red" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                        <%--    <div style="float: right; margin-right: 80px;">
                            <asp:RegularExpressionValidator ID="revCard" runat="server" ControlToValidate="txtcard"
                                ForeColor="Red" Display="Dynamic" Text="Enter valid card number" ValidationExpression="^((4\d{3})|(5[1-5]\d{2})|(6011)|(34\d{1})|(37\d{1}))-?\d{4}-?\d{4}-?\d{4}|3[4,7][\d\s-]{15}$"></asp:RegularExpressionValidator>
                        </div>--%>
                        <br />
                        <br />
                        <label>
                            Amount:</label>
                        <asp:TextBox ID="txtAmount" ReadOnly="True" class="textbox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount"
                            ForeColor="Red" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                        <div style="float: right; margin-right: 80px;">
                            <asp:RegularExpressionValidator ID="revAmount" runat="server" ControlToValidate="txtAmount"
                                ForeColor="Red" Text="Enter amount in correct format" Display="Dynamic" ValidationExpression="^(?:\d+(?:,\d{3})*(?:\.\d{2})?|\d+(?:\.\d{3})*(?:,\d{2})?)$"></asp:RegularExpressionValidator>
                        </div>
                        <br />
                        <h5 style="color: gray; margin-left: 85px;">
                            Vat@ 20% included</h5>
                        <br />
                        <label>
                            Payment Type:</label>
                        <asp:DropDownList ID="ddlBankCardTypes" class="textbox listbox" runat="server">
                            <asp:ListItem runat="server">MasterCard</asp:ListItem>
                            <asp:ListItem runat="server" Selected="True">Visa</asp:ListItem>
                            <asp:ListItem runat="server">American Express</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <br />
                        <label>
                            Expiry Date:</label>
                        <strong>MM</strong>
                        <asp:DropDownList class="textbox checkout-date" runat="server" ID="ddlMonth">
                        </asp:DropDownList>
                        <strong>YY</strong>
                        <asp:DropDownList class="textbox checkout-date" runat="server" ID="ddlYear">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <label>
                            CSC:<br />
                            <a href="#">What is this?</a></label>
                        <asp:TextBox ID="txtcsc" runat="server" class="textbox checkout-date"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCSC" runat="server" ControlToValidate="txtcsc"
                            ForeColor="Red" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <br />
                        <br />
                        <label>
                            First Name:</label>
                        <asp:TextBox ID="txtfirst" runat="server" class="textbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFirst" runat="server" ControlToValidate="txtfirst"
                            ForeColor="Red" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <label>
                            Middle Name:<br />
                            (Optional)</label>
                        <asp:TextBox ID="txtmiddle" class="textbox" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        <label>
                            Last Name:</label>
                        <asp:TextBox ID="txtlast" class="textbox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLast" runat="server" ControlToValidate="txtlast"
                            ForeColor="Red" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                        <div style="float: right; margin-right: 80px;">
                            <asp:RegularExpressionValidator ID="revLast" runat="server" ControlToValidate="txtlast"
                                ForeColor="Red" Text="Enter last name in correct format" Display="Dynamic" ValidationExpression="^[a-z,A-Z]{3,50}$"></asp:RegularExpressionValidator>
                        </div>
                        <br />
                        <br />
                        <label>
                            Address Line1:</label>
                        <asp:TextBox ID="txtadress" class="textbox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvAdd" runat="server" ControlToValidate="txtadress"
                            ForeColor="Red" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <label>
                            Address Line2:<br />
                            (Optional)</label>
                        <asp:TextBox ID="txtaddress2" class="textbox" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        <label>
                            Town / City:</label>
                        <asp:TextBox ID="txtcity" runat="server" class="textbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtcity"
                            ForeColor="Red" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <label>
                            State:</label>
                        <asp:TextBox runat="server" ID="txtState" class="textbox"></asp:TextBox>
                        <br />
                        <br />
                        <label>
                            Telephone:</label>
                        <asp:TextBox ID="txtpin" runat="server" class="textbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPin" runat="server" ControlToValidate="txtpin"
                            ForeColor="Red" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                        <div style="float: right; margin-right: 80px;">
                            <asp:RegularExpressionValidator ID="revtele" runat="server" ControlToValidate="txtpin"
                                ForeColor="Red" Text="Enter valid phone number" Display="Dynamic" ValidationExpression="^[0-9]{3,15}$"></asp:RegularExpressionValidator>
                        </div>
                        <br />
                        <br />
                        <label>
                            Pincode:</label>
                        <asp:TextBox ID="txttele" runat="server" class="textbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvTele" runat="server" ControlToValidate="txttele"
                            ForeColor="Red" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                        <label>
                            E-mail:</label>
                        <asp:TextBox ID="txtemail" runat="server" class="textbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtemail"
                            ForeColor="Red" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                        <div style="float: right; margin-right: 80px;">
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtemail"
                                ForeColor="Red" Text="Enter valid email" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </div>
                        <br />
                        <br />
                        <label>
                            &nbsp;</label>
                        <asp:Button ID="btnreview" runat="server" Text="Review and Continue" class="button-green button-green-upload "
                            OnClick="BtnClientReviewAndContinue" />
                        <br />
                        <br />
                        <br />
                        <label>
                            &nbsp;</label>
                        <img src="images/secured-payment.jpg" width="227" height="115" alt="Secure Payment"
                            title="Secure Payment" /></div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
