function GetComments(feedId) {
    $.ajax({
        type: "POST",
        url: "/UserFeedService.asmx/GetComments",
        data: '{'
                    + '"feedId":"' + feedId + '"'
                    + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (msg) {
            CommentDetail = msg.d;
            if (CommentDetail.comments != null) {
                var strDesc = '';
                $.each(CommentDetail.comments, function (index, record) {
                    strDesc = strDesc + record.feedDescription;
                });
                $('div[id="feed_comment_container_' + feedId + '"]').html(strDesc);
                $('div[id="feed_comment_container_' + feedId + '"]').show();
                if ($('div[id="feed_comment_container_' + feedId + '"]').find('.comments-head').length <= 0) {
                    $('div[id="feed_comment_container_' + feedId + '"]').prepend('<div class="comments-head" style="display:none;"></div>');
                }
                if (CommentDetail.CommentHeader != '') {
                    $('div[id="feed_comment_container_' + feedId + '"]').find('.comments-head').html(CommentDetail.CommentHeader);
                    $('div[id="feed_comment_container_' + feedId + '"]').find('.comments-head').show();
                }
                $('#feed_' + feedId).find('#hdnfeed_Id').val(feedId);
                $('#feed_' + feedId).find('#hdnfeed_comment_oldest').val(CommentDetail.OldestCommentId);
                $('#feed_' + feedId).find('#hdnfeed_comment_latest').val(CommentDetail.LatestCommentId);
            }
        },
        error: function (msg) {
        }
    });
    return false;
}
var xhrGetPreviousComments = null;
function GetPreviousComments(feedId, feedUserId, OldestFeedId, type, refRecordId, pageSize) {
    
    if (xhrGetPreviousComments == null) {
        xhrGetPreviousComments = $.ajax({
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
                CommentDetail = msg.d;
                var str = '';
                if (CommentDetail.comments != null) {
                    $.each(CommentDetail.comments, function (index, record) {
                        str = str + record.feedDescription;
                        //                    $('#feed_comment_container_' + feedId).eq(index + 1).after(record.feedDescription);
                    });
                }
                //$('div[id="feed_comment_container_' + feedId + '"]').prepend(str);
                $('div[id="feed_comment_container_' + feedId + '"]').find('.comments-head').after(str);
                
                if ($('div[id="feed_comment_container_' + feedId + '"]').find('.comments-head').length <= 0) {
                    $('div[id="feed_comment_container_' + feedId + '"]').prepend('<div class="comments-head" style="display:none;"></div>');
                }
                
                if (CommentDetail.CommentHeader != '') {
                    $('div[id="feed_comment_container_' + feedId + '"]').find('.comments-head').html(CommentDetail.CommentHeader);
                    $('div[id="feed_comment_container_' + feedId + '"]').find('.comments-head').show();
                }
                else {
                    $('div[id="feed_comment_container_' + feedId + '"]').find('.comments-head').html('');
                    $('div[id="feed_comment_container_' + feedId + '"]').find('.comments-head').hide();
                }
                $('#feed_' + feedId).find('#hdnfeed_Id').val(feedId);
                $('#feed_' + feedId).find('#hdnfeed_comment_oldest').val(CommentDetail.OldestCommentId);
                $('#feed_' + feedId).find('#hdnfeed_comment_latest').val(CommentDetail.LatestCommentId);
                xhrGetPreviousComments = null;
                
            },
            error: function (msg) {
            }
        });
        xhrGetPreviousComments = null;
    }
}
function DisplayComment(mainFeedId, userImg) {
    $('div[id="feed_comment_new_' + mainFeedId + '"]').html(GetCommentTextBox(mainFeedId, userImg));
}
function GetCommentTextBox(mainFeedId, userImg) {
    var str = '';
    str += '<div class="comments-desc">';
    str += '<div class="comments-desc-left comments-desc-left-new">';
    str += '<a href="#">';
    str += '<img width="46" height="45" src="' + userImg + '" alt="img">';
    str += '</a>';
    str += '</div>';
    str += '<div class="comments-desc-right">';
    str += '<textarea'
            + ' onblur="if(this.value==\'\')this.value=this.defaultValue;"'
            + ' onfocus="if(this.value==this.defaultValue)this.value=\'\';"'
            + ' onkeypress="addComment(' + mainFeedId + ',this,event)"'
            + ' class="textarea-profile textarea-comment textarea-comment-pr">'
            + 'Write a comment...'
            + '</textarea>'
            + ' <br><br>';
    str += '</div>';
    str += '</div>';
    return str;
}
function addComment(feedId, txt, event) {
    if (event.keyCode && event.keyCode == '13') {
        if ($.trim($(txt).val()).length > 0) {
            if (CheckIfUserLoggedIn()) {
                var latestFeedId = $('#feed_' + feedId).find('#hdnfeed_comment_latest').val();
                $.ajax({
                    type: "POST",
                    url: "/UserFeedService.asmx/AddFeedComment",
                    data: '{'
                    + '"feedId":"' + feedId + '",'
                    + '"comments":"' + $(txt).val() + '",'
                    + '"latestFeedId":"' + latestFeedId + '"'
                    + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    success: function (msg) {
                        CommentDetail = msg.d;
                        if (CommentDetail.comments.length > 0) {
                            $('div[id="feed_comment_container_' + feedId + '"]').show();
                            $.each(CommentDetail.comments, function (index, record) {
                                $('div[id="feed_comment_container_' + feedId + '"]').append(record.feedDescription);
                            });
                            if (CommentDetail.CommentHeader != '') {
                                $('div[id="feed_comment_container_' + feedId + '"]').find('.comments-head').html(CommentDetail.CommentHeader);
                                $('div[id="feed_comment_container_' + feedId + '"]').find('.comments-head').show();
                            }
                            $('#feed_' + feedId).find('#hdnfeed_Id').val(feedId);
                            $('#feed_' + feedId).find('#hdnfeed_comment_oldest').val(CommentDetail.OldestCommentId);
                            $('#feed_' + feedId).find('#hdnfeed_comment_latest').val(CommentDetail.LatestCommentId);
                        }
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


function MarkLike(feedId, likeType, refId) {
    if (CheckIfUserLoggedIn()) {
        $.ajax({
            type: "POST",
            url: "/UserFeedService.asmx/MarkLike",
            data: '{'
                    + '"feedId":"' + feedId + '",'
                    + '"type":"' + likeType + '",'
                    + '"refRecordId":"' + refId + '"'
                    + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {
                LikeDetail = msg.d;
                $('a[id="feed_link_like_' + feedId + '"]').replaceWith(LikeDetail.LikeLinkHTML);
                $('div[id="feed_like_container_' + feedId + '"]').html(LikeDetail.LikeHeader);
                $('div[id="feed_like_container_' + feedId + '"]').show();
            },
            error: function (msg) {
            }
        });
    }
}
function MarkUnlike(feedId, likeType, refId) {
    if (CheckIfUserLoggedIn()) {
        $.ajax({
            type: "POST",
            url: "/UserFeedService.asmx/MarkUnlike",
            data: '{'
                    + '"feedId":"' + feedId + '",'
                    + '"type":"' + likeType + '",'
                    + '"refRecordId":"' + refId + '"'
                    + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {
                LikeDetail = msg.d;
                $('a[id="feed_link_like_' + feedId + '"]').replaceWith(LikeDetail.LikeLinkHTML);
                $('div[id="feed_like_container_' + feedId + '"]').html(LikeDetail.LikeHeader);
                $('div[id="feed_like_container_' + feedId + '"]').show();
            },
            error: function (msg) {
            }
        });
    }
}

function MarkCommentLike(feedId, ctrl) {
    if (CheckIfUserLoggedIn()) {
        $.ajax({
            type: "POST",
            url: "/UserFeedService.asmx/MarkCommentLike",
            data: '{'
                    + '"feedId":"' + feedId + '"'
                    + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (msg) {
                LikeDetail = msg.d;
                $('span[id="' + ctrl + feedId + '"]').replaceWith(LikeDetail);
                //                $('#' + ctrl + feedId).replaceWith(LikeDetail);
            },
            error: function (msg) {
            }
        });
    }
}
function MarkCommentUnlike(feedId, ctrl) {
    if (CheckIfUserLoggedIn()) {
        $.ajax({
            type: "POST",
            url: "/UserFeedService.asmx/MarkCommentUnlike",
            data: '{'
                    + '"feedId":"' + feedId + '"'
                    + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {
                LikeDetail = msg.d;
                $('span[id="' + ctrl + feedId + '"]').replaceWith(LikeDetail);
                //                $('#' + ctrl + feedId).replaceWith(LikeDetail);
            },
            error: function (msg) {
            }
        });
    }
}

function deleteFeed(id, ctrl) {
    if (CheckIfUserLoggedIn()) {
        $.ajax({
            type: "POST",
            url: "/UserFeedService.asmx/DeleteFeed",
            data: '{'
                    + '"feedId":"' + id + '"'
                    + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (msg) {
                $('#' + ctrl).remove();
            },
            error: function (msg) {
            }
        });
    }
}
function hideFeed(id, ctrl) {
    if (CheckIfUserLoggedIn()) {
        $.ajax({
            type: "POST",
            url: "/UserFeedService.asmx/HideFeed",
            data: '{'
                    + '"feedId":"' + id + '"'
                    + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (msg) {
                $('#' + ctrl).remove();
            },
            error: function (msg) {
            }
        });
    }
}
function markFollow(userId, ctrlId) {
    if (CheckIfUserLoggedIn()) {
        $.ajax({
            type: "POST",
            url: "/UserFeedService.asmx/MarkFollow",
            data: '{'
                    + '"userId":"' + userId + '"'
                    + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
            success: function (msg) {
                $('a[id="' + ctrlId + '"]').remove();
                //                $('#' + ctrlId).remove();
            },
            error: function (msg) {
            }
        });
    }
}