using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Request.Pay;
using Ae.C.MiniApp.Api.Client.Response.Pay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IPayClient
    {
        /// <summary>
        /// 确认支付
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<ConfirmPayClientResponse>> ConfirmPay(ConfirmPayClientRequest request);
        /// <summary>
        /// 关闭支付
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> ClosePay(ClosePayClientRequest request);

        /// <summary>
        /// 企业付款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<EnterprisePayClientResponse> EnterprisePay(EnterprisePayClientRequest request);

        /// <summary>
        /// 微信转账到支付宝账户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<WeChatTransferAliPayClientResponse> WeChatTransferAliPay(
            WeChatTransferAliPayClientRequest request);
    }
}
