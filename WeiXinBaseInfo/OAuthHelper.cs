using System;
using WeiXinCommon;

namespace WeiXinBaseInfo
{
    class OAuthHelper
    {
        /// <summary>
        /// 获取微信用户基本信息
        /// </summary>
        /// <param name="openID"></param>
        /// <returns></returns>
        public static WeiXinUserInfo GetWeiXinUserInfo(String openID)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", GetAccessToken(Config.AppID, Config.AppSecret).AccessToken, openID);
            string r = Utility.GetRequest(url);
            return r.JsonTo<WeiXinUserInfo>();
        }

        internal static AccessTokenInfo GetAccessToken(string appId, string secret)
        {
            //object ticket = HttpRuntime.Cache[TokenKey];
            //if (ticket == null)
            //{
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, secret);
            string r = Utility.GetRequest(url);

            AccessTokenInfo at = r.JsonTo<AccessTokenInfo>();
            //HttpRuntime.Cache.Insert(TokenKey, at, null, DateTime.Now.AddSeconds(at.ExpiresIn), System.Web.Caching.Cache.NoSlidingExpiration);
            return at;

            //}
            //return ticket as AccessTokenInfo;
        }
    }
}
