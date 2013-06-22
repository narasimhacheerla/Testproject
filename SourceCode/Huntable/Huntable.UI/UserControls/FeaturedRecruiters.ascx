<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeaturedRecruiters.ascx.cs"
    Inherits="Huntable.UI.UserControls.FeaturedRecruiters" %>
<link href="../css/style-innerpage.css" type="text/css" rel="stylesheet" />

    <div style= "height:auto">
        <h3 class="featured-head" style="float: left;">
            Featured Recruiters</h3>
        <a href="featuredindustry.aspx" class="accounts-link get-feature">Get Featured Here</a></div>
    <div class="box-right">
        <div class="head-ash">
            <h3>
                <span id="companies" runat="server"></span>Recruiters Including</h3>
        </div>
        <div style="margin-top:15px;margin-left:20px">
<asp:DataList ID="dtlistFeatured" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
            Width="290px" >
            <ItemTemplate>
                <ul>
                    <li style="height:76px;">
                       <a href='<%#UrlGenerator(Eval("CompanyId")) %>' class="accounts-link"><asp:Image runat="server"  ID="btnCompany" Width="96" Height="56" ImageUrl='<%#Picture(Eval("CompanyLogoId"))%>' />
                      <h2 style="font-size: 13px; text-align: center;">
                    
                    (<%#Jobs(Eval("CompanyId"))%>)Jobs</h2></a><br />
                        <br />
                    </li>
                </ul>
            </ItemTemplate>
        </asp:DataList><a href= "seemorerecruiter.aspx" class="learn-more">See More Recruiter </a></div>
    </div>
