<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Likes.aspx.cs" Inherits="Huntable.UI.Likes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdnOldestLikeFeedId" runat="server" />
    <asp:HiddenField ID="hdnprofileUserId" runat="server" Value="0" />
    <link href="js/nyroModal/styles/nyroModal.css" rel="stylesheet" type="text/css" />
    <script src="js/nyroModal/js/jquery.nyroModal.custom.min.js" type="text/javascript"></script>
    <!--[if IE 6]>
	<script type="text/javascript" src="js/jquery.nyroModal-ie6.min.js"></script>
<![endif]-->
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left content-inner-left-message">
                <div>
                    <h4 class="login-heading">
                        <a href="#" class="accounts-link"><Asp:Label runat="server" ID="lblName"></Asp:Label></a> > <a href="#" class="accounts-link">
                            Profile</a> > <a href="#" class="accounts-link">Activity</a> > Likes</h4>
                    <%--<a href="#?w=830" class="button-ash poplight" rel="popup15" style="float: right;
                        margin-top: -50px; margin-right: -74px;">Add Videos</a> <a href="#?w=830" class="button-ash button-ash-m poplight"
                            rel="popup14" style="float: right; margin-top: -50px;">Add Pictures</a>--%>
                    <div class="gallery">
                        <ul id="divUserPhotoLikes">
                        </ul>
                        <%-- <ul id="videos">
                            <li><a href="images/cake1.jpg" name="image" title="Bottle Opener" class="video_link"
                                rel="gallery">
                                <img src="images/featured-logo1.jpg" class="profile-pic" alt="bottleopener" width="160" /></a>
                                <a href="#" class="reply-link">
                                    <img alt="Like" src="images/icon-like.png" />Like</a> </li>
                        </ul>--%>
                    </div>
                </div>
            </div>
            <div class="content-inner-right content-inner-right-message">
                <div class="google-add">
                     <asp:Image ID="bimage" runat="server" CssClass="advert1"  ImageUrl="images/basic-user-advert.gif"/>
                <asp:Image ID="pimage" runat="server" CssClass="advert1"   ImageUrl="images/premium-user-advert.gif" />
                </div>
            </div>
        </div>
        <!-- content inner ends -->
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            GetLikedPhotos();
            $(window).bind("scroll", function () {
                if ($(window).scrollTop() + 300 > $(document).height() - $(window).height()) {
                    if ($("#<%=hdnOldestLikeFeedId.ClientID %>").val() != '0')
                        GetLikedPhotos();
                }
            });
        });
        var cnt = 1;
        function GetLikedPhotos() {
            $.ajax({
                type: "POST",
                url: "/UserFeedService.asmx/GetUserPhotoLikes",
                data: '{'
                    + '"pagesize":"20",'
                    + '"profileUserId":"' + $("#<%=hdnprofileUserId.ClientID %>").val() + '",'
                    + '"oldestLikeId":"' + $("#<%=hdnOldestLikeFeedId.ClientID %>").val() + '",'
                    + '"width":""'
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
                            str = '';
                            //                            if (index != 0 && index % 4 == 0)
                            //                                str = '<li class="clear"></li>';
                            str = str
                                + '<li>'
                                + record.detailHTML
                                + record.actionHTML
                                + '</li>';
                            $('#divUserPhotoLikes').append(str);
                        });
                        if (cnt < 6 && LikeDetail.OldestLikeId > 0)
                            GetLikedPhotos();
                        cnt++;
                    }
                },
                error: function (msg) {
                }
            });
        }
    </script>
    <script type="text/javascript">
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
                        $('#feed_link_like_' + feedId).replaceWith(LikeDetail.LikeLinkHTML);
                    },
                    error: function (msg) {
                    }
                });
            }
        }
    </script>
    <script type="text/javascript">
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
                        $('#feed_link_like_' + feedId).replaceWith(LikeDetail.LikeLinkHTML);
                    },
                    error: function (msg) {
                    }
                });
            }
        }
    </script>
</asp:Content>
