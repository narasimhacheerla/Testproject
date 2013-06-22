<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangeEmail.aspx.cs" Inherits="Huntable.UI.ChangeEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
<script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
<div id="content-section">
    	<div id="content-inner">  
        	<div class="general-setting-tab">
            	<h3 class="setting-active" style=" float: left;width: 200px; font-size: 20px; font-family: 'GalantBold'; text-shadow: 0px 1px 1px white; color: #747474; margin-right: 10px;
                                                     padding: 7px 10px; text-align: center; border: 1px solid #CCC; border-radius: 4px 4px; background: -webkit-gradient(linear,left top,left bottom,color-stop(0%,#FBFBFB), color-stop(100%,#CECECE));">General Settings</h3>
                <a href="UserEmailNotification.aspx">E-mail Notifications</a>
            </div> 
            <div class="general-setting">       
               <ul class="general-setting-list1">
               	<li class="general-setting-list1-head"><strong>E-mail</strong><a href="ChangeEmailSettings.aspx" class="button-ash floatright">Change</a></li>
                <li><p>Your Contact E-mail</p><a href="#"><asp:Label ID="lblemail" runat="server" ></asp:Label></a></li>
               </ul> 
               <ul class="general-setting-list1">
               
               	<li class="general-setting-list1-head"><strong>Password</strong><a href="ChangeEmailSettings.aspx" class="button-ash floatright">Change</a></li>
                <li><p>Your Login Password</p><a href="#"><asp:Label ID="lblpassword" runat="server" Visible="false"></asp:Label></a> <div style="margin-left:553px"><asp:Label ID="dtpass" runat="server" Text = "*********" ></asp:Label></div> 
                    <div style="margin-left: 503px;
margin-top: 8px;"><asp:Button Text="show password" runat="server" ID="shwpsd" class="button-ash"
                        onclick="shwpsd_Click"  /></div></li>
               </ul>
            </div> 
                	        
        	
        </div><!-- content inner ends -->
    </div>
<!-- content section ends --> 
</asp:Content>
