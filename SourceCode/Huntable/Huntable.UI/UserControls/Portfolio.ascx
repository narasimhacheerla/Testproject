<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Portfolio.ascx.cs" Inherits="Huntable.UI.Controls.Portfolio" %>
<div class="add-picture">
                        <div class="add-image " style="margin: 5px 20px 20px 0px;">
                            <a runat="server" onclick="OnAddPhotoClick" id="lnkAddPoto2" class="add-image-link">
                                <img src="images/add-photo-thumb_caption.jpg" class="profile-pic" width="79" height="91"
                                    alt="add-photo" /></a>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <asp:FileUpload ID="filePortfolio" runat="server" /><br />
                            <strong style="width: 250px; float: left;">Supported file types:GIF,JPG,PNG</strong>
                        </div>
                    </div>