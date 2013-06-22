<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Facebookfriends.aspx.cs" Inherits="Huntable.UI.Facebookfriends" %>

<%@ Register Src="UserControls/YourAccountAtGlance.ascx" TagName="YourAccountAtGlance"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/InvitingFriends.ascx" TagPrefix="uc2" TagName="Invite" %>
<%@ Register Src="~/UserControls/ImportContacts.ascx" TagPrefix="uc3" TagName="import" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60623.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/style-innerpage.css" type="text/css" rel="Stylesheet" />
    <script src='http://connect.facebook.net/en_US/all.js'></script>
    <script type="text/javascript">
        FB.init({ appId: "307487099357078", status: true, cookie: true });
        $(function () {

            var msie6 = $.browser == 'msie' && $.browser.version < 7;
            if (!msie6) {
                var top = $('#bx28').offset().top;
                $(window).scroll(function (event) {
                    var y = $(this).scrollTop();
                    if (y >= 621) { $('#bx28').addClass('fixed'); }
                    else { $('#bx28').removeClass('fixed'); }
                });
            }
        });

        $(function () {

            //Binding click for Invite links

            $("a[data-id]").click(function () {

                var id = $(this).attr("data-id");
                var uri = "/Facebookfriends.aspx/SendInvite";
                var anchor = $(this);
                $.ajax({
                    data: "{id:" + id + "}",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    url: uri,
                    success: function (result) {

                        if (result.d) {
                            anchor.html("Invited");
                            anchor.unbind("click").click(function () {

                                alert("Already Invited!!");

                            });
                        }
                    }

                });

            });

        });

        function postToFeed(url,message, fbid) {
          
           var fn = function () {
               Sys.Extended.UI.ModalPopupBehavior.invokeViaServer('MainContent_mpeCustomize', false); 
               Sys.Application.remove_load(fn);
           }; 
            Sys.Application.add_load(fn); 
           

            var obj = {
                method: 'feed',
                to: fbid,
                redirect_uri: 'https://huntable.co.uk/Facebookfriends.aspx',
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
    
        function DefaultText(txt, evt) {

            if (txt.value.length == 0 && evt.type == "blur") {

                var field = document.getElementById('<%= hfMessage.ClientID %>');
                txt.value = field.value;
                //grey

                txt.style.color = "#BFC0BD";
            }

            if (evt.type == "focus") {

                txt.value = "";
                //Black

                txt.style.color = "black";
            }

        }
  
        function overlay(id) {
            el = document.getElementById('ovrly');
            $('#ovrly').show();

            $('#<%= pbl.ClientID %>').text(id);

        }
        $(document).ready(function () {

            $('#ximg').click(function () {
                $('#ovrly').hide();
                return false;
            });
        });

    </script>
    
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }
        .pnlBackGround
        {
            position: fixed;
            text-align: center;
            background-color: White;
            border: solid 3px black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div id="fb-root"></div>
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> 
  </div>
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="contacts-head">
                    <h3 style="float: left;">
                        Friends from your various networks and connections
                    </h3>
                </div>
                <div class="contacts-select-inner" style="width:635px;" >
                    <asp:Repeater ID="rptrFriends" OnItemDataBound="rptrFriends_OnItemDataBound" runat="server">
                        <ItemTemplate>
                            <div class="invite-friends-tab1" style="width:297px;height:171px;">
                                <img align="absmiddle" class="profile-pic" width="76" height="76" alt="Invite-friends-img"
                                    src='<%#Eval("ProfilePictureUrl")%>' />
                                <b>
                                    <%#Eval("Name")%></b>
                             
                                <asp:LinkButton ID="lbtnInvite" ToolTip='<%#Eval("UniqueId")%>' OnClick="lbtnInvite_OnClick"
                                    Enabled='<%#  (Eval("UniqueId").ToString() != "0")  %>' Text="Invite Friend +" CssClass="invite-friend-btn invite-friend-btn01 " 
                                    runat="server"></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="content-inner-right" style="height:885px">
                <div class="box-right">
                    <uc1:YourAccountAtGlance ID="YourAccountAtGlance1" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc2:Invite ID="inviteFriends" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
               <div id="bx28"><div class="box-right">
                    <uc3:import ID="impContacts" runat="server" />
                </div></div> 
            </div>
        </div>
        
          <div>
             <asp:Button ID="btnHiddenCustom" runat="Server" Style="display: none" />
             <asp:HiddenField ID="hfImageId" runat="server"/>
                <asp:ModalPopupExtender ID="mpeCustomize" runat="server" TargetControlID="btnHiddenCustom" PopupControlID="pnlCustom" CancelControlID="ibtnClose" BackgroundCssClass="modalBackground" 
                                        PopupDragHandleControlID="pnlCaption" Drag="true">
                </asp:ModalPopupExtender>
            <asp:Panel ID="pnlCustom" runat="server" CssClass="pnlBackGround" >
                
                <table>
                    <tr>
                        <td colspan="2" align="right">
                           <asp:ImageButton ID="ibtnClose" ImageUrl="images/close_pop.png" runat="server"/>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlCaption" runat="server" Style="cursor: hand; margin-bottom: 10px;" Height="29px" Font-Bold="True" ForeColor="#004000" HorizontalAlign="Center">
                        Customize Your Invitation</asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             Use this picture when my friends see the invitation<br/>
                             <img id="Img1" src="images/you-invited.jpg" width="100"  height="150" alt="You-Invited" runat="server" />
                        </td>
                        <td>
                             Your personalized message to your friend
                             <asp:TextBox ID="txtMessage" Width="100%" Rows="5"  TextMode="MultiLine" runat="server" onblur = "DefaultText(this, event);" onfocus = "DefaultText(this, event);" style =" color:#BFC0BD"></asp:TextBox>
                             <asp:HiddenField ID="hfMessage" runat="server"/>
                             <asp:RequiredFieldValidator runat="server" ID="rfvMessage" ControlToValidate="txtMessage" ValidationGroup="custom" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:FileUpload  ID="fuPhoto" runat="server"/>&nbsp;<asp:LinkButton ID="lbtnChangeImage" onclick="lbtnChangeImage_Click" runat="server">Change Picture</asp:LinkButton>
                        </td>
                        <td>
                             <asp:LinkButton ID="lbNoCustom" runat="server" onclick="lbNoCustom_Click">Don't use customized invitation</asp:LinkButton>&nbsp;<asp:Button 
                                 ID="btnCustomInvite" Text="Send Invite" runat="server" ValidationGroup="custom" CausesValidation="True" 
                                 onclick="btnCustomInvite_Click"/>
                        </td>

                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
     <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript">  </script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
