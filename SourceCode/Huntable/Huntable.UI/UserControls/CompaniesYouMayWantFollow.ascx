<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompaniesYouMayWantFollow.ascx.cs"
    Inherits="Huntable.UI.UserControls.CompaniesYouMayWantFollow" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
<div class="content-inner-right">
    <div class="box-right">
        <div class="head-ash">
            <h3>
                Companies you may want to follow</h3>
        </div>
        <div style="height: 333px; overflow-y: scroll;" runat="server">
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
    </div>
</div>
