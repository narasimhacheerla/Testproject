<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfosysEmployees(3).ascx.cs" Inherits="Huntable.UI.UserControls.InfosysEmployees_3_" %>


   <link href="css/portfolio-js-style.css" type="text/css" rel="stylesheet" />
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
            function rowAction01() {
                if (document.getElementById('<%=hdnUserId.ClientID %>').value != "") {

                }
                else {
                    $('#<%=lblConfirmMessage.ClientID %>').text("You are not logged In.Please login first.");
                    $('#dialog').dialog('open');
                    return false;

                }
            }
</script>
<script type="text/javascript" src="../js1/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../js1/jquery-ui-1.8.23.custom.min.js"></script>
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



      <div>
                            <h2 align="center" style="font-size: 14px; margin-bottom: 10px; font-weight: bold;">
                                                                   <asp:Label runat="server" ID="lbl" ></asp:Label> Employees
                                                                    <br />You May Want To Follow
</h2>
                        </div>
                        <div class="box-right">

                         <div style="overflow-y:scroll;overflow-x:hidden;height:238px">
                        <asp:UpdatePanel ID="Update_Panel" runat="server">
                              <ContentTemplate>
                              <asp:HiddenField runat="server" Value="" ID="Hidden_Field" />


                        <asp:DataList runat="server" ID="dl" ><ItemTemplate>
                            <div class="want-to-follow-list">
                                <div class="want-to-follow-list-left">
                                    <a href='<%#UrlGenerator(Eval("Id")) %>'>
                                   <asp:Image ID="Image1" class="profile-pic profile-pic2" runat="server" ImageUrl = '<%#Picture(Eval("PersonalLogoFileStoreId")) %>' Height="40" Width="40" /> </a>
                                </div>
                                <div class="want-to-follow-list-middle">
                                     <strong><a href='<%#UrlGenerator(Eval("Id")) %>' class="accounts-link"><asp:Label ID="Label1" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label> </a></strong>
                                 <p>
                             <asp:Label ID="Label2" runat="server" Text='<%#Eval("JobTitle") %>'></asp:Label></p>
                                </div>
                                <div class="want-to-follow-list-right">
                                       <div id="DivFollowing" runat="server" visible='<%#IsThisUserFollowingCompany(Eval("Id"))%>'>
                                                                <asp:LinkButton ID="linkButtonFollowing" Text="Following" CommandArgument='<%#Eval("Id")%>' OnCommand="CommandCompanyEmployeeUnFollowClick"
                                                                    runat="server" OnClientClick="return rowAction01();"  style="padding: 5px 7px; width: 51px; float: left;" class="invite-friend-btn"></asp:LinkButton>
                                                            </div>
                                                            <div id="DivFollow" runat="server" visible='<%#!IsThisUserFollowingCompany(Eval("Id"))%>'>
                                                                <asp:LinkButton ID="linkButtonFollow" OnCommand="CommandCompanyEmployeeFollowClick"
                                                                    Text="+Follow" CommandArgument='<%#Eval("Id")%>' runat="server"  OnClientClick="return rowAction01();"
                                                                    class="invite-friend-btn" style="padding: 5px 7px; width: 45px; float: left;"></asp:LinkButton>
                                                            </div>
                                                                   
                                </div>
                            
                            
                            </div> </ItemTemplate></asp:DataList>

                           <asp:Button runat="server" ID="btn_more" Text=">see more" OnClick="click_seemore" style="margin-left: 220px; color: #008CA1; border: #D5E2E2;" />
                               </ContentTemplate>
                             
                                    </asp:UpdatePanel></div>
                           
                        </div>
                        