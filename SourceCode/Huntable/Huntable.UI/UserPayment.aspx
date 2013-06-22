<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPayment.aspx.cs" Inherits="Huntable.UI.UserPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<div style="margin-top: 10px; border: 1px dashed #73edff; background-color: #edfdff;
    border-radius: 4px 4px; padding: 10px 10px; float: left; width: 553px; font-size: 12px;">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="10" style="font-size: 12px;">
        <tr>
            
            <td colspan="3" align="right" style="border-bottom: 1px solid #7eefff;">
                <asp:Button runat="server" CssClass="button-ash floatright"  Text="Download" onclick="Downloadclick" />
                Billing date : <asp:Label runat="server" ID="lblDtae"></asp:Label><br />
                Invoice #:<asp:Label runat="server" ID="lblInvoiceno"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                       Huntable <br/>
                       2 Woodberry Grove <br/>
                       Finchley <br/>
                       London <br/>
                       N12 0DR
            </td>
            <td>
                Bill to<br />
                <asp:Label runat="server" ID="lblName"></asp:Label><br />
                <asp:Label runat="server" ID="lblHomeAddress"></asp:Label><br />
                <asp:Label runat="server" ID="lblCity"></asp:Label><br />
                <asp:Label runat="server" ID="lblLoc"></asp:Label><br />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" style="border-bottom: 1px solid #7eefff;">
                <strong>Description</strong>
            </td>
            <td style="border-bottom: 1px solid #7eefff;">
                <strong>Price</strong>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Premium membership subscriptions
            </td>
            <td>
                $<asp:Label runat="server" ID="lblPremiumPrice"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Jobs package - ( <asp:Label ID="lblJobpackageCount" runat="server"></asp:Label>Jobs - package )
            </td>
            <td>
                $<asp:Label runat="server" ID="lblJobsPackage"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Featured recruiters<br />
                Industry - ( <asp:Label ID="lblIndustries" runat="server"></asp:Label> Industries)
            </td>
            <td valign="bottom">
                $ <asp:Label runat="server" ID="lblIndustry"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Skill - (<asp:Label ID="lblSkillCount" runat="server"></asp:Label> Skills)
            </td>
            <td valign="bottom">
                $<asp:Label runat="server" ID="lblSkill"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Country - (<asp:Label runat="server" ID="lblCountryCount"></asp:Label> Countries)
            </td>
            <td valign="bottom">
                $<asp:Label runat="server" ID="lblCountry"></asp:Label>
            </td>
        </tr>
          <tr>
            <td colspan="2">
                Interests - (<asp:Label runat="server" ID="lblInterestCount"></asp:Label> Interests)
            </td>
            <td valign="bottom">
                $<asp:Label runat="server" ID="lblinterests"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="right" style="border-top: 1px solid #7eefff;">
                <strong>Total :</strong>
            </td>
            <td style="border-top: 1px solid #7eefff;">
                <strong>$<asp:Label runat="server" ID="lblTotalAmount"></asp:Label></strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="right" style="border-bottom: 1px solid #7eefff;">
                <strong>VAT @20%</strong>
            </td>
            <td style="border-bottom: 1px solid #7eefff;">
                <strong>$<asp:label runat="server" ID="lblVat"></asp:label></strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="right" style="border-bottom: 1px solid #7eefff;">
                <strong>Total </strong>
            </td>
            <td style="border-bottom: 1px solid #7eefff;">
                <strong>$<asp:Label runat="server" ID="lblTotalAmountAfterVat"></asp:Label></strong>
            </td>
        </tr>
    </table></form>
</div>

