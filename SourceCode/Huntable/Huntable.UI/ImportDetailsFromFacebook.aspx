<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ImportDetailsFromFacebook.aspx.cs" Inherits="Huntable.UI.ImportDetailsFromFacebook" Debug="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Textarea onclick event script Begins -->
    <script type="text/javascript">
        var TextMessage = 'Write Your Message Here...';
        function SetMsg(txt, active) {
            if (txt == null) return;

            if (active) {
                if (txt.value == TextMessage) txt.value = '';
            } else {
                if (txt.value == '') txt.value = TextMessage;
            }
        }

        window.onload = function () { SetMsg(document.getElementById('TxtareaInput', false)); }
    </script>
    <script type="text/javascript">
        var TextMessage = 'Write Your Message Here...';
        function SetMsg(txt, active) {
            if (txt == null) return;

            if (active) {
                if (txt.value == TextMessage) txt.value = '';
            } else {
                if (txt.value == '') txt.value = TextMessage;
            }
        }

        window.onload = function () { SetMsg(document.getElementById('TxtareaInput1', false)); }
    </script>
    <script type="text/javascript">
        var TextMessage = 'Add your message here...';
        function SetMsg(txt, active) {
            if (txt == null) return;

            if (active) {
                if (txt.value == TextMessage) txt.value = '';
            } else {
                if (txt.value == '') txt.value = TextMessage;
            }
        }

        window.onload = function () { SetMsg(document.getElementById('TxtareaInput-email', false)); }
    </script>
    <!-- Textarea onclick event script Ends -->
    <!-- Tooltip Script Begins -->
    <script type="text/javascript" src="http://code.jquery.com/jquery-latest.js"></script>
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
    <!-- Tooltip Script Ends -->
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="popup9" class="popup_block">
        <div class="apply-job">
            <h2>
                Upload .csv file only in the format shown here</h2>
            <input type="file" /><br />
            <br />
            <a href="#" class="button-orange floatleft">Upload File</a>
            <div style="float: left; clear: both; margin-top: 20px; width: 100%;">
                <table class="csv-table">
                    <tr>
                        <th valign="top">
                            S.no
                        </th>
                        <th valign="top">
                            First Name
                        </th>
                        <th valign="top">
                            E-mail Address
                        </th>
                    </tr>
                    <tr>
                        <td valign="top">
                            <strong>1</strong>
                        </td>
                        <td valign="top">
                            Christopher
                        </td>
                        <td valign="top">
                            christopher@gmail.com
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <strong>2</strong>
                        </td>
                        <td valign="top">
                            James
                        </td>
                        <td valign="top">
                            james@gmail.com
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <strong>3</strong>
                        </td>
                        <td valign="top">
                            Christopher
                        </td>
                        <td valign="top">
                            christopher@gmail.com
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <strong>4</strong>
                        </td>
                        <td valign="top">
                            James
                        </td>
                        <td valign="top">
                            james@gmail.com
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <!-- Range Slider Script Begins -->
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <!-- Range Slider Script Ends -->
    <!-- Popup Div Section Begins -->
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
    <!-- Share Tab Script begins -->
    <script type="text/javascript">
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
    </script>
    <!-- Share Tab Script Ends -->
</asp:Content>
