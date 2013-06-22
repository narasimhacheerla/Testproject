<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompaniesYouMayBeIntrested.ascx.cs"
    Inherits="Huntable.UI.UserControls.CompaniesYouMayBeIntrested" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
<div class="box-right" >
    <div class="head-ash">
        <h3>
            Companies you may be interested</h3>
    </div>
    <div style="height:340px;overflow-y:scroll;overflow-x:hidden">
    <asp:DataList ID="dlcomp" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
        CellSpacing="3" CellPadding="5">
        <ItemTemplate>
            <a href='<%#UrlGenerator(Eval("Id")) %>' id="compwebsite" runat="server">
                <asp:Image runat="server" ID="smr" Width="87px" Height="47px" ImageUrl='<%#Picture(Eval("CompanyLogoId"))%>' /><h2
                    style="font-size: 13px; text-align: center;">
                    (<%#Jobs(Eval("Id")) %>)Jobs</h2>
            </a>&nbsp&nbsp&nbsp
        </ItemTemplate>
    </asp:DataList>
    </div>
    <a href="seemorerecruiter.aspx" class="learn-more">See More Companies</a>
</div>
