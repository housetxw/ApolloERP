using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Model;
using Ae.OrderComment.Service.Client.Request;
using Ae.OrderComment.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Client.Interface
{
    public interface IOrderClient
    {
        Task<ApiResult<GetFreshFramerOrderDetailResponse>> GetFreshFramerOrderDetail(GetFreshFramerOrderDetailRequest request);

        Task<ApiResult<GetOrderDetailClientResponse>> GetOrderDetail(GetOrderDetailClientRequest request);
        Task<ApiResult<OrderCarDTO>> GetCarByOrderNo(GetCarByOrderNoClientRequest request);
        /// <summary>
        /// 根据门店ID批量获取订单数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<BatchGetOrderCountByShopIdResponse>>> BatchGetOrderCountByShopId(BatchGetOrderCountByShopIdRequest request);

        Task<long> UpdateOrderStatus(UpdateOrderStatusClientRequest request);
    }
}
