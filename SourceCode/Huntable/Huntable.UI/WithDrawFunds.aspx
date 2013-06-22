<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableSessionState="true"
    AutoEventWireup="true" CodeBehind="WithDrawFunds.aspx.cs" Inherits="Huntable.UI.WithDrawFunds" %>

<%@ Register Src="UserControls/JobControl.ascx" TagName="JobControl" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">
        function callAlert(msg) {
            alert(msg);
        }
        function Test() {
            alert("Minimum withdrawl limit is 100 dollars");
        }

    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="contacts-head">
                <h3 style="float: left; font-size: 15px; font-weight: normal;">
                    Your Transaction Details
                </h3>
                <a href="MyAccount.aspx" class="learn-more" style="background: none;">&lsaquo;&lsaquo;
                    Back to my accounts</a>
            </div>
            <div class="earning-table">
                <asp:GridView ID="gvInvSummary" HorizontalAlign="Center" AutoGenerateColumns="False"
                    runat="server" Width="100%" Height="125px" BackColor="#CCCCCC" BorderColor="#999999"
                    BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                    <Columns>
                        <asp:BoundField DataField="Level" />
                        <asp:BoundField HeaderText="INVITED" DataField="Invited" />
                        <asp:BoundField HeaderText="JOINED" DataField="Joined" />
                        <asp:BoundField HeaderText="EARNINGS" DataField="Earnings" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="BLack" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="Black" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
                <div style="margin-left: 830px;">
                    <asp:Label Font-Size="Large" runat="server" ID="lblTotalEarnings"></asp:Label>
                </div>
            </div>
            <br />
            <asp:Button ID="btnwthdraw" class="button-orange button-orange-withdraw" runat="server"
                OnClick="BtnInvoiceClick" Text="Withdraw now" /><br />
            <%--<a href="#" class="button-orange button-orange-withdraw">Withdraw now</a>--%>
            <div class="earning-table">
                <asp:GridView ID="gvTranscation" OnRowCommand="GvTranscationRowCommand" runat="server"
                    BackColor="White" AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None"
                    OnRowDataBound="GvRowDataBound" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                    Width="980px">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Serial No">
                            <ItemTemplate>
                                <%# ((GridViewRow)Container).RowIndex + 1%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" Visible="false" />
                        <asp:BoundField DataField="TotalInvited" NullDisplayText="0" HeaderText="Toatal Invited"
                            SortExpression="TotalInvited" />
                        <asp:BoundField DataField="DrawnLevel1Count" NullDisplayText="0" HeaderText="1st Connection"
                            SortExpression="DrawnLevel1Count" />
                        <asp:BoundField DataField="DrawnLevel2Count" NullDisplayText="0" HeaderText="2nd Connection"
                            SortExpression="DrawnLevel2Count" />
                        <asp:BoundField DataField="DrawnLevel3Count" NullDisplayText="0" HeaderText="3rd Connection"
                            SortExpression="DrawnLevel3Count" />
                        <asp:BoundField DataField="Amount" HeaderText="Total Affilate Earnings" SortExpression="Amount" />
                        <asp:BoundField DataField="WithdrawnDateTime" HeaderText="Requested Date" SortExpression="WithdrawnDateTime"
                            DataFormatString="{0:d}" HtmlEncode="false" />
                        <asp:BoundField DataField="TransactionCompletedDateTime" NullDisplayText="Under Process"
                            HeaderText="Paid on" SortExpression="TranscationCompletedDateTime" />
                        <%--    <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkname" runat="server" OnClientClick="lb1_Click"  Text="Invoice" CssClass="button-green" CommandArgument='<%#Eval("Id") %>'
                                    CommandName="linkButton">
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:HyperLinkField runat="server" ItemStyle-CssClass="button-green" Target="_search"
                            DataNavigateUrlFields="Id" DataNavigateUrlFormatString="UserInvoice.aspx?Id={0}"
                            Text="Invoice"></asp:HyperLinkField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
            </div>
            <div class="withdraw-desc">
                <h2>
                    To see how you can receive by paypal,
                    <asp:HyperLink ID="hyppaypal" Text="Click here" Target="_blank" NavigateUrl="https://www.paypal.com/webapps/helpcenter/article/?articleID=94024&topicID=11500011&m=TKB#receiving_money"
                        runat="server"></asp:HyperLink>
                </h2>
                <ul class="menu collapsible menu-withdraw">
                    <li>
                        <h2 class="faq">
                            Faq</h2>
                    </li>
                    <li class="expand"><a href="#" class="menu-ash" style="font-family: Georgia">How do
                        I receive funds into my Paypal account?<b>&nbsp;</b></a>
                        <ul class="acitem">
                            <li>
                                <p style="font-family: Georgia;color: black">
                                    You can withdraw all your affiliate earnings by clicking on the Withdraw now button
                                    at the top. Once you have done this, we will be notified and will will check the
                                    credentials to make sure all the details are correct.<br />
                                    <br />
                                    We will then make the payment through PayPal to your email address registered in
                                    Huntable. If you already registered with PayPal, you can immediately see that payment
                                    has been made and organise a withdrawal.<br />
                                    <br />
                                    If you do not have a PayPal account, you will need to create one and then you will
                                    be able receive funds into your account.<br />
                                    <br />
                                    Once you have connected your PayPal account to your bank account, you can then transfer
                                    funds from your PayPal to your bank account.<br />
                                    <br />
                                    You must have a minimum balance of $100 to be collected before you can make a withdrawal.
                                    There is also an admin charge of $10 per transaction when you withdraw.<br />
                                    <br />
                                </p>
                            </li>
                        </ul>
                    </li>
                    <li><a href="#" class="menu-ash" style="font-family: Georgia">How can I transfer funds from my PayPal account to
                        my bank account?<b>&nbsp;</b></a>
                        <ul class="acitem">
                            <li>
                                <p style="font-family: Georgia;color: black">
                                    Once you have received the funds in your PayPal account, you can simply transfer
                                    them to your connected bank account.<br />
                                    <br />
                                    If your bank account has not yet been connected, you can do so easily within the
                                    PayPal website, simply by following the instructions given.<br />
                                    <br />
                                    DISCLAIMER: Huntable does not take any responsibility for any delays or problems
                                    directly associated with the PayPal website. You will need to contact PayPal directly
                                    if you have any issues.<br />
                                    <br />
                                </p>
                            </li>
                        </ul>
                    </li>
                </ul>
                <div class="paypal-icon">
                    <img src="images/paypal-icon.jpg" width="216" height="73" alt="paypal" />
                </div>
                <!-- Range Slider Script Begins -->
                <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
                <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
                    type="text/javascript"></script>
                <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
                <!-- Range Slider Script Ends -->
                <!-- Help Tab Script Begins -->
                <script src="js/menu.js" type="text/javascript"></script>
                <!-- Help Tab Script Ends -->
            </div>
        </div>
    </div>
</asp:Content>
