using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using Ae.C.Login.Api.Client.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.Login.Api.Common.Constant;
using Ae.C.Login.Api.Common.Extension;
using Ae.C.Login.Api.Core.Enums;

namespace Ae.C.Login.Api.Client.Clients.WeChatService
{
    public class WeChatClient : IWeChatClient
    {
        private readonly ApolloErpLogger<WeChatClient> logger;
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        private readonly string className;

        public WeChatClient(IHttpClientFactory clientFactory, IConfiguration configuration, ApolloErpLogger<WeChatClient> logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            this.logger = logger;
            className = this.GetType().Name;
        }

        /// <summary>
        /// 获取微信openid
        /// </summary>
        /// <param name="jsCode"></param>
        /// <returns></returns>
        public async Task<GetJsCodeResponse> GetJscode2session(LoginPlatform platform, string jsCode)
        {
            GetJsCode2SessionRequest request = new GetJsCode2SessionRequest();
            string openId = "";
            try
            {
                //创建调用client
                var client = clientFactory.CreateClient("WebChat");
                //整合调用参数
                request.Js_Code = jsCode;
                if (platform== LoginPlatform.YangChe)
                {
                    request.AppId = configuration["Mini:YangChe:AppId"];
                    request.Secret = configuration["Mini:YangChe:Secret"];
                    request.Grant_Type = configuration["Mini:YangChe:Grant_Type"];
                }
                else if(platform == LoginPlatform.CheBang)
                {
                    request.AppId = configuration["Mini:CheBang:AppId"];
                    request.Secret = configuration["Mini:CheBang:Secret"];
                    request.Grant_Type = configuration["Mini:CheBang:Grant_Type"];
                }
                else if(platform == LoginPlatform.QiPei)
                {
                    request.AppId = configuration["Mini:QiPei:AppId"];
                    request.Secret = configuration["Mini:QiPei:Secret"];
                    request.Grant_Type = configuration["Mini:QiPei:Grant_Type"];
                }

                //调微信接口
                openId = await client.GetAsStringAsync<GetJsCode2SessionRequest>(configuration["WebChat:jscode2session"], request);
                GetJsCodeResponse result = JsonConvert.DeserializeObject<GetJsCodeResponse>(openId);
                return result;
            }
            catch (Exception ex)
            {
                logger.Error($"微信GetJscode2session:Request:{JsonConvert.SerializeObject(request)}, openId={openId}", ex);
                return null;
            }
        }

        public async Task<object> GetWxacodeScene(GetWxacodeSceneRequest req)
        {
            ApiResult<object> res = new ApiResult<object>();

            try
            {
                res = await clientFactory.CreateClient("ActivityServer")
                    .GetAsJsonAsync<GetWxacodeSceneRequest, ApiResult<object>>
                        (configuration["ActivityServer:GetWxacodeScene"], req);

                if (res.Code != ResultCode.Success)
                {
                    logger.Warn(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeFailed));
                    return res.Data;
                }
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeException), e);
            }
            finally
            {
                logger.Info($"API: {className}.GetWxacodeScene 请求参数：{JsonConvert.SerializeObject(req)}");
                logger.Info($"API: {className}.GetWxacodeScene 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res.Data;
        }

    }
}
