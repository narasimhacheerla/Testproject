<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Article.aspx.cs" Inherits="Huntable.UI.Article" %>
<%@ Register Src="~/UserControls/Followers.ascx" TagName="flw" TagPrefix="uc" %> 
<%@ Register Src="~/UserControls/Messagepopup.ascx" TagName="mesgpopup" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="GMaps, Version=4.0.0.7, Culture=neutral, PublicKeyToken=258385c8a4e17a2d" %>
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
    $().ready(function () {
        $('#dialogContent').dialog({
            autoOpen: false,
            modal: true,
            bgiframe: true,
            title: "Send Message",
            width: 600,
            height: 380
        });
        $('#dialogContent').parent().appendTo($("form:first"));
    });
    function rowAction01() {
        if (document.getElementById('<%=hdnUserId.ClientID %>').value != "") {

        }
        else {
            $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
            $('#dialog').dialog('open');
            return false;

        }
    }
</script>
  <script type="text/javascript">
      function overlay(id) {
          el = document.getElementById('ovrly');
          $('#ovrly').show();

          $('#<%= pbl.ClientID %>').text(id);

      }
      $(document).ready(function () {

          $('#ximg').click(function () {
              $('#ovrly').hide();
              window.location.reload();
              return false;
          });
      });
    </script>
<script type="text/javascript" src="../js1/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js1/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript">
        $(function () {
            // Dialog
            $('#dialog').dialog({
                autoOpen: false,
                modal: true,
                width: 600,
                buttons: {
                    "Ok": function () {
                        $(this).dialog("close");
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                }
            });
        });
    </script>

</head>

<body>
 <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>
<div id="content-section">
<asp:HiddenField ID="hdnUserId" runat="server" />
 <div id="dialog" title="Confirm Message">
    <asp:Label runat="server" Text="You are not logged In.Please login first." ID="lblConfirmMessage"></asp:Label>
    </div>


  <div id="content-inner">
  <div class="accounts-profile2 ">
  <div class="top-breadcrumb">
      <asp:DataList ID ="dl3" runat="server" OnItemDataBound="Itembund" ><ItemTemplate><div class="accounts-profile2-left"> 
      <a class="accounts-link" href="companieshome.aspx">Company</a>&nbsp;››&nbsp;<a class="accounts-link" runat="server" id="art" href="#"><asp:Label runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label></a>&nbsp;››&nbsp;<strong>Article</strong></div></ItemTemplate></asp:DataList>
      <a href="Company-registration.aspx" class="button-yellow top-minus" onclick="return rowAction01();" style="margin-top:-18px;">Add Your Business Page</a>
      
      
      </div>
     <ul class="overview-tab">                
                <li><a id="overview" runat="server" >Overview</a></li>
                <li><a id="activity" runat="server" >Activity</a></li>
                <li><a id="productsandservices" runat="server" >Products &amp; Services</a></li>
                <li><a id="careers" runat="server" >Careers</a></li>
                <li><a id="busunessblog" runat="server" >Business Blog</a></li>
                <li><a id="article" runat="server" class="selected-tab">Article</a></li>                
               <%-- <a style="margin:8px 10px 0px 0px; padding:6px 12px; float:right;" class="button-orange" href="#"> Message </a> --%>
               <uc1:mesgpopup ID="mesgpopup" runat="server" />          
              </ul> 
  </div>
      
    <div class="content-inner-left" style="width:755px;">
      <div class="notification">
      
        <div class="notification-left-main">
         <div class="job-posted-logo"> <a href="#">
          
          <asp:Image ID="img" runat="server"  style="width: 204px;height:123px;" />
          </a> </div>
          <div id="divLikeProfile" runat="server">
                                <img src="images/like-icon.png" width="16" height="13" alt="like" />&nbsp;&nbsp;
                                <a href="#" class="accounts-link" runat="server" id="hypLikeProfileCount"><span id="lblLikeProfileCount" runat="server">20</span> People</a> 
                                <a href="#" class="accounts-link" runat="server" id="hypLikeProfile">like this company</a>
                            </div>
          <div class="cn-blk">
          <p>
            	<asp:Label ID="Label4" runat="server" ></asp:Label><br />
                <a class="accounts-link" id="a_comp" runat="server" href="#" target="_blank">
                <a runat="server" id="a_web" target="_blank" >
                <asp:Label ID="lbl" runat="server" ></asp:Label>
               </a>
               </a><br />
                <asp:Label runat="server"  ID="lbl_ph"></asp:Label>
            </p>
          <p>
            	<a class="accounts-link" href="#"></a><br />
               
                <asp:Label runat="server" ID="lbl_add1" >
                </asp:Label>,
                <asp:Label runat="server" ID="lbl_add2" >
                </asp:Label>,
                <asp:Label runat="server" ID="lbl_town" >
                </asp:Label>
						
                </span>
            </p> 
         <%-- <p>
                
            	<a rel="popup13" class="reply-link poplight" style="margin-top:0px; margin-left:35px;" href="#?w=730"> <cc1:GMap ID="GMap1" runat="server" /> </a>
            </p>--%>
          </div>
          <div class="blue-box-company blue-box-company-bk ">
          <div id="div_follow" runat="server" visible="false">
          	<a href="#">
            <asp:Button runat="server" Text="Follow" ID="btn_follow" OnClientClick="return rowAction01();" OnClick = "Follow" style="font-size:12px; font-family:Arial, Helvetica, sans-serif; padding:5px 15px;" class="button-orange floatleft"/>
            </a>  <strong style="margin-left: 7px;">  <asp:Label ID="Label3" runat="server" ></asp:Label> now</strong>
           
        </div>
        <div visible="false"  id="div_following" runat="server">
        
        <asp:Button  ID="btn_following" OnClientClick="return rowAction01();" CssClass="button-orange button-orange-fc" Style="margin-left: 9px;"
                                    Text="Following" runat="server" OnClick="Following" />
        <strong style="margin-left: 7px;">  <asp:Label ID="Label5" runat="server" ></asp:Label></strong>
       
            </div>
        
      
        <div class="notification-right notification-right1" style="margin-left: 223px;margin-top: -266px;">
         
         <h3 class="head-tab">Latest Press Releases</h3> 
          <asp:DataList ID="dl1" runat="server" >
          <ItemTemplate>
         
          <table class="article-tab">
   <tr>
    <th align="left" valign="top"><asp:Label runat="server" Text='<%#Eval("PostedDate") %>'></asp:Label></th>
    <td align="left" valign="top">
    <a runat="server"  href='<%# "Articleview.aspx?Id=" + Eval("CompanyId")+"&ATId="+Eval("Id") %>' ><asp:Label ID="Label1" runat="server" Text='<%#Eval("Article") %>'></asp:Label></a>
    
    </td>
  </tr>
   
</table> </ItemTemplate></asp:DataList>

          
        </div>
        
        
      </div>
    </div></div>
    <div class="content-inner-right content-inner-right-width" style=" margin-left: -28px; margin-right: -232px;">
       <div class="view-company-info" style="width:198px;">
        <%--<a id="pstjob" runat="server" style="margin-right:10px;" class="button-green floatleft"></a>--%>
        <asp:Button runat="server" ID="btn_post" class="button-green floatleft" Text="Post an opportunity" OnClick="post_Click" OnClientClick="return rowAction01();" />&nbsp&nbsp&nbsp
       <%-- <a href="#"> <asp:Button runat="server" Text="Follow" ID="btn" OnClick = "Follow" style="padding:5px 7px;" class="button-orange view-company-info-follow"/> </a>--%>
         
          	
            <asp:Button runat="server" OnClientClick="return rowAction01(this.name);" Visible="false" Text="Follow" ID="Button1"  OnClick = "Follow" style="padding:5px 7px;" class="button-orange view-company-info-follow"/>
             
           
       
      
        
        <asp:Button ID="Button2" Visible="false" CssClass="button-orange button-orange-fc" style="padding:5px 7px; margin-left: 129px; margin-top: -28px;" class="button-orange view-company-info-follow"
                       OnClientClick="return rowAction01();"  Text="Following" runat="server" OnClick="Following" />
        
       
          
 </div>
  <p class="margin-top-visible">&nbsp;</p>
 <div class="box-right box-right-companyl">
                
              
                 <uc:flw runat="server" ID="flw" />
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

<!-- Popup Div begins --><asp:DataList ID="dl2" runat="server"><ItemTemplate>
<div id="popup13" class="popup_block">
 <div class="apply-job ">
<h1>
	<asp:Label runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
</h1>
<strong><asp:Label ID="Label2" runat="server" Text='<%#Eval("CompanyHeading") %>'></asp:Label></strong>
<div class="map-block">
	<div class="map-block-left">
    <iframe width="405" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="http://maps.google.co.in/maps?hl=en&amp;ie=UTF8&amp;ll=10.782836,78.288503&amp;spn=5.588364,7.064209&amp;t=m&amp;z=7&amp;output=embed"></iframe><br /><small><a href="http://maps.google.co.in/maps?hl=en&amp;ie=UTF8&amp;ll=10.782836,78.288503&amp;spn=5.588364,7.064209&amp;t=m&amp;z=7&amp;source=embed" target="_blank" style="color:#0000FF;text-align:left">View Larger Map</a></small>
    </div>
    <div class="map-block-right">
    	<a href="#"><img src="images/google-add.jpg" width="236" height="421" alt="google-add" /></a>
    </div>
</div>
</div>
  
</div></ItemTemplate></asp:DataList>
<!-- Popup Div ends -->

    </div>
     <link href="js/nyroModal/styles/nyroModal.css" rel="stylesheet" type="text/css" />
    <script src="js/nyroModal/js/jquery.nyroModal.custom.min.js" type="text/javascript"></script>
    <!--[if IE 6]>
	<script type="text/javascript" src="js/jquery.nyroModal-ie6.min.js"></script>
<![endif]-->
<script type="text/javascript">
    function MarkDirectLike(feedId, likeType, refId) {
        if (CheckIfUserLoggedIn()) {
            $.ajax({
                type: "POST",
                url: "UserFeedService.asmx/MarkDirectLike",
                data: '{'
                    + '"feedId":"' + feedId + '",'
                    + '"type":"' + likeType + '",'
                    + '"refRecordId":"' + refId + '"'
                    + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (msg) {
                    LikeDetail = msg.d;
                    $('#<%=hypLikeProfile.ClientID %>').attr("href", "javascript:" + LikeDetail);
                    $('#<%=hypLikeProfile.ClientID %>').html("liked this company");
                    var cnt = $('#<%=lblLikeProfileCount.ClientID %>').html();
                    $('#<%=lblLikeProfileCount.ClientID %>').html(parseInt(cnt) + 1);
                    $('#<%=hypLikeProfile.ClientID %>').show();
                },
                error: function (msg) {
                }
            });
        }
    }
</script>
<script type="text/javascript">
    function MarkDirectUnlike(feedId, likeType, refId) {
        if (CheckIfUserLoggedIn()) {
            $.ajax({
                type: "POST",
                url: "UserFeedService.asmx/MarkDirectUnlike",
                data: '{'
                    + '"feedId":"' + feedId + '",'
                    + '"type":"' + likeType + '",'
                    + '"refRecordId":"' + refId + '"'
                    + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (msg) {
                    LikeDetail = msg.d;
                    $('#<%=hypLikeProfile.ClientID %>').attr("href", "javascript:" + LikeDetail);
                    $('#<%=hypLikeProfile.ClientID %>').html("like this company");
                    var cnt = $('#<%=lblLikeProfileCount.ClientID %>').html();
                    $('#<%=lblLikeProfileCount.ClientID %>').html(parseInt(cnt) - 1);
                },
                error: function (msg) {
                }
            });
        }
    }
</script>


</asp:Content>
