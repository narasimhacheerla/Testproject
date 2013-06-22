<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CustomizeFeedsCountry.aspx.cs" Inherits="Huntable.UI.CustomizeFeedsCountry" %>

<%@ Register Src="~/UserControls/FeaturedRecruiters.ascx" TagName="uclfeatured" TagPrefix="uc6" %>
<%@ Register Src="~/UserControls/ImportContacts.ascx" TagName="uclimport" TagPrefix="uc8" %>
<%@ Register Src="~/UserControls/atozsearch.ascx" TagName="atoz" TagPrefix="uc" %>
<%@ Register src="UserControls/PeopleYouMayKnow.ascx" tagName="pplUMayKnow" tagPrefix="uc12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                    <li><a href="Customize-User.aspx" style="border-right: 1px solid #fff;" class="select">Customize Feeds</a></li>
                    <li><a href="CustomizeJobssalary.aspx" runat="server" >Customize Jobs</a></li>
                </ul>
                <ul class="tab-list tab-list-search">
                    <li><a  href="javascript:callnextbutton('customize-user.aspx');" class="st_tab">User</a></li>
                    <li><a  href="javascript:callnextbutton('CustomizeFeedsIndustry.aspx');" class="st_tab ">Industry</a></li>
                    <li><a  href="javascript:callnextbutton('CustomizeFeedsSkill.aspx');" class="st_tab">Skill</a></li>
                    <li><a  href="javascript:callnextbutton('CustomizeFeedsInterest.aspx');" class="st_tab">Interest</a></li>
                    <li><a  href="javascript:callnextbutton('CustomizeFeedscountry.aspx');" class="st_tab st_first_tab st_tab_active">Country</a></li>
                </ul>
                

                <uc:atoz runat="server" ID="ucAtoZFeedsCountries" SearchTitle = "Search by Country:"/>
                <p class="margin-top-visible">&nbsp;</p>                 
                </div>
           
            <!-- content inner left ends -->
            <div class="content-inner-right">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                         <uc12:pplUMayKnow ID="ucPplUMayKnow1" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
               
                    <p class="margin-top-visible">
                        &nbsp;</p>
                <uc6:uclfeatured ID="uc6" runat="server" />
                <p class="margin-top-visible">
                    &nbsp;</p>
                <%--<div class="box-right">
                    <uc8:uclimport ID="uc8" runat="server" />
                </div>--%>
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
</asp:Content>
