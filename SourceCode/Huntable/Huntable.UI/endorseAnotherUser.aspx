<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="endorseAnotherUser.aspx.cs" Inherits="Huntable.UI.endorseAnotherUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="Styles/NewStylesheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="tableStyles1">
        <tr>
            <td>
    <asp:Panel runat="server" Width="550px" BackColor="lightskyblue">
        <table class="tableStyle">
            <tr class="rowStyles1">
                <td class="cellStyle4"></td>
                <td>
                    <asp:ImageButton runat="server" ID="imgLady" ImageUrl="image/imageUc.png"/>
                </td>
                <td>
                    <p>
                        <font color="Blue"> Angela Moore</font>Business Development Manager at Newick Park Hotel & Country Estate
                    </p>
                    <asp:Label runat="server" ID="lblDate" Text="Date:March 14,2012"></asp:Label>
                </td>
            </tr>
          </table>
    </asp:Panel>
    </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblEndorse" Text="Endorse Darrun,"></asp:Label><br/><br/>
                
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Panel runat="server" ID="panMiddle" Width="500px" Height="150px" CssClass="tableStyles1">
                    
                    <table style="height: 100%;" class="tableStyle">
                        <tr>
                            <td valign="bottom">
                               <asp:Label runat="server" ID="lblAngela" Text="Angela Moore" ForeColor="Blue" Font-Size="Large"></asp:Label>,Business Development Manager,Netwick Park Hotel
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <p class="style15">
                    Write something about Darrun
                    </p>
                <p class="style15">
                    Note:Endorsing your friends will hwlp your friends get more
                    opportunity from any of his future job search
                    it also helps you build your network of whos has endorsed your friend.
                </p>
            </td>
        </tr>
        <tr>
            <td align="right" valign="bottom">
                <asp:Button runat="server" ID="btnEndorseNow" Text="Endorse Now"/>
            </td>
        </tr>
   </table>
    </div>
    </form>
</body>
</html>
