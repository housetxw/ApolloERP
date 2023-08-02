using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Model;
using Ae.B.Order.Api.Client.Request;
using Ae.B.Order.Api.Client.Response;
using Ae.B.Order.Api.Core.Request.OrderQuery;
using Ae.B.Order.Api.Core.Response.OrderQuery;

namespace Ae.B.Order.Api.Client.Clients
{
    public class ConsumerOrderClient : IConsumerOrderClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public ConsumerOrderClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<GetOrderDetailForBossClientResponse>> GetOrderDetailForBoss(GetOrderDetailForBossClientRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.GetAsJsonAsync<GetOrderDetailForBossClientRequest, ApiResult<GetOrderDetailForBossClientResponse>>(configuration["ConsumerOrderServer:GetOrderDetailForBoss"], request);
            return response;
        }

        public async Task<ApiResult> CheckOrder(CheckOrderClientRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.PostAsJsonAsync<CheckOrderClientRequest, ApiResult>(configuration["ConsumerOrderServer:CheckOrder"], request);
            return response;
        }

        public async Task<ApiResult<AppendSubmitOrderClientResponse>> AppendSubmitOrder(ApiRequest<AppendSubmitOrderClientRequest> request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.PostAsJsonAsync<ApiRequest<AppendSubmitOrderClientRequest>, ApiResult<AppendSubmitOrderClientResponse>>(configuration["ConsumerOrderServer:AppendSubmitOrder"], request);
            return response;
        }

        public async Task<ApiPagedResult<OrderLogDTO>> GetOrderLogList(GetOrderLogListClientRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.GetAsJsonAsync<GetOrderLogListClientRequest, ApiPagedResult<OrderLogDTO>>(configuration["ConsumerOrderServer:GetOrderLogList"], request);
            return response;
        }

        public async Task<ApiResult<GetOrderConfirmResponse>> GetOrderConfirm(ApiRequest<GetOrderConfirmRequest> request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.PostAsJsonAsync<GetOrderConfirmRequest, ApiResult<GetOrderConfirmResponse>>(configuration["ConsumerOrderServer:GetOrderConfirm"], request.Data);
            return response;
        }

        public async Task<ApiResult<TrialCalcOrderAmountResponse>> TrialCalcOrderAmount(TrialCalcOrderAmountRequest request)
        {
            var client = clientFactory.CreateClient("ConsumerOrderServer");
            var response = await client.PostAsJsonAsync<TrialCalcOrderAmountRequest, ApiResult<TrialCalcOrderAmountResponse>>(configuration["ConsumerOrderServer:TrialCalcOrderAmount"],request);
            return response;
        }
    }
}
