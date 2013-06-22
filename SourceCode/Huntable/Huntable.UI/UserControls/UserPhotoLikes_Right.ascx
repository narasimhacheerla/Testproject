<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserPhotoLikes_Right.ascx.cs"
    Inherits="Huntable.UI.UserControls.UserPhotoLikes_Right" %>
<asp:HiddenField ID="hdnOldestLikeFeedId" runat="server" />
<asp:HiddenField ID="hdnprofileUserId" runat="server" Value="0" />
<div class="activity-img-block">
    <h3>
        <a href="https://huntable.co.uk/Likes.aspx" id="hyptopLike" runat="server"><span id="user_likes_photos_total"></span> likes</a></h3>
    <div class="auto-adjust-imgblock">
        <div id="likedPhotoList">
        </div>
    </div>
    <a href="https://huntable.co.uk/Likes.aspx" id="hypbottomLike" runat="server" class="accounts-link accounts-link-more" style="margin-left: 142px;">
        &rsaquo;&rsaquo;&nbsp; See more</a>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        GetLikedPhotos();
//        $(window).bind("scroll", function () {
//            if ($(window).scrollTop() + 300 > $(document).height() - $(window).height()) {
//                GetLikedPhotos();
//            }
//        });
    });
    var cnt = 1;
    function GetLikedPhotos() {
        $.ajax({
            type: "POST",
            url: "/UserFeedService.asmx/GetUserPhotoLikes",
            data: '{'
                    + '"pagesize":"4",'
                    + '"profileUserId":"' + $("#<%=hdnprofileUserId.ClientID %>").val() + '",'
                    + '"oldestLikeId":"' + $("#<%=hdnOldestLikeFeedId.ClientID %>").val() + '",'
                    + '"width":"107px"'
                    + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {
                LikeDetail = msg.d;
                $("#<%=hdnOldestLikeFeedId.ClientID %>").val(LikeDetail.OldestLikeId);
                $('#user_likes_photos_total').html(LikeDetail.totalLikes);
                if (LikeDetail.detail != null) {
                    var str = '';
                    $.each(LikeDetail.detail, function (index, record) {
                        if (index % 4 == 0)
                            str = str + '<div class="clear"></div>';
                        str = str + record.detailHTML;
                    });
                    $('#likedPhotoList').html(str);
//                    if(cnt<6)
//                        GetLikedPhotos();
                }
            },
            error: function (msg) {
            }
        });
    }
</script>
<script type="text/javascript">
    function MarkPhotoLike(feedId, likeType, refId) {
        if (CheckIfUserLoggedIn()) {
            $.ajax({
                type: "POST",
                url: "/UserFeedService.asmx/MarkPhotoLike",
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
                    $('#feed_link_like_' + feedId).replaceWith(LikeDetail.LikeLinkHTML);
                    $('#feed_like_container_' + feedId).html(LikeDetail.LikeHeader);
                    $('#feed_like_container_' + feedId).show();
                },
                error: function (msg) {
                }
            });
        }
    }
</script>
<script type="text/javascript">
    function MarkPhotoUnlike(feedId, likeType, refId) {
        if (CheckIfUserLoggedIn()) {
            $.ajax({
                type: "POST",
                url: "/UserFeedService.asmx/MarkPhotoUnlike",
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
                    $('#feed_link_like_' + feedId).replaceWith(LikeDetail.LikeLinkHTML);
                    $('#feed_like_container_' + feedId).html(LikeDetail.LikeHeader);
                    $('#feed_like_container_' + feedId).show();
                },
                error: function (msg) {
                }
            });
        }
    }
</script>
