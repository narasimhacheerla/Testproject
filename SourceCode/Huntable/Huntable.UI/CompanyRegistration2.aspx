   
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CompanyRegistration2.aspx.cs" Inherits="Huntable.UI.CompanyRegistration2" %>
   
    
    
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Huntable - The Professional Network</title>
        <meta name="description" content="Huntable - The Professional Network" />
        <meta name="keywords" content="The Professional Network" />
        <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
        <link rel="stylesheet" href="http://code.jquery.com/ui/1.9.1/themes/base/jquery-ui.css" />
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
        <script src="http://code.jquery.com/ui/1.9.1/jquery-ui.js"></script>
        <link rel="stylesheet" href="/resources/demos/style.css" />
        <script type="text/javascript">
            function overlay(id) {
                el = document.getElementById('ovrly');
                $('#ovrly').show();
                
                $('#<%= pbl.ClientID %>').text(id);

            }


               
            $(document).ready(function () {

                $('#ximg').click(function () {
                    $('#ovrly').hide();
                    return false;
                });
            });
    </script>
         <script type="text/javascript">
            $(function () {
                
                $("#tabs").tabs();
            });
        </script>
        
    </head>
    <body>
        <!-- Header section ends -->
        <!--[if lte IE 6]>
<script type="text/javascript" >
//author: ?, I found this useful.
startList = function() {
if (document.all&&document.getElementById) {
navRoot = document.getElementById("nav");
for (i=0; i<navRoot.childNodes.length; i++) {
node = navRoot.childNodes[i];
if (node.nodeName=="LI") {
node.onmouseover=function() {
this.className+=" over";
  }
  node.onmouseout=function() {
  this.className=this.className.replace(" over", "");
   }
   }
  }
 }
}
window.onload=startList;

</script>
<![endif]-->
        <!-- main menu ends -->
         <div   id="ovrly"  style="height:30px;width:100%;z-index:2000; background-color:#FF9242;border:1px solid #FF9242;border-radius:2px;margin-top: 10px;display:none;">
      <div style ="width:980px;padding: 0px 10px;margin: 0px auto;">
    
      <div style="width: 30px;margin-left: 292px;">
      <image   src="images/tick.png" width="25px"  height="25px"></image></div>
      <div style="margin-top:-20px" >
      <asp:Label style="color:White;width: 200px;margin-top:-40px;margin-left: 377px;" ID="pbl" runat="server"></asp:Label></div>
   <%--  <h5  style="color:White;width: 200px;margin-top:-20px;margin-top: -20px;margin-left: 377px;">product added sucessfully</h5> --%>
     <div  style="margin-left: 621px;;margin-top:-13px"><image id="ximg"  src="images/orange-check-mark-md.png" width="10px" height="10px"></image></div></div> </div>
        <div id="content-section">
            <div id="content-inner">
                <div class="top-breadcrumb">
                    <div class="accounts-profile2-left">
                        <a class="accounts-link" href="#">Company</a>&nbsp;››&nbsp;<strong><asp:Label runat="server" ID="lblCompName" ></asp:Label></strong></div>
                </div>
                <div class="user-regis">
                    <div class="user-regis-left">
                        <div align="center" class="user-regis-left-img">
                            <a href="#" class="logo-like"></a><a href="#">
                                <asp:image id="imgCompany" runat="server" width="314" height="163" />
                               <%-- <img src="images/what-like-img1.jpg"  alt="what-like" />--%>
                                                              </a>
                        </div>
                        <div class="user-regis-left-ut">
                            <a href="#" class="accounts-link">
                               
                                <img src="images/edit-icon.jpg" style="vertical-align: middle;" width="23" height="23"
                                    alt="edit" align="top" />Edit
                            </a>
                            <br />
                            <asp:FileUpload runat="server" ID="fuCompanyImage" /><br />
                            Supported file types: GIF,JPG,PNG
                        </div>
                        <asp:Button runat="server" class="button-green button-green-sa" Text="Save" ID="btnsaveUpper"
                            OnClick="BtnsaveUpperClick" />
                    </div>
                    <div class="user-regis-right">
                        <table class="reg-table">
                            <tr>
                                <td valign="top">
                                    <asp:TextBox ID="txtCompanyName" runat="server" onblur="if (this.value == '') {this.value ='e.g: Your company name';}"
                                        onfocus="if (this.value =='e.g: Your company name') {this.value ='';}" value="e.g: Your company name"
                                        class="textbox textbox-ut" /><a href="#"><img src="images/edit-icon.jpg" width="23"
                                            height="23" alt="edit" align="top" /></a>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                        ControlToValidate="txtCompanyName" ForeColor="Red">*</asp:RequiredFieldValidator> 
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:TextBox ID="txtCompanyHeading" MaxLength="200" runat="server" onblur="if (this.value == '') {this.value ='e.g: Brief heading about your company (max 200 characters)';}"
                                        onfocus="if (this.value =='e.g: Brief heading about your company (max 200 characters)') {this.value ='';}"
                                        value="e.g: Brief heading about your company (max 200 characters)" class="textbox textbox-ut" /><a href="#"><img
                                            src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top" /></a>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="txtCompanyHeading" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    
                                </td>
                            </tr>
                            <tr>
                               <%-- <td valign="top">
                                    <asp:TextBox ID="txtCompanyDescription" class="textbox textbox-ut" onfocus="if(this.value==this.defaultValue)this.value='';"
                                        onblur="if(this.value=='')this.value=this.defaultValue;" runat="server">Now lets hear about your business, short &amp; sweet... (max 300 characters)</asp:TextBox>
                                    <a href="#">
                                        <img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top" /></a>
                                    <asp:RequiredFieldValidator ID="RFV_3" runat="server" 
                                        ControlToValidate="txtCompanyDescription" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>--%>
                                 <td valign="top">
                                    <asp:TextBox ID="txtCompanyDescription"  MaxLength="200" runat="server"  onfocus="if(this.value=='Now lets hear about your business, short &amp; sweet... (max 300 characters)'){this.value='';}"
                                        onblur="if(this.value==''){this.value='Now lets hear about your business, short &amp; sweet... (max 300 characters)';}" value="Now lets hear about your business, short &amp; sweet... (max 300 characters)" class="textbox textbox-ut"></asp:TextBox>
                                    <a href="#">
                                        <img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top" /></a>
                                    <asp:RequiredFieldValidator ID="RFV_3" runat="server" 
                                        ControlToValidate="txtCompanyDescription" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        
                                </td>
                            </tr>
                        </table>
                        <table class="reg-table">
                            <tr>
                                <td colspan="2">
                                    <strong style="font-size: 18px;">Business Info</strong>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td width="49%" height="29">
                                    <asp:DropDownList runat="server" ID="ddlIndustry" CssClass="textbox textbox-ut2" />
                                     <%--<sup class="red-color red-color1">*&nbsp;</sup>--%>
                                     <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlIndustry" ErrorMessage="*" Text="*" ForeColor="Red" InitialValue="155"></asp:RequiredFieldValidator>
                                </td>
                                <td width="51%">
                                    <asp:DropDownList runat="server" ID="ddlEmployess" CssClass="textbox textbox-ut2" />
                                    <%--<sup class="red-color red-color1">*&nbsp;</sup>--%>
                                    
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:TextBox ID="txtCompanyWebsite" runat="server" onblur="if (this.value == '') {this.value ='website address www.companyname.com';}"
                                        onfocus="if (this.value =='website address www.companyname.com') {this.value ='';}"
                                        value="website address www.companyname.com" class="textbox textbox-ut1" /><a href="#"><img
                                            src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top" /></a>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                        ControlToValidate="txtCompanyWebsite" ForeColor="Red">*</asp:RequiredFieldValidator>
                                
                                </td>
                                <td>
                                    <asp:TextBox ID="txtaddress1" runat="server" onblur="if (this.value == '') {this.value ='address';}"
                                        onfocus="if (this.value =='address') {this.value ='';}" value="address" class="textbox textbox-ut1" /><img
                                            src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top" />
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                        ControlToValidate="txtaddress1" ForeColor="Red">*</asp:RequiredFieldValidator>
                                
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:TextBox ID="txtPhoneNum" runat="server" onblur="if (this.value == '') {this.value ='Phone no: eg 0044 2000 67 456';}"
                                        onfocus="if (this.value =='Phone no: eg 0044 2000 67 456') {this.value ='';}"
                                        value="Phone no: eg 0044 2000 67 456" class="textbox textbox-ut1" /><a href="#"><img
                                            src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top" /></a>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                        ControlToValidate="txtPhoneNum" InitialValue="Phone no: eg 0044 2000 67 456" ForeColor="Red">*</asp:RequiredFieldValidator>
                                
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddress2" runat="server" onblur="if (this.value == '') {this.value ='address';}"
                                        onfocus="if (this.value =='address') {this.value ='';}" value="address" class="textbox textbox-ut1" /><a
                                            href="#"><img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top" /></a>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="txtAddress2" ForeColor="Red">*</asp:RequiredFieldValidator>
                                
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:TextBox ID="txtEmailAddress" runat="server" onblur="if (this.value == '') {this.value ='Alternate email address';}"
                                        onfocus="if (this.value =='Alternate email address') {this.value ='';}" value="Alternate email address"
                                        class="textbox textbox-ut1" /><a href="#"><img src="images/edit-icon.jpg" width="23"
                                            height="23" alt="edit" align="top" /></a> 
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                        ControlToValidate="txtEmailAddress" ForeColor="Red">*</asp:RequiredFieldValidator>
                                
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTownCity" runat="server" onblur="if (this.value == '') {this.value ='Town/ciy';}"
                                        onfocus="if (this.value =='Town/ciy') {this.value ='';}" value="Town/ciy" class="textbox textbox-ut1" /><a
                                            href="#"><img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top" /></a>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                        ControlToValidate="txtTownCity" ForeColor="Red">*</asp:RequiredFieldValidator>
                                
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPostCode" runat="server" onblur="if (this.value == '') {this.value ='Post code';}"
                                        onfocus="if (this.value =='Post code') {this.value ='';}" value="Post code" class="textbox textbox-ut1" /><a
                                            href="#"><img src="images/edit-icon.jpg" width="23" height="23" alt="edit" align="top" /></a>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                        ControlToValidate="txtPostCode" ForeColor="Red">*</asp:RequiredFieldValidator>
                                
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:DropDownList runat="server" ID="ddlcountry" CssClass="textbox textbox-ut2" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                        ControlToValidate="ddlcountry" ForeColor="Red" ErrorMessage="*" Text="*" InitialValue="1"></asp:RequiredFieldValidator>
                                
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
              
                        <div class="user-add">
                            <ul class="overview-tab">
                            <li>&nbsp<asp:Button runat="server" ID="btnOverview"  CssClass="sel_tab btnborder" Text="Overview" Width="70px" OnClick="btnOverview_Click" /></li>
                                <li><asp:Button runat="server" ID="btnservices" Text="Products & Services" 
                                         OnClick="btnservices_Click"  CssClass="btnborder"  width="133px" /></li>
                                <li>&nbsp<asp:Button runat="server" ID="btncarrer" Text="Careers" width="68px"
                                        CssClass="selected-tab btnborder" onclick="btncarrer_Click" /> </li>
                                <li><asp:Button runat="server" ID="btnBusiness" Text="Business Blog"  OnClick="btnBusiness_Click"  width="113px" CssClass="btnborder"/></li>
                                <li><asp:Button runat="server" ID="btnarticle" Text="Article" OnClick="btnarticle_Click" Width="67px" CssClass="btnborder"/></li>
                            </ul>
                            <div class="user-add-left">
                                <h3>
                                    Your Portfolio</h3>
                                <div class="prot-list">
                                <div style="float: left; width: 145px;">
                                    
                                        <%--<a href="#" class="logo-like logo-like1">--%>
                                        <asp:Image runat="server" ID="portfolioimg"   width="125" height="125" alt="No Image Available" /><%--</a>--%><strong></strong>
                                           <img id="portfolioId" runat="server"  src="images/no-image1.jpg" visible="false" width="125" height="125" alt="no-image" />
                                           
                                            <%--<asp:image ID="portfolioimg" Visible="False"  runat="server"/>--%>
                                        <%--<img id="portfoliold" runat="server" src="images/no-image1.jpg" class="profile-pic" width="125" height="125" alt="no-image" />--%><br />
                                        <asp:FileUpload runat="server" ID="fpPortfolio" /><br />
                                         <a href="#" class="accounts-link accounts-link-ut">
                                    <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="images/add-icon.png"/>                                    
                                     <div style="margin-top:12px; margin-left: -12px;"><asp:LinkButton runat="server" 
                                             Text="Add" ID="btnAdd" Width="37px" onclick="btnAdd_Click" /></div>
                                </a>
                                    </div>
                                    <p class="text-port" style="width: 73%;">
                                        <asp:TextBox ID="txtPortfolioDescription"  runat="server" class="textbox textbox-ut textbox-ut-port" onblur="if (this.value == '') {this.value ='Say something about this portfolio';}"
                                        onfocus="if (this.value =='Say something about this portfolio') {this.value ='';}" value="Say something about this portfolio" TextMode="MultiLine"  />
                                         </p> <a href="#">
                                                <img src="images/edit-icon-new.png" align="top" width="23" height="23" alt="edit" /></a><br />
                                        
                                        <p style="margin-top: -15px;">
                                            <sup class="red-color red-color1" style="margin-left:23px";>*&nbsp;</sup>Your latest portfolio will be shown
                                            first</p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                    
                                </div>
                                <div class="block-margin">
                                    <asp:Button runat="server" ID="btnsave" Text="Save" class="button-blue button-blue-sk "
                                        OnClick="BtnsaveClick" />
                                    <asp:LinkButton runat="server" OnClick="BtnNextClick" ID="lnkbtnNext"></asp:LinkButton>
                                 <asp:Button runat="server" ID="btn_skip" Text="Skip" class="_btn_skip"  OnClick="btn_skip_Click" />
                                </div>
                            </div>
                            <div class="user-add-right">
                                <h3>
                                    Your Video</h3>
                                <p class="block-margin">
                                    Share a link to a You tube,Vimeo,DailyMotion, or Google video about your company
                                </p>
                                <asp:TextBox ID="txtVideoUrl"  runat="server" onblur="if (this.value == '') {this.value ='Video url';}"
                                    onfocus="if (this.value =='Video url') {this.value ='';}" value="Video url" class="textbox1" />
                                <br />
                                <a href="#" class="accounts-link accounts-link-ut" style="margin-right:0px";>

                                    <asp:ImageButton ID="ImageButton2" runat="server"  ImageUrl="images/add-icon.png"/>                                    
                                    <div style="margin-top:12px; margin-left: -12px;"><asp:LinkButton runat="server" 
                                             Text="Add" ID="LinkButton1" Width="37px" onclick="BtnVideoAddClick" /></div>
                                </a>

                                <div class="block-margin2">
                                   
                                    <iframe ID="ifrmVideo"  style="background:transparent url('images/index.jpg');background-position:center; background-repeat: no-repeat; margin-top:-50px" runat="server" />
                                    
                                </div>
                            </div>
                        </div>
                        <div>
                            <asp:Button runat="server" class="button-green button-green-port" Text="Next" ID="OverViewNextClick"
                                OnClick="OverViewNextClickClick"  />
                        </div>
                        <a id="1"></a>
                        <asp:Panel Visible="False" ID="pnlproductsservices" runat="server">
                            <div class="user-regis user-regis-margin">
                                <div class="user-regis-left">
                                    <div align="center" class="user-regis-left-img">
                                        <a href="#">
                                            <img id="ProductImage" runat="server" src="images/what-like-img2.jpg" visible="false" width="314" height="163" alt="what-like" />
                                            <asp:Image runat="server"  ID="imgproduct" width="314" height="163" /></a>
                                    </div>
                                    <div class="user-regis-left-ut">
                                        <a href="#" class="accounts-link">
                                            <img src="images/edit-icon.jpg" style="vertical-align: middle;" width="23" height="23"
                                                alt="edit" align="top" />Edit</a>
                                        <br />
                                        <asp:FileUpload runat="server" ID="Fproduct" /><br />
                                        Supported file types: GIF,JPG,PNG
                                    </div>
                                  
                                </div>
                                <div class="user-regis-right">
                                    <table class="reg-table">
                                        <tr>
                                            <td>
                                                <h3>
                                                    <sup class="red-color red-color1">*&nbsp;</sup> Product Description</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                
                                                    <asp:TextBox ID="txtProductDescriptipon"  runat="server" class="textbox textbox-ut textbox-ut-port" onblur="if (this.value == '') {this.value ='Say something about this portfolio';}"
                                        onfocus="if (this.value =='Say something about this portfolio') {this.value ='';}" value="Say something about this portfolio" TextMode="MultiLine"  />
                                            </td>
                                            <td ><div style="margin-right:80px"><asp:Button runat="server" ID="btnCompanyPorducts" CssClass="button-green button-green-sa"
                                        Text="Save" OnClick="BtnCompanyPorductsClick" /></div></td>
                                        </tr>
                                       
                                    </table>
                                </div>
                                <div class="rgis-bot">
                                    <h3>
                                        Careers:</h3>
                                    <table class="tab-care">
                                        <tr>
                                            <td width="222">
                                                Your company career page web link
                                            </td>
                                            <td width="12">
                                                :
                                            </td>
                                            <td width="240">
                                                <asp:TextBox runat="server" class="textbox1" ID="txtComapnyCarrer"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Your Company Blog address
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" class="textbox1" ID="txtCompanyBlog"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button runat="server" class="button-green floatright" Text="Save" ID="btnCompanycarrersave"
                                                    OnClick="BtnCompanycarrersaveClick" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <a  style = "position:relative;top:4px" href="#" class="accounts-link">Skip</a>
                                            </td>
                                        </tr>
                                       
                                    </table>
                                </div>
                                  <div class="rgis-bot">
                                    <h3>
                                        Article:</h3>
                                    <table class="tab-care">
                                        <tr>
                                            <td width="222">
                                                Article Title
                                            </td>
                                            <td width="12">
                                                :
                                            </td>
                                            <td width="240">
                                                <asp:TextBox runat="server" class="textbox1" ID="txtArticleTitle"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Article Description
                                            </td>
                                            <td>
                                                :
                                            </td>
                                            <td>
                                               
                                                <asp:TextBox ID="txtArticleDescription"  runat="server" class="textbox textbox-ut textbox-ut-port" onblur="if (this.value == '') {this.value ='Say something about this portfolio';}"
                                        onfocus="if (this.value =='Say something about this portfolio') {this.value ='';}" value="Say something about this portfolio" TextMode="MultiLine"  />
                                       
                                            </td>
                                            <td>
                                                <asp:Button runat="server" class="button-green floatright" Text="Save" 
                                                    ID="btnArticleSave" onclick="BtnArticleSaveClick" 
                                                   />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <a style = "position:relative;top:4px" href="#" class="accounts-link">Skip</a>
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </div>
                                <div class="rgis-bot">
                                    <h3>
                                        Settings:</h3>
                                    <ul class="set-list">
                                        <li>
                                            <h3>
                                                Invite your employees to join your company page</h3>
                                        </li>
                                        <li>
                                            <div class="bord">
                                                <p>
                                                    <strong>Import your contacts from a CSV file</strong></p>
                                                <a href="#?w=500" class="button-orange floatleft poplight" rel="popup9" style="float:right; margin-top:11px;">Import </a>
                                                <strong style="float: right; margin: 7px 10px 0px 0px;">
                                                    <img src="images/excel-icon.png" width="30" height="32" alt="Excel-icon" /></strong>
                                            </div>
                                        </li>
                                        <li>
                                            <%--<asp:Button runat="server" class="button-green floatright" Style="margin-right: 160px;"
                                                ID="BTnInviteFriendsClick" Text="Save" OnClick="BTnInviteFriendsClickClick" />--%>
                                        </li>
                                    </ul>
                                    <ul class="set-list set-list1" style="border-left: white;">
                                        <li>
                                            <h3>
                                                Enter your employees email address to invite them to join your company</h3>
                                        </li>
                                        <li>
                                            <asp:TextBox runat="server" ID="txtMailIDs" class="textbox1 floatleft"></asp:TextBox>
                                          
                                                <asp:Button ID="Button1" ValidationGroup="invitefriends" Text="Send Invite" runat="server"
                            CssClass="button-orange floatleft" rel="popup2" Style="margin-left: 10px;"
                            OnClick="BtnInviteByEmailAddressesClick" />
                                        </li>
                                        <li>
                                        <br />
                                        You can invite by exporting their email address
                                        </li>
                                       
                                    </ul>
                                    
                                    <div class="social-icon" style="padding-left: 0px; margin-left: 28px;">
                            <a href="#" title="Facebook">
                                <asp:ImageButton ID="ibtnFacebook" runat="server" Width="34" Height="34" alt="Facebook"
                                    ImageUrl="images/facebook.jpg" OnClick="IbtnFacebookClick" CausesValidation="false" />
                            </a><a href="#" title="Linked in">
                                <asp:ImageButton ID="ibtnLinkedIn" runat="server" ImageUrl="images/linkedin.jpg"
                                    Width="34" Height="34" alt="Linkedin" OnClick="IbtnLinkedInClick" CausesValidation="false" />
                            </a><a href="#" title="Twitter">
                                <asp:ImageButton ID="ibtnTwitter" runat="server" ImageUrl="images/twitter.jpg" Width="34"
                                    Height="34" alt="Gmail" OnClick="IbtnTwitterClick" CausesValidation="false" />
                            </a><a href="#" title="Gmail">
                                <asp:ImageButton ID="ibtnGoogle" runat="server" ImageUrl="images/gmail.jpg" Width="34"
                                    Height="34" alt="Gmail" OnClick="IbtnGoogleClick" CausesValidation="false" /></a>
                            <a href="#" title="yahoo">
                                <asp:ImageButton ID="ibtnYahoo" runat="server" Width="34" Height="34" alt="yahoo"
                                    ImageUrl="images/yahoo.png" OnClick="IbtnYahooClick" CausesValidation="false" /></a>
                            <a href="#" title="Msn">
                                <asp:ImageButton ID="ibtnLive" runat="server" Width="34" Height="34" alt="Msn" ImageUrl="images/msn.jpg"
                                    OnClick="IbtnLiveClick" CausesValidation="false" /></a>
                        </div>
                                </div>
                            </div>
                        </asp:Panel>
                   
                <!-- content inner ends -->
            </div></div>

    <div id="popup9" class="popup_block">
        <div class="apply-job">
            <h2>
                Upload .csv file only in the format shown here</h2>
            <asp:FileUpload runat="server" ID="fuInvitationFriends" class="textbox float-block" /><br />
            <br />
            <asp:Button runat="server" CssClass="button-orange floatleft" ID="btnUpload" CausesValidation="false"
                Text="Upload File" OnClick="UploadInvites" />
            <div style="float: left; clear: both; margin-top: 20px; width: 100%;">
                <a href="#" class="learn-more" style="margin-right: 0px; font-size: 11px; margin-top: -4px;">
                    Cant't find your social network. Click here to see more</a>
            </div>
        </div>
    </div>

            <!-- content section ends -->
            <!-- Range Slider Script Begins -->
            <!--<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>-->
            <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"
                type="text/javascript"></script>
            <script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
            <script type="text/javascript" src="js/flipcounter.js"></script>
            <!-- Range Slider Script Ends -->
            <!-- Footer section ends -->


             <!-- Popup Script Begins -->
       <script type="text/javascript">
           $(function () {
               $("#slider").slider({
                   range: "min",
                   value: 200,
                   min: 1,
                   max: 700,
                   slide: function (event, ui) {
                       $("#amountin").val(ui.value);
                       $("#amountin1").val("$" + ui.value * 5);
                       $("#slider").children("div").css("background", "#D77D00");
                   }

               });
               $("#amountin").val("$" + $("#slider").slider("value"));
               $("#amountin1").val("$" + $("#slider").slider("value"));
           });
           $("a.expand").click(function () {
               $(this).parent().children(".toggle").slideToggle(200);
               return false;
           });
    </script>  
    <script type="text/javascript">
        $(document).ready(function () {

            //When you click on a link with class of poplight and the href starts with a # 
            $('a.poplight[href^=#]').click(function () {
                var popID = $(this).attr('rel'); //Get Popup Name
                var popURL = $(this).attr('href'); //Get Popup href to define size

                //Pull Query & Variables from href URL
                var query = popURL.split('?');
                var dim = query[1].split('&');
                var popWidth = dim[0].split('=')[1]; //Gets the first query string value

                //Fade in the Popup and add close button
                $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="images/close_pop.png" class="btn_close" title="Close Window" alt="Close" /></a>');

                //Define margin for center alignment (vertical + horizontal) - we add 80 to the height/width to accomodate for the padding + border width defined in the css
                var popMargTop = ($('#' + popID).height() + 80) / 2;
                var popMargLeft = ($('#' + popID).width() + 80) / 2;

                //Apply Margin to Popup
                $('#' + popID).css({
                    'margin-top': -popMargTop,
                    'margin-left': -popMargLeft
                });

                //Fade in Background
                $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.
                $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); //Fade in the fade layer 

                return false;
            });


            //Close Popups and Fade Layer
            $('a.close, #fade').live('click', function () { //When clicking on the close or fade layer...
                $('#fade , .popup_block').fadeOut(function () {
                    $('#fade, a.close').remove();
                }); //fade them both out

                return false;
            });


        });

    </script>
    </body>
    </html>
    </div> </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
</asp:Content>
