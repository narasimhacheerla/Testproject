<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewersOfThisProfile.ascx.cs"
    Inherits="Huntable.UI.UserControls.ViewersOfThisProfile" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
<div class="box-right">
    <div class="head-ash">
        <h3>
            Viewers of this profile also viewed...</h3>
    </div>
    <asp:DataList ID="rspview" runat="server">
        <ItemTemplate>
            <div class="friends-invite">
                <ul>
                    <li class="medium">
                 <a runat="server" href='<%#UrlGenerator(Eval("ID")) %>'>
                        <asp:Image class="profile-pic" ImageUrl='<%#Picture(Eval("ID"))%>'
                            runat="server" ID="img1" Width="46" Height="45" alt="friends-img"  /></a></li>
                    <li class="large">
                    <a runat="server" href='<%#UrlGenerator(Eval("ID")) %>'>
                        <asp:Label ID="lblname1" Text='<%#Eval("Name") %>' runat="server"></asp:Label>
                    </a>,
                    <asp:Label ID="lblposition1" Text = '<%#Eval("JobTitle") %>' runat = "server"></asp:Label>
                    at &nbsp<asp:Label ID ="lblcompany1" Text= '<%#Eval("MasterCompany") %>' runat="server"></asp:Label>
                   <%-- <asp:Label ID="lblposition1" Text='<%#Eval("JobTitle")%>' runat="server"></asp:Label>--%><%--at<asp:Label
                        ID="lblcompany1" Text='<%# Eval("MasterCompany")%>' runat="server"></asp:Label>--%>
                    </li>
                </ul>
            </div>
        </ItemTemplate>
    </asp:DataList>
    <asp:Label runat="server" ID="lblMessage"></asp:Label>
</div>
