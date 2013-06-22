<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="signout3.aspx.cs" Inherits="Huntable.UI.signout3" %>
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

    </head>

    <body>

<!-- Header section ends -->

<div id="content-section">
      <div id="content-inner" >
      <div class="fullwidth">
      
      <div class="sign-out-indicator"><div class="sign-out-message">You're Signed Out</div></div></div>
      
 <div class="fullwidth">
 
 <div class="sign-out-left sign-out-left2"><img alt="Step upto your new and innovative profile" title="Step upto your new and innovative profile" width="440" src="images/signout3.jpg" /></div>
   
   <div class=" sign-out-right31">
  <p class="signout3-font-size1">Step up to your new and innovative profile</p>
  <p class="signout3-font-size2">Visual Profile<br />


Traditional profile<br />
Activity Profile</p>

<p class="signout3-font-size3">Your growth ladder is waiting for you<br />
All you have to do, is just step up</p></div>
               
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

</asp:Content>
