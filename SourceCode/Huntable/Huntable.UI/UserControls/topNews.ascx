<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="topNews.ascx.cs" Inherits="Huntable.UI.UserControls.TopNews" %>
<link href="../Styles/NewStylesheet.css" rel="Stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
    var link1 = '<%=Links[0] %>';
    var link2 = '<%=Links[1] %>';
    var link3 = '<%=Links[2] %>';
    var link4 = '<%=Links[3] %>';
</script>
<script type="text/javascript" src="../js/jquery-1.7.1.min.js"></script>
<style type="text/css">
    .jcarousel-skin-tango .jcarousel-container
    {
        -moz-border-radius: 10px;
        -webkit-border-radius: 10px;
        border-radius: 10px;
    }
    
    .jcarousel-skin-tango .jcarousel-direction-rtl
    {
        direction: rtl;
    }
    
    .jcarousel-skin-tango .jcarousel-container-horizontal
    {
        width: 564px;
        padding: 0px 40px 0px 40px;
    }
    
    .jcarousel-skin-tango .jcarousel-container-vertical
    {
        width: 75px;
        height: 245px;
        padding: 40px 20px;
    }
    
    .jcarousel-skin-tango .jcarousel-clip
    {
        overflow: hidden;
    }
    
    .jcarousel-skin-tango .jcarousel-clip-horizontal
    {
        width: 562px;
        height: 170px;
    }
    
    .jcarousel-skin-tango .jcarousel-clip-vertical
    {
        width: 75px;
        height: 245px;
    }
    
    .jcarousel-skin-tango .jcarousel-item
    {
        width: 136px;
        height: 170px;
    }
    
    .jcarousel-skin-tango .jcarousel-item-horizontal
    {
        margin-left: 0;
        margin-right: 10px;
    }
    
    .jcarousel-skin-tango .jcarousel-direction-rtl .jcarousel-item-horizontal
    {
        margin-left: 10px;
        margin-right: 0;
    }
    
    .jcarousel-skin-tango .jcarousel-item-vertical
    {
        margin-bottom: 10px;
    }
    
    .jcarousel-skin-tango .jcarousel-item-placeholder
    {
        background: #fff;
        color: #000;
    }
    
    /**
 *  Horizontal Buttons
 */
    .jcarousel-skin-tango .jcarousel-next-horizontal
    {
        position: absolute;
        top: 50px;
        right: 5px;
        width: 32px;
        height: 52px;
        cursor: pointer;
        background: transparent url(../images/next-horizontal.png) no-repeat 0 0;
    }
    
    .jcarousel-skin-tango .jcarousel-direction-rtl .jcarousel-next-horizontal
    {
        left: 5px;
        right: auto;
        background-image: url(prev-horizontal.png);
    }
    
    .jcarousel-skin-tango .jcarousel-next-horizontal:hover, .jcarousel-skin-tango .jcarousel-next-horizontal:focus
    {
        background-position: -32px 0;
    }
    
    .jcarousel-skin-tango .jcarousel-next-horizontal:active
    {
        background-position: -64px 0;
    }
    
    .jcarousel-skin-tango .jcarousel-next-disabled-horizontal, .jcarousel-skin-tango .jcarousel-next-disabled-horizontal:hover, .jcarousel-skin-tango .jcarousel-next-disabled-horizontal:focus, .jcarousel-skin-tango .jcarousel-next-disabled-horizontal:active
    {
        cursor: default;
        background-position: -96px 0;
    }
    
    .jcarousel-skin-tango .jcarousel-prev-horizontal
    {
        position: absolute;
        top: 50px;
        left: 5px;
        width: 32px;
        height: 52px;
        cursor: pointer;
        background: transparent url(../images/prev-horizontal.png) no-repeat 0 0;
    }
    
    .jcarousel-skin-tango .jcarousel-direction-rtl .jcarousel-prev-horizontal
    {
        left: auto;
        right: 5px;
        background-image: url(../images/next-horizontal.png);
    }
    
    .jcarousel-skin-tango .jcarousel-prev-horizontal:hover, .jcarousel-skin-tango .jcarousel-prev-horizontal:focus
    {
        background-position: -32px 0;
    }
    
    .jcarousel-skin-tango .jcarousel-prev-horizontal:active
    {
        background-position: -64px 0;
    }
    
    .jcarousel-skin-tango .jcarousel-prev-disabled-horizontal, .jcarousel-skin-tango .jcarousel-prev-disabled-horizontal:hover, .jcarousel-skin-tango .jcarousel-prev-disabled-horizontal:focus, .jcarousel-skin-tango .jcarousel-prev-disabled-horizontal:active
    {
        cursor: default;
        background-position: -96px 0;
    }
    
    /**
 *  Vertical Buttons
 */
    .jcarousel-skin-tango .jcarousel-next-vertical
    {
        position: absolute;
        bottom: 10px;
        left: 43px;
        width: 32px;
        height: 52px;
        cursor: pointer;
        background: transparent url(../images/next-vertical.png) no-repeat 0 0;
    }
    
    .jcarousel-skin-tango .jcarousel-next-vertical:hover, .jcarousel-skin-tango .jcarousel-next-vertical:focus
    {
        background-position: 0 -32px;
    }
    
    .jcarousel-skin-tango .jcarousel-next-vertical:active
    {
        background-position: 0 -64px;
    }
    
    .jcarousel-skin-tango .jcarousel-next-disabled-vertical, .jcarousel-skin-tango .jcarousel-next-disabled-vertical:hover, .jcarousel-skin-tango .jcarousel-next-disabled-vertical:focus, .jcarousel-skin-tango .jcarousel-next-disabled-vertical:active
    {
        cursor: default;
        background-position: 0 -96px;
    }
    
    .jcarousel-skin-tango .jcarousel-prev-vertical
    {
        position: absolute;
        top: 5px;
        left: 43px;
        width: 32px;
        height: 32px;
        cursor: pointer;
        background: transparent url(../images/prev-vertical.png) no-repeat 0 0;
    }
    
    .jcarousel-skin-tango .jcarousel-prev-vertical:hover, .jcarousel-skin-tango .jcarousel-prev-vertical:focus
    {
        background-position: 0 -32px;
    }
    
    .jcarousel-skin-tango .jcarousel-prev-vertical:active
    {
        background-position: 0 -64px;
    }
    
    .jcarousel-skin-tango .jcarousel-prev-disabled-vertical, .jcarousel-skin-tango .jcarousel-prev-disabled-vertical:hover, .jcarousel-skin-tango .jcarousel-prev-disabled-vertical:focus, .jcarousel-skin-tango .jcarousel-prev-disabled-vertical:active
    {
        cursor: default;
        background-position: 0 -96px;
    }
</style>
<script type="text/javascript">
    $('document').ready(function () {
        $(".event_news li:nth-child(3)").addClass("doShow");

        $("a.next").click(function () {
            $('.first').toggleClass("first");
            var $toHighlight = $('.doShow').next('li').length > 0 ? $('.doShow').next('li') : $('.event_news li').first();
            $('.doShow').removeClass('doShow');
            $toHighlight.addClass('doShow');
        });

        $("a.prev").click(function () {
            $('.first').toggleClass("first");
            var $toHighlight = $('.doShow').prev('li').length > 0 ? $('.doShow').prev('li') : $('.event_news li').last();
            $('.doShow').removeClass('doShow');
            $toHighlight.addClass('doShow');
        });

    });  
</script>
​​
<div class="top-news">
    <h2>
        Today's Top News</h2>
    <div class="top-news-inner">
        <div id="scroller-header">
            <a href="#panel-1" rel="panel" style="margin-left: 7px;" class="selected">Skills</a>
            <a href="#panel-2" rel="panel">Industry</a> <a href="#panel-3" rel="panel">Interests</a>
            <a href="#panel-4" rel="panel" style="margin-right: 0px;">Country</a>
        </div>
        <div id="scroller-body">
            <div id="mask">
                <div id="panel">
                    <div id="panel-1" class="unselect">
                        <ul id="first-carousel" class="first-and-second-carousel jcarousel-skin-tango">
                            <li>
                                <asp:ImageButton ID="imgNews1" CssClass="profile-pic" Width="80" Height="60" AlternateText="top-news-img"
                                    runat="server" OnClientClick="javascript:window.open(link1,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;" />
                                <p>
                                    <asp:LinkButton ID="lnkApple" runat="server" CssClass="LinkButton" OnClientClick="javascript:window.open(link1,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                            </li>
                            <li>
                                <asp:ImageButton ID="imgNews2" CssClass="profile-pic" Width="80" Height="60" AlternateText="top-news-img"
                                    runat="server" OnClientClick="javascript:window.open(link2,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;" />
                                <p>
                                    <asp:LinkButton ID="lnkMac" runat="server" ForeColor="Black" CssClass="LinkButton"
                                        OnClientClick="javascript:window.open(link2,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                            </li>
                            <li>
                                <asp:ImageButton ID="imgNews3" CssClass="profile-pic" Width="80" Height="60" AlternateText="top-news-img"
                                    runat="server" OnClientClick="javascript:window.open(link3,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;" />
                                <p>
                                    <asp:LinkButton ID="lnkErrorMessages" runat="server" ForeColor="Black" CssClass="LinkButton"
                                        OnClientClick="javascript:window.open(link3,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                            </li>
                            <li>
                                <asp:ImageButton ID="imgNews6" CssClass="profile-pic" Width="80" Height="60" AlternateText="top-news-img"
                                    runat="server" OnClientClick="javascript:window.open(link4,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;" />
                                <p>
                                    <asp:LinkButton ID="lnkMac1" runat="server" ForeColor="Black" CssClass="LinkButton"
                                        OnClientClick="javascript:window.open(link4,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                            </li>
                        </ul>
                    </div>
                    <div id="panel-2" style="display: none;" class="unselect">
                        <ul id="Ul1" class="first-and-second-carousel jcarousel-skin-tango">
                            <li>
                                <asp:Image ID="imgNews10" CssClass="profile-pic" Width="80" Height="60" AlternateText="top-news-img"
                                    runat="server" />
                                <p>
                                    <asp:LinkButton ID="lnkEM" runat="server" ForeColor="Black" CssClass="LinkButton"
                                        OnClientClick="javascript:window.open(link5,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                            </li>
                            <li>
                                <asp:Image ID="Image1" CssClass="profile-pic" Width="80" Height="60" AlternateText="top-news-img"
                                    runat="server" />
                                <p>
                                    <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" CssClass="LinkButton"
                                        OnClientClick="javascript:window.open(link6,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                            </li>
                            <li>
                                <asp:Image ID="Image2" CssClass="profile-pic" Width="80" Height="60" AlternateText="top-news-img"
                                    runat="server" />
                                <p>
                                    <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Black" CssClass="LinkButton"
                                        OnClientClick="javascript:window.open(link7,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                            </li>
                            <li>
                                <asp:Image ID="Image3" CssClass="profile-pic" Width="80" Height="60" AlternateText="top-news-img"
                                    runat="server" />
                                <p>
                                    <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Black" CssClass="LinkButton"
                                        OnClientClick="javascript:window.open(link8,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                            </li>
                            <li class="top-news-arrow"><a href="#">
                                <img src="../images/top-news-tab-arrow.png" width="15" height="20" alt="arrow" /></a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<%-- <div class="top-news">
            <h2>Today's Top News</h2>
            <div class="top-news-inner">
            <div id="Div1">
<a href="#panel-1" rel="panel" style="margin-left:7px;"  class="selected">Skills</a>
<a href="#panel-2" rel="panel">Industry</a>
<a href="#panel-3" rel="panel">Interests</a>
<a href="#panel-4" rel="panel" style="margin-right:0px;">Country</a>
</div>
            <div id="Div2">
<div id="Div3">
<div id="Div4">
	<div id="Div5" class="unselect">
	<ul id="first-carousel" class="first-and-second-carousel jcarousel-skin-tango">
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 1</p>
    </li>
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 1</p>
    </li>
     <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 1</p>
    </li>
     <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 1</p>
    </li>
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 1</p>
    </li>
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 1</p>
    </li>
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 1</p>
    </li>
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 1</p>
    </li>
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 1</p>
    </li>
    
    
  </ul>

        
	</div>
 	<div id="panel-2"  style="display:none;" class="unselect">
		<ul id="first-carousel" class="first-and-second-carousel jcarousel-skin-tango">
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 2</p>
    </li>
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 2</p>
    </li>
     <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 2</p>
    </li>
     <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 2</p>
    </li>
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 2</p>
    </li>
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 2</p>
    </li>
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 2</p>
    </li>
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 2</p>
    </li>
    <li>
    <a href="#"><img src="images/top-news-img1.jpg" class="profile-pic" width="120" height="120" alt="top-news-img" /></a>
    <p>Image 2</p>
    </li>
    
    
  </ul>
  </div>
  </div>
  </div>--%>
<!-- Top news tab function Script begins -->
<script type="text/javascript" src="../js/jquery-1.7.1.min.js"></script>
<script type="text/javascript" src="../js/jquery.scrollTo.js"></script>
<script type="text/javascript">
//	<![CDATA[
    $(document).ready(function () {

        //Get the height of the first item
        //$('#mask').css({'height':$('#panel-1').height()});	

        //Calculate the total width - sum of all sub-panels width
        //Width is generated according to the width of #mask * total of sub-panels
        $('#panel').width(parseInt($('#mask').width() * $('#panel div').length));

        //Set the sub-panel width according to the #mask width (width of #mask and sub-panel must be same)
        $('#panel div').width($('#mask').width());
        $('.unselect').hide();
        $('#panel-1').show();
        //Get all the links with rel as panel
        $('a[rel=panel]').click(function () {

            //Get the height of the sub-panel
            var panelheight = $($(this).attr('href')).height();
            $('.unselect').hide();
            $($(this).attr('href')).show();

            //Set class for the selected item
            $('a[rel=panel]').removeClass('selected');
            $(this).addClass('selected');

            //Resize the height
            $('#mask').animate({ 'height': panelheight }, { queue: false, duration: 500 });

            //Scroll to the correct panel, the panel id is grabbed from the href attribute of the anchor
            $('#mask').scrollTo($(this).attr('href'), 800);

            //Discard the link default behavior
            return false;
        });

    });
//]]>
</script>
<!-- Top news tab function Script ends -->
<!-- Image Carousel Script Begins -->
<script type="text/javascript" src="../js/jquery.jcarousel.min.js"></script>
<!--<link rel="stylesheet" type="text/css" href="css/skin1.css" />-->
<script type="text/javascript">
//	<![CDATA[
    jQuery(document).ready(function () {
        jQuery('.first-and-second-carousel').jcarousel();
        jQuery('#third-carousel').jcarousel({
            vertical: true
        });
    });
//]]>
</script>
<!-- Image Carousel Script Ends -->
<script type="text/javascript">
//	<![CDATA[
    jQuery(document).ready(function () {
        jQuery("#tabswitch ul li:first").addClass("active");
        jQuery("#tabswitch div.tab-container:first").show();
        jQuery("#tabswitch ul li").click(function () {
            jQuery("#tabswitch div.tab-container").hide();
            jQuery("#tabswitch ul li").removeClass("active");
            var tab_class = jQuery(this).attr("class");
            jQuery("#tabswitch div." + tab_class).show();
            jQuery("#tabswitch ul li." + tab_class).addClass("active");
        })
    });
//]]>
</script>
