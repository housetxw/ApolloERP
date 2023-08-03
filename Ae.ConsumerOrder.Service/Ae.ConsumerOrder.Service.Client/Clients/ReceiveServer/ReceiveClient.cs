using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Request.Receive;

namespace Ae.ConsumerOrder.Service.Client.Clients.ReceiveServer
{
    public class ReceiveClient : IReceiveClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public ReceiveClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }


        public async Task<ApiResult<bool>> CheckTheDayReserveWithUserCarId(CheckReserveTimeRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.GetAsJsonAsync<CheckReserveTimeRequest, ApiResult<bool>>(configuration["ReceiveServer:CheckTheDayReserveWithUserCarId"], request);
            return response;
        }

        public async Task<ApiResult> AddShopReserveV3(AddReserveV3Request request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<AddReserveV3Request, ApiResult>(configuration["ReceiveServer:AddShopReserveV3"], request);
            return response;
        }
    }
}
