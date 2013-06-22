<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="VisualCvActivity.aspx.cs" Inherits="Huntable.UI.VisualCvActivity" %>
    <%@ Register Src="~/UserControls/UserMessagepopup.ascx" TagName="_msgPopup" TagPrefix="uc4" %>

<%@ Register Src="~/UserControls/UserFeedList.ascx" TagName="UserFeedList" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/UserPhotoLikes_Right.ascx" TagName="UserPhotoLikes_Right"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdnUserId" runat="server" />
     <asp:HiddenField ID="hdnOtherUserId" runat="server" />
    <div id="dialog" title="Confirm Message">
        <asp:Label ID="lblConfirmMessage" runat="server" />
    </div>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Huntable - The Professional Network</title>
        <meta name="description" content="Huntable - The Professional Network" />
        <meta name="keywords" content="The Professional Network" />
        <link href="https://huntable.co.uk/css/style-innerpage.css" type="text/css" rel="stylesheet" />
        <link rel="stylesheet" type="text/css" href="https://huntable.co.uk/css/skin2.css" />
        <link rel="stylesheet" type="text/css" href="https://huntable.co.uk/css/skin3.css" />
         <script src="https://huntable.co.uk/Scripts/fancybox/lightbox1.js" type="text/javascript"></script>
            <script src="https://huntable.co.uk/Scripts/fancybox/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="https://huntable.co.uk/Scripts/fancybox/lightbox1.js" type="text/javascript"></script>
    <link href="https://huntable.co.uk/Scripts/fancybox/lightbox1.css" rel="stylesheet" />
    <link href="https://huntable.co.uk/Scripts/fancybox/lightbox1.css" rel="stylesheet" />
    <script type="text/javascript">

    
        function MarkDirectLike(feedId, likeType, refId) {
            if (CheckIfUserLoggedIn()) {
                $.ajax({
                    type: "POST",
                    url: "/UserFeedService.asmx/MarkDirectLike",
                    data: '{'
                        + '"feedId":"' + feedId + '",'
                        + '"type":"' + likeType + '",'
                        + '"refRecordId":"' + refId + '"'
                        + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    success: function (msg) {
                        var likeDetail = msg.d;
                        $('#<%=hypLikeProfile.ClientID %>').attr("href", "javascript:" + likeDetail);
                        $('#<%=hypLikeProfile.ClientID %>').html("liked my profile");
                    },
                    error: function (msg) {
                    }
                });
            }
        }
    </script>
<script type="text/javascript">
    function MarkDirectUnlike(feedId, likeType, refId) {
        if (CheckIfUserLoggedIn()) {
            $.ajax({
                type: "POST",
                url: "/UserFeedService.asmx/MarkDirectUnlike",
                data: '{'
                    + '"feedId":"' + feedId + '",'
                    + '"type":"' + likeType + '",'
                    + '"refRecordId":"' + refId + '"'
                    + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (msg) {
                    var likeDetail = msg.d;
                    $('#<%=hypLikeProfile.ClientID %>').attr("href", "javascript:" + likeDetail);
                    $('#<%=hypLikeProfile.ClientID %>').html("like my profile");
                },
                error: function (msg) {
                }
            });
        }
    }
</script>
          <script type="text/javascript">
              function validate() {
                  var uploadcontrol = $('#MainContent_fp').val();
                  if (uploadcontrol === "") {
                      alert("Please choose a file!");
                      return false;
                  }
                  //Regular Expression for fileupload control.
                  var allowedexts = ".gif.GIF.jpg.JPG.png.PNG.JPEG.jpeg";
                  if (uploadcontrol.length > 0 && uploadcontrol.indexOf('.') > 0) {
                      var fullfilename = uploadcontrol.split('.');
                      var fileext = fullfilename[fullfilename.length - 1];
                      //Checks with the control value.
                      if (allowedexts.indexOf(fileext) > 0) {
                          return true;
                      }
                      else {
                          //If the condition not satisfied shows error message.
                          $('#MainContent_fp').val('');
                          alert("Only .GIF,.JPG,.PNG files are allowed!");
                          return false;
                      }
                  }
              }
              function overlay(id) {
                  var el = document.getElementById('ovrly');
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
           <script type="text/javascript">

               function rowAction22() {
                   $('#dialog11').dialog({

                       autoOpen: true,
                       modal: true,
                       width: 600,
                       open: function (event, ui) {
                           $(event.target).parent().css('position', 'fixed');
                           $(event.target).parent().css('top', '155px');

                       },
                       buttons: {
//                    "Ok": function () {
//                        
//                        $(this).dialog("close");
//                    },
//                    "Cancel": function () {
//                        $(this).dialog("close");
//                    }

                       }

                   });
                   return false;
               }

     
           </script>
  
        <!-- Fancy box popup picture style ends -->
        <style type="text/css">
            .jcarousel-skin-tango .jcarousel-container-horizontal
            {
                width: 750px;
            }
            .jcarousel-skin-tango .jcarousel-clip-horizontal
            {
                width: 580px;
                height: 195px;
            }
            .jcarousel-skin-tango .jcarousel-item-horizontal
            {
                margin-right: 19px;
            }
            .jcarousel-skin-tango .jcarousel-item
            {
                width: 136px;
                height: 165px;
            }
            .jcarousel-skin-tango .jcarousel-clip-horizontal
            {
                width: 747px;
            }
            .jcarousel-skin-tango .jcarousel-prev-horizontal, .jcarousel-skin-tango .jcarousel-next-horizontal
            {
                top: 75px;
            }
        </style>
        <%--<link rel="stylesheet" href="css/jquery.fancybox.css" type="text/css" media="screen" />--%>
        <link rel="stylesheet" type="text/css" href="https://huntable.co.uk/css/jquery.fancybox-picture.css?v=2.1.2"
            media="screen" />
    </head>
    <body>
        <!-- Header section ends -->
        <!--[if lte IE 6]>
<script type="text/javascript" >
//author: ?, I found this useful.
    startList = function() {
        if (document.all&&document.getElementById) {
            navRoot = document.getElementById("nav");
            for (i=0; i<navRoot.childNodes.length; i++) {
                node = navRoot.childNodes[i];
                if (node.nodeName=="LI") {
                    node.onmouseover=function() {
                        this.className+=" over";
                    };
                    node.onmouseout=function() {
                        this.className=this.className.replace(" over", "");
                    };
                }
            }
        }
    };
    window.onload=startList;

</script>
<![endif]-->
        <!-- main menu ends -->
     <div id="dialog11" style="display: none;" title="List Of Companies">
         <table>
         <asp:DataList runat="server" ID="dlListOfCompanies">
             <ItemTemplate>
                 <tr>
                     <td style="width: 235px;">
               <a href= '<%#CompanyUrlGenerator(Eval("Id") )%>' runat="server"> <asp:Label ID="Label1" runat="server" Text='<%#Eval("CompanyName") %>' style="color:  #008ca1;"></asp:Label> </a> </td>
                 <%--<asp:Label runat="server" Text='<%#Eval("CompanyWebsite") %>'></asp:Label>
                 <asp:Label ID="Label1" runat="server" Text='<%#Eval("CompanyEmail") %>'></asp:Label>--%>
               <td> <a href= '<%#CompanyUrlGenerator(Eval("Id") )%>' runat="server"> <asp:Image ID="Image1" ImageUrl='<%#Picturec(Eval("CompanyLogoId"))%>' runat="server" Width="119px"
                                        Height="62px" /></a> </td>
               </tr>
             </ItemTemplate>
             
         </asp:DataList>
         </table>
        <%-- <asp:Label id="TextBox1" runat="server" Text="22aaaaaaaaaaa"></asp:Label>  
         <asp:Image runat="server" ID="imgHasCompany0001" ImageUrl="https://huntable.co.uk/images/UserCompany.png" Width="33px" Height="35px" />--%> 
   </div>
        
  <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="https://huntable.co.uk/images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;margin-top:-13px"> <image id="ximg"  src="https://huntable.co.uk/images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>

        <div id="content-section">
            <div id="content-inner">
                <div class="content-inner-left">
                    <div class="tab-cv">
                        <a id="vc" runat="server" style="margin-left: 10px;">Visual</a> &nbsp&nbsp <a id="vt"
                            runat="server">Text</a> <a id="vca" runat="server" class="selectedcv" style="margin-left: 10px;">
                                Activity</a>
                    </div>
                    <div class="cv-top-left cv-top-left1">
                        
                         <a href="#" runat="server" id="showProfileImage12" rel="lightbox"><img runat="server" id="imgProfile" src="" class="profile-pic profile-pic-cv" width="76"
                            height="81" alt="Profile-pic" /></a> 
                        <div class="cv-top-left-inner" style="width: 553px;">
                            <h2>
                                <asp:Label class="accounts-link" ID="lblName" runat="server" Style="color: #008ca1;"></asp:Label></h2>
                            <strong>
                                <asp:Label ID="lblCurrentRoleProfileSection" class="accounts-link" runat="server"></asp:Label></strong><br />
                            <div id="divOnlineNoewChatWithMe" runat="server">
                                <a href="https://huntable.co.uk/VisualCV.aspx" class="green-color" style="margin-left: -52px;">
                                    <asp:ImageButton ID="userOnline" Width="10" Height="10" alt="green-circel" runat="server" /></a>
                                    <asp:Label ID="onlineinfo" runat="server"></asp:Label>
                                    &nbsp&nbsp<a href="#" class="blue-color blue-color-cv" id="a_chat" runat="server"
                                        onclick="return ChatWindow();"><img src="https://huntable.co.uk/images/chat-icon.png" width="17" height="16"
                                            alt="chat" />
                                        Chat with me</a>
                                    <asp:Button ID="flwr" runat="server" class="button-orange button-orange-follow" Text="Follow"
                                        Style="margin-top: 26px;" OnClick="FollowClick" />
                                    <asp:Button ID="flwngr" runat="server" Visible="False" class="button-orange button-orange-follow"
                                        Style="margin-top: 26px;" Text="Following" OnClick="UnfollowClick" />
                                      
                                    
                            </div>
                             <asp:ImageButton runat="server"   
                                     ID="imgHasCompany" ImageUrl="https://huntable.co.uk/images/UserCompany.png" Width="33px" Height="35px" OnClick="UserCompaniesClick"    />
                            <div class="cv-position-right" runat="server" style="margin-top: -54px" id="messageboxdiv">
                                        <img width="25" height="28" alt="like" src="https://huntable.co.uk/images/hand-thumb.png" />
                                        <div>
                                            <h2 >
                                                Like my profile <a style="font-size: 12px;" href="#">Contact me here</a></h2>
                                        </div>
                                       
                                         <p class="profile-subdiv" >
                            &nbsp;<div style="margin-left: -11px; margin-top: -17px;" runat="server" id="p_msg_like"><uc4:_msgPopup id="UcMessage" runat="server" /></div>
                        
                        
                            </p>
                        
                        
                            &nbsp;&nbsp;&nbsp;
                             <a href="#" class="button-orange profile-like" runat="server" id="hypLikeProfile" style="float: right;">like
my                             profile</a>
                       
                                    </div>
                              
                            <a href="AddVideos.aspx" runat="server" id="a_addvideos" class="button-ash floatright poplight" rel="popup15" style="margin-left: 20px;">
                                Add Videos</a> <a href="https://huntable.co.uk/AddPictures.aspx" runat="server" class="button-ash floatright poplight" id="a_addpictures"
                                    rel="popup14">Add Pictures</a>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />  <br /> <hr />
                        <br />
                        <div id="tellthe" runat="server" class="tell-the-world ">
                            <asp:TextBox Height="35px" Width="512px" ID="txtTellworld" Text="Got something to say, ask, post, share…"  
                            onfocus="SetMsg(this,true);" onblur="SetMsg(this, false);"  CssClass="textarea-profile"
                                runat="server"></asp:TextBox>
                                
                             

                            <asp:Label ID="lblurl" runat="server" Text="http://www.huntable.co.uk" Visible="false"></asp:Label>
                            <div class="slidingDiv slidingdiv-new" style="width: 522px; margin-left: -17px;">
                                <asp:FileUpload ID="fp" runat="server" />
                                <asp:Button ID="btnattach" Text="Save" OnClientClick="return validate();" runat="server" Style="
                                    width: 44px; height: 25px; background-color: #819FF7;" OnClick="Btnattachclick" />
                                <a href="#" class="attach-link show_hide" target="_self" style="float: right; margin-right: 48px;">
                                    X</a>
                            </div>
                            <a href="#" class="attach-link show_hide" target="_self">
                                <img src="https://huntable.co.uk/images/attach-link.png" width="12" height="12" alt="link" id="attachlink" />
                                Attach Picture </a>
                            <div class="social-utilites">
                                <asp:CheckBox ID="chkTwitter" AutoPostBack="True" runat="server" 
                                    oncheckedchanged="chkTwitter_CheckedChanged" />&nbsp;Twitter
                               <%-- <asp:CheckBox ID="chkgoogle" Enabled="False" runat="server" />&nbsp;Google+ &nbsp;--%>
                                <asp:CheckBox ID="chkFacebook" AutoPostBack="True" runat="server" oncheckedchanged="chkFacebook_CheckedChanged" />&nbsp;Facebook &nbsp;
                                <asp:CheckBox ID="chkLinkedIn" AutoPostBack="True" runat="server" oncheckedchanged="chkLinkedIn_CheckedChanged" />&nbsp;LinkedIn
                            </div>
                            <asp:Button ID="btnJoin" runat="server" Text="Tell the World" CssClass="button-green button-green-profile"
                                OnClick="BtnTellWorldClick" />
                        </div>
                     
                        <div id ="divaftretell" runat="server" ><uc1:UserFeedList ID="UserFeedList1" runat="server" PageType="Visual_CV_Activity" /></div>
                    </div></div>
                    <div class="content-inner-right" >
                        <div class="activity-img-block">
                            <h3>
                                <a runat="server" id="pictureslinktop">
                                    <asp:Label ID="lblpicturesCount" runat="server" />Pictures</a></h3>
                            <asp:DataList ID="dlpictures" RepeatDirection="Horizontal" RepeatColumns="2" runat="server">
                                <ItemTemplate>
                                    <div class="auto-adjust-imgblock">
                                        <ul>
                                            <li><a class="nyroModal" href="javascript:OpenMainPopup('/ajax-picture.aspx?UserPhotoId=<%# Eval("Id")%>')">
                                                <asp:Image class="profile-pic" ImageUrl='<%#Picture(Eval("PictureId"))%>' runat="server"
                                                    Width="100px" Height="107px" ID="rspimage" />
                                            </a></li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                            <a id="pictureslink" runat="server" class="accounts-link accounts-link-more" style="margin-left: 158px;">
                                &rsaquo;&rsaquo;&nbsp; See more</a>
                        </div>
                        <p class="margin-top-visible">
                            &nbsp;</p>
                        <div class="activity-img-block" id="followingdiv" runat="server">
                            <h3>
                                <a id="companylinktop" runat="server">Companies following
                                    <asp:Label runat="server" ID="lblCompanycount"></asp:Label></a></h3>
                            <asp:DataList runat="server" ID="dlComapnies" RepeatDirection="Horizontal" RepeatColumns="2">
                                <ItemTemplate>
                                    <div class="auto-adjust-imgblock">
                                        <a href='<%#UrlGenerator(Container.DataItem) %>' runat="server">
                                            <asp:Image class="profile-pic" ImageUrl='<%#CompanyPicture(Container.DataItem)%>'
                                                Width="109px" Height="64px" runat="server" ID="rspimage" /></a>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                            <a id="lblCompanyBottomClick" runat="server" class="accounts-link accounts-link-more"
                                style="margin-left: 164px;">&rsaquo;&rsaquo;&nbsp; See more</a>
                        </div>
                        <p class="margin-top-visible">
                            &nbsp;</p>
                        <div class="activity-img-block">
                            <h3>
                                <a id="followerstoplink" runat="server">
                                    <asp:Label runat="server" ID="lblFolowers"></asp:Label>
                                    Followers</a> &nbsp;&nbsp;<a id="followingtoplink" runat="server"><asp:Label runat="server"
                                        ID="lblfollowing"></asp:Label>
                                        Following</a>
                            </h3>
                            <asp:DataList runat="server" ID="dlfollowers" RepeatDirection="Horizontal" RepeatColumns="2" OnItemDataBound="Itemflwrsbound">
                                <ItemTemplate>
                                    <div class="auto-adjust-imgblock">
                                        <ul>
                                            <li><a    id="anchor2" runat="server">
                                                <asp:Image class="profile-pic" ImageUrl='<%#ProfilePicture(Container.DataItem)%>'
                                                    Width="100px" Height="107px" runat="server" ID="rspimage" />
                                            </a></li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                            <a id="followbottom" runat="server" style="margin-left: 148px;" class="accounts-link accounts-link-more">
                                &rsaquo;&rsaquo;&nbsp; See more</a>
                        </div>
                        <div class="activity-img-block" id="followersdiv" visible="False" runat="server">
                            <h3>
                                <a id="A1" runat="server">
                                    <asp:Label runat="server" ID="Label1"></asp:Label>
                                    Followers</a> &nbsp;&nbsp;<a id="A2" runat="server"><asp:Label runat="server" ID="Label2"></asp:Label>
                                        Following</a>
                            </h3>
                            <asp:DataList runat="server" ID="DataList1" RepeatDirection="Horizontal" RepeatColumns="2" OnItemDataBound="Itemflwngbound" >
                                <ItemTemplate>
                                    <div class="auto-adjust-imgblock">
                                        <ul>
                                            <li><a  id="anchor1" runat="server"  >
                                                <asp:Image class="profile-pic" ImageUrl='<%#ProfilePicture(Eval("UserId"))%>' Width="100px"
                                                    Height="107px" runat="server" ID="rspimage" />
                                            </a></li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                            <a id="A3" runat="server" class="accounts-link accounts-link-more">&rsaquo;&rsaquo;&nbsp;
                                See more</a>
                        </div>
                        <p class="margin-top-visible">
                            &nbsp;</p>
                        <uc2:UserPhotoLikes_Right ID="UserPhotoLikes_Right1" runat="server" />
                        <p class="margin-top-visible">
                            &nbsp;</p>
                        <div class="activity-img-block">
                            <h3>
                                <a id="videotoplink" runat="server">Videos</a></h3>
                            <asp:DataList runat="server" RepeatDirection="Horizontal" ID="dlvideos" RepeatColumns="2">
                                <ItemTemplate>
                                    <div class="auto-adjust-imgblock">
                                        <ul id="videos">
                                            <li>
                                                <iframe id="videourl01" runat="server" width="128" height="75px" class="profile-pic"
                                                    style="margin-bottom: 4px" frameborder="0" src='<%#Eval("VideoUrl") %>'></iframe>
                                            </li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                            <a id="videobottom" runat="server" style="margin-left: 184px;" class="accounts-link accounts-link-more">
                                &rsaquo;&rsaquo;&nbsp; See more</a>
                        </div>
                    </div>
                </div>
                <!-- content inner ends -->
            </div>
            <!-- content section ends -->
            <!-- Range Slider Script Begins -->
            <!--<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>-->
            <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
                type="text/javascript"></script>
            <script src="https://huntable.co.uk/js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
            <script type="text/javascript" src="https://huntable.co.uk/js/flipcounter.js"></script>
            <!-- Range Slider Script Ends -->
            <!-- Footer section ends -->
            <!-- Popup Div begins -->
            <div id="popup14" class="popup_block">
                <div class="apply-job ">
                    <input type="text" class="textbox-white" value="Give a name to your picture" onfocus="if (this.value =='Give a name to your picture') {this.value ='';}"
                        onblur="if (this.value == '') {this.value ='Give a name to your picture';}" />
                    <div class=" jcarousel-skin-tango">
                        <div class="jcarousel-container jcarousel-container-horizontal" style="position: relative;
                            display: block;">
                            <div class="jcarousel-clip jcarousel-clip-horizontal" style="position: relative;">
                                <ul class="first-and-second-carousel jcarousel-list jcarousel-list-horizontal" id="first-carousel"
                                    style="overflow: hidden; position: relative; top: 0px; margin: 0px; padding: 0px;
                                    left: 0px; width: 1300px;">
                                    <li class="jcarousel-item jcarousel-item-horizontal jcarousel-item-1 jcarousel-item-1-horizontal"
                                        style="float: left; list-style: none outside none;" jcarouselindex="1">
                                        <img src="https://huntable.co.uk/images/add-your-picture.jpg" width="120" height="160" alt="add-picture" />
                                    </li>
                                    <li class="jcarousel-item jcarousel-item-horizontal jcarousel-item-1 jcarousel-item-1-horizontal"
                                        style="float: left; list-style: none outside none;" jcarouselindex="1"><a class="accounts-link accounts-link-ut"
                                            href="#">
                                            <img width="14" style="border: 0px solid #FFFFFF; box-shadow: 0 0 0px 0px #CCCCCC;"
                                                height="14" alt="add" src="https://huntable.co.uk/images/add-icon.png" />
                                            Add more</a> </li>
                                </ul>
                            </div>
                            <div class="user-regis-left-ut" style="margin-top: 0;">
                                <input type="file" /><br />
                                Supported file types: GIF,JPG,PNG
                            </div>
                            <div class="jcarousel-prev jcarousel-prev-horizontal jcarousel-prev-disabled jcarousel-prev-disabled-horizontal"
                                style="display: block;" disabled="disabled">
                            </div>
                            <div class="jcarousel-next jcarousel-next-horizontal" style="display: block;">
                            </div>
                        </div>
                    </div>
                    <div style="float: right; width: 100%; margin-top: 20px; clear: both; text-align: right;">
                        <a href="#" style="margin-right: 20px;" class="accounts-link">Cancel</a><input type="button"
                            class="button-orange" value="Upload now" />
                    </div>
                </div>
            </div>
            <!-- Popup Div ends -->
            <!-- Popup Div begins -->
            <div id="popup15" class="popup_block">
                <div class="apply-job ">
                    <input type="text" class="textbox-white" value="Give a name to your Video" onfocus="if (this.value =='Give a name to your Video') {this.value ='';}"
                        onblur="if (this.value == '') {this.value ='Give a name to your Video';}" />
                    <div style="float: left; width: 100%;">
                        <h3>
                            Your Video</h3>
                        <p style="margin: 7px 0px;">
                            Share link to a You Tube, Vimeo, Dailmotion or Google video about you <a class="accounts-link"
                                href="#">
                                <img width="14" style="float: none; margin-left: 10px; vertical-align: middle;" height="14"
                                    alt="add" src="https://huntable.co.uk/images/add-icon.png" />
                                Add More</a>
                        </p>
                        <input type="text" class="textbox" value="eg:Video URL" onfocus="if (this.value =='eg:Video URL') {this.value ='';}"
                            onblur="if (this.value == '') {this.value ='eg:Video URL';}" /><input style="margin-left: 15px;"
                                type="button" class="button-green" value="Save" />
                    </div>
                    <div class=" jcarousel-skin-tango">
                        <div class="jcarousel-container jcarousel-container-horizontal" style="position: relative;
                            display: block;">
                            <div class="jcarousel-clip jcarousel-clip-horizontal" style="position: relative;">
                                <ul class="first-and-second-carousel jcarousel-list jcarousel-list-horizontal" id="first-carousel"
                                    style="overflow: hidden; position: relative; top: 0px; margin: 0px; padding: 0px;
                                    left: 0px; width: 1300px;">
                                    <li class="jcarousel-item jcarousel-item-horizontal jcarousel-item-1 jcarousel-item-1-horizontal"
                                        style="float: left; list-style: none outside none;" jcarouselindex="1">
                                        <img src="https://huntable.co.uk/images/video-comes-here.jpg" width="120" height="160" alt="video" />
                                    </li>
                                </ul>
                            </div>
                            <div class="jcarousel-prev jcarousel-prev-horizontal jcarousel-prev-disabled jcarousel-prev-disabled-horizontal"
                                style="display: block;" disabled="disabled">
                            </div>
                            <div class="jcarousel-next jcarousel-next-horizontal" style="display: block;">
                            </div>
                        </div>
                    </div>
                    <div style="float: right; width: 100%; margin-top: 20px; clear: both; text-align: right;">
                        <a href="#" style="margin-right: 20px;" class="accounts-link">Cancel</a><input type="button"
                            class="button-orange" value="Upload now" />
                    </div>
                </div>
            </div>
            <!-- Popup Div ends -->
            <!-- Popup videos Script Ends -->
            <script type="text/javascript" src="https://huntable.co.uk/js/flowplayer-3.1.4.min.js"></script>
            <script type="text/javascript" src="https://huntable.co.uk/js/jquery.easing.1.3.js"></script>
            <script type="text/javascript">

                var videopath = "http://www.burconsult.com/tutorials/fp2/";
                var swfplayer = videopath + "videos/flowplayer-3.1.5.swf";
                var swfcontent = videopath + "videos/flowplayer.content-3.1.0.swf";
                var swfcaptions = videopath + "videos/flowplayer.captions-3.1.4.swf";

            </script>
            <!-- Popup videos Script Ends -->
            <!-- Image Carousel Script Begins -->
            <script type="text/javascript" src="https://huntable.co.uk/js/jquery.jcarousel.min.js"></script>
            <script type="text/javascript">

                jQuery(document).ready(function () {
                    // Initialise the first and second carousel by class selector.
                    // Note that they use both the same configuration options (none in this case).
                    jQuery('.first-and-second-carousel').jcarousel();

                    // If you want to use a caoursel with different configuration options,
                    // you have to initialise it seperately.
                    // We do it by an id selector here.
                    jQuery('#third-carousel').jcarousel({
                        vertical: true
                    });
                });

            </script>
            <!-- Image Carousel Script Ends -->
            <!-- Clickable toggle view begins -->
            <script type="text/javascript">

                $(document).ready(function () {

                    $(".slidingDiv").hide();
                    $(".show_hide").show();

                    $('.show_hide').click(function () {
                        $(".slidingDiv").slideToggle();
                    });

                });
                function ChatWindow() {
                    var userid = document.getElementById('<%=hdnUserId.ClientID %>').value;
                    var otheruserid = document.getElementById('<%=hdnOtherUserId.ClientID %>').value;

                    if (userid == "" || otheruserid=="") {
                        $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
                        $('#dialog').dialog('open');
                        return false;
                    } else {

                        window.open(otheruserid, "ajaxim_" + userid + "_", "width=650,height=600,resizable=1,menubar=0,status=0,toolbar=0");
                        return false;
                    }

                }
            </script>
            <!-- Clickable toggle view ends -->
            <!-- Popup Script Begins -->
            <script type="text/javascript">
                $(document).ready(function () {

                    //When you click on a link with class of poplight and the href starts with a # 
                    $('a.poplight[href^=#]').click(function () {
                        var popId = $(this).attr('rel'); //Get Popup Name
                        var popUrl = $(this).attr('href'); //Get Popup href to define size

                        //Pull Query & Variables from href URL
                        var query = popUrl.split('?');
                        var dim = query[1].split('&');
                        var popWidth = dim[0].split('=')[1]; //Gets the first query string value

                        //Fade in the Popup and add close button
                        $('#' + popId).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="https://huntable.co.uk/images/close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>');

                        //Define margin for center alignment (vertical + horizontal) - we add 80 to the height/width to accomodate for the padding + border width defined in the css
                        var popMargTop = ($('#' + popId).height() + 80) / 2;
                        var popMargLeft = ($('#' + popId).width() + 80) / 2;

                        //Apply Margin to Popup
                        $('#' + popId).css({
                            'margin-top': -popMargTop,
                            'margin-left': -popMargLeft
                        });

                        //Fade in Background
                        $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
                        $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); //Fade in the fade layer 

                        return false;
                    });


                    //Close Popups and Fade Layer
                    $('a.close, #fade').on('click', function () { //When clicking on the close or fade layer...
                        
                        $('#fade , .popup_block').fadeOut(function () {
                            $('#fade, a.close').remove();
                        }); //fade them both out

                        return false;
                    });


                });

            </script>
            <!-- Popup Script Ends -->
            <!-- Add fancyBox main JS and CSS files -->
            <script type="text/javascript">
                $(document).ready(function () {
                    /*
                    *  Simple image gallery. Uses default settings
                    */

                    $('.fancybox').fancybox();

                    /*
                    *  Different effects
                    */

                    // Change title type, overlay closing speed
                    $(".fancybox-effects-a").fancybox({
                        helpers: {
                            title: {
                                type: 'outside'
                            },
                            overlay: {
                                speedOut: 0
                            }
                        }
                    });

                    // Disable opening and closing animations, change title type
                    $(".fancybox-effects-b").fancybox({
                        openEffect: 'none',
                        closeEffect: 'none',

                        helpers: {
                            title: {
                                type: 'over'
                            }
                        }
                    });

                    // Set custom style, close if clicked, change title type and overlay color
                    $(".fancybox-effects-c").fancybox({
                        wrapCSS: 'fancybox-custom',
                        closeClick: true,

                        openEffect: 'none',

                        helpers: {
                            title: {
                                type: 'inside'
                            },
                            overlay: {
                                css: {
                                    'background': 'rgba(238,238,238,0.85)'
                                }
                            }
                        }
                    });

                    // Remove padding, set opening and closing animations, close if clicked and disable overlay
                    $(".fancybox-effects-d").fancybox({
                        padding: 0,

                        openEffect: 'elastic',
                        openSpeed: 150,

                        closeEffect: 'elastic',
                        closeSpeed: 150,

                        closeClick: true,

                        helpers: {
                            overlay: null
                        }
                    });

                    /*
                    *  Button helper. Disable animations, hide close button, change title type and content
                    */

                    $('.fancybox-buttons').fancybox({
                        openEffect: 'none',
                        closeEffect: 'none',

                        prevEffect: 'none',
                        nextEffect: 'none',

                        closeBtn: false,

                        helpers: {
                            title: {
                                type: 'inside'
                            },
                            buttons: {}
                        },

                        afterLoad: function () {
                            this.title = 'Image ' + (this.index + 1) + ' of ' + this.group.length + (this.title ? ' - ' + this.title : '');
                        }
                    });


                    /*
                    *  Thumbnail helper. Disable animations, hide close button, arrows and slide to next gallery item if clicked
                    */

                    $('.fancybox-thumbs').fancybox({
                        prevEffect: 'none',
                        nextEffect: 'none',

                        closeBtn: false,
                        arrows: false,
                        nextClick: true,

                        helpers: {
                            thumbs: {
                                width: 50,
                                height: 50
                            }
                        }
                    });

                    /*
                    *  Media helper. Group items, disable animations, hide arrows, enable media and button helpers.
                    */
                    $('.fancybox-media')
                        .attr('rel', 'media-gallery')
                        .fancybox({
                            openEffect: 'none',
                            closeEffect: 'none',
                            prevEffect: 'none',
                            nextEffect: 'none',

                            arrows: false,
                            helpers: {
                                media: {},
                                buttons: {}
                            }
                        });

                    /*
                    *  Open manually
                    */

                    $("#fancybox-manual-a").click(function () {
                        $.fancybox.open('1_b.jpg');
                    });

                    $("#fancybox-manual-b").click(function () {
                        $.fancybox.open({
                            href: 'iframe.html',
                            type: 'iframe',
                            padding: 5
                        });
                    });

                    $("#fancybox-manual-c").click(function () {
                        $.fancybox.open([
                                {
                                    href: '1_b.jpg',
                                    title: 'My title'
                                }, {
                                    href: '2_b.jpg',
                                    title: '2nd title'
                                }, {
                                    href: '3_b.jpg'
                                }
                            ], {
                                helpers: {
                                    thumbs: {
                                        width: 75,
                                        height: 50
                                    }
                                }
                            });
                    });


                });
            </script>
            <script type="text/javascript" src="https://huntable.co.uk/js/jquery.fancybox-activity.js"></script>
            <%--<script type="text/javascript" src="js/jquery.fancybox-1.2.1.pack.js"></script>--%>
            <script type="text/javascript">
                $(document).ready(function () {
                    $('.fancybox').fancybox();
                }); 
            </script>
            
     <link href="https://huntable.co.uk/js/nyroModal/styles/nyroModal.css" rel="stylesheet" type="text/css" />
    <script src="https://huntable.co.uk/js/nyroModal/js/jquery.nyroModal.custom.min.js" type="text/javascript"></script>
    <!--[if IE 6]>
	<script type="text/javascript" src="js/jquery.nyroModal-ie6.min.js"></script>
<![endif]--></div>
    </body>
    </html>
        <script type="text/javascript">
            var TextMessage = 'Got something to say, ask, post, share…';
            function SetMsg(txt, active) {
                if (txt == null) return;

                if (active) {
                    if (txt.value == TextMessage) txt.value = '';
                } else {
                    if (txt.value == '') txt.value = TextMessage;
                }
            }

            window.onload = function () { SetMsg(document.getElementById('txtTellworld', false)); };
        </script>

</asp:Content>
