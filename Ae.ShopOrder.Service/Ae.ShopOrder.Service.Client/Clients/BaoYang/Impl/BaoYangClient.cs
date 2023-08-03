using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Model.BaoYang;
using Ae.ShopOrder.Service.Core.Request.BaoYang;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.BaoYang.Impl
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

        /// <summary>
        /// 获取服务项目枚举
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<ServiceTypeEnumDTO>>> GetServiceTypeEnum()
        {
            var client = clientFactory.CreateClient("BaoYangServer");
            var response = await client.GetAsJsonAsync<ApiResult<List<ServiceTypeEnumDTO>>>(configuration["BaoYangServer:GetServiceTypeEnum"],null);
            return response;
        }

        public async Task<ApiResult<List<InstallServiceDetailVo>>> GetAdditionalPriceByServiceId(AdditionalPriceByServiceIdRequest request)
        {
            var client = clientFactory.CreateClient("BaoYangServer");
            var response = await client.PostAsJsonAsync<AdditionalPriceByServiceIdRequest, ApiResult<List<InstallServiceDetailVo>>>(configuration["BaoYangServer:GetAdditionalPriceByServiceId"], request);
            return response;
        }
    }
}
