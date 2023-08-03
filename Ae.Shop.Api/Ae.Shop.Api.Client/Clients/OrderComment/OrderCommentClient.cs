using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Model;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Client.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients.OrderComment
{
    public class OrderCommentClient : IOrderCommentClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public OrderCommentClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiPagedResult<GetOrderCommentForShopDTO>> GetOrderCommentForShopList(ApiRequest<GetOrderCommentBaseClientRequest> request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            var response = await client.PostAsJsonAsync<ApiRequest<GetOrderCommentBaseClientRequest>, ApiPagedResult<GetOrderCommentForShopDTO>>(configuration["OrderCommentServer:GetOrderCommentForShopList"], request);
            return response;
        }

        public async Task<ApiResult<ReplyDetailClientResponse>> ReplyDetail(ReplyDetailClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            var response = await client.GetAsJsonAsync<ReplyDetailClientRequest, ApiResult<ReplyDetailClientResponse>>(configuration["OrderCommentServer:ReplyDetail"], request);
            return response;
        }

        public async Task<ApiResult> ReplyComment(ReplyCommentClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            var response = await client.PostAsJsonAsync<ReplyCommentClientRequest, ApiResult>(configuration["OrderCommentServer:ReplyComment"], request);
            return response;
        }

        public async Task<ApiResult<GetCommentListClientResponse>> GetCommentList(GetCommentListClientRequest request)
        {
            var client = clientFactory.CreateClient("OrderCommentServer");
            var response = await client.GetAsJsonAsync<GetCommentListClientRequest, ApiResult<GetCommentListClientResponse>>(configuration["OrderCommentServer:GetCommentList"], request);
            return response;
        }
    }
}
