<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CompanyProducts.aspx.cs" Inherits="Huntable.UI.CompanyProducts" %>

<%@ Register Src="~/UserControls/Messagepopup.ascx" TagName="mesgpopup" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Followers(6).ascx" TagName="followers_6" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/PortfolioImages.ascx" TagName="port" TagPrefix="uc3" %>
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
            $(function () {

                var msie6 = $.browser == 'msie' && $.browser.version < 7;
                if (!msie6) {
                    var top = $('#bx10').offset().top;
                    $(window).scroll(function (event) {
                        var y = $(this).scrollTop();
                        if (y >= 50) { $('#bx10').addClass('fixed'); }
                        else { $('#bx10').removeClass('fixed'); }
                    });
                }
            });</script>
        <!-- portfolio script style begins -->
        <link href="css/portfolio-js-style.css" type="text/css" rel="stylesheet" />
        <!-- portfolio script style ends -->
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
                    <image src="images/tick.png" width="25px" height="25px"></image>
                </div>
                <div style="margin-top: -20px">
                    <asp:Label Style="color: White; width: 200px; margin-top: -40px; margin-left: 377px;"
                        ID="pbl" runat="server"></asp:Label></div>
                <div style="margin-left: 621px; margin-top: -13px">
                    <image id="ximg" src="images/orange-check-mark-md.png" width="10px" height="10px"></image>
                </div>
            </div>
        </div>
        <div id="content-section">
            <div id="content-inner">
                <div class="accounts-profile2">
                    <div class="top-breadcrumb">
                        <div class="accounts-profile2-left">
                            <asp:DataList ID="dlcompname" runat="server">
                                <ItemTemplate>
                                    <a href="companieshome.aspx" class="accounts-link">Company</a>&nbsp;&rsaquo;&rsaquo;&nbsp;<a
                                        href='<%#UrlGenerator(Eval("Id")) %>' class="accounts-link"><asp:Label
                                            Text='<%#Eval("CompanyName") %>' runat="server"></asp:Label></a>&nbsp;&rsaquo;&rsaquo;&nbsp;<strong>Products
                                                &amp; Services</strong></div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <div id="bx10">
                            <ul class="overview-tab">
                                <li><a id="overview" runat="server">Overview</a></li>
                                <li><a id="activity" runat="server">Activity</a></li>
                                <li><a id="productsandservices" runat="server" class="selected-tab">Products &amp; Services</a></li>
                                <li><a id="careers" runat="server">Careers</a></li>
                                <li><a id="busunessblog" runat="server">Business Blog</a></li>
                                <li><a id="article" runat="server">Article</a></li>
                                <uc1:mesgpopup ID="mesgpopup" runat="server" />
                            </ul>
                        </div>
                    </div>
                    <div>
                        <div class="content-inner-left" style="margin-top: 50px;">
                            <div class="notification">
                                <div class="all-feeds-main-inner all-feeds-main-inner-company">
                                    <div id="tabswitch tabswitch1">
                                        <div class="tab-container tab1" style="display: block; padding-left: 0px; width: 654px;">
                                            <div class="all-feeds-list">
                                                <br />
                                                <asp:Label runat="server" ID="lblmsg" Visible="false" Text="No data Found" Font-Size="25px"></asp:Label>
                                                <asp:Image runat="server" ID="prdtimg" Width="630px" Height="300px" /><br />
                                                <h2 style="font-size: 24px;" class="login-heading" id="prooverview" runat="server">
                                                    Product Overview</h2>
                                                <p>
                                                    <asp:Label runat="server" ID="lblDescription"></asp:Label></p>
                                                <%--	<div>
                            <div align="center">
                            <img width="630" height="300" src="images/coke.jpg" />
                            </div>
                            <br />

<h2 style="margin-bottom:10px; font-size:24px;" class="login-heading">Product Overview</h2>

                            <p> Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.</p>
                   <br />
         <p> Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.</p><br />

                            </div>--%>
                                                <br />
                                                <div class="feed-right">
                                                    <asp:Literal ID="litLikeComment" runat="server"></asp:Literal>
                                                    <%--<div class="like-portion">
                                                        <a style="margin-left: 0px;" href="#">
                                                            <img width="13" height="12" alt="Like" src="images/icon-like.png" />Like</a>
                                                        <a href="#">
                                                            <img width="13" height="12" alt="Like" src="images/icon-comment.png" />Comment</a>
                                                        <span>5 Minutes ago</span>
                                                        <div class="comments">
                                                            <div class="comments-head">
                                                                <img width="13" height="12" alt="comments" style="margin-top: 2px;" src="images/icon-like1.png" /><span
                                                                    style="float: left; margin: 0px;">You and&nbsp; </span><a href="#?w=300" class="accounts-link poplight"
                                                                        rel="popup13">2 others</a>&nbsp;like this
                                                            </div>
                                                            <div class="comments-desc">
                                                                <div class="comments-desc-left">
                                                                    <a href="#">
                                                                        <img width="46" height="45" alt="img" src="images/profile-thumb-small.jpg" />
                                                                    </a>
                                                                </div>
                                                                <div class="comments-desc-right">
                                                                    <textarea class="textarea-profile textarea-comment" onfocus="if(this.value==this.defaultValue)this.value='';"
                                                                        onblur="if(this.value=='')this.value=this.defaultValue;">Write a comment...</textarea>
                                                                    <br />
                                                                    <br />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                                <div class="next-block">
                                                    <a id="a_next" runat="server" class="button-green button-green-next">Next</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="content-inner-right" style="margin-top: 50px;">
                            <div class="blue-box-company" runat="server" id="div_follow" visible="false">
                                <asp:Button ID="btn_follow" CssClass="button-orange button-orange-fc" Style="margin-left: 85px;"
                                    Text="Follow" OnClientClick="return rowAction01();" runat="server" OnClick="Follow" />
                                <br />
                                <br />
                                <p>
                                    <asp:Label ID="Label1" Style="margin-left: 5px;" runat="server"></asp:Label>
                                    and get upto date info about this company and get their jobs straight into your
                                    feeds
                                </p>
                                <div class="button-green-company" align="center">
                                </div>
                            </div>
                            <div class="blue-box-company" runat="server" id="div_following" visible="false">
                                <br />
                                <br />
                                <asp:Button ID="btn_following" CssClass="button-orange button-orange-fc" Style="margin-left: 85px;"
                                    Text="Following" OnClientClick="return rowAction01();" runat="server" OnClick="Following" /><br />
                                <br />
                                <br />
                                <p>
                                    <asp:Label ID="Label2" Style="margin-left: 5px;" runat="server"></asp:Label>
                                    and get upto date info about this company and get their jobs straight into your
                                    feeds
                                </p>
                                <div class="button-green-company" align="center">
                                </div>
                            </div>
                            <p class="margin-top-visible">
                                &nbsp;</p>
                            <div class="box-right box-right02">
                                <uc2:followers_6 runat="server" />
                                <uc3:port runat="server"></uc3:port>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- content inner ends -->
            </div>
            <!-- content section ends -->
            <!-- Popup Div begins -->
            <div id="popup13" class="popup_block">
                <div class="apply-job ">
                    <div class="box-right">
                        <div class="head-ash">
                            <h3>
                                User's Who like this</h3>
                        </div>
                        <div class="want-to-follow-list">
                            <div class="want-to-follow-list-left">
                                <a href="#">
                                    <img alt="Invite-friends-img" class="profile-pic profile-pic2" src="images/profile-thumb-small.jpg" /></a>
                            </div>
                            <div class="want-to-follow-list-middle">
                                <strong><a href="#" class="accounts-link">Frida Dahlstorm</a></strong>
                                <p>
                                    <a href="#" class="accounts-link">Senior Software engineer</a> at <a href="#" class="accounts-link">
                                        Mahindra Satyam</a></p>
                            </div>
                            <div class="want-to-follow-list-right">
                                <a class="invite-friend-btn" href="#">Follow +</a>
                            </div>
                        </div>
                        <div class="want-to-follow-list">
                            <div class="want-to-follow-list-left">
                                <a href="#">
                                    <img alt="Invite-friends-img" class="profile-pic profile-pic2" src="images/profile-thumb-small.jpg" /></a>
                            </div>
                            <div class="want-to-follow-list-middle">
                                <strong><a href="#" class="accounts-link">Frida Dahlstorm</a></strong>
                                <p>
                                    <a href="#" class="accounts-link">Senior Software engineer</a> at <a href="#" class="accounts-link">
                                        Mahindra Satyam</a></p>
                            </div>
                            <div class="want-to-follow-list-right">
                                <a class="invite-friend-btn" href="#">Follow +</a>
                            </div>
                        </div>
                        <div class="want-to-follow-list">
                            <div class="want-to-follow-list-left">
                                <a href="#">
                                    <img alt="Invite-friends-img" class="profile-pic profile-pic2" src="images/profile-thumb-small.jpg" /></a>
                            </div>
                            <div class="want-to-follow-list-middle">
                                <strong><a href="#" class="accounts-link">Frida Dahlstorm</a></strong>
                                <p>
                                    <a href="#" class="accounts-link">Senior Software engineer</a> at <a href="#" class="accounts-link">
                                        Mahindra Satyam</a></p>
                            </div>
                            <div class="want-to-follow-list-right">
                                <a class="invite-friend-btn" href="#">Follow +</a>
                            </div>
                        </div>
                        <div class="want-to-follow-list">
                            <div class="want-to-follow-list-left">
                                <a href="#">
                                    <img alt="Invite-friends-img" class="profile-pic profile-pic2" src="images/profile-thumb-small.jpg" /></a>
                            </div>
                            <div class="want-to-follow-list-middle">
                                <strong><a href="#" class="accounts-link">Frida Dahlstorm</a></strong>
                                <p>
                                    <a href="#" class="accounts-link">Senior Software engineer</a> at <a href="#" class="accounts-link">
                                        Mahindra Satyam</a></p>
                            </div>
                            <div class="want-to-follow-list-right">
                                <a class="invite-friend-btn" href="#">Follow +</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Popup Div ends -->
            <!-- Range Slider Script Begins -->
            <!--<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>-->
            <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
                type="text/javascript"></script>
            <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
            <script type="text/javascript" src="js/flipcounter.js"></script>
            <!-- Range Slider Script Ends -->
            <!-- Footer section ends -->
            <!--Infosys employee gallery script begins -->
            <script type="text/javascript" language="javascript" src="js/jquery.carouFredSel-6.0.3-packed.js"></script>
            <!-- optionally include helper plugins -->
            <script type="text/javascript" language="javascript" src="js/jquery.mousewheel.min.js"></script>
            <script type="text/javascript" language="javascript" src="js/jquery.touchSwipe.min.js"></script>
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
            <!-- Popup Script Ends -->
            <input type="hidden" id="hdnUserImage" runat="server" value="0" />
    <script src="js/UserFeed.js" type="text/javascript"></script>
           
            
     <link href="js/nyroModal/styles/nyroModal.css" rel="stylesheet" type="text/css" />
    <script src="js/nyroModal/js/jquery.nyroModal.custom.min.js" type="text/javascript"></script>
    <!--[if IE 6]>
	<script type="text/javascript" src="js/jquery.nyroModal-ie6.min.js"></script>
<![endif]-->
    </body>
    </html>
    </div> </div> </div>
</asp:Content>
