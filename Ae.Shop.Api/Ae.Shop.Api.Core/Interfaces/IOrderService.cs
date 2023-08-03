using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model.Order;
using Ae.Shop.Api.Core.Model.Reserve;
using Ae.Shop.Api.Core.Request.Order;
using Ae.Shop.Api.Core.Response.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// 得到订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<OrderDTO>> GetOrderInfoListForShop(GetOrderInfoListForShopRequest request);

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
        /// 得到预约信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<ShopReserveDTO>> GetReserverInfo(GetReserverInfoRequest request);

        Task<ApiResult<OrderInsuranceCompanyDTO>> GetOrderInsuranceCompany(GetOrderInsuranceCompanyRequest request);

        Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo( GetOrderCarsRequest request);

        Task<ApiResult<List<OrderDispatchDTO>>> GetOrderDispatch(GetOrderDispatchRequest request);

        Task<ApiResult<OrderDispatchDTO>> GetDispatchInfo(GetReserverInfoRequest request);

        #region Shop的报表中心

        Task<ApiResult<List<GetOrderStaticReportResponse>>> GetOrderStaticReportForShop();

        Task<ApiPagedResult<GetOrderDetailStaticReportResponse>> GetOrderDetailStaticReportForShop(
            GetOrderDetailStaticReportApiRequest request);


        Task<ApiPagedResult<GetOrderOutProductsProfitResponse>> GetOrderOutProductsProfitReportForShop(
            ApiRequest<GetOrderOutProductsProfitRequest> request);

        Task<ApiPagedResult<OrderProductNewDTO>> GetOrderProductsReportForShop(ApiRequest<GetOrderProductsRequest> request);

        #endregion

    }
}
