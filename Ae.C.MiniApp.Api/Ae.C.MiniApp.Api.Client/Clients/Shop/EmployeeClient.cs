using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Common.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Client.Clients.Shop
{
    public class EmployeeClient  : IEmployeeClient
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<EmployeeClient> _logger;
        private IConfiguration configuration { get; }
        public EmployeeClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<EmployeeClient> logger
            )
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 获取员工简单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetEmployeeClientResponse> GetEmployeeSimpleInfo(GetEmployeeInfoClientRequest request)
        {
            var client = clientFactory.CreateClient("ShopManageServer");
            ApiResult<GetEmployeeClientResponse> result = await client.GetAsJsonAsync<GetEmployeeInfoClientRequest, ApiResult<GetEmployeeClientResponse>>(configuration["ShopManageServer:GetEmployeeSimpleInfo"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetEmployeeSimpleInfo {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

    }
}
