
using Microsoft.Extensions.Configuration;

namespace BizNest.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool IsDev
        {
            get
            {
                return Get("site.env") == "dev";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static bool IsDebug
        {
            get
            {
                return Get("site.env") == "debug";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static bool IsProd
        {
            get
            {
                return Get("site.env") == "prod";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="otherwise"></param>
        /// <returns></returns>
        public static string Get(string key, string otherwise = "")
        {
            //var env = ConfigurationManager.AppSettings["site.env"];
            //if (env == "dev")
            //{
            //    return otherwise;
            //}
            //var str = ConfigurationManager.AppSettings[key];
            //if (!string.IsNullOrEmpty(str)) return str;
            return otherwise;
        }

        /// <summary>
        /// GEt a integer value from the apps settings
        /// </summary>
        /// <param name="key">The appsetting key</param>
        /// <param name="otherwise">Default value if nothing is found</param>
        /// <returns></returns>
        public static int GetInt(string key, int otherwise = 0)
        {
            var str = Get(key);
            var val = otherwise;
            try
            {
                val = int.Parse(str);
            }
            catch
            {
            }
            return val;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="otherwise"></param>
        /// <returns></returns>
        public static long GetLong(string key, long otherwise = 0)
        {
            var str = Get(key);
            var val = otherwise;
            try
            {
                val = long.Parse(str);
            }
            catch
            {
            }
            return val;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ConnectionString(string key = "")
        {
            return ""; //ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string Connection()
        {
            return "";
        }
    }

}
