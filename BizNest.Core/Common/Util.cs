using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

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

        public static string CleanText(string name)
        {
            var t = new StringBuilder(name);
            t.Replace("\"", "");
            t.Replace("\'", "");
            t.Replace("/", "");
            t.Replace("\\", "");
            t.Replace("?", "");
            t.Replace("!", "");
            t.Replace("?", "");
            return t.ToString();
        }

        public static string TimeStampCode(string prefix = "")
        {
            Thread.Sleep(1);
            string stamp = DateTime.Now.ToString("yyMMddHHmmssffffff");
            long num = long.Parse(stamp);

            var g = Guid.NewGuid().ToString().Substring(0, 3).ToUpper();
            return prefix + DecimalToArbitrarySystem(num, 36) + g;
        }


        public static string DecimalToArbitrarySystem(long decimalNumber, int radix)
        {
            const int BitsInLong = 64;
            const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (radix < 2 || radix > Digits.Length)
                throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

            if (decimalNumber == 0)
                return "0";

            int index = BitsInLong - 1;
            long currentNumber = Math.Abs(decimalNumber);
            char[] charArray = new char[BitsInLong];

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % radix);
                charArray[index--] = Digits[remainder];
                currentNumber = currentNumber / radix;
            }

            string result = new string(charArray, index + 1, BitsInLong - index - 1);
            if (decimalNumber < 0)
            {
                result = "-" + result;
            }

            return result;
        }
    }
}
