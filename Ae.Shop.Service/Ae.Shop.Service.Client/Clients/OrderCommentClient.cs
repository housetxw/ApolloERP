using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Model.OrderComment;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Clients
{
    public class OrderCommentClient : IOrderCommentClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        private readonly ApolloErpLogger<OrderCommentClient> _logger;

        public OrderCommentClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<OrderCommentClient> logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 获取门店评论总数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetShopCommentNumClientResponse>> GetShopCommentNum(BaseGetShopClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            try
            {
                ApiResult<GetShopCommentNumClientResponse> result = await client.GetAsJsonAsync<BaseGetShopClientRequest, ApiResult<GetShopCommentNumClientResponse>>(configuration["OrderCommentServer:GetShopCommentNum"], request);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        /// <summary>
        /// 获取门店评分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopScoreDto>> GetShopScore(ShopScoreClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            ApiResult<List<ShopScoreDto>> result =
                await client.PostAsJsonAsync<ShopScoreClientRequest, ApiResult<List<ShopScoreDto>>>(
                    configuration["OrderCommentServer:GetShopScore"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ShopScoreDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Info($"GetShopScore_Error {msg}");
                return new List<ShopScoreDto>();
            }
        }
    }
}
