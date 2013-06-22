<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdvancedSearch.aspx.cs" Inherits="Huntable.UI.AdvancedSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Huntable - The Professional Network</title>
<meta name="description" content="Huntable - The Professional Network" />
<meta name="keywords" content="The Professional Network" />
<link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
<script type="text/javascript">
    $(function () {

        var msie6 = $.browser == 'msie' && $.browser.version < 7;
        if (!msie6) {
            var top = $('#bx9').offset().top;
            $(window).scroll(function (event) {
                var y = $(this).scrollTop();
                if (y >= 0) { $('#bx9').addClass('fixed'); }
                else { $('#bx9').removeClass('fixed'); }
            });
        }
    });</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="content-section">

<div id="content-inner">
  <div class="content-inner-left">
  <h2 class="login-heading">Advanced Search</h2>
  <div class="advanced-search">
    <div> <input type="text" class="textbox textbox-search1 textbox-search3" onblur="if (this.value == '') {this.value ='Skills';}" onfocus="if (this.value =='Skills') {this.value ='';}" value="Skills" /> <a href="#"><img src="images/search-img.png" width="28" height="27" alt="search" /></a></div>
       <div><input type="text" class="textbox textbox-search1 textbox-search3" onblur="if (this.value == '') {this.value ='Keywords';}" onfocus="if (this.value =='Keywords') {this.value ='';}" value="Keywords" /> <a href="#"><img src="images/search-img.png" width="28" height="27" alt="search" /></a></div>
       <div><input name="email" type="text" onblur="if (this.value == '') {this.value ='Title';}" onfocus="if (this.value =='Title') {this.value ='';}" value="Title" class="textbox textbox-search4" /></div>
       <div><select class="textbox textbox-search4" style="width:263px;">
                    	<option>Current</option>
                        <option>Past</option>                      
                    </select></div>
       <div> <a href="#" class="button-green" style="float:left;">Search</a></div>
       </div>
       <div class="company-name-list">
             <h3>Alphabetical List</h3>
                  <div class="alphabet"> <a href="#">a</a><a href="#">b</a><a href="#">c</a><a href="#">d</a><a href="#">e</a><a href="#">f</a><a href="#">g</a> <a href="#">i</a><a href="#">j</a><a href="#">k</a><a href="#">l</a><a href="#">m</a><a href="#">n</a><a href="#">0</a> <a href="#">p</a><a href="#">q</a><a href="#">r</a><a href="#">t</a><a href="#">u</a><a href="#">v</a><a href="#">w</a> <a href="#">x</a><a href="#">y</a><a href="#" style="border-right:0px;">z</a> </div> 
           <div class="upload-inner">
                	<label>Firstname:</label><input type="text" class="textbox" /><br /><br />
					<label>Lastname:</label><input type="text" class="textbox" /><br /><br />
                    <label>School:</label>
                    <input type="text" class="textbox" /><br /><br />
                    <label>Experience:</label><input type="text" class="textbox" /><br /><br />
                    <label>Company:</label><input type="text" class="textbox" /><br /><br />
                     <label>Current or Past:</label>
                    <select class="textbox listbox">
                    	<option>Current</option>
                        <option>Past</option>                        
                    </select><br /><br />
                     <label>Country:</label>
                    <select class="textbox listbox">
                    	<option>Uk</option>
                        <option>Us</option>                        
                    </select><br /><br />
                    <label>Available now:</label>
                    <select class="textbox listbox">
                    	<option>Yes</option>
                        <option>No</option>                        
                    </select><br /><br />
                    <label>Endorsesment:</label>
                    <select class="textbox listbox">
                    	<option>Yes</option>
                        <option>No</option>                        
                    </select><br /><br />
                    <label>Profile News:</label>
                    <select class="textbox listbox">
                    	<option>Yes</option>
                        <option>No</option>                        
                    </select><br /><br />
                    <label>Select Language:</label>
                    <select class="textbox listbox">
                    	<option>Yes</option>
                        <option>No</option>                        
                    </select><br /><br />
                    <label>Industry:</label>
                    <select class="textbox listbox">
                    	<option>Yes</option>
                        <option>No</option>                        
                    </select><br /><br />
                    <label>Interest:</label>
                    <select class="textbox listbox">
                    	<option>Yes</option>
                        <option>No</option>                        
                    </select><br /><br />
                    <label>Connection:</label>
                    <select class="textbox listbox">
                    	<option>Yes</option>
                        <option>No</option>                        
                    </select><br /><br />
                    <label>&nbsp;</label>
                    <a href="#" class="button-green button-green-upload ">Search</a>
                    
                   
                    
         </div>
                  
      </div>
                      
    </div><!-- content inner left ends -->
            <div class="content-inner-right" id="bx9">
            <div>
      <img src="images/advanced-search-img.png" width="294" style="float:left;" class="profile-pic" height="141" alt="advanced" />
      <div class="advanced-desc">
      Find Best Talent<br />
      <strong class="blue-color">Advanced Search Filters</strong><br />
      <a href="UserSearch.aspx" class="button-green floatleft" style="margin:10px 0px 0px 105px;">Search Now</a>
      </div>
      </div>            
      </div><!-- content inner right ends -->
        </div><!-- content inner ends -->
    </div>
     <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
