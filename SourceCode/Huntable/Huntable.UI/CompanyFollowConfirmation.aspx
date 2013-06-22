<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyFollowConfirmation.aspx.cs" Inherits="Huntable.UI.CompanyFollowConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <div id="content-section">
        <div runat="server" id="pnlAlreadyConfirmed" visible="false">
            <div id="content-inner">
                <div class="content-inner-left">
                    <div class="profile-box">
                        <div class="accounts-profile accounts-profile1 accounts-profile-picture">
                            <h2>
                                Company Follow Confirmation</h2>
                            <div class="style1">
                                <p class="style1" style="color: Red; font-size: 15px">
                                    <strong>You are Already Following the Company</strong></p>
                                <br />
                              
                              
                                <p style="color: Green">
                                    Sign In To See Updates of company  in Huntable </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" id="pnlSuccess" visible="false">
        <div id="content-section">
            <div id="content-inner">
                <div class="content-inner-left">
                    <div class="profile-box">
                        <div class="accounts-profile accounts-profile1 accounts-profile-picture">
                            <h2>
                               Company Follow Confirmation</h2>
                            <div class="style1">
                                <p class="style1" style="color: Green; font-size: 15px">
                                    <strong>You are Now Following the Company</strong></p>
                                <br />
                                
                              
                                <p style="color: Green">
                                    Sign In To Start Company Page in Huntable</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
