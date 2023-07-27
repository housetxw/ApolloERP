using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Model.User;
using Ae.Shop.Service.Client.Model.Vehicle;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Common.Exceptions;

namespace Ae.Shop.Service.Client.Clients
{
    public class VehicleClient : IVehicleClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApolloErpLogger<VehicleClient> _logger;
        private readonly IConfiguration _configuration;

        public VehicleClient(IHttpClientFactory clientFactory, ApolloErpLogger<VehicleClient> logger,
            IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// 获取用户默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserVehicleDto>> GetUserDefaultVehicleBatch(UserDefaultVehicleBatchRequest request)
        {
            var client = _clientFactory.CreateClient("VehicleServer");
            ApiResult<List<UserVehicleDto>> result =
                await client.PostAsJsonAsync<UserDefaultVehicleBatchRequest, ApiResult<List<UserVehicleDto>>>(
                    _configuration["VehicleServer:GetUserDefaultVehicleBatch"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<UserVehicleDto>();
            }
            else
            {
                _logger.Warn($"GetUserDefaultVehicleBatch_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        public async  Task<List<GetVehicleBrandDTO>> GetVehicleBrandList()
        {
            var client = _clientFactory.CreateClient("VehicleServer");
            ApiResult<List<GetVehicleBrandDTO>> result = await 
                client.GetAsJsonAsync<ApiResult<List<GetVehicleBrandDTO>>>(_configuration["VehicleServer:GetVehicleBrandList"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetVehicleBrandList_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }
    }
}
