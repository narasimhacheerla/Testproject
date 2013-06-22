<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RssJobFeeds.aspx.cs" Inherits="Huntable.UI.RssJobFeeds" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <Asp:FileUpload runat="server" ID="UploadContacts"/>
 <%--  <asp:TextBox runat="server" ID="txtrssfeed"></asp:TextBox>
   <Asp:button ID="feed" runat="server" Text="Feedupdate" onclick="feed_Click"  />--%>
  <asp:Button runat="server" Text="upload" OnClick="UploadInvites"/>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</asp:Content>

