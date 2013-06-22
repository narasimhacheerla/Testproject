<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Followers(6).ascx.cs" Inherits="Huntable.UI.UserControls.Followers_6_" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />


 <div class="r-heading"><h3 style="font-size:14px;"><asp:Label ID="lblCount" runat="server" ></asp:Label> Followers</h3></div>
              <ul class="r-followers" >
                 	<asp:Label runat="server" Text="No Followers to display" ID="lbl_foll"  Visible="false"></asp:Label>
            <asp:DataList ID="dlFollowers" runat="server" RepeatColumns="3" OnItemDataBound="itembound">
            <ItemTemplate>
            <li style="padding:5px 4px;">
               <a runat="server" id="a1" href="#" ><asp:Image ImageUrl='<%#FollowersPhoto(Container.DataItem) %>' ID="imgfollowers" runat="server" width="76" height="81" CssClass="profile-pic" /></a>
             </li></ItemTemplate>
            </asp:DataList>
                   
                    
               </ul><a class="learn-more" id ="A2" href="#" runat="server">See more</a>