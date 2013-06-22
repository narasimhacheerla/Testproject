<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GiridharDemo.aspx.cs" Inherits="Huntable.UI.GiridharDemo" %>
<%@ Register Src="~/UserControls/GoogleAdds.ascx" TagName="Googleadds" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Huntable - The Professional Network</title>
<meta name="description" content="Huntable - The Professional Network" />
<meta name="keywords" content="The Professional Network" />
<link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
</head>

<body>


<div id="content-section">
  <div id="content-inner">
    <div class="content-inner-left content-inner-left-message">
      <h4 class="login-heading"> <asp:Label ID="lblUserName" runat="server"></asp:Label> > Profile > Activity > <a href="#" class="accounts-link">Followers</a></h4>
      <div class="followers-list">
      
      <div class="followers-link">
      	<a href="#" class="follow-ash">Followers (<asp:Label ID="lblFollowersCount" runat="server"></asp:Label>) </a>
        <a href="#" class="follow-blue">Following (236)  </a>
      </div> 
              
    <asp:DataList ID="rspFollowers" runat ="server" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout ="Table" >
    <ItemTemplate>  
    <table class="followers-list-table" width="200" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td width="60" valign="top"><a href="#">
        <img class="profile-pic profile-pic2" alt="Feaured-logo" src="images/profile-thumb-large.jpg" /></a></td>
        <td width="140" align="left" valign="top"><p style="line-height:18px;"> <a class="accounts-link" href="#"><%#(Eval("FirstName")) %></a><br />
            <%#(Eval("CurrentCompany"))%><br />
            <strong><%#(Eval("City"))%></strong><br />
            <a class="orange-link" href="#">Follow</a> </p></td>
    </tr>
    </table>
    </ItemTemplate>
    </asp:DataList> 


      </div>
      
      
    </div>
    <div class="content-inner-right content-inner-right-message">
     <uc:Googleadds ID="Googleadds" runat="server" />    </div>
  </div>
  <!-- content inner ends --> 
</div>
<!-- content section ends -->
<!-- Range Slider Script Begins -->
<!--<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>-->

<!-- Range Slider Script Ends -->




    
<!-- Footer section ends -->
</body>
</html>

</asp:Content>
