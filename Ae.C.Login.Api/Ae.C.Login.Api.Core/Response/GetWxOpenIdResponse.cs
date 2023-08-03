using Ae.C.Login.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.Login.Api.Core.Response
{
    /// <summary>
    /// 获取微信开放openid返回
    /// </summary>
    public class GetWxOpenIdResponse
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfoVO UserInfo { get; set; }
        /// <summary>
        /// 登录token信息
        /// </summary>
        public TokenInfoVO TokenInfo { get; set; }
        /// <summary>
        /// 是否存在该用户
        /// </summary>
        public Boolean IsExistUser { get; set; }
        /// <summary>
        /// 微信用户openid
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 联合ID唯一
        /// </summary>
        public string UnionId { get; set; }

    }
}
