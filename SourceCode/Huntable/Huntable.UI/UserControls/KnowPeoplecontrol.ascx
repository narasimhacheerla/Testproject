<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KnowPeoplecontrol.ascx.cs" Inherits="Huntable.UI.UserControls.KnowPeoplecontrol" %>
  <link href="../Styles/NewStylesheet.css" rel="Stylesheet" type="text/css" />
  <table class="tableStyles1">
      <tr>
          <td colspan="4">
              <asp:Panel runat="server" ID="panGray" Width="100%" Height="24px" BackColor="gray" >
                  <asp:Label runat="server" ID="lblHeadingTop" Text="Friends to Invite" Font-Size="Larger"></asp:Label>
              </asp:Panel>
          </td>
      </tr>
      <tr>
          <td class="columnStyleuc"></td>
          <td class="style4">
              <asp:ImageButton runat="server" ID="img1" ImageUrl="~/image/imageUc.png"/>
          </td>
          <td class="style17">
              <p>
                  <b>Angela Malik</b>,Owner,Angela Malik Ltd
              </p>
              <asp:ImageButton runat="server" ID="img2" ImageUrl="./../image/imguc1.png" />
              <asp:LinkButton runat="server" ID="lnkInvite1" Text="Invite" CssClass="LinkButton" ForeColor="Blue"></asp:LinkButton>
          </td>
          <td valign="top">
              <asp:Label runat="server" ID="lblClose" Text="x"/>
          </td>
      </tr>
      <tr>
          <td></td>
          <td>
              <asp:ImageButton runat="server" ID="img3" ImageUrl="./../image/imguc2.png"/>
          </td>
          <td class="style1">
              <p>
                  <b>Tom Cummings</b>,Upnorth Group Field Sales Executive
              </p>
               <asp:ImageButton runat="server" ID="img4" ImageUrl="./../image/imguc1.png" />
              <asp:LinkButton runat="server" ID="lblInvite2" Text="Invite" CssClass="LinkButton" ForeColor="Blue"></asp:LinkButton>
          </td>
          <td>
               <asp:Label runat="server" ID="lblClose1" Text="x"/>
          </td>
      </tr>
      <tr>
          <td></td>
          <td>
              <asp:ImageButton runat="server" ID="img5" ImageUrl="./../image/imguc3.png"/>
          </td>
          <td class="style1">
              <p>
                 <b>Tony Devine</b>,Assistant Front of House Manager at Cameron House
              </p>
              <asp:ImageButton runat="server" ID="img6" ImageUrl="./../image/imguc1.png" />
              <asp:LinkButton runat="server" ID="lblInvite3" Text="Invite" CssClass="LinkButton" ForeColor="Blue"></asp:LinkButton>
          </td>
          <td>
              <asp:Label runat="server" ID="lblClose3" Text="x"/>
          </td>
      </tr>
  </table>