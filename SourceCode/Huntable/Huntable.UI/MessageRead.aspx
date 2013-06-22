<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MessageRead.aspx.cs" Inherits="Huntable.UI.MessageRead" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="JqueryPopup/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
    <link href="Styles/PagerStyles.css" rel="Stylesheet" type="text/css" />
    <link href="css/style-innerpage.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <!-- Share Tab Script begins -->
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.5.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/redmond/jquery-ui.css"
        rel="stylesheet" type="text/css" />
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
    <script type="text/javascript">
            $().ready(function () {
	        $('#dialogContent').dialog({
	            autoOpen: false,
	            modal: true,
	            bgiframe: true,
	            title: "Reply"
                // width: 800,
	           // height: 400
	        });
            $('#dialogContent').parent().appendTo($("form:first"));
	        $('#confirmationDialog').dialog({
	            autoOpen: false,
	            modal: true,
	            bgiframe: false,
	            title: "Delete",
	            width: 500,
	            height: 300
	        });
	        $('#confirmationDialog').parent().appendTo($("form:first"));
	        $('#readDialog').dialog({
	            autoOpen: false,
	            modal: true,
	           // bgiframe: true,
	          title: "Read Message",
	            width: 500,
	            height: 300
	        });
	        $('#readDialog').parent().appendTo($("form:first"));
	        $('#divCompose').dialog({
	            autoOpen: false,
	            modal: true,
	            bgiframe: true,
	            title: "New Message",
//	            width: 800,
//	            height: 400
	        });
	        $('#divCompose').parent().appendTo($("form:first"));


	        $('#btnSelectAll').click(function () {
	            $("span.selectCheckbox input:first-child").prop("checked", true);
	        });

	        $('#btnDeselectAll').click(function () {
	            $("span.selectCheckbox input:first-child").prop("checked", false);
	        });

	        $('#btnSelectRead').click(function () {
	            $('#btnDeselectAll').trigger("click");
	            $("div.readMessageStyle span.selectCheckbox input:first-child").prop("checked", true);	            
	        });
	    });


        

function composeMsg(uniqueID) {
    $('#<%=txtComposeSubject.ClientID %>').val('');
    $('#<%=txtComposeBody.ClientID %>').val('');
    $('#divCompose').dialog('option', 'buttons',
				{
				    "Send": function () { __doPostBack(uniqueID, ''); $(this).dialog("close"); },
				    "Cancel": function () { $(this).dialog("close"); }
				});

				$('#divCompose').dialog('open');

    return false;
}


		function rowDeleteAction(uniqueID, message) {
			$('#<%=lblConfirmMessage.ClientID %>').text(message);
			$('#confirmationDialog').dialog('option', 'buttons',
				{
					"OK": function () { __doPostBack(uniqueID, ''); $(this).dialog("close"); },
					"Cancel": function () { $(this).dialog("close"); }
				});

			$('#confirmationDialog').dialog('open');

			return false;
		}
         $(function () {
            $('#<%= txtToUser.ClientID %>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "HuntableWebService.asmx/SearchComposeToUser",
                        data: "{ 'word': '" + request.term + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 1
            });
        });
		function selectedDelete(uniqueID, message) {
				$('#<%=lblConfirmMessage.ClientID %>').text(message);
				$('#confirmationDialog').dialog('option', 'buttons',
				{
					"OK": function () { __doPostBack(uniqueID, ''); $(this).dialog("close"); },
					"Cancel": function () { $(this).dialog("close"); }
				});

				$('#confirmationDialog').dialog('open');
				return false;
		}
    </script>
    <script type="text/javascript">
        $(function () {

            var msie6 = $.browser == 'msie' && $.browser.version < 7;
            if (!msie6) {
                var top = $('#box').offset().top;
                $(window).scroll(function (event) {
                    var y = $(this).scrollTop();
                    if (y >= 150) { $('#box').addClass('fixed'); }
                    else { $('#box').removeClass('fixed'); }
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

                var top = $('#box').offset().top;

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

                    if (y >= 150) {

                        // Set the box to the 'fixed' class.

                        $('#box').addClass('fixed');

                    } else {

                        // Remove the 'fixed' class 

                        $('#box').removeClass('fixed');
                    }
                });
            }
        });
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
        .buttonAsLink
        {
            border: 0px;
            background: none;
            color: Blue;
            cursor: pointer;
        }
        .readMessageStyle
        {
            font-weight: normal;
            background-color: #F5F5F5;
            border-top: 1px solid gray;
        }
        .unReadMessageStyle
        {
            font-weight: bold;
            background-color: #FFFFFF;
            border-top: 1px solid gray;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dialogContent').dialog({
                autoOpen: false,
                modal: true,
                bgiframe: true,
                title: "Reply",
                width: 800,
                height: 400
            });
            $('#dialogContent').parent().appendTo($("form:first"));
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

        function showReply() {
            //$('#<%=txtToAddress.ClientID %>').val(userName);
            //$('#<%=lblName.ClientID %>').text(userName);
            $('#<%=txtSubject.ClientID %>').val('');
            $('#<%=txtMessage.ClientID %>').val('');
            //$('#<%=hdnSentTo.ClientID %>').val(userId);
            $('#dialogContent').dialog('option', 'buttons',
				{ "Cancel": function () { $(this).dialog("close"); },
				    "Send": function () {
				        var sub = $('#<%=txtSubject.ClientID %>').val();
				        var body = $('#<%=txtMessage.ClientID %>').val();
				        if (sub == "" || body == "") {
				            $('#replyErrorMsg').show();
				            $('#replyErrorMsg').html('Subject and body are required');
				            return;
				        }
				        $('#<%=hdnReplySubject.ClientID%>').val(sub);
				        $('#<%=hdnReplyBody.ClientID%>').val(body);
				        __doPostBack($('#<%=hdnSentTo.ClientID %>').val(), '');
				        $(this).dialog("close");
				    }
				});

            $('#dialogContent').dialog('open');

            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="content-inner-left content-inner-left-message">
                    <div class="profile-main">
                        <div class="accounts-profile accounts-profile1" style="width: 732px;">
                            <div class="accounts-profile-left">
                                <asp:Image runat="server" ID="imgProfilePicture" class="profile-pic" Width="76" Height="81"
                                    alt="Profile-pic" />
                                <asp:Label ID="lblProfileName" Text="User Profile Name" runat="server"></asp:Label>
                            </div>
                            <div class="accounts-profile-right">
                                <div class="accounts-top">
                                    <table>
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblPercentCompleted" class="profile-complete" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                Complete
                                            </td>
                                            <td style="text-align: left;">
                                                <eo:ProgressBar ID="ProgressBar2" runat="server" Width="90px" BackgroundImage="00060301"
                                                    BackgroundImageLeft="00060302" BackgroundImageRight="00060303" ControlSkinID="None"
                                                    IndicatorImage="00060304" ShowPercentage="True" Value="30">
                                                </eo:ProgressBar>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <span class="last-login">Last Login Time:<b><asp:Label ID="lblLogDate" runat="server"></asp:Label></b>&nbsp;<b>
                                    <asp:Label runat="server" ID="lblLastLoginTime"></asp:Label></b></span> <span class="last-update">
                                        Last Profile Updated on: <b>
                                            <asp:Label runat="server" ID="lblLastProfileUpdatedOn" Text="Last Profile Updated Time"></asp:Label></b></span>
                            </div>
                        </div>
                        <div class="notification">
                            
                            <div id="box" style="margin-left: -489px;">
                                <input type="text" class="textbox textbox-search1" onblur="if (this.value == '') {this.value ='Search Inbox';}"
                                    onfocus="if (this.value =='Search Inbox') {this.value ='';}" value="Search Inbox" />
                                <a href="#">
                                    <img src="images/search-img.png" width="28" height="27" alt="search" /></a>
                                <a href="#?w=750" class="invite-friend-btn invite-friend-btn-notification floatleft poplight"
                                    rel="popup2">Compose Message
                                    <img src="images/notification-msg.jpg" width="16" height="12" alt="Compose" />
                                </a><a class="invite-friend-btn invite-friend-btn-notification floatleft poplight"
                                    href="MessageInbox.aspx">Inbox</a>
                            </div>
                            <div class="notification-right notification-right-message" style="margin-left: 219px;">
                                <div class="notification-head">
                                    <asp:Button ID="btnSelect" Visible="False" runat="server" Text="All" CssClass="button-ash button-ash-notification"
                                        BorderStyle="None" />
                                    <asp:Button ID="btnUnCheck" Visible="False" runat="server" Text="None" CssClass="button-ash button-ash-notification"
                                        BorderStyle="None" />
                                    <asp:Button runat="server" ID="btnMarkAsUnreadSelectedMessages" Text="Mark as Unread"
                                        CssClass="button-ash button-ash-notification" />
                                    <asp:Button runat="server" CssClass="button-ash button-ash-notification" ID="Button1"
                                        Text="Delete" OnClick="Delete" UseSubmitBehavior="false" OnClientClick="javascript:return selectedDelete(this.name,'Are you sure?')" />
                                </div>
                                <div class="next">
                                    <asp:HyperLink runat="server" ID="lnkPrevious" Text="< Previous" class="button-ash button-ash-notification floatleft"></asp:HyperLink>
                                    <asp:HyperLink runat="server" ID="lnkNext" Text="Next >" class="button-ash button-ash-notification floatright"></asp:HyperLink>
                                </div>
                                <%--<asp:Repeater ID="rspMessages" runat="server">
                                        <ItemTemplate>
                                         <div id="divMsg" runat="server" class="blue-box blue-box1" style="width: 502px;">
                                            <h3 class="tip-to-use">
                                            <asp:Label runat="server" ID="lblsubject" Text='<%#Eval("Subject") %>'></asp:Label></h3>
                                            <asp:ImageButton ID="imgBtn1" runat="server" Width="76" Height="81" CssClass="profile-pic profile-pic2"
                                                ImageUrl='<%#Picture(Eval("SentBy"))%>' />
                                           <asp:Label runat="server" ID="Label1" Text='<%#Position(Eval("SentBy")) %>'></asp:Label><br />
                                           Date:<asp:Label runat="server" ID="lblDate" Text='<%#Eval("SentDate", "{0:MM/dd/yyyy}") %>' ></asp:Label>
                                            </div>
                                            <%--<asp:Label runat="server" ID="lblsubject" Text='<%#Eval("Subject") %>'></asp:Label>--%>
                                <%--<div class="linked-tip">
                                            <asp:Label runat="server" ID="lblBody" Text='<%#Eval("Body") %>'></asp:Label>
                                            <br />
                                            <a href="#" style="float: right;" class="button-green" onclick="javascript:showReply('<%#Eval("User.Name")%>','<%#Eval("User.Id")%>');">Reply</a>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>--%>
                                <div id="divMsg" runat="server" class="blue-box blue-box1" style="width: 502px; background: #edfdff;">
                                    <asp:ImageButton ID="imgBtnSentBy" runat="server" Width="76" Height="81" CssClass="profile-pic profile-pic2" />
                                    <h3 class="tip-to-use">
                                        <asp:Label runat="server" ID="lbluserName"></asp:Label></h3>
                                    <br />
                                    <asp:Label runat="server" ID="lblsubject" Font-Bold="true"></asp:Label>
                                    <div class="linked-tip">
                                        <asp:Label runat="server" ID="lblBody"></asp:Label>
                                    </div>
                                </div>
                                <div>
                                     <asp:Label runat="server" Text="Message Sent SuccessFully" Visible="False" ForeColor="Green" Font-Size="16px" ID="lblMessage"></asp:Label>
                                    <a href="#" style="float: right; margin-top: 20px;" class="button-green" onclick="javascript:showReply();">
                                        Reply</a>
                                </div>
                            </div>
                            <%--  <div class="linked-tip">
                                    <h3 class="login-heading login-heading1">
                                        Linked in Tip</h3>
                                    <strong>Step 1:</strong><br />
                                    Login to Linked In (If you haven't already)<br />
                                    <br />
                                    <strong>Step 2:</strong><br />
                                    Go on Edit Profile (drop down option on the "profile" tab)<br />
                                    <br />
                                    <strong>Step 3:</strong><br />
                                    Click edit next to the link you want to edit in the websites section<br />
                                    <br />
                                    <strong>Step 4:</strong><br />
                                    In the new page where it curently says "Company Website" or "Personal Website" change
                                    the drop down option to "other"<br />
                                    <br />
                                    <strong>Step 5:</strong><br />
                                    Place the desired text in the first box, I have used "Dino Digital - Web Agency"
                                    and in the second box the web address (web address should stay the same).<br />
                                    <br />
                                    <strong>Step 6:</strong><br />
                                    Save the changes.<br />
                                    <br />
                                    <b>This will encourage people to click your link as well as help with your search engine
                                        rank.</b><br />
                                    <br />
                                    <a href="#" style="float: right;" class="button-green">Reply</a>
                                </div>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="content-inner-right content-inner-right-message">
                <div class="google-add">
                     <asp:Image ID="bimage" CssClass="advert2" runat="server"  ImageUrl="images/basic-user-advert.gif"/>
                <asp:Image ID="pimage" runat="server" CssClass="advert2"  ImageUrl="images/premium-user-advert.gif" />
                </div>
            </div>
        </div>
        <div>
            <asp:LinkButton ID="lnkMesage" Visible="False" runat="server" PostBackUrl="~/MessageInbox.aspx"
                Text="Back"></asp:LinkButton>
            <asp:Button runat="server" Visible="False" ID="btnDeleteSelectedMessages" Text="Delete"
                OnClick="Delete" /></div>
    </div>
    <br />
    <br />
    <div class="colmask threecol">
        <div class="colmid">
            <div class="colleft">
            </div>
        </div>
    </div>
    <div id="dialogContent">
        <table>
            <tr>
                <td>
                    To:
                </td>
                <td>
                    <asp:TextBox ID="txtToAddress" runat="server" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    What would you like to message
                    <asp:Label ID="lblName" runat="server" />
                    ?
                </td>
            </tr>
            <tr>
                <td>
                    Subject<font color="red">*</font>
                </td>
                <td>
                    <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Body<font color="red">*</font>
                </td>
                <td>
                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Height="150" Width="400" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span id="replyErrorMsg" style="display: none;" class="error"></span>
                </td>
            </tr>
        </table>
    </div>
    <div id="confirmationDialog">
        <asp:Label runat="server" ID="lblConfirmMessage"></asp:Label>
    </div>
    <%--<div id="readDialog" class="popup_block">
        <table>
            <tr>
                <td>
                    Subject:
                    <asp:Label ID="lblSubject" runat="server"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    Message:
                    <asp:Label ID="lblMessageContent" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>--%>
    <div id="popup2" class="popup_block">
        <table>
            <tr>
                <td colspan="2">
                    Whom would you like to message :
                    <asp:TextBox runat="server" ID="txtToUser" class="tb" CssClass="textbox" ValidationGroup="SaveCurrentExp"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Subject:
                </td>
                <td>
                    <input type="text" id="txtComposeSubject" runat="server" placeholder="Subject..."
                        style="width: 400px;" />
                    <asp:RequiredFieldValidator ID="rfvSub" runat="server" ControlToValidate="txtComposeSubject"
                        ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Body:
                </td>
                <td>
                    <textarea id="txtComposeBody" rows="5" cols="1" runat="server" placeholder="Body..."
                        style="width: 400px;" />
                    <asp:RequiredFieldValidator ID="rfvBody" runat="server" ControlToValidate="txtComposeBody"
                        ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button runat="server" CssClass="invite-friend-btn invite-friend-btn-notification"
                        Text="Send" ID="btnCompose" UseSubmitBehavior="false" />
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="hdnSentTo" runat="server" />
    <input type="hidden" id="hdnReplySubject" runat="server" />
    <input type="hidden" id="hdnReplyBody" runat="server" />
    <input type="hidden" id="hdnNext" runat="server" />
    <input type="hidden" id="hdnPrevious" runat="server" />
    </div> </div> </div> </div></div></div></div></div>
</asp:Content>
