<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="atozsearch.ascx.cs"
    Inherits="Huntable.UI.UserControls.Atozsearch" %>
<%@ Register Src="ItemDisplayTemplate.ascx" TagName="ItemDisplay" TagPrefix="uc" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
<link href="../css1/jquery-ui-1.8.23.custom.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
  <script type="text/javascript">
      function rowAction2(calling) {
          $('#confirmation').dialog({
              autoOpen: true,
              modal: true,
              width: 600,              
              buttons: {
                  "Save": function (e) {
                      if (calling == 'test') {<%= Page.ClientScript.GetPostBackEventReference(this.btnUpdate, string.Empty) %>;
                          
                      }
                     
                      $(this).dialog("close");
                     
                      
                  },
                  "Cancel": function() {

                      $(this).dialog("close");
                  }
              }
          });
          return false;
      }     
      function callnextbutton(pagename) {
          $('#<%= HiddenField1.ClientID %>').val(pagename);
          <%= Page.ClientScript.GetPostBackEventReference(this.btnNext, string.Empty) %>;
             
        
      }
 </script>
<div>
    <asp:Label ID="lblmessage" Visible="False" runat="server" Font-Bold="True"></asp:Label>
</div>
<div class="profile-search">
    <b>
        <asp:Label ID="lblSearchTitle" Text="Search by Name" runat="server" />
    </b>
    <asp:TextBox runat="server" class="textbox-search textbox-search-inner textbox-search-customize"
        ID="txtSearch" />
    <asp:LinkButton ID="lnksrch" runat="server" class="button-orange button-orange-search">Search<img src="../images/search-arrow.png"  onclick="OnSearchContent"
                        width="22" height="23" alt="arrow" /></asp:LinkButton>
</div>
<div class="company-name-list" style="margin-top: 20px;">
    <h3>
        Alphabetical List</h3>
    <div class="alphabet">
        <asp:ListView ID="lvLetters" runat="server" OnItemDataBound="LettersItemDataBound">
            <ItemTemplate>
                <asp:LinkButton ID="lnkLetter" runat="server" CommandName="Filter" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Letter")%>'
                    OnCommand="LettersItemCommand" Font-Size="8px">
                         <%# DataBinder.Eval(Container, "DataItem.Letter")%>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:ListView>
    </div>
    <div id="Industry" class="industry-list industry-list-customize">
        <asp:ListView ID="lvItems" runat="server" GroupPlaceholderID="groupPlaceHolder" ItemPlaceholderID="itemPlaceHolder"
            GroupItemCount="3" OnItemDataBound="ItemsItemDataBound">
            <LayoutTemplate>
                <table>
                    <tr>
                        <td>
                            <table>
                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder"></asp:PlaceHolder>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                </tr>
            </GroupTemplate>
            <ItemTemplate>
                <td>
                    <asp:HiddenField ID="hfKey" runat="server" />
                    <uc:ItemDisplay runat="server" ID="ucItemDisplay" ClientIDMode="Static" />
                </td>
            </ItemTemplate>
        </asp:ListView>
    </div>
</div>
<asp:Button ID="btnUpdate" CssClass="button-orange" Style="float: right; margin-left: 100px;"
    Text="Save" runat="server" OnClick="OnUpdate" />
    <asp:HiddenField runat="server" ID="hdnsave" Value="tt"/>
    <asp:HiddenField runat="server" ID="HiddenField1" />
     <asp:Button ID="btnNext" runat="server" style="margin-top:40px ;margin-right:-144px" class="button-green floatright" Text="Next" OnClick="BtnNextClick" /> 
     <div id="confirmation" style="display:none;">
                <asp:Label ID="Label1" runat="server" Text="Would you like to save your preferences?" ></asp:Label>
                </div> 
<p class="margin-top-visible">
    &nbsp;</p>
