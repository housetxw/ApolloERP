using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.OrderQuery;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Core.Response.OrderQuery;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 订单查询
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(OrderQueryController))]
    public class OrderQueryController : ControllerBase
    {
        private readonly IOrderQueryService orderQueryService;

        public OrderQueryController(IOrderQueryService orderQueryService)
        {
            this.orderQueryService = orderQueryService;
        }

        /// <summary>
        /// 获取首次加载订单确认页信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ApiResult<GetOrderConfirmResponse>> GetOrderConfirm([FromBody]ApiRequest<GetOrderConfirmRequest> request)
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
        public async Task<ApiResult<CalcOrderAmountResponse>> TrialCalcOrderAmount([FromBody]ApiRequest<CalcOrderAmountRequest> request)
        {
            return await orderQueryService.TrialCalcOrderAmount(request);
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiPagedResult<GetOrderListResponse>> GetOrderList([FromQuery]GetOrderListRequest request)
        {
            return await orderQueryService.GetOrderListForMiniApp(request);
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<GetOrderDetailResponse>> GetOrderDetail([FromQuery]GetOrderDetailRequest request)
        {
            return await orderQueryService.GetOrderDetailForMiniApp(request);
        }

        /// <summary>
        /// 获取订单核销码集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetOrderVerificationCodeInfosResponse>> GetOrderVerificationCodeInfos([FromQuery]GetOrderVerificationCodeInfosRequest request)
        {
            return await orderQueryService.GetOrderVerificationCodeInfos(request);
        }

        /// <summary>
        /// 批量获取我的页面各状态订单数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetEachStatusOrderCountResponse>>> GetEachStatusOrderCount([FromQuery]GetEachStatusOrderCountRequest request)
        {
            return await orderQueryService.GetEachStatusOrderCount(request);
        }

        /// <summary>
        /// 得到订单服务方式
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        public Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceType([FromQuery]GetOrderServiceTypeRequest request)
        {
            return orderQueryService.GetOrderServiceType(request);
        }

        /// <summary>
        /// 得到订单服务方式
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        public Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceTypeV2([FromBody]ApiRequest<GetOrderServiceTypeRequest> request)
        {
            return orderQueryService.GetOrderServiceTypeV2(request.Data);
        }


        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<List<GetOrderPackageCardsResponse>>> GetOrderPackageCards([FromQuery] GetOrderPackageCardsRequest request)
        {
            return await orderQueryService.GetOrderPackageCards(request);
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<List<GetPackageCardMainInfoResponse>>> GetPackageCardMainInfo([FromQuery] GetPackageCardMainInfoRequest request)
        {
            return await orderQueryService.GetPackageCardMainInfo(request);
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<GetPackageVerificationCodeDetailResponse>> GetPackageVerificationCodeDetail([FromQuery] GetPackageVerificationCodeDetailRequest request)
        {
            return await orderQueryService.GetPackageVerificationCodeDetail(request);
        }
    }
}
