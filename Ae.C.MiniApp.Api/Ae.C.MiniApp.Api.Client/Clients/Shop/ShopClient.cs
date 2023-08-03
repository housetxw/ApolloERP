using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Model.Shop;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Request.Shop;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Clients.Shop
{
    public class ShopClient : IShopClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<ShopClient> _logger;
        private IConfiguration configuration { get; }


        public ShopClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<ShopClient> logger
            )
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }
        /// <summary>
        /// 加盟
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> JoinInAsync(JoinInClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<bool> result = await client.PostAsJsonAsync<JoinInClientRequest, ApiResult<bool>>(configuration["ShopManageServer:JoinIn"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"JoinIn_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 查询附近门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<NearShopInfoDTO>> GetNearShopListAsync(Request.GetNearShopListClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiPagedResult<NearShopInfoDTO> result = await client.PostAsJsonAsync<Request.GetNearShopListClientRequest, ApiPagedResult<NearShopInfoDTO>>(configuration["ShopManageServer:GetNearShopListAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetNearShopListAsync_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }
        /// <summary>
        /// 获取门店详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetShopDetailClientResponse> GetShopDetailAsync(GetShopDetailClientRequest request)
        {
            _logger.Info($"GetShopDetailAsync request={JsonConvert.SerializeObject(request)}");
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<GetShopDetailClientResponse> result = await client.GetAsJsonAsync<GetShopDetailClientRequest, ApiResult<GetShopDetailClientResponse>>(configuration["ShopManageServer:GetShopDetailAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetShopDetailAsync_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }


        /// <summary>
        /// 获取城市下的区县门店数量
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopLocationDTO>> GetRegionByCityId(GetRegionByCityIdClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<List<ShopLocationDTO>> result = await client.GetAsJsonAsync<GetRegionByCityIdClientRequest, ApiResult<List<ShopLocationDTO>>>(configuration["ShopManageServer:GetRegionByCityId"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetRegionByCityId_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 获取公司简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CompanySimpleInfoClientResponse> MiniGetCompanyInfo(GetCompanyInfoClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<CompanySimpleInfoClientResponse> result = await client.GetAsJsonAsync<GetCompanyInfoClientRequest, ApiResult<CompanySimpleInfoClientResponse>>(configuration["ShopManageServer:MiniGetCompanyInfo"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"MiniGetCompanyInfo_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }
        /// <summary>
        /// 获取所有城市/热门城市
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetCityListClientResponse> GetChinaCitys()
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<GetCityListClientResponse> result = await client.GetAsJsonAsync<ApiResult<GetCityListClientResponse>>(configuration["ShopManageServer:GetAllCitys"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetAllCitys_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 查询门店服务上下架
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopOnSaleServiceDTO>> GetOnSaleShopServiceList(ShopServiceListRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<List<ShopOnSaleServiceDTO>> result =
                await client.PostAsJsonAsync<ShopServiceListRequest, ApiResult<List<ShopOnSaleServiceDTO>>>(
                    configuration["ShopManageServer:GetShopServiceListWithPID"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ShopOnSaleServiceDTO>();
            }
            else
            {
                _logger.Warn($"GetOnSaleShopServiceList_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }
        /// <summary>
        /// 获取门店开通的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceProjectDTO>> GetShopServiceProject(BaseShopClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<List<ShopServiceProjectDTO>> result = await client.GetAsJsonAsync<BaseShopClientRequest, ApiResult<List<ShopServiceProjectDTO>>>(configuration["ShopManageServer:GetShopServiceTypeAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetShopServiceProject_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 定位门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetOptimalShopClientResponse> GetOptimalShop(GetOptimalShopClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<GetOptimalShopClientResponse> result = await client.GetAsJsonAsync<GetOptimalShopClientRequest, ApiResult<GetOptimalShopClientResponse>>(configuration["ShopManageServer:GetOptimalShop"], request);
            if (result.IsNotNullSuccess())
            {
                return result.GetSuccessData();
            }
            else
            {
                _logger.Warn($"GetOptimalShop_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }
    }
}
