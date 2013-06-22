<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyBlogsRecent.aspx.cs" Inherits="Huntable.UI.CompanyBlogsRecent" %>
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
     <script type="text/javascript">
         $(function () {

             var msie6 = $.browser == 'msie' && $.browser.version < 7;
             if (!msie6) {
                 var top = $('#bx10').offset().top;
                 $(window).scroll(function (event) {
                     var y = $(this).scrollTop();
                     if (y >= 42) { $('#bx10').addClass('fixed'); }
                     else { $('#bx10').removeClass('fixed'); }
                 });
             }
         });</script>
    
<style type="text/css">

#slidebox{position:relative;  }
#slidebox, #slidebox .content{width:650px; margin-bottom:20px;}
#slidebox, #slidebox .container, #slidebox .content{height:230px;}
#slidebox{overflow:hidden;}
#slidebox .container{position:relative; left:0;
        top: 0px;
    }
#slidebox .content{background:#eee; float:left;}
#slidebox .content div{ height:100%; font-family:Verdana, Geneva, sans-serif; font-size:13px;}
#slidebox .next, #slidebox .previous{position:absolute; z-index:2; display:block; width:21px; height:21px;}
#slidebox .next{right:0; margin-right:10px; background:url(slidebox_next.png) no-repeat left top;}
#slidebox .next:hover{background:url(slidebox_next_hover.png) no-repeat left top;}
#slidebox .previous{margin-left:10px; background:url(slidebox_previous.png) no-repeat left top;}
#slidebox .previous:hover{background:url(slidebox_previous_hover.png) no-repeat left top;}
#slidebox .thumbs{position:absolute; z-index:2; bottom:10px; right:10px;}
#slidebox .thumbs .thumb{display:block; margin-left:5px; float:left; font-family:Verdana, Geneva, sans-serif; font-size:9px; text-decoration:none; padding:2px 4px; background:url(slidebox_thumb.png); color:#fff;}
#slidebox .thumbs .thumb:hover{background:#fff; color:#000;}
#slidebox .selected_thumb{background:#fff; color:#000; display:block; margin-left:5px; float:left; font-family:Verdana, Geneva, sans-serif; font-size:9px; text-decoration:none; padding:2px 4px;}


</style>
  
    
    </head>

    <body>



</script>
<![endif]-->
<!-- main menu ends -->
<div id="content-section">
      <div id="content-inner">
      
      <div class="accounts-profile2 ">
      <div class="top-breadcrumb"> 
      <div class="accounts-profile2-left"> 
      <a href="companieshome.aspx" class="accounts-link">Company</a>&nbsp;&rsaquo;&rsaquo;&nbsp;<a href="#" class="accounts-link">Infosys</a>&nbsp;&rsaquo;&rsaquo;&nbsp;<strong>Blogs</strong></div>
      <div class="accounts-profile2-right">
      <!--&nbsp;&lsaquo;&lsaquo;&nbsp;<a href="#" class="accounts-link">Back to main menu</a>--></div>
      </div>
     
     <div id="bx10"><ul class="overview-tab">                
                <li><a id="a_overview" runat="server" >Overview</a></li>
                <li><a id="a_Activity" runat="server"  >Activity</a></li>
                <li><a id="a_Product" runat="server" >Products &amp; Services</a></li>
                <li><a id="a_careers" runat="server"  >Careers</a></li>
                <li><a id="a_business" runat="server" class="selected-tab">Business Blog</a></li>
                <li><a id="a_article" runat="server"  >Articles</a></li>
                
                <uc1:mesgpopup ID="mesgpopup" runat="server" />         
              </ul> </div>      
      </div>
      <div class="blog-head" style="margin-top: 50px;">
        	<h2>Infosys Blogs</h2>
            <p>Infosys' experts discuss the key drivers of business transformation and accelerating innovation.</p>
        </div>
        
        
      <div class="content-inner-left">
      	<div class="business-blog">
        
        
        <div class="business-blog-top">
        	<div id="slidebox">

<div class="thumbs">
<a href="#" onclick="Slidebox('1');return false" class="thumb">1</a> 
<a href="#" onclick="Slidebox('2');return false" class="thumb">2</a> 
<a href="#" onclick="Slidebox('3');return false" class="thumb">3</a>

</div>
	<div class="container">
    	<div class="content">
        	<div>
              <img src="images/infosys-banner.jpg" width="650" height="233" alt="infosys-banner" />
              </div>
        </div>
        <div class="content">
        	<div>
            <img src="images/infosys-banner2.jpg" width="650" height="233" alt="infosys-banner" />
            </div>
        </div>
        <div class="content">
        	<div>
            <img src="images/infosys-banner3.jpg" width="650" height="233" alt="infosys-banner" />
            </div>
        </div>
       
	</div>
</div>
          
          
          </div>
          
          
          <div class="tab-blog">
          <div class="tab-cv tab-cv-blog">
  	<a href="companyblogsrecent.aspx"  class="selectedcv">Recent Posts</a>
    <a href="company-blogs-popular.aspx">Most Popular</a>    
  </div>
  
  	      <div class="tab-blog-list">
    	<div class="tab-blog-list-inner">
        
        	<ul>
                <li>
                	<a href="#" class="heading-blog"><strong>Application Services</strong></a>
                </li>
            	<li>
                	<a href="#">Is consumerism in IT industry impacting the enterprise applications?</a>
                </li>
                <li>
                	<a href="#">Why invest in UX for your staff?</a>
                </li>
                <li>
                	<a href="#">Unique opportunities in changing government technology landscape</a>
                </li>
            </ul>
        </div>
        
        <div class="tab-blog-list-inner">
        
        	<ul>
                <li>
                	<a href="#" class="heading-blog"><strong>Application Services</strong></a>
                </li>
            	<li>
                	<a href="#">Is consumerism in IT industry impacting the enterprise applications?</a>
                </li>
                <li>
                	<a href="#">Why invest in UX for your staff?</a>
                </li>
                <li>
                	<a href="#">Unique opportunities in changing government technology landscape</a>
                </li>
            </ul>
        </div>
        
        <div class="tab-blog-list-inner">
        
        	<ul>
                <li>
                	<a href="#" class="heading-blog"><strong>Application Services</strong></a>
                </li>
            	<li>
                	<a href="#">Is consumerism in IT industry impacting the enterprise applications?</a>
                </li>
                <li>
                	<a href="#">Why invest in UX for your staff?</a>
                </li>
                <li>
                	<a href="#">Unique opportunities in changing government technology landscape</a>
                </li>
            </ul>
        </div>
        
        <div class="tab-blog-list-inner">
        
        	<ul>
                <li>
                	<a href="#" class="heading-blog"><strong>Application Services</strong></a>
                </li>
            	<li>
                	<a href="#">Is consumerism in IT industry impacting the enterprise applications?</a>
                </li>
                <li>
                	<a href="#">Why invest in UX for your staff?</a>
                </li>
                <li>
                	<a href="#">Unique opportunities in changing government technology landscape</a>
                </li>
            </ul>
        </div>
        
        <div class="tab-blog-list-inner">
        
        	<ul>
                <li>
                	<a href="#" class="heading-blog"><strong>Application Services</strong></a>
                </li>
            	<li>
                	<a href="#">Is consumerism in IT industry impacting the enterprise applications?</a>
                </li>
                <li>
                	<a href="#">Why invest in UX for your staff?</a>
                </li>
                <li>
                	<a href="#">Unique opportunities in changing government technology landscape</a>
                </li>
            </ul>
        </div>
        
        <div class="tab-blog-list-inner">
        
        	<ul>
                <li>
                	<a href="#" class="heading-blog"><strong>Application Services</strong></a>
                </li>
            	<li>
                	<a href="#">Is consumerism in IT industry impacting the enterprise applications?</a>
                </li>
                <li>
                	<a href="#">Why invest in UX for your staff?</a>
                </li>
                <li>
                	<a href="#">Unique opportunities in changing government technology landscape</a>
                </li>
            </ul>
        </div>
        
        <div class="tab-blog-list-inner">
        
        	<ul>
                <li>
                	<a href="#" class="heading-blog"><strong>Application Services</strong></a>
                </li>
            	<li>
                	<a href="#">Is consumerism in IT industry impacting the enterprise applications?</a>
                </li>
                <li>
                	<a href="#">Why invest in UX for your staff?</a>
                </li>
                <li>
                	<a href="#">Unique opportunities in changing government technology landscape</a>
                </li>
            </ul>
        </div>
        
        <div class="tab-blog-list-inner">
        
        	<ul>
                <li>
                	<a href="#" class="heading-blog"><strong>Application Services</strong></a>
                </li>
            	<li>
                	<a href="#">Is consumerism in IT industry impacting the enterprise applications?</a>
                </li>
                <li>
                	<a href="#">Why invest in UX for your staff?</a>
                </li>
                <li>
                	<a href="#">Unique opportunities in changing government technology landscape</a>
                </li>
            </ul>
        </div>
        
       
    </div>
          </div>
          
          
        </div>
        	
        </div>
        
      <div class="content-inner-right">
      	<div class="twitter-block">
      	<a href="#"><img src="images/infosys-twitter.gif" width="219" height="64" alt="Twitter" title="Twitter" /></a>
        </div>
        <div class="twitter-blogs">
        <h2 class="twit-hd">
       	<img src="images/icon-blog.gif" width="9" height="13" alt="Blogs" /> &nbsp;Blogs
        </h2>
        
        <div class="twitter-blogs-inner">
       	  <p class="twitter-blogs-inner-title"><a href="#" ><strong>Application Services</strong></a>
          </p>
            <a href="#"><img src="images/icon-blog.gif" width="9" height="13" alt="Discuss" />Discuss</a>&nbsp;&nbsp;|&nbsp;&nbsp;
            <a href="#"><img src="images/icon-feed.gif" width="10" height="10" alt="Rss" />Rss</a>&nbsp;&nbsp;|&nbsp;&nbsp;
            <a href="#">Atom</a>
        </div>
        
        <div class="twitter-blogs-inner">
       	  <p class="twitter-blogs-inner-title"><a href="#" ><strong>Application Services</strong></a>
          </p>
            <a href="#"><img src="images/icon-blog.gif" width="9" height="13" alt="Discuss" />Discuss</a>&nbsp;&nbsp;|&nbsp;&nbsp;
            <a href="#"><img src="images/icon-feed.gif" width="10" height="10" alt="Rss" />Rss</a>&nbsp;&nbsp;|&nbsp;&nbsp;
            <a href="#">Atom</a>
        </div>
        
        <div class="twitter-blogs-inner">
       	  <p class="twitter-blogs-inner-title"><a href="#" ><strong>Application Services</strong></a>
          </p>
            <a href="#"><img src="images/icon-blog.gif" width="9" height="13" alt="Discuss" />Discuss</a>&nbsp;&nbsp;|&nbsp;&nbsp;
            <a href="#"><img src="images/icon-feed.gif" width="10" height="10" alt="Rss" />Rss</a>&nbsp;&nbsp;|&nbsp;&nbsp;
            <a href="#">Atom</a>
        </div>
        
        <div class="twitter-blogs-inner">
       	  <p class="twitter-blogs-inner-title"><a href="#" ><strong>Application Services</strong></a>
          </p>
            <a href="#"><img src="images/icon-blog.gif" width="9" height="13" alt="Discuss" />Discuss</a>&nbsp;&nbsp;|&nbsp;&nbsp;
            <a href="#"><img src="images/icon-feed.gif" width="10" height="10" alt="Rss" />Rss</a>&nbsp;&nbsp;|&nbsp;&nbsp;
            <a href="#">Atom</a>
        </div>
        
        <div class="twitter-blogs-inner">
       	  <p class="twitter-blogs-inner-title"><a href="#" ><strong>Application Services</strong></a>
          </p>
            <a href="#"><img src="images/icon-blog.gif" width="9" height="13" alt="Discuss" />Discuss</a>&nbsp;&nbsp;|&nbsp;&nbsp;
            <a href="#"><img src="images/icon-feed.gif" width="10" height="10" alt="Rss" />Rss</a>&nbsp;&nbsp;|&nbsp;&nbsp;
            <a href="#">Atom</a>
        </div>
        
       
        
        <div class="rss-blogs">
        <h2 class="twit-hd">
       	<img src="images/how-to-icon.gif" width="13" height="13" alt="how-icon" /> &nbsp;How to subscribe to feeds
        </h2>
        <ul>
        	<li>
            	<a href="#">
                	What are RSS and Atom?
                </a>
            </li>
            
            <li>
            	<a href="#">
                	How can I read web feeds from Infosys blogs?
                </a>
            </li>
            
            <li>
            	<a href="#">
                	How can I add feeds to my feed reader?
                </a>
            </li>
        </ul>
        </div>
        
        </div>
      </div>
      	
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

    
      <script type="text/javascript" src="js/query.easing.1.3.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var autoPlayTime = 7000;
        autoPlayTimer = setInterval(autoPlay, autoPlayTime);
        function autoPlay() {
            Slidebox('next');
        }
        $('#slidebox .next').click(function () {
            Slidebox('next', 'stop');
        });
        $('#slidebox .previous').click(function () {
            Slidebox('previous', 'stop');
        });
        var yPosition = ($('#slidebox').height() - $('#slidebox .next').height()) / 2;
        $('#slidebox .next').css('top', yPosition);
        $('#slidebox .previous').css('top', yPosition);
        $('#slidebox .thumbs a:first-child').removeClass('thumb').addClass('selected_thumb');
        $("#slidebox .content").each(function (i) {
            slideboxTotalContent = i * $('#slidebox').width();
            $('#slidebox .container').css("width", slideboxTotalContent + $('#slidebox').width());
        });
    });

    function Slidebox(slideTo, autoPlay) {
        var animSpeed = 1000; //animation speed
        var easeType = 'easeInOutExpo'; //easing type
        var sliderWidth = $('#slidebox').width();
        var leftPosition = $('#slidebox .container').css("left").replace("px", "");
        if (!$("#slidebox .container").is(":animated")) {
            if (slideTo == 'next') { //next
                if (autoPlay == 'stop') {
                    clearInterval(autoPlayTimer);
                }
                if (leftPosition == -slideboxTotalContent) {
                    $('#slidebox .container').animate({ left: 0 }, animSpeed, easeType); //reset
                    $('#slidebox .thumbs a:first-child').removeClass('thumb').addClass('selected_thumb');
                    $('#slidebox .thumbs a:last-child').removeClass('selected_thumb').addClass('thumb');
                } else {
                    $('#slidebox .container').animate({ left: '-=' + sliderWidth }, animSpeed, easeType); //next
                    $('#slidebox .thumbs .selected_thumb').next().removeClass('thumb').addClass('selected_thumb');
                    $('#slidebox .thumbs .selected_thumb').prev().removeClass('selected_thumb').addClass('thumb');
                }
            } else if (slideTo == 'previous') { //previous
                if (autoPlay == 'stop') {
                    clearInterval(autoPlayTimer);
                }
                if (leftPosition == '0') {
                    $('#slidebox .container').animate({ left: '-' + slideboxTotalContent }, animSpeed, easeType); //reset
                    $('#slidebox .thumbs a:last-child').removeClass('thumb').addClass('selected_thumb');
                    $('#slidebox .thumbs a:first-child').removeClass('selected_thumb').addClass('thumb');
                } else {
                    $('#slidebox .container').animate({ left: '+=' + sliderWidth }, animSpeed, easeType); //previous
                    $('#slidebox .thumbs .selected_thumb').prev().removeClass('thumb').addClass('selected_thumb');
                    $('#slidebox .thumbs .selected_thumb').next().removeClass('selected_thumb').addClass('thumb');
                }
            } else {
                var slide2 = (slideTo - 1) * sliderWidth;
                if (leftPosition != -slide2) {
                    clearInterval(autoPlayTimer);
                    $('#slidebox .container').animate({ left: -slide2 }, animSpeed, easeType); //go to number
                    $('#slidebox .thumbs .selected_thumb').removeClass('selected_thumb').addClass('thumb');
                    var selThumb = $('#slidebox .thumbs a').eq((slideTo - 1));
                    selThumb.removeClass('thumb').addClass('selected_thumb');
                }
            }
        }
    }
</script>
  

    
  
</body>
</html>

</asp:Content>
