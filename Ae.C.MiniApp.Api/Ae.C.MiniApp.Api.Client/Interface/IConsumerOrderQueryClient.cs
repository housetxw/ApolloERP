using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Request.Order;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Client.Response.Order;
using Ae.C.MiniApp.Api.Core.Request.OrderQuery;
using Ae.C.MiniApp.Api.Core.Request.SharingPromotion;
using Ae.C.MiniApp.Api.Core.Response.OrderQuery;
using Ae.C.MiniApp.Api.Core.Response.SharingPromotion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IConsumerOrderQueryClient
    {
        Task<ApiResult<GetOrderConfirmClientResponse>> GetOrderConfirm(GetOrderConfirmClientRequest request);
        Task<ApiResult<TrialCalcOrderAmountClientResponse>> TrialCalcOrderAmount(TrialCalcOrderAmountClientRequest request);
        Task<ApiResult<GetOrderDetailClientResponse>> GetOrderDetail(GetOrderDetailClientRequest request);
        Task<ApiResult<GetOrderVerificationCodeInfosClientResponse>> GetOrderVerificationCodeInfos(GetOrderVerificationCodeInfosClientRequest clientRequest);

        Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceType(GetOrderServiceTypeRequest request);

        Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceTypeV2(GetOrderServiceTypeRequest request);

        Task<ApiResult<GetSharingSummaryResponse>> GetSharingSummary(GetSharingSummaryRequest request);

        Task<ApiResult<List<string>>> GetSharingRuleDescription();

        Task<ApiPagedResult<GetSharingOrdersResponse>> GetSharingOrders(GetSharingOrdersRequest request);


        Task<ApiResult<GetSharingCouponResponse>> GetSharingCoupon(GetSharingCouponRequest request);

        Task<ApiResult<GetPackageVerificationCodeDetailResponse>> GetPackageVerificationCodeDetail(GetPackageVerificationCodeDetailRequest request);
    }
}
