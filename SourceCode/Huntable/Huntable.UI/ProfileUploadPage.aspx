<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfileUploadPage.aspx.cs"
    Inherits="Huntable.UI.HProfileUploadPage" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Src="~/UserControls/ProfileCompletionSteps.ascx" TagPrefix="uc1" TagName="ProfileCompletion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function validate() {
            var uploadcontrol = $('#MainContent_uploadResume').val();
            if (uploadcontrol == "") {
                alert("Please choose a file!");
                return false;
            }
            //Regular Expression for fileupload control.
            var allowedexts = ".doc.DOC.docx.DOCX.txt.TXT.rtf.RTF.pdf.PDF";
            if (uploadcontrol.length > 0 && uploadcontrol.indexOf('.') > 0) {
                var fullfilename = uploadcontrol.split('.');
                var fileext = fullfilename[fullfilename.length - 1];
                //Checks with the control value.
                if (allowedexts.indexOf(fileext) > 0) {
                    return true;
                }
                else {
                    //If the condition not satisfied shows error message.
                    $('#MainContent_uploadResume').val('');
                    alert("Only .DOC,.DOCX,.TXT,.PDF files are allowed!");
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Header section ends -->
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="profile-box-main">
                    <div class="add-image">
                        <a href="#?w=750" class="add-image-link poplight" rel="popup2">+ Add Photo</a> <a
                            href="#">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/add-photo-thumb.jpg"
                                CssClass="profile-pic" Width="79" Height="91" OnClick="ImageButton1Click" />
                            <%-- <img src="images/add-photo-thumb.jpg" class="profile-pic" width="79" height="91"
                                alt="add-photo" /></a>--%>
                    </div>
                    <div class="profile-upload">
                        <%--<a href="#?w=500" class="button-green floatleft poplight" rel="popup3">Upload Your Profile</a>--%>
                        <asp:FileUpload ID="uploadResume" runat="server" />
                        <asp:Button ID="btnUploadProfile" runat="server" Text="Upload Your Profile" BorderStyle="None"
                            CssClass="button-green floatleft poplight   greenup" OnClientClick="return validate();" OnClick="BtnprofileClick" />
                        <asp:Label ID="lblUploadResumeMessage" runat="server" />
                        <div class="blue-box">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPercentCompleted" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        completed
                                    </td>
                                    <td>
                                        <eo:progressbar id="ProgressBar2" runat="server" width="90px" backgroundimage="00060301"
                                            backgroundimageleft="00060302" backgroundimageright="00060303" controlskinid="None"
                                            indicatorimage="00060304" showpercentage="True" value="30">
                                        </eo:progressbar>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <span style="color: Black;"><strong>Complete Your profile Quickly</strong></span><br />
                            <br />
                            <img src="images/icon-doc.png" width="26" height="26" alt="word-doc" />
                            <a href="#" class="import-link">Import your resume to build a complete profile in minutes</a>
                        </div>
                    </div>
                </div>
                <b class="or">or </b>
                <div class="import-profile">
                    <div class="import-profile-left">
                        <strong>Import Your Profile</strong><br />
                        Instantly update your profile by<br />
                        Importing from Linkedin or Facebook
                    </div>
                    <div class="import-profile-right">
                        <%--  <asp:ImageButton ID="imgBtnFB" runat="server" PostBackUrl="https://www.facebook.com/login.php"
                            ImageUrl="images/import-facebook.png" Width="133" Height="41" AlternateText="Facebook" />
                        &nbsp;&nbsp;
                        <asp:ImageButton ID="btnImportFromLinkedin" runat="server" ImageUrl="images/import-linkedin.png"
                            Width="133" Height="41" alt="Linked in" OnClick="BtnImportFromLinkedinClick" />--%>
                        <asp:ImageButton ID="imgBtnFB" runat="server" ImageUrl="images/import-facebook.png"
                            Width="133" Height="41" AlternateText="Facebook" PostBackUrl="https://www.facebook.com/login.php"
                            CausesValidation="false" />&nbsp;&nbsp;
                        <asp:ImageButton ID="imgBtnLIn" runat="server" ImageUrl="images/import-linkedin.png"
                            Width="133" Height="41" AlternateText="Linked In" OnClick="BtnImportFromLinkedinClick"
                            CausesValidation="false" />
                    </div>
                </div>
                <b class="or">or </b>
                <div align="center">
                    <asp:Button ID="btndetails" runat="server" Text="Enter Details" class="button-green floatleft"
                        Style="margin-left: 280px;" OnClick="BtndetailsClick" />
                </div>
            </div>
            <!-- content inner left ends -->
            <uc1:profilecompletion id="profComp" runat="server"></uc1:profilecompletion>
            <!-- content inner right ends -->
        </div>
        <!-- content inner ends -->
    </div>
    <!-- content section ends -->
    <!-- Range Slider Script Begins -->
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <!-- Range Slider Script Ends -->
    <!-- Footer section ends -->
    <!-- Popup Div Section Starts -->
    <div id="popup2" class="popup_block">
        <div class="apply-job">
            <div class="add-left">
                <strong>Current Photo</strong>
                <img src="images/add-photo-thumb.jpg" class="profile-pic" width="79" height="91"
                    alt="add-photo" />
            </div>
            <div class="add-right">
                <strong>Upload a Photo</strong><br />
                You can upload a JPG, GIF or PNG file (File size limit is 4 MB)<br />
                <input type="file" />
                <div class="upload">
                    <a href="#" class="button-green floatleft">Upload Photo</a> &nbsp; &nbsp;or&nbsp;
                    &nbsp; <a href="#" class="accounts-link">Cancel</a><br />
                    <br />
                    By Clicking "Upload Photo". You certify that you have the right to distribute the
                    photo and that i does not visible the <a href="#" class="accounts-link">User Agreement</a>
                </div>
            </div>
        </div>
        
    </div>
    <div id="popup3" class="popup_block">
        <div class="apply-job">
            <div class="add-right" style="width: 480px;">
                <h3 class="popup-head">
                    Import Your Resume
                </h3>
                Upload Microsoft Word, PDF, text of HTML files of up to 500KB<br />
                <input type="file" /><br />
                <br />
                <a href="#" class="button-green floatleft">Upload Photo</a> &nbsp; &nbsp;or&nbsp;
                &nbsp; <a href="#" class="accounts-link">Cancel</a><br />
                <br />
            </div>
        </div>
    </div>
    <div id="hideshow" style="visibility: hidden;">
            <div id="fade_in">
            </div>
            <div id="showdiv">
                <div class="popup_blocks" style="margin-top: 131px">
                    <div class="popups">
                        <a href="javascript:hideDiv()">
                            <img src="images/close_pop.png" class="cntrl" title="Close"></a>
                        <p>
                            Dear User,<br />
                            <br />
                            Although we take every measure to import all the details from your profile, there
                            could be few details missing.
                            <br />
                            We recommend, that you revise your profile once again to make sure all the details
                            are right.<br />
                            <br />
                            <span>Note: You can add pictures, Achievements, &amp; Videos to each experience, when
                                you click - EDIT under experience.</span>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <!--END POPUP-->
        <script language="javascript" type="text/javascript">
            function hideDiv() {
                window.location.replace("EditProfilePage.aspx");
                if (document.getElementById) { // DOM3 = IE5, NS6 
                    document.getElementById('hideshow').style.visibility = 'hidden';
                }
                else {
                    if (document.layers) { // Netscape 4 
                        document.hideshow.visibility = 'hidden';
                    }
                    else { // IE 4 
                        document.all.hideshow.style.visibility = 'hidden';
                    }
                }
            }

            function showDiv() {
                if (document.getElementById) { // DOM3 = IE5, NS6 
                    document.getElementById('hideshow').style.visibility = 'visible';
                }
                else {
                    if (document.layers) { // Netscape 4 
                        document.hideshow.visibility = 'visible';
                    }
                    else { // IE 4 
                        document.all.hideshow.style.visibility = 'visible';
                    }
                }
            } 
        </script>
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
    <script type="text/javascript">

        var _gaq = _gaq || [];

        _gaq.push(['_setAccount', 'UA-32991521-1']);

        _gaq.push(['_trackPageview']);



        (function () {

            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;

            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';

            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);

        })();

 
    </script>
</asp:Content>
