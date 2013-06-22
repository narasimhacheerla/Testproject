<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BusinessActivity.aspx.cs" Inherits="Huntable.UI.BusinessActivity" %>

<%@ Register Src="~/UserControls/Messagepopup.ascx" TagName="Mesgpopup" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Followers(6).ascx" TagName="flw" TagPrefix="uc" %>
<%@ Register Src="UserControls/UserFeedList.ascx" TagName="UserFeedList" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/CompaniesYouMayBeIntrested.ascx" TagName ="cmpint" TagPrefix ="uc3" %>
<%@ Register Src="~/UserControls/ViewersOfCompanyAlso.ascx" TagName ="cmpview" TagPrefix ="uc4" %>
<%@ Register Src="~/UserControls/InfosysEmployees(3).ascx" TagName ="infview" TagPrefix ="uc5" %>
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
            $(function () {

                var msie6 = $.browser == 'msie' && $.browser.version < 7;
                if (!msie6) {
                    var top = $('#bx11').offset().top;
                    $(window).scroll(function (event) {
                        var y = $(this).scrollTop();
                        if (y >= 1280) { $('#bx11').addClass('fixed'); }
                        else { $('#bx11').removeClass('fixed'); }
                    });
                }
            });</script>
        <!-- viewers viewed script style begins -->
        <link href="css/portfolio-js-style.css" type="text/css" rel="stylesheet" />
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

        <!-- viewers viewed script style ends -->
        <link rel="stylesheet" type="text/css" href="css/jquery.fancybox-picture.css?v=2.1.2"
            media="screen" />
    </head>
    <body>
    <asp:HiddenField ID="hdnUserId" runat="server" />
 <div id="dialog" title="Confirm Message">
    <asp:Label runat="server" Text="You are not logged In.Please login first." ID="lblConfirmMessage"></asp:Label>
    </div>
    <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>


        <div id="content-section">
            <div id="content-inner">
                <div class="accounts-profile2 ">
                    <div class="top-breadcrumb">
                        <div class="accounts-profile2-left">
                            <a href="companieshome.aspx" class="accounts-link">Company</a>&nbsp;&rsaquo;&rsaquo;&nbsp;<a id="cmpvw" runat="server" href="#"
                                class="accounts-link"><asp:Label runat="server" ID="lblcname"></asp:Label> </a>&nbsp;&rsaquo;&rsaquo;&nbsp;<strong>Activity</strong></div>
                        <a href="Company-registration.aspx" class="button-yellow top-minus" onclick="return rowAction01();">Add Your Business Page</a>
                    </div>
                    <ul class="overview-tab">
                        <li><a id="overview" runat="server">Overview</a></li>
                        <li><a id="activity" runat="server" class="selected-tab">Activity</a></li>
                        <li><a id="productsandservices" runat="server">Products &amp; Services</a></li>
                        <li><a id="careers" runat="server">Careers</a></li>
                        <li><a id="busunessblog" runat="server">Business Blog</a></li>
                        <li><a id="article" runat="server">Article</a></li>
                        <uc1:Mesgpopup ID="mesgpopup" runat="server" />
                    </ul>
                </div>
                <div class="content-inner-left">
                    <div class="notification">
                        <div class="all-feeds-main-inner all-feeds-main-inner-company">
                            <div id="tabswitch tabswitch1">
                                <div class="tab-container tab1" style="display: block; padding-left: 0px; width: 654px;">
                                    <div class="all-feeds-list">
                                        <div class="general-setting-tab1 general-setting-tab1-overview">
                                            <a class="setting-active1" href="#" id="bact" runat="server">Business Activity</a> <a
                                                href="#" id="eact" runat="server" >Employee Activity</a>
                                        </div>
                                    </div>
                                   <uc1:UserFeedList ID="UserFeedList1" runat="server" PageType="Business_Activity"/>
                                 
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="content-inner-right" style="height:1500px">
                    <div id="dvcmp" runat="server" class="blue-box-company1">
                          <asp:Button  OnClientClick="return rowAction01();" class="button-orange button-orange-fc" runat="server" id="follo" OnClick ="follow_click"  Text="Follow" ></asp:Button><br />
                       <asp:Button  OnClientClick="return rowAction01();" class="button-orange button-orange-fc" runat="server" id="Unfollo" OnClick="unfollow_Click" Text="Following" ></asp:Button><br />
                        <br />
                        <br />
                        <asp:Label runat="server" ID="lblcmpny"></asp:Label> and get upto date info about this company and get their jobs straight into
                        your feeds
                        <div class="button-green-company" align="center">
                        </div>
                    </div>
                    <div class="box-right box-right02">
                        <div class="r-heading">
                           
                        </div>
                        <uc:flw ID="flwr" runat="server" />
                    </div>
                       <uc3:cmpint ID="compint" runat="server" />
                    <p class="margin-top-visible">
                        &nbsp;</p>
                       <uc4:cmpview id="cmview" runat ="server"></uc4:cmpview>
                    <p class="margin-top-visible">
                        &nbsp;</p>
                        <br /><br /><br />
                    <div id="bx11">
                       <uc5:infview id ="info" runat ="server"></uc5:infview>
               
                    </div>
                </div>
            </div>
        </div>
        <!--viewers views gallery script begins -->
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
                    pagination: "#pager4",
                    auto: true,
                    mousewheel: false,
                    scroll: 2,
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
     <link href="js/nyroModal/styles/nyroModal.css" rel="stylesheet" type="text/css" />
    <script src="js/nyroModal/js/jquery.nyroModal.custom.min.js" type="text/javascript"></script>
    <!--[if IE 6]>
	<script type="text/javascript" src="js/jquery.nyroModal-ie6.min.js"></script>
<![endif]-->
    </body>
    </html>
    </div>
</asp:Content>
