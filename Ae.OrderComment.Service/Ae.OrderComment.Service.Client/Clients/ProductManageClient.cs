using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Interface;
using Ae.OrderComment.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Client.Clients
{
    public class ProductManageClient : IProductManageClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public ProductManageClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<bool>> BatchUpdateProduct(BatchUpdateProductRequest request)
        {
            var client = clientFactory.CreateClient("ProductServer");
            var response = await client.PostAsJsonAsync<BatchUpdateProductRequest, ApiResult<bool>>(configuration["ProductServer:BatchUpdateProduct"], request);
            return response;
        }
    }
}
