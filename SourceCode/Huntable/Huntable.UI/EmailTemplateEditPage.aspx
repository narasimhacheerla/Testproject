<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="EmailTemplateEditPage.aspx.cs"
    Inherits="Huntable.UI.Admin.EmailTemplateEditPage" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">

        function Validate() {

            if (document.getElementById("<%= ddlEmailTemplate.ClientID %>").value == "-1") {
                alert('Please select a proper Template to update.');
                return false;
            }

            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <input type="hidden" value="" id="HdnSelectedText" runat="server" />
                <table width="100%">
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <h3 class="featured-head" style="float: left;">
                                Email Templates</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblTemplateName" Text="Template Name:"></asp:Label>
                            *
                        </td>
                        <td style="float:right;">
                            <asp:DropDownList ID="ddlEmailTemplate" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="DDLEmailTemplateSelectedIndexChanged" CssClass="textbox listbox">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <CKEditor:CKEditorControl Width="100%" Height="150px" ID="ckTemplate" runat="server"></CKEditor:CKEditorControl>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button runat="server" ID="btnOK" Text="Save" ValidationGroup="Edtname" OnClientClick="return Validate();" CssClass="button-green button-green-join"
                                OnClick="BtnOkClick" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js" type="text/javascript"></script>
<script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
