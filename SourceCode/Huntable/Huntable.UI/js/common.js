
function OpenPopup(URL, popup_width, popup_height, fun_afterClose) {
    $.nmManual(URL, {
        sizes: {
            initW: popup_width, initH: popup_height,
            minW: popup_width, minH: popup_height,
            w: popup_width, h: popup_height
        },
        resizable: true,
        closeOnEscape: true,
        callbacks: {
            afterClose: fun_afterClose,
            initFilters: function (nm) {
                nm.filters.push('link');
                nm.filters.push('iframe');
            }

        }
    });

}
function OpenMainPopup(URL, fun_afterClose) {
    OpenPopup(URL, 920, 503, fun_afterClose);
}
function OpenLikePopup(URL, fun_afterClose) {
    OpenPopup(URL, 100, 503, fun_afterClose);
}
function openLink(url) {
    if (typeof parent.$.nmTop == 'function') {
        parent.window.location.href = url;
        parent.$.nmTop().close();
    }
    else {
        window.location.href = url;
    }
//    alert(typeof parent.$.nmTop == 'function'); // && parent.$.nmTop() !== undefined);
}
function CheckIfUserLoggedIn() {
    var res = true;
    $.ajax({
        type: "POST",
        url: "/UserFeedService.asmx/CheckIfUserLoggedIn",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            LikeDetail = msg.d;
            res = LikeDetail && res;
            if (LikeDetail == false) {
                alert("You are not logged In.Please login first.");
            }
        },
        error: function (msg) {
        }
    });
    return res;
}