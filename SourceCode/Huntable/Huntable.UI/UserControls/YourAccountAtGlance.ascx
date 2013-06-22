<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YourAccountAtGlance.ascx.cs"
    Inherits="Huntable.UI.UserControls.YourAccountAtGlance" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
<div id="acctsDiv" runat="server" class="box-right">
    <div class="head-ash">
        <h3>
            Your Invitations Glance</h3>
    </div>
    <p class="account-rating">
        Total Invitations&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;<strong>
            <asp:Label ID="lblTotalInvitaions" runat="server" ForeColor="Red" Text=""></asp:Label></strong></p>
    <p class="account-rating">
        Friends Joined&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;<strong>
            <asp:Label ID="lblFriendsJoined" runat="server" ForeColor="Red" Text=""></asp:Label></strong></p>
    <%--  <tr>
        <td>
            Total 1st connections:
        </td>
        <td>
            <asp:Label ID="lblLevelOne" runat="server" ForeColor="#0066FF" Text=""></asp:Label>
        </td>
    </tr>--%>
    <p class="account-rating">
        Total 2nd Connections&nbsp;&nbsp;:&nbsp;&nbsp;<strong>
            <asp:Label ID="lblLevelTwo" runat="server" ForeColor="Red" Text=""></asp:Label></strong></p>
    <p class="account-rating">
        Total 3rd Connections&nbsp;&nbsp;:&nbsp;&nbsp;<strong>
            <asp:Label ID="lblLevelThree" runat="server" Text="" ForeColor="Red"></asp:Label></strong></p>
    <p class="account-rating color-hunt">
        Total Affiliate Earning&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;<strong>
            <asp:Label ID="lblTotalEarnings" runat="server" ForeColor="#0066FF" Text=""></asp:Label></strong></p>
</div>
