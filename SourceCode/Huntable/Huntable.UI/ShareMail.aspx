<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareMail.aspx.cs" Inherits="Huntable.UI.ShareMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="css/style-innerpage.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        
    </style>
    <script type="text/javascript">
        function geturl() {
            document.getElementById('twitter').href = 'https://twitter.com/share?url=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value) + '&summary=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value);
            document.getElementById('linkedin').href = 'http://www.linkedin.com/shareArticle?mini=true&url=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value) + '&summary=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value);
            document.getElementById('facebook').href = 'http://www.facebook.com/sharer.php?u=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value) + '&summary=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value);
        }
        function getTxt() {
            document.getElementById('<%=txtShareMessage.ClientID %>').value =  window.window.top.location;
            document.getElementById('<%=txtMessage.ClientID %>').value = window.window.top.location;
           
            document.getElementById('<%=fe_text.ClientID %>').value =  window.window.top.location;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="popup7" class="active">
        <div class="apply-job">
            <div id="tabswitch">
                <ul class="tabswitch-share">
                    <li class="tab1">Copy Url </li>
                    <li class="tab2">Share by E-mail </li>
                    <li class="tab3">Share to Social </li>
                </ul>
                <div class="tab-container tab-container-share tab1">
                    <table class="login-table-share">
                        <tr class="social-share">
                            <td width="30%" align="right" valign="top">
                                <label>
                                    You Can Share this content Using the URL</label>
                            </td>
                            <td width="70%" valign="top">
                                <br />
                                <asp:TextBox runat="server" ID="fe_text" TextMode="MultiLine" cols="10" Rows="4"
                                    class="textbox textbox-share"></asp:TextBox><br />
                            </td>
                        </tr>
                        <tr>
                            <td width="30%" valign="top">
                                &nbsp;
                            </td>
                            <td width="70%" valign="top">
                                <asp:Button ID="d_clip_button" runat="server" Text="Copy To Clipboard..." class="button-green" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tab-container tab-container-share tab2">
                    <table class="login-table-share">
                        <tr>
                            <td width="30%" align="right" valign="top">
                                <label>
                                    To:</label>
                            </td>
                            <td width="70%" valign="top">
                                <asp:TextBox ID="txtTo" runat="server" class="textbox textbox-share"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="rfvTo" runat="server" ForeColor="Red" ErrorMessage="Please enter Email Id"
                                    ControlToValidate="txtTo" ValidationGroup="mail"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="30%" align="right" valign="top">
                                <label>
                                    Add Your Message Here:</label>
                            </td>
                            <td width="70%" valign="top">
                                <asp:TextBox ID="txtMessage" runat="server" ValidationGroup="mail" TextMode="MultiLine"
                                    class="textbox textbox-share" cols="10" Rows="4"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="30%" valign="top">
                                &nbsp;
                            </td>
                            <td width="70%" valign="top">
                                <asp:Button ID="txtSharebyEmail" ValidationGroup="mail" runat="server" class="button-green"
                                    Text="Share" OnClick="txtSharebyEmail_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tab-container tab-container-share tab3">
                    <table class="login-table-share">
                        <tr>
                            <td width="30%" align="right" valign="top">
                                <label>
                                    Add Your Message Here:</label>
                            </td>
                            <td width="70%" valign="top">
                                <asp:TextBox ID="txtShareMessage" class="textbox textbox-share" TextMode="MultiLine"
                                    cols="10" Rows="4" runat="server" onchange="geturl()">Add a Message</asp:TextBox>
                            </td>
                        </tr>
                        <tr class="social-share">
                            <td width="30%" align="right" valign="top">
                                <label>
                                    Select Where to Share:</label>
                            </td>
                            <td width="70%" valign="top">
                                <p>
                                    <asp:CheckBox ID="chkTwitter" runat="server" OnCheckedChanged="chkTwitter_CheckedChanged"/>
                                    <a id="twitter" href="#" title="Twitter Share Button">
                                        <img src="images/twitter.png" width="20" height="20"></a>
                                    <asp:CheckBox ID="chkLinkedIn" runat="server" OnCheckedChanged="chkLinkedIn_CheckedChanged"/><a id="linkedin" href="#">
                                        <img src="images/linkedin.png" alt="linkedin share button" width="20" height="20"
                                            title="LinkedIn Share Button" /></a><asp:CheckBox ID="chkFacebook" runat="server" OnCheckedChanged="chkFacebook_CheckedChanged"/>
                                    <a id="facebook" href="#" title="Facebook Share Button">
                                        <img src="images/facebook.jpg" width="20" height="20"></a>
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td width="30%" valign="top">
                                &nbsp;
                            </td>
                            <td width="70%" valign="top">
                                <asp:Button ID="btnShare" runat="server" Text="Share" class="button-green" OnClick="btnShare_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="pop" runat="server" />
    <script type="text/javascript" src="js/ZeroClipboard.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            geturl();
            getTxt();
            jQuery.noConflict();
            var clip = new ZeroClipboard.Client();
            jQuery("#tabswitch ul li:first").addClass("active");
            jQuery("#tabswitch div.tab-container:first").show();
            jQuery("#tabswitch ul li").click(function () {
                jQuery("#tabswitch div.tab-container").hide();
                jQuery("#tabswitch ul li").removeClass("active");
                var tab_class = jQuery(this).attr("class");
                jQuery("#tabswitch div." + tab_class).show();
                jQuery("#tabswitch ul li." + tab_class).addClass("active");
            });

            clip.setText(''); // will be set later on mouseDown
            clip.setHandCursor(true);
            clip.addEventListener('load', function (client) {
                //  alert("movie is loaded");
            });
            clip.addEventListener('complete', function (client, text) {
                //  alert("Copied text to clipboard: " + text);
            });
            clip.addEventListener('mouseDown', function (client) {
                // set text to copy here
                clip.setText(document.getElementById('fe_text').value);
                //    alert("Copied text to clipboard: " + text);
            });
            
            clip.glue('d_clip_button');
            geturl();
            getTxt();
        });
    </script>
    </form>
</body>
</html>
