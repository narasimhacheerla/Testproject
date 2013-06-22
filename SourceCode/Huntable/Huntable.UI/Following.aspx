<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Following.aspx.cs" Inherits="Huntable.UI.Following" %>

<%@ Register Src="~/UserControls/GoogleAdds.ascx" TagName="Googleadds" TagPrefix="uc" %>
<%@ Register src="~/UserControls/PeopleYouMayKnow.ascx" tagName="PplYouMayKNow" tagPrefix="uc2" %>
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
                    var top = $('#bx5').offset().top;
                    $(window).scroll(function (event) {
                        var y = $(this).scrollTop();
                        if (y >= 45) { $('#bx5').addClass('fixed'); }
                        else { $('#bx5').removeClass('fixed'); }
                    });
                }
            });</script>
        <script type="text/javascript">
            $(function () {

                var msie6 = $.browser == 'msie' && $.browser.version < 7;
                if (!msie6) {
                    var top = $('#bx20').offset().top;
                    $(window).scroll(function (event) {
                        var y = $(this).scrollTop();
                        if (y >= 50) { $('#bx20').addClass('fixed'); }
                        else { $('#bx20').removeClass('fixed'); }
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
    </head>
    <body>
          <div id="ovrly" style="height: 30px; width: 100%; z-index: 2000; background-color: #FF9242;
            border: 1px solid #FF9242; border-radius: 2px; margin-top: 10px; display: none;">
            <div style="width: 980px; padding: 0px 10px; margin: 0px auto;">
                <div style="width: 30px; margin-left: 292px;">
                    <image src="images/tick.png" width="25px" height="25px"></image>
                </div>
                <div style="margin-top: -20px">
                    <asp:Label Style="color: White; width: 200px; margin-top: -40px; margin-left: 377px;"
                        ID="pbl" runat="server"></asp:Label></div>
                <div style="margin-left: 621px; margin-top: -13px">
                    <image id="ximg" src="images/orange-check-mark-md.png" width="10px" height="10px"></image>
                </div>
            </div>
        </div>
        <div id="content-section">
            <div id="content-inner">
                <div class="content-inner-left content-inner-left-message">
                    <h4 class="login-heading" style="width: 689px;">
                        <asp:Label ID="lblname" runat="server"></asp:Label> > Profile > Activity > <a href="#" class="accounts-link">Following</a></h4>
                    <div class="followers-list">
                        <div id="bx20">
                        </div>
                        <div id="bx5" class="followers-link" style="width: 689px;">
                            <a runat="server" id="follower" class="follow-ash">My Followers (<asp:Label runat="server"
                                ID="lblFollowers"></asp:Label>)</a> <a runat="server" id="following" class="follow-blue">I am Following
                                    (<asp:Label runat="server" ID="lblfollowing"></asp:Label>)</a>
                        </div>
                        <div class="followers-list-table" id="tabs-2" style="margin-top: 65px; margin-left: 17px;">
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="update">
                                <ContentTemplate>
                                    <asp:DataList runat="server" ID="dlFollowing" RepeatColumns="3" RepeatDirection="Horizontal" CellSpacing="1" CellPadding="1" OnItemDataBound="itembound">
                                        <ItemTemplate>
                                         <div style="width:230px;">
                                                <a id="A2" runat="server"><asp:Image runat="server" 
                                                    class="profile-pic profile-pic2" ID="ibprofilepic" Width="80px" Height="85px"
                                                    ImageUrl='<%#Picture(Eval("Id"))%>' /></a>
                                                <a id="A1" runat="server">
                                                    <asp:Label runat="server" class="accounts-link" Text='<%#Eval(" FirstName") %>' ID="lblName"></asp:Label></a><br />
                                                   <asp:Label runat="server" Text='<%#Eval(" CurrentPosition") %>' ID="Label2"></asp:Label><br />
                                                <asp:Label runat="server" Text='<%#Eval("CurrentCompany")%>' ID="lblEdu"></asp:Label><br />
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("City") %>'></asp:Label>
                                                <div id ="dv" runat="server" >
                                                <div id="Div1" visible='<%#!IsThisUserFollowing(Eval("Id"))%>' runat="server">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("Id")%>'
                                                        OnClick="FollowupClick" class="invite-friend-btn">
                                                Follow +</asp:LinkButton></div>
                                                <div id="Div2" class="floating" visible='<%#IsThisUserFollowing(Eval("Id"))%>' runat="server"
                                                    style="margin-left: 178px;">
                                                    <%-- <asp:Image runat="server" Width="20" Height="20" ID="Following" ImageAlign="Right"
                                            ImageUrl="images/tick.png" Visible="false" />--%>
                                                   <%-- <asp:LinkButton runat="server" ID="follower" Width="20" CommandArgument='<%#Eval("Id")%>'
                                                        OnClick="UnFollowClick" > </asp:LinkButton>--%>
                        <div style="margin-top: -32px; margin-left: -18px;"><asp:LinkButton runat="server" OnClick="UnFollowClick" CommandArgument='<%#Eval("Id")%>'  Width="48"  Height="12" Text="UnFollow"  Font-Size="11px"  CssClass="invite-friend-btn invite-friend-btn1 follbtn"></asp:LinkButton></div>
                                                </div></div>
                                                <div id="undv" runat="server" style="width:190px"></div>
                                        <%-- <hr style="width: 232px; margin-top: 58px; margin-left: -2px;margin-bottom:16px;" />--%>
                                         </div>
                                        </ItemTemplate>
                                        <SeparatorTemplate>
                                      
                                          <br />
                                            <hr style="width: 232px; margin-top: 120px; margin-left: -232px;" />
                                            
                                        </SeparatorTemplate>
                                    </asp:DataList>
                                   
                                </ContentTemplate>
                            </asp:UpdatePanel>
                              <asp:UpdateProgress ID="updateProgressBar1" AssociatedUpdatePanelID="update" runat="server">
                                    <ProgressTemplate>
                                        <img src="images/loadingBar.gif" style="height: 50px" alt="Update in progress" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                        </div>
                        <%--   <div class="followers-list-table" id="tabs-1">
                        <asp:DataList runat="server" ID="dlFollowers" RepeatColumns="3" RepeatDirection="Horizontal">
                        <ItemTemplate>
                        <div class="followers-list-table"  > 
                        <asp:ImageButton runat="server" class="profile-pic profile-pic2" ID="ibprofilepic" Width="80px" Height="85px" ImageUrl='<%#Picture(Eval("Id"))%>' />
                                <asp:Label runat="server" class="accounts-link"  Text='<%#Eval(" FirstName") %>' ID="lblName"></asp:Label>
                                <asp:Label runat="server" Text='<%#Eval("CurrentCompany")%>' ID="lblEdu"></asp:Label>
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("City") %>'></asp:Label>
                        </div>
                        </ItemTemplate>
                        <SeparatorTemplate>
                        <br />
                        </SeparatorTemplate>
                        </asp:DataList>
                      
                        </div>   --%>
                    </div>
                </div>
               
                <div class="content-inner-right content-inner-right-message">
                  <div style="margin-right:-40px" runat="server" id="pplYouMayKnowDiv" >
                      <asp:UpdatePanel runat="server">
                          <ContentTemplate>
                              <uc2:PplYouMayKNow ID="PplYouMayKNow1" runat="server" />
                          </ContentTemplate>
                      </asp:UpdatePanel>
                      
                  </div>
                         <asp:Image ID="bimage" style="margin-top:35px" runat="server"  ImageUrl="images/basic-user-advert.gif"/>
                <asp:Image ID="pimage" runat="server" style="margin-top:35px"  ImageUrl="images/premium-user-advert.gif" />
                </div>
            </div>
            <!-- content inner ends -->
        </div>
        <!-- content section ends -->
        <!-- Range Slider Script Begins -->
        <!--<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>-->
        <!-- Range Slider Script Ends -->
        <!-- Footer section ends -->
    </body>
    </html>
</asp:Content>
