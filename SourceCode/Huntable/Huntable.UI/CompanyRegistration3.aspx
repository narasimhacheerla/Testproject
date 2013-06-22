<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyRegistration3.aspx.cs" Inherits="Huntable.UI.CompanyRegistration3" %>
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
        <div class="top-breadcrumb">
      <div class="accounts-profile2-left"> 
      <a class="accounts-link" href="#">Company</a>&nbsp;››&nbsp;<strong>Infosys</strong></div>
      
      </div>
      <div class="user-regis">
      	<div class="user-regis-left">
        <div align="center" class="user-regis-left-img">
        <a href="#" class="logo-like"></a>
   	    <a href="#"><img src="images/what-like-img1.jpg" width="314" height="163" alt="what-like" /></a>
        
        </div>
        <div class="user-regis-left-ut">
        
        <a href="#" class="accounts-link"><img src="images/edit-icon.jpg" style="vertical-align:middle;" width="23" height="23" alt="edit" align="top"  />Edit</a>
        <br /><input type="file" /><br /> Supported file types: GIF,JPG,PNG
        
        </div>
        <input type="button" class="button-green button-green-sa" value="Save" />
        
        </div>
        <div class="user-regis-right">
        <table class="reg-table">
  <tr>
    <td  valign="top">
    
    <input type="text" onblur="if (this.value == '') {this.value ='e.g: Your company name';}" onfocus="if (this.value =='e.g: Your company name') {this.value ='';}" value="e.g: Your company name" class="textbox textbox-ut" /><a href="#"><img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top"  /></a>
    <sup class="red-color red-color1">*&nbsp;</sup>
    </td>
    
  </tr>
  <tr>
    <td  valign="top">
    
    <input type="text" onblur="if (this.value == '') {this.value ='e.g: Brief heading about your company';}" onfocus="if (this.value =='e.g: Brief heading about your company') {this.value ='';}" value="e.g: Brief heading about your company" class="textbox textbox-ut" /><a href="#"><img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top"  /></a>
    <sup class="red-color red-color1">*&nbsp;</sup>
    </td>    
  </tr>
  <tr>
    <td  valign="top">
    
    <textarea class="textbox textbox-ut" onfocus="if(this.value==this.defaultValue)this.value='';" onblur="if(this.value=='')this.value=this.defaultValue;">Now lets hear about your business, short &amp; sweet...</textarea>
   <a href="#"> <img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top"  /></a>
    <sup class="red-color red-color1">*&nbsp;</sup>
    </td>
    
  </tr>
</table>
<table class="reg-table">
<tr>
	<td colspan="2"><strong style="font-size:18px;">Business Info</strong></td>
</tr>
  <tr valign="top">
    <td width="49%" height="29">
    
    	<select class="textbox textbox-ut2">
        	<option>Industry1</option>
            <option>Industry2</option>
            <option>Industry3</option>
        </select>
        <sup class="red-color red-color1">*&nbsp;</sup>
    </td>
    <td width="51%">
        <select class="textbox textbox-ut2">
        	<option>No of employees</option>
            <option>1</option>
            <option>2</option>
        </select>
        <sup class="red-color red-color1">*&nbsp;</sup>
    </td>
  </tr>
  
  <tr valign="top">
    <td><input type="text" onblur="if (this.value == '') {this.value ='website address www.companyname.com';}" onfocus="if (this.value =='website address www.companyname.com') {this.value ='';}" value="website address www.companyname.com" class="textbox textbox-ut1" /><a href="#"><img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top"  /></a>
    <sup class="red-color red-color1">*&nbsp;</sup>
    </td>
    <td><input type="text" onblur="if (this.value == '') {this.value ='address';}" onfocus="if (this.value =='address') {this.value ='';}" value="address" class="textbox textbox-ut1" /><img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top"  />
    <sup class="red-color red-color1">*&nbsp;</sup>
    </td>
  </tr>
  <tr valign="top">
    <td><input type="text" onblur="if (this.value == '') {this.value ='Phone no: eg 0044 2000 67 456';}" onfocus="if (this.value =='Phone no: eg 0044 2000 67 456') {this.value ='';}" value="Phone no: eg 0044 2000 67 456" class="textbox textbox-ut1" /><a href="#"><img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top"  /></a>
    <sup class="red-color red-color1">*&nbsp;</sup>
    </td>
    <td><input type="text" onblur="if (this.value == '') {this.value ='address';}" onfocus="if (this.value =='address') {this.value ='';}" value="address" class="textbox textbox-ut1" /><a href="#"><img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top"  /></a>
    <sup class="red-color red-color1">*&nbsp;</sup>
    </td>
  </tr>
  
  <tr valign="top">
    <td><input type="text" onblur="if (this.value == '') {this.value ='email address';}" onfocus="if (this.value =='email address') {this.value ='';}" value="email address" class="textbox textbox-ut1" /><a href="#"><img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top"  /></a>
    <sup class="red-color red-color1">*&nbsp;</sup>
    </td>
    <td><input type="text" onblur="if (this.value == '') {this.value ='Town/ciy';}" onfocus="if (this.value =='Town/ciy') {this.value ='';}" value="Town/ciy" class="textbox textbox-ut1" /><a href="#"><img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top"  /></a>
    <sup class="red-color red-color1">*&nbsp;</sup>
    </td>
  </tr>
  
  <tr valign="top">
    <td>&nbsp;</td>
    <td><input type="text" onblur="if (this.value == '') {this.value ='Post code';}" onfocus="if (this.value =='Post code') {this.value ='';}" value="Post code" class="textbox textbox-ut1" /><a href="#"><img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top"  /></a>
    <sup class="red-color red-color1">*&nbsp;</sup>
    </td>
  </tr>
  
  <tr>
    <td>&nbsp;</td>
    <td valign="top">
    
    <select class="textbox textbox-ut2">
        	<option>Country</option>
            <option>Country 1</option>
            <option>Country 2</option>
        </select>
        <sup class="red-color red-color1">*&nbsp;</sup>
    </td>
  </tr>
  
  
  
  
</table>


        </div>
      </div>
      
      <div class="user-add">
      <ul class="overview-tab">                
                <li><a class="selected-tab" href="companyoverview.aspx">Overview</a></li>               
                <li><a href="companyproducts.aspx">Products &amp; Services</a></li>
                <li><a href="careers.aspx">Careers</a></li>
                <li><a href="companyblogsrecent.aspx">Business Blog</a></li>
                <li><a href="article.aspx">Article</a></li>                
                            
              </ul>
      <div class="user-add-left">
      <h3>Your Portfolio</h3>
      	<div class="prot-list">
        	<div style="float:left; width:145px;">
             <a href="#" class="logo-like logo-like1"><img src="images/icon-plus-inner.png" width="23" height="24" alt="plus" /></a>
       	    <img src="images/no-image1.jpg" class="profile-pic" width="125" height="125" alt="no-image" /><br />
            <input type="file" size="7" /><br />
            <a href="#" class="accounts-link accounts-link-ut" ><img src="images/add-icon.png" width="14" height="14" alt="add" /> Add</a>
            </div>
            
           <p class="text-port" style="width:73%;">
        <textarea  class="textbox textbox-ut textbox-ut-port" onfocus="if(this.value==this.defaultValue)this.value='';" onblur="if(this.value=='')this.value=this.defaultValue;">Say something about this portfolio</textarea>	
        <a href="#"><img src="images/edit-icon-new.png" align="top" width="23" height="23" alt="edit" /></a><br /><br />
       <p > <sup class="red-color red-color1">*&nbsp;</sup>Your latest portfolio will be shown first
        </p>                         
            
        
        </div>
        <div class="block-margin">
            <input type="button" value="Save" class="button-blue button-blue-sk " />
            <a href="#" class="accounts-link accounts-link-sk">Skip</a>
            </div>
        
      </div>
      <div class="user-add-right">
      <h3>Your Video</h3>
      <p class="block-margin">
      	Share a link to a You tube,Vimeo,DailyMotion, or Google video about your company	
      </p>
      <input type="text" onblur="if (this.value == '') {this.value ='Video url';}" onfocus="if (this.value =='Video url') {this.value ='';}" value="Video url" class="textbox" />
      <br />
      <a href="#" class="accounts-link accounts-link-ut"><img src="images/add-icon.png" width="14" height="14" alt="add" /> Add</a>
      <div class="block-margin1">
      	Video
        </div>
      </div>
      </div>
      
      <div class="user-regis user-regis-margin">
      	<div class="user-regis-left">
        <div align="center" class="user-regis-left-img">
        
   	    <a href="#"><img src="images/what-like-img2.jpg" width="314" height="163" alt="what-like" /></a>
        
        </div>
        <div class="user-regis-left-ut">
        
        <a href="#" class="accounts-link"><img src="images/edit-icon.jpg" style="vertical-align:middle;" width="23" height="23" alt="edit" align="top"  />Edit</a>
        <br /><input type="file" /><br /> Supported file types: GIF,JPG,PNG
        
        </div>
        <input type="button" class="button-green button-green-sa" value="Save" />
        
        </div>
        <div class="user-regis-right">
        <table class="reg-table">
  
  <tr>
  	<td><h3><sup class="red-color red-color1">*&nbsp;</sup> Product Description</h3></td>
  </tr>
  <tr>
    <td  valign="top">
    
    <textarea class="textbox textbox-ut" onfocus="if(this.value==this.defaultValue)this.value='';" onblur="if(this.value=='')this.value=this.defaultValue;"></textarea>
   
    
    </td>
    
  </tr>
</table>


        </div>
        <div class="rgis-bot">
        	<h3>Careers:</h3>
            <table class="tab-care">
  <tr>
    <td width="222">Your company career page web link</td>
    <td width="12">:</td>
    <td width="240"><input type="text" class="textbox1" /></td>
  </tr>
  <tr>
    <td>Your Company Blog address</td>
    <td>:</td>
    <td><input type="text" class="textbox1" /></td>
  </tr>
  <tr>
    <td><input type="button" class="button-green floatright" value="Save" /></td>
    <td>&nbsp;</td>
    <td><a href="#" class="accounts-link">Skip</a></td>
  </tr>
</table>

        </div>
        
        <div class="rgis-bot">
       	  <h3>Settings:</h3>
            <ul class="set-list">
            	<li><h3>Invite your employees to join your company page</h3></li>
                <li>
                <div class="bord">
                <p >
                <strong>Import your contacts from a CSV file</strong></p>
                 <input type="button" class="button-orange floatright" value="Import" />
                <strong style="float:right; margin:7px 10px 0px 0px;"><img src="images/excel-icon.png" width="30" height="32" alt="Excel-icon" /></strong>
               </div>
                </li>
                <li>
                <input type="button" class="button-green floatright" style="margin-right:160px;" value="Save" />
                </li>
            </ul>
            <ul class="set-list set-list1">
            	<li><h3>Enter your employees email address to invite them to join your  company</h3></li>
                <li>
                
               
                 <input type="text" class="textbox1 floatleft" />
                 <input type="button" class="button-orange floatleft" style="margin-left:10px;" value="Send invite" />
               
               
                </li>
                <li style="margin-top:10px;">
                You can invite by  exporting their email address<br /><br />
                <a href="#"><img src="images/si-1.jpg" width="45" height="46" alt="opera" title="Opera" /></a>
                <a href="#"><img src="images/si-2.jpg" width="45" height="46" alt="yahoo" title="yahoo" /></a>
                <a href="#"><img src="images/si-3.jpg" width="45" height="46" alt="G-mail" title="G-mail" /></a>
                <a href="#"><img src="images/si-4.jpg" width="45" height="46" alt="Msn" title="Msn" /></a>
                
                </li>
            </ul>
        </div>
      </div>
      
      
      
      
        	
        </div><!-- content inner ends -->
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
