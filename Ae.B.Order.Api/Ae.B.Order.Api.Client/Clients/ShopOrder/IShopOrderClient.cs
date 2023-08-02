using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Model;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Client.Response;
using Ae.B.Order.Api.Core.Request;
using Ae.B.Order.Api.Core.Request.OrderCommand;
using Ae.B.Order.Api.Core.Response.OrderCommand;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Order.Api.Core.Request.OrderQuery;
using Ae.B.Order.Api.Core.Response.OrderQuery;
using Ae.B.Order.Api.Core.Model.OrderDetail;

namespace Ae.B.Order.Api.Client.Clients.ShopOrder
{
    public interface IShopOrderClient
    {
        /// <summary>
        /// 获取BOSS订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetOrderDetailForBossClientResponse>> GetOrderDetailForBoss(GetOrderDetailForBossClientRequest request);
        Task<ApiPagedResult<OrderLogDTO>> GetOrderLogList(GetOrderLogListClientRequest request);

        Task<ApiResult<SubmitOrderResponse>> SubmitOrder(ApiRequest<SubmitOrderRequest> request);

        Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request);

        Task<ApiResult> UpdateOrderAddress(ApiRequest<UpdateOrderAddressRequest> request);

        Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReport(
            GetOrderDetailStaticReportRequest request);

        Task<ApiPagedResult<VerificationRuleDTO>> GetVerificationRule(GetVerificationRuleRequest request);

        Task<ApiResult> SaveVerificationRule(ApiRequest<SaveVerificationRuleRequest> request);

        Task<ApiResult> SaveBeautiyOrPackageCardVerificationProduct(ApiRequest<SaveBeautiyOrPackageCardVerificationProductRequest> request);

        Task<ApiPagedResult<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfitReport(
            ApiRequest<GetOrderOutProductsProfitRequest> request);

        Task<ApiPagedResult<OrderProductNewDTO>> GetOrderProductsReport(ApiRequest<GetOrderProductsRequest> request);

        Task<ApiResult<bool>> UpdateOrderCouponAmount(UpdateCouponAmountRequest request);

        Task<ApiResult> UpdateOrderProductCostPrice(ApiRequest<BatchUpdateCompleteStatusRequest> request);
        Task<ApiResult> UpdateOrderProductActualPayAmount(ApiRequest<BatchUpdateCompleteStatusRequest> request);
        Task<ApiResult> BatchUpdateOrderProductCostPriceActualPayAmount(ApiRequest<BatchUpdateOrderRequest> request);
        Task<ApiResult> BatchUpdateEmployeePerformanceOrder(ApiRequest<BatchUpdateOrderRequest> request);
    }
}
