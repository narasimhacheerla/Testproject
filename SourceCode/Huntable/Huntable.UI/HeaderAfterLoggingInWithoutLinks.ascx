<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderAfterLoggingInWithoutLinks.ascx.cs"
    Inherits="Huntable.UI.HeaderAfterLoggingInWithoutLinks" %>
<link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
<link href="JqueryPopup/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
<div style="position: fixed; width: 100%; z-index: 300;">
<div ID="popupmessage2" runat="server" Visible="False" class="poupmessage2" style="width: 1000px;
margin: 0px auto; background-color:White;">
         
        Message Saved Successfully
        <asp:Image runat="server" Width="21px" Height="13px" ID="Following" 
                                            ImageUrl="images/tick.png" class="tick"/>
    </div>
    <div id="header-section">
        <script type="text/javascript">
            $(document).ready(function () {
                $('div.test').click(function () {
                    $('ul.list').slideToggle('medium');
                });
            });
            $(document).ready(function () {
                $('div.test1').click(function () {
                    $('ul.list1').slideToggle('medium');
                });
            });
        </script>
        <script type="text/javascript">
            $(document).ready(function () {

                //When you click on a link with class of poplight and the href starts with a # 
                $('a.poplight[href^=#]').click(function () {
                    var popID = $(this).attr('rel'); //Get Popup Name
                    var popURL = $(this).attr('href'); //Get Popup href to define size

                    url.src = "ShareMail.aspx";
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


            });

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
        <script language="javascript" type="text/javascript">
            function ShowSharingPopUp(actin) {
                document.forms[0].action = actin;
                window.open(actin, 'mywindow', 'width=500,height=300,toolbar=no, location=no,directories=no,statusbar=no,menubar=no,scrollbars=no,copyhistory=no, resizable=yes');
                document.forms[0].target = 'mywindow';
                return true;
            }
        </script>
        <div id="header-inner">
            <div class="logonew">
                
                    <img id="Img1" title="Huntable - Find Hunt Hire" runat="server" alt="Huntable - Find Hunt Hire"
                        src="~/HuntableImages/logo.png" width="158" height="40" />
            </div>
            <div class="menu1">
                <div class="test">
                    <div class="list-main">
                        <a class="name1" href="#">
                            <asp:Label ID="lblUserName" runat="server" />
                        </a>
                        <ul class="list">
                            <li><a id="_profile" runat="server">Profile</a></li>
                            <li><a href="Signout.aspx">Signout</a></li></ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
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
</div>
<div style="height: 80px;">
</div>

