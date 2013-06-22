<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="header-innerpage.aspx.cs" Inherits="Huntable.UI.header_innerpage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- this script used for both clickable slide and tab slide function -->
<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
<!-- this script used for both clickable slide and tab slide function -->
<script type="text/javascript">
    $(document).ready(function () {
        $('div.test').click(function () {
            $('ul.list').slideToggle('medium');
        });
    });
    $(document).ready(function () {
        $('div.test1').click(function () {
            $('ul.list1').slideToggle('medium');
        });
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
   <div id="header-inner">
  <div class="logo"> <a href="." title="Huntable - Find Hunt Hire"><img src="images/logo.png" width="170" height="44" alt="Huntable - Find Hunt Hire" title="Huntable - Find Hunt Hire" /></a> </div>
  <div class="menu1">
    <ul class="menu-list">
      <li><a href="." title="Home"><img src="images/icon-home.png" width="14" height="15" alt="Home" title="Home" />Home</a></li>
      <li><a href="#" title="What is Huntable"><img src="images/icon-what.png" width="14" height="15" alt="What is Huntable" title="What is Huntable" />What is Huntable</a></li>
      <li><a href="#" title="Find Friends"><img src="images/icon-friends.png" width="14" height="15" alt="Find Friends" title="Find Friends" />Find Friends</a></li>
    </ul> 
      	
    <div  class="test" >
    <div class="list-main">
    <a href="#" class="name1">David Walsh</a>
    <ul class="list">
     <li><a href="#">Profile</a></li>
     <li><a href="#"> Signout</a></li>
     </ul>
    
    </div>
    
  </div>  
  </div> 
   
</div>
    </form>
</body>
</html>
