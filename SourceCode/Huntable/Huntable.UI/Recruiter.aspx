<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recruiter.aspx.cs" Inherits="Huntable.UI.Recruiter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"><head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/counter.css" />
   <!-- viewers viewed script style begins -->
    <link href="css/portfolio-js-style.css" type="text/css" rel="stylesheet" />    
    <!-- viewers viewed script style ends -->
    <link href="css/slider.css" type="text/css" rel="stylesheet" /> 

    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>

<!--   <script src='js/jquery-1.4.1.min.js' language="javascript" type="text/javascript"></script>-->
<script language="javascript" type="text/javascript" src="js/jquery.easing.js"></script>
<script language="javascript" type="text/javascript" src="js/script1.js"></script>
<script type="text/javascript">

//	<![CDATA[
    $(document).ready(function () {

        $obj = $('#lofslidecontent45').lofJSidernews({ interval: 8000,
            easing: 'easeInOutQuad',
            duration: 1200,
            auto: true,
            maxItemDisplay: 3,
            startItem: 1,
            navPosition: 'horizontal', // horizontal
            navigatorHeight: 11,
            navigatorWidth: 15,
            mainWidth: 979
        });
    });
		//]]>
</script>
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
<div class="banner-im" >
      	<!--<img src="images/banner-im1.jpg" />-->
        
        <div id="lofslidecontent45" class="lof-slidecontent" style="width:979px; height:441px;">
<div class="preload"><div></div></div>

  <div class="lof-main-outer" style="width:979px; height:441px;">
  	<ul class="lof-main-wapper">
  		<li>
        		<img src="images/banner-im1.jpg" title="Newsflash 2" >           
                 <div class="lof-main-item-desc">
                <h3>Best talent updated every second for you.</h3>
                <p>All your leads from any source in one place
                <a class="readmore" href="#">Learn more</a>
                </p>
                <span class="sign-up"> <a href="Company-registration.aspx?Id=Recuirter" 
                         style="margin-top:30px; background-image: url('images/Interests/signup-bg.png');">Sign up for free &gt;&gt;</a></span>
             </div>
        </li> 
       <li>
       	  <img src="images/banner-im2.jpg" title="Newsflash 1" >           
          	 <div class="lof-main-item-desc">
     <span class="sign-up"><a href="Company-registration.aspx?Id=Recuirter" style="margin-top:30px; background-image :url('images/Interests/signup-bg.png');">Sign up for free &gt;&gt;</a></span>
             </div>
        </li> 
       <li>
       	  <img src="images/banner-im3.jpg" title="Newsflash 3" >            
          	<div class="lof-main-item-desc">
               
                 <span class="sign-up"><a style="margin-top:30px; background-image: url('images/Interests/signup-bg.png');" href="Company-registration.aspx?Id=Recuirter">Sign up for free &gt;&gt;</a></span>
             </div>
        </li> 

      </ul>  	
  </div>
  
<div class="lof-navigator-wapper">
      <div class="lof-navigator-outer">
            <ul class="lof-navigator" style="width: 45px; left: 0px;">
            <li class ="active" style="height: 11px; width: 15px;"> </li>
            <li class ="" style="height: 11px; width: 15px;"></li>
            <li class ="" style="height: 11px; width: 15px;"></li>   
     </ul>
      </div>
 </div> 

 </div>
      </div>
<div id="content-section">
      <div id="content-inner" >
      
      <div class="s-banner">
      <ul class="slide-img">
      	<li style="margin-left:8px; padding-bottom: 5px; background: url(../images/shadow.png) bottom center no-repeat;" runat="server">
        	
        <div>
        <img src="images/s-banner-img1.jpg" />
        </div>
        </li>
        <li>
         <div>
         <img src="images/s-banner-img2.jpg" />
        </div>
        </li>
        <li>
         <div>
         <img src="images/s-banner-img3.jpg" />
        </div>
        </li>
        <li>
         <div>
         <img src="images/s-banner-img4.jpg" />
        </div>
        </li>    
      </ul>
      <div class="r-link1">
      Enterprise Class Recruiting Software- Absolutely FREE!!! Coming Soon. 
      <a href="Company-registration.aspx?Id=Recuirter" > Register now</a>
      <Asp:LinkButton runat="server" ID="Download" OnClick="DownloadTool" Text="Download" ></Asp:LinkButton>
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

   <!--viewers views gallery script begins -->

<script type="text/javascript" language="javascript" src="js/jquery.carouFredSel-6.0.3-packed.js"></script>

<!-- optionally include helper plugins -->
<script type="text/javascript" language="javascript" src="js/jquery.mousewheel.min.js"></script>
<script type="text/javascript" language="javascript" src="js/jquery.touchSwipe.min.js"></script>

<!-- fire plugin onDocumentReady -->

<!--<script type="text/javascript" language="javascript">
			$(function() {

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
					width:280,
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
		</script>-->
        
<!--viewers views gallery script ends -->   

    
    
  
</body>
</html>

    </div>

</asp:Content>
