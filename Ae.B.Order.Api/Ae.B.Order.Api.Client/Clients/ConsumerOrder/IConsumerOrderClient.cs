using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Model;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Client.Response;
using Ae.B.Order.Api.Core.Request.OrderQuery;
using Ae.B.Order.Api.Core.Response.OrderQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Order.Api.Client.Clients
{
    public interface IConsumerOrderClient
    {
        /// <summary>
        /// 获取BOSS订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetOrderDetailForBossClientResponse>> GetOrderDetailForBoss(GetOrderDetailForBossClientRequest request);
        /// <summary>
        /// 审核确认订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> CheckOrder(CheckOrderClientRequest request);
        /// <summary>
        /// 追加下单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<AppendSubmitOrderClientResponse>> AppendSubmitOrder(ApiRequest<AppendSubmitOrderClientRequest> request);
        /// <summary>
        /// 订单日志列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<OrderLogDTO>> GetOrderLogList(GetOrderLogListClientRequest request);

        /// <summary>
        /// 获取首次加载订单确认页信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetOrderConfirmResponse>> GetOrderConfirm(ApiRequest<GetOrderConfirmRequest> request);

        /// <summary>
        /// 试算订单金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<TrialCalcOrderAmountResponse>> TrialCalcOrderAmount(TrialCalcOrderAmountRequest request);
    }
}
