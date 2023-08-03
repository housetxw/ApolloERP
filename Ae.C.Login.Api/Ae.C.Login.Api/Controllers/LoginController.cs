using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Microsoft.AspNetCore.Mvc;
using Ae.C.Login.Api.Core.Interfaces;
using Ae.C.Login.Api.Core.Request;
using Ae.C.Login.Api.Filters;
using Ae.C.Login.Api.Core.Model;
using System.Reflection;
using System.Text;
using Ae.C.Login.Api.Imp.Helpers;
using Microsoft.AspNetCore.Cors;
using log4net;
using log4net.Repository;
using log4net.Config;
using System.IO;
using ApolloErp.Log;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Ae.C.Login.Api.Core.Response;
using Ae.C.Login.Api.Common.Exceptions;
using Newtonsoft.Json;

namespace Ae.C.Login.Api.Controllers
{
    /// <summary>
    /// 登录认证
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(LoginController))]
    public class LoginController : ControllerBase
    {
        private readonly ILoginAuthService loginAuthService;
        private readonly ApolloErpLogger<LoginController> logger;
        private readonly ILoginService loginService;


        public LoginController(ILoginAuthService loginAuthService, ILoginService loginService, ApolloErpLogger<LoginController> logger)
        {
            this.loginAuthService = loginAuthService;
            this.loginService = loginService;
            this.logger = logger;
        }
        /// <summary>
        /// test
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> GetWxOpenIdTest([FromBody] ApiRequest<GetWxOpenIdRequest> request)
        {
            try
            {
                logger.Info("测试elk");
                logger.Error("测试elk错误",new Exception("test异常"));
                return true;
            }
            catch (Exception ex)
            {
                logger.Error($"GetWxOpenId失败:Request:{JsonConvert.SerializeObject(request)}", ex);
                throw new CustomException("登录失败");
            }
        }


        /// <summary>
        /// 获取微信openid接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<GetWxOpenIdResponse>> GetWxOpenId([FromBody] ApiRequest<GetWxOpenIdRequest> request)
        {
            try
            {
                //获取openid
                GetWxOpenIdResponse openIdInfo = await loginService.GetWxOpenId(request.Data, new Uri(Request.GetUri(), Url.Action("RefreshToken")).ToString());
                return Result.Success(openIdInfo);
            }
            catch (Exception ex)
            {
                logger.Warn($"GetWxOpenId失败:Request:{JsonConvert.SerializeObject(request)}", ex);
                throw new CustomException("登录失败");
            }
        }

        /// <summary>
        /// 三方登录接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<LoginResponse>> ThirtyLogin([FromBody] ApiRequest<ThirtyLoginRequest> request)
        {
            try
            {
                LoginResponse loginInfo = await loginService.ThirtyLoginRegister(request.Data, new Uri(Request.GetUri(), Url.Action("RefreshToken")).ToString());
                return Result.Success(loginInfo);
            }
            catch (Exception ex)
            {
                logger.Error($"ThirtyLogin失败:Request:{JsonConvert.SerializeObject(request)}", ex);
                throw new CustomException("登录失败");
            }
        }

        /// <summary>
        /// 发短信
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SendVerifyCodeSms([FromBody] ApiRequest<VerificationCodeSmsRequest> request)
        {
            try
            {
                await loginService.SendLoginVerificationCodeSms(request.Data);
                return Result.Success(true);
            }
            catch (Exception ex)
            {
                logger.Error($"SendVerifyCodeSms失败:Request:{JsonConvert.SerializeObject(request)}", ex);
                throw new CustomException("短信发送失败");
            }

        }

        /// <summary>
        /// 用户手机登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<LoginResponse>> RgLogin([FromBody] ApiRequest<RgLoginRequest> request)
        {
            try
            {
                LoginResponse loginInfo = await loginService.RgLogin(request.Data, new Uri(Request.GetUri(), Url.Action("RefreshToken")).ToString());
                return Result.Success(loginInfo);
            }
            catch (Exception ex)
            {
                logger.Error($"RgLogin失败:Request:{JsonConvert.SerializeObject(request)}", ex);
                throw new CustomException("登录失败");
            }
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<TokenInfoVO>> RefreshToken([FromBody] ApiRequest<RefreshTokenRequest> request)
        {
            try
            {
                TokenInfoVO tokenInfo = loginAuthService.RefreshToken(request.Data.RefreshToken, new Uri(Request.GetUri(), Url.Action("RefreshToken")).ToString());
                return Result.Success(tokenInfo);
            }
            catch (Exception ex)
            {
                logger.Error($"RefreshToken失败:Request:{JsonConvert.SerializeObject(request)}", ex);
                throw new CustomException("刷新token失败");
            }

        }






    }
}
