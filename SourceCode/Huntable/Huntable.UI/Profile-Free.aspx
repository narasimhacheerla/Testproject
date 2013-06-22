<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Profile-Free.aspx.cs" Inherits="Huntable.UI.ProfileFree" %>
<%@ Register Src="UserControls/KnowPeoplecontrol.ascx" TagPrefix="snovasys" TagName="Friend" %>
<%@ Register Src="UserControls/cvStatistics.ascx" TagPrefix="snovaspace" TagName="Cv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/NewStylesheet.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tableStyle">
        <tr>
            <td class="cellStyle4">
            </td>
            <td class="style15">
                <asp:Panel runat="server" ID="panHeader" BackColor="snow" Width="506px">
                    <table class="tableStyle">
                        <tr>
                            <td>
                                <asp:LinkButton runat="server" CssClass="LinkButton" ID="lnkHome" Text="Home" Font-Bold="True"
                                    OnClick="LnkHomeClick"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" CssClass="LinkButton" ID="lnkProfile" Text="Profile"
                                    ForeColor="Blue" OnClick="LnkProfileClick"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" CssClass="LinkButton" ID="lnkJobs" Text="Jobs" ForeColor="Blue"
                                    OnClick="LnkJobsClick"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" CssClass="LinkButton" ID="lnkFeeds" Text="Feeds" ForeColor="Blue"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" CssClass="LinkButton" ID="lnkfindfriends" Text="Find friends"
                                    ForeColor="Blue"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:Label runat="server" CssClass="LinkButton" ID="lnkAccount" Text="Account" ForeColor="Blue"></asp:Label>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" CssClass="LinkButton" ID="lnkSettings" Text="Settings"
                                    ForeColor="Blue"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:ImageButton runat="server" ID="imgPost" ImageUrl="image/postCover.png" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td align="right" class="style20">
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td class="style9">
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2">
                <asp:Label runat="server" ID="lblSearch" Text="Search for anyone profile now:"></asp:Label>
                <asp:TextBox runat="server" ID="txtSearch" Text="e.g Name,company,skill,job title"
                    ForeColor="gray" Font-Size="small" CssClass="style17"></asp:TextBox>
                    <asp:Button runat="server" ID="btnSearch" Text="Search" ForeColor="Gray" />
          
            
            <asp:Button runat="server" ID="btnAdvanced" Text="Advanced" Font-Size="XX-Small" BorderStyle="None" />
              </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <table class="tableStyle">
        <tr>
            <td class="cellStyle4">
            </td>
            <td valign="top" colspan="2" class="tableStyles1">
                <table cellpadding="0px" cellspacing="0px" class="tableStyle">
                    <tr>
                        <td>
                        </td>
                        <td class="style26">
                            
                        </td>
                        <td class="columnStyles4">
                            
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;" cellspacing="0px" cellpadding="0px" class="tableStyles">
                    <tr>
                        <td valign="top" class="style17">
                            <table class="tableStyle">
                                <tr>
                                    <td valign="top">
                                        <asp:ImageButton runat="server" ID="imgDavid" ImageUrl="image/photo51.png" Height="65px" />
                                        <asp:Label runat="server" ID="lblName" Text="David Coutts" CssClass="linkStyle"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblPercentage" Text="10% Complete" Font-Size="Smaller"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtPercentage" Height="16px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            <asp:Label runat="server" ID="lblDarun" Text="Darun Wilson" Font-Bold="True"></asp:Label><br />
                            <asp:Label runat="server" ID="lbladd1" Text="Head Chef at Jamies Italian"></asp:Label><br />
                            <asp:Label runat="server" ID="lblAdd2" Text="London,United Kingdom"></asp:Label><br />
                            <asp:Label runat="server" ID="lblAdd3" Text="Skill:Hospitality >Kitchen"></asp:Label>
                        </td>
                        <td valign="top" align="right">
                            <asp:LinkButton runat="server" ID="lnkProfileHuntable" CssClass="LinkButton" Text="Is your profile untable?Update - Edit Now"
                                ForeColor="Black" BackColor="yellow"></asp:LinkButton><br />
                            <asp:LinkButton runat="server" ID="lnkOpportunity" Text="Post an Opportunity" ForeColor="white"
                                BackColor="Green"></asp:LinkButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table class="tableStyle">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblLoginTime" Text="Last login time:"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblProfileUpdated" Text="Last Profile Updated on:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblCurrent" Text="Current"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblHead" Text="Head Chef at Jamies Italian"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkRequest" Text="Request Endorsement"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkAddPicture" Text="Add a picture[9%]" CssClass="LinkButton"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblPast" Text="Past"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblHeadChef" Text="Head Chef at Jamies Italian" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkAddSummary" Text="Add your Summary[29%]" CssClass="LinkButton"></asp:LinkButton>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblHiltonMarriot" Text="Head Chef at Hilton Hotels"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkAddPosition" Text="Add your Current position[5%]"
                                CssClass="LinkButton"></asp:LinkButton>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblHeadMarriot" Text="Head Chef at Marriot Hotels"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkExperience" Text="Add your Experience[10%]"
                                CssClass="LinkButton"></asp:LinkButton>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblSouthChef" Text="South Chef at Hilton Hotels"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkPastExperience" Text="Add your Past Experience"
                                CssClass="LinkButton"></asp:LinkButton>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkMore" Text="...more" CssClass="LinkButton"></asp:LinkButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkEducation" Text="Add your Education[25%]" CssClass="LinkButton"></asp:LinkButton>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkSchool" Text="Add your School[19%]" CssClass="LinkButton"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblEducation1" Text="Education"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblMaster" Text="Masters degree in Culinary skills Diploma in Hotel management"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton runat="server" ID="imgPrint" ImageUrl="image/sharePrint.png" />
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="lnkOnline" Text="Online Now"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkInternet" Text="Add your Internet etc.[105] "></asp:LinkButton>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:LinkButton runat="server" Text="...more" CssClass="LinkButton"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ImageUrl="image/pdf.png" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblEdorsements" Text="Endorsements"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblEndorsed" Text="5 person has endorsed Darun"></asp:Label>
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="imgSharePrint" ImageUrl="image/print.png" />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table width="100%" class="tableStyles1">
                    <tr>
                        <td>
                            <asp:Panel runat="server" ID="panupgrade" CssClass="tableStyle" BackColor="Gray">
                                <asp:Label runat="server" ID="lblProfile" Text="Upgrade your profile now"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblMessage" Text="Send message to anyone"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblChat" Text="Chat with any user if online"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblInvite" Text="Invite Friends and earn money"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblCV" Text="view complete stats about your CV view"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblPost" Text="Post Jobs"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblPostDaily" Text="Post daily Deals"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblUpdateDaily" Text="Update your daily availability"></asp:Label>
                            <asp:LinkButton runat="server" ID="lnkUpgrade" Text="Upgrade Now" ForeColor="Blue"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <table class="tableStyle">
                    <snovaspace:Cv runat="server" ID="cvStatisticsUc" />
                </table>
            </td>
            <td>
            </td>
    </table>
    <table class="tableStyle">
        <tr>
            <td class="cellStyle4">
            </td>
            <td align="left" style="width: 71%;">
                <asp:LinkButton runat="server" ID="lnkSummary" Text="Summary"></asp:LinkButton>
            </td>
            <td valign="top" class="columnStyles5" rowspan="3">
                <table style="width: 100%" class="tableStyles1">
                    <tr>
                        <td class="style9" colspan="2">
                            <asp:Panel runat="server" ID="panViewProfile" Width="100%" BackColor="Gray">
                                <asp:Label runat="server" ID="lblViewProfile" Text="Who's viewed your profile"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblToday" Text="Today"></asp:Label>
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkLast" Text="Last Week" CssClass="LinkButton"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkMonth" Text="Last Month" CssClass="LinkButton"> </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkAltime" Text="AllTime" CssClass="LinkButton"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton runat="server" ID="imgJosYousuf" ImageUrl="image/photo51.png" />
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkJosYousuf" Text="Jos Yousuf"></asp:LinkButton><br />
                            <asp:Label runat="server" ID="lblAddJos" Text="Owner at Cognos Ltd"></asp:Label><br />
                            <asp:Label runat="server" ID="lblAddJos2" Text="Chamberely,United Kingdom|Management Consulting"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ImageButton runat="server" ID="imgPadro" ImageUrl="image/photo51.png" />
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="lnkPadro" Text=" Padro Azevedo"></asp:LinkButton><br />
                            <asp:Label runat="server" ID="lblPadro" Text="Private Chef"></asp:Label><br />
                            <asp:Label runat="server" ID="lblchefAdd" Text="Chamberely Area,France|Food&Beverages"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td valign="top" class="columnStyles5">
                <p>
                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem
                    Ipsum has been the industry's standard dummy text ever since the 1500s, when an
                    unknown printer took a galley of type and scrambled it to make a type specimen book.
                    It has survived not only five centuries, but also the leap into electronic typesetting,
                    remaining essentially unchanged.
                </p>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:LinkButton runat="server" ID="lnkExperience2" Text="Experience" CssClass="LinkButton"></asp:LinkButton>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td valign="top" class="columnStyles5">
                <asp:Label runat="server" ID="lblHead1" Text="Head Chef"></asp:Label><br />
                <asp:Label runat="server" ID="lblJamies" Text="Jamies Italian" ForeColor="blue"></asp:Label><br />
                <asp:Label runat="server" ID="lblLondon" Text="london,United Kingdom"></asp:Label><br />
                <asp:Label runat="server" ID="lblYear" Text="January 2011-Present(1 year 2 months)"></asp:Label><br />
                <p>
                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem
                    Ipsum has been the industry's standard dummy text ever since the 1500s, when an
                    unknown printer took a galley of type and scrambled it to make a type specimen book.
                    It has survived not only five centuries, but also the leap into electronic typesetting,
                    remaining essentially unchanged.
                </p>
            </td>
            <td>
                <snovasys:Friend ID="FriendCv" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label runat="server" ID="lblHead2" Text="Head Chef"></asp:Label><br />
                <asp:Label runat="server" ID="lblAdd4" Text="Jamies Italian" ForeColor="blue"></asp:Label><br />
                <asp:Label runat="server" ID="lblAdd5" Text="london,United Kingdom"></asp:Label><br />
                <asp:Label runat="server" ID="lblAdd6" Text="January 2011-Present(1 year 2 months)"></asp:Label><br />
                <p>
                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem
                    Ipsum has been the industry's standard dummy text ever since the 1500s, when an
                    unknown printer took a galley of type and scrambled it to make a type specimen book.
                    It has survived not only five centuries, but also the leap into electronic typesetting,
                    remaining essentially unchanged.
                </p>
            </td>
            <td>
                <asp:Panel ID="Panel1" runat="server" BorderColor="#DCDCDC" BorderStyle="Solid" BorderWidth="2px"
                    Width="100%">
                    <asp:Label ID="Label1" runat="server" Width="100%" BackColor="#DCDCDC" Text="Invite your friends & Earn money."></asp:Label>
                    <%--Table in panel--%>
                    <table class="tableStyle">
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgTwitter" runat="server" Height="24px" ImageUrl="~/image/twitterimg.png" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgFacebook" runat="server" CssClass="imageStyle2" ImageUrl="~/image/f+.png" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgLinkedIn" runat="server" CssClass="imageStyle2" ImageUrl="~/image/linkedinimg.png" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgMail" runat="server" CssClass="imageStyle2" ImageUrl="~/image/mail.png" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgYahoo" runat="server" CssClass="imageStyle2" ImageUrl="~/image/yahoo.png" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgMail2" runat="server" CssClass="imageStyle2" ImageUrl="~/image/imgMail.png" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cellStyle15" colspan="6">
                                <asp:LinkButton ID="lnkConnHeading" runat="server" Text="This is how it works" ForeColor="Blue"
                                    Font-Size="Large"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <%--Table below table--%>
                    <table cellpadding="0px" cellspacing="0px" class="tableStyle">
                        <tr>
                            <td class="cellStyle16">
                                <asp:LinkButton ID="lnkConn1" runat="server" CssClass="LinkButton" Text="1st Connection"
                                    ForeColor="Blue"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblConn1" runat="server" Font-Size="Small" Text="You invite you friends-You get -$2 per friend"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkConn2" runat="server" CssClass="LinkButton" Text="2nd Connection"
                                    ForeColor="Blue"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblConn2" runat="server" Font-Size="Small" Text="Your friends invite their friends- Your friend gets $2-you get $1 per friends"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkConn3" runat="server" CssClass="LinkButton" Text="3rd Connection"
                                    ForeColor="Blue"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblConn3" runat="server" Font-Size="Small" Text="Their friends inturn invite their friends- Your friends friend get $2, Your friend gets $1 and you get $0.5 per friend"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="cellStyle21">
                                <asp:LinkButton ID="lnkLearnMore" runat="server" Text="Learn more >" ForeColor="Blue"
                                    Font-Size="Small" CssClass="LinkButton"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <%-- Table below table--%>
                    <table class="tableStyle">
                        <tr>
                            <td class="cellStyle16">
                                <asp:ImageButton ID="img1" runat="server" ImageUrl="~/image/image7_1.png" /><br />
                                <asp:LinkButton ID="lnkImg1" Font-Size="X-Small" runat="server" ForeColor="Blue"
                                    Text="326" CssClass="LinkButton"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="img2" runat="server" ImageUrl="~/image/image7_2.png" /><br />
                                <asp:LinkButton ID="lnkImg2" runat="server" Font-Size="X-Small" ForeColor="Blue"
                                    Text="236" CssClass="LinkButton"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="img3" runat="server" ImageUrl="~/image/image7_3.png" /><br />
                                <asp:LinkButton ID="lnkImg3" runat="server" Font-Size="X-Small" ForeColor="Blue"
                                    Text="28" CssClass="LinkButton"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="img4" runat="server" ImageUrl="~/image/imageLady.png" /><br />
                                <asp:LinkButton ID="lnkImg4" runat="server" Font-Size="X-Small" ForeColor="Blue"
                                    Text="59" CssClass="LinkButton"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="img5" runat="server" ImageUrl="~/image/image50.png" /><br />
                                <asp:LinkButton ID="lnkimg5" runat="server" Font-Size="X-Small" ForeColor="Blue"
                                    Text="326" CssClass="LinkButton"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:LinkButton runat="server" ID="lnkShowMore" Text="Show more..." CssClass="LinkButton"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="lnkEducation1" Text="Education" CssClass="LinkButton"></asp:LinkButton>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td class="columnStyles5">
                <asp:LinkButton runat="server" ID="lnkIndustry" Text="Industry,Skills & Expertise"
                    CssClass="LinkButton"></asp:LinkButton>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td class="columnStyles5">
                <asp:LinkButton runat="server" ID="lnkPersonal" Text="Personal Deatails" CssClass="LinkButton"></asp:LinkButton>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td class="columnStyles5">
                <asp:LinkButton runat="server" ID="lnkEndorsements" Text="Endorsements" CssClass="LinkButton"></asp:LinkButton>
            </td>
            <td>
                <table class="tableStyle">
                    <tr>
                        <td class="cellStyles12">
                            <asp:Label ID="lblUsers" runat="server" Text="Users" ForeColor="Orange"></asp:Label>
                        </td>
                        <td class="columnStyles1">
                            <asp:Label ID="lblUsers1" runat="server" Text="103,546"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="cellStyles12">
                            <asp:Label ID="lblInvitesSent1" runat="server" Text="Invites sent" ForeColor="Orange"></asp:Label>
                        </td>
                        <td class="columnStyles1">
                            <asp:Label ID="lblInvitesSent2" runat="server" Text="1,363,948"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="cellStyles12">
                            <asp:Label ID="lblInvitesValues1" runat="server" Text="Total Value of Invites" ForeColor="Orange"></asp:Label>
                        </td>
                        <td class="columnStyles1">
                            <asp:Label ID="lblInvitesValues2" runat="server" Text="$3409870"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="cellStyles12">
                            <asp:Label ID="lblMoneyReleased1" runat="server" Text="Money released to users" ForeColor="Orange"></asp:Label>
                        </td>
                        <td class="columnStyles1">
                            <asp:Label ID="lblMoneyreleased2" runat="server" Text="$258,865"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td align="center">
                <asp:ImageButton runat="server" ID="imgSquare" ImageUrl="image/imagesquare.png" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="center">
                <asp:ImageButton runat="server" ID="imgFooter" ImageUrl="image/headerimg1.png" />
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
     <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
