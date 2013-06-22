<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataPager.ascx.cs" Inherits="Snovaspace.Util.Controls.DataPager" %>

<table cellpadding="0" cellspacing="0" width="100%" border="0" class="PagingContainer">
    <tr class="GridFooter" style="height: 25px;">
        <td align="left" style="width: 25%" class="PagingShowingRecord">
            <asp:Label runat="server" ID="lblTotalRecord">Showing 00 of 00 record(s)</asp:Label>
        </td>
        <td align="center" style="width: 50%">
            <table cellpadding="0" cellspacing="0" border="0" class="PagingNavigation">
                <tr>
                    <td>
                        <asp:Button runat="server" ToolTip="Move First" ID="btnMoveFirst" CssClass="PagingFirst"
                            CommandArgument="First" OnCommand="BtnMoveClick" />
                    </td>
                    <td>
                        <asp:Button runat="server" ToolTip="Move Previous" ID="btnMovePrevious" CssClass="PagingPrevious"
                            CommandArgument="Previous" OnCommand="BtnMoveClick" />
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" AutoPostBack="true" CssClass="CurrentPage" ID="txtPage"
                                        OnTextChanged="TxtPageTextChanged" Width="40"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblTotalPage">Showing 00 of 00 record(s)</asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Button runat="server" ToolTip="Move Next" ID="btnMoveNext" CssClass="PagingNext"
                            CommandArgument="Next" OnCommand="BtnMoveClick" />
                    </td>
                    <td>
                        <asp:Button runat="server" ToolTip="Move Last" ID="btnMoveLast" CssClass="PagingLast"
                            CommandArgument="Last" OnCommand="BtnMoveClick" />
                    </td>
                </tr>
            </table>
        </td>
        <td align="right">
            <table cellpadding="0" cellspacing="0" class="PagingRecords">
                <tr>
                    <td>
                        <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlRecords" OnSelectedIndexChanged="DDLRecordsSelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
