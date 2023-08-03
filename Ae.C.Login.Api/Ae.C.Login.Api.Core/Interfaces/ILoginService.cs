using Ae.C.Login.Api.Core.Request;
using Ae.C.Login.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.Login.Api.Core.Interfaces
{
    public interface ILoginService
    {
        /// <summary>
        /// 获取微信openid接口
        /// </summary>
        /// <param name="request"></param>
        /// <param name="refreshUri"></param>
        /// <returns></returns>
        Task<GetWxOpenIdResponse> GetWxOpenId(GetWxOpenIdRequest request, string refreshUri);

        /// <summary>
        /// 三方登录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="refreshUri"></param>
        /// <returns></returns>
        Task<LoginResponse> ThirtyLoginRegister(ThirtyLoginRequest request, string refreshUri);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="refreshUri"></param>
        /// <returns></returns>
        Task<LoginResponse> RgLogin(RgLoginRequest request, string refreshUri);

        /// <summary>
        /// 发送登录验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task SendLoginVerificationCodeSms(VerificationCodeSmsRequest request);
    }
}
