<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InviteFriendsnew.aspx.cs" Inherits="Huntable.UI.Invite_Friends" %>
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
            var top = $('#bx6').offset().top;
            $(window).scroll(function (event) {
                var y = $(this).scrollTop();
                if (y >=642) { $('#bx6').addClass('fixed'); }
                else { $('#bx6').removeClass('fixed'); }
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
        	<div class="content-inner-left">
            	<div class="contacts-head">
                	<h3 style="float:left;">Friends from your various networks and connections </h3>                    
            	</div>
            <div class="contacts-select" style="width: 635px;">
                	<span class="floatleft" style="margin-top:7px;"><input  type="checkbox" class="checkbox"  /> Select all</span>
                <a href="#" class="button-green" style=" float:right;">Invite all  +</a>
            </div>
            <div class="contacts-select-inner" style="width: 635px;">
                	<div class="invite-friends-tab">
                    <input  type="checkbox" class="checkbox1"  /> 
                    <a href="#"><img src="images/profile-thumb-large.jpg" class="profile-pic"  width="76" height="81" alt="Invite-friends-img" /></a>
                    <a href="#" class="accounts-link ">Rhodes
                    </a>
                    <br />
                    <span><a href="#" class="accounts-link">Rajaram Rajgopal</a> is a Mutual Friend</span>
                    <a  href="#"  rel="popup4 " class="invite-friend-btn  poplight">Invite Friend  +</a>
                     </div>
            
                     <div class="invite-friends-tab" ">
                    <input  type="checkbox" class="checkbox1"   /> 
                    <a href="#"><img src="images/profile-thumb-large.jpg" class="profile-pic"  width="76" height="81" alt="Invite-friends-img" /></a>
                    <a href="#" class="accounts-link ">Rhodes
                    </a> 
                    <br />
                    <span>IHM Chennai</span><br />
                    <span><a href="#" class="accounts-link ">Poonam Dhadwal</a> and <a href="#" class="accounts-link ">3 other Mutual friends</a></span>
                      <a  href="#"  rel="popup4 " class="invite-friend-btn  poplight">Invite Friend  +</a>
                     </div>
                     <div class="invite-friends-tab" >
                    <input  type="checkbox" class="checkbox1"   /> 
                    <a href="#"><img src="images/profile-thumb-large.jpg" class="profile-pic"  width="76" height="81" alt="Invite-friends-img" /></a>
                    <a href="#" class="accounts-link ">Rhodes
                    </a>
                    <br />
                    <span>IHM Chennai</span><br />
                    <span><a href="#" class="accounts-link ">Yogesh Dhandara</a> and <a href="#" class="accounts-link ">Ganesh</a> are Mutual Friends</span>
                      <a  href="#"  rel="popup4 " class="invite-friend-btn  poplight">Invite Friend  +</a>
                     </div>
                     <div class="invite-friends-tab">
                    <input  type="checkbox" class="checkbox1"   /> 
                    <a href="#"><img src="images/profile-thumb-large.jpg" class="profile-pic"  width="76" height="81" alt="Invite-friends-img" /></a>
                    <a href="#" class="accounts-link ">Rhodes
                    </a>
                    <br />
                    <span><a href="#" class="accounts-link">Rajaram Rajgopal</a> is a Mutual Friend</span>
                      <a  href="#"  rel="popup4 " class="invite-friend-btn  poplight">Invite Friend  +</a>
                     </div>
                     <div class="invite-friends-tab" >
                    <input  type="checkbox" class="checkbox1"   /> 
                    <a href="#"><img src="images/profile-thumb-large.jpg" class="profile-pic"  width="76" height="81" alt="Invite-friends-img" /></a>
                    <a href="#" class="accounts-link ">Rhodes
                    </a>
                    <br />
                    <span>IHM Chennai</span><br />
                    <span><a href="#" class="accounts-link ">Poonam Dhadwal</a> and <a href="#" class="accounts-link ">3 other Mutual friends</a></span>
                      <a  href="#"  rel="popup4 " class="invite-friend-btn  poplight">Invite Friend  +</a>
                     </div>
                     <div class="invite-friends-tab">
                    <input  type="checkbox" class="checkbox1"   /> 
                    <a href="#"><img src="images/profile-thumb-large.jpg" class="profile-pic"  width="76" height="81" alt="Invite-friends-img" /></a>
                    <a href="#" class="accounts-link ">Rhodes
                    </a>
                    <br />
                    <span>IHM Chennai</span><br />
                    <span><a href="#" class="accounts-link ">Yogesh Dhandara</a> and <a href="#" class="accounts-link ">Ganesh</a> are Mutual Friends</span>
                     <a  href="#"  rel="popup4 " class="invite-friend-btn  poplight">Invite Friend  +</a>
                     </div>
                     
                     
            </div>
            <a href="#" class="show-more show-more-margin">Show More</a>
           
            </div><!-- content inner left ends -->
            <div class="content-inner-right">
              <div class="box-right" >
                <div class="head-ash">
                    	<h3>Your Invitations Glance</h3>
                    </div>
                <p class="account-rating">Total Invitations&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;<strong>450</strong></p>
                <p class="account-rating">Friends Joined&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;<strong>342</strong></p>
                <p class="account-rating">Total 1st Connections&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;<strong>245</strong></p>
                <p class="account-rating">Total 2nd Connections&nbsp;&nbsp;:&nbsp;&nbsp;<strong>74325</strong></p>
                <p class="account-rating">Total 3rd Connections&nbsp;&nbsp;:&nbsp;&nbsp;<strong>62345</strong></p>
                <p class="account-rating color-hunt">Total Affiliate Earning&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;<strong>67235</strong></p>                   
                
              </div>                <p class="margin-top-visible">&nbsp;</p>
           	  <div class="box-right" >
                <div class="head-ash">
               	  <h3>Invites Your Friends &amp; Earn Money</h3>
                  </div>
                  <div class="social-icon">
                 <a href="#" title="Facebook"><img src="images/facebook.jpg" width="34" height="34" alt="Facebook" title="Facebook" /></a> <a href="#" title="Linked in"><img src="images/linkedin.jpg" width="34" height="34" alt="Linkedin" title="Linked in" /></a> <a href="#" title="Twitter"><img src="images/twitter.jpg" width="34" height="34" alt="Twitter" title="Twitter" /></a> <a href="#" title="Gmail"><img src="images/gmail.jpg" width="34" height="34" alt="Gmail" title="Gmail" /></a><a href="#" title="yahoo"><img src="images/yahoo.png" width="34" height="34" alt="yahoo" /></a> <a href="#" title="Msn"><img src="images/msn.jpg" width="34" height="34" alt="Msn" title="Msn" /></a>            
                   <b>This is how it works!</b>
                    </div>
                    <ul class="connection-list">
                   	<li>
                    	<a href="#">1st Connection</a>
                        <p>You Get<strong> $4 </strong>per connection</p>
                    </li>
                    <li>
                    	<a href="#">2nd Connection</a>
                        <p>Your friend gets <strong>$4 </strong>– You get <strong>$1</strong> per connection </p>
                    </li>
                    <li>
                    	<a href="#">3rd Connection</a>
                        <p>Your friends friend gets<strong> $4,</strong> Your friend gets <strong>$1</strong>, you get<strong> $0.5</strong> per connection</p>
                    </li>
                   </ul>
                   <a href="#" class="learn-more">Learn More</a>
                 <ul class="how-it-work-list">
                 	<li><a href="#"><img class="profile-pic" src="images/profile-thumb-small.jpg" width="46" height="45" alt="thumb-small" /></a>234</li>
                    <li><a href="#"><img class="profile-pic" src="images/profile-thumb-small.jpg" width="46" height="45" alt="thumb-small" /></a>435345</li>
                    <li><a href="#"><img class="profile-pic" src="images/profile-thumb-small.jpg" width="46" height="45" alt="thumb-small" /></a>355</li>
                    <li><a href="#"><img class="profile-pic" src="images/profile-thumb-small.jpg" width="46" height="45" alt="thumb-small" /></a>947</li>
                    <li><a href="#"><img class="profile-pic" src="images/profile-thumb-small.jpg" width="46" height="45" alt="thumb-small" /></a>46</li>
                 </ul>
              </div>                <p class="margin-top-visible">&nbsp;</p>                
              <div class="box-right" id="bx6">
                <div class="head-ash">
               	  <h3>Import Contacts</h3>
                  </div>
                  <p class="import-contact-desc">
                  Its easy to search your social, email contacts and grow your network.
                  </p>
                
                 <div class="social-icon">
                 <a href="#" title="Facebook"><img src="images/facebook.jpg" width="34" height="34" alt="Facebook" title="Facebook" /></a> <a href="#" title="Linked in"><img src="images/linkedin.jpg" width="34" height="34" alt="Linkedin" title="Linked in" /></a> <a href="#" title="Twitter"><img src="images/twitter.jpg" width="34" height="34" alt="Twitter" title="Twitter" /></a> <a href="#" title="Gmail"><img src="images/gmail.jpg" width="34" height="34" alt="Gmail" title="Gmail" /></a><a href="#" title="yahoo"><img src="images/yahoo.png" width="34" height="34" alt="yahoo" /></a> <a href="#" title="Msn"><img src="images/msn.jpg" width="34" height="34" alt="Msn" title="Msn" /></a>            
                   
                    </div>
                    <a href="#" class="invite-friend-btn" style="margin:0px 0px 5px 15px; float:left;">Upload File</a> 
                    <a href="#" class="learn-more">Import</a>
              </div>                               
          </div><!-- content inner right ends -->
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

<div id="popup4" class="popup_block">
 <div class="apply-job ">
<div class="invi-left">
<h3>Use this pictures when my friends see the invitation</h3>
<a href="#"><img width="76" height="81" alt="Invite-friends-img" class="profile-pic profile-pic2" src="images/profile-thumb-large.jpg" /></a>
<input type="button" class="button-green" value="Yes" /><br /><br />

<strong>Change Picture</strong><br />
<input type="file" />
	
</div>
<div class="invi-right">
<h3>Use this pictures when my friends see the invitation</h3>
 <textarea  onblur="if(this.value=='')this.value=this.defaultValue;" onfocus="if(this.value==this.defaultValue)this.value='';"  class="textarea-profile textarea-comment" style="width:370px;">Write Something to invite your friends to join your network</textarea><br /><br />
<a href="#" class="accounts-link">Don't use customized invitation</a>

<input type="button" class="button-yellow button-orange-invite" value="Send Invite"  /><br />
<span class="red-color">*</span> Terms &amp; Condition apply
</div>

</div>
  
</div>

<!-- Popup Script Begins -->


<!-- Popup Script Ends -->
</body>
</html>

</asp:Content>
