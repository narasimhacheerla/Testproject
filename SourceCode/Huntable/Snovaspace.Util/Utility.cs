using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Snovaspace.Util.Logging;

namespace Snovaspace.Util
{
    public class Utility
    {
        public string ReadFromEmbeddedResource(Assembly assembly, string name)
        {
            using (Stream stream = assembly.GetManifestResourceStream(name))
                if (stream != null)
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
            return null;
        }

        public XmlDocument DownloadUrlAsXml(string sUrl)
        {
            WebRequest wrGETURL = WebRequest.Create(sUrl);

            Stream objStream = wrGETURL.GetResponse().GetResponseStream();

            if (objStream != null)
            {
                var objReader = new StreamReader(objStream);

                var document = new XmlDocument();
                document.Load(objReader.BaseStream);
                return document;
            }
            return null;
        }

        public Utility BindDropdownList<T>(ListControl dropDownList, List<T> objects, string valueName = "Id", string keyName = "Description")
        {
            LoggingManager.Debug("Bind Drop down list " + dropDownList.ID + ", with objects count: " + objects.Count + ", and with Id as " + valueName + ", description as " + keyName);

            dropDownList.DataSource = objects;
            dropDownList.DataTextField = keyName;
            dropDownList.DataValueField = valueName;
            dropDownList.DataBind();

            return this;
        }

        public static string GetProviderFromEmailAddress(string emailAddress)
        {
            var provider = "other";
         
            var items = emailAddress.Split('@');
            if (items.Length == 2)
            {
                var domain = items[1].ToLower();

                
                switch (domain)
                {
                    case "gmail.com":
                        provider = "google";
                        break;
                    case "yahoo.com":
                        provider = "yahoo";
                        break;
                    case "ymail.com":
                        provider = "yahoo";
                        break;
                    case "yahoo.co.in":
                        provider = "yahoo";
                        break;
                    case "yahoo.in":
                        provider = "yahoo";
                        break;
                    case "live.com":
                        provider = "live";
                        break;
                    case "hotmail.com":
                        provider = "live";
                        break;
                    case "outlook.com":
                        provider = "live";
                        break;
                    case "snovaspace.com":
                        provider = "google";
                        break;
                    case "huntable.co.uk":
                        provider = "google";
                        break;
                    case "airhub.com":
                        provider = "google";
                        break;
                }
            }
            return provider;
        }

        public static Type ConvertString(string value, out object convertedValue)
        {
            // First check the whole number types, because floating point types will always parse whole numbers
            // Start with the smallest types
            byte byteResult;
            if (Byte.TryParse(value, out byteResult))
            {
                convertedValue = byteResult;
                return typeof(byte);
            }

            short shortResult;
            if (Int16.TryParse(value, out shortResult))
            {
                convertedValue = shortResult;
                return typeof(short);
            }

            int intResult;
            if (Int32.TryParse(value, out intResult))
            {
                convertedValue = intResult;
                return typeof(int);
            }

            long longResult;
            if (Int64.TryParse(value, out longResult))
            {
                convertedValue = longResult;
                return typeof(long);
            }

            ulong ulongResult;
            if (UInt64.TryParse(value, out ulongResult))
            {
                convertedValue = ulongResult;
                return typeof(ulong);
            }

            // No need to check the rest of the unsigned types, which will fit into the signed whole number types

            // Next check the floating point types
            float floatResult;
            if (Single.TryParse(value, out floatResult))
            {
                convertedValue = floatResult;
                return typeof(float);
            }


            // It's not clear that there's anything that double.TryParse() and decimal.TryParse() will parse 
            // but which float.TryParse() won't
            double doubleResult;
            if (Double.TryParse(value, out doubleResult))
            {
                convertedValue = doubleResult;
                return typeof(double);
            }

            decimal decimalResult;
            if (Decimal.TryParse(value, out decimalResult))
            {
                convertedValue = decimalResult;
                return typeof(decimal);
            }

            // It's not a number, so it's either a bool, char or string
            bool boolResult;
            if (Boolean.TryParse(value, out boolResult))
            {
                convertedValue = boolResult;
                return typeof(bool);
            }

            char charResult;
            if (Char.TryParse(value, out charResult))
            {
                convertedValue = charResult;
                return typeof(char);
            }

            convertedValue = value;
            return typeof(string);
        }

        /// <summary>
        /// Compare two types and find a type that can fit both of them
        /// </summary>
        /// <param name="typeA">First type to compare</param>
        /// <param name="typeB">Second type to compare</param>
        /// <returns>The type that can fit both types, or string if they're incompatible</returns>
        public static Type FindCommonType(Type typeA, Type typeB)
        {
            // Build the singleton type map (which will rebuild it in a typesafe manner
            // if it's not already built).
            BuildTypeMap();

            if (!_typeMap.ContainsKey(typeA))
                return typeof(string);

            if (!_typeMap[typeA].ContainsKey(typeB))
                return typeof(string);

            return _typeMap[typeA][typeB];
        }


        // Dictionary to map two types to a common type that can hold both of them
        private static Dictionary<Type, Dictionary<Type, Type>> _typeMap;

        // Locker object to build the singleton typeMap in a typesafe manner
        private static readonly object Locker = new object();

        /// <summary>
        /// Build the singleton type map in a typesafe manner.
        /// This map is a dictionary that maps a pair of types to a common type.
        /// So typeMap[typeof(float)][typeof(uint)] will return float, while
        /// typemap[typeof(char)][typeof(bool)] will return string.
        /// </summary>
        private static void BuildTypeMap()
        {
            lock (Locker)
            {
                if (_typeMap == null)
                {
                    _typeMap = new Dictionary<Type, Dictionary<Type, Type>>
                                   {
                                       // Comparing byte
                                       {typeof(byte), new Dictionary<Type, Type>
                                                          {
                                                                                       { typeof(byte), typeof(byte) },
                                                                                       { typeof(short), typeof(short) },
                                                                                       { typeof(int), typeof(int) },
                                                                                       { typeof(long), typeof(long) },
                                                                                       { typeof(ulong), typeof(ulong) },
                                                                                       { typeof(float), typeof(float) },
                                                                                       { typeof(double), typeof(double) },
                                                                                       { typeof(decimal), typeof(decimal) },
                                                                                       { typeof(bool), typeof(string) },
                                                                                       { typeof(char), typeof(string) },
                                                                                       { typeof(string), typeof(string) },
                                                                                   }},

                                       // Comparing short
                                       {typeof(short), new Dictionary<Type, Type>
                                                           {
                                                                                        { typeof(byte), typeof(short) },
                                                                                        { typeof(short), typeof(short) },
                                                                                        { typeof(int), typeof(int) },
                                                                                        { typeof(long), typeof(long) },
                                                                                        { typeof(ulong), typeof(ulong) },
                                                                                        { typeof(float), typeof(float) },
                                                                                        { typeof(double), typeof(double) },
                                                                                        { typeof(decimal), typeof(decimal) },
                                                                                        { typeof(bool), typeof(string) },
                                                                                        { typeof(char), typeof(string) },
                                                                                        { typeof(string), typeof(string) },
                                                                                    }},

                                       // Comparing int
                                       {typeof(int), new Dictionary<Type, Type>
                                                         {
                                                                                      { typeof(byte), typeof(int) },
                                                                                      { typeof(short), typeof(int) },
                                                                                      { typeof(int), typeof(int) },
                                                                                      { typeof(long), typeof(long) },
                                                                                      { typeof(ulong), typeof(ulong) },
                                                                                      { typeof(float), typeof(float) },
                                                                                      { typeof(double), typeof(double) },
                                                                                      { typeof(decimal), typeof(decimal) },
                                                                                      { typeof(bool), typeof(string) },
                                                                                      { typeof(char), typeof(string) },
                                                                                      { typeof(string), typeof(string) },
                                                                                  }},

                                       // Comparing long
                                       {typeof(long), new Dictionary<Type, Type>
                                                          {
                                                                                       { typeof(byte), typeof(long) },
                                                                                       { typeof(short), typeof(long) },
                                                                                       { typeof(int), typeof(long) },
                                                                                       { typeof(long), typeof(long) },
                                                                                       { typeof(ulong), typeof(ulong) },
                                                                                       { typeof(float), typeof(float) },
                                                                                       { typeof(double), typeof(double) },
                                                                                       { typeof(decimal), typeof(decimal) },
                                                                                       { typeof(bool), typeof(string) },
                                                                                       { typeof(char), typeof(string) },
                                                                                       { typeof(string), typeof(string) },
                                                                                   }},

                                       // Comparing ulong
                                       {typeof(ulong), new Dictionary<Type, Type>
                                                           {
                                                                                        { typeof(byte), typeof(ulong) },
                                                                                        { typeof(short), typeof(ulong) },
                                                                                        { typeof(int), typeof(ulong) },
                                                                                        { typeof(long), typeof(ulong) },
                                                                                        { typeof(ulong), typeof(ulong) },
                                                                                        { typeof(float), typeof(float) },
                                                                                        { typeof(double), typeof(double) },
                                                                                        { typeof(decimal), typeof(decimal) },
                                                                                        { typeof(bool), typeof(string) },
                                                                                        { typeof(char), typeof(string) },
                                                                                        { typeof(string), typeof(string) },
                                                                                    }},

                                       // Comparing float
                                       {typeof(float), new Dictionary<Type, Type>
                                                           {
                                                                                        { typeof(byte), typeof(float) },
                                                                                        { typeof(short), typeof(float) },
                                                                                        { typeof(int), typeof(float) },
                                                                                        { typeof(long), typeof(float) },
                                                                                        { typeof(ulong), typeof(float) },
                                                                                        { typeof(float), typeof(float) },
                                                                                        { typeof(double), typeof(double) },
                                                                                        { typeof(decimal), typeof(decimal) },
                                                                                        { typeof(bool), typeof(string) },
                                                                                        { typeof(char), typeof(string) },
                                                                                        { typeof(string), typeof(string) },
                                                                                    }},

                                       // Comparing double
                                       {typeof(double), new Dictionary<Type, Type>
                                                            {
                                                                                         { typeof(byte), typeof(double) },
                                                                                         { typeof(short), typeof(double) },
                                                                                         { typeof(int), typeof(double) },
                                                                                         { typeof(long), typeof(double) },
                                                                                         { typeof(ulong), typeof(double) },
                                                                                         { typeof(float), typeof(double) },
                                                                                         { typeof(double), typeof(double) },
                                                                                         { typeof(decimal), typeof(decimal) },
                                                                                         { typeof(bool), typeof(string) },
                                                                                         { typeof(char), typeof(string) },
                                                                                         { typeof(string), typeof(string) },
                                                                                     }},

                                       // Comparing decimal
                                       {typeof(decimal), new Dictionary<Type, Type>
                                                             {
                                                                                          { typeof(byte), typeof(decimal) },
                                                                                          { typeof(short), typeof(decimal) },
                                                                                          { typeof(int), typeof(decimal) },
                                                                                          { typeof(long), typeof(decimal) },
                                                                                          { typeof(ulong), typeof(decimal) },
                                                                                          { typeof(float), typeof(decimal) },
                                                                                          { typeof(double), typeof(decimal) },
                                                                                          { typeof(decimal), typeof(decimal) },
                                                                                          { typeof(bool), typeof(string) },
                                                                                          { typeof(char), typeof(string) },
                                                                                          { typeof(string), typeof(string) },
                                                                                      }},

                                       // Comparing bool
                                       {typeof(bool), new Dictionary<Type, Type>
                                                          {
                                                                                       { typeof(byte), typeof(string) },
                                                                                       { typeof(short), typeof(string) },
                                                                                       { typeof(int), typeof(string) },
                                                                                       { typeof(long), typeof(string) },
                                                                                       { typeof(ulong), typeof(string) },
                                                                                       { typeof(float), typeof(string) },
                                                                                       { typeof(double), typeof(string) },
                                                                                       { typeof(decimal), typeof(string) },
                                                                                       { typeof(bool), typeof(bool) },
                                                                                       { typeof(char), typeof(string) },
                                                                                       { typeof(string), typeof(string) },
                                                                                   }},

                                       // Comparing char
                                       {typeof(char), new Dictionary<Type, Type>
                                                          {
                                                                                       { typeof(byte), typeof(string) },
                                                                                       { typeof(short), typeof(string) },
                                                                                       { typeof(int), typeof(string) },
                                                                                       { typeof(long), typeof(string) },
                                                                                       { typeof(ulong), typeof(string) },
                                                                                       { typeof(float), typeof(string) },
                                                                                       { typeof(double), typeof(string) },
                                                                                       { typeof(decimal), typeof(string) },
                                                                                       { typeof(bool), typeof(string) },
                                                                                       { typeof(char), typeof(char) },
                                                                                       { typeof(string), typeof(string) },
                                                                                   }},

                                       // Comparing string
                                       {typeof(string), new Dictionary<Type, Type>
                                                            {
                                                                                         { typeof(byte), typeof(string) },
                                                                                         { typeof(short), typeof(string) },
                                                                                         { typeof(int), typeof(string) },
                                                                                         { typeof(long), typeof(string) },
                                                                                         { typeof(ulong), typeof(string) },
                                                                                         { typeof(float), typeof(string) },
                                                                                         { typeof(double), typeof(string) },
                                                                                         { typeof(decimal), typeof(string) },
                                                                                         { typeof(bool), typeof(string) },
                                                                                         { typeof(char), typeof(string) },
                                                                                         { typeof(string), typeof(string) },
                                                                                     }},

                                   };
                }
            }
        }

        public void DisplayMessage(Page page, string message)
        {
            LoggingManager.Debug("Display message to user: " + message);
            page.ClientScript.RegisterStartupScript(GetType(), "onload", "alert('" + message + "');", true);
        }

        public void DisplayMessageWithPostback(Page page, string message)
        {
            LoggingManager.Debug("Display message to user: " + message);
            page.ClientScript.RegisterStartupScript(GetType(), "onload", "alert('" + message + "');__doPostBack(null,null);", true);
        }

        public string GetApplicationBaseUrl()
        {
            LoggingManager.Debug("Entering GetApplicationBaseUrl - InviteFriends.aspx");
            //Return variable declaration 
            string appPath = null;
            try
            {
                //Getting the current context of HTTP request 
                var context = HttpContext.Current;

                //Checking the current context content 
                if (context != null)
                {
                    //Formatting the fully qualified website url/name 
                    appPath = String.Format("{0}://{1}{2}{3}",
                                            context.Request.Url.Scheme,
                                            context.Request.Url.Host,
                                            context.Request.Url.Port == 80
                                                ? String.Empty
                                                : ":" + context.Request.Url.Port,
                                            context.Request.ApplicationPath);
                }

                if (appPath != null && !appPath.EndsWith("/"))
                    appPath += "/";
            }
            catch (Exception ex)
            {
                LoggingManager.Error(ex);
            }
            LoggingManager.Debug("Exiting GetApplicationBaseUrl - InviteFriends.aspx");
            return appPath;
        }

        public int? ConvertToInt(object obj)
        {
            if (obj == null) return null;

            return ConvertToInt(obj.ToString());
        }

        public int? ConvertToInt(string objectName)
        {
            if (string.IsNullOrWhiteSpace(objectName)) return null;
            int value;
            bool result = Int32.TryParse(objectName, out  value);
            if (result)
            {
                return value;
            }
            else
            {
                return null;
            }



        }

        public void RunAsTask(Action action)
        {
            Task.Factory.StartNew(() => TryCatch(action));
        }

        public void TryCatch(Action action)
        {
            try
            {
                LoggingManager.Enter(1);
                action();
                LoggingManager.Exit(1);
            }
            catch (Exception exception)
            {
                LoggingManager.Error(exception);
            }
        }

        public static DateTime? ParseDate(string date)
        {
            try
            {
                DateTime dateOut;

                if (DateTime.TryParse(date, out dateOut))
                {
                    return dateOut;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static DateTime? ParseDate(string date, string format, CultureInfo currentCulture)
        {
            try
            {
                DateTime dateOut;

                if (DateTime.TryParseExact(date, format, currentCulture, DateTimeStyles.AssumeLocal, out dateOut))
                {
                    return dateOut;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string Pad(string content, int noOfDigits)
        {
            if (string.IsNullOrWhiteSpace(content)) return "00";
            if (content.Length == noOfDigits - 1) return "0" + content;
            if (content.Length == noOfDigits - 2) return "00" + content;
            if (content.Length == noOfDigits - 3) return "000" + content;
            if (content.Length == noOfDigits - 4) return "0000" + content;
            return content;
        }

        public static string FormatDate(DateTime? submissionDate)
        {
            if (submissionDate.HasValue)
            {
                return submissionDate.Value.ToString("dd/MM/yyyy");
            }

            return null;
        }

        public void RedirectUrl(HttpResponse response, string url)
        {
            response.Redirect(url, false);
        }
    }
}
