<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowProfileImage.ascx.cs"
    Inherits="Huntable.UI.UserControls.ShowProfileImage" %>
<script type="text/javascript">$(document).ready(function () { /* * Examples - images */ $("a#bigImage").fancybox(); });</script>
<a class="fancybox" runat="server" rel="group" id="bigImage" clientidmode="Static" >
    <img id="smallImage" runat="server" alt="" width="50" height="50" clientidmode="Static" /></a> 