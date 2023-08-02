using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Core.Model;
using Ae.B.Order.Api.Core.Model.OrderDetail;
using Ae.B.Order.Api.Core.Request;
using Ae.B.Order.Api.Core.Request.OrderQuery;
using Ae.B.Order.Api.Core.Response;
using Ae.B.Order.Api.Core.Response.OrderQuery;

namespace Ae.B.Order.Api.Core.Interfaces
{
    public interface IOrderQueryService
    {
        Task<ApiPagedResult<GetOrderListResponse>> GetOrderList(GetOrderListRequest request);
        Task<ApiPagedResult<GetOrderMergeListResponse>> GetMergeOrderList(GetMergeOrderListRequest request);
        Task<ApiResult<GetOrderDetailResponse>> GetOrderDetail(GetOrderDetailRequest request);
        Task<ApiResult<List<ReverseReasonVO>>> GetReverseReasons(GetReverseReasonsRequest request);
        Task<ApiPagedResult<OrderLogVO>> GetOrderLogList(GetOrderLogListRequest request);

        Task<ApiResult<GetOrderConfirmResponse>> GetOrderConfirm(ApiRequest<GetOrderConfirmRequest> request);

        Task<ApiResult<TrialCalcOrderAmountResponse>> TrialCalcOrderAmount(ApiRequest<TrialCalcOrderAmountRequest> request);

        Task<ApiResult<List<GetOrderStaticReportResponse>>> GetOrderStaticReport();

        Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReport(
            GetOrderDetailStaticReportApiRequest request);

        Task<ApiPagedResult<GetOrderPackageCardsResponse>> GetOrderPackageCards(GetOrderPackageCardsRequest request);

        Task<ApiPagedResult<GetPackageCardRecordsResponse>> GetPackageCardRecords(GetPackageCardRecordsRequest request);

        Task<ApiPagedResult<VerificationRuleDTO>> GetVerificationRule(GetVerificationRuleRequest request);


        Task<ApiPagedResult<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfitReport(
            ApiRequest<GetOrderOutProductsProfitRequest> request);

        Task<ApiPagedResult<OrderProductNewDTO>> GetOrderProductsReport(ApiRequest<GetOrderProductsRequest> request);
    }
}
