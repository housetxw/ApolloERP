using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Core.Model;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Core.Response;
using Ae.Order.Service.Core.Response.Order;
using Ae.Order.Service.Dal.Model.Order;
using Ae.Order.Service.Filters;

namespace Ae.Order.Service.Controllers
{
    /// <summary>
    /// 订单基础查询服务
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(OrderQueryController))]
    public class OrderQueryController : Controller
    {
        private readonly IOrderQueryService orderQueryService;

        private readonly string constMessage = "无数据";

        public OrderQueryController(IOrderQueryService orderQueryService)
        {
            this.orderQueryService = orderQueryService;
        }

        /// <summary>
        /// 得到订单基本信息集合
        /// </summary>
        [HttpPost]
        public async Task<ApiResult<List<OrderDTO>>> GetOrderBaseInfo([FromBody] GetOrderBaseInfoRequest request)
        {
            List<OrderDTO> getOrdersBaseInfo = await orderQueryService.GetOrderBaseInfo(request);

            if (getOrdersBaseInfo == null || !getOrdersBaseInfo.Any())
            {
                return new ApiResult<List<OrderDTO>>()
                {
                    Code = ResultCode.SUCCESS_NOT_EXIST,
                    Message = constMessage
                };
            }
            else
            {
                return new ApiResult<List<OrderDTO>>()
                {
                    Code = ResultCode.Success,
                    Data = getOrdersBaseInfo
                };
            }
        }


        /// <summary>
        /// 查询订单基本信息根据业务状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<OrderDTO>> GetOrderBaseInfoForBusinessStatus(
           [FromBody] GetOrderBaseInfoForBusinessStatusRequest request)
        {
            ApiPagedResultData<OrderDTO> getOrdersForSearch = await orderQueryService.GetOrderBaseInfoForBusinessStatus(request);

            ApiPagedResult<OrderDTO> response = new ApiPagedResult<OrderDTO>()
            {
                Code = ResultCode.Success,
                Data = getOrdersForSearch
            };
            if (response.Data == null && response.Code == ResultCode.Success)
            {
                response.Message = constMessage;
                response.Code = ResultCode.SUCCESS_NOT_EXIST;
            }
            return response;
        }

        /// <summary>
        /// 得到订单产品信息集合
        /// </summary>
        [HttpPost]
        public async Task<ApiResult<List<OrderProductDTO>>> GetOrderProduct([FromBody] GetOrderProductRequest request)
        {
            List<OrderProductDTO> getOrdersBaseInfo = await orderQueryService.GetOrderDetailByOrderIds(request.OrderNos);

            ApiResult<List<OrderProductDTO>> response = new ApiResult<List<OrderProductDTO>>()
            {
                Code = ResultCode.Success,
                Data = getOrdersBaseInfo
            };

            return response;
        }

        /// <summary>
        /// 根据订单号获取订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<OrderDTO>> GetOrderByNo([FromQuery]GetOrderByNoRequest request)
        {
            return await orderQueryService.GetOrderByNo(request);
        }

        /// <summary>
        /// 获取未安装订单列表（目前新增预约时查询使用）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetUninstalledOrderListResponse>> GetUninstalledOrderList([FromQuery]GetUninstalledOrderListRequest request)
        {
            return await orderQueryService.GetUninstalledOrderList(request);
        }

        /// <summary>
        /// 计算核对账单金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<GetAccountCheckAmountResponse>>> GetAccountCheckAmount(
           [FromBody] GetAccountCheckAmountRequest request)
        {
            return await orderQueryService.GetAccountCheckAmount(request);
        }


        /// <summary>
        /// 返回未对账的数据根据门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<GetNoReconciliationAmountDTO>>> GetNoReconciliationAmount([FromBody]ShopRequest request)
        {
            return await orderQueryService.GetNoReconciliationAmount(request);
        }

        /// <summary>
        /// 得到订单的数量（待签收、待安装、未对账、异常中、已取消
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetOrderNumForHomeResponse>> GetOrderNumForHome(GetOrderNumForHomeRequest request)
        {
            return await orderQueryService.GetOrderNumForHome(request);
        }

        /// <summary>
        /// 根据用户ID获取购买过的非服务商品ID集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<string>>> GetPidsByUserId([FromQuery]GetPidsByUserIdRequest request)
        {
            return await orderQueryService.GetPidsByUserId(request);
        }

        /// <summary>
        /// 根据PID列表批量获取订单商品销量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<GetSalesByPidsResponse>>> GetSalesByPids([FromBody]GetSalesByPidsRequest request)
        {
            return await orderQueryService.GetSalesByPids(request);
        }

        /// <summary>
        /// 根据门店ID批量获取订单数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<BatchGetOrderCountByShopIdResponse>>> BatchGetOrderCountByShopId([FromBody]BatchGetOrderCountByShopIdRequest request)
        {
            return await orderQueryService.BatchGetOrderCountByShopId(request);
        }

        /// <summary>
        /// 得到订单的车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo([FromBody]GetOrderCarsRequest request)
        {
            return await orderQueryService.GetOrderCarsInfo(request);
        }

        /// <summary>
        /// 得到当前客户此门的车型订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetOrdersByUserIdResponse>>> GetOrdersByUserId([FromQuery]GetOrdersByUserIdRequest request)
        {
            return await orderQueryService.GetOrdersByUserId(request);
        }

        /// <summary>
        /// 得到未支付的订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<OrderDTO>> GetNotPayHaveStockOrder([FromQuery]GetNotPayOrderRequest request)
        {
            ApiPagedResultData<OrderDTO> getNotPayOrder = await orderQueryService.GetNotPayHaveStockOrder(request);

            return new ApiPagedResult<OrderDTO>()
            {
                Code = ResultCode.Success,
                Data = getNotPayOrder
            };
        }

        [HttpPost]
        public async Task<ApiResult<List<OrderAddressDTO>>> BatchGetOrderAddress([FromBody]BatchGetOrderAddressRequest request)
        {
            return await orderQueryService.BatchGetOrderAddress(request);
        }

        /// <summary>
        /// 已完工订单报表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetOrderCompleteStaticReportResponse>> GetOrderCompleteStaticReport(
            [FromQuery] GetOrderCompleteStaticReportRequest request)
        {
            return await orderQueryService.GetOrderCompleteStaticReport(request);
        }

        /// <summary>
        /// 未完工订单报表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetOrderNotCompleteStaticReportResponse>> GetOrderNotCompleteStaticReport(
            [FromQuery] GetOrderNotCompleteStaticReportRequest request)
        {
            return await orderQueryService.GetOrderNotCompleteStaticReport(request);
        }

        /// <summary>
        /// 查询订单优惠卷
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<OrderCouponDTO>>> GetOrderCoupons([FromBody] GetOrderCouponsRequest request)
        {
            return await orderQueryService.GetOrderCoupons(request);
        }
    }
}
