using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snovaspace.Facebook.Entities;
using Snovaspace.Facebook.Tools;
using System.Web.Script.Serialization;

namespace Snovaspace.Facebook
{
    /// <summary>
    /// Summary description for FacebookService
    /// </summary>
    public class FacebookService
    {
        public bool Save(Snovaspace.Facebook.Entities.Facebook entity)
        {
            // do you own saving stuff in here, cause am not doing it for you!!

            System.Web.HttpContext.Current.Session["FacebookAccessToken"] = entity.FacebookAccessToken;
            System.Web.HttpContext.Current.Session["FacebookId"] = entity.FacebookId;

            return true;
        }

        private string GetSessionVariable(string key)
        {
            
            var value = System.Web.HttpContext.Current.Session[key];
            if (value != null)
                return value.ToString();
            else
                return string.Empty;
        }

        public Snovaspace.Facebook.Entities.Facebook GetById(int id)
        {
            // this is just going to be a fake'd object get your's from the database
            Snovaspace.Facebook.Entities.Facebook fb = new Snovaspace.Facebook.Entities.Facebook();
            fb.FacebookAccessToken = GetSessionVariable("FacebookAccessToken"); // facebook will give you this
            fb.FacebookId = GetSessionVariable("FacebookId");  // get this from facebook
            fb.Id = 99; // the database row id, it's not really though

            return fb;
        }

        public FacebookProfile User_GetDetails(string accessToken)
        {
            string url = "https://graph.facebook.com/me?access_token=" + accessToken;
            return CallUrl<FacebookProfile>(url);
        }

        //public FacebookPhotos Photos_FetchAllOfMe(string userName, string accessToken)
        //{
        //    string url = "https://graph.facebook.com/" + userName + "/photos?access_token=" + accessToken;
        //    return CallUrl<FacebookPhotos>(url);
        //}

        //public FacebookAlbums Albums_FetchAll(string userName, string accessToken)
        //{
        //    string url = "https://graph.facebook.com/" + userName + "/albums?access_token=" + accessToken;
        //    return CallUrl<FacebookAlbums>(url);
        //}

        //public FacebookPhotos Photos_FetchAllFromAlbum(string albumId, string accessToken)
        //{
        //    string url = "https://graph.facebook.com/" + albumId + "/photos?access_token=" + accessToken;
        //    return CallUrl<FacebookPhotos>(url);
        //}

        //public FacebookLikes Pages_FetchAllILike(string userName, string accessToken)
        //{
        //    string url = "https://graph.facebook.com/" + userName + "/likes?access_token=" + accessToken;
        //    return CallUrl<FacebookLikes>(url);
        //}

        private T CallUrl<T>(string url)
        {
            try
            {
                string result = FacebookTools.CallUrl(url);
                T items = FromJson<T>(result);
                return items;
            }
            catch
            {
                return default(T);
            }
        }

        public T FromJson<T>(string s)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return ser.Deserialize<T>(s);
        }
    }
}
