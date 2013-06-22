<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopNewsDesign.ascx.cs"
    Inherits="Huntable.UI.UserControls.TopNewsDesign" %>
<script language="javascript" type="text/javascript">
    var link1 = '<%=Links[0] %>';
    var link2 = '<%=Links[1] %>';
    var link3 = '<%=Links[2] %>';
    var link4 = '<%=Links[3] %>';
    var link5 = '<%=Links[4] %>';
    var link6 = '<%=Links[5] %>';
    var link7 = '<%=Links[6] %>';
    var link8 = '<%=Links[7] %>';
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Untitled Document</title>
    <link href="../Styles/NewStyle.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../Styles/NewsSkin.css" />
</head>
<body>
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
                            <ul id="first-carousel" class="first-and-second-carousel jcarousel-skin-tango" style="width: -20px">
                                <li>
                                    <asp:ImageButton ID="imgNews1" CssClass="profile-pic" Width="120" Height="120" AlternateText="top-news-img"
                                        runat="server" OnClientClick="javascript:window.open(link1,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;" />
                                    <p>
                                        <asp:LinkButton ID="lnkApple" runat="server" CssClass="LinkButton" OnClientClick="javascript:window.open(link1,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                                </li>
                                <li>
                                    <asp:ImageButton ID="imgNews2" CssClass="profile-pic" Width="120" Height="120" AlternateText="top-news-img"
                                        runat="server" OnClientClick="javascript:window.open(link2,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;" />
                                    <p>
                                        <asp:LinkButton ID="lnkMac" runat="server" ForeColor="Black" CssClass="LinkButton"
                                            OnClientClick="javascript:window.open(link2,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                                </li>
                                <li>
                                    <asp:ImageButton ID="imgNews3" CssClass="profile-pic" Width="120" Height="120" AlternateText="top-news-img"
                                        runat="server" OnClientClick="javascript:window.open(link3,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;" />
                                    <p>
                                        <asp:LinkButton ID="lnkErrorMessages" runat="server" ForeColor="Black" CssClass="LinkButton"
                                            OnClientClick="javascript:window.open(link3,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                                </li>
                                <li>
                                    <asp:ImageButton ID="imgNews6" CssClass="profile-pic" Width="120" Height="120" AlternateText="top-news-img"
                                        runat="server" OnClientClick="javascript:window.open(link4,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;" />
                                    <p>
                                        <asp:LinkButton ID="lnkMac1" runat="server" ForeColor="Black" CssClass="LinkButton"
                                            OnClientClick="javascript:window.open(link4,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                                </li>
                                <li>
                                    <asp:ImageButton ID="imgNews10" CssClass="profile-pic" Width="120" Height="120" AlternateText="top-news-img"
                                        runat="server"  OnClientClick="javascript:window.open(link5,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;" />
                                    <p>
                                        <asp:LinkButton ID="lnkEM" runat="server" ForeColor="Black" CssClass="LinkButton"
                                            OnClientClick="javascript:window.open(link5,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                                </li>
                                <li>
                                    <asp:ImageButton ID="Image1" CssClass="profile-pic" Width="120" Height="120" AlternateText="top-news-img"
                                        runat="server"  OnClientClick="javascript:window.open(link6,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;" />
                                    <p>
                                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Black" CssClass="LinkButton"
                                            OnClientClick="javascript:window.open(link6,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                                </li>
                                <li>
                                    <asp:ImageButton ID="Image2" CssClass="profile-pic" Width="120" Height="120" AlternateText="top-news-img"
                                        runat="server"  OnClientClick="javascript:window.open(link7,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"/>
                                    <p>
                                        <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="Black" CssClass="LinkButton"
                                            OnClientClick="javascript:window.open(link7,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                                </li>
                                <li>
                                    <asp:ImageButton ID="Image3" CssClass="profile-pic" Width="120" Height="120" AlternateText="top-news-img"
                                        runat="server"  OnClientClick="javascript:window.open(link8,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;" />
                                    <p>
                                        <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Black" CssClass="LinkButton"
                                            OnClientClick="javascript:window.open(link8,'_blank','width=500,height=400,resizable=yes,scrollbars=yes,top=140,left=300'); return false;"></asp:LinkButton></p>
                                </li>
                                <%-- <li class="top-news-arrow"><a href="#">
                                    <img src="../images/top-news-tab-arrow.png" width="15" height="20" alt="arrow" /></a>
                                </li>--%>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
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
</body>
</html>
