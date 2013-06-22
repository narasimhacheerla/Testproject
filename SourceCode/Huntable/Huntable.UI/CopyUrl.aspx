<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CopyUrl.aspx.cs" Inherits="Huntable.UI.CopyUrl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="Styles/NewStylesheet.css" rel="Stylesheet" type="text/css" />
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="tableStyles2">
            <tr>
                <td class="cellStyle2">
                               <table class="tableStyle">
                                   <tr>
                                       <td style="background-color: skyblue;" class="style22"></td>
                                   </tr>
                                   <tr>
                                       <td style="background-color: skyblue;">
                                           <asp:ImageButton runat="server" ID="imgSocialWeb" ImageUrl="image/social.png"/>
                                           <asp:Label runat="server" ID="lblSocial" Text="Share to Social Web" ForeColor="white"></asp:Label>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="background-color: skyblue;">
                                           <asp:ImageButton runat="server" ID="imgPost" ImageUrl="image/postCover.png"/>
                                           <asp:Label runat="server" ID="lblEmail" Text="Share by email" ForeColor="white"></asp:Label>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td>
                                           <asp:ImageButton runat="server" ID="imgCopy" ImageUrl="image/copy.png"/>
                                           <asp:Label runat="server" ID="lblCopy" Text="Copy URL" ></asp:Label>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="background-color: skyblue;">
                                           <asp:ImageButton runat="server" ID="imgHome" ImageUrl="image/home.png"/>
                                           <asp:Label runat="server" ID="lblhome" Text="your Network" ForeColor="white" ></asp:Label>
                                       </td>
                                   </tr>
                                   <tr>
                                       <td style="background-color: skyblue;" class="columnStyles10"></td>
                                   </tr>
                               </table>

                </td>
                <td valign="top">
                    <table class="tableStyle">
                        <tr>
                            <td class="cellStyle4"></td>
                            <td class="columnStyles11">
                                <asp:Label runat="server" ID="lblHeading" Text="Copy URl" Font-Size="X-Large"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td class="style9"></td>
                            <td class="colmnStyles12"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label runat="server" ID="lblShareURL" Text="You can share this content using the URL:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtUrl" Text="http://WWW.skillpages.com/s/n1Y19X0SEeGF" Font-Size="Large" BorderColor="SkyBlue" Width="350px"></asp:TextBox>
                                <asp:Button runat="server" ID="btnCopy" Text="Copy" ForeColor="white" BackColor="Green"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
