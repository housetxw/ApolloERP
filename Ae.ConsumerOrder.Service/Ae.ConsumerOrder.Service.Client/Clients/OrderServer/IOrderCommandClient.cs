using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Core.Request.OrderCommand;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    /// <summary>
    /// 主订单操作客户端
    /// </summary>
    public interface IOrderCommandClient
    {
        /// <summary>
        /// 同步订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> SyncOrder(SyncOrderRequest request);

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<long>> UpdateOrderStatus(UpdateOrderStatusRequest request);

        /// <summary>
        /// 修改订单主状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderMainStatus(UpdateOrderMainStatusRequest request);

        /// <summary>
        /// 更新订单库存状态
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

        /// <summary>
        /// 订单逆向通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> OrderReverseNotify(OrderReverseNotifyRequest request);
        /// <summary>
        /// 修改订单预约状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderReserveStatus(UpdateOrderReserveStatusClientRequest request);
    }
}
