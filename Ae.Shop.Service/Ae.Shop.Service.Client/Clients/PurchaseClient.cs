using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Clients
{
    public class PurchaseClient : IPurchaseClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<PurchaseClient> _logger;
        private IConfiguration configuration { get; }

        public PurchaseClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<PurchaseClient> logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }

        public async Task<List<VenderDTO>> GetVenders()
        {
            var client = clientFactory.CreateClient("PurchaseServer");
            ApiResult<List<VenderDTO>> result = await client.GetAsJsonAsync<GetShopInfoRequest, ApiResult<List<VenderDTO>>>(configuration["PurchaseServer:GetVendersCommon"], new GetShopInfoRequest { });
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"GetVenders_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
