using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Pay;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.C.MiniApp.Api.Core.Response.Pay;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface IPayService
    {
        /// <summary>
        /// 确认支付
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<ConfirmPayResponse>> ConfirmPay(ApiRequest<ConfirmPayRequest> request);
        /// <summary>
        /// 关闭支付
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> ClosePay(ApiRequest<ClosePayRequest> request);

        /// <summary>
        /// 微信转账到支付宝账户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<WeChatTransferAliPayResponse> WeChatTransferAliPay(WeChatTransferAliPayRequest request);
    }
}
