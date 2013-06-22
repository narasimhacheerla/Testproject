<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AdminExportData.aspx.cs" Inherits="Huntable.UI.AdminExportData" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javaScript" type="text/javaScript">
        function FromToDateValidation() {
            var fromDate = document.getElementById("MainContent__fromDateText").value;
            var toDate = document.getElementById("MainContent__toDateText").value;
            if (fromDate == '' && toDate == '') {
                alert('Please enter from and to dates');
                document.getElementById("MainContent__fromDateText").focus();
                return false;
            }
            else if (fromDate == '' && toDate != '') {
                alert('Please enter from date');
                document.getElementById("MainContent__fromDateText").focus();
                return false;
            }
            else if (fromDate != '' && toDate == '') {
                alert('Please enter to date');
                document.getElementById("MainContent__toDateText").focus();
                return false;
            }
            else if (fromDate != '' && toDate != '') {
                if (fromDate > toDate) {
                    alert('To date must be greater than from date!');
                    document.getElementById("MainContent__toDateText").focus();
                    return false;
                }
            }
        }
    </script>
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div id="st_horizontal" class="st_horizontal">
                    <b>
                        <asp:Label ID="_fromDateLabel" runat="server" Text="From Date:" CssClass="labelDisplay"></asp:Label></b>
                    <br />
                    <br />
                    <asp:TextBox ID="_fromDateText" runat="server"></asp:TextBox>
                    <asp:ImageButton runat="Server" ID="_fromDateImage" ImageUrl="~/images/calendar.png"
                        AlternateText="Click to show calendar" />
                    <asp:CalendarExtender ID="_calendarExtenderFromDate" runat="server" TargetControlID="_fromDateText"
                        PopupButtonID="_fromDateImage" Format="dd/MMM/yyyy" />
                </div>
                 <p class="margin-top-visible">
                    &nbsp;</p>
                <div id="Div1" class="st_horizontal">
                    <b>
                        <asp:Label ID="_toDateLabel" runat="server" Text="To Date:" CssClass="labelDisplay"></asp:Label></b>
                    <br />
                    <br />
                    <asp:TextBox ID="_toDateText" runat="server"></asp:TextBox>
                    <asp:ImageButton runat="Server" ID="_toDateImage" ImageUrl="~/images/calendar.png"
                        AlternateText="Click to show calendar" />
                    <asp:CalendarExtender ID="_calendarExtenderToDate" runat="server" TargetControlID="_toDateText"
                        PopupButtonID="_toDateImage" Format="dd/MMM/yyyy" />
                        <br /><br />
                    <asp:Button ID="btnExportUsers" runat="server" Text="ExportUsers" OnClick="BtnExportUsersClick" CssClass="button-green button-green-join" />
                    <asp:Button ID="btnExportJobs" runat="server" Text="ExportJobs" OnClick="BtnExportJobsClick" CssClass="button-green button-green-join" />
                </div>
            </div>
        </div>
    </div>
     <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
