<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PostJob.aspx.cs" Inherits="Huntable.UI.PostJob" EnableViewState="true" %>

<%@ Register Src="UserControls/FeaturedRecruiters.ascx" TagName="FeaturedRecruters"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Scripts/SyntaxHighlighter.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
<script type="text/javascript" src="js/flipcounter.js"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="css/skin2.css" />
<link rel="stylesheet" type="text/css" href="css/skin3.css" />

<link rel="stylesheet" type="text/css" href="css/jquery.fancybox-picture.css?v=2.1.2" media="screen" />
    <style type="text/css">



	.jcarousel-skin-tango .jcarousel-container-horizontal{ width:521px;margin-left: -159px;}
	.jcarousel-skin-tango .jcarousel-clip-horizontal{ width:580px; height:134px;}
	.jcarousel-skin-tango .jcarousel-item-horizontal{ margin-right:19px;}
	.jcarousel-skin-tango .jcarousel-item{ width:83px; height:126px;}
	.jcarousel-skin-tango .jcarousel-clip-horizontal{ width:500px;}
	.jcarousel-skin-tango .jcarousel-prev-horizontal,.jcarousel-skin-tango .jcarousel-next-horizontal{ top:75px;}
	
	
	.fancybox-custom .fancybox-skin {
			box-shadow: 0 0 50px #222;
		}
			

</style>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js" type="text/javascript"></script>
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
    <script type="text/javascript" src="Scripts/shCore.js"></script>
    <script src="Scripts/shBrushJScript.js" type="text/javascript"></script>
    <script src="Scripts/ModalPopups.js" type="text/javascript"></script>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>  
      <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" language="ecmascript">
        function fnBack() {
            history.back();
        }

        function ModalPopupsAlert3() {
            var dt = new Date();
            var jobType = document.getElementById('<%=ddlJobType.ClientID%>');
            var industry = document.getElementById('<%=ddlIndustry.ClientID%>');

            var country = document.getElementById('<%=ddlCountry.ClientID%>');
            ModalPopups.Alert("jsAlert3",
        "Job Preview",
        "<div style='word-wrap: break-word;width: 600px;'>" +
        "<table>" +
        "<tr><td><font size=3 color=green><b>" + document.getElementById('<%=txtJobTitle.ClientID%>').value + "</b></font></td><tr><tr><td style=height:5px></tr>" +
        "<tr><td><font size=3 color=black><b>Job Description : </b></font></td></tr><tr><td>" + document.getElementById('<%=txtJobDesc.ClientID%>').value + "</td><tr></br>" +
        "<tr><td><font size=3 color=black><b>About Company  : " + document.getElementById('<%=txtCompanyName.ClientID%>').value + "</b></font></td></tr><tr><td>" + document.getElementById('<%=txtAboutCompany.ClientID%>').value + "</td><tr></br>" +
        "<tr><td><font size=3 color=black><b>Desired Candidate profile  : </b></font></td></tr><tr><td>" + document.getElementById('<%=txtCanprofile.ClientID%>').value + "</td><tr></br></br>" +
        "<tr><td><font size=3 color=blue><b>Essential Information  : </b></font></td></tr>" +
        "<tr><td align=left><b>Job Type  : </b>" + jobType.options[jobType.selectedIndex].text + "</td><tr>" +
        "<tr><td><table><tr><td align=left><b>Experience   : </b>" + document.getElementById('<%=txtMin.ClientID%>').value + " - " + document.getElementById('<%=txtMax.ClientID%>').value + "</td><tr>" +
        "<tr><td><table><tr><td align=left><b>Industry     : </b>" + industry.options[industry.selectedIndex].text + "</td><tr>" +

        "<tr><td><table><tr><td align=left><b>Salary       : </b>" + document.getElementById('<%=txtSalary.ClientID%>').value + "</td><tr>" +
        "<tr><td><table><tr><td align=left><b>Location     : </b>" + document.getElementById('<%=txtLocationName.ClientID%>').value + ", " + country.options[country.selectedIndex].text + "</td><tr>" +
        "<tr><td><table><tr><td align=left><b>Email        : </b>" + document.getElementById('<%=txtApplicants.ClientID%>').value + "</td><tr>" +
        "<tr><td><table><tr><td align=left><b>Website      : </b>" + document.getElementById('<%=txtExternalApplicant.ClientID%>').value + "</td><tr>" +
        "<tr><td><table><tr><td align=left><b>Poted Date   : </b>" + dt.toLocaleDateString() + "</td><tr>" +
        "</div>",
        {
            titleBackColor: "#A1B376",
            titleFontColor: "white",
            popupBackColor: "#E9E8CF",
            popupFontColor: "black",
            footerBackColor: "#A1B376",
            footerFontColor: "white"
        }
    );
        }  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%= txtSkill.ClientID %>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "HuntableWebService.asmx/SearchSkills",
                        data: "{ 'word': '" + request.term + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 1
            });
        });
	</script>.
      <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>


    <div id="content-inner">
        <h2 class="login-heading">
            Post your Job</h2>
        <div class="content-inner-left">
            <div class="upload-box" style="margin-top: 0px;">
                <div class="upload-inner">
                    <label>
                        Job Title:<strong class="red-color">*</strong>
                    </label>
                    <asp:TextBox ID="txtJobTitle" runat="server" class="textbox"></asp:TextBox>
                    <span class="err-msg-upload">
                        <asp:RequiredFieldValidator ID="valJobtitle" runat="server" Text="*" Visible="true"
                            ForeColor="Red" ValidationGroup="jobDetails" Display="Dynamic" ControlToValidate="txtJobTitle"></asp:RequiredFieldValidator></span>
                    <br />
                    <label>
                        Company name:<strong class="red-color"></strong>
                    </label>
                    <asp:TextBox ID="txtCompanyName" runat="server" class="textbox"></asp:TextBox>
                   <%-- <span class="err-msg-upload">
                        <asp:RequiredFieldValidator ID="valCompanyName" Display="Dynamic" runat="server"
                            Text="*" Visible="true" ForeColor="Red" ValidationGroup="jobDetails" ControlToValidate="txtCompanyName"
                            ErrorMessage="* Please enter your Phone Number"></asp:RequiredFieldValidator></span>--%>
                    <br />
                    <br />
                    <label>
                        Salary:<strong class="red-color">*</strong>
                    </label>
                 
                    <asp:TextBox class="textbox listbox listbox-small" ID="txtSalary" runat="server"></asp:TextBox>
                    <span class="err-msg-upload">
                        <asp:RequiredFieldValidator ID="valSalary" runat="server" Text="*" ForeColor="Red"
                            Display="Dynamic" ValidationGroup="jobDetails" ControlToValidate="txtSalary"></asp:RequiredFieldValidator>   <asp:CompareValidator 
      ID="CompareValidator1" runat="server" ControlToValidate="txtSalary"
      ErrorMessage="Please enter a numberical value." ForeColor="Red"
      Operator="DataTypeCheck" Type="Integer">Enter a non-decimal value
</asp:CompareValidator></span>
                    <asp:DropDownList ID="ddlSalaryType" AppendDataBoundItems="True" runat="server">
                        <asp:ListItem Selected="True" Text="- Please Select -"></asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <label>
                        Country:<strong class="red-color">* </strong>
                    </label>
                    <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="True" class="textbox listbox">
                        <asp:ListItem Selected="True" Text="- Please Select -"></asp:ListItem>
                        <asp:ListItem Text="Please" Value="0" />
                    </asp:DropDownList>
                    <span class="err-msg-upload">
                        <asp:RequiredFieldValidator ID="valCountry" runat="server" Text="*" ForeColor="Red"
                            ControlToValidate="ddlCountry" ValidationGroup="jobDetails" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    <br />
                    <label>
                        Location:<strong class="red-color">* </strong>
                    </label>
                    <asp:TextBox ID="txtLocationName" class="textbox" runat="server"></asp:TextBox>
                    <span class="err-msg-upload">
                        <asp:RequiredFieldValidator ID="valLocation" runat="server" Text="*" ForeColor="Red"
                            ValidationGroup="jobDetails" ControlToValidate="txtLocationName" Display="Dynamic"></asp:RequiredFieldValidator>
                    </span>
                    <br />
                    <label>
                        Description:<strong class="red-color">*</strong>
                    </label>
                    <asp:TextBox ID="txtJobDesc" runat="server" class="textarea textbox" TextMode="MultiLine"></asp:TextBox>
                    <span class="err-msg-upload">
                        <asp:RequiredFieldValidator ID="valJobdesc" runat="server" Text="*" ForeColor="Red"
                            ValidationGroup="jobDetails" ControlToValidate="txtJobDesc" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    <br />
                    <br />
                    <br />
                    <label>
                        Experience:<strong class="red-color"> *</strong></label>
                    <span>Min:</span><asp:TextBox ID="txtMin" runat="server" class="textbox listbox listbox-small"
                        Style="margin-left: 10px; margin-right: 10px;"></asp:TextBox>
                    <span>Max: </span>
                    <asp:TextBox ID="txtMax" runat="server" class="textbox listbox listbox-small" Style="margin-left: 10px;"></asp:TextBox>
                    <%--<span class="err-msg-upload">
                        <asp:RequiredFieldValidator ID="valMaxExperience" runat="server" Text="*" ForeColor="Red"
                            ValidationGroup="jobDetails" ControlToValidate="txtMax" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="valMinexperience" runat="server" Text="*" ForeColor="Red"
                            ValidationGroup="jobDetails" ControlToValidate="txtMin" Display="Dynamic"></asp:RequiredFieldValidator>
                    </span>
                    --%><br />
                    <br />
                    <label>
                        Job Type:<strong class="red-color">* </strong>
                    </label>
                    <asp:DropDownList ID="ddlJobType" class="textbox listbox" AppendDataBoundItems="True"
                        runat="server">
                        <asp:ListItem Selected="True" Text="- Please Select -"></asp:ListItem>
                    </asp:DropDownList>
                    <span class="err-msg-upload">
                        <asp:RequiredFieldValidator ID="valjobType" runat="server" Text="*" ForeColor="Red"
                            ValidationGroup="jobDetails" Display="Dynamic" ControlToValidate="ddlJobType"></asp:RequiredFieldValidator>
                    </span>
                    <br />
                    <br />
                    <label>
                        Industry:<strong class="red-color">*</strong>
                    </label>
                    <asp:DropDownList ID="ddlIndustry" runat="server" AppendDataBoundItems="True" class="textbox listbox">
                        <asp:ListItem Selected="True" Text="- Please Select -"></asp:ListItem>
                    </asp:DropDownList>
                    <span class="err-msg-upload">
                        <asp:RequiredFieldValidator ID="valIndustry" runat="server" Text="*" ForeColor="Red"
                            ValidationGroup="jobDetails" ControlToValidate="ddlIndustry" Display="Dynamic"></asp:RequiredFieldValidator>
                    </span>
                    <br />
                    <br />
                   <%-- <label>
                        Sector:<strong class="red-color"> *</strong></label>
                    <asp:DropDownList ID="ddlSector" runat="server" AppendDataBoundItems="True" class="textbox listbox">
                        <asp:ListItem Selected="True" Text="- Please Select -"></asp:ListItem>
                        <asp:ListItem Text="Please select" Value="0" />
                    </asp:DropDownList>
                    <span class="err-msg-upload">
                        <asp:RequiredFieldValidator ID="valsector" runat="server" Text="*" ForeColor="Red"
                            ValidationGroup="jobDetails" ControlToValidate="ddlSector" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    <br />
                    <br />--%>
                    <label>
                        Skill:<strong class="red-color">*</strong>
                    </label>
                   <%-- <asp:DropDownList ID="ddlSkill" runat="server" AppendDataBoundItems="True" class="textbox listbox">
                        <asp:ListItem Selected="True" Text="- Please Select -"></asp:ListItem>
                    </asp:DropDownList>--%>
                      <asp:TextBox ID="txtSkill" CssClass="textbox" runat="server"></asp:TextBox>
                    <span class="err-msg-upload">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ForeColor="Red"
                            ValidationGroup="jobDetails" ControlToValidate="txtSkill" Display="Dynamic"></asp:RequiredFieldValidator>                 </span>
                    <br />
                    <br />
                    <label>
                        Desired Candidate Profile:<strong class="red-color">*</strong></label>
                    <asp:TextBox ID="txtCanprofile" runat="server" class="textarea textbox" TextMode="MultiLine"></asp:TextBox>
                    <span class="err-msg-upload">
                        <asp:RequiredFieldValidator ID="valCanProfile" runat="server" Text="*" ForeColor="Red"
                            ValidationGroup="jobDetails" ControlToValidate="txtCanProfile" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    <br />
                    <br />
                    <label>
                        About Your Company: <strong class="red-color">* </strong>
                    </label>
                    <asp:TextBox ID="txtAboutCompany" runat="server" class="textarea textbox" TextMode="MultiLine"></asp:TextBox>
                    <span class="err-msg-upload">
                        <asp:RequiredFieldValidator ID="valAboutCompany" runat="server" Text="*" ForeColor="Red"
                            ValidationGroup="jobDetails" ControlToValidate="txtAboutCompany" Display="Dynamic"></asp:RequiredFieldValidator></span>
                    <br />
                    <br />
                   <label>
                        Picture:</label>
                    <%--<asp:CheckBox ID="cbPersonel" runat="server" Style="margin-left: 20px; margin-right: 5px;" /><strong><span>Personnel</span></strong><asp:CheckBox
                        ID="cbCompany" runat="server" Style="margin-left: 30px; margin-right: 1px;" /><strong><span>Company
                            logo</span> </strong>--%>
                    <div class="add-picture">
                        <div class="add-image " style="margin: 5px 70px 20px 0px;">
                            <a href="#">
                              <%--  <<img src="images/add-photo-thumb.jpg" class="profile-pic" width="79" height="91"
                                    alt="add-photo" /></a>--%>
                             <asp:Image ID="imgJob" runat="server" class="profile-pic" Width="100" Height="92"
                                    alt="add-photo" /></a>
                        </div>
                        </div>
                       <%-- <div class="add-image " style="<%--margin: 5px 0px 20px 0px;--%>
                      <%--      <a href="#">
                                img src="images/company-logo.png" class="profile-pic" width="150" height="92" alt="add-photo" /></a>
                                <asp:Image ID="imgJob1" runat="server" class="profile-pic" Width="100" Height="92"
                                    alt="add-photo" /></a>
                        </div>
                    </div>--%>
                   
                    
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                      <asp:HiddenField ID="hfPhoto1" runat="server" />
                        <asp:HiddenField ID="hfThumbPhoto1" runat="server" />
                        <asp:PlaceHolder runat="server" ID="phAddExample1" />
                          <asp:HiddenField ID="hfPhoto2" runat="server" />
                        <asp:HiddenField ID="hfThumbPhoto2" runat="server" />
                        <asp:PlaceHolder runat="server" ID="phAddExample2" />
                           <asp:HiddenField ID="hfPhoto3" runat="server" />
                        <asp:HiddenField ID="hfThumbPhoto3" runat="server" />
                        <asp:PlaceHolder runat="server" ID="phAddExample3" />
                           <asp:HiddenField ID="hfPhoto4" runat="server" />
                        <asp:HiddenField ID="hfThumbPhoto4" runat="server" />
                        <asp:PlaceHolder runat="server" ID="phAddExample4" />
                          <asp:HiddenField ID="hfPhoto5" runat="server" />
                        <asp:HiddenField ID="hfThumbPhoto5" runat="server" />
                        <asp:PlaceHolder runat="server" ID="phAddExample5" />
                    <%--<div >
                        <asp:FileUpload ID="fuPhoto1" runat="server" class="invite-friend-btn invite-friend-btn1" />
                        <asp:ImageButton ID="deleteIcon1" runat="server" Height="20" Width="20" ImageUrl="~/images/Delete-icon.png"
                            OnClick="DeleteIcon1Click" />
                        <asp:HiddenField ID="hfPhoto1" runat="server" />
                        <asp:HiddenField ID="hfThumbPhoto1" runat="server" />
                        <asp:PlaceHolder runat="server" ID="phAddExample1" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:FileUpload ID="fuPhoto2" runat="server" class="invite-friend-btn invite-friend-btn1" />
                        <asp:ImageButton ID="deleteIcon2" runat="server" Height="20" Width="20" ImageUrl="~/images/Delete-icon.png"
                            OnClick="DeleteIcon2Click" />
                        <asp:HiddenField ID="hfPhoto2" runat="server" />
                        <asp:HiddenField ID="hfThumbPhoto2" runat="server" />
                        <asp:PlaceHolder runat="server" ID="phAddExample2" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:FileUpload ID="fuPhoto3" runat="server" class="invite-friend-btn invite-friend-btn1" />
                        <asp:ImageButton ID="deleteIcon3" runat="server" Height="20" Width="20" ImageUrl="~/images/Delete-icon.png"
                            OnClick="DeleteIcon3Click" />
                        <asp:HiddenField ID="hfPhoto3" runat="server" />
                        <asp:HiddenField ID="hfThumbPhoto3" runat="server" />
                        <asp:PlaceHolder runat="server" ID="phAddExample3" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:FileUpload ID="fuPhoto4" runat="server" class="invite-friend-btn invite-friend-btn1" />
                        <asp:ImageButton ID="deleteIcon4" runat="server" Height="20" Width="20" ImageUrl="~/images/Delete-icon.png"
                            OnClick="DeleteIcon4Click" />
                        <asp:HiddenField ID="hfPhoto4" runat="server" />
                        <asp:HiddenField ID="hfThumbPhoto4" runat="server" />
                        <asp:PlaceHolder runat="server" ID="phAddExample4" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:FileUpload ID="fuPhoto5" runat="server" class="invite-friend-btn invite-friend-btn1" />
                        <asp:ImageButton ID="deleteIcon5" runat="server" Height="20" Width="20" ImageUrl="~/images/Delete-icon.png"
                            OnClick="DeleteIcon5Click" />
                        <asp:HiddenField ID="hfPhoto5" runat="server" />
                        <asp:HiddenField ID="hfThumbPhoto5" runat="server" />
                        <asp:PlaceHolder runat="server" ID="phAddExample5" />
                    </div>--%>
                    <div style="padding-left: 115px">
 
  
    <asp:FileUpload ID="fp" runat="server" />
    <asp:Button ID="btn" OnClick="Addpic" CausesValidation="True" runat="server" Text="ADD PICTURE" /><br />
    Supported file types: GIF,JPG,PNG
     <br/><h5 style="color: gray">You can add 5 pictures</h5>
      <div class=" jcarousel-skin-tango">
            <div class="jcarousel-container jcarousel-container-horizontal" style="
                display: block;">
                <div class="jcarousel-clip jcarousel-clip-horizontal" >
                    <ul class="first-and-second-carousel jcarousel-list jcarousel-list-horizontal" id="first-carousel"
                        style="overflow: hidden; position: relative; top: 0px; margin: 0px; padding: 0px;
                        left: 0px; width: 1300px;height:122px;">
  <asp:Repeater ID="rpic" runat="server">
  <ItemTemplate> 
  <li class="jcarousel-item jcarousel-item-horizontal jcarousel-item-1 jcarousel-item-1-horizontal"
                            style="float: left; list-style: none outside none;" jcarouselindex="1">
        <asp:ImageButton Style="margin-left: 71px;" runat="server"  ID="delete"
                                OnClick="DeleteClick" CommandArgument='<%#Container.DataItem%>' ImageUrl="images/cancel.jpg"
                                Width="10px" Height="10px" />
  <asp:Image ID="Image1" runat="server" ImageUrl=' <%# "~/LoadFile.ashx?id=" + Container.DataItem%>' Width="80px" Height="125px" />
  </li>
  </ItemTemplate>
  </asp:Repeater>
  </ul>
  </div>
   <div class="user-regis-left-ut" style="margin-top: 0px;">
                </div>
                <div class="jcarousel-prev jcarousel-prev-horizontal jcarousel-prev-disabled jcarousel-prev-disabled-horizontal"
                    style="display: block;" disabled="disabled">
                </div>
                <div class="jcarousel-next jcarousel-next-horizontal" style="display: block;">
                </div>
 </div>

  </div>
  <br />
  <br />
  <br />
  <div><%--<asp:Button ID="upb" runat="server" CausesValidation="False" class="button-orange picsty" Text="Upload now" OnClick="Uplot"  />
   <asp:Button ID="cncl" runat="server" CausesValidation="False" class= "button-orange picsty1" Text="Cancel" OnClick="Cancel" />  --%></div>            
                   
      </div>
       <br />
  <br />
                   <%--  <div>
                        <label style="clear: both;">
                            Pictures:<strong class="red-color"> *</strong></label><a href="PictureUpload.aspx"
                                class="invite-friend-btn invite-friend-btn1">Add Picture +</a>
                    </div>--%>
                    <br />
                    <br />
                    <label style="clear: both;">
                        Receive Applicants:<strong class="red-color"></strong></label>
                    <p class="upload-desc" style="margin-top: 5px">
                        Enter the email address you want the applicants to come to</p>
                    <label>
                        &nbsp;</label>
                    <asp:TextBox ID="txtApplicants" runat="server" class="textbox"></asp:TextBox>
                    <br />
                    <br />
                    <label style="clear: both;">
                        External Site Applicants:<strong class="red-color"></strong></label>
                    <p class="upload-desc">
                        Enter your web Address here if you want candidates to apply in your site directly</p>
                    <label>
                        &nbsp;</label>
                    <asp:TextBox ID="txtExternalApplicant" runat="server" class="textbox"></asp:TextBox>
                    <br />
                    <br />
                     <label style="clear: both;">
                        JobReference Number:<strong class="red-color"></strong></label>
                    <asp:TextBox ID="txtreference" runat="server" class="textbox"></asp:TextBox>
                    <br/>
                    <br/>
                    <label>
                        &nbsp;</label>
                    <%-- <a href="#" onclick="ModalPopupsAlert3();" class="button-orange button-green-upload ">
                        Preview before Posting</a>--%>
                    <asp:Button runat="server" CssClass="button-orange button-green-upload " Text="Preview before Posting"
                        ID="btnPreview" PostBackUrl="~/PreviewJob.aspx" OnClick="TempPhotosUpload"  ValidationGroup="jobDetails" />
                    <%-- <a href="#" onclick="ModalPopupsAlert3();" class="button-orange button-green-upload ">
                        Preview before Posting</a>--%>
                    <asp:LinkButton runat="server" ValidationGroup="jobDetails" ID="lbBtnPostNow" OnClick="lbPostNowCLick"
                        class="button-green button-green-upload " Style="margin-left: 10px;">Post Now</asp:LinkButton>
                </div>
                <div class="posting-block">
                    <div class="posting-block-inner">
                        1 Click job posting & Candidate search <a rel="tooltip" title="You can endorse your collegue or friend you have worked together.This is your professional opinion so keep it professional.Tip: Keep it clear and simple, Your friend can delete your endorsement.if they are innappropriate. ">
                            <img src="images/icon-tips.png" width="14" height="15" alt="tips" /></a>
                        <div class="blue-box blue-box-upload">
                            By Clicking here, your job will be posted, at the same time, relevant matching Candidates
                            will allow be listed for your convenience
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:LinkButton runat="server" ID="lbBtnCandidateSearch" OnClick="BtnCandidateSearchClick"
                            class="button-orange button-green-upload" Style="margin-top: 10px;" ValidationGroup="jobDetails">Post &amp; Search</asp:LinkButton>
                        <div class="req-field">
                            <strong class="red-color">* </strong>Required Fields</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="content-inner-right">
            <div class="post-job-middle">
                Available Credit:
                <asp:Label ID="lblCredit" runat="server" Font-Size="Small" ForeColor="Blue"></asp:Label>
                jobs<br />
                <br />
                <div style="float: right; margin-right: 63px;">
                    <asp:LinkButton ID="lnkcredit" runat="server" Text="Buy more credit Now:" BackColor="Yellow"
                        OnClick="LnkcreditClick" CssClass="button-green button-green-upload"> </asp:LinkButton>
                </div>
            </div>
            <p class="margin-top-visible">
                &nbsp;</p>
            <div class="post-job-right">
                <strong class="blue-color">
                  <a href="MyPostedJobs.aspx" > <asp:Label ID="lblPostedJobs" runat="server" Font-Size="Small" ForeColor="Blue"></asp:Label></a> </strong><br />
                <br />
                <div class="applicant-box">
                    <asp:Panel ID="panApplications" runat="server" Width="150px" Height="65px" HorizontalAlign="Center"
                        BackColor="Snow">
                      <a href="Applicants.aspx"><asp:Label ID="lblMyApplications" runat="server" ForeColor="Blue" Font-Size="Small"></asp:Label></a> 
                        <%--<asp:Image ID="imgApplications" runat="server" ImageUrl="~/image/lines.png" Width="91px" />--%>
                    </asp:Panel>
                    <img src="images/applicant-bar-graph.png" width="163" height="63" alt="applicant-bar-graph" /></div>
            </div>
            <p class="margin-top-visible">
                &nbsp;</p>
            <uc2:FeaturedRecruters ID="ucFeaturedRecruters" runat="server" />
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
     <script type="text/javascript" src="js/jquery.jcarousel.min.js"></script>
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
 <%--   </span>--%>
</asp:Content>
<%--</div>
    </div>--%>
