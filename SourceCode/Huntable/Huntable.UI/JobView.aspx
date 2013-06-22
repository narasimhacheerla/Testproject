<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="JobView.aspx.cs" Inherits="Huntable.UI.JobView" %>

<%@ Register TagPrefix="uc" TagName="ShowProfileImage" Src="~/UserControls/ShowProfileImage.ascx" %>
<%@ Register Src="~/UserControls/CompaniesYouMayWantFollow.ascx" TagName="CompaniesYouMayWantToFollow"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://huntable.co.uk/Scripts/fancybox/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="https://huntable.co.uk/Scripts/fancybox/lightbox1.js" type="text/javascript"></script>
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
    <link href="https://huntable.co.uk/Scripts/fancybox/lightbox1.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:HiddenField ID="hdnUserId" runat="server" />
   <div id="dialog" title="Confirm Message" >
        <asp:Label ID="lblConfirmMessage" runat="server" />
    </div>
  <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image  src="https://huntable.co.uk/images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="https://huntable.co.uk/images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="accounts-profile accounts-profile1">
                    <a href="JobSearch.aspx" class="accounts-link" runat="server" onclick="javascript:history.go(-1)">&lsaquo;&lsaquo;&nbsp;Go
                        Back to Search Results</a></div>
                <div class="blue-box blue-box-job" style="font-family: Georgia; font-size: 14;">
                   <strong class="ad-head">  <asp:Label ID="lblJobTitle" runat="server"></asp:Label></strong>
                    <div class="address">
                        <asp:Label ID="lblCountry" runat="server" Font-Names="Georgia"></asp:Label>,<br />
                        <asp:Label ID="lblSym" runat="server"></asp:Label>
                        <asp:Label ID="lblSalary" runat="server"></asp:Label>&nbsp;&nbsp;<asp:Label runat="server"
                            ID="lblSalCurType"></asp:Label>
                        <br />
                        <asp:Label ID="lblNumberOfNumberOfApplicationsOfThisJob" runat="server"></asp:Label>
                        Applications<br />
                        <a href="#" class="accounts-link">Total Views:
                            <asp:Label ID="lblJobTotalNumberOfviews" runat="server"></asp:Label></a>
                    </div>
                    <div class="ad-desc">
                        Job type:<asp:Label ID="lblJobType" runat="server"></asp:Label><br />
                        <asp:Label ID="lblJobPostedDate" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="profile-desc">
                    <h2 class="profile-desc-heading" style="font-family: Georgia; font-size: 14;">
                        Job Description:</h2>
                    <p>
                        <asp:Label runat="server" ID="lblJobDescription" Text="Job Description" Font-Names="Georgia"></asp:Label>
                    </p>
                    <br />
                </div>
                <div class="profile-desc">
                    <h2 class="profile-desc-heading" style="font-family: Georgia; font-size: 14;">
                        About company:</h2>
                    <p>
                        <asp:Label runat="server" ID="lblAbtComp" Text="About company" Font-Names="Georgia"></asp:Label>
                    </p>
                    <br />
                </div>
                <div class="profile-desc">
                    <h2 class="profile-desc-heading" style="font-family: Georgia; font-size: 14;">
                        Candidate Profile:</h2>
                    <p>
                        <asp:Label runat="server" ID="lblcandProf" Text="Candidate Profile" Font-Names="Georgia"></asp:Label>
                    </p>
                    <br />
                </div>
                <div class="ess-info" style="font-family: Georgia; font-size: 14;">
                    <div class="ess-info-left">
                        <h3>
                            Essesntial Information</h3>
                        <ul style="font-family: Georgia;">
                            <li class="info1">Job Type:</li>
                            <li class="info2">
                                <asp:Label ID="lblJobTypeEssential" runat="server"></asp:Label></li>
                            <li class="info1" style="margin-left: 2px;">Experience req:</li>
                            <li class="info2" style="margin-left: -2px;">
                                <asp:Label ID="lblExperienceRequiredEssential" runat="server"></asp:Label>
                            </li>
                            <li class="info1" style="margin-left: 2px;">Industry:</li>
                            <li class="info2" style="margin-left: -2px;">
                                <asp:Label ID="lblIndustryEssential" runat="server"></asp:Label>
                            </li>
                            <li class="info1" style="margin-left: 2px;">Skill:</li>
                            <li class="info2" style="margin-left: -2px;">
                                <asp:Label ID="lblSkillTypeEssential" runat="server" Text="Skills"></asp:Label></li>
                            <%--<li class="info1">Skill:</li>
                            <li class="info2"> <asp:Label ID="lblSkillTypeEssential" runat="server"></asp:Label></li>--%>
                            <li class="info1" style="margin-left: 2px;">Salary:</li>
                            <li class="info2" style="margin-left: -2px;">
                                <asp:Label ID="lblCSym" runat="server"></asp:Label>
                                <asp:Label ID="lblSalaryEssential" runat="server"></asp:Label>&nbsp;<asp:Label ID="lblCurr"
                                    runat="server"></asp:Label>
                            </li>
                            <li class="info1" style="margin-left: 2px;">Job Posted on:</li>
                            <li class="info2" style="margin-left: -2px;">
                                <asp:Label ID="lblJobPostedOnEssential" runat="server"></asp:Label>
                            </li> 
                            <li class="info1" style="margin-left: 2px;">Reference Number:</li>
                            <li class="info2" style="margin-left: -2px;">
                                <asp:Label ID="lblreference" runat="server"></asp:Label>
                            </li>
                        </ul>
                    </div>
                    <div class="ess-info-right">
                        <table>
                            <tr>
                                <td>
                                    <uc:ShowProfileImage runat="server" class="fancyzoom" ID="showProfileImage" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <a href="#" runat="server" id="showProfileImage11" rel="lightbox">
                                        <img src="" runat="server" class="pics" alt="" id="shwProfileImage11" width="57"
                                            height="65" border="1" />
                                    </a>
                                </td>
                                <td>
                                    <a href="#" runat="server" id="showProfileImage12" rel="lightbox">
                                        <img src="" runat="server" alt="" class="pics" id="shwProfileImage12" width="57"
                                            height="65" border="1" />
                                    </a>
                                </td>
                                <td>
                                    <a href="#" runat="server" id="showProfileImage13" rel="lightbox">
                                        <img src="" runat="server" alt="" class="pics" id="shwProfileImage13" width="57"
                                            height="65" border="1" />
                                    </a>
                                    <%--<uc:ShowProfileImage runat="server" class="fancyzoom" ID="showProfileImage3" />--%>
                                </td>
                                <td>
                                    <a href="#" runat="server" id="showProfileImage14" rel="lightbox">
                                        <img src="" runat="server" alt="" class="pics" id="shwProfileImage14" width="57"
                                            height="65" border="1" />
                                    </a>
                                    <%--<uc:ShowProfileImage runat="server" class="fancyzoom" ID="showProfileImage4" />--%>
                                </td>
                                <td>
                                    <a href="#" runat="server" id="showProfileImage15" rel="lightbox">
                                        <img src="" runat="server" alt="" class="pics" id="shwProfileImage15" width="57"
                                            height="65" border="1" />
                                    </a>
                                    <%--<uc:ShowProfileImage runat="server" class="fancyzoom" ID="showProfileImage5" />--%>
                                </td>
                            </tr>
                        </table>
                        <asp:LinkButton ID="lbAlreadyApplied" class="button-green floatleft poplight " Text="Applied"
                            Style="clear: both; margin-left: 40px;" runat="server" Visible="false"></asp:LinkButton>
                        <asp:LinkButton ID="lbApplyNowbtn" class="button-green floatleft poplight " OnClick="BtnApplyNowClick"
                            Text="Apply now" runat="server"   Style="clear: both;
                            margin-left: 40px;"></asp:LinkButton>
                            <div id="lblapplynowdiv" runat="server"><a href="#?w=350"  class="button-green floatleft poplight" rel="popup12">
                                                            Apply now </a></div>

                    </div>
                </div>
            </div>
            <!-- content inner left ends -->
            <div class="content-inner-right">
                <div class="post-opportunity">
                    <a  id="Epp" runat="server"  class="button-orange floatleft " style="font-size: 12px; padding: 7px 10px;">
                        Is Your Profile Huntable? Update-Edit now</a>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <div class="share-logo">
                        
                            <%--<img src="images/share-img.jpg" width="116" height="88" alt="Share-img" /></a>--%>
                            <a runat="server" id="aj" ><asp:Image ID="imgUserCompany" runat="server" ImageUrl="" AlternateText="Image Not found"
                                Width="116" Height="88" /></a>
                            <a id ="followrs" runat="server" href="#" class="accounts-link" style="padding-top: 15px;">Followers:
                               <asp:Label ID="lblcount" runat="server"></asp:Label></a> 
                    </div>
                    <div class="sharejob">
                        <img src="https://huntable.co.uk/images/icon-share.jpg" width="20" height="18" alt="share" />
                     <a href="#?w=500" class="poplight" rel="popupShare" > Share:</a> 
                          <a href="#?w=500" class="poplight" rel="popupShare" >  <img src="https://huntable.co.uk/images/facebook.jpg" width="34" height="34" alt="Facebook" title="Facebook" />
                        </a><a href="#?w=500" class="poplight" rel="popupShare" >
                            <img src="https://huntable.co.uk/images/linkedin.jpg" width="34" height="34" alt="Linkedin" title="Linked in" /></a>
                               <a href="#?w=500" class="poplight" rel="popupShare" > <img src="https://huntable.co.uk/images/twitter.jpg" width="34" height="34" alt="Twitter"
                                    title="Twitter" /></a>
                    </div>
                     <div id="cmpnyfollow" runat="server">
                    <p style="float: left; clear: both; margin: 0px 0px 10px 10px;">
                        <img src="https://huntable.co.uk/images/star.png" width="16" height="16" alt="star" />
                       
                        <asp:LinkButton ID="lbFollowCompany" class="accounts-link" runat="server" OnClick="LbFollowCompanyClick"
                            Font-Names="Georgia" Font-Size="14"><asp:Label runat ="server" ID="flwcmpny" Text="Follow Company"/></asp:LinkButton></p></div>
                            <div id="cmpnyunfollow" runat="server" Visible="False" ><p style="float: left; clear: both; margin: 0px 0px 10px 10px;"><asp:LinkButton ID="LinkButton1" class="accounts-link" runat="server" 
                            Font-Names="Georgia" Font-Size="14" OnClick="Lblunfollow"><asp:Label runat ="server" ID="Label1" Text="Following"></asp:Label></asp:LinkButton></p></div>
                        <%-- <a href="#" class="accounts-link">
                            Follow Company</a>--%>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <div class="people-applied">
                        <h3>
                            People Who applied to this job also applied to</h3>
                        <ul class="people-applied-list">
                            <asp:Repeater ID="rpPeopleAppliedToSimilarJobs" runat="server">
                                <ItemTemplate>
                                    <li><strong>Job Title:</strong><%#Eval("Title")%><br /><strong>Location:</strong><%#Eval("Description")%><br /><strong>Salary:</strong> £<%#Eval("Salary")%><br /><a href="<%#UrlGenerator(Eval("Id")) %>" class="accounts-link">View Job</a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc1:CompaniesYouMayWantToFollow ID="ucCompaniesYouMayWantToFollow" runat="server" />
                </div>
            </div>
            <!-- content inner right ends -->
        </div>
           <div id="popupShare" class="popup_block">
                    <iframe id="url" src="http://huntable.co.uk/ShareMail.aspx" style="border: none;" width="100%" height="280px"
                        frameborder="0" scrolling="no"></iframe>
                                                
                </div>
                    <script type="text/javascript">
                     

                            //When you click on a link with class of poplight and the href starts with a #
                        $('a.poplight[href^=#]').click(function () {
                            if (document.getElementById('<%=hdnUserId.ClientID %>').value == "") {
                                //                                $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
                                //                                $('#dialog').dialog({
                                //                                    autoOpen: true,
                                //                                    modal: true,
                                //                                    bgiframe: true,
                                //                                    height:200,
                                //                                    width: 600,
                                //                                    buttons: {
                                //                                        "Ok": function () {
                                //                                            $(this).dialog("close");
                                //                                        },
                                //                                        "Cancel": function () {
                                //                                            $(this).dialog("close");
                                //                                        }
                                //                                    }
                                //                                });
                                //                                $('#dialogContent').parent().appendTo($("form:first"));
                                //                                return false;
                                alert("You are not logged In.Please login first");
                            }
                            else {
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
                            }
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


                     
                        function fdout() {
                            $('#fade').remove();

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
        <!-- content inner ends -->
        <script type="text/javascript" src="https://huntable.co.uk/js/jquery-1.7.1.min.js"></script>
        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
            type="text/javascript"></script>
        <script src="https://huntable.co.uk/js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
             <div id="popup12" class="popup_block">
                                                    <div class="apply-job ">
                                                        <strong>Write some covering message to the job poster.<br />
                                                            Make sure your profile is Update before you apply for this job</strong><br />
                                                        <br />
                                                        <asp:TextBox  runat="server" TextMode="MultiLine" Rows="9" ID = "txtapply" style="width: 350px;color: gray" />
                                                       <asp:Button ID="Button2" runat="server" Text="Apply Job"   OnClick="BtnApplyNowClick"
                                                            class="button-orange" />
                                                    </div>
                                                </div>
    </div>
</asp:Content>
