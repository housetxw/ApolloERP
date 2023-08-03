
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Request.Order;

namespace Ae.Order.Service.Core.Interfaces
{
    /// <summary>
    /// 订单操作
    /// </summary>
    public interface IOrderCommandService
    {
        /// <summary>
        /// 更新订单子状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<long>> UpdateOrderStatus(UpdateOrderStatusRequest request);

        /// <summary>
        /// 同步订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> SyncOrder(SyncOrderRequest request);

        /// <summary>
        /// 修改订单主状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderMainStatus(UpdateOrderMainStatusRequest request);

        /// <summary>
        /// 修改订单库存状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderStockStatus(UpdateOrderStockStatusRequest request);

        /// <summary>
        /// 更新商品库存状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateProductStockStatus(UpdateProductStockStatusRequest request);

        /// <summary>
        /// 修改订单支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdatePayStatus(UpdatePayStatusRequest request);

        /// <summary>
        /// 修改订单优惠金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> UpdateCouponAmount(UpdateCouponAmountRequest request);

        /// <summary>
        /// 修改支付到账状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateMoneyArriveStatus(UpdateMoneyArriveStatusRequest request);

        /// <summary>
        /// 更新订单商品成本价
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateProductTotalCostPrice(UpdateProductTotalCostPriceRequest request);
        Task<ApiResult> OrderReverseNotify(OrderReverseNotifyRequest request);
        /// <summary>
        /// 修改订单预约状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderReserveStatus(UpdateOrderReserveStatusRequest request);

        /// <summary>
        /// 取消订单为预约或到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> CancelOrderForReserverOrArrival(CancelOrderForReserverOrArrivalRequest request);

        /// <summary>
        /// 批量更新付款状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> BatchUpdatePayStatus(BatchUpdatePayStatusRequest request);

        /// <summary>
        /// 批量更新完结状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> BatchUpdateCompleteStatus(BatchUpdateCompleteStatusRequest request);

        /// 更新订单的派工状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderDispatchStatus(UpdateOrderDispatchStatusRequest request);

        /// <summary>
        /// 修改订单支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> BatchUpdateInstallStatus(BatchUpdateInstallStatusRequest request);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request);

        /// <summary>
        /// 完结订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderCompleteStatus( ApiRequest<UpdateCompleteStatusRequest> request);

        Task<ApiResult> UpdateOrderCar(ApiRequest<UpdateOrderCarRequest> request);
    }
}

