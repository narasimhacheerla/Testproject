<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CompaniesSearch.aspx.cs" Inherits="Huntable.UI.CompaniesSearch" %>
    <%@ Register Src="~/UserControls/CompaniesYouMayBeIntrested.ascx" TagName="comp"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div  class="content-inner-left" style="margin-top:90px">
 <div class="profile-search profile-search-company" style="margin-left:176px;margin-top:-63px;">
                            <b>Search a Company:</b>
                            <asp:TextBox ID="txtUserSearchKeyword" runat="server" onblur="if (this.value == '') {this.value ='e.g: Name, Company, Skill, Job title';}"
                                onfocus="if (this.value =='e.g: By Company name, Keywords or Industry') {this.value ='';}"
                                value="e.g: By Company name, Keywords or Industry" class="textbox-search textbox-search-inner textbox-search-inner-company"></asp:TextBox>
                            <asp:Button class="button-orange button-orange-search" ID="btnUserSearch" runat="server"
                                Text="Search" OnClientClick="return ValidateText()" OnClick="BtnCompaniesSearchClick" />
                        </div>
                         <h3 style="margin-left:177px;">
                            Alphabitical List</h3>
                        <div class="alphabet" style="margin-left:176px;">
                            <asp:ListView ID="lvLetters" runat="server" OnItemDataBound="LettersItemDataBound">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkLetter" runat="server" Style="font-size: 11px; margin-left: -1px;"
                                        CommandName="Filter" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Letter")%>'
                                        OnCommand="LettersItemCommand">
                                              <%# DataBinder.Eval(Container, "DataItem.Letter") %>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <Asp:HiddenField runat="server" ID="hfkeyword" Visible="False" Value=""/>
    <div class = "notification-company" 
        style="border-left: thin inset #FFFFFF; border-right: thin outset #FFFFFF; border-top: thin inset #FFFFFF; border-bottom: thin outset #FFFFFF; font-family: Arial; font-size: small; width :603px; margin-left:179px; margin-top :15px; padding-left: 50px; padding-top: 15px; height: 1800px;"> 
 

   <%-- 
   <div   
       
        style="border-width: thin; border-color: #FFFFFF; font-family: Arial; font-size: small; border-style:inset outset outset inset;  width :450px; margin-left:250px; margin-top :15px; padding-left: 50px; padding-top: 15px;">     --%>
       
    <asp:ListView ID="rspdata" runat="server">       
     
        <LayoutTemplate>
            <table cellpadding="2"  runat="server" id="tblEmployees" style="width: 460px">
                <tr runat="server" id="itemPlaceholder">
                </tr>
            </table>
           <%-- <div class="pagination1">--%>
                <div   align="left">
                    <img src="images/google-banner-medium.jpg" width="300" height="100" alt="google-add" />
                </div>
                <br />
                <div class="pagination1">
                <asp:DataPager ID="dpgSearchResultsPager" runat="server" PagedControlID="rspdata"
                    PageSize="10">
                    <Fields>
                        <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
                        <asp:NumericPagerField />
                        <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" />
                    </Fields>
                </asp:DataPager>
                </div>
            <%--</div>--%>
        </LayoutTemplate>
        <ItemTemplate>
            <div style="border-bottom: 1px dotted #0A0A0A; width: 570px; height: 142px;">
                <div class="company-left">
                     <a  href='<%#UrlGenerator(Eval("Id")) %>' id="compwebsite" runat="server">
                        <asp:Image ID="Image1" runat="server" alt="Feaured-logo" Width="128px" Height="74px"
                            ImageUrl='<%#Eval("CompanyImagePath")%>' />
                        <%--<img alt="Feaured-logo" class="profile-pic01 profile-pic profile-pic2" src="images/featured-logo1.jpg" />--%>
                    </a>
                     <div class="company-left">
                            <div id="Div1" class="floating" visible='<%#Eval("IsUserFollowingCompany")%>' runat="server">
                           <asp:Button ID="Button1" class="button-orange button-orange-cf" Text="Follow" runat="server"
                                   OnClick="FollowupClick" CommandArgument='<%#Eval("Id")%>' /></div>
                            <div id="div2" runat="server" Visible='<%#Eval("IsUserNotFollowingCompany") %>'>
                             <asp:Button ID="Button2" runat="server" CommandArgument='<%#Eval("Id")%>' OnClick="UnfollowCompanyClick" class="button-orange button-orange-cf" Text="Following"/>
                       </div>
                   </div><br />
                    <%-- <input type="button" class="button-orange button-orange-cf" value="Follow" />--%><br />
                    <a href="#" class="green-color hover-text">
                        <asp:Label ID="Label1" runat="server" Text='<%#Followers(Eval("Id")) %>' />
                       <strong>Followers</strong></a>
                </div>
                <div class="company-right">
                   <a  href='<%#UrlGenerator(Eval("Id")) %>' id="A1" runat="server" class="accounts-link">
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label></a><br />
                    <strong>
                        <%#Eval("MasterIndustry.Description")%></strong><br />
                    <%#Eval("MasterCountry.Description")%><br />
                    <%#Eval("MasterEmployee.NoofEmployess")%>employees<br />
                    <a href="#" class="accounts-link accounts-link-offer">
                     <asp:Label ID="Label3" runat="server" Text='<%#Jobs(Eval("Id")) %>'></asp:Label> Job Offers</a>
                </div>
            </div>
            <br />
            </ItemTemplate>
            
       </asp:ListView>
   
    </div>
    </div>
     <div class="content-inner-right" id="bx9" style="margin-left:858px;margin-top:24px;">
                    <div class="blue-box-company1">
                        <h3>
                            Promote your business</h3>
                        Create your business profile and promote your company now.
                        <div class="button-green-company" align="center">
                            <a href="Company-Registration.aspx" class="button-yellow button-green-company2">Add
                                a Company</a>
                        </div>
                    </div>
                    <p class="margin-top-visible">
                        &nbsp;</p>
                    <div class="box-right">
                        <uc2:comp ID="Comp1" runat="server" />
                    </div>
                    <p class="margin-top-visible">
                        &nbsp;</p>
                </div>
             
</asp:Content>
