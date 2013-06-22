<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HtmlAgilityForJobs.aspx.cs" Inherits="Huntable.UI.HtmlAgilityForJobs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div style="margin-top: 47px;">
    <asp:TextBox runat="server" ID="txturl"></asp:TextBox>
    <asp:Button runat="server" ID="btnUpload" Text="UPLOAD" OnClick="btnUploadClick"/><br/><br/>
    
    <asp:Label runat="server" Text="Designation" Visible="False"></asp:Label>
    <asp:TextBox runat="server" ID="txtdesignation" TextMode="MultiLine" Visible="False"></asp:TextBox><br/>
    
    <asp:Label ID="Label3" runat="server" Text="txtjobdescription" Visible="False"></asp:Label>
    <asp:TextBox runat="server" ID="txtjobdescription" Visible="False"></asp:TextBox>
    
     <asp:Label ID="Label4" runat="server" Text="txtwebsite" Visible="False"></asp:Label>
    <asp:TextBox runat="server" ID="txtwebsite" TextMode="MultiLine" Visible="False"></asp:TextBox>

    <asp:Label ID="Label1" runat="server" Text="Role" Visible="False"></asp:Label>
    <asp:TextBox runat="server" ID="txtrole" TextMode="MultiLine" Visible="False"></asp:TextBox ><br/>

    <asp:Label ID="Label2" runat="server" Text="txtDeriredProfile"></asp:Label>
    <asp:TextBox runat="server" ID="txtDeriredProfile" TextMode="MultiLine"></asp:TextBox>

    <asp:Panel ID="pnlExpertSkill" runat="server">
                                                                    </asp:Panel>

   
    </div>--%>
   
    <asp:DataList runat="server" ID="dlfollowingusers">
    <ItemTemplate>
   <span><asp:Label runat="server" Text='<%#Eval("UserId") %>' ></asp:Label>
    </span>
    </ItemTemplate>
   </asp:DataList>
   <br /><br />
    <asp:DataList runat="server" ID="dlmutualfollowers">
    <ItemTemplate>
   <span><asp:Label ID="Label1" runat="server" Text='<%#Eval("UserId") %>' ></asp:Label>
    </span>
    </ItemTemplate>
   </asp:DataList>
   <br /><br /><br />
   <asp:DataList runat="server" ID="dlmutualFollowersWithTwo">
    <ItemTemplate>
   <span><asp:Label ID="Label1" runat="server" Text='<%#Eval("UserId") %>' ></asp:Label>
    </span>
    </ItemTemplate>
   </asp:DataList>
   <br /><br /><br />
    <asp:DataList runat="server" ID="dlmutualFollowersFollowers">
    <ItemTemplate>
   <span><asp:Label ID="Label1" runat="server" Text='<%#Eval("UserId") %>' ></asp:Label>
    </span>
    </ItemTemplate>
   </asp:DataList>
   
</asp:Content>
