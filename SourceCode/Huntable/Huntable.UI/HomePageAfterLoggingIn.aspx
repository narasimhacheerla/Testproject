<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HomePageAfterLoggingIn.aspx.cs" Inherits="Huntable.UI.HomePageAfterLoggingIn" %>

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
<%@ Register Src="~/UserControls/Followers(6).ascx" TagName="followers" TagPrefix="uc10" %>
<%@ Register Src="~/UserControls/CompaniesYouMayBeIntrested.ascx" TagName="comp_Intrested"
    TagPrefix="uc11" %>
<%@ Register Src="UserControls/UserFeedList.ascx" TagName="UserFeedList" TagPrefix="uc1" %>
<%@ Register src="UserControls/PeopleYouMayKnow.ascx" tagName="pplUMayKnow" tagPrefix="uc12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
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
        function validate() {
            var uploadcontrol = $('#MainContent_fp').val();
            if (uploadcontrol == "") {
                alert("Please choose a file!");
                return false;
            }
            //Regular Expression for fileupload control.
            var allowedexts = ".gif.GIF.jpg.JPG.png.PNG.JPEG.jpeg";
            if (uploadcontrol.length > 0 && uploadcontrol.indexOf('.') > 0) {
                var fullfilename = uploadcontrol.split('.');
                var fileext = fullfilename[fullfilename.length - 1];
                //Checks with the control value.
                if (allowedexts.indexOf(fileext) > 0) {
                    return true;
                }
                else {
                    //If the condition not satisfied shows error message.
                    $('#MainContent_fp').val('');
                    alert("Only .GIF,.JPG,.PNG files are allowed!");
                    return false;
                }
            }
        }
    </script>
    <script type="text/javascript">
        

            function rowAction(jobid) {

                $('#<%=jobid.ClientID %>').val(jobid);

            
            //When you click on a link with class of poplight and the href starts with a # 
          
                var popID = popup12; //Get Popup Name
//                var popURL = $(this).attr('href'); //Get Popup href to define size

                url.src = "ShareMail.aspx";
                //Pull Query & Variables from href URL
//                var query = popURL.split('?');
//                var dim = query[1].split('&');
//                var popWidth = dim[0].split('=')[1]; //Gets the first query string value

                //Fade in the Popup and add close button
                $(popID).fadeIn().css({ 'width': Number(400) }).prepend('<a href="#" class="close"><img src="https://huntable.co.uk/images/close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>');

                //Define margin for center alignment (vertical + horizontal) - we add 80 to the height/width to accomodate for the padding + border width defined in the css
                var popMargTop = ($(popID).height() + 80) / 2;
                var popMargLeft = ($(popID).width() + 80) / 2;

                //Apply Margin to Popup
                $(popID).css({
                    'margin-top': -popMargTop,
                    'margin-left': -popMargLeft
                });

                //Fade in Background
                $('body').append('<div id="fade" style="z-index:10"></div>'); //Add the fade layer to bottom of the body tag.
                $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); //Fade in the fade layer 



            }
            
        $(document).ready(function () {
            //Close Popups and Fade Layer
            $('a.close, #fade').live('click', function () { //When clicking on the close or fade layer...
                $('#fade , .popup_block').fadeOut(function () {
                    $('#fade, a.close').remove();
                }); //fade them both out

                return false;
            });
            function fdout() {
                $('#fade , .popup_block').fadeOut(function () {
                    $('#fade, a.close').remove();
                }); //fade them both out

                return false;

            }


        });
        function fdout() {
            $('#fade , .popup_block').fadeOut(function () {
                $('#fade, a.close').remove();
            }); //fade them both out

            return false;

        }

        function HideCtrl(ctrl, timer) {
            var ctryArray = ctrl.split(",");
            var num = 0, arrLength = ctryArray.length;
            while (num < arrLength) {
                if (document.getElementById(ctryArray[num])) {
                    setTimeout('document.getElementById("' + ctryArray[num] + '").style.display = "none";', timer);
                } s
                num += 1;
            }
            return false;
        }
        </script>
    <script type="text/javascript">
        $(document).ready(function () {

            var msie6 = $.browser == 'msie' && $.browser.version < 7;
            if (!msie6) {
                $(window).bind("scroll", function (event) {
                    var y = $(this).scrollTop();
                    if (y >= 670) { $('#bx12').addClass('fixed'); }
                    else { $('#bx12').removeClass('fixed'); }
                });
                //                var top = $('#bx12').offset().top;
                //                $(window).scroll(function (event) {
                //                    var y = $(this).scrollTop();
                //                    if (y >= 670) { $('#bx12').addClass('fixed'); }
                //                    else { $('#bx12').removeClass('fixed'); }

                //                    if (GetUserFeeds != null)
                //                        GetUserFeeds();
                //                });
            }
        });
        $(document).ready(function () {

            // Check whether browser is IE6

            var msie6 = $.browser == 'msie' && $.browser.version < 7;

            // Only run the following code if browser
            // is not IE6. On IE6, the box will always
            // scroll.

            if (!msie6) {

                // Set the 'top' variable. The following
                // code calculates the initial position of
                // the box. 

                //                var top = $('#bx12').offset().top;

                // Next, we use jQuery's scroll function
                // to monitor the page as we scroll through.

                $(window).scroll(function (event) {

                    // In the following line, we set 'y' to
                    // be the amount of pixels scrolled
                    // from the top.

                    var y = $(this).scrollTop();

                    // Have you scrolled beyond the
                    // box? If so, we need to set the box
                    // to fixed.

                    if (y >= 670) {

                        // Set the box to the 'fixed' class.

                        $('#bx12').addClass('fixed');

                    } else {

                        // Remove the 'fixed' class 

                        $('#bx12').removeClass('fixed');
                    }
                });
            }
        });
    </script>

    <script type="text/javascript">
        function overlay(id) {
            $('#fade , .popup_block').fadeOut(function () {
                $('#fade, a.close').remove();
            });
           
            el = document.getElementById('ovrly');
            $('#ovrly').show();

            $('#<%= pbl.ClientID %>').text(id);

        }
        function overlay1(id) {
            function fdout() {
                $('#fade , .popup_block').fadeOut(function () {
                    $('#fade, a.close').remove();
                }); //fade them both out

//                return false;

            }
            el = document.getElementById('ovrly');
            $('#ovrly').show();

            $('#<%= pbl.ClientID %>').text(id);

        }



        $(document).ready(function () {

            $('#ximg').click(function () {
                window.location.replace("homepageafterloggingin.aspx");
                $('#ovrly').hide();
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="ovrly" style="height: 30px; width: 100%; z-index: 2000; background-color: #FF9242;
        border: 1px solid #FF9242; border-radius: 2px; margin-top: 10px; display: none;">
        <div style="width: 980px; padding: 0px 10px; margin: 0px auto;">
            <div style="width: 30px; margin-left: 292px;">
                <image src="images/tick.png" width="25px" height="25px"></image>
            </div>
            <div style="margin-top: -20px">
                <asp:Label Style="color: White; width: 200px; margin-top: -40px; margin-left: 377px;"
                    ID="pbl" runat="server"></asp:Label></div>
            <div style="margin-left: 621px; margin-top: -13px">
                <image id="ximg" src="images/orange-check-mark-md.png" width="10px" height="10px"></image>
            </div>
        </div>
    </div>
    <%--<link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
   <link rel="stylesheet" type="text/css" href="css/skin2.css" />
<link rel="stylesheet" type="text/css" href="css/skin3.css" />
<link rel="stylesheet" href="css/jquery.fancybox.css" type="text/css" media="screen" />
<link rel="stylesheet" type="text/css" href="css/counter.css" />--%>
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="profile-main">
                    <div class="profile-left">
                        <a id="A1" href="~/PictureUpload.aspx" runat="server">
                            <asp:Image runat="server" ID="imgProfile" class="profile-pic" Width="76" Height="81"
                                alt="Profile-pic" />
                            <asp:Image runat="server" ID="cmpimage" class="profile-pic" Width="170" Height="87"
                                Visible="false" alt="Profile-pic" /></a><br />
                        <a id="AA1" runat="server">
                            <asp:Label class="profile-name" ID="lblUserName" runat="server" /></a>
                        <div class="profile-complete">
                            <asp:Label ID="lblPercentCompleted" class="profile-complete" runat="server"></asp:Label><asp:Label
                                runat="server" ID="lblAcccount"></asp:Label><br />
                            <eo:progressbar id="ProgressBar2" runat="server" width="90px" backgroundimage="00060301"
                                backgroundimageleft="00060302" backgroundimageright="00060303" controlskinid="None"
                                indicatorimage="00060304" showpercentage="True" value="30">
                            </eo:progressbar>
                        </div>
                    </div>
                    <div class="profile-right" runat="server" id="tell">
                        <div class="tell-the-world " style="margin-left: 20px;" id="divTellTheWorld" runat="server">
                            <asp:TextBox Height="35px" Width="485px" Text="Got something to say, ask, post, share…"
                                onfocus="SetMsg(this, true);" onblur="SetMsg(this, false);" ID="txtTellworld"
                                CssClass="textarea-profile" runat="server"></asp:TextBox>
                            <asp:Label ID="lblurl" runat="server" Text="http://www.huntable.co.uk" Visible="false"></asp:Label>
                            <div class="slidingDiv slidingdiv-new" style="width: 522px;">
                                <asp:FileUpload ID="fp" runat="server" />
                                <asp:Button ID="btnattach" Text="Publish photo" OnClientClick="return validate();"
                                    runat="server" Style="width: 44px; height: 25px; background-color: #819FF7;"
                                    OnClick="Btnattachclick" />
                                <a href="#" class="attach-link show_hide" target="_self" style="float: right; margin-right: 48px;">
                                    X</a>
                            </div>
                            <a href="#" class="attach-link show_hide" target="_self">
                                <img src="images/attach-link.png" width="12" height="12" alt="link" id="attachlink" />
                                Attach Picture </a>
                            <div class="social-utilites">
                                <asp:CheckBox ID="chkTwitter" AutoPostBack="True" runat="server" 
                                    oncheckedchanged="chkTwitter_CheckedChanged" />&nbsp;Twitter
                               <%-- <asp:CheckBox ID="chkgoogle" Enabled="False" runat="server" />&nbsp;Google+ &nbsp;--%>
                                <asp:CheckBox ID="chkFacebook" AutoPostBack="True" runat="server" oncheckedchanged="chkFacebook_CheckedChanged" />&nbsp;Facebook &nbsp;
                                <asp:CheckBox ID="chkLinkedIn" AutoPostBack="True" runat="server" oncheckedchanged="chkLinkedIn_CheckedChanged" />&nbsp;LinkedIn
                            </div>
                            <asp:Button ID="btnJoin" runat="server" Text="Tell the World" CssClass="button-green button-green-profile"
                                OnClick="BtnTellWorldClick" />
                        </div>
                        <span class="last-login" id="spnLoginTime" runat="server">Last Login Time:<b><asp:Label
                            ID="lblLogDate" runat="server"></asp:Label></b>&nbsp;<b><asp:Label ID="lblLogin"
                                runat="server"></asp:Label></b></span> <span class="last-update">Last Profile Updated
                                    on: <b>
                                        <asp:Label ID="lblProfile" runat="server"></asp:Label></b></span>
                    </div>
                </div>
                <%-- <div class="profile-right" runat="server" id="tell1" visible="false">
                    <div class="tell-the-world " style="margin-left: 88px; margin-top: -165px;">
                        <asp:TextBox Height="35px" Width="485px" Text="Got something to say, ask, post, share…"
                            onfocus="SetMsg(this, true);" onblur="SetMsg(this, false);" ID="TextBox1" CssClass="textarea-profile"
                            runat="server"></asp:TextBox>
                        <div class="slidingDiv slidingdiv-new" style="width: 522px; margin-left: -17px;">
                            <asp:FileUpload ID="fp1" runat="server" />
                            <asp:Button ID="Button2" Text="Save" runat="server" Style="margin-left: -59px; width: 44px;
                                height: 25px; background-color: #819FF7;" OnClick="Btnattachclick" />
                            <a href="#" class="attach-link show_hide" target="_self" style="float: right; margin-right: 48px;">
                                X</a>
                        </div>
                        <a href="#" class="attach-link show_hide" target="_self">
                            <img src="images/attach-link.png" width="12" height="12" alt="link" id="Img1" />
                            Attach Picture </a>
                        <div class="social-utilites">
                            <asp:CheckBox ID="chktwitter1" runat="server" />&nbsp;Twitter
                            <asp:CheckBox ID="chkgoogle1" runat="server" />&nbsp;Google+
                            <asp:CheckBox ID="chkfacebook1" runat="server" />&nbsp;Facebook &nbsp;
                            <asp:CheckBox ID="chklinkedin1" runat="server" />&nbsp;LinkedIn
                        </div>
                        <asp:Button ID="Button3" runat="server" Text="Tell the World" CssClass="button-green button-green-profile"
                            OnClick="BtnTellWorldClick" />
                    </div>
                    <span class="last-login" style="margin-left: 74px; margin-top: -33px;">Last Login Time:<b><asp:Label
                        ID="lbllastlogin" runat="server"></asp:Label></b>&nbsp;<b><asp:Label ID="lbllog"
                            runat="server"></asp:Label></b></span> <span class="last-update" style="margin-top: -33px;
                                margin-right: -56px;">Last Profile Updated on: <b>
                                    <asp:Label ID="lblpupdate" runat="server"></asp:Label></b></span>
                </div>--%>
                <div class="profile-search">
                    <b>Search for anyone's profile now:</b>
                    <asp:TextBox ID="txtUserSearchKeyword" runat="server" onblur="if (this.value == '') {this.value ='e.g: Name, Company, Skill, Job title';}"
                        onfocus="if (this.value =='e.g: Name, Company, Skill, Job title') {this.value ='';}"
                        value="e.g: Name, Company, Skill, Job title" class="textbox-search textbox-search-inner"></asp:TextBox>
                    <asp:Button class="button-orange button-orange-search" ID="btnUserSearch" runat="server"
                        Text="Search" OnClientClick="return ValidateText()" OnClick="BtnUserSearchClick" />
                </div>
                <div class="all-feeds-main-inner" style="height: 100%;">
                    <div id="tabswitch">
                        <ul>
                            <li class="tab1" style="width: 300px;"><b>All Feeds:&nbsp;&nbsp;</b><a href="CustomizeFeedsSkill.aspx">Skills</a><a
                                href="CustomizeFeedsIndustry.aspx">Industry</a><a href="CustomizeFeedsCountry.aspx">Country</a><a
                                    href="CustomizeFeedsInterest.aspx">Interests</a></li>
                            <li style="float: right;" class="tab2" runat="server" id="opp1"><b>Opportunities:&nbsp;&nbsp;</b><a
                                href="CustomizeJobsSalary.aspx">Salary</a><a href="CustomizeJobsUsers.aspx">Experience</a><a
                                    href="CustomizeJobsCountry.aspx">Location</a></li>
                        </ul>
                        <div class="tab-container tab1">
                            <uc1:userfeedlist id="UserFeedList1" runat="server" pagetype="Networking" profileuserid="" />
                        </div>
                        <div class="tab-container tab2">
                            <div class="notification-right notification-right-home">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField ID="hJobfield" Value="" runat="server" />
                                        <asp:ListView ID="lvJobs" OnItemDataBound="LvItemound" runat="server" OnItemCommand="LvJobsItemCommand" ClientIDMode="Static">
                                            <ItemTemplate>
                                                <div class="notification-head">
                                                    <a id="A2" href='<%#UrlGenerator(Eval("Id")) %>' class="grey-link"
                                                        runat="server">
                                                        <img src='<%# Eval("ProfileImagePath") %>' class="profile-pic profile-pic2" width="76"
                                                            height="81" alt="Job" /></a> <a href="<%#UrlGenerator(Eval("Id")) %>"
                                                                class="grey-link">
                                                                <%# Eval("Title")%></a><br />
                                                    <br />
                                                    <span class="blue-color" style="margin-left: 65px;">Job Type :</span>
                                                    <%# Eval("MasterJobType.Description")%>
                                                    <br />
                                                    <span class="blue-color" style="margin-left: 27px;">Experience Req :</span>
                                                    <%# Eval("MinExperience")%>
                                                    Minimum<br />
                                                    <span class="blue-color" style="margin-left: 71px;">Industry :</span>
                                                    <%# Eval("MasterIndustry.Description")%>
                                                    <br />
                                                    <span class="blue-color" style="margin-left: 80px;">Salary :</span>
                                                    <%# Eval("Salary")%><%# Eval("MasterCurrencyType.Description")%><br />
                                                    <span class="blue-color" style="margin-left: 180px;">Skill :</span>
                                                    <%# Eval("MasterSkill.Description")%><br />
                                                    <span class="blue-color" style="margin-left: 119px;">JobPostedDate : </span>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("CreatedDateTime", "{0:MM/dd/yyyy}")%>'></asp:Label><br />
                                                    <br />
                                                    <asp:Label ID="Label2" CssClass="ShortDesc" Text='<%# Eval("JobDescription").ToString().Substring(0,Math.Min(300,Eval("JobDescription").ToString().Length)) %>'
                                                        runat="server"></asp:Label>...<a class="orange-link" href="<%#UrlGenerator(Eval("Id")) %>" >more</a>
                                                    <br />
                                                    <%--<div><asp:Button ID="btn1" runat="server" Text="Apply now +" OnClick="Applynowclicked"/></div>--%>
                                                    <div id="Div1" class="notification-links notification-links1 notification-links-home"
                                                        runat="server" visible='<%# Eval("IsUserAlreadyToThisJob") %>'>
                                                        <%--    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="invite-friend-btn invite-friend-btn1"
                                                            Text="Apply now + " CommandName="Apply" CommandArgument='<%# Eval("Id")%>' />--%>
                                                        <a href='<%# DataBinder.Eval(Container.DataItem, "Id", "javascript:rowAction(\"{0}\");")%>' class="invite-friend-btn invite-friend-btn1 poplight" rel="popup12"  >
                                                            Apply now + </a>
                                                            <div id="popup12" class="popup_block">
                                                        <div class="apply-job ">
                                                            <strong>Write some covering message to the job poster.<br />
                                                                Make sure your profile is Update before you apply for this job</strong><br />
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtapply" TextMode="MultiLine" Rows="9" Style="width: 350px;color: gray" />
                                                            <asp:Button ID="Button2" runat="server" Text="Apply Job" CommandName="Apply" CommandArgument='<%# Eval("Id")%>'
                                                                class="button-orange" />
                                                        </div>
                                                    </div>
                                                    </div>
                                                    <div id="div2" runat="server" visible='<%# Eval("IsUserNotAppliedToThisJob") %>'
                                                        style="margin-left: 552px;">
                                                        <asp:Label runat="server" Width="41" Height="16" ID="Following" CssClass="invite-friend-btn invite-friend-btn1"
                                                            Text="Applied" /></div>
                                                    
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <asp:Button ID="Button1" OnClick="BtnJobShowCLick" Style="margin-left: 180px;" class="show-more"
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
                <uc9:ucnewsdesign id="topnews" runat="server" />
            </div>
            <!-- ADDING COMPANY RELATED DATA--->
            <div id="div_comp" runat="server" visible="false" style="margin-left: 17px; margin-right: -124px;">
                <div class="box-right" runat="server" style="margin-left: 95px; margin-right: 16px;
                    border: aliceBlue;">
                    <div>
                        <uc10:followers id="Followers1" runat="server" />
                    </div>
                    <br />
                    <div>
                        <uc11:comp_intrested id="Comp_Intrested1" runat="server" />
                    </div>
                </div>
                <div id="invitefriends" runat="server" class="box-right" style="margin-left: 97px;
                    margin-top: 11px;">
                    <div class="head-ash">
                        <h3>
                            Invite Your Friends & Employees</h3>
                    </div>
                    <div class="social-icon">
                        <a href="InviteFriends.aspx" title="Facebook">
                            <img src="images/facebook.jpg" width="34" height="34" alt="Facebook" title="Facebook" /></a>
                        <a href="InviteFriends.aspx" title="Linked in">
                            <img src="images/linkedin.jpg" width="34" height="34" alt="Linkedin" title="Linked in" /></a>
                        <a href="InviteFriends.aspx" title="Twitter">
                            <img src="images/twitter.jpg" width="34" height="34" alt="Twitter" title="Twitter" /></a>
                        <a href="InviteFriends.aspx" title="Gmail">
                            <img src="images/gmail.jpg" width="34" height="34" alt="Gmail" title="Gmail" /></a><a
                                href="InviteFriends.aspx" title="yahoo"><img src="images/yahoo.png" width="34" height="34"
                                    alt="yahoo" /></a> <a href="InviteFriends.aspx" title="Msn">
                                        <img src="images/msn.jpg" width="34" height="34" alt="Msn" title="Msn" /></a>
                    </div>
                </div>
            </div>
            <!-- ENDING COMPANY RELATED DATA--->
            <!-- content inner left ends -->
            <div id="rightDiv" runat="server" class="content-inner-right" style="height: 1000px">
                <div class="box-right">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <uc12:pplUMayKnow runat="server" /><br/>
                            <p class="margin-top-visible">
                    &nbsp;</p>
                            <uc3:uclfriendsinvite id="ucl3" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc4:uclstatistcs id="ucl4" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc7:uclaccountat id="uc3" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right" id="bx12">
                    <uc5:uclinvitefriends id="uc5" runat="server" />
                </div>
                <!-- content inner right ends -->
                <!-- content inner ends -->
                <!-- content section ends -->
                <!-- Textarea onclick event script Begins -->
            </div>
        </div>
        <asp:HiddenField runat="server" ID="jobid"/>
        </div>
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
        <script type="text/javascript">

            $(document).ready(function () {

                $(".slidingDiv").hide();
                $(".show_hide").show();

                $('.show_hide').click(function () {
                    $(".slidingDiv").slideToggle();
                });

            });
 
        </script>
        <link href="js/nyroModal/styles/nyroModal.css" rel="stylesheet" type="text/css" />
        <script src="js/nyroModal/js/jquery.nyroModal.custom.min.js" type="text/javascript"></script>
        <!--[if IE 6]>
	<script type="text/javascript" src="js/jquery.nyroModal-ie6.min.js"></script>
<![endif]-->
</asp:Content>
