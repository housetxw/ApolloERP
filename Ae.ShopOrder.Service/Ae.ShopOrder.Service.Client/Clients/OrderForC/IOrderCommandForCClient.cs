using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Request.OrderForC;

namespace Ae.ShopOrder.Service.Client.Clients.OrderForC
{
    /// <summary>
    /// 订单操作ForC
    /// </summary>
    public interface IOrderCommandForCClient
    {
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<long>> UpdateOrderStatus(UpdateOrderStatusRequest request);


        Task<ApiResult> UpdateOrderDispatchStatus(UpdateOrderDispatchStatusRequest request);

        Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request);

        Task<ApiResult> UpdatePayStatus(UpdatePayStatusRequest request);

        Task<ApiResult> UpdateMoneyArriveStatus(UpdateMoneyArriveStatusRequest request);

        Task<ApiResult> UpdateOrderCompleteStatus(ApiRequest<UpdateCompleteStatusRequest> request);

        Task<ApiResult> SharingPromotion(SharingPromotionRequest request);

    }
}
