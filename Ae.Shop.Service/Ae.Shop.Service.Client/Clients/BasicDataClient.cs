using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Response;
using Ae.Shop.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Clients
{
    public class BasicDataClient : IBasicDataClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<BasicDataClient> _logger;
        private IConfiguration configuration { get; }


        public BasicDataClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<BasicDataClient> logger
            )
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }


        /// <summary>
        /// 获取城市下的区县
        /// </summary>
        /// <returns></returns>
        public async Task<List<RegionChinaDTO>> RegionChinaReqByRegionId(RegionChinaReqByRegionIdClientRequest request)
        {
            var client = clientFactory.CreateClient("BasicDataServer");
            ApiResult<List<RegionChinaDTO>> result = await client.GetAsJsonAsync<RegionChinaReqByRegionIdClientRequest, ApiResult<List<RegionChinaDTO>>>(configuration["BasicDataServer:GetRegionChinaListByRegionId"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetRegionChinaListByRegionId_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }


        /// <summary>
        /// 获取所有城市
        /// </summary>
        /// <returns></returns>
        public async Task<List<RegionChinaDTO>> GetAllCitys(RegionChinaReqByLevelClientRequest request)
        {
            var client = clientFactory.CreateClient("BasicDataServer");
            ApiResult<List<RegionChinaDTO>> result = await client.GetAsJsonAsync<RegionChinaReqByLevelClientRequest, ApiResult<List<RegionChinaDTO>>>(configuration["BasicDataServer:GetRegionChinaListByLevel"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetRegionChinaListByLevel_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 根据经纬度获取当前位置
        /// </summary>
        /// <returns></returns>
        public async Task<GetPositionClientResponse> GetPosition(GetPositionClientRequest request)
        {
            _logger.Warn($"GetPosition {JsonConvert.SerializeObject(request)}");
            var client = clientFactory.CreateClient("BasicDataServer");

            ApiResult<GetPositionClientResponse> result = await client.GetAsJsonAsync<GetPositionClientRequest, ApiResult<GetPositionClientResponse>>(configuration["BasicDataServer:GetPosition"], request);
            if (result != null && result.Code == ResultCode.Success)
            {
                return result.Data ?? new GetPositionClientResponse();
            }
            else
            {
                _logger.Warn($"GetPosition_Error {result?.Message}");
                return new GetPositionClientResponse();
            }
        }

        /// <summary>
        /// 根据地址获取坐标
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetCoordinateClientResponse> GetCoordinate(GetCoordinateClientRequest request)
        {
            _logger.Warn($"GetCoordinate {JsonConvert.SerializeObject(request)}");
            var client = clientFactory.CreateClient("BasicDataServer");

            ApiResult<GetCoordinateClientResponse> result = await client.GetAsJsonAsync<GetCoordinateClientRequest, ApiResult<GetCoordinateClientResponse>>(configuration["BasicDataServer:GetCoordinate"], request);
            if (result != null && result.Code == ResultCode.Success)
            {
                return result.Data ?? new GetCoordinateClientResponse();
            }
            else
            {
                _logger.Warn($"GetPosition_Error {result?.Message}");
                return new GetCoordinateClientResponse();
            }
        }

        /// <summary>
        /// 获取ParentId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetParentRegionId(ParentRegionIdClientRequest request)
        {
            var client = clientFactory.CreateClient("BasicDataServer");

            ApiResult<string> result =
                await client.GetAsJsonAsync<ParentRegionIdClientRequest, ApiResult<string>>(
                    configuration["BasicDataServer:GetParentRegionId"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetParentRegionId_Error {msg}");
                throw new CustomException(msg);
            }
        }
    }
}
