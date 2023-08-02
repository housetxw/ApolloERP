using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.Activity.Api.Client.Request.Vehicle;
using Ae.B.Activity.Api.Client.Response.Vehicle;
using Ae.B.Activity.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Activity.Api.Client.Clients.Vehicle
{
    public class VehicleClient : IVehicleClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        private readonly ApolloErpLogger<VehicleClient> _logger;

        public VehicleClient(IHttpClientFactory clientFactory, IConfiguration configuration, ApolloErpLogger<VehicleClient> _logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            this._logger = _logger;
        }

        public async  Task<List<string>> GetPaiLiangByVehicleId(GetPaiLiangByVehicleIdClientRequest request)
        {
            var client = clientFactory.CreateClient("VehicleServer");
            ApiResult<List<string>> result = await
                client.GetAsJsonAsync<GetPaiLiangByVehicleIdClientRequest,ApiResult<List<string>>>(configuration["VehicleServer:GetPaiLiangByVehicleId"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetPaiLiangByVehicleId_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        public async Task<List<GetVehicleBrandClientResponse>> GetVehicleBrandList()
        {
            var client = clientFactory.CreateClient("VehicleServer");
            ApiResult<List<GetVehicleBrandClientResponse>> result = await
                client.GetAsJsonAsync<ApiResult<List<GetVehicleBrandClientResponse>>>(configuration["VehicleServer:GetVehicleBrandList"], null);
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

        public async  Task<List<GetVehicleListClientResponse>> GetVehicleListByBrand(GetVehicleListByBrandClientRequest request)
        {
            var client = clientFactory.CreateClient("VehicleServer");
            ApiResult<List<GetVehicleListClientResponse>> result = await
                client.GetAsJsonAsync<GetVehicleListByBrandClientRequest, ApiResult<List<GetVehicleListClientResponse>>>(configuration["VehicleServer:GetVehicleListByBrand"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetVehicleListByBrand_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        public async  Task<List<string>> GetVehicleNianByPaiLiang(GetVehicleNianByPaiLiangClientRequest request)
        {
            var client = clientFactory.CreateClient("VehicleServer");
            ApiResult<List<string>> result = await
                client.GetAsJsonAsync<GetVehicleNianByPaiLiangClientRequest, ApiResult<List<string>>>(configuration["VehicleServer:GetVehicleNianByPaiLiang"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetVehicleNianByPaiLiang_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        public async  Task<List<GetVehicleSalesNameByNianClientResponse>> GetVehicleSalesNameByNian(GetVehicleSalesNameByNianClientRequest request)
        {
            var client = clientFactory.CreateClient("VehicleServer");
            ApiResult<List<GetVehicleSalesNameByNianClientResponse>> result = await
                client.GetAsJsonAsync<GetVehicleSalesNameByNianClientRequest, ApiResult<List<GetVehicleSalesNameByNianClientResponse>>>(configuration["VehicleServer:GetVehicleSalesNameByNian"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetVehicleSalesNameByNian_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }
    }
}
