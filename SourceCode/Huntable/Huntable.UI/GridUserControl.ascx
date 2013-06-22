<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridUserControl.ascx.cs"
    Inherits="Huntable.UI.GridUserControl" %>
<asp:UpdatePanel ID="upFeeds" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:GridView ID="gvDetails" runat="server" Width="100%" GridLines="None" AllowPaging="false"
            ShowFooter="false" AutoGenerateColumns="false" ShowHeader="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <img src='<%# Eval("ProfilePictureDisplayPath") %>' runat="server" alt="" id="imgProfilePicture"
                            height="81" width="76" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%# Eval("CreatedDateTime")%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <%# Eval("FeedDesription")%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
