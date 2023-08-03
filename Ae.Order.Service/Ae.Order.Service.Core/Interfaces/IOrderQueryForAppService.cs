using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Model;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Model.ShopOrder;
using Ae.Order.Service.Core.Request.ShopOrder;


namespace Ae.Order.Service.Core.Interfaces
{
    /// <summary>
    /// 订单查询服务FoApp
    /// </summary>
    public interface IOrderQueryForAppService
    {
        /// <summary>
        /// 查询订单信息根据客户搜索条件（手机号、订单号、产品信息)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GetOrdersForSearchDTO>> GetOrdersForSearch(GetOrdersForSearchRequest request);

        /// <summary>
        /// 查询主订单信息为App
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<OrderDTO>> GetMainOrdersForSearch(GetOrdersForSearchRequest request);

        /// <summary>
        /// 查询订单列表For 业务状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GetOrdersForStatusDTO>> GetOrdersForStatus(GetOrdersForStatusRequest request);

        /// <summary>
        /// 查询主订单信息根据订单状态
        /// </summary>
        /// <param name="getOrdersForStatusRequest"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<OrderDTO>> GetMainOrdersForStatus(GetOrdersForStatusRequest getOrdersForStatusRequest);
    }
}
