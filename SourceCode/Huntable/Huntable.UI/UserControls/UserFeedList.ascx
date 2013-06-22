<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserFeedList.ascx.cs"
    Inherits="Huntable.UI.UserControls.UserFeedList" %>
<asp:HiddenField ID="hdnprofileUserId" runat="server" />
<asp:HiddenField ID="hdnPageType" runat="server" />
<div id="UserFeedContainer">
</div>
<div class="all-feeds-list">
    <asp:Button ID="btnShowMoreFeeds" OnClientClick="return GetUserFeeds()" Style="margin-left: 180px;"
        class="show-more" Text="Show more" runat="server" />
    <input type="hidden" id="hdnPageIndex" value="0" />
    <input type="hidden" id="hdnLatestFeedId" value="0" />
    <input type="hidden" id="hdnUserImage" runat="server" value="0" />
</div>
    <script src="https://huntable.co.uk/js/UserFeed.js" type="text/javascript"></script>
<script type="text/javascript">
    var timeout;
    var xhrGetLatestUserFeeds = null;
    var xhrGetUserFeeds = null;
    var reachedEnd = false;

    $(document).ready(function () {
        
        GetUserFeeds();
        setFunctionTimeourt();
        $(window).bind("scroll", function () {
            if ($(window).scrollTop() + 300 > $(document).height() - $(window).height()) {
                GetUserFeeds();
            }
        });
    });
    function setFunctionTimeourt() {
        clearTimeout(timeout);
        timeout = setTimeout("GetLatestUserFeeds()", 60000);
    }
    function GetLatestUserFeeds() {
        clearTimeout(timeout);
        if (xhrGetLatestUserFeeds == null) {
            xhrGetLatestUserFeeds = $.ajax({
                type: "POST",
                url: "/UserFeedService.asmx/GetUserFeedList",
                data: '{'
                    + '"type":"' + $("#<%=hdnPageType.ClientID %>").val() + '",'
                    + '"profileUserId":"' + $("#<%=hdnprofileUserId.ClientID %>").val() + '",'
                    + '"latestFeedId":"' + $("#hdnLatestFeedId").val() + '",'
                    + '"pageIndex":"0"'
                    + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (msg) {
                    var List = msg.d;
                    $("#hdnLatestFeedId").val(List.LatestFeedId);
                    var str = '';
                    if (List.feeds != null) {
                        var str = '';
                        $.each(List.feeds, function (index, record) {
                            str = str + record.feedDescription;
//                            $('#UserFeedContainer').eq(index).after(record.feedDescription);
                        });
                        $('#UserFeedContainer').prepend(str);
                        $.each(List.feeds, function (index, record) {
                            GetComments(record.feedId);
                            DisplayComment(record.feedId, $('#<%=hdnUserImage.ClientID %>').val());
                        });
                    }
                    setFunctionTimeourt();
                    xhrGetLatestUserFeeds = null;
                },
                error: function (msg) {
                }
            });
        }
        return false;
    }
    function GetUserFeeds() {
        if (xhrGetUserFeeds == null && reachedEnd == false) {
            xhrGetUserFeeds = $.ajax({
                type: "POST",
                url: "/UserFeedService.asmx/GetUserFeedList",
                data: '{'
                    + '"type":"' + $("#<%=hdnPageType.ClientID %>").val() + '",'
                    + '"profileUserId":"' + $("#<%=hdnprofileUserId.ClientID %>").val() + '",'
                    + '"latestFeedId":"0",'
                    + '"pageIndex":"' + $("#hdnPageIndex").val() + '"'
                    + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (msg) {
                    var List = msg.d;
                    if ($("#hdnLatestFeedId").val() == '0')
                        $("#hdnLatestFeedId").val(List.LatestFeedId);
                    $("#hdnPageIndex").val(List.PageIndex);

                    if (List.feeds != null) {
                        $.each(List.feeds, function (index, record) {
                            $('#UserFeedContainer').append(record.feedDescription);
                        });
                        $.each(List.feeds, function (index, record) {
                            GetComments(record.feedId);
                            DisplayComment(record.feedId, $('#<%=hdnUserImage.ClientID %>').val());
                        });
                    }
                    else {
                        $("#<%=btnShowMoreFeeds.ClientID %>").hide();
                        reachedEnd = true;
                    }
                    xhrGetUserFeeds = null;
                },
                error: function (msg) {
                }
            });
        }
        return false;
    }
</script>
