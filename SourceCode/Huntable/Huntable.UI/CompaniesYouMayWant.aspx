<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CompaniesYouMayWant.aspx.cs" Inherits="Huntable.UI.CompaniesYouMayWant" %>

<%@ Register Src="~/UserControls/JobControl.ascx" TagName="JobControl" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/FeaturedRecruiters.ascx" TagName="Recruiters" TagPrefix="uc1" %>
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
                    var top = $('#box-jobsearch').offset().top;
                    $(window).scroll(function (event) {
                        var y = $(this).scrollTop();
                        if (y >= 17) { $('#box-jobsearch').addClass('fixed'); }
                        else { $('#box-jobsearch').removeClass('fixed'); }
                    });
                }
            });
            $(function () {

                // Check whether browser is IE6

                var msie6 = $.browser == 'msie' && $.browser.version < 7;

                // Only run the following code if browser
                // is not IE6. On IE6, the box will always
                // scroll.

                if (!msie6) {

                    // Set the 'top' variable. The following
                    // code calculates the initial position of
                    // the box. 

                    var top = $('#box-jobsearch').offset().top;

                    // Next, we use jQuery's scroll function
                    // to monitor the page as we scroll through.

                    $(window).scroll(function (event) {

                        // In the following line, we set 'y' to
                        // be the amount of pixels scrolled
                        // from the top.

                        var y = $(this).scrollTop();

                        // Have you scrolled beyond the
                        // box? If so, we need to set the box
                        // to fixed.

                        if (y >= 17) {

                            // Set the box to the 'fixed' class.

                            $('#box-jobsearch').addClass('fixed');

                        } else {

                            // Remove the 'fixed' class 

                            $('#box-jobsearch').removeClass('fixed');
                        }
                    });
                }
            });
   </script>
    </head>
    <body>
        <div id="content-section" style="height:511px;">
            <div id="content-inner">
                <div class="content-inner-left">
                    <div class="notification" id="box-jobsearch" style="margin-left:-501px;border:1px solid rgba(248, 245, 245, 0.59);">
                        <uc:JobControl ID="JobControl" runat="server" />
                        <div class="notification-right notification-right1" style="width: 436px; margin-top: -472px;margin-left: 214px;"">
                            <h3 class="login-heading">
                                Companies you may want to follow</h3>
                            <ul class="company-you-follow-list">
                                <li>
                                
                                <asp:DataList runat="server" ID="rspcom" RepeatColumns="2" RepeatDirection="Horizontal">
                                <ItemTemplate> 
                                            <table>
                                                <tr style="width:100px;">
                                                <td>
                                                     <a  href='<%#UrlGenerator(Eval("Id")) %>' id="compwebsite" runat="server">
                                                        <asp:ImageButton ID="ImageButton1" Width="126px" Height="72px" runat="server" ImageUrl='<%#Picture(Eval("CompanyId"))%>' />
                                                      </a>
                                                </td>
                                                <td>
                                                <asp:Label runat="server" Text='<%#Eval("Description")%>' Visible="false" ID="CompanyName"></asp:Label>  
                                                        (<asp:Label ID="Label1" runat="server"  Text='<%#Jobs(Eval("Description")) %>'></asp:Label>)Jobs
                                                  
                                                        <asp:LinkButton ID="linkbutton"  class="invite-friend-btn" CommandArgument='<%#Eval("CompanyId")%>'
                                                            OnClick="FollowupClick" Text="Follow+" runat="server"></asp:LinkButton>  
                                                </td>
                                                </tr>
                                                        
                                             
                                            </table>
                                        </ItemTemplate>
                                        <SeparatorTemplate>
                                       
                                        </SeparatorTemplate>
                                </asp:DataList>
                               

                                   <%-- <asp:Repeater runat="server" ID="rspcompny">
                                        <ItemTemplate>
                                            <div style="width:100%;">
                                                
                                                        <asp:ImageButton Width="126px" Height="72px" runat="server" ImageUrl='<%#Picture(Eval("CompanyId"))%>' /><br />
                                                        <asp:Label runat="server" Text='<%#Eval("Description")%>' Visible="false" ID="CompanyName"></asp:Label>  
                                                        (<asp:Label runat="server"  Text='<%#Jobs(Eval("Description")) %>'></asp:Label>)Jobs
                                                  
                                                        <asp:LinkButton ID="linkbutton"  class="invite-friend-btn" CommandArgument='<%#Eval("CompanyId")%>'
                                                            OnClick="FollowupClick" Text="Follow" runat="server"></asp:LinkButton>  
                                             
                                            </div>
                                        </ItemTemplate>
                                        <SeparatorTemplate>
                                        <br />
                                        </SeparatorTemplate>
                                    </asp:Repeater>--%>
                                    <a style="margin-left: 60px;" class="show-more" href="#">Show More </a>
                        </div>
                </div>
                <div style="width:305px; margin-left:650px ; margin-left:665px;">
               <uc1:Recruiters ID="Recruiter" runat="server" />
            </div>
            </div>
            <!-- content inner ends -->
        </div>
        </div>
    </body>
    </html>
   
</asp:Content>
