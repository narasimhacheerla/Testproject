<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProfileStatusAtGlance.aspx.cs" Inherits="Huntable.UI.ProfileStatusAtGlance" %>

<%@ Register Src="~/UserControls/cvStatistics.ascx" TagPrefix="CV" TagName="Statistics" %>
<%@ Register Src="~/UserControls/YourAccountAtGlance.ascx" TagPrefix="Accts" TagName="Glance" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
     <script type="text/javascript">
         $(function () {

             var msie6 = $.browser == 'msie' && $.browser.version < 7;
             if (!msie6) {
                 var top = $('#bx32').offset().top;
                 $(window).scroll(function (event) {
                     var y = $(this).scrollTop();
                     if (y >= 370) { $('#bx32').addClass('fixed'); }
                     else { $('#bx32').removeClass('fixed'); }
                 });
             }
         });</script>
    <style type="text/css">
        .style15
        {
            width: 276px;
            float: left;
        }
        .style16
        {
            width: 200px;
        }
        .style17
        {
            width: 291px;
        }
        .style19
        {
            width: 270px;
        }
        .style20
        {
            color: #FFFFFF;
        }
    </style>
    <script type="text/javascript" src="Scripts/JQPlot/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/JQPlot/jquery.jqplot.min.js"></script>
    <script type="text/javascript" src="Scripts/JQPlot/plugins/jqplot.cursor.min.js"></script>
    <script type="text/javascript" src="Scripts/JQPlot/plugins/jqplot.dateAxisRenderer.min.js"></script>
    <script type="text/javascript" src="Scripts/JQPlot/plugins/jqplot.logAxisRenderer.min.js"></script>
    <script type="text/javascript" src="Scripts/JQPlot/plugins/jqplot.canvasTextRenderer.min.js"></script>
    <script type="text/javascript" src="Scripts/JQPlot/plugins/jqplot.canvasAxisTickRenderer.min.js"></script>
    <link rel="stylesheet" type="text/css" href="Scripts/JQPlot/jquery.jqplot.min.js" />
    <asp:Literal ID="scriptLiteral" runat="server"></asp:Literal>
    <script type="text/javascript">
        $(document).ready(function () {
            if (histData != null) {
                // goog = [["6/22/2009", 425.32], ["6/8/2009", 424.84], ["5/26/2009", 417.23], ["5/11/2009", 390], ["4/27/2009", 393.69], ["4/13/2009", 392.24], ["3/30/2009", 369.78], ["3/16/2009", 330.16], ["3/2/2009", 308.57], ["2/17/2009", 346.45], ["2/2/2009", 371.28], ["1/20/2009", 324.7], ["1/5/2009", 315.07], ["12/22/2008", 300.36], ["12/8/2008", 315.76], ["11/24/2008", 292.96], ["11/10/2008", 310.02], ["10/27/2008", 359.36], ["10/13/2008", 372.54], ["9/29/2008", 386.91], ["9/15/2008", 449.15], ["9/2/2008", 444.25], ["8/25/2008", 463.29], ["8/11/2008", 510.15], ["7/28/2008", 467.86], ["7/14/2008", 481.32], ["6/30/2008", 537], ["6/16/2008", 546.43], ["6/2/2008", 567], ["5/19/2008", 544.62], ["5/5/2008", 573.2], ["4/21/2008", 544.06], ["4/7/2008", 457.45], ["3/24/2008", 438.08], ["3/10/2008", 437.92], ["2/25/2008", 471.18], ["2/11/2008", 529.64], ["1/28/2008", 515.9], ["1/14/2008", 600.25], ["12/31/2007", 657], ["12/17/2007", 696.69], ["12/3/2007", 714.87], ["11/19/2007", 676.7], ["11/5/2007", 663.97], ["10/22/2007", 674.6], ["10/8/2007", 637.39], ["9/24/2007", 567.27], ["9/10/2007", 528.75], ["8/27/2007", 515.25]];
                //alert(timer);
                goog = [["6/22/2009", 0], ["6/8/2009", 0]];
                var plot1 = $.jqplot('chart1', [histData], {
                    title: '',
                    series: [{ label: '', neighborThreshold: -1}],
                    axes: { xaxis: { renderer: $.jqplot.DateAxisRenderer, tickRenderer: $.jqplot.CanvasAxisTickRenderer,
                        tickOptions: { angle: -30 }
                    },
                        yaxis: { renderer: $.jqplot.LogAxisRenderer, tickOptions: { prefix: ''} }
                    },
                    cursor: { show: true, zoom: true }
                });
            }
        });
    </script>
    <!-- Top news tab function Script begins -->
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="js/jquery.scrollTo.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            //Get the height of the first item
            $('#mask').css({ 'height': $('#panel-1').height() });

            //Calculate the total width - sum of all sub-panels width
            //Width is generated according to the width of #mask * total of sub-panels
            $('#panel').width(parseInt($('#mask').width() * $('#panel div').length));

            //Set the sub-panel width according to the #mask width (width of #mask and sub-panel must be same)
            $('#panel div').width($('#mask').width());

            //Get all the links with rel as panel
            $('a[rel=panel]').click(function () {

                //Get the height of the sub-panel
                var panelheight = $($(this).attr('href')).height();

                //Set class for the selected item
                $('a[rel=panel]').removeClass('selected');
                $(this).addClass('selected');

                //Resize the height
                $('#mask').animate({ 'height': panelheight }, { queue: false, duration: 500 });

                //Scroll to the correct panel, the panel id is grabbed from the href attribute of the anchor
                $('#mask').scrollTo($(this).attr('href'), 800);

                //Discard the link default behavior
                return false;
            });
        });
    </script>
    <!-- Top news tab function Script ends -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="contacts-head">
                    <h3 style="float: left;">
                        Your Huntable Profile Stats</h3>
                    <div class="date">
                        <strong>
                            <asp:Label ID="lblRemDay"  runat="server"></asp:Label>
                            </strong><asp:Label ID="lblJoinedDate" runat="server"></asp:Label>
                        -<asp:Label ID="lblToDate" runat="server"></asp:Label></div>
                </div>
                <div class="profile-box">
                    <a id="A1" href="~/PictureUpload.aspx" runat="server">
                        <asp:Image runat="server" ID="imgProfile" class="profile-pic" Width="74" Height="81"
                            alt="Profile-pic" /></a> <a href="ViewUserProfile.aspx">
                                <asp:Label class="profile-name" ID="lblUserName" runat="server" /></a>
                    <div class="ashab-box" id="divYes" runat="server">
                        <asp:Label ID="lblposition1" runat="server"></asp:Label> &nbsp at &nbsp<asp:Label ID="lblcompany1"
                            runat="server"></asp:Label><br />
                        <label>
                            Is this your current Position?</label>
                        <div style="margin-top: 10px;">
                            <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="button-green button-green-stats"
                                OnClick="BtnYesClick" />
                            <a href="EditProfilePage.aspx" class="button-ash button-ash-stats">
                                <img src="images/icon-close-stats.png" width="17" height="16" alt="Yes" />No</a>
                        </div>
                    </div>
                    <div class="blueab-box-stats">
                        Updating your profile lets you keep ahead of your competition. Your profile will
                        be HUNTABLE
                    </div>
                </div>
                <div class="profile-graph">
                    
                
                <div id="chart1" style="width: 653px; height: 200px;">
                <asp:PlaceHolder ID="phGraph" runat="server"></asp:PlaceHolder>
                </div></div>
                <div class="accounts-profile accounts-profile-stats">
                    <strong>Who's Viewed Your Profile </strong>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    
                    <ContentTemplate>
                        <asp:HiddenField ID="hfiled" Value="" runat="server" />
                        <div class="top-news">
                            <div class="top-news-inner">
                                <div id="scroller-header">
                                    <asp:LinkButton ID="btnToday" Text="TODAY" runat="server" OnClick="BtnTodayClick" />
                                    <asp:LinkButton ID="btnLastWeek" Text="LAST WEEK" runat="server" OnClick="BtnLastWeekClick" />
                                    <asp:LinkButton ID="btnLatMonth" Text="LAST MONTH" runat="server" />
                                    <asp:LinkButton ID="btnAll" Text="OVERALL" runat="server" OnClick="BtnAllClick"></asp:LinkButton>
                                </div>
                                <div id="scroller-body">
                                    <div id="mask" style="padding-top: 0px;">
                                        <div id="panel">
                                            <div id="panel-1">
                                                <p class="strip-bottom">
                                                    &nbsp;</p>
                                                <asp:Repeater ID="rpProfileVisitedHistory" runat="server" OnItemDataBound="Itembound">
                                                    <SeparatorTemplate>
                                                        <hr />
                                                    </SeparatorTemplate>
                                                    <ItemTemplate>
                                                        <div class="today-news">
                                                            <div style="float: left; width: 60px; margin-left: 0px;">
                                                                <a id="A1" runat="server" class="accounts-link">
                                                                    <asp:Image ID="imgPhoto" runat="server" Width="50px" ImageUrl='<% #Eval("ImagePath") %>' /></a>
                                                            </div>
                                                            <div style="float: left; margin-left: 70px;">
                                                                <a id="A2" runat="server" class="accounts-link">
                                                                    <asp:Label ID="Label1" runat="server" Text='<% #Eval("Name") %>' /></a><br />
                                                                <asp:Label ID="Label2" runat="server" Text='<% #Eval("Role") %>' /><br />
                                                                <asp:Label ID="Label3" runat="server" Text='<% #Eval("Title") %>' /><br />
                                                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="Button1" OnClick="BtnJobShowCLick" Style="margin-left: 180px;" class="show-more"
                            Text="Show more" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                 <asp:UpdateProgress ID="updateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                                    <ProgressTemplate>
                                        <img src="images/loadingBar.gif" style="height: 50px" alt="Update in progress" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
            </div>
            <div class="content-inner-right" style="height:695px">
                <CV:Statistics ID="userStats" runat="server"></CV:Statistics>
                <p class="margin-top-visible">
                    &nbsp;</p>
               <div id="bx32"> <Accts:Glance ID="UserAccts" runat="server" />
                <p class="margin-top-visible">
                    &nbsp;</p>
                <%--<div class="box-right box-right-bg">
                    <div class="head-ash">
                        <h3>
                            Viewers by type</h3>
                    </div>
                    <div class="map-overlay-viewers">
                        <img src="images/viewer-by-type-img.jpg" style="float: left;" width="106" height="85"
                            alt="Viewer-by-type" />
                        <ul class="viewers-list">
                            <li><a href="#" class="accounts-link">HR Manager</a></li>
                            <li><a href="#" class="accounts-link">Recruitment Consultants</a></li>
                            <li><a href="#" class="accounts-link">Head Chef</a></li>
                            <li><a href="#" class="accounts-link">Sales Manager</a></li>
                        </ul>
                    </div></div>--%>
                    <div class="box-right box-right-bg">
            <div>
      <img src="images/advanced-search-img.png" width="294" style="float:left;" class="profile-pic" height="141" alt="advanced" />
      <div class="advanced-desc">
      Find Best Talent<br />
      <strong class="blue-color">Advanced Search Filters</strong><br />
      <a href="UserSearch.aspx" class="button-green floatleft" style="margin:10px 0px 0px 105px;">Search Now</a>
      </div>
      </div>            
      </div>

                </div>
            </div>
        </div>
    </div>
    <!-- Range Slider Script Begins -->
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <!-- Range Slider Script Ends -->
</asp:Content>
