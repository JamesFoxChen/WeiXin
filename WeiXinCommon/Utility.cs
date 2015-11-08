using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeiXinCommon
{
    public class Utility
    {
        /// <summary>
        /// MD5信息
        /// </summary>
        private static MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        /// <summary>
        /// 供创建随机字符串使用
        /// </summary>
        private static string[] strs = new string[]
                                 {
                                  "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                                  "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
                                 };

        ///// <summary>
        ///// 生成签名
        ///// 签名在线验证工具：
        ///// </summary>
        ///// <param name="dic">参与签名生成的参数列表</param>
        ///// <returns></returns>
        //public static string Sign(IDictionary<string, string> dic)
        //{
        //    return Sign(dic, Config.PartnerKey);
        //}

        /// <summary>
        /// 生成签名
        /// 签名在线验证工具：
        /// http://mch.weixin.qq.com/wiki/tools/signverify/
        /// </summary>
        /// <param name="dic">参与签名生成的参数列表</param>
        /// <param name="partnerKey">商家私钥</param>
        /// <returns></returns>
        public static string Sign(IDictionary<string, string> dic, string partnerKey)
        {
            var sb = new StringBuilder();
            foreach (var sA in dic.OrderBy(x => x.Key))//参数名ASCII码从小到大排序（字典序）；
            {
                if (sA.Key == "sign") continue;
                if (string.IsNullOrEmpty(sA.Value)) continue;//参数的值为空不参与签名；
                sb.Append(sA.Key).Append("=").Append(sA.Value).Append("&");
            }
            var string1 = sb.ToString();
            string1 = string1.Remove(string1.Length - 1, 1);
            sb.Append("key=").Append(partnerKey);//在stringA最后拼接上key=(API密钥的值)得到stringSignTemp字符串
            var stringSignTemp = sb.ToString();
            var sign = Utility.MD5(stringSignTemp).ToUpper();//对stringSignTemp进行MD5运算，再将得到的字符串所有字符转换为大写，得到sign值signValue。 
            return sign;
        }

        /// <summary>
        /// 生成POST的xml数据字符串
        /// </summary>
        /// <param name="postdataDict">>参与生成的参数列表</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static string GeneralPostdata(IDictionary<string, string> postdataDict, string sign)
        {
            var sb2 = new StringBuilder();
            sb2.Append("<xml>");
            foreach (var sA in postdataDict.OrderBy(x => x.Key))//参数名ASCII码从小到大排序（字典序）；
            {
                sb2.Append("<" + sA.Key + ">")
                   .Append(System.Web.HttpUtility.HtmlEncode(sA.Value))//参数值用XML转义即可，CDATA标签用于说明数据不被XML解析器解析。 
                   .Append("</" + sA.Key + ">");
            }
            sb2.Append("<sign>").Append(sign).Append("</sign>");
            sb2.Append("</xml>");
            return sb2.ToString();
        }

        /// <summary>
        /// 获取post数据
        /// </summary>
        /// <returns></returns>
        public static String GetPostString()
        {
            Stream s = HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            HttpContext.Current.Request.InputStream.Seek(0, SeekOrigin.Begin);
            return System.Text.Encoding.UTF8.GetString(b);
        }

        /// <summary>
        /// 创建时间戳
        /// </summary>
        /// <returns></returns>
        public static long ToTimestamp(System.DateTime dt)
        {
            return (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        /// <summary>
        /// 获取字符串的MD5值
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <returns></returns>
        public static string MD5(string value)
        {
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(value))).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Sha1
        /// </summary>
        /// <param name="orgStr"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Sha1(string orgStr, string encode = "UTF-8")
        {
            var sha1 = new SHA1Managed();
            var sha1bytes = System.Text.Encoding.GetEncoding(encode).GetBytes(orgStr);
            byte[] resultHash = sha1.ComputeHash(sha1bytes);
            string sha1String = BitConverter.ToString(resultHash).ToLower();
            sha1String = sha1String.Replace("-", "");
            return sha1String;
        }

        /// <summary>
        /// 创建随机字符串
        /// </summary>
        /// <returns></returns>
        public static string CreateNonceStr()
        {
            Random r = new Random();
            var sb = new StringBuilder();
            var length = strs.Length;
            for (int i = 0; i < 15; i++)
            {
                sb.Append(strs[r.Next(length - 1)]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static string GetRequest(string url)
        {
            HttpResponseMessage result = null;
            try
            {
                result = new HttpClient().GetAsync(url).Result;
            }
            catch (Exception ex)
            {
                //LogHelper.Log(ex, "get请求的url:" + url);
                throw ex;
            }
            if (!result.IsSuccessStatusCode) return string.Empty;
            return result.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static string PostRequest(string url, string data)
        {
            HttpResponseMessage result = null;
            try
            {
                result = new HttpClient().PostAsync(url, new StringContent(data)).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (!result.IsSuccessStatusCode) return string.Empty;
            return result.Content.ReadAsStringAsync().Result;
        }
    }
}
