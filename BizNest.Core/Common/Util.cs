using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BizNest.Core.Common
{
    public static class Util
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime CurrentDateTime()
        {
            return DateTime.UtcNow;
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static T DeserializeJSON<T>(string objectData)
        {
            return JsonConvert.DeserializeObject<T>(objectData);
        }

        public static string SerializeJSON(object objectInstance)
        {
            return JsonConvert.SerializeObject(objectInstance);
        }


        public static object ShapeList<TSource>(this IList<TSource> obj, string fields)
        {
            List<string> lstOfFields = new List<string>();
            if (string.IsNullOrEmpty(fields))
            {
                return obj;
            }
            lstOfFields = fields.Split(',').ToList();
            List<string> lstOfFieldsToWorkWith = new List<string>(lstOfFields);

            List<System.Dynamic.ExpandoObject> lsobjectToReturn = new List<System.Dynamic.ExpandoObject>();
            if (!lstOfFieldsToWorkWith.Any())
            {
                return obj;
            }
            else
            {
                foreach (var kj in obj)
                {
                    System.Dynamic.ExpandoObject objectToReturn = new System.Dynamic.ExpandoObject();

                    foreach (var field in lstOfFieldsToWorkWith)
                    {
                        try
                        {
                            var fieldValue = kj.GetType()
                            .GetProperty(field.Trim(), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                            .GetValue(kj, null);
                            ((IDictionary<String, Object>)objectToReturn).Add(field.Trim(), fieldValue);
                        }
                        catch
                        {
                        }
                    }

                    lsobjectToReturn.Add(objectToReturn);
                }
            }
            return lsobjectToReturn;
        }
    }
}
