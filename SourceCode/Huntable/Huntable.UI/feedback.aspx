<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="feedback.aspx.cs" Inherits="Huntable.UI.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
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
               <div id="bx8">  <ul id="tabsNav">
                    <li><a href="#huntable" class="tab-ash">
                        <img src="images/huntable-tab.png" width="22" height="16" alt="Huntable" />What
                        is Huntable</a></li>
                    <li><a href="#started" class="tab-ash">
                        <img src="images/help-tab.png" width="22" height="16" alt="Get Started" />
                        Get Started</a></li>
                    <li><a href="#faq" class="tab-ash">
                        <img src="images/faq-tab.png" width="22" height="16" alt="Faq" />FAQ</a></li>
                    <li><a href="#feedback" class="tab-ash">
                        <img src="images/feedback.png" width="22" height="16" alt="Feedback" />Send Feedback</a></li>
                    <li><a href="#contact" class="tab-ash">
                        <img src="images/contact-tab.png" width="22" height="16" alt="Contact" />Contact
                        Us</a></li>
                    <div id="Div1" runat="server" visible="false"><li><a href="#terms" class="tab-ash">Terms &amp; Conditions</a></li>
                    <li><a href="#privacy" class="tab-ash">Privacy Policy</a></li>
                    <li><a href="#cookie" class="tab-ash">Cookie Policy</a></li></div>
                </ul></div>
                <ul id="tabContent" style="margin-left: 225px;">
                    <li id="feedback">
                        <div class="welcome-tab">
                            <h2 class="heading2 heading2-tab">
                                Send Feedback</h2>
                            <div class="huntable-desc" style="border-bottom: none;">
                                <h2>
                                    What would you like to tell us?</h2>
                                <div class="contact-main">
                                    <div class="feedback-icon">
                                        <asp:LinkButton ID="lnkRP" runat="server" OnClick="lnkRP_Click">
                                            <img src="images/feedback-icon1.jpg" width="16" height="17" alt="Report a Problem" />Report
                                            a Problem</asp:LinkButton>
                                        <asp:LinkButton ID="lnkQ" runat="server" OnClick="lnkQ_Click">
                                                <img src="images/feedback-icon2.jpg" width="16" height="17" alt="Question" />Question</asp:LinkButton>
                                        <asp:LinkButton ID="lnkSugg" runat="server" OnClick="lnkSugg_Click">
                                            <img src="images/feedback-icon3.jpg" width="16" height="17" alt="Suggestion" />Suggestion</asp:LinkButton>
                                        <asp:LinkButton ID="lnkApp" runat="server" OnClick="lnkApp_Click">
                                            <img src="images/feedback-icon4.jpg" width="16" height="17" alt="Appreciate" />Appreciate</asp:LinkButton>
                                        <asp:LinkButton ID="lnkAny" runat="server" OnClick="lnkAny_Click">
                                            <img src="images/feedback-icon5.jpg" width="16" height="17" alt="Anything else" />Anything
                                            else</asp:LinkButton>
                                    </div>
                                    <div class="contact-left" style="margin-top: 20px;">
                                        <label>
                                            Subject:</label>
                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="textbox"></asp:TextBox>
                                        <br />
                                        <br />
                                        <label>
                                            Tell us:</label>
                                        <asp:TextBox ID="txtSub" runat="server" CssClass="textbox" TextMode="MultiLine"></asp:TextBox>
                                        <%--<textarea class="textarea textbox" cols="1" rows="2"></textarea>--%>
                                        <br />
                                        <br />
                                        <label>
                                            Name:</label>
                                        <asp:TextBox ID="txtName" runat="server" CssClass="textbox"></asp:TextBox>
                                        <%-- <input type="text" class="textbox" />--%>
                                        <br />
                                        <br />
                                        <label>
                                            E-mail:</label>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox"></asp:TextBox>
                                        <%-- <input type="text" class="textbox" />--%>
                                        <br />
                                        <asp:Label ID="lblSuccess" runat="server" Visible="false"></asp:Label>
                                        <br />
                                        <label>
                                            &nbsp;</label>
                                        <asp:Button ID="btnSendMail" runat="server" Text="Send" CssClass="button-orange button-green-upload"
                                            OnClick="btnSendMail_Click" />
                                        <%--<a href="#" class="button-orange button-green-upload ">Send</a>--%>
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
                    <li id="huntable">
                        <div class="welcome-tab">
                            <h2 class="heading2 heading2-tab">
                                What is Huntable</h2>
                            <div class="huntable-desc">
                                <h2>
                                    Huntable is a Professional Resourcing network.</h2>
                                <p style="font-family: Georgia;color: black">
                                     Huntable is all about you, showcasing your professional achievements and finding the best 
                                    opportunities, right at your fingertips. It gives you the opportunity to shine the spotlight on 
                                    individuals and companies who use this state of the art platform to present their profile to the world.
                                    <br />
                                    <br />
                                    Make sure you’re being noticed by the right people and promoting your specific achievements, to stand 
                                    out from the crowd. 
                                    <br />
                                    <br />
                                    Huntable enables you to get your information in front of the right people, connecting you with 
                                    like-minded individuals, industry leaders and company heads from around the globe.
                                    </p>
                            </div>
                            <div class="huntable-desc">
                                <h2>
                                    How can Huntable help you?</h2>
                                <p class="huntable-desc1" style="font-family: Georgia;color: black">
                                    <strong >Search for a job</strong><br />
                                    Are you sending off CVs but not being asked to interview?  With many vacancies attracting dozens of 
                                    applications, it now takes more than a plain CV and covering letter to stand out from all other 
                                    applicants.
                                    <br />
                                    <br />
                                    Competition is fierce, but Huntable can help you to get your achievements and profile noticed by those 
                                    who really count.
                                    <br />
                                    <br />
                                    Not looking for any opportunity, but might consider a better one?  Then customise your search results 
                                    to show exactly which kind of jobs might interest you. When a new opportunity arises, you will be 
                                    notified instantly
                                    <br />
                                    <br />
                                    •	Huntable functionality helps bring your profile to life.<br />
                                    •	Identify & follow industry leaders & decision makers.<br />
                                    •	Keep up to date with company & industry news.<br />
                                    •	Full customisation – see only the most relevant jobs & information.<br />
                                </p>
                                <img src="images/about-search.jpg" width="311" height="223" alt="about-searcg" />
                            </div>
                            <div class="huntable-desc">
                                <img src="images/about-connect.jpg" class="floatleft" width="311" height="223" alt="about-connect" />
                                <p class="huntable-desc1" style="font-family: Georgia;color: black">
                                    <strong>Connect with other users</strong><br />
                                   One of the benefits of using Huntable is that it’s easy to connect with other users, sharing useful information and identifying the most relevant people in your industry. 
 <br />
 <br />
Whether you’re looking to keep up to date with industry trends, or trying to make contact with a key person in a specific organisation, Huntable offers the fastest and easiest route to success.

                                </p>
                            </div>
                            <div class="huntable-desc">
                                <p class="huntable-desc1" style="font-family: Georgia;color: black">
                                    <strong>Follow Your favourite skill, Industry, user or company</strong><br />
                                    Your time is limited, so you want fast access to the information you need, without having to wade 
                                   through pages of pointless updates and conversation streams.  You decide what updates you want to see.  
                                   <br />
                                   <br />
                                    Choose from a wide range of criteria including skill, industry, user or company. With Huntable, 
                                    you’re in control of your information feeds.
                                </p>
                                <img src="images/about-follow.jpg" class="floatleft" width="311" height="223" alt="about-follow" />
                            </div>
                            <div class="huntable-desc">
                                <img src="images/aboutus-customize.jpg" class="floatleft" width="311" height="223"
                                    alt="about-follow" />
                                <p class="huntable-desc1"  style="font-family: Georgia;color: black">
                                    <strong>Customize your jobs &amp; feeds</strong><br />
                                    Customising your jobs and feeds has been made exceptionally easy. 
                                    Job searches are made faster and more relevant, whilst truly useful information can be accessed 
                                    in seconds.  
                                    <br />
                                    <br />
                                    Huntable has been designed with your needs in mind – so you control what you see.
                                    
                                </p>
                            </div>
                            <div class="huntable-desc">
                                <p class="huntable-desc1" style="font-family: Georgia;color: black">
                                    <strong>Promote Your company</strong><br />
                                     Make smart use of your marketing budget by registering your company on Huntable.  It’s absolutely free,
                                    is easily and quickly uploaded, and your Huntable profile will prove invaluable in promoting your brand
                                     and sharing information with both your target market and potential employees.  
                                   <br />
                                   <br />
                                   Interested users will have access to your articles, blog and current vacancies, saving significant 
                                   costs for both your marketing and recruitment budgets.
                                </p>
                                <img src="images/aboutus-promote.jpg" class="floatleft" width="311" height="223"
                                    alt="about-follow" />
                            </div>
                            <div class="huntable-desc">
                                <img src="images/about-mission.jpg" class="floatleft" width="311" height="223" alt="about-follow" />
                                <p class="huntable-desc1" style="font-family: Georgia;color: black">
                                    <strong>Our Mission</strong><br />
                                    Our mission is to create a place where everyone can showcase their profile, so companies and recruiters can find and hire the best matching people without any strings attached.  We want a world where all users can promote, connect, follow and express their views, without any limitations. 
                                    <br />
                                    <br />
We want the Huntable application to showcase every person’s professional side.
<br />
<br />
-	A user should be able to tell the world what they have done and promote their expertise and skill without any limitation.<br />
-	Companies and recruiters should be able to find, hunt and hire anyone across the globe within only a few clicks.

                                </p>
                            </div>
                        </div>
                    </li>
                    <li id="started">
                        <div class="welcome-tab">
                            <h2 class="heading2 heading2-tab">
                                Get Started</h2>
                            <ul class="menu collapsible">
                                <li>
                                    <h2 class="get-head">
                                        Registering with Huntable</h2>
                                </li>
                                <li class="expand"><a href="#" class="menu-ash" style="font-family: Georgia">What is Huntable<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                                 Simply because no other professional platform has our power and capabilities.  We’ll help you to keep in touch with whatever is happening in your industry and whoever has influence and expertise.  Showcase your profile to industry leaders and develop those all-important contacts, right across the world. 
                                                <br />
                                                <br />
For companies and recruiters, Huntable isn’t just a means of connecting with others in your industry; it’s a great way to promote your products and services, too.


                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">Creating your profile<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                                As soon as you’ve created your account, simply move to the Edit Profile page, where you’ll be asked to enter details of your professional background and contact details, along with the type of job you’re looking for and a photo.  You can even save time by using our automatic Facebook or LinkedIn profile upload tool.  It really is as simple as that.  Rest assured your contact details will remain strictly confidential.
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">Customising your feeds<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                                Having access to the right information is vital in forming those all-important 
                                                connections, receiving specific company updates and learning about new trends in the 
                                                industry.  
                                                <br />
                                                <br />
                                                 Our feeds can be customised by criteria including company and industry to ensure that 
                                                 your profile always reflects current knowledge, skills and expertise.
                                                 <br />
                                                 <br />
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">Customising your jobs<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                                Huntable’s job customisation feature means you’ll receive news about new vacancies, 
                                                which are appropriate for you.  Choose to hear about only those jobs, which match your 
                                                very specific criteria including salary range, industry, type of job and skills required.
                                                <br />
                                                <br />
                                                Huntable was designed with you in mind.  
                                                <br />
                                                <br />
                                                Being able to highlight specific skills, knowledge and experience within your profile, 
                                                based on industry and company information, will give you the edge in a competitive market 
                                                and allow you to present your career achievements in a relevant and timely way.
                                                <br />
                                                <br />
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">Adding/Editing your experiences<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                               You can add or edit all your experiences in the Edit Profile section. Simply click Edit, under an experience. This will give you the option to add all the necessary details about that experience.
                                                <br />
                                                <br />
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">Adding pictures & videos<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                               You can add achievements, pictures and video clips to your profile, after adding your experience.
                                                <br />
                                                <br />
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">Deleting a picture, video or other experiences<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                               Once you have added a picture or video, you can still delete them from the Edit profile in the relevant experience.
                                                <br />
                                                <br />
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <h2 class="get-head">
                                        Companies &amp; Recruitement agencies</h2>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">Promoting your company in Huntable<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                              Make a presence in our community by being active. Post feeds, upload information about your products or services, add company pictures and videos or notify everyone about your current vacancies. 
                                                <br />
                                                <br />
This will draw attention to your online company profile, which in turn will encourage followers.  You can also promote your company within our featured recruiters section.

                                                <br />
                                                <br />
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">Sending job postings to relevant job seekers<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                                Customised jobs is a great feature that we offer for all users who are looking for opportunities. Here they can customise jobs by criteria including salary expected, their industry, skill, Job type and country of their choice. 
                                                <br />
                                                <br />
When a company post a job matching these criteria, this job will be instantly shown in the user’s feeds. This gives maximum exposure to your job. 
<br />
<br />
Your job will also be shown to every user who is following your company. 

                                                <br />
                                                <br />
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">Uploading & updating old CVs currently in your system<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                                 Huntable offers the most recent, innovative application to recruiters and headhunters. This application will enable you to refresh and update all your old and out-dated CVs, which you may still retain in your system.  It doesn’t matter how old that CV is, your candidate database will be updated instantly. 
                                                <br />
                                                <br />
All you need to do is download the free application to your desktop, pointing to the folder where your entire database is located. The application will automatically add these CVs into your own secure Huntable database. 
<br />
<br />
So if you don’t know what a candidate with an old CV is doing now, your out of date record will be automatically updated with their most recent profile, which is also searchable. 
<br />
<br />
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <%--<li>
                                    <h2 class="get-head">
                                        Companies &amp; Recruitement agencies</h2>
                                </li>
                                <li><a href="#" class="menu-ash">What is the Benefit for companies?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">Target advertisement<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">How is my job postings sent to relevent job seekers?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">Headhunt anyone from around the world<b>&nbsp;</b></a>
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
                                <li><a href="#" class="menu-ash">Get Headhunted by companies around the globe.<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">Find your true potential. There is someone out there
                                    looking for you.<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">Tell the world who you are, and what you can do.<b>&nbsp;</b></a>
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
                                <li><a href="#" class="menu-ash">Target Your audience by country, industry, skill and
                                    many more<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">Use our free Blogging services to promote yourself
                                    and your company<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                            </ul>--%>
                            <h2 class="get-head" style="color: #000;">
                                If you cant find answers to your question, Please contact our <a href="#">support team.</a></h2>
                        </div>
                    </li>
                    <li id="faq">
                        <div class="welcome-tab">
                            <h2 class="heading2 heading2-tab">
                                FAQ</h2>
                            <ul class="menu collapsible">
                                <li>
                                    <h2 class="get-head" style="font-family: Georgia">
                                        Users</h2>
                                </li>
                                <li class="expand"><a href="#" class="menu-ash" style="font-family: Georgia">Is my profile
                                    available for anyone to see?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                              Huntable is all about promoting your skills and expertise to the world, so your
                                                online profile is visible for everyone to view. Anyone can contact you if they find
                                                your skills and expertise are relevant to them. Huntable users can also check if
                                                you are online and chat with you, if you agree to connect.<br /
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">How can I be head hunted
                                    by companies around the globe?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                                If you can be flexible in where you work and have a particular niche skill set or
                                                experience, then your profile will be made available and highlighted in searches
                                                carried out by headhunters, internationally.<br />
                                                <br />
                                                Our powerful recruitment functionality, combined with social media compatibility,
                                                means that for the first time, you can truly tell the world who you are and what
                                                you can do.<br />
                                                <br />
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <%--<li><a href="#" class="menu-ash">What do I Get from Huntable?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>--%>
                                <li>
                                    <h2 class="get-head" style="font-family: Georgia">
                                        Companies & recruitment agencies </h2>
                                </li>
                                <li><a href="#" class="menu-ash"  style="font-family: Georgia">What are the benefits
                                    for companies?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                                By being able to easily view a user’s professional achievements, experience and
                                                career to date through their up to date profile, companies can analyse an individual
                                                much more thoroughly than could be done through a simple CV or application form.<br />
                                                <br />
                                                By subscribing to your feed, they’ll already be knowledgeable about your products,
                                                services and up to the minute company news.<br />
                                                <br />
                                                You can also take advantage of feeds from industry leaders and drivers, who can
                                                provide innovation, inspiration and insight.<br />
                                                <br />
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">Can I advertise my company
                                    on Huntable?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                               Absolutely. In addition to automatically promoting your products or services throughout
                                                the world simply by creating a user account, you can also showcase your company
                                                on Huntable through advertising or by taking advantage of our free blogging services.
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">If I find a user who interests
                                    me, can I contact them?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                                Yes, you can contact anyone through the messaging system. However, for security
                                                and privacy purposes, we will not reveal any email addresses.<br />
                                                <br />
                                                If the user is online, you can also hold a conversation with them through our chat
                                                facility. Please be aware that if we find any user or company spamming other users,
                                                we may block their account.<br />
                                                <br />
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">How can I attract followers
                                    to my company?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                               You can encourage users to follow your company by inviting all your present & past
                                                employees to do so first, using the Find Friends section or in the Company Edit
                                                Profile section.<br />
                                                <br />
                                                You can also achieve more followers by constantly being active in the community,
                                                which will encourage other users to follow your company.<br />
                                                <br />
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">How do I headhunt someone
                                    in a different part of the world?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                                It doesn’t matter where users are situated. If they possess the relevant skills
                                                and experience, you’ll be able to find them using Huntable’s powerful search functionality.
                                                Simply enter the relevant criteria in your user search, including country, industry
                                                or skill sets.
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash" style="font-family: Georgia">How can I promote my company
                                    only to specific users?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p style="font-family: Georgia;color: black">
                                               If you’re a company or a recruiter, you can promote your organisation by targeting
                                                users from a specific industry, skill or country in our Featured Recruiters section.
                                                Your company logo and your job information will be then be displayed to the relevant
                                                users.
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <%--<li>
                                    <h2 class="get-head">
                                        Your Network</h2>
                                </li>
                                <li><a href="#" class="menu-ash">How to import my profile?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">How to edit my profile?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">How to delete my profile?<b>&nbsp;</b></a>
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
                                        Login Details </h2>
                                </li>
                                <li><a href="#" class="menu-ash">How to import my profile?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">How to edit my profile?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                                <li><a href="#" class="menu-ash">How to delete my profile?<b>&nbsp;</b></a>
                                    <ul class="acitem">
                                        <li>
                                            <p>
                                                Lorem Lipsum Dummy
                                            </p>
                                        </li>
                                    </ul>
                                </li>
--%>                            </ul>
                        </div>
                    </li>
                    <li id="contact">
                        <div class="welcome-tab">
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
                                            Name:</label><input type="text" class="textbox" runat="server" id="txtNames" />
                                        <br />
                                        <br />
                                        <label>
                                            E-mail:</label><input type="text" class="textbox" runat="server" id="txtEmails" />
                                        <br />
                                        <br />
                                        <label>
                                            Message:</label><textarea class="textarea textbox" cols="1" rows="2" runat="server" id="txtMessage"></textarea>
                                        <br />
                                        <br />
                                        <label>
                                            &nbsp;</label>
                                        <asp:Button ID="btnSend" runat="server" Text="Send" 
                                            CssClass="button-orange button-green-upload" onclick="btnSend_Click" />
                                        <%--<a href="#" class="button-orange button-green-upload ">Send</a>--%>
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
