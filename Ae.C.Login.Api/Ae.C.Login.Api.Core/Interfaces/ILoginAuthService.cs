using Ae.C.Login.Api.Core.Model;
using Ae.C.Login.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.Login.Api.Core.Interfaces
{
    /// <summary>
    /// 登录认证接口
    /// </summary>
    public interface ILoginAuthService
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="refreshUri"></param>
        /// <returns></returns>
        TokenInfoVO GetTokenInfo(UserInfoVO user,string refreshUri);
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="refreshUri"></param>
        /// <returns></returns>
        TokenInfoVO RefreshToken(string refreshToken, string refreshUri);
    }
}
