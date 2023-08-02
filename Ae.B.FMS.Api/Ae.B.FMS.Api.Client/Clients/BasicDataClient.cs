using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Client.Request;
using Ae.B.FMS.Api.Client;
using Ae.B.FMS.Api.Common.Exceptions;
using Ae.B.FMS.Api.Client.Interface;
using Ae.B.FMS.Api.Client.Model;


namespace Ae.B.FMS.Api.Client.Clients
{
  public  class BasicDataClient: IBasicDataClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<BasicDataClient> _logger;

        public BasicDataClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<BasicDataClient> logger)
        {
            this._httpClientFactory = httpClientFactory;
            this._configuration = configuration;
            this._logger = logger;
        }

        public async  Task<List<GetRegionChinaListByRegionIdDTO>> GetRegionChinaListByRegionId(GetRegionChinaListByRegionIdClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BasicDataServer");
            ApiResult<List<GetRegionChinaListByRegionIdDTO>> result =
                await client
                    .GetAsJsonAsync<GetRegionChinaListByRegionIdClientRequest,
                        ApiResult<List<GetRegionChinaListByRegionIdDTO>>>(
                        _configuration["BasicDataServer:GetRegionChinaListByRegionId"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<GetRegionChinaListByRegionIdDTO>();
            }
            else
            {
                _logger.Error($"GetRegionChinaListByRegionId_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
    }
}
