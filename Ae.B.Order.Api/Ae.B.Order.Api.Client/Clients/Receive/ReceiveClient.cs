using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Client.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Order.Api.Client.Clients.Receive
{
    public class ReceiveClient : IReceiveClient
    {
        private readonly IHttpClientFactory clientFactory;
        private IConfiguration configuration { get; }
        public ReceiveClient(IHttpClientFactory clientFactory, IConfiguration configuration
            )
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
        }


        /// <summary>
        /// 修改预约时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> ModifyReserveTime(ModifyReserveTimeClientRequest request)
        {
            var client = clientFactory.CreateClient("ReceiveServer");
            var response = await client.PostAsJsonAsync<ModifyReserveTimeClientRequest, ApiResult>(configuration["ReceiveServer:ModifyReserveTime"], request);
            return response;
        }
    }
}
