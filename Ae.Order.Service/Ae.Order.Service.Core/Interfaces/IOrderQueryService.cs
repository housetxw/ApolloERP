using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Model;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Request;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Core.Response;
using Ae.Order.Service.Core.Response.Order;

namespace Ae.Order.Service.Core.Interfaces
{
    /// <summary>
    /// 订单查询服务
    /// </summary>
    public interface IOrderQueryService
    {

        /// <summary>
        /// 查询订单明细根据orderIds
        /// </summary>
        /// <param name="orderNos"></param>
        /// <returns></returns>
        Task<List<OrderProductDTO>> GetOrderDetailByOrderIds(List<string> orderNos);


        /// <summary>
        /// 查询订单基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<OrderDTO>> GetOrderBaseInfo(GetOrderBaseInfoRequest request);

        /// <summary>
        /// 查询订单基本信息根据业务状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<OrderDTO>> GetOrderBaseInfoForBusinessStatus(GetOrderBaseInfoForBusinessStatusRequest request);

        /// <summary>
        /// 根据订单号获取订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<OrderDTO>> GetOrderByNo(GetOrderByNoRequest request);

        /// <summary>
        /// 得到对账金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<GetAccountCheckAmountResponse>>> GetAccountCheckAmount(GetAccountCheckAmountRequest request);

        /// <summary>
        /// 得到对账金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<GetNoReconciliationAmountDTO>>> GetNoReconciliationAmount(ShopRequest request);

        /// <summary>
        /// 得到订单的数量（待签收、待安装、未对账、异常中、已取消）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetOrderNumForHomeResponse>> GetOrderNumForHome(GetOrderNumForHomeRequest request);

        /// <summary>
        /// 根据用户ID获取购买过的商品ID集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<string>>> GetPidsByUserId(GetPidsByUserIdRequest request);

        /// <summary>
        /// 获取未安装订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<GetUninstalledOrderListResponse>> GetUninstalledOrderList(GetUninstalledOrderListRequest request);

        /// <summary>
        /// 根据PID列表批量获取订单商品销量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<GetSalesByPidsResponse>>> GetSalesByPids(GetSalesByPidsRequest request);

        /// <summary>
        /// 根据门店ID批量获取订单数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<BatchGetOrderCountByShopIdResponse>>> BatchGetOrderCountByShopId(BatchGetOrderCountByShopIdRequest request);

        /// <summary>
        /// 得到车辆的信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo(GetOrderCarsRequest request);

        /// <summary>
        /// 得到订单信息根据用户ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        //  Task<ApiResult<List<OrderDTO>>> GetOrdersByUserId(GetOrdersByUserIdRequest request);

        /// <summary>
        /// 查询未付款的订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<OrderDTO>> GetNotPayHaveStockOrder(GetNotPayOrderRequest request);

        Task<ApiResult<List<GetOrdersByUserIdResponse>>> GetOrdersByUserId(GetOrdersByUserIdRequest request);

        Task<ApiResult<List<OrderAddressDTO>>> BatchGetOrderAddress(BatchGetOrderAddressRequest request);

        Task<ApiResult<GetOrderCompleteStaticReportResponse>> GetOrderCompleteStaticReport(
            GetOrderCompleteStaticReportRequest request);

        Task<ApiResult<GetOrderNotCompleteStaticReportResponse>> GetOrderNotCompleteStaticReport(
          GetOrderNotCompleteStaticReportRequest request);

        Task<ApiResult<List<OrderCouponDTO>>> GetOrderCoupons(GetOrderCouponsRequest request);

    }
}
