using ApolloErp.Log;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Model.Receive;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Common.Exceptions;
using Ae.Shop.Service.Client.Response;

namespace Ae.Shop.Service.Client.Clients
{
    public class ReceiveClient: IReceiveClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApolloErpLogger<ReceiveClient> _logger;
        private readonly IConfiguration _configuration;

        public ReceiveClient(IHttpClientFactory clientFactory, ApolloErpLogger<ReceiveClient> logger,
            IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// 根据到店记录Id查询到店记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopReceiveDto>> GetReceiveByIds(ReceiveByIdsClientRequest request)
        {
            var client = _clientFactory.CreateClient("ReceiveServer");
            ApiResult<List<ShopReceiveDto>> result =
                await client.PostAsJsonAsync<ReceiveByIdsClientRequest, ApiResult<List<ShopReceiveDto>>>(
                    _configuration["ReceiveServer:GetReceiveByIds"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ShopReceiveDto>();
            }
            else
            {
                _logger.Warn($"GetReceiveByIds_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 得到上一次到店记录下的ShopId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> GetLastShopForLastArrival(GetLastShopForLastArrivalClientRequest request)
        {
            var client = _clientFactory.CreateClient("ReceiveServer");
            ApiResult<long> result =
                await client.GetAsJsonAsync<GetLastShopForLastArrivalClientRequest, ApiResult<long>>(
                    _configuration["ReceiveServer:GetLastShopForLastArrival"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetLastShopForLastArrival_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 获取门店预约总数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetShopTotalReserveClientResponse> GetShopTotalReserve(BaseGetShopClientRequest request)
        {
            var client = _clientFactory.CreateClient("ReceiveServer");
            ApiResult<GetShopTotalReserveClientResponse> result =
                await client.GetAsJsonAsync<BaseGetShopClientRequest, ApiResult<GetShopTotalReserveClientResponse>>(
                    _configuration["ReceiveServer:GetShopTotalReserve"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetShopTotalReserve_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        public async  Task<GetShopArrivalOrderForStaticResponse> GetShopArrivalOrderForStatic(GetShopArrivalForStaticClientRequest request)
        {
            var client = _clientFactory.CreateClient("ReceiveServer");
            ApiResult<GetShopArrivalOrderForStaticResponse> result =
                await client.PostAsJsonAsync<GetShopArrivalForStaticClientRequest, ApiResult<GetShopArrivalOrderForStaticResponse>>(
                    _configuration["ReceiveServer:GetShopArrivalOrderForStatic"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else {
                throw new Exception(result?.Message);
            }
        }
    }
}
