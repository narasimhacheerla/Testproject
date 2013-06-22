<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Company-blogs-popular.aspx.cs" Inherits="Huntable.UI.Company_blogs_popular" %>
<%@ Register Src="~/UserControls/Messagepopup.ascx" TagName="message" TagPrefix="uc1" %>
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
                 var top = $('#bxover').offset().top;
                 $(window).scroll(function (event) {
                     var y = $(this).scrollTop();
                     if (y >=50) { $('#bxover').addClass('fixed'); }
                     else { $('#bxover').removeClass('fixed'); }
                 });
             }
         });</script>
           <script type="text/javascript">
               $(function () {

                   var msie6 = $.browser == 'msie' && $.browser.version < 7;
                   if (!msie6) {
                       var top = $('#overlap').offset().top;
                       $(window).scroll(function (event) {
                           var y = $(this).scrollTop();
                           if (y >= 50) { $('#overlap').addClass('fixed'); }
                           else { $('#overlap').removeClass('fixed'); }
                       });
                   }
               });</script>
  <style type="text/css">

#slidebox{position:relative;  }
#slidebox, #slidebox .content{width:650px; margin-bottom:20px;}
#slidebox, #slidebox .container, #slidebox .content{height:230px;}
#slidebox{overflow:hidden;}
#slidebox .container{position:relative; left:0;}
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

<div id="content-section">
      <div id="content-inner">
      
      <div class="accounts-profile2 ">
       <div class="top-breadcrumb">
      <div class="accounts-profile2-left"> 
     <a href="companieshome.aspx" class="accounts-link">Company</a>&nbsp;&rsaquo;&rsaquo;&nbsp;<a id="comp_url_link" runat="server" class="accounts-link"><asp:Label ID="lblcompname" runat="server" Text="Infosys"></asp:Label></a>&nbsp;&rsaquo;&rsaquo;&nbsp;<strong>Blogs</strong>
      </div>
      <%--<div class="accounts-profile2-right">
      &nbsp;&lsaquo;&lsaquo;&nbsp;<a href="CompanyOverview.aspx" class="accounts-link">Back to main menu</a></div>
      </div>--%>
     <div id="overlap" ></div> 
     <div id="bxover"><ul class="overview-tab">                
                <li><a id="overview" runat="server" >Overview</a></li>
                <li><a id="activity" runat="server" >Activity</a></li>
                <li><a id="productsandservices" runat="server"> Products &amp; Services</a></li>
                <li><a id="careers" runat="server" >Careers</a></li>
                <li><a id="busunessblog" runat="server"  class="selected-tab" >Business Blog</a></li>
                <li><a id="article" runat="server" >Article</a></li>   
               <uc1:message runat="server" />
                         
              </ul> </div>      
      </div>
    <br /><br /><br /><br /><br />
   <div id="divreg" runat="server" visible="false" style="border:1px solid grey;margin-top: 60px;
height: 60px;border-radius: 5px;">
  <h2 style="font-family:Georgia;text-align: center;
margin-top: 20px;">No Blogs to display</h2>  
   </div>
 <div style="margin-top:14px">
        <iframe id="ifblog"  runat="server"  width="975px" height= "1300px" visible="true"></iframe>
 
 </div>



    

    
  
</body>
</html>

    
    </div>

    
    </div>

    
</asp:Content>
