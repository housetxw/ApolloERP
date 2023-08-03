using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Model.BaoYang;
using Ae.Shop.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients.BaoYang
{
    public class BaoYangClient: IBaoYangClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly ApolloErpLogger<BaoYangClient> logger;

        public BaoYangClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<BaoYangClient> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.logger = logger;
        }

        /// <summary>
        /// 获取服务项目
        /// </summary>
        /// <returns></returns>
        public async Task<List<ServiceTypeEnumDto>> GetServiceTypeEnum()
        {
            var client = httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<ServiceTypeEnumDto>> result =
                await client.GetAsJsonAsync<ApiResult<List<ServiceTypeEnumDto>>>(
                    configuration["BaoYangServer:GetServiceTypeEnum"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<ServiceTypeEnumDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                logger.Info($"GetServiceTypeEnum_Error {msg}");
                throw new CustomException(msg);
            }
        }
    }
}
