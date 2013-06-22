<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" EnableSessionState="true"
    AutoEventWireup="true" CodeBehind="JobsApplied.aspx.cs" Inherits="Huntable.UI.JobsApplied" %>
  
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
     <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>

    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="accounts-profile accounts-profile1">
                    <a href="myaccount.aspx" class="accounts-link">My Accounts</a>&nbsp;&rsaquo;&rsaquo;&nbsp;<a>My job
                        applications</a>
                </div>
                <div class="notification">
                    <%--   <div class="notification-left">
                        <b class="job-search-heading">Job Search</b>
                        <asp:TextBox ID="jobtitle" class="textbox textbox-inner" runat="server" onblur="if (this.value == '') {this.value ='Job Title';}"
                            onfocus="if (this.value =='Job Title') {this.value ='';}" Text="Job Title" />
                        <asp:TextBox ID="keywords" class="textbox textbox-inner" runat="server" onblur="if (this.value == '') {this.value ='Keywords';}"
                            onfocus="if (this.value =='Keywords') {this.value ='';}" Text="Keywords" />
                        <asp:DropDownList class="textbox selectbox-inner" ID="ddlCountry" runat="server">
                            <asp:ListItem Value="Country"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList class="textbox selectbox-inner" ID="ddlLocation" runat="server">
                            <asp:ListItem Value="Location"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList class="textbox selectbox-inner" ID="ddlSalary" runat="server">
                            <asp:ListItem Value="Salary"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList class="textbox selectbox-inner" ID="ddlExperience" runat="server">
                            <asp:ListItem Value="Experience"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="company" class="textbox textbox-inner" runat="server" onblur="if (this.value == '') {this.value ='Company';}"
                            onfocus="if (this.value =='Company') {this.value ='';}" Text="Company" />
                        <asp:DropDownList class="textbox selectbox-inner" ID="ddlIndustry" runat="server">
                            <asp:ListItem Value="Industry"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList class="textbox selectbox-inner" ID="ddlSkill" runat="server">
                            <asp:ListItem Value="Skill"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList class="textbox selectbox-inner" ID="ddlJobType" runat="server">
                            <asp:ListItem Value="Job Type"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="BtnSearch" CssClass="button-green button-green-jobpost" />
                    </div>--%>
                    <%--<div class="content-inner">--%>
                    <h3 class="login-heading">
                        My Job Applications</h3>
                    <%-- <div class="notification-head">--%>
                    <asp:ListView ID="lvJobsApplied" runat="server" DataKeyNames="Id" OnItemCommand="LvJobsAppliedItemCommand">
                        <LayoutTemplate>
                            <table cellpadding="2" runat="server" id="tblEmployees" style="width: 460px">
                                <tr runat="server" id="itemPlaceholder">
                                </tr>
                            </table>
                            <div class="pagination">
                                <div align="center" style="margin-left:-233px">
                                    <img src="images/google-banner-medium.jpg" width="300" height="100" alt="google-add" />
                                </div>
                                <br />
                                <div style="margin-left:33px;width:90px;">
                                <asp:DataPager ID="dpgSearchResultsPager" runat="server" PagedControlID="lvJobsApplied"
                                    PageSize="10">
                                    <Fields>
                                        <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
                                        <asp:NumericPagerField />
                                        <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" />
                                    </Fields>
                                </asp:DataPager>
                                </div>
                            </div>
                        </LayoutTemplate>
                        <ItemSeparatorTemplate>
                          <%--  <hr />--%>
                        </ItemSeparatorTemplate>
                        <ItemTemplate>
                            <div class="notification-head" style="width: 430px;">
                                <a href="<%#UrlGenerator(Eval("Id")) %>" >
                                    <img src="<%#Eval("ProfileImagePath")%>" class="profile-pic profile-pic2" width="76"
                                        height="81" alt="Image not found" /></a><a href="<%#UrlGenerator(Eval("Id")) %>"
                                            class="accounts-link">
                                            <%#Eval("Title") %></a><br />
                                <b class="green-color">Salary
                                    <%#Eval("Salary") %>
                                    k</b><br />
                                <strong>
                                    <%# Convert.ToString(Eval("LocationName")) + ", " + Convert.ToString(Eval("CountryName"))%></strong><br />
                                <%#Eval("JobDescription").ToString().Substring(0, Math.Min(500, Eval("JobDescription").ToString().Length))%>
                                <div class="notification-links notification-links1">
                                    <strong>Date Applied: </strong>
                                    <%#Eval("AppliedDate")%> <asp:LinkButton ID="LinkButton1" runat="server" CssClass="invite-friend-btn invite-friend-btn1"
                                                Text="Delete" CommandName="jobDelete" CommandArgument='<%# Eval("Id")%>' />
                                </div>
                                <div class="total-views">
                                   <%-- <a href="#" class="accounts-link">
                                        <%# "Total Views : " + Convert.ToString(Eval("TotalViews"))%></a>--%>&nbsp;&nbsp;
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                    <%-- </div>--%>
                </div>
                <%--</div>--%>
            </div>
            <div class="content-inner-right">
                <div class="post-opportunity">
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
                <div class="google-add">
                        <asp:Image ID="bimage" runat="server" CssClass="advert1"  ImageUrl="images/basic-user-advert.gif"/>
                <asp:Image ID="pimage" runat="server" CssClass="advert1"   ImageUrl="images/premium-user-advert.gif" />
                </div>
            </div>
        </div>
        <!-- content inner ends -->
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
