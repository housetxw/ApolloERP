using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Core.Request;
using Ae.B.Order.Api.Core.Request.OrderCommand;
using Ae.B.Order.Api.Core.Request.OrderQuery;
using Ae.B.Order.Api.Core.Response;
using Ae.B.Order.Api.Core.Response.OrderCommand;

namespace Ae.B.Order.Api.Core.Interfaces
{
    public interface IOrderCommandService
    {
        Task<ApiResult<CreateReverseOrderBaseResponse>> CancelOrder(ApiRequest<CancelOrderRequest> request);
        Task<ApiResult<CreateReverseOrderBaseResponse>> RefundApply(ApiRequest<RefundApplyRequest> request);
        Task<ApiResult> CheckOrder(ApiRequest<CheckOrderRequest> request);
        Task<ApiResult<AppendSubmitOrderResponse>> AppendSubmitOrder(ApiRequest<AppendSubmitOrderRequest> request);

        Task<ApiResult<SubmitOrderResponse>> SubmitOrder(ApiRequest<SubmitOrderRequest> request);

        Task<ApiResult> UpdateOrderAddress(ApiRequest<UpdateOrderAddressRequest> request);

        Task<ApiResult<long>> UpdateOrderSignStatus(UpdateOrderStatusRequest request);
        Task<ApiResult<bool>> UpdateCouponAmount(UpdateCouponAmountRequest request);

        Task<ApiResult> SaveVerificationRule(ApiRequest<SaveVerificationRuleRequest> request);

        Task<ApiResult> SaveBeautiyOrPackageCardVerificationProduct(ApiRequest<SaveBeautiyOrPackageCardVerificationProductRequest> request);

        Task<ApiResult> UpdateOrderPackage( ApiRequest<UpdateOrderPackageRequest> request);
        Task<ApiResult> UpdateOrderProductCostPrice(ApiRequest<BatchUpdateCompleteStatusRequest> request);
        Task<ApiResult> UpdateOrderProductActualPayAmount(ApiRequest<BatchUpdateCompleteStatusRequest> request);
        /// <summary>
        /// 批次更新订单产品的成本价格\优惠和实付金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> BatchUpdateOrderProductCostPriceActualPayAmount(ApiRequest<BatchUpdateOrderRequest> request);
        Task<ApiResult> BatchUpdateEmployeePerformanceOrder(ApiRequest<BatchUpdateOrderRequest> request);
    }
}
