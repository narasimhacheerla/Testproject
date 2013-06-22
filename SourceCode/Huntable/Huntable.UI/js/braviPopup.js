/*
Author      :   Tanweer Akhtar (tanweer_bravi@yahoo.com)
Date        :   24-05-2012
Description :   A very light and easy popup control using jquery
License     :   Copyright@Tanweer Bravi. tanweer_bravi@yahoo.com
*/
(function ($) {
    $.braviPopUp = function (title, src, width, height) {
        //Destroy if exist
        $('#dv_move').remove();
        //create hte popup html
        var html = '<div class="main" id="dv_move" style="width:' + width + 'px; height:' + height + 'px;">';
        html += '  <div >';
        html += '    <span >' + title + '</span> <span class="close">';
        html += ' <img id="img_close" src="images/close.png" width="25" height="23" onclick="CloseDialog();"></span></div>';
        html += ' <div id="dv_no_move">';
        html += '<div ><img src="images/circular.gif"/></div>';
        html += ' <iframe id="url" scrolling="auto" src="' + src + '"  style="border:none;" width="100%" height="100%"></iframe>';
        html += ' </div>';
        html += ' </div>';

        //add to body
        $('<div></div>').prependTo('body').attr('id', 'overlay'); // add overlay div to disable the parent page
        $('body').append(html);
        //enable dragable
        $('#dv_move').draggable();
        //enable resizeable
        $("#dv_move").resizable({
            minWidth: 300,
            minHeight: 100,
            maxHeight: 768,
            maxWidth: 1024
        });

        $("#dv_no_move").mousedown(function () {
            return false;
        });
        $("#title_left").mousedown(function () {
            return false;
        });
        $("#img_close").mousedown(function () {
            return false;
        });
        //change close icon image on hover
        $("#img_close").mouseover(function () {
            $(this).attr("src", 'images/close2.png');
        });
        $("#img_close").mouseout(function () {
            $(this).attr("src", 'images/close.png');
        });

        setTimeout("$('#dv_load').hide();", 1500);
    };
})(jQuery);

//close popup
function CloseDialog() {
    $('#overlay').fadeOut('slow');
    $('#dv_move').fadeOut('slow');
    setTimeout("$('#dv_move').remove();", 1000);

    //call Refresh(); if we need to reload the parent page on its closing
    // parent.Refresh();
}