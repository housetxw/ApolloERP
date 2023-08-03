using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 微信支付
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(WxPayController))]
    public class WxPayController : ControllerBase
    {
        private readonly ApolloErpLogger<WxPayController> logger;
        private readonly IWxPayService wxPayService;

        public WxPayController(ApolloErpLogger<WxPayController> logger, IWxPayService wxPayService)
        {
            this.logger = logger;
            this.wxPayService = wxPayService;
        }

        /// <summary>
        /// 创建微信支付预付单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<CreateWxPrePayOrderResponse>> CreateWxPrePayOrder([FromBody]ApiRequest<CreateWxPrePayOrderRequest> request)
        {
            return await wxPayService.CreateWxPrePayOrder(request);
        }

        /// <summary>
        /// 微信支付结果通知
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> WxPayResultNotify()
        {
            var requestXml = string.Empty;
            var responseXml = string.Empty;
            try
            {
                using (var reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8))
                {
                    requestXml = reader.ReadToEnd();
                }
                responseXml = await wxPayService.WxPayResultNotify(requestXml);
            }
            catch (Exception ex)
            {
                logger.Error($"WxPayResultNotify_ApiEx requestXml={requestXml} responseXml={responseXml}", ex);
                responseXml = @"<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[EXCEPTION]]></return_msg></xml>";
            }
            finally
            {
                logger.Info($"WxPayResultNotify_Api requestXml={requestXml} responseXml={responseXml}");
            }
            return responseXml;
        }

        /// <summary>
        /// 环迅支付结果通知
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> IpsPayResultNotify()
        {
            logger.Info($"IpsPayResultNotify Start");
            using (var reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8))
            {
                var requestXml = reader.ReadToEnd();

                logger.Info($"IpsPayResultNotify  {requestXml}");
            }


            return await Task.FromResult("Ips");
        }
    }
}
