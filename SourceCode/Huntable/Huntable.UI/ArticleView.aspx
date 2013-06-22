<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArticleView.aspx.cs" Inherits="Huntable.UI.ArticleView" %>
<%@ Register Src="~/UserControls/Followers(6).ascx" TagName="flw" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/PortfolioImages.ascx" TagName="portfolio" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/Messagepopup.ascx" TagName="message" TagPrefix="uc3" %>
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
    
    <!-- portfolio script style begins -->
    <link href="css/portfolio-js-style.css" type="text/css" rel="stylesheet" />    
    <!-- portfolio script style ends -->
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
     <asp:HiddenField ID="hdnUserId" runat="server" />
 <div id="dialog" title="Confirm Message">
    <asp:Label runat="server" Text="You are not logged In.Please login first." ID="lblConfirmMessage"></asp:Label>
    </div>
<div id="content-section">
      <div id="content-inner">
      
      <div class="accounts-profile2">
       <div class="top-breadcrumb">
      <div class="accounts-profile2-left"> 
      <a href="companieshome.aspx" class="accounts-link">Company</a>&nbsp;&rsaquo;&rsaquo;&nbsp;<a id="a_comp_name" runat="server" class="accounts-link"><asp:Label runat="server" ID="lbl_comp_name" ></asp:Label></a>&nbsp;&rsaquo;&rsaquo;&nbsp;<strong>Article view</strong></div>
      
      </div>
      
      
      
      <ul class="overview-tab">                
                <li><a id="a_overview" runat="server"  >Overview</a></li>
                <li><a id="a_Activity" runat="server"  >Activity</a></li>
                <li><a id="a_Products" runat="server" >Products &amp; Services</a></li>
                <li><a id="a_careers" runat="server"  >Careers</a></li>
                <li><a id="a_Business" runat="server"  >Business Blog</a></li>
                <li><a id="a_Article" runat="server" class="selected-tab">Articles</a></li>
               <uc3:message runat="server" />
                   
              </ul>
      
      </div>
      
      
      
    <div class="content-inner-left">
       <div class="article-view">
       	
       <asp:Label runat="server" ID="articledesc" runat="server" ></asp:Label>
       </div>\
          
        </div>
    <div class="content-inner-right">
    
 
      
      
      
       <div class="blue-box-company" runat="server" id="div_follow" visible="false">
                                <asp:Button ID="btn_follow" CssClass="button-orange button-orange-fc" Style="margin-left: 85px;"
                                    Text="Follow" OnClientClick="return rowAction01();"  runat="server" OnClick="Follow" />
                                <br />
                                <br />
                             <p>
                               <asp:Label ID="Label1" style="margin-left: 5px;" runat="server"></asp:Label> and get upto date info about this company and get their jobs straight into your
                                    feeds
                               </p>    
                                <div class="button-green-company" align="center">
                                </div>
                            </div>
                            <div class="blue-box-company" runat="server" id="div_following" visible="false">
                                <br />
                                <br />
                                <asp:Button ID="btn_following" CssClass="button-orange button-orange-fc" Style="margin-left: 85px;"
                                    Text="Following" OnClientClick="return rowAction01();"  runat="server" OnClick="Following" /><br />
                                <br />
                                <br />
                                <p>
                                <asp:Label ID="Label2" style="margin-left: 5px;" runat="server"></asp:Label>
                                and get upto date info about this company and get their jobs straight into your
                                feeds
                                </p>
                                <div class="button-green-company" align="center">
                                </div>
                            </div>
      
                <p class="margin-top-visible">&nbsp;</p>
        <div style="margin-top:-90px"> <uc2:portfolio runat="server" /> </div>           
         
          <div class="box-right box-right02">
               
              
                 <uc:flw ID="flwr" runat="server" />
              </div>             </div>
  </div>
      <!-- content inner ends --> 
    </div>
<!-- content section ends -->





<!-- Range Slider Script Begins -->
<!--<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>-->
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js" type="text/javascript"></script>
<script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
<script type="text/javascript" src="js/flipcounter.js"></script>
<!-- Range Slider Script Ends -->



<
<!-- Footer section ends -->

  <!--Infosys employee gallery script begins -->

<script type="text/javascript" language="javascript" src="js/jquery.carouFredSel-6.0.3-packed.js"></script>

<!-- optionally include helper plugins -->
<script type="text/javascript" language="javascript" src="js/jquery.mousewheel.min.js"></script>
<script type="text/javascript" language="javascript" src="js/jquery.touchSwipe.min.js"></script>

<!-- fire plugin onDocumentReady -->

<script type="text/javascript" language="javascript">
    $(function () {

        //	Basic carousel, no options
        $('#foo0').carouFredSel();

        //	Basic carousel + timer
        $('#foo1').carouFredSel({
            auto: {
                pauseOnHover: 'resume',
                progress: '#timer1'
            },
            prev: '#prev1',
            next: '#next1'
        });

        //	Scrolled by user interaction
        $('#foo2').carouFredSel({
            auto: false,
            prev: '#prev2',
            next: '#next2',
            pagination: "#pager2",
            auto: true,
            mousewheel: false,

            circular: false,
            infinite: false
        });

        //	Variable number of visible items with variable sizes
        $('#foo3').carouFredSel({
            width: 280,
            height: 'auto',
            prev: '#prev3',
            next: '#next3',
            auto: true,
            circular: false,
            infinite: false
        });

        //	Responsive layout, resizing the items
        $('#foo4').carouFredSel({
            responsive: true,
            width: '100%',
            scroll: 2,
            items: {
                width: 400,
                //	height: '30%',	//	optionally resize item-height
                visible: {
                    min: 2,
                    max: 6
                }
            }
        });

        //	Fuild layout, centering the items
        $('#foo5').carouFredSel({
            width: '100%',
            scroll: 2
        });

    });
		</script>
        
<!--Infosys employee gallery script ends -->  

   
  
</body>
</html>


</asp:Content>
