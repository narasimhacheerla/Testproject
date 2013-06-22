<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="InvitationsOverview.aspx.cs" Inherits="Huntable.UI.InvitationsOverview" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Src="~/UserControls/YourAccountAtGlance.ascx" TagName="youraccount"
    TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/InvitingFriends.ascx" TagName="invitefriends" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/ImportContacts.ascx" TagName="importcontacts" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <ul class="tab-list">
                    <li><a href="InvitationsOverview.aspx" class="st_tab ">My
                        Accounts </a></li>
                    <li><a href="InvitationsReport.aspx" class="st_tab">Invitations</a></li>
                    <li><a href="InvitationsOverview.aspx" class="st_tab st_first_tab st_tab_active">OverView</a></li>
                </ul>
                <div class="accounts-profile accounts-profile-account">
                    <div class="accounts-profile-left">
                        <a id="imagehref" runat="server">
                            <asp:Image runat="server" ID="imgProfile" class="profile-pic" Width="76" Height="81"
                                alt="Profile-pic" /></a> <a id="namehref" runat="server" class="profile-name">
                                    <asp:Label ID="lblName" runat="server" Text="Label" Font-Bold="True"></asp:Label></a>
                    </div>
                    <div class="accountsab-profile-right">
                        <div class="accounts-top">
                            <table>
                                <tr>
                                    <td style="width:20px; line-height:14px;">
                                        <asp:Label ID="lblPercentCompleted" class="profile-complete" runat="server"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <span>Complete</span>
                                    </td>
                                    <td align="left" style="width:200px">
                                        <eo:ProgressBar ID="ProgressBar2" runat="server" Width="90px" BackgroundImage="00060301"
                                            BackgroundImageLeft="00060302" BackgroundImageRight="00060303" ControlSkinID="None"
                                            IndicatorImage="00060304" ShowPercentage="True" Value="30">
                                        </eo:ProgressBar>
                                    </td>
                                      <td valign="top"><asp:Label ID="lblmember"  runat="server" Width="130px"  ></asp:Label>
                                  <asp:Label ID="Label1" runat="server" Text="Member since:"  Width="90px" ></asp:Label>
                                        
                                    
                                </td>
                                </tr>
                            </table>
                        </div>
                        <span class="last-login">Last Login Time:<b>
                            <asp:Label ID="lblLogin" runat="server"></asp:Label></b></span> <span class="last-update">
                                Last Profile Updated on: <b>
                                    <asp:Label ID="lblProfile" runat="server"></asp:Label></b></span>
                    </div>
                </div>
                <p>
                </p>
                <div class="invitation-list">
                   
                        <asp:GridView ID="gvInvSummary" HorizontalAlign="Center" AutoGenerateColumns="False"
                            runat="server" Width="100%" Height="125px" BackColor="#CCCCCC" BorderColor="#999999"
                            BorderStyle="Solid" BorderWidth="1px" GridLines="None" CellPadding="4" CellSpacing="2"
                            ForeColor="Black" HeaderStyle-CssClass="bg-ash-invite1">
                            <Columns>
                                <asp:BoundField DataField="Level" />
                                <asp:BoundField HeaderText="INVITED" DataField="Invited" />
                                <asp:BoundField HeaderText="JOINED" DataField="Joined" />
                                <asp:BoundField HeaderText="EARNINGS" DataField="Earnings" />
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <%-- <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />--%>
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                            <RowStyle BackColor="White" CssClass="bg-ash-invite" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                        </asp:GridView>
                       
                </div>
                <br />
                <div style="float: right; margin-left: 470px;">
                    <asp:Label Font-Size="Large" runat="server" ID="lblTotalEarnings"></asp:Label>
                </div>
                <div class="names-list">
                    <h3 class="job-search-heading">
                        Your Connections</h3>
                    <div class="connection-main">
                        <div class="connection-inner">
                            <b class="connection-inner-head">1st Connections</b>
                            <asp:GridView HorizontalAlign="Center" ID="gvLevel1" DataKeyNames="Id" Width="100%" CssClass="name-list1"
                                AutoGenerateColumns="False" EmptyDataText="No connections found" ShowHeader="False"
                                runat="server" OnSelectedIndexChanged="gvLevel1_SelectedIndexChanged" EnablePersistedSelection="True"
                                OnRowDataBound="gvLevel1_RowDataBound" SelectedRowStyle="SelectedRowStyle" ShowFooter="True">
                                <Columns>
                                    <asp:ButtonField CommandName="select" Visible="false" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle BackColor="#CCCCCC" />
                            </asp:GridView>
                        </div>
                        <div class="connection-inner">
                            <b class="connection-inner-head">2nd Connections</b>
                            <asp:GridView ID="gvLevel2" HorizontalAlign="Center" DataKeyNames="Id" AutoGenerateColumns="False" CssClass="name-list1"
                                EmptyDataText="No connections found" Width="100%" runat="server" OnSelectedIndexChanged="gvLevel2_SelectedIndexChanged"
                                EnablePersistedSelection="True" OnRowDataBound="gvLevel2_RowDataBound" ShowHeader="False"
                                ShowFooter="True">
                                <Columns>
                                    <asp:ButtonField CommandName="select" Visible="false" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle BackColor="#CCCCCC" />
                            </asp:GridView>
                        </div>
                        <div class="connection-inner">
                            <b class="connection-inner-head">3rd Connections</b>
                            <asp:GridView ID="gvLevel3" HorizontalAlign="Center" DataKeyNames="Id" AutoGenerateColumns="False" CssClass="name-list1"
                                EmptyDataText="No connections found" Width="100%" runat="server" ShowHeader="False"
                                OnRowDataBound="gvLevel3_RowDataBound" ShowFooter="True">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="content-inner-right">
                <div class="box-right">
                    <uc3:youraccount ID="ucyour" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc4:invitefriends ID="ucinvite" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc5:importcontacts ID="ucimport" runat="server" />
                </div>
            </div>
        </div>
    </div>
     <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript" ></script>
</asp:Content>
