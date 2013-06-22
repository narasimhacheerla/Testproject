<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderAfterLoggingIn.ascx.cs"
    Inherits="Huntable.UI.HeaderAfterLoggingIn" %>
<%@ Register Src="UserControls/UserFeedAlerts.ascx" TagName="UserFeedAlerts" TagPrefix="uc1" %>
<link href="https://huntable.co.uk/css/style-innerpage.css" type="text/css" rel="stylesheet" />
<link href="https://huntable.co.uk/JqueryPopup/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
<div style="position: fixed; width: 100%; z-index: 300;">
    <div id="popupmessage2" runat="server" visible="False" class="poupmessage2" style="width: 1000px;
        margin: 0px auto; background-color: White;">
        Message Saved Successfully
        <asp:Image runat="server" Width="21px" Height="13px" ID="Following" ImageUrl="images/tick.png"
            class="tick" />
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

                    url.src = "http://huntable.co.uk/ShareMail.aspx";
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
                $('a.close, #fade').on('click', function () { //When clicking on the close or fade layer...
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


            });
            function fdout() {
                $('#fade').remove();

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
                <a title="Huntable - Find Hunt Hire" href="Default.aspx">
                  
                    <img id="Img1" title="Huntable - Find Hunt Hire" runat="server" alt="Huntable - Find Hunt Hire"
                        src="~/HuntableImages/logo.png" width="158" height="40" />
                </a>
            </div>
            <div class="menu1">
                <div id="menu2" runat="server">
                    <ul class="menu-list">
                        <li><a id="HomeForall" runat="server">
                            <img id="Img2" title="Home" alt="Home" src="https://huntable.co.uk/HuntableImages/icon-home.png" width="14"
                                height="15" />Home</a></li>
                        <li><a runat="server" title="What is Huntable" href="AboutUs.aspx">
                            <img id="Img3" title="What is Huntable" alt="What is Huntable" src="https://huntable.co.uk/HuntableImages/icon-what.png"
                                width="14" height="15" />What is Huntable</a></li>
                        <li><a title="Find Friends" runat="server" id="findhref" >
                            <img id="Img4" title="Find Friends" alt="Find Friends" src="https://huntable.co.uk/HuntableImages/icon-friends.png"
                                width="14" height="15" /><asp:Label runat="server" ID="FindLabel"></asp:Label></a></li>
                        <li><a runat="server" title="Recruiter" href="Recruiter.aspx">
                            <img id="img5" title="Recruiter" alt="Recruiter" src="https://huntable.co.uk/images/icon-recruiter.png"
                                width="14" height="15" />Recruiter</a></li></ul>
                </div>
                <div class="test">
                    <div class="list-main">
                        <a class="name1" >
                            <asp:Label ID="lblUserName" runat="server" />
                        </a>
                        <ul class="list">
                            <li><a id="_profile" runat="server">Profile</a></li>
                            <li><a runat="server" href="Signout.aspx">Signout</a></li></ul>
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
    <div id="main-menu001">
        <div class="main-menu-inner">
            <div id="menu" runat="server">
                <div id="navigation">
                    <ul id="nav">
                        <li style="margin-top: -10px;"><a id="Homepageafterlogin" runat="server">Home</a></li>
                        <li style="margin-top: -10px;"><a href="VisualCV.aspx" id="aprofile" runat="server">
                            Profile</a>
                            <ul runat="server" id="liprofile">
                                <li><a runat="server" href="Visualcv.aspx">View Profile</a></li>
                                <li><a runat="server" href="EditProfilePage.aspx">Edit Profile</a></li>
                                <li><a runat="server" href="Endorsement.aspx">Endorsement</a></li>
                                <%-- <li><a href="">Export</a></li>--%>
                                <li><a href="">Print</a></li>
                                <li class="last">
                                <li class="last"><a id="ssss" href="#?w=500" class="poplight" rel="popupShare">Share</a></li>
                            </ul>
                            <li style="margin-top: -10px;"><a runat="server" href="JobsearchTips.aspx">Jobs</a>
                                <ul>
                                    <li><a runat="server" href="JobsearchTips.aspx">Search Jobs</a></li>
                                    <li>
                                        <%--<a href="PostJob.aspx">Post Jobs Availability</a>--%><asp:LinkButton ID="lnkJobs"
                                            runat="server" Text="Post Jobs Availability" OnClick="lnkJobsClick"></asp:LinkButton>
                                    </li>
                                    <li><a runat="server" href="CustomizeJobsSalary.aspx">Customise Your Jobs</a></li>
                                    <li class="last"><%--<a id="buycredits" runat="server">Buy Job Credits</a>--%>
                                    <asp:LinkButton ID="lnkbuy"
                                            runat="server" Text="Buy Job Credits" OnClick="lnkBuyClick"></asp:LinkButton>
                                    </li></ul>
                            </li>
                        <li style="margin-top: -10px;"><a runat="server" href="Customize-User.aspx">Feeds</a>
                            <ul>
                                <li><a runat="server" href="Customize-User.aspx">Customize Your Feeds</a></li>
                                <li><a runat="server" href="CustomizeFeedsIndustry.aspx">Industry</a></li>
                                <li><a runat="server" href="CustomizeFeedsSkill.aspx">Skill</a></li>
                                <li><a runat="server" href="CustomizeFeedsCountry.aspx">Country</a></li>
                                <li><a runat="server" href="CustomizeFeedsInterest.aspx">Interest</a></li>
                                <li class="last"><a runat="server" href="Customize-User.aspx">Friends</a></li></ul>
                        </li>
                        <li style="margin-top: -10px;"><a runat="server" href="companieshome.aspx">Companies</a>
                            <ul>
                                <li><a runat="server" href="companieshome.aspx">View Companies</a></li>
                                <li><a id="aviewprofile" runat="server" visible="false">View Profile</a></li>
                                <li><a runat="server" href="company-registration.aspx">Add Company</a></li>
                                <li><a href="companyregistration2.aspx" id="_Editprofile" runat="server" visible="false">
                                    Edit Profile</a></li>
                            </ul>
                        </li>
                        <div runat="server" id="adminDiv">
                            <li><a runat="server" href="#">Admin</a>
                                <ul>
                                    <li><a id="A1" href="AdminExportData.aspx" runat="server">Export Data</a></li>
                                    <li><a id="A2" href="EmailTemplateEditPage.aspx" runat="server">Email Template Edit
                                        Page</a></li>
                                    <li><a id="A3" href="InsertNews.aspx" runat="server">Insert News</a></li>
                                    <li><a id="A4" href="AdminInvoices.aspx" runat="server">Invoices</a></li>
                                    <li class="last"><a runat="server" href="AdminFunctions.aspx">Admin Functions</a></li></ul>
                            </li>
                        </div>
                        <li style="margin-top: -10px;" class="hasmore"><a runat="server" href="MyAccount.aspx"><span>Account</span></a></li>
                        <li style="margin-top: -10px;" class="hasmore"><a style="border-right-color: currentColor;
                            border-right-width: 0px; border-right-style: none;" runat="server" href="UserEmailNotification.aspx">
                            <span style="padding-right: 0px;">Settings</span></a></li>
                    </ul>
                </div>
                <div id="popupShare" class="popup_block">
                    <iframe id="url" src="http://huntable.co.uk/ShareMail.aspx" style="border: none;" width="100%" height="280px"
                        frameborder="0" scrolling="no"></iframe>
                </div>
                <div class="message-inbox" id="message" runat="server" style="margin-top: -10px;
                    margin-left: 0px;">
                  <a href="MessageInbox.aspx" runat="server"> <img id="Img5" alt="inbox" src="https://huntable.co.uk/HuntableImages/icon-inbox.png" width="20" height="14" /></a> <a
                        href="MessageInbox.aspx" class="count" id="hypMessageCount" runat="server">
                        <asp:Label runat="server" ID="lblMessagesCount"></asp:Label></a></div>
                <uc1:UserFeedAlerts ID="UserFeedAlerts1" runat="server" />
            </div>
            <div class="search-main001">
                <div class="test1">
                    <div class="list-main1">
                        <h3 class="name3" style="font-size: 12px;">
                            People
                        </h3>
                    </div>
                </div>
                <div class="search">
                    <input id="txtSearch" runat="server" onblur="if (this.value == '') {this.value ='Search here...';}"
                        class="textbox-search" onfocus="if (this.value =='Search here...') {this.value ='';}"
                        name="email" value="Search here..." type="text" />
                    <asp:ImageButton ID="imgSearchUsers" ValidationGroup="searchforusers" runat="server"
                        alt="search" OnClick="BtnSearchForUsersClick" src="https://huntable.co.uk/HuntableImages/icon-search.png"
                        CssClass="imageAlign" Width="30" Height="22" />&nbsp;&nbsp;&nbsp; <a class="advanced"
                            href="https://huntable.co.uk/UserSearch.aspx">Advanced</a>
                </div>
            </div>
            <%--<div class="message-inbox" id="Div1" visible="false" runat="server" style="margin-top: -7px;
                margin-left: 426px; width: 30px; height: 13px;">
                <a href="MessageInbox.aspx" class="count">
                    <img id="Img7" alt="inbox" src="HuntableImages/icon-inbox.png" width="20" height="14"
                        style="position: relative; top: -6px;" />
                </a>
            </div>
            <div class="bell" id="Div2" visible="false" runat="server" style="margin-top: -12px;
                width: 30px; height: 13px; margin-left: 461px;">
                <img id="Img6" alt="inbox" src="Images/bell01.png" width="20" height="14" style="position: relative;
                    top: -7px;" />
            </div>--%>
        </div>
    </div>
</div>
<div style="height: 80px;">
</div>
