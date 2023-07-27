using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Clients
{
    public class BaoYangClient : IBaoYangClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<BaoYangClient> _logger;
        private IConfiguration configuration { get; }

        public BaoYangClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<BaoYangClient> logger
        )
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 获取服务项目枚举
        /// </summary>
        /// <returns></returns>
        public async Task<List<ServiceTypeDTO>> GetServiceTypeEnum()
        {
            var client = clientFactory.CreateClient("BaoYangServer");
            ApiResult<List<ServiceTypeDTO>> result = await client.GetAsJsonAsync<ApiResult<List<ServiceTypeDTO>>>(configuration["BaoYangServer:GetServiceTypeEnum"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetRegionChinaListByRegionId_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

    }
}
