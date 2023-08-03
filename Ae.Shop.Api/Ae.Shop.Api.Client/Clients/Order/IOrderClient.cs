using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Client.Response;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Model.Order;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.Order;
using Ae.Shop.Api.Core.Response.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients.Order
{
    public interface IOrderClient
    {
        /// <summary>
        /// 得到订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<OrderDTO>> GetOrderInfoListForShop(
             GetOrderInfoListForShopRequest request);

        /// <summary>
        /// 得到订单基本信息集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<OrderDTO>>> GetOrderBaseInfo(GetOrderBaseInfoRequest request);

        /// <summary>
        /// 得到订单产品信息集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<OrderProductDTO>>> GetOrderProduct(GetOrderProductRequest request);

        /// <summary>
        /// 占库结束通知订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
         Task<string> OrderUseStockNotify(OrderUseStockNotifyClientRequest request);

        /// <summary>
        /// 查询订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<QueryUseStockOrderDetailResponse>> QueryUseStockOrderDetail(QueryUseStockOrderDetailClientRequest request);
        Task<ApiResult<QueryOrderDetailUseStockResponse>> QueryOrderRealProductDetail(QueryUseStockOrderDetailClientRequest request);

        Task<ApiResult<OrderInsuranceCompanyDTO>> GetOrderInsuranceCompany(GetOrderInsuranceCompanyRequest request);

        Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo(GetOrderCarsRequest request);

        Task<ApiResult<List<OrderDispatchDTO>>> GetOrderDispatch(GetOrderDispatchRequest request);

        Task<ApiResult<GetOrderCompleteStaticReportResponse>> GetOrderCompleteStaticReport(
    GetOrderCompleteStaticReportRequest request);

        Task<ApiResult<GetOrderNotCompleteStaticReportResponse>> GetOrderNotCompleteStaticReport(
    GetOrderNotCompleteStaticReportRequest request);

        Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReport(
    GetOrderDetailStaticReportRequest request);

        Task<ApiPagedResult<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfitReport(
    ApiRequest<GetOrderOutProductsProfitRequest> request);

        Task<ApiPagedResult<OrderProductNewDTO>> GetOrderProductsReport(ApiRequest<GetOrderProductsRequest> request);

        Task<ApiResult<List<EmployeePerformanceOrderDTO>>> GetEmployeePerformanceOrderList(EmployeePerformanceRequest request);

        Task<ApiResult> UpdateEmployeePerformanceOrder(UpdateEmployeePerformanceRequest request);
    }
}
