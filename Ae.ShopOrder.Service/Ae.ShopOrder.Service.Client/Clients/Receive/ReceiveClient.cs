using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Request.Receive;
using Ae.ShopOrder.Service.Client.Response.Receive;
using Ae.ShopOrder.Service.Common.Exceptions;
using Ae.ShopOrder.Service.Core.Model.Arrival;
using Ae.ShopOrder.Service.Core.Request.Arrival;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response;
using Ae.ShopOrder.Service.Core.Response.Order;

namespace Ae.ShopOrder.Service.Client.Clients.Receive
{

    public class ReceiveClient : IReceiveClient
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<ReceiveClient> logger;
        private readonly IConfiguration configuration;
        private readonly HttpClient client;

        public ReceiveClient(IHttpClientFactory clientFactory, ApolloErpLogger<ReceiveClient> logger, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            client = clientFactory.CreateClient("ReceiveServer");
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task<ApiResult> AddShopReserveV3(AddReserveV3Request request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<AddReserveV3Request, ApiResult>(configuration["ReceiveServer:AddShopReserveV3"], request);
            return response;
        }

        public async Task<ApiResult<List<ShopArrivalVideoResponse>>> GetShopArrivalVideoForOrder(GetShopArrivalVideoForOrderRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<GetShopArrivalVideoForOrderRequest, ApiResult<List<ShopArrivalVideoResponse>>>(configuration["ReceiveServer:GetShopArrivalVideoForOrder"], request);
            return response;
        }

        /// <summary>
        /// 最近一次到店记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetArrivalListResponse>> GetLastArrival(GetLastArrivalRequest request)
        {
            return await clientFactory.CreateClient("ReceiveServer")
        .GetAsJsonAsync<GetLastArrivalRequest, ApiResult<GetArrivalListResponse>>
            (configuration["ReceiveServer:GetLastArrival"], request);
        }

        /// <summary>
        /// 关联订单到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> ArrivalRelateOrder(ArrivalRelateOrderRequest request)
        {
            return await client.PostAsJsonAsync<ArrivalRelateOrderRequest, ApiResult<bool>>(configuration["ReceiveServer:ArrivalRelateOrder"], request);
        }

        public async Task<ApiResult<bool>> CheckTheDayReserveWithUserCarId(CheckReserveTimeRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<CheckReserveTimeRequest, ApiResult<bool>>(configuration["ReceiveServer:CheckTheDayReserveWithUserCarId"], request);
            return response;
        }

        // ---------------------------------- 接口实现 --------------------------------------

        public async Task<List<ShopReserveOrderDTO>> GetReserveInfoByReserveIdOrOrderNum(GetReserveInfoByReserveIdOrOrderNum req)
        {
            var res = await client.PostAsJsonAsync<GetReserveInfoByReserveIdOrOrderNum, ApiResult<List<ShopReserveOrderDTO>>>(configuration["ReceiveServer:GetReserveInfoByReserveIdOrOrderNum"], req);
            if (res.IsNotNullSuccess() && res.Code == ResultCode.Success)
            {
                return res.Data;
            }

            logger.Warn($"GetReserveInfoByReserveIdOrOrderNum_Error {res?.Message}");
            throw new CustomException(res?.Message);
        }

        public async Task<ApiResult<List<ShopArrivalOrderDTO>>> GetShopArrivalOrder(GetShopArrivalOrderRequest request)
        {
            return await client.PostAsJsonAsync<GetShopArrivalOrderRequest, ApiResult<List<ShopArrivalOrderDTO>>>(configuration["ReceiveServer:GetShopArrivalOrder"], request);
        }

        public async Task<ApiResult<ShopReserveDTO>> GetShopReserveDO(ReserveDetailRequest request)
        {
            return await client.GetAsJsonAsync<ReserveDetailRequest, ApiResult<ShopReserveDTO>>(configuration["ReceiveServer:GetShopReserveDO"], request);
        }
    }
}
