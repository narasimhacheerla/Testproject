<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableSessionState="true"
    AutoEventWireup="true" CodeBehind="UserPassiveJobs.aspx.cs" Inherits="Huntable.UI.UserPassiveJobs" %>

<%@ Register Src="~/UserControls/JobControl.ascx" TagPrefix="uc1" TagName="JobControl" %>
<%@ Register Src="~/UserControls/ImportContacts.ascx" TagPrefix="uc2" TagName="Contacts" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">
        function rowAction() {
            alert("This job has been expired");
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="profile-main">
                    <div class="accounts-profile accounts-profile1 accounts-profile-passive">
                        Passive Jobs
                    </div>
                </div>
                <br/>
                <br/>
                <div class="notification notification-job" style="margin-top: 30px;">
                    <uc1:JobControl ID="jControl" runat="server" />
                    <div class="notification-right notification-right1" style="width:432px">
                        <ul class="passive-list">
                            <li>
                                <asp:ListView runat="server" ID="empllstvw">
                                    <LayoutTemplate>
                                        <table cellpadding="2" runat="server" id="tblEmployees" style="width: 460px">
                                            <tr runat="server" id="itemPlaceholder">
                                            </tr>
                                        </table>
                                        <div class="pagination pagination-passive">
                                            <asp:DataPager ID="DataPagerProducts" runat="server" PagedControlID="empllstvw" PageSize="20"
                                                OnPreRender="DataPagerProductsPreRender">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
                                                    <asp:NumericPagerField />
                                                    <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <p>
                                            <asp:LinkButton ID="FirstNameLabel" runat="server" Text='<%#Eval("Title") %>' OnClientClick="javascript:return rowAction()" /></p>
                                          <label>  <asp:Label ID="PhoneLabel" runat="server" Text='<%#Eval("CountryName") %>'></asp:Label></label>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("Id") %>' />
                                       <%-- <asp:Label ID="JobTitleLabel" runat="server" Text='<%#Eval("JobDescription") %>'></asp:Label>--%>
                                        <br />
                                        
                                    </ItemTemplate>
                                </asp:ListView>
                            </li>
                        </ul>
                        <br />
                    </div>
                </div>
            </div>
            <div class="content-inner-right">
                <div id="postDiv" runat="server" class="post-opportunity">
                    <a href="#" class="button-orange floatleft " style="font-size: 12px; padding: 7px 10px;">
                        Is Your Profile Huntable? Update-Edit now</a>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="post-opportunity">
                    <asp:Button ID="Button1" class="button-green post-opportunity-btn" runat="server" Text="Post an Opportunity" OnClick ="BtnPostOpportunityClick"/>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div>
                    <img src="images/advanced-search-img.png" width="294" style="float: left;" class="profile-pic"
                        height="141" alt="advanced" />
                    <div class="advanced-desc">
                        Find Best Talent<br />
                        <strong class="blue-color">Advanced Search Filters</strong><br />
                        <a href="#" class="button-green floatleft" style="margin: 10px 0px 0px 105px;">Search
                            Now</a>
                    </div>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <uc2:Contacts ID="importContacts" runat="server" />
            </div>
            <div class="how-list">
                <ul>
                    <li>
                        <h2>
                            Super Power Your Profile</h2>
                        <a href="Super-power.aspx"> <img src="images/how-img1.jpg" width="280" height="174" alt="Super Power Your Profile" /></a>
                    </li>
                    <li>
                        <h2>
                            Customize feeds &amp; jobs you receive</h2>
                        <a href="CustomizeFeedsAndJobs.aspx"><img src="images/how-img2.jpg" width="280" height="174" alt="Customize feeds &amp; jobs you receive" /></a>
                    </li>
                    <li>
                        <h2>
                            Follow your favourite person, company skill or industry.</h2>
                        <a href="Follow.aspx"><img src="images/how-img3.jpg" width="280" height="174" alt="Follow your favourite person, company skill or industry." /></a>
                    </li>
                    <li>
                        <h2>
                            Connect &amp; network</h2>
                        <a href="ConnectAndNetwork.aspx"><img src="images/how-img4.jpg" width="280" height="174" alt="Connect &amp; network" /></a>
                    </li>
                    <li>
                        <h2>
                            Get Headhunted</h2>
                        <a href="GetHunted.aspx"><img src="images/how-img5.jpg" width="280" height="174" alt="Get Headhunted" /></a>
                    </li>
                    <li>
                        <h2>
                            Find your Dream Job</h2>
                        <a href="FindYourDreamJob.aspx"><img src="images/how-img6.jpg" width="280" height="174" alt="Find your Dream Job" /></a>
                    </li>
                </ul>
                <div align="center" style="font-size: 30px; margin-top: 20px; float: left; width: 100%;">
                    Register now and is totally <strong class="red-color">Free!!!</strong>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
