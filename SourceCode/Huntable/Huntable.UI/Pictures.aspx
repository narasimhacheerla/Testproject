<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Pictures.aspx.cs" Inherits="Huntable.UI.Pictures" %>

<%@ Register Src="~/UserControls/GoogleAdds.ascx" TagName="ga" TagPrefix="uc" %>
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
        <link rel="stylesheet" type="text/css" href="css/jquery.fancybox-picture.css?v=2.1.2"
            media="screen" />
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
            
            
            .fancybox-custom .fancybox-skin
            {
                box-shadow: 0 0 50px #222;
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
        <!-- main menu ends -->
        <div id="content-section">
            <div id="content-inner">
                <div class="content-inner-left content-inner-left-message">
                    <div>
                        <h4 class="login-heading">
                            <a id="vw" runat="server" class="accounts-link">
                                <asp:Label ID="uname" runat="server"></asp:Label>
                            </a>> <a href="#" class="accounts-link">Profile</a> > <a href="#" class="accounts-link">
                                Activity</a> > Pictures&nbsp;&nbsp;&nbsp;&nbsp; <a href="addvideos.aspx" class="button-ash poplight"
                                    rel="popup15" style="float: right;">Add Videos</a> <a href="addpictures.aspx" class="button-ash button-ash-m poplight"
                                        style="float: right;">Add Pictures</a>
                        </h4>
                    </div>
                    <asp:Label Text="No Pictures Added" runat="server" ID="lblpictures" Visible="false"
                        Font-Size="17px"></asp:Label>
                     
                    <asp:DataList runat="server" ID="dlpicture" OnItemDataBound="RepeaterItemDataBound"
                        RepeatColumns="4" RepeatDirection="Horizontal">
                        <ItemTemplate>
                            <asp:ImageButton Style="margin-left: 71px;" runat="server" Visible="False" ID="delete"
                                OnClick="DeleteClick" CommandArgument='<%#Eval("Id")%>' ImageUrl="images/cancel.jpg"
                                Width="10px" Height="10px" />
                            <div style="height: 140px; width: 160px;">
                                <a href='javascript:OpenMainPopup("/ajax-picture.aspx?UserPhotoId=<%#Eval("Id")%>")'>
                                    <asp:Image ImageUrl='<%#Picture(Eval("PictureId"))%>' runat="server" Width="150px"
                                        Height="100px" /></a> <a href='javascript:OpenMainPopup("/ajax-picture.aspx?UserPhotoId=<%#Eval("Id")%>")'>
                                            <img alt="Like" src="images/icon-like1.png" />Like</a> <a href='javascript:OpenMainPopup("/ajax-picture.aspx?UserPhotoId=<%#Eval("Id")%>")'>
                                                <img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
                            </div>
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <br />
                            <br />
                        </SeparatorTemplate>
                    </asp:DataList>
                </div>
                <div class="content-inner-right content-inner-right-message">
                      <asp:Image ID="bimage" runat="server"   ImageUrl="../images/basic-user-advert.gif"/>
                <asp:Image ID="pimage" runat="server"   ImageUrl="../images/premium-user-advert.gif" />
                </div>
            </div>
            <!-- content inner ends -->
        </div>
        <!-- content section ends -->
        <!-- Range Slider Script Begins -->
        <!--<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>-->
        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
            type="text/javascript"></script>
        <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
        <script type="text/javascript" src="js/flipcounter.js"></script>
        <link href="js/nyroModal/styles/nyroModal.css" rel="stylesheet" type="text/css" />
        <script src="js/nyroModal/js/jquery.nyroModal.custom.min.js" type="text/javascript"></script>
        <!--[if IE 6]>
	<script type="text/javascript" src="js/jquery.nyroModal-ie6.min.js"></script>
<![endif]-->
        <!-- Range Slider Script Ends -->
        <!-- Footer section ends -->
        <!-- Popup Div begins -->
        <div id="popup13" class="popup_block">
            <div class="apply-job ">
                <div class="comments">
                    <div class="comments-head">
                        <img width="13" height="12" src="images/icon-like1.png" style="margin-top: 2px;"
                            alt="comments" /><span style="float: left; margin: 0px;">You and&nbsp; </span>
                        <a href="#">2 others</a>&nbsp;like this
                    </div>
                    <div class="comments-desc">
                        <div class="comments-desc-left">
                            <a href="#">
                                <img width="46" height="45" src="images/profile-thumb-small.jpg" alt="img" />
                            </a>
                        </div>
                        <div class="comments-desc-right">
                            <textarea onblur="if(this.value=='')this.value=this.defaultValue;" onfocus="if(this.value==this.defaultValue)this.value='';"
                                class="textarea-profile textarea-comment">Write a comment...</textarea>
                            <br />
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Popup Div ends -->
        <!-- Popup Div begins -->
        <div id="popup14" class="popup_block">
            <div class="apply-job ">
                <input type="text" class="textbox-white" value="Give a name to your picture" onfocus="if (this.value =='Give a name to your picture') {this.value ='';}"
                    onblur="if (this.value == '') {this.value ='Give a name to your picture';}" />
                <div class=" jcarousel-skin-tango">
                    <div class="jcarousel-container jcarousel-container-horizontal" style="position: relative;
                        display: block;">
                        <div class="jcarousel-clip jcarousel-clip-horizontal" style="position: relative;">
                            <ul class="first-and-second-carousel jcarousel-list jcarousel-list-horizontal" id="first-carousel"
                                style="overflow: hidden; position: relative; top: 0px; margin: 0px; padding: 0px;
                                left: 0px; width: 1300px;">
                                <li class="jcarousel-item jcarousel-item-horizontal jcarousel-item-1 jcarousel-item-1-horizontal"
                                    style="float: left; list-style: none outside none;" jcarouselindex="1">
                                    <asp:Image ID="im" runat="server" Width="120" Height="160" />
                                </li>
                                <li class="jcarousel-item jcarousel-item-horizontal jcarousel-item-1 jcarousel-item-1-horizontal"
                                    style="float: left; list-style: none outside none;" jcarouselindex="1"><a class="accounts-link accounts-link-ut"
                                        href="#">
                                        <img width="14" style="border: 0px solid #FFFFFF; box-shadow: 0 0 0px 0px #CCCCCC;"
                                            height="14" alt="add" src="images/add-icon.png" />
                                        Add more</a> </li>
                            </ul>
                        </div>
                        <div class="user-regis-left-ut" style="margin-top: 0px;">
                            <asp:FileUpload ID="fp" runat="server" />
                            <asp:Button ID="btn" runat="server" Text="ADD PICTURE" /><br />
                            Supported file types: GIF,JPG,PNG
                        </div>
                        <div class="jcarousel-prev jcarousel-prev-horizontal jcarousel-prev-disabled jcarousel-prev-disabled-horizontal"
                            style="display: block;" disabled="disabled">
                        </div>
                        <div class="jcarousel-next jcarousel-next-horizontal" style="display: block;">
                        </div>
                    </div>
                </div>
                <div style="float: right; width: 100%; margin-top: 20px; clear: both; text-align: right;">
                    <a href="#" style="margin-right: 20px;" class="accounts-link">Cancel</a><input type="button"
                        class="button-orange" value="Upload now" />
                    <asp:Button ID="upb" runat="server" class="button-orange" Text="Upload now" />
                </div>
            </div>
        </div>
        <!-- Popup Div ends -->
        <!-- Popup Div begins -->
        <div id="popup15" class="popup_block">
            <div class="apply-job ">
                <input type="text" class="textbox-white" value="Give a name to your Video" onfocus="if (this.value =='Give a name to your Video') {this.value ='';}"
                    onblur="if (this.value == '') {this.value ='Give a name to your Video';}" />
                <div style="float: left; width: 100%;">
                    <h3>
                        Your Video</h3>
                    <p style="margin: 7px 0px;">
                        Share link to a You Tube, Vimeo, Dailmotion or Google video about you <a class="accounts-link"
                            href="#">
                            <img width="14" style="float: none; margin-left: 10px; vertical-align: middle;" height="14"
                                alt="add" src="images/add-icon.png" />
                            Add More</a>
                    </p>
                    <input type="text" class="textbox" value="eg:Video URL" onfocus="if (this.value =='eg:Video URL') {this.value ='';}"
                        onblur="if (this.value == '') {this.value ='eg:Video URL';}" /><input style="margin-left: 15px;"
                            type="button" class="button-green" value="Save" />
                </div>
                <div class=" jcarousel-skin-tango">
                    <div class="jcarousel-container jcarousel-container-horizontal" style="position: relative;
                        display: block;">
                        <div class="jcarousel-clip jcarousel-clip-horizontal" style="position: relative;">
                            <ul class="first-and-second-carousel jcarousel-list jcarousel-list-horizontal" id="first-carousel"
                                style="overflow: hidden; position: relative; top: 0px; margin: 0px; padding: 0px;
                                left: 0px; width: 1300px;">
                                <li class="jcarousel-item jcarousel-item-horizontal jcarousel-item-1 jcarousel-item-1-horizontal"
                                    style="float: left; list-style: none outside none;" jcarouselindex="1">
                                    <img src="images/video-comes-here.jpg" width="120" height="160" alt="video" />
                                </li>
                            </ul>
                        </div>
                        <div class="jcarousel-prev jcarousel-prev-horizontal jcarousel-prev-disabled jcarousel-prev-disabled-horizontal"
                            style="display: block;" disabled="disabled">
                        </div>
                        <div class="jcarousel-next jcarousel-next-horizontal" style="display: block;">
                        </div>
                    </div>
                </div>
                <div style="float: right; width: 100%; margin-top: 20px; clear: both; text-align: right;">
                    <a href="#" style="margin-right: 20px;" class="accounts-link">Cancel</a><input type="button"
                        class="button-orange" value="Upload now" />
                </div>
            </div>
        </div>
        <!-- Popup Div ends -->
        <!-- Image Carousel Script Begins -->
        <!-- Popup Script Ends -->
        <!-- Add fancyBox main JS and CSS files -->
    </body>
    </html>
</asp:Content>
