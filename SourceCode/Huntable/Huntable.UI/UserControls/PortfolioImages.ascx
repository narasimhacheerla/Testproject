<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioImages.ascx.cs"
    Inherits="Huntable.UI.UserControls.PortfolioImages" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="../text/html; charset=utf-8" />
<meta name="description" content="Huntable - The Professional Network" />
<meta name="keywords" content="The Professional Network" />
<link type="text/css" href="../css1/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
<%--<script type="text/javascript" language="javascript">
    $(function () {

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
        $('#foo122').carouFredSel({
            auto: true,
            width: 280,
            scroll: 1,
            prev: '#prev122',
            next: '#next122',
            pagination: "#pager122",
            mousewheel: false,

            circular: false,
            infinite: false
        });

        //	Variable number of visible items with variable sizes
        $('#foo3').carouFredSel({
            width: 280,
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
            scroll: 1
        });

    });
</script>
<div class="box-right" id="div_portfolio" runat="server">
    <div class="head-ash">
        <h3>
            Portfolio</h3>
    </div>
    <div class="list_carousel">
        <ul id="foo122">
            <%=GetMyPictures() %>
        </ul>
        <div class="clearfix">
        </div>
        <div class="pager-main">
            <a id="prev122" class="prev" href="#">
                <img src="images/prev.png" width="17" height="22" alt="previous" /></a>
            <div class="pager" id="pager122" style="display: block;">
            </div>
            <a id="next122" class="next" href="#">
                <div style="margin-left: 255px; margin-top: -20px;">
                    <img src="images/next.png" width="17" height="22" alt="Next" /></div>
            </a>
        </div>
    </div>
</div>--%>
<!-- Image Slide Show New-->
<style type="text/css">
    #scroller {
        position: relative;
    }
    #scroller .innerScrollArea {
        overflow: hidden;
        position: absolute;
        left: 0;
        right: 0;
        top: 0;
        bottom: 0;
    }
    #scroller ul {
        padding: 0;
        margin: 0;
        position: relative;
    }
    #scroller li {
        padding: 0;
        margin: 0;
        list-style-type: none;
        position: absolute;
    }
</style>
<br /><br />
<div style="margin-top:210px;" id="div_portfolio" runat="server">
    <div class="head-ash">
        <h3>
            Portfolio</h3>
    </div>
    <div id="scroller" class="box-right"  style="height:178px">
    
    
    <div class="innerScrollArea"  runat="server">
        <ul>
           <%=GetMyPictures() %>
        </ul>
    </div>
</div>
</div>
    
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js"></script>
<script type="text/javascript">
    $(function () {
        var scroller = $('#scroller div.innerScrollArea');
        var scrollerContent = scroller.children('ul');
        scrollerContent.children().clone().appendTo(scrollerContent);
        var curX = 0;
        scrollerContent.children().each(function () {
            var $this = $(this);
            $this.css('left', curX);
            curX += $this.outerWidth(true);
        });
        var fullW = curX / 2;
        var viewportW = scroller.width();

        // Scrolling speed management
        var controller = { curSpeed: 0, fullSpeed: 2 };
        var $controller = $(controller);
        var tweenToNewSpeed = function (newSpeed, duration) {
            if (duration === undefined)
                duration = 600;
            $controller.stop(true).animate({ curSpeed: newSpeed }, duration);
        };

        // Pause on hover
        scroller.hover(function () {
            tweenToNewSpeed(0);
        }, function () {
            tweenToNewSpeed(controller.fullSpeed);
        });

        // Scrolling management; start the automatical scrolling
        var doScroll = function () {
            var curX = scroller.scrollLeft();
            var newX = curX + controller.curSpeed;
            if (newX > fullW * 2 - viewportW)
                newX -= fullW;
            scroller.scrollLeft(newX);
        };
        setInterval(doScroll, 20);
        tweenToNewSpeed(controller.fullSpeed);
    });
</script>


<%--<link href="../themes/generic.css" rel="stylesheet" type="text/css" />
<link href="../themes/1/slider.css" rel="stylesheet" type="text/css" />
<script src="../themes/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="../themes/1/jquery-slider.js" type="text/javascript"></script>
<div class="div2" id="div_portfolio" runat="server" style="margin-top: 251px; margin-left: -251px;">
<div style="width: 290px; height: 36px; background: -webkit-gradient(linear,left top,left bottom,color-stop(0%,#fdfdfd), color-stop(100%,#cdcdcd));
            text-shadow: 0px 1px 1px #fff; color: #343434; display: block; padding-top: 8px;
            padding-left: 8px; margin-left:251px">
            <h3 style="font-size: 16px; font-weight: bold;">
               Portfolio </h3>
        </div>
    <div id="mcts1" style="width: 281px; padding-top: 0px; height: 190px; padding-right: 7px;
        padding-left: 7px; padding-bottom: 30px;">
        <%=GetMyPictures() %>
    </div>
</div>--%>
