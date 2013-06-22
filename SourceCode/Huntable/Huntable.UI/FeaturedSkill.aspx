<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FeaturedSkill.aspx.cs" Inherits="Huntable.UI.FeaturedSkill" %>

<%@ Register Src="~/UserControls/FeaturedRecruiters.ascx" TagPrefix="uc1" TagName="Featuredrecruiters" %>
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
       $(function () {

           var msie6 = $.browser == 'msie' && $.browser.version < 7;
           if (!msie6) {
               var top = $('#bx').offset().top;
               $(window).scroll(function (event) {
                   var y = $(this).scrollTop();
                   if (y >=323) { $('#bx').addClass('fixed'); }
                   else { $('#bx').removeClass('fixed'); }
               });
           }
       });</script>
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
        <div id="content-section">
            <div id="content-inner">
                <div class="green-desc">
                    Feature your company and logo here. Choose your target audience. Each selection
                    is only $<asp:Label ID="lblFeaturedCategoryPrice" runat="server" Text="10" Visible="true"></asp:Label>
                </div>
                <div class="content-inner-left">
                    <div class="accounts-profile accounts-profile1" style="margin-bottom: 20px;">
                        <a href="seemorerecruiter.aspx" class="accounts-link">Recruiters</a>&nbsp;&rsaquo;&rsaquo;&nbsp;<a>Get Featured</a>
                    </div>
                    <ul class="tab-list tab-list-search">
                        <%--<li><a href="featured-user.php" class="st_tab">User</a></li>--%>
                        <li><a href="featuredindustry.aspx" class="st_tab">Industry</a></li>
                        <li><a href="featuredskill.aspx" class="st_tab st_first_tab st_tab_active">Skill</a></li>
                        <li><a href="featuredinterest.aspx" class="st_tab">Interest</a></li>
                        <li><a href="featuredcountry.aspx" class="st_tab">Country</a></li>
                    </ul>
                    <div class="company-name-list" style="margin-top: 20px;">
                        <%--  <h3>
                            Alphabetical List</h3>
                        <div class="alphabet">
                            <a href="#">a</a><a href="#">b</a><a href="#">c</a><a href="#">d</a><a href="#">e</a><a
                                href="#">f</a><a href="#">g</a> <a href="#">i</a><a href="#">j</a><a href="#">k</a><a
                                    href="#">l</a><a href="#">m</a><a href="#">n</a><a href="#">0</a> <a href="#">p</a><a
                                        href="#">q</a><a href="#">r</a><a href="#">t</a><a href="#">u</a><a href="#">v</a><a
                                            href="#">w</a> <a href="#">x</a><a href="#">y</a><a href="#" style="border-right: 0px;">z</a>
                        </div>--%>
                        <div id="Skill">
                            <asp:DataList ID="dlSkill" runat="server" RepeatColumns="2" >
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkBtnFeatured" runat="server" Text='<%# Bind("Description")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFeatured" runat="server" Text='<%# Bind("Id")%>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
                <!-- content inner left ends -->
                <div class="content-inner-right" style="height:700px">
                    <div class="post-opportunity">
                        <asp:Button ID="Button1" class="button-green post-opportunity-btn" runat="server" Text="Post an Opportunity" OnClick ="BtnPostOpportunityClick"/>
                    </div>
                    <div class="post-opportunity">
                    <asp:Button ID="btnCheckOut" class="button-orange" Style="float: left; margin-left: 100px;" runat="server" Text="Continue" OnClick="BtnCheckOutClick" />
                    </div>
                    <p class="margin-top-visible">
                        &nbsp;</p>
                    <div class="total-reach">
                          Job Package:<asp:Label runat="server" ID="lblJobPackage" Text="" Visible="True"></asp:Label><br/>
                      Industries:<asp:Label ID="lblIndustries" runat="server" Text="" Visible="true"></asp:Label><br/>
                Countries:<asp:Label ID="lblCountries" runat="server" Text="" Visible="true"></asp:Label><br/>
                Interests:<asp:Label ID="lblInterests" runat="server" Text="" Visible="true"></asp:Label><br/>
                Skills:<asp:Label ID="lblSkills" runat="server" Text="" Visible="true"></asp:Label>
                        <div class="total-reach-inner">
                            Total Reach: <strong><label runat="server" id="lblTotalReach">0</label></strong>&nbsp;&nbsp; Total Cost: <strong>$<asp:Label ID="lblTotalCost"
                            runat="server" Text="" Visible="true"></asp:Label></strong>
                        </div>
                      <asp:Button ID="btnSkill" class="button-orange" style="float: left; margin-left: 100px;" runat="server" Text="Check Out" OnClick="BtnSkillClick" />
                    </div>
                    <p class="margin-top-visible">
                        &nbsp;</p>
                    <div id="bx"><uc1:Featuredrecruiters ID="featuredRec" runat="server" /></div>
                </div>
                <!-- content inner right ends -->
            </div>
            <!-- content inner ends -->
        </div>
        <!-- content section ends -->
        <!-- Range Slider Script Begins -->
        <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
            type="text/javascript"></script>
        <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
        <!-- Range Slider Script Ends -->
        
        <!-- Footer section ends -->
    </body>
    </html>
</asp:Content>
