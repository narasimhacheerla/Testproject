<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmailConfirmationSentSuccess.aspx.cs" Inherits="Huntable.UI.EmailConfirmationSentSuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--  <p>
        &nbsp;</p>
    <p>
        <span class="style10"><strong>Thanks for joining!</strong></span><span 
            class="style11"> Now it&#39;s time to verify </span>
    </p>
    <p class="style15">
        Your email address.</p>
    <p class="style15">
        &nbsp;</p>
    <p class="style16">
        Check the link in the email we sent to &#39;<asp:Label ID="lblEmail" runat="server"></asp:Label>   
        &#39;   to complete</p>
    <p class="style16">
        your account.</p>
    <p class="style16">
        &nbsp;</p>
    <ul>
        <li>
            <p class="style16">
                <asp:LinkButton ID="btnResend" runat="server" Text="Resend verification email." 
                    onclick="btnResentEmail_Click" ></asp:LinkButton></p>
        </li>
    </ul>--%>
    <div id="content-section">
        <div id="content-inner">
            <div class="email-confirmation">
                <h1>
                    <span>Thanks for joining! </span>Now it's time to verify your email address.</h1>
                <h2>
                    Check the link in the email we sent to <%--<a href="#"> 'chefarun@hotmail.co.uk'</a>--%><asp:Label ID="lblEmail" ForeColor="Blue" runat="server"></asp:Label>
                    <asp:LinkButton ID="lnkEmail" runat="server"></asp:LinkButton> 
                   complete your account</h2>
                
                <ul class="list-green">
                   <li> <asp:LinkButton class="accounts-link" ID="btnResentEmail" runat="server" OnClick="BtnResentEmailClick"
                    Text="Resend verification email."></asp:LinkButton></li>
                    <li id="lblcon" runat="server" visible="false">
                       <asp:Label runat="server" id="lblconfirm" Font-Size="14px" Text="verification mail sent successfully"></asp:Label>
                    </li>
                </ul>
                <ul class="list-green">
                  <li><asp:LinkButton class="accounts-link" ID="LinkButton1" runat="server" 
                    Text="Change My email Address." onclick="LinkButton1Click"></asp:LinkButton></li>
              </ul>
            </div>
            <div class="share-email">
                <h2>
                    Share <b>HUNTABLE</b> with your friends</h2>
                <br />
                <asp:imagebutton id="imgFb" PostBackUrl="https://www.facebook.com/login.php" runat="server" src="images/share-email-fb.png" width="73" height="32" alt="Facebook" />
    
                <asp:imagebutton id="Imagebutton1" PostBackUrl="https://twitter.com/login/" runat="server" src="images/share-email-tr.png" width="73" height="32" alt="Twitter" />

                 <asp:imagebutton id="Imagebutton2" PostBackUrl="https://accounts.google.com/ServiceLogin?service=oz&continue=https://plus.google.com/?gpcaz%3Db7fec591&hl=en-GB"  runat="server" src="images/share-email-gplus.png" width="73" height="32" alt="Twitter" />

                 <asp:imagebutton id="Imagebutton3" PostBackUrl="https://login.yahoo.com/config/login_verify2?&.src=ym" runat="server" src="images/share-email-mail.png" width="73" height="32" alt="Twitter" />
                   
            </div>
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
