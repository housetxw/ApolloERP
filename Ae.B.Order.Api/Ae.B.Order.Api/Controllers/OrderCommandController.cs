using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Core.Interfaces;
using Ae.B.Order.Api.Core.Request;
using Ae.B.Order.Api.Core.Request.OrderCommand;
using Ae.B.Order.Api.Core.Request.OrderQuery;
using Ae.B.Order.Api.Core.Response;
using Ae.B.Order.Api.Core.Response.OrderCommand;
using Ae.B.Order.Api.Filters;

namespace Rg.ConsumerOrder.Service.Controllers
{
    /// <summary>
    /// 订单操作
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(OrderQueryController))]
    public class OrderCommandController : ControllerBase
    {
        private readonly IOrderCommandService orderCommandService;
        private readonly IIdentityService _identityService;

        public OrderCommandController(IOrderCommandService orderCommandService, IIdentityService identityService)
        {
            this.orderCommandService = orderCommandService;
            _identityService = identityService;
        }

        /// <summary>
        /// 审核确认订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CheckOrder([FromBody]ApiRequest<CheckOrderRequest> request)
        {
            return await orderCommandService.CheckOrder(request);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ApiResult<CreateReverseOrderBaseResponse>> CancelOrder([FromBody]ApiRequest<CancelOrderRequest> request)
        {
            return await orderCommandService.CancelOrder(request);
        }

        /// <summary>
        /// 退款申请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<CreateReverseOrderBaseResponse>> RefundApply([FromBody]ApiRequest<RefundApplyRequest> request)
        {
            return await orderCommandService.RefundApply(request);
        }

        /// <summary>
        /// 追加下单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<AppendSubmitOrderResponse>> AppendSubmitOrder([FromBody]ApiRequest<AppendSubmitOrderRequest> request)
        {
            return await orderCommandService.AppendSubmitOrder(request);
        }

        /// <summary>
        /// 订单提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //  [AllowAnonymous]
        public async Task<ApiResult<SubmitOrderResponse>> SubmitOrder([FromBody] ApiRequest<SubmitOrderRequest> request)
        {
            request.Data.CreatedBy = _identityService.GetUserName();
            return await orderCommandService.SubmitOrder(request);
        }

        /// <summary>
        /// 更新订单地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ApiResult> UpdateOrderAddress([FromBody]ApiRequest<UpdateOrderAddressRequest> request)
        {
            return await orderCommandService.UpdateOrderAddress(request);
        }

        /// <summary>
        /// 更新订单签收状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ApiResult<long>> UpdateOrderSignStatus([FromBody]ApiRequest<UpdateOrderStatusRequest> request)
        {
            return await orderCommandService.UpdateOrderSignStatus(request.Data);
        }

        /// <summary>
        /// 更新订单优惠金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ApiResult<bool>> UpdateCouponAmount([FromBody] ApiRequest<UpdateCouponAmountRequest> request)
        {
            return await orderCommandService.UpdateCouponAmount(request.Data);
        }

        [HttpPost]
        public async Task<ApiResult> SaveVerificationRule([FromBody] ApiRequest<SaveVerificationRuleRequest> request)
        {
           
            return await orderCommandService.SaveVerificationRule(request);
        }

        [HttpPost]
        public async Task<ApiResult> SaveBeautiyOrPackageCardVerificationProduct([FromBody] ApiRequest<SaveBeautiyOrPackageCardVerificationProductRequest> request)
        {
            return await orderCommandService.SaveBeautiyOrPackageCardVerificationProduct(request);
        }


        /// <summary>
        /// 更新套餐卡核销码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateOrderPackage([FromBody] ApiRequest<UpdateOrderPackageRequest> request)
        {
            return await orderCommandService.UpdateOrderPackage(request);
        }

        /// <summary>
        /// 更新订单产品的成本价格
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<ApiResult> UpdateOrderProductCostPrice([FromBody] ApiRequest<BatchUpdateCompleteStatusRequest> request)
        //{
        //    return await orderCommandService.UpdateOrderProductCostPrice(request);
        //}

        /// <summary>
        /// 更新订单产品的优惠和实付金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<ApiResult> UpdateOrderProductActualPayAmount([FromBody] ApiRequest<BatchUpdateCompleteStatusRequest> request)
        //{
        //    return await orderCommandService.UpdateOrderProductActualPayAmount(request);
        //}

        /// <summary>
        /// 批次更新订单产品的成本价格\优惠和实付金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> BatchUpdateOrderProductCostPriceActualPayAmount([FromBody] ApiRequest<BatchUpdateOrderRequest> request)
        {
            return await orderCommandService.BatchUpdateOrderProductCostPriceActualPayAmount(request);
        }

        /// <summary>
        /// 批次更新员工绩效订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> BatchUpdateEmployeePerformanceOrder([FromBody] ApiRequest<BatchUpdateOrderRequest> request)
        {
            return await orderCommandService.BatchUpdateEmployeePerformanceOrder(request);
        }

    }
}
