using ApolloErp.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ae.OrderComment.Service.Client.Response;
using Ae.OrderComment.Service.Client.Request;
using Ae.OrderComment.Service.Client.Model;
using Ae.OrderComment.Service.Client.Interface;

namespace Ae.OrderComment.Service.Client.Clients
{
    public class ShopManageClient : IShopManageClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public ShopManageClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiPagedResult<ShopSimpleInfoClientDTO>> GetShopListAsync(GetShopListClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiPagedResult<ShopSimpleInfoClientDTO> result =
                await client.PostAsJsonAsync<GetShopListClientRequest, ApiPagedResult<ShopSimpleInfoClientDTO>>(
                    configuration["ShopManageServer:GetShopListAsync"], request);
            return result;
        }

        /// <summary>
        /// 获取门店简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetShopInfoClientResponse>> GetShopSimpleInfoAsync(GetShopSimpleInfoClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<GetShopInfoClientResponse> result = await client.GetAsJsonAsync<GetShopSimpleInfoClientRequest, ApiResult<GetShopInfoClientResponse>>(configuration["ShopManageServer:GetShopSimpleInfoAsync"], request);
            return result;
        }

        public async Task<ApiResult> UpdateShopScoreAsync(UpdateShopScoreByShopIdsRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            var result = await client.PostAsJsonAsync<UpdateShopScoreByShopIdsRequest, ApiResult>(configuration["ShopManageServer:UpdateShopScoreAsync"], request);
            return result;
        }
    }
}
