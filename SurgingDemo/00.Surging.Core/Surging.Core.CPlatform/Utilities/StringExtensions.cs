using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace Surging.Core.CPlatform.Utilities
{
    public static class StringExtensions
    {
        public static bool IsIP(this string input) => input.IsMatch(@"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\:\d{2,5}\b");

        public static bool IsMatch(this string str, string op)
        {
            if (str.Equals(String.Empty) || str == null) return false;
            var re = new Regex(op, RegexOptions.IgnoreCase);
            return re.IsMatch(str);
        }
        public static bool IsValidJson(this string strInput)
        {
            if (string.IsNullOrEmpty(strInput))
            {
                throw new ArgumentNullException(nameof(strInput));
            }

            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    // :todo 日志
                    //Exception in parsing json
                    return false;
                }
                catch (Exception ex) //some other exception
                {

                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}