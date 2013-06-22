<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestFeedHtml.aspx.cs" Inherits="Huntable.UI.TestFeedHtml" %>

<%@ Register src="UserControls/UserFeedList.ascx" tagname="UserFeedList" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:UserFeedList ID="UserFeedList1" runat="server" PageType="Networking" />
    
    </div>
    </form>
</body>
</html>
