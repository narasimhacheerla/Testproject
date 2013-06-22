using System;
using System.Configuration;

namespace Huntable.Data.UI
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.ServerVariables["REMOTE_ADDR"] == "::1" || Request.ServerVariables["REMOTE_ADDR"] == "81.149.184.243"
                || Request.ServerVariables["REMOTE_ADDR"] == "81.149.184.243")
            {
                Session["loggedin"] = "true";
            }
            else
            {
                Response.Redirect("notloggedin.aspx");
            }
        }
    }
}
