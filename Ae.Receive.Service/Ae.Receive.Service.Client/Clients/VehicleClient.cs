using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Client.Inteface;
using Ae.Receive.Service.Client.Model;
using Ae.Receive.Service.Client.Model.Vehicle;
using Ae.Receive.Service.Client.Request;
using Ae.Receive.Service.Client.Request.Vehicle;
using Ae.Receive.Service.Common.Exceptions;

namespace Ae.Receive.Service.Client.Clients
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

        /// <summary>
        /// 根据CarId查询车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserVehicleDTO> GetUserVehicleByCarIdAsync(UserVehicleByCarIdClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<UserVehicleDTO> result =
                await client.GetAsJsonAsync<UserVehicleByCarIdClientRequest, ApiResult<UserVehicleDTO>>(
                    _configuration["VehicleServer:GetUserVehicleByCarIdAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetUserVehicleByCarIdAsync_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 添加用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> AddUserCarAsync(AddUserCarClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<string> result =
                await client.PostAsJsonAsync<AddUserCarClientRequest, ApiResult<string>>(
                    _configuration["VehicleServer:AddUserCarAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"AddUserCarAsync_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 得到用户默认车辆信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserVehicleDTO> GetUserDefaultVehicleAsync(UserDefaultVehicleClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<UserVehicleDTO> result =
                await client.GetAsJsonAsync<UserDefaultVehicleClientRequest, ApiResult<UserVehicleDTO>>(
                    _configuration["VehicleServer:GetUserDefaultVehicleAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetUserDefaultVehicleAsync_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 编辑车辆
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditUserCarAsync(EditUserCarClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditUserCarClientRequest, ApiResult<bool>>(
                    _configuration["VehicleServer:EditUserCarAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"EditUserCarAsync_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 用户车辆档案
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserCarFileDto> GetUserCarFile(UserCarFileClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<UserCarFileDto> result =
                await client.GetAsJsonAsync<UserCarFileClientRequest, ApiResult<UserCarFileDto>>(
                    _configuration["VehicleServer:GetUserCarFile"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetUserCarFile_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 更新车辆部位检查信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCarPartCheckResult(UpdateCarPartCheckResultClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<UpdateCarPartCheckResultClientRequest, ApiResult<bool>>(
                    _configuration["VehicleServer:UpdateCarPartCheckResult"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"UpdateCarPartCheckResult_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 车辆部位异常记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserCarComponentsResultDto>> GetUserCarComponentsErrorCheck(UserCarComponentsErrorClientCheckRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<UserCarComponentsResultDto>> result =
                await client.GetAsJsonAsync<UserCarComponentsErrorClientCheckRequest, ApiResult<List<UserCarComponentsResultDto>>>(
                    _configuration["VehicleServer:GetUserCarComponentsErrorCheck"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<UserCarComponentsResultDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetUserCarComponentsErrorCheck_Error {msg}");
                throw new CustomException(msg);
            }
        }
    }
}
