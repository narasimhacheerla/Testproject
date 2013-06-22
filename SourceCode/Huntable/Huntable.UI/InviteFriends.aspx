<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="InviteFriends.aspx.cs" Inherits="Huntable.UI.InviteFriends" %>
 <%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60623.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
 <%@ Register src="UserControls/PeopleYouMayKnowHorizontalDisplay.ascx" tagName="pplUMayKnow" tagPrefix="uc12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">
        function overlay(id) {
            el = document.getElementById('ovrly');
            $('#ovrly').show();

            $('#<%= pbl.ClientID %>').text(id);

        }
        $(document).ready(function () {

            $('#ximg').click(function () {
                $('#ovrly').hide();
                return false;
            });

            $('#dialog').dialog({
                autoOpen: false,
                modal: true,
                bgiframe: true,
                title: "Confirm Message",
                width: 500,
                height: 200,
                buttons: {
                            "Ok": function () {
                                            
                                $(this).dialog("close");
                            }
                }
            });
        });

        function ConfirmLogin() {
            var userid = document.getElementById('<%=hdnUserId.ClientID %>').value;
            if (userid == "") {
                $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
                $('#dialog').dialog('open');
                return false;
            } else {

             
                return true;
            }

        }
    </script>
    
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.2/themes/base/jquery-ui.css" />
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="JqueryPopup/jquery-ui-1.7.2.custom.css"></script>
    <script type="text/javascript" src="css/jquery-ui.css"></script>
     <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.3.js"></script>   
      <script type="text/javascript" src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>
      <style type="text/css">
.modalBackground
{
    background-color:Gray;
    filter:alpha(opacity=50);
    opacity:0.7;
}
.pnlBackGround
{
 position:fixed;  
    text-align:center;
    background-color:White;
    border:solid 3px black;
}
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
       <asp:HiddenField ID="hdnUserId" runat="server" />
        <div id="dialog" title="Confirm Message">
        <asp:Label ID="lblConfirmMessage" runat="server" />
    </div>
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <div id="content-section">
        <div id="content-inner">
            <div class="invite-friends-main">
                <div class="invite-friends-left" style="font-family: Georgia">
                    <h2>
                        How many Friends do you have?</h2>
                    <p style="font-family: Georgia">
                       You can even earn money by inviting your friends to join Huntable! Once they’ve
signed up as a premium member, you’ll receive the equivalent of $4. You can invite
as many as you like – there’s no limit to the amount you can earn. You’ll even receive
funds if your friend then goes on to sign up another person!<br /><br />

It’s simple, fast – and it’s a great way to increase your revenue from the comfort of
your own home.<br /><br /></p>
                </div>
              
               <div class="invite-friends-right">
                    <div class="click" >
                        join our affiliate and earn money-with just 4click                     
                        <br />
                        <br />
                            <span style="font-size: 13px; font-weight: bold;">Total friends You have :</span>
                         <br />
                             <div id="slider"  class="Dragval" style="float: left; width: 200px;">                                                        
                           <input type="text" class="Output"  readonly="readonly"  id="amountin"  style="float:left; margin-right:80px; margin-top:-17px;"/>
                          <%-- <div>
                                <input type="text" value="90" class="Output1" />
                            </div>--%>                      
                        </div>                      
                        <div class="block-one">
                            <br />
                            This is how much you can get!
                            <input  type="text" class="Output1" readonly="readonly"  id="amountin1" style="margin-top:-30px;"/>
                           
                            <br />
                            <strong style="color: #ff8c3d;">You have nothing to lose</strong>
                        </div>
                    </div>
                </div>
            
        
          
            <div class="invite-bottom-main">
                <div class="get-start-invite">
                    <iframe frameborder="0" class="profile-pic" width="169" height="144" src="http://player.vimeo.com/video/65019649">
                    </iframe>
                    <%--<img src="images/laptop.jpg" width="169" height="144" alt="laptop" />--%>
                    <a id="getStartedHere" runat="server" class="button-green button-green-invite">Get Started Here</a>
                    <%--<a href="#" class="accounts-link accounts-link-invite">Learn More&nbsp;&nbsp;<img src="images/see-more-arrow.png"
                            width="4" height="7" alt="arrow" /></a>--%>
                </div>
                <div class="how-it-work-block">
                    <h3>
                        This is How it Works!</h3>
                    <ul class="connection-list connection-list-invite">
                        <li><a href="https://huntable.co.uk/InvitationsOverview.aspx">1st Connection</a>
                            <p>
                                You get<strong> $4 </strong>per connection</p>
                        </li>
                        <li><a href="https://huntable.co.uk/InvitationsOverview.aspx">2nd Connection</a>
                            <p>
                                Your get <strong>$1</strong><br />
                                You Friend gets <strong>$4</strong> per connection
                            </p>
                        </li>
                        <li><a href="https://huntable.co.uk/InvitationsOverview.aspx">3rd Connection</a>
                            <p>
                                Your get<strong> $0.5,</strong>
                                <br />
                                Your friend gets <strong>$1</strong>,
                                <br />
                                Your Friends friend gets<strong> $4</strong> per connection</p>
                        </li>
                    </ul>
                </div>
                <div class="invite-fields">
                    <div class="invite-fields1">
                        <strong>Please Enter Your Friend's E-mail Here. (Separate E-mail with a comma)</strong><br />
                        <br />
                        <asp:TextBox type="text" runat="server" ID="txtMailIDs" class="textbox float-block"
                            ValidationGroup="invitefriends" />
                        <asp:Button ID="Button1" ValidationGroup="invitefriends" Text="Send Invite" runat="server"
                            CssClass="button-green float-block poplight" rel="popup2" Style="margin-left: 10px;"
                            OnClick="BtnInviteByEmailAddressesClick" /><br />
                        <br />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtMailIDs"
                            Display="Dynamic" ForeColor="Red" Text="Please enter email"></asp:RequiredFieldValidator><br />
                        <br />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtMailIDs"
                            Display="Dynamic" ForeColor="Red" Text="Please enter email in correct format"
                            ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([ ,])*)*"></asp:RegularExpressionValidator>
                    </div>
                    <div class="invite-fields3">
                        <h2>
                            Look through your E-mail &amp; social contacts</h2>
                        <div class="social-icon" style="padding-left: 0px;">
                            <a href="#" title="Facebook">
                                <asp:ImageButton ID="ibtnFacebook" OnClientClick="return ConfirmLogin();" runat="server" Width="34" Height="34" alt="Facebook"
                                    ImageUrl="images/facebook.jpg" OnClick="IbtnFacebookClick" CausesValidation="false" />
                            </a><a href="#" title="Linked in">
                                <asp:ImageButton ID="ibtnLinkedIn" OnClientClick="return ConfirmLogin();" runat="server" ImageUrl="images/linkedin.jpg"
                                    Width="34" Height="34" alt="Linkedin" OnClick="IbtnLinkedInClick" CausesValidation="false" />
                            </a><a href="#" title="Twitter">
                                <asp:ImageButton ID="ibtnTwitter" OnClientClick="return ConfirmLogin();" runat="server" ImageUrl="images/twitter.jpg" Width="34"
                                    Height="34" alt="Gmail" OnClick="IbtnTwitterClick" CausesValidation="false" />
                            </a><a href="#" title="Gmail">
                                <asp:ImageButton ID="ibtnGoogle" OnClientClick="return ConfirmLogin();" runat="server" ImageUrl="images/gmail.jpg" Width="34"
                                    Height="34" alt="Gmail" OnClick="IbtnGoogleClick" CausesValidation="false" /></a>
                            <a href="#" title="yahoo">
                                <asp:ImageButton ID="ibtnYahoo" OnClientClick="return ConfirmLogin();" runat="server" Width="34" Height="34" alt="yahoo"
                                    ImageUrl="images/yahoo.png" OnClick="IbtnYahooClick" CausesValidation="false" /></a>
                            <a href="#" title="Msn">
                                <asp:ImageButton ID="ibtnLive" OnClientClick="return ConfirmLogin();" runat="server" Width="34" Height="34" alt="Msn" ImageUrl="images/msn.jpg"
                                    OnClick="IbtnLiveClick" CausesValidation="false" /></a>
                        </div>
                        <p class="margin-top-visible">
                            &nbsp;</p>
                        <div class="floatleft" style="margin-top: 20">
                            <img src="../images/excel.png" width="35" height="35" alt="Excel format" />&nbsp;&nbsp;Import
                            your contacts from a CSV file<br />
                            <a href="#?w=500" class="button-orange floatleft poplight" rel="popup9" style="float:right;">Import </a>
                        </div>
                    </div>
                </div>
                <br/><br />
                <div style="margin-right: 679px; margin-top:12px" runat="server" id="pplYoumayKnow" >
                <asp:UpdatePanel runat="server">
                <ContentTemplate>
                 <uc12:pplUMayKnow ID="PplUMayKnow1" runat="server"></uc12:pplUMayKnow>
                </ContentTemplate>
                </asp:UpdatePanel>
                   
                </div>
            </div>
            
        </div>
       
     </div>
      
    <div id="popup9" class="popup_block">
        <div class="apply-job">
            <h2>
                Upload .csv file only in the format shown here</h2>
            <asp:FileUpload runat="server" ID="fuInvitationFriends" class="textbox float-block" /><br />
            <br />
            <asp:Button runat="server" CssClass="button-orange floatleft" ID="btnUpload" CausesValidation="false"
                Text="Upload File" OnClick="UploadInvites" />
            <div style="float: left; clear: both; margin-top: 20px; width: 100%;">
            <table class="csv-table">
  <tr>
   
    <th valign="top">First Name</th>
    <th valign="top">E-mail Address</th>
  </tr>
  <tr>
    
    <td valign="top">Christopher</td>
    <td valign="top">christopher@gmail.com</td>
  </tr>
  <tr>
    
    <td valign="top">James</td>
    <td valign="top">james@gmail.com</td>
  </tr>
  <tr>
    
    <td valign="top">Christopher</td>
    <td valign="top">christopher@gmail.com</td>
  </tr>
  <tr>
  
    <td valign="top">James</td>
    <td valign="top">james@gmail.com</td>
  </tr>
</table>
               <%-- <a href="#" class="learn-more" style="margin-right: 0px; font-size: 11px; margin-top: -4px;">
                    Cant't find your social network. Click here to see more</a>--%>
            </div>
        </div>
    </div>
     <div>
             <asp:Button ID="btnHiddenCustom" runat="Server" Style="display: none" />
             <asp:HiddenField ID="hfImageId" runat="server"/>
                <asp:ModalPopupExtender ID="mpeCustomize" runat="server" TargetControlID="btnHiddenCustom"
                                        PopupControlID="pnlCustom" CancelControlID="ibtnClose" BackgroundCssClass="modalBackground" 
                                        PopupDragHandleControlID="pnlCaption" Drag="true">
                </asp:ModalPopupExtender>
            <asp:Panel ID="pnlCustom" runat="server" CssClass="pnlBackGround" >
                
                <table>
                    <tr>
                        <td colspan="2" align="right">
                           <asp:ImageButton ID="ibtnClose" ImageUrl="images/close_pop.png" runat="server"/>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlCaption" runat="server" Style="cursor: hand; margin-bottom: 10px;" Height="29px" Font-Bold="True" ForeColor="#004000" HorizontalAlign="Center">
                        Customize Your Invitation</asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             Use this picture when my friends see the invitation<br/>
                             <img id="Img1" src="images/you-invited.jpg" width="100"  height="150" alt="You-Invited" runat="server" />
                        </td>
                        <td>
                             Your personalized message to your friend
                             <asp:TextBox ID="txtMessage" Width="100%" Rows="5"  TextMode="MultiLine" runat="server" onblur ="DefaultText(this, event);" onfocus ="DefaultText(this, event);" style =" color:#BFC0BD">
                               
                             </asp:TextBox>
                             <asp:HiddenField ID="hfMessage" runat="server"/>
                             <asp:RequiredFieldValidator runat="server" ID="rfvMessage" ControlToValidate="txtMessage" ValidationGroup="custom" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:FileUpload  ID="fuPhoto" runat="server"/>&nbsp;<asp:LinkButton ID="lbtnChangeImage" onclick="lbtnChangeImage_Click" runat="server">Change Picture</asp:LinkButton>
                        </td>
                        <td>
                             <asp:LinkButton ID="lbNoCustom" runat="server" onclick="lbNoCustom_Click">Don't use customized invitation</asp:LinkButton>&nbsp;<asp:Button 
                                 ID="btnCustomInvite" Text="Send Invite" runat="server" ValidationGroup="custom" CausesValidation="True" 
                                 onclick="btnCustomInvite_Click"/>
                        </td>

                    </tr>
                </table>
               
             
               
               
            </asp:Panel>
        </div>
    <!-- Popup Script Begins -->
       <script type="text/javascript">
           $(function () {
               $("#slider").slider({
                   range: "min",
                   value: 200,
                  
                   min: 1,
                   max: 700,
                   slide: function (event, ui) {
                       $("#amountin").val(ui.value);
                       $("#amountin1").val("$" + ui.value * 5);
                       $("#slider").children("div").css("background", "#D77D00");
                   }

               });
               $("#amountin").val( $("#slider").slider("value"));
               $("#amountin1").val("$" + $("#slider").slider("value"));
              
           });
           $("a.expand").click(function () {
               $(this).parent().children(".toggle").slideToggle(200);
               return false;
           });
           function DefaultText(txt, evt) {

               if (txt.value.length == 0 && evt.type == "blur") {

                   var field = document.getElementById('<%= hfMessage.ClientID %>');
                   txt.value = field.value;
                   //grey

                   txt.style.color = "#BFC0BD";
               }

               if (evt.type == "focus") {

                   txt.value = "";
                   //Black

                   txt.style.color = "black";
               }

           }
    </script>  
    <script type="text/javascript">
        $(document).ready(function () {

            //When you click on a link with class of poplight and the href starts with a # 
            $('a.poplight[href^=#]').click(function () {
                var popID = $(this).attr('rel'); //Get Popup Name
                var popURL = $(this).attr('href'); //Get Popup href to define size

                //Pull Query & Variables from href URL
                var query = popURL.split('?');
                var dim = query[1].split('&');
                var popWidth = dim[0].split('=')[1]; //Gets the first query string value

                //Fade in the Popup and add close button
                $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="images/close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>');

                //Define margin for center alignment (vertical + horizontal) - we add 80 to the height/width to accomodate for the padding + border width defined in the css
                var popMargTop = ($('#' + popID).height() + 80) / 2;
                var popMargLeft = ($('#' + popID).width() + 80) / 2;

                //Apply Margin to Popup
                $('#' + popID).css({
                    'margin-top': -popMargTop,
                    'margin-left': -popMargLeft
                });

                //Fade in Background
                $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
                $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); //Fade in the fade layer 

                return false;
            });


            //Close Popups and Fade Layer
            $('a.close, #fade').live('click', function () { //When clicking on the close or fade layer...
                $('#fade , .popup_block').fadeOut(function () {
                    $('#fade, a.close').remove();
                }); //fade them both out

                return false;
            });


        });

    </script></div>
    <!-- Popup Script Ends -->
</asp:Content>

