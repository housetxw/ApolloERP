using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Client.Model;
using Ae.Receive.Service.Client.Request;
using Ae.Receive.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Receive.Service.Client.Model.Order;
using Ae.Receive.Service.Client.Request.Order;
using Ae.Receive.Service.Core.Request;
using Ae.Receive.Service.Core.Request.Reserve;

namespace Ae.Receive.Service.Client.Inteface
{
    public interface IOrderClient
    {
        Task<ApiPagedResult<GetUninstalledOrderListClientResponse>> GetUninstalledOrderList(GetUninstalledOrderListClientRequest request);
        Task<ApiResult<OrderCarDTO>> GetCarByOrderNo(GetCarByOrderNoClientRequest request);
        Task<ApiResult<GetOrderDetailClientResponse>> GetOrderDetail(GetOrderDetailClientRequest request);
        /// <summary>
        /// 修改订单预约状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderReserveStatus(UpdateOrderReserveStatusClientRequest request);

        /// <summary>
        /// 根据订单批量查商品信息的
        /// </summary>
        /// <param name="request"></param>
        Task<List<OrderProductDto>> GetOrderProduct(OrderProductRequest request);
        Task<List<string>> CreateOrderForArrivalOrReserver(CreateOrderForArrivalOrReserverRequest request);

        Task<bool> UpdateOrderPayStatus(ApiRequest<UpdateOrderPayStatusRequest> request);

        /// <summary>
        /// 取消订单For预约Or到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> CancelOrderForReserverOrArrival(ApiRequest<CancelOrderForReserverOrArrivalRequest> request);

        /// <summary>
        /// 得到主订单基本信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<OrderDTO>> GetOrderBaseInfo(GetOrderBaseInfoRequest request);

        /// <summary>
        /// 更改订单完结状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateOrderCompleteStatus(ApiRequest<BatchUpdateCompleteStatusRequest> request);

        /// 添加排工记录表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> SaveOrderDispatch(ApiRequest<List<SaveOrderDispatchRequest>> request);

        /// <summary>
        /// 修改订单安装状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> BatchUpdateInstallStatus(BatchUpdateInstallStatusRequest request);

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetOrderDetailClientResponse>> GetOrderDetailForMiniApp(GetOrderDetailClientRequest request);

        /// <summary>
        ///  根据订单号查询车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo(GetOrderCarsRequest request);

        /// <summary>
        /// 批量获取订单集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<ApiResult<List<GetOrderListForMiniAppResponse>>> BatchGetOrderListForMiniApp(BatchGetOrderListForMiniAppRequest request);

        /// <summary>
        /// 查到店记录的用户订单记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetOrdersByUserIdResponse>> GetOrdersByUserId(GetOrdersByUserIdRequest request);

        Task<List<OrderAddressDto>> GetOrderAddressList(OrderAddressListClientRequest request);

        /// <summary>
        /// 更新订单车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<ApiResult> UpdateOrderCar(ApiRequest<UpdateOrderCarRequest> request);

    }
}
