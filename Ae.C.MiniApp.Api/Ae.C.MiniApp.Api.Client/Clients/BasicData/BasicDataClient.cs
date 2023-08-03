using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Clients.BasicData
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
        /// 获取省市区地址
        /// </summary>
        /// <returns></returns>
        public async Task<GetRegionChinaClientResponse> GetAllRegionChinaWithThreeLayer()
        {
            var client = clientFactory.CreateClient("BasicDataServer");
            ApiResult<GetRegionChinaClientResponse> result = await client.GetAsJsonAsync<ApiResult<GetRegionChinaClientResponse>>(configuration["BasicDataServer:GetAllRegionChinaWithThreeLayer"],null);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new GetRegionChinaClientResponse();
            }
            else
            {
                _logger.Warn($"GetAllRegionChinaWithThreeLayer_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }
        /// <summary>
        /// 获取所有城市
        /// </summary>
        /// <returns></returns>
        public async Task<List<RegionChinaClientDTO>> GetAllCitys(RegionChinaReqByLevelClientRequest request)
        {
            var client = clientFactory.CreateClient("BasicDataServer");
            ApiResult<List<RegionChinaClientDTO>> result = await client.GetAsJsonAsync<RegionChinaReqByLevelClientRequest,ApiResult<List<RegionChinaClientDTO>>>(configuration["BasicDataServer:GetRegionChinaListByLevel"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<RegionChinaClientDTO>();
            }
            else
            {
                _logger.Warn($"GetRegionChinaListByLevel_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }


        /// <summary>
        /// 获取省市区三级地址
        /// </summary>
        /// <returns></returns>
        public async Task<ThreeLevelRegionChinaClientResponse> GetThreeLevelRegionChina()
        {
            var client = clientFactory.CreateClient("BasicDataServer");
            ApiResult<ThreeLevelRegionChinaClientResponse> result = await client.GetAsJsonAsync<ApiResult<ThreeLevelRegionChinaClientResponse>>(configuration["BasicDataServer:GetThreeLevelRegionChina"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new ThreeLevelRegionChinaClientResponse();
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
            var client = clientFactory.CreateClient("BasicDataServer");
            ApiResult<GetPositionClientResponse> result = await client.GetAsJsonAsync<GetPositionClientRequest,ApiResult<GetPositionClientResponse>>(configuration["BasicDataServer:GetPosition"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new GetPositionClientResponse();
            }
            else
            {
                _logger.Warn($"GetPosition_Error {result?.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
