using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Common.Constant;
using Ae.ConsumerOrder.Service.Core.Interfaces;
using Ae.ConsumerOrder.Service.Core.Model;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Core.Request.OrderQuery;
using Ae.ConsumerOrder.Service.Core.Response;
using Ae.ConsumerOrder.Service.Core.Response.OrderQuery;
using Ae.ConsumerOrder.Service.Filters;

namespace Ae.ConsumerOrder.Service.Controllers
{
    /// <summary>
    /// 订单查询服务
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(OrderQueryController))]
    public class OrderQueryController : Controller
    {
        private readonly IOrderQueryService orderQueryService;

        public OrderQueryController(IOrderQueryService orderQueryService)
        {
            this.orderQueryService = orderQueryService;
        }

        /// <summary>
        /// 根据订单ID集合批量获取车辆信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<OrderCarDTO>>> GetOrderCarInfo([FromBody] GetOrderCarsRequest request)
        {
            var orderCars = await orderQueryService.GetOrderCars(request);
            ApiResult<List<OrderCarDTO>> response = new ApiResult<List<OrderCarDTO>>()
            {
                Code = ResultCode.Success,
                Data = orderCars
            };
            if (response.Data == null && response.Code == ResultCode.Success)
                response.Message = CommonConstant.NullData;

            return response;
        }

        /// <summary>
        /// 根据订单号查询订单车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<OrderCarDTO>> GetCarByOrderNo([FromQuery] GetCarByOrderNoRequest request)
        {
            return await orderQueryService.GetCarByOrderNo(request);
        }

        /// <summary>
        /// 根据订单号查询订单用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<OrderUserDTO>> GetUserByOrderNo([FromQuery] GetUserByOrderNoRequest request)
        {
            return await orderQueryService.GetUserByOrderNo(request);
        }

        /// <summary>
        /// 获取首次加载订单确认页信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<GetOrderConfirmResponse>> GetOrderConfirm([FromBody] GetOrderConfirmRequest request)
        {
            return await orderQueryService.GetOrderConfirm(request);
        }

        /// <summary>
        /// 试算订单金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<TrialCalcOrderAmountResponse>> TrialCalcOrderAmount([FromBody] TrialCalcOrderAmountRequest request)
        {
            return await orderQueryService.TrialCalcOrderAmount(request);
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetOrderDetailResponse>> GetOrderDetail([FromQuery] GetOrderDetailRequest request)
        {
            return await orderQueryService.GetOrderDetail(request);
        }

        /// <summary>
        /// 获取订单核销码集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetOrderVerificationCodeInfosResponse>> GetOrderVerificationCodeInfos([FromQuery] GetOrderVerificationCodeInfosRequest request)
        {
            return await orderQueryService.GetOrderVerificationCodeInfos(request);
        }

        /// <summary>
        /// 扫码核销解析
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<ScanCodeResponse>> ScanCode([FromBody] ScanCodeRequest request)
        {
            return await orderQueryService.ScanCode(request);
        }

        /// <summary>
        /// 核销码订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetVerificationCodeOrderListResponse>> GetVerificationCodeOrderList([FromQuery] GetVerificationCodeOrderListRequest request)
        {
            return await orderQueryService.GetVerificationCodeOrderList(request);
        }

        /// <summary>
        /// 核销码订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetVerificationCodeOrderDetailResponse>> GetVerificationCodeOrderDetail([FromQuery] GetVerificationCodeOrderDetailRequest request)
        {
            return await orderQueryService.GetVerificationCodeOrderDetail(request);
        }

        /// <summary>
        /// 获取BOSS订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetOrderDetailForBossResponse>> GetOrderDetailForBoss([FromQuery] GetOrderDetailForBossRequest request)
        {
            return await orderQueryService.GetOrderDetailForBoss(request);
        }

        /// <summary>
        /// 订单日志列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<OrderLogDTO>> GetOrderLogList([FromQuery] GetOrderLogListRequest request)
        {
            return await orderQueryService.GetOrderLogList(request);
        }

        /// <summary>
        /// 得到订单服务方式
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceType([FromQuery] GetOrderServiceTypeRequest request)
        {
            return await orderQueryService.GetOrderServiceType(request);
        }

        [HttpPost]
        public async Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceTypeV2([FromBody] GetOrderServiceTypeRequest request)
        {
            return await orderQueryService.GetOrderServiceTypeV2(request);
        }

        [HttpGet]
        public async Task<ApiResult<GetPackageVerificationCodeDetailResponse>> GetPackageVerificationCodeDetail([FromQuery] GetPackageVerificationCodeDetailRequest request)
        {
            return await orderQueryService.GetPackageVerificationCodeDetail(request);
        }
    }
}
