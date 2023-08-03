using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.ConsumerOrder.Service.Core.Request.OrderCommand;
using Ae.ConsumerOrder.Service.Core.Request.SharingPromotion;

namespace Ae.ConsumerOrder.Service.Core.Interfaces
{
    public interface IOrderCommandService
    {
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<SubmitOrderResponse>> SubmitOrder(ApiRequest<SubmitOrderRequest> request);

        /// <summary>
        /// 购买套餐卡订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<SubmitOrderResponse>> SubmitBuyPackageOrder(ApiRequest<SubmitOrderRequest> request);
        /// <summary>
        /// 购买套餐卡订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<SubmitOrderResponse>> UseBuyPackageOrder(ApiRequest<SubmitOrderRequest> request);
        /// <summary>
        /// 购买套餐卡订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<SubmitOrderResponse>> SecKillOrder(ApiRequest<SubmitOrderRequest> request);
        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<long>> UpdateOrderStatus(UpdateOrderStatusRequest request);
        /// <summary>
        /// 审核订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> CheckOrder(CheckOrderRequest request);
        /// <summary>
        /// 订单支付成功通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> OrderPaySuccessNotify(OrderPaySuccessNotifyRequest request);
        /// <summary>
        /// 提交使用核销码产生的订单（即ProduceType=2使用核销码产生）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<SubmitUseVerificationCodeOrderResponse>> SubmitUseVerificationCodeOrder(ApiRequest<SubmitUseVerificationCodeOrderRequest> request);
        /// <summary>
        /// 订单逆向通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> OrderReverseNotify(OrderReverseNotifyRequest request);
        /// <summary>
        /// 再次购买
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<BuyAgainResponse>> BuyAgain(ApiRequest<BuyAgainRequest> request);
        /// <summary>
        /// 追加下单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<AppendSubmitOrderResponse>> AppendSubmitOrder(ApiRequest<AppendSubmitOrderRequest> request);
        /// <summary>
        /// 更新订单的产品成本
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderProductCost(UpdateOrderProductRequest request);
        /// <summary>
        /// 催促订单付款
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> UrgeOrderPayment();
        /// <summary>
        /// 自动取消超时订单
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> AutoCancelTimeOutOrder();
        /// <summary>
        /// 安装完成订单评价提醒
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        Task<ApiResult> InstalledOrderCommentRemind(string orderNo);
        /// <summary>
        /// 修改订单预约状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderReserveStatus(UpdateOrderReserveStatusRequest request);

        /// <summary>
        /// 更新订单的派工状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderDispatchStatus(UpdateOrderDispatchStatusRequest request);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> CancelOrder(ApiRequest<CancelOrderRequest> request);

        /// <summary>
        /// 更改订单支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdatePayStatus(UpdatePayStatusRequest request);

        /// <summary>
        /// 更改到账状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateMoneyArriveStatus(UpdateMoneyArriveStatusRequest request);

        Task<ApiResult> UpdateOrderCompleteStatus( ApiRequest<UpdateCompleteStatusRequest> request);

        Task<ApiResult> SharingPromotion(SharingPromotionRequest request);
    }
}
