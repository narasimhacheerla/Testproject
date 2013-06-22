<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="~/search.aspx.cs" Inherits="Huntable.UI.search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Header section ends -->
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <h1 class="welcome-heading">
                    Welcome to <strong>HUNTABLE</strong></h1>
                <br />
                <b style="color: #028ca2;">Lets get started with few features of Huntable.</b>
                <br />
                <br />
                <h3 class="login-heading1">
                    Searching &amp; Connecting with other users</h3>
                <ul class="list-green list-green-jobs">
                    <li>You can customize the feed you receive by skill, interest, person, company etc..</li>
                    <li>Just click on the</li>
                    <li>Chat with users who are online. Like the or profile, you can even conduct interview
                        more...</li>
                </ul>
                <a href="#" class="button-green floatright">Ok, I got it</a>
            </div>
            <!-- content inner left ends -->
            <div class="content-inner-right">
                <iframe height="215" frameborder="0" class="profile-pic" width="296" src="http://www.youtube.com/embed/kl1ujzRidmU">
                </iframe>
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
   
    <!-- Footer section ends -->
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
