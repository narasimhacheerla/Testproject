<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewersOfCompanyAlso.ascx.cs"
    Inherits="Huntable.UI.UserControls.ViewersOfCompanyAlso" %>
  <link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />
   <div class="block-border-right">
                    <div style="margin-top: -219;">
                    <div class="box-right" id="div_viwers" runat="server" >
                <div class="head-ash" style="text-align:center;">
               	  <h3>Viewers also viewed</h3>
                  </div>
                  <div style="overflow-y:scroll;overflow-x:hidden;height:238px">
                     <asp:UpdatePanel ID="Update_Panel" runat="server">
                              <ContentTemplate>
                              <asp:HiddenField runat="server" Value="" ID="Hidden_Field" />
                        <asp:DataList runat="server" ID="dlview" RepeatDirection="Horizontal" RepeatColumns="2">
                             <ItemTemplate>
                             <a href = '<%#UrlGenerator(Eval("Id")) %>' runat="server">
                             <asp:Image runat="server" ID="image1" Width="128px" Height="74px" ImageUrl='<%#Picture(Eval("CompanyLogoId")) %>'  /></a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                             </ItemTemplate>
                             
                             </asp:DataList>
                             <asp:Button runat="server" ID="btn_more" Text=">see more" OnClick="click_seemore" style="margin-left: 233px; color: #008CA1; border: #D5E2E2;" />
                               </ContentTemplate>
                                    </asp:UpdatePanel>
                    </div><asp:Label runat="server" ID="lblMessage"></asp:Label>
                    </div></div>
                    <%--    <div class="box-right" style="height: 284px;margin-top: -219px;">
                        <div class="head-ash">
                            <h3>
                                Viewers also Viewed</h3>
                        </div>
                        <div class="list_carousel">
                            <ul id="foo2">
                                <asp:DataList ID="dlCompanyViewers" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                    <ItemTemplate>
                                        <li style="width: 134px; height: 78px;" class="img-left1"><a href="#">
                                            <asp:ImageButton class="profile-pic" ImageUrl='<%#Picture(Eval("CompanyLogoId"))%>' runat="server"
                                                ID="companyimgs" Width="124" Height="70" alt="Feaured-logo" /></a> </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                       <%-- <asp:Label ID="lblEmptyCompanyViewers" Text="No videos found for this company" runat="server"
                                            Visible='<%#bool.Parse((dlCompanyViewers.Items.Count==0).ToString())%>'>
                                        </asp:Label>--%>
                    <%--<div class="clearfix">
                            </div>
                            <div class="pager-main">
                                <a id="prev2" class="prev" href="#">
                                    <img src="images/prev.png" width="17" height="22" alt="previous" /></a>
                                <div class="pager" id="pager2" style="display: block;">
                                </div>
                                <a id="next2" class="next" href="#">
                                    <div style="margin-left: 255px; margin-top: -20px;">
                                        <img src="images/next.png" width="17" height="22" alt="Next" /></div>
                                </a>
                            </div>--%>
                    
                </div>