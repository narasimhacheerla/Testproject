<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemDisplayTemplate.ascx.cs"
    Inherits="test1.ItemDisplayTemplate" %>
<div>
    <asp:CheckBox ID="chkItem" runat="server" />
    <asp:HiddenField ID="hfId" runat="server"/>
    <asp:HiddenField ID="hfOriginalSelected" runat="server"/>
    <asp:Label ID="lblItemDescription" runat="server" Font-Size="12" />
</div>
