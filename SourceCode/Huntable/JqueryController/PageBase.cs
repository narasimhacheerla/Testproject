using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace JqueryController
{
    public class PageBase : Page, IPageBase
    {

        #region private members

        private bool _IsPartialRendering = false;
        private StringBuilder _ResponseToRender = new StringBuilder();
        private Dictionary<string, string> _postParameters = new Dictionary<string, string>();

        #endregion

        #region IPageBase Members

        public Dictionary<string, string> PostParameter
        {
            get { return _postParameters; }
        }

        public StringBuilder ResponseToRender
        {
            get
            {
                return _ResponseToRender;
            }
        }

        public bool IsPartialRendering
        {
            get
            {
                return _IsPartialRendering;
            }
            set
            {
                _IsPartialRendering = value;
            }
        }

        public bool PanelRefresh { get; set; }



        #endregion

        private void PopulatePostParameters()
        {
            try
            {
                string[] parameters = Request.Form["__parameters"].ToString().Split('&');
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (string.IsNullOrEmpty(parameters[i]) == false)
                    {
                        int equalPosition = parameters[i].IndexOf("=");
                        this._postParameters.Add(parameters[i].Substring(0, equalPosition), parameters[i].Substring(equalPosition + 1));
                    }
                }
            }
            catch (Exception ex)
            {

                throw new FormatException("The parameters string is not in the correct format", ex);
            }

        }

        protected override void OnLoad(EventArgs e)
        {

            IsPartialRendering = false;
            if (Request.Form["__parameters"] != null)
            {
                IsPartialRendering = true;
                this.PopulatePostParameters();
            }
            base.OnLoad(e);
        }

        public void AddToRender(string PaneldId, string htmlToAdd)
        {
            //that's is just to avoid sitax error if there is javascript on the control that is gonna be rendered
            //Must be improved/refactored
            htmlToAdd = htmlToAdd.Replace("\\", "\\\\");
            htmlToAdd = htmlToAdd.Replace(Environment.NewLine, string.Empty);
            htmlToAdd = htmlToAdd.Replace(@"""", "\\\"");
            this.ResponseToRender.Append(@"$(""#" + PaneldId + @""").html(""" + htmlToAdd + @""");");
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (PanelRefresh)
            {
                Response.Write(this.ResponseToRender.ToString());

                Response.End();
            }
            base.Render(writer);
        }


        public void AddCallBack(string functionName, params string[] callBackParms)
        {
            var paramToJs = new StringBuilder();
            if (callBackParms.Length > 0)
            {
                foreach (string parm in callBackParms)
                {
                    paramToJs.Append("'" + parm + "',");
                }
                paramToJs.Remove(paramToJs.Length - 1, 1);
            }
            ResponseToRender.Append(functionName + "(" + paramToJs.ToString() + ");");
        }
    }
}
