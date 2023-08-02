using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using Ae.Product.Service.Client.Interface;
using Ae.Product.Service.Client.Request.BaoYang;
using Ae.Product.Service.Client.Response.BaoYang;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Common.Exceptions;
using Ae.Product.Service.Client.Model.BaoYang;

namespace Ae.Product.Service.Client.Clients
{
    public class BaoYangClient : IBaoYangClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<BaoYangClient> _logger;

        public BaoYangClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<BaoYangClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 轮胎适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TireServiceListResponse> GetTireCategoryListAllProductAsync(TireCategoryListRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<TireServiceListResponse> result =
                await client.PostAsJsonAsync<TireCategoryListRequest, ApiResult<TireServiceListResponse>>(
                    _configuration["BaoYangServer:GetTireCategoryListAllProductAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetTireCategoryListAllProductAsync_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 保养适配首页接口 - 返回所有适配商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageModelDto>> GetBaoYangPackagesAllProductAsync(
            GetBaoYangPackagesRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangPackageModelDto>> result =
                await client.PostAsJsonAsync<GetBaoYangPackagesRequest, ApiResult<List<BaoYangPackageModelDto>>>(
                    _configuration["BaoYangServer:GetBaoYangPackagesAllProductAsync"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<BaoYangPackageModelDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetBaoYangPackagesAllProductAsync_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 获取GetBaoYangTypeConfig
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangTypeConfigDto>> GetBaoYangTypeConfig()
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangTypeConfigDto>> result =
                await client.GetAsJsonAsync<ApiResult<List<BaoYangTypeConfigDto>>>(
                    _configuration["BaoYangServer:GetBaoYangTypeConfig"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<BaoYangTypeConfigDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetBaoYangTypeConfig_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 产品新增/更新 自动更新配件号关联Pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AutoInsertPartsAssociation(AutoInsertPartsAssociationClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<AutoInsertPartsAssociationClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:AutoInsertPartsAssociation"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string msg = result?.Message ?? "系统异常";
                _logger.Info($"AutoInsertPartsAssociation_Error {msg}");
                throw new CustomException(msg);
            }
        }
    }
}
