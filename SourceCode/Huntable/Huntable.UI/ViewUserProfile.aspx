<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="ViewUserProfile.aspx.cs" Inherits="Huntable.UI.ViewUserProfile" %>

<%@ Register Src="~/UserControls/CvStatistics.ascx" TagPrefix="uc1" TagName="CVStats" %>
<%@ Register Src="~/UserControls/ViewersOfThisProfile.ascx" TagPrefix="uc2" TagName="your" %>
<%@ Register Src="~/UserControls/Messagepopup.ascx" TagName="mesgpopup" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/SeeYourFriendsInvitations.ascx" TagName="seeyourfriends"
    TagPrefix="uc3" %>
<asp:Content ID="content2" ContentPlaceHolderID="headContent" runat="server">
    <link type="text/css" href="https://huntable.co.uk/css1/jquery-ui-1.8.23.custom.css"
        rel="stylesheet" />
    <link href="https://huntable.co.uk/css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <link href="https://huntable.co.uk/JqueryPopup/jquery-ui-1.7.2.custom.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-latest.js"></script>
    <script src="https://huntable.co.uk/Scripts/fancybox/lightbox1.js" type="text/javascript"></script>
    <link href="https://huntable.co.uk/css/skin2.css" rel="stylesheet" type="text/css" />
    <link href="https://huntable.co.uk/css/skin3.css" rel="stylesheet" type="text/css" />
    <link href="https://huntable.co.uk/Scripts/fancybox/lightbox1.css" rel="stylesheet" />
    <link href="https://huntable.co.uk/JqueryPopup/jquery-ui-1.7.2.custom.css" rel="stylesheet"
        type="text/css" />
           <script src="https://huntable.co.uk/Scripts/fancybox/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="https://huntable.co.uk/Scripts/fancybox/lightbox1.js" type="text/javascript"></script>
    <link href="https://huntable.co.uk/Scripts/fancybox/lightbox1.css" rel="stylesheet" />
    <%--<script type="text/javascript" src="https://huntable.co.uk/js/jquery.blockUI.js"></script>--%>
    <script type="" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js">
    </script>
    <link rel="stylesheet" type="text/css" href="https://huntable.co.uk/css/jquery.fancybox-picture.css?v=2.1.2"
        media="screen" />
    <style type="text/css">
        .jcarousel-skin-tango .jcarousel-container-horizontal
        {
            width: 585px;
        }
        .jcarousel-skin-tango .jcarousel-clip-horizontal
        {
            width: 570px;
            height: 120px;
        }
        .jcarousel-skin-tango .jcarousel-item-horizontal
        {
            margin-right: 19px;
        }
        .jcarousel-skin-tango .jcarousel-item-h
        {
            width: 102px;
            height: 110px;
        }
        
        .jcarousel-skin-tango .jcarousel-clip-horizontal
        {
            width: 570px;
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
    <script type="text/javascript">

        $(document).ready(function () {

            //Select all anchor tag with rel set to tooltip
            $('a[rel=tooltip]').mouseover(function (e) {

                //Grab the title attribute's value and assign it to a variable
                var tip = $(this).attr('title');

                //Remove the title attribute's to avoid the native tooltip from the browser
                $(this).attr('title', '');

                //Append the tooltip template and its value
                $(this).append('<div id="tooltip"><div class="tipHeader"></div><div class="tipBody">' + tip + '</div><div class="tipFooter"></div></div>');

                //Show the tooltip with faceIn effect
                $('#tooltip').fadeIn('500');
                $('#tooltip').fadeTo('10', 0.9);

            }).mousemove(function (e) {

                //Keep changing the X and Y axis for the tooltip, thus, the tooltip move along with the mouse
                $('#tooltip').css('top', e.pageY + 10);
                $('#tooltip').css('left', e.pageX + 20);

            }).mouseout(function () {

                //Put back the title attribute's value
                $(this).attr('title', $('.tipBody').html());

                //Remove the appended tooltip template
                $(this).children('div#tooltip').remove();

            });

        });

    </script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        function overlay(id) {
            el = document.getElementById('ovrly');
            $('#ovrly').show();

            $('#<%= pbl.ClientID %>').text(id);

        }

        $(document).ready(function () {

            $('#ximg').click(function () {
                $('#ovrly').hide();
                return false;
            });

            jQuery.noConflict();
            var clip = new ZeroClipboard.Client();
            jQuery("#tabswitch ul li:first").addClass("active");
            jQuery("#tabswitch div.tab-container:first").show();
            jQuery("#tabswitch ul li").click(function () {
                jQuery("#tabswitch div.tab-container").hide();
                jQuery("#tabswitch ul li").removeClass("active");
                var tab_class = jQuery(this).attr("class");
                jQuery("#tabswitch div." + tab_class).show();
                jQuery("#tabswitch ul li." + tab_class).addClass("active");


                clip.setText(''); // will be set later on mouseDown
                clip.setHandCursor(true);
                clip.addEventListener('load', function (client) {
                    //  alert("movie is loaded");
                });
                clip.addEventListener('complete', function (client, text) {
                    //  alert("Copied text to clipboard: " + text);
                });
                clip.addEventListener('mouseDown', function (client) {
                    // set text to copy here
                    clip.setText(document.getElementById('fe_text').value);
                    //    alert("Copied text to clipboard: " + text);
                });

                clip.glue('d_clip_button');
                geturl();
                getTxt();
            });
        });
    </script>
    <script type="text/javascript">
        $().ready(function () {
            $('#confirmationDialog').dialog({
                autoOpen: false,
                modal: true,
                bgiframe: true,
                title: "Request Endorse",
                width: 500,
                height: 300
            });
            $('#confirmationDialog').parent().appendTo($("form:first"));
            $('#confirmationDialog_Block').dialog({
                autoOpen: false,
                modal: true,
                bgiframe: true,
                title: "Block User",
                width: 500,
                height: 300
            });
            $('#confirmationDialog_Block').parent().appendTo($("form:first"));
            $('#divEndorse').dialog({
                autoOpen: false,
                modal: true,
                bgiframe: true,
                title: "Endorse Now",
                width: 800,
                height: 400
            });
            $('#divEndorse').parent().appendTo($("form:first"));
        });


        $().ready(function () {
            $('#dialogContent').dialog({
                autoOpen: false,
                modal: true,
                bgiframe: true,
                title: "Send Message",
                width: 600,
                height: 350
            });
            $('#dialogContent').parent().appendTo($("form:first"));
        });
        var empMoreClicks = 1;
        function employmentHistoryMore(e, lnk) {
            //empMoreClicks++;
            var es = e.split('~');
            var count = 0;
            var strEmp = "";
            $.each(es, function (index, value) {
                count++;
                //if (count <= empMoreClicks * 2) {
                strEmp += value;
                //}
            });
            $('#MainContent_lblPastPosition').html(strEmp.substr(0, strEmp.length - 7)); // + "&nbsp;<a href='javascript:employmentHistoryMore(\"" + e + "\")'>More</a>"
        }
        var eduMoreClicks = 1;
        function educationHistoryMore(e) {
            //eduMoreClicks++;
            var es = e.split('~');
            var count = 0;
            var strEdu = "";
            $.each(es, function (index, value) {
                count++;
                //if (count <= eduMoreClicks * 2) {
                strEdu += value;
                //}
            });
            $('#MainContent_lblEducation').html(strEdu.substr(0, strEdu.length - 4)); // + "&nbsp;<a href='javascript:educationHistoryMore(\"" + e + "\")'>More</a>"
        }
        function endorseUser(uniqueID) {
            $('#<%=txtEndorseComment.ClientID %>').val('');
            //$('#<%=lblEndorsedUser.ClientID %>').val($('#<%=lblName.ClientID %>').val(''));
            $('#divEndorse').dialog('option', 'buttons',
                {
                    "Endorse Now": function () { __doPostBack(uniqueID, ''); $(this).dialog("close"); },
                    "Cancel": function () { $(this).dialog("close"); }
                });
            $('#divEndorse').dialog('open');
            return false;
        }

        function ChatWindow() {
            var userid = document.getElementById('<%=hdnUserId.ClientID %>').value;
            var otheruserid = document.getElementById('<%=hdnOtherUserId.ClientID %>').value;

            if (userid == "") {
                $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
                $('#dialog').dialog('open');
                return false;
            } else {
                
                window.open("<%# CalculateChatAuthHash() %>", "ajaxim_" + userid + "_" + otheruserid, "width=650,height=600,resizable=1,menubar=0,status=0,toolbar=0");

                return false;
            }

        }
        function rowAction(uniqueID) {
            if (document.getElementById('<%=hdnUserId.ClientID %>').value != "") {
                $('#<%=txtToAddress.ClientID %>').val(document.getElementById('<%=lblUName.ClientID %>').innerHTML);
                $('#<%=lblName.ClientID %>').text('');
                $('#<%=txtMessage.ClientID %>').val('');
                $('#dialogContent').dialog('option', 'buttons',
                    {
                        "Send": function () { __doPostBack(uniqueID, ''); $(this).dialog("close"); },
                        "Cancel": function () { $(this).dialog("close"); }
                    });

                $('#dialogContent').dialog('open');

                return false;
            }
            else {
                $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
                $('#dialog').dialog('open');
                return false;

            }
        }

        function HideCtrl(ctrl, timer) {
            var ctryArray = ctrl.split(",");
            var num = 0, arrLength = ctryArray.length;
            while (num < arrLength) {
                if (document.getElementById(ctryArray[num])) {
                    setTimeout('document.getElementById("' + ctryArray[num] + '").style.display = "none";', timer);
                } s
                num += 1;
            }
            return false;
        }
    </script>
    <script type="text/javascript">
        $(function () {

            var msie6 = $.browser == 'msie' && $.browser.version < 7;
            if (!msie6) {
                var top = $('#bx18').offset().top;
                $(window).scroll(function (event) {
                    var y = $(this).scrollTop();
                    if (y >= 710) { $('#bx18').addClass('fixed'); }
                    else { $('#bx18').removeClass('fixed'); }
                });
            }
        });
        $(function () {

            // Check whether browser is IE6

            var msie6 = $.browser == 'msie' && $.browser.version < 7;

            // Only run the following code if browser
            // is not IE6. On IE6, the box will always
            // scroll.

            if (!msie6) {

                // Set the 'top' variable. The following
                // code calculates the initial position of
                // the box. 

                var top = $('#bx18').offset().top;

                // Next, we use jQuery's scroll function
                // to monitor the page as we scroll through.

                $(window).scroll(function (event) {

                    // In the following line, we set 'y' to
                    // be the amount of pixels scrolled
                    // from the top.

                    var y = $(this).scrollTop();

                    // Have you scrolled beyond the
                    // box? If so, we need to set the box
                    // to fixed.

                    if (y >= 710) {

                        // Set the box to the 'fixed' class.

                        $('#bx18').addClass('fixed');

                    } else {

                        // Remove the 'fixed' class 

                        $('#bx18').removeClass('fixed');
                    }
                });
            }
        });
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
        $(function Hide() {
            setTimeout(function () {
                $("#popupmessage2").fadeOut("slow") //#popupBox is the DIV to fade out
            }, 3000); //5000 equals 5 seconds
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
    <script src="https://huntable.co.uk/js1/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="https://huntable.co.uk/js1/jquery-ui-1.8.23.custom.min.js" type="text/javascript"></script>
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

        function rowAction22() {
            $('#dialog11').dialog({

                autoOpen: true,
                modal: true,
                width: 600,
                open: function (event, ui) {
                    $(event.target).parent().css('position', 'absolute');
                    $(event.target).parent().css('top', '155px');
                    $(event.target).parent().css('left', '450px');

                },
                buttons: {
                //                    "Ok": function () { 
                //                        ($this).dialog("close");
                //                    },
                //                 "Cancel": function () {
                //                       $(this).dialog("close");
                //                   }

            }

        });
        return false;
    }

     
    </script>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            border: 0; /* This removes the border around the viewport in old versions of IE */
            width: 100%;
            background: #fff;
            min-width: 600px; /* Minimum width of layout - remove line if not required */ /* The min-width property does not work in old versions of Internet Explorer */
            font-size: 90%;
        }
        /* column container */.colmask
        {
            position: relative; /* This fixes the IE7 overflow hidden bug */
            clear: both;
            float: left;
            width: 100%; /* width of whole page */
            overflow: hidden; /* This chops off any overhanging divs */
        }
        /* common column settings */.colright, .colmid, .colleft
        {
            float: left;
            width: 100%; /* width of page */
            position: relative;
        }
        .col1, .col2, .col3
        {
            float: left;
            position: relative;
            padding: 0 0 1em 0; /* no left and right padding on columns, we just make them narrower instead 
					only padding top and bottom is included here, make it whatever value you need */
            overflow: hidden;
        }
        /* 3 Column settings */.threecol
        {
            background: #eee; /* right column background colour */
        }
        .threecol .colmid
        {
            right: 25%; /* width of the right column */
            background: #fff; /* center column background colour */
        }
        .threecol .colleft
        {
            right: 50%; /* width of the middle column */
            background: #f4f4f4; /* left column background colour */
        }
        .threecol .col1
        {
            width: 46%; /* width of center column content (column width minus padding on either side) */
            left: 102%; /* 100% plus left padding of center column */
        }
        .threecol .col2
        {
            width: 21%; /* Width of left column content (column width minus padding on either side) */
            left: 31%; /* width of (right column) plus (center column left and right padding) plus (left column left padding) */
        }
        .threecol .col3
        {
            width: 21%; /* Width of right column content (column width minus padding on either side) */
            left: 85%; /* Please make note of the brackets here:
					(100% - left column width) plus (center column left and right padding) plus (left column left and right padding) plus (right column left padding) */
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="dialog11" style="display: none;" title="List Of Companies">
        <table>
            <asp:DataList runat="server" ID="dlListOfCompanies">
                <ItemTemplate>
                    <tr>
                        <td style="width: 235px;">
                            <a href='<%#CompanyUrlGenerator(Eval("Id") )%>' runat="server" >
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("CompanyName") %>' Style="color: #008ca1;"></asp:Label>
                            </a>
                        </td>
                        <%--<asp:Label runat="server" Text='<%#Eval("CompanyWebsite") %>'></asp:Label>
                 <asp:Label ID="Label1" runat="server" Text='<%#Eval("CompanyEmail") %>'></asp:Label>--%>
                        <td>
                            <a href='<%#CompanyUrlGenerator(Eval("Id") )%>' runat="server" >
                                <asp:Image ID="Image1" ImageUrl='<%#Picturec(Eval("CompanyLogoId"))%>' runat="server"
                                    Width="119px" Height="62px" /></a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </table>
        <%-- <asp:Label id="TextBox1" runat="server" Text="22aaaaaaaaaaa"></asp:Label>  
         <asp:Image runat="server" ID="imgHasCompany0001" ImageUrl="https://huntable.co.uk/images/UserCompany.png" Width="33px" Height="35px" />--%>
    </div>
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <asp:HiddenField ID="hdnOtherUserId" runat="server" />
    <div id="dialog" title="Confirm Message">
        <asp:Label runat="server" Text="You are not logged In.Please login first." ID="lblConfirmMessage"></asp:Label>
    </div>
    <p class="margin-top-visible">
        &nbsp;</p>
    <div id="divViewProfile" runat="server" class="content-section">
        <div id="ovrly" style="height: 36px; width: 100%; z-index: 2000; background-color: #FF9242;
            border: 1px solid #FF9242; border-radius: 2px; margin-top: 10px; display: none;">
            <div style="width: 980px; padding: 0px 10px; margin: 0px auto;">
                <div style="width: 30px; margin-left: 292px;">
                    <image src="https://huntable.co.uk/images/tick.png" width="25px" height="25px"></image>
                </div>
                <div style="margin-top: -25px">
                    <asp:Label Style="color: White; width: 200px; margin-top: -40px; margin-left: 377px;"
                        ID="pbl" runat="server"></asp:Label></div>
                <div style="margin-left: 692px; margin-top: -13px">
                    <image id="ximg" src="https://huntable.co.uk/images/orange-check-mark-md.png" width="10px"
                        height="10px"></image>
                </div>
            </div>
        </div>
        <div id="content-inner">
            <div class="tab-cv">
                <a id="vc" runat="server">Visual</a> <a id="vt" runat="server" style="margin-left: 10px;"
                    class="selectedcv">Text</a> <a id="visualcvactivity" runat="server" style="margin-left: 10px;">
                        Activity</a>
            </div>
            <div class="content-inner-left">
                <div class="profile-box-main">
                    <div class="profile-box-main-top">
                        <div class="profile-box-left">
                            <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                            <a href="#" runat="server" id="showProfileImage12" rel="lightbox">
                                <img runat="server" id="imgProfile" src="" class="profile-pic profile-pic-cv" width="76"
                                    height="81" alt="Profile-pic" />
                            </a>
                            <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label><br />
                            <asp:Label ID="lblCurrentRole" class="accounts-link" runat="server"></asp:Label><br />
                            <asp:Label ID="lblLocation" runat="server"></asp:Label><br />
                            <asp:Label ID="lblTown" runat="server"></asp:Label><br />
                            Skill:
                            <asp:Label ID="lblSkills" runat="server"></asp:Label>
                        </div>
                        <div id="divMsg" runat="server" class="profile-box-right">
                            <p>
                                No Need to connect<a href="#"><asp:ImageButton ID="userOnline" Width="10" Height="10"
                                    alt="green-circel" runat="server" />
                                    <asp:Label ID="onlineinfo" runat="server"></asp:Label></a></p>
                            <br />
                            <b class="send-message">Send a message to&nbsp;<br />
                                <asp:Label ID="lblUName" runat="server"></asp:Label>&nbsp;now</b>
                            <%-- <a href="#?w=500" class="button-ash floatleft poplight"
                                rel="popup3">Message</a>--%>
                            <asp:Button ID="btnMessage" Text="Message" runat="server" UseSubmitBehavior="false"
                                CssClass="button-ash floatleft marginleft" OnClientClick="return rowAction(this.name);"
                                OnClick="BtnMessageClick" />
                            <asp:Button ID="btnChat_Click" runat="server" Text="Chat" CssClass="button-ash floatleft marginleft"
                                OnClientClick="return ChatWindow();" />
                            <%-- <a id="chat" href="#" class="button-ash floatleft marginleft" onclick="chatClick" runat="server">Chat</a>--%><br />
                            <br />
                            <span style="font-weight: normal;">You Can even Conduct Online interview now</span>
                            <asp:HiddenField ID="hfSubject" runat="server" />
                        </div>
                    </div>
                    <div class="profile-box-main-bottom">
                        <div>
                            <ul class="profile-details">
                                <li class="details-small">Current </li>
                                <li class="details-large">
                                    <asp:Label ID="lblCurrentPosition" runat="server"></asp:Label></li>
                                <li class="details-small">Past </li>
                                <li class="details-large">
                                    <asp:Label ID="lblPastPosition" runat="server"></asp:Label><br />
                                </li>
                                <li class="details-small">Education </li>
                                <li class="details-large">
                                    <asp:Label ID="lblEducation" runat="server"></asp:Label><br />
                                </li>
                                <li class="details-small">Endorsements </li>
                                <li class="details-large">
                                    <asp:Label ID="lblEndorsement" runat="server"></asp:Label></li>
                            </ul>
                        </div>
                        <div class="print">
                            <asp:Button class="button-green floatleft marginleft1" runat="server" Text="Post an Opportunity"
                                OnClientClick="return rowAction01();" ID="btnPostOpportunity" OnClick="BtnPostOpportunityClick" /><br />
                            <br />
                            <br />
                            <br />
                            <table style="float: right;">
                                <tr>
                                    <td>
                                        <a id="imgshare" href="#?w=500" class="share poplight1" rel="popupSharev">
                                            <image id="sdf" src="https://huntable.co.uk/images/icon-share.png">Share</image>
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <a href="#">
                                        <div style="border: 2px; border-style: solid; border-color: rgb(219, 215, 215); width: 75px;
                                            text-align: center; margin-left: 66px;">
                                            <asp:LinkButton runat="server" ID="LinkButton3" OnClick="ImgbtnPdfClick">
                                                <img alt="pdf" width="18" height="19" src="https://huntable.co.uk/images/icon-pdf.png" /><asp:Label
                                                    Style="color: Black;" runat="server" Text="Print" ID="lbl_print"></asp:Label>
                                            </asp:LinkButton>
                                        </div>
                                    </a>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnFollow" OnClick="BtnFollowClick" Text="Follow" runat="server"
                                            CssClass="button-orange" Style="font-size: 12px; font-family: Arial, Helvetica, sans-serif;
                                            padding: 5px 15px; position: relative; top: 4px;" Visible="false"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton1" OnClick="BtnUnFollowClick" Text="Following" runat="server"
                                            CssClass="button-orange" Style="font-size: 12px; font-family: Arial, Helvetica, sans-serif;
                                            padding: 5px 15px; position: relative; top: 4px;" Visible="false"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:ImageButton runat="server" ID="imgHasCompany" ImageUrl="https://huntable.co.uk/images/UserCompany.png"
                            Width="33px" Height="35px" OnClick="UserCompaniesClick" Style="margin-left: 111px;
                            margin-top: 12px;" />
                    </div>
                </div>
                <div class="profile-desc">
                    <h2 class="profile-desc-heading">
                        Summary</h2>
                    <p>
                        <asp:Label runat="server" ID="lblSummary"></asp:Label>
                        <br />
                    </p>
                    <br />
                    <h2 class="profile-desc-heading">
                        Experience</h2>
                    <div class="experience">
                        <asp:Repeater runat="server" ID="rptrExperience" OnItemDataBound="itembound">
                            <ItemTemplate>
                                <div class="experience">
                                    <a>
                                        <asp:Label ID="Label13" class="accounts-link" runat="server" Text='<%#Eval("JobTitle") %>'></asp:Label></a>
                                    <br />
                                    <a>
                                        <asp:Label ID="Label14" class="accounts-link" runat="server" Text='<%#Eval("Company") %>'></asp:Label></a><br />
                                    <%-- <asp:Label ID="Label1" runat="server" Text='<%#Eval("town") %>'></asp:Label><br />
                                    <asp:Label ID="Label15" runat="server" Text='<%#Eval("Location") %>'></asp:Label><br />--%>
                                    <asp:Label ID="Label16" runat="server" Text='<%#Eval("Period") %>'></asp:Label><br />
                                    <asp:Label ID="Label17" runat="server" Text='<%#Eval("Description") %>'></asp:Label><br />
                                    <div class="skills-tag">
                                        <div class="skills-tag" style="width: 30%; margin-top: 10px">
                                            <div class="skil-title">
                                                Skill</div>
                                            <div class="colon">
                                                :</div>
                                            <div class="skill-t">
                                                <span>
                                                    <img src="https://huntable.co.uk/images/skils-tag-left.png" width="11" height="20"
                                                        alt="skils-left" /></span>
                                                <asp:Label runat="server" ID="lblExperienceSkill" class="cent-span" Width="11px" Text='<%#Eval("Skill") %>'></asp:Label><span>
                                                    <img width="25" style="margin-left: 0px" height="20" alt="skils-right" src="https://huntable.co.uk/images/skils-tag.png">
                                                </span>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                        </div>
                                        <div class="skills-tag" style="width: 30%; margin-top: 10px">
                                            <div class="skil-title">
                                                Industry</div>
                                            <div class="colon">
                                                :</div>
                                            <div class="skill-t">
                                                <span>
                                                    <img src="https://huntable.co.uk/images/skils-tag-left.png" width="11" height="20"
                                                        alt="skils-left" /></span>
                                                <asp:Label class="cent-span" runat="server" ID="lblExperienceIndustry" Text='<%#Eval("Industry") %>'></asp:Label><span>
                                                    <img width="25" style="margin-left: 0px" height="20" alt="skils-right" src="https://huntable.co.uk/images/skils-tag.png">
                                                </span>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                        </div>
                                        <div class="skills-tag" style="width: 30%; margin-top: 10px">
                                            <div class="skil-title">
                                                Level</div>
                                            <div class="colon">
                                                :</div>
                                            <div class="skill-t">
                                                <span>
                                                    <img src="https://huntable.co.uk/images/skils-tag-left.png" width="11" height="20"
                                                        alt="skils-left" /></span>
                                                <asp:Label runat="server" class="cent-span" ID="lblExperienceLevel" Text='<%#Eval("Level") %>'></asp:Label><span>
                                                    <img width="25" style="margin-left: 0px" height="20" alt="skils-right" src="https://huntable.co.uk/images/skils-tag.png">
                                                </span>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                                        </div>
                                    </div>
                                    <br />
                                    <asp:Label ID="lblname" runat="server"></asp:Label>
                                    <div runat="server" id="aediv">
                                        <a id="ae" href="#?w=750" class=" floatleft poplight1" rel="popup6">Endorse
                                            <asp:Label ID="en" runat="server" Text='<%#Name(Eval("UserId")) %>'></asp:Label>'s
                                            Work in
                                            <asp:Label ID="en1" runat="server" Text='<%#Eval("Company") %>'></asp:Label></a></div>
                                    <a id="te" runat="server" rel="tooltip" title="You can endorse your collegue or friend you have worked together.This is your professional opinion so keep it professional.Tip: Keep it clear and simple, Your friend can delete your endorsementif they are innappropriate. ">
                                        <img src="https://huntable.co.uk/images/icon-tips.png" width="14" height="15" alt="tips" /></a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <br />
                    <h2 class="profile-desc-heading">
                        Education</h2>
                    <p>
                        &nbsp;<asp:Repeater runat="server" ID="rpEducations">
                            <ItemTemplate>
                                <div class="experience">
                                    <asp:Label ID="Label13" runat="server" Text='<%#Eval("Institution") %>'></asp:Label><br />
                                    <asp:Label ID="Label14" runat="server" Text='<%#Eval("Course") %>'></asp:Label><br />
                                    <asp:Label ID="Label15" runat="server" Text='<%#Eval("Period") %>'></asp:Label><br />
                                </div>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <br />
                            </SeparatorTemplate>
                        </asp:Repeater>
                    </p>
                    <br />
                    <h2 class="profile-desc-heading">
                        Industry, Skills &amp; Expertise</h2>
                    <ul class="industry-list-profile">
                        <li style="background: none; border: none;"><strong>Industries:</strong></li>
                        
                            <asp:Repeater runat="server" ID="rpindstry">
                                <ItemTemplate>
                                    <li>
                            <asp:Label runat="server" ID="lbliustrndy" Text='<%#Eval("Industry") %>'></asp:Label>
                            </li>
                            </ItemTemplate>
                            </asp:Repeater>
                        
                    </ul>
                    <ul class="industry-list-profile">
                        <li style="background: none; border: none;"><strong>Skill:</strong></li>
                        <asp:Repeater runat="server" ID="rpSkill">
                            <ItemTemplate>
                                <li>
                                    <asp:Label runat="server"  Text='<%#Eval("Skill") %>'></asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <br />
                    <h2 id="headPersonal" runat="server" class="profile-desc-heading">
                        Personal Details</h2>
                    <p>
                        <table id="tblPersonal" runat="server">
                            <tr>
                                <td>
                                    Phone:
                                </td>
                                <td>
                                    <asp:Label ID="lblPhoneNumber" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Address:
                                </td>
                                <td>
                                    <asp:Label ID="lblAddress" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    City:
                                </td>
                                <td>
                                    <asp:Label ID="lblCity" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Country:
                                </td>
                                <td>
                                    <asp:Label ID="lblCountry" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Birthday:
                                </td>
                                <td>
                                    <asp:Label ID="lblBirthDay" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Marital status:
                                </td>
                                <td>
                                    <asp:Label ID="lblMaritalStatus" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </p>
                    <h2 class="profile-desc-heading">
                        Endorsements</h2>
                    <asp:Repeater ID="rspEndorse" OnItemDataBound="RepeaterItemDataBound" runat="server">
                        <ItemTemplate>
                            <div class="experience">
                                <a id="vup" runat="server">
                                    <asp:Label Text='<%#Format(Eval("EndorsedUserId"))%>' runat="server"></asp:Label></a>
                                <asp:Label runat="server" Text='<%#Pict(Eval("EndorsedUserId"))%>'></asp:Label><br />
                                <asp:Label runat="server" Text='<%#Eval("Comments")%>' ID="Label3"></asp:Label><br />
                                <asp:Label runat="server" ID="lblEndorsed" Text='<%#Eval("EndorsedDateTime", "{0:MMMM dd,yyyy}")%>'
                                    Font-Italic="true" Font-Bold="true"></asp:Label>
                                <asp:LinkButton runat="server" OnClick="DeleteEndorsements" Text="Delete" ID="btnDelete"
                                    CommandArgument='<%#Eval("Id")%>' />
                            </div>
                            <br></br>
                        </ItemTemplate>
                    </asp:Repeater>
                    <h2 class="profile-desc-heading">
                        General
                    </h2>
                    <br />
                    <strong>Languages Known</strong>
                    <asp:Panel ID="pnlLanguages" runat="server">
                    </asp:Panel>
                    <br />
                    <strong>Interests </strong>
                    <br />
                    <asp:Panel ID="pnlInterests" runat="server">
                    </asp:Panel>
                    <br />
                    <strong>Skill</strong><br />
                    Expert-<asp:Panel ID="pnlExpertSkill" runat="server" style="margin-left: 50px;">
                    </asp:Panel>
                    Strong-
                    <asp:Panel ID="PanelStrongSkill" runat="server" style="margin-left: 50px;">
                    </asp:Panel>
                    Good-<asp:Panel ID="PanelGoodSkill" runat="server" style="margin-left: 50px;">
                    </asp:Panel>
                    <asp:Label ID="lblEndorse" runat="server"></asp:Label>
                    <p>
                    </p>
                    <br />
                    <br />
                    <br />
                    <div>
                        <div id="ssd" runat="server">
                            <a href="#?w=500" class="button-orange floatleft poplight1" rel="popupSharev">Share</a></div>
                        <div id="divBLock" runat="server">
                            <div id="divEndorseBLock" runat="server">
                                <a href="#?w=750" class="button-orange floatleft poplight1" rel="popup6">Endorse Now
                                </a>
                            </div>
                            <div id="RequestEndorsements" runat="server">
                                <a href="#?w=750" id="popup12" class="button-orange floatleft poplight1" rel="popup7">
                                    Request Endorsement</a></div>
                            <div id="partialunblock" runat="server">
                                <a href="#?w=750" class="button-orange floatleft poplight1" rel="popup2">Block Messages
                                    from this user</a></div>
                        </div>
                    </div>
                </div>
                <div id="divunblock" visible="False" runat="server">
                    <asp:Button ID="btnUnblock" class="button-orange floatleft poplight1" Text="Unblock"
                        runat="server" OnClick="BtnUnblockClick" />
                </div>
            </div>
            <div id="popup2" class="popup_block">
                <div class="apply-job">
                    <h3 class="popup-head">
                        Block
                    </h3>
                    <strong class="floatleft">All messages from this user have been blocked.</strong><br />
                    <asp:Button runat="server" class="button-green floatleft" Text="Block " ID="btnBlockUser"
                        UseSubmitBehavior="false" OnClick="BtnBlockUserClick" /><a href="javascript:history.back()"
                            class="button-ash floatleft" style="margin: 10px 0px 0px 10px;">cancel</a>
                </div>
            </div>
            <div class="content-inner-right">
                <div class="box-right">
                    <uc1:CVStats ID="uc1" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc2:your ID="ucyour" runat="server"></uc2:your>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div id="bx18">
                    <div class="box-right">
                        <div class="head-ash">
                            <h3>
                                See
                                <asp:Label ID="lblSName" runat="server"></asp:Label>
                                Connections</h3>
                        </div>
                        <p class="cv-rating">
                            <strong>
                                <asp:Label ID="lblinvited" runat="server"></asp:Label></strong>Invited</p>
                        <p class="cv-rating">
                            <strong>
                                <asp:Label ID="lblJoinedFriends" runat="server"></asp:Label></strong>Friends
                            Joined Huntable</p>
                        <p class="cv-rating">
                            <strong>
                                <asp:Label ID="lblAffiliate" runat="server"></asp:Label></strong>Affiliate Earnings</p>
                        <a href="#" class="learn-more">Show More</a>
                    </div>
                    <p class="margin-top-visible">
                        &nbsp;</p>
                    <div class="box-right">
                        <uc3:seeyourfriends ID="uc3" runat="server" />
                    </div>
                 
                    
                <div id="nonc" runat="server" style=" width: 276px; padding: 10px 10px;margin-top: 426px;
                        border: 1px solid #C4D1D3; border-radius: 4px 4px; font-weight: bold;">
                        <p style="font-size: 12px;">
                            No Need to connect<a href="#" style="float: right;"><asp:ImageButton ID="ImageButton1"
                                Width="10" Height="10" alt="green-circel" runat="server" />
                                <asp:Label ID="Label1" runat="server"></asp:Label></a></p>
                        <br />
                        <b class="send-message send-message-text" style="font-size: 12px">Send a message to
                            <asp:Label ID="Label4" runat="server"></asp:Label></b>
                        <br />
                        <asp:Button ID="Button1" Text="Message" runat="server" UseSubmitBehavior="false"
                            CssClass="button-ash floatleft marginleft" OnClientClick="return rowAction(this.name);"
                            OnClick="BtnMessageClick" />
                        <asp:Button ID="Button2" runat="server" Text="Chat" CssClass="button-ash floatleft marginleft"
                            OnClientClick="return ChatWindow();" /><br />
                        <br />
                        <span style="font-weight: normal; font-size: 13px; float: left;">You Can even Conduct
                            Online interview now</span><br/>
                    </div>
                </div>
               
                   
                <p class="margin-top-visible">
                    &nbsp;</p>
            </div>
            <script type="text/javascript">
                function geturl() {
                    //document.getElementById('twitter').href = 'https://twitter.com/share?url=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value) + '&summary=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value);
                    //document.getElementById('linkedin').href = 'http://www.linkedin.com/shareArticle?mini=true&url=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value) + '&summary=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value);
                    //                    document.getElementById('facebook').href = 'http://www.facebook.com/sharer.php?u=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value) + '&summary=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value);
                }
                function getTxt() {
                    document.getElementById('<%=txtShareMessage.ClientID %>').value = window.window.top.location;
                    document.getElementById('<%=txtMessage.ClientID %>').value = window.window.top.location;

                    document.getElementById('<%=fe_text.ClientID %>').value = window.window.top.location;

                }
            </script>
            <script type="text/javascript">



                //When you click on a link with class of poplight and the href starts with a # 
                $('a.poplight1[href^=#]').click(function () {
                    var popID = $(this).attr('rel'); //Get Popup Name
                    var popURL = $(this).attr('href'); //Get Popup href to define size

                    //                url.src = "ShareMail.aspx";
                    //Pull Query & Variables from href URL
                    var query = popURL.split('?');
                    var dim = query[1].split('&');
                    var popWidth = dim[0].split('=')[1]; //Gets the first query string value

                    //Fade in the Popup and add close button
                    $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="https://huntable.co.uk/images/close_pop.png" class="btn_close" title="Close Window1" alt="Close" /></a>');

                    //Define margin for center alignment (vertical + horizontal) - we add 80 to the height/width to accomodate for the padding + border width defined in the css
                    var popMargTop = ($('#' + popID).height() + 80) / 2;
                    var popMargLeft = ($('#' + popID).width() + 80) / 2;

                    //Apply Margin to Popup
                    $('#' + popID).css({
                        'margin-top': -popMargTop,
                        'margin-left': -popMargLeft
                    });

                    //Fade in Background
                    $('body').append('<div id="fade" style="z-index:10"></div>'); //Add the fade layer to bottom of the body tag.
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
                function fdout() {
                    $('#fade , .popup_block').fadeOut(function () {
                        $('#fade, a.close').remove();
                    }); //fade them both out

                    return false;

                }



                function fdout() {
                    $('#fade').remove();

                }

                function HideCtrl(ctrl, timer) {
                    var ctryArray = ctrl.split(",");
                    var num = 0, arrLength = ctryArray.length;
                    while (num < arrLength) {
                        if (document.getElementById(ctryArray[num])) {
                            setTimeout('document.getElementById("' + ctryArray[num] + '").style.display = "none";', timer);
                        }
                        num += 1;
                    }
                    return false;
                }
            </script>
            <div id="popup7" class="popup_block">
                <table>
                    <tr>
                        <td colspan="2">
                            Endorse
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Company
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddljob" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea id="txtarea" rows="6" cols="1" runat="server" placeholder="Comments..."
                                style="width: 400px;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" Text="Request Endorsement" class="button-orange floatleft poplight1"
                                ID="btnRequestEndorse" UseSubmitBehavior="false" OnClick="BtnRequestEndorseClick">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="popup6" class="popup_block">
                <table>
                    <tr>
                        <td colspan="2">
                            Endorse
                            <asp:Label runat="server" ID="lblEndorsedUser"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Company
                            <asp:DropDownList runat="server" ID="ddljobtitle" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea id="txtEndorseComment" rows="6" cols="1" runat="server" placeholder="Comments..."
                                style="width: 400px;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" Text="Endorse Now" class="button-orange floatleft poplight1"
                                ID="btnEndorseUser" UseSubmitBehavior="false" OnClick="BtnEndorseUserClick" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="popupSharev" class="popup_block">
                <div id="Div2" class="active">
                    <div class="apply-job">
                        <div id="tabswitch">
                            <ul class="tabswitch-share">
                                <li class="tab1">Copy Url </li>
                                <li class="tab2">Share by E-mail </li>
                                <li class="tab3">Share to Social </li>
                            </ul>
                            <div class="tab-container tab-container-share tab1">
                                <table class="login-table-share">
                                    <tr class="social-share">
                                        <td width="30%" align="right" valign="top">
                                            <label>
                                                You Can Share this content Using the URL</label>
                                        </td>
                                        <td width="70%" valign="top">
                                            <br />
                                            <asp:TextBox runat="server" ID="fe_text" TextMode="MultiLine" cols="10" Rows="4"
                                                class="textbox textbox-share"></asp:TextBox><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%" valign="top">
                                            &nbsp;
                                        </td>
                                        <td width="70%" valign="top">
                                            <asp:Button ID="d_clip_button" runat="server" Text="Copy To Clipboard..." class="button-green" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="tab-container tab-container-share tab2">
                                <table class="login-table-share">
                                    <tr>
                                        <td width="30%" align="right" valign="top">
                                            <label>
                                                To:</label>
                                        </td>
                                        <td width="70%" valign="top">
                                            <asp:TextBox ID="txtTo" runat="server" class="textbox textbox-share"></asp:TextBox><asp:RequiredFieldValidator
                                                ID="rfvTo" runat="server" ForeColor="Red" ErrorMessage="Please enter Email Id"
                                                ControlToValidate="txtTo" ValidationGroup="mail"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%" align="right" valign="top">
                                            <label>
                                                Add Your Message Here:</label>
                                        </td>
                                        <td width="70%" valign="top">
                                            <asp:TextBox ID="TextBox1" runat="server" ValidationGroup="mail" TextMode="MultiLine"
                                                class="textbox textbox-share" cols="10" Rows="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%" valign="top">
                                            &nbsp;
                                        </td>
                                        <td width="70%" valign="top">
                                            <asp:Button ID="txtSharebyEmail" ValidationGroup="mail" runat="server" class="button-green"
                                                Text="Share" OnClick="txtSharebyEmail_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="tab-container tab-container-share tab3">
                                <table class="login-table-share">
                                    <tr>
                                        <td width="30%" align="right" valign="top">
                                            <label>
                                                Add Your Message Here:</label>
                                        </td>
                                        <td width="70%" valign="top">
                                            <asp:TextBox ID="txtShareMessage" class="textbox textbox-share" TextMode="MultiLine"
                                                cols="10" Rows="4" runat="server" onchange="geturl()">Add a Message</asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="social-share">
                                        <td width="30%" align="right" valign="top">
                                            <label>
                                                Select Where to Share:</label>
                                        </td>
                                        <td width="70%" valign="top">
                                            <p>
                                                <asp:CheckBox ID="chkTwitter" runat="server" OnCheckedChanged="chkTwitter_CheckedChanged" />
                                                <a id="twitter" href="#" title="Twitter Share Button">
                                                    <img src="https://huntable.co.uk/images/twitter.png" width="20" height="20"></a>
                                                <asp:CheckBox ID="chkLinkedIn" runat="server" OnCheckedChanged="chkLinkedIn_CheckedChanged" /><a
                                                    id="linkedin" href="#">
                                                    <img src="https://huntable.co.uk/images/linkedin.png" alt="linkedin share button"
                                                        width="20" height="20" title="LinkedIn Share Button" /></a><asp:CheckBox ID="chkFacebook"
                                                            runat="server" OnCheckedChanged="chkFacebook_CheckedChanged" />
                                                <a id="facebook" href="#" title="Facebook Share Button">
                                                    <img src="https://huntable.co.uk/images/facebook.jpg" width="20" height="20"></a>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%" valign="top">
                                            &nbsp;
                                        </td>
                                        <td width="70%" valign="top">
                                            <asp:Button ID="btnShare" runat="server" Text="Share" class="button-green" OnClick="btnShare_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="pop" runat="server" />
                <script type="text/javascript" src="https://huntable.co.uk/js/ZeroClipboard.js"></script>
            </div>
        </div>
    </div>
    <!-- Popup Script Ends -->
    <script type="text/javascript" language="javascript">
    </script>
    <!-- Share Tab Script begins -->
    <script type="text/javascript" src="https://huntable.co.uk/js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="https://huntable.co.uk/js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <div id="dialogContent" align="center">
        <div>
            To:
            <asp:TextBox ID="txtToAddress" runat="server" ReadOnly="true" /><br />
            What would you like to message<asp:Label ID="Label2" runat="server" />?<br />
            <asp:RadioButtonList ID="rbMessageList" runat="server">
                <asp:ListItem Value="0" Text="Job Enquiry"></asp:ListItem>
                <asp:ListItem Value="1" Text="Request endorsement"></asp:ListItem>
                <asp:ListItem Value="2" Text="Introduce Yourself"></asp:ListItem>
                <asp:ListItem Value="3" Text="New Business Opportunity"></asp:ListItem>
                <asp:ListItem Value="4" Text="Your Recruitment requirement"></asp:ListItem>
            </asp:RadioButtonList>
            <%--   Subject:
            <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox><br />--%>
            <%-- $('#<%=txtSubject.ClientID %>').val('');--%>
            <br />
            Message:
            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Style="font-size: 12px;
                height: 65px; width: 150px;" /><br />
            <%--  <asp:Button ID="btnMessage" runat="server" CssClass="button-ash floatleft poplight"
                Text="Message" OnClick="btnMessage_Click" />--%>
        </div>
    </div>
</div>
</div>
</div>
</div>
</div>
</div>
</asp:Content>
