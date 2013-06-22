<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserSearch.aspx.cs" Inherits="Huntable.UI.UserSearch" %>

<%@ Register Src="~/UserControls/FriendsToInvite.ascx" TagPrefix="uc1" TagName="InviteFr" %>
<%@ Register Src="~/UserControls/cvStatistics.ascx" TagPrefix="uc2" TagName="CvStats" %>
<%@ Register Src="~/UserControls/SeeYourFriendsInvitations.ascx" TagPrefix="uc3"
    TagName="FriendsInvitations" %>
<%@ Register Src="~/Controls/DataPager.ascx" TagPrefix="uc" TagName="DataPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link type="text/css" href="css1/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        $().ready(function () {
            $('#dialogContent').dialog({
                autoOpen: false,
                modal: true,
                bgiframe: true,
                title: "Send Message",
                width: 600,
                height: 380
            });
            $('#dialogContent').parent().appendTo($("form:first"));
        });

        $(function () {
            $('#<%= txtSearchSkill.ClientID %>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "HuntableWebService.asmx/SearchSkills",
                        data: "{ 'word': '" + request.term + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 2
            });
        });

        function ChatWindow(target) {
            if (document.getElementById('<%=hdnUserId.ClientID %>').value == "") {
                $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
                $('#dialog').dialog('open');
                return false;
            } else {
                window.open(target, "ajaxim_", "width=650,height=400,resizable=1,menubar=0,status=0,toolbar=0");
                return false;
            }

        }
        function rowAction(uniqueID, userName) {
            if (document.getElementById('<%=hdnUserId.ClientID %>').value != "") {
                $('#dialogContent').show();
                $('#<%=txtToAddress.ClientID %>').val(userName);
                $('#<%=lblName.ClientID %>').text(userName);
                $('#<%=txtMessage.ClientID %>').val('');
                $('#dialogContent').dialog('option', 'buttons',
				{
				    "Send": function () { __doPostBack(uniqueID, ''); $(this).dialog("close"); },
				    "Cancel": function () { $(this).dialog("close"); }
				});

                $('#dialogContent').dialog('open');
                return true;
            }
            else {
                $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
                $('#dialog').dialog('open');
                return false;

            }
        }
    </script>
    <script type="text/javascript">
        function overlay(id) {
            el = document.getElementById('ovrly');
            $('#ovrly').show();

            $('#<%= pbl.ClientID %>').text(id);

        }
        $(document).ready(function () {

            $('#ximg').click(function () {
                $('#ovrly').hide();
                return false;
            });
        });
    </script>
    <script type="text/javascript">

        function rowAction22() {
            $('#dialog11').dialog({

                autoOpen: true,
                modal: true,
                width: 600,

                buttons: {
//                    "Ok": function () {
//                        
//                        $(this).dialog("close");
//                    },
//                    "Cancel": function () {
//                        $(this).dialog("close");
//                    }

                }

            });
            return false;
        }

     
 </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="js1/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js1/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript">
        $(function () {
            // Dialog
            $('#dialog').dialog({
                autoOpen: false,
                modal: true,
                width: 600,
                buttons: {
                    "Ok": function () {
                        $(this).dialog("close");
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                }
            });
        });
    </script>

    <script type="text/javascript">
        $(function () {

            var msie6 = $.browser == 'msie' && $.browser.version < 7;
            if (!msie6) {
                var top = $('#box-usersearch').offset().top;
                $(window).scroll(function (event) {
                    var y = $(this).scrollTop();
                    if (y >= 547) { $('#box-usersearch').addClass('fixed'); }
                    else { $('#box-usersearch').removeClass('fixed'); }
                });
            }
        });
        $(function () {

            // Check whether browser is IE6

            var msie6 = $.browser == 'msie' && $.browser.version < 7;

            // Only run the following code if browser
            // is not IE6. On IE6, the box will always
            // scroll.

            if (!msie6) {

                // Set the 'top' variable. The following
                // code calculates the initial position of
                // the box. 

                var top = $('#box-usersearch').offset().top;

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

                    if (y >= 47) {

                        // Set the box to the 'fixed' class.

                        $('#box-usersearch').addClass('fixed');

                    } else {

                        // Remove the 'fixed' class 

                        $('#box-usersearch').removeClass('fixed');
                    }
                });
            }
        });
   </script>
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <div id="dialog" title="Confirm Message">
        <asp:Label ID="lblConfirmMessage" runat="server" />
    </div>
     <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>
     <div id="dialog11" style="display: none;" title="List Of Companies">
         <table>
         <asp:DataList runat="server" ID="dlListOfCompanies">
             <ItemTemplate>
                 <tr>
                     <td style="width: 235px;">
               <a href='<%#CompanyUrlGenerator(Eval("Id") )%>'> <asp:Label runat="server" Text='<%#Eval("CompanyName") %>' style="color:  #008ca1;"></asp:Label> </a> </td>
                 <%--<asp:Label runat="server" Text='<%#Eval("CompanyWebsite") %>'></asp:Label>
                 <asp:Label ID="Label1" runat="server" Text='<%#Eval("CompanyEmail") %>'></asp:Label>--%>
               <td> <a href='<%#CompanyUrlGenerator(Eval("Id") )%>'> <asp:Image ID="Image1" ImageUrl='<%#Picture(Eval("CompanyLogoId"))%>' runat="server" Width="119px"
                                        Height="62px" /></a> </td>
               </tr>
             </ItemTemplate>
             
         </asp:DataList>
         </table>
        <%-- <asp:Label id="TextBox1" runat="server" Text="22aaaaaaaaaaa"></asp:Label>  
         <asp:Image runat="server" ID="imgHasCompany0001" ImageUrl="images/UserCompany.png" Width="33px" Height="35px" />--%> 
   </div>
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="notification">
                    <div class="advanced1">
                        <%--<a href="#" class="accounts-link">User Search Page ></a>--%>
                    </div>
                   
                    <div class="notification-left" style="margin-top:-6px;">
                        <asp:TextBox ID="txtSearchSkill" runat="server" CssClass="textbox textbox-inner"
                            Text="Skill" onblur="if (this.value == '') {this.value ='Skill';}" onfocus="if (this.value =='Skill') {this.value ='';}"></asp:TextBox>
                        <br />
                        <asp:TextBox ID="txtSearchKeywords" runat="server" CssClass="textbox textbox-inner"
                            Text="Keywords" onblur="if (this.value == '') {this.value ='Keywords';}" onfocus="if (this.value =='Keywords') {this.value ='';}"></asp:TextBox>
                        <br />
                        <asp:TextBox ID="txtSearchFirstName" runat="server" CssClass="textbox textbox-inner"
                            Text="FirstName" onblur="if (this.value == '') {this.value ='FirstName';}" onfocus="if (this.value =='FirstName') {this.value ='';}" /><br />
                        <asp:TextBox ID="txtSearchLastName" runat="server" CssClass="textbox textbox-inner"
                            Text="LastName" onblur="if (this.value == '') {this.value ='LastName';}" onfocus="if (this.value =='LastName') {this.value ='';}" /><br />
                        <asp:TextBox ID="txtSearchTitle" runat="server" CssClass="textbox textbox-inner"
                            Text="Title" onblur="if (this.value == '') {this.value ='Title';}" onfocus="if (this.value =='Title') {this.value ='';}" /><br />
                        <asp:DropDownList ID="rbSearchTitleType" runat="server">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Current" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Past" Value="2" />
                        </asp:DropDownList>
                        <asp:TextBox ID="txtSearchCompany" runat="server" CssClass="textbox textbox-inner"
                            Text="Company" onblur="if (this.value == '') {this.value ='Company';}" onfocus="if (this.value =='Company') {this.value ='';}" />
                        <asp:DropDownList ID="rbSearchCompanyType" runat="server">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Current" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Past" Value="2" />
                        </asp:DropDownList>
                        <br />
                        <asp:TextBox ID="txtSearchSchool" runat="server" CssClass="textbox textbox-inner"
                            Text="School" onblur="if (this.value == '') {this.value ='School';}" onfocus="if (this.value =='School') {this.value ='';}" /><br /><br />
                        Experience :
                        <asp:TextBox ID="txtSearchExpFrom" runat="server" Width="46px" />
                        -<asp:TextBox ID="txtSearchExpTo" runat="server" Width="46px" /><br />
                        <br />
                        Country :
                       <div style="margin-top: -31px; width: 129px; margin-left: 50px;"><asp:DropDownList ID="ddlSearchCountry" runat="server" CssClass="textbox selectbox-inner"  Width="129px" />
                       </div><br />
                        <b>Available Now :</b>
                        <asp:DropDownList ID="rbListAvailable" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2" />
                        </asp:DropDownList>
                       <br /><br />
                       <%--<asp:TextBox ID="txtLanguagesKnown" runat="server" name="email" type="text" onblur="if (this.value == '') {this.value ='Language Known';}"
                            onfocus="if (this.value =='Language Known') {this.value ='';}" 
                            class="textbox textbox-inner" /><br />--%>
                        Industry:
                       <div style="margin-top: -31px;width: 130px;margin-left: 48px;"> <asp:DropDownList ID="ddlindustry" Width="130px" runat="server" CssClass="textbox selectbox-inner" /></div>
                        <br />
                        Interest:
                        <div style="margin-top: -31px; margin-left: 48px;">
                        <asp:DropDownList ID="ddlinterest" runat="server" CssClass="textbox selectbox-inner" Width="133px"
                            AppendDataBoundItems="True">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList></div>
                        <br />
                        
                        <asp:Button ID="btnSearch" runat="server" CssClass="button-green" Text="Search" OnClick="BtnSearchClick" />
                    </div>
                 
                    <div class="notification-right notification-right12">
                        <h3 class="login-heading">
                            Your search results <b class="green-color">
                                <asp:Label runat="server" ID="lblNoOfSearchResults"></asp:Label>
                                People</b></h3>
                        <asp:Label runat="server" ID="lblMessage"></asp:Label>
                        <asp:Repeater ID="rpUserSearchResults" runat="server" OnItemDataBound="RpUserSearchResultsItemDataBound">
                            <ItemTemplate>
                                <div class="notification-head">
                                    <div class="user-left">
                                        <asp:ImageButton runat="server" ID="ibtnSenderProfilePicture" ImageUrl='<%#Eval("UserProfilePictureDisplayUrl")%>'
                                            CssClass="profile-pic profile-pic2" Width="76" Height="81" PostBackUrl='<%#UrlGenerator(Eval("ID")) %>' />
                                        <asp:ImageButton ID="imgAvailability" runat="server" ImageUrl='<%#Eval("UserAvailabilityImagePath") %>'
                                            Width="16px" Height="16px" OnClientClick='<%# "return ChatWindow(\""+ GetUrl(Eval("Id"))+"\");" %>' />
                                        <asp:Label Font-Size="11px" runat="server" Text='<%# Eval("MemberAvailabilityMessage") %>'></asp:Label>
                                    </div>
                                    <div class="user-right">
                                        
                                        <a href='<%#UrlGenerator(Eval("Id")) %>' class="accounts-link">
                                            <%#Eval("Name") %></a><br />
                                        <a href='<%#UrlGenerator(Eval("Id")) %>' class="accounts-link">
                                            <%#Eval("CurrentPosition")%></a> at <a href='<%#UrlGenerator(Eval("Id")) %>'
                                                class="accounts-link">
                                                <%# Eval("CurrentCompany")%></a><br />
                                        <strong>Past : </strong><a href="#" class="accounts-link">
                                            <%#Eval("PastPosition")%></a> at <a href="#" class="accounts-link">
                                                <%#Eval("PastCompany")%></a><a href='<%#UrlGenerator(Eval("Id")) %>'
                                                    class="details-link details-link-user">More...</a><br />
                                        <strong>
                                            <%#Eval("City")%>
                                            : </strong>
                                        <%#Eval("County")%><br />
                                        <strong>Experience : </strong>
                                        <%#Eval("TotalExperienceInYears") %>
                                        Years<br />
                                        
                                    </div>
                                    
                                        
                                   
                                    <div class="notification-links notification-links2">
                                        <b>Available Now :</b>
                                        <%# Eval("UserAvailabilityInformation")%><a href='<%#UrlGenerator(Eval("Id")) %>'
                                            class="details-link"><%# Eval("ProfessionalRecommendationsCount") %>
                                            Professional Recommendations&nbsp;&nbsp;<img src="images/pagination-arrow-right.png"
                                                width="4" height="7" alt="arrow" /></a>
                                    </div>

                                    <asp:ImageButton runat="server" CommandArgument='<%#Eval("Id")%>' OnClick="UserCompaniesClick" 
                                     ID="imgHasCompany" ImageUrl="Images/UserCompany.png" Width="33px" Height="35px" Visible='<%#Eval("UserHasCompany") %>'  style="margin-top: 5px; margin-left: -45px;"  />

                                    <div class="total-views">
                                        <div class="floatleft">
                                            <a href='<%#UrlGenerator(Eval("Id")) %>' class="accounts-link">Times Profile viewed :<%#Eval("ProfileVisitedCount") %></a>&nbsp;&nbsp;<a
                                                href="#" class="accounts-link">Affiliate Earnings :<%#Eval("AffliateAmountAsText") %></a></div>
                                        <div id="btnLogged" runat="server">
                                            <asp:Button ID="btnMessage" Text="Message" runat="server" UseSubmitBehavior="false"
                                                CssClass="button-orange button-orange-msg" CommandArgument='<%#Eval("Id")%>'
                                                OnClientClick='<%# DataBinder.Eval(Container.DataItem, "Name", "javascript:return rowAction(this.name,\"{0}\");")%>'
                                                OnClick="BtnMessageClick" /></div>
                                    </div>
                                  
                                   
                                    <%--<asp:ImageButton runat="server" CommandArgument='<%#Eval("Id")%>' OnClick="UserCompaniesClick" 
                                     ID="imgHasCompany" ImageUrl='<%#Eval("UserHasCompany") %>' Width="33px" Height="35px"  />--%>
                                     
                                     

                                </div>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <hr />
                            </SeparatorTemplate>
                        </asp:Repeater>
                        <asp:HiddenField ID="hfSubject" runat="server" />
                        <div align="center">
                            <a href="#">
                                <img src="images/google-banner-medium.jpg" width="300" height="100" alt="google-add" /></a>
                        </div>
                        <p class="strip-bottom">
                            &nbsp;</p>
                        <uc:DataPager ID="pagerUsers" runat="server" PageIndex="1" RecordsPerPage="10" TotalRecords="0" />
                        <div id="dialogContent" align="center">
                            <%--   <div class="apply-job">--%>
                            To:
                            <asp:TextBox ID="txtToAddress" runat="server" ReadOnly="true" /><br />
                            <div>
                                What would you like to message<asp:Label ID="lblName" runat="server" />?<br />
                                <asp:RadioButtonList ID="rbMessageList" runat="server">
                                    <asp:ListItem Value="0" Text="Job Enquiry"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Request endorsement"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Introduce Yourself"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="New Business Opportunity"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Your Recruitment requirement"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <%-- Subject:
                                <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox><br />--%>
                            <%-- $('#<%=txtSubject.ClientID %>').val('');--%>
                            <br />
                            Message:
                            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" style="font-size:12px;height:65px;width:150px;"/>
                            <%--  </div>--%>
                        </div>
                    </div>
                </div>
            </div>
            <div id="rtContent" runat="server" class="content-inner-right">
                <div class="box-right">
                    <uc1:InviteFr ID="InviteFriends" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc2:CvStats ID="CvStatistics" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc3:FriendsInvitations ID="friendsInvts" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <%--<div id="dialogContent" style="visibility:hidden;">
        <table>
            <tr>
                <td>
                    To:
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    What would you like to message
                    <asp:Label ID="Label1" runat="server" />
                    ?
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" />
                </td>
            </tr>
        </table>
    </div>--%>
</asp:Content>
