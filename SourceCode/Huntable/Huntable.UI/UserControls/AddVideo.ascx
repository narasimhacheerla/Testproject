<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddVideo.ascx.cs" Inherits="Huntable.UI.UserControls.AddVideo" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
<link rel="stylesheet" type="text/css" href="../css/skin2.css" />
<link rel="stylesheet" type="text/css" href="../css/skin3.css" />
<link rel="stylesheet" href="../css/jquery.fancybox.css" type="text/css" media="screen" />
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
<a href="AddVideos.aspx"  class="button-ash button-ash-m poplight"  style="float: right">Add
    Videos</a> <a href="AddPictures.aspx" class="button-ash button-ash-m poplight" 
        style="float: right;">Add Pictures</a>


<!-- content inner ends -->

<!-- content section ends -->
<!-- Range Slider Script Begins -->
<!--<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>-->
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
    type="text/javascript"></script>
<script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
<script type="text/javascript" src="js/flipcounter.js"></script>
<!-- Range Slider Script Ends -->
<!-- Footer section ends -->
<!-- Popup Div begins -->
<div id="popup13" class="popup_block">
    <div class="apply-job ">
        <div class="comments">
            <div class="comments-head">
                <img width="13" height="12" src="images/icon-like1.png" style="margin-top: 2px;"
                    alt="comments"><span style="float: left; margin: 0px;">You and&nbsp; </span>
                <a href="#">2 others</a>&nbsp;like this
            </div>
            <div class="comments-desc">
                <div class="comments-desc-left">
                    <a href="#">
                        <img width="46" height="45" src="images/profile-thumb-small.jpg" alt="img">
                    </a>
                </div>
                <div class="comments-desc-right">
                    <textarea onblur="if(this.value=='')this.value=this.defaultValue;" onfocus="if(this.value==this.defaultValue)this.value='';"
                        class="textarea-profile textarea-comment">Write a comment...</textarea>
                    <br>
                    <br>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- Popup Div ends -->
<!-- Popup Div begins -->
<div id="popup14" class="popup_block">
    <div class="apply-job ">
        <asp:TextBox ID="txtpicturedescription" type="text" class="textbox-white" value="Give a name to your picture"
            onfocus="if (this.value =='Give a name to your picture') {this.value ='';}" onblur="if (this.value == '') {this.value ='Give a name to your picture';}"
            runat="server" />
        <div style="float: left; width: 100%;">
            <h3>
                Your Picture</h3>
            <asp:FileUpload runat="server" ID="fpPictureUpload" /><asp:Button runat="server"
                ID="Btnpicture" OnClick="BtnPictureSave" Style="margin-left: 15px;" Text="Save"
                CssClass="button-green" />
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
                            <img src="images/add-your-picture.jpg" width="120" height="160" alt="add-picture" />
                        </li>
                        <li class="jcarousel-item jcarousel-item-horizontal jcarousel-item-1 jcarousel-item-1-horizontal"
                            style="float: left; list-style: none outside none;" jcarouselindex="1"><a class="accounts-link accounts-link-ut"
                                href="#">
                                <img width="14" style="border: 0px solid #FFFFFF; box-shadow: 0 0 0px 0px #CCCCCC;"
                                    height="14" alt="add" src="images/add-icon.png">
                                Add more</a> </li>
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
        <div style="float: right; width: 100%; margin-top: 20px; clear: both; text-align: right;">
            <a href="#" style="margin-right: 20px;" class="accounts-link">Cancel</a><input type="button"
                class="button-orange" value="Upload now" />
        </div>
    </div>
</div>
<!-- Popup Div ends -->
<!-- Popup Div begins -->
<div id="popup15" class="popup_block">
    <div class="apply-job ">
        <asp:TextBox ID="txtvideoname" type="text" class="textbox-white" value="Give a name to your Video"
            onfocus="if (this.value =='Give a name to your Video') {this.value ='';}" onblur="if (this.value == '') {this.value ='Give a name to your Video';}"
            runat="server" />
        <div style="float: left; width: 100%;">
            <h3>
                Your Video</h3>
            <p style="margin: 7px 0px;">
                Share link to a You Tube, Vimeo, Dailmotion or Google video about you <a class="accounts-link"
                    href="#">
                    <img width="14" style="float: none; margin-left: 10px; vertical-align: middle;" height="14"
                        alt="add" src="images/add-icon.png">
                    Add More</a>
            </p>
            <asp:TextBox ID="txtvideourl" type="text" class="textbox" value="eg:Video URL" onfocus="if (this.value =='eg:Video URL') {this.value ='';}"
                onblur="if (this.value == '') {this.value ='eg:Video URL';}" runat="server" /><%--<input style="margin-left:15px;" type="button" class="button-green" value="Save" />--%><asp:Button
                    runat="server" ID="btnvideosaveclick" OnClick="BtnVideoSave" Style="margin-left: 15px;"
                    Text="Save" CssClass="button-green" />
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
<!-- Popup videos Script Ends -->
<script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
<script type="text/javascript" src="js/flowplayer-3.1.4.min.js"></script>
<script type="text/javascript" src="js/jquery.fancybox-1.2.1.pack.js"></script>
<script type="text/javascript" src="js/jquery.easing.1.3.js"></script>
<script type="text/javascript" src="js/fancyplayer.js"></script>
<script type="text/javascript">

    var videopath = "http://www.burconsult.com/tutorials/fp2/";
    var swfplayer = videopath + "videos/flowplayer-3.1.5.swf";
    var swfcontent = videopath + "videos/flowplayer.content-3.1.0.swf";
    var swfcaptions = videopath + "videos/flowplayer.captions-3.1.4.swf";

</script>
<!-- Popup videos Script Ends -->
<!-- Image Carousel Script Begins -->
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
<!-- Image Carousel Script Ends -->
<!-- Popup Script Begins -->
<script type="text/javascript">
    $(document).ready(function () {

        //When you click on a link with class of poplight and the href starts with a # 
        $('a.poplight[href^=#]').click(function () {
            var popID = $(this).attr('rel'); //Get Popup Name
            var popURL = $(this).attr('href'); //Get Popup href to define size

            //Pull Query & Variables from href URL
            var query = popURL.split('?');
            var dim = query[1].split('&');
            var popWidth = dim[0].split('=')[1]; //Gets the first query string value

            //Fade in the Popup and add close button
            $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="images/close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>');

            //Define margin for center alignment (vertical + horizontal) - we add 80 to the height/width to accomodate for the padding + border width defined in the css
            var popMargTop = ($('#' + popID).height() + 80) / 2;
            var popMargLeft = ($('#' + popID).width() + 80) / 2;

            //Apply Margin to Popup
            $('#' + popID).css({
                'margin-top': -popMargTop,
                'margin-left': -popMargLeft
            });

            //Fade in Background
            $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
            $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); //Fade in the fade layer 

            return false;
        });


        //Close Popups and Fade Layer
        $('a.close, #fade').live('click', function () { //When clicking on the close or fade layer...
            $('#fade , .popup_block').fadeOut(function () {
                $('#fade, a.close').remove();
            }); //fade them both out

            return false;
        });


    });

</script>
