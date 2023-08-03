using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Model.Product;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.Core.Request.Product;

namespace Ae.ShopOrder.Service.Core.Interfaces
{
    /// <summary>
    /// 订单查询服务
    /// </summary>
    public interface IOrderQueryService
    {
        /// <summary>
        /// 得到车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<OrderCarDTO>>> GetOrderCar(GetOrderCarRequest request);

        /// <summary>
        /// 得到订单不适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetOrderNotAdapterInfoResponse>> GetOrderNotAdapter(GetOrderNotAdapterRequest request);

        /// <summary>
        /// 得到移库单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetWareHouseTransferResponse>> GetWarehouseTransferAllTask(
            GetWareHouseTransferRequest request);

        /// <summary>
        /// 批量的查询物流信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<GetBatchWarehouseTransferPackagesDTO>>> GetBatchWarehouseTransferPackages(
            GetBatchWarehouseTransferPackagesRequest request);


        Task<ApiResult<GetOrderDetailResponse>> GetOrderDetailForMiniApp(GetOrderDetailRequest request);

        /// <summary>
        /// 获取Boss 订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetOrderDetailForBossResponse>> GetOrderDetailForBoss(GetOrderDetailForBossRequest request);

        Task<ApiPagedResult<OrderLogDTO>> GetOrderLogList(GetOrderLogListRequest request);

        /// <summary>
        /// 得到派工订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<OrderDispatchDTO>>> GetOrderDispatch(GetOrderDispatchRequest request);

        Task<GetOrderDetailPackageProductServicesResponse> GetOrderDetailPackageProductServices(long orderId);

        /// <summary>
        /// 得到可派工的技师列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<GetDispatchTechniciansResponse>>> GetDispatchTechnicians(GetDispatchTechniciansRequest request);

        /// <summary>
        /// 判断是否唤醒收银台
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> CheckIsNeedPayControl(CheckIsNeedPayControlRequest request);

        /// <summary>
        /// 支付列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<PayListResponse>> PayList(PayRequest request);

        Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReport(
            GetOrderDetailStaticReportRequest request);

        Task<ApiResult<List<FranchisesConfigDTO>>> GetFranchisesConfig(GetFranchisesConfigRequest request);

        Task<ApiPagedResult<GetOrdersForOfficeResponse>> GetOrdersForOffice(GetOrdersForOfficeRequest request);


        Task<ApiResult<OrderInsuranceCompanyDTO>> GetOrderInsuranceCompany(GetOrderInsuranceCompanyRequest request);

        Task<ApiResult<List<InsuranceCompanyDTO>>> GetInsuranceCompanyList(GetShopInfoRequest request);

        Task<ApiPagedResult<VerificationRuleDTO>> GetVerificationRule(GetVerificationRuleRequest request);

        Task<ApiResult<List<GetShopInfoByRefSmallWarehouseIdResponse>>> GetShopInfoByRefSmallWarehouseId(GetShopInfoByRefSmallWarehouseIdRequest request);

        Task<ApiResult<List<GetWaitingPaySmallWarehouseOrderResponse>>> GetWaitingPaySmallWarehouseOrder(GetWaitingPaySmallWarehouseOrderRequest request);

        Task<ApiResult<List<GetRefDelegateOrdersResponse>>> GetRefDelegateOrders(GetRefDelegateOrdersRequest request);

        Task<ApiPagedResult<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfitReport(
            ApiRequest<GetOrderOutProductsProfitRequest> request);

        Task<ApiPagedResult<OrderProductNewDTO>> GetOrderProductsReport(ApiRequest<GetOrderProductsRequest> request);

        Task<ApiResult<bool>> CheckIsNeedPayMergeControl(CheckIsNeedPayMergeControlRequest request);

        Task<ApiResult<List<ShopSalesMonthResponse>>> GetShopSalesMonthList( GetShopSalesMonthRequest request);
        Task<ApiResult<List<OrderPackageCardDTO>>> GetOrderPackageCard(GetOrderPackageCardRequest request);
    }
}
