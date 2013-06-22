<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Messagepopup.ascx.cs" Inherits="Huntable.UI.UserControls.Messagepopup" %>

<link href="https://huntable.co.uk/css/style-innerpage.css" type="text/css" rel="stylesheet" />
    <link type="text/css" href="https://huntable.co.uk/css1/jquery-ui-1.8.23.custom.css" rel="stylesheet" />

    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
        <script type="text/javascript" src="https://huntable.co.uk/js1/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="https://huntable.co.uk/js1/jquery-ui-1.8.23.custom.min.js"></script>

    <script type="text/javascript" src="https://huntable.co.uk/js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="https://huntable.co.uk/js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
    


<div style=" margin: 8px 10px 0px 0px; padding: 6px 12px; float: right;"><asp:Button ID="btnMessage" Text="Message" runat="server" UseSubmitBehavior="false"
                                CssClass="button-orange" OnClientClick="return rowAction(this.name);"
                                OnClick="BtnMessageClick" /> </div>
                 <asp:HiddenField ID="hfSubject" runat="server" />  

<script type="text/javascript">
    $().ready(function () {
        $('#dialogContent').dialog({
            autoOpen: false,
            modal: true,
            bgiframe: true,
            title: "Send Message",
            width: 600,
            height: 380
        });
        $('#dialogContent').parent().appendTo($("form:first"));
    });
    function rowAction(uniqueID) {
        if (document.getElementById('<%=hdnUserId.ClientID %>').value != "") {
         
            $('#<%=lblname.ClientID %>').text('');
            $('#<%=txtMessage.ClientID %>').val('');
            $('#dialogContent').dialog('option', 'buttons',
				{
				    "Send": function () { __doPostBack(uniqueID, ''); $(this).dialog("close"); },
				    "Cancel": function () { $(this).dialog("close"); }
				});

            $('#dialogContent').dialog('open');

            return false;
        }
        else {
            $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
            $('#dialog').dialog('open');
            return false;

        }
    }
</script>

<script type="text/javascript" src="https://huntable.co.uk/js1/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="https://huntable.co.uk/js1/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript">
        $(function () {
            // Dialog
            $('#dialog').dialog({
                autoOpen: false,
                modal: true,
                width: 600,
                buttons: {
                    "Ok": function () {
                        $(this).dialog("close");
                    },
                    "Cancel": function () {
                        $(this).dialog("close");
                    }
                }
            });
        });
    </script>
<asp:HiddenField ID="hdnUserId" runat="server" />
    <div id="dialog" title="Confirm Message">
    <asp:Label runat="server" Text="You are not logged In.Please login first." ID="lblConfirmMessage"></asp:Label>
    </div>
 <asp:Label runat="server" ID="lblname"  Visible="False"></asp:Label>
 <div id="dialogContent" align="center">
        <div>
            To:
            <asp:TextBox ID="txtToAddress" runat="server" ReadOnly="true" /><br />
            What would you like to message<asp:Label ID="Label2" runat="server" />?<br />
            <asp:RadioButtonList ID="rbMessageList" runat="server">
                <asp:ListItem Value="0" Text="Job Enquiry"></asp:ListItem>
                <asp:ListItem Value="1" Text="Request endorsement"></asp:ListItem>
                <asp:ListItem Value="2" Text="Introduce Yourself"></asp:ListItem>
                <asp:ListItem Value="3" Text="New Business Opportunity"></asp:ListItem>
                <asp:ListItem Value="4" Text="Your Recruitment requirement"></asp:ListItem>
            </asp:RadioButtonList>
            <%--   Subject:
            <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox><br />--%>
            <%-- $('#<%=txtSubject.ClientID %>').val('');--%>
            <br />
            Message:
            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" style="font-size:12px;height:65px;width:150px;"/><br />
            <%--  <asp:Button ID="btnMessage" runat="server" CssClass="button-ash floatleft poplight"
                Text="Message" OnClick="btnMessage_Click" />--%>
        </div>
    </div>