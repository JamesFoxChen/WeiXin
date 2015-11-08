using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinBaseInfo
{
    /// <summary>
    /// 公众号令牌
    /// </summary>
    [DataContract]
    public class AccessTokenInfo
    {
        /// <summary>
        /// 公众号令牌
        /// </summary>
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }
    }
}
