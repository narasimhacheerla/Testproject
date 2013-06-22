<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Viewers_also_viewed.ascx.cs" Inherits="Huntable.UI.UserControls.Viewers_also_viewed" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
  <meta http-equiv="Content-Type" content="../text/html; charset=utf-8" />
   
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
 
    <link type="text/css" href="../css1/jquery-ui-1.8.23.custom.css" rel="stylesheet" />

     <div class="box-right" >
                <div class="head-ash" style="text-align:center;">
               	  <h3>Viewers also viewed</h3>
                  </div>
           <div class="list_carousel">
           <ul>
           <li style="width:280px; height:180px;" class="img-left" id="li01" runat="server">
           <img  class="profile-pic" width="124" height="70" alt="Feaured-logo"  runat="server" id="img0" visible="false" />
           <img  class="profile-pic" width="124" height="70" alt="Feaured-logo" visible="false" style="margin-left: 140px; margin-top: -77px;"  runat="server" id="img1"/>
           <img  class="profile-pic" width="124" height="70" alt="Feaured-logo"  runat="server" id="img2" visible="false"/>
           <img  class="profile-pic" width="124" height="70" alt="Feaured-logo"  runat="server" id="img3" visible="false" style="margin-left: 140px; margin-top: -77px;"/>
     
           </li>

           <li style="width:280px; height:180px;" class="img-left" id="li02" runat="server" visible="false">
           <img  class="profile-pic" width="124" height="70" alt="Feaured-logo"  runat="server" id="img4" visible="false" />
           <img  class="profile-pic" width="124" height="70" alt="Feaured-logo" visible="false" style="margin-left: 140px; margin-top: -77px;"  runat="server" id="img5"/>
           <img  class="profile-pic" width="124" height="70" alt="Feaured-logo"  runat="server" id="img6" visible="false"/>
           <img  class="profile-pic" width="124" height="70" alt="Feaured-logo" visible="false"  runat="server" id="img7" style="margin-left: 140px; margin-top: -77px;"/>
     
           </li>
           </ul>
           <div style="text-align:center;">
           <a href="#"><asp:Button runat="server" ID="btn_li01" Text="1" OnClick="btn_li01_Click" Visible="false" /></a>
           <asp:Button runat="server" ID="btn_li02" Text="2" OnClick="btn_li02_Click" Visible="false" />
           </div>

           </div>      
 