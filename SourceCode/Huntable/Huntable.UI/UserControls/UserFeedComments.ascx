<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserFeedComments.ascx.cs"
    Inherits="Huntable.UI.UserControls.UserFeedComments" %>
<div style="width: 230px; border: 1px solid #89EFFF; border-radius: 4px 4px; margin-bottom: 10px;"
    class="comments-head" id="divCommentHeader">
</div>
<div class="comments" id="divCommentContainer">
</div>
<div class="comments" id="divNewComment">
</div>
<asp:HiddenField ID="hdnUserImage" runat="server" />
<asp:HiddenField ID="hdnFeedUserId" runat="server" />
<asp:HiddenField ID="hdnFeedId" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />
<asp:HiddenField ID="hdnRefRecId" runat="server" />
<script type="text/javascript">
    var OldestCommentId = 0;
    var latestFeedId = 0;
    $(document).ready(function () {
//        GetComments($("#<%=hdnFeedId.ClientID %>").val());
        GetPreviousFeedComments($("#<%=hdnFeedId.ClientID %>").val(), $("#<%=hdnFeedUserId.ClientID %>").val()
        , 0, $("#<%=hdnType.ClientID %>").val(), $("#<%=hdnRefRecId.ClientID %>").val(), 6);
    });
    function GetPreviousFeedComments(feedId, feedUserId, OldestFeedId, type, refRecordId, pageSize) {
        $.ajax({
            type: "POST",
            url: "/UserFeedService.asmx/GetPreviousComments",
            data: '{'
                    + '"feedId":"' + feedId + '",'
                    + '"feedUserId":"' + feedUserId + '",'
                    + '"type":"' + type + '",'
                    + '"refRecordId":"' + refRecordId + '",'
                    + '"pageSize":"' + pageSize + '",'
                    + '"oldestFeedId":"' + OldestFeedId + '"'
                    + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (msg) {
                var List = msg.d;
                if (List.CommentHeader != '') {
                    $("#divCommentHeader").html(List.CommentHeader);
                    $("#divCommentHeader").show();
                }
                else {
                    $("#divCommentHeader").hide();
                }
                if ($("#divCommentLink") != null)
                    $("#divCommentLink").html(List.CommentLinkHTML);

                if (List.comments != null) {
                    $.each(List.comments, function (index, record) {
                        $("#divCommentContainer").append(record.feedDescription);
                    });
                }
                $("#divNewComment").html(GetCommentFeedTextBox(List.feedId));
                OldestCommentId = List.OldestCommentId;
                latestFeedId = List.LatestCommentId;
            },
            error: function (msg) {
            }
        });
        return false;
    }
</script>
<script type="text/javascript">
    function GetCommentFeedTextBox(mainFeedId) {
        var str = '';
        str += '<div class="comments-desc" id="divNewComment">';
        str += '<div class="comments-desc-left comments-desc-left-new">';
        str += '<a href="#">';
        str += '<img width="46" src="' + $('#<%=hdnUserImage.ClientID %>').val() + '" alt="img">';
        str += '</a>';
        str += '</div>';
        str += '<div class="comments-desc-right">';
        str += '<textarea'
            + ' onblur="if(this.value==\'\')this.value=this.defaultValue;"'
            + ' onfocus="if(this.value==this.defaultValue)this.value=\'\';"'
            + ' onkeypress="AddCommentNotification(' + mainFeedId + ',\'' + $("#<%=hdnFeedUserId.ClientID %>").val() + '\',\'' + $("#<%=hdnType.ClientID %>").val() + '\',' + $("#<%=hdnRefRecId.ClientID %>").val() + ',this,event)"'
            + ' class="textarea-profile textarea-comment textarea-comment-pr">'
            + 'Write a comment...'
            + '</textarea>'
            + ' <br><br>';
        str += '</div>';
        str += '</div>';
        return str;
    }
</script>
<script type="text/javascript">
    function AddCommentNotification(feedId,feedUserId, type, refRecordId, txt, event) {
        if (event.keyCode && event.keyCode == '13') {
            if ($.trim($(txt).val()).length > 0) {
//                var latestFeedId = $('#feed_' + feedId).find('#hdnfeed_comment_latest').val();
                if (CheckIfUserLoggedIn()) {
                    $.ajax({
                        type: "POST",
                        url: "/UserFeedService.asmx/AddCommentNotification",
                        data: '{'
                    + '"feedId":"' + feedId + '",'
                    + '"feedUserId":"' + feedUserId + '",'
                    + '"type":"' + type + '",'
                    + '"refRecordId":"' + refRecordId + '",'
                    + '"comments":"' + $(txt).val() + '",'
                    + '"latestFeedId":"' + latestFeedId + '"'
                    + '}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        success: function (msg) {
                            var List = msg.d;
                            if (List.CommentHeader != '') {
                                $("#divCommentHeader").html(List.CommentHeader);
                            }
                            else {
                                $("#divCommentHeader").hide();
                            }
                            if ($("#divCommentLink") != null)
                                $("#divCommentLink").html(List.CommentLinkHTML);

                            if (List.comments != null) {
                                $.each(List.comments, function (index, record) {
                                    $("#divCommentContainer").append(record.feedDescription);
                                });
                            }
                            $("#divNewComment").html(GetCommentFeedTextBox(List.feedId));
                            OldestCommentId = List.OldestCommentId;
                            latestFeedId = List.LatestCommentId;
                        },
                        error: function (msg) {
                        }
                    });
                    $(txt).val('');
                }
            }
        }
        return true;
    }
</script>
    <script src="/js/UserFeed.js" type="text/javascript"></script>
