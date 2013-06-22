<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CustomizeJobsSalary.aspx.cs" Inherits="Huntable.UI.CustomizeFeedsSalary" %>

<%@ Register Src="~/UserControls/FeaturedRecruiters.ascx" TagName="uclfeatured" TagPrefix="uc6" %>
<%@ Register Src="~/UserControls/SeeYourFriendsInvitations.ascx" TagName="uslseemore"
    TagPrefix="uc7" %>
<%@ Register Src="~/UserControls/ImportContacts.ascx" TagName="uclimport" TagPrefix="uc8" %>
<%@ Register src="UserControls/PeopleYouMayKnow.ascx" tagName="pplUMayKnow" tagPrefix="uc12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <link href="css1/jquery-ui-1.8.23.custom.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
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
      function rowAction2(calling) {
          $('#confirmation').dialog({
              autoOpen: true,
              modal: true,
              width: 600,              
              buttons: {
                  "Save": function () {
                      if (calling == 'test') {<%= Page.ClientScript.GetPostBackEventReference(this.btnSave, string.Empty) %>;
                      }
                      $(this).dialog("close");
                  },
                  "Cancel": function() {
                  if (calling == 'test') {<%= Page.ClientScript.GetPostBackEventReference(this.btnActualValues, string.Empty) %>;
                      }
                      $(this).dialog("close");
                  }
              }
          });
          return false;
      }   
         function callnextbutton(pagename) {
          $('#<%= HiddenField1.ClientID %>').val(pagename);
          <%= Page.ClientScript.GetPostBackEventReference(this.btnNext, string.Empty) %>;
             
        
      }
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
                    <li><a href="Customize-User.aspx" style="border-right: 1px solid #fff;">Customize Feeds</a></li>
                    <li><a href="CustomizeJobsSalary.aspx" class="select">Customize Jobs</a></li>
                </ul>
                <div id="st_horizontal" class="st_horizontal">
                    <div class="st_tabs_container st_sliding_active">
                        <div style="overflow: visible;" class="st_slide_container">
                            <ul style="margin-left: 0px;" class="st_tabs">
                             <li><a  href="javascript:callnextbutton('customizejobssalary.aspx');" class="st_tab1 st_first_tab st_tab_active">Salary</a></li>
                                <li><a   href="javascript:callnextbutton('customizejobsindustry.aspx');" class="st_tab1 ">Industry</a></li>
                                <li><a   href="javascript:callnextbutton('customizejobsskill.aspx');" class="st_tab1 ">Skill</a></li>
                                <li><a   href="javascript:callnextbutton('customizejobsjobtype.aspx');" class="st_tab1">Job type</a></li>
                               <%-- <li><a href="CustomizeJobsCompany.aspx" class="st_tab">Company</a></li>
                                <li><a href="CustomizeJobsInterest.aspx" class="st_tab">Interest</a></li>--%>
                                <li><a  href="javascript:callnextbutton('customizejobscountry.aspx');" class="st_tab1">Country</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="company-name-list" style="margin-top: 20px;">
                    <span>Minimum Salary:</span>&nbsp;
                    <asp:DropDownList ID="ddlMin" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Value="-1" Text="Please Select" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    <span>Maximum Salary:</span>&nbsp;
                    <asp:DropDownList ID="ddlMax" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Value="-1" Selected="True" Text="Please Select"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    <span>Currency:</span>
                    <asp:DropDownList ID="ddlCurrency" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Value="-1" Selected="True" Text="Please Select" ></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <br />
                <asp:Button ID="btnSave" CssClass="button-orange" Style="float: right;" Text="Save"
                    runat="server" OnClick="BtnSaveClick" /><br />
                  <asp:HiddenField runat="server" ID="hdnsave" Value="tt"/>
                   <asp:HiddenField runat="server" ID="HiddenField1" />
                   <%-- <a href="CustomizeJobsIndustry.aspx" style="margin-top:40px ;margin-right:-45px" class="button-green floatright">Next</a>--%>
                   <asp:Button runat="server" ID="btnActualValues" Visible="False" OnClick="BtnActualValuesClick"/>
                    <asp:Button ID="btnNext" runat="server" style="margin-top:40px ;margin-right:-45px" class="button-green floatright" Text="Next" OnClick="BtnNextClick" />                   
                    <div id="confirmation" style="display:none;">
                <asp:Label runat="server" Text="Would you like to save your preferences?" ></asp:Label>
                </div> 
                <div>
            </div> 
            </div>
            <!-- content inner left ends -->
            <div class="content-inner-right">
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
                    <div class="box-right">
                        <%--<uc8:uclimport ID="uc8" runat="server" />
                        <p class="margin-top-visible">
                            &nbsp;</p>--%>
                        <div class="box-right">
                            <uc7:uslseemore ID="uc7" runat="server" />
                        </div>
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
        </div>
    </div>
</asp:Content>
