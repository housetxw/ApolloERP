using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Core.Model;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Core.Request.OrderQuery;
using Ae.ConsumerOrder.Service.Core.Response;
using Ae.ConsumerOrder.Service.Core.Response.OrderQuery;

namespace Ae.ConsumerOrder.Service.Core.Interfaces
{
    /// <summary>
    /// 订单查询服务
    /// </summary>
    public interface IOrderQueryService
    {
        /// <summary>
        /// 【内部公用方法】获取订单确认页套餐单品服务信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetOrderConfirmPackageProductServicesResponse> GetOrderConfirmPackageProductServices(GetOrderConfirmPackageProductServicesRequest request);
        Task<GetOrderDetailPackageProductServicesResponse> GetOrderDetailPackageProductServices(long orderId);
        Task<List<OrderCarDTO>> GetOrderCars(GetOrderCarsRequest request);
        Task<ApiResult<GetOrderConfirmResponse>> GetOrderConfirm(GetOrderConfirmRequest request);
        Task<ApiResult<TrialCalcOrderAmountResponse>> TrialCalcOrderAmount(TrialCalcOrderAmountRequest request);
        Task<ApiResult<GetOrderDetailResponse>> GetOrderDetail(GetOrderDetailRequest request);
        /// <summary>
        /// 【内部公用方法】根据订单号获取订单详情页套餐单品服务信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetOrderVerificationCodeInfosResponse>> GetOrderVerificationCodeInfos(GetOrderVerificationCodeInfosRequest request);
        Task<ApiResult<OrderCarDTO>> GetCarByOrderNo(GetCarByOrderNoRequest request);
        Task<ApiResult<OrderUserDTO>> GetUserByOrderNo(GetUserByOrderNoRequest request);
        Task<ApiResult<ScanCodeResponse>> ScanCode(ScanCodeRequest request);
        Task<ApiPagedResult<GetVerificationCodeOrderListResponse>> GetVerificationCodeOrderList(GetVerificationCodeOrderListRequest request);
        Task<ApiResult<GetVerificationCodeOrderDetailResponse>> GetVerificationCodeOrderDetail(GetVerificationCodeOrderDetailRequest request);
        Task<ApiResult<GetOrderDetailForBossResponse>> GetOrderDetailForBoss(GetOrderDetailForBossRequest request);
        Task<ApiPagedResult<OrderLogDTO>> GetOrderLogList(GetOrderLogListRequest request);

        Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceType(GetOrderServiceTypeRequest request);

        Task<GetOrderConfirmPackageProductServicesResponse> GetShopProduct(GetOrderConfirmPackageProductServicesRequest request, List<GetPromotionActivitByProductCodeListResponse> activityProduct);

        Task<ApiResult<GetPackageVerificationCodeDetailResponse>> GetPackageVerificationCodeDetail(GetPackageVerificationCodeDetailRequest request);

        Task<ApiResult<GetOrderServiceTypeResponse>> GetOrderServiceTypeV2(GetOrderServiceTypeRequest request);
    }
}
