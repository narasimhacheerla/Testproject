<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompaniesFindFollowers.aspx.cs" Inherits="Huntable.UI.CompaniesFindFollowers" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60623.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<%@ Register Src="~/Controls/DataPager.ascx" TagPrefix="uc" TagName="DataPager" %>
<%@ Register src="UserControls/PeopleYouMayKnow.ascx" tagName="pplUMayKnow" tagPrefix="uc12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Huntable - The Professional Network</title>
<meta name="description" content="Huntable - The Professional Network" />
<meta name="keywords" content="The Professional Network" />
<link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />\
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.2/themes/base/jquery-ui.css" />
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="JqueryPopup/jquery-ui-1.7.2.custom.css"></script>
    <script type="text/javascript" src="css/jquery-ui.css"></script>
     <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.3.js"></script>   
      <script type="text/javascript" src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>
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
            });
    </script>
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
</head>

<body>

<!-- Header section ends -->

<!-- main menu ends -->
<div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
           <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>
<div id="content-section">
    	<div id="content-inner">
        	<div class="green-desc companies_find_msg">
            	Grow your network, Invite your Employees, friends and other influencers you may want to be followed<br />
<span>Get Followers to your company with just few clicks</span>
            </div>
        	<div class="content-inner-left">
            <div class="profile-box-main">
            <div class="profile-box-main-top">
            <div class="invite_friends_employees">
            <span class="invite_title">Enter your Friends or Employees email address to Invite them to follow your company</span>
                <asp:TextBox ID="txtMailIDs"  class="textbox textbox-company" value="enter the email addresses separated by comma" onfocus="if (this.value =='enter the email addresses separated by comma') {this.value ='';}" onblur="if (this.value == '') {this.value ='enter the email addresses separated by comma';}" ValidationGroup="invitefriends" name="email" style="width:400px;" runat="server"/>
                <asp:Button ID="Button1" ValidationGroup="invitefriends" Text="Send Invite" runat="server" OnClick="BtnInviteByEmailAddressesClick" class="button-orange "  ></asp:Button>
             <%-- <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtMailIDs"
                            Display="Dynamic" ForeColor="Red" Text="Please enter email"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtMailIDs"
                            Display="Dynamic" ForeColor="Red" Text="Please enter email in correct format"
                            ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*([ ,])*)*"></asp:RegularExpressionValidator>--%></div>
            <div class="invite_friends_employees">
            <div class="box-right" >
                  <p class="import-contact-desc">
              <strong> Look through your E-mail &amp; social contacts</strong>
                  </p>
                 <div class="social-icon">
                 <a href="#" title="Facebook">
                                <asp:ImageButton ID="ibtnFacebook" runat="server" Width="34" Height="34" alt="Facebook"
                                    ImageUrl="images/facebook.jpg" OnClick="IbtnFacebookClick" CausesValidation="false" />
                            </a><a href="#" title="Linked in">
                                <asp:ImageButton ID="ibtnLinkedIn" runat="server" ImageUrl="images/linkedin.jpg"
                                    Width="34" Height="34" alt="Linkedin" OnClick="IbtnLinkedInClick" CausesValidation="false" />
                            </a><a href="#" title="Twitter">
                                <asp:ImageButton ID="ibtnTwitter" runat="server" ImageUrl="images/twitter.jpg" Width="34"
                                    Height="34" alt="Gmail" OnClick="IbtnTwitterClick" CausesValidation="false" />
                            </a><a href="#" title="Gmail">
                                <asp:ImageButton ID="ibtnGoogle" runat="server" ImageUrl="images/gmail.jpg" Width="34"
                                    Height="34" alt="Gmail" OnClick="IbtnGoogleClick" CausesValidation="false" /></a>
                            <a href="#" title="yahoo">
                                <asp:ImageButton ID="ibtnYahoo" runat="server" Width="34" Height="34" alt="yahoo"
                                    ImageUrl="images/yahoo.png" OnClick="IbtnYahooClick" CausesValidation="false" /></a>
                            <a href="#" title="Msn">
                                <asp:ImageButton ID="ibtnLive" runat="server" Width="34" Height="34" alt="Msn" ImageUrl="images/msn.jpg"
                                    OnClick="IbtnLiveClick" CausesValidation="false" /></a>
              </div>
                   
              </div>
              <div class="box-right invite_right" >
              <div class="invite_friends_employees">
            <img src="images/excel-icon.png" width="30" height="32" alt="Excel" title="Excel" /><strong>Import your contacts from a CSV file</strong>
                  </div>
                  <div class="invite_friends_employees" style="text-align:right;">
           <a href="#?w=500" class="button-orange poplight" rel="popup9" >Import</a>
                  </div>
                   
                 
                   
              </div>
               </div>
             </div> 
               
                 
                </div>
        	  <div class="profile-search">
                    <b>Search for User:</b><asp:TextBox runat="server" ID="txtTopSearchKeyword" CssClass="textbox-search textbox-search-inner textbox-search-customize"
                        name="email" /><a runat="server" id="btnSearchTopKeyword" class="button-orange button-orange-search">Search<img
                            alt="arrow" src="HuntableImages/search-arrow.png" width="22" height="23"></a></div> 
                 <div class="notification notification-user">
                    <div class="notification-left">
                        <asp:TextBox ID="txtSearchSkills" runat="server" onblur="if (this.value == '') {this.value ='Skills';}"
                            CssClass="textbox textbox-search1" onfocus="if (this.value =='Skills') {this.value ='';}"
                            value="Skills" />
                        <asp:ImageButton ID="btnSkillsSearch" OnClick="BtnSkillsSearchClick" ImageUrl="HuntableImages/search-img.png"
                            Width="28" Height="27" runat="server" />
                        <br/>
                        <br/>
                        <asp:TextBox ID="txtSearchKeywords" runat="server" onblur="if (this.value == '') {this.value ='Keywords';}"
                            CssClass="textbox textbox-search1" onfocus="if (this.value =='Keywords') {this.value ='';}"
                            value="Keywords" />
                        <asp:ImageButton ID="btnKeywordSearch" OnClick="BtnKeywordSearchClick" ImageUrl="HuntableImages/search-img.png"
                            Width="28" Height="27" runat="server" />
                        <asp:TextBox ID="txtSearchFirstName" runat="server" onblur="if (this.value == '') {this.value ='First name';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='First name') {this.value ='';}"
                            name="email" value="First name" />
                        <asp:TextBox ID="txtSearchLastName" runat="server" onblur="if (this.value == '') {this.value ='Last name';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Last name') {this.value ='';}"
                            name="email" value="Last name" />
                        <asp:TextBox ID="txtTitle" runat="server" onblur="if (this.value == '') {this.value ='Title';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Title') {this.value ='';}"
                            name="email" value="Title" />
                        <select class="textbox selectbox-inner">
                            <option>Current or Past</option>
                            <option>Job Type 1</option>
                            <option>Job Type 2</option>
                        </select>
                        <asp:TextBox ID="txtSearchCompany" runat="server" onblur="if (this.value == '') {this.value ='Company';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Company') {this.value ='';}"
                            name="email" value="Company" />
                        <asp:TextBox ID="txtSearchSchool" runat="server" onblur="if (this.value == '') {this.value ='School';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='School') {this.value ='';}"
                            name="email" value="School" />
                        <asp:TextBox ID="txtSearchExp" runat="server" onblur="if (this.value == '') {this.value ='Experience';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Experience') {this.value ='';}"
                            name="email" value="Experience" />
                        <asp:TextBox ID="txtSearchYear" runat="server" onblur="if (this.value == '') {this.value ='Year';}"
                            CssClass="textbox textbox-inner" onfocus="if (this.value =='Year') {this.value ='';}"
                            name="email" value="Year" />
                        <br>
                        <br>
                        <b>Available Now</b>
                        <asp:DropDownList ID="ddlAvailableNow" runat="server" CssClass="textbox selectbox-inner">
                            <asp:ListItem Text="Yes" Selected="True" />
                            <asp:ListItem Text="No" />
                        </asp:DropDownList>
                        <br>
                        <br>
                        <b>Country</b>
                        <asp:DropDownList ID="ddlSearchCountry" runat="server" CssClass="textbox selectbox-inner" />
                        <asp:Button ID="Button2" runat="server" Text="Search" CssClass="button-green button-green-jobpost"
                            OnClick="BtnSearchClick" />
                    </div>
                    <div class="notification-right">
                        <ul class="user-list">
                            <asp:DataList ID="lstUsers" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                <ItemTemplate>
                                    <li><a href="ViewUserProfile.aspx">
                                            <img id="Img2" class="profile-pic profile-pic-user" alt="User" runat="server" src='<%#Eval("UserProfilePictureDisplayUrl")%>'
                                                 width="46" height="45"/></a> <a class="accounts-link"  href='<%#UserUrlGenerator(Eval("Id")) %>'>
                                                <%#Eval("Name")%></a><br>
                                        <%#Eval("CurrentPosition")%>,
                                        <%#Eval("CurrentCompany")%><br>
                                        <strong>Location:</strong>
                                        <%#Eval("CountryName")%>
                                        <div id="Div1" class="floating" visible='<%#!IsThisUserFollowingCompany(Eval("Id"))%>' runat="server">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("Id")%>' OnClick="FollowupClick"
                                                class="invite-friend-btn">
                                                Invite+</asp:LinkButton></div>
                                        <div id="Div2" class="floating" visible='<%#IsThisUserFollowingCompany(Eval("Id"))%>' runat="server">
                                        <asp:Image runat="server" Width="20" Height="20" ID="Following" ImageAlign="Right"
                                            ImageUrl="images/tick.png" />
                                           <%-- <asp:Label runat="server" Text="Following Now"></asp:Label>--%>
                                        </div>
                                        
                                        </div> </li>
                                </ItemTemplate>
                            </asp:DataList>
                        </ul>
                        <div class="pagination pagination-user">
                            <uc:DataPager ID="pagerUsers" runat="server" PageIndex="1" RecordsPerPage="10" TotalRecords="0" />
                        </div>
                    </div>
                </div>
              
            </div><!-- content inner left ends -->
            <div class="content-inner-right" runat="server" id="pplYouMayKnowDiv">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <uc12:pplUMayKnow runat="server" />  
                    </ContentTemplate>
                </asp:UpdatePanel>
                                         
           <%--<div class="box-right" >
                <div class="head-ash">
               	  <h3>People you can follow</h3>
                  </div>
                  <ul class="user-list companies_followers">
           	<li>
       	     <a href="#"><img src="images/profile-thumb-small.jpg" width="46" height="45" alt="User" class="profile-pic profile-pic-user" /></a>
             <div class="com_folow">
             <a href="#" class="accounts-link">Angelena Malik</a><br />
             Owner, Angela Malik Ltd<br />
             United Kingdom
             </div>
           
             <a href="#" class="invite-friend-btn" style="float:left;">Follow +</a>
            
              </li>
              <li>
       	     <a href="#"><img src="images/profile-thumb-small.jpg" width="46" height="45" alt="User" class="profile-pic profile-pic-user" /></a>
             <div class="com_folow">
             <a href="#" class="accounts-link">Angelena Malik</a><br />
             Owner, Angela Malik Ltd<br />
             United Kingdom
             </div>
           
             <a href="#" class="invite-friend-btn" style="float:left;">Follow +</a>
            
              </li>
              <li>
       	     <a href="#"><img src="images/profile-thumb-small.jpg" width="46" height="45" alt="User" class="profile-pic profile-pic-user" /></a>
             <div class="com_folow">
             <a href="#" class="accounts-link">Angelena Malik</a><br />
             Owner, Angela Malik Ltd<br />
             United Kingdom
             </div>
           
             <a href="#" class="invite-friend-btn" style="float:left;">Follow +</a>
            
              </li>
           </ul>
           <div class="floating">
             <a href="#">Refresh</a> <a href="#">View all</a>
             </div>
                
                 
              </div>--%>
          </div><!-- content inner right ends -->
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
        </div><!-- content inner ends -->
    </div>
<!-- content section ends --> 
<!-- Range Slider Script Begins -->
<!--<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>-->
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js" type="text/javascript"></script>
<script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
<script type="text/javascript" src="js/flipcounter.js"></script>
<!-- Range Slider Script Ends -->


 
    
    


<!-- Footer section ends -->

   <!--viewers views gallery script begins -->

<script type="text/javascript" language="javascript" src="js/jquery.carouFredSel-6.0.3-packed.js"></script>

<!-- optionally include helper plugins -->
<script type="text/javascript" language="javascript" src="js/jquery.mousewheel.min.js"></script>
<script type="text/javascript" language="javascript" src="js/jquery.touchSwipe.min.js"></script>

<!-- fire plugin onDocumentReady -->

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
         $("#amountin").val($("#slider").slider("value"));
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

    </script>
        
<!--viewers views gallery script ends -->   <!-- Footer section ends -->
</body>
</html>

</asp:Content>
