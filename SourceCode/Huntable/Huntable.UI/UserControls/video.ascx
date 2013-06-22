<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="video.ascx.cs" Inherits="Huntable.UI.UserControls.video" %>
<link href="https://huntable.co.uk/css/style-innerpage.css" type="text/css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="../text/html; charset=utf-8" />
<meta name="description" content="Huntable - The Professional Network" />
<meta name="keywords" content="The Professional Network" />
<link type="text/css" href="https://huntable.co.uk/css1/jquery-ui-1.8.23.custom.css"
    rel="stylesheet" />
<%--  <script type="text/javascript" language="javascript">
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
		</script>--%>
<div class="box-right" id="div_video" runat="server" >
    <div class="head-ash">
        <h3>
            Videos</h3>
    </div>
    <div class="list_carousel" style="max-height:257px; overflow:scroll; width: 291px;">
        <asp:UpdatePanel ID="Update_Panel" runat="server">
            <ContentTemplate>
                <asp:HiddenField runat="server" Value="" ID="Hidden_Field" />
                <asp:DataList runat="server" ID="dlvideos">
                    <ItemTemplate>
                        <iframe id="videourl01" runat="server" width="280" height="125" frameborder="0" src='<%#Eval("VideoUrl") %>'>
                        </iframe>
                    </ItemTemplate>
                </asp:DataList>
                <asp:Button runat="server" Text=">more" ID="btn_next" OnClick="btn_click" Style="margin-left: 238px;
                    color: #008CA1; border: #D5E2E2;" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<%--<div class="box-right" >
                <div class="head-ash">
               	  <h3>Portfolio</h3>
                  </div>
           <div class="list_carousel"> --%>
<%--   <ul>
                <li style="width:280px;" id="li01" runat="server">
               	 <iframe  id="videourl01" runat="server" width="280" height="125"
                                                frameborder="0"></iframe>
                 </li>
               
               <li style="width:280px; height:190px;" id="li02" runat="server" visible="false">
                 <iframe  id="videourl02" runat="server" width="280" height="125"
                                                frameborder="0"></iframe>
                 </li>
                
                </ul>
 
	    </div>
        <div style="text-align:center;">
        <asp:Button runat="server" ID="btn_slide" OnClick="btn_slide_Click" Text="1" />
         <asp:Button runat="server" ID="btn_next" OnClick="btn_next_Click" Text="2" /></div>
              </div>--%>