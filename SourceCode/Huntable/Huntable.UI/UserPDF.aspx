<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPDF.aspx.cs" Inherits="Huntable.UI.UserPDF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="profile-box-main">
        <div class="profile-box-main-top">
            <div class="profile-box-left">
                <asp:Image runat="server" ID="imgProfile" class="profile-pic" Width="76" Height="81"
                    alt="Profile-pic" />
                <div style="float: right; margin-top: 0px;">
                    <asp:Label ID="lblName" runat="server" Text="Label" Font-Bold="True"></asp:Label><br />
                    <asp:Label ID="lblCurrentRole" class="accounts-link" runat="server" Text="Label"></asp:Label><br />
                    <asp:Label ID="lblLocation" runat="server" Text="Label"></asp:Label><br />
                    <asp:Label ID="lblTown" runat="server" Text="Label"></asp:Label><br />
                    skill:
                    <asp:Label ID="lblSkills" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
        </div>
        <div class="profile-box-main-bottom">
            <ul class="profile-details">
                <li class="details-small">Currents </li>
                <li class="details-large">
                    <asp:Label ID="lblCurrentPosition" runat="server"></asp:Label></li>
                <li class="details-small">Past </li>
                <li class="details-large">
                    <asp:Label ID="lblPastPosition" runat="server"></asp:Label><br />
                </li>
                <li class="details-small">Education </li>
                <li class="details-large">
                    <asp:Label ID="lblEducation" runat="server"></asp:Label><br />
                </li>
                <li class="details-small">Endorsements </li>
                <li class="details-large">
                    <asp:Label ID="lblEndorsement" runat="server"></asp:Label></li>
            </ul>
        </div>
    </div>
    <div class="profile-desc">
        <h2 class="profile-desc-heading">
            <b>Summary</b></h2>
        <p>
            <asp:Label runat="server" ID="lblSummary"></asp:Label>
            <br />
        </p>
        <br />
        <h2 class="profile-desc-heading">
            <b>Experience</b></h2>
        <div class="experience">
            <asp:Repeater runat="server" ID="rptrExperience">
                <ItemTemplate>
                    <div>
                        <asp:Label ID="Label13" class="accounts-link" runat="server" Text='<%#Eval("JobTitle") %>'></asp:Label><br />
                        <asp:Label ID="Label14" class="accounts-link" runat="server" Text='<%#Eval("Company") %>'></asp:Label><br />
                        <asp:Label ID="Label15" runat="server" Text='<%#Eval("Location") %>'></asp:Label><br />
                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("town") %>'></asp:Label><br />
                        <strong>
                            <asp:Label ID="Label16" runat="server" Text='<%#Eval("Period") %>'></asp:Label></strong><br />
                        <asp:Label ID="Label17" runat="server" Text='<%#Eval("Description") %>'></asp:Label><br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <br />
        <h2 class="profile-desc-heading">
            <b>Education</b></h2>
        <p>
            &nbsp;<asp:Repeater runat="server" ID="rpEducations">
                <ItemTemplate>
                    <div>
                        <asp:Label ID="Label13" runat="server" Text='<%#Eval("Institution") %>'></asp:Label><br />
                        <asp:Label ID="Label14" runat="server" Text='<%#Eval("Course") %>'></asp:Label><br />
                        <asp:Label ID="Label15" runat="server" Text='<%#Eval("Period") %>'></asp:Label><br />
                    </div>
                </ItemTemplate>
                <SeparatorTemplate>
                    <br />
                </SeparatorTemplate>
            </asp:Repeater>
        </p>
        <br />
        <h2 class="profile-desc-heading">
            <b>Industry, Skills &amp; Expertise</b></h2>
        <p>
            <asp:Label runat="server" ID="lblSkillsDetail"></asp:Label></p>
        <br />
        <h2 class="profile-desc-heading">
            <b>Personal Details</b></h2>
        <p>
            <table>
                <tr>
                    <td>
                        Phone:
                    </td>
                    <td>
                        <asp:Label ID="lblPhoneNumber" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Address:
                    </td>
                    <td>
                        <asp:Label ID="lblAddress" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        City:
                    </td>
                    <td>
                        <asp:Label ID="lblCity" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Country:
                    </td>
                    <td>
                        <asp:Label ID="lblCountry" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Birthday:
                    </td>
                    <td>
                        <asp:Label ID="lblBirthDay" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Marital status:
                    </td>
                    <td>
                        <asp:Label ID="lblMaritalStatus" runat="server" />
                    </td>
                </tr>
            </table>
        </p>
    </div>
    </form>
</body>
</html>
