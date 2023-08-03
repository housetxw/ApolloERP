using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Client.Inteface;
using Ae.Receive.Service.Client.Model;
using Ae.Receive.Service.Client.Model.Order;
using Ae.Receive.Service.Client.Request;
using Ae.Receive.Service.Client.Request.Order;
using Ae.Receive.Service.Client.Response;
using Ae.Receive.Service.Common.Exceptions;
using Ae.Receive.Service.Core.Request;
using Ae.Receive.Service.Core.Request.Reserve;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Client.Clients
{
    public class OrderClient : IOrderClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        private readonly ApolloErpLogger<OrderClient> _logger;
        public OrderClient(IHttpClientFactory clientFactory, IConfiguration configuration,ApolloErpLogger<OrderClient> logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }
        /// <summary>
        /// 获取未安装订单列表（目前新增预约时查询使用）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<GetUninstalledOrderListClientResponse>> GetUninstalledOrderList(GetUninstalledOrderListClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var response = await client.GetAsJsonAsync<GetUninstalledOrderListClientRequest, ApiPagedResult<GetUninstalledOrderListClientResponse>>(configuration["OrderServer:GetUninstalledOrderList"], request);
            return response;
        }

        /// <summary>
        ///  根据订单号查询车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<OrderCarDTO>> GetCarByOrderNo(GetCarByOrderNoClientRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            ApiResult<OrderCarDTO> response = await client.GetAsJsonAsync<GetCarByOrderNoClientRequest, ApiResult<OrderCarDTO>>(configuration["ConsumerOrderServer:GetCarByOrderNo"], request);
            return response;
        }

        /// <summary>
        ///  根据订单号查询车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<OrderCarDTO>>> GetOrderCarsInfo(GetOrderCarsRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            ApiResult<List<OrderCarDTO>> response = await client.PostAsJsonAsync<GetOrderCarsRequest, ApiResult<List<OrderCarDTO>>>(configuration["OrderServer:GetOrderCarsInfo"], request);
            return response;
        }


        /// <summary>
        /// 查询订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetOrderDetailClientResponse>> GetOrderDetail(GetOrderDetailClientRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            ApiResult<GetOrderDetailClientResponse> response = await client.GetAsJsonAsync<GetOrderDetailClientRequest, ApiResult<GetOrderDetailClientResponse>>(configuration["ConsumerOrderServer:GetOrderDetail"], request);
            return response;
        }

        /// <summary>
        /// 查询订单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetOrderDetailClientResponse>> GetOrderDetailForMiniApp(GetOrderDetailClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            ApiResult<GetOrderDetailClientResponse> response = await client.GetAsJsonAsync<GetOrderDetailClientRequest, ApiResult<GetOrderDetailClientResponse>>(configuration["ShopOrderServer:GetOrderDetailForMiniApp"], request);
            return response;
        }

        /// <summary>
        /// 修改订单预约状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateOrderReserveStatus(UpdateOrderReserveStatusClientRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.PostAsJsonAsync<UpdateOrderReserveStatusClientRequest, ApiResult>(configuration["ConsumerOrderServer:UpdateOrderReserveStatus"], request);
            return response;
        }

        /// <summary>
        /// 根据订单批量查商品信息的
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<OrderProductDto>> GetOrderProduct(OrderProductRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var result =
                await client.PostAsJsonAsync<OrderProductRequest, ApiResult<List<OrderProductDto>>>(
                    configuration["OrderServer:GetOrderProduct"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<OrderProductDto>();
            }
            else
            {
                _logger.Info($"GetOrderProduct_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }


        /// <summary>
        /// 查到店记录的用户订单记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetOrdersByUserIdResponse>> GetOrdersByUserId(GetOrdersByUserIdRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var result =
                await client.GetAsJsonAsync<GetOrdersByUserIdRequest, ApiResult<List<GetOrdersByUserIdResponse>>>(
                    configuration["OrderServer:GetOrdersByUserId"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<GetOrdersByUserIdResponse>();
            }
            else
            {
                _logger.Info($"GetOrdersByUserId_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 预约创建订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> CreateOrderForArrivalOrReserver(CreateOrderForArrivalOrReserverRequest request) 
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var result =
                await client.PostAsJsonAsync<CreateOrderForArrivalOrReserverRequest, ApiResult<List<string>>>(
                    configuration["ShopOrderServer:CreateOrderForArrivalOrReserver"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<string>();
            }
            else
            {
                _logger.Info($"CreateOrderForArrivalOrReserver_Error {result?.Message}");
               
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 更新订单支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOrderPayStatus(ApiRequest<UpdateOrderPayStatusRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var result =
                await client.PostAsJsonAsync<ApiRequest<UpdateOrderPayStatusRequest>, ApiResult>(
                    configuration["ShopOrderServer:UpdateOrderPayStatus"], request);
            if (result.IsNotNullSuccess())
            {
                return true;
            }
            else
            {
                _logger.Info($"UpdateOrderPayStatus {result?.Message}");
                return false;
            }
        }

        /// <summary>
        /// 取消订单For预约Or到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CancelOrderForReserverOrArrival(ApiRequest<CancelOrderForReserverOrArrivalRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var result =
                await client.PostAsJsonAsync<ApiRequest<CancelOrderForReserverOrArrivalRequest>, ApiResult>(
                    configuration["ShopOrderServer:CancelOrderForReserverOrArrival"], request);
            return result;
        }

        public async Task<List<OrderDTO>> GetOrderBaseInfo(GetOrderBaseInfoRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var result =
                await client.PostAsJsonAsync<GetOrderBaseInfoRequest, ApiResult<List< OrderDTO >>> (
                    configuration["OrderServer:GetOrderBaseInfo"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<OrderDTO>();
            }
            else
            {
                _logger.Info($"GetOrderBaseInfo {result.Message}");
               // throw new CustomException(result.Message);
               return new List<OrderDTO>();
            }
        }

        /// <summary>
        /// 更新订单的支付状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOrderCompleteStatus(ApiRequest<BatchUpdateCompleteStatusRequest> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            var result =
                await client.PostAsJsonAsync<ApiRequest<BatchUpdateCompleteStatusRequest>, ApiResult>(
                    configuration["ShopOrderServer:UpdateOrderCompleteStatus"], request);
            if (result.IsNotNullSuccess())
            {
                return true;
            }
            else
            {
                _logger.Info($"UpdateOrderCompleteStatus {result?.Message}");
                return false;
            }
        }
        /// 更新订单的派工状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SaveOrderDispatch(ApiRequest<List<SaveOrderDispatchRequest>> request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");

            var response =
                await client
                    .PostAsJsonAsync<ApiRequest<List<SaveOrderDispatchRequest>>, ApiResult>(
                        configuration["ShopOrderServer:SaveOrderDispatch"], request);

            return response;
        }

        /// <summary>
        /// 修改订单安装状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> BatchUpdateInstallStatus(BatchUpdateInstallStatusRequest request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");

            var response =
                await client
                    .PostAsJsonAsync<BatchUpdateInstallStatusRequest, ApiResult>(
                        configuration["ShopOrderServer:BatchUpdateInstallStatus"], request);

            return response;
        }

        /// <summary>
        /// 批量获取订单集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetOrderListForMiniAppResponse>>> BatchGetOrderListForMiniApp(BatchGetOrderListForMiniAppRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");
            var result =
                await client.PostAsJsonAsync<BatchGetOrderListForMiniAppRequest, ApiResult<List<GetOrderListForMiniAppResponse>>>(configuration["OrderServer:BatchGetOrderListForMiniApp"], request);
            return result;
        }

        public async Task<List<OrderAddressDto>> GetOrderAddressList(OrderAddressListClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderServer");

            var result =
                await client.PostAsJsonAsync<OrderAddressListClientRequest, ApiResult<List<OrderAddressDto>>>(
                    configuration["OrderServer:BatchGetOrderAddress"], request);

            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<OrderAddressDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"BatchGetOrderAddress_Error Message={msg}");
                throw new CustomException(msg);
            }
        }

        public async Task<ApiResult> UpdateOrderCar(ApiRequest<UpdateOrderCarRequest> request)
        {
            var client = clientFactory.CreateClient("OrderServer");

            var response =
              await client
                  .PostAsJsonAsync<ApiRequest<UpdateOrderCarRequest>, ApiResult>(
                      configuration["OrderServer:UpdateOrderCar"], request);

            return response;
        }
    }
}
