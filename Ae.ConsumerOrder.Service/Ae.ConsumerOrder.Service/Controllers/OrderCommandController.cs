using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Common.Extension;
using Ae.ConsumerOrder.Service.Core.Enums;
using Ae.ConsumerOrder.Service.Core.Interfaces;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Core.Request.OrderCommand;
using Ae.ConsumerOrder.Service.Core.Request.SharingPromotion;
using Ae.ConsumerOrder.Service.Core.Response;
using Ae.ConsumerOrder.Service.Filters;

namespace Ae.ConsumerOrder.Service.Controllers
{
    /// <summary>
    /// 订单操作
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(OrderCommandController))]
    public class OrderCommandController : ControllerBase
    {
        private readonly IOrderCommandService orderCommandService;

        /// <summary>
        /// 依赖注入构造函数
        /// </summary>
        /// <param name="orderCommandService"></param>
        public OrderCommandController(IOrderCommandService orderCommandService)
        {
            this.orderCommandService = orderCommandService;
        }

        /// <summary>
        /// 提交订单（0普通产生或1购买核销码产生）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<SubmitOrderResponse>> SubmitOrder([FromBody] ApiRequest<SubmitOrderRequest> request)
        {
            if (request.Data.ProduceType == ProductTypeEnum.BuyPackageCard.ToSbyte())
            {
                return await orderCommandService.SubmitBuyPackageOrder(request);
            }
            else if (request.Data.ProduceType == ProductTypeEnum.UsePackageCard.ToSbyte())
            {
                return await orderCommandService.UseBuyPackageOrder(request);
            }
            else if (request.Data.ProduceType == ProductTypeEnum.SeckillOrder.ToSbyte())
            {
                return await orderCommandService.SecKillOrder(request);
            }
            else
            {
                return await orderCommandService.SubmitOrder(request);
            }
        }

        /// <summary>
        /// 再次购买
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Obsolete("流程调整暂不上线")]
        [HttpPost]
        public async Task<ApiResult<BuyAgainResponse>> BuyAgain([FromBody] ApiRequest<BuyAgainRequest> request)
        {
            return await orderCommandService.BuyAgain(request);
        }

        /// <summary>
        /// 更新订单子状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<long>> UpdateOrderStatus([FromBody] UpdateOrderStatusRequest request)
        {
            return await orderCommandService.UpdateOrderStatus(request);
        }

        /// <summary>
        /// 审核确认订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CheckOrder([FromBody] CheckOrderRequest request)
        {
            return await orderCommandService.CheckOrder(request);
        }

        /// <summary>
        /// 订单支付成功通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> OrderPaySuccessNotify([FromBody] OrderPaySuccessNotifyRequest request)
        {
            return await orderCommandService.OrderPaySuccessNotify(request);
        }

        /// <summary>
        /// 提交使用核销码产生的订单（即ProduceType=2使用核销码产生）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<SubmitUseVerificationCodeOrderResponse>> SubmitUseVerificationCodeOrder([FromBody] ApiRequest<SubmitUseVerificationCodeOrderRequest> request)
        {
            return await orderCommandService.SubmitUseVerificationCodeOrder(request);
        }

        /// <summary>
        /// 订单逆向通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> OrderReverseNotify([FromBody] OrderReverseNotifyRequest request)
        {
            return await orderCommandService.OrderReverseNotify(request);
        }

        /// <summary>
        /// 追加下单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<AppendSubmitOrderResponse>> AppendSubmitOrder([FromBody] ApiRequest<AppendSubmitOrderRequest> request)
        {
            return await orderCommandService.AppendSubmitOrder(request);
        }

        /// <summary>
        /// 更新订单的成本
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderProductCost([FromBody] UpdateOrderProductRequest request)
        {
            return await orderCommandService.UpdateOrderProductCost(request);
        }

        /// <summary>
        /// JOB催促订单付款（目前超时前30分钟）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UrgeOrderPayment()
        {
            return await orderCommandService.UrgeOrderPayment();
        }

        /// <summary>
        /// JOB自动取消超时订单（目前24小时超时）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AutoCancelTimeOutOrder()
        {
            return await orderCommandService.AutoCancelTimeOutOrder();
        }

        /// <summary>
        /// 修改订单预约状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderReserveStatus([FromBody] UpdateOrderReserveStatusRequest request)
        {
            return await orderCommandService.UpdateOrderReserveStatus(request);
        }

        /// 更新订单的派工状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderDispatchStatus([FromBody] UpdateOrderDispatchStatusRequest request)
        {
            return await orderCommandService.UpdateOrderDispatchStatus(request);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CancelOrder([FromBody] ApiRequest<CancelOrderRequest> request)
        {
            return await orderCommandService.CancelOrder(request);
        }

        /// <summary>
        /// 修改订单支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdatePayStatus([FromBody] UpdatePayStatusRequest request)
        {
            return await orderCommandService.UpdatePayStatus(request);
        }

        /// <summary>
        /// 修改支付到账状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateMoneyArriveStatus([FromBody] UpdateMoneyArriveStatusRequest request)
        {
            return await orderCommandService.UpdateMoneyArriveStatus(request);
        }

        /// <summary>
        /// 修改订单完结状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderCompleteStatus([FromBody] ApiRequest<UpdateCompleteStatusRequest> request)
        {
            return await orderCommandService.UpdateOrderCompleteStatus(request);
        }

        /// <summary>
        /// 订单完结发放促销优惠卷-当分享人达到10人下保养订单时即发放指定优惠券
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> SharingPromotion([FromQuery] SharingPromotionRequest request)
        {
            return await orderCommandService.SharingPromotion(request);
        }
    }
}
