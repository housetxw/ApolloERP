using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Log;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Clients.Interface;
using Ae.C.MiniApp.Api.Client.Model.Vehicle;
using Ae.C.MiniApp.Api.Client.Request.Vehicle;

namespace Ae.C.MiniApp.Api.Client.Clients.Vehicle
{
    public class VehicleClient : IVehicleClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<VehicleClient> _logger;

        public VehicleClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<VehicleClient> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
            this._logger = logger;
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
                _logger.Info($"GetVehicleBrandList_Error {result.Message}");
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
                _logger.Info($"GetVehicleListByBrand_Error {result.Message}");
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
                _logger.Info($"GetPaiLiangByVehicleId_Error {result.Message}");
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
                _logger.Info($"GetVehicleNianByPaiLiang_Error {result.Message}");
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
                _logger.Info($"GetVehicleSalesNameByNian_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<List<VehicleSearchByNameClientResponse>> VehicleSearchByNameAsync(
            VehicleSearchByNameClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<VehicleSearchByNameClientResponse>> result =
                await client
                    .GetAsJsonAsync<VehicleSearchByNameClientRequest,
                        ApiResult<List<VehicleSearchByNameClientResponse>>>(
                        _configuration["VehicleServer:VehicleSearchByName"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<VehicleSearchByNameClientResponse>();
            }
            else
            {
                _logger.Info($"VehicleSearchByName_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<string> AddUserCarAsync(AddUserCarClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<string> result =
                await client.PostAsJsonAsync<AddUserCarClientRequest, ApiResult<string>>(
                    _configuration["VehicleServer:AddUserCarAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"AddUserCarAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<bool> SetUserDefaultVehicleAsync(SetUserDefaultVehicleClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<SetUserDefaultVehicleClientRequest, ApiResult<bool>>(
                    _configuration["VehicleServer:SetUserDefaultVehicleAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"SetUserDefaultVehicleAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<List<UserVehicleDTO>> GetAllUserVehiclesAsync(AllUserVehiclesClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<UserVehicleDTO>> result =
                await client.GetAsJsonAsync<AllUserVehiclesClientRequest, ApiResult<List<UserVehicleDTO>>>(
                    _configuration["VehicleServer:GetAllUserVehiclesAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetAllUserVehiclesAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<UserVehicleDTO> GetUserVehicleByCarIdAsync(UserVehicleByCarIdClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<UserVehicleDTO> result =
                await client.GetAsJsonAsync<UserVehicleByCarIdClientRequest, ApiResult<UserVehicleDTO>>(
                    _configuration["VehicleServer:GetUserVehicleByCarIdAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetUserVehicleByCarIdAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<UserVehicleDTO> GetUserDefaultVehicleAsync(UserDefaultVehicleClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<UserVehicleDTO> result =
                await client.GetAsJsonAsync<UserDefaultVehicleClientRequest, ApiResult<UserVehicleDTO>>(
                    _configuration["VehicleServer:GetUserDefaultVehicleAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetUserDefaultVehicleAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<bool> DeleteUserCarAsync(DeleteUserCarClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<DeleteUserCarClientRequest, ApiResult<bool>>(
                    _configuration["VehicleServer:DeleteUserCarAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"DeleteUserCarAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<bool> EditUserCarAsync(EditUserCarClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditUserCarClientRequest, ApiResult<bool>>(
                    _configuration["VehicleServer:EditUserCarAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"EditUserCarAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<List<InsuranceCompanyDto>> GetInsuranceCompanyListAsync(InsuranceCompanyListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<InsuranceCompanyDto>> result =
                await client.GetAsJsonAsync<InsuranceCompanyListClientRequest, ApiResult<List<InsuranceCompanyDto>>>(
                    _configuration["VehicleServer:GetInsuranceCompanyListAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<InsuranceCompanyDto>();
            }
            else
            {
                _logger.Info($"GetInsuranceCompanyListAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据车系，排量[年/款型Id]查询车型基本信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleBaseInfoDto>> GetVehicleBaseInfoList(VehicleInfoListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<VehicleBaseInfoDto>> result =
                await client
                    .GetAsJsonAsync<VehicleInfoListClientRequest, ApiResult<List<VehicleBaseInfoDto>>>(
                        _configuration["VehicleServer:GetVehicleBaseInfoList"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<VehicleBaseInfoDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetVehicleBaseInfoList_Error {msg}");
                throw new CustomException(msg);
            }
        }
    }
}
