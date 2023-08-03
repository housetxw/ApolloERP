using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.FMS.Service.Core.Request;
using Ae.FMS.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.FMS.Service.Client.Clients
{
    public class ApolloErpWmsClient : IApolloErpWMSClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private IConfiguration _configuration { get; }

        public ApolloErpWmsClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<ApiResult<List<GetOrderOccupyShopProductPurchaseResponse>>> GetOrderOccupyShopProductPurchaseInfo(GetOrderOccupyShopProductPurchaseInfoReqeust reqeust)
        {
            var client = _clientFactory.CreateClient("ApolloErpWMSServer");

            var response =
                await client.PostAsJsonAsync<GetOrderOccupyShopProductPurchaseInfoReqeust, ApiResult<List<GetOrderOccupyShopProductPurchaseResponse>>>(
                    _configuration["ApolloErpWMSServer:GetOrderOccupyShopProductPurchaseInfo"], reqeust);

            return response;
        }
    }
}
