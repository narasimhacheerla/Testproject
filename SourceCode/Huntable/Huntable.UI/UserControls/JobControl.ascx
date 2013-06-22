<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobControl.ascx.cs"
    Inherits="Huntable.UI.UserControls.JobControl" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>  
      <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
<div class="notification-left">
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
             minLength: 2
         });
     });
	</script>
    <b class="job-search-heading">Job Search</b>
    <asp:TextBox runat="server" ID="txtjobTitle" onblur="if (this.value == '') {this.value ='Job Title';}"
        Style="margin-top: 0px;" onfocus="if (this.value =='Job Title') {this.value ='';}"
        Text="Job Title" class="textbox textbox-inner"></asp:TextBox>
    <asp:TextBox runat="server" ID="txtKeywords" Text="Keywords" class="textbox textbox-inner"
        onblur="if (this.value == '') {this.value ='Keywords';}" onfocus="if (this.value =='Keywords') {this.value ='';}"></asp:TextBox>
    <asp:DropDownList runat="server" ID="ddlCountry" class="textbox selectbox-inner"
        AppendDataBoundItems="True">
        <asp:ListItem Value="0">Country</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="txtLocation" class="textbox textbox-inner" runat="server" onblur="if (this.value == '') {this.value ='Location';}"
        onfocus="if (this.value =='Location') {this.value ='';}" Text="Location"></asp:TextBox>
    <asp:DropDownList runat="server" ID="ddlSalary" AppendDataBoundItems="true" class="textbox selectbox-inner">
        <asp:ListItem Value="0">Salary</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList runat="server" ID="ddlExperience" class="textbox selectbox-inner">
        <asp:ListItem Value="0">Experience</asp:ListItem>
        <asp:ListItem Value="1">10</asp:ListItem>
        <asp:ListItem Value="2">20</asp:ListItem>
        <asp:ListItem Value="3">30</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="txtCompany" onblur="if (this.value == '') {this.value ='Company';}"
        onfocus="if (this.value =='Company') {this.value ='';}" Text="Company" class="textbox textbox-inner"
        runat="server"></asp:TextBox>
    <asp:DropDownList runat="server" class="textbox selectbox-inner" ID="ddlIndustry"
        AppendDataBoundItems="True">
        <asp:ListItem Value="0">Industry</asp:ListItem>
    </asp:DropDownList>
   <%-- <asp:DropDownList class="textbox selectbox-inner" runat="server" ID="ddlSkill" AppendDataBoundItems="True">
        <asp:ListItem Value="0">Skill</asp:ListItem>
    </asp:DropDownList>--%>
      <asp:TextBox ID="txtSkill" onblur="if (this.value == '') {this.value ='Skill';}"
        onfocus="if (this.value =='Skill') {this.value ='';}" Text="Skill" class="textbox textbox-inner"
        runat="server"></asp:TextBox>
    <asp:DropDownList class="textbox selectbox-inner" runat="server" ID="ddlJobType"
        AutoPostBack="True" OnSelectedIndexChanged="DdlJobTypeSelectedIndexChanged" AppendDataBoundItems="True">
        <asp:ListItem Value="0">Job Type</asp:ListItem>
    </asp:DropDownList>
    <asp:Button runat="server" ID="btnSearch" Text="Search" class="button-green button-green-jobpost"
        OnClick="BtnSearchClick" />
</div>
