<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserFeedLikedUser.ascx.cs"
    Inherits="Huntable.UI.UserControls.UserFeedLikedUser" %>
<div class="box-right" style="margin-top: 20px;" id="divLikeListContainer">
    <div class="comments-head comments-head-com " id="divLikeHeader">
        <img width="13" height="12" src="images/icon-like1.png" alt="Like">
        You and<a class="accounts-link" style="margin: 0px 5px;" href="#">2 others</a>like
        this
    </div>
    <div id="divLikeContainer"></div>
</div>
<asp:HiddenField ID="hdnFeedId" runat="server" />
<asp:HiddenField ID="hdnType" runat="server" />
<asp:HiddenField ID="hdnRefRecId" runat="server" />
<script type="text/javascript">
    $(document).ready(function () {
        $.ajax({
            type: "POST",
            url: "/UserFeedService.asmx/GetFeedLikeDetail",
            data: '{'
                    + '"feedId":"' + $("#<%=hdnFeedId.ClientID %>").val() + '",'
                    + '"type":"' + $("#<%=hdnType.ClientID %>").val() + '",'
                    + '"refRecordId":"' + $("#<%=hdnRefRecId.ClientID %>").val() + '"'
                    + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {
                var List = msg.d;

                if (List.LikeHeader != null && List.LikeHeader != '') {
                    $("#divLikeHeader").html(List.LikeHeader);
                    $("#divLikeHeader").show();
                }
                else
                    $("#divLikeHeader").hide();

                if ($("#divLikeLink") != null)
                    $("#divLikeLink").html(List.LikeLinkHTML);
                GetLikedUserList(0);
            },
            error: function (msg) {
            }
        });
    });
</script>
<script type="text/javascript">
    function GetLikedUserList(oldestLikeId) {
        $("input.show-more").remove();
        $.ajax({
            type: "POST",
            url: "/UserFeedService.asmx/GetFeedLikedUser",
            data: '{'
                    + '"feedId":"' + $("#<%=hdnFeedId.ClientID %>").val() + '",'
                    + '"type":"' + $("#<%=hdnType.ClientID %>").val() + '",'
                    + '"refRecordId":"' + $("#<%=hdnRefRecId.ClientID %>").val() + '",'
                    + '"pagesize":"10",'
                    + '"oldestLikeId":"' + oldestLikeId + '"'
                    + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            success: function (msg) {
                var List = msg.d;

                if (List.LikedUsers != null) {
                    $.each(List.LikedUsers, function (index, record) {
                        $("#divLikeContainer").append(record);
                    });
                }
            },
            error: function (msg) {
            }
        });
    }
</script>
    <script src="/js/UserFeed.js" type="text/javascript"></script>
