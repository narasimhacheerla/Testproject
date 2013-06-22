<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Customize-User.aspx.cs" Inherits="Huntable.UI.CustomizeUser" %>
<%@ Register Src="~/UserControls/ImportContacts.ascx" TagPrefix="imp" TagName="contacts" %>
<%@ Register Src="~/UserControls/FeaturedRecruiters.ascx" TagName="uclfeatured" TagPrefix="uc6" %>
<%@ Register Src="UserControls/SeeYourFriendsInvitations.ascx" TagName="uc" TagPrefix="Seeyour" %>
<%@ Register Src="~/Controls/DataPager.ascx" TagPrefix="uc" TagName="DataPager" %>
<%@ Register src="UserControls/PeopleYouMayKnow.ascx" tagName="pplUMayKnow" tagPrefix="uc12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/PagerStyles.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-1.7.1.min - Copy.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.dragval-1.0-pack.js"></script>
      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>  
      <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
      <script type="text/javascript">
          $(function () {
              $('#<%= txtSearchSkills.ClientID %>').autocomplete({
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
           <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>
    <div id="content-section">
        <div id="content-inner">
            <div class="green-desc">
                Customize Your Feeds and jobs here. Select either individual or multiple selections
                from various selection.
            </div>
            <div class="content-inner-left">
                <ul class="customize-feed">
                    <li><a style="border-right-color: rgb(255, 255, 255); border-right-width: 1px; border-right-style: solid;"
                        class="select" href="Customize-User.aspx">Customize Feeds</a></li>
                    <li runat="server" id="jobsal"><a href="CustomizeJobsSalary.aspx" runat="server" visible="true">Customize Jobs</a></li></ul>
                <ul class="tab-list tab-list-search">
                    <li><a class="st_tab st_first_tab st_tab_active" href="Customize-User.aspx">User</a></li>
                    <li><a class="st_tab" href="CustomizeFeedsIndustry.aspx">Industry</a></li>
                    <li><a class="st_tab" href="CustomizeFeedsSkill.aspx">Skill</a></li>
                    <li><a class="st_tab" href="CustomizeFeedsInterest.aspx">Interest</a></li>
                    <li><a class="st_tab" href="CustomizeFeedsCountry.aspx">Country</a></li>
                
                </ul>
                <div class="job-type job-type-user">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:CheckBox ID="cbShowConnections" runat="server" CssClass="checkbox-tab" OnCheckedChanged="CbShowConnectionsCheckedChanged" />
                            <label>
                                <strong>Show Feeds From my Connections</strong></label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="profile-search">
                    <b>Search for User:</b><asp:TextBox runat="server" ID="txtTopSearchKeyword" CssClass="textbox-search textbox-search-inner textbox-search-customize"
                        name="email" /><a runat="server" id="btnSearchTopKeyword" class="button-orange button-orange-search">Search<img
                            alt="arrow" src="HuntableImages/search-arrow.png" width="22" height="23"></a></div>
                <div class="notification notification-user">
                    <div class="notification-left">
                        <asp:TextBox ID="txtSearchSkills" runat="server" onblur="if (this.value == '') {this.value ='Skills';}"
                            CssClass="textbox textbox-search1" onfocus="if (this.value =='Skills') {this.value ='';}"
                            value="Skills" />
                        <asp:ImageButton ID="btnSkillsSearch" OnClick="BtnSkillsSearchClick" ImageUrl="HuntableImages/search-img.png"
                            Width="28" Height="27" runat="server" />
                        <br/>
                        <br/>
                        <asp:TextBox ID="txtSearchKeywords" runat="server" onblur="if (this.value == '') {this.value ='Keywords';}"
                            CssClass="textbox textbox-search1" onfocus="if (this.value =='Keywords') {this.value ='';}"
                            value="Keywords" />
                        <asp:ImageButton ID="btnKeywordSearch" OnClick="BtnKeywordSearchClick" ImageUrl="HuntableImages/search-img.png"
                            Width="28" Height="27" runat="server" />
                        <asp:TextBox ID="txtSearchFirstName" runat="server" onblur="if (this.value == '') {this.value ='First name';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='First name') {this.value ='';}"
                            name="email" value="First name" />
                        <asp:TextBox ID="txtSearchLastName" runat="server" onblur="if (this.value == '') {this.value ='Last name';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Last name') {this.value ='';}"
                            name="email" value="Last name" />
                        <asp:TextBox ID="txtTitle" runat="server" onblur="if (this.value == '') {this.value ='Title';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Title') {this.value ='';}"
                            name="email" value="Title" />
                        <select class="textbox selectbox-inner">
                            <option>Current or Past</option>
                            <option>Job Type 1</option>
                            <option>Job Type 2</option>
                        </select>
                        <asp:TextBox ID="txtSearchCompany" runat="server" onblur="if (this.value == '') {this.value ='Company';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Company') {this.value ='';}"
                            name="email" value="Company" />
                        <asp:TextBox ID="txtSearchSchool" runat="server" onblur="if (this.value == '') {this.value ='School';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='School') {this.value ='';}"
                            name="email" value="School" />
                        <asp:TextBox ID="txtSearchExp" runat="server" onblur="if (this.value == '') {this.value ='Experience';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Experience') {this.value ='';}"
                            name="email" value="Experience" />
                        <asp:TextBox ID="txtSearchYear" runat="server" onblur="if (this.value == '') {this.value ='Year';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Year') {this.value ='';}"
                            name="email" value="Year" />
                        <br>
                        <br>
                        <b>Available Now</b>
                        <asp:DropDownList ID="ddlAvailableNow" runat="server" CssClass="textbox selectbox-inner">
                            <asp:ListItem Text="Yes" Selected="True" />
                            <asp:ListItem Text="No" />
                        </asp:DropDownList>
                        <br>
                        <br>
                        <b>Country</b>
                        <asp:DropDownList ID="ddlSearchCountry" runat="server" CssClass="textbox selectbox-inner" />
                        <asp:Button runat="server" Text="Search" CssClass="button-green button-green-jobpost"
                            OnClick="BtnSearchClick" />
                    </div>
                    <div class="notification-right">
                        <ul class="user-list">
                            <asp:DataList ID="lstUsers" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                    <li><a href='<%#UrlGenerator(Eval("Id")) %>'>
                                            <img class="profile-pic profile-pic-user" alt="User" runat="server" src='<%#Eval("UserProfilePictureDisplayUrl")%>'
                                                 width="46" height="45"/></a> <a class="accounts-link"  href='<%#UrlGenerator(Eval("Id")) %>'>
                                                <%#Eval("Name")%></a><br>
                                        <%#Eval("CurrentPosition")%>,
                                        <%#Eval("CurrentCompany")%><br>
                                        <strong>Location:</strong>
                                        <%#Eval("CountryName")%>
                                        <div class="floating" visible='<%#!IsThisUserFollowingCompany(Eval("Id"))%>' runat="server">
                                            <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id")%>' OnClick="FollowupClick"
                                                class="invite-friend-btn">
                                                Follow +</asp:LinkButton></div>
                                        <div id="Div1" class="floating" visible='<%#IsThisUserFollowingCompany(Eval("Id"))%>' runat="server">
                                        <asp:Image runat="server" Width="20" Height="20" ID="Following" ImageAlign="Right"
                                            ImageUrl="images/tick.png" />
                                           <%-- <asp:Label runat="server" Text="Following Now"></asp:Label>--%>
                                        </div>
                                        
                                        </div> </li>
                                </ItemTemplate>
                            </asp:DataList>
                        </ul>
                        <div class="pagination pagination-user">
                            <uc:DataPager ID="pagerUsers" runat="server" PageIndex="1" RecordsPerPage="10" TotalRecords="0" />
                        </div>
                    </div>
                </div>
                 <a href="CustomizeFeedsIndustry.aspx" class="button-green floatright">Next</a>
            </div>
            <!-- content inner left ends -->
            <div class="content-inner-right">
             

                 <div class="box-right" runat="server" id="pplYoumayKnowDiv">
                  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                 <uc12:pplUMayKnow ID="PplUMayKnow1" runat="server" /> &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                     </ContentTemplate>
                    </asp:UpdatePanel>
                 
             </div><br/>
             <div class="box-right">
                
                 
                 <uc6:uclfeatured ID="uc6" runat="server" />
             </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <%--<div class="box-right">
                <imp:contacts ID="impCon" runat="server" ></imp:contacts>
                   </div>--%>
                <%--<p class="margin-top-visible">
                    &nbsp;</p>--%>
                <div class="box-right">
                 <Seeyour:uc ID="Seeyour" runat="server" />
                   </div>
               
            </div>
        </div>
        <!-- content inner right ends -->
    </div>
    <!-- content inner ends -->
    </div>
     <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js" type="text/javascript"></script>
<script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
