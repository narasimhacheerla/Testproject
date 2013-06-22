using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Huntable.UI.Controls
{
    public partial class Portfolio : System.Web.UI.UserControl
    {
        public FileUpload PortfolioFile { get { return filePortfolio; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}