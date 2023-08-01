using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.BaoYang.Service.Client.Model;
using Ae.BaoYang.Service.Client.Request;
using Ae.BaoYang.Service.Client.Response;
using Ae.BaoYang.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Client.Clients
{
    public class VehicleClient
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
        /// 根据车系，排量[年/款型Id]查询车型基本信息信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleBaseInfoDTO>> GetVehicleBaseInfoListAsync(VehicleInfoListClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<VehicleBaseInfoDTO>> result =
                await client
                    .GetAsJsonAsync<VehicleInfoListClientRequest, ApiResult<List<VehicleBaseInfoDTO>>>(
                        _configuration["VehicleServer:GetVehicleBaseInfoList"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<VehicleBaseInfoDTO>();
            }
            else
            {
                _logger.Error($"GetVehicleBaseInfoList_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        public async Task<List<VehicleConfigDTO>> GetVehicleConfigByTidList(VehicleConfigClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<List<VehicleConfigDTO>> result =
                await client.PostAsJsonAsync<VehicleConfigClientRequest, ApiResult<List<VehicleConfigDTO>>>(
                    _configuration["VehicleServer:GetVehicleConfigByTidList"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<VehicleConfigDTO>();
            }
            else
            {
                _logger.Error($"GetVehicleConfigByTidList_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据车系Id查详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<VehicleTypeDto> GetVehicleTypeById(VehicleTypeByIdClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<VehicleTypeDto> result =
                await client.GetAsJsonAsync<VehicleTypeByIdClientRequest, ApiResult<VehicleTypeDto>>(
                    _configuration["VehicleServer:GetVehicleTypeById"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"GetVehicleTypeById_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 原配轮胎尺寸
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OeTireSizeClientResponse> GetOeTireSizeByTid(OeTireSizeByTidClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<OeTireSizeClientResponse> result =
                await client.GetAsJsonAsync<OeTireSizeByTidClientRequest, ApiResult<OeTireSizeClientResponse>>(
                    _configuration["VehicleServer:GetOeTireSizeByTid"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"GetOeTireSizeByTid_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 原配轮胎查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> GetOeTirePidByTid(OeTirePidByTidClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<OeTirePidByTidClientResponse> result =
                await client.GetAsJsonAsync<OeTirePidByTidClientRequest, ApiResult<OeTirePidByTidClientResponse>>(
                    _configuration["VehicleServer:GetOeTirePidByTid"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data?.PidList ?? new List<string>();
            }
            else
            {
                _logger.Error($"GetOeTirePidByTid_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 查询用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserVehicleDto> GetUserVehicleByCarIdAsync(UserVehicleByCarIdClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("VehicleServer");
            ApiResult<UserVehicleDto> result =
                await client.GetAsJsonAsync<UserVehicleByCarIdClientRequest, ApiResult<UserVehicleDto>>(
                    _configuration["VehicleServer:GetUserVehicleByCarIdAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"GetUserVehicleByCarIdAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
