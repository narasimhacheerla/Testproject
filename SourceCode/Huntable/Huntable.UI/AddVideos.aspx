<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddVideos.aspx.cs" Inherits="Huntable.UI.AddVideos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<html>
<head>
<link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />

<link rel="stylesheet" type="text/css" href="css/skin2.css" />
<link rel="stylesheet" type="text/css" href="css/skin3.css" />

<link rel="stylesheet" type="text/css" href="css/jquery.fancybox-picture.css?v=2.1.2" media="screen" />
    <style type="text/css">



	.jcarousel-skin-tango .jcarousel-container-horizontal{ width:750px;}
	.jcarousel-skin-tango .jcarousel-clip-horizontal{ width:580px; height:195px;}
	.jcarousel-skin-tango .jcarousel-item-horizontal{ margin-right:19px;}
	.jcarousel-skin-tango .jcarousel-item{ width:136px; height:165px;}
	.jcarousel-skin-tango .jcarousel-clip-horizontal{ width:747px;}
	.jcarousel-skin-tango .jcarousel-prev-horizontal,.jcarousel-skin-tango .jcarousel-next-horizontal{ top:75px;}
	
	
	.fancybox-custom .fancybox-skin {
			box-shadow: 0 0 50px #222;
		}
			

</style>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js" type="text/javascript"></script>
<script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
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
<script type="text/javascript" src="js/flipcounter.js"></script>
</head>
<body>
  <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>

    <div style="margin-left:200px;margin-top: 46px;">
 
   
 
   <label > Add Title  for your Video</label><asp:TextBox ID="txd" runat="server"></asp:TextBox>
    <asp:TextBox ID="tx" runat="server"></asp:TextBox>
    <asp:Button CausesValidation="True" ID="btn" OnClick="Addpic" runat="server" Text="ADD VIDEO" /><br />
  Share link to a You Tube, Vimeo, Dailmotion or Google video about you
     
      <div class=" jcarousel-skin-tango">
            <div class="jcarousel-container jcarousel-container-horizontal" style="position: relative;
                display: block;">
                <div class="jcarousel-clip jcarousel-clip-horizontal" style="position: relative;">
                    <ul class="first-and-second-carousel jcarousel-list jcarousel-list-horizontal" id="first-carousel"
                        style="overflow: hidden; position: relative; top: 0px; margin: 0px; padding: 0px;
                        left: 0px; width: 1300px;height:164px;">
  <asp:Repeater ID="rpic" runat="server">
  <ItemTemplate> 
  <li class="jcarousel-item jcarousel-item-horizontal jcarousel-item-1 jcarousel-item-1-horizontal"
                            style="float: left; list-style: none outside none;" jcarouselindex="1">
  <iframe ID="Image1" runat="server" src ='<%#Container.DataItem%>' width="150px" height="160px"></iframe>
  </li>
  </ItemTemplate>
  </asp:Repeater>
  </ul>
  </div>
   <div class="user-regis-left-ut" style="margin-top: 0px;">
                </div>
                <div class="jcarousel-prev jcarousel-prev-horizontal jcarousel-prev-disabled jcarousel-prev-disabled-horizontal"
                    style="display: block;" disabled="disabled">
                </div>
                <div class="jcarousel-next jcarousel-next-horizontal" style="display: block;">
                </div>
 </div>

  </div>
  <br />
  <br />
  <br />
  <div><asp:Button ID="upb" CausesValidation="False" runat="server" class="button-orange picsty" Text="Upload now" OnClick="Uplot"  />
   <asp:Button ID="cncl" CausesValidation="False" runat="server" class= "button-orange picsty1" Text="Cancel" OnClick="cancel" />  </div>            
                   
      </div>  
      </body>       
 </html> 
 <script type="text/javascript" src="js/jquery.jcarousel.min.js"></script>
<script type="text/javascript">

    jQuery(document).ready(function () {
        // Initialise the first and second carousel by class selector.
        // Note that they use both the same configuration options (none in this case).
        jQuery('.first-and-second-carousel').jcarousel();

        // If you want to use a caoursel with different configuration options,
        // you have to initialise it seperately.
        // We do it by an id selector here.
        jQuery('#third-carousel').jcarousel({
            vertical: true
        });
    });

</script> 
</asp:Content>
