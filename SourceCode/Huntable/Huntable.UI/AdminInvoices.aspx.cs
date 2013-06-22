using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Huntable.Data;
using Snovaspace.Util.Logging;

namespace Huntable.UI
{
    public partial class AdminInvoices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering Page_Load - AdminInvoices");

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                Invoices.DataSource = context.Invoices.Where(x => x.TransactionComplete == false).ToList();
                Invoices.DataBind();
            }

            LoggingManager.Debug("Exiting Page_Load - AdminInvoices");
        }
        protected void payuser(object sender, EventArgs e)
        {
            LoggingManager.Debug("Entering payuser - AdminInvoices");

            var button = sender as Button;
            if (button!=null)
            {
                 int id = Convert.ToInt32(button.CommandArgument);
                using (var context = huntableEntities.GetEntitiesWithNoLock())
                {
                    var payupdate = context.Invoices.FirstOrDefault(x => x.Id == id&&x.TransactionComplete==false&&x.TransactionCompletedDateTime==null);
                    if (payupdate != null)
                    {
                        payupdate.TransactionComplete = true;
                        payupdate.TransactionCompletedDateTime = DateTime.Now;
                    }
                    context.SaveChanges();
                    Response.Redirect("AdminInvoices.aspx");
                }
            }
           
           LoggingManager.Debug("Exiting PayUser - AdminInvoices");
        }

      

  
    }
}