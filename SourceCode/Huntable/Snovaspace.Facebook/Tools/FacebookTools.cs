using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;


namespace Snovaspace.Facebook.Tools
{
     [Serializable]
    public class FacebookTools
    {
        /// <summary>
        /// Method to simplify calling an external url.  Any results from the url will be saved as a string that can
        /// either be xml, html, json, etc etc.  It won't handle calling files directly, I think, not tried it,
        /// not had a need.  It works great with string based result though.
        /// </summary>
        /// <param name="url">The url you want to call</param>
        /// <param name="method">How you want to call it e.g. GET or POST (well that's the only 2 you'd use really!)</param>
        /// <returns></returns>
        public static string CallUrl(string url, string method)
        {
            string UserAgent = "WiseCMS/4.0 Facebook API (compatible; MSIE 6.0; Windows NT 5.1)";
            int _timeout = 300000;

            HttpWebRequest req = null;
            HttpWebResponse res = null;

            // Initialise the web request
            req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = method.Length > 0 ? method : "POST";

            req.UserAgent = UserAgent;

            // if (Proxy != null) req.Proxy = Proxy;
            req.Timeout = _timeout;
            req.KeepAlive = false;

            // This is needed in the Compact Framework
            // See for more details: http://msdn2.microsoft.com/en-us/library/1afx2b0f.aspx
            if (method != "GET")
                req.GetRequestStream().Close();

            string responseString = string.Empty;

            try
            {
                // Get response from the internet
                res = (HttpWebResponse)req.GetResponse();
                using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                {
                    responseString = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                // only throw this error if the website is not live
                // erm do something better than this!!  cause this sucks ass!
            }

            return responseString;
        }

        /// <summary>
        /// Just an overload of above.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string CallUrl(string url)
        {
            return CallUrl(url, "GET");
        }

        //public static T FromJson<T>(this string s)
        //{
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    return ser.Deserialize<T>(s);
        //}

        //public static string ToJson(this object obj)
        //{
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    return ser.Serialize(obj);
        //}
    }
}
