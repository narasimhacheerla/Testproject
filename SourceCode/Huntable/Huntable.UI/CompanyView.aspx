<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CompanyView.aspx.cs" Inherits="Huntable.UI.CompanyView" %>

<%@ Register Src="~/UserControls/Followers.ascx" TagName="followers" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/video.ascx" TagName="portImages" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/PortfolioImages.ascx" TagName="VAV" TagPrefix="uc5" %>
<%@ Register Src="~/UserControls/PortfolioImages.ascx" TagName="abc" TagPrefix="uc6" %>
<%@ Register Src="~/UserControls/Messagepopup.ascx" TagName="message" TagPrefix="uc1" %>
<%@ Register TagPrefix="cc1" Namespace="Subgurim.Controles" Assembly="GMaps, Version=4.0.0.7, Culture=neutral, PublicKeyToken=258385c8a4e17a2d" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Huntable - The Professional Network</title>
        <meta name="description" content="Huntable - The Professional Network" />
        <meta name="keywords" content="The Professional Network" />
        <link href="https://huntable.co.uk/css/style-innerpage.css" type="text/css" rel="stylesheet" />
        <link rel="stylesheet" type="text/css" href="https://huntable.co.uk/css/skin2.css" />
        <link rel="stylesheet" type="text/css" href="https://huntable.co.uk/css/skin3.css" />
        <link type="text/css" href="https://huntable.co.uk/css1/jquery-ui-1.8.23.custom.css"
            rel="stylesheet" />
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
        <script type="text/javascript">

            $(function () {

                // The height of the content block when it's not expanded
                var adjustheight = 20;
                // The "more" link text
                var moreText = "More";
                // The "less" link text
                var lessText = "Less";

                // Sets the .more-block div to the specified height and hides any content that overflows
                $(".more-less .more-block").css('height', adjustheight).css('overflow', 'hidden');

                // The section added to the bottom of the "more-less" div
                $(".more-less").append('<p class="continued"></p><a href="#" class="adjust"></a>');

                // $("a.adjust").text(Text);

                $(".adjust").toggle(function () {
                    $(this).parents("div:first").find(".more-block").css('height', 'auto').css('overflow', 'visible');
                    // Hide the [...] when expanded
                    $(this).parents("div:first").find("p.continued").css('display', 'none');
                    $(this).text(lessText);
                }, function () {
                    $(this).parents("div:first").find(".more-block").css('height', adjustheight).css('overflow', 'hidden');
                    $(this).parents("div:first").find("p.continued").css('display', 'block');
                    $(this).text(moreText);
                });
            });

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
        <script type="text/javascript" src="https://huntable.co.uk/js1/jquery-1.8.0.min.js"></script>
        <script type="text/javascript" src="https://huntable.co.uk/js1/jquery-ui-1.8.23.custom.min.js"></script>
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
        <style type="text/css">
            .jcarousel-skin-tango .jcarousel-container-horizontal
            {
                width: 575px;
            }
            .jcarousel-skin-tango .jcarousel-clip-horizontal
            {
                width: 580px;
                height: 105px;
            }
            .jcarousel-skin-tango .jcarousel-item-horizontal
            {
                margin-right: 19px;
            }
            .jcarousel-skin-tango .jcarousel-item
            {
                width: 136px;
                height: 100px;
            }
            .jcarousel-skin-tango .jcarousel-clip-horizontal
            {
                width: 560px;
            }
            .jcarousel-skin-tango .jcarousel-prev-horizontal, .jcarousel-skin-tango .jcarousel-next-horizontal
            {
                top: 55px;
            }
            
            
            .fancybox-custom .fancybox-skin
            {
                box-shadow: 0 0 50px #222;
            }
        </style>
        <!-- Employe follow script style begins -->
        <link href="https://huntable.co.uk/css/EmpFollow.css" type="text/css" rel="stylesheet" />
        <!-- Employe follow script style ends -->
        <!-- viewers viewed script style begins -->
        <link href="https://huntable.co.uk/css/portfolio-js-style.css" type="text/css" rel="stylesheet" />
        <!-- viewers viewed script style ends -->
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
        <asp:HiddenField ID="hdnUserId" runat="server" />
        <div id="dialog" title="Confirm Message">
            <asp:Label runat="server" Text="You are not logged In.Please login first." ID="lblConfirmMessage"></asp:Label>
        </div>
        <div id="ovrly" style="height: 30px; width: 100%; z-index: 2000; background-color: #FF9242;
            border: 1px solid #FF9242; border-radius: 2px; margin-top: 10px; display: none;">
            <div style="width: 980px; padding: 0px 10px; margin: 0px auto;">
                <div style="width: 30px; margin-left: 292px;">
                    <image src="https://huntable.co.uk/images/tick.png" width="25px" height="25px"></image>
                </div>
                <div style="margin-top: -20px">
                    <asp:Label Style="color: White; width: 200px; margin-top: -40px; margin-left: 377px;"
                        ID="pbl" runat="server"></asp:Label></div>
                <div style="margin-left: 621px; margin-top: -13px">
                    <image id="ximg" src="https://huntable.co.uk/images/orange-check-mark-md.png" width="10px"
                        height="10px"></image>
                </div>
            </div>
        </div>
        <div id="content-section">
            <div id="content-inner">
                <div class="accounts-profile2 ">
                    <div class="top-breadcrumb">
                        <div class="accounts-profile2-left">
                            <a href="#" class="accounts-link">Company</a>&nbsp;&rsaquo;&rsaquo;&nbsp; <strong>
                                <asp:Label ID="lblCompanyName" runat="server"></asp:Label></strong></div>
                        <a onclick="return rowAction01();" class="button-yellow top-minus" runat="server"
                            id="addyourbusinesspage">Add Your Business Page</a>
                    </div>
                    <div class="c-desc-blog">
                        <div class="c-desc-blog-left" align="center">
                            <a href="#">
                                <asp:Image ID="companylogoimg" runat="server" alt="Image not available" Width="304"
                                    Height="153" />
                            </a>
                            <br />
                            <br />
                            <div id="divLikeProfile" runat="server">
                                <img src="https://huntable.co.uk/images/like-icon.png" width="16" height="13" alt="like" />&nbsp;&nbsp;
                                <a href="#" class="accounts-link" runat="server" id="hypLikeProfileCount"><span id="lblLikeProfileCount"
                                    runat="server">20</span> People</a> <a href="#" class="accounts-link" runat="server"
                                        id="hypLikeProfile">like this company</a>
                            </div>
                            <div class="blue-box-company blue-box-company-info" id="div_follow" runat="server"
                                visible="false">
                                <asp:LinkButton OnClientClick="return rowAction01();" class="button-orange floatleft"
                                    Style="font-size: 12px; font-family: Arial, Helvetica, sans-serif; padding: 5px 15px;"
                                    ID="btn_follow" OnClick="Follow" Text="Follow" runat="server"> </asp:LinkButton><br />
                                <div style="margin-top: 11px;">
                                    <asp:Label ID="lblCompanyName3" runat="server"></asp:Label></div>
                            </div>
                            <div class="blue-box-company blue-box-company-info" id="div_following" runat="server"
                                visible="false">
                                <asp:LinkButton OnClientClick="return rowAction01();" class="button-orange floatleft"
                                    Style="font-size: 12px; font-family: Arial, Helvetica, sans-serif; padding: 5px 15px;"
                                    ID="btn_following" OnClick="Following" Text="Following" runat="server"> </asp:LinkButton><br />
                                <div style="margin-top: 11px;">
                                    <asp:Label ID="Label1" runat="server"></asp:Label></div>
                            </div>
                        </div>
                        <div class="c-desc-blog-mid">
                            <div class="c-desc-blog-mid-top">
                                <h1>
                                    <asp:Label ID="lblCompanyName1" runat="server"></asp:Label></h1>
                                <h3>
                                    <asp:Label ID="lblCompanyHeading" runat="server"></asp:Label></h3>
                                <div style="height: 50px">
                                    <p>
                                        <asp:Label ID="lblCompanyDescription" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="c-desc-blog-mid-bottom">
                                <h1 style="font-size: 14px;">
                                    <b>Business Info</b></h1>
                                <h3>
                                    <asp:Label ID="lblBusinessInformation" runat="server"></asp:Label></h3>
                                <p>
                                    <asp:Label ID="lblCompanyIndustry" runat="server"></asp:Label><br />
                                    <a id="a_comp" runat="server" target="_blank">
                                        <asp:Label ID="lblWebsite" runat="server"></asp:Label></a><br />
                                    <span>
                                        <asp:Label ID="lblPhoneNo" runat="server"></asp:Label></span>
                                </p>
                                <p>
                                    <a href="#" class="accounts-link">
                                        <asp:Label ID="lblNoofEmployees" runat="server">Employees</asp:Label></a>
                                    <br />
                                    <span>
                                        <asp:Label ID="lblAddress1" runat="server"></asp:Label>
                                        <asp:Label ID="lblAddress2" runat="server"></asp:Label>
                                        <asp:Label ID="lblTownCity" runat="server"></asp:Label><br />
                                        <asp:Label ID="lblcountry" runat="server"></asp:Label>
                                    </span>
                                </p>
                                <%--<div>
                                    <cc1:GMap ID="GMap1" runat="server" />
                                </div>--%>
                            </div>
                        </div>
                        <div class="c-desc-blog-right">
                            <div class="box-right1 box-right-company1">
                                <div class="r-heading">
                                    <uc3:followers runat="server"></uc3:followers>
                                </div>
                            </div>
                        </div>
                    </div>
                    <ul class="overview-tab" style="width: 760px;">
                        <li><a id="overview" runat="server" class="selected-tab">Overview</a></li>
                        <li><a id="activity" runat="server">Activity</a></li>
                        <li><a id="productsandservices" runat="server">Products &amp; Services</a></li>
                        <li><a id="careers" runat="server">Careers</a></li>
                        <li><a id="busunessblog" runat="server">Business Blog</a></li>
                        <li><a id="article" runat="server">Article</a></li>
                        <uc1:message runat="server" />
                    </ul>
                </div>
                <div class="content-inner-left">
                    <script type="text/javascript">
                        function tooltipContent(selected) {
                            $('.heading-font').hide();
                            $('#tooltip-content' + selected).show();
                        }
                    </script>
                    <div class="gallery-company">
                        <div style="width: 107px; float: left;" class="achivement achivement-top">
                            <img src="https://huntable.co.uk/images/portfolio-icon1.jpg" width="13" height="16"
                                alt="portfolio-icon" />
                            <a class="port-link" href="#">Portfolio</a> <a href="#">
                                <img width="23" height="23" alt="edit" src="https://huntable.co.uk/images/edit-icon.jpg" /></a>
                        </div>
                        <div class=" jcarousel-skin-tango">
                            <div class="jcarousel-container jcarousel-container-horizontal" style="position: relative;
                                display: block;">
                                <div class="jcarousel-clip jcarousel-clip-horizontal" style="position: relative;">
                                    <ul class="first-and-second-carousel jcarousel-list jcarousel-list-horizontal" id="first-carousel"
                                        style="overflow: hidden; position: relative; top: 0px; margin: 0px; padding: 0px;
                                        left: 0px; width: auto">
                                        <asp:Repeater ID="dlr" runat="server">
                                            <ItemTemplate>
                                                <li class="jcarousel-item jcarousel-item-horizontal jcarousel-item-1 jcarousel-item-1-horizontal"
                                                    style="float: left; list-style: none outside none;" jcarouselindex="1"><a href="#">
                                                        <asp:ImageButton ID="portfolioimg" onmouseover="tooltipContent(1);" runat="server"
                                                            alt="portfolio" Width="100" Height="100" ImageUrl='<%#CompanyPortfolioPicture(Eval("PortfolioImageid"))%>'
                                                            OnClick="dispic" CommandArgument='<%#Eval("Id") %>' />
                                                    </a></li>
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
                    </div>
                    <div class="more-less">
                        <div class="more-block">
                            <p class="heading-font" id="tooltip-content1" style="display: block;">
                                <asp:DataList ID="dlPortfolioDescription" runat="server" RepeatDirection="Horizontal"
                                    RepeatColumns="6">
                                    <ItemTemplate>
                                        <%-- <asp:Label runat="server" ID="lblPortfolioDescription" Text='<%#Eval("PortfolioDescription") %>'></asp:Label--%>>
                                        <%-- <a href="#" class="accounts-link"
                            style="font-size: 12px;">More</a>--%>
                                    </ItemTemplate>
                                </asp:DataList>
                            </p>
                        </div>
                    </div>
                    <asp:Label ID="deslbl1" runat="server"></asp:Label>
                    <asp:Label ID="deslbl" runat="server"></asp:Label>
                </div>
                <div class="block-border-right">
                    <div class="content-inner-right">
                        <uc4:portImages runat="server" />
                    </div>
                    <div style="margin-top: -219;">
                        <div class="box-right" id="div_viwers" runat="server">
                            <div class="head-ash" style="text-align: center;">
                                <h3>
                                    Viewers also viewed</h3>
                            </div>
                            <div style="overflow-y: scroll; overflow-x: hidden; height: 238px">
                                <asp:UpdatePanel ID="Update_Panel" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" Value="" ID="Hidden_Field" />
                                        <asp:DataList runat="server" ID="dlview" RepeatDirection="Horizontal" RepeatColumns="2">
                                            <ItemTemplate>
                                                <a href='<%#UrlGenerator(Eval("Id")) %>' runat="server" id="a_compview">
                                                    <asp:Image runat="server" ID="image1" Width="128px" Height="74px" ImageUrl='<%#Picture(Eval("CompanyLogoId")) %>' />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                                </a>
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <asp:Button runat="server" ID="btn_more" Text=">see more" OnClick="click_seemore"
                                            Style="margin-left: 233px; color: #008CA1; border: #D5E2E2;" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <asp:Label runat="server" ID="lblMessage"></asp:Label>
                </div>
            <div class="block-border" style="width: 650px;">
                <div class="block-border-left">
                    <div class="employers-follow">
                        <h3>
                            <asp:Label ID="lblCompanyName4" runat="server"></asp:Label>
                            employee you may want to follow</h3>
                        <asp:UpdatePanel ID="Update1" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField runat="server" Value="" ID="hfemplyee" />
                                <asp:DataList ID="dlCompanyEmployees" runat="server" RepeatDirection="Horizontal"
                                    RepeatColumns="3">
                                    <ItemTemplate>
                                        <table border="0" width="100%" style="margin-bottom: 10px;">
                                            <tr>
                                                <td width="58" valign="top">
                                                    <a href='<%#UserUrlGenerator(Eval("Id")) %>' runat="server">
                                                        <asp:Image ID="userimg" runat="server" alt="Feaured-logo" Width="108px" Height="74px"
                                                            ImageUrl='<%#Picture(Eval("PersonalLogoFileStoreId"))%>' /></a>
                                                </td>
                                                <td width="222" valign="top">
                                                    <strong><a class="accounts-link" href="#"><a href='<%#UserUrlGenerator(Eval("Id")) %>'
                                                        runat="server">
                                                        <asp:Label ID="lblfirstName" Text='<%#Eval("FirstName")%>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblLastname" Text='<%#Eval("LastName")%>' runat="server"></asp:Label></a>
                                                    </a></strong>
                                                    <p style="padding: 3px 0px;">
                                                        <asp:Label ID="lbljobTitle" Text='<%#JobTitle(Eval("Id"))%>' runat="server"></asp:Label></p>
                                                    <div id="DivFollowing" runat="server" visible='<%#IsThisUserFollowingCompany(Eval("Id"))%>'>
                                                        <asp:LinkButton ID="linkButtonFollowing" Text="Following" CommandArgument='<%#Eval("Id")%>'
                                                            OnCommand="CommandCompanyEmployeeUnFollowClick" OnClientClick="return rowAction01();"
                                                            runat="server" CssClass="share" Style="padding: 5px 7px; width: 51px; float: left;"
                                                            class="invite-friend-btn floatleft1"></asp:LinkButton>
                                                    </div>
                                                    <div id="DivFollow" runat="server" visible='<%#!IsThisUserFollowingCompany(Eval("Id"))%>'>
                                                        <asp:LinkButton ID="linkButtonFollow" OnCommand="CommandCompanyEmployeeFollowClick"
                                                            Text="Follow" CommandArgument='<%#Eval("Id")%>' runat="server" CssClass="share"
                                                            OnClientClick="return rowAction01();" class="invite-friend-btn floatleft1" Style="padding: 5px 7px;
                                                            width: 45px; float: left;"></asp:LinkButton>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblEmpty" Text="No employees found for this company" runat="server"
                                            Visible='<%#bool.Parse((dlCompanyEmployees.Items.Count==0).ToString())%>'>
                                        </asp:Label>
                                    </FooterTemplate>
                                </asp:DataList>
                                <asp:Button runat="server" ID="btnpanel" Text=">See More" OnClick="seemore_click"
                                    Style="color: #008CA1; border: #D5E2E2; float: right;" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Label runat="server" ID="lblUpdate" Text="Panel Updated  " Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
            </div>
        </div>
        <!-- content inner ends -->
        <!-- content section ends -->
        <!-- Range Slider Script Begins -->
        <!-- Footer section ends -->
        <!-- Popup Div begins -->
        <div id="popup13" class="popup_block">
            <div class="apply-job ">
                <h1>
                    <asp:Label ID="lblCompanyName2" runat="server"></asp:Label>
                </h1>
                <strong>
                    <asp:Label ID="lblCompanyHeading1" runat="server"></asp:Label></strong>
                <div class="map-block">
                    <div class="map-block-left">
                    </div>
                </div>
            </div>
        </div>
        <!-- Popup Div ends -->
        <!-- Image Carousel Script Begins -->
        <script type="text/javascript" src="https://huntable.co.uk/js/jquery.jcarousel.min.js"></script>
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
                    $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="https://huntable.co.uk/images/close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>');

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
        <!-- Popup Script Ends -->
        <!--Infosys employee gallery script begins -->
        <script type="text/javascript" language="javascript" src="https://huntable.co.uk/js/jquery.carouFredSel-6.0.3-packed.js"></script>
        <!-- optionally include helper plugins -->
        <script type="text/javascript" language="javascript" src="https://huntable.co.uk/js/jquery.mousewheel.min.js"></script>
        <script type="text/javascript" language="javascript" src="https://huntable.co.uk/js/jquery.touchSwipe.min.js"></script>
        <!-- fire plugin onDocumentReady -->
        <script type="text/javascript" language="javascript">
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
                $('#foo2').carouFredSel({
                    auto: false,
                    prev: '#prev2',
                    next: '#next2',
                    pagination: "#pager2",
                    auto: true,
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
                    scroll: 2
                });

            });
        </script>
        <!--Infosys employee gallery script ends -->
    </body>
    </html>
    <script type="text/javascript" src="https://huntable.co.uk/js/jquery.jcarousel.min.js"></script>
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
    <link href="https://huntable.co.uk/js/nyroModal/styles/nyroModal.css" rel="stylesheet"
        type="text/css" />
    <script src="https://huntable.co.uk/js/nyroModal/js/jquery.nyroModal.custom.min.js"
        type="text/javascript"></script>
    <!--[if IE 6]>
	<script type="text/javascript" src="js/jquery.nyroModal-ie6.min.js"></script>
<![endif]-->
    <script type="text/javascript">
        function MarkDirectLike(feedId, likeType, refId) {
            if (CheckIfUserLoggedIn()) {
                $.ajax({
                    type: "POST",
                    url: "/UserFeedService.asmx/MarkDirectLike",
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
                    url: "/UserFeedService.asmx/MarkDirectUnlike",
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
