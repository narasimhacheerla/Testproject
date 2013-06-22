 <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderBeforeLoggingIn.ascx.cs"
    Inherits="Huntable.UI.UserControls.HeaderBeforeLoggingIn" %>
<link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
  <div style="position:fixed; width:100%;"><div id="header-section" onkeypress="return WebForm_FireDefaultButton(event, '<%= btnLogin.ClientID %>')">
    
    
    <script type="text/javascript" src="js/script.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('div.test').click(function () {
                $('ul.list').slideToggle('medium');
            });
            var localTime = new Date();
            var year = localTime.getYear();
            var month = localTime.getMonth() + 1;
            var date = localTime.getDate();
            var hours = localTime.getHours();
            var minutes = localTime.getMinutes();
            var seconds = localTime.getSeconds();
            document.getElementById("headerBeforeLoggingIn_hdndt").value = "2013" + "/" + month + "/" + date + " " + hours + ":" + minutes + ":" + seconds;
          
        
        });
        $(document).ready(function () {
            $('div.test1').click(function () {
                $('ul.list1').slideToggle('medium');
            });
        });

        function popupform1(actin) {
            if (!window.focus) return true;
            document.forms[0].action = actin;
            window.open(actin, 'Please Login', 'width=600,height=300,toolbar=no, location=center,directories=no,statusbar=no,menubar=no,scrollbars=yes,copyhistory=no, resizable=yes');
            document.forms[0].target = 'mywindow';
            return true;
        }

        function openNewWindow() {
            alert("Please login to see these details.");
        }
    </script>
    <div id="Div3">
           <asp:HiddenField runat="server" ID="hdndt"/>
        <div id="header-inner">
            <div class="logo">
                <a title="Huntable - Find Hunt Hire" href="default.aspx">
                    <img id="Img1" title="Huntable - Find Hunt Hire" runat="server" alt="Huntable - Find Hunt Hire"
                        src="~/HuntableImages/logo.png" width="170" height="44" /></a>
            </div>
            <div class="menu1" style="margin-left:20px;">
                <ul class="menu-list">
                    <li><a title="Home" runat="server"  href="Default.aspx">
                        <img title="Home" alt="Home" runat="server" src="HuntableImages/icon-home.png" width="14"   height="15" />Home</a></li>
                    <li><asp:LinkButton ID="lnkHunt" runat="server" OnClick="lnkHunt_click" CausesValidation="false">
                        <img title="What is Huntable" runat="server" alt="What is Huntable" src="HuntableImages/icon-what.png"
                            width="14" height="15" />What is Huntable</asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkFriends" runat="server"  CausesValidation="false" 
                            onclick="lnkFriends_Click">
                        <img title="Find Friends" runat="server" alt="Find Friends" src="HuntableImages/icon-friends.png"
                            width="14" height="15" />Find Friends</asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkRecruiter" runat="server" CausesValidation="false" 
                              OnClick="lnkRecruiter_Click">
                        <img  title="Recruiter" runat="server" alt="Recruiter" src="images/icon-recruiter.png"
                            width="14" height="15" />Recruiter</asp:LinkButton></li></ul>
                <div class="login-section" style="margin-right:-90px;">
                    <input id="txtEmail" runat="server" type="text" onblur="if (this.value == '') {this.value ='E-mail';}"
                        onfocus="if (this.value =='E-mail') {this.value ='';}" value="E-mail" class="textbox-login" />
                    <div id="div2">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox-login" Text="Password" TextMode="Password"
                     onblur="if (this.value == '') {this.value ='Password';}" onfocus="if (this.value =='Password') {this.value ='';}" ></asp:TextBox>
                       <%-- <input id="txtPassword" class="textbox-login" value="Password" runat="server" type="password"
                            onblur="if (this.value == '') {this.value ='Password';}" onfocus="if (this.value =='Password') {this.value ='';}" />--%>
                    </div>
                    <b class="button-login">
                        <img runat="server" src="images/icon-login.png" width="10" height="15" alt="login" />
                        <asp:Button ID="btnLogin" OnClick="BtnSignInClick" runat="server" CssClass="button-login-inner" CausesValidation="false"
                            Text="Login" />
                    </b>
                    <asp:Label ID="lblSignInError" runat="server" Visible="False" />
                    <br />
                    <span class="rememberme">
                        <input name="rememberme" class="checkbox" type="checkbox" runat="server" value=""
                            id="chkRemember" />
                        Remember me &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a
                            href="ForgotPassword.aspx">Forgot Password?</a></span>
                </div>
            </div>
        </div>
    </div>
</div></div><div style=" height:79px;"> </div>
<!-- Header section ends -->
<!--[if lte IE 6]>
<script type="text/javascript" >
//author: ?, I found this useful.
startList = function() {
if (document.all&&document.getElementById) {
navRoot = document.getElementById("nav");
for (i=0; i<navRoot.childNodes.length; i++) {
node = navRoot.childNodes[i];
if (node.nodeName=="LI") {
node.onmouseover=function() {
this.className+=" over";
  }
  node.onmouseout=function() {
  this.className=this.className.replace(" over", "");
   }
   }
  }
 }
}
window.onload=startList;

</script>
<![endif]-->
