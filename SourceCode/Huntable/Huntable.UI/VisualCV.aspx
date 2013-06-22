 <%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="VisualCV.aspx.cs" Inherits="Huntable.UI.VisualCV" %>
<%@ Register Src="~/UserControls/UserMessagepopup.ascx" TagName="_msgPopup" TagPrefix="uc4" %>
<asp:Content ID="content2" ContentPlaceHolderID="headContent" runat="server">
    <link type="text/css" href="https://huntable.co.uk/css1/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <link href="https://huntable.co.uk/css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <link href="https://huntable.co.uk/JqueryPopup/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
    <link href="https://huntable.co.uk/css/skin2.css" rel="stylesheet" type="text/css" />
    <link href="https://huntable.co.uk/css/skin3.css" rel="stylesheet" type="text/css" />
    <link href="https://huntable.co.uk/JqueryPopup/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="https://huntable.co.uk/js/jquery.blockUI.js"></script>--%>
    <link rel="stylesheet" type="text/css" href="https://huntable.co.uk/css/jquery.fancybox-picture.css?v=2.1.2" media="screen" />
     <script src="https://huntable.co.uk/Scripts/fancybox/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="https://huntable.co.uk/Scripts/fancybox/lightbox1.js" type="text/javascript"></script>
    <link href="https://huntable.co.uk/Scripts/fancybox/lightbox1.css" rel="stylesheet" />
    <style type="text/css">



	.jcarousel-skin-tango .jcarousel-container-horizontal{ width:585px;}
	.jcarousel-skin-tango .jcarousel-clip-horizontal{ width:570px; height:120px;}
	.jcarousel-skin-tango .jcarousel-item-horizontal{ margin-right:19px;}
	.jcarousel-skin-tango .jcarousel-item-h{ width:102px; height:110px;}
    
	.jcarousel-skin-tango .jcarousel-clip-horizontal{ width:570px;}
	.jcarousel-skin-tango .jcarousel-prev-horizontal,.jcarousel-skin-tango .jcarousel-next-horizontal{ top:55px;}
	
	
		
	
	
	.fancybox-custom .fancybox-skin {
			box-shadow: 0 0 50px #222;
		}
			

</style>

  <script type="text/javascript">
      $().ready(function () {
          $('#confirmationDialog').dialog({
              autoOpen: false,
              modal: true,
              bgiframe: true,
              title: "Request Endorse",
              width: 500,
              height: 300
          });
          $('#confirmationDialog').parent().appendTo($("form:first"));
          $('#confirmationDialog_Block').dialog({
              autoOpen: false,
              modal: true,
              bgiframe: true,
              title: "Block User",
              width: 500,
              height: 300
          });
          $('#confirmationDialog_Block').parent().appendTo($("form:first"));
          $('#divEndorse').dialog({
              autoOpen: false,
              modal: true,
              bgiframe: true,
              title: "Endorse Now",
              width: 800,
              height: 400
          });
          $('#divEndorse').parent().appendTo($("form:first"));
      });
      
      var empMoreClicks = 1;
      function employmentHistoryMore(e, lnk) {
          //empMoreClicks++;
          var es = e.split('~');
          var count = 0;
          var strEmp = "";
          $.each(es, function (index, value) {
              count++;
              //if (count <= empMoreClicks * 2) {
              strEmp += value + ", ";
              //}
          });
          $('#MainContent_lblPastPosition').html(strEmp.substr(0, strEmp.length - 7)); // + "&nbsp;<a href='javascript:employmentHistoryMore(\"" + e + "\")'>More</a>"
      }
      var eduMoreClicks = 1;
      function educationHistoryMore(e) {
          //eduMoreClicks++;
          var es = e.split('~');
          var count = 0;
          var strEdu = "";
          $.each(es, function (index, value) {
              count++;
              //if (count <= eduMoreClicks * 2) {
              strEdu += value + ", ";
              //}
          });
          $('#MainContent_lblEducation').html(strEdu.substr(0, strEdu.length - 4)); // + "&nbsp;<a href='javascript:educationHistoryMore(\"" + e + "\")'>More</a>"
      }
      function endorseUser(uniqueID) {
          $('#<%=txtEndorseComment.ClientID %>').val('');
          //$('#<%=lblEndorsedUser.ClientID %>').val($('#<%=lblName.ClientID %>').val(''));
          $('#divEndorse').dialog('option', 'buttons',
				{
				    "Endorse Now": function () { __doPostBack(uniqueID, ''); $(this).dialog("close"); },
				    "Cancel": function () { $(this).dialog("close"); }
				});
          $('#divEndorse').dialog('open');
          return false;
      }

      function ChatWindow() {
          var userid = document.getElementById('<%=hdnUserId.ClientID %>').value;
          var otheruserid = document.getElementById('<%=hdnOtherUserId.ClientID %>').value;

          if (userid == "") {
              $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
              $('#dialog').dialog('open');
              return false;
          } else {

              window.open("<%# CalculateChatAuthHash() %>", "ajaxim_" + userid + "_" + otheruserid, "width=650,height=600,resizable=1,menubar=0,status=0,toolbar=0");
              return false;
          }

      }
      

      function HideCtrl(ctrl, timer) {
          var ctryArray = ctrl.split(",");
          var num = 0, arrLength = ctryArray.length;
          while (num < arrLength) {
              if (document.getElementById(ctryArray[num])) {
                  setTimeout('document.getElementById("' + ctryArray[num] + '").style.display = "none";', timer);
              } s
              num += 1;
          }
          return false;
      }
    </script>

    <script type="text/javascript">
        function readMoreAboutMyself() {
            var objLbl = $get('<%=lblabt.ClientID%>');
            objLbl.style.display = "block";
            document.getElementById('MainContent_lblabt').style.display = "block";
            document.getElementById('MainContent_lnkReadMoreAboutMyself').style.display = "none";
            document.getElementById('MainContent_lblAboutMyself').style.display = "none"; 
          
        }
        function getAchivements(id) {
            var l = window.location;
            var base_url = l.protocol + "//" + l.host + "/";
            var res = $.ajax({
                url: base_url+'Ajax/GetEmploymentHistoryAchivements.ashx',
                data: 'id=' + id,
                type: 'GET',
                async: false,
                dataType: 'html'
            }).responseText;
            $('#divAchievements_' + id).html('');
            var htmlToDisplayFirstPart = " <div class='achievements-list'><table class='skill-table'><tr><td>";
            var htmlToDisplayTailPart = "</td></tr></table></div>";
            var htmlToReplaceMiddlePart = "<table class='skill-table-inner skill-table-inner-bot'><tr><th align='left'></th></tr><tr><td>[Achievement]</td></tr></table>";
            if (res !== "") {
                var achs = res.split('~#~');
                var htmlToDisplayMiddlePart = "";
                $.each(achs, function (index, value) {
                    var v = htmlToReplaceMiddlePart.replace("[Achievement]", value);
                    htmlToDisplayMiddlePart += v;
                })

                $('#divAchievements_' + id).html(htmlToDisplayFirstPart + htmlToDisplayMiddlePart + htmlToDisplayTailPart);
            }
            else {
                $('#divAchievements_' + id).html(htmlToDisplayFirstPart + "<li>No data to display</li>" + htmlToDisplayTailPart);
            }
        }
        function getVedios(id) {
            var l = window.location;
            var base_url = l.protocol + "//" + l.host + "/";
            var res = $.ajax({
                url: base_url+'Ajax/GetEmploymentHistoryVedios.ashx'.replace("users/",""),
                data: 'id=' + id,
                type: 'GET',
                async: false,
                dataType: 'html'
            }).responseText;
            $('#divVedios_' + id).html('');
            var htmlToDisplayFirstPart = "<div class='video-list'><ul class='video-list-inner'>"
            var htmlToDisplayTailPart = "</ul></div>"
            var htmlToReplaceMiddlePart = "<li> <iframe id='iFrameVideoView" + id + "' width='113' height='109' frameborder='0' src='[VedioUrl]'></iframe> </li>"
            if (res !== "") {
                var vds = res.split('~#~');
                var htmlToDisplayMiddlePart = "";
                $.each(vds, function (index, value) {
                    if (value != "") {
                        var v = htmlToReplaceMiddlePart.replace("[VedioUrl]", value);
                        htmlToDisplayMiddlePart += v;
                    } 
                })

                $('#divVedios_' + id).html(htmlToDisplayFirstPart + htmlToDisplayMiddlePart + htmlToDisplayTailPart);
            }
            else {
                $('#divVedios_' + id).html(htmlToDisplayFirstPart + "<li>No videos to view</li>" + htmlToDisplayTailPart);
            }
        }
        function getPortfolios(id) {
            var l = window.location;
            var base_url = l.protocol + "//" + l.host + "/";
            var res = $.ajax({
                url: base_url+'Ajax/GetEmploymentHistoryPortfolios.ashx',
                data: 'id=' + id,
                type: 'GET',
                async: false,
                dataType: 'html'
            }).responseText;
            $('#divPortfolios_' + id).html('');
            var htmlToDisplayFirstPart = "<div class='video-list'><ul class='video-list-inner'>"
            var htmlToDisplayTailPart = "</ul></div>"
            var htmlToReplaceMiddlePart = "<li> <img id='imgView" + id + "' width='113' height='109' frameborder='0' src='[ImgUrl]'></img> </li>"
            if (res !== "") {
                var htmlToDisplayMiddlePart = "";
                var imgs = res.split('~#~');
                $.each(imgs, function (index, value) {
                    var v = htmlToReplaceMiddlePart.replace("[ImgUrl]", value);
                    htmlToDisplayMiddlePart += v;
                })

                $('#divPortfolios_' + id).html(htmlToDisplayFirstPart + htmlToDisplayMiddlePart + htmlToDisplayTailPart);
            }
            else {
                $('#divPortfolios_' + id).html(htmlToDisplayFirstPart + "<li>No portfolio to view</li>" + htmlToDisplayTailPart);
            }
        }
    </script>
     <script type="text/javascript">
         $().ready(function () {
             $('#confirmationDialog').dialog({
                 autoOpen: false,
                 modal: true,
                 bgiframe: true,
                 title: "Request Endorse",
                 width: 500,
                 height: 300
             });
             $('#confirmationDialog').parent().appendTo($("form:first"));
             $('#confirmationDialog_Block').dialog({
                 autoOpen: false,
                 modal: true,
                 bgiframe: true,
                 title: "Block User",
                 width: 500,
                 height: 300
             });
             $('#confirmationDialog_Block').parent().appendTo($("form:first"));
             $('#divEndorse').dialog({
                 autoOpen: false,
                 modal: true,
                 bgiframe: true,
                 title: "Endorse Now",
                 width: 800,
                 height: 400
             });
             $('#divEndorse').parent().appendTo($("form:first"));
         });

         
         var empMoreClicks = 1;
         function employmentHistoryMore(e, lnk) {
             //empMoreClicks++;
             var es = e.split('~');
             var count = 0;
             var strEmp = "";
             $.each(es, function (index, value) {
                 count++;
                 //if (count <= empMoreClicks * 2) {
                 strEmp += value + ", ";
                 //}
             });
             $('#MainContent_lblPastPosition').html(strEmp.substr(0, strEmp.length - 7)); // + "&nbsp;<a href='javascript:employmentHistoryMore(\"" + e + "\")'>More</a>"
         }
         var eduMoreClicks = 1;
         function educationHistoryMore(e) {
             //eduMoreClicks++;
             var es = e.split('~');
             var count = 0;
             var strEdu = "";
             $.each(es, function (index, value) {
                 count++;
                 //if (count <= eduMoreClicks * 2) {
                 strEdu += value + ", ";
                 //}
             });
             $('#MainContent_lblEducation').html(strEdu.substr(0, strEdu.length - 4)); // + "&nbsp;<a href='javascript:educationHistoryMore(\"" + e + "\")'>More</a>"
         }


         function ChatWindow() {
             var userid = document.getElementById('<%=hdnUserId.ClientID %>').value;
             var otheruserid = document.getElementById('<%=hdnOtherUserId.ClientID %>').value;

             if (userid == "") {
                 $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
                 $('#dialog').dialog('open');
                 return false;
             } else {

                 window.open("<%# CalculateChatAuthHash() %>", "ajaxim_" + userid + "_" + otheruserid, "width=650,height=600,resizable=1,menubar=0,status=0,toolbar=0");
               
                 return false;
             }

         }
        

//         function ChatWindow() {
//             if (document.getElementById('<%=hdnUserId.ClientID %>').value == "") {
//                 $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
//                 $('#dialog').dialog('open');
//                 return false;
//             } else {
//                 window.open('https://huntable.co.uk/AjaxChat/MessengerWindow.aspx?init=1&target=" + OtherUserId + "', '', 'width=650,height=400,resizable=1,menubar=0,status=0,toolbar=0');
//                 return true;
//             }

//         }
         
         function HideCtrl(ctrl, timer) {
             var ctryArray = ctrl.split(",");
             var num = 0, arrLength = ctryArray.length;
             while (num < arrLength) {
                 if (document.getElementById(ctryArray[num])) {
                     setTimeout('document.getElementById("' + ctryArray[num] + '").style.display = "none";', timer);
                 } s
                 num += 1;
             }
             return false;
         }
    </script>
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
     <script type="text/javascript">

        function rowAction22() {
            $('#dialog11').dialog({

                autoOpen: true,
                modal: true,
                width: 600,

                buttons: {
//                    "Ok": function () { 
//                        ($this).dialog("close");
//                    },
//                 "Cancel": function () {
//                       $(this).dialog("close");
//                   }

                }

            });
            return false;
        }

     
 </script>


      

    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            border: 0; /* This removes the border around the viewport in old versions of IE */
            width: 100%;
            background: #fff;
            min-width: 600px; /* Minimum width of layout - remove line if not required */ /* The min-width property does not work in old versions of Internet Explorer */
            font-size: 90%;
        }
        /* column container */.colmask
        {
            position: relative; /* This fixes the IE7 overflow hidden bug */
            clear: both;
            float: left;
            width: 100%; /* width of whole page */
            overflow: hidden; /* This chops off any overhanging divs */
        }
        /* common column settings */.colright, .colmid, .colleft
        {
            float: left;
            width: 100%; /* width of page */
            position: relative;
        }
        .col1, .col2, .col3
        {
            float: left;
            position: relative;
            padding: 0 0 1em 0; /* no left and right padding on columns, we just make them narrower instead 
					only padding top and bottom is included here, make it whatever value you need */
            overflow: hidden;
        }
        /* 3 Column settings */.threecol
        {
            background: #eee; /* right column background colour */
        }
        .threecol .colmid
        {
            right: 25%; /* width of the right column */
            background: #fff; /* center column background colour */
        }
        .threecol .colleft
        {
            right: 50%; /* width of the middle column */
            background: #f4f4f4; /* left column background colour */
        }
        .threecol .col1
        {
            width: 46%; /* width of center column content (column width minus padding on either side) */
            left: 102%; /* 100% plus left padding of center column */
        }
        .threecol .col2
        {
            width: 21%; /* Width of left column content (column width minus padding on either side) */
            left: 31%; /* width of (right column) plus (center column left and right padding) plus (left column left padding) */
        }
        .threecol .col3
        {
            width: 21%; /* Width of right column content (column width minus padding on either side) */
            left: 85%; /* Please make note of the brackets here:
					(100% - left column width) plus (center column left and right padding) plus (left column left and right padding) plus (right column left padding) */
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
      <div id="dialog11" style="display: none;" title="List Of Companies">
         <table>
         <asp:DataList runat="server" ID="dlListOfCompanies">
             <ItemTemplate>
                 <tr>
                     <td style="width: 235px;">
               <a href= '<%#CompanyUrlGenerator(Eval("Id") )%>' runat="server" > <asp:Label ID="Label1" runat="server" Text='<%#Eval("CompanyName") %>' style="color:  #008ca1;"></asp:Label> </a> </td>
                 <%--<asp:Label runat="server" Text='<%#Eval("CompanyWebsite") %>'></asp:Label>
                 <asp:Label ID="Label1" runat="server" Text='<%#Eval("CompanyEmail") %>'></asp:Label>--%>
               <td> <a href='<%#CompanyUrlGenerator(Eval("Id") )%>' runat="server"> <asp:Image ID="Image1" ImageUrl='<%#Picturec(Eval("CompanyLogoId"))%>' runat="server" Width="119px"
                                        Height="62px" /></a> </td>
               </tr>
             </ItemTemplate>
             
         </asp:DataList>
         </table>
        <%-- <asp:Label id="TextBox1" runat="server" Text="22aaaaaaaaaaa"></asp:Label>  
         <asp:Image runat="server" ID="imgHasCompany0001" ImageUrl="https://huntable.co.uk/images/UserCompany.png" Width="33px" Height="35px" />--%> 
   </div>
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <asp:HiddenField ID="hdnOtherUserId" runat="server" />
    <div id="dialog" title="Confirm Message">
        <asp:Label ID="lblConfirmMessage" runat="server" />
    </div>
    <p class="margin-top-visible">
        &nbsp;</p>
    <div id="divViewProfile" runat="server" class="content-section">
        
         <div   id="ovrly"  style="height:36px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image src="https://huntable.co.uk/images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-25px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 692px;;margin-top:-13px"><image id="ximg"  src="https://huntable.co.uk/images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>

        <div id="content-inner">
            <div class="tab-cv">
            <a id ="vc" runat="server" class="selectedcv">Visual</a>
                <a  id="vt" runat="server" style="margin-left: 10px;">Text</a>                 
                     <a id="vca" runat="server" style="margin-left: 10px;">Activity</a>
            </div>
            <div class="visual-cv">
                <div class="cv-top">
                    <div class="cv-top-left">
                         <a href="#" runat="server" id="showProfileImage12" rel="lightbox">
                            <img runat="server" id="imgProfile" src="" class="profile-pic profile-pic-cv" width="76"
                                height="81" alt="Profile-pic" />
                                </a>
                        <div class="cv-top-left-inner">
                            <h2>
                                
                                    <asp:Label class="accounts-link" ID="lblName" runat="server" style="color:#008ca1;"></asp:Label></h2>
                            <strong>
                                <asp:Label ID="lblCurrentRoleProfileSection" class="accounts-link" runat="server"></asp:Label></strong><br />
                            <div id="divOnlineNoewChatWithMe" runat="server" >
                                <a href="https://huntable.co.uk/VisualCV.aspx" class="green-color" >
                                   <asp:ImageButton ID="userOnline" Width="10" Height="10"
                                    alt="green-circel" runat="server" />
                                    <asp:Label ID="onlineinfo" runat="server"></asp:Label>
                            
                                 &nbsp&nbsp
                                
                                 <a href="#"  class="blue-color blue-color-cv" id="a_chat" runat="server" onclick="return ChatWindow();">
                                     <img src="https://huntable.co.uk/images/chat-icon.png"  width="17" height="16" alt="chat" /> Chat with me
                                 </a>  <%--<asp:Label style="color: #008397; font-size: 10px;" Text="Chat with me" runat="server" ID="lbl_chat"></asp:Label>--%>
                                </a>
                            </div>
                        </div>
                        <div class="visual-hd-links">
                            <asp:Button id="flwr" runat="server"   class="button-orange button-orange-follow" Text="Follow" onclick="FollowClick" />
                            <asp:Button id="flwngr" runat="server"   class="button-orange button-orange-follow" Text="Following" onclick="UnfollowClick"/>
                            <a href="#" onclick="javascript:$('#popup14').show();" class=" button-grey-new poplight" rel="popup14">
                                <img src="https://huntable.co.uk/images/growth-icon.png" width="16" height="16" alt="Profile-growth" />
                                &nbsp;Profile Growth</a>
                            <div id="popup14" class="popup_block" style="display: none; width: 750px; margin-top: -229px;
                                margin-left: -415px;">
                                <a href="#" onclick="javascript:$('#popup14').hide();">
                                    <img src="https://huntable.co.uk/images/close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>
                                <div class="apply-job">
                                    <div>
                                        <h2>
                                            <img src="https://huntable.co.uk/images/growth-icon.png" width="16" height="16" alt="Profile-growth" />
                                            &nbsp;Profile Growth</h2>
                                    </div>
                                    <div class="growth-chart">
                                        <asp:Chart runat="server" ImageStorageMode="UseImageLocation" ImageLocation="~" ID="chartGrowth" Width="750" Height="335">
                                            <Series>
                                                <asp:Series IsVisibleInLegend="false"  ChartType="Bar" Palette="EarthTones">
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1">
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                    </div>
                                </div>
                            </div>
                             <asp:ImageButton runat="server"   
                                     ID="imgHasCompany" ImageUrl="https://huntable.co.uk/images/UserCompany.png" Width="33px" Height="35px" OnClick="UserCompaniesClick"   />
                        </div>
                    </div>
                    <div class="cv-top-right">
                        <div class="achivement achivement-top" style="width: 107px;">
                            <img src="https://huntable.co.uk/images/portfolio-icon.jpg" width="22" height="24" alt="potfolio" />
                            <a href="https://huntable.co.uk/VisualCV.aspx" class="port-link">Portfolio</a> <a href="https://huntable.co.uk/EditProfilePage.aspx">
                                <img src="https://huntable.co.uk/images/edit-icon.jpg" width="23" height="23" alt="edit" /></a>
                        </div>
                        <div class="jcarousel-skin-tango">
                            <div class="jcarousel-container jcarousel-container-horizontal" style="position: relative;
                                display: block;">
                                <div class="jcarousel-clip jcarousel-clip-horizontal" style="position: relative;">
                                    <ul id="first-carousel" class="first-and-second-carousel jcarousel-list jcarousel-list-horizontal"
                                        style="overflow: hidden; position: relative; top: 0px; margin: 0px; padding: 0px;
                                        left: 0px; width: auto;">
                                       <%=GetMyPictures() %>
                                    </ul>
                                </div>
                                <div class="jcarousel-prev jcarousel-prev-horizontal jcarousel-prev-disabled jcarousel-prev-disabled-horizontal"
                                    style="display: block;" disabled="disabled">
                                </div>
                                <div class="jcarousel-next jcarousel-next-horizontal" style="display: block;">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="cv-position">
                    <div class="cv-position-left">
                    <div class="cv-postion-left-inner" style="border-right: 0px;">
                            <h2>
                                Experience</h2>
                            <asp:Label ID="lblExperience" class="accounts-link" runat="server"></asp:Label>
                        </div>
                        <div class="cv-postion-left-inner">
                            <h2>
                                Current Position</h2>
                            <asp:Label ID="lblCurrentRole" class="accounts-link" runat="server"></asp:Label>
                        </div>
                        <div class="cv-postion-left-inner">
                            <h2>
                                Working at</h2>
                            <asp:Label ID="lblWorkingAt" class="accounts-link" runat="server"></asp:Label>
                        </div>
                        <div class="cv-postion-left-inner">
                            <h2>
                                Lives in</h2>
                            <asp:Label ID="lblLivein" class="accounts-link" runat="server"></asp:Label>
                        </div>
                        
                    </div>
                    <div class="cv-position-right" id="divLikeMyProfile" runat="server">
                        <img src="https://huntable.co.uk/images/hand-thumb.png" width="25" height="28" alt="like" />
                        <div>
                            <h2>
                                Like my profile <label  style="font-size: 12px;">Contact me here</label></h2>
                        </div>
                        <p class="profile-subdiv">
                            &nbsp;<div style="margin-left: -11px; margin-top: -17px;" runat="server" id="p_msg_like"><uc4:_msgPopup id="ucMessage" runat="server" /></div>
                            </p>
                        
                            &nbsp;&nbsp;&nbsp;
                            <a href="#" runat="server" id="hypLikeProfile" class="button-orange profile-like"  style="float: right;">like
                                my profile</a>
                      
                    </div>
                </div>
            </div>
            <div class="cv-content">
                <div class="cv-content-left">
                    <div class="cv-about">
                        <h2 class="orange-head">
                            About Myself</h2>
                        <p>
                            <asp:Label runat="server" ID="lblAboutMyself"></asp:Label>
                            <asp:Label runat="server" ID="lblabt" style="display:none" ></asp:Label>
                        </p>
                        <a id="lnkReadMoreAboutMyself" runat="server" href="javascript:readMoreAboutMyself();"
                            class="accounts-link" style="float: right;">›› Read More</a>
                    </div>
                    <div class="profile-content">
                        <div class="profile-content-left-head">
                            <h2 class="orange-head">
                                Experience</h2>
                            <h2 class="orange-head" style="border-left: 10px solid #fff;">
                                Description</h2>
                        </div>
                        <asp:Repeater runat="server" ID="rptrExperience">
                            <ItemTemplate>
                                <div class="profile-content-inner">
                                    <div class="profile-content-left">
                                        <h2>
                                            <asp:Label runat="server" ID="lblExperiencePeriod" Text='<%#Eval("Period") %>'></asp:Label></h2>
                                        <strong>
                                            <asp:Label runat="server" ID="lblExperienceRole" Text='<%#Eval("JobTitle") %>'></asp:Label></strong><br />
                                        <asp:Label runat="server" ID="lblExperienceCompany" Text='<%#Eval("Company") %>'></asp:Label><br />
                                        <asp:Label runat="server" ID="lblExperienceDuration" Text='<%#Eval("Duration") %>'></asp:Label><br />
                                        <div class="skills-tag">
                                            <div class="skil-title">Skill</div><div class="colon">:</div><div class="skill-t">
                <span><img src="https://huntable.co.uk/images/skils-tag-left.png" width="11" height="20" alt="skils-left" /></span>
                <asp:Label  runat="server"  class="cent-span" ID="lblExperienceSkill" Text= '<%#Eval("Skill") %>'></asp:Label>
                <span><img src="https://huntable.co.uk/images/skils-tag.png" width="25" height="20" alt="skils-right" /></span>
                </div>
                                            
                                        </div>
                                        <div class="skills-tag">
                                        <div class="skil-title">Industry</div><div class="colon">:</div><div class="skill-t">
                <span><img src="https://huntable.co.uk/images/skils-tag-left.png" width="11" height="20" alt="skils-left" /></span>
                <asp:Label  runat="server"  class="cent-span" ID="Label2" Text= '<%#Eval("Industry") %>'></asp:Label>
                <span><img src="https://huntable.co.uk/images/skils-tag.png" width="25" height="20" alt="skils-right" /></span>
                </div></div>
                                        <div class="skills-tag">
                                         <div class="skil-title">Level</div><div class="colon">:</div><div class="skill-t">
                <span><img src="https://huntable.co.uk/images/skils-tag-left.png" width="11" height="20" alt="skils-left" /></span>
                <asp:Label  runat="server"  class="cent-span" ID="Label3" Text= '<%#Eval("Level") %>'></asp:Label>
                <span><img src="https://huntable.co.uk/images/skils-tag.png" width="25" height="20" alt="skils-right" /></span>
                </div>    
                             </div>      
                                        </div>
                                     
                                            
                                       

                                        <a href="https://huntable.co.uk/visualcv.aspx" class="accounts-link">
                                            <asp:Label runat="server" ID="lblExperienceEndorsements"></asp:Label></a>
                                   
                                    <div class="profile-content-mid">
                                        <img src="https://huntable.co.uk/images/range-circle-img.png" width="15" height="16" alt="circle" />
                                    </div>
                                    <div class="profile-content-right">
                                        <p class="wrappermorewidth">
                                            <asp:Label runat="server" ID="lblExpSummary" Text='<%#Eval("Description") %>'></asp:Label>
                                        </p>
                                        <div class="profile-content-right-link" id="divParentAchievement">
                                            <div class="achivement achivement-first">
                                                <img src="https://huntable.co.uk/images/achievements-icon.jpg" width="22" height="24" alt="achievements" />
                                                
                                                <a  style="margin-left:-10px;" class="accounts-link">Achievements</a>
                                              
                                                
                                                <a href="https://huntable.co.uk/EditProfilePage.aspx">
                                                    <img src="https://huntable.co.uk/images/edit-icon.jpg" width="23" height="23" alt="edit" /></a>
                                            </div>
                                            <div id="divAchievements_<%#Eval("Id") %>" style="display: block;">
                                            <script type="text/javascript" language="JavaScript">
                                                getAchivements('<%#Eval("Id") %>');
                                                </script>
                                            </div>
                                            <div class="achivement">
                                                <img src="https://huntable.co.uk/images/portfolio-icon.jpg" width="22" height="24" alt="potfolio">
                                                <a  class="accounts-link">Portfolio</a>
                                                <a href="https://huntable.co.uk/EditProfilePage.aspx">
                                                    <img src="https://huntable.co.uk/images/edit-icon.jpg" width="23" height="23" alt="edit" /></a>
                                                    
                                            </div>
                                            <div id="divPortfolios_<%#Eval("Id") %>" style="display: block;">
                                            <script type="text/javascript" language="JavaScript">
                                                getPortfolios('<%#Eval("Id") %>');
                                                </script>
                                            </div>
                                            <div class="achivement">
                                                <img src="https://huntable.co.uk/images/video-icon.jpg" width="22" height="24" alt="video">
                                                <a  class="accounts-link">Video</a>
                                                <a href="https://huntable.co.uk/EditProfilePage.aspx">
                                                    <img src="https://huntable.co.uk/images/edit-icon.jpg" width="23" height="23" alt="edit" /></a>
                                            </div>
                                            <div id="divVedios_<%#Eval("Id") %>" style="display: block;">
                                            <script type="text/javascript" language="JavaScript">
                                                getVedios('<%#Eval("Id") %>');
                                                </script>
                                            </div>
                                        </div>
                                    </div>
                                </div> 
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                     <div id="sssd" runat="server"><a   href="#?w=500" class="button-orange floatleft poplight" rel="popupSharev">
                            Share</a></div>
                    <div id="divBLock" runat="server">
                        <div id="endorsecomplete" runat="server">
                            <div id="divEndorseBLock" runat="server">
                                <a href="#?w=750"  class="button-orange floatleft poplight" rel="popup6">Endorse Now
                                </a>
                            </div>
                            <div id="RequestEndorsements" runat="server">
                                <a href="#?w=750"   class="button-orange floatleft poplight"
                                    rel="popup7">Request Endorsement</a></div>
                                    </div>
                            <div id="partialunblock" runat="server">
                                <a href="#?w=750"  class="button-orange floatleft poplight" rel="popup2">Block Messages
                                    from this user</a></div>
                        </div>
                        <div id="divunblock" visible="False" runat="server">
                    <asp:Button ID="btnUnblock" class="button-orange floatleft poplight" Text="Unblock"
                        runat="server" OnClick="BtnUnblockClick" />
                </div>
                </div>
                <div class="cv-content-right" id ="skilladj" runat="server">
                    <div class="skill">
                        <h2 class="orange-head orange-head-skill">
                            <img src="https://huntable.co.uk/images/act-icon2.png" width="20" height="19" alt="Skill" />Skills</h2>
                        <table class="skill-table">
                            <tbody>
                                <tr>
                                    <td>
                                        <table class="skill-table-inner">
                                            <tbody>
                                                <tr valign="top">
                                                    <td width="10px">
                                                        Expert
                                                    </td>
                                                    <td>
                                                        <div id="divExpertSkills" runat="server">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="skill-table-inner">
                                            <tbody>
                                                <tr valign="top">
                                                    <td width="10px">
                                                        Strong
                                                    </td>
                                                    <td>
                                                        <div id="divStrongSkills" runat="server">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="skill-table-inner">
                                            <tbody>
                                                <tr valign="top">
                                                    <td width="10px">
                                                        Good
                                                    </td>
                                                    <td>
                                                        <div id="divGoodSkills" runat="server">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="position">
                        <h2>
                            Position Looking for
                            <asp:LinkButton ID="lnkHidePosittionLookingFor1" runat="server" CssClass="show_hide1 show_hide-new"
                                Text="Hide" OnClick="HidePosittionLookingFor"></asp:LinkButton>
                        </h2>
                        <div id="divPositionLookingFor" runat="server" class="slidingDiv1">
                        </div>
                    </div>
                    <div class="position">
                        <h2>
                            Salary From - To
                            <asp:LinkButton ID="lnkHideExpectedSalary1" runat="server" CssClass="show_hide1 show_hide-new"
                                Text="Hide" OnClick="HideExpectedSalary"></asp:LinkButton>
                        </h2>
                        <div id="divExpectedSalary" runat="server" class="slidingDiv2">
                        </div>
                    </div>
                    <div class="skill">
                        <h2 class="orange-head orange-head-skill">
                            <img src="https://huntable.co.uk/images/act-icon3.png" width="20" height="19" alt="Language Known" />Languages
                            Known</h2>
                        <div id="divLanguages" runat="server">
                        </div>
                    </div>
                    <div class="skill">
                        <h2 class="orange-head orange-head-skill">
                            <img src="https://huntable.co.uk/images/act-icon1.png" width="20" height="19" alt="Interests">Interest</h2>
                        <p class="interest-list" id="pInterest" runat="server">
                        </p>
                    </div>
                    <div class="skill">
                        <h2 class="orange-head orange-head-skill">
                            <img src="https://huntable.co.uk/images/act-icon4.png" width="20" height="19" alt="Videos" />Videos</h2>
                        <div>
                            <div class=" jcarousel-skin-tango">
                                <%--<div class="jcarousel-container jcarousel-container-vertical" style="position: relative;
                                    display: block;">--%>
                                    <div  style="position: relative;width:321px;overflow-y:scroll;height:400px;">
                                     <asp:Label runat="server" ID="vlbl" Text="No Videos Found" ></asp:Label>
                                     <%--   <ul id="third-carousel" class="jcarousel-list jcarousel-list-vertical" style="overflow: scroll;
                                            position: relative; top: 0px; margin: 0px; padding: 0px; left: 0px; height: 975px;">--%>
                                       
                                        <asp:Repeater ID="rptr" runat="server" >
                                        <ItemTemplate>
                                       
                                        <iframe id ="ifm" runat ="server" src= '<%#Container.DataItem %>'></iframe>
                                        
                                        </ItemTemplate>
                                        </asp:Repeater>
                                      <%--  </ul>--%>
                                    </div>
                                    <div class="jcarousel-prev jcarousel-prev-vertical jcarousel-prev-disabled jcarousel-prev-disabled-vertical"
                                        style="display: block;" disabled="disabled">
                                    </div>
                                    <div class="jcarousel-next jcarousel-next-vertical" style="display: block;">
                                    </div>
                                    </div>
                                <%--</div>--%>
                            <%--</div>--%>
                        </div>
                    </div>
                </div>
            </div>
               <script type="text/javascript">
                   function geturl() {
                       document.getElementById('twitter').href = 'https://twitter.com/share?url=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value) + '&summary=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value);
                       document.getElementById('linkedin').href = 'http://www.linkedin.com/shareArticle?mini=true&url=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value) + '&summary=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value);
                       document.getElementById('facebook').href = 'http://www.facebook.com/sharer.php?u=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value) + '&summary=' + encodeURI(document.getElementById('<%=txtShareMessage.ClientID %>').value);
                   }
                   function getTxt() {
                       document.getElementById('<%=txtShareMessage.ClientID %>').value = window.window.top.location;
                       document.getElementById('<%=txtMessage.ClientID %>').value = window.window.top.location;

                       document.getElementById('<%=fe_text.ClientID %>').value = window.window.top.location;

                   }
    </script>
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
            <script type="text/javascript">
                $(document).ready(function () {

                    //When you click on a link with class of poplight and the href starts with a # 
                    $('a.poplight[href^=#]').click(function () {
                        var popID = $(this).attr('rel'); //Get Popup Name
                        var popURL = $(this).attr('href'); //Get Popup href to define size

                        url.src = "http://huntable.co.uk/ShareMail.aspx";
                        //Pull Query & Variables from href URL
                        var query = popURL.split('?');
                        var dim = query[1].split('&');
                        var popWidth = dim[0].split('=')[1]; //Gets the first query string value

                        //Fade in the Popup and add close button
                        $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="https://huntable.co.uk/images/close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>');

                        //Define margin for center alignment (vertical + horizontal) - we add 80 to the height/width to accomodate for the padding + border width defined in the css
                        var popMargTop = ($('#' + popID).height() + 80) / 2;
                        var popMargLeft = ($('#' + popID).width() + 80) / 2;

                        //Apply Margin to Popup
                        $('#' + popID).css({
                            'margin-top': -popMargTop,
                            'margin-left': -popMargLeft
                        });

                        //Fade in Background
                        $('body').append('<div id="fade" style="z-index:10"></div>'); //Add the fade layer to bottom of the body tag.
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
                    function fdout() {
                        $('#fade , .popup_block').fadeOut(function () {
                            $('#fade, a.close').remove();
                        }); //fade them both out

                        return false;

                    }


                });
                function fdout() {
                    $('#fade').remove();

                }

                function HideCtrl(ctrl, timer) {
                    var ctryArray = ctrl.split(",");
                    var num = 0, arrLength = ctryArray.length;
                    while (num < arrLength) {
                        if (document.getElementById(ctryArray[num])) {
                            setTimeout('document.getElementById("' + ctryArray[num] + '").style.display = "none";', timer);
                        } 
                        num += 1;
                    }
                    return false;
                }
        </script>
           <div id="popup7" class="popup_block">
                    <table>
                        <tr>
                            <td colspan="2">a
                                Endorse
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Jobtitle
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddljob" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <textarea id="txtarea" rows="6" cols="1" runat="server" placeholder="Comments..."
                                    style="width: 400px;" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button runat="server" Text="Request Endorsement" class="button-orange floatleft poplight"
                                    ID="btnRequestEndorse" UseSubmitBehavior="false" OnClick="BtnRequestEndorseClick">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
      <div id="popup6" class="popup_block">
            <table>
                <tr>
                    <td colspan="2">
                        Endorse
                        <asp:Label runat="server" ID="lblEndorsedUser"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Company
               
                    <asp:DropDownList runat="server" ID="ddljobtitle" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="txtEndorseComment" rows="6" cols="1" runat="server" placeholder="Comments..."
                            style="width: 400px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Endorse Now" class="button-orange floatleft poplight"
                            ID="btnEndorseUser" UseSubmitBehavior="false" OnClick="BtnEndorseUserClick" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="div1" visible="False" runat="server">
                    <asp:Button ID="Button1" class="button-orange floatleft poplight" Text="Unblock"
                        runat="server" OnClick="BtnUnblockClick" />
                </div>
    <div id="popup2" class="popup_block">
        <div class="apply-job">
            <h3 class="popup-head">
                Block
            </h3>
            <strong class="floatleft">All messages from this user have been blocked.</strong><br />
            <asp:Button runat="server" class="button-green floatleft" Text="Block " OnClick="BtnBlockUserClick" ID="btnBlockUser"
                UseSubmitBehavior="false" /><a id="CancelButton" runat="server" class="button-ash floatleft" style="margin: 10px 0px 0px 10px;">cancel</a>
        </div>
    </div>
          <div id="popupSharev" class="popup_block">
                   <div id="Div2" class="active">
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
                                    <asp:CheckBox ID="chkTwitter" runat="server" oncheckedchanged="chkTwitter_CheckedChanged"/>
                                    <a id="twitter" href="#" target="_blank" title="Twitter Share Button">
                                        <img src="https://huntable.co.uk/images/twitter.png" width="20" height="20"></a>
                                    <asp:CheckBox ID="chkLinkedIn" runat="server" oncheckedchanged="chkLinkedIn_CheckedChanged"/><a id="linkedin" href="#"
                                        target="_new">
                                        <img src="https://huntable.co.uk/images/linkedin.png" alt="linkedin share button" width="20" height="20"
                                            title="LinkedIn Share Button" /></a><asp:CheckBox ID="chkFacebook" runat="server" oncheckedchanged="chkFacebook_CheckedChanged" />
                                    <a id="facebook" target="_blank" href="#" title="Facebook Share Button">
                                        <img src="https://huntable.co.uk/images/facebook.jpg" width="20" height="20"></a>
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
    <script type="text/javascript" src="https://huntable.co.uk/js/ZeroClipboard.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
          
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
                </div> </div>
    </div>
  
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
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
    
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="https://huntable.co.uk/js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <input type="hidden" runat="server" id="hdnAbpoutMyself" />


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
</div>
</div>
</div>
</asp:Content>
