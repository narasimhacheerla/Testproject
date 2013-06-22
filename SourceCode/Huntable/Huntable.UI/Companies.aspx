<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Companies.aspx.cs" Inherits="Huntable.UI.Companies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left content-inner-left-message">
                
                    <h4 class="login-heading">
                        Arun Manickam > Profile > Activity > <a href="#" class="accounts-link">Companies</a></h4>
                        
                    <asp:DataList ID="dtCompanies" runat="server" RepeatColumns="3">
                        <ItemTemplate>
                            <ul class="company-you-follow-list1">
                                <li>
                                    <table width="215" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td rowspan="2">
                                                <a href="#">
                                                    <asp:Image runat="server" Width="126" Height="72" ImageUrl='<%#Picture(Eval("CompanyID"))%>' /></a>
                                            </td>
                                            <td align="center" valign="top">
                                                <a href="#" class="accounts-link"><asp:Label CssClass="accounts-link" Text='<%#Jobs(Eval("CompanyID")) %>' runat="server"></asp:Label> Jobs</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle">
                                                <asp:LinkButton CssClass="invite-friend-btn" OnClick="FollowupClick" CommandArgument='<%#Eval("CompanyID")%>'  runat="server" ID="BtnFollow" Text="Follow +" />
                                            </td>
                                        </tr>
                                    </table>
                                </li>
                            </ul>
                        </ItemTemplate>
                    </asp:DataList>
                
            </div>
            <div class="content-inner-right content-inner-right-message">
                
                  
                   <asp:Image ID="bimage" runat="server"  ImageUrl="images/basic-user-advert.gif"/>
                <asp:Image ID="pimage" runat="server"  ImageUrl="images/premium-user-advert.gif" />
                
            </div>
        </div>
        <!-- content inner ends -->
    </div>
    <!-- content section ends -->
</asp:Content>
