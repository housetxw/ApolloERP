using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Model.Promotion;
using Ae.C.MiniApp.Api.Client.Request.Promotion;
using Ae.C.MiniApp.Api.Common.Exceptions;
using Ae.C.MiniApp.Api.Core.Request.Promotion;
using Ae.C.MiniApp.Api.Core.Response.Product;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Clients.Product
{
    public class PromotionClient : IPromotionClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ApolloErpLogger<PromotionClient> _logger;
        private readonly IConfiguration _configuration;

        public PromotionClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<PromotionClient> logger)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductPromotionDetailDto> GetProductPromotionDetail(ProductPromotionDetailClientRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiResult<ProductPromotionDetailDto> result = await client.GetAsJsonAsync<ProductPromotionDetailClientRequest, ApiResult<ProductPromotionDetailDto>>(
                _configuration["ProductServer:GetProductPromotionDetail"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetProductPromotionDetail_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据商品Pid查询促销信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetPromotionActivitByProductCodeListResponse>>> GetPromotionActivitByProductCodeList(GetPromotionActivitByProductCodeListRequest request)
        {
            var client = _clientFactory.CreateClient("ProductServer");
            ApiResult<List<GetPromotionActivitByProductCodeListResponse>> result = await client.PostAsJsonAsync<GetPromotionActivitByProductCodeListRequest, ApiResult<List<GetPromotionActivitByProductCodeListResponse>>>(
                _configuration["ProductServer:GetPromotionActivitByProductCodeList"], request);
            return result;
        }
    }
}
