<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shareByEmail.aspx.cs" Inherits="Huntable.UI.shareByEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="Styles/NewStylesheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
   <asp:Panel runat="server" ID="panShare" Width="700px">
       <table class="tableStyles2">
           <tr>
               <td class="cellStyle2">
                   <table class="tableStyle">
                       <tr>
                           <td class="cellStyles16"></td>
                       </tr>
                       <tr>
                           <td class="cellStyles16">
                               <asp:ImageButton runat="server" ID="imgShareSocial" ImageUrl="image/social.png"/> &nbsp;
                               <asp:Label runat="server" ID="lblShareSocial" Text="Share to Social Web" ForeColor="white"></asp:Label>
                           </td>
                       </tr>
                       <tr>
                           <td>
                               <asp:ImageButton runat="server" ID="imgPost" Width="24px" ImageUrl="image/postCover.png"/> &nbsp;
                               <asp:Label runat="server" ID="lblShareEmail" Text="Share by Email"></asp:Label>
                           </td>
                       </tr>
                       <tr>
                           <td class="cellStyles16">
                               <asp:ImageButton runat="server" ID="imgCopy" ImageUrl="image/copy.png" Width="24px"/> &nbsp;
                               <asp:Label runat="server" ID="lblCopy" Text="Copy URL" ForeColor="white"></asp:Label>
                           </td>
                       </tr>
                       <tr>
                           <td class="cellStyles16">
                               <asp:ImageButton runat="server" ID="imgHome" ImageUrl="image/home.png" Width="24px"/> &nbsp;
                               <asp:Label runat="server" ID="lblHome" Text="Your Network" ForeColor="white"></asp:Label>
                           </td>
                       </tr>
                       <tr>
                           <td class="cellStyles17">
                               
                           </td>
                       </tr>
                   </table>
               </td>
               <td>
                   <table class="tableStyles3">
                       <tr>
                           <td>
                               
                           </td>
                       </tr>
                   </table>
               </td>
           </tr>
       </table>
   </asp:Panel>
    </div>
    </form>
</body>
</html>
