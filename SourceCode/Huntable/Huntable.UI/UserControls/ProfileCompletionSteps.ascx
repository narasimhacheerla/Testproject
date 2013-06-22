<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileCompletionSteps.ascx.cs"
    Inherits="Huntable.UI.UserControls.ProfileCompletionSteps" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />

    <div class="box-right">
        <div class="head-ash">
            <h3 class="completion-heading">
                Profile Completion tips <a href="EditProfilePage.aspx">(Why do this?)</a></h3>
        </div>
        <ul class="profile-complete-list">
            <li runat="server" id="liProfile"><a runat="server" href="~/PictureUpload.aspx">Add
                a picture (5%)</a></li>
            <li runat="server" id="liSummary"><a runat="server" href="~/EditProfilePage.aspx">Add
                Your Summary (20%)</a></li>
            <li runat="server" id="liCurrentEmp"><a runat="server" href="~/EditProfilePage.aspx#Experience - Current">
                Add Your current Position (5%)</a></li>
            <li runat="server" id="liEmp"><a runat="server" href="~/EditProfilePage.aspx">Add Your
                Experience (10%)</a></li>
            <li runat="server" id="liPastExp"><a runat="server" href="~/EditProfilePage.aspx">Add
                Your Past Experience (20%)</a></li>
            <li runat="server" id="liEduHistroy"><a runat="server" href="~/EditProfilePage.aspx">
                Add Your Education (20%)</a></li>
            <li runat="server" id="liSchool"><a runat="server" href="~/EditProfilePage.aspx">Add
                Your School (5%)</a></li>
            <li runat="server" id="liIntrests"><a runat="server" href="~/EditProfilePage.aspx">Add
                Your Interest, etc... (10%)</a></li>
        </ul>
    </div>

