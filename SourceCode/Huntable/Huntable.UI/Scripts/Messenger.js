var Messenger = (function ($) {
    var module = {};
    var timerId;
    var commandUrl;
    var dialog = null;

    function createDialogContent(request) {
        // The new chat request dialog is generated here
        // You can edit the html and css classes to better match your site design
        var dialogContent = "<div>" +
            "<img align='middle' style='height:30px;width:30px' src='" + request.FromThumbnailUrl + "' />" +
                "<span>" + request.ChatRequestMessage + "</span>" +
                    "<div>" +
                        "<a href='/Users/" + request.FromUserId + "' target='_blank'>view user profile</a>" +
                            "</div>" +
                                "</div>";

        return $(dialogContent);
    }

    function updateOnline() {
        $.getJSON(commandUrl, function (result) {
            if (result != null && dialog == null) {
                // A chat request waiting - show the dialog
                dialog = $('<div />').html(createDialogContent(result).html()).dialog(
                    {
                        title: "Incoming Chat Request",
                        width: 400,
                        height: 300,
                        draggable: true,
                        open: function (event, ui) {
                            $(event.target).parent().css('position', 'fixed');
                            $(event.target).parent().css('top', '100px');
                            $(event.target).parent().css('left', '470px');

                        },
                        buttons: {
                            "Accept": function () {
                                $.getJSON(commandUrl + "&reqInitiator=" + result.FromUserId + "&reject=false");
                                $(this).dialog("destroy");
                                dialog = null;
                                window.open(result.MessengerUrl, result.FromUserId, 'width=640,height=500,resizable=0,menubar=0,status=0,toolbar=0');
                            },
                            "Reject": function () {
                                $(this).dialog("close");
                            }
                        },
                        close: function (ev, ui) {
                            $.getJSON(commandUrl + "&reqInitiator=" + result.FromUserId + "&reject=true");
                            $(this).dialog("destroy");
                            dialog = null;
                        }
                    });
                 
            }
        });
    }

    // Initialization function - call this from your code
    module.initialize = function (chatHomeUrl, userId, timestamp, hash, updateOnlineFrequency, name, thumbUrl) {
        
        commandUrl = chatHomeUrl + "/MessengerCommand.ashx?id=" + userId + "&timestamp=" + timestamp + "&hash=" + hash + "&name="+name+"&thumbUrl="+thumbUrl+"&callback=?";
        timerId = setInterval(updateOnline, updateOnlineFrequency);
        updateOnline();
    };

    return module;

} (jQuery));