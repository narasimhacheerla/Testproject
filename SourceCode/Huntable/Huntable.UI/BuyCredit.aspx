<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BuyCredit.aspx.cs" Inherits="Huntable.UI.BuyCredit" %>

<%@ Register Src="~/UserControls/FeaturedRecruiters.ascx" TagPrefix="uc1" TagName="Recruiters" %>
<%@ Register Src="~/UserControls/YourAccountAtGlance.ascx" TagPrefix="uc2" TagName="accts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-section">
        <div id="content-inner">
            <div class="content-inner-left">
             <div id="credits" class="contacts-select" style="width: 615px;margin-bottom:10px;" runat="server">
                 <b style="font-size:14px;">10 Job postings for a new User,Companies & Recruiters</b>
                 <asp:Button runat="server" Text="Get It Free" style="background-color:brown;float:right;width:125px;height:28px;color:white;font-weight:bold;" OnClick="BtnGetItFreeClick"/>
             </div>           
                <div class="contacts-select" style="width: 615px;">
                    <div class="job-criteria-price">
                        <h2 class="margin-bot">
                            Job Packages</h2>
                            
                        <ul class="job-criteria-list">
                          
                            <li class="bg-ash">
                           
                            <b>
                           
                                <asp:RadioButtonList ID="rblCredits" DataTextField="DisplayText" DataValueField="DisplayValue"
                                    RepeatDirection="Vertical" runat="server" CellSpacing="28" CellPadding="15" RepeatLayout="UnorderedList"  Width="100%">
                                    
                                </asp:RadioButtonList>
                            </b>
                            
                            </li>
                            
                        </ul>
                      
                        <br />
                        <br />
                        <br />
                        <ul class="job-criteria-list">
                            <li class="bg-ash">* All Jobs are posted for 30 days in Huntable. All Purchases are
                                not transferable
                                <%--<h2 class="maring-tp">
                                    Target Your Audience - Promote Your Company</h2>--%>
                            </li>
                        </ul>
                        <h2 class="maring-tp">
                            Target Your Audience - Promote Your Company
                        </h2>
                        <div class="job-criteria-desc">
                            <div class="delete">
                                <b>Get Your Company Featured for only GBP £10.00</b>
                                <br />
                                <br />
                            </div>
                            <%--Get Your Company Featured For only<strong> £10.00 </strong>--%>&nbsp;&nbsp;<a
                                href="featuredindustry.aspx" class="accounts-link" style="float:left; margin-left:230px; width:100px;"> Find Out More...</a>
                                <asp:HiddenField ID="hdntemp" runat="server" Value="tt"/>
                            <asp:Button runat="server" ID="btnAdd" OnClick="BtnAddClick" Text="Add +" class="button-green"
                                Style="float: right;" />
                        </div>
                    </div>
                </div>
                <div class="payment-option">
                    <img src="images/payment-option.jpg" align="left" width="300" height="72" alt="Payment-option" /><br />
                    <br />
                    <br />
                 <div style="margin-right: 159px;
margin-top: -14px;">   <asp:LinkButton ID="btnContinue"  OnClick="BtnContinueClick" runat="server"
                CausesValidation="false" CssClass="button-orange button-orange-search">Buy now<img src="images/search-arrow.png" width="22" height="23" alt="arrow" /></asp:LinkButton></div>
                 <%--   
                    <asp:Button runat="server" ID="btnContinue" Text="Continue" CssClass="button-orange button-orange-job"
                        OnClick="BtnContinueClick" />--%>
                </div>
            </div>
            <div class="content-inner-right">
                <div class="box-right box-right-promote">
                    <h2>
                        Promote Your Company by targeted ad's</h2>
                    Industry, Skill, Interest, Country<br />
                    <br />
                    <a href="FeaturedIndustry.aspx" class="accounts-link">Find out more...</a>
                    <img class="promot-img" src="images/promote-arrow.png" width="29" height="47" alt="arrow" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div class="box-right">
                    <uc2:accts ID="acctsGlance" runat="server" />
                </div>
                <p class="margin-top-visible">
                    &nbsp;</p>
                <div >
                    <uc1:Recruiters ID="featuredrecruiters" runat="server" />
                </div>
            </div>
        </div>
    </div>
     <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
        type="text/javascript"></script>
    <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
</asp:Content>
