using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Model.Promotion;
using Ae.B.Product.Api.Client.Model.ShopProduc;
using Ae.B.Product.Api.Client.Request.Promotion;
using Ae.B.Product.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Client.Clients
{
    public class PromotionClient : IPromotionClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<PromotionClient> _logger;

        public PromotionClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<PromotionClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 活动列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<PromotionActivityListDto>> SearchPromotionActivity(
            SearchPromotionActivityClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiPagedResult<PromotionActivityListDto> result =
                await client
                    .GetAsJsonAsync<SearchPromotionActivityClientRequest, ApiPagedResult<PromotionActivityListDto>>(
                        _configuration["ProductServer:SearchPromotionActivity"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"SearchPromotionActivity_Error Result={JsonConvert.SerializeObject(result)}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 新增促销活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> AddPromotionActivity(AddPromotionActivityClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<string> result =
                await client
                    .PostAsJsonAsync<AddPromotionActivityClientRequest, ApiResult<string>>(
                        _configuration["ProductServer:AddPromotionActivity"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"AddPromotionActivity_Error Result={JsonConvert.SerializeObject(result)}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PromotionActivityDetailDto> GetPromotionActivityDetail(PromotionActivityDetailClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<PromotionActivityDetailDto> result =
                await client
                    .GetAsJsonAsync<PromotionActivityDetailClientRequest, ApiResult<PromotionActivityDetailDto>>(
                        _configuration["ProductServer:GetPromotionActivityDetail"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetPromotionActivityDetail_Error Result={JsonConvert.SerializeObject(result)}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AuditPromotionActivity(AuditPromotionActivityClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result = await client.PostAsJsonAsync<AuditPromotionActivityClientRequest, ApiResult<bool>>(
                _configuration["ProductServer:AuditPromotionActivity"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"AuditPromotionActivity_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 查询单个门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopProductDto> GetShopProductByCode(string shopProductCode)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<ShopProductDto> result = await client.GetAsJsonAsync<object, ApiResult<ShopProductDto>>(
                _configuration["ProductServer:GetShopProductByCode"], new { shopProductCode = shopProductCode });
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"GetShopProductByCode_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 结束活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> FinishPromotion(FinishPromotionClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("ProductServer");
            ApiResult<bool> result = await client.PostAsJsonAsync<FinishPromotionClientRequest, ApiResult<bool>>(
                _configuration["ProductServer:FinishPromotion"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Info($"FinishPromotion_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
