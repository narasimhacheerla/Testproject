<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HuntableTourFeatures.aspx.cs" Inherits="Huntable.UI.TourOfHuntable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <h1 class="welcome-heading" style="font-family:Georgia">
                    Welcome to <strong>HUNTABLE</strong></h1>
                <br />
                <b style="color: #028ca2;font-family:Georgia">Thanks for taking your first step towards demonstrating your
                    wide range of professional skills, extensive knowledge and expertise to companies
                    and users worldwide. <br /><br /> Now, you can begin to connect with your peers and key influential
                    figures in companies, showing precisely why you stand out from the crowd. <br /><br /> Take the
                    time to familiarise yourself with all the features, so that you can take full advantage
                    of Huntable..</b> 
                <br />
                <br />
                <%--<h3 class="login-heading1">
                    Searching &amp; Connecting with other users</h3>--%>
                <ul class="list-green list-green-jobs" style="font-family:Georgia; color: black">
                    <li>Search, connect and follow users and companies across the globe.</li>
                    <li>Support your profile with pictures, videos, achievements, expertise, interest, languages
                        and everything about you.</li>
                    <li>Customise jobs and feeds to ensure you’ll never miss an important topic of conversation
                        or that dream job.</li>
                    <li>Follow friends and fellow users with the same skill, industry leaders or a specific
                        company.</li>
                    <li>Post, search, or apply for jobs with our innovative visual, text and activity profile,
                        so companies can find everything about you.</li>
                    <li>Chat with other online users.</li>
                    <li>Send a message to any user.</li>
                </ul>
                <asp:Button ID="btnIGotIt" runat="server" Text="Ok, I got it" CssClass="button-green floatright"
                    OnClick="BtnIGotItClick" />
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
