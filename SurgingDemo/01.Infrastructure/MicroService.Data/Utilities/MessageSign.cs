using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MicroService.Data.Utilities
{
    public class MessageSign
    {
        /// <summary>
        /// 使用Secret对指定的对象进行签名
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="hashedSecret"></param>
        /// <returns></returns>
        public static string Sign(object obj, string hashedSecret)
        {
            // 1 提取出obj中除了Sign之外的所有属性，进行序列化处理
            var props = obj.GetType().GetProperties().Where(x => !"Sign".Equals(x.Name, StringComparison.OrdinalIgnoreCase));

            // 2 将各个属性序列化之后的字符串放入数组并进行排序
            var list = new List<string>();
            foreach (var prop in props)
            {
                var val = prop.GetValue(obj);
                if (val == null)
                    continue;
                list.Add(ToJsonString(val));
            }

            // 3 排序之后对数组进行展开连接
            list.Sort();
            var text = string.Join(',', list) + hashedSecret;

            // 4 对展开的字符串进行hash
            return MD5Hash(text);
        }
        /// netcore下的实现MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }


        /// <summary>
        /// 对象转换为Json字符串
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToJsonString(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

    }
}
