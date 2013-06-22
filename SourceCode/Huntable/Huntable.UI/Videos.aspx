<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Videos.aspx.cs" Inherits="Huntable.UI.Videos" %>

<%@ Register Src="~/UserControls/AddVideo.ascx" TagName="AddVideos" TagPrefix="uc1" %>
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
        <link rel="stylesheet" type="text/css" href="css/skin2.css" />
        <link rel="stylesheet" type="text/css" href="css/skin3.css" />
        <!-- Fancy box style begins -->
        <link rel="stylesheet" href="css/jquery.fancybox.css" type="text/css" media="screen" />
        <!-- Fancy box style begins -->
        <style type="text/css">
            .jcarousel-skin-tango .jcarousel-container-horizontal
            {
                width: 750px;
            }
            .jcarousel-skin-tango .jcarousel-clip-horizontal
            {
                width: 580px;
                height: 195px;
            }
            .jcarousel-skin-tango .jcarousel-item-horizontal
            {
                margin-right: 19px;
            }
            .jcarousel-skin-tango .jcarousel-item
            {
                width: 136px;
                height: 165px;
            }
            .jcarousel-skin-tango .jcarousel-clip-horizontal
            {
                width: 747px;
            }
            .jcarousel-skin-tango .jcarousel-prev-horizontal, .jcarousel-skin-tango .jcarousel-next-horizontal
            {
                top: 75px;
            }
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
                <div class="content-inner-left content-inner-left-message">
                    <div>
                        <h4 class="login-heading" >
                        <a href="#" class="accounts-link">
                            <asp:Label runat="server" ID="lblname"></asp:Label></a> > <a href="#" class="accounts-link">
                                Profile</a> > <a href="#" class="accounts-link">Activity</a> > Videos&nbsp;&nbsp;&nbsp;&nbsp;
                        <uc1:AddVideos ID="addVideos" runat="server" /></h4>
                        <br />
                        <asp:Label Text="No Videos Added" runat="server" ID="lblvideos" Visible="false" Font-Size="17px"
                            CssClass="advert3"></asp:Label>
                        <br />
                        <asp:DataList OnItemDataBound="RepeaterItemDataBound" runat="server" ID="dlvideo"
                            RepeatColumns="4" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:ImageButton Style="margin-left: 71px;" Visible="False" runat="server" ID="delete"
                                    OnClick="DeleteClick" CommandArgument='<%#Eval("Id")%>' ImageUrl="images/cancel.jpg"
                                    Width="10px" Height="10px" />
                                <div style="height: 140px; width: 190px;">
                                    <iframe src='<%#Eval("VideoUrl")%>' id="videourl" runat="server" width="160" height="92">
                                    </iframe>
                                    <br />
                                    <a href='javascript:OpenMainPopup("/ajax-picture.aspx?UserVideoId=<%#Eval("Id")%>")'>
                                        <img alt="Like" src="images/icon-like1.png" />Like</a> <a href='javascript:OpenMainPopup("/ajax-picture.aspx?UserVideoId=<%#Eval("Id")%>")'>
                                            <img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
                                </div>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <br />
                                <br />
                            </SeparatorTemplate>
                        </asp:DataList>
                        <%--<div class="gallery">   
       <ul id="videos">
        	<li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
            <li>
            	<a href="images/tour_video.png" name="videos/tour.flv" title="Café Tour" class="video_link captions" rel="gallery">
                <img src="images/video-thumb.jpg"  alt="tour" width="160" style="margin-bottom:4px" />
                </a><br />
                <a href="#" class="reply-link"><img alt="Like" src="images/icon-like1.png" />Like</a>
          <a href="#?w=530" class="reply-link poplight" rel="popup13"><img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
            </li>
          
              
        </ul>
       
    </div>--%>
                    </div>
                </div>
                <div class="content-inner-right content-inner-right-message">
                    <div class="google-add">
                        <asp:Image ID="bimage" runat="server" ImageUrl="images/basic-user-advert.gif" />
                        <asp:Image ID="pimage" runat="server" ImageUrl="images/premium-user-advert.gif" />
                        <script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
                        </script>
                        <script type="text/javascript" src="UserControls/js/jquery-1.7.1.min.js"></script>
                        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
                            type="text/javascript"></script>
                        <script src="UserControls/js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
                    </div>
                </div>
            </div>
        </div>
        <link href="js/nyroModal/styles/nyroModal.css" rel="stylesheet" type="text/css" />
        <script src="js/nyroModal/js/jquery.nyroModal.custom.min.js" type="text/javascript"></script>
        <!--[if IE 6]>
	<script type="text/javascript" src="js/jquery.nyroModal-ie6.min.js"></script>
<![endif]-->
    </body>
    </html>
</asp:Content>
