<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvitingFriends.ascx.cs"
    Inherits="Huntable.UI.UserControls.InvitingFriends" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
<div id="invitefriends" runat="server" class="box-right">
    <div class="head-ash">
        <h3>
            Invites Your Friends &amp; Earn Money</h3>
    </div>
    <div class="social-icon">
        <a href="../InviteFriends.aspx" title="Facebook">
            <img src="../images/facebook.jpg" width="34" height="34" alt="Facebook" title="Facebook" /></a>
        <a href="../InviteFriends.aspx" title="Linked in">
            <img src="../images/linkedin.jpg" width="34" height="34" alt="Linkedin" title="Linked in" /></a>
        <a href="../InviteFriends.aspx" title="Twitter">
            <img src="../images/twitter.jpg" width="34" height="34" alt="Twitter" title="Twitter" /></a>
        <a href="../InviteFriends.aspx" title="Gmail">
            <img src="../images/gmail.jpg" width="34" height="34" alt="Gmail" title="Gmail" /></a><a
                href="../InviteFriends.aspx" title="yahoo"><img src="../images/yahoo.png" width="34"
                    height="34" alt="yahoo" /></a> <a href="../InviteFriends.aspx" title="Msn">
                        <img src="../images/msn.jpg" width="34" height="34" alt="Msn" title="Msn" /></a>
        <b>This is how it works!</b>
    </div>
    <ul class="connection-list">
        <li><a href="InviteFriends.aspx">1st Connection</a>
            <p>
                You Get<strong> $4 </strong>per connection</p>
        </li>
        <li><a href="InviteFriends.aspx">2nd Connection</a>
            <p>
                Your friend gets <strong>$4 </strong>– You get <strong>$1</strong> per connection
            </p>
        </li>
        <li><a href="InviteFriends.aspx">3rd Connection</a>
            <p>
                Your friends friend gets<strong> $4,</strong> Your friend gets <strong>$1</strong>,
                you get<strong> $0.5</strong> per connection</p>
        </li>
    </ul>
    <a href="InviteFriends.aspx" class="learn-more">Learn More</a>
    <asp:DataList ID="dlFriends" runat="server" RepeatDirection="Horizontal">
        <ItemTemplate>
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgFriend" runat="server" ImageUrl='<%#Eval("url") %>' PostBackUrl='<%#UrlGenerator(Eval("userId")) %>'
                            Height="45" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCount" runat="server" Text='<%#Eval("count") %>'></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                       
                    </td>
                </tr>


            </table>
        </ItemTemplate>
    </asp:DataList>
</div>
