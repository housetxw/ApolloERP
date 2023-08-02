using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Model;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Client.Response;
using Ae.B.Order.Api.Core.Request.OrderCommand;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Order.Api.Core.Request.OrderQuery;
using Ae.B.Order.Api.Core.Response.OrderQuery;
using Ae.B.Order.Api.Core.Response;
using Ae.B.Order.Api.Core.Request;
using Ae.B.Order.Api.Core.Model.OrderDetail;

namespace Ae.B.Order.Api.Client.Clients
{
    public interface IOrderClient
    {
        /// <summary>
        /// 获取BOSS订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<GetOrderListForBossClientResponse>> GetOrderListForBoss(GetOrderListForBossClientRequest request);
        /// <summary>
        ///  根据订单编号查询订单信息
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        Task<ApiPagedResult<OrderDetailForBossOrderInfoDTO>> GetOrderByNo(string OrderNo);

        Task<ApiResult<long>> UpdateOrderStatus(UpdateOrderStatusRequest request);

        Task<ApiResult<bool>> UpdateCouponAmount(UpdateCouponAmountRequest request);

        Task<ApiResult<GetOrderCompleteStaticReportResponse>> GetOrderCompleteStaticReport(
            GetOrderCompleteStaticReportRequest request);

        Task<ApiResult<GetOrderNotCompleteStaticReportResponse>> GetOrderNotCompleteStaticReport(
            GetOrderNotCompleteStaticReportRequest request);
        Task<ApiPagedResult<GetOrderPackageCardsResponse>> GetOrderPackageCards(GetOrderPackageCardsRequest request);

        Task<ApiPagedResult<GetPackageCardRecordsResponse>> GetPackageCardRecords(GetPackageCardRecordsRequest request);

        Task<ApiPagedResult<MergePayNoDTO>> GetMergeOrderListForBoss(GetMergeOrderListRequest request);

        Task<ApiResult> UpdateOrderPackage(ApiRequest<UpdateOrderPackageRequest> request);

        Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo(GetOrderCarsRequest request);

    }
}
