<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AdminInvoices.aspx.cs" Inherits="Huntable.UI.AdminInvoices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="contacts-select">
                    <div class="job-criteria-price">
                        <h2 class="margin-bot">
                            Invoices to be cleared</h2>
                        <ul class="job-criteria-list">
                            <li class="bg-ash">
                                <asp:Repeater ID="Invoices" runat="server">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td align="center" style="width:150px;">
                                                    <asp:Label ID="LblAmount" Text='<%# Eval("Amount")%>' runat="server"></asp:Label>
                                                </td>
                                                <td align="center" style="width:150px;">
                                                    <asp:Label ID="Label1" Text='<%# Eval("WithdrawnDateTime")%>' runat="server"></asp:Label>
                                                </td>
                                                <td align="center" style="width:120px;">
                                                    <asp:Button ID="btnPay" OnClick="payuser" Text="Pay" CommandArgument='<%#Eval("Id")%>' CssClass="button-green"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <SeparatorTemplate>
                                        <br />
                                    </SeparatorTemplate>
                                </asp:Repeater>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
