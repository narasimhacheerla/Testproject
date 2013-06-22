<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CompaniesFollowers.aspx.cs" Inherits="Huntable.UI.CompaniesFollowers" %>

<%@ Register Src="~/UserControls/CompaniesYouMayBeIntrested.ascx" TagName="compIntrested"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Huntable - The Professional Network</title>
        <meta name="description" content="Huntable - The Professional Network" />
        <meta name="keywords" content="The Professional Network" />
        <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
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
            $(function () {

                var msie6 = $.browser == 'msie' && $.browser.version < 7;
                if (!msie6) {
                    var top = $('#bx9').offset().top;
                    $(window).scroll(function (event) {
                        var y = $(this).scrollTop();
                        if (y >= 14) { $('#bx9').addClass('fixed'); }
                        else { $('#bx9').removeClass('fixed'); }
                    });
                }
            });</script>
        <style type="text/css">
            div.st_horizontal ul.st_tabs li a
            {
                width: 110px;
                text-align: center;
                padding: 10px 10px;
            }
        </style>
    </head>
    <body>
        <!-- Header section ends -->
        <!--[if lte IE 6]>
<script type="text/javascript" >
//author: ?, I found this useful.
startList = function() {
if (document.all&&document.getElementById) {
navRoot = document.getElementById("nav");
for (i=0; i<navRoot.childNodes.length; i++) {
node = navRoot.childNodes[i];
if (node.nodeName=="LI") {
node.onmouseover=function() {
this.className+=" over";
  }
  node.onmouseout=function() {
  this.className=this.className.replace(" over", "");
   }
   }
  }
 }
}
window.onload=startList;

</script>
<![endif]-->
        <!-- main menu ends -->
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
                    <div class="notification">
                        <div class="profile-search profile-search-company">
                            <b>Search a Company:</b>
                            <asp:TextBox ID="txtUserSearchKeyword" runat="server" onblur="if (this.value == '') {this.value ='e.g: Name, Company, Skill, Job title';}"
                                onfocus="if (this.value =='e.g: By Company name, Keywords or Industry') {this.value ='';}"
                                value="e.g: By Company name, Keywords or Industry" class="textbox-search textbox-search-inner textbox-search-inner-company"></asp:TextBox>
                            <asp:Button class="button-orange button-orange-search" ID="btnUserSearch" runat="server"
                                Text="Search" OnClientClick="return ValidateText()" OnClick="BtnCompaniesSearchClick" />
                        </div>
                        <h3>
                            Alphabitical List</h3>
                        <div class="alphabet">
                            <asp:ListView ID="lvLetters" runat="server" OnItemDataBound="LettersItemDataBound">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkLetter" runat="server" style="font-size:11px;margin-left:-1px;" CommandName="Filter" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Letter")%>'
                                        OnCommand="LettersItemCommand">
                                              <%# DataBinder.Eval(Container, "DataItem.Letter") %>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <div id="st_horizontal" style="margin-top: 20px;" class="st_horizontal">
                            <div class="st_tabs_container st_sliding_active">
                                <div style="overflow: hidden; margin: 0px 0px 20px 0px;" class="st_slide_container">
                                    <ul style="margin-left: 0px;" class="st_tabs">
                                        <li><a href="Companieshome.aspx" class="st_tab">Industry</a></li>
                                        <li><a href="companiescountry.aspx" class="st_tab">Country</a></li>
                                        <li><a href="companiessize.aspx" class="st_tab ">Size</a></li>
                                        <li><a href="companiesjobs.aspx" class="st_tab">Jobs Available</a></li>
                                        <li><a href="companiesfollowers.aspx" class="st_tab st_first_tab st_tab_active">Followers</a></li>
                                    </ul>
                                </div>
                                <!-- /.st_slide_container -->
                            </div>
                            <!-- /.st_tabs_container -->
                            <div class="text">
                                <div class="alphabet-list">
                                    <div class="notification-left-top">
                                        <div class="notification-left notification-left-tab ">
                                            <div class="tab-form">
                                                <label class="checkbox-desc">
                                                    <strong>Followers</strong>
                                                </label>
                                                <asp:Repeater runat="server" ID="rspFollowers">
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" CssClass=" checkbox-tab" ID="chbfollowers" Text='<%#(Eval("NoofEmployess"))%>' /><br />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                        <asp:Button runat="server" ID="btnSearch" Text="Search" class="button-green button-green-jobpost"
                                            OnClick="BtnSearchClick" />
                                    </div>
                                    <div class="notification-right notification-right-tab notification-right-tab-new">
                                        <div class="notification-head">
                                            <asp:ListView ID="rspdata" runat="server" OnItemDataBound="ItemDataBound">
                                                <LayoutTemplate>
                                                    <table cellpadding="2" runat="server" id="tblEmployees" style="width: 460px">
                                                        <tr runat="server" id="itemPlaceholder">
                                                        </tr>
                                                    </table>
                                                    <div class="pagination1">
                                                        <div align="left">
                                                            <img src="images/google-banner-medium.jpg" width="300" height="100" alt="google-add" />
                                                        </div>
                                                        <br />
                                                        <asp:DataPager ID="dpgSearchResultsPager" runat="server" PagedControlID="rspdata"
                                                            PageSize="10">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
                                                                <asp:NumericPagerField />
                                                                <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" />
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <div style="border-bottom: 1px dotted #0A0A0A; width: 384px; height: 142px;">
                                                        <div class="company-left">
                                                           <a  href='<%#UrlGenerator(Eval("Id")) %>' id="compwebsite" runat="server">
                                                                <asp:Image ID="Image1" runat="server" alt="Feaured-logo" Width="128px" Height="74px"
                                                                    ImageUrl='<%#Eval("CompanyImagePath")%>' />
                                                                <%--<img alt="Feaured-logo" class="profile-pic01 profile-pic profile-pic2" src="images/featured-logo1.jpg" />--%>
                                                            </a>
                                                            <div class="company-left" >
                                                                <div id="Div1" class="floating" visible='<%#Eval("IsUserFollowingCompany")%>' runat="server" >
                                                                    <asp:Button ID="Button1" class="button-orange button-orange-cf" Text="Follow" runat="server"
                                                                        OnClick="FollowupClick" CommandArgument='<%#Eval("Id")%>' /></div>
                                                                        <div id="div2" runat="server" Visible='<%#Eval("IsUserNotFollowingCompany") %>'>
                                                                            <asp:Button ID="Button2" runat="server" CommandArgument='<%#Eval("Id")%>' OnClick="UnfollowCompanyClick" class="button-orange button-orange-cf" Text="Following"/>
                                                                        </div>
                                                            </div><br />
                                                           <a href='<%#"followers.aspx?UserId=" +Eval("UserId") %>' class="green-color hover-text">
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Followers(Eval("Id")) %>'></asp:Label>
                                                              <strong>Followers</strong></a>
                                                        </div>
                                                        <div class="company-right">
                                                           <a  href='<%#UrlGenerator(Eval("Id")) %>' id="A1" runat="server">
                                                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label></a><br />
                                                            <strong>
                                                                <%#Eval("MasterIndustry.Description")%></strong><br />
                                                            <%#Eval("MasterCountry.Description")%><br />
                                                            <%#Eval("MasterEmployee.NoofEmployess")%>employees<br />
                                                            <a href='<%#"companyjobs.aspx?Id=" +Eval("Id") %>' class="accounts-link accounts-link-offer">
                                                         <asp:Label ID="Label3" runat="server" Text='<%#Jobs(Eval("Id")) %>'></asp:Label> Job Offers</a>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
              
                <div class="content-inner-right" id="bx9">
                    <div class="blue-box-company">
                        <h3>
                            Promote your business</h3>
                        Create your business profile and promote your company now.
                        <div class="button-green-company" align="center">
                            <a href="Company-registration.aspx" class="button-yellow button-green-company1">Add
                                a Company</a>
                        </div>
                    </div>
                    <p class="margin-top-visible">
                        &nbsp;</p>
                    <uc1:compIntrested runat="server" />
                    <p class="margin-top-visible">
                        &nbsp;</p>
                </div>
            </div>
            <!-- content inner ends -->
        </div>
        <!-- content section ends -->
        <!-- Range Slider Script Begins -->
        <!--<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>-->
        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
            type="text/javascript"></script>
        <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
        <script type="text/javascript" src="js/flipcounter.js"></script>
        <!-- Range Slider Script Ends -->
        <!-- Footer section ends -->
    </body>
    </html>
    
</asp:Content>
