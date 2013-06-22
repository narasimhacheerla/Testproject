<%@ Page Title="" Language="C#" MasterPageFile="~/BlankSite.Master" AutoEventWireup="true" CodeBehind="CustomizedInvitation.aspx.cs" Inherits="Huntable.UI.CustomizedInvitation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
<div id="header-section"  style= "height:79px;">
  <p class="top-strip">&nbsp;</p>
  <!-- this script used for both clickable slide and tab slide function -->

<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>

<!-- this script used for both clickable slide and tab slide function -->

<script type="text/javascript">
//	<![CDATA[
    $(document).ready(function () {
        $('div.test').click(function () {
            $('ul.list').slideToggle('medium');
        });
    });
    $(document).ready(function () {
        $('div.test1').click(function () {
            $('ul.list1').slideToggle('medium');
        });
    });
//]]>
</script>

<div id="header-inner">
  <div class="logo"> <a href="." title="Huntable - Find Hunt Hire"><img src="images/logo.png" width="170" height="44" alt="Huntable - Find Hunt Hire" title="Huntable - Find Hunt Hire" /></a> </div>
  <div class="menu1">
    <ul class="menu-list">
      <li><a href="." title="Home"><img src="images/icon-home.png" width="14" height="15" alt="Home" title="Home" />Home</a></li>
      <li><a href="#" title="What is Huntable"><img src="images/icon-what.png" width="14" height="15" alt="What is Huntable" title="What is Huntable" />What is Huntable</a></li>
      <li><a href="#" title="Find Friends"><img src="images/icon-friends.png" width="14" height="15" alt="Find Friends" title="Find Friends" />Find Friends</a></li>
    </ul> 
      	
    <div  class="test" >
    
    
  </div>  
  </div> 
   
</div>
   
</div>
<!-- Header section ends -->

<div id="content-section">
    	<div id="content-inner">
   	  <div class="c-main">
            	<div class="c-left">
           	    <h2>Hi <strong class="red-color">Santosh</strong></h2>
                    <h3>Arun Manickam has invited you to join Huntable</h3>
                    <div align="center" style="margin:10px 0px;box-shadow:0px 0px 2px 2px #ccc; border:1px solid #ccc;">
                    <img src="images/you-invited.jpg" width="378"  height="403" alt="You-Invited" runat="server" />
                    </div>
                </div>
                
                <div class="c-right">
                 <h2>Arun Message to you</h2>
                	<div class="user-msg">
                   
                    <p>
                    	Someone out there needs you. You are worth more than you think. Keep
                        your Profile up-to-date so employers &amp; Recruiters can find you.
                    </p>
                    </div>
                    <div class="huntable-join">
                    
                     <div class="slidingDiv slidingdiv-new1">
                   
                   <div class="banner-right">
      <h1>Join Huntable Today</h1>
      <div class="banner-right-inner">
        <input type="text" style="margin-top:0px;" class="textbox textbox-join" value="First name" onfocus="if (this.value =='First name') {this.value ='';}" onblur="if (this.value == '') {this.value ='First name';}" name="email">
        <input type="text" class="textbox textbox-join" value="Last name" onfocus="if (this.value =='Last name') {this.value ='';}" onblur="if (this.value == '') {this.value ='Last name';}" name="email">
        <input type="text" class="textbox textbox-join" value="E-mail" onfocus="if (this.value =='E-mail') {this.value ='';}" onblur="if (this.value == '') {this.value ='E-mail';}" name="email">
        <input type="text" class="textbox textbox-join" value="Password" onfocus="if (this.value =='Password') {this.value ='';}" onblur="if (this.value == '') {this.value ='Password';}" name="email">
        <a class="signin" href="default.aspx">Already a Member Sign in now</a><br>
        <input type="button" value="Join Now" class="button-green button-green-join">
        <b class="star">*</b> <span class="join">Join Huntable today for free.</span> </div>
      <p class="privacy"> <img width="14" height="18" alt="Privacy" src="images/icon-privacy.png"><b>Huntable Protects Your Privacy</b></p>
    </div>
       
                      </div>
            <a class="button-green button-green-join show_hide">
            Join now
            </a>
                  
                    <b class="star">*</b>
                    <span class="join">Join Huntable today for free.</span>
                    <p class="privacy" style="width:66%;"> <img width="14" height="18" alt="Privacy" src="images/icon-privacy.png" /><b>Huntable Protects Your Privacy</b></p>
                   </div>
                   
                  <div class="box-right box-right-new">
               
                    	<h3>Arun Activites</h3>
                    
                    <table class="a-table">
  <tr>
    <td width="156" valign="top">Following</td>
    <td width="19" valign="top">-</td>
    <td width="159" valign="top"><strong>45</strong></td>
  </tr>
  <tr>
    <td valign="top">Followers</td>
    <td valign="top">-</td>
    <td valign="top"><strong>45</strong></td>
  </tr>
  <tr>
    <td valign="top">Companies Following</td>
    <td valign="top">-</td>
    <td valign="top"><strong>45</strong></td>
  </tr>
  <tr>
    <td valign="top">Profile Views</td>
    <td valign="top">-</td>
    <td valign="top"><strong>45</strong></td>
  </tr>
  <tr>
    <td valign="top">Affiliate Earnings</td>
    <td valign="top">-</td>
    <td valign="top"><strong>$ 1445</strong></td>
  </tr>
  <tr>
    <td valign="top">Jobs on site</td>
    <td valign="top">-</td>
    <td valign="top"><strong>56,888</strong></td>
  </tr>
</table>

                
              </div>
              <a href="#" style="margin-top:15px; font-size:14px; float:left;" class="accounts-link">Find out more &rsaquo;&rsaquo;</a>
                </div>
               
            </div>
          <div id="search-inner1">
    <label>Search People:</label>
    <input type="text" class="textbox-search1-new1" value="e.g: Name, Company, Skill, Job title" onfocus="if (this.value =='e.g: Name, Company, Skill, Job title') {this.value ='';}" onblur="if (this.value == '') {this.value ='e.g: Name, Company, Skill, Job title';}" name="email" />
    <label>Search Jobs:</label>
    <input type="text" class="textbox-search1-new1" value="e.g: Job title, Skill, Keyword, Location" onfocus="if (this.value =='e.g: Job title, Skill, Keyword, Location') {this.value ='';}" onblur="if (this.value == '') {this.value ='e.g: Job title, Skill, Keyword, Location';}" name="email" /> 
    <a class="button-orange button-orange-search1-new" href="#">Search<img width="22" height="23" alt="arrow" src="images/search-arrow.png" /></a> </div>
    
   <h2 class="join-fmly"> Join Arun's network and create your own Network in 2 easy steps.</h2>
    <ul class="customized-img-list">
    	<li>
        	<p><a href="#">Super Power Your Profile</a></p>
       <a href="#"> <img src="images/customized-img1.jpg" width="139" height="94" alt="Super Power Your Profile" runat="server" /> </a>
        </li>
        <li>
        	<p><a href="#">Connect &amp; Network</a></p>
        <a href="#"><img src="images/customized-img4.jpg" width="139" height="94" alt="Connect &amp; Network" runat="server" /> </a>
        </li>
        <li>
        	<p><a href="#">Customize feeds &amp; jobs you receive</a></p>
        <a href="#"><img src="images/customized-img2.jpg" width="139" height="94" alt="Customize feeds &amp; jobs you receive"  runat="server"/> </a>
        </li>
        <li>
        	<p><a href="#">Follow your favourite person, company skill or industry.</a></p>
        <a href="#"><img src="images/customized-img3.jpg" width="139" height="94" alt="Follow your favourite person, company skill or industry." runat="server"/> </a>
        </li>
        <li>
        	<p><a href="#">Get Headhunted</a></p>
        <a href="#"><img src="images/customized-img5.jpg" width="139" height="94" alt="Get Headhunted" runat="server" /></a> 
        </li>
        <li style="margin-right:0px;">
        	<p><a href="#">Find your Dream Job</a></p>
       <a href="#"> <img src="images/customized-img6.jpg" width="139" height="94" alt="Find your Dream Job"  runat="server"/></a> 
        </li>
    </ul>
    <div class="upgrade register-font" > <strong ><a class="accounts-link register-link">Register Now</a> and its totally <strong class="red-color">Free !!!</strong></strong> 
     </div>
     
     
     
     
        
        
        </div><!-- content inner ends -->
    </div>
<!-- content section ends --> 





<!-- Clickable toggle view begins -->

<script type="text/javascript">

    $(document).ready(function () {

        $(".slidingDiv").hide();
        $(".show_hide").show();

        $('.show_hide').click(function () {
            $(".slidingDiv").slideToggle();
        });

    });
 
</script>

<!-- Clickable toggle view ends -->



</body>
</html>

</asp:Content>
