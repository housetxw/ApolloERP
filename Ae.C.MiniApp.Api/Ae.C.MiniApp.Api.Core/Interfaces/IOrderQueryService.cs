using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.OrderQuery;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Core.Response.OrderQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    /// <summary>
    /// 订单查询服务
    /// </summary>
    public interface IOrderQueryService
    {
        Task<ApiResult<GetOrderConfirmResponse>> GetOrderConfirm(ApiRequest<GetOrderConfirmRequest> request);
        Task<ApiResult<CalcOrderAmountResponse>> TrialCalcOrderAmount(ApiRequest<CalcOrderAmountRequest> request);
        Task<ApiPagedResult<GetOrderListResponse>> GetOrderListForMiniApp(GetOrderListRequest request);
        Task<ApiResult<GetOrderDetailResponse>> GetOrderDetailForMiniApp(GetOrderDetailRequest request);
        Task<ApiResult<GetOrderVerificationCodeInfosResponse>> GetOrderVerificationCodeInfos(GetOrderVerificationCodeInfosRequest request);
        Task<ApiResult<List<GetEachStatusOrderCountResponse>>> GetEachStatusOrderCount(GetEachStatusOrderCountRequest request);

        Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceType(GetOrderServiceTypeRequest request);

        Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceTypeV2(GetOrderServiceTypeRequest request);

        Task<ApiResult<List<GetOrderPackageCardsResponse>>> GetOrderPackageCards(GetOrderPackageCardsRequest request);

        Task<ApiResult<List<GetPackageCardMainInfoResponse>>> GetPackageCardMainInfo(GetPackageCardMainInfoRequest request);

        Task<ApiResult<GetPackageVerificationCodeDetailResponse>> GetPackageVerificationCodeDetail(GetPackageVerificationCodeDetailRequest request);
    }
}
