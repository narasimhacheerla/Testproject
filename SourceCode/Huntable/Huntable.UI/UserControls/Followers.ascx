<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Followers.ascx.cs" Inherits="Huntable.UI.UserControls.Followers" %>
<link href="https://huntable.co.uk/css/style-innerpage.css" type="text/css" rel="stylesheet" />
<div class="r-heading">
    <h3 style="font-size: 14px;">
        <asp:Label ID="lbl" runat="server"></asp:Label>
        Followers</h3>
</div>
<ul class="r-followers" style="width: 178px;">
    <asp:DataList ID="dl" runat="server" RepeatDirection="Horizontal" RepeatColumns="2"
        OnItemDataBound="itembound">
        <ItemTemplate>
            <li style="padding: 5px 4px;"><a runat="server" id="a1" href="#">
                <asp:Image runat="server" ImageUrl='<%#Picture(Container.DataItem) %>' Width="76"
                    Height="81" />
            </a></li>
        </ItemTemplate>
    </asp:DataList>
    <a runat="server" id="seemore" class="learn-more">See More </a>
</ul>
