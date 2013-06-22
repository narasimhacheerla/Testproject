using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for Class1
/// </summary>
public class RenderFactory
{
    
    public static string RenderUserControl(Control crtl)
    {
        Control control = null;
        const string STR_BeginRenderControlBlock = "<!-- BLOCK RENDER CONTROL -->";
        const string STR_EndRenderControlBlock = "<!-- END RENDERCONTROL -->";
        StringWriter tw = new StringWriter();
        Page page = new Page();
        page.EnableViewState = false;
        HtmlForm form = new HtmlForm(); 
        form.ID = "__temporanyForm"; 
        page.Controls.Add(form);
        form.Controls.Add(new LiteralControl(STR_BeginRenderControlBlock));
        form.Controls.Add(crtl); 
        form.Controls.Add(new LiteralControl(STR_EndRenderControlBlock)); 
        HttpContext.Current.Server.Execute(page, tw, true); 
        string Html = tw.ToString();   
        //TO DO:clean the response!!!!!
        int start = Html.IndexOf("<!-- BLOCK RENDER CONTROL -->");
        int end = Html.Length - start;
        Html = Html.Substring(start,end);
        return Html;
        
    }

    public static string RenderUserControl(string crtlPath)
    {

        Control control = null;
        const string STR_BeginRenderControlBlock = "<!-- BLOCK RENDER CONTROL -->";
        const string STR_EndRenderControlBlock = "<!-- END RENDERCONTROL -->";
        StringWriter tw = new StringWriter();
        Page page = new Page();
        page.EnableViewState = false;
        control=page.LoadControl(crtlPath);
        HtmlForm form = new HtmlForm();
        form.ID = "__temporanyForm";
        page.Controls.Add(form);
        form.Controls.Add(new LiteralControl(STR_BeginRenderControlBlock));
        form.Controls.Add(control);
        form.Controls.Add(new LiteralControl(STR_EndRenderControlBlock));
        HttpContext.Current.Server.Execute(page, tw, true);
        string Html = tw.ToString();
        //TO DO:clean the response!!!!!
        return Html;

    }



}
