<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SendInvitations.aspx.cs" Inherits="Huntable.UI.SendInvitations" %>

<%@ Register TagPrefix="uc1" TagName="YourAccountAtGlance" Src="~/UserControls/YourAccountAtGlance.ascx" %>
<%@ Register TagPrefix="uc2" TagName="Invitation" Src="~/UserControls/InvitingFriends.ascx" %>
<%@ Register TagPrefix="uc3" TagName="Import" Src="~/UserControls/ImportContacts.ascx" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60623.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        function changeAllCheckBoxes(sender) {
            var gridViewRows = GetParentElementByTagName(sender, "TABLE").rows;
            for (var i = 1; i < gridViewRows.length; ++i) {
                gridViewRows[i].cells[0].childNodes[0].checked = sender.checked;
            }
        }

        function GetParentElementByTagName(element, tagName) {
            var element = element;
            while (element.tagName != tagName)
                element = element.parentNode;
            return element;
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


    </script>
    <style type="text/css">
.modalBackground
{
    background-color:Gray;
    filter:alpha(opacity=50);
    opacity:0.7;
}
.pnlBackGround
{
 position:fixed;  
    text-align:center;
    background-color:White;
    border:solid 3px black;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="contacts-head">
                    <h3 style="float: left;">
                        Friends from your various networks and connections
                    </h3>
                    <a href="#" class="learn-more">View all sent invitations</a>
                </div>
               <%-- <div class="contacts-select">
                    <span class="floatleft">
                    <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select all" 
                        TextAlign="Right" CssClass="checkbox" 
                        oncheckedchanged="chkSelectAll_CheckedChanged" ViewStateMode="Enabled" AutoPostBack="true" />
                        <input type="checkbox" class="checkbox" onclick="changeAllCheckBoxes(this)" />
                        Select all</span> <a href="#" class="button-green" style="float: right;">Invite all
                            +</a>
                </div>--%>
                <div class="contacts-select-inner">
                    <div class="invite-friends-tab">
                        <asp:GridView Width="320px" ID="gvInvitations" AutoGenerateColumns="False" runat="server"
                            DataKeyNames="Name,Email" GridLines="None" 
                            OnRowCommand="GVInvitationsRowCommand" 
                            onrowdatabound="gvInvitations_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                     <HeaderTemplate>
                                      <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select all" 
                        TextAlign="Right" CssClass="checkbox" 
                        oncheckedchanged="chkSelectAll_CheckedChanged" ViewStateMode="Enabled" AutoPostBack="true" />
                                     </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="checkbox1" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" />
                                <asp:BoundField DataField="Email" />
                                <asp:TemplateField>
                                     <HeaderTemplate>
                                    <asp:Button ID="btnInviteAll" runat="server" class="button-green" style="float: right;" Text="Invite +" CommandName="inviteall"
                                        CommandArgument='<%#DataBinder.Eval(Container,"rowindex") %>' />
                                </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Button ID="btnInvite" runat="server" Text="Invite Friend +" CssClass="invite-friend-btn"
                                            CommandName="invite"  CommandArgument='<%#DataBinder.Eval(Container,"rowindex") %>' /><br /><br /><br />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                   
                </div>
                <a href="#" class="show-more show-more-margin">Show More</a>
            </div>
            <!-- content inner left ends -->
            <div class="content-inner-right">
                <uc1:YourAccountAtGlance ID="Accts" runat="server" />
                <p class="margin-top-visible">
                    &nbsp;</p>
                <uc2:Invitation ID="InvtFr" runat="server" />
                <p class="margin-top-visible">
                    &nbsp;</p>
                <uc3:Import ID="ImpContacts" runat="server" />
            </div>
            <!-- content inner right ends -->
        </div>
        <div>
             <asp:Button ID="btnHiddenCustom" runat="Server" Style="display: none" />
             <asp:HiddenField ID="hfImageId" runat="server"/>
                <asp:ModalPopupExtender ID="mpeCustomize" runat="server" TargetControlID="btnHiddenCustom"
                                        PopupControlID="pnlCustom" CancelControlID="ibtnClose" BackgroundCssClass="modalBackground" 
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
                             <asp:TextBox ID="txtMessage" Width="100%" Rows="5"  TextMode="MultiLine" runat="server" onblur = "DefaultText(this, event);" onfocus = "DefaultText(this, event);" style =" color:#BFC0BD">
                               
                             </asp:TextBox>
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
        <!-- content inner ends -->
        <!-- content section ends -->
        <!-- Range Slider Script Begins -->
        <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
            type="text/javascript"></script>
        <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
        <!-- Range Slider Script Ends -->
    </div>
</asp:Content>
