<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="InvitationsReport.aspx.cs" Inherits="Huntable.UI.InvitationsReport" %>

<%@ Register TagPrefix="uc1" TagName="SeeFrInvitations" Src="~/UserControls/SeeYourFriendsInvitations.ascx" %>
<%@ Register TagPrefix="uc2" TagName="FriendsToInvite" Src="~/UserControls/FriendsToInvite.ascx" %>
<%@ Register TagPrefix="uc3" TagName="AccountsGlance" Src="~/UserControls/YourAccountAtGlance.ascx" %>
<%@ Register TagPrefix="uc4" TagName="CVStats" Src="~/UserControls/cvStatistics.ascx" %>
<%@ Register Src="UserControls/PeopleYouMayKnow.ascx" TagName="pplUMayKnow" TagPrefix="uc12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <script src='http://connect.facebook.net/en_US/all.js'></script>
    <script language="javascript" type="text/javascript">

        var doneEvent = function () {
            document.getElementById("<%= btnHidden.ClientID %>").click();
        };
        FB.init({ appId: "307487099357078", status: true, cookie: true });
        function postToFeed(url, message, fbid) {

            var obj = {
                method: 'feed',
                to: fbid,
                redirect_uri: 'https://huntable.co.uk/InvitationsReport.aspx',
                link: url,
                picture: 'http://huntable.co.uk/images/huntable_logo.jpg',
                name: message,
                caption: 'Inviting you to join huntable',
                description: 'The fastest growing Professional Resourcing Network'
            };

            function callback(response) {
                if (response) {
                    alert('Invitation Sent');
                    var result = $("[title=" + fbid + "]");
                    if (result) {
                        result[0].innerText = 'Re-Invite';
                    }
                }

            }

            FB.ui(obj, callback);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="fb-root">
    </div>
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <ul class="tab-list">
                    <li><a href="MyAccount.aspx" class="st_tab">My Accounts </a></li>
                    <li><a href="InvitationsReport.aspx" class="st_tab st_first_tab st_tab_active">Invitations</a></li>
                    <li><a href="InvitationsOverview.aspx" class="st_tab">OverView</a></li>
                </ul>
                <ul class="invitation-list" style="height: 400px;">
                    <li class="bg-ash-invite"><span class="invite-width-one"><strong>List of all invitations
                        sent</strong></span> <span class="invite-width-two">
                            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">Status</asp:ListItem>
                                <asp:ListItem Value="1">Joined</asp:ListItem>
                                <asp:ListItem Value="2">Not-Joined Yet</asp:ListItem>
                            </asp:DropDownList>
                            <%-- <select class="textbox listbox" style="width: 120px;">
                                <option>Status</option>
                                <option>Joined</option>
                                <option>Not-Joined Yet</option>
                            </select>--%>
                        </span>&nbsp;&nbsp;<asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All"
                            Visible="false" />
                    </li>
                    <li>
                        <asp:Button ID="btnHidden" runat="Server" Style="display: none" OnClick="BtnHiddenClick" />
                        <asp:GridView ID="gvInvitations" AutoGenerateColumns="False" runat="server" DataKeyNames="Id,InvitationTypeId"
                            OnRowCommand="GVInvitationsRowCommand" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="Name" DataField="Name" />
                                <asp:BoundField HeaderText="Email" DataField="EmailAddress" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" Text='<%# Eval("IsJoined").ToString()=="False" ? "NOT-JOINED YET" :"JOINED"  %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnInvite" runat="server" Visible='<%# (Eval("IsJoined").ToString()=="False") %>'
                                            Text="RE SEND INVITATION" CommandName="invite" CommandArgument='<%#DataBinder.Eval(Container,"rowindex") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img width="15" height="15" alt="close" src="images/icon-close.png" runat="server"
                                            visible='<%# (Eval("IsJoined").ToString()=="False") %>' />
                                        <asp:LinkButton ID="lbtnDelete" runat="server" Visible='<%# (Eval("IsJoined").ToString()=="False") %>'
                                            Text="DELETE" CommandName="deleterow" CommandArgument='<%#DataBinder.Eval(Container,"rowindex") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </li>
                </ul>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <uc1:SeeFrInvitations ID="SeeFriendsInvitaions" runat="server" />
            </div>
            <div class="content-inner-right">
                <div class="blue-box-company">
                    How you added more new contacts to your email/social accounts recently?
                    <div class="button-green-company" align="center">
                        <a href="InviteFriends.aspx" class="button-green button-green-company1">Sync Now</a><br />
                        <br />
                        To send invitation to your new contacts
                    </div>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <contenttemplate>
                            <uc12:pplUMayKnow runat="server"  ID="ucpplYouMayKnow"/>
                     </contenttemplate>
                </asp:UpdatePanel>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <uc2:FriendsToInvite ID="FrInvite" runat="server" />
                <p class="margin-top-visible">
                    &nbsp;</p>
                <uc3:AccountsGlance ID="YourAccts" runat="server" />
                <p class="margin-top-visible">
                    &nbsp;</p>
                <uc4:CVStats ID="cvStatistics" runat="server" />
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
