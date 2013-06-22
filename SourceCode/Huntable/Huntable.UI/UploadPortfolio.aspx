<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPortfolio.aspx.cs"
    Inherits="Huntable.UI.UploadPortfolio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
            <div>
                <div class="upload-inner upload-inner1">
                    <label>
                        Portfolio:</label>
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
                            <asp:FileUpload ID="filePhotoUpload" runat="server" /><br />
                            <strong style="width: 250px; float: left;">Supported file types:GIF,JPG,PNG</strong>
                        </div>
                    </div>
                    <strong><a href="" class="accounts-link">
                        <img src="images/add-icon.png" style="vertical-align: middle;" width="14" height="14"
                            alt="add" />Add</a></strong>
                    <asp:ImageButton ID="imgAddPortfolio" runat="server" OnClick="AddPortfolioClick"
                        CssClass="profile-pic" ImageUrl="images/add-icon.png" />
                    <table class="video-table">
                        <tbody>
                            <tr>
                                <td valign="top">
                                    <strong>Video</strong>
                                </td>
                                <td valign="top">
                                    <h3>
                                        Your Video</h3>
                                    Share link to a YouTube, Vimeo, Dailymotion or Google video about you<br>
                                    <br />
                                    <input type="text" class="textbox" value="e.g: Video URL" onfocus="if (this.value ==&#39;e.g: Video URL&#39;) {this.value =&#39;&#39;;}"
                                        onblur="if (this.value == &#39;&#39;) {this.value =&#39;e.g: Video URL&#39;;}" />
                                    &nbsp;&nbsp;&nbsp;<input type="button" value="Save" class="button-green" />
                                    <img src="images/video-img.png" width="279" height="230" alt="video" /><br />
                                    <strong><a href="#" class="accounts-link">
                                        <img src="images/add-icon.png" style="vertical-align: middle;" width="14" height="14"
                                            alt="add" />Add</a></strong>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <br />
                    <label>
                        Portfolio:</label>
                    <textarea class="textbox" rows="4" cols="25" style="width: 290px;" onblur="if(this.value==&#39;&#39;)this.value=this.defaultValue;"
                        onfocus="if(this.value==this.defaultValue)this.value=&#39;&#39;;">Say something about your achievement in no more than 140 characters</textarea>
                    <br />
                    <strong><a href="#" style="padding: 0px; display: inherit; margin-left: 200px; color: #08aac3;">
                        <img src="images/add-icon.png" style="vertical-align: middle;" width="14" height="14"
                            alt="add" />&nbsp;&nbsp;Add</a></strong>
                </div>
                <div class="upload-inner upload-inner1" style="width: 320px;">
                    <a href="#" class="button-green button-green-upload " style="float: right;">Save Changes</a>
                </div>
            </div>
    </form>
</body>
</html>
