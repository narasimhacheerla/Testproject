<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="WhatIsHuntable.aspx.cs" Inherits="Huntable.UI.WhatIsHuntable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="how-img-main">
        <div class="how-img">
            <a href="Default.aspx" class=" button-green button-green-how">Sign in</a>
        </div>
    </div>
    <div id="content-section">
        <div id="content-inner">
            <div class="how-list">
                <ul>
                    <li>
                        <h2>
                            Super Power Your Profile</h2>
                        <asp:ImageButton runat="server" ImageUrl="images/how-img1.jpg" width="280" 
                            height="174" alt="Super Power Your Profile" ID="sp" OnClick="spbtn"/>
                        

                    </li>
                    <li>
                        <h2>
                            Customize feeds &amp; jobs you receive</h2>
                        <a href="CustomizeFeedsAndJobs.aspx"><img src="images/how-img2.jpg" width="280" height="174" alt="Customize feeds &amp; jobs you receive" /></a>
                    </li>
                    <li>
                        <h2>
                            Follow your favourite person, company skill or industry.</h2>
                        <a href="Follow.aspx"><img src="images/how-img3.jpg" width="280" height="174" alt="Follow your favourite person, company skill or industry." /></a>
                    </li>
                    <li>
                        <h2>
                            Connect &amp; network</h2>
                       <a href="ConnectAndNetwork.aspx"><img src="images/how-img4.jpg" width="280" height="174" alt="Connect &amp; network" /></a> 
                    </li>
                    <li>
                        <h2>
                            Get Headhunted</h2>
                       <a href="GetHunted.aspx"><img src="images/how-img5.jpg" width="280" height="174" alt="Get Headhunted" /></a> 
                    </li>
                    <li>
                        <h2>
                            Find your Dream Job</h2>
                        <a href="FindYourDreamJob.aspx"><img src="images/how-img6.jpg" width="280" height="174" alt="Find your Dream Job" /></a>
                    </li>
                </ul>
                <div align="center" style="font-size: 30px; margin-top: 20px; float: left; width: 100%;">
                    Register now and is totally <strong class="red-color">Free!!!</strong>
                </div>
            </div>
        </div>
        <!-- content inner ends -->
          <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
                <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
                    type="text/javascript"></script>
                <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    </div>
    </div>
</asp:Content>
