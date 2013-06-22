<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Help.aspx.cs" Inherits="Huntable.UI.Help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    $(function () {

        var msie6 = $.browser == 'msie' && $.browser.version < 7;
        if (!msie6) {
            var top = $('#bx8').offset().top;
            $(window).scroll(function (event) {
                var y = $(this).scrollTop();
                if (y >= 14) { $('#bx8').addClass('fixed'); }
                else { $('#bx8').removeClass('fixed'); }
            });
        }
    });</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div id="tabsAndContent">
               <div id="bx8"> <ul id="tabsNav">
                    <li><a href="#huntable" class="tab-ash">
                        <img src="../images/huntable-tab.png" width="22" height="16" alt="Huntable" /><span
                            style="font-size: 14px;"> What is Huntable</span></a></li>
                    <li><a href="#started" class="tab-ash">
                        <img src="../images/help-tab.png" width="22" height="16" alt="Get Started" /><span style="font-size: 14px;">
                            Get Started</span> </a></li>
                    <li><a href="#faq" class="tab-ash">
                        <img src="../images/faq-tab.png" width="22" height="16" alt="Faq" /><span style="font-size: 14px;">FAQ</span>
                    </a></li>
                    <li><a href="#feedback" class="tab-ash">
                        <img src="../images/feedback.png" width="22" height="16" alt="Feedback" /><span style="font-size: 14px;">Send
                            Feedback</span> </a></li>
                    <li><a href="#contact" class="tab-ash">
                        <img src="../images/contact-tab.png" width="22" height="16" alt="Contact" /><span style="font-size: 14px;">Contact
                            Us</span> </a></li>
                    <div id="Div1" runat="server" visible="false"><li><a href="#terms" class="tab-ash"><span style="font-size: 14px;">Terms &amp; Conditions</span>
                    </a></li>
                    <li><a href="#privacy" class="tab-ash"><span style="font-size: 14px;">Privacy Policy</span></a></li>
                    <li><a href="#cookie" class="tab-ash"><span style="font-size: 14px;">Cookie Policy</span></a></li></div>
                </ul></div>
                <ul id="tabContent" style="margin-left: 225px;">
                    <li id="started">
                        <div class="welcome-tab">
                            <h2 class="heading2 heading2-tab">
                                Get Started</h2>
                            <ul class="menu collapsible">
                                <li>
                                    <h2 class="get-head">
                                        Huntable</h2>
                                </li>
                                <li class="expand"><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        What is Huntable</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        Why I should I register in Huntable?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        What is customizing feeds?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        What is customizing jobs?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        Whats special about customizing feeds &amp; Jobs?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <h2 class="get-head">
                                        Profile &amp; your account</h2>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        How to upload my profile?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        Is my profile available for anyone to see?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <h2 class="get-head">
                                        Companies &amp; Recruitement agencies</h2>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        What is the Benefit for companies?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        Target advertisement</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        How is my job postings sent to relevent job seekers?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        Headhunt anyone from around the world</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <h2 class="get-head">
                                        Job seekers, Students, Freelancers, Passive job seekers</h2>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        Get Headhunted by companies around the globe.</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        Find your true potential. There is someone out there looking for you.</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        Tell the world who you are, and what you can do.</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <h2 class="get-head">
                                        Advertise &amp; Bloggers</h2>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        Target Your audience by country, industry, skill and many more</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        Use our free Blogging services to promote yourself and your company</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                            <h2 class="get-head" style="color: #000;">
                                If you cant find answers to your question, Please contact our <a href="#">support team.</a></h2>
                        </div>
                    </li>
                    <li id="huntable">
                        <div class="welcome-tab">
                            <h2 class="heading2 heading2-tab">
                                What is Huntable</h2>
                            <div class="huntable-desc">
                                <h2>
                                    Huntable is a Professional Resourcing network.</h2>
                                <p>
                                    Huntable lets you put your profile in fromt of the world so any one can see what
                                    you have achieved and what is that you are looking to achieve. Best of all, you
                                    can cutomize your feeds and jobs you receive. Huntable also let you connect with
                                    other professionals, companies, recruiters and friends. Here you can share, comment,
                                    ask, post to any users around the world.</p>
                            </div>
                            <div class="huntable-desc">
                                <h2>
                                    How can Huntable help you?</h2>
                                <p class="huntable-desc1">
                                    <strong><span style="font-size: 17px;">Search for a job</span></strong><br />
                                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem
                                    Ipsum has been the industry's standard dummy text ever since the 1500s, when an
                                    unknown printer took a galley of type and scrambled it to make a type specimen book.
                                    It has survived not only five centuries, but also the leap into electronic typesetting,
                                    remaining essentially unchanged.
                                </p>
                                <img src="images/about-search.jpg" width="311" height="223" alt="about-searcg" />
                            </div>
                            <div class="huntable-desc">
                                <img src="images/about-connect.jpg" class="floatleft" width="311" height="223" alt="about-connect" />
                                <p class="huntable-desc1">
                                    <strong><span style="font-size: 17px;">Connect with other users</span></strong><br />
                                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem
                                    Ipsum has been the industry's standard dummy text ever since the 1500s, when an
                                    unknown printer took a galley of type and scrambled it to make a type specimen book.
                                    It has survived not only five centuries, but also the leap into electronic typesetting,
                                    remaining essentially unchanged.
                                </p>
                            </div>
                            <div class="huntable-desc">
                                <p class="huntable-desc1">
                                    <strong><span style="font-size: 17px;">Follow Your favourite skill, Industry, user or
                                        company</span></strong><br />
                                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem
                                    Ipsum has been the industry's standard dummy text ever since the 1500s, when an
                                    unknown printer took a galley of type and scrambled it to make a type specimen book.
                                    It has survived not only five centuries, but also the leap into electronic typesetting,
                                    remaining essentially unchanged.
                                </p>
                                <img src="images/about-follow.jpg" class="floatleft" width="311" height="223" alt="about-follow" />
                            </div>
                            <div class="huntable-desc">
                                <img src="images/aboutus-customize.jpg" class="floatleft" width="311" height="223"
                                    alt="about-follow" />
                                <p class="huntable-desc1">
                                    <strong><span style="font-size: 17px;">Customize your jobs &amp; feeds</span></strong><br />
                                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem
                                    Ipsum has been the industry's standard dummy text ever since the 1500s, when an
                                    unknown printer took a galley of type and scrambled it to make a type specimen book.
                                    It has survived not only five centuries, but also the leap into electronic typesetting,
                                    remaining essentially unchanged.
                                </p>
                            </div>
                            <div class="huntable-desc">
                                <p class="huntable-desc1">
                                    <strong><span style="font-size: 17px;">Promote Your company</span></strong><br />
                                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem
                                    Ipsum has been the industry's standard dummy text ever since the 1500s, when an
                                    unknown printer took a galley of type and scrambled it to make a type specimen book.
                                    It has survived not only five centuries, but also the leap into electronic typesetting,
                                    remaining essentially unchanged.
                                </p>
                                <img src="images/aboutus-promote.jpg" class="floatleft" width="311" height="223"
                                    alt="about-follow" />
                            </div>
                            <div class="huntable-desc">
                                <img src="images/about-mission.jpg" class="floatleft" width="311" height="223" alt="about-follow" />
                                <p class="huntable-desc1">
                                    <strong><span style="font-size: 17px;">Our Mission</span></strong><br />
                                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem
                                    Ipsum has been the industry's standard dummy text ever since the 1500s, when an
                                    unknown printer took a galley of type and scrambled it to make a type specimen book.
                                    It has survived not only five centuries, but also the leap into electronic typesetting,
                                    remaining essentially unchanged.
                                </p>
                            </div>
                        </div>
                    </li>
                    <li id="faq" runat="server">
                        <div id="faqs" runat="server" class="welcome-tab">
                            <h2 class="heading2 heading2-tab">
                                FAQ</h2>
                            <ul class="menu collapsible">
                                <li>
                                    <h2 class="get-head">
                                        Huntable Introduction<strong>(4)</strong></h2>
                                </li>
                                <li class="expand"><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        What is Huntable</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        How can Huntable help me?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        What do I Get from Huntable?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <h2 class="get-head">
                                        Profile <strong>(5)</strong></h2>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        How to import my profile?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        How to edit my profile?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        How to delete my profile?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <h2 class="get-head">
                                        Your Network <strong>(1)</strong></h2>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        How to import my profile?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        How to edit my profile?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        How to delete my profile?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <h2 class="get-head">
                                        Login Details <strong>(3)</strong></h2>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        How to import my profile?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        How to edit my profile?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">
                                    <label style="font-size: 14px;">
                                        How to delete my profile?</label><b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </li>
                    <li id="feedback">
                        <div class="welcome-tab">
                            <h2 class="heading2 heading2-tab">
                                Send Feedback</h2>
                            <div class="huntable-desc" style="border-bottom: none;">
                                <h2>
                                    What would you like to tell us?</h2>
                                <div class="contact-main">
                                    <div class="feedback-icon">
                                        <a href="#">
                                            <img src="images/feedback-icon1.jpg" width="16" height="17" alt="Report a Problem" />Report
                                            a Problem</a> <a href="#">
                                                <img src="images/feedback-icon2.jpg" width="16" height="17" alt="Question" />Question</a>
                                        <a href="#">
                                            <img src="images/feedback-icon3.jpg" width="16" height="17" alt="Suggestion" />Suggestion</a>
                                        <a href="#">
                                            <img src="images/feedback-icon4.jpg" width="16" height="17" alt="Appreciate" />Appreciate</a>
                                        <a href="#">
                                            <img src="images/feedback-icon5.jpg" width="16" height="17" alt="Anything else" />Anything
                                            else</a>
                                    </div>
                                    <div class="contact-left" style="margin-top: 20px;">
                                        <label>
                                            Tell us:</label><textarea class="textarea textbox" cols="1" rows="2"></textarea>
                                        <br />
                                        <br />
                                        <label>
                                            Name:</label><input type="text" class="textbox" />
                                        <br />
                                        <br />
                                        <label>
                                            E-mail:</label><input type="text" class="textbox" />
                                        <br />
                                        <br />
                                        <label>
                                            &nbsp;</label>
                                        <a href="#" class="button-orange button-green-upload ">Send</a>
                                    </div>
                                    <div class="contact-right">
                                        <div class="blue-box" style="width: 300px;">
                                            Do you have a suggestion or feature request? Let us know what you think of Huntable.
                                            we would love to hear from you
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li id="contact">
                        <div runat="server" id="contactus" class="welcome-tab">
                            <h2 class="heading2 heading2-tab">
                                Contact Us</h2>
                            <div class="huntable-desc huntable-desc-bg">
                                <strong>Frequently Asked Questions</strong>
                                <p>
                                    Please Visit our <a href="faq.aspx" class="accounts-link">FAQ</a> as your question may
                                    have already been answered there!
                                </p>
                            </div>
                            <div class="huntable-desc" style="border-bottom: none;">
                                <h2>
                                    How can we help you?</h2>
                                <p>
                                    Feel free to contact us with service questions, partnership proposals, or media
                                    inquiries below.
                                </p>
                                <div class="contact-main">
                                    <div class="contact-left">
                                        <label>
                                            Name:</label><input type="text" class="textbox" />
                                        <br />
                                        <br />
                                        <label>
                                            E-mail:</label><input type="text" class="textbox" />
                                        <br />
                                        <br />
                                        <label>
                                            Message:</label><textarea class="textarea textbox" cols="1" rows="2"></textarea>
                                        <br />
                                        <br />
                                        <label>
                                            &nbsp;</label>
                                        <a href="#" class="button-orange button-green-upload ">Send</a>
                                    </div>
                                    <div class="contact-right">
                                        <strong>Advertising:</strong> <a href="#">ads@huntable.co.uk</a><br />
                                        <strong>Public Relations:</strong> <a href="#">pr@huntable.co.uk</a><br />
                                        <strong>Jobs:</strong> <a href="#">jobs@huntable.co.uk</a><br />
                                        <strong>General Enquiries:</strong> <a href="#">support@huntable.co.uk</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li id="terms">
                        <div class="welcome-tab">
                            <h2 class="heading2 heading2-tab">
                                Tems &amp; Conditions</h2>
                            <p>
                                Lorem LIpsum</p>
                        </div>
                    </li>
                    <li id="privacy">
                        <div class="welcome-tab">
                            <h2 class="heading2 heading2-tab">
                                Privacy Policy</h2>
                            <p>
                                Lorem LIpsum</p>
                        </div>
                    </li>
                    <li id="cookie">
                        <div class="welcome-tab">
                            <h2 class="heading2 heading2-tab">
                                Cookie Policy</h2>
                            <p>
                                Lorem LIpsum</p>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script> <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
    type="text/javascript"></script> <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
     <!-- Range Slider Script Begins -->
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <!-- Range Slider Script Ends -->
    <!-- Footer section ends -->
    <!-- Left Tab Script Begins -->
    <script type="text/javascript" src="js/myTheme.js"></script>
    <!-- Left Tab Script Ends -->
    <!-- Help Tab Script Begins -->
    <script src="js/menu.js" type="text/javascript"></script>
    <!-- Help Tab Script Ends -->
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
