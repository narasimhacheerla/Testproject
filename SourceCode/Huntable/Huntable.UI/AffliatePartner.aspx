<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AffliatePartner.aspx.cs" Inherits="Huntable.UI.AffliatePartner" %>
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
     function overlay(id) {
         el = document.getElementById('ovrly');
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
</head>

<body>

<!-- Header section ends -->

<!-- main menu ends -->
        <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>
<div id="content-section">
    	<div class="affiliate_partner">
        <h1>Affiliate Partner</h1>
        <div class="profile-box-main affiliate_partner_box">
       	<div class="left-content">
        	<div class="social-icons">
            	 <a href="InviteFriends.aspx"><img src="~/images/h.png" runat="server" width="48" height="48" alt="h" /></a>
            	<a href="InviteFriends.aspx"><img src="~/images/fb.png" runat="server" width="48" height="48" alt="fb" /></a>
                <a href="InviteFriends.aspx"><img src="~/images/tw.png" runat="server" width="48" height="48" alt="tw" /></a>
                <a href="InviteFriends.aspx"><img src="~/images/in.png" runat="server" width="48" height="45" alt="in" /></a>
                <a href="InviteFriends.aspx"><img src="~/images/gg.png" runat="server" width="48" height="45" alt="google" /></a>
                <a href="InviteFriends.aspx"><img src="~/images/p.png" runat="server" width="45" height="45" alt="p" /></a>
               
                
  </div>
            <div  class="benefits">
            	<h3>Benefits of Affliate Partner</h3>
                <p>The affiliate programme provides a way to earn you a comission on sales or conversion you make.</p>
                <br/>
              <p>
Conversion - When your friend or anyone you invited joins Huntable as a Free member<br/>
Sales - When a free user upgrades to a premium memeber in Huntable
                </p>
                <br/><br/>
    <h4>Become a affliate partner with Huntable today and enjoy these benefits.</h4>
                <ul>
                	<li><img src="~/images/benfilt-1-icon.png" runat="server" width="27" height="28" alt="ben1" />You promote Huntable, for every sale we get from you, we will pay you a comission. Its that simple.</li>
                    <li><img src="~/images/benfilt-2-icon.png" runat="server" width="27" height="28" alt="ben-2" />Earn $6 per person when they join as premium member</li>
                    <li><img src="~/images/benfilt-3-icon.png" runat="server" width="27" height="28" alt="ben-3" />Use you email, Facebook and other social contacts to invite</li>
                </ul>
 <br/><br/>
                <h3 >Contact us to find out more !!!</h3>
               
                <p style="font-size:12px;">Note: All your friends or connections should register through your contact.</p>
                
            </div>
        </div>
        <div class="right-login">
        	<div class="partner-form">
        	<h3>Contact us to become a partner</h3>
            <form action="#">
            	<ul>
                	<li>
                    	<div class="label">
                        <label>Name</label>
                        </div>
                        <div class="text-box">
                            <asp:TextBox runat="server" ID="Txtnamme"  class="textbox txtnamme textbox-join" value="" onfocus="if (this.value =='') {this.value ='';}" onblur="if (this.value == '') {this.value ='';}" name="email"/>
                        </div>
                    </li>
                    <li>
                    	<div class="label lableaff">
                        <label>Email</label>
                        </div>
                        <div class="text-box">
                            <asp:TextBox type="text" ID="Txtemail" class="textbox txtnamme textbox-join" value="" onfocus="if (this.value =='') {this.value ='';}" onblur="if (this.value == '') {this.value ='';}" name="email" runat="server"/>
                        </div>
                    </li>
                    <li>
                    	<div class="label lableaff">
                        <label >Message</label>
                        </div>
                        <div class="text-box">
                        <asp:TextBox class="textbox textbox-join" ID="txtmsg" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </li>
                    <li>
                    	<div class="label">
                        	<label>&nbsp;</label>
                        </div>
                        <div class="text-box" style="float:right;margin-top: 12px;">
                        	<asp:Button  Text="Send" name="send" OnClick="SendMailonclick" class="button-orange button-orange-search"  runat="server"/>
                        </div>
                    </li>
                </ul>
            </form>
            </div>
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


        
<!--viewers views gallery script ends -->   <!-- Footer section ends -->


<script src="js/jtip.js" type="text/javascript"></script>
</body>
</html>

</asp:Content>
