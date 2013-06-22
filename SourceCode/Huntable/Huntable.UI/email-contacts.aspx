<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="email-contacts.aspx.cs" Inherits="Huntable.UI.email_contacts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
    	<div class="email_contacts" style="background:none;">
        <div class="profile-box-main">
        <div class="email_contact_head">
        <h1>Just one more click…<br />
          <span style="font-size:22px;">To get your friends to follow you </span></h1>
        <h2>Click 'Invite' to continue</h2>
        <asp:LinkButton ID="lbtnInvite" runat="server" Text="Invite &raquo;" CssClass="invite-email"
            onclick="lbtnInvite_Click"></asp:LinkButton>
        </div>
        <div class="contacts-select-inner email_contact_list">
              <asp:GridView ID="gvInvitations" AutoGenerateColumns="False" runat="server"
                            DataKeyNames="Name,Email" GridLines="None" >
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" Checked="True" runat="server" CssClass="checkbox1" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Email" />
                            </Columns>
                        </asp:GridView>
         </div>
         <div class="email_contact_head">
             <asp:LinkButton ID="lbtnInvite2" runat="server" Text="Invite &raquo;" CssClass="invite-email"
            onclick="lbtnInvite_Click"></asp:LinkButton>
            </div>
        </div>
        </div><!-- content inner ends -->
    </div>
</asp:Content>
