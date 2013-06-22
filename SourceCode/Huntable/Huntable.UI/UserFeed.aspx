<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserFeed.aspx.cs" Inherits="Huntable.UI.UserFeed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left alert_bg">
                <asp:Literal ID="litFeed" runat="server"></asp:Literal>
                <%-- <div class="all-feeds-list alert_innerdiv">
                    <div class="feed-left">
                        <a href="#">
                            <img width="76" height="81" class="profile-pic" src="images/profile-thumb-large.jpg"
                                alt="feeds-img"></a></div>
                    <div class="feed-right feed-right-inner">
                        <a href="#">Komal Patel </a>liked <a href="#">Srihari Kothapalli</a>'s update
                        <div class="video-comment">
                            <div class="video-post">
                                <img class="profile-pic profile-pic2" alt="Feaured-logo" src="images/profile-thumb-large.jpg">
                            </div>
                            <div class="follow_profile">
                                <a href="#">Srihari Kothapalli</a> is following Company <a href="#">IBM - Internation
                                    Business Machines</a><br>
                                <img width="569" height="278" title="IBM" alt="IBM" src="images/ibm_logo.jpg"><br>
                                <a href="www.ibm.com">www.ibm.com</a><br>
                                IBM is extremely excited about enhancing its presence in your city - and looks forward
                                to reaching out to a wide range of organizations across the public and private sector<br>
                                <a class="invite-friend-btn invite-friend-btn-ov" href="#">Follow</a></div>
                        </div>
                        <div class="like-portion">
                            <a href="#" style="margin-left: 0px;">
                                <img width="13" height="12" src="images/icon-like.png" alt="Like">Like</a> <a href="#">
                                    <img width="13" height="12" src="images/icon-comment.png" alt="Like">Comment</a>
                            <span>5 Minutes ago</span>
                            <div class="comments-head">
                                <img width="13" height="12" alt="comments" style="margin-top: 2px;" src="images/icon-like1.png"><span
                                    style="float: left; margin: 0px;">You and&nbsp; </span><a href="#?w=300" class="accounts-link poplight"
                                        rel="popup13">2 others</a>&nbsp;like this
                            </div>
                            <div class="comments">
                                <div class="comments-desc">
                                    <div class="comments-desc-left">
                                        <a href="#">
                                            <img width="46" height="45" src="images/profile-thumb-small.jpg" alt="img">
                                        </a>
                                    </div>
                                    <div class="comments-desc-right">
                                        <textarea onblur="if(this.value=='')this.value=this.defaultValue;" onfocus="if(this.value==this.defaultValue)this.value='';"
                                            class="textarea-profile textarea-comment">Write a comment...</textarea>
                                        <br>
                                        <br>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
            </div>
            <div class="content-inner-right advertisement_rightside">
                <img width="120" height="600" title="Advertisement" alt="Advertisement" src="images/basic-user-advert.gif"></div>
        </div>
        <!-- content inner ends -->
    </div>
    <asp:HiddenField ID="hdnFeedId" runat="server" Value="0" />
    <asp:HiddenField ID="hdnUserImage" runat="server" Value="0" />
    <link href="js/nyroModal/styles/nyroModal.css" rel="stylesheet" type="text/css" />
    <script src="js/nyroModal/js/jquery.nyroModal.custom.min.js" type="text/javascript"></script>
    <!--[if IE 6]>
	<script type="text/javascript" src="js/jquery.nyroModal-ie6.min.js"></script>
<![endif]-->
    <script src="js/UserFeed.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var feedId = $("#<%=hdnFeedId.ClientID %>").val();
            GetComments(feedId);
            DisplayComment(feedId, $('#<%=hdnUserImage.ClientID %>').val());
        });
    </script>
</asp:Content>
