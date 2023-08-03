using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Client.Inteface;
using Ae.Receive.Service.Client.Model.BaoYang;
using Ae.Receive.Service.Common.Exceptions;

namespace Ae.Receive.Service.Client.Clients
{
    public class BaoYangClient : IBaoYangClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<BaoYangClient> _logger;

        public BaoYangClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<BaoYangClient> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
            this._logger = logger;
        }

        /// <summary>
        /// 获取服务项目
        /// </summary>
        /// <returns></returns>
        public async Task<List<ServiceTypeEnumDto>> GetServiceTypeEnum()
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<ServiceTypeEnumDto>> result =
                await client.GetAsJsonAsync<ApiResult<List<ServiceTypeEnumDto>>>(
                    _configuration["BaoYangServer:GetServiceTypeEnum"], null);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<ServiceTypeEnumDto>();
            }
            else
            {
                _logger.Info($"GetServiceTypeEnum_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
