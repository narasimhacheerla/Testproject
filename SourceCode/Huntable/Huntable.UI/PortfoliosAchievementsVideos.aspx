<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PortfoliosAchievementsVideos.aspx.cs"
    Inherits="Huntable.UI.PortfoliosAchievementsVideos" %>

<%@ Register Src="~/UserControls/Portfolio.ascx" TagName="portfolio" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Styles/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <link href="Styles/PagerStyles.css" rel="Stylesheet" type="text/css" />
    <link href="JqueryPopup/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/counter.css" />
    <link rel="stylesheet" href="fancybox/source/jquery.fancybox.css?v=2.0.6" type="text/css"
        media="screen" />
    <link rel="stylesheet" href="fancybox/source/helpers/jquery.fancybox-buttons.css?v=1.0.2"
        type="text/css" media="screen" />
    <link rel="stylesheet" href="fancybox/source/helpers/jquery.fancybox-thumbs.css?v=2.0.6"
        type="text/css" media="screen" />
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" language="javascript">
        function showVideo() {
            var url = $('#txtVideoUrl').val();
            if (url !== "") {
                if (!(url.indexOf("youtube") > 0 || url.indexOf("vimeo") > 0 || url.indexOf("youtu.be") > 0)) {
                    alert("Please provide a valid Youtube/Vimeo video link.");
                    return;
                }
                if (url.indexOf("youtube") > 0) {
                    var v = getParameterByName(url, "v");
                    if (v === "") {
                        alert("Please provide a valid Youtube video link");
                        return;
                    }
                    url = "http://www.youtube.com/embed/" + getParameterByName(url, "v");
                }
                if (url.indexOf("youtu.be") > 0) {
                    var v = url.split('/')[url.split('/').length - 1];
                    if (v === "") {
                        alert("Please provide a valid Youtube video share link");
                        return;
                    }
                    url = "http://www.youtube.com/embed/" + v;
                }
                $('#divVideoPreview').hide();
                $('#imgVideoThumb').hide();
                $('#iFrameVideoView').attr('src', url);
                $('#hdnVedioUrl').val(url);
                $('#iFrameVideoView').show();
            }
        }
        function getParameterByName(url, name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(url);
            if (results == null)
                return "";
            else
                return decodeURIComponent(results[1].replace(/\+/g, " "));
        }
        function validate() {
            var uploadcontrol = $('#filePhotoUpload').val();
            if (uploadcontrol === "") {
                alert("Please choose a file!");
                return false;
            }
            //Regular Expression for fileupload control.
            var allowedexts=".gif.GIF.jpg.JPG.png.PNG.JPEG.jpeg";
            if (uploadcontrol.length > 0 && uploadcontrol.indexOf('.')>0) {
                var fullfilename = uploadcontrol.split('.');
                var fileext = fullfilename[fullfilename.length - 1];
                //Checks with the control value.
                if(allowedexts.indexOf(fileext)>0)
                {
                    return true;
                }
                else {
                    //If the condition not satisfied shows error message.
                    $('#filePhotoUpload').val('');
                    alert("Only .GIF,.JPG,.PNG files are allowed!");
                    return false;
                }
            }
        } //End of function validate.
        $('#lnkAddPoto2').on("click", function () { return validate(); });
    </script>
</head>
<body style="margin:0px;">
    <form id="form1" runat="server">
    <div class="upload-iframe">
        <table width="450px">
            <tr>
                <td valign="top">
                    <label>
                        Portfolio</label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <%--<asp:Panel runat="server" ID="pnlPortfolio">
                                </asp:Panel>--%>
                                <div style="width:375px;overflow:scroll;">
                                <asp:DataList ID="dlportfolio" runat="server" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                <div style="text-align:center;"><asp:ImageButton runat="server" ID="close" ImageUrl="images/cancel.jpg" Width="10px" Height="10px" OnClick="DeleteClick" CommandArgument='<%#Eval("FileId")%>' /></div>
                                <asp:Image runat="server" ID="imgportfolio" Height="91px" Width="79px" ImageUrl='<%#GetPortfolioPictureDisplayUrl(Eval("FileId")) %>' />
                                </ItemTemplate>
                                </asp:DataList>
                                </div>
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
                                        <strong style="width: 250px; float: left;">Supported file types:GIF,JPEG,PNG</strong>
                                        <br />
                                       <%-- <asp:ImageButton BorderWidth="0px" OnClientClick="return validate();" BorderStyle="None" ID="imgAddPortfolio" runat="server"
                                            OnClick="AddPortfolioClick" ImageUrl="images/add-icon-withtext.png" />--%>
                                        
                                            <asp:Button BorderWidth="0px" OnClientClick="return validate();" BorderStyle="None" ID="imgAddPortfolio" runat="server" 
                                            OnClick="AddPortfolioClick"  Text="+ Add picture" ForeColor="#0CAEC6" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <label>
                        Video</label>
                </td>
                <td>
                   <%-- <asp:Panel ID="pnlVideo" runat="server">
                    </asp:Panel>--%>
                    <asp:DataList runat="server" ID="dl_videos" >
                    <ItemTemplate>
                     <div style="text-align:center;"><asp:ImageButton runat="server" ID="btn_del" ImageUrl="images/cancel.jpg" height="15px" width="10px" OnClick="del_Video" CommandArgument='<%#Eval("Id")%>' />
                    </div>
                    <iframe id="if_video" runat="server" src='<%#Eval("VideoURL") %>' width="279px" height="230"></iframe>
                    </ItemTemplate>
                   <SeparatorTemplate>
                   <br />
                   </SeparatorTemplate>
                    </asp:DataList>
                    <div class="add-picture">
                        Share link to a YouTube, Vimeo video about you<br />
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <input id="txtVideoUrl" runat="server" type="text" class="textbox" value="e.g: Video URL"
                                        onfocus="if (this.value ==&#39;e.g: Video URL&#39;) {this.value =&#39;&#39;;}"
                                        onblur="if (this.value == &#39;&#39;) {this.value =&#39;e.g: Video URL&#39;;} else {showVideo();}" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSaveVideo" runat="server" Text="Save" OnClick="AddVideoClick"
                                        CssClass="button-green button-green-upload" ValidationGroup="GeneralDetails" />
                                </td>
                            </tr>
                        </table>
                        <img id="imgVideoThumb" src="images/video-img.png" width="279" height="230" alt="video" />
                        <iframe runat="server" id="iFrameVideoView" style="display: none;" width="279" height="230" frameborder="0">
                        </iframe>
                        <div id="divVideoPreview">
                        </div>
                        <br />
                        <asp:ImageButton BorderWidth="0px" BorderStyle="None" ID="imgAddVideo" runat="server"
                            ImageUrl="images/add-icon-withtext.png" OnClick="AddVideoClick" />
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <label>
                        Achievement</label>
                </td>
                <td>
                    <asp:Panel ID="pnlAchievement" runat="server">
                    </asp:Panel>
                    <textarea id="txtAchievement" runat="server" class="textbox" rows="4" cols="25" style="width: 290px;"
                        onblur="if(this.value==&#39;&#39;)this.value=this.defaultValue;" onfocus="if(this.value==this.defaultValue)this.value=&#39;&#39;;">Say something about your achievement in no more than 140 characters</textarea>
                    <br />
                    <asp:ImageButton BorderWidth="0px" BorderStyle="None" ID="imgAddAchievement" OnClick="AddAchievementClick"
                        runat="server" ImageUrl="images/add-icon-withtext.png" />
                </td>
                <%--<td>
                    <asp:Button ID="btnSaveAcievement" runat="server" Text="Save" OnClick="AddAchievement"
                        CssClass="button-green button-green-upload" ValidationGroup="GeneralDetails" />
                </td>--%>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <div class="upload-inner upload-inner1" style="width: 320px;">
                        <asp:Button runat="server" ID="btnSaveChanges" CssClass="button-green button-green-upload "
                            Text="Save Changes" OnClick="SaveChangesClick" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
<input type="hidden" id="hdnVedioUrl" runat="server" />
    </form>
</body>
</html>
