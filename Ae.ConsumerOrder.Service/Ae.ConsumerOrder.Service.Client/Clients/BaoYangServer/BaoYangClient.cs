using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model;
using Ae.ConsumerOrder.Service.Client.Model.BaoYang;
using Ae.ConsumerOrder.Service.Client.Request;
using Ae.ConsumerOrder.Service.Client.Request.BaoYang;

namespace Ae.ConsumerOrder.Service.Client.Clients
{
    public class BaoYangClient : IBaoYangClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }

        public BaoYangClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }

        public async Task<ApiResult<List<PartProductRefDTO>>> GetAdaptiveProductByPartTypeAndCarId(AdaptiveProductByPartTypeAndCarIdRequest request)
        {
            var client = clientFactory.CreateClient("BaoYangServer");
            var response = await client.PostAsJsonAsync<AdaptiveProductByPartTypeAndCarIdRequest, ApiResult<List<PartProductRefDTO>>>(configuration["BaoYangServer:GetAdaptiveProductByPartTypeAndCarId"], request);
            return response;
        }


        public async Task<ApiResult<InstallServiceByProductDTO>> GetInstallServiceByProduct(InstallServiceByProductRequest request)
        {
            var client = clientFactory.CreateClient("BaoYangServer");
            var response = await client.PostAsJsonAsync<InstallServiceByProductRequest, ApiResult<InstallServiceByProductDTO>>(configuration["BaoYangServer:GetInstallServiceByProduct"], request);
            return response;
        }
    }
}
