<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CompanyJobs.aspx.cs" Inherits="Huntable.UI.CompanyJobs" %>

<%@ Register Src="UserControls/JobControl.ascx" TagName="jobcontrol" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/Messagepopup.ascx" TagName="mesgpopup" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Followers.ascx" TagName="followers" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/InfosysEmployees.ascx" TagName="infosysemp" TagPrefix="uc4" %>
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
                    var top = $('#bx40').offset().top;
                    $(window).scroll(function (event) {
                        var y = $(this).scrollTop();
                        if (y >= 330) { $('#bx40').addClass('fixed'); }
                        else { $('#bx40').removeClass('fixed'); }
                    });
                }
            });</script>
        <script type="text/javascript">
            $(function () {

                var msie6 = $.browser == 'msie' && $.browser.version < 7;
                if (!msie6) {
                    var top = $('#bx16').offset().top;
                    $(window).scroll(function (event) {
                        var y = $(this).scrollTop();
                        if (y >= 238) { $('#bx16').addClass('fixed'); }
                        else { $('#bx16').removeClass('fixed'); }
                    });
                }
            });</script>
        <script type="text/javascript">
            $(function () {

                var msie6 = $.browser == 'msie' && $.browser.version < 7;
                if (!msie6) {
                    var top = $('#bx10').offset().top;
                    $(window).scroll(function (event) {
                        var y = $(this).scrollTop();
                        if (y >= 50) { $('#bx10').addClass('fixed'); }
                        else { $('#bx10').removeClass('fixed'); }
                    });
                }
            });</script>
              <script type="text/javascript">
                  function overlay(id) {
                      el = document.getElementById('ovrly');
                      $('#ovrly').show();

                      $('#<%= pbl.ClientID %>').text(id);

                  }
                  $(document).ready(function () {

                      $('#ximg').click(function () {
                          $('#ovrly').hide();
                          window.location.reload();
                          return false;
                      });
                  });
    </script>

   <!--alert message-->
   <script type="text/javascript">
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
       function rowAction01() {
           if (document.getElementById('<%=hdnUserId.ClientID %>').value != "") {

           }
           else {
               $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
               $('#dialog').dialog('open');
               return false;

           }
       }
</script>
<script type="text/javascript" src="../js1/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js1/jquery-ui-1.8.23.custom.min.js"></script>
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
    <!--alert msg end-->

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

        <div id="content-section" >
    <asp:HiddenField ID="hdnUserId" runat="server" />
 <div id="dialog" title="Confirm Message">
    <asp:Label runat="server" Text="You are not logged In.Please login first." ID="lblConfirmMessage"></asp:Label>
    </div>
            <div id="content-inner">
                <div class="accounts-profile2 ">
                    <div class="top-breadcrumb">
                        <div class="accounts-profile2-left">
                            <a class="accounts-link" href="companieshome.aspx">Company</a>&nbsp;››&nbsp;<a class="accounts-link"
                                id="a_comP_view" runat="server" href="#"><asp:Label runat="server" ID="lbl_compName">
      </asp:Label></a>&nbsp;››&nbsp;<strong>Careers</strong></div>
                        <br />
                    </div>
                </div>
                <%--<div id="bx10">--%>
                    <ul class="overview-tab" style="margin-top:-10px;">
                        <li><a id="overview" runat="server" style="font-size: 12px">Overview</a></li>
                        <li><a id="activity" runat="server" style="font-size: 12px">Activity</a></li>
                        <li><a id="productsandservices" runat="server" style="font-size: 12px">Products &amp;
                            Services</a></li>
                        <li><a id="careers" runat="server" class="selected-tab" style="font-size: 12px">Careers</a></li>
                        <li><a id="busunessblog" runat="server" style="font-size: 12px">Business Blog</a></li>
                        <li><a id="article" runat="server" style="font-size: 12px">Article</a></li>
                         <uc1:mesgpopup ID="mesgpopup" runat="server" />   
                    </ul>
                <%--</div>--%>
                <div id="dv" runat="server" class="content-inner-left">
                    <div class="notification">
                        <div class="all-feeds-list" style="margin-top: 22px;">
                            <div class="general-setting-tab1 general-setting-tab1-overview">
                                <a class="setting-active" id="cj" runat="server" href="#" style="background: -webkit-gradient(linear,left top,left bottom,color-stop(0%,#FBFBFB), color-stop(100%,#CECECE));
                                    color: Black;">Company jobs in Huntable</a> <a runat="server" id="a_Comp_carrer">Company
                                        Careers page</a>
                                      
                            </div>
                        </div>
                        <div class="notification-left-main">
                            <div class="job-posted-logo">
                                <a href="#">
                                    <asp:Image runat="server" ID="imCompanyImage" Width="204px" Height="123px" />
                                </a>
                            </div>
                            <div id="bx40">
                                <uc:jobcontrol ID="Jobcontrol1" runat="server" />
                            </div>
                        </div>
                        <div class="notification-right notification-right1" style="margin-left: 19px; width: 329px;">
                            <div class="r-notification-main">
                                <div class="r-notification">
                                    <div class="r-notification-top">
                                        <div class="r-notification-mid" style="width: 300px;">
                                          <div id="njbs" runat ="server" visible="false" style="border:1px solid grey;margin-top: 60px;
height: 60px;border-radius: 5px;">
  <h2 style="font-family:Georgia;text-align: center;
margin-top: 20px;">There are no active jobs</h2>  
   </div>
                                            <asp:ListView ID="lvJobs" runat="server" OnItemDataBound="lvjobs_apply" OnItemCommand="LvJobsItemCommand" ClientIDMode="Static">
                                                <LayoutTemplate>
                                                    <table cellpadding="2" runat="server" id="tblEmployees" style="width: 460px">
                                                        <tr runat="server" id="itemPlaceholder">
                                                        </tr>
                                                    </table>
                                                    <div class="pagination">
                                                        <div align="center" style="margin-left: 30px">
                                                            <img src="images/google-banner-medium.jpg" width="300" height="100" alt="google-add" />
                                                        </div>
                                                        <br />
                                                        <asp:DataPager ID="dpgSearchResultsPager" runat="server" PagedControlID="lvJobs"
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
                                                    <div style="width:550px;height:auto;border-bottom: 1px dotted #ccc;">
                                                    <div>
                                                        <a id="A2" href='<%#UrlGenerator(Eval("Id")) %>' class="grey-link"
                                                            runat="server">
                                                            <img src='<%# Eval("ProfileImagePath") %>' class="r-notification-left" width="124"
                                                                height="70" alt="Job" /></a> 
                                                                </div>
                                                                <div style="width:400px"><div align="center" style="width: 255px;margin-left: 85px;"><a href="<%#UrlGenerator(Eval("Id")) %>"
                                                                    class="grey-link">
                                                                    <%# Eval("Title")%></a></div><br />
                                                        
                                                        <span class="blue-color" style="margin-left: 65px;">Job Type :</span>
                                                        <%# Eval("MasterJobType.Description")%>
                                                        <br />
                                                        <span class="blue-color" style="margin-left: 27px;">Experience Req :</span>
                                                        <%# Eval("MinExperience")%>
                                                        Minimum<br />
                                                        <span class="blue-color" style="margin-left: 150px;">Industry :</span>
                                                        <%# Eval("MasterIndustry.Description")%>
                                                        <br />
                                                        <span class="blue-color" style="margin-left: 160px;">Salary :</span>
                                                        <%# Eval("Salary")%><%# Eval("MasterCurrencyType.Description")%><br />
                                                        <span class="blue-color" style="margin-left: 170px;">Skill :</span>
                                                        <%# Eval("MasterSkill.Description")%><br />
                                                        <span class="blue-color" style="margin-left: 109px;">JobPostedDate : </span>
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("CreatedDateTime", "{0:MM/dd/yyyy}")%>'></asp:Label><br />
                                                        </div>
                                                        <div style="margin-top: -300px;margin-left: 330px;">
                                                      <div id="Div1" class="notification-links notification-links1 notification-links-home" 
                                                            runat="server" visible='<%# Eval("IsUserAlreadyToThisJob") %>' style="width:100px;margin-top:-76px;">
                                                           <%-- <asp:LinkButton ID="LinkButton1" runat="server" CssClass="invite-friend-btn invite-friend-btn1"
                                                                Text="Apply now + " OnClientClick="return rowAction01();" CommandName="Apply" CommandArgument='<%# Eval("Id")%>' />--%>
                                                                    <a href="#?w=350" class="invite-friend-btn invite-friend-btn1 poplight" rel="popup12">
                                                            Apply now + </a>
                                                        </div>
                                                        <div id="div2" runat="server" visible='<%# Eval("IsUserNotAppliedToThisJob") %>' style="width:100px;margin-top:-76px" >
                                                            <asp:Label runat="server" Width="41" Height="16" ID="Following" CssClass="invite-friend-btn invite-friend-btn1"
                                                                Text="Applied" /></div>
                                                                </div>
                                                       <br />
                                                        <div style="width:450px;margin-top:300px;" >
                                                        <%--<div><asp:Button ID="btn1" runat="server" Text="Apply now +" OnClick="Applynowclicked"/></div>--%>
                                                          <asp:Label ID="Label2" CssClass="ShortDesc" Text='<%# Eval("JobDescription").ToString().Substring(0,Math.Min(300,Eval("JobDescription").ToString().Length)) %>'
                                                            runat="server"></asp:Label>...<a class="orange-link" href="<%#UrlGenerator(Eval("Id")) %>" >more</a>
                                                      <%--  <asp:LinkButton runat="server" ID="btn_link" Text="afafafae"></asp:LinkButton>--%>
                                                        </div>
                                                    </div>
                                                       <div id="popup12" class="popup_block">
                                                    <div class="apply-job ">
                                                        <strong>Write some covering message to the job poster.<br />
                                                            Make sure your profile is Update before you apply for this job</strong><br />
                                                        <br />
                                                        <asp:TextBox  runat="server"  TextMode="MultiLine" Rows="9" ID = "txtapply" style="width: 350px;color: gray" />
                                                       <asp:Button ID="Button2" runat="server" Text="Apply Job"   CommandName = "Apply" CommandArgument='<%# Eval("Id")%>'
                                                            class="button-orange" />
                                                    </div>
                                                </div>
                                                    <br />
                                                    <br />
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

        <!-- content inner ends --> 
        </div>
       
        <div class="box-right box-right-companyl" style="border:#E6EEEE; margin-top: 59px; margin-left: 106px;">
         <div class="view-company-info" style="width:198px;">
             <asp:LinkButton  style="margin-right:10px;" class="button-green floatleft"  ID="lnkJobs" runat="server" OnClientClick="return rowAction01();"  Text="Post Jobs Availability" OnClick="LnkJobsClick"></asp:LinkButton>
       
        <asp:Button ID="btn_follow"  Visible="false" CssClass="button-orange view-company-info-follow" style="padding:5px 7px; margin-left: 135px; margin-top: -28px;"
                                    Text="Follow" runat="server" OnClick="Follow" OnClientClick="return rowAction01();"  />
                                     <asp:Button ID="btn_following" Visible="false" CssClass="button-orange view-company-info-follow" style="padding:5px 7px; margin-left: 135px; margin-top: -28px;"
                                    Text="Following" runat="server" OnClick="Following" OnClientClick="return rowAction01();"  /><br />
 </div><br /><br /><br />
        <a id="a_compInfo" runat="server" style="text-decoration: none; color: #008CA1; font-weight: bold; margin-left: 19px;">View company Info</a>
        <div>
        <uc3:followers runat="server" /></div>
        <br />
             <div>
        <uc4:infosysemp runat="server" /></div>
        </div>

        </div>
        </div>
         <!-- content section ends --> <!-- Range Slider
        Script Begins --> <!--<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>-->
        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script> <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
        <script type="text/javascript" src="js/flipcounter.js"></script> <!-- Range Slider
        Script Ends --> <!-- Footer section ends -->
    </body>
    </html>
    </div> </div> </div>
        </div>
</asp:Content>
