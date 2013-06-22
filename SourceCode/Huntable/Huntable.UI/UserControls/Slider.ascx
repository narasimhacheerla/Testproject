<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Slider.ascx.cs" Inherits="Huntable.UI.UserControls.Slider" %>
<link rel="Stylesheet" type="text/css" href="../css/style-innerpage.css" />
<div class="click">
    join our affiliate and earn money-with just 4click
    <div class="Dragval">
        <span>Total friends You have :</span>
        <div id="Slider" style="float: right; width: 50%; margin-top: -0px; margin-right: -15px">
        </div>
        <div style="margin-right: -25px; margin-top: -0px">
            <input class="Output" type="text" id="amount" runat="server" /></div>
    </div>
    <div class="block-one">
        <br />
        This is how much you can get!
        <input class="Output1" type="text" id="txtValue" runat="server" /><br />
        <strong style="color: rgb(255, 140, 61);">You have nothing to loose</strong>
    </div>
</div>
   <script type="text/javascript">
       $(function () {
           $("#Slider").slider({
               range: "min",
               value: 1,
               min: 1,
               max: 700,
               slide: function (event, ui) {
                   $("#amount").val("$" + ui.value);
                   $("#txtValue").val("$" + ui.value * 5);
               }

           });

       });
    </script>