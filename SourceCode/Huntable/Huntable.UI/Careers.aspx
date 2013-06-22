 <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Careers.aspx.cs" Inherits="Huntable.UI.Careers" %>
<%@ Register Src="~/UserControls/Messagepopup.ascx" TagName="mesgpopup" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/InfosysEmployees.ascx" TagName="infosysemp" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/Followers.ascx" TagName="followers" TagPrefix="uc3" %>
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
 <script type="text/javascript">
     $(function () {

         var msie6 = $.browser == 'msie' && $.browser.version < 7;
         if (!msie6) {
             var top = $('#bx7').offset().top;
             $(window).scroll(function (event) {
                 var y = $(this).scrollTop();
                 if (y >= 315) { $('#bx7').addClass('fixed'); }
                 else { $('#bx7').removeClass('fixed'); }
             });
         }
     });</script>
</head>

<body>

<!-- Header section ends -->
<!--[if lte IE 6]>
<script type="text/javascript" >
//author: ?, I found this useful.
startList = function() {
if (document.all&&document.getElementById) {
navRoot = document.getElementById("nav");
for (i=0; i<navRoot.childNodes.length; i++) {
node = navRoot.childNodes[i];
if (node.nodeName=="LI") {
node.onmouseover=function() {
this.className+=" over";
  }
  node.onmouseout=function() {
  this.className=this.className.replace(" over", "");
   }
   }
  }
 }
}
window.onload=startList;

</script>
<![endif]-->
<!-- main menu ends -->
<div id="content-section">

  <div id="content-inner">
  <div class="accounts-profile2 ">
  <div class="top-breadcrumb">
      <div class="accounts-profile2-left"> 
      <a class="accounts-link" href="#">Business</a>&nbsp;››&nbsp;<a class="accounts-link" href="#">Infosys</a>&nbsp;››&nbsp;<strong>Careers</strong></div>
      
      </div>
     <ul class="overview-tab">                
                <li><a href="companyview.aspx" >Overview</a></li>
                <li><a href="businessactivity.aspx" >Activity</a></li>
                <li><a href="companyproducts.aspx">Products &amp; Services</a></li>
                <li><a href="CompanyJobs.aspx" class="selected-tab" >Careers</a></li>
                <li><a href="companyblogsrecent.aspx" >Business Blog</a></li>
                <li><a href="article.aspx" >Article</a></li>                
                <uc1:mesgpopup ID="mes" runat="server" />              
              </ul> 
  </div>
      
    <div class="content-inner-left" style="width:755px;">
      <div class="notification">
      <div class="all-feeds-list">
                        	
                         <div class="general-setting-tab1 general-setting-tab1-overview">
            	<a class="setting-active" href="companyjobs.aspx">Company jobs in Huntable</a>
                <a href="companycareers.aspx">Company Careers page</a>
            </div>
                           
                
                    </div>
        <div class="notification-left-main">
          <div class="job-posted-logo"> <a href="#"><img src="images/job-posted-logo.jpg" width="204" height="123" alt="infosys" /></a> </div>
          <div class="notification-left" id="bx7" > <b class="job-search-heading">Job Search</b>
            <input name="email" type="text" onblur="if (this.value == '') {this.value ='Job Title';}" onfocus="if (this.value =='Job Title') {this.value ='';}" value="Job Title" class="textbox textbox-inner" style="margin-top:0px;" />
            <input name="email" type="text" onblur="if (this.value == '') {this.value ='Keywords';}" onfocus="if (this.value =='Keywords') {this.value ='';}" value="Keywords" class="textbox textbox-inner" />
            <select class="textbox selectbox-inner">
              <option>Country</option>
              <option>Country 1</option>
              <option>Country 2</option>
            </select>
            <select class="textbox selectbox-inner">
              <option>Location</option>
              <option>Location 1</option>
              <option>Location 2</option>
            </select>
            <select class="textbox selectbox-inner">
              <option>Salary</option>
              <option>24234</option>
              <option>234234</option>
            </select>
            <select class="textbox selectbox-inner">
              <option>Experience</option>
              <option>10</option>
              <option>20</option>
            </select>
            <input name="email" type="text" onblur="if (this.value == '') {this.value ='Company';}" onfocus="if (this.value =='Company') {this.value ='';}" value="Company" class="textbox textbox-inner" />
            <select class="textbox selectbox-inner">
              <option>Industry</option>
              <option>Industry 1</option>
              <option>Industry 1</option>
            </select>
            <select class="textbox selectbox-inner">
              <option>Skill</option>
              <option>c</option>
              <option>PHP</option>
            </select>
            <select class="textbox selectbox-inner">
              <option>Job Type</option>
              <option>Job Type 1</option>
              <option>Job Type 2</option>
            </select>
            <a href="#" class="button-green button-green-jobpost">Search</a> </div>
        </div>
        
        
        <div class="notification-right notification-right1">
         <div class="r-notification-main">
          	<div class="r-notification">
     	     <div class="r-notification-top">
       <div class="r-notification-left" style="width:130px;" > 
            <a href="#"> 
            <img src="images/featured-logo2.jpg" class="profile-pic" width="124" height="70" alt="logo" />
             </a>
       </div>
            
            <div class="r-notification-mid" style="width:300px;">
             <table class="r-notification-mid-table" width="100%" >
               <tr>
               	<td colspan="3" align="center" valign="top">
                 <a href="#" class="grey-link">Sales Consultants for Exciting new 24 gym in Stanway colchester</a>
                </td>
              </tr>
  <tr>
    <th width="41%" align="right" valign="top">Job Type</th>
    <td width="3%" valign="top">:</td>
    <td width="56%" align="left" valign="top">Fulltime</td>
  </tr>
  <tr>
    <th align="right" valign="top">Experience Req</th>
    <td valign="top">:</td>
    <td align="left" valign="top">2 Minimum</td>
  </tr>
  <tr>
    <th align="right" valign="top" >Industry</th>
    <td valign="top">:</td>
    <td align="left" valign="top">Accounting</td>
  </tr>
  <tr>
    <th align="right" valign="top" >Skill</th>
    <td valign="top">:</td>
    <td align="left" valign="top">Chef</td>
  </tr>
  <tr>
    <th align="right" valign="top" >Salary</th>
    <td valign="top">:</td>
    <td align="left" valign="top">40000 UK Pounds</td>
  </tr>
  <tr>
    <th align="right" valign="top" >Job Posted on</th>
    <td valign="top">:</td>
    <td align="left" valign="top">9/25/2012 1:58:43 AM</td>
  </tr>
</table>
            </div>
            
            <div class="r-notification-right">
             <a class="invite-friend-btn top-space"  href="#">Apply now + </a>
              </div>
              </div>
            <p> Letting Manager / Assistant Manager - Bristol Avon due to the ongoing expansion. Letting division our clients are loking for a ambitious and enthusiastic Manager's to the launch of several brand new office..&nbsp;
            <a href="#" class="orange-link">more</a>
            </p>
            </div>
          
          </div>
          
          <div align="center"> 
          <a href="#"><img src="images/google-banner-medium.jpg" width="300" height="100" alt="google-add" /></a>
          
           </div>
          <p class="strip-bottom">&nbsp;</p>
          <div class="pagination-new" style="width:520px;"> <span>01-10 Of <strong>34</strong></span> <a href="#"><img src="images/pagination-arrow-left1.png" width="6" height="7" alt="arrow" /></a> <a href="#"><img src="images/pagination-arrow-left.png" width="4" height="7" alt="arrow" /></a>
            <input type="text" class="textbox textbox-page" value="1" />
            Of 4 Pages <a href="#"><img src="images/pagination-arrow-right.png" width="4" height="7" alt="arrow" /></a> <a href="#"><img src="images/pagination-arrow-right1.png" width="6" height="7" alt="arrow" /></a>
            <select class="textbox select-page">
              <option>1</option>
              <option>2</option>
              <option>3</option>
            </select>
          </div>
        </div>
        
        
      </div>
    </div>
    <div class="content-inner-right content-inner-right-width">
       <div class="view-company-info" style="width:198px;">
        <a href="#" style="margin-right:10px;" class="button-green floatleft">Post an opportunity</a>
        <a class="button-orange view-company-info-follow" style="padding:5px 7px;" href="#"> Follow </a>
 </div>
 <p class="margin-top-visible">&nbsp;</p>
 <div class="box-right box-right-company">
             <uc3:followers ID="Followers1" runat="server"></uc3:followers>
              </div> 
  <p class="margin-top-visible">&nbsp;</p>
              
 <uc4:infosysemp  ID="infy" runat="server"/>
              
              

    
    

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




<!-- Footer section ends -->
</body>
</html>



</asp:Content>
