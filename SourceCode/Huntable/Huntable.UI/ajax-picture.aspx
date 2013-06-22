<%@ Page Title="" Language="C#" MasterPageFile="~/Site_popup.Master" AutoEventWireup="true"
    CodeBehind="ajax-picture.aspx.cs" Inherits="Huntable.UI.ajax_picture" %>

<%@ Register Src="UserControls/UserFeedLikedUser.ascx" TagName="UserFeedLikedUser"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/UserFeedComments.ascx" TagName="UserFeedComments"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdnPictureFeedType" runat="server" />
    <div class="pic-main">
        <div class="pic-left">
            <asp:Literal ID="LitOwner" runat="server"></asp:Literal>
            <uc1:UserFeedLikedUser ID="UserFeedLikedUser1" runat="server" />
        </div>
        <div class="pic-right">
            <div align="center">
                <h2 style="margin: 5px 0px;">
                    <asp:Literal ID="litDescription" runat="server"></asp:Literal></h2>
                <table>
                    <tr>
                        <td width="2%">
                            <asp:ImageButton ID="imgPrev" runat="server" ImageUrl="~/images/prev.png" AlternateText="Prev"
                                OnClick="imgPrev_Click" />
                        </td>
                        <td>
                            <asp:Literal ID="litDetail" runat="server"></asp:Literal>
                        </td>
                        <td width="2%">
                            <asp:ImageButton ID="imgNext" runat="server" ImageUrl="~/images/next.png" AlternateText="Next"
                                OnClick="imgNext_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="all-feeds-list-ajax-right">
                <div class="com-pic-top">
                    <div id="divLikeLink" style="display: inline;">
                    </div>
                    <div id="divCommentLink" style="display: inline;">
                    </div>
                    <asp:Label ID="lblTimeStamp" runat="server"></asp:Label>
                </div>
                <uc2:UserFeedComments ID="UserFeedComments1" runat="server" />
            </div>
        </div>
    </div>
    <script src="js/UserFeed.js" type="text/javascript"></script>

</asp:Content>
