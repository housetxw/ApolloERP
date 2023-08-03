using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Filters;

namespace Ae.Order.Service.Controllers
{
    /// <summary>
    /// 订单基础操作服务
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(OrderCommandController))]
    public class OrderCommandController : Controller
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
        /// 同步订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SyncOrder([FromBody]SyncOrderRequest request)
        {
            return await orderCommandService.SyncOrder(request);
        }

        /// <summary>
        /// 修改订单子状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<long>> UpdateOrderStatus([FromBody]UpdateOrderStatusRequest request)
        {
            return await orderCommandService.UpdateOrderStatus(request);
        }

        /// <summary>
        /// 修改订单主状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderMainStatus([FromBody]UpdateOrderMainStatusRequest request)
        {
            return await orderCommandService.UpdateOrderMainStatus(request);
        }

        /// <summary>
        /// 更新订单库存状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderStockStatus([FromBody]UpdateOrderStockStatusRequest request)
        {
            return await orderCommandService.UpdateOrderStockStatus(request);
        }

        /// <summary>
        /// 更新商品库存状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateProductStockStatus([FromBody]UpdateProductStockStatusRequest request)
        {
            return await orderCommandService.UpdateProductStockStatus(request);
        }

        /// <summary>
        /// 修改订单支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdatePayStatus([FromBody]UpdatePayStatusRequest request)
        {
            return await orderCommandService.UpdatePayStatus(request);
        }

        /// <summary>
        /// 修改订单优惠金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateCouponAmount([FromBody] UpdateCouponAmountRequest request)
        {
            return await orderCommandService.UpdateCouponAmount(request);
        }

        /// <summary>
        /// 修改支付到账状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateMoneyArriveStatus([FromBody]UpdateMoneyArriveStatusRequest request)
        {
            return await orderCommandService.UpdateMoneyArriveStatus(request);
        }

        /// <summary>
        /// 更新订单商品成本价
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateProductTotalCostPrice([FromBody]UpdateProductTotalCostPriceRequest request)
        {
            return await orderCommandService.UpdateProductTotalCostPrice(request);
        }

        /// <summary>
        /// 订单逆向通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> OrderReverseNotify([FromBody]OrderReverseNotifyRequest request)
        {
            return await orderCommandService.OrderReverseNotify(request);
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

        /// <summary>
        /// 取消订单为预约或到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CancelOrderForReserverOrArrival([FromBody]CancelOrderForReserverOrArrivalRequest request)
        {
            return await orderCommandService.CancelOrderForReserverOrArrival(request);
        }


        /// <summary>
        ///批量 修改订单支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> BatchUpdatePayStatus([FromBody]BatchUpdatePayStatusRequest request)
        {
            return await orderCommandService.BatchUpdatePayStatus(request);
        }


        /// <summary>
        /// 批量 修改订单完结状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> BatchUpdateCompleteStatus([FromBody]BatchUpdateCompleteStatusRequest request)
        {
            return await orderCommandService.BatchUpdateCompleteStatus(request);
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
        ///批量 修改订单安装状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> BatchUpdateInstallStatus([FromBody]BatchUpdateInstallStatusRequest request)
        {
            return await orderCommandService.BatchUpdateInstallStatus(request);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CancelOrder([FromBody]ApiRequest<CancelOrderRequest> request)
        {
            return await orderCommandService.CancelOrder(request);
        }


        /// <summary>
        /// 完结订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderCompleteStatus([FromBody] ApiRequest<UpdateCompleteStatusRequest> request)
        {
            return await orderCommandService.UpdateOrderCompleteStatus(request);
        }

        /// <summary>
        /// 更新订单车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderCar([FromBody]ApiRequest<UpdateOrderCarRequest> request)
        {
            return await orderCommandService.UpdateOrderCar(request);
        }

    }
}
