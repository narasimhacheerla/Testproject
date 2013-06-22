using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace JqueryController
{
    public interface IPageBase 
    {
         bool IsPartialRendering { get; set; }
         StringBuilder ResponseToRender { get; }
         void AddToRender(string PaneldId, string htmlToAdd);
         void AddCallBack(string functionName,params string[] callBackParms);
         bool PanelRefresh { get; set; }
         Dictionary<string, string> PostParameter { get; }
    }
}
