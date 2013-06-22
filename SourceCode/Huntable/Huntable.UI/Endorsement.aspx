<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Endorsement.aspx.cs" Inherits="Huntable.UI.Endorsement" %>
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
<link rel="stylesheet" type="text/css" href="css/counter.css" />
<%-- ReSharper disable UnusedLocals --%>
  <script type="text/javascript">
      function overlay(id) {
          var el = document.getElementById('ovrly');
          $('#ovrly').show();

          $('#<%= pbl.ClientID %>').text(id);

      }
      $(document).ready(function () {

          $('#ximg').click(function () {
              $('#ovrly').hide();
              return false;
          });
      });
    </script>
<%-- ReSharper restore UnusedLocals --%>


</head>

<body>

<!-- Header section ends -->
<!--[if lte IE 6]>
<script type="text/javascript" >
//author: ?, I found this useful.
startList = function() {
if (document.all&&document.getElementById) {
    var navRoot = document.getElementById("nav");
    for (var i = 0; i<navRoot.childNodes.length; i++) {
        var node = navRoot.childNodes[i];
        if (node.nodeName=="LI") {
node.onmouseover=function() {
this.className+=" over";
  };
            node.onmouseout=function() {
  this.className=this.className.replace(" over", "");
   };
        }
    }
}
};
window.onload=startList;

</script>
<![endif]-->
<!-- main menu ends -->
<div id="content-section">
    <div   id="ovrly"  style="height:36px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0 10px;margin: 0 auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-25px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 692px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px" /></div></div> </div>
    	<div class="email_contacts email_contacts_invite">
       
      
        <h1 class="endo_title">Ask for endorsements from your Boss, Managers, collegues &amp; Friends</h1>
        <div class="profile-box-main">
        <h2 class="endosub_title">Who do you want to ask for endorsement?</h2>
        <div class="comments endo_field">
       <asp:TextBox ID="emailtextbox" class="textbox textbox-company" value="Enter the email addresses (sepreated by comma)" onfocus="if (this.value =='Enter the email addresses (sepreated by comma)') {this.value ='';}" onblur="if (this.value == '') {this.value ='Enter the email addresses (sepreated by comma)';}" name="email" style="width:605px;" runat="server"></asp:TextBox></div>
        <div class="comments endo_field">
        <asp:DropDownList ID = "ddl" class="textbox textbox-reg"   style="width:615px;" runat="server" AppendDataBoundItems="True">
            <asp:ListItem Text="Choose" Value ="-1"></asp:ListItem>
      
        </asp:DropDownList></div>
         <div class="comments endo_field" style="background:none; border:none;">
        <asp:TextBox TextMode="MultiLine" ID="EndTextbox" cols="5" rows="9"  class="textarea-profile textarea-comment"  style="width:605px; color:#909090;" runat="server">


        </asp:TextBox>
        </div>
        <div style="clear:both; text-align:center;  width: 605px;">
        <asp:Button  Text="Send" class="button-orange" OnClick="Sendendorsementmessage" runat="server"></asp:Button>
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
        
<!--viewers views gallery script ends -->   <!-- Footer section ends -->


<script src="js/jtip.js" type="text/javascript"></script>
<script type="text/javascript" src="js/flipcounter.js"></script>
</body>
</html>

</asp:Content>
