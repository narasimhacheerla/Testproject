<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ViewPurchases.aspx.cs" Inherits="Huntable.UI.ViewPurchases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/style-innerpage.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <h2 class="login-heading login-heading-trans">
                <strong>Transaction history</strong> <a href="MyAccount.aspx">&lsaquo;&lsaquo; &nbsp;Back
                    to my accounts</a>
            </h2>
            <div class="transaction-history-list">
                <asp:GridView ID="gvSummary" HorizontalAlign="Center" AutoGenerateColumns="False"
                    runat="server" OnRowDataBound="GvRowDataBound" Width="100%" GridLines="None">
                    <%--This attribute belongs to Gridview Height="125px"--%>
                    <%--<RowStyle Height = "45px"></RowStyle>--%>
                    <AlternatingRowStyle BackColor="White" Height="35px" ForeColor="Black" />
                    <Columns>
                        <%-- <asp:TemplateField HeaderText="Serial No">
                                    <itemtemplate>
                                <%# ((GridViewRow)Container).RowIndex + 1%>
                                 </itemtemplate>
                                </asp:TemplateField>--%>
                        <asp:BoundField HeaderText="Purchase Date" DataFormatString="{0:d}" DataField="PurchaseDateTime" />
                        <asp:BoundField HeaderText="Purpose" NullDisplayText="JObs Credit" DataField="Purpose" />
                        <asp:BoundField HeaderText="No Of Jobs" DataField="NoOfCredits" />
                        <asp:BoundField HeaderText="Amount Paid" DataField="AmountPaid" />
                        <asp:HyperLinkField runat="server"  Target="_search"
                            DataNavigateUrlFields="Id" DataNavigateUrlFormatString="UserPayment.aspx?Id={0}"
                            Text="Invoice"></asp:HyperLinkField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle ForeColor="Black" BackColor="Transparent" CssClass="trans-head" Font-Size="12px"
                        Font-Names="GalantBold" Height="20px" HorizontalAlign="Left" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle Height="12PX" ForeColor="#0396ac" BackColor="#e9fcff" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="Black" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
                <asp:Label Font-Size="Large" runat="server" ForeColor="Red" ID="lblTotalEarnings"></asp:Label>
            </div>
            <div class="amount-icon" style="width: 750px;">
                <%-- <a href="#">
                    <img src="images/trans-list.png" width="22" height="20" alt="Invoice" />Invoice</a>
                <a href="#">
                    <img src="images/credit-icon.png" width="22" height="20" alt="credit" />Payment</a>--%>
                <p style="font-size: 14px">
                    All amounts shown are in US dollars</p>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
