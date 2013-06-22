<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="EditProfilePage.aspx.cs" Inherits="Huntable.UI.EditProfilePage" %>

<%@ Register Src="~/UserControls/ProfileCompletionSteps.ascx" TagPrefix="uc1" TagName="ProfileCompletionSteps" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />
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
          function validate() {
              var uploadcontrol = $('#MainContent_uploadResume').val();
              if (uploadcontrol == "") {
                  alert("Please choose a file!");
                  return false;
              }
              //Regular Expression for fileupload control.
              var allowedexts = ".doc.DOC.docx.DOCX.txt.TXT.rtf.RTF.pdf.PDF";
              if (uploadcontrol.length > 0 && uploadcontrol.indexOf('.') > 0) {
                  var fullfilename = uploadcontrol.split('.');
                  var fileext = fullfilename[fullfilename.length - 1];
                  //Checks with the control value.
                  if (allowedexts.indexOf(fileext) > 0) {
                      return true;
                  }
                  else {
                      //If the condition not satisfied shows error message.
                      $('#MainContent_uploadResume').val('');
                      alert("Only .DOC,.DOCX,.TXT,.PDF files are allowed!");
                      return false;
                  }
              }
          }
    </script>
     <script type="text/javascript">
         function overly(id) {
             el = document.getElementById('Div1');
             $('#Div1').show();

             $('#<%= Label1.ClientID %>').text(id);

         }
         $(document).ready(function () {

             $('#ximg1').click(function () {
                 window.location.replace("ViewUserProfile.aspx");
                 $('#Div1').hide();
                 return false;
             });
         });
    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#dlLanguageLevel dt strong").on('click', function () {
                $("#dlLanguageLevel dd ul").toggle();
            });

            $("#dlStrongSkillHowLong dt strong").click(function () {
                $("#dlStrongSkillHowLong dd ul").toggle();
            });

            $("#dlExpertSkillHowLong dt strong").click(function () {
                $("#dlExpertSkillHowLong dd ul").toggle();
            });

            $("#dlGoodSkillHowLong dt strong").click(function () {
                $("#dlGoodSkillHowLong dd ul").toggle();
            });

            $(document).bind('click', function (e) {
                var $clicked = $(e.target);
                if (!$clicked.parents().hasClass("dropdown"))
                    $(".dropdown dd ul").hide();
            });
        });

        //        function languagelevelclick() {
        //            $("#dlLanguageLevel dd ul").toggle();
        //        }

        function Check() {
            var fromYear = document.getElementById("ddlFromYear");
            var fromYearText = fromYear.options[fromYear.selectedIndex].text;

            var toYear = document.getElementById("ddlToYear");
            var toYearText = toYear.options[toYear.selectedIndex].text;

            var fromMonth = document.getElementById("ddlFromMonth");
            var fromMonthText = fromMonth.options[fromMonth.selectedIndex].value;

            var toMonth = document.getElementById("ddlToMonth");
            var toMonthText = toMonth.options[toMonth.selectedIndex].value;

            var fromDateString = fromMonthText + "/01/" + fromYearText;
            var toDateString = toMonthText + "/01/" + toYearText;

            var fromDate = Date.parse(fromDateString);
            var toDate = Date.parse(toDateString);
            if (fromDate > toDate) {
                alert("From date can not be greater than to date.");    
                return false;

            }
            var currentDate = Date.parse(new Date());

            if (fromDate > currentDate || toDate>currentDate) {
                alert("Does not Exceed the current date.");
                return false;
            }
            return true;
        }

        function Check1() {
            var fromYear = document.getElementById("ddlFromYear1");
            var fromYearText = fromYear.options[fromYear.selectedIndex].text;

            var toYear = document.getElementById("ddlToYear1");
            var toYearText = toYear.options[toYear.selectedIndex].text;

            var fromMonth = document.getElementById("ddlFromMonth1");
            var fromMonthText = fromMonth.options[fromMonth.selectedIndex].value;

            var toMonth = document.getElementById("ddlToMonth1");
            var toMonthText = toMonth.options[toMonth.selectedIndex].value;

            var fromDateString = fromMonthText + "/01/" + fromYearText;
            var toDateString = toMonthText + "/01/" + toYearText;

            var fromDate = Date.parse(fromDateString);
            var toDate = Date.parse(toDateString);
            if (fromDate > toDate) {
                alert("From date can not be greater than to date.");
                return false;
            }
            var currentDate = Date.parse(new Date());

            if (fromDate > currentDate || toDate > currentDate) {
                alert("Does not Exceed the current date.");
                return false;
            }
            return true;
        }

        function Check2() {
            var fromYear = document.getElementById("ddlFromYear2");
            var fromYearText = fromYear.options[fromYear.selectedIndex].text;

            var toYear = document.getElementById("ddlToYear2");
            var toYearText = toYear.options[toYear.selectedIndex].text;

            var fromMonth = document.getElementById("ddlFromMonth2");
            var fromMonthText = fromMonth.options[fromMonth.selectedIndex].value;

            var toMonth = document.getElementById("ddlToMonth2");
            var toMonthText = toMonth.options[toMonth.selectedIndex].value;

            var fromDateString = fromMonthText + "/01/" + fromYearText;
            var toDateString = toMonthText + "/01/" + toYearText;

            var fromDate = Date.parse(fromDateString);
            var toDate = Date.parse(toDateString);
            if (fromDate > toDate) {
                alert("From date can not be greater than to date.");
                return false;
            }
            var currentDate = Date.parse(new Date());

            if (fromDate > currentDate || toDate > currentDate) {
                alert("Does not Exceed the current date.");
                return false;
            }
            return true;
        }

        function Check3() {
            var fromYear = document.getElementById("ddlFromYear3");
            var fromYearText = fromYear.options[fromYear.selectedIndex].text;

            var toYear = document.getElementById("ddlToYear3");
            var toYearText = toYear.options[toYear.selectedIndex].text;

            var fromMonth = document.getElementById("ddlFromMonth3");
            var fromMonthText = fromMonth.options[fromMonth.selectedIndex].value;

            var toMonth = document.getElementById("ddlToMonth3");
            var toMonthText = toMonth.options[toMonth.selectedIndex].value;

            var fromDateString = fromMonthText + "/01/" + fromYearText;
            var toDateString = toMonthText + "/01/" + toYearText;

            var fromDate = Date.parse(fromDateString);
            var toDate = Date.parse(toDateString);
            if (fromDate > toDate) {
                alert("From date can not be greater than to date.");
                return false;
            }
            var currentDate = Date.parse(new Date());

            if (fromDate > currentDate || toDate > currentDate) {
                alert("From date or to date cannot be greater than current date.");
                return false;
            }
            return true;
        }
        
    </script>
    <style>
        #mydiv {
    position:fixed;
    top: 50%;
    left: 50%;
    margin-top: -9em; /*set to a negative number 1/2 of your height*/
    margin-left: -15em; /*set to a negative number 1/2 of your width*/
    border: 1px solid #ccc;
    background-color: #f3f3f3;
}
    </style>
    <script type="text/javascript">
        $(function () {

            var msie6 = $.browser == 'msie' && $.browser.version < 7;
            if (!msie6) {
                var top = $('#bx5').offset().top;
                $(window).scroll(function (event) {
                    var y = $(this).scrollTop();
                    if (y >= 14) { $('#bx5').addClass('fixed'); }
                    else { $('#bx5').removeClass('fixed'); }
                });
            }
        });</script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>
       <div   id="Div1"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="Label1" runat="server"></asp:Label></div>
     <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg1"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <h2 class="login-heading login-heading-profile">
                    You can review, edit &amp; update it here</h2>
                <%--   <b class="blue-color">Your details are extracted from your resume, CV and displayed
                    as your Huntable profile. You can edit if necessary.</b>--%>
                <ul class="menu collapsible " style="margin-top: 10px; margin-left: 0px;">
                    <li class="expand"><a href="#" class="menu-ash menu-ash-inner"><strong>Summary</strong><b>&nbsp;</b></a>
                        <ul class="acitem">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <li>
                                        <div class="summary-box">
                                            <%-- <asp:ValidationSummary ID="vsSummary" runat="server" ShowMessageBox="true" ShowSummary="false"
                                        ForeColor="Red" DisplayMode="BulletList" HeaderText="Following errors occurred" />--%>
                                            <asp:TextBox ID="txtSummary" runat="server" CssClass="textarea textarea-summary"
                                                ValidationGroup="SummaryEdit" Columns="2" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSummary" runat="server" ControlToValidate="txtSummary"
                                                ForeColor="Red" Text="*" Display="Dynamic" ErrorMessage="Please enter summary"
                                                ValidationGroup="SummaryEdit"></asp:RequiredFieldValidator>
                                            <%--  <asp:RegularExpressionValidator ID="revSummary" runat="server" ControlToValidate="txtSummary"
                                        ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter valid text in summary box"
                                        ValidationExpression="^[a-z,A-Z]{1,200}$"></asp:RegularExpressionValidator>--%>
                                        </div>
                                        <div class="upload-inner">
                                            <label>
                                                Position Looking for:</label>
                                            <asp:TextBox ID="txtPositionLookingFor" runat="server" ValidationGroup="SummaryEdit"
                                                CssClass="textbox" />
                                            <%--<asp:RequiredFieldValidator ID="rfvPosition" runat="server" ControlToValidate="txtPositionLookingFor"
                                                ValidationGroup="SummaryEdit" ForeColor="Red" Text="*" Display="Dynamic" ErrorMessage="Please fill the Position field"></asp:RequiredFieldValidator>--%>
                                            <%--  <asp:RegularExpressionValidator ID="revPosition" runat="server" ControlToValidate="txtPositionLookingFor"
                                        ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter valid text in Positionlookingfor"
                                        ValidationExpression="^[a-z,A-Z]{1,50}$"></asp:RegularExpressionValidator>--%>
                                            <br />
                                            <label>
                                                Availabel Now</label>
                                            <asp:CheckBox ID="cbAvailaleNow" runat="server" /><br />
                                            <asp:Button ID="btnSumSave" runat="server" Text="Save" OnClick="BtnSummarySaveClick"
                                                ValidationGroup="SummaryEdit" CssClass="pButton" Font-Bold="true" Font-Size="Small" />
                                            <br />
                                            <div id="lblSummaryMessage" runat="server" style="color: Green;" visible="false">
                                                Details saved successfully.
                                            </div>
                                        </div>
                                    </li>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSumSave" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="updateProgress6" AssociatedUpdatePanelID="UpdatePanel6" runat="server">
                                <ProgressTemplate>
                                    <div id="mydiv" style="text-align: center">
                                        <img src="images/loadingBar.gif" style="height: 50px" alt="Update in progress" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ul>
                    </li>
                    <li><a href="#" class="menu-ash menu-ash-inner"><strong>Experience - Current</strong><b>&nbsp;</b></a>
                        <ul class="acitem">
                            <li>
                                <div class="upload-inner">
                                    <br />
                                    <label>
                                        Total Exp
                                    </label>
                                    <asp:TextBox ID="txtExp" runat="server" CssClass="textbox" /><br />
                                </div>
                                <div class="upload-inner">
                                    <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table width="550px;">
                                                <tr>
                                                    <td>
                                                        <asp:HiddenField ID="hfUserId" runat="server" />
                                                        <asp:Repeater ID="rpCurrentExperience" runat="server" OnItemDataBound="RpCurrentExperienceItemDataBound"
                                                            OnItemCommand="RpCurrentExperienceItemCommand" OnItemCreated="RpCurrentExperienceItemCreated">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:HiddenField ID="id" runat="server" />
                                                                        <table id="tblReadonly" runat="server" width="520px;">
                                                                            <tr>
                                                                                <td colspan="5" class="ButtonCellStyle">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 20%;">
                                                                                    <label>
                                                                                        Title:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox" Text='<% #Eval("JobTitle") %>'></asp:TextBox>
                                                                                    <%-- <asp:Label ID="lblTitle" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Company Name:</label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCurCompany" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    <%--<asp:Label ID="lblCompany" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Period:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCPeriod" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    <%--<asp:Label ID="lblDates" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Description:</label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCDesc" runat="server" Width="237px" CssClass="textbox" TextMode="MultiLine"></asp:TextBox>
                                                                                    <%-- <asp:Label ID="lblDescription" runat="server" Width="100%" CssClass="paraGraphtext"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Level:</label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCLevel" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Industry:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCInd" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    <%--<asp:Label ID="lblIndustry" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Skill:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCSkill" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="height: 15px;">
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="newRows">
                                                                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="pButton"
                                                                                            Font-Bold="true" Font-Size="Small" CausesValidation="false" />&nbsp;&nbsp;&nbsp;
                                                                                        <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CssClass="pButton"
                                                                                            Font-Bold="true" Font-Size="Small" CausesValidation="false" />
                                                                                            <asp:Button ID="btnPast" runat="server" Text="Move to Past Experience" CommandName="Move" CssClass="pButton"
                                                                                            Font-Bold="true" Font-Size="Small" CausesValidation="false" />
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table id="tblEditMode" runat="server" visible="false">
                                                                            <tr>
                                                                                <td colspan="5" class="ButtonCellStyle">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Job Title
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtJobTitle" runat="server" CssClass="textbox" ValidationGroup="SaveCurrentExp" />
                                                                                    <asp:RequiredFieldValidator ID="rfvJob" runat="server" ErrorMessage="*" ControlToValidate="txtJobTitle"
                                                                                        ForeColor="Red" ValidationGroup="SaveCurrentExp" /><br />
                                                                                    <%-- <asp:RegularExpressionValidator ID="revJob" runat="server" ControlToValidate="txtJobTitle"
                                                                                        ForeColor="Red" ErrorMessage="*Please enter valid text" ValidationExpression="^[a-zA-Z]$"
                                                                                        Display="Dynamic"></asp:RegularExpressionValidator><br />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Company Name
                                                                                    </label>
                                                                                    <label>
                                                                                        <asp:CheckBox ID="cbPresent" runat="server" Text="Present" TextAlign="Left" AutoPostBack="false" /></label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCompany" runat="server" CssClass="textbox" ValidationGroup="SaveCurrentExp" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="txtCompany" ForeColor="Red" ValidationGroup="SaveCurrentExp" />
                                                                                    <%-- <asp:RegularExpressionValidator ID="revCompany" runat="server" ControlToValidate="txtCompany"
                                                                                        ForeColor="Red" ErrorMessage="*Please enter valid text" ValidationExpression="^[a-zA-Z]$"
                                                                                        Display="Dynamic"></asp:RegularExpressionValidator><br />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Period
                                                                                    </label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        From
                                                                                    </label>
                                                                                </td>
                                                                                <td style="vertical-align: middle">
                                                                                    <asp:DropDownList ID="ddlFromMonth" ClientIDMode="Static" runat="server" ValidationGroup="SaveCurrentExp">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlFromMonth" ForeColor="Red" ValidationGroup="SaveCurrentExp" />
                                                                                    <%-- <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlFromMonth" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1"
                                                                                        ValidationGroup="SaveCurrentExp" />--%>
                                                                                    &nbsp;-&nbsp;
                                                                                    <asp:DropDownList ID="ddlFromYear" runat="server" ClientIDMode="Static" ValidationGroup="SaveCurrentExp">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlFromYear" ForeColor="Red" ValidationGroup="SaveCurrentExp" />
                                                                                    <%--   <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlFromYear" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1"
                                                                                        ValidationGroup="SaveCurrentExp" />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <label>
                                                                                        To
                                                                                    </label>
                                                                                </td>
                                                                                <td style="vertical-align: middle">
                                                                                    <asp:DropDownList ID="ddlToMonth" ClientIDMode="Static" runat="server" ValidationGroup="SaveCurrentExp">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlToMonth" ForeColor="Red" ValidationGroup="SaveCurrentExp" />
                                                                                    <%-- <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Check month" ForeColor="Red"
                                                                                        ControlToValidate="ddlToMonth" Operator="GreaterThanEqual" Type="Date" ControlToCompare="ddlFromMonth"
                                                                                        ValidationGroup="SaveCurrentExp" />--%>
                                                                                    &nbsp;-&nbsp;
                                                                                    <asp:DropDownList ID="ddlToYear" ClientIDMode="Static" runat="server" ValidationGroup="SaveCurrentExp">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlToYear" ForeColor="Red" ValidationGroup="SaveCurrentExp" />
                                                                                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        Text="*" ControlToValidate="ddlToYear" Operator="GreaterThanEqual" Type="Integer"
                                                                                        ControlToCompare="ddlFromYear" ValidationGroup="SaveCurrentExp" /><br />
                                                                                    <br />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Description 
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="textarea textarea-summary"
                                                                                        Width="237px" ValidationGroup="SaveCurrentExp"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="txtDescription" ForeColor="Red" ValidationGroup="SaveCurrentExp"></asp:RequiredFieldValidator>
                                                                                    <%--   <asp:RegularExpressionValidator ID="revDesc" runat="server" ControlToValidate="txtDescription"
                                                                                        ForeColor="Red" ErrorMessage="*Please enter valid text" ValidationExpression="^[a-zA-Z]$"
                                                                                        Display="Dynamic"></asp:RegularExpressionValidator><br />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Level
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:DropDownList ID="ddlLevel" runat="server" CssClass="textbox listbox" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Industry
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:DropDownList ID="ddlIndustry" runat="server" CssClass="textbox listbox" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Skill
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <%--  <asp:ListBox ID="lbSkills" SelectionMode="Multiple" runat="server" CssClass="textbox listbox" />--%>
                                                                                    <asp:TextBox runat="server" ID="txtSkill" CssClass="skilltextbox" ValidationGroup="SaveCurrentExp"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Town
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="txtTown" runat="server" Width="237px" CssClass="textbox" ValidationGroup="SaveCurrentExp"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="txtTown" ForeColor="Red" ValidationGroup="SaveCurrentExp"></asp:RequiredFieldValidator>
                                                                                    <%--   <asp:RegularExpressionValidator ID="revTown" runat="server" ControlToValidate="txtTown"
                                                                                        ForeColor="Red" ErrorMessage="*Please enter valid text" ValidationExpression="^[a-zA-Z]$"
                                                                                        Display="Dynamic"></asp:RegularExpressionValidator><br />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Country
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:DropDownList runat="server" ID="ddlPstExpCountry" CssClass="textbox listbox">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="height: 15px;">
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="newRows">
                                                                                        <asp:Button ID="btnSave01" runat="server" Text="Save" CommandName="Save" ValidationGroup="SaveCurrentExp"
                                                                                            CssClass="pButton" Font-Bold="true" Font-Size="Small" OnClientClick="return Check()" />&nbsp;&nbsp;&nbsp;
                                                                                        <asp:Button ID="btnCancel01" runat="server" Text="Cancel" CommandName="Cancel" CssClass="pButton"
                                                                                            Font-Bold="true" Font-Size="Small" CausesValidation="false" />&nbsp;
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr id="trPortfoliosAchievementsVideos" runat="server">
                                                                                <td colspan="2">
                                                                                    <a class="accounts-link" onclick="toggleAttachments();">
                                                                                        <img src="images/add-icon.png" width="14" height="14" alt="add">
                                                                                        Add Achievements, Portfolios &amp; Videos for this job</a>
                                                                                    <div class="slidingDiv" id="divAttachments">
                                                                                        <asp:Button ID="btn__save" runat="server" Text="Save" CommandName="Save" CssClass="pButton"
                                                                                            Font-Bold="true" Font-Size="Small" CausesValidation="false" Style="margin-left: 138px;" Visible="False" />
                                                                                        <br />
                                                                                        <br />
                                                                                        <iframe id="ifPortfoliosAchievementsVideos" runat="server" style="border: 0; width: 100%;">
                                                                                        </iframe>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ButtonCellStyle">
                                                        <div class="newRowDiv">
                                                            <asp:Button ID="btnAddNewPresentEmploymentHistory" runat="server" Text="+ Add More Experience"
                                                                OnClick="BtnAddNewPresentEmploymentHistoryClick" CausesValidation="false" CssClass="pButton" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rpCurrentExperience" EventName="ItemCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnAddNewPresentEmploymentHistory" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="updateProgressBar1" AssociatedUpdatePanelID="pnl" runat="server">
                                        <ProgressTemplate>
                                            <div id="mydiv" style="text-align: center">
                                                <img src="images/loadingBar.gif" style="height: 50px" alt="Update in progress" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </li>
                        </ul>
                    </li>
                    <li><a href="#" class="menu-ash menu-ash-inner"><strong>Experience - Past</strong><b>&nbsp;</b></a>
                        <ul class="acitem">
                            <li>
                                <div class="upload-inner">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table width="550px">
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="rpPastEmployment" runat="server" OnItemDataBound="RpPastEmploymentItemDataBound"
                                                            OnItemCommand="RpPastEmploymentItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:HiddenField ID="id" runat="server" />
                                                                        <table id="tblReadonly" runat="server" width="520px">
                                                                            <tr>
                                                                                <td colspan="5" class="ButtonCellStyle">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 20%;">
                                                                                    <label>
                                                                                        Title:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    <%--<asp:Label ID="lblTitle" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Company Name:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCurCompany" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    <%-- <asp:Label ID="lblCompany" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Period:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCPeriod" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    <%--<asp:Label ID="lblDates" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Description:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCDesc" runat="server" Width="237px" CssClass="textbox" TextMode="MultiLine"></asp:TextBox>
                                                                                    <%--<asp:Label ID="lblDescription" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Level:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCLevel" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    <%--<asp:Label ID="lblDescription" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Industry:</label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCInd" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    <%--<asp:Label ID="lblIndustry" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Skill:</label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCSkill" runat="server" CssClass="skilltextbox"></asp:TextBox>
                                                                                    <%--<asp:Label ID="lblSkills" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="height: 15px;">
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="newRows">
                                                                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="pButton"
                                                                                            Font-Bold="true" Font-Size="Small" CausesValidation="false" />&nbsp;&nbsp;&nbsp;
                                                                                        <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CssClass="pButton"
                                                                                            Font-Bold="true" Font-Size="Small" CausesValidation="false" />
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table id="tblEditMode" runat="server" visible="false">
                                                                            <tr>
                                                                                <td colspan="5" class="ButtonCellStyle">
                                                                                    <%--   <asp:Button ID="btnSave1" runat="server" Text="Save" CommandName="Save01" CssClass="pButton" />
                                                                                    <asp:Button ID="btnCancel1" runat="server" Text="Cancel" CommandName="Cancel01" CssClass="pButton"
                                                                                        CausesValidation="false" />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Job Title
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtJobTitle" runat="server" CssClass="textbox" ValidationGroup="PastEmployment" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="txtJobTitle" ForeColor="Red" ValidationGroup="PastEmployment" /><br />
                                                                                    <%-- <asp:RegularExpressionValidator ID="revJobPast" runat="server" ControlToValidate="txtJobTitle"
                                                                                        ForeColor="Red" ErrorMessage="*Please enter valid text" ValidationExpression="^[a-zA-Z]$"
                                                                                        Display="Dynamic"></asp:RegularExpressionValidator><br />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Company Name
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCompany" runat="server" CssClass="textbox" ValidationGroup="PastEmployment" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="txtCompany" ForeColor="Red" ValidationGroup="PastEmployment" /><br />
                                                                                    <%-- <asp:RegularExpressionValidator ID="revCompany" runat="server" ControlToValidate="txtCompany"
                                                                                        ForeColor="Red" ErrorMessage="*Please enter valid text" ValidationExpression="^[a-zA-Z]$"
                                                                                        Display="Dynamic"></asp:RegularExpressionValidator><br />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Period
                                                                                    </label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        From
                                                                                    </label>
                                                                                </td>
                                                                                <td style="vertical-align: middle">
                                                                                    <asp:DropDownList ID="ddlFromMonth1" ClientIDMode="Static" runat="server" CssClass="textbox listbox"
                                                                                        ValidationGroup="PastEmployment">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlFromMonth1" ForeColor="Red" ValidationGroup="PastEmployment" />
                                                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlFromMonth1" Operator="GreaterThanEqual" Type="Integer"
                                                                                        ValueToCompare="1" ValidationGroup="PastEmployment" />
                                                                                    &nbsp;-&nbsp;
                                                                                    <asp:DropDownList ID="ddlFromYear1" ClientIDMode="Static" runat="server" CssClass="textbox listbox"
                                                                                        ValidationGroup="PastEmployment">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlFromYear1" ForeColor="Red" ValidationGroup="PastEmployment" />
                                                                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlFromYear1" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1"
                                                                                        ValidationGroup="PastEmployment" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        To
                                                                                    </label>
                                                                                </td>
                                                                                <td style="vertical-align: middle">
                                                                                    <asp:DropDownList ID="ddlToMonth1" ClientIDMode="Static" runat="server" CssClass="textbox listbox"
                                                                                        ValidationGroup="PastEmployment">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlToMonth1" ForeColor="Red" ValidationGroup="PastEmployment" />
                                                                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlToMonth1" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1"
                                                                                        ValidationGroup="PastEmployment" />
                                                                                    &nbsp;-&nbsp;
                                                                                    <asp:DropDownList ID="ddlToYear1" ClientIDMode="Static" runat="server" CssClass="textbox listbox"
                                                                                        ValidationGroup="PastEmployment">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlToYear1" ForeColor="Red" ValidationGroup="PastEmployment" />
                                                                                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlToYear1" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1"
                                                                                        ValidationGroup="PastEmployment" /><br />
                                                                                    <%-- <br />
                                                                                    <asp:CheckBox ID="cbPresent" runat="server" Text="Present" />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Description
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="textarea textarea-summary"
                                                                                        Width="237px" ValidationGroup="PastEmployment"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="txtDescription" ForeColor="Red" ValidationGroup="PastEmployment"></asp:RequiredFieldValidator>
                                                                                    <%-- <asp:RegularExpressionValidator ID="revDescription" runat="server" ControlToValidate="txtDescription"
                                                                                        ForeColor="Red" ErrorMessage="*Please enter valid text" ValidationExpression="^[a-zA-Z]$"
                                                                                        Display="Dynamic"></asp:RegularExpressionValidator><br />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Level
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:DropDownList ID="ddlLevel" runat="server" CssClass="textbox listbox" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Industry
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:DropDownList ID="ddlIndustry" runat="server" CssClass="textbox listbox" ValidationGroup="PastEmployment"
                                                                                        AppendDataBoundItems="true">
                                                                                        <asp:ListItem Text="Select Industry" Value="-1" />
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlIndustry" ForeColor="Red" InitialValue="-1" ValidationGroup="PastEmployment"></asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="height: 20px;">
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Skill
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <%-- <asp:ListBox ID="lbSkills" SelectionMode="Multiple" runat="server" CssClass="textbox listbox" />--%>
                                                                                    <asp:TextBox runat="server" ID="txtSkill" CssClass="skilltextbox" ValidationGroup="PastEmployment"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="height: 20px;">
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Town
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="txtTown" runat="server" Width="237px" CssClass="textarea textarea-summary"
                                                                                        ValidationGroup="PastEmployment"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="txtTown" ForeColor="Red" ValidationGroup="PastEmployment"></asp:RequiredFieldValidator><br />
                                                                                    <%--<asp:RegularExpressionValidator ID="revTown" runat="server" ControlToValidate="txtTown"
                                                                                        ForeColor="Red" ErrorMessage="*Please enter valid text" ValidationExpression="^[a-zA-Z]$"
                                                                                        Display="Dynamic"></asp:RegularExpressionValidator><br />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Country
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:DropDownList runat="server" ID="ddlPstExpCountry" CssClass="textbox listbox">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="newRows">
                                                                                        <asp:Button ID="btnSaveEdit" runat="server" Text="Save" CommandName="Save" ValidationGroup="PastEmployment"
                                                                                            CssClass="pButton" Font-Bold="true" Font-Size="Small" OnClientClick="return Check1()" />&nbsp;&nbsp;&nbsp;
                                                                                        <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" CommandName="Cancel"
                                                                                            CssClass="pButton" Font-Bold="true" Font-Size="Small" CausesValidation="false" />
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <a class="accounts-link" onclick="toggleAttachments();">
                                                                                        <img src="images/add-icon.png" width="14" height="14" alt="add">
                                                                                        Add Achievements, Portfolios &amp; Videos for this job</a>
                                                                                    <div class="slidingDiv" id="divAttachments">
                                                                                        <iframe id="ifPortfoliosAchievementsVideos" runat="server" style="border: 0; width: 100%;">
                                                                                        </iframe>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ButtonCellStyle">
                                                        <div class="newRowDiv">
                                                            <asp:Button ID="btnAddNewPastEmployment" runat="server" Text="+ Add More Experience"
                                                                OnClick="BtnAddNewPastEmploymentClick" CausesValidation="false" CssClass="pButton" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rpPastEmployment" EventName="ItemCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnAddNewPastEmployment" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="updateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                                        <ProgressTemplate>
                                            <div  id="mydiv" style="text-align: center">
                                                <img src="images/loadingBar.gif" style="height: 50px" alt="Update in progress" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </li>
                        </ul>
                    </li>
                    <li><a href="#" class="menu-ash menu-ash-inner"><strong>Education</strong><b>&nbsp;</b></a>
                        <ul class="acitem">
                            <li>
                                <div class="upload-inner">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table xml:lang="550px">
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="rpEducation" runat="server" OnItemDataBound="RpEducationItemDataBound"
                                                            OnItemCommand="RpEducationItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:HiddenField ID="id" runat="server" />
                                                                        <table id="tblReadonly" runat="server" width="520px">
                                                                            <tr>
                                                                                <td colspan="5" class="ButtonCellStyle">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 20%;">
                                                                                    <label>
                                                                                        College/University:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCollegeDet" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    <%--<asp:Label ID="lblCollege" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Degree:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDeg" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    <%--<asp:Label ID="lblDegree" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Period:</label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCPeriod" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    <%-- <asp:Label ID="lblDates" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Description:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCDesc" runat="server" Width="237px" CssClass="textbox" TextMode="MultiLine"></asp:TextBox>
                                                                                    <%--<asp:Label ID="lblDescription" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="newRows">
                                                                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="pButton"
                                                                                            Font-Bold="true" Font-Size="Small" CausesValidation="false" />&nbsp;&nbsp;&nbsp;
                                                                                        <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CssClass="pButton"
                                                                                            Font-Bold="true" Font-Size="Small" CausesValidation="false" />
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table id="tblEditMode" runat="server" visible="false">
                                                                            <tr>
                                                                                <td colspan="5" class="ButtonCellStyle">
                                                                                    <%-- <asp:Button ID="btnSave2" runat="server" Text="Save" CommandName="Save" CssClass="pButton" Font-Bold="true" Font-Size="Small" />&nbsp;&nbsp;&nbsp;
                                                                                    <asp:Button ID="btnCancel2" runat="server" Text="Cancel" CommandName="Cancel" CssClass="pButton" Font-Bold="true" Font-Size="Small"
                                                                                        CausesValidation="false" />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        College/University
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCollege" runat="server" CssClass="textbox" ValidationGroup="Education" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="txtCollege" ForeColor="Red" ValidationGroup="Education" /><br />
                                                                                    <%-- <asp:RegularExpressionValidator ID="revCollege" runat="server" ControlToValidate="txtCollege"
                                                                                        ForeColor="Red" ErrorMessage="*Please enter valid text" ValidationExpression="^[a-zA-Z]$"
                                                                                        Display="Dynamic"></asp:RegularExpressionValidator><br />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Degree</label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDegree" runat="server" CssClass="textbox" ValidationGroup="Education" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="txtDegree" ForeColor="Red" ValidationGroup="Education" /><br />
                                                                                    <%--  <asp:RegularExpressionValidator ID="revDegree" runat="server" ControlToValidate="txtDegree"
                                                                                        ForeColor="Red" ErrorMessage="*Please enter education qualification" ValidationExpression="^[a-zA-Z]$"
                                                                                        Display="Dynamic"></asp:RegularExpressionValidator><br />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Period</label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        From
                                                                                    </label>
                                                                                </td>
                                                                                <td style="vertical-align: middle">
                                                                                    <asp:DropDownList ID="ddlFromMonth2" ClientIDMode="Static" runat="server" CssClass="textbox listbox"
                                                                                        ValidationGroup="Education">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlFromMonth2" ForeColor="Red" ValidationGroup="Education" />
                                                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlFromMonth2" Operator="GreaterThanEqual" Type="Integer"
                                                                                        ValueToCompare="1" ValidationGroup="Education" />
                                                                                    &nbsp;-&nbsp;
                                                                                    <asp:DropDownList ID="ddlFromYear2" ClientIDMode="Static" runat="server" CssClass="textbox listbox"
                                                                                        ValidationGroup="Education">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlFromYear2" ForeColor="Red" ValidationGroup="Education" />
                                                                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlFromYear2" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1"
                                                                                        ValidationGroup="Education" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        To
                                                                                    </label>
                                                                                </td>
                                                                                <td style="vertical-align: middle">
                                                                                    <asp:DropDownList ID="ddlToMonth2" ClientIDMode="Static" runat="server" CssClass="textbox listbox"
                                                                                        ValidationGroup="Education">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlToMonth2" ForeColor="Red" ValidationGroup="Education" />
                                                                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlToMonth2" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1"
                                                                                        ValidationGroup="Education" />
                                                                                    &nbsp;-&nbsp;
                                                                                    <asp:DropDownList ID="ddlToYear2" ClientIDMode="Static" runat="server" CssClass="textbox listbox"
                                                                                        ValidationGroup="Education">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlToYear2" ForeColor="Red" ValidationGroup="Education" />
                                                                                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlToYear2" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1"
                                                                                        ValidationGroup="Education" /><br />
                                                                                    <br />
                                                                                    <asp:CheckBox ID="cbPresent" runat="server" Text="Present" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Description
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="txtDescription" Width="237px" runat="server" TextMode="MultiLine"
                                                                                        CssClass="textarea textarea-summary" ValidationGroup="Education"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="txtDescription" ForeColor="Red" ValidationGroup="Education"></asp:RequiredFieldValidator><br />
                                                                                    <%-- <asp:RegularExpressionValidator ID="revDescriptionCol" runat="server" ControlToValidate="txtDescription"
                                                                                        ForeColor="Red" ErrorMessage="*Please enter valid text" ValidationExpression="^[a-zA-Z]$"
                                                                                        Display="Dynamic"></asp:RegularExpressionValidator><br />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="newRows">
                                                                                        <asp:Button ID="btnSave2" runat="server" Text="Save" CommandName="Save" OnClientClick="return Check2()"
                                                                                            CssClass="pButton" Font-Bold="true" Font-Size="Small" ValidationGroup="Education" />&nbsp;&nbsp;&nbsp;
                                                                                        <asp:Button ID="btnCancel2" runat="server" Text="Cancel" CommandName="Cancel" CssClass="pButton"
                                                                                            Font-Bold="true" Font-Size="Small" CausesValidation="false" />
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ButtonCellStyle">
                                                        <div class="newRowDiv">
                                                            <asp:Button ID="btnAddNewEducation" runat="server" Text="+ Add More Education" OnClick="BtnAddNewEducationClick"
                                                                CausesValidation="false" CssClass="pButton" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rpEducation" EventName="ItemCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnAddNewEducation" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="updateProgress3" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                                        <ProgressTemplate>
                                            <div  id="mydiv" style="text-align: center">
                                                <img src="images/loadingBar.gif" style="height: 50px" alt="Update in progress" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </li>
                        </ul>
                    </li>
                    <li><a href="#" class="menu-ash menu-ash-inner"><strong>School</strong><b>&nbsp;</b></a>
                        <ul class="acitem">
                            <li>
                                <div class="upload-inner">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table width="550px">
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="rpSchool" runat="server" OnItemDataBound="RpSchoolItemDataBound"
                                                            OnItemCommand="RpSchoolItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:HiddenField ID="id" runat="server" />
                                                                        <table id="tblReadonly" runat="server" width="550px">
                                                                            <tr>
                                                                                <td colspan="5" class="ButtonCellStyle">
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 20%;">
                                                                                    <label>
                                                                                        Name of the School:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCollegeDet" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    <%-- <asp:Label ID="lblCollege" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Period:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCPeriod" runat="server" CssClass="textbox"></asp:TextBox>
                                                                                    <%-- <asp:Label ID="lblDates" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Description:
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCDesc" Width="237px" runat="server" CssClass="textbox" TextMode="MultiLine"></asp:TextBox>
                                                                                    <%--<asp:Label ID="lblDescription" runat="server" Width="100%"></asp:Label>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr style="height: 15px;">
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="newRows">
                                                                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CssClass="pButton"
                                                                                            Font-Bold="true" Font-Size="Small" CausesValidation="false" />&nbsp;&nbsp;&nbsp;
                                                                                        <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CssClass="pButton"
                                                                                            Font-Bold="true" Font-Size="Small" CausesValidation="false" />
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table id="tblEditMode" runat="server" visible="false">
                                                                            <tr>
                                                                                <td colspan="5" class="ButtonCellStyle">
                                                                                    <%-- <asp:Button ID="btnSave" runat="server" Text="Save" CommandName="Save" CssClass="pButton" />
                                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="pButton"
                                                                                        CausesValidation="false" />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Name of the School
                                                                                    </label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCollege" runat="server" CssClass="textbox" ValidationGroup="School" />
                                                                                    <asp:RequiredFieldValidator ID="rfvColl" runat="server" ErrorMessage="*" ControlToValidate="txtCollege"
                                                                                        ForeColor="Red" ValidationGroup="School" /><br />
                                                                                    <%--<asp:RegularExpressionValidator ID="revSchool" runat="server" ControlToValidate="txtCollege"
                                                                                        ForeColor="Red" ErrorMessage="*Please enter valid text" ValidationExpression="^[a-zA-Z]$"
                                                                                        Display="Dynamic"></asp:RegularExpressionValidator><br />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Period
                                                                                    </label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        From
                                                                                    </label>
                                                                                </td>
                                                                                <td style="vertical-align: middle">
                                                                                    <asp:DropDownList ID="ddlFromMonth3" ClientIDMode="Static" runat="server" CssClass="textbox listbox"
                                                                                        ValidationGroup="School">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlFromMonth3" ForeColor="Red" ValidationGroup="School" />
                                                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlFromMonth3" Operator="GreaterThanEqual" Type="Integer"
                                                                                        ValueToCompare="1" ValidationGroup="School" />
                                                                                    &nbsp;-&nbsp;
                                                                                    <asp:DropDownList ID="ddlFromYear3" ClientIDMode="Static" runat="server" CssClass="textbox listbox"
                                                                                        ValidationGroup="School">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlFromYear3" ForeColor="Red" ValidationGroup="School" />
                                                                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlFromYear3" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1"
                                                                                        ValidationGroup="School" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        To
                                                                                    </label>
                                                                                </td>
                                                                                <td style="vertical-align: middle">
                                                                                    <asp:DropDownList ID="ddlToMonth3" ClientIDMode="Static" runat="server" CssClass="textbox listbox"
                                                                                        ValidationGroup="School">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlToMonth3" ForeColor="Red" ValidationGroup="School" />
                                                                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlToMonth3" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1"
                                                                                        ValidationGroup="School" />
                                                                                    &nbsp;-&nbsp;
                                                                                    <asp:DropDownList ID="ddlToYear3" ClientIDMode="Static" runat="server" CssClass="textbox listbox"
                                                                                        ValidationGroup="School">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="ddlToYear3" ForeColor="Red" ValidationGroup="School" />
                                                                                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="*" ForeColor="Red"
                                                                                        ControlToValidate="ddlToYear3" Operator="GreaterThanEqual" Type="Integer" ValueToCompare="1"
                                                                                        ValidationGroup="School" /><br />
                                                                                    <br />
                                                                                    <asp:CheckBox ID="cbPresent" runat="server" Text="Present" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <label>
                                                                                        Description
                                                                                    </label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="textarea textarea-summary"
                                                                                        ValidationGroup="School" Width="237px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*"
                                                                                        ControlToValidate="txtDescription" ForeColor="Red" ValidationGroup="School"></asp:RequiredFieldValidator><br />
                                                                                    <%-- <asp:RegularExpressionValidator ID="revDescriptionCol" runat="server" ControlToValidate="txtDescription"
                                                                                        ForeColor="Red" ErrorMessage="*Please enter valid text" ValidationExpression="^[a-zA-Z]$"
                                                                                        Display="Dynamic"></asp:RegularExpressionValidator><br />--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="newRows">
                                                                                        <asp:Button ID="btnSave4" runat="server" Text="Save" OnClientClick="return Check3()"
                                                                                            CommandName="Save" CssClass="pButton" Font-Bold="true" Font-Size="Small" ValidationGroup="School" />&nbsp;&nbsp;&nbsp;
                                                                                        <asp:Button ID="btnCancel4" runat="server" Text="Cancel" CommandName="Cancel" CssClass="pButton"
                                                                                            Font-Bold="true" Font-Size="Small" CausesValidation="false" />
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ButtonCellStyle">
                                                        <div class="newRowDiv">
                                                            <asp:Button ID="btnAddNewSchool" runat="server" Text="+ Add More School" OnClick="BtnAddNewSchoolClick"
                                                                CausesValidation="false" CssClass="pButton" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rpSchool" EventName="ItemCommand" />
                                            <asp:AsyncPostBackTrigger ControlID="btnAddNewSchool" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="updateProgress1" AssociatedUpdatePanelID="UpdatePanel3" runat="server">
                                        <ProgressTemplate>
                                            <div  id="mydiv" style="text-align: center">
                                                <img src="images/loadingBar.gif" style="height: 50px" alt="Update in progress" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </li>
                        </ul>
                    </li>
                    <li><a href="#" class="menu-ash menu-ash-inner"><strong>Contact Details</strong><b>&nbsp;</b></a>
                        <ul class="acitem">
                            <li>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="upload-inner">
                                            <label>
                                                Phone Number:</label>
                                            <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="textbox" ValidationGroup="ContactDetails" />
                                            <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhoneNumber"
                                                ForeColor="Red" Text="*" Display="Dynamic" ErrorMessage="Please enter phone number"
                                                ValidationGroup="ContactDetails"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhoneNumber"
                                                ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter valid phone number"
                                                ValidationExpression="^[0-9]{1,12}$" ValidationGroup="ContactDetails"></asp:RegularExpressionValidator>
                                            <label>
                                                Date of Birth:</label>
                                            <asp:TextBox ID="txtDOB" runat="server" CssClass="textbox" ValidationGroup="ContactDetails" />
                                            <asp:CalendarExtender runat="server" Enabled="true" TargetControlID="txtDOB" Format="dd/MM/yyyy" />
                                            <label>
                                                Alternative Email:</label>
                                            <asp:TextBox ID="txtAlternameEmail" runat="server" CssClass="textbox" ValidationGroup="ContactDetails" />
                                            <asp:RequiredFieldValidator ID="rfvAltEmail" runat="server" ControlToValidate="txtAlternameEmail"
                                                ForeColor="Red" Text="*" Display="Dynamic" ErrorMessage="Please enter email"
                                                ValidationGroup="ContactDetails"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revAltEmail" runat="server" ControlToValidate="txtAlternameEmail"
                                                ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter valid email" ValidationGroup="ContactDetails"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            <br />
                                            <label>
                                                Home Address:</label>
                                            <asp:TextBox ID="txtHomeAddress" runat="server" CssClass="textbox" ValidationGroup="ContactDetails" />
                                            <asp:RequiredFieldValidator ID="rfvHome" runat="server" ControlToValidate="txtHomeAddress"
                                                ForeColor="Red" Text="*" Display="Dynamic" ErrorMessage="Please enter home address"
                                                ValidationGroup="ContactDetails"></asp:RequiredFieldValidator>
                                            <br />
                                            <label>
                                                Web Site:</label>
                                            <asp:TextBox ID="txtWebSite" runat="server" CssClass="textbox" />
                                            <%-- <asp:RequiredFieldValidator ID="rfvWeb" runat="server" ControlToValidate="txtWebSite"
                                        ForeColor="Red" Text="*" Display="Dynamic" ErrorMessage="Please enter website"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revWeb" runat="server" ControlToValidate="txtWebSite"
                                        ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter valid website address"
                                        ValidationExpression="^([a-zA-Z0-9]+(\.[a-zA-Z0-9]+)+.*)$"></asp:RegularExpressionValidator>--%>
                                            <br />
                                            <label>
                                                Blog:</label>
                                            <asp:TextBox ID="txtBlog" runat="server" CssClass="textbox" />
                                            <%-- <asp:RequiredFieldValidator ID="rfvBlog" runat="server" ControlToValidate="txtBlog"
                                        ForeColor="Red" Text="*" Display="Dynamic" ErrorMessage="Please enter blog address"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revblog" runat="server" ControlToValidate="txtBlog"
                                        ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter valid blog address"
                                        ValidationExpression="^([a-zA-Z0-9]+(\.[a-zA-Z0-9]+)+.*)$"></asp:RegularExpressionValidator>--%>
                                            <br />
                                            <label>
                                                City:</label>
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="textbox" ValidationGroup="ContactDetails" /><br />
                                            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity"
                                                ForeColor="Red" Text="*" Display="Dynamic" ErrorMessage="Please enter city" ValidationGroup="ContactDetails"></asp:RequiredFieldValidator>
                                            <%-- <asp:RegularExpressionValidator ID="revCity" runat="server" ControlToValidate="txtCity"
                                        ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter valid city name"
                                        ValidationExpression="^[a-z,A-Z]{1,50}$"></asp:RegularExpressionValidator>--%>
                                            <br />
                                            <label>
                                                Country:</label>
                                            <%--<asp:TextBox ID="txtCountry" runat="server" />--%>
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="textbox listbox">
                                            </asp:DropDownList>
                                            <label>
                                                Martial Status:</label>
                                            <asp:RadioButtonList ID="rbMartialStatus" runat="server">
                                                <asp:ListItem Text="Married" />
                                                <asp:ListItem Text="Unmarried" Selected="True" />
                                            </asp:RadioButtonList>
                                            <label>
                                                Nationality:</label>
                                            <asp:DropDownList ID="ddlNationality" runat="server" CssClass="textbox listbox" />
                                            <br />
                                            <asp:Button ID="btnCntsSave" runat="server" Text="Save" OnClick="BtnSaveClick" CssClass="pButton"
                                                Font-Bold="true" Font-Size="Small" ValidationGroup="ContactDetails" />
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnCntsSave" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="updateProgress4" AssociatedUpdatePanelID="UpdatePanel4" runat="server">
                                    <ProgressTemplate>
                                        <div  id="mydiv" style="text-align: center">
                                            <img src="images/loadingBar.gif" style="height: 50px" alt="Update in progress" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </li>
                        </ul>
                    </li>
                    <li><a href="#" class="menu-ash menu-ash-inner"><strong>General</strong><b>&nbsp;</b></a>
                        <ul class="acitem">
                            <li>
                                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <script type="text/javascript">
                                            var prm = Sys.WebForms.PageRequestManager.getInstance();

                                            prm.add_endRequest(function () {
                                                // re-bind your jQuery events here
                                                $(document).ready(function () {

                                                    $("#dlLanguageLevel dt strong").on('click', function () {
                                                        $("#dlLanguageLevel dd ul").toggle();
                                                    });

                                                    $("#dlStrongSkillHowLong dt strong").click(function () {
                                                        $("#dlStrongSkillHowLong dd ul").toggle();
                                                    });

                                                    $("#dlExpertSkillHowLong dt strong").click(function () {
                                                        $("#dlExpertSkillHowLong dd ul").toggle();
                                                    });

                                                    $("#dlGoodSkillHowLong dt strong").click(function () {
                                                        $("#dlGoodSkillHowLong dd ul").toggle();
                                                    });

                                                    $(document).bind('click', function (e) {
                                                        var $clicked = $(e.target);
                                                        if (!$clicked.parents().hasClass("dropdown"))
                                                            $(".dropdown dd ul").hide();
                                                    });
                                                });
                                            });
                                        

                                        </script>
                                        <div class="upload-inner">
                                            <div>
                                                <table class="skills-list-table">
                                                    <tbody>
                                                        <tr>
                                                            <td width="15px" valign="top">
                                                                <label>
                                                                    Languages Known:</label>
                                                            </td>
                                                            <%--   <td width="89%">
                                                                <asp:Panel ID="pnlLanguages" runat="server">
                                                                </asp:Panel>--%>
                                                            <asp:DataList runat="server" ID="dllang">
                                                                <ItemTemplate>
                                                                    <asp:Table ID="Table1" runat="server">
                                                                        <asp:TableRow>
                                                                            <asp:TableCell Width="100px">
                                                                                <asp:Label runat="server" Width="125px" ID="lbllang" Text='<%#Eval("MasterLanguage.Description")%>'></asp:Label>
                                                                            </asp:TableCell>
                                                                            <asp:TableCell Width="100px" CssClass="del">
                                                                                <div style="margin-top: 18px;">
                                                                                    <asp:Image runat="server" ID="star" ImageUrl='<%#Stars(Eval("Level") )%>' />
                                                                                </div>
                                                                            </asp:TableCell>
                                                                            <asp:TableCell Width="100px">
                                                                                <div style="margin-top: 6px; margin-left: -7px;">
                                                                                    <asp:ImageButton ID="btndel" ImageUrl="images/cancel.jpg" OnClick="DeleteClick" CommandArgument='<%#Eval("MasterLanguageId")%>'
                                                                                        AlternateText="Delete" runat="server" Height="13px" Width="13px" />
                                                                                </div>
                                                                            </asp:TableCell></asp:TableRow>
                                                                    </asp:Table>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                </FooterTemplate>
                                                            </asp:DataList>
                                                            <table>
                                                                <tr valign="middle">
                                                                    <td>
                                                                        <input id="txtLanguageKnown" runat="server" type="text" class="textbox ui-autocomplete-input"
                                                                            value="e.g: Search..." onfocus="if (this.value ==&#39;e.g: Search...&#39;) {this.value =&#39;&#39;;}"
                                                                            onblur="if (this.value == &#39;&#39;) {this.value =&#39;e.g: Search...&#39;;}" />
                                                                        <br>
                                                                        <asp:ImageButton OnClick="AddLanguageClick" runat="server" ID="imgAddLanguage" ImageUrl="images/add-icon-withtext.png" />
                                                                    </td>
                                                                    <td>
                                                                        <dl id="dlLanguageLevel" class="dropdown dropdown1">
                                                                            <dt><strong>What is your level?</strong></dt><dd class="dropdown-inner">
                                                                                <ul style="display: none;">
                                                                                    <li>
                                                                                        <div class="field">
                                                                                            <asp:RadioButton ID="rdLanguageRatingNative" runat="server" GroupName="LanguageRating" />
                                                                                            &nbsp;Native</div>
                                                                                        <div class="ratings">
                                                                                            <img src="images/rating-star5.png" width="74" height="14" alt="ratings"></div>
                                                                                    </li>
                                                                                    <li>
                                                                                        <div class="field">
                                                                                            <asp:RadioButton ID="rdLanguageRatingExcellent" runat="server" GroupName="LanguageRating" />
                                                                                            &nbsp;Excellent</div>
                                                                                        <div class="ratings">
                                                                                            <img src="images/rating-star4.png" width="74" height="14" alt="ratings"></div>
                                                                                    </li>
                                                                                    <li>
                                                                                        <div class="field">
                                                                                            <asp:RadioButton ID="rdLanguageRatingFluent" runat="server" GroupName="LanguageRating" />
                                                                                            &nbsp;Fluent</div>
                                                                                        <div class="ratings">
                                                                                            <img src="images/rating-star3.png" width="74" height="14" alt="ratings"></div>
                                                                                    </li>
                                                                                    <li>
                                                                                        <div class="field">
                                                                                            <asp:RadioButton ID="rdLanguageRatingIntermediate" runat="server" GroupName="LanguageRating" />
                                                                                            &nbsp;Intermediate</div>
                                                                                        <div class="ratings">
                                                                                            <img src="images/rating-star2.png" width="74" height="14" alt="ratings"></div>
                                                                                    </li>
                                                                                    <li style="background: none;">
                                                                                        <div class="field">
                                                                                            <asp:RadioButton ID="rdLanguageRatingBasic" runat="server" GroupName="LanguageRating" />
                                                                                            &nbsp;Basic</div>
                                                                                        <div class="ratings">
                                                                                            <img src="images/rating-star1.png" width="74" height="14" alt="ratings"></div>
                                                                                    </li>
                                                                                </ul>
                                                                            </dd>
                                                                        </dl>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSaveLanguage" OnClick="AddLanguageClick" runat="server" Text="Save"
                                                                            CssClass="button-green button-green-save" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </td>
                                                        </tr>
                                                        <tr class="dotted">
                                                            <td colspan="3">
                                                                <p class="dotted">
                                                                    &nbsp;</p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <h3 style="margin-left: 48px; font-size: 12px;">
                                                                    Interest</h3>
                                                            </td>
                                                            <td valign="top">
                                                                <div style="margin-left: 121px; margin-top: -10px;">
                                                                    <%-- <asp:Panel ID="pnlInterests" runat="server">
                                                                    </asp:Panel>--%>
                                                                    <asp:DataList runat="server" ID="dlintrest" RepeatDirection="Horizontal" RepeatColumns="4">
                                                                        <ItemTemplate>
                                                                            <ul style="width: 81px;">
                                                                                <li>
                                                                                    <div style="text-align: center; width: 57px;">
                                                                                        <asp:ImageButton ID="btninterest" ImageUrl="images/cancel.jpg" OnClick="InterestClick"
                                                                                            CommandArgument='<%#Eval("MasterInterestId")%>' AlternateText="Delete" runat="server"
                                                                                            Height="13px" Width="13px" /></div>
                                                                                </li>
                                                                                <li>
                                                                                    <asp:Image runat="server" CssClass="textbox" ID="Img_intrest" Height="48px" Width="50px"
                                                                                        ImageUrl='<%#Interest(Eval("MasterInterest.Description")) %>' />
                                                                                </li>
                                                                                <li>
                                                                                    <asp:Label runat="server" ID="Description" Text='<%#Eval("MasterInterest.Description") %>'
                                                                                        Height="27px" Width="77px"></asp:Label>
                                                                                </li>
                                                                            </ul>
                                                                        </ItemTemplate>
                                                                    </asp:DataList>
                                                                    <%--<input runat="server" id="txtInterest" type="text" class="textbox ui-autocomplete-input"
                                                                        value="e.g: Search..." onfocus="if (this.value ==&#39;e.g: Search...&#39;) {this.value =&#39;&#39;;}"
                                                                        onblur="if (this.value == &#39;&#39;) {this.value =&#39;e.g: Search...&#39;;}" />--%>
                                                                    <br />
                                                                    <br />
                                                                    <asp:DropDownList ID="ddlInterest" ClientIDMode="Static" runat="server" CssClass="textbox listbox">
                                                                        <asp:ListItem Text="--Please Select--" Value="-1" Selected="True" Enabled="true"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <br>
                                                                    <asp:ImageButton runat="server" OnClick="AddInterestClick" ID="imgAddInterest" ImageUrl="images/add-icon-withtext.png" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr class="dotted">
                                                            <td colspan="3">
                                                                <p class="dotted">
                                                                    &nbsp;</p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                <label>
                                                                    Skill</label>
                                                            </td>
                                                            <td valign="top">
                                                               <%-- <asp:Panel ID="pnlExpertSkill" runat="server" style="margin-left: 203px; width: 503px;" Visible="False">
                                                                </asp:Panel>--%>
                                                                <table>
                                                                    <tr>
                                                                     
                                                                <asp:DataList runat="server" ID="dlExpertSkillDisplay" RepeatColumns="1" RepeatDirection="Vertical" Width="503px">
                                                                    <ItemTemplate>
                                                                        <td style="float: left; margin-left: 174px;">
                                                                        <asp:Label runat="server" Text='<%#Eval("Description") %>'  style="width: auto; height: auto;"></asp:Label>
                                                                        </td><td>
                                                                        <asp:Label runat="server" style="width: 60px"><%#Eval("HowLong") %></asp:Label>
                                                                       </td>
                                                                        <td>
                                                                            <asp:ImageButton ImageUrl="images/cancel.jpg"  runat="server" ID="btnDeleteExpert" OnClick="BtnDeleteExpertClick" CommandArgument='<%#Eval("Id") %>' style="height:13px;width:13px; margin-top: 10px"/>
                                                                        </td>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:DataList>
                                                                   
                                                                    </tr>
                                                                </table>
                                                                <br/>
                                                                <div style="float:left;">
                                                                    <strong>Expert:</strong>
                                                                    <input id="txtExpertSkill" runat="server" type="text" class="textbox ui-autocomplete-input"
                                                                        value="e.g: Search..." onfocus="if (this.value ==&#39;e.g: Search...&#39;) {this.value =&#39;&#39;;}"
                                                                        onblur="if (this.value == &#39;&#39;) {this.value =&#39;e.g: Search...&#39;;}" /><br>
                                                                   <%-- <asp:ImageButton runat="server" ValidationGroup="Expert" OnClick="AddExpertSkillClick" ID="imgAddExpertSkill"
                                                                        ImageUrl="images/add-icon-withtext.png" />--%>
                                                                        </div>
                                                            </td>
                                                            
                                                            <td valign="top">
                                                                <asp:DropDownList runat="server" ID="ddlExpertSkillHowLong" Style="margin-left: 441px; margin-top:-19px; width: 134px" ></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Expert" ControlToValidate="ddlExpertSkillHowLong"
                                                                    InitialValue="-1" Text="*" EnableClientScript="True" ErrorMessage="Please Select" ForeColor="red"></asp:RequiredFieldValidator>
                                                                <%--<dl id="dlExpertSkillHowLong" class="dropdown1 dropdown" style="margin-left: 441px; margin-top: -53px;">
                                                                    <dt><strong>How Long ?</strong></dt><dd class="dropdown-inner">
                                                                        <ul style="display: none;">
                                                                            <li><a href="javascript:expertSkilHowLongClick('1');">< 1 Year</a></li><li><a href="javascript:expertSkilHowLongClick('2');">< 2 Years</a></li><li><a href="javascript:expertSkilHowLongClick('3');">< 3 Years</a></li><li><a href="javascript:expertSkilHowLongClick('4');">< 4 Years</a></li><li><a href="javascript:expertSkilHowLongClick('5');">< 5 Years</a></li><li><a href="javascript:expertSkilHowLongClick('6');">< 6 Years</a></li><li><a href="javascript:expertSkilHowLongClick('7');">< 7 Years</a></li><li><a href="javascript:expertSkilHowLongClick('8');">< 8 Years</a></li><li><a href="javascript:expertSkilHowLongClick('9');">< 9 Years</a></li><li><a href="javascript:expertSkilHowLongClick('10');">> 10 Years</a></li></ul></dd></dl>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        <td>
                                                             <asp:ImageButton runat="server" ValidationGroup="Expert" OnClick="AddExpertSkillClick" ID="imgAddExpertSkill"
                                                                        ImageUrl="images/add-icon-withtext.png" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                &nbsp;
                                                            </td>
                                                            <td valign="top">
                                                               <%-- <asp:Panel ID="pnlStrongSkill" runat="server" style="margin-left: 203px; width: 503px;" Visible="False">
                                                                </asp:Panel>--%>
                                                                 <table>
                                                                    <tr>
                                                                <asp:DataList runat="server" ID="dlStrongSkill" RepeatColumns="1" RepeatDirection="Vertical" Width="503px">
                                                                    <ItemTemplate>
                                                                        <td style="float: left; margin-left: 174px">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Description") %>' style="width: auto; height: auto;"></asp:Label>
                                                                        </td><td>
                                                                        <asp:Label ID="Label3" runat="server" style="width: 60px"><%#Eval("HowLong") %> </asp:Label>
                                                                       </td>
                                                                        <td>
                                                                            <asp:ImageButton ImageUrl="images/cancel.jpg"  runat="server" ID="btnDeleteExpert" OnClick="BtnDeleteExpertClick" CommandArgument='<%#Eval("Id") %>' style="height:13px;width:13px; margin-top: 10px"/>
                                                                        </td>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:DataList>
                                                                   
                                                                    </tr>
                                                                </table>
                                                                <div>
                                                                    <strong>Strong:</strong>
                                                                    <input id="txtStrongSkill" runat="server" type="text" class="textbox ui-autocomplete-input"
                                                                        value="e.g: Search..." onfocus="if (this.value ==&#39;e.g: Search...&#39;) {this.value =&#39;&#39;;}"
                                                                        onblur="if (this.value == &#39;&#39;) {this.value =&#39;e.g: Search...&#39;;}" /><br>
                                                                    <%--<asp:ImageButton runat="server" ValidationGroup="Strong" OnClick="AddStrongSkillClick" ID="imgAddStrongSkill"
                                                                        ImageUrl="images/add-icon-withtext.png" />--%>
                                                                        </div>
                                                            </td>
                                                            <td valign="top">
                                                                <asp:DropDownList runat="server" ID="ddlSkillHowLongYears" Style="margin-left: 441px;margin-top: -19px; width: 134px" ></asp:DropDownList>
                                                                <asp:RequiredFieldValidator runat="server" ValidationGroup="Strong" ControlToValidate="ddlSkillHowLongYears"
                                                                    InitialValue="-1" ForeColor="red"  EnableClientScript="True" Text="*" ErrorMessage="Please Select" ></asp:RequiredFieldValidator>
                                                                <%--                                                          <dl id="dlStrongSkillHowLong" class="dropdown1 dropdown" style="margin-left: 441px; margin-top: -54px;">
                                                                    <dt><strong>How Long ?</strong></dt><dd class="dropdown-inner">
                                                                        <ul style="display: none;">
                                                                            <li><a href="javascript:strongSkilHowLongClick('1');">< 1 Year</a></li><li><a href="javascript:strongSkilHowLongClick('2');">< 2 Years</a></li><li><a href="javascript:strongSkilHowLongClick('3');">< 3 Years</a></li><li><a href="javascript:strongSkilHowLongClick('4');">< 4 Years</a></li><li><a href="javascript:strongSkilHowLongClick('5');">< 5 Years</a></li><li><a href="javascript:strongSkilHowLongClick('6');">< 6 Years</a></li><li><a href="javascript:strongSkilHowLongClick('7');">< 7 Years</a></li><li><a href="javascript:strongSkilHowLongClick('8');">< 8 Years</a></li><li><a href="javascript:strongSkilHowLongClick('9');">< 9 Years</a></li><li><a href="javascript:strongSkilHowLongClick('10');">> 10 Years</a></li></ul></dd></dl>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        <td>
                                                        <asp:ImageButton runat="server" ValidationGroup="Strong" OnClick="AddStrongSkillClick" ID="imgAddStrongSkill"
                                                                        ImageUrl="images/add-icon-withtext.png" />
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top">
                                                                &nbsp;

                                                            </td>
                                                            <td valign="top">
                                                               <%-- <asp:Panel ID="pnlGoodSkill" runat="server" style="margin-left: 203px; width: 503px;" Visible="False">
                                                                </asp:Panel>--%>
                                                                <table>
                                                                    <tr>
                                                                <asp:DataList runat="server" ID="dlGoodSkills" RepeatColumns="1" RepeatDirection="Vertical" Width="503px">
                                                                    <ItemTemplate>
                                                                        <td style="float: left; margin-left: 174px">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Description") %>' style="width: auto; height: auto;"></asp:Label>
                                                                        </td><td>
                                                                        <asp:Label ID="Label3" runat="server" style="width: 60px"><%#Eval("HowLong") %></asp:Label>
                                                                       </td>
                                                                        <td>
                                                                            <asp:ImageButton ImageUrl="images/cancel.jpg"  runat="server" ID="btnDeleteExpert" OnClick="BtnDeleteExpertClick" CommandArgument='<%#Eval("Id") %>' style="height:13px;width:13px; margin-top: 10px"/>
                                                                        </td>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:DataList>
                                                                   
                                                                    </tr>
                                                                </table>
                                                                <div>
                                                                    <strong>Good:</strong>
                                                                    <input runat="server" id="txtGoodSkill" type="text" class="textbox ui-autocomplete-input"
                                                                        value="e.g: Search..." onfocus="if (this.value ==&#39;e.g: Search...&#39;) {this.value =&#39;&#39;;}"
                                                                        onblur="if (this.value == &#39;&#39;) {this.value =&#39;e.g: Search...&#39;;}" /><br>
                                                                   <%-- <asp:ImageButton runat="server" ValidationGroup="Good" OnClick="AddGoodSkillClick" ID="imgAddGoodSkill"
                                                                        ImageUrl="images/add-icon-withtext.png" />--%></div>
                                                            </td>
                                                           
                                                            <td valign="top">
                                                                <asp:DropDownList runat="server" ID="ddlSkillGoodHowLong" Style="margin-left: 441px; margin-top: -19px; width: 134px" ></asp:DropDownList>
                                                                <asp:RequiredFieldValidator EnableClientScript="True" ID="RequiredFieldValidator2" ValidationGroup="Good" runat="server" ControlToValidate="ddlSkillGoodHowLong"
                                                                    InitialValue="-1" Text="*" ErrorMessage="Please select" ForeColor="red"></asp:RequiredFieldValidator>
                                                                   </td>
                                                                   </tr>
                                                                   <tr>
                                                                   <td>
                                                                    <asp:ImageButton runat="server" ValidationGroup="Good" OnClick="AddGoodSkillClick" ID="imgAddGoodSkill"
                                                                        ImageUrl="images/add-icon-withtext.png" />
                                                                   </td>
                                                                   </tr>
                                                                <%--                                             <dl id="dlGoodSkillHowLong" class="dropdown1 dropdown" style="margin-left: 441px; margin-top: -57px;">
                                                                    <dt><strong>How Long ?</strong></dt><dd class="dropdown-inner">
                                                                        <ul style="display: none;">
                                                                            <li><a href="javascript:goodSkilHowLongClick('1');">< 1 Year</a></li><li><a href="javascript:goodSkilHowLongClick('2');">< 2 Years</a></li><li><a href="javascript:goodSkilHowLongClick('3');">< 3 Years</a></li><li><a href="javascript:goodSkilHowLongClick('4');">< 4 Years</a></li><li><a href="javascript:goodSkilHowLongClick('5');">< 5 Years</a></li><li><a href="javascript:goodSkilHowLongClick('6');">< 6 Years</a></li><li><a href="javascript:goodSkilHowLongClick('7');">< 7 Years</a></li><li><a href="javascript:goodSkilHowLongClick('8');">< 8 Years</a></li><li><a href="javascript:goodSkilHowLongClick('9');">< 9 Years</a></li><li><a href="javascript:goodSkilHowLongClick('10');">> 10 Years</a></li></ul></dd></dl></td></tr></tbody></table></div><br /><br />--%>
                                                               <br/>
                                                                <label>
                                                                    Expected Salary</label>
                                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="textbox" Visible="false"></asp:TextBox><br />
                                                                <br />
                                                                <label style="margin-left: -1px">
                                                                    Min:</label><br />
                                                                <div style="margin-top: -11px;">
                                                                    <asp:DropDownList ID="ddlMin" runat="server">
                                                                        <asp:ListItem Selected="True" Text="-Please Selct-" Value="0">
                                                                        </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <br />
                                                                <label>
                                                                    Max:</label><br />
                                                                <div style="margin-top: -11px;">
                                                                    <asp:DropDownList ID="ddlMax" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <br />
                                                                <label>
                                                                    Currency:</label><br />
                                                                <div style="margin-top: -11px;">
                                                                    <asp:DropDownList ID="ddlCurr" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <br />
                                                                <label>
                                                                    Industry:</label><br />
                                                                <div style="margin-top: -16px;">
                                                                    <asp:DropDownList ID="ddlExpIndustry" runat="server" CssClass="textbox listbox" AppendDataBoundItems="true">
                                                                        <asp:ListItem Text="--Please Select--" Value="0" Selected="True" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlExpIndustry"
                                                                    ValidationGroup="GeneralDetails" ForeColor="Red" Text="*" Display="Dynamic" ErrorMessage="Please select industry"
                                                                    InitialValue="0" />
                                                                <br />
                                                                <%--<label>
                                                Company Logo:</label> <div style="margin-top:5px">
                                                &nbsp; <asp:FileUpload ID="uploadPhoto" runat="server" />--%>
                                                                <%--<asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="absmiddle" alt="" src="images/uploading.gif"/></asp:Label>
                                        <asp:AjaxFileUpload ID="AjaxFileUpload1" runat="server" OnUploadComplete="AjaxFileUpload1UploadComplete"
                                            ThrobberID="myThrobber" MaximumNumberOfFiles="10" AllowedFileTypes="jpg,jpeg" />--%>
                                            </div>
                                            <br />
                                            <br />
                                            <div style="float: right; width: 100%; height: 55px; width: 160px; display: inline;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            margin-right: 0px; text-align: right;">
                                                <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="BtnSaveClick"
                                                    CssClass="button-green button-green-upload" ValidationGroup="GeneralDetails" />
                                                <asp:Label runat="server" Text="Your Data Saved Successfully" ID="lbltext" Visible="false"
                                                    Width="221px"></asp:Label><%--<asp:Button ID="btnChange" runat="server" Text="Change" CssClass="button-green" />--%></div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="updateProgress5" AssociatedUpdatePanelID="UpdatePanel5" runat="server">
                                    <ProgressTemplate>
                                        <div  id="mydiv" style="text-align: center">
                                            <img src="images/loadingBar.gif" style="height: 50px" alt="Update in progress" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </li>
                        </ul>
                    </li>
                </ul>
                 </div>
                <div class="content-inner-right">
                    <uc1:ProfileCompletionSteps ID="ProfileCompletion" runat="server" />
                    <p class="margin-top-visible">
                        &nbsp;</p>
                    <div class="box-right">
                        <div class="head-ash">
                            <h3>
                                Import Your Profile
                                <p class="import-contact-desc">
                                    Instantly update your profile by<br />
                                    Importing from Linkedin or Facebook
                                </p>
                                <asp:ImageButton ID="imgBtnFB" runat="server" ImageUrl="images/import-facebook.png"
                                    Width="133" Height="41" AlternateText="Facebook" PostBackUrl="https://www.facebook.com/login.php"
                                    CausesValidation="false" />&nbsp;&nbsp;
                                <asp:ImageButton ID="imgBtnLIn" runat="server" ImageUrl="images/import-linkedin.png" OnClick="BtnImportFromLinkedinClick"
                                    Width="133" Height="41" AlternateText="Linked In" CausesValidation="false" />
                                <%-- <a href="#" title="Facebook" style="margin-left: 10px;">
                        <img src="images/import-facebook.png" width="133" height="41" alt="Facebook" /></a>--%>
                                <%-- <img src="images/import-linkedin.png" width="133" height="41" alt="Linked in" />--%>
                                <br />
                                <br />
                        </div>
                        <p class="margin-top-visible">
                            &nbsp;</p>
                        <div class="box-right">
                            <div class="privacy">
                                <img src="images/icon-privacy1.png" width="48" height="48" alt="Privacy" />
                                <strong>Your privacy is important to us. Your contact details is never shown to others</strong>
                            </div>
                            <%-- 
                    <asp:TextBox runat ="server" ID = "TextBox1" class ="tb"></asp:TextBox>--%><%--  <input type="text" id="txtSearch" class="autosuggest"/>--%></div>
                    <div>
                   <asp:FileUpload ID="uploadResume" runat="server" /></div><br /><br />
                        <div style="margin-left: -219px;"><asp:Button  ID="btnUploadProfile" runat="server" Text="Upload Your Profile" BorderStyle="None"
                            CssClass="button-green  poplight   greenup" OnClientClick="return validate();" OnClick="BtnprofileClick"  /></div>
                    </div>
                </div>
           
            <!-- content inner ends -->
        </div>
        <!-- content section ends -->
        <!-- Range Slider Script Begins -->
        <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
        <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
            type="text/javascript">  </script>
        <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
        <!-- Range Slider Script Ends -->
        <%-- <script type="text/javascript">
        $(function () {
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "EditProfilePage.aspx/GetAutoCompleteData",
                        data: "{ 'username': '" + request.term + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
//                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
//                minLength: 2
            });
        });
</script>--%><script type="text/javascript">
                 $(document).ready(function () {

                 });
                 function toggleAttachments() {
                     $("#divAttachments").slideToggle();
                 }
                 function strongSkilHowLongClick(howlong) {
                     $('#MainContent_hdnHowLongStrongSkill').val(howlong);
                 }
                 function goodSkilHowLongClick(howlong) {
                     $('#MainContent_hdnHowLongGoodSkill').val(howlong);
                 }
                 function expertSkilHowLongClick(howlong) {
                     $('#MainContent_hdnHowLongExpertSkill').val(howlong);
                 }
                 function pageLoad(sender, args) {

                     $(function () {
                         $(".skilltextbox").autocomplete({
                             source: function (request, response) {
                                 $.ajax({
                                     url: "HuntableWebService.asmx/Searchskillsexp",
                                     data: "{ 'word': '" + request.term + "' }",
                                     dataType: "json",
                                     type: "POST",
                                     contentType: "application/json; charset=utf-8",
                                     dataFilter: function (data) { return data; },
                                     success: function (data) {
                                         response($.map(data.d, function (item) {
                                             return {
                                                 value: item.desc,
                                                 label: item.desc,
                                                 id: item.id
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

                     $(function () {
                         $("#MainContent_txtLanguageKnown").autocomplete({
                             source: function (request, response) {
                                 $.ajax({
                                     url: "HuntableWebService.asmx/SearchLanguages",
                                     data: "{ 'word': '" + request.term + "' }",
                                     dataType: "json",
                                     type: "POST",
                                     contentType: "application/json; charset=utf-8",
                                     dataFilter: function (data) { return data; },
                                     success: function (data) {
                                         response($.map(data.d, function (item) {
                                             return {
                                                 value: item.desc,
                                                 label: item.desc,
                                                 id: item.id
                                             }
                                         }))
                                     },
                                     error: function (XMLHttpRequest, textStatus, errorThrown) {
                                         //alert(textStatus);
                                     }
                                 });
                             },
                             minLength: 1,
                             change: function (event, ui) {
                                 $('#MainContent_hdnLanguageSelected').val(ui.item.id);
                             }
                         });
                     });


                     $(function () {
                         $("#MainContent_txtInterest").autocomplete({
                             source: function (request, response) {
                                 $.ajax({
                                     url: "HuntableWebService.asmx/SearchInterests",
                                     data: "{ 'word': '" + request.term + "' }",
                                     dataType: "json",
                                     type: "POST",
                                     contentType: "application/json; charset=utf-8",
                                     dataFilter: function (data) { return data; },
                                     success: function (data) {
                                         response($.map(data.d, function (item) {
                                             return {
                                                 value: item.desc,
                                                 label: item.desc,
                                                 id: item.id
                                             }
                                         }))
                                     },
                                     error: function (XMLHttpRequest, textStatus, errorThrown) {
                                         //alert(textStatus);
                                     }
                                 });
                             },
                             minLength: 1,
                             change: function (event, ui) {
                                 $('#MainContent_hdnInterestSelected').val(ui.item.id);
                             }
                         });
                     });

                     $(function () {
                         $("#MainContent_txtExpertSkill").autocomplete({
                             source: function (request, response) {
                                 $.ajax({
                                     url: "HuntableWebService.asmx/Searchskillsexp",
                                     data: "{ 'word': '" + request.term + "' }",
                                     dataType: "json",
                                     type: "POST",
                                     contentType: "application/json; charset=utf-8",
                                     dataFilter: function (data) { return data; },
                                     success: function (data) {
                                         response($.map(data.d, function (item) {
                                             return {
                                                 value: item.desc,
                                                 label: item.desc,
                                                 id: item.id
                                             }
                                         }))
                                     },
                                     error: function (XMLHttpRequest, textStatus, errorThrown) {
                                         //alert(textStatus);
                                     }
                                 });
                             },
                             minLength: 1,
                             change: function (event, ui) {
                                 $('#MainContent_hdnExpertSkillSelected').val(ui.item.id);
                             }
                         });
                     });

                     $(function () {
                         $("#MainContent_txtStrongSkill").autocomplete({
                             source: function (request, response) {
                                 $.ajax({
                                     url: "HuntableWebService.asmx/Searchskillsexp",
                                     data: "{ 'word': '" + request.term + "' }",
                                     dataType: "json",
                                     type: "POST",
                                     contentType: "application/json; charset=utf-8",
                                     dataFilter: function (data) { return data; },
                                     success: function (data) {
                                         response($.map(data.d, function (item) {
                                             return {
                                                 value: item.desc,
                                                 label: item.desc,
                                                 id: item.id
                                             }
                                         }))
                                     },
                                     error: function (XMLHttpRequest, textStatus, errorThrown) {
                                         //alert(textStatus);
                                     }
                                 });
                             },
                             minLength: 1,
                             change: function (event, ui) {
                                 $('#MainContent_hdnStrongSkillSelected').val(ui.item.id);
                             }
                         });
                     });

                     $(function () {
                         $("#MainContent_txtGoodSkill").autocomplete({
                             source: function (request, response) {
                                 $.ajax({
                                     url: "HuntableWebService.asmx/Searchskillsexp",
                                     data: "{ 'word': '" + request.term + "' }",
                                     dataType: "json",
                                     type: "POST",
                                     contentType: "application/json; charset=utf-8",
                                     dataFilter: function (data) { return data; },
                                     success: function (data) {
                                         response($.map(data.d, function (item) {
                                             return {
                                                 value: item.desc,
                                                 label: item.desc,
                                                 id: item.id
                                             }
                                         }))
                                     },
                                     error: function (XMLHttpRequest, textStatus, errorThrown) {
                                         //alert(textStatus);
                                     }
                                 });
                             },
                             minLength: 1,
                             change: function (event, ui) {
                                 $('#MainContent_hdnGoodSkillSelected').val(ui.item.id);
                             }
                         });
                     });
                 }

                 $(document).ready(function () {
                     $('div.test').click(function () {
                         $('ul.list').slideToggle('medium');
                     });
                 });
                 //$("#MainContent_rpCurrentExperience_ifPortfoliosAchievementsVideos_0").css({ height: iframe.$("body").outerHeight() });
        
</script><!-- Footer section ends --><!-- Help Tab Script Begins --><script src="js/menu.js"
    type="text/javascript"></script><!-- Help Tab Script Ends --><input type="hidden"
        id="hdnLanguageSelected" runat="server" /><input type="hidden" id="hdnInterestSelected"
            runat="server" /><input type="hidden" id="hdnExpertSkillSelected" runat="server" /><input
                type="hidden" id="hdnGoodSkillSelected" runat="server" /><input type="hidden" id="hdnStrongSkillSelected"
                    runat="server" /><input type="hidden" id="hdnHowLongExpertSkill" runat="server" /><input
                        type="hidden" id="hdnHowLongGoodSkill" runat="server" /><input type="hidden" id="hdnHowLongStrongSkill"
                            runat="server" />
                            

<div id="hideshow" style="visibility: hidden;">
	<div id="fade_in"></div>
    <div id="showdiv">
	<div class="popup_blocks" style="margin-top:131px">
		<div class="popups">
			<a href="javascript:hideDiv()"><img src="images/close_pop.png" class="cntrl" title="Close"></a>
            <p>
			Dear User,<br />
<br />
Although we take every measure to import all the details from your profile, there could be few details missing.
<br />
We recommend, that you revise your profile once again to make sure all the details are right.<br /><br />

<span>Note: You can add pictures, Achievements, &amp; Videos to each experience, when you click - EDIT under experience.</span>
</p>


		</div>
	</div>
    </div>
</div>

<!--END POPUP-->





<script language="javascript" type="text/javascript">
    function hideDiv() {
        if (document.getElementById) { // DOM3 = IE5, NS6 
            document.getElementById('hideshow').style.visibility = 'hidden';
        }
        else {
            if (document.layers) { // Netscape 4 
                document.hideshow.visibility = 'hidden';
            }
            else { // IE 4 
                document.all.hideshow.style.visibility = 'hidden';
            }
        }
    }

    function showDiv() {
        if (document.getElementById) { // DOM3 = IE5, NS6 
            document.getElementById('hideshow').style.visibility = 'visible';
        }
        else {
            if (document.layers) { // Netscape 4 
                document.hideshow.visibility = 'visible';
            }
            else { // IE 4 
                document.all.hideshow.style.visibility = 'visible';
            }
        }
    } 
</script>  </div>

</asp:Content>
