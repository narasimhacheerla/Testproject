<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PreviewJob.aspx.cs" Inherits="Huntable.UI.PreviewJob" %>

<%@ Register TagPrefix="uc" TagName="ShowProfileImage" Src="~/UserControls/ShowProfileImage.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/NewStylesheet.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <div class="blue-box blue-box-job">
                    <asp:Label ID="lblJobTitle" runat="server" Font-Names="Georgia" Font-Size="14"></asp:Label>
                    <div class="address">
                        <asp:Label ID="lblCountry" runat="server" Font-Names="Georgia"></asp:Label>,<asp:Label
                            runat="server" ID="lblJobLoc" Font-Names="Georgia"></asp:Label><br />
                        <asp:Label ID="lblCurrSymbol" runat="server"></asp:Label>
                        <asp:Label ID="lblSalary" runat="server"></asp:Label>&nbsp;&nbsp;<asp:Label runat="server"
                            ID="lblSalCurType"></asp:Label>
                        <br />
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
                        <asp:Label runat="server" ID="lblCompName" Text="Company name" Font-Names="Georgia"></asp:Label>
                    </p>
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
                <div class="ess-info">
                    <div class="ess-info-left" style="font-family: Georgia; font-size: 14;">
                        <h3>
                            Essesntial Information</h3>
                        <ul style="font-family: Georgia; font-size: 14;">
                            <li class="info1">Job Type:</li>
                            <li class="info2">
                                <asp:Label ID="lblJobTypeEssential" runat="server"></asp:Label></li>
                            <li class="info1">Experience req:</li>
                            <li class="info2">
                                <asp:Label ID="lblExperienceRequiredEssential" runat="server"></asp:Label>
                            </li>
                            <li class="info1">Industry:</li>
                            <li class="info2">
                                <asp:Label ID="lblIndustryEssential" runat="server"></asp:Label>
                            </li>
                            <li class="info1">Skill:</li>
                            <li class="info2">
                                <asp:Label ID="lblSkillTypeEssential" Text="n/a" runat="server"></asp:Label></li>
                            <li class="info1">Salary:</li>
                            <li class="info2">
                                <asp:Label ID="lblSym" runat="server"></asp:Label>
                                <asp:Label ID="lblSalaryEssential" runat="server"></asp:Label>&nbsp;
                                <asp:Label ID="lblCurr" runat="server"></asp:Label>
                            </li>
                            <li class="info1" runat="server" visible="false">Sector:</li>
                            <li class="info2" runat="server" visible="false">
                                <asp:Label ID="lblSector" runat="server"></asp:Label>
                            </li>
                         <li id="Li1" class="info1" runat="server" >ReferenceNumber:</li>

                            <li id="Li2" class="info2" runat="server" >
                                <asp:Label ID="lblrefNumber" runat="server"></asp:Label>
                            </li>
                        </ul>
                    </div>
                </div>
                <asp:Button runat="server" ID="btnSave" Text="Post Now" class="button-green button-green-upload "
                    OnClick="LbPostNowCLick" />&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnCancel" Text="Edit Job" CssClass="button-orange button-green-upload "
                    PostBackUrl="~/Postjob.aspx?from=Preview" />
            </div>
            <div class="ess-info-right" style="margin-left: 392px; margin-top: -200px;">
                <table>
                    <tr>
                        <td>
                            <uc:ShowProfileImage runat="server" class="fancyzoom" ID="showProfileImage" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image runat="server" ID="showimage" />
                            <img src="" runat="server" class="pics" alt="" id="shwProfileImage11" width="57"
                                height="65" border="1" />
                        </td>
                        <td>
                            <img src="" runat="server" alt="" class="pics" id="shwProfileImage12" width="57"
                                height="65" border="1" />
                        </td>
                        <td>
                            <img src="" runat="server" alt="" class="pics" id="shwProfileImage13" width="57"
                                height="65" border="1" />
                            <%--<uc:ShowProfileImage runat="server" class="fancyzoom" ID="showProfileImage3" />--%>
                        </td>
                        <td>
                            <img src="" runat="server" alt="" class="pics" id="shwProfileImage14" width="57"
                                height="65" border="1" />
                            <%--<uc:ShowProfileImage runat="server" class="fancyzoom" ID="showProfileImage4" />--%>
                        </td>
                        <td>
                            <img src="" runat="server" alt="" class="pics" id="shwProfileImage15" width="57"
                                height="65" border="1" />
                            <%--<uc:ShowProfileImage runat="server" class="fancyzoom" ID="showProfileImage5" />--%>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:CheckBox runat="server" ID="chkcbCompany" Visible="false" />
            <asp:HiddenField runat="server" ID="hdnCountry" />
            <asp:HiddenField runat="server" ID="hdnMinExp" />
            <asp:HiddenField runat="server" ID="hdnMaxExp" />
            <asp:HiddenField runat="server" ID="hdnJobType" />
            <asp:HiddenField runat="server" ID="hdnIndustry" />
            <asp:HiddenField runat="server" ID="hdnSector" />
            <asp:HiddenField runat="server" ID="hdnSkill" />
            <asp:HiddenField runat="server" ID="hdnSalaryType" />
            <asp:HiddenField runat="server" ID="hdnSalary" />
            <asp:HiddenField runat="server" ID="phfPhoto1" />
            <asp:HiddenField runat="server" ID="phfPhoto2" />
            <asp:HiddenField runat="server" ID="phfPhoto3" />
            <asp:HiddenField runat="server" ID="phfPhoto4" />
            <asp:HiddenField runat="server" ID="phfPhoto5" />
             <asp:HiddenField runat="server" ID="phfPhoto1Full" />
            <asp:HiddenField runat="server" ID="phfPhoto2Full" />
            <asp:HiddenField runat="server" ID="phfPhoto3Full" />
            <asp:HiddenField runat="server" ID="phfPhoto4Full" />
            <asp:HiddenField runat="server" ID="phfPhoto5Full" />
            <asp:HiddenField runat="server" ID="referencenumber"/>
             <asp:HiddenField runat="server" ID="lblRecAppl"/>
             <asp:HiddenField runat="server" ID="lblExtSiteAppl"/>
        </div>
        <!-- content inner ends -->
    </div>
</asp:Content>
