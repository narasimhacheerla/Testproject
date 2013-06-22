using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JqueryController
{
    public class JqueryController : Panel
    {
        public PageBase PageContainer { get; set; }
        public IPageBase PageBase { get; set; }
        public string ClientCallBackFunction { get; set; }

        protected override void OnPreRender(EventArgs e)
        {

            base.OnPreRender(e);
            AddClientResource();
        }

        private void AddClientResource()
        {
            const string resourceName = "JqueryController.JqueryController.js";
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterClientScriptResource(typeof(JqueryController), resourceName);
            cs.RegisterClientScriptInclude("JqueryInclude", "http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js");
        }

        public void RefreshPanel(params string[] callBackParms)
        {
            PageBase = (IPageBase)Page;
            IEnumerable<UserControl> controls = Controls.OfType<UserControl>();
            string htmlToRender = RenderFactory.RenderUserControl(controls.First());
            PageBase.AddToRender(ClientID, htmlToRender);
            PageBase.PanelRefresh = true;
            if (string.IsNullOrEmpty(ClientCallBackFunction) == false)
            {
                PageBase.AddCallBack(ClientCallBackFunction, callBackParms);
            }
        }
    }
}
