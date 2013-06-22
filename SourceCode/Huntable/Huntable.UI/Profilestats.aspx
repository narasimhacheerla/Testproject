<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProfileStats.aspx.cs" Inherits="Huntable.UI.ProfileStats" %>

<%@ Register Src="UserControls/cvStatistics.ascx" TagName="cvStatistics" TagPrefix="uc1" %>
<%@ Register Src="UserControls/YourAccountAtGlance.ascx" TagName="YourAccountAtGlance"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style15
        {
            width: 51px;
        }
        .style16
        {
            width: 85px;
        }
        .style17
        {
            width: 76px;
        }
        .style18
        {
            width: 81px;
        }
        .style19
        {
            width: 102px;
        }
        .style20
        {
            width: 97px;
        }
        .style21
        {
            width: 77px;
        }
        .style22
        {
            width: 182px;
        }
        .style23
        {
            width: 300px;
        }
        .style24
        {
        }
        .style26
        {
            width: 387px;
            height: 213px;
        }
        .style27
        {
            width: 385px;
        }
        .style28
        {
            width: 216px;
        }
        .style29
        {
            width: 104px;
            height: 213px;
        }
        .style30
        {
            width: 117px;
        }
        .style31
        {
        }
        .style32
        {
            width: 119px;
        }
        .style33
        {
            height: 213px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="style10">
        <tr>
            <td class="style15">
                <asp:HyperLink ID="HyperLink1" runat="server">Home</asp:HyperLink>
            </td>
            <td class="style16">
                <asp:HyperLink ID="HyperLink2" runat="server">Profile</asp:HyperLink>
            </td>
            <td class="style17">
                <asp:HyperLink ID="HyperLink3" runat="server">Jobs</asp:HyperLink>
            </td>
            <td class="style18">
                <asp:HyperLink ID="HyperLink4" runat="server">Feeds</asp:HyperLink>
            </td>
            <td class="style19">
                <asp:HyperLink ID="HyperLink5" runat="server">Find friends</asp:HyperLink>
            </td>
            <td class="style20">
                <asp:HyperLink ID="HyperLink6" runat="server">Account</asp:HyperLink>
            </td>
            <td class="style21">
                Settings
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table class="style10">
        <tr>
            <td class="style22">
                Your Huntable Profile Stats
            </td>
            <td class="style23">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Last 90 Days december 15,2011-april 2012"></asp:Label>
            </td>
        </tr>
    </table>
    <hr />
    <br />
    <table class="style10" style="height: 151px">
        <tr>
            <td class="style29">
                <asp:Image ID="imgphoto" runat="server" ImageUrl="~/image/image7_1.png" />
            </td>
            <td class="style26">
                <table class="style10" style="height: 123px; width: 100%">
                    <tr>
                        <td class="style28">
                            <asp:Panel ID="Panel2" runat="server" Height="97px" Width="205px">
                                <table class="style10">
                                    <tr>
                                        <td class="style27">
                                            <asp:Label ID="Label4" runat="server" Text="Head chief"></asp:Label>
                                            &nbsp;At
                                            <asp:Label ID="Label5" runat="server" Text="James thane"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style27">
                                            Is this your current position
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style27">
                                            <asp:Button ID="Button3" runat="server" BackColor="#00CC99" BorderStyle="None" Text="Yes" />
                                            &nbsp;
                                            <asp:Button ID="Button4" runat="server" BackColor="#FF3300" BorderStyle="None" Text="NO" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>
                            <asp:Panel ID="Panel3" runat="server" Height="96px">
                                <p>
                                    Updating Your profile lest you keep ahead of your competetation your Cv huntable</p>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="style33">
                <asp:Panel ID="Panel4" runat="server" Height="193px" Width="312px">
                    <uc2:YourAccountAtGlance ID="YourAccountAtGlance1" runat="server" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style24" colspan="2">
                <asp:Image ID="imggraph" runat="server" ImageUrl="~/image/biggraph.png" />
                <uc1:cvStatistics ID="cvStatistics1" runat="server" />
            </td>
            <td rowspan="2">
                <asp:Panel ID="Panel5" runat="server" Height="692px">
                    <table class="style10">
                        <tr>
                            <td>
                                <asp:Image ID="imgmap" runat="server" ImageUrl="~/image/map.png" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Image ID="imgview" runat="server" ImageUrl="~/image/viewersbytype.png" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Users" Font-Bold="True" ForeColor="#FF9966"></asp:Label>
                                &nbsp;103,546<br />
                                <asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="#FF9900" Text="Invites sent"></asp:Label>
                                &nbsp;1,363,948<br />
                                <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="#FF9900" Text="Total Value of Invites"></asp:Label>
                                &nbsp;$3409870<br />
                                <asp:Label ID="Label13" runat="server" Font-Bold="True" ForeColor="#FF9900" Text="Money released to users"></asp:Label>
                                &nbsp;$258,865<br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Image ID="image3" runat="server" img src="images/cropped.png" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style24" colspan="2">
                <table class="style10">
                    <tr>
                        <td style="background-color: Gray">
                            Who&#39;s viewed your Profile
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="style10">
                                <tr>
                                    <td class="style30">
                                        Today
                                    </td>
                                    <td class="style31">
                                        LAST WEEK
                                    </td>
                                    <td class="style32">
                                        LAST MONTH
                                    </td>
                                    <td>
                                        OVERALL
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style30">
                                        <asp:Image ID="imgguest" runat="server" ImageUrl="~/image/image7_2.png" />
                                    </td>
                                    <td class="style31" colspan="3">
                                        <table class="style10">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" ForeColor="#3399FF" Text="Some one in food production"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label7" runat="server" ForeColor="#3399FF" Text="Today"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style30">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/image/image7_2.png" />
                                    </td>
                                    <td class="style31" colspan="3">
                                        <table class="style10">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" ForeColor="#3399FF" Text="Some one in food production"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" ForeColor="#3399FF" Text="Today"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style30">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/image/image7_2.png" />
                                    </td>
                                    <td class="style31" colspan="3">
                                        <table class="style10">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" ForeColor="#3399FF" Text="Some one in food production"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label9" runat="server" ForeColor="#3399FF" Text="Today"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
