<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyOverview.aspx.cs" Inherits="Huntable.UI.CompanyOverview" %>
<%@ Register Src="~/UserControls/Messagepopup.ascx" TagName="mesgpopup"  TagPrefix="uc1"%>
<%@ Register Src="~/UserControls/Followers(6).ascx" TagName="followers" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/CompaniesYouMayBeIntrested.ascx" TagName="compintrested" TagPrefix="uc3" %>

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
     <!-- viewers viewed script style begins -->
    <link href="css/portfolio-js-style.css" type="text/css" rel="stylesheet" />    
    <!-- viewers viewed script style ends -->
    <script type="text/javascript">
        function rowAction01() {
            if (document.getElementById('<%=hdnUserId.ClientID %>').value != "") {

            }            
        }
    </script>
    </head>

    <body>
    <asp:HiddenField ID="hdnUserId" runat="server" />
<div id="content-section">
      <div id="content-inner">
      
      <div class="accounts-profile2 ">
       <div class="top-breadcrumb">
      <div class="accounts-profile2-left"> 
      <a href="companieshome.aspx" class="accounts-link">Company</a>&nbsp;&rsaquo;&rsaquo;&nbsp;<a href="#" class="accounts-link">Infosys</a>&nbsp;&rsaquo;&rsaquo;&nbsp;<strong>Activity</strong></div>
      <a href="Company-registration.aspx" class="button-yellow top-minus" >Add Your Business Page</a>
      </div>
      <ul class="overview-tab">                
               <li><a id="overview" runat="server" class="selected-tab" >Overview</a></li>
                <li><a id="activity" runat="server" >Activity</a></li>
                <li><a id="productsandservices" runat="server" >Products &amp; Services</a></li>
                <li><a id="careers" runat="server" >Careers</a></li>
                <li><a id="busunessblog" runat="server" >Business Blog</a></li>
                <li><a id="article" runat="server">Article</a></li>         
            <uc1:mesgpopup ID="mesgbox" runat="server" />              
              </ul>      
      </div>
      
      <div class="content-inner-left">
          
          <div class="notification">
        
              
            
              
              <div class="all-feeds-main-inner all-feeds-main-inner-company">
            
          
                   <div id="tabswitch tabswitch1">
                    
                     <div class="tab-container tab1" style="display: block; padding-left:0px; width:654px;">
                  
                    <div class="all-feeds-list">
                        	
                         <div class="general-setting-tab1 general-setting-tab1-overview">
            	<a class="setting-active1" href="#">Business Activity</a>
                <a href="#">Employee Activity</a>
            </div>
                           
                
                    </div>
                    
                    <div class="all-feeds-list">
                        	<div class="feed-left"><a href="#"><img width="76" height="81" alt="feeds-img" src="images/profile-thumb-large.jpg" class="profile-pic" /></a></div>
                          
                           <div class="feed-right feed-right-inner"> 
                           <a href="#">Kelly Retica</a>added a video to his <a href="#">Portfolio</a>
                           <div class="video-comment">
                           	<div class="video-post">
                       	     <img src="images/video-thumb.jpg" width="105" height="60" alt="video-thumb" />
                             
                             </div>
                            <div class="video-desc">
                            <a href="#" class="accounts-link"> Hey Jude Olympic (Spontaneous 27.07.12) @ Liverpool st Station LONDON</a><br />
                            <a href="#" class="live-link">www.youtube.com</a>
                            <p>
                            After olympic opening ceremony
                             After olympic opening ceremony
                              After olympic opening ceremony
                               After olympic opening ceremony
                                After olympic opening ceremony
                                 After olympic opening ceremony
                                  After olympic opening ceremony
                            </p>
                            </div>
                           </div>
                           <div class="like-portion">
                           
                           <a style="margin-left:0px;" href="#"><img width="13" height="12" alt="Like" src="images/icon-like.png" />Like</a> <a href="#"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a> <span>5 Minutes ago</span>
                           <div class="comments01">
                           	
                            
                             <div class="comments-desc">
                             <div class="comments-desc-left">
                             	<a href="#">
                               	<img width="46" height="45" alt="img" src="images/profile-thumb-small.jpg"  />
                                 </a>
                             </div>
                             <div class="comments-desc-right">
                             	 <textarea class="textarea-profile textarea-comment" onfocus="if(this.value==this.defaultValue)this.value='';" onblur="if(this.value=='')this.value=this.defaultValue;">Write a comment...</textarea> <br /><br />
                             </div>                             	
                             </div>
                           </div>
                           </div>
                          </div>
                      
                       
                
                    </div>
                    
                    <div class="all-feeds-list">
                        	<div class="feed-left"><a href="#"><img width="76" height="81" alt="feeds-img" src="images/profile-thumb-large.jpg" class="profile-pic" /></a></div>
                          
                           <div class="feed-right feed-right-inner"> 
                           <a href="#">Kelly Retica</a>Shared a<a href="#"> link</a>
                           <div class="user-posted-link">
                           <a href="#" class="accounts-link">www.tesco.com</a>,<a href="#" class="accounts-link">www.tesco.com</a>,<a href="#" class="accounts-link">www.tesco.com</a>
                           </div>
                           <div class="video-comment">
                           	
                            <div class="video-desc video-desc-wp">
                            <a href="#" class="accounts-link"> Hey Jude Olympic (Spontaneous 27.07.12) @ Liverpool st Station LONDON</a><br />
                            <a href="#" class="live-link">www.youtube.com</a>
                            <p>
                            After olympic opening ceremony
                             After olympic opening ceremony
                              After olympic opening ceremony
                               After olympic opening ceremony
                                After olympic opening ceremony
                                 After olympic opening ceremony
                                  After olympic opening ceremony
                            </p>
                            </div>
                           </div>
                           <div class="like-portion">
                           
                           <a style="margin-left:0px;" href="#"><img width="13" height="12" alt="Like" src="images/icon-like.png" />Like</a> <a href="#"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a> <span>5 Minutes ago</span>
                           <div class="comments01">
                           	
                            
                             <div class="comments-desc">
                             <div class="comments-desc-left">
                             	<a href="#">
                               	<img width="46" height="45" alt="img" src="images/profile-thumb-small.jpg"  />
                                 </a>
                             </div>
                             <div class="comments-desc-right">
                             	 <textarea class="textarea-profile textarea-comment" onfocus="if(this.value==this.defaultValue)this.value='';" onblur="if(this.value=='')this.value=this.defaultValue;">Write a comment...</textarea> <br /><br />
                             </div>                             	
                             </div>
                           </div>
                           </div>
                          </div>
                      
                       
                
                    </div>
                    
                    <div class="all-feeds-list">
                        	<div class="feed-left"><a href="#"><img width="76" height="81" alt="feeds-img" src="images/profile-thumb-large.jpg" class="profile-pic" /></a></div>
                          
                           <div class="feed-right feed-right-inner"> 
                           <a href="#">Kelly Retica</a>Shared a<a href="#"> link</a>
                           <div class="comments-desc">
                             <div class="comments-desc-left">
                             	<a href="#">
                               	<img width="46" height="45" alt="img" src="images/profile-thumb-small.jpg"  />
                                 </a>
                             </div>
                             <div class="comments-desc-right">
                             	<a href="#">Alex Groose</a>	
                                <p>Lorem Lipsum</p>
                                <span><strong>8 hours ago</strong> <a href="#">Like</a></span>
                             </div>                             	
                             </div>
                          
                           <div class="like-portion">
                           
                           <a style="margin-left:0px;" href="#"><img width="13" height="12" alt="Like" src="images/icon-like.png" />Like</a> <a href="#"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a><span>5 Minutes ago</span>
                           <div class="comments01">
                           	
                            
                             <div class="comments-desc">
                             <div class="comments-desc-left">
                             	<a href="#">
                               	<img width="46" height="45" alt="img" src="images/profile-thumb-small.jpg"  />
                                 </a>
                             </div>
                             <div class="comments-desc-right">
                             	 <textarea class="textarea-profile textarea-comment" onfocus="if(this.value==this.defaultValue)this.value='';" onblur="if(this.value=='')this.value=this.defaultValue;">Write a comment...</textarea> <br /><br />
                             </div>                             	
                             </div>
                           </div>
                           </div>
                          </div>
                      
                       
                
                    </div>                    
                    
                    <div class="all-feeds-list">
                        	<div class="feed-left"><a href="#"><img width="76" height="81" alt="feeds-img" src="images/profile-thumb-large.jpg" class="profile-pic" /></a></div>
                          
                           <div class="feed-right feed-right-inner"> 
                           <a href="#">Kelly Retica</a>Shared a<a href="#"> link</a>
                           
                          
                           <div class="like-portion">
                           
                           <a style="margin-left:0px;" href="#"><img width="13" height="12" alt="Like" src="images/icon-like.png" />Like</a> <a href="#"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a> <span>5 Minutes ago</span>
                           <div class="comments01">
                           	
                            
                             <div class="comments-desc">
                             <div class="comments-desc-left">
                             	<a href="#">
                               	<img width="46" height="45" alt="img" src="images/profile-thumb-small.jpg"  />
                                 </a>
                             </div>
                             <div class="comments-desc-right">
                             	 <textarea class="textarea-profile textarea-comment" onfocus="if(this.value==this.defaultValue)this.value='';" onblur="if(this.value=='')this.value=this.defaultValue;">Write a comment...</textarea> <br /><br />
                             </div>                             	
                             </div>
                           </div>
                           </div>
                          </div>
                      
                       
                
                    </div>
                    
                    <div class="all-feeds-list">
                        	<div class="feed-left"><a href="#"><img width="76" height="81" alt="feeds-img" src="images/profile-thumb-large.jpg" class="profile-pic" /></a></div>
                          
                           <div class="feed-right feed-right-inner "> 
                           <a href="#">Kelly Retica</a>Shared a<a href="#"> link</a>
                           
                          <table class="user-follow">
            <tbody><tr>
              <td width="18%"  valign="top"><a href="#">
              <img src="images/profile-thumb-large.jpg" alt="Feaured-logo" class="profile-pic profile-pic2" /></a></td>
              <td width="82%" align="left"  valign="top"><p style="line-height:18px;"> <a href="#" class="accounts-link">Ruben Daniel</a><br />
                  Loyola College of Engineering<br />
                  <strong>Chennai (Madras)</strong><br />
                  <a href="#" class="invite-friend-btn invite-friend-btn-ov">Follow</a> </p></td>
            </tr>
          </tbody></table>
                           <div class="like-portion">
                           
                           <a style="margin-left:0px;" href="#"><img width="13" height="12" alt="Like" src="images/icon-like.png" />Like</a> <a href="#"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a> <span>5 Minutes ago</span>
                           
                           </div>
                          </div>
                      
                       
                
                    </div>
                    
                    <a class="show-more show-more-margin" href="#">Show More</a>
                      
                    </div>
                  
            </div>  
            </div>
              
             
              
           
      </div>
        </div>
    <div class="content-inner-right">
    
 
          <div class="blue-box-company1">
          	<a href="#" class="button-orange button-orange-fc" >Follow</a><br /><br /><br />
            Infosys and get upto date info
about this company and get their jobs
straight into your feeds
            <div class="button-green-company" align="center">

</div>
          </div>           
              
          <div class="box-right box-right02">
        <uc2:followers runat="server" />
              </div>          
        <uc3:compintrested runat="server" />
              
              
              
                         
           <p class="margin-top-visible">&nbsp;</p>
           
           <div class="box-right" >
                              
                  <div class="head-ash">
               	  <h3>Viewers also Viewed</h3>
                  </div>
                  
                 
                 
                 <div class="list_carousel">
				<ul id="foo2">
				<li style="width:280px; height:172px;" class="img-left">
               	  <a href="#"><img src="images/featured-logo1.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo1.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo1.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo1.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo1.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo1.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
                 
                 </li>
  				<li style="width:280px; height:172px;" class="img-left">
               	  <a href="#"><img src="images/featured-logo2.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo2.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo2.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo2.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo2.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo2.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
                 
                 </li>
				<li style="width:280px; height:172px;" class="img-left">
               	  <a href="#"><img src="images/featured-logo1.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo1.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo1.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo2.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo2.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo2.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
                 
                 </li>
               
  				<li style="width:280px; height:172px;" class="img-left">
               	  <a href="#"><img src="images/featured-logo1.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo2.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo1.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo2.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo1.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
               	  <a href="#"><img src="images/featured-logo2.jpg" class="profile-pic" width="124" height="70" alt="Feaured-logo" /></a>
                 
                 </li>
               
                 
                 
                </ul>
				<div class="clearfix"></div>
                <div class="pager-main">
                <a id="prev2" class="prev" href="#"><img src="images/prev.png" width="17" height="22" alt="previous" /></a>
				
                <div class="pager" id="pager2" style="display: block;">
                &nbsp;
                </div>
                <a id="next2" class="next" href="#"><div style="margin-left:255px; margin-top:-20px;"><img src="images/next.png" width="17" height="22" alt="Next" /></div></a>
				</div>
	    </div>                
</div>           
           <p class="margin-top-visible">&nbsp;</p>
           
            <div>
        <h2 align="center" style="font-size:14px; margin-bottom:10px; font-weight:bold;">Infosys employees <br />
you may want to follow</h2>
        </div>
    
    <div class="box-right">
    
    <div class="want-to-follow-list" >
    <div class="want-to-follow-list-left" >
    	<a href="#"><img alt="Invite-friends-img" class="profile-pic profile-pic2" src="images/profile-thumb-small.jpg" /></a>
    </div>  
    <div class="want-to-follow-list-middle"> 
    	<strong><a href="#" class="accounts-link">Frida Dahlstorm</a></strong>
     	<p>Trainee at Snovaspace pvt Ltd</p>
    </div>  
        <div class="want-to-follow-list-right">
    	<a class="invite-friend-btn" href="#">Follow +</a>
    </div>
    </div>
    
    <div class="want-to-follow-list" >
    <div class="want-to-follow-list-left" >
    	<img alt="Invite-friends-img" class="profile-pic profile-pic2" src="images/profile-thumb-small.jpg" />
    </div>  
    <div class="want-to-follow-list-middle"> 
    	<strong><a href="#" class="accounts-link">Frida Dahlstorm</a></strong>
     	<p>Trainee at Snovaspace pvt Ltd</p>
    </div>    
    
        <div class="want-to-follow-list-right">
    	<a class="invite-friend-btn" href="#">Follow +</a>
    </div>
    </div>
    
    <div class="want-to-follow-list" style="border-bottom: none;" >
    <div class="want-to-follow-list-left" >
    	<a href="#"><img alt="Invite-friends-img" class="profile-pic profile-pic2" src="images/profile-thumb-small.jpg" /></a>
    </div>  
    <div class="want-to-follow-list-middle"> 
    	<strong><a href="#" class="accounts-link">Frida Dahlstorm</a></strong>
     	<p>Trainee at Snovaspace pvt Ltd</p>
    </div>    
    
        <div class="want-to-follow-list-right">
    	<a class="invite-friend-btn" href="#">Follow +</a>
    </div>
    </div>
   
    <a class="learn-more" href="#">See More</a>
    </div>          
             </div>
  </div>

     <!--viewers views gallery script begins -->

<script type="text/javascript" language="javascript" src="js/jquery.carouFredSel-6.0.3-packed.js"></script>

<!-- optionally include helper plugins -->
<script type="text/javascript" language="javascript" src="js/jquery.mousewheel.min.js"></script>
<script type="text/javascript" language="javascript" src="js/jquery.touchSwipe.min.js"></script>

<!-- fire plugin onDocumentReady -->

<script type="text/javascript" language="javascript">
    $(function () {

        //	Basic carousel, no options
        $('#foo0').carouFredSel();

        //	Basic carousel + timer
        $('#foo1').carouFredSel({
            auto: {
                pauseOnHover: 'resume',
                progress: '#timer1'
            },
            prev: '#prev1',
            next: '#next1'
        });

        //	Scrolled by user interaction
        $('#foo2').carouFredSel({
            auto: false,
            prev: '#prev2',
            next: '#next2',
            pagination: "#pager4",
            auto: true,
            mousewheel: false,
            scroll: 2,
            circular: false,
            infinite: false
        });

        //	Variable number of visible items with variable sizes
        $('#foo3').carouFredSel({
            width: 280,
            height: 'auto',
            prev: '#prev3',
            next: '#next3',
            auto: true,
            circular: false,
            infinite: false
        });

        //	Responsive layout, resizing the items
        $('#foo4').carouFredSel({
            responsive: true,
            width: '100%',
            scroll: 2,
            items: {
                width: 400,
                //	height: '30%',	//	optionally resize item-height
                visible: {
                    min: 2,
                    max: 6
                }
            }
        });

        //	Fuild layout, centering the items
        $('#foo5').carouFredSel({
            width: '100%',
            scroll: 2
        });

    });
</script>




    
    
  
</body>
</html>

    </div>

</asp:Content>
