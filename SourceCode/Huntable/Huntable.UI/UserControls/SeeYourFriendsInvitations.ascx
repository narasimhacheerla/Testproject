<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeeYourFriendsInvitations.ascx.cs"
    Inherits="Huntable.UI.UserControls.SeeYourFriendsInvitations" %>
<link rel="Stylesheet" type="text/css" href="../css/style-innerpage.css" />
<div class="floatleft">
    <div id="seeInvitations" runat="server" class="box-right">
        <div class="head-ash">
            <h3>
                See Your Friends Invitations</h3>
        </div>
        <ul style="display: inline; list-style-type: none;" class="how-it-work-list">
            <li>
                <asp:Repeater ID="rsplist" runat="server" >
                    <ItemTemplate>
                        <li><a runat="server" href='<%#UrlGenerator(Eval("Id")) %>' class="accounts-link">
                            <asp:Image ID="imgBtn1"  runat="server" class="profile-pic" ImageUrl='<%#Eval("UserProfilePictureDisplayUrl")%>'
                                Width="46" Height="45" alt="thumb-small" /></a><asp:Label runat="server" Text='<%#(Eval("LevelOneInvitedCount")) %>'></asp:Label></li>
                    </ItemTemplate>
                </asp:Repeater>
            </li>
        </ul>
        <a  runat ="server" href="~/TopReferrers.aspx" class="learn-more">Show More</a>
    </div>
</div>
