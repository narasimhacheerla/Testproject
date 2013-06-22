<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TopReferrers.aspx.cs" Inherits="Huntable.UI.TopReferrers" %>

<%@ Register TagPrefix="uc1" TagName="YourAccountAtGlance" Src="~/UserControls/YourAccountAtGlance.ascx" %>
<%@ Register TagPrefix="uc2" TagName="FriendsInvitaions" Src="~/UserControls/InvitingFriends.ascx" %>
<%@ Register Src="~/UserControls/SeeYourFriendsInvitations.ascx" TagName="seeyourfriends" TagPrefix="uc3" %>
<%@ Register TagPrefix="uc4" TagName="Import" Src="~/UserControls/ImportContacts.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
     <script type="text/javascript">
         $(function () {

             var msie6 = $.browser == 'msie' && $.browser.version < 7;
             if (!msie6) {
                 var top = $('#bx31').offset().top;
                 $(window).scroll(function (event) {
                     var y = $(this).scrollTop();
                     if (y >= 620) { $('#bx31').addClass('fixed'); }
                     else { $('#bx31').removeClass('fixed'); }
                 });
             }
         });</script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="recommendation-head">
                   <%-- <ul class="recommendation-list">
                        <li class="rec-size1"><b class="heading2">Your Friend's Invitations</b></li>
                    </ul>--%>    <div id="njbs" runat ="server" visible="false" style="border:1px solid grey;margin-top: 60px;height: 60px;border-radius: 5px;">
  <h2 style="font-family:Georgia;text-align: center;margin-top: 20px;">None of your friends have invited their friends</h2>  </div>
                 
                            <asp:GridView ID="gvTopReferrers"  AutoGenerateColumns="False" runat="server" DataKeyNames="Id"
                                BorderStyle="None" OnRowDataBound="gv_RowDataBound" BorderWidth="0" GridLines="None" Width="100%">
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="Your Friend's Invitations" HeaderStyle-Font-Size="15px" HeaderStyle-HorizontalAlign="Left">
                                      <ItemTemplate>
                                         <br />
                                         <asp:ImageButton ID="imgBtn1"  runat="server" class="profile-pic" ImageUrl='<%#Eval("UserProfilePictureDisplayUrl")%>' Width="76" Height="81" alt="thumb-small" PostBackUrl='<%#UrlGenerator(Eval("ID")) %>'  />
                                         <div style="float:left; margin-left:94px; margin-top:-50px;"><a href='<%#UrlGenerator(Eval("Id")) %>'><asp:Label ID="lblName" Text='<%# Eval("Name")  %>' runat="server"></asp:Label></a></div>
                                        <br />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                              
                                    <asp:BoundField HeaderText="Invitations Sent" HeaderStyle-CssClass="rec-size2" HeaderStyle-HorizontalAlign="Center" NullDisplayText="0" DataField="LevelOneInvitedCount">
                                    <ItemStyle HorizontalAlign="Center"/>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Joined" HeaderStyle-CssClass="rec-size3" HeaderStyle-HorizontalAlign="Center" NullDisplayText="0" DataField="LevelOnePremiumCount">
                                     <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Earnings" HeaderStyle-CssClass="rec-size4" HeaderStyle-HorizontalAlign="Center" NullDisplayText="0" DataField="AffliateAmountAsText">
                                     <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                
                                </Columns>
                                <RowStyle CssClass="newRows"/>
                                <HeaderStyle CssClass="newRows" />
                            </asp:GridView>
                       
                </div>
                <br />
                <asp:LinkButton runat="server" ID="lbtnShowMore" 
                    class="show-more show-more-margin" onclick="lbtnShowMore_Click">Show More</asp:LinkButton>
                    <asp:HiddenField ID="hfLimit" runat="server"/>
            </div>
            <div class="content-inner-right" style="height:1020px;">
                <div class="box-right">
                    <uc1:YourAccountAtGlance ID="YourAccountAtGlance1" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc2:FriendsInvitaions ID="FriendsInvitaions1" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div id="bx31"><div class="box-right">
                    <uc3:seeyourfriends ID="uc3" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc4:Import ID="ImportContacts" runat="server" />
                </div></div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
