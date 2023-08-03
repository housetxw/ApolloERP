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
using Ae.C.MiniApp.Api.Core.Request.Pay;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Core.Response.Pay;
using Ae.C.MiniApp.Api.Filters;
using Rg.Pay.Service.Core.Enums.Pay;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 支付中心
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(PayController))]
    public class PayController : ControllerBase
    {
        private readonly ApolloErpLogger<PayController> logger;
        private readonly IPayService payService;

        public PayController(ApolloErpLogger<PayController> logger, IPayService payService)
        {
            this.logger = logger;
            this.payService = payService;
        }

        /// <summary>
        /// 确认支付
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<ConfirmPayResponse>> ConfirmPay([FromBody]ApiRequest<ConfirmPayRequest> request)
        {
            return await payService.ConfirmPay(request);
        }

        /// <summary>
        /// 关闭支付
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> ClosePay([FromBody]ApiRequest<ClosePayRequest> request)
        {
            return await payService.ClosePay(request);
        }

        /// <summary>
        /// 微信转账到支付宝账户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<WeChatTransferAliPayResponse>> WeChatTransferAliPay(
            [FromBody] ApiRequest<WeChatTransferAliPayRequest> request)
        {
            var result = await payService.WeChatTransferAliPay(request.Data);

            return Result.Success(result);
        }
    }
}
