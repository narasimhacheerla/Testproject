<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestFeed.aspx.cs" Inherits="Huntable.UI.TestFeed" %>

<%@ Register src="UserControls/UserFeedList.ascx" tagname="UserFeedList" tagprefix="uc1" %>
<%@ Register src="UserControls/UserFeedAlerts.ascx" tagname="UserFeedAlerts" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <uc2:UserFeedAlerts ID="UserFeedAlerts1" runat="server" />
                     <%--<uc1:UserFeedList ID="UserFeedList1" runat="server" PageType="Networking"/>--%>
</asp:Content>
