<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="JobsStatus.aspx.cs" Inherits="Huntable.UI.JobsStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left content-inner-left-message">
                <table>
                    <tr style="border-bottom: 1px solid #000000; font-size: 15px; font-weight: bold;">
                        <td width="40.2%">
                            <asp:Label ID="Label2" runat="server" Text="Jobs"></asp:Label>
                        </td>
                        <td width="52%">
                            <asp:Label ID="Label3" runat="server" Text="Date and Time"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text= "Type"></asp:Label>
                        </td>
                    </tr>
                   </table>
                        <hr style="width: 452px;" />
                   <table>
                    <asp:DataList runat="server" ID="dlJobsStatus">
                        <ItemTemplate>
                            <tr>
                                <td width="40%">
                                    <asp:Label runat="server" Text='<%#Eval("NoOfJobs") %>'></asp:Label>
                                </td>
                                <td width="52%">
                                    <asp:Label runat="server" Text='<%#Eval("dttime") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("type") %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <SeparatorTemplate>
                            <br />
                        </SeparatorTemplate>
                    </asp:DataList>
                </table>
                <br />
                <br />
              

            </div>
        </div>
    </div>
</asp:Content>
