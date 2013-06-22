<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyCareers.aspx.cs" Inherits="Huntable.UI.CompanyCareers" %>
<%@ Register Src="~/UserControls/Messagepopup.ascx" TagName="mesgpopup" TagPrefix="uc1" %>
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
<link type="text/css" href="css1/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    
</head>

<body>


<!-- main menu ends -->
<div id="content-section">

  <div id="content-inner">
  <div class="accounts-profile2 ">
  <div class="top-breadcrumb">
      <div class="accounts-profile2-left"> 
      <a class="accounts-link" href="companieshome.aspx">Company</a>&nbsp;››&nbsp;<a class="accounts-link" id="a_comP_view" runat="server">
      <asp:Label runat="server" ID="lbl_compName">
      </asp:Label></a>&nbsp;››&nbsp;<strong>Careers</strong></div>
      
      </div>
     <ul class="overview-tab">                
                <li><a id="overview" runat="server" >Overview</a></li>
                <li><a id="activity" runat="server" >Activity</a></li>
                <li><a id="productsandservices" runat="server" >Products &amp; Services</a></li>
                <li><a id="careers" runat="server" class="selected-tab">Careers</a></li>
                <li><a id="busunessblog" runat="server" >Business Blog</a></li>
                <li><a id="article" runat="server" >Article</a></li>     
                 <uc1:mesgpopup ID="mesgpopup" runat="server" />
                 </ul> 
  </div>
      <div class="general-setting-tab1 general-setting-tab-overview">
            	<a id="a_Comp_jobs" runat="server" >Company jobs in Huntable</a>
                <a class="setting-active1" id="ccr" runat="server" href="#">Company Careers page</a>
            </div>

            <br /><br /><br /><br /><br />
   <div id="divreg" runat="server" visible="false" style="border:1px solid grey;margin-top: 60px;
height: 60px;border-radius: 5px;">
  <h2 style="font-family:Georgia;text-align: center;
margin-top: 20px;">This company has not updated their career page link</h2>  
   </div>
           <div class="career-block">
            	  <iframe id="ccl"  runat="server"   width="994px" height= "1300px" visible="true"></iframe>
 
               </iframe>
            </div>
           
    
    
  </div>
  <!-- content inner ends --> 
</div>
<!-- content section ends -->
<!-- Range Slider Script Begins -->
<!--<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>-->
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js" type="text/javascript"></script>
<script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
<script type="text/javascript" src="js/flipcounter.js"></script>
<!-- Range Slider Script Ends -->




</body>
</html>
</asp:Content>
