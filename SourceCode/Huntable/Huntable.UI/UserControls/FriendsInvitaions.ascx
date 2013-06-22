<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FriendsInvitaions.ascx.cs"
    Inherits="Huntable.UI.UserControls.FriendsInvitaions" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
<div class="box-right">
    <div class="head-ash">
        <h3>
            See Your Friends Invitations</h3>
    </div>
   <ul class="how-it-work-list">

        <li>
            <asp:Repeater ID="rptrFriends" runat="server">
                <ItemTemplate>
                    <div style="float: left; border: 2px solid #ccc; padding: 5px; margin: 5px;">
                        <img align="absmiddle" class="profile-pic" width="46" height="45" alt="thumb-small" src='<%# "ProfileImages/"+Eval("UserProfilePictureDisplayUrl") %>' /><br />
                        <asp:LinkButton ID="lbtnDetails" ToolTip='<%#Eval("Id")%>' OnClick="lbtnDetails_OnClick"
                            Text='<%#Eval("LevelOneInvitedCount")%>' runat="server"></asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </li>
    </ul>
    
    <a href="#" class="learn-more">Show More</a>
</div>
