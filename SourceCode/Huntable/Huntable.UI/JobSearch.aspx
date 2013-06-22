<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableSessionState="true"
    AutoEventWireup="true" CodeBehind="JobSearch.aspx.cs" Inherits="Huntable.UI.JobSearch" %>

<%@ Register Src="UserControls/JobControl.ascx" TagPrefix="uc1" TagName="JobControl" %>
<%@ Register Src="UserControls/FeaturedRecruiters.ascx" TagName="FeaturedRecruters"
    TagPrefix="uc2" %>
<%@ Register Src="~/Controls/DataPager.ascx" TagPrefix="uc" TagName="DataPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    $(function () {

        var msie6 = $.browser == 'msie' && $.browser.version < 7;
        if (!msie6) {
            var top = $('#bx21').offset().top;
            $(window).scroll(function (event) {
                var y = $(this).scrollTop();
                if (y >= 50) { $('#bx21').addClass('fixed'); }
                else { $('#bx21').removeClass('fixed'); }
            });
        }
    });</script>
    <script type="text/javascript">
        $(function () {

            var msie6 = $.browser == 'msie' && $.browser.version < 7;
            if (!msie6) {
                var top = $('#bx22').offset().top;
                $(window).scroll(function (event) {
                    var y = $(this).scrollTop();
                    if (y >= 8) { $('#bx22').addClass('fixed'); }
                    else { $('#bx22').removeClass('fixed'); }
                });
            }
        });</script>



    <div id="content-section" >
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="notification" style="width:754px">

             
                   <uc1:JobControl ID="JobControl1" runat="server"></uc1:JobControl>

                    <div class="notification-right notification-right1" >
                        <h3 class="login-heading">
                            Your search results <b class="green-color">
                                <label runat="server" id="lblNoOfJobs">
                                </label>
                                jobs</b></h3>
                        <asp:Repeater ID="empllstvw" runat="server" OnItemCommand="EmpllstvwItemCommand">
                            <ItemTemplate>
                                <div class="notification-head">
                                    <a href="<%#UrlGenerator(Eval("Id")) %>" >
                                        <img id="Img1" src='<%#Eval("profileImagePath")%>' class="profile-pic profile-pic2"
                                            runat="server" width="76" height="81" /></a><a href="<%#UrlGenerator(Eval("Id")) %>"
                                                class="accounts-link">
                                                <%# Eval("Title") %></a><br />
                                    <b class="green-color">Salary
                                        <%#Eval("Salary") %>
                                        k</b><br />
                                    <strong>
                                        <%#Eval("LocationName")%>,
                                        <%#Eval("CountryName")%></strong><br />
                                    <%#Eval("JobDescription").ToString().Substring(0, Math.Min(500, Eval("JobDescription").ToString().Length))%>
                                    <div class="notification-links notification-links1">
                                        <asp:LinkButton ID="lbUserAlreadyApplied" class="invite-friend-btn invite-friend-btn1"
                                            Text="Already Applied " runat="server" Visible="false"></asp:LinkButton>
                                        <asp:LinkButton ID="lbUserNotAppliedToJob" class="invite-friend-btn invite-friend-btn1"
                                            Text="Apply now + " runat="server" CommandName="ApplyNow" CommandArgument='<%#Eval("Id")%>'
                                            Visible="false"></asp:LinkButton>
                                        JobPostedDate:<asp:Label ID="Label1" runat="server" Text='<%#Eval("CreatedDateTime", "{0:MM/dd/yyyy}")%>'></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <hr />
                            </SeparatorTemplate>
                        </asp:Repeater>
                         <p class="strip-bottom">
                            &nbsp;</p>
                         <uc:DataPager ID="pagerUsers" runat="server" PageIndex="1" RecordsPerPage="10" TotalRecords="0" />
                    </div>
                </div>
          </div> 
            <div class="content-inner-right" style="margin-right:-99px;height:510px;">
                <div class="post-opportunity">
                    <a class="button-orange floatleft " href="EditProfilePage.aspx" style="font-size: 12px;
                        padding: 7px 10px;">Is Your Profile Huntable? Update-Edit now</a>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
               
                   <div id="bx21"> <uc2:FeaturedRecruters ID="ucFeaturedRecruters" runat="server" /></div>
                
            </div>  </div>
        </div>
        <!-- content inner ends -->
        
       
           
           
       
    
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
