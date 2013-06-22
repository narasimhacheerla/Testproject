<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cvStatistics.ascx.cs"
    Inherits="Huntable.UI.UserControls.CVStatistics" %>
<link href="https://huntable.co.uk/css/style-innerpage.css" type="text/css" rel="stylesheet" />
<script class="code" type="text/javascript">

    $(document).ready(function () {
        if (typeof histData == 'undefined') return;
        if (histData != null) {
            // goog = [["6/22/2009", 425.32], ["6/8/2009", 424.84], ["5/26/2009", 417.23], ["5/11/2009", 390], ["4/27/2009", 393.69], ["4/13/2009", 392.24], ["3/30/2009", 369.78], ["3/16/2009", 330.16], ["3/2/2009", 308.57], ["2/17/2009", 346.45], ["2/2/2009", 371.28], ["1/20/2009", 324.7], ["1/5/2009", 315.07], ["12/22/2008", 300.36], ["12/8/2008", 315.76], ["11/24/2008", 292.96], ["11/10/2008", 310.02], ["10/27/2008", 359.36], ["10/13/2008", 372.54], ["9/29/2008", 386.91], ["9/15/2008", 449.15], ["9/2/2008", 444.25], ["8/25/2008", 463.29], ["8/11/2008", 510.15], ["7/28/2008", 467.86], ["7/14/2008", 481.32], ["6/30/2008", 537], ["6/16/2008", 546.43], ["6/2/2008", 567], ["5/19/2008", 544.62], ["5/5/2008", 573.2], ["4/21/2008", 544.06], ["4/7/2008", 457.45], ["3/24/2008", 438.08], ["3/10/2008", 437.92], ["2/25/2008", 471.18], ["2/11/2008", 529.64], ["1/28/2008", 515.9], ["1/14/2008", 600.25], ["12/31/2007", 657], ["12/17/2007", 696.69], ["12/3/2007", 714.87], ["11/19/2007", 676.7], ["11/5/2007", 663.97], ["10/22/2007", 674.6], ["10/8/2007", 637.39], ["9/24/2007", 567.27], ["9/10/2007", 528.75], ["8/27/2007", 515.25]];
            goog = [["6/22/2009", 0], ["6/8/2009", 0]];
            var plot1 = $.jqplot('chart1', [histData], {
                title: '',
                series: [{ label: '', neighborThreshold: -1}],
                axes: { xaxis: { renderer: $.jqplot.DateAxisRenderer, tickRenderer: $.jqplot.CanvasAxisTickRenderer,
                    tickOptions: { angle: -30 }
                },
                    yaxis: { renderer: $.jqplot.LogAxisRenderer, tickOptions: { prefix: ''} }
                },
                cursor: { show: true, zoom: true }
            });
        }

    });


</script>
<asp:Literal ID="scriptLiteral" runat="server"></asp:Literal>
<div id="cvStats" class="box-right" runat="server">
    <div class="head-ash">
        <h3>
            Your Profile statistics at a glance</h3>
    </div>
    <p class="cv-rating">
        <strong>
            <asp:Label runat="server" ID="lblTotalViews" Text="188" ForeColor="Red"></asp:Label></strong>Total
        views</p>
    <p class="cv-rating">
        <strong>
            <asp:Label runat="server" ID="lblTotalSearchResults" Text="1941" ForeColor="Red"></asp:Label></strong>Total
        Search Results</p>
    <p class="cv-rating">
        <strong>
            <asp:Label runat="server" ID="lblJobEnquiryMessages" Text="10" ForeColor="Red"></asp:Label></strong>Job
        Enquiry Messages</p>
    <asp:PlaceHolder ID="phGraph" runat="server"></asp:PlaceHolder>
    <div id ="show" runat="server" style="height:160px;width:100%"></div> 
    <%--<img src="../images/cv-chart.png" width="294" height="124" alt="cv-chart" />--%>
    <%--<b class="views">140 views</b>--%>
    <asp:LinkButton runat="server" ID="lnkViewMore" Text="View More.." CssClass="learn-more"
        ForeColor="Blue" OnClick="lnkViewMore_Click"></asp:LinkButton>
</div>
