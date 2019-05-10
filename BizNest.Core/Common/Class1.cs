using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;


namespace BizNest.Core.Common
{
    /// <summary>
    /// /
    /// </summary>
    public class JObjectHelper
    {
        private List<string> ls;
        private JObject jo;
        /// <summary>
        /// 
        /// </summary>
        public JObjectHelper()
        {
            jo = new JObject();
            ls = new List<string>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="fields"></param>
        /// <param name="extended"></param>
        public JObjectHelper(string sort = "", int page = 1, int pageSize = 10, string fields = "", bool? extended = null)
        {
            jo = new JObject();
            ls = new List<string>();
            if (!string.IsNullOrEmpty(sort))
                Add("sort", sort);
            if (page > 1)
                Add("page", page);
            if (pageSize >= -1)
                Add("pageSize", pageSize);
            if (!string.IsNullOrEmpty(fields))
                Add("fields", fields);
            if (extended.HasValue)
                Add("extended", extended);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Add(string key, int data)
        {
            Remove(key);
            jo.Add(key, data);
            ls.Add(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Add(string key, Guid data)
        {
            Remove(key);
            jo.Add(key, data);
            ls.Add(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Add(string key, long data)
        {
            Remove(key);
            jo.Add(key, data);
            ls.Add(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        private void Remove(string key)
        {
            try
            {
                if (jo[key] != null)
                    jo.Remove(key);
            }
            catch { }

            try
            {
                if (ls.Contains(key))
                    ls.Remove(key);
            }
            catch { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Add(string key, string data)
        {
            Remove(key);
            jo.Add(key, data);
            ls.Add(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        public void Parse(string json)
        {
            jo = JObject.Parse(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPath"></param>
        /// <returns></returns>
        public string DataItem(string dataPath)
        {
            var resp = "";
            char[] sep = { ',' };
            char[] prt = { '.' };
            var items = dataPath.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length <= 0) return string.Empty;
            foreach (var item in items)
            {
                var bits = item.Split(prt, StringSplitOptions.RemoveEmptyEntries);
                if (bits.Length <= 0) continue;

                switch (bits.Length)
                {
                    case 1:
                        resp += (string)jo[bits[0]];
                        break;
                    case 2:
                        resp += (string)jo[bits[0]][bits[1]];
                        break;
                    case 3:
                        resp += (string)jo[bits[0]][bits[1]][bits[2]];
                        break;
                    case 4:
                        resp += (string)jo[bits[0]][bits[1]][bits[2]][bits[3]];
                        break;
                    case 5:
                        resp += (string)jo[bits[0]][bits[1]][bits[2]][bits[3]][bits[4]];
                        break;
                }
            }
            return resp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Add(string key, decimal data)
        {
            jo.Add(key, data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Add(string key, DateTime data)
        {
            Remove(key);
            jo.Add(key, data);
            ls.Add(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Add(string key, DateTime? data)
        {
            Remove(key);
            jo.Add(key, data.HasValue ? data.GetValueOrDefault().ToString() : "");
            ls.Add(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Add(string key, bool data)
        {
            Remove(key);
            jo.Add(key, data);
            ls.Add(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Add(string key, bool? data)
        {
            Remove(key);
            jo.Add(key, data.HasValue ? data.GetValueOrDefault().ToString() : "");
            ls.Add(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Add(string key, float data)
        {
            Remove(key);
            jo.Add(key, data);
            ls.Add(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Add(string key, double data)
        {
            Remove(key);
            jo.Add(key, data);
            ls.Add(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var urlEncodedString = string.Join("&", jo.Properties()
                .Select(property => property.Name + "=" + property.Value.ToString())
                .ToArray());
            return urlEncodedString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToQueryString()
        {
            return "?" + ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object ToObject()
        {
            ExpandoObject objectToReturn = new ExpandoObject();
            foreach (var field in ls)
            {
                try
                {
                    var typ = jo.GetValue(field).GetType();
                    var fieldValue = jo.GetValue(field).ToObject(typ);
                    ((IDictionary<string, object>)objectToReturn).Add(field, fieldValue);
                }
                catch (Exception ex)
                {
                    //Log.Error(ex);
                }
            }
            return objectToReturn;
        }
        /// <summary>
        /// 
        /// </summary>
        public JObject JObject
        {
            get
            {
                return jo;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JObjectString
        {
            get
            {
                return Util.SerializeJSON(jo);
            }
        }
    }
}
