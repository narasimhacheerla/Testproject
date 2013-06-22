<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HuntableTourCustomizeJobs.aspx.cs" Inherits="Huntable.UI.CustomizeJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <h3 class="login-heading1" style="font-family: Georgia; color:black">
                    Customise Jobs – Hear about the best vacancies for you</h3>
                <br />
                <br />
                <div style="font-family: Georgia; color:black">First! Customising jobs is a great tool, allowing you to define precisely the kind
                of opportunity in which you may be interested. You’ll be updated the second a relevant
                job is posted – wherever it is in the world.<br />
                <br />
                Live in the UK but planning a new life and searching for a job in Australia? When
                a new job is posted in Australia, which matches your requirements, you will be notified
                instantly. It’s that simple.<br />
                <br />
                Not looking for any opportunity, but might consider a better one? Then customise
                your search results to show exactly which kind of jobs might interest you. When
                a new opportunity arises, you will be notified instantly.<br /></div>
                <br />
                <ul class="list-green list-green-jobs" style="font-family: Georgia; color: black">
                    <li>Customise your jobs and feeds by salary, industry, skill, job type and country</li>
                    <li>You decide the currency</li>
                    <li>Select full/part-time, permanent, temporary, freelance or apprenticeship</li>
                    <li>Our powerful functionality will search all relevant jobs throughout the world</li>
                    <li>Never miss an opportunity again.</li>
                </ul>
                <%--  <a href="#" class="button-green floatright">Ok, I got it</a>--%>
                <asp:Button ID="btnIGotIt" runat="server" CssClass="button-green floatright" OnClick="BtnIGotItClick"
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
</asp:Content>
