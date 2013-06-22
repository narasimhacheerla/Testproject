<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PeopleYouMayKnowHorizontalDisplay.ascx.cs"
    Inherits="Huntable.UI.UserControls.PeopleYouMayKnowHorizontalDisplay" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
<div class="content-inner-right">
    <div class="box-right" style="width: 940px; margin-top:12px">
        <%--<div style="width: 932px; height: 36px; background: -webkit-gradient(linear,left top,left bottom,color-stop(0%,#fdfdfd), color-stop(100%,#cdcdcd));
            text-shadow: 0px 1px 1px #fff; color: #343434; display: block; padding-top: 8px;
            padding-left: 8px;">
            <h3 style="font-size: 23px; font-weight: bold;">
                People you can Follow</h3>
        </div>--%>
         <div class="head-ash" style="padding: 1%; padding-right: 46px;">
            <h3>
                People you can follow</h3>
        </div>
        <br />
        <asp:UpdatePanel ID="Update_Panel" runat="server">
            <ContentTemplate>
                <asp:DataList runat="server" ID="dlPeopleYouMayKnow" RepeatColumns="3" RepeatDirection="Horizontal"
                    CellSpacing="1" CellPadding="1" >
                    <ItemTemplate>
                        <div id="Div1" runat="server" style="width: 314px; height: 90px">
                            <a href='<%#UrlGenerator(Eval("Id")) %>' runat="server">
                                <asp:Image ID="Image1" ImageUrl='<%#Picture(Eval("Id"))%>' runat="server"
                                    Width="100px" Height="50px" /></a>
                            <div style="margin-left: 120px; margin-top: -54px; font-size: 12px ; height: 45px">
                                <a href='<%#UrlGenerator(Eval("Id")) %>' runat="server">
                                    <asp:Label ID="Label1" class="accounts-link" runat="server" Text='<%#Eval("FirstName") %>'
                                        Style="font-weight: bold;"></asp:Label></a><br />
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("JobTitle") %>'></asp:Label><br />
                                <%--<asp:Label ID="Label3" runat="server" Text='<%#Eval("Description") %>'></asp:Label>--%><br />
                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("City") %>'></asp:Label>
                            </div>
                            <div id="Div2" visible='<%#!IsThisUserFollowing(Eval("Id"))%>' runat="server" style="margin-right: 14px;
                                margin-top: 7px;">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("Id")%>' CausesValidation="False"
                                    OnClick="FollowupClick" class="invite-friend-btn">
                                                Follow +</asp:LinkButton></div>
                            <%--<div id="Div3" class="floating" visible='<%#IsThisUserFollowing(Eval("Id"))%>' runat="server"
                            style="margin-left: 178px;">
                            <div style="margin-top: -42px; margin-left: -18px;">
                                <asp:LinkButton runat="server" OnClick="UnFollowClick" CommandArgument='<%#Eval("Id")%>'
                                    Width="48" Height="12" Text="UnFollow" Font-Size="11px" CssClass="invite-friend-btn invite-friend-btn1 follbtn"></asp:LinkButton></div>--%>
                        </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div class="floating" style="text-align: right; font-size: 13px; margin-top: -18px">
            <asp:LinkButton ID="refresh" runat="server" Text="Refresh" OnClick="RefreshClick" CausesValidation="False" Style="background: white;
                        border: white; font-size: 13px;  color: #008ca1;" ></asp:LinkButton> <a href="peopleyoucanfollw.aspx">View all</a>
        </div>
    </div>
</div>
