<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PeopleYouCanFollw.aspx.cs" Inherits="Huntable.UI.PeopleYouCanFollw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
         function overlay(id) {
             el = document.getElementById('ovrly');
             $('#ovrly').show();

             $('#<%= pbl.ClientID %>').text(id);

         }
         $(document).ready(function () {

             $('#ximg').click(function () {
                 $('#ovrly').hide();
                 window.location.reload();
                 return false;
             });
         });
        </script>
        
<div id="ovrly" style="height: 30px; width: 100%; z-index: 2000; background-color: #FF9242;
            border: 1px solid #FF9242; border-radius: 2px; margin-top: 10px; display: none;">
            <div style="width: 980px; padding: 0px 10px; margin: 0px auto;">
                <div style="width: 30px; margin-left: 292px;">
                    <image src="images/tick.png" width="25px" height="25px"></image>
                </div>
                <div style="margin-top: -20px">
                    <asp:Label Style="color: White; width: 200px; margin-top: -40px; margin-left: 377px;"
                        ID="pbl" runat="server"></asp:Label></div>
                <div style="margin-left: 621px; margin-top: -13px">
                    <image id="ximg" src="images/orange-check-mark-md.png" width="10px" height="10px"></image>
                </div>
            </div>
        </div>   

    <br />
    <br />
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left content-inner-left-message" style="margin-left:-24px;">
                <b>
                    <h2 style="font-size: 26px; font-weight: 600;">
                        People you can follow</h2>
                </b>
                <h3>
                    Our suggested users you can Follow and grow your network...
                </h3>
                <hr style="width: 813px;" /><br/>
                <%--<asp:UpdatePanel ID="Update_Panel" runat="server">
                    <ContentTemplate>--%>
                        <asp:DataList runat="server" ID="dlPeopleYouMayKnow" RepeatColumns="3" RepeatDirection="Horizontal"
                            CellSpacing="1" CellPadding="1" OnItemDataBound="itembound">
                            <ItemTemplate>
                                <div runat="server" style="width: 255px; height: 100px">
                                   <a href='<%#UrlGenerator(Eval("Id")) %>' runat="server">
                                        <asp:Image ID="Image1" ImageUrl='<%#Picture(Eval("Id"))%>' runat="server"
                                            Width="90px" Height="50px" /></a>
                                    <div style="margin-left: 95px; margin-top: -54px; font-size: 12px; height:100%" >
                                       <a href='<%#UrlGenerator(Eval("Id")) %>' runat="server">
                                            <asp:Label ID="Label1" class="accounts-link" runat="server" Text='<%#Eval("FirstName") %>' style="font-weight: bold;"></asp:Label></a><br />
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("JobTitle") %>'></asp:Label><br />
                                        <%--<asp:Label ID="Label3" runat="server" Text='<%#Eval("Description") %>'></asp:Label>--%><br />
                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("City") %>'></asp:Label><br />
                                    </div>
                                   
                                        <div id="Div1" visible='<%#!IsThisUserFollowing(Eval("Id"))%>' runat="server" style="margin-top: -18px;">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("Id")%>'
                                                OnClick="FollowupClick" class="invite-friend-btn">
                                                Follow +</asp:LinkButton></div>
                                        <%-- <div id="Div2" class="floating" visible='<%#IsThisUserFollowing(Eval("Id"))%>' runat="server"
                            style="margin-left: 178px;">
                            <div style="margin-top: -42px; margin-left: -18px;">
                                <asp:LinkButton runat="server" OnClick="UnFollowClick" CommandArgument='<%#Eval("Id")%>'
                                    Width="48" Height="12" Text="UnFollow" Font-Size="11px" CssClass="invite-friend-btn invite-friend-btn1 follbtn"></asp:LinkButton></div>
                        </div>--%>
                                    
                                    <br />
                                </div>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <br />
                                <hr style="width: 267px; margin-top: 120px; margin-left: -256px;" />
                                <br />
                            </SeparatorTemplate>
                        </asp:DataList>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <div class="content-inner-right content-inner-right-message" style="text-align:right; margin-right:42px;">
                    <asp:Image ID="bimage" runat="server"  ImageUrl="images/basic-user-advert.gif"/>
                <asp:Image ID="pimage" runat="server"  ImageUrl="images/premium-user-advert.gif" />
                </div>
        </div>
    </div>
</asp:Content>
