<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePageTour.aspx.cs" Inherits="Huntable.UI.HomePageTour" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Src="GridUserControl.ascx" TagName="GridUserControl" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/YourAccountAtGlance.ascx" TagName="uclaccountat"
    TagPrefix="uc7" %>
<%@ Register Src="~/UserControls/FriendsToInvite.ascx" TagName="uclfriendsinvite"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/TopNewsDesign.ascx" TagName="ucnewsdesign" TagPrefix="uc9" %>
<%@ Register Src="~/UserControls/CvStatistics.ascx" TagName="uclstatistcs" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/InvitingFriends.ascx" TagName="uclinvitefriends"
    TagPrefix="uc5" %>
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
<link rel="stylesheet" type="text/css" href="css/skin.css" />
<link rel="stylesheet" type="text/css" href="css/counter.css" />

<!-- Textarea onclick event script Begins -->
<script type="text/javascript">
//	<![CDATA[
    var TextMessage = 'Got something to say, ask, post, share…';
    function SetMsg(txt, active) {
        if (txt == null) return;

        if (active) {
            if (txt.value == TextMessage) txt.value = '';
        } else {
            if (txt.value == '') txt.value = TextMessage;
        }
    }

    window.onload = function () { SetMsg(document.getElementById('TxtareaInput', false)); }
//]]>
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

    $(document).ready(function () {
        $('.popup').hide();
        $('#skip1').show();

    });
    function skipWindow(no) {
        $('.popup').hide();
        $('#skip' + no).fadeIn();
        if (no == 2) {
            window.scrollBy(0, 50);
            
        }
    }
    function ValidateText() {
        var searchText = document.getElementById("<%=txtUserSearchKeyword.ClientID%>").value;

        if (searchText == "e.g: Name, Company, Skill, Job title" || searchText == null) {
            alert("Please enter Name/Company/Skill/Job Title in search box");
            document.getElementById("<%=txtUserSearchKeyword.ClientID%>").focus();
            return false;
        }
        return true;
    }
</script>
 <script type="text/javascript">
     var TextMessage = 'Got something to say, ask, post, share…';
     function SetMsg(txt, active) {
         if (txt == null) return;

         if (active) {
             if (txt.value == TextMessage) txt.value = '';
         } else {
             if (txt.value == '') txt.value = TextMessage;
         }
     }

     window.onload = function () { SetMsg(document.getElementById('txtTellworld', false)); }
     function showIt(elId) {
         var el = document.getElementById(elId);
         el.scrollIntoView(true);
     }
                </script>
<!-- Tooltip Script Ends -->
<script>
    function scrollWindow() {
        window.scrollTo(100, 500);
    }
</script>
</head>

<body><div class="blur" style= "z-index:500;margin-top: -79px;">&nbsp;</div>



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
<div id="content-section">
        <div id="content-inner">
        <div class="r-tool-tip" style="z-index:598;">
      <div class="popup" id="skip1" >
        <p class="popup-content">You can customize the feeds you recieve by choosing your choice of Skills, Industry, Country, Interest and anyone person you like to follow.</p>
         <div class="skip" ><a href="#" onclick="skipWindow(2);">skip</a></div>
         <div class="popup-up-arrow"></div>
      </div>
      
      <div class="popup popup1" onclick="jumpScroll()" id="skip2">
        <p class="popup-content">You can customize the jobs you recieve by choosing your choice of Salary, Industry, Skill, Job type &amp; Country.
 You will recieve jobs only what you want here. You can always search for more jobs from Search job section.</p>
         <div class="skip"><a href='javascript:scrollWindow()' onclick="skipWindow(3);">skip</a></div>
         
         <div class="popup-up-arrow"></div>
      </div>
       
      <div class="popup popup2" id="skip3">
        <p class="popup-content">Grow your network and connect with other users of interest. You can also invite your friends &amp; earn money through our affiliate programme with just
few clicks.</p>
         <div class="skip"><a href="HomePageAfterLoggingIn.aspx">skip</a></div>
         <div class="popup-right-arrow"></div>
      </div>
 </div>
            <div class="content-inner-left">
                <div class="profile-main">
                    <div class="profile-left">
                        <a id="A1" href="~/PictureUpload.aspx" runat="server">
                            <asp:Image runat="server" ID="imgProfile" class="profile-pic" Width="76" Height="81"
                                alt="Profile-pic" /></a><br />
                        <a href="ViewUserProfile.aspx">
                            <asp:Label class="profile-name" ID="lblUserName" runat="server" /></a>
                        <div class="profile-complete">
                            <asp:Label ID="lblPercentCompleted" class="profile-complete" runat="server"></asp:Label><asp:Label
                                runat="server" ID="lblAcccount"></asp:Label><br />
                            <eo:progressbar ID="ProgressBar2" runat="server" Width="90px" BackgroundImage="00060301"
                                BackgroundImageLeft="00060302" BackgroundImageRight="00060303" ControlSkinID="None"
                                IndicatorImage="00060304" ShowPercentage="True" Value="30">
                            </eo:progressbar>
                        </div>
                    </div>
                    <div class="profile-right">
                        <div class="tell-the-world">
                            <asp:TextBox Height="35px" Width="485px" Text="Got something to say, ask, post, share…"
                                onfocus="SetMsg(this, true);" onblur="SetMsg(this, false);" ID="txtTellworld"
                                CssClass="textarea-profile" runat="server"></asp:TextBox>
                            <asp:Button ID="btnJoin" runat="server"  Text="Tell the World"
                                CssClass="button-green button-green-profile" />
                        </div>
                        <span class="last-login">Last Login Time:<b><asp:Label ID="lblLogDate" runat="server"></asp:Label></b>&nbsp;<b><asp:Label
                            ID="lblLogin" runat="server"></asp:Label></b></span> <span class="last-update">Last
                                Profile Updated on: <b>
                                    <asp:Label ID="lblProfile" runat="server"></asp:Label></b></span>
                    </div>
                </div>
                <div class="profile-search">
                    <b>Search For anyones Profile now:</b>
                    <asp:TextBox ID="txtUserSearchKeyword" runat="server" onblur="if (this.value == '') {this.value ='e.g: Name, Company, Skill, Job title';}"
                        onfocus="if (this.value =='e.g: Name, Company, Skill, Job title') {this.value ='';}"
                        value="e.g: Name, Company, Skill, Job title" class="textbox-search textbox-search-inner"></asp:TextBox>
                    <asp:Button class="button-orange button-orange-search" ID="btnUserSearch" runat="server"
                        Text="Search" OnClientClick="return ValidateText()" />
                </div>
                <div class="all-feeds-main-inner" style="height: 100%;">
                    <div id="tabswitch">
                        <ul>
                            <li class="tab1" style="width: 300px;"><b>All Feeds:&nbsp;&nbsp;</b><a href="CustomizeFeedsSkill.aspx">Skills</a><a
                                href="CustomizeFeedsIndustry.aspx">Industry</a><a href="CustomizeFeedsCountry.aspx">Country</a><a
                                    href="CustomizeFeedsInterest.aspx">Interests</a></li>
                            <li style="float: right;" class="tab2"><b>Opportunities:&nbsp;&nbsp;</b><a href="CustomizeJobsSalary.aspx">Salary</a><a
                                href="CustomizeJobsUsers.aspx">Experience</a><a href="CustomizeJobsCountry.aspx">Location</a></li>
                        </ul>
                        <div class="tab-container tab1">
                            <div class="all-feeds-list">
                                <asp:UpdatePanel ID="update" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="hField" Value="" runat="server" />
                                        <uc4:GridUserControl ID="gvUserFeeds" runat="server" />
                                        <asp:Button ID="btnShowMoreFeeds"  Style="margin-left: 180px;"
                                            class="show-more" Text="Show more" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:HiddenField runat="server" ID="hfPageSize" />
                                <asp:UpdateProgress ID="updateProgressBar1" AssociatedUpdatePanelID="update" runat="server">
                                    <ProgressTemplate>
                                        <img src="images/loadingBar.gif" style="height: 50px" alt="Update in progress" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                        <div class="tab-container tab2">
                            <div class="notification-right notification-right-home">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="hJobfield" Value="" runat="server" />
                                        <asp:ListView ID="lvJobs" runat="server" ClientIDMode="Static">
                                            <ItemTemplate>
                                                <div class="notification-head">
                                                    <a id="A2" href='<%#UrlGenerator(Eval("Id")) %>'
                                                        class="grey-link" runat="server">
                                                        <img src='<%# Eval("ProfileImagePath") %>' class="profile-pic profile-pic2" width="76"
                                                            height="81" alt="Job" /></a> <a href="<%#UrlGenerator(Eval("Id")) %>"
                                                                class="grey-link">
                                                                <%# Eval("Title")%></a><br />
                                                    <span class="blue-color" style="margin-left: 65px;">Job Type :</span>
                                                    <%# Eval("MasterJobType.Description")%>
                                                    <br />
                                                    <span class="blue-color" style="margin-left: 24px;">Experience Req :</span>
                                                    <%# Eval("MinExperience")%>
                                                    Minimum<br />
                                                    <span class="blue-color" style="margin-left: 65px;">Industry :</span>
                                                    <%# Eval("MasterIndustry.Description")%>
                                                    <br />
                                                    <span class="blue-color" style="margin-left: 76px;">Salary :</span>
                                                    <%# Eval("Salary")%><%# Eval("MasterCurrencyType.Description")%><br />
                                                    <span class="blue-color" style="margin-left: 76px;">Skill :</span>
                                                    <%# Eval("Salary")%><%# Eval("MasterSkill.Description")%><br />
                                                    <span class="blue-color" style="margin-left: 105px;">JobPostedDate : </span>
                                                    <asp:Label ID="Label21" runat="server" Text='<%#Eval("CreatedDateTime", "{0:MM/dd/yyyy}")%>'></asp:Label><br/>
                                                    <asp:Label ID="Label31" Text='<%# Eval("JobDescription")%>' runat="server"></asp:Label><a class="orange-link" href="<%#UrlGenerator(Eval("Id")) %>" >more</a> <br />
                                                    <%--<div><asp:Button ID="btn1" runat="server" Text="Apply now +" OnClick="Applynowclicked"/></div>--%>
                                                    <div id="Div1" class="notification-links notification-links1 notification-links-home"
                                                        runat="server" visible='<%# Eval("IsUserAlreadyToThisJob") %>'>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="invite-friend-btn invite-friend-btn1"
                                                            Text="Apply now + " CommandName="Apply" CommandArgument='<%# Eval("Id")%>' />
                                                    </div>
                                                    <asp:Image runat="server" Width="20" Height="20" ID="Following" Visible='<%# Eval("IsAlreadyNotApplied") %>'
                                                        ImageAlign="Right" ImageUrl="images/tick.png" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <asp:Button ID="Button1"  Style="margin-left: 180px;" class="show-more"
                                            Text="Show more" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="updateProgress1" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                                    <ProgressTemplate>
                                        <img src="images/loadingBar.gif" style="height: 50px" alt="Update in progress" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </div>
                </div>
                <uc9:ucnewsdesign ID="topnews" runat="server" />
            </div>
            <!-- content inner left ends -->
            <div id="rightDiv" runat="server" class="content-inner-right">
                <div class="box-right">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <uc3:uclfriendsinvite ID="ucl3" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc4:uclstatistcs ID="ucl4" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc7:uclaccountat ID="uc3" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc5:uclinvitefriends ID="uc5" runat="server" />
                </div>
                <!-- content inner right ends -->
                <!-- content inner ends -->
                <!-- content section ends -->
                <!-- Textarea onclick event script Begins -->
                <script type="text/javascript">
                    var TextMessage = 'Got something to say, ask, post, share…';
                    function SetMsg(txt, active) {
                        if (txt == null) return;

                        if (active) {
                            if (txt.value == TextMessage) txt.value = '';
                        } else {
                            if (txt.value == '') txt.value = TextMessage;
                        }
                    }

                    window.onload = function () { SetMsg(document.getElementById('txtTellworld', false)); }
                </script>
            </div>
        </div>
    </div>
<!-- content section ends -->
<!-- Range Slider Script Begins -->
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js" type="text/javascript"></script>
<script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
<!-- My flip counter script, REQUIRED -->
	<script type="text/javascript" src="js/flipcounter.js"></script>

	<!-- Style sheet for the counter, REQUIRED -->
	
	
<!-- Range Slider Script Ends -->



   

</body>
</html>


</asp:Content>
