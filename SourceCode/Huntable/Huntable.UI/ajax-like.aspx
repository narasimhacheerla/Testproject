<%@ Page Title="" Language="C#" MasterPageFile="~/Site_popup.Master" AutoEventWireup="true" CodeBehind="ajax-like.aspx.cs" Inherits="Huntable.UI.ajax_like" %>
<%@ Register src="UserControls/UserFeedLikedUser.ascx" tagname="UserFeedLikedUser" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <uc1:UserFeedLikedUser ID="UserFeedLikedUser1" runat="server" />
    
</asp:Content>
