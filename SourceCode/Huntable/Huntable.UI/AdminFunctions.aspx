<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AdminFunctions.aspx.cs" Inherits="Huntable.UI.AdminFunctions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <asp:Button ID="btnCustomizeFeedsBatchRun" runat="server" Text="Customize Feeds Batch Run"
                    class="button-orange floatleft poplight" OnClick="BtnCustomizeFeedsBatchRunClick" />
                <asp:Button ID="btnCustomizeJobsBatchRun" runat="server" Text="Customize Jobs Batch Run"
                    class="button-orange floatleft poplight" OnClick="BtnCustomizeJobsBatchRunClick" />
                <asp:Button ID="btnPeopleYouMayKnow" runat="server" Text="People You May Know" OnClick="BtnPeopleYouMayKnowClick"
                    class="button-orange floatleft poplight" />
                <asp:Button ID="Button1" runat="server" Text="Featured Recruiters" OnClick="BtnFeaturedRecruiters"
                    class="button-orange floatleft poplight" />
                <asp:Button ID="btnClearCache" runat="server" Text="Clear Cache" OnClick="BtnClearCacheClick"
                    class="button-orange floatleft poplight" />
                <asp:Button ID="btnResendInvitations" runat="server" Text="Resend Invitations" OnClick="BtnResendInvitations"
                    class="button-orange floatleft poplight" />
                    <asp:Button ID="jobfeeds" runat="server" Text ="Job Feeds" OnClick="BtnJobFeeds" class="button-orange floatleft poplight" />
                    <asp:Label ID ="jbfeeds" runat ="server"></asp:Label>
                     <asp:Button ID="RemamberEmail" runat="server" Text ="Rememeber Email" OnClick="BtnRememberEmail" class="button-orange floatleft poplight" />
               <asp:Label ID ="RmbrEmail" runat ="server"></asp:Label>
                 <asp:Button ID="btnJobRemember" runat="server" Text ="Job Remember Email" OnClick="BtnJobrememberEmail" class="button-orange floatleft poplight" />
               <asp:Label ID ="lbljobRemember" runat ="server"></asp:Label>
                <asp:Button ID="btnEmailInvites" runat="server" Text="Email Invites" OnClick="btnEmailInvitesClick"
                    class="button-orange floatleft poplight" />
                <asp:Button ID="btnJobsStatus" runat="server" Text="Jobs Count" OnClick="btnJobsStatusClick"
                    class="button-orange floatleft poplight" />
                          <asp:Button ID="btnsitemap" runat="server" Text="Site map" OnClick="btnsitemapClick"
                    class="button-orange floatleft poplight" />
                <asp:Label ID ="lblEmailInvites" runat ="server"></asp:Label>
                <br />
                <br />
                <br />
                <span>Post news </span>
                <br />
                <br />
                <span>Subject:</span>
                <asp:TextBox ID="txtSubject" runat="server" CssClass="textarea textarea-summary"></asp:TextBox>
                <br />
                <br />
                <span>Body:</span>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Rows="6" Columns="2"
                    CssClass="textarea textarea-summary"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="button-orange" OnClick="BtnSendClick" />
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
