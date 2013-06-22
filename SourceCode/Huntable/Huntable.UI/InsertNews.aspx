<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="InsertNews.aspx.cs" Inherits="Huntable.UI.InsertNews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/NewStylesheet.css" type="text/css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
                <h3 class="login-heading">
                    Please Enter The Values</h3>
                <div id="st_horizontal" class="st_horizontal">
                    <asp:Label ID="lblTitle" runat="server" Text="Title:"></asp:Label>
                </div>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="textbox"></asp:TextBox>
                <br /><br />
                 <div id="Div1" class="st_horizontal">
                    <asp:Label ID="lblLink" runat="server" Text="Link:"></asp:Label>
                </div>
                <asp:TextBox ID="txtLink" runat="server" CssClass="textbox"></asp:TextBox>
                <br /><br />
                <div id="Div2" class="st_horizontal">
                    <asp:Label ID="lblPicturePath" runat="server" Text="PicturePath:"></asp:Label>
                </div>
                <asp:FileUpload ID="fuploads" runat="server" />
                <br /><br />
               <div style="margin-left:200px;" >
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                    CssClass="button-green button-green-join" />
                    </div>
                <asp:Label ID="Label5" runat="server" Visible="False"></asp:Label>
            </div>
        </div>
    </div>
     <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript" ></script>
</asp:Content>
