using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Model.Vehicle;
using Ae.Shop.Api.Client.Request.Vehicle;
using Ae.Shop.Api.Client.Response.Vehicle;
using Ae.Shop.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients.VehicleServer
{
    public class VehicleClient: IVehicleClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly ApolloErpLogger<VehicleClient> logger;

        public VehicleClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<VehicleClient> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.logger = logger;
        }

        /// <summary>
        /// 获取用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserVehicleDto>> GetAllUserVehiclesAsync(AllUserVehiclesClientRequest request)
        {
            var client = httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<UserVehicleDto>> result =
                await client.GetAsJsonAsync<AllUserVehiclesClientRequest, ApiResult<List<UserVehicleDto>>>(
                    configuration["VehicleServer:GetAllUserVehiclesAsync"], request);
            if (result?.Code == ResultCode.Success)
            {
                return result.Data ?? new List<UserVehicleDto>();
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Error($"GetAllUserVehiclesAsync_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        /// <summary>
        /// 获取用户默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserVehicleDto> GetUserDefaultVehicleAsync(UserDefaultVehicleClientRequest request)
        {
            var client = httpClientFactory.CreateClient("VehicleServer");
            ApiResult<UserVehicleDto> result =
                await client.GetAsJsonAsync<UserDefaultVehicleClientRequest, ApiResult<UserVehicleDto>>(
                    configuration["VehicleServer:GetUserDefaultVehicleAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Error($"GetUserDefaultVehicleAsync_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        public async Task<List<VehicleBrandClientResponse>> GetVehicleBrandListAsync()
        {
            var client = httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<VehicleBrandClientResponse>> result =
                await client.GetAsJsonAsync<ApiResult<List<VehicleBrandClientResponse>>>(
                    configuration["VehicleServer:GetVehicleBrandList"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<VehicleBrandClientResponse>();
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Error($"GetVehicleBrandList_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        public async Task<List<VehicleListByBrandClientResponse>> GetVehicleListByBrandAsync(
            VehicleListByBrandClientRequest request)
        {
            var client = httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<VehicleListByBrandClientResponse>> result =
                await client
                    .GetAsJsonAsync<VehicleListByBrandClientRequest, ApiResult<List<VehicleListByBrandClientResponse>>>(
                        configuration["VehicleServer:GetVehicleListByBrand"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<VehicleListByBrandClientResponse>();
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Error($"GetVehicleListByBrand_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        public async Task<List<string>> GetPaiLiangByVehicleIdAsync(
            PaiLiangByVehicleIdClientRequest request)
        {
            var client = httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<string>> result =
                await client
                    .GetAsJsonAsync<PaiLiangByVehicleIdClientRequest, ApiResult<List<string>>>(
                        configuration["VehicleServer:GetPaiLiangByVehicleId"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<string>();
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Error($"GetPaiLiangByVehicleId_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        public async Task<List<string>> GetVehicleNianByPaiLiangAsync(
            VehicleNianByPaiLiangClientRequest request)
        {
            var client = httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<string>> result =
                await client
                    .GetAsJsonAsync<VehicleNianByPaiLiangClientRequest, ApiResult<List<string>>>(
                        configuration["VehicleServer:GetVehicleNianByPaiLiang"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<string>();
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Error($"GetVehicleNianByPaiLiang_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }

        public async Task<List<VehicleSalesNameByNianClientResponse>> GetVehicleSalesNameByNianAsync(
            VehicleSalesNameByNianClientRequest request)
        {
            var client = httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<VehicleSalesNameByNianClientResponse>> result =
                await client
                    .GetAsJsonAsync<VehicleSalesNameByNianClientRequest,
                        ApiResult<List<VehicleSalesNameByNianClientResponse>>>(
                        configuration["VehicleServer:GetVehicleSalesNameByNian"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<VehicleSalesNameByNianClientResponse>();
            }
            else
            {
                var errorMsg = result?.Message ?? "系统异常";
                logger.Error($"GetVehicleSalesNameByNian_Error {errorMsg}");
                throw new CustomException(errorMsg);
            }
        }
    }
}
