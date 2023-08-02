using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Model;
using Ae.B.Product.Api.Client.Model.Vehicle;
using Ae.B.Product.Api.Client.Request;
using Ae.B.Product.Api.Client.Request.Vehicle;
using Ae.B.Product.Api.Client.Response.Vehicle;
using Ae.B.Product.Api.Common.Exceptions;

namespace Ae.B.Product.Api.Client.Clients
{
    public class VehicleClient : IVehicleClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<VehicleClient> _logger;

        public VehicleClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<VehicleClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<VehicleInfo>> GetVehicleInfoList(VehicleInfoListRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<VehicleInfo>> result =
                await client.GetAsJsonAsync<VehicleInfoListRequest, ApiResult<List<VehicleInfo>>>(
                    _configuration["VehicleServer:GetVehicleInfoList"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<VehicleInfo>();
            }
            else
            {
                _logger.Error($"GetVehicleInfoList_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<List<VehicleBrandClientResponse>> GetVehicleBrandListAsync()
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<VehicleBrandClientResponse>> result =
                await client.GetAsJsonAsync<ApiResult<List<VehicleBrandClientResponse>>>(
                    _configuration["VehicleServer:GetVehicleBrandList"], null);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<VehicleBrandClientResponse>();
            }
            else
            {
                _logger.Error($"GetVehicleBrandList_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<List<VehicleListByBrandClientResponse>> GetVehicleListByBrandAsync(
            VehicleListByBrandClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<VehicleListByBrandClientResponse>> result =
                await client
                    .GetAsJsonAsync<VehicleListByBrandClientRequest, ApiResult<List<VehicleListByBrandClientResponse>>>(
                        _configuration["VehicleServer:GetVehicleListByBrand"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<VehicleListByBrandClientResponse>();
            }
            else
            {
                _logger.Error($"GetVehicleListByBrand_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<List<string>> GetPaiLiangByVehicleIdAsync(
            PaiLiangByVehicleIdClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<string>> result =
                await client
                    .GetAsJsonAsync<PaiLiangByVehicleIdClientRequest, ApiResult<List<string>>>(
                        _configuration["VehicleServer:GetPaiLiangByVehicleId"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<string>();
            }
            else
            {
                _logger.Error($"GetPaiLiangByVehicleId_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<List<string>> GetVehicleNianByPaiLiangAsync(
            VehicleNianByPaiLiangClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<string>> result =
                await client
                    .GetAsJsonAsync<VehicleNianByPaiLiangClientRequest, ApiResult<List<string>>>(
                        _configuration["VehicleServer:GetVehicleNianByPaiLiang"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<string>();
            }
            else
            {
                _logger.Error($"GetVehicleNianByPaiLiang_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<List<VehicleSalesNameByNianClientResponse>> GetVehicleSalesNameByNianAsync(
            VehicleSalesNameByNianClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<VehicleSalesNameByNianClientResponse>> result =
                await client
                    .GetAsJsonAsync<VehicleSalesNameByNianClientRequest,
                        ApiResult<List<VehicleSalesNameByNianClientResponse>>>(
                        _configuration["VehicleServer:GetVehicleSalesNameByNian"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<VehicleSalesNameByNianClientResponse>();
            }
            else
            {
                _logger.Error($"GetVehicleSalesNameByNian_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据车系，排量[年/款型Id]查询车型基本信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleBaseInfo>> GetVehicleBaseInfoList(VehicleInfoListRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<VehicleBaseInfo>> result =
                await client.GetAsJsonAsync<VehicleInfoListRequest, ApiResult<List<VehicleBaseInfo>>>(
                    _configuration["VehicleServer:GetVehicleBaseInfoList"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<VehicleBaseInfo>();
            }
            else
            {
                _logger.Error($"GetVehicleBaseInfoList_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据Tids查询车型基本信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleBaseInfo>> GetVehicleBaseInfoListByTids(
            VehicleBaseInfoListByTidsClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<VehicleBaseInfo>> result =
                await client.PostAsJsonAsync<VehicleBaseInfoListByTidsClientRequest, ApiResult<List<VehicleBaseInfo>>>(
                    _configuration["VehicleServer:GetVehicleBaseInfoListByTids"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<VehicleBaseInfo>();
            }
            else
            {
                _logger.Error($"GetVehicleBaseInfoListByTids_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 删除车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteVehicleByTid(DeleteVehicleByTidClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<DeleteVehicleByTidClientRequest, ApiResult<bool>>(
                    _configuration["VehicleServer:DeleteVehicleByTid"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"DeleteVehicleByTid_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据车型查询Tids
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> GetTidsByVehicle(TidsByVehicleClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<string>> result =
                await client.GetAsJsonAsync<TidsByVehicleClientRequest, ApiResult<List<string>>>(
                    _configuration["VehicleServer:GetTidsByVehicle"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<string>();
            }
            else
            {
                _logger.Error($"GetTidsByVehicle_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
