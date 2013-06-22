<%@ Page Language="C#" AutoEventWireup="true" Inherits="oauth" Codebehind="oauth.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function closepopup() {
            window.opener.doneEvent();
            window.self.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:HiddenField ID="hfMode" runat="server"/>
       <asp:HiddenField ID="hfInvId" runat="server"/>
    </div>
    </form>
</body>
</html>
