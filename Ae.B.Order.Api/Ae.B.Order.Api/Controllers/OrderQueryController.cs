using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Core.Interfaces;
using Ae.B.Order.Api.Core.Model;
using Ae.B.Order.Api.Core.Model.OrderDetail;
using Ae.B.Order.Api.Core.Request;
using Ae.B.Order.Api.Core.Request.OrderQuery;
using Ae.B.Order.Api.Core.Response;
using Ae.B.Order.Api.Core.Response.OrderQuery;
using Ae.B.Order.Api.Filters;

namespace Rg.ConsumerOrder.Service.Controllers
{
    /// <summary>
    /// 订单查询
    /// </summary>
    [Route("[controller]/[action]")]
    // [Filter(nameof(OrderQueryController))]
    public class OrderQueryController : ControllerBase
    {
        private readonly IOrderQueryService orderQueryService;


        public OrderQueryController(IOrderQueryService orderQueryService)
        {
            this.orderQueryService = orderQueryService;
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiPagedResult<GetOrderListResponse>> GetOrderList([FromQuery] GetOrderListRequest request)
        {
            return await orderQueryService.GetOrderList(request);
        }

        /// <summary>
        /// 获取合并支付订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiPagedResult<GetOrderMergeListResponse>> GetMergeOrderList([FromQuery] GetMergeOrderListRequest request)
        {
            return await orderQueryService.GetMergeOrderList(request);
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<GetOrderDetailResponse>> GetOrderDetail([FromQuery] GetOrderDetailRequest request)
        {
            return await orderQueryService.GetOrderDetail(request);
        }

        /// <summary>
        /// 获取申请原因集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReverseReasonVO>>> GetReverseReasons([FromQuery] GetReverseReasonsRequest request)
        {
            return await orderQueryService.GetReverseReasons(request);
        }

        /// <summary>
        /// 订单日志列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<OrderLogVO>> GetOrderLogList([FromQuery] GetOrderLogListRequest request)
        {
            return await orderQueryService.GetOrderLogList(request);
        }

        /// <summary>
        /// 获取首次加载订单确认页信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ApiResult<GetOrderConfirmResponse>> GetOrderConfirm([FromBody] ApiRequest<GetOrderConfirmRequest> request)
        {

            return await orderQueryService.GetOrderConfirm(request);
        }

        /// <summary>
        /// 试算订单金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ApiResult<TrialCalcOrderAmountResponse>> TrialCalcOrderAmount([FromBody] ApiRequest<TrialCalcOrderAmountRequest> request)
        {
            return await orderQueryService.TrialCalcOrderAmount(request);
        }

        /// <summary>
        /// 订单统计报表
        /// </summary>
        /// <returns></returns>
        // [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<GetOrderStaticReportResponse>>> GetOrderStaticReport()
        {
            return await orderQueryService.GetOrderStaticReport();
        }


        /// <summary>
        /// 订单报表详情( Week，Month，Year)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReport(
            [FromQuery] GetOrderDetailStaticReportApiRequest request)
        {

            return await orderQueryService.GetOrderDetailStaticReport(request);
        }

        [HttpGet]
        public async Task<ApiPagedResult<GetOrderPackageCardsResponse>> GetOrderPackageCards([FromQuery] GetOrderPackageCardsRequest request)
        {
            return await orderQueryService.GetOrderPackageCards(request);
        }

        [HttpGet]
        public async Task<ApiPagedResult<GetPackageCardRecordsResponse>> GetPackageCardRecords([FromQuery] GetPackageCardRecordsRequest request)
        {
            return await orderQueryService.GetPackageCardRecords(request);
        }

        [HttpGet]
        public async Task<ApiPagedResult<VerificationRuleDTO>> GetVerificationRule([FromQuery] GetVerificationRuleRequest request)
        {
            return await orderQueryService.GetVerificationRule(request);
        }

        [HttpPost]
        public async Task<ApiPagedResult<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfitReport([FromBody] ApiRequest<GetOrderOutProductsProfitRequest> request)
        {
            return await orderQueryService.GetOrderOutProductsProfitReport(request);
        }

        [HttpPost]

        public async Task<ApiPagedResult<OrderProductNewDTO>> GetOrderProductsReport([FromBody] ApiRequest<GetOrderProductsRequest> request)
        {
            return await orderQueryService.GetOrderProductsReport(request);
        }
    }
}
