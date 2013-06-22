<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserFeedAlerts.ascx.cs"
    Inherits="Huntable.UI.UserControls.UserFeedAlerts" %>
<div style="margin-top: -10px; margin-left: 0px; position: relative;" class="message-inbox"
    id="divAlertCotainer">
    <img width="20" height="14" style="position: relative;" src="/Images/bell01.png"
        alt="inbox" id="Img6">
    <a href='#' id='hypalertcount' style='display: none;' class="count"></a>
    <div class="hover-menu" style="display: none;">
        <div class="arrowt shadow">
            <img src="/images/top-arrow1.png" width="23" height="16" alt="arrow" /></div>
        <div class="hover-menu-wrap">
            <div class="top-part">
                <h4 class="head-line">
                    Alerts
                    <%--(<span id="lblalertcount"></span>)--%></h4>
                <ul id="ulalert">
                    <li>
                        <div class="sub-text">
                            No Alerts
                        </div>
                    </li>
                </ul>
            </div>
            <%-- <div class="clr">
            </div>
            <div class="bot-part">
                <h4 class="head-line">
                    Messages (2)</h4>
                <h4 class="hedd-line-left">
                    Compose Message</h4>
                <ul>
                    <li>
                        <img src="/images/d-user.jpg" width="42" height="42" alt="user-ic" />
                        <div class="name-date">
                            <span class="name">User Name</span> <span class="date">Feb 22</span><br />
                        </div>
                        <div class="sub-button">
                            <a href="#" class="ac-but">Reply</a> <a href="#" class="ig-but">Delete</a>
                        </div>
                        <div class="sub-text">
                            Ceo Of Pvt Ltd Company
                        </div>
                    </li>
                    <li>
                        <img src="/images/d-user.jpg" width="42" height="42" alt="user-ic" />
                        <div class="name-date">
                            <span class="name">User Name</span> <span class="date">Feb 22</span><br />
                        </div>
                        <div class="sub-button">
                            <a href="#" class="ac-but">Reply</a> <a href="#" class="ig-but">Delete</a>
                        </div>
                        <div class="sub-text">
                            Ceo Of Pvt Ltd Company
                        </div>
                    </li>
                </ul>
            </div>--%>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $('#divAlertCotainer').hover(
	     function () {
	         resetCounter();
	         $('#divAlertCotainer').find('div.hover-menu').fadeIn();
	     },
		 function () {
		     $('#divAlertCotainer').find('div.hover-menu').fadeOut();
		 });

        //        $('.hover-menu-wrap ul li').hover(
        //	       function () {
        //	           $(this).find('div.sub-text').hide();
        //	           $(this).find('div.sub-button').show();
        //	       },
        //           function () {
        //               $(this).find('div.sub-text').show();
        //               $(this).find('div.sub-button').hide();
        //        });

    });
</script>
<script type="text/javascript">
    var alerttimeout;
    var xhrgetAlertCount = null;

    $(document).ready(function () {
        getAlertCount();
        setAlertTimeout();
    });
    function setAlertTimeout() {
        clearTimeout(alerttimeout);
        alerttimeout = setTimeout("getAlertCount()", 60000);
    }
    function resetCounter() {
        $('#hypalertcount').html('');
        //        $('#lblalertcount').html('');
        $('#hypalertcount').hide();
    }
    function getAlertCount() {
        if (CheckIfUserLoggedIn()) {
            if (xhrgetAlertCount == null) {
                xhrgetAlertCount = $.ajax({
                    type: "POST",
                    url: "/UserFeedService.asmx/getAlertCount",
                    data: '{}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    success: function (msg) {
                        var cnt = msg.d;
                        getAlertList();
                        if (cnt > 0) {
                            $('#hypalertcount').html(cnt);
                            //                        $('#lblalertcount').html(cnt);
                            $('#hypalertcount').show();
                        }
                        else {
                            $('#hypalertcount').html('');
                            //                        $('#lblalertcount').html('');
                            $('#hypalertcount').hide();
                        }
                        setAlertTimeout();
                        xhrgetAlertCount = null;
                    },
                    error: function (msg) {
                    }
                });
            }
        }
    }
    function getAlertList() {
        if (CheckIfUserLoggedIn()) {
            $.ajax({
                type: "POST",
                url: "/UserFeedService.asmx/getAlertList",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (msg) {
                    var list = msg.d;
                    if (list != null) {
                        var str = '';
                        $.each(list, function (index, record) {
                            str = str + record.feedDescription;
                        });
                        //                        alert(str);
                        $('#ulalert').html(str);
                    }
                    else {
                        var str = '';
                        str = str + '<li>';
                        str = str + '<div class="sub-text">';
                        str = str + 'No Alerts';
                        str = str + '</div>';
                        str = str + '</li>';
                        $('#ulalert').html(str);
                    }
                },
                error: function (msg) {
                }
            });
        }
    }
    function displayFeed(id) {
        var l = window.location;
        var baseUrl = l.protocol + "//" + l.host + "/";
        window.document.location.href = baseUrl+"UserFeed.aspx?feedId=" + id;
    }
</script>
