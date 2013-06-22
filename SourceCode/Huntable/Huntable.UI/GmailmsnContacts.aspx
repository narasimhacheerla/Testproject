<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="GmailmsnContacts.aspx.cs"
    Inherits="Huntable.UI.GmailmsnContacts" %>

<%@ Register Src="UserControls/InvitingFriends.ascx" TagPrefix="snovaspace" TagName="Friends" %>
<%@ Register Src="~/UserControls/HAccountsAtGlance.ascx" TagPrefix="uc1" TagName="Accounts" %>
<%@ Register Src="~/UserControls/ImportContacts.ascx" TagPrefix="uc2" TagName="Import" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="Styles/NewStylesheet.css" rel="Stylesheet" type="text/css" />
     <script type="text/javascript">
         $(function () {

             var msie6 = $.browser == 'msie' && $.browser.version < 7;
             if (!msie6) {
                 var top = $('#bx15').offset().top;
                 $(window).scroll(function (event) {
                     var y = $(this).scrollTop();
                     if (y >= 621) { $('#bx15').addClass('fixed'); }
                     else { $('#bx15').removeClass('fixed'); }
                 });
             }
         });</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left" style="height:855px">
                <div class="contacts-head">
                    <h3 style="float: left;">
                        Your Contacts
                    </h3>
                    <a href="InvitationsReport.aspx" class="learn-more">View all sent invitations</a>
                </div>
                <div class="contacts-select" style="width: 635px;">
                    <input type="checkbox" class="checkbox" />
                    Select all
                    <img style="float: right;" src="images/windows-live.jpg" width="107" height="27"
                        alt="Windows-live" />
                </div>
                <div class="contacts-select-inner" style="width: 635px;">
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> securenext@hotmail.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> glendamutyhep@hotmail.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> brandleewov@hotmail.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> admin@usaccounts.co.uk</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> ameen@globalbheema.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> zebarshya@gmail.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> ami@msn.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> madan99@gmail.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> natesanking@yahoo.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> securenext@hotmail.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> securenext@hotmail.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> glendamutyhep@hotmail.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> brandleewov@hotmail.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> admin@usaccounts.co.uk</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> ameen@globalbheema.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> zebarshya@gmail.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> ami@msn.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> madan99@gmail.com</a></p>
                    <p>
                        <input type="checkbox" class="checkbox" /><a href="#"> natesanking@yahoo.com</a></p>
                </div>
                <a href="InviteFriends.aspx" class="show-more">Show More</a> <a href="#" class="button-green contact-btn">
                    Invite to Connect (370)</a>
            </div>
            <!-- content inner left ends -->
            <div class="content-inner-right">
                <uc1:Accounts ID="AcctsAtGlance" runat="server" />
                <p class="margin-top-visible">
                    &nbsp;</p>
                  <snovaspace:Friends ID="ucInvitaionFriends" runat="server" />
             <div id="bx15"> <div class="box-right">
                    <uc2:Import ID="impContacts" runat="server" />
                </div></div>
            </div>
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
    <!-- Footer section ends -->
    </div>
</asp:Content>
