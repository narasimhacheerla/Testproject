<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GetHunted.aspx.cs" Inherits="Huntable.UI.GetHunted" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <%-- <p class="top-strip">
        &nbsp;</p>--%>
    <!-- this script used for both clickable slide and tab slide function -->
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <!-- this script used for both clickable slide and tab slide function -->
    <script type="text/javascript">
//	<![CDATA[
        $(document).ready(function () {
            $('div.test1').click(function () {
                $('ul.list1').slideToggle('medium');
            });
        });
        $(document).ready(function () {
            $('div.test2').click(function () {
                $('ul.list2').slideToggle('medium');
            });
        });
//]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="content-section">
        <div id="content-inner">
            <div id="tabsAndContent">
                <ul id="tabsNav1">
                    <li><a href="#super-power" class="tab-ash1">
                        <img src="images/power.png" width="22" height="16" alt="Huntable" />Power your profile</a></li>
                    <li><a href="#customize-feeds" class="tab-ash1">
                        <img src="images/help-tab.png" width="22" height="16" alt="Get Started" />Customize
                        Feeds and jobs</a></li>
                    <li><a href="#follow" class="tab-ash1">
                        <img src="images/follow.png" alt="Follow" width="22" height="16" />Follow Users/companies</a></li>
                    <li><a href="#connect-and-network" class="tab-ash1">
                        <img src="images/network.png" width="22" height="16" alt="Connect and Network" />Connect
                        and Network</a></li>
                    <li class="active"><a href="#head-hunted" class="tab-ash1">
                        <img src="images/selected.png" width="22" height="16" alt="Get headhunted" />Get
                        headhunted</a></li>
                    <li><a href="#find-job" class="tab-ash1">
                        <img src="images/dream-job.png" width="22" height="16" alt="Dream job" />Find your
                        dream job</a></li>
                    <li><a href="#promote" class="tab-ash1">
                        <img src="images/promote.png" width="22" height="16" alt="Promote your company" />Promote
                        your company</a></li>
                    <li><a href="#aim" class="tab-ash1">
                        <img src="images/aim.png" width="22" height="16" alt="Our aim" />Our aim</a></li>
                </ul>
                <ul id="tabContent">
                      <li id="head-hunted">
                        <div class="welcome-tab" style="border-bottom: 1px dotted #CCCCCC; padding-bottom: 10px;">
                            <div class="huntable-desc r-huntable-desc" style="border-bottom: none;">
                                <h2>
                                    Get Headhunted</h2>
                               <p class="huntable-desc2" style="font-family: Georgia;color: black">
                                    Ensure that headhunters can see your profile right across the globe. Showcase your
                                    skills, knowledge and expertise, demonstrating to the world that you’re a key player
                                    in your industry.<br />
                                </p>
                                <img width="311" height="223" src="images/how-img5.jpg" alt="about-follow" />
                            </div>
                            <p style="font-family: Georgia;color: black">
                                Make sure you have the right keywords, skills, interest and all other relevant information
                                in your profile. This will eventually be searchable when companies and recruiters
                                are looking for a matching profile.</p>
                        </div>
                        <div class="upgrade register-font">
                            <strong class="floatleft"><a class="accounts-link register-link">Register Now</a> and
                                its totally <strong class="red-color">Free !!!</strong></strong> <a class="button-green button-green-register"
                                    href="#">Sign up</a>
                        </div>
                    </li>
                     <li id="customize-feeds">
                        <div class="welcome-tab" style="border-bottom: 1px dotted #CCCCCC; padding-bottom: 10px;">
                            <div class="huntable-desc r-huntable-desc" style="border-bottom: none;">
                                <h2>
                                    Customize feeds &amp; Jobs</h2>
                               <p class="huntable-desc2" style="font-family: Georgia;color: black">
                                    Your information feeds can be customised by criteria including company and industry
                                    to ensure that your profile always reflects your current knowledge, skills and expertise.<br />
                                </p>
                                <img width="311" height="223" src="images/aboutus-customize.jpg" alt="about-follow" />
                            </div>
                           <p style="font-family: Georgia;color: black">
                                Our job customisation feature means you’ll receive news about new vacancies, which
                                are appropriate for you. Choose to hear about only those jobs, which match your
                                very specific criteria including salary range, industry, type of job and skills
                                required.</p>
                        </div>
                        <div class="upgrade register-font">
                            <strong class="floatleft"><a class="accounts-link register-link">Register Now</a> and
                                its totally <strong class="red-color">Free !!!</strong></strong> <a class="button-green button-green-register"
                                    href="#">Sign up</a>
                        </div>
                    </li>
                    <li id="super-power">
                        <div class="welcome-tab" style="border-bottom: 1px dotted #CCCCCC; padding-bottom: 10px;">
                            <div class="huntable-desc r-huntable-desc" style="border-bottom: none;">
                                <h2>
                                    Super power your profile</h2>
                                <p class="huntable-desc2"  style="font-family: Georgia; color:black">
                                    Super charge your profile and show everyone what you have achieved easily and with
                                    text, visual and networking profile.<br />
                                </p>
                                <img width="311" height="223" alt="about-mission" src="images/about-mission.jpg" />
                            </div>
                           <p style="font-family: Georgia;color: black">
                                Why not super charge your profile and demonstrate your achievements to potential
                                employers, companies and recruiters, through your networking profile and a mixed
                                medium of text and visual aids?<br />
                                <br />
                                Ensure you have completed all the information required, including experience, skills,
                                summary, achievements, interest and languages spoken. Your profile will then be
                                searchable for all the relevant information entered and displayed visually for every
                                one to see.<br />
                                <br />
                                This will give employers and headhunters an immediate snapshot of your suitability
                                for any potential vacancies.<br />
                                <br />
                            </p>
                        </div>
                        <div class="upgrade register-font">
                            <strong class="floatleft"><a class="accounts-link register-link">Register Now</a> and
                                its totally <strong class="red-color">Free !!!</strong></strong> <a style="margin-left: 20px;"
                                    class="button-green button-green-register" href="#">Sign up</a>
                        </div>
                    </li>
                    
                    <li id="follow">
                        <div class="welcome-tab" style="border-bottom: 1px dotted #CCCCCC; padding-bottom: 10px;">
                            <div class="huntable-desc r-huntable-desc" style="border-bottom: none;">
                                <h2>
                                    Follow users &amp; companies</h2>
                                <p class="huntable-desc2" style="font-family: Georgia;color: black">
                                    It’s so easy to connect with other users and companies in your industry. Whether
                                    you’re looking to keep up to date with current trends, or trying to get a foot in
                                    the door of a company, Huntable offers the fastest and easiest route to success.<br />
                                </p>
                                <img width="311" height="223" src="images/about-follow.jpg" alt="about-follow" />
                            </div>
                            <p style="font-family: Georgia; color:black">
                                Choose from a wide range of criteria including skill, industry, user or company.</p>
                        </div>
                        <div class="upgrade register-font">
                            <strong class="floatleft"><a class="accounts-link register-link">Register Now</a> and
                                its totally <strong class="red-color">Free !!!</strong></strong> <a class="button-green button-green-register"
                                    href="#">Sign up</a>
                        </div>
                    </li>
                    <li id="connect-and-network">
                        <div class="welcome-tab" style="border-bottom: 1px dotted #CCCCCC; padding-bottom: 10px;">
                            <div class="huntable-desc r-huntable-desc" style="border-bottom: none;">
                                <h2>
                                    Connect &amp; Network</h2>
                                 <p class="huntable-desc2" style="font-family: Georgia;color: black">
                                    A powerful online networking tool, Huntable has harnessed the functionality of social
                                    media to enable you to make contact and share information with industry leaders,
                                    key company figures and other users..<br />
                                </p>
                                <img width="311" height="223" src="images/how-img4.jpg" alt="about-follow" />
                            </div>
                            <p>
                            </p>
                        </div>
                        <div class="upgrade register-font">
                            <strong class="floatleft"><a class="accounts-link register-link">Register Now</a> and
                                its totally <strong class="red-color">Free !!!</strong></strong> <a class="button-green button-green-register"
                                    href="#">Sign up</a>
                        </div>
                    </li>
                    
                    <li id="find-job">
                        <div class="welcome-tab" style="border-bottom: 1px dotted #CCCCCC; padding-bottom: 10px;">
                            <div class="huntable-desc r-huntable-desc" style="border-bottom: none;">
                                <h2>
                                    Find your dream job</h2>
                                 <p class="huntable-desc2" style="font-family: Georgia;color: black">
                                    In such a competitive environment, it’s vital that you have what it takes to stand
                                    out from the crowd. Huntable can help you to get your achievements and profile noticed
                                    by those who really count, enabling you to find that elusive dream job.<br />
                                </p>
                                <img width="311" height="223" src="images/how-img6.jpg" alt="about-follow" />
                            </div>
                            <p>
                            </p>
                        </div>
                        <div class="upgrade register-font">
                            <strong class="floatleft"><a class="accounts-link register-link">Register Now</a> and
                                its totally <strong class="red-color">Free !!!</strong></strong> <a class="button-green button-green-register"
                                    href="#">Sign up</a>
                        </div>
                    </li>
                    <li id="promote">
                        <div class="welcome-tab" style="border-bottom: 1px dotted #CCCCCC; padding-bottom: 10px;">
                            <div class="huntable-desc r-huntable-desc" style="border-bottom: none;">
                                <h2>
                                    Promote your company</h2>
                                <p class="huntable-desc1">
                                    <strong>Super charge your profile and show everyone what you have achieved easily and
                                        with text, visual and networking profile.</strong><br />
                                </p>
                                <img width="311" height="223" alt="about-follow" src="images/aboutus-promote.jpg" />
                            </div>
                           <p style="font-family: Georgia;color: black">
                                It’s free and easy to promote your company on Huntable. Forget expensive recruitment
                                advertising and marketing campaigns. Instead, focus on this worldwide platform,
                                targeting key individuals and reinforcing your brand messages.</p>
                        </div>
                        <div class="upgrade register-font">
                            <strong class="floatleft"><a class="accounts-link register-link">Register Now</a> and
                                its totally <strong class="red-color">Free !!!</strong></strong> <a class="button-green button-green-register"
                                    href="#">Sign up</a>
                        </div>
                    </li>
                    <li id="aim">
                        <div class="welcome-tab" style="border-bottom: 1px dotted #CCCCCC; padding-bottom: 10px;">
                            <div class="huntable-desc r-huntable-desc" style="border-bottom: none;">
                                <h2>
                                    Our aim</h2>
                                 <p class="huntable-desc2" style="font-family: Georgia;color: black">
                                    Our aim is simply to allow users to showcase their professional profile – wherever
                                    and whenever. The Huntable objective is to provide a means of connecting companies
                                    and recruiters with users, sharing information and making the recruitment process
                                    faster, cheaper and much more efficient.<br />
                                </p>
                                <img width="311" height="223" alt="Vison Misson" src="images/vision-mission.jpg" />
                            </div>
                            <p>
                            </p>
                        </div>
                        <div class="upgrade register-font">
                            <strong class="floatleft"><a class="accounts-link register-link">Register Now</a> and
                                its totally <strong class="red-color">Free !!!</strong></strong> <a class="button-green button-green-register"
                                    href="#">Sign up</a>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <!-- content inner ends -->
    </div>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script type="text/javascript" src="js/flipcounter.js"></script>
    <!-- Left Tab Script Begins -->
    <script type="text/javascript" src="js/myTheme.js"></script>
    <!-- Left Tab Script Ends -->
    <!-- Help Tab Script Begins -->
    <script src="js/menu.js" type="text/javascript"></script>
</asp:Content>
