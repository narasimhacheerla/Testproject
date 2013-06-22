<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HuntableTourCustomizeFeeds.aspx.cs" Inherits="Huntable.UI.CustomizeFeeds" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left" style="font-family: Georgia;color: black">
                <h3 class="login-heading1" style="font-family: Georgia">
                    Customise Feeds</h3>
                We know how important is for you to keep track of your friends, followers, and individual
                companies. That’s why we’ve introduced our customise feeds function.<br />
                <br />
                Here, you can choose to receive feeds from an individual user, friend or an influential
                person with your same skills or working in your industry.<br />
                <br />
                You can also choose which company to follow, so you never miss that important conversation
                that matters to you.<br />
                <br />
                You can also choose:<br />
                <br />
                <ul class="list-green list-green-jobs" style="font-family: Georgia">
                    <li>Search and connect directly with users.</li>
                    <li>Select the most useful feed format from criteria including skills, keywords,
                        name, company, school, experience or country.</li>
                    <li>You can even locate users who are available right now.</li>
                </ul>
                <%--<a href="#" class="button-green floatright">Ok, I got it</a>--%>
                <asp:Button ID="btnIGotIt" runat="server" CssClass="button-green floatright" OnClick="btnIGotIt_Click"
                    Text="Ok, I got it" />
            </div>
            <!-- content inner left ends -->
            <div class="content-inner-right">
                <iframe height="215" frameborder="0" class="profile-pic" width="296" src="http://player.vimeo.com/video/65019649">
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
