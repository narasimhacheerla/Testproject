function JqueryPost() {

    var argv = JqueryPost.arguments;
    var argc = argv.length;
    var strParms = new String()
    page = argv[0];
    for (var i = 1; i < argc; i++) {
        strParms += argv[i] + '&'
    }
    strParms = strParms.substr(0, strParms.length - 1);

    $.post(page, { __parameters: strParms },
        function (data) {
            jQuery.globalEval(data);
        });

}


    
  