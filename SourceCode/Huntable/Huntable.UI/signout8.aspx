<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="signout8.aspx.cs" Inherits="Huntable.UI.signout8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<!-- Header section ends -->

<div id="content-section">
      <div id="content-inner" >
      <div class="fullwidth">
      
      <div class="sign-out-indicator"><div class="sign-out-message">You're Signed Out</div></div></div>
      
 <div class="fullwidth">
 <div class="sign-out-left8">
   <p class="signout8-font-size1">Super power your profile - add photos, videos and achievements</p>
    <p class="signout8-font-size2">Did you know</p>

<p class="signout8-font-size3">You can add photos, videos and achievements<br /> to each of your jobs/business?</p>
<p class="signout8-font-size3">Add them to your profile when you edit</p>
<p class="signout8-font-size3">Show your true potential</p>
<p class="signout8-font-size3">What you have achieved and what you can to the world</p>

</div>
 <div class="sign-out-right8"><img alt="Super power your profile " title="Super power your profile " width="420" src="images/signout8.jpg" /></div>
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

    
    
  


</asp:Content>
