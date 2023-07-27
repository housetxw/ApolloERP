using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Model.ShopOrder;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Clients
{
    public class ShopOrderClient: IShopOrderClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<ShopOrderClient> _logger;
        private IConfiguration configuration { get; }


        public ShopOrderClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<ShopOrderClient> logger
        )
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }

        public async  Task<List<OrderDispatchClientDTO>> GetOrderDispatch(GetOrderDispatchClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopOrderServer");
            ApiResult<List<OrderDispatchClientDTO>> result = await client.PostAsJsonAsync<GetOrderDispatchClientRequest, ApiResult<List<OrderDispatchClientDTO>>>(configuration["ShopOrderServer:GetOrderDispatch"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"GetProductsByProductCodes_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
