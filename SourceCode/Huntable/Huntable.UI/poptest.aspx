<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopTest.aspx.cs" Inherits="Huntable.UI.PopTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function popupform1(actin) {
            //            if (!window.focus) return true;
            document.forms[0].action = actin;
            window.open(actin, 'mywindow', 'width=800,height=500,toolbar=no, location=no,directories=no,statusbar=no,menubar=no,scrollbars=yes,copyhistory=no, resizable=yes');
            document.forms[0].target = 'mywindow';
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <asp:ImageButton ID="Share" runat="server" ImageUrl="~/images/social.png" />
    </div>
    </form>
</body>
</html>
