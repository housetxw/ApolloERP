using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Request.Shop;
using Ae.ShopOrder.Service.Common.Constant;
using Ae.ShopOrder.Service.Core.Interfaces;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Model.Product;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Arrival;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Request.Product;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.WebApi.Filters;

namespace Ae.ShopOrder.Service.Controllers
{
    /// <summary>
    /// 订单查询服务
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(OrderQueryController))]
    public class OrderQueryController : Controller
    {
        private readonly IOrderQueryService orderQuerySvc;

        public OrderQueryController(IOrderQueryService orderQuerySvc)
        {
            this.orderQuerySvc = orderQuerySvc;
        }

        /// <summary>
        /// 得到订单的车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<OrderCarDTO>>> GetOrderCarInfo([FromBody] GetOrderCarRequest request)
        {
            var response = await orderQuerySvc.GetOrderCar(request);

            if (response.Data == null && response.Code == ResultCode.Success)
                response.Message = CommonConstant.NullData;

            return response;
        }

        /// <summary>
        /// 返回订单不适配结果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetOrderNotAdapterInfoResponse>> GetOrderNotAdapter(
           [FromQuery] GetOrderNotAdapterRequest request)
        {
            return await orderQuerySvc.GetOrderNotAdapter(request);
        }

        /// <summary>
        /// 查询WMS移库单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetWareHouseTransferResponse>> GetWarehouseTransferAllTask(
            GetWareHouseTransferRequest request)
        {
            return await orderQuerySvc.GetWarehouseTransferAllTask(request);
        }

        /// <summary>
        /// 批量的查询物流接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<GetBatchWarehouseTransferPackagesDTO>>> GetBatchWarehouseTransferPackages([FromBody] GetBatchWarehouseTransferPackagesRequest request)
        {
            return await orderQuerySvc.GetBatchWarehouseTransferPackages(request);
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetOrderDetailResponse>> GetOrderDetailForMiniApp([FromQuery] GetOrderDetailRequest request)
        {
            return await orderQuerySvc.GetOrderDetailForMiniApp(request);
        }

        /// <summary>
        /// 获取BOSS订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetOrderDetailForBossResponse>> GetOrderDetailForBoss([FromQuery] GetOrderDetailForBossRequest request)
        {
            return await orderQuerySvc.GetOrderDetailForBoss(request);
        }
        /// <summary>
        /// 订单日志列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<OrderLogDTO>> GetOrderLogList([FromQuery] GetOrderLogListRequest request)
        {
            return await orderQuerySvc.GetOrderLogList(request);
        }

        /// <summary>
        /// 得到派工订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<OrderDispatchDTO>>> GetOrderDispatch([FromBody] GetOrderDispatchRequest request)
        {
            return await orderQuerySvc.GetOrderDispatch(request);
        }

        /// <summary>
        /// 得到派工技师列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetDispatchTechniciansResponse>>> GetDispatchTechnicians([FromQuery] GetDispatchTechniciansRequest request)
        {
            return await orderQuerySvc.GetDispatchTechnicians(request);
        }

        /// <summary>
        /// 判断是否唤起收银台
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CheckIsNeedPayControl([FromBody] CheckIsNeedPayControlRequest request)
        {
            return await orderQuerySvc.CheckIsNeedPayControl(request);
        }

        /// <summary>
        /// 判断是否唤起收银台合并支付
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CheckIsNeedPayMergeControl([FromBody] CheckIsNeedPayMergeControlRequest request)
        {
            return await orderQuerySvc.CheckIsNeedPayMergeControl(request);
        }

        /// <summary>
        /// 收款列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<PayListResponse>> PayList([FromQuery] PayRequest request)
        {
            return await orderQuerySvc.PayList(request);
        }

        /// <summary>
        /// 订单明细报表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReport([FromQuery] GetOrderDetailStaticReportRequest request)
        {
            return await orderQuerySvc.GetOrderDetailStaticReport(request);
        }

        [HttpPost]
        public async Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReportForShop([FromBody] GetOrderDetailStaticReportRequest request)
        {
            return await orderQuerySvc.GetOrderDetailStaticReport(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<FranchisesConfigDTO>>> GetFranchisesConfig([FromQuery] GetFranchisesConfigRequest request)
        {
            return await orderQuerySvc.GetFranchisesConfig(request);
        }

        [HttpGet]
        public async Task<ApiPagedResult<GetOrdersForOfficeResponse>> GetOrdersForOffice([FromQuery] GetOrdersForOfficeRequest request)
        {
            return await orderQuerySvc.GetOrdersForOffice(request);
        }

        [HttpGet]
        public async Task<ApiResult<OrderInsuranceCompanyDTO>> GetOrderInsuranceCompany([FromQuery] GetOrderInsuranceCompanyRequest request)
        {
            return await orderQuerySvc.GetOrderInsuranceCompany(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<InsuranceCompanyDTO>>> GetInsuranceCompanyList([FromQuery] GetShopInfoRequest request)
        {
            return await orderQuerySvc.GetInsuranceCompanyList(request);
        }

        [HttpGet]
        public async Task<ApiPagedResult<VerificationRuleDTO>> GetVerificationRule([FromQuery] GetVerificationRuleRequest request)
        {
            return await orderQuerySvc.GetVerificationRule(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<GetShopInfoByRefSmallWarehouseIdResponse>>> GetShopInfoByRefSmallWarehouseId([FromQuery] GetShopInfoByRefSmallWarehouseIdRequest request)
        {
            return await orderQuerySvc.GetShopInfoByRefSmallWarehouseId(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<GetWaitingPaySmallWarehouseOrderResponse>>> GetWaitingPaySmallWarehouseOrder([FromQuery] GetWaitingPaySmallWarehouseOrderRequest request)
        {
            return await orderQuerySvc.GetWaitingPaySmallWarehouseOrder(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<GetRefDelegateOrdersResponse>>> GetRefDelegateOrders([FromQuery] GetRefDelegateOrdersRequest request)
        {
            return await orderQuerySvc.GetRefDelegateOrders(request);
        }

        [HttpPost]
        public async Task<ApiPagedResult<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfitReport([FromBody] ApiRequest<GetOrderOutProductsProfitRequest> request)
        {
            return await orderQuerySvc.GetOrderOutProductsProfitReport(request);
        }

        [HttpPost]

        public async Task<ApiPagedResult<OrderProductNewDTO>> GetOrderProductsReport([FromBody] ApiRequest<GetOrderProductsRequest> request)
        {
            return await orderQuerySvc.GetOrderProductsReport(request);
        }

        /// <summary>
        /// 经营月报报表查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopSalesMonthResponse>>> GetShopSalesMonthList([FromQuery] GetShopSalesMonthRequest request)
        {
            return await orderQuerySvc.GetShopSalesMonthList(request);
        }

        /// <summary>
        /// 得到套餐卡信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<OrderPackageCardDTO>>> GetOrderPackageCard([FromBody] GetOrderPackageCardRequest request)
        {
            return await orderQuerySvc.GetOrderPackageCard(request);
        }


    }
}