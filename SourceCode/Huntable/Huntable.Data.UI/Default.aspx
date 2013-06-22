﻿<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Default.aspx.cs"
    Inherits="Huntable.Data.UI.Default" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2 class="DDSubHeader">
        My tables</h2>
    <br />
    <br />
    <asp:GridView ID="Menu1" runat="server" AutoGenerateColumns="false" CssClass="DDGridView"
        RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6">
        <Columns>
            <asp:TemplateField HeaderText="Table Name" SortExpression="TableName">
                <ItemTemplate>
                    <asp:DynamicHyperLink ID="HyperLink1" runat="server"><%# Eval("DisplayName") %></asp:DynamicHyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
