<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SeeMoreRecruiter.aspx.cs" Inherits="Huntable.UI.SeeMoreRecruiter" %>

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
                    var top = $('#bx13').offset().top;
                    $(window).scroll(function (event) {
                        var y = $(this).scrollTop();
                        if (y >= 14) { $('#bx13').addClass('fixed'); }
                        else { $('#bx13').removeClass('fixed'); }
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
                         return false;
                     });
                 });
    </script>
    </head>
    <body>
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
                    <div class="see-recruiters">
                        <%--<div class="rlink">
                            <a href="#">See More Recruiters</a></div>--%>
                       <%-- <h3>
                            Alphabitical List</h3>--%>
                        <div class="alphabet" id="alpa" runat="server">
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
                        <ul class="how-it-work-list how-it-work-list1 how-it-work-list-recruit  r-recruits"
                            style="margin-top: 43px;">
                           
                            <asp:DataList ID="rpsmrlist" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <li><a href='<%#UrlGenerator(Eval("Id")) %>' id="A1" runat="server">
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                                        <asp:Image runat="server" ID="smr" Width="124px" Height="70px" Style="margin-bottom: 11px;"
                                            ImageUrl='<%#Picture(Eval("CompanyLogoId"))%>' /></a>
                                            <a href='<%# "~/companyjobs.aspx?Id=" +Eval("Id")  %>' id="compwebsite" runat="server">
                                        <asp:Label ID="Label2" runat="server" Text='<%#(Jobs(Eval("Id"))) %>' Style="margin-left: -44px;"></asp:Label>
                                        Jobs </a>
                                        <div id="DivFollowing" runat="server" visible='<%#IsThisUserFollowingCompany(Eval("Id"))%>'>
                                            <asp:LinkButton ID="LinkButton1" class="button-orange button-orange-cf"  OnClick="CommandCompanyEmployeeUnFollowClick"
                                                CommandArgument='<%#Eval("Id")%>' Text="Following" runat="server" Style="margin-right: -50px;" /><br />
                                        </div>
                                        <div id="DivFollow" runat="server" visible='<%#!IsThisUserFollowingCompany(Eval("Id"))%>'>
                                            <asp:LinkButton ID="linkButtonFollow"  class="button-orange button-orange-cf" OnClick="CommandCompanyEmployeeFollowClick"
                                                Text="Follow" CommandArgument='<%#Eval("Id")%>' runat="server" 
                                               Style="margin-right: -50px;"></asp:LinkButton>
                                        </div>
                                    </li>
                            </ItemTemplate>
                            </asp:DataList>
                        </ul>
                    </div>
                </div>
                <!-- content inner left ends -->
                <div >
                    <div class="content-inner-right">
                        <div class="box-right box-right-promote">
                            <h2>
                                Promote Your Company by targeted ad's</h2>
                            Industry, Skill, Interest, Country<br />
                            <br />
                            <a href="FeaturedIndustry.aspx" class="accounts-link">Find out more...</a>
                            <img class="promot-img" src="images/promote-arrow.png" width="29" height="47" alt="arrow" />
                        </div>
                        <p class="margin-top-visible">
                            &nbsp;</p>
                        <div class="google-add">
                                  <asp:Image ID="bimage" runat="server"   ImageUrl="images/basic-user-advert.gif"/>
                <asp:Image ID="pimage" runat="server"   ImageUrl="images/premium-user-advert.gif" />
                            <script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
                            </script>
                            <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
                            <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
                                type="text/javascript"></script>
                            <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
                        </div>
                    </div>
                </div>
                <!-- content inner right ends -->
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
    </body>
    </html>
</asp:Content>
