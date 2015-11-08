using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinCommon
{
    public static class StringExtensions
    {
        /// <summary>
        /// 用DataContract反序列化json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonTo<T>(this string json)
        {
            return JsonTo<T>(json, Encoding.UTF8);
        }

        /// <summary>
        /// 用DataContract反序列化json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonTo<T>(string json, Encoding encoding)
        {
            using (Stream s = new MemoryStream(encoding.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(s);
            }
        }
    }
}
