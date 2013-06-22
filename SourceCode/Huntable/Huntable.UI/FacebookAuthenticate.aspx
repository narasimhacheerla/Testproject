<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FacebookAuthenticate.aspx.cs" Inherits="Huntable.UI.FacebookAuthenticate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
    	<div id="content-inner">
        	<div class="content-inner-left" style="width:980px;">
            <h3 class="login-heading1" style="text-align:center; border:none; font-size:24px; font-family:Georgia, 'Times New Roman', Times, serif; color:#399FAE;">With just one click, let your friends know what you do Professionally </h3>
            <div style="text-align:center; width:100%; margin-bottom:10px;"><img src="images/social/facebook_authenticate2.png" alt=" " width="424" height="113" border="0" usemap="#Map" />
              <map name="Map" id="Map">
                <area shape="rect" coords="0,1,108,112" href="#" />
              </map><br />
</div>
<div style="margin:0 auto; float:none; width:660px; overflow:hidden;">
              <div style="float:left; width:655px; text-align:right; margin-bottom:10px;">
                  <asp:ImageButton ID="lbtnConnect" 
                      ImageUrl="images/social/connect_facebook.png"  runat="server" 
                      onclick="lbtnConnect_Click"></asp:ImageButton>
<br />
Post your feeds instantly to you Facebook
              </div>
              <div class="profile-box-main">
<ul class="list-green list-green-jobs">
            	<li>Grow your Network </li>
                <li>Get more Followers</li>
                <li>Let the world know what you do professionally</li>
            </ul><br />
 <div style="font-size:14px; line-height:24px; padding:2%; font-weight:bold; float:left; width:92%; margin:0px 2%; border:1px solid #DDDDDD; color:#399FAE; text-align:center; background:#fff;">Link  your Facebook account and get more Followers &amp; grow your network…<br />
With just one click </div>
<div style="font-size:14px; line-height:24px; font-weight:bold; float:left; width:100%; margin:2%; color:#399FAE; text-align:center;">
            <a href="#" class="button-green floatright" style="float:none;">Find out more...</a></div>
            </div>
            </div>
       	     
           
            </div><!-- content inner left ends -->
            <!-- content inner right ends -->
        </div><!-- content inner ends -->
    </div>
</asp:Content>
