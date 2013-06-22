<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInvoice.aspx.cs" Inherits="Huntable.UI.UserInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="profile-box-main">
        <div class="apply-job">
            <table class="invoice-earning">
                <tr>
                    <td width="70%" colspan="2" valign="top" style="border-bottom: 1px dotted #ccc;"
                        class="blue-color">
                        <asp:Label ID="lbltranscationid" runat="server"></asp:Label>
                    </td>
                    <td width="30%" valign="top" style="border-bottom: 1px dotted #ccc;">
                        <asp:Button ID="btnprint" runat="server" OnClick="BtnprintClick" Text="Print Invoice"
                            CssClass="button-ash floatright" /><br />
                        <asp:Label ID="lblHuntable" ForeColor="Green" runat="server"></asp:Label><br />
                        <asp:Label ID="lblstate" runat="server"></asp:Label>,
                        <asp:Label ID="lblcountry" runat="server"></asp:Label><br />
                        <a href="#">
                            <asp:Label ID="lblemail" runat="server"></asp:Label></a>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <strong>Billed to:</strong><br />
                        Huntable Limited<br />
                        Address<br />
                        London<br />
                        United Kingdom
                    </td>
                    <td valign="top" align="right">
                        <strong>Date:</strong>
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblwithdrawn" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" colspan="3">
                        <table class="invoice-earning-inner">
                            <tr>
                                <th valign="top">
                                    Item
                                </th>
                                <th valign="top">
                                    Description
                                </th>
                                <th valign="top">
                                    Quantity
                                </th>
                                <th valign="top">
                                    Rate
                                </th>
                                <th valign="top">
                                    Price
                                </th>
                            </tr>
                            <tr>
                                <td valign="top">
                                    1
                                </td>
                                <td valign="top">
                                    Affiliate Friends - Recommending Friends
                                </td>
                                <td valign="top">
                                    1
                                </td>
                                <td valign="top">
                                     $<asp:Label ID="lblTotAmount" runat="server"></asp:Label>
                                </td>
                                <td valign="top">
                                     $<asp:Label ID="lblTotalAmount" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top" colspan="2" align="right">
                        <strong>Total:</strong>
                    </td>
                    <td valign="top" class="blue-color">
                        $<asp:Label ID="lblAmount" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" colspan="2" align="right" style="border-bottom: 1px dotted #ccc;">
                        <strong>Admin Charges:</strong>
                    </td>
                    <td valign="top" style="border-bottom: 1px dotted #ccc;" class="blue-color">
                        <asp:Label ID="lblAdminCharges" runat="server" Text=" Dollar $5.00" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" colspan="2" align="right" class="blue-color">
                        <strong>Total:</strong>
                    </td>
                    <td valign="top" class="blue-color">
                    <asp:Label ID="lblAmountToWithdraw" runat="server" ></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbltrancation" runat="server"></asp:Label>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    </form>
</body>
</html>
