<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PeopleYouMayKnow.ascx.cs"
    Inherits="Huntable.UI.UserControls.PeopleYouMayKnow" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
<div class="content-inner-right">
    <div class="box-right" style="width: 298px">
        <div class="head-ash">
            <h3>
                People you can follow</h3>
        </div>
        <asp:UpdatePanel ID="Update_Panel" runat="server">
            <ContentTemplate>
                <asp:DataList runat="server" ID="dlPeopleYouMayKnow" RepeatColumns="1" RepeatDirection="Horizontal"
                    CellSpacing="1" CellPadding="1">
                    <ItemTemplate>
                       &nbsp;&nbsp;
                        <div id="Div1" runat="server" style="width: 296px; height: 77px; border-bottom: 1px dotted #ccc; padding-left: 3px; margin-top: -16px ">
                            <a href='<%#UrlGenerator(Eval("Id")) %>'  runat="server">
                                <asp:Image ID="Image1" ImageUrl='<%#Picture(Eval("Id"))%>' runat="server"
                                    Height="71px" Width="68px" /></a>
                            <div style="margin-left: 75px; margin-top: -70px; font-size: 12px; height: 46px">
                                <a href='<%#UrlGenerator(Eval("Id")) %>'  runat="server">
                                    <asp:Label ID="Label1" class="accounts-link" runat="server" Text='<%#Eval("FirstName") %>'
                                        Style="font-weight: bold;"></asp:Label></a><br />
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("JobTitle") %>'></asp:Label><br />
                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("City") %>'></asp:Label>
                            </div>
                            <div id="Div2" visible='<%#!IsThisUserFollowing(Eval("Id"))%>' runat="server" style="margin-right: 5px;
                               ">
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("Id")%>'
                                    OnClick="FollowupClick" class="invite-friend-btn">
                                                Follow +</asp:LinkButton></div>
                            <%--<div id="Div3" class="floating" visible='<%#IsThisUserFollowing(Eval("Id"))%>' runat="server"
                            style="margin-left: 178px;">
                            <div style="margin-top: -42px; margin-left: -18px;">
                                <asp:LinkButton runat="server" OnClick="UnFollowClick" CommandArgument='<%#Eval("Id")%>'
                                    Width="48" Height="12" Text="UnFollow" Font-Size="11px" CssClass="invite-friend-btn invite-friend-btn1 follbtn"></asp:LinkButton></div>--%>
                        </div>
                      
                    </ItemTemplate>
                    <%--<SeparatorTemplate>
                        <hr style="width: 299px;  margin-left: -285px; border: 1px dotted #ccc;" />
                       
                    </SeparatorTemplate>--%>
                </asp:DataList>
                
                <div class="floating" style="text-align: right; font-size: 13px;">
                  <asp:LinkButton ID="refresh" runat="server" Text="Refresh" OnClick="RefreshClick" Style="background: white;
                        border: white; font-size: 13px;  color: #008ca1;"></asp:LinkButton>
                        
                    <a href="peopleyoucanfollw.aspx">View all</a>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
