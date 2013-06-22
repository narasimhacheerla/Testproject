<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FriendsToInvite.ascx.cs"
    Inherits="Huntable.UI.UserControls.FriendsToInvite" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
<div id="friendsInvite" class="box-right" runat="server">
    <div class="head-ash" >
        <h3>
            Friends to Invite</h3>
    </div>
    <asp:Repeater ID="rpFriendsToInvite" runat="server" OnItemCommand="RpFriendsToInviteItemCommand" >
        <ItemTemplate>
            <div class="friends-invite">
                <ul>
                    <li class="large">
                        <%# Eval("Name") %><br />
                        <asp:LinkButton ID="btnInvite" Text="Invite" class="invite" runat="server" CommandName="Invite" />
                    </li>
                    <li class="small">
                        <asp:ImageButton ID="imgClose" runat="server" ImageUrl="../images/icon-close.png" CommandName="Close" />
                    </li>
                    <asp:HiddenField ID="hfFriendsToInviteId" Value='<%# Eval("Id") %>' runat="server" />
                </ul>
            </div>
        </ItemTemplate>
        <SeparatorTemplate>
            <hr />
        </SeparatorTemplate>
    </asp:Repeater>
    <asp:HiddenField ID="hfCurrentRecordCount" runat="server" />
    <asp:LinkButton ID="btnSeeMore" Text="See More" class="learn-more" OnClick="BtnSeeMoreClick"
        runat="server" />
</div>
<p class="margin-top-visible">
    &nbsp;</p>
