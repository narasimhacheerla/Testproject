using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Logging;
using Snovaspace.Util.Mail;

namespace Huntable.UI
{
    public partial class AffliatePartner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load -  AffliatePartner .aspx");
            LoggingManager.Debug("Exiting Page_Load -  AffliatePartner .aspx");
        }
        protected void SendMailonclick(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering SendMailonclick -  AffliatePartner .aspx");

            StringBuilder body = new StringBuilder()
           .Append("Name:")
           .Append(Txtnamme.Text)
           .AppendLine("<br/>")
           .Append("Email :")
           .Append(Txtemail.Text)
           .AppendLine("<br/>")
           .Append("Message:")
            .Append((txtmsg.Text).Replace("\n", "<br/>"))
           .AppendLine("<br/>");
            string sub = "Affiliate partner";
            string body1 = body.ToString();
            string email = "support@huntable.co.uk";

            SnovaUtil.SendEmail(sub, body1, email);
            Txtnamme.Text = "";
            Txtemail.Text = "";
            txtmsg.Text = "";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Call my function", "overlay('Message sent successfully')", true);
            //txtEmail.Text = "";
            //lblSuccess.Text = "Message sent successfully";
            //lblSuccess.ForeColor = System.Drawing.Color.Green;
            //lblSuccess.Visible = true;
            LoggingManager.Debug("Entering SendMailonclick -  AffliatePartner .aspx");
        }
    }
}