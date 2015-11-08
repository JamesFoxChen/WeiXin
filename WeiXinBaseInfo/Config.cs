using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinBaseInfo
{
    /// <summary>
    /// 微信公众号配置
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 公众号token(这个是在后台配置的, 用来管理腰间配置的token, 并不是换取的token)
        /// </summary>
        public static readonly string WeiXinToken = ConfigurationManager.AppSettings["WeiXinToken"];

        /// <summary>
        /// 微信公众号(AppID)
        /// </summary>
        public static readonly string AppID = ConfigurationManager.AppSettings["AppId"];

        /// <summary>
        /// 应用密钥(AppSecret)
        /// </summary>
        public static readonly string AppSecret = ConfigurationManager.AppSettings["AppSecret"];

        /// <summary>
        /// 商户号(在微信支付一栏中可以看到)
        /// </summary>
        public static readonly string MerchantID = ConfigurationManager.AppSettings["MerchantId"];

        /// <summary>
        /// 商户密钥
        /// </summary>
        public static readonly string PartnerKey = ConfigurationManager.AppSettings["PartnerKey"];

        /// <summary>
        /// 标识(可自己定义, 用来存储获取到当前用户openid的cookie标识)
        /// </summary>
        public static readonly string OpenIDCookie = ConfigurationManager.AppSettings["WxOpenID"];

        /// <summary>
        /// 微信公众号系统服务器(开发者中心配置的服务器地址)
        /// </summary>
        public static readonly string WeiXinServer = ConfigurationManager.AppSettings["WeiXinServer"];
    }
}
