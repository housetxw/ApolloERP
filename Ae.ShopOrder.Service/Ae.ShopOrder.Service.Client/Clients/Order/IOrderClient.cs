using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.Order;
using Ae.ShopOrder.Service.Client.Request.Order;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Request.OrderForC;

namespace Ae.ShopOrder.Service.Client.Clients.Order
{
    /// <summary>
    /// 订单中心基本服务
    /// </summary>
    public interface IOrderClient
    {
        Task<ApiPagedResult<OrderDTO>> GetOrderBaseInfoForBusinessStatus(
            GetOrderBaseInfoForBusinessStatusRequest request);

        /// <summary>
        /// 得到订单基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<OrderDTO>>> GetOrderBaseInfo(GetOrderBaseInfoRequest request);

        /// <summary>
        /// 订单产品详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<OrderProductDTO>>> GetOrderProduct(GetOrderProductRequest request);

        /// <summary>
        /// 同步订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> SyncOrder(SyncOrderRequest request);

        /// <summary>
        /// 取消订单为预约或到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> CancelOrderForReserverOrArrival(CancelOrderForReserverOrArrivalRequest request);

        Task<ApiResult> BatchUpdatePayStatus(BatchUpdatePayStatusRequest request);

        /// <summary>
        /// 订单逆向通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> OrderReverseNotify(OrderReverseNotifyRequest request);

        /// <summary>
        /// 修改订单完结状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> BatchUpdateCompleteStatus(BatchUpdateCompleteStatusRequest request);


        /// <summary>
        /// 更新订单的派工状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderDispatchStatus(UpdateOrderDispatchStatusRequest request);

        /// <summary>
        /// 修改订单安装状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> BatchUpdateInstallStatus(BatchUpdateInstallStatusRequest request);


        /// <summary>
        /// 更新商品库存状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateProductStockStatus(UpdateProductStockStatusRequest request);

        /// <summary>
        /// 更新订单库存状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderStockStatus(UpdateOrderStockStatusRequest request);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request);

        /// <summary>
        /// 修改订单的支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdatePayStatus(UpdatePayStatusRequest request);

        /// <summary>
        /// 修改订单的到账状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateMoneyArriveStatus(UpdateMoneyArriveStatusRequest request);

        /// <summary>
        /// 更改订单的状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<long>> UpdateOrderStatus(UpdateOrderStatusRequest request);

        Task<ApiResult> UpdateOrderCompleteStatus(ApiRequest<UpdateCompleteStatusRequest> request);

        Task<ApiPagedResult<OrderDTO>> GetNotPayHaveStockOrder(GetNotPayOrderRequest request);
        
    }
}
