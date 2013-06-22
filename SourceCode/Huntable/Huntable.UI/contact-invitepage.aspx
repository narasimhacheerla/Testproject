<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="contact-invitepage.aspx.cs" Inherits="Huntable.UI.contact_invitepage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
       
        .ui-dialog {
            position:absolute;
            overflow:hidden
        }      

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div id="content-section">
    	<div class="email_contacts email_contacts_invite">
        <div class="email_contact_inner">
        <div class="email_contact_head profile-box-main">
        <h1>Great! You're almost there...</h1>
        <h2>Click 'Invite' to continue</h2>
         <asp:Button ID="btnConnect" Text="Invite &raquo;" runat="server" onclick="lbtnInvite_Click" CssClass="invite-email"></asp:Button>
        <br />
<h3>or<br />
<span style="color:#666; font-size:12px; font-weight:normal;">Manually select contacts...</span></h3>
        </div>
              
        </div>
         <a  href="#" onclick="jQuery('.skipdialog').dialog('open');" class="nyroModal invite-friend-btn" style="float:left; margin-top:10px; font-weight:normal; font-size:11px;">Skip</a>
        </div><!-- content inner ends -->
    </div>
    <div>
    </div>
    <div class="skipdialog">
        <div class="email_contact_head profile-box-main" style="background: none; border: none;">
            <h2 style="line-height: 24px; margin-bottom: 15px; margin-top: 40px;">
                Don't miss out on the opportunity to grow your<br />
                Network with this simple step</h2>
            <a href="#" onclick="closedailog();" class="invite-email">Invite &raquo;</a> <a class="nyroModalClose nyroModalCloseButton" onclick="skipdailog();"
                title="close" href="/HomePageAfterLoggingIn.aspx" style="color: #f00; font-size: 12px; background: none;
                                                                                                                                                                                                                                                                                                               font-weight: normal;">Skip Away &raquo;&rsaquo;</a>
        </div>
    </div>
    <script type="text/javascript" src="../js1/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js1/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript">
        function closedailog() {
            jQuery('.skipdialog').dialog('close');
            <%= Page.ClientScript.GetPostBackEventReference(this.btnConnect, string.Empty) %>;
              
        };

        function skipdailog() {
            jQuery('.skipdialog').dialog('close');
        };

            Sys.Application.add_load(function () {
                $('.skipdialog').dialog(
                {
                    height: "auto",
                    width:"680px",
                    autoOpen: false,
                    modal: true
                });
            });
            

        </script>
</asp:Content>
